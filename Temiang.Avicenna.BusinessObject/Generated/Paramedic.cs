/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/4/2023 11:12:26 AM
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
    abstract public class esParamedicCollection : esEntityCollectionWAuditLog
    {
        public esParamedicCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ParamedicCollection";
        }

        #region Query Logic
        protected void InitQuery(esParamedicQuery query)
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
            this.InitQuery(query as esParamedicQuery);
        }
        #endregion

        virtual public Paramedic DetachEntity(Paramedic entity)
        {
            return base.DetachEntity(entity) as Paramedic;
        }

        virtual public Paramedic AttachEntity(Paramedic entity)
        {
            return base.AttachEntity(entity) as Paramedic;
        }

        virtual public void Combine(ParamedicCollection collection)
        {
            base.Combine(collection);
        }

        new public Paramedic this[int index]
        {
            get
            {
                return base[index] as Paramedic;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Paramedic);
        }
    }

    [Serializable]
    abstract public class esParamedic : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esParamedicQuery GetDynamicQuery()
        {
            return null;
        }

        public esParamedic()
        {
        }

        public esParamedic(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String paramedicID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paramedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(paramedicID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paramedicID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paramedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(paramedicID);
        }

        private bool LoadByPrimaryKeyDynamic(String paramedicID)
        {
            esParamedicQuery query = this.GetDynamicQuery();
            query.Where(query.ParamedicID == paramedicID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String paramedicID)
        {
            esParameters parms = new esParameters();
            parms.Add("ParamedicID", paramedicID);
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
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "ParamedicName": this.str.ParamedicName = (string)value; break;
                        case "ParamedicInitial": this.str.ParamedicInitial = (string)value; break;
                        case "DateOfBirth": this.str.DateOfBirth = (string)value; break;
                        case "SRParamedicType": this.str.SRParamedicType = (string)value; break;
                        case "SRParamedicStatus": this.str.SRParamedicStatus = (string)value; break;
                        case "SRReligion": this.str.SRReligion = (string)value; break;
                        case "SRNationality": this.str.SRNationality = (string)value; break;
                        case "SRSpecialty": this.str.SRSpecialty = (string)value; break;
                        case "StreetName": this.str.StreetName = (string)value; break;
                        case "District": this.str.District = (string)value; break;
                        case "City": this.str.City = (string)value; break;
                        case "County": this.str.County = (string)value; break;
                        case "State": this.str.State = (string)value; break;
                        case "ZipCode": this.str.ZipCode = (string)value; break;
                        case "PhoneNo": this.str.PhoneNo = (string)value; break;
                        case "FaxNo": this.str.FaxNo = (string)value; break;
                        case "Email": this.str.Email = (string)value; break;
                        case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
                        case "LicenseNo": this.str.LicenseNo = (string)value; break;
                        case "TaxRegistrationNo": this.str.TaxRegistrationNo = (string)value; break;
                        case "IsPKP": this.str.IsPKP = (string)value; break;
                        case "IsAvailable": this.str.IsAvailable = (string)value; break;
                        case "NotAvailableUntil": this.str.NotAvailableUntil = (string)value; break;
                        case "IsAnesthetist": this.str.IsAnesthetist = (string)value; break;
                        case "IsAudiologist": this.str.IsAudiologist = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "LicensePeriodeStart": this.str.LicensePeriodeStart = (string)value; break;
                        case "LicensePeriodeEnd": this.str.LicensePeriodeEnd = (string)value; break;
                        case "IsParamedicFeeUsePercentage": this.str.IsParamedicFeeUsePercentage = (string)value; break;
                        case "ParamedicFeeAmount": this.str.ParamedicFeeAmount = (string)value; break;
                        case "Bank": this.str.Bank = (string)value; break;
                        case "BankAccount": this.str.BankAccount = (string)value; break;
                        case "ParamedicFeeAmountReferral": this.str.ParamedicFeeAmountReferral = (string)value; break;
                        case "IsUsingQue": this.str.IsUsingQue = (string)value; break;
                        case "SRParamedicRL1": this.str.SRParamedicRL1 = (string)value; break;
                        case "IsDeductionFeeUsePercentage": this.str.IsDeductionFeeUsePercentage = (string)value; break;
                        case "DeductionFeeAmount": this.str.DeductionFeeAmount = (string)value; break;
                        case "DeductionFeeAmountReferral": this.str.DeductionFeeAmountReferral = (string)value; break;
                        case "ChartOfAccountIdAPParamedicFee": this.str.ChartOfAccountIdAPParamedicFee = (string)value; break;
                        case "SubledgerIdAPParamedicFee": this.str.SubledgerIdAPParamedicFee = (string)value; break;
                        case "ParamedicFee": this.str.ParamedicFee = (string)value; break;
                        case "IsPrintInSlip": this.str.IsPrintInSlip = (string)value; break;
                        case "Bank2": this.str.Bank2 = (string)value; break;
                        case "BankAccount2": this.str.BankAccount2 = (string)value; break;
                        case "GuaranteeFee": this.str.GuaranteeFee = (string)value; break;
                        case "CoorporateGradeID": this.str.CoorporateGradeID = (string)value; break;
                        case "CoorporateGradeValue": this.str.CoorporateGradeValue = (string)value; break;
                        case "BankAccountName": this.str.BankAccountName = (string)value; break;
                        case "BankAccountName2": this.str.BankAccountName2 = (string)value; break;
                        case "Ssn": this.str.Ssn = (string)value; break;
                        case "IsPhysicianTeam": this.str.IsPhysicianTeam = (string)value; break;
                        case "ParamedicQueueCode": this.str.ParamedicQueueCode = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DateOfBirth":

                            if (value == null || value is System.DateTime)
                                this.DateOfBirth = (System.DateTime?)value;
                            break;
                        case "IsPKP":

                            if (value == null || value is System.Boolean)
                                this.IsPKP = (System.Boolean?)value;
                            break;
                        case "IsAvailable":

                            if (value == null || value is System.Boolean)
                                this.IsAvailable = (System.Boolean?)value;
                            break;
                        case "NotAvailableUntil":

                            if (value == null || value is System.DateTime)
                                this.NotAvailableUntil = (System.DateTime?)value;
                            break;
                        case "IsAnesthetist":

                            if (value == null || value is System.Boolean)
                                this.IsAnesthetist = (System.Boolean?)value;
                            break;
                        case "IsAudiologist":

                            if (value == null || value is System.Boolean)
                                this.IsAudiologist = (System.Boolean?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "LicensePeriodeStart":

                            if (value == null || value is System.DateTime)
                                this.LicensePeriodeStart = (System.DateTime?)value;
                            break;
                        case "LicensePeriodeEnd":

                            if (value == null || value is System.DateTime)
                                this.LicensePeriodeEnd = (System.DateTime?)value;
                            break;
                        case "IsParamedicFeeUsePercentage":

                            if (value == null || value is System.Boolean)
                                this.IsParamedicFeeUsePercentage = (System.Boolean?)value;
                            break;
                        case "ParamedicFeeAmount":

                            if (value == null || value is System.Decimal)
                                this.ParamedicFeeAmount = (System.Decimal?)value;
                            break;
                        case "ParamedicFeeAmountReferral":

                            if (value == null || value is System.Decimal)
                                this.ParamedicFeeAmountReferral = (System.Decimal?)value;
                            break;
                        case "IsUsingQue":

                            if (value == null || value is System.Boolean)
                                this.IsUsingQue = (System.Boolean?)value;
                            break;
                        case "IsDeductionFeeUsePercentage":

                            if (value == null || value is System.Boolean)
                                this.IsDeductionFeeUsePercentage = (System.Boolean?)value;
                            break;
                        case "DeductionFeeAmount":

                            if (value == null || value is System.Decimal)
                                this.DeductionFeeAmount = (System.Decimal?)value;
                            break;
                        case "DeductionFeeAmountReferral":

                            if (value == null || value is System.Decimal)
                                this.DeductionFeeAmountReferral = (System.Decimal?)value;
                            break;
                        case "ChartOfAccountIdAPParamedicFee":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdAPParamedicFee = (System.Int32?)value;
                            break;
                        case "SubledgerIdAPParamedicFee":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdAPParamedicFee = (System.Int32?)value;
                            break;
                        case "ParamedicFee":

                            if (value == null || value is System.Boolean)
                                this.ParamedicFee = (System.Boolean?)value;
                            break;
                        case "IsPrintInSlip":

                            if (value == null || value is System.Boolean)
                                this.IsPrintInSlip = (System.Boolean?)value;
                            break;
                        case "Foto":

                            if (value == null || value is System.Byte[])
                                this.Foto = (System.Byte[])value;
                            break;
                        case "GuaranteeFee":

                            if (value == null || value is System.Decimal)
                                this.GuaranteeFee = (System.Decimal?)value;
                            break;
                        case "CoorporateGradeID":

                            if (value == null || value is System.Int32)
                                this.CoorporateGradeID = (System.Int32?)value;
                            break;
                        case "CoorporateGradeValue":

                            if (value == null || value is System.Decimal)
                                this.CoorporateGradeValue = (System.Decimal?)value;
                            break;
                        case "IsPhysicianTeam":

                            if (value == null || value is System.Boolean)
                                this.IsPhysicianTeam = (System.Boolean?)value;
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
        /// Maps to Paramedic.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ParamedicName
        /// </summary>
        virtual public System.String ParamedicName
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.ParamedicName);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.ParamedicName, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ParamedicInitial
        /// </summary>
        virtual public System.String ParamedicInitial
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.ParamedicInitial);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.ParamedicInitial, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.DateOfBirth
        /// </summary>
        virtual public System.DateTime? DateOfBirth
        {
            get
            {
                return base.GetSystemDateTime(ParamedicMetadata.ColumnNames.DateOfBirth);
            }

            set
            {
                base.SetSystemDateTime(ParamedicMetadata.ColumnNames.DateOfBirth, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.SRParamedicType
        /// </summary>
        virtual public System.String SRParamedicType
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.SRParamedicType);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.SRParamedicType, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.SRParamedicStatus
        /// </summary>
        virtual public System.String SRParamedicStatus
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.SRParamedicStatus);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.SRParamedicStatus, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.SRReligion
        /// </summary>
        virtual public System.String SRReligion
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.SRReligion);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.SRReligion, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.SRNationality
        /// </summary>
        virtual public System.String SRNationality
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.SRNationality);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.SRNationality, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.SRSpecialty
        /// </summary>
        virtual public System.String SRSpecialty
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.SRSpecialty);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.SRSpecialty, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.StreetName
        /// </summary>
        virtual public System.String StreetName
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.StreetName);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.StreetName, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.District
        /// </summary>
        virtual public System.String District
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.District);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.District, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.City
        /// </summary>
        virtual public System.String City
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.City);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.City, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.County
        /// </summary>
        virtual public System.String County
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.County);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.County, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.State
        /// </summary>
        virtual public System.String State
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.State);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.State, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ZipCode
        /// </summary>
        virtual public System.String ZipCode
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.ZipCode);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.ZipCode, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.PhoneNo
        /// </summary>
        virtual public System.String PhoneNo
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.PhoneNo);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.PhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.FaxNo
        /// </summary>
        virtual public System.String FaxNo
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.FaxNo);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.FaxNo, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.Email
        /// </summary>
        virtual public System.String Email
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.Email);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.Email, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.MobilePhoneNo
        /// </summary>
        virtual public System.String MobilePhoneNo
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.MobilePhoneNo);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.MobilePhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.LicenseNo
        /// </summary>
        virtual public System.String LicenseNo
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.LicenseNo);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.LicenseNo, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.TaxRegistrationNo
        /// </summary>
        virtual public System.String TaxRegistrationNo
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.TaxRegistrationNo);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.TaxRegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsPKP
        /// </summary>
        virtual public System.Boolean? IsPKP
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsPKP);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsPKP, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsAvailable
        /// </summary>
        virtual public System.Boolean? IsAvailable
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsAvailable);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsAvailable, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.NotAvailableUntil
        /// </summary>
        virtual public System.DateTime? NotAvailableUntil
        {
            get
            {
                return base.GetSystemDateTime(ParamedicMetadata.ColumnNames.NotAvailableUntil);
            }

            set
            {
                base.SetSystemDateTime(ParamedicMetadata.ColumnNames.NotAvailableUntil, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsAnesthetist
        /// </summary>
        virtual public System.Boolean? IsAnesthetist
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsAnesthetist);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsAnesthetist, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsAudiologist
        /// </summary>
        virtual public System.Boolean? IsAudiologist
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsAudiologist);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsAudiologist, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.LicensePeriodeStart
        /// </summary>
        virtual public System.DateTime? LicensePeriodeStart
        {
            get
            {
                return base.GetSystemDateTime(ParamedicMetadata.ColumnNames.LicensePeriodeStart);
            }

            set
            {
                base.SetSystemDateTime(ParamedicMetadata.ColumnNames.LicensePeriodeStart, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.LicensePeriodeEnd
        /// </summary>
        virtual public System.DateTime? LicensePeriodeEnd
        {
            get
            {
                return base.GetSystemDateTime(ParamedicMetadata.ColumnNames.LicensePeriodeEnd);
            }

            set
            {
                base.SetSystemDateTime(ParamedicMetadata.ColumnNames.LicensePeriodeEnd, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsParamedicFeeUsePercentage
        /// </summary>
        virtual public System.Boolean? IsParamedicFeeUsePercentage
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsParamedicFeeUsePercentage);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ParamedicFeeAmount
        /// </summary>
        virtual public System.Decimal? ParamedicFeeAmount
        {
            get
            {
                return base.GetSystemDecimal(ParamedicMetadata.ColumnNames.ParamedicFeeAmount);
            }

            set
            {
                base.SetSystemDecimal(ParamedicMetadata.ColumnNames.ParamedicFeeAmount, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.Bank
        /// </summary>
        virtual public System.String Bank
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.Bank);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.Bank, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.BankAccount
        /// </summary>
        virtual public System.String BankAccount
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.BankAccount);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.BankAccount, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ParamedicFeeAmountReferral
        /// </summary>
        virtual public System.Decimal? ParamedicFeeAmountReferral
        {
            get
            {
                return base.GetSystemDecimal(ParamedicMetadata.ColumnNames.ParamedicFeeAmountReferral);
            }

            set
            {
                base.SetSystemDecimal(ParamedicMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsUsingQue
        /// </summary>
        virtual public System.Boolean? IsUsingQue
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsUsingQue);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsUsingQue, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.SRParamedicRL1
        /// </summary>
        virtual public System.String SRParamedicRL1
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.SRParamedicRL1);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.SRParamedicRL1, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsDeductionFeeUsePercentage
        /// </summary>
        virtual public System.Boolean? IsDeductionFeeUsePercentage
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsDeductionFeeUsePercentage);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.DeductionFeeAmount
        /// </summary>
        virtual public System.Decimal? DeductionFeeAmount
        {
            get
            {
                return base.GetSystemDecimal(ParamedicMetadata.ColumnNames.DeductionFeeAmount);
            }

            set
            {
                base.SetSystemDecimal(ParamedicMetadata.ColumnNames.DeductionFeeAmount, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.DeductionFeeAmountReferral
        /// </summary>
        virtual public System.Decimal? DeductionFeeAmountReferral
        {
            get
            {
                return base.GetSystemDecimal(ParamedicMetadata.ColumnNames.DeductionFeeAmountReferral);
            }

            set
            {
                base.SetSystemDecimal(ParamedicMetadata.ColumnNames.DeductionFeeAmountReferral, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ChartOfAccountIdAPParamedicFee
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdAPParamedicFee
        {
            get
            {
                return base.GetSystemInt32(ParamedicMetadata.ColumnNames.ChartOfAccountIdAPParamedicFee);
            }

            set
            {
                base.SetSystemInt32(ParamedicMetadata.ColumnNames.ChartOfAccountIdAPParamedicFee, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.SubledgerIdAPParamedicFee
        /// </summary>
        virtual public System.Int32? SubledgerIdAPParamedicFee
        {
            get
            {
                return base.GetSystemInt32(ParamedicMetadata.ColumnNames.SubledgerIdAPParamedicFee);
            }

            set
            {
                base.SetSystemInt32(ParamedicMetadata.ColumnNames.SubledgerIdAPParamedicFee, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ParamedicFee
        /// </summary>
        virtual public System.Boolean? ParamedicFee
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.ParamedicFee);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.ParamedicFee, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsPrintInSlip
        /// </summary>
        virtual public System.Boolean? IsPrintInSlip
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsPrintInSlip);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsPrintInSlip, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.Bank2
        /// </summary>
        virtual public System.String Bank2
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.Bank2);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.Bank2, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.BankAccount2
        /// </summary>
        virtual public System.String BankAccount2
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.BankAccount2);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.BankAccount2, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.Foto
        /// </summary>
        virtual public System.Byte[] Foto
        {
            get
            {
                return base.GetSystemByteArray(ParamedicMetadata.ColumnNames.Foto);
            }

            set
            {
                base.SetSystemByteArray(ParamedicMetadata.ColumnNames.Foto, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.GuaranteeFee
        /// </summary>
        virtual public System.Decimal? GuaranteeFee
        {
            get
            {
                return base.GetSystemDecimal(ParamedicMetadata.ColumnNames.GuaranteeFee);
            }

            set
            {
                base.SetSystemDecimal(ParamedicMetadata.ColumnNames.GuaranteeFee, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.CoorporateGradeID
        /// </summary>
        virtual public System.Int32? CoorporateGradeID
        {
            get
            {
                return base.GetSystemInt32(ParamedicMetadata.ColumnNames.CoorporateGradeID);
            }

            set
            {
                base.SetSystemInt32(ParamedicMetadata.ColumnNames.CoorporateGradeID, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.CoorporateGradeValue
        /// </summary>
        virtual public System.Decimal? CoorporateGradeValue
        {
            get
            {
                return base.GetSystemDecimal(ParamedicMetadata.ColumnNames.CoorporateGradeValue);
            }

            set
            {
                base.SetSystemDecimal(ParamedicMetadata.ColumnNames.CoorporateGradeValue, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.BankAccountName
        /// </summary>
        virtual public System.String BankAccountName
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.BankAccountName);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.BankAccountName, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.BankAccountName2
        /// </summary>
        virtual public System.String BankAccountName2
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.BankAccountName2);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.BankAccountName2, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.Ssn
        /// </summary>
        virtual public System.String Ssn
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.Ssn);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.Ssn, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.IsPhysicianTeam
        /// </summary>
        virtual public System.Boolean? IsPhysicianTeam
        {
            get
            {
                return base.GetSystemBoolean(ParamedicMetadata.ColumnNames.IsPhysicianTeam);
            }

            set
            {
                base.SetSystemBoolean(ParamedicMetadata.ColumnNames.IsPhysicianTeam, value);
            }
        }
        /// <summary>
        /// Maps to Paramedic.ParamedicQueueCode
        /// </summary>
        virtual public System.String ParamedicQueueCode
        {
            get
            {
                return base.GetSystemString(ParamedicMetadata.ColumnNames.ParamedicQueueCode);
            }

            set
            {
                base.SetSystemString(ParamedicMetadata.ColumnNames.ParamedicQueueCode, value);
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
            public esStrings(esParamedic entity)
            {
                this.entity = entity;
            }
            public System.String ParamedicID
            {
                get
                {
                    System.String data = entity.ParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicID = null;
                    else entity.ParamedicID = Convert.ToString(value);
                }
            }
            public System.String ParamedicName
            {
                get
                {
                    System.String data = entity.ParamedicName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicName = null;
                    else entity.ParamedicName = Convert.ToString(value);
                }
            }
            public System.String ParamedicInitial
            {
                get
                {
                    System.String data = entity.ParamedicInitial;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicInitial = null;
                    else entity.ParamedicInitial = Convert.ToString(value);
                }
            }
            public System.String DateOfBirth
            {
                get
                {
                    System.DateTime? data = entity.DateOfBirth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfBirth = null;
                    else entity.DateOfBirth = Convert.ToDateTime(value);
                }
            }
            public System.String SRParamedicType
            {
                get
                {
                    System.String data = entity.SRParamedicType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRParamedicType = null;
                    else entity.SRParamedicType = Convert.ToString(value);
                }
            }
            public System.String SRParamedicStatus
            {
                get
                {
                    System.String data = entity.SRParamedicStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRParamedicStatus = null;
                    else entity.SRParamedicStatus = Convert.ToString(value);
                }
            }
            public System.String SRReligion
            {
                get
                {
                    System.String data = entity.SRReligion;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRReligion = null;
                    else entity.SRReligion = Convert.ToString(value);
                }
            }
            public System.String SRNationality
            {
                get
                {
                    System.String data = entity.SRNationality;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRNationality = null;
                    else entity.SRNationality = Convert.ToString(value);
                }
            }
            public System.String SRSpecialty
            {
                get
                {
                    System.String data = entity.SRSpecialty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRSpecialty = null;
                    else entity.SRSpecialty = Convert.ToString(value);
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
            public System.String LicenseNo
            {
                get
                {
                    System.String data = entity.LicenseNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LicenseNo = null;
                    else entity.LicenseNo = Convert.ToString(value);
                }
            }
            public System.String TaxRegistrationNo
            {
                get
                {
                    System.String data = entity.TaxRegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TaxRegistrationNo = null;
                    else entity.TaxRegistrationNo = Convert.ToString(value);
                }
            }
            public System.String IsPKP
            {
                get
                {
                    System.Boolean? data = entity.IsPKP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPKP = null;
                    else entity.IsPKP = Convert.ToBoolean(value);
                }
            }
            public System.String IsAvailable
            {
                get
                {
                    System.Boolean? data = entity.IsAvailable;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAvailable = null;
                    else entity.IsAvailable = Convert.ToBoolean(value);
                }
            }
            public System.String NotAvailableUntil
            {
                get
                {
                    System.DateTime? data = entity.NotAvailableUntil;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NotAvailableUntil = null;
                    else entity.NotAvailableUntil = Convert.ToDateTime(value);
                }
            }
            public System.String IsAnesthetist
            {
                get
                {
                    System.Boolean? data = entity.IsAnesthetist;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAnesthetist = null;
                    else entity.IsAnesthetist = Convert.ToBoolean(value);
                }
            }
            public System.String IsAudiologist
            {
                get
                {
                    System.Boolean? data = entity.IsAudiologist;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAudiologist = null;
                    else entity.IsAudiologist = Convert.ToBoolean(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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
            public System.String LicensePeriodeStart
            {
                get
                {
                    System.DateTime? data = entity.LicensePeriodeStart;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LicensePeriodeStart = null;
                    else entity.LicensePeriodeStart = Convert.ToDateTime(value);
                }
            }
            public System.String LicensePeriodeEnd
            {
                get
                {
                    System.DateTime? data = entity.LicensePeriodeEnd;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LicensePeriodeEnd = null;
                    else entity.LicensePeriodeEnd = Convert.ToDateTime(value);
                }
            }
            public System.String IsParamedicFeeUsePercentage
            {
                get
                {
                    System.Boolean? data = entity.IsParamedicFeeUsePercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsParamedicFeeUsePercentage = null;
                    else entity.IsParamedicFeeUsePercentage = Convert.ToBoolean(value);
                }
            }
            public System.String ParamedicFeeAmount
            {
                get
                {
                    System.Decimal? data = entity.ParamedicFeeAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicFeeAmount = null;
                    else entity.ParamedicFeeAmount = Convert.ToDecimal(value);
                }
            }
            public System.String Bank
            {
                get
                {
                    System.String data = entity.Bank;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Bank = null;
                    else entity.Bank = Convert.ToString(value);
                }
            }
            public System.String BankAccount
            {
                get
                {
                    System.String data = entity.BankAccount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccount = null;
                    else entity.BankAccount = Convert.ToString(value);
                }
            }
            public System.String ParamedicFeeAmountReferral
            {
                get
                {
                    System.Decimal? data = entity.ParamedicFeeAmountReferral;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicFeeAmountReferral = null;
                    else entity.ParamedicFeeAmountReferral = Convert.ToDecimal(value);
                }
            }
            public System.String IsUsingQue
            {
                get
                {
                    System.Boolean? data = entity.IsUsingQue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsingQue = null;
                    else entity.IsUsingQue = Convert.ToBoolean(value);
                }
            }
            public System.String SRParamedicRL1
            {
                get
                {
                    System.String data = entity.SRParamedicRL1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRParamedicRL1 = null;
                    else entity.SRParamedicRL1 = Convert.ToString(value);
                }
            }
            public System.String IsDeductionFeeUsePercentage
            {
                get
                {
                    System.Boolean? data = entity.IsDeductionFeeUsePercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDeductionFeeUsePercentage = null;
                    else entity.IsDeductionFeeUsePercentage = Convert.ToBoolean(value);
                }
            }
            public System.String DeductionFeeAmount
            {
                get
                {
                    System.Decimal? data = entity.DeductionFeeAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DeductionFeeAmount = null;
                    else entity.DeductionFeeAmount = Convert.ToDecimal(value);
                }
            }
            public System.String DeductionFeeAmountReferral
            {
                get
                {
                    System.Decimal? data = entity.DeductionFeeAmountReferral;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DeductionFeeAmountReferral = null;
                    else entity.DeductionFeeAmountReferral = Convert.ToDecimal(value);
                }
            }
            public System.String ChartOfAccountIdAPParamedicFee
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdAPParamedicFee;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdAPParamedicFee = null;
                    else entity.ChartOfAccountIdAPParamedicFee = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdAPParamedicFee
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdAPParamedicFee;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdAPParamedicFee = null;
                    else entity.SubledgerIdAPParamedicFee = Convert.ToInt32(value);
                }
            }
            public System.String ParamedicFee
            {
                get
                {
                    System.Boolean? data = entity.ParamedicFee;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicFee = null;
                    else entity.ParamedicFee = Convert.ToBoolean(value);
                }
            }
            public System.String IsPrintInSlip
            {
                get
                {
                    System.Boolean? data = entity.IsPrintInSlip;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPrintInSlip = null;
                    else entity.IsPrintInSlip = Convert.ToBoolean(value);
                }
            }
            public System.String Bank2
            {
                get
                {
                    System.String data = entity.Bank2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Bank2 = null;
                    else entity.Bank2 = Convert.ToString(value);
                }
            }
            public System.String BankAccount2
            {
                get
                {
                    System.String data = entity.BankAccount2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccount2 = null;
                    else entity.BankAccount2 = Convert.ToString(value);
                }
            }
            public System.String GuaranteeFee
            {
                get
                {
                    System.Decimal? data = entity.GuaranteeFee;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuaranteeFee = null;
                    else entity.GuaranteeFee = Convert.ToDecimal(value);
                }
            }
            public System.String CoorporateGradeID
            {
                get
                {
                    System.Int32? data = entity.CoorporateGradeID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CoorporateGradeID = null;
                    else entity.CoorporateGradeID = Convert.ToInt32(value);
                }
            }
            public System.String CoorporateGradeValue
            {
                get
                {
                    System.Decimal? data = entity.CoorporateGradeValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CoorporateGradeValue = null;
                    else entity.CoorporateGradeValue = Convert.ToDecimal(value);
                }
            }
            public System.String BankAccountName
            {
                get
                {
                    System.String data = entity.BankAccountName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccountName = null;
                    else entity.BankAccountName = Convert.ToString(value);
                }
            }
            public System.String BankAccountName2
            {
                get
                {
                    System.String data = entity.BankAccountName2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccountName2 = null;
                    else entity.BankAccountName2 = Convert.ToString(value);
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
            public System.String IsPhysicianTeam
            {
                get
                {
                    System.Boolean? data = entity.IsPhysicianTeam;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPhysicianTeam = null;
                    else entity.IsPhysicianTeam = Convert.ToBoolean(value);
                }
            }
            public System.String ParamedicQueueCode
            {
                get
                {
                    System.String data = entity.ParamedicQueueCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicQueueCode = null;
                    else entity.ParamedicQueueCode = Convert.ToString(value);
                }
            }
            private esParamedic entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esParamedicQuery query)
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
                throw new Exception("esParamedic can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Paramedic : esParamedic
    {
    }

    [Serializable]
    abstract public class esParamedicQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ParamedicMetadata.Meta();
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicName
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ParamedicName, esSystemType.String);
            }
        }

        public esQueryItem ParamedicInitial
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ParamedicInitial, esSystemType.String);
            }
        }

        public esQueryItem DateOfBirth
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
            }
        }

        public esQueryItem SRParamedicType
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.SRParamedicType, esSystemType.String);
            }
        }

        public esQueryItem SRParamedicStatus
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.SRParamedicStatus, esSystemType.String);
            }
        }

        public esQueryItem SRReligion
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.SRReligion, esSystemType.String);
            }
        }

        public esQueryItem SRNationality
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.SRNationality, esSystemType.String);
            }
        }

        public esQueryItem SRSpecialty
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.SRSpecialty, esSystemType.String);
            }
        }

        public esQueryItem StreetName
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.StreetName, esSystemType.String);
            }
        }

        public esQueryItem District
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.District, esSystemType.String);
            }
        }

        public esQueryItem City
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.City, esSystemType.String);
            }
        }

        public esQueryItem County
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.County, esSystemType.String);
            }
        }

        public esQueryItem State
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.State, esSystemType.String);
            }
        }

        public esQueryItem ZipCode
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ZipCode, esSystemType.String);
            }
        }

        public esQueryItem PhoneNo
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.PhoneNo, esSystemType.String);
            }
        }

        public esQueryItem FaxNo
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.FaxNo, esSystemType.String);
            }
        }

        public esQueryItem Email
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.Email, esSystemType.String);
            }
        }

        public esQueryItem MobilePhoneNo
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
            }
        }

        public esQueryItem LicenseNo
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.LicenseNo, esSystemType.String);
            }
        }

        public esQueryItem TaxRegistrationNo
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.TaxRegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem IsPKP
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsPKP, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAvailable
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsAvailable, esSystemType.Boolean);
            }
        }

        public esQueryItem NotAvailableUntil
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.NotAvailableUntil, esSystemType.DateTime);
            }
        }

        public esQueryItem IsAnesthetist
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsAnesthetist, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAudiologist
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsAudiologist, esSystemType.Boolean);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem LicensePeriodeStart
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.LicensePeriodeStart, esSystemType.DateTime);
            }
        }

        public esQueryItem LicensePeriodeEnd
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.LicensePeriodeEnd, esSystemType.DateTime);
            }
        }

        public esQueryItem IsParamedicFeeUsePercentage
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
            }
        }

        public esQueryItem ParamedicFeeAmount
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem Bank
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.Bank, esSystemType.String);
            }
        }

        public esQueryItem BankAccount
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.BankAccount, esSystemType.String);
            }
        }

        public esQueryItem ParamedicFeeAmountReferral
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
            }
        }

        public esQueryItem IsUsingQue
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsUsingQue, esSystemType.Boolean);
            }
        }

        public esQueryItem SRParamedicRL1
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.SRParamedicRL1, esSystemType.String);
            }
        }

        public esQueryItem IsDeductionFeeUsePercentage
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
            }
        }

        public esQueryItem DeductionFeeAmount
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DeductionFeeAmountReferral
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
            }
        }

        public esQueryItem ChartOfAccountIdAPParamedicFee
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ChartOfAccountIdAPParamedicFee, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdAPParamedicFee
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.SubledgerIdAPParamedicFee, esSystemType.Int32);
            }
        }

        public esQueryItem ParamedicFee
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ParamedicFee, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPrintInSlip
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsPrintInSlip, esSystemType.Boolean);
            }
        }

        public esQueryItem Bank2
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.Bank2, esSystemType.String);
            }
        }

        public esQueryItem BankAccount2
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.BankAccount2, esSystemType.String);
            }
        }

        public esQueryItem Foto
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.Foto, esSystemType.ByteArray);
            }
        }

        public esQueryItem GuaranteeFee
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.GuaranteeFee, esSystemType.Decimal);
            }
        }

        public esQueryItem CoorporateGradeID
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.CoorporateGradeID, esSystemType.Int32);
            }
        }

        public esQueryItem CoorporateGradeValue
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.CoorporateGradeValue, esSystemType.Decimal);
            }
        }

        public esQueryItem BankAccountName
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.BankAccountName, esSystemType.String);
            }
        }

        public esQueryItem BankAccountName2
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.BankAccountName2, esSystemType.String);
            }
        }

        public esQueryItem Ssn
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.Ssn, esSystemType.String);
            }
        }

        public esQueryItem IsPhysicianTeam
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.IsPhysicianTeam, esSystemType.Boolean);
            }
        }

        public esQueryItem ParamedicQueueCode
        {
            get
            {
                return new esQueryItem(this, ParamedicMetadata.ColumnNames.ParamedicQueueCode, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ParamedicCollection")]
    public partial class ParamedicCollection : esParamedicCollection, IEnumerable<Paramedic>
    {
        public ParamedicCollection()
        {

        }

        public static implicit operator List<Paramedic>(ParamedicCollection coll)
        {
            List<Paramedic> list = new List<Paramedic>();

            foreach (Paramedic emp in coll)
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
                return ParamedicMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Paramedic(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Paramedic();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ParamedicQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicQuery();
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
        public bool Load(ParamedicQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Paramedic AddNew()
        {
            Paramedic entity = base.AddNewEntity() as Paramedic;

            return entity;
        }
        public Paramedic FindByPrimaryKey(String paramedicID)
        {
            return base.FindByPrimaryKey(paramedicID) as Paramedic;
        }

        #region IEnumerable< Paramedic> Members

        IEnumerator<Paramedic> IEnumerable<Paramedic>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Paramedic;
            }
        }

        #endregion

        private ParamedicQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Paramedic' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Paramedic ({ParamedicID})")]
    [Serializable]
    public partial class Paramedic : esParamedic
    {
        public Paramedic()
        {
        }

        public Paramedic(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicMetadata.Meta();
            }
        }

        override protected esParamedicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ParamedicQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicQuery();
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
        public bool Load(ParamedicQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ParamedicQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ParamedicQuery : esParamedicQuery
    {
        public ParamedicQuery()
        {

        }

        public ParamedicQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ParamedicQuery";
        }
    }

    [Serializable]
    public partial class ParamedicMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ParamedicMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.ParamedicID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ParamedicName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.ParamedicName;
            c.CharacterMaxLength = 255;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ParamedicInitial, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.ParamedicInitial;
            c.CharacterMaxLength = 5;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.DateOfBirth, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicMetadata.PropertyNames.DateOfBirth;
            c.HasDefault = true;
            c.Default = @"(CONVERT([smalldatetime],'19000101',(105)))";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.SRParamedicType, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.SRParamedicType;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.SRParamedicStatus, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.SRParamedicStatus;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.SRReligion, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.SRReligion;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.SRNationality, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.SRNationality;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.SRSpecialty, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.SRSpecialty;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.StreetName, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.StreetName;
            c.CharacterMaxLength = 250;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.District, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.District;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.City, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.City;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.County, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.County;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.State, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.State;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ZipCode, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.ZipCode;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.PhoneNo, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.PhoneNo;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.FaxNo, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.FaxNo;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.Email, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.Email;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.MobilePhoneNo, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.LicenseNo, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.LicenseNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.TaxRegistrationNo, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.TaxRegistrationNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsPKP, 21, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsPKP;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsAvailable, 22, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsAvailable;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.NotAvailableUntil, 23, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicMetadata.PropertyNames.NotAvailableUntil;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsAnesthetist, 24, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsAnesthetist;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsAudiologist, 25, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsAudiologist;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsActive, 26, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsActive;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.Notes, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.LastUpdateDateTime, 28, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.LastUpdateByUserID, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.RegistrationNo, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 70;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.LicensePeriodeStart, 31, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicMetadata.PropertyNames.LicensePeriodeStart;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.LicensePeriodeEnd, 32, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicMetadata.PropertyNames.LicensePeriodeEnd;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsParamedicFeeUsePercentage, 33, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsParamedicFeeUsePercentage;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ParamedicFeeAmount, 34, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicMetadata.PropertyNames.ParamedicFeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.Bank, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.Bank;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.BankAccount, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.BankAccount;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ParamedicFeeAmountReferral, 37, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicMetadata.PropertyNames.ParamedicFeeAmountReferral;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsUsingQue, 38, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsUsingQue;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.SRParamedicRL1, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.SRParamedicRL1;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsDeductionFeeUsePercentage, 40, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsDeductionFeeUsePercentage;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.DeductionFeeAmount, 41, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicMetadata.PropertyNames.DeductionFeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.DeductionFeeAmountReferral, 42, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicMetadata.PropertyNames.DeductionFeeAmountReferral;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ChartOfAccountIdAPParamedicFee, 43, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ParamedicMetadata.PropertyNames.ChartOfAccountIdAPParamedicFee;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.SubledgerIdAPParamedicFee, 44, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ParamedicMetadata.PropertyNames.SubledgerIdAPParamedicFee;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ParamedicFee, 45, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.ParamedicFee;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsPrintInSlip, 46, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsPrintInSlip;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.Bank2, 47, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.Bank2;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.BankAccount2, 48, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.BankAccount2;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.Foto, 49, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = ParamedicMetadata.PropertyNames.Foto;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.GuaranteeFee, 50, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicMetadata.PropertyNames.GuaranteeFee;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.CoorporateGradeID, 51, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ParamedicMetadata.PropertyNames.CoorporateGradeID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.CoorporateGradeValue, 52, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicMetadata.PropertyNames.CoorporateGradeValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.BankAccountName, 53, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.BankAccountName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.BankAccountName2, 54, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.BankAccountName2;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.Ssn, 55, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.Ssn;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.IsPhysicianTeam, 56, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicMetadata.PropertyNames.IsPhysicianTeam;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicMetadata.ColumnNames.ParamedicQueueCode, 57, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicMetadata.PropertyNames.ParamedicQueueCode;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ParamedicMetadata Meta()
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
            public const string ParamedicID = "ParamedicID";
            public const string ParamedicName = "ParamedicName";
            public const string ParamedicInitial = "ParamedicInitial";
            public const string DateOfBirth = "DateOfBirth";
            public const string SRParamedicType = "SRParamedicType";
            public const string SRParamedicStatus = "SRParamedicStatus";
            public const string SRReligion = "SRReligion";
            public const string SRNationality = "SRNationality";
            public const string SRSpecialty = "SRSpecialty";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string PhoneNo = "PhoneNo";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string LicenseNo = "LicenseNo";
            public const string TaxRegistrationNo = "TaxRegistrationNo";
            public const string IsPKP = "IsPKP";
            public const string IsAvailable = "IsAvailable";
            public const string NotAvailableUntil = "NotAvailableUntil";
            public const string IsAnesthetist = "IsAnesthetist";
            public const string IsAudiologist = "IsAudiologist";
            public const string IsActive = "IsActive";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string RegistrationNo = "RegistrationNo";
            public const string LicensePeriodeStart = "LicensePeriodeStart";
            public const string LicensePeriodeEnd = "LicensePeriodeEnd";
            public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
            public const string ParamedicFeeAmount = "ParamedicFeeAmount";
            public const string Bank = "Bank";
            public const string BankAccount = "BankAccount";
            public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
            public const string IsUsingQue = "IsUsingQue";
            public const string SRParamedicRL1 = "SRParamedicRL1";
            public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
            public const string DeductionFeeAmount = "DeductionFeeAmount";
            public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
            public const string ChartOfAccountIdAPParamedicFee = "ChartOfAccountIdAPParamedicFee";
            public const string SubledgerIdAPParamedicFee = "SubledgerIdAPParamedicFee";
            public const string ParamedicFee = "ParamedicFee";
            public const string IsPrintInSlip = "IsPrintInSlip";
            public const string Bank2 = "Bank2";
            public const string BankAccount2 = "BankAccount2";
            public const string Foto = "Foto";
            public const string GuaranteeFee = "GuaranteeFee";
            public const string CoorporateGradeID = "CoorporateGradeID";
            public const string CoorporateGradeValue = "CoorporateGradeValue";
            public const string BankAccountName = "BankAccountName";
            public const string BankAccountName2 = "BankAccountName2";
            public const string Ssn = "Ssn";
            public const string IsPhysicianTeam = "IsPhysicianTeam";
            public const string ParamedicQueueCode = "ParamedicQueueCode";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ParamedicID = "ParamedicID";
            public const string ParamedicName = "ParamedicName";
            public const string ParamedicInitial = "ParamedicInitial";
            public const string DateOfBirth = "DateOfBirth";
            public const string SRParamedicType = "SRParamedicType";
            public const string SRParamedicStatus = "SRParamedicStatus";
            public const string SRReligion = "SRReligion";
            public const string SRNationality = "SRNationality";
            public const string SRSpecialty = "SRSpecialty";
            public const string StreetName = "StreetName";
            public const string District = "District";
            public const string City = "City";
            public const string County = "County";
            public const string State = "State";
            public const string ZipCode = "ZipCode";
            public const string PhoneNo = "PhoneNo";
            public const string FaxNo = "FaxNo";
            public const string Email = "Email";
            public const string MobilePhoneNo = "MobilePhoneNo";
            public const string LicenseNo = "LicenseNo";
            public const string TaxRegistrationNo = "TaxRegistrationNo";
            public const string IsPKP = "IsPKP";
            public const string IsAvailable = "IsAvailable";
            public const string NotAvailableUntil = "NotAvailableUntil";
            public const string IsAnesthetist = "IsAnesthetist";
            public const string IsAudiologist = "IsAudiologist";
            public const string IsActive = "IsActive";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string RegistrationNo = "RegistrationNo";
            public const string LicensePeriodeStart = "LicensePeriodeStart";
            public const string LicensePeriodeEnd = "LicensePeriodeEnd";
            public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
            public const string ParamedicFeeAmount = "ParamedicFeeAmount";
            public const string Bank = "Bank";
            public const string BankAccount = "BankAccount";
            public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
            public const string IsUsingQue = "IsUsingQue";
            public const string SRParamedicRL1 = "SRParamedicRL1";
            public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
            public const string DeductionFeeAmount = "DeductionFeeAmount";
            public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
            public const string ChartOfAccountIdAPParamedicFee = "ChartOfAccountIdAPParamedicFee";
            public const string SubledgerIdAPParamedicFee = "SubledgerIdAPParamedicFee";
            public const string ParamedicFee = "ParamedicFee";
            public const string IsPrintInSlip = "IsPrintInSlip";
            public const string Bank2 = "Bank2";
            public const string BankAccount2 = "BankAccount2";
            public const string Foto = "Foto";
            public const string GuaranteeFee = "GuaranteeFee";
            public const string CoorporateGradeID = "CoorporateGradeID";
            public const string CoorporateGradeValue = "CoorporateGradeValue";
            public const string BankAccountName = "BankAccountName";
            public const string BankAccountName2 = "BankAccountName2";
            public const string Ssn = "Ssn";
            public const string IsPhysicianTeam = "IsPhysicianTeam";
            public const string ParamedicQueueCode = "ParamedicQueueCode";
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
            lock (typeof(ParamedicMetadata))
            {
                if (ParamedicMetadata.mapDelegates == null)
                {
                    ParamedicMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ParamedicMetadata.meta == null)
                {
                    ParamedicMetadata.meta = new ParamedicMetadata();
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

                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicInitial", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfBirth", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("SRParamedicType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRParamedicStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRNationality", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRSpecialty", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LicenseNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TaxRegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPKP", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAvailable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("NotAvailableUntil", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("IsAnesthetist", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAudiologist", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LicensePeriodeStart", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LicensePeriodeEnd", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Bank", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankAccount", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsUsingQue", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("SRParamedicRL1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ChartOfAccountIdAPParamedicFee", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdAPParamedicFee", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ParamedicFee", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPrintInSlip", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Bank2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankAccount2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Foto", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("GuaranteeFee", new esTypeMap("decimal", "System.Decimal"));
                meta.AddTypeMap("CoorporateGradeID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("CoorporateGradeValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BankAccountName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankAccountName2", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Ssn", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPhysicianTeam", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParamedicQueueCode", new esTypeMap("varchar", "System.String"));


                meta.Source = "Paramedic";
                meta.Destination = "Paramedic";
                meta.spInsert = "proc_ParamedicInsert";
                meta.spUpdate = "proc_ParamedicUpdate";
                meta.spDelete = "proc_ParamedicDelete";
                meta.spLoadAll = "proc_ParamedicLoadAll";
                meta.spLoadByPrimaryKey = "proc_ParamedicLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ParamedicMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
