/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 13/07/2023 16:27:19
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
    abstract public class esSupplierCollection : esEntityCollectionWAuditLog
    {
        public esSupplierCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "SupplierCollection";
        }

        #region Query Logic
        protected void InitQuery(esSupplierQuery query)
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
            this.InitQuery(query as esSupplierQuery);
        }
        #endregion

        virtual public Supplier DetachEntity(Supplier entity)
        {
            return base.DetachEntity(entity) as Supplier;
        }

        virtual public Supplier AttachEntity(Supplier entity)
        {
            return base.AttachEntity(entity) as Supplier;
        }

        virtual public void Combine(SupplierCollection collection)
        {
            base.Combine(collection);
        }

        new public Supplier this[int index]
        {
            get
            {
                return base[index] as Supplier;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Supplier);
        }
    }

    [Serializable]
    abstract public class esSupplier : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSupplierQuery GetDynamicQuery()
        {
            return null;
        }

        public esSupplier()
        {
        }

        public esSupplier(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String supplierID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(supplierID);
            else
                return LoadByPrimaryKeyStoredProcedure(supplierID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String supplierID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(supplierID);
            else
                return LoadByPrimaryKeyStoredProcedure(supplierID);
        }

        private bool LoadByPrimaryKeyDynamic(String supplierID)
        {
            esSupplierQuery query = this.GetDynamicQuery();
            query.Where(query.SupplierID == supplierID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String supplierID)
        {
            esParameters parms = new esParameters();
            parms.Add("SupplierID", supplierID);
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
                        case "SupplierID": this.str.SupplierID = (string)value; break;
                        case "SupplierName": this.str.SupplierName = (string)value; break;
                        case "ShortName": this.str.ShortName = (string)value; break;
                        case "SRSupplierType": this.str.SRSupplierType = (string)value; break;
                        case "ContractNumber": this.str.ContractNumber = (string)value; break;
                        case "ContractStart": this.str.ContractStart = (string)value; break;
                        case "ContractEnd": this.str.ContractEnd = (string)value; break;
                        case "ContractSummary": this.str.ContractSummary = (string)value; break;
                        case "ContactPerson": this.str.ContactPerson = (string)value; break;
                        case "IsPKP": this.str.IsPKP = (string)value; break;
                        case "TaxRegistrationNo": this.str.TaxRegistrationNo = (string)value; break;
                        case "TermOfPayment": this.str.TermOfPayment = (string)value; break;
                        case "LeadTime": this.str.LeadTime = (string)value; break;
                        case "CreditLimit": this.str.CreditLimit = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
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
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ChartOfAccountIdAP": this.str.ChartOfAccountIdAP = (string)value; break;
                        case "SubledgerIdAP": this.str.SubledgerIdAP = (string)value; break;
                        case "ChartOfAccountIdAPInProcess": this.str.ChartOfAccountIdAPInProcess = (string)value; break;
                        case "SubledgerIdAPInProcess": this.str.SubledgerIdAPInProcess = (string)value; break;
                        case "TaxPercentage": this.str.TaxPercentage = (string)value; break;
                        case "ChartOfAccountIdAPTemporary": this.str.ChartOfAccountIdAPTemporary = (string)value; break;
                        case "SubledgerIdAPTemporary": this.str.SubledgerIdAPTemporary = (string)value; break;
                        case "ChartOfAccountIdAPCost": this.str.ChartOfAccountIdAPCost = (string)value; break;
                        case "SubledgerIdAPCost": this.str.SubledgerIdAPCost = (string)value; break;
                        case "PBFLicenseNo": this.str.PBFLicenseNo = (string)value; break;
                        case "PBFLicenseValidDate": this.str.PBFLicenseValidDate = (string)value; break;
                        case "BankAccountNo": this.str.BankAccountNo = (string)value; break;
                        case "BankName": this.str.BankName = (string)value; break;
                        case "ChartOfAccountIdPOReturn": this.str.ChartOfAccountIdPOReturn = (string)value; break;
                        case "SubledgerIdPOReturn": this.str.SubledgerIdPOReturn = (string)value; break;
                        case "SRApAgingDateType": this.str.SRApAgingDateType = (string)value; break;
                        case "ChartOfAccountIdAPNonMedic": this.str.ChartOfAccountIdAPNonMedic = (string)value; break;
                        case "SubledgerIdAPNonMedic": this.str.SubledgerIdAPNonMedic = (string)value; break;
                        case "ChartOfAccountIdAPTemporaryNonMedic": this.str.ChartOfAccountIdAPTemporaryNonMedic = (string)value; break;
                        case "SubledgerIdAPTemporaryNonMedic": this.str.SubledgerIdAPTemporaryNonMedic = (string)value; break;
                        case "ChartOfAccountIdPOReturnNonMedic": this.str.ChartOfAccountIdPOReturnNonMedic = (string)value; break;
                        case "SubledgerIdPOReturnNonMedic": this.str.SubledgerIdPOReturnNonMedic = (string)value; break;
                        case "Branch": this.str.Branch = (string)value; break;
                        case "ChartOfAccountIdGrantReceive": this.str.ChartOfAccountIdGrantReceive = (string)value; break;
                        case "SubledgerIdGrantReceive": this.str.SubledgerIdGrantReceive = (string)value; break;
                        case "ChartOfAccountIdGrantReceiveNmed": this.str.ChartOfAccountIdGrantReceiveNmed = (string)value; break;
                        case "SubledgerIdGrantReceiveNmed": this.str.SubledgerIdGrantReceiveNmed = (string)value; break;
                        case "IsUsingRounding": this.str.IsUsingRounding = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ContractStart":

                            if (value == null || value is System.DateTime)
                                this.ContractStart = (System.DateTime?)value;
                            break;
                        case "ContractEnd":

                            if (value == null || value is System.DateTime)
                                this.ContractEnd = (System.DateTime?)value;
                            break;
                        case "IsPKP":

                            if (value == null || value is System.Boolean)
                                this.IsPKP = (System.Boolean?)value;
                            break;
                        case "TermOfPayment":

                            if (value == null || value is System.Decimal)
                                this.TermOfPayment = (System.Decimal?)value;
                            break;
                        case "LeadTime":

                            if (value == null || value is System.Byte)
                                this.LeadTime = (System.Byte?)value;
                            break;
                        case "CreditLimit":

                            if (value == null || value is System.Decimal)
                                this.CreditLimit = (System.Decimal?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "ChartOfAccountIdAP":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdAP = (System.Int32?)value;
                            break;
                        case "SubledgerIdAP":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdAP = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdAPInProcess":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdAPInProcess = (System.Int32?)value;
                            break;
                        case "SubledgerIdAPInProcess":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdAPInProcess = (System.Int32?)value;
                            break;
                        case "TaxPercentage":

                            if (value == null || value is System.Decimal)
                                this.TaxPercentage = (System.Decimal?)value;
                            break;
                        case "ChartOfAccountIdAPTemporary":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdAPTemporary = (System.Int32?)value;
                            break;
                        case "SubledgerIdAPTemporary":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdAPTemporary = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdAPCost":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdAPCost = (System.Int32?)value;
                            break;
                        case "SubledgerIdAPCost":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdAPCost = (System.Int32?)value;
                            break;
                        case "PBFLicenseValidDate":

                            if (value == null || value is System.DateTime)
                                this.PBFLicenseValidDate = (System.DateTime?)value;
                            break;
                        case "ChartOfAccountIdPOReturn":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdPOReturn = (System.Int32?)value;
                            break;
                        case "SubledgerIdPOReturn":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdPOReturn = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdAPNonMedic":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdAPNonMedic = (System.Int32?)value;
                            break;
                        case "SubledgerIdAPNonMedic":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdAPNonMedic = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdAPTemporaryNonMedic":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdAPTemporaryNonMedic = (System.Int32?)value;
                            break;
                        case "SubledgerIdAPTemporaryNonMedic":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdAPTemporaryNonMedic = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdPOReturnNonMedic":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdPOReturnNonMedic = (System.Int32?)value;
                            break;
                        case "SubledgerIdPOReturnNonMedic":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdPOReturnNonMedic = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdGrantReceive":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdGrantReceive = (System.Int32?)value;
                            break;
                        case "SubledgerIdGrantReceive":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdGrantReceive = (System.Int32?)value;
                            break;
                        case "ChartOfAccountIdGrantReceiveNmed":

                            if (value == null || value is System.Int32)
                                this.ChartOfAccountIdGrantReceiveNmed = (System.Int32?)value;
                            break;
                        case "SubledgerIdGrantReceiveNmed":

                            if (value == null || value is System.Int32)
                                this.SubledgerIdGrantReceiveNmed = (System.Int32?)value;
                            break;
                        case "IsUsingRounding":

                            if (value == null || value is System.Boolean)
                                this.IsUsingRounding = (System.Boolean?)value;
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
        /// Maps to Supplier.SupplierID
        /// </summary>
        virtual public System.String SupplierID
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.SupplierID);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.SupplierID, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SupplierName
        /// </summary>
        virtual public System.String SupplierName
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.SupplierName);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.SupplierName, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ShortName
        /// </summary>
        virtual public System.String ShortName
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.ShortName);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.ShortName, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SRSupplierType
        /// </summary>
        virtual public System.String SRSupplierType
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.SRSupplierType);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.SRSupplierType, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ContractNumber
        /// </summary>
        virtual public System.String ContractNumber
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.ContractNumber);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.ContractNumber, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ContractStart
        /// </summary>
        virtual public System.DateTime? ContractStart
        {
            get
            {
                return base.GetSystemDateTime(SupplierMetadata.ColumnNames.ContractStart);
            }

            set
            {
                base.SetSystemDateTime(SupplierMetadata.ColumnNames.ContractStart, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ContractEnd
        /// </summary>
        virtual public System.DateTime? ContractEnd
        {
            get
            {
                return base.GetSystemDateTime(SupplierMetadata.ColumnNames.ContractEnd);
            }

            set
            {
                base.SetSystemDateTime(SupplierMetadata.ColumnNames.ContractEnd, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ContractSummary
        /// </summary>
        virtual public System.String ContractSummary
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.ContractSummary);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.ContractSummary, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ContactPerson
        /// </summary>
        virtual public System.String ContactPerson
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.ContactPerson);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.ContactPerson, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.IsPKP
        /// </summary>
        virtual public System.Boolean? IsPKP
        {
            get
            {
                return base.GetSystemBoolean(SupplierMetadata.ColumnNames.IsPKP);
            }

            set
            {
                base.SetSystemBoolean(SupplierMetadata.ColumnNames.IsPKP, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.TaxRegistrationNo
        /// </summary>
        virtual public System.String TaxRegistrationNo
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.TaxRegistrationNo);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.TaxRegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.TermOfPayment
        /// </summary>
        virtual public System.Decimal? TermOfPayment
        {
            get
            {
                return base.GetSystemDecimal(SupplierMetadata.ColumnNames.TermOfPayment);
            }

            set
            {
                base.SetSystemDecimal(SupplierMetadata.ColumnNames.TermOfPayment, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.LeadTime
        /// </summary>
        virtual public System.Byte? LeadTime
        {
            get
            {
                return base.GetSystemByte(SupplierMetadata.ColumnNames.LeadTime);
            }

            set
            {
                base.SetSystemByte(SupplierMetadata.ColumnNames.LeadTime, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.CreditLimit
        /// </summary>
        virtual public System.Decimal? CreditLimit
        {
            get
            {
                return base.GetSystemDecimal(SupplierMetadata.ColumnNames.CreditLimit);
            }

            set
            {
                base.SetSystemDecimal(SupplierMetadata.ColumnNames.CreditLimit, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(SupplierMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(SupplierMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.StreetName
        /// </summary>
        virtual public System.String StreetName
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.StreetName);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.StreetName, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.District
        /// </summary>
        virtual public System.String District
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.District);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.District, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.City
        /// </summary>
        virtual public System.String City
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.City);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.City, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.County
        /// </summary>
        virtual public System.String County
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.County);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.County, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.State
        /// </summary>
        virtual public System.String State
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.State);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.State, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ZipCode
        /// </summary>
        virtual public System.String ZipCode
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.ZipCode);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.ZipCode, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.PhoneNo
        /// </summary>
        virtual public System.String PhoneNo
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.PhoneNo);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.PhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.FaxNo
        /// </summary>
        virtual public System.String FaxNo
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.FaxNo);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.FaxNo, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.Email
        /// </summary>
        virtual public System.String Email
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.Email);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.Email, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.MobilePhoneNo
        /// </summary>
        virtual public System.String MobilePhoneNo
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.MobilePhoneNo);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.MobilePhoneNo, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(SupplierMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(SupplierMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdAP
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdAP
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAP);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAP, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdAP
        /// </summary>
        virtual public System.Int32? SubledgerIdAP
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAP);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAP, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdAPInProcess
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdAPInProcess
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPInProcess);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPInProcess, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdAPInProcess
        /// </summary>
        virtual public System.Int32? SubledgerIdAPInProcess
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPInProcess);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPInProcess, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.TaxPercentage
        /// </summary>
        virtual public System.Decimal? TaxPercentage
        {
            get
            {
                return base.GetSystemDecimal(SupplierMetadata.ColumnNames.TaxPercentage);
            }

            set
            {
                base.SetSystemDecimal(SupplierMetadata.ColumnNames.TaxPercentage, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdAPTemporary
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdAPTemporary
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporary);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporary, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdAPTemporary
        /// </summary>
        virtual public System.Int32? SubledgerIdAPTemporary
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPTemporary);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPTemporary, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdAPCost
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdAPCost
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPCost);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPCost, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdAPCost
        /// </summary>
        virtual public System.Int32? SubledgerIdAPCost
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPCost);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPCost, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.PBFLicenseNo
        /// </summary>
        virtual public System.String PBFLicenseNo
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.PBFLicenseNo);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.PBFLicenseNo, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.PBFLicenseValidDate
        /// </summary>
        virtual public System.DateTime? PBFLicenseValidDate
        {
            get
            {
                return base.GetSystemDateTime(SupplierMetadata.ColumnNames.PBFLicenseValidDate);
            }

            set
            {
                base.SetSystemDateTime(SupplierMetadata.ColumnNames.PBFLicenseValidDate, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.BankAccountNo
        /// </summary>
        virtual public System.String BankAccountNo
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.BankAccountNo);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.BankAccountNo, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.BankName
        /// </summary>
        virtual public System.String BankName
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.BankName);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.BankName, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdPOReturn
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdPOReturn
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturn);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturn, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdPOReturn
        /// </summary>
        virtual public System.Int32? SubledgerIdPOReturn
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdPOReturn);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdPOReturn, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SRApAgingDateType
        /// </summary>
        virtual public System.String SRApAgingDateType
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.SRApAgingDateType);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.SRApAgingDateType, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdAPNonMedic
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdAPNonMedic
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPNonMedic);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPNonMedic, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdAPNonMedic
        /// </summary>
        virtual public System.Int32? SubledgerIdAPNonMedic
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPNonMedic);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPNonMedic, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdAPTemporaryNonMedic
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdAPTemporaryNonMedic
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporaryNonMedic);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporaryNonMedic, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdAPTemporaryNonMedic
        /// </summary>
        virtual public System.Int32? SubledgerIdAPTemporaryNonMedic
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPTemporaryNonMedic);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdAPTemporaryNonMedic, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdPOReturnNonMedic
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdPOReturnNonMedic
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturnNonMedic);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturnNonMedic, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdPOReturnNonMedic
        /// </summary>
        virtual public System.Int32? SubledgerIdPOReturnNonMedic
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdPOReturnNonMedic);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdPOReturnNonMedic, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.Branch
        /// </summary>
        virtual public System.String Branch
        {
            get
            {
                return base.GetSystemString(SupplierMetadata.ColumnNames.Branch);
            }

            set
            {
                base.SetSystemString(SupplierMetadata.ColumnNames.Branch, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdGrantReceive
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdGrantReceive
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceive);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceive, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdGrantReceive
        /// </summary>
        virtual public System.Int32? SubledgerIdGrantReceive
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdGrantReceive);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdGrantReceive, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.ChartOfAccountIdGrantReceiveNmed
        /// </summary>
        virtual public System.Int32? ChartOfAccountIdGrantReceiveNmed
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceiveNmed);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceiveNmed, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.SubledgerIdGrantReceiveNmed
        /// </summary>
        virtual public System.Int32? SubledgerIdGrantReceiveNmed
        {
            get
            {
                return base.GetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdGrantReceiveNmed);
            }

            set
            {
                base.SetSystemInt32(SupplierMetadata.ColumnNames.SubledgerIdGrantReceiveNmed, value);
            }
        }
        /// <summary>
        /// Maps to Supplier.IsUsingRounding
        /// </summary>
        virtual public System.Boolean? IsUsingRounding
        {
            get
            {
                return base.GetSystemBoolean(SupplierMetadata.ColumnNames.IsUsingRounding);
            }

            set
            {
                base.SetSystemBoolean(SupplierMetadata.ColumnNames.IsUsingRounding, value);
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
            public esStrings(esSupplier entity)
            {
                this.entity = entity;
            }
            public System.String SupplierID
            {
                get
                {
                    System.String data = entity.SupplierID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierID = null;
                    else entity.SupplierID = Convert.ToString(value);
                }
            }
            public System.String SupplierName
            {
                get
                {
                    System.String data = entity.SupplierName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SupplierName = null;
                    else entity.SupplierName = Convert.ToString(value);
                }
            }
            public System.String ShortName
            {
                get
                {
                    System.String data = entity.ShortName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ShortName = null;
                    else entity.ShortName = Convert.ToString(value);
                }
            }
            public System.String SRSupplierType
            {
                get
                {
                    System.String data = entity.SRSupplierType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRSupplierType = null;
                    else entity.SRSupplierType = Convert.ToString(value);
                }
            }
            public System.String ContractNumber
            {
                get
                {
                    System.String data = entity.ContractNumber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractNumber = null;
                    else entity.ContractNumber = Convert.ToString(value);
                }
            }
            public System.String ContractStart
            {
                get
                {
                    System.DateTime? data = entity.ContractStart;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractStart = null;
                    else entity.ContractStart = Convert.ToDateTime(value);
                }
            }
            public System.String ContractEnd
            {
                get
                {
                    System.DateTime? data = entity.ContractEnd;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractEnd = null;
                    else entity.ContractEnd = Convert.ToDateTime(value);
                }
            }
            public System.String ContractSummary
            {
                get
                {
                    System.String data = entity.ContractSummary;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContractSummary = null;
                    else entity.ContractSummary = Convert.ToString(value);
                }
            }
            public System.String ContactPerson
            {
                get
                {
                    System.String data = entity.ContactPerson;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ContactPerson = null;
                    else entity.ContactPerson = Convert.ToString(value);
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
            public System.String TermOfPayment
            {
                get
                {
                    System.Decimal? data = entity.TermOfPayment;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TermOfPayment = null;
                    else entity.TermOfPayment = Convert.ToDecimal(value);
                }
            }
            public System.String LeadTime
            {
                get
                {
                    System.Byte? data = entity.LeadTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LeadTime = null;
                    else entity.LeadTime = Convert.ToByte(value);
                }
            }
            public System.String CreditLimit
            {
                get
                {
                    System.Decimal? data = entity.CreditLimit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreditLimit = null;
                    else entity.CreditLimit = Convert.ToDecimal(value);
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
            public System.String ChartOfAccountIdAP
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdAP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdAP = null;
                    else entity.ChartOfAccountIdAP = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdAP
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdAP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdAP = null;
                    else entity.SubledgerIdAP = Convert.ToInt32(value);
                }
            }
            public System.String ChartOfAccountIdAPInProcess
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdAPInProcess;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdAPInProcess = null;
                    else entity.ChartOfAccountIdAPInProcess = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdAPInProcess
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdAPInProcess;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdAPInProcess = null;
                    else entity.SubledgerIdAPInProcess = Convert.ToInt32(value);
                }
            }
            public System.String TaxPercentage
            {
                get
                {
                    System.Decimal? data = entity.TaxPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TaxPercentage = null;
                    else entity.TaxPercentage = Convert.ToDecimal(value);
                }
            }
            public System.String ChartOfAccountIdAPTemporary
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdAPTemporary;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdAPTemporary = null;
                    else entity.ChartOfAccountIdAPTemporary = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdAPTemporary
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdAPTemporary;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdAPTemporary = null;
                    else entity.SubledgerIdAPTemporary = Convert.ToInt32(value);
                }
            }
            public System.String ChartOfAccountIdAPCost
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdAPCost;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdAPCost = null;
                    else entity.ChartOfAccountIdAPCost = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdAPCost
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdAPCost;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdAPCost = null;
                    else entity.SubledgerIdAPCost = Convert.ToInt32(value);
                }
            }
            public System.String PBFLicenseNo
            {
                get
                {
                    System.String data = entity.PBFLicenseNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PBFLicenseNo = null;
                    else entity.PBFLicenseNo = Convert.ToString(value);
                }
            }
            public System.String PBFLicenseValidDate
            {
                get
                {
                    System.DateTime? data = entity.PBFLicenseValidDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PBFLicenseValidDate = null;
                    else entity.PBFLicenseValidDate = Convert.ToDateTime(value);
                }
            }
            public System.String BankAccountNo
            {
                get
                {
                    System.String data = entity.BankAccountNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankAccountNo = null;
                    else entity.BankAccountNo = Convert.ToString(value);
                }
            }
            public System.String BankName
            {
                get
                {
                    System.String data = entity.BankName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BankName = null;
                    else entity.BankName = Convert.ToString(value);
                }
            }
            public System.String ChartOfAccountIdPOReturn
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdPOReturn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdPOReturn = null;
                    else entity.ChartOfAccountIdPOReturn = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdPOReturn
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdPOReturn;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdPOReturn = null;
                    else entity.SubledgerIdPOReturn = Convert.ToInt32(value);
                }
            }
            public System.String SRApAgingDateType
            {
                get
                {
                    System.String data = entity.SRApAgingDateType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRApAgingDateType = null;
                    else entity.SRApAgingDateType = Convert.ToString(value);
                }
            }
            public System.String ChartOfAccountIdAPNonMedic
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdAPNonMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdAPNonMedic = null;
                    else entity.ChartOfAccountIdAPNonMedic = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdAPNonMedic
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdAPNonMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdAPNonMedic = null;
                    else entity.SubledgerIdAPNonMedic = Convert.ToInt32(value);
                }
            }
            public System.String ChartOfAccountIdAPTemporaryNonMedic
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdAPTemporaryNonMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdAPTemporaryNonMedic = null;
                    else entity.ChartOfAccountIdAPTemporaryNonMedic = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdAPTemporaryNonMedic
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdAPTemporaryNonMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdAPTemporaryNonMedic = null;
                    else entity.SubledgerIdAPTemporaryNonMedic = Convert.ToInt32(value);
                }
            }
            public System.String ChartOfAccountIdPOReturnNonMedic
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdPOReturnNonMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdPOReturnNonMedic = null;
                    else entity.ChartOfAccountIdPOReturnNonMedic = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdPOReturnNonMedic
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdPOReturnNonMedic;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdPOReturnNonMedic = null;
                    else entity.SubledgerIdPOReturnNonMedic = Convert.ToInt32(value);
                }
            }
            public System.String Branch
            {
                get
                {
                    System.String data = entity.Branch;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Branch = null;
                    else entity.Branch = Convert.ToString(value);
                }
            }
            public System.String ChartOfAccountIdGrantReceive
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdGrantReceive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdGrantReceive = null;
                    else entity.ChartOfAccountIdGrantReceive = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdGrantReceive
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdGrantReceive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdGrantReceive = null;
                    else entity.SubledgerIdGrantReceive = Convert.ToInt32(value);
                }
            }
            public System.String ChartOfAccountIdGrantReceiveNmed
            {
                get
                {
                    System.Int32? data = entity.ChartOfAccountIdGrantReceiveNmed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChartOfAccountIdGrantReceiveNmed = null;
                    else entity.ChartOfAccountIdGrantReceiveNmed = Convert.ToInt32(value);
                }
            }
            public System.String SubledgerIdGrantReceiveNmed
            {
                get
                {
                    System.Int32? data = entity.SubledgerIdGrantReceiveNmed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SubledgerIdGrantReceiveNmed = null;
                    else entity.SubledgerIdGrantReceiveNmed = Convert.ToInt32(value);
                }
            }
            public System.String IsUsingRounding
            {
                get
                {
                    System.Boolean? data = entity.IsUsingRounding;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsUsingRounding = null;
                    else entity.IsUsingRounding = Convert.ToBoolean(value);
                }
            }
            private esSupplier entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSupplierQuery query)
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
                throw new Exception("esSupplier can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Supplier : esSupplier
    {
    }

    [Serializable]
    abstract public class esSupplierQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return SupplierMetadata.Meta();
            }
        }

        public esQueryItem SupplierID
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SupplierID, esSystemType.String);
            }
        }

        public esQueryItem SupplierName
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SupplierName, esSystemType.String);
            }
        }

        public esQueryItem ShortName
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ShortName, esSystemType.String);
            }
        }

        public esQueryItem SRSupplierType
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SRSupplierType, esSystemType.String);
            }
        }

        public esQueryItem ContractNumber
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ContractNumber, esSystemType.String);
            }
        }

        public esQueryItem ContractStart
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ContractStart, esSystemType.DateTime);
            }
        }

        public esQueryItem ContractEnd
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ContractEnd, esSystemType.DateTime);
            }
        }

        public esQueryItem ContractSummary
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ContractSummary, esSystemType.String);
            }
        }

        public esQueryItem ContactPerson
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ContactPerson, esSystemType.String);
            }
        }

        public esQueryItem IsPKP
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.IsPKP, esSystemType.Boolean);
            }
        }

        public esQueryItem TaxRegistrationNo
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.TaxRegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem TermOfPayment
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.TermOfPayment, esSystemType.Decimal);
            }
        }

        public esQueryItem LeadTime
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.LeadTime, esSystemType.Byte);
            }
        }

        public esQueryItem CreditLimit
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.CreditLimit, esSystemType.Decimal);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem StreetName
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.StreetName, esSystemType.String);
            }
        }

        public esQueryItem District
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.District, esSystemType.String);
            }
        }

        public esQueryItem City
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.City, esSystemType.String);
            }
        }

        public esQueryItem County
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.County, esSystemType.String);
            }
        }

        public esQueryItem State
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.State, esSystemType.String);
            }
        }

        public esQueryItem ZipCode
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ZipCode, esSystemType.String);
            }
        }

        public esQueryItem PhoneNo
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.PhoneNo, esSystemType.String);
            }
        }

        public esQueryItem FaxNo
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.FaxNo, esSystemType.String);
            }
        }

        public esQueryItem Email
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.Email, esSystemType.String);
            }
        }

        public esQueryItem MobilePhoneNo
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ChartOfAccountIdAP
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdAP, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdAP
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdAP, esSystemType.Int32);
            }
        }

        public esQueryItem ChartOfAccountIdAPInProcess
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdAPInProcess, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdAPInProcess
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdAPInProcess, esSystemType.Int32);
            }
        }

        public esQueryItem TaxPercentage
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.TaxPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem ChartOfAccountIdAPTemporary
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporary, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdAPTemporary
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdAPTemporary, esSystemType.Int32);
            }
        }

        public esQueryItem ChartOfAccountIdAPCost
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdAPCost, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdAPCost
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdAPCost, esSystemType.Int32);
            }
        }

        public esQueryItem PBFLicenseNo
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.PBFLicenseNo, esSystemType.String);
            }
        }

        public esQueryItem PBFLicenseValidDate
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.PBFLicenseValidDate, esSystemType.DateTime);
            }
        }

        public esQueryItem BankAccountNo
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.BankAccountNo, esSystemType.String);
            }
        }

        public esQueryItem BankName
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.BankName, esSystemType.String);
            }
        }

        public esQueryItem ChartOfAccountIdPOReturn
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturn, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdPOReturn
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdPOReturn, esSystemType.Int32);
            }
        }

        public esQueryItem SRApAgingDateType
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SRApAgingDateType, esSystemType.String);
            }
        }

        public esQueryItem ChartOfAccountIdAPNonMedic
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdAPNonMedic, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdAPNonMedic
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdAPNonMedic, esSystemType.Int32);
            }
        }

        public esQueryItem ChartOfAccountIdAPTemporaryNonMedic
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporaryNonMedic, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdAPTemporaryNonMedic
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdAPTemporaryNonMedic, esSystemType.Int32);
            }
        }

        public esQueryItem ChartOfAccountIdPOReturnNonMedic
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturnNonMedic, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdPOReturnNonMedic
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdPOReturnNonMedic, esSystemType.Int32);
            }
        }

        public esQueryItem Branch
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.Branch, esSystemType.String);
            }
        }

        public esQueryItem ChartOfAccountIdGrantReceive
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceive, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdGrantReceive
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdGrantReceive, esSystemType.Int32);
            }
        }

        public esQueryItem ChartOfAccountIdGrantReceiveNmed
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceiveNmed, esSystemType.Int32);
            }
        }

        public esQueryItem SubledgerIdGrantReceiveNmed
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.SubledgerIdGrantReceiveNmed, esSystemType.Int32);
            }
        }

        public esQueryItem IsUsingRounding
        {
            get
            {
                return new esQueryItem(this, SupplierMetadata.ColumnNames.IsUsingRounding, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SupplierCollection")]
    public partial class SupplierCollection : esSupplierCollection, IEnumerable<Supplier>
    {
        public SupplierCollection()
        {

        }

        public static implicit operator List<Supplier>(SupplierCollection coll)
        {
            List<Supplier> list = new List<Supplier>();

            foreach (Supplier emp in coll)
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
                return SupplierMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Supplier(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Supplier();
        }

        #endregion

        [BrowsableAttribute(false)]
        public SupplierQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierQuery();
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
        public bool Load(SupplierQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Supplier AddNew()
        {
            Supplier entity = base.AddNewEntity() as Supplier;

            return entity;
        }
        public Supplier FindByPrimaryKey(String supplierID)
        {
            return base.FindByPrimaryKey(supplierID) as Supplier;
        }

        #region IEnumerable< Supplier> Members

        IEnumerator<Supplier> IEnumerable<Supplier>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Supplier;
            }
        }

        #endregion

        private SupplierQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Supplier' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Supplier ({SupplierID})")]
    [Serializable]
    public partial class Supplier : esSupplier
    {
        public Supplier()
        {
        }

        public Supplier(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SupplierMetadata.Meta();
            }
        }

        override protected esSupplierQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SupplierQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public SupplierQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SupplierQuery();
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
        public bool Load(SupplierQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SupplierQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class SupplierQuery : esSupplierQuery
    {
        public SupplierQuery()
        {

        }

        public SupplierQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SupplierQuery";
        }
    }

    [Serializable]
    public partial class SupplierMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SupplierMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SupplierID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.SupplierID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SupplierName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.SupplierName;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.ShortName;
            c.CharacterMaxLength = 35;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SRSupplierType, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.SRSupplierType;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ContractNumber, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.ContractNumber;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ContractStart, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierMetadata.PropertyNames.ContractStart;
            c.HasDefault = true;
            c.Default = @"(CONVERT([smalldatetime],'19000101',(105)))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ContractEnd, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierMetadata.PropertyNames.ContractEnd;
            c.HasDefault = true;
            c.Default = @"(CONVERT([smalldatetime],'19000101',(105)))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ContractSummary, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.ContractSummary;
            c.CharacterMaxLength = 16;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ContactPerson, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.ContactPerson;
            c.CharacterMaxLength = 100;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.IsPKP, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SupplierMetadata.PropertyNames.IsPKP;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.TaxRegistrationNo, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.TaxRegistrationNo;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.TermOfPayment, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierMetadata.PropertyNames.TermOfPayment;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.LeadTime, 12, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = SupplierMetadata.PropertyNames.LeadTime;
            c.NumericPrecision = 3;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.CreditLimit, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierMetadata.PropertyNames.CreditLimit;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.IsActive, 14, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SupplierMetadata.PropertyNames.IsActive;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.StreetName, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.StreetName;
            c.CharacterMaxLength = 250;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.District, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.District;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.City, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.City;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.County, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.County;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.State, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.State;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ZipCode, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.ZipCode;
            c.CharacterMaxLength = 15;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.PhoneNo, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.PhoneNo;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.FaxNo, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.FaxNo;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.Email, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.Email;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.MobilePhoneNo, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.MobilePhoneNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.LastUpdateDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.LastUpdateByUserID, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdAP, 27, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdAP;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdAP, 28, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdAP;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdAPInProcess, 29, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdAPInProcess;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdAPInProcess, 30, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdAPInProcess;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.TaxPercentage, 31, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = SupplierMetadata.PropertyNames.TaxPercentage;
            c.NumericPrecision = 5;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporary, 32, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdAPTemporary;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdAPTemporary, 33, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdAPTemporary;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdAPCost, 34, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdAPCost;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdAPCost, 35, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdAPCost;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.PBFLicenseNo, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.PBFLicenseNo;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.PBFLicenseValidDate, 37, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = SupplierMetadata.PropertyNames.PBFLicenseValidDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.BankAccountNo, 38, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.BankAccountNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.BankName, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.BankName;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturn, 40, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdPOReturn;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdPOReturn, 41, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdPOReturn;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SRApAgingDateType, 42, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.SRApAgingDateType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdAPNonMedic, 43, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdAPNonMedic;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdAPNonMedic, 44, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdAPNonMedic;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdAPTemporaryNonMedic, 45, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdAPTemporaryNonMedic;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdAPTemporaryNonMedic, 46, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdAPTemporaryNonMedic;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdPOReturnNonMedic, 47, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdPOReturnNonMedic;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdPOReturnNonMedic, 48, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdPOReturnNonMedic;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.Branch, 49, typeof(System.String), esSystemType.String);
            c.PropertyName = SupplierMetadata.PropertyNames.Branch;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceive, 50, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdGrantReceive;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdGrantReceive, 51, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdGrantReceive;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.ChartOfAccountIdGrantReceiveNmed, 52, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.ChartOfAccountIdGrantReceiveNmed;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.SubledgerIdGrantReceiveNmed, 53, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = SupplierMetadata.PropertyNames.SubledgerIdGrantReceiveNmed;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SupplierMetadata.ColumnNames.IsUsingRounding, 54, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = SupplierMetadata.PropertyNames.IsUsingRounding;
            _columns.Add(c);

        }
        #endregion

        static public SupplierMetadata Meta()
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
            public const string SupplierID = "SupplierID";
            public const string SupplierName = "SupplierName";
            public const string ShortName = "ShortName";
            public const string SRSupplierType = "SRSupplierType";
            public const string ContractNumber = "ContractNumber";
            public const string ContractStart = "ContractStart";
            public const string ContractEnd = "ContractEnd";
            public const string ContractSummary = "ContractSummary";
            public const string ContactPerson = "ContactPerson";
            public const string IsPKP = "IsPKP";
            public const string TaxRegistrationNo = "TaxRegistrationNo";
            public const string TermOfPayment = "TermOfPayment";
            public const string LeadTime = "LeadTime";
            public const string CreditLimit = "CreditLimit";
            public const string IsActive = "IsActive";
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
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ChartOfAccountIdAP = "ChartOfAccountIdAP";
            public const string SubledgerIdAP = "SubledgerIdAP";
            public const string ChartOfAccountIdAPInProcess = "ChartOfAccountIdAPInProcess";
            public const string SubledgerIdAPInProcess = "SubledgerIdAPInProcess";
            public const string TaxPercentage = "TaxPercentage";
            public const string ChartOfAccountIdAPTemporary = "ChartOfAccountIdAPTemporary";
            public const string SubledgerIdAPTemporary = "SubledgerIdAPTemporary";
            public const string ChartOfAccountIdAPCost = "ChartOfAccountIdAPCost";
            public const string SubledgerIdAPCost = "SubledgerIdAPCost";
            public const string PBFLicenseNo = "PBFLicenseNo";
            public const string PBFLicenseValidDate = "PBFLicenseValidDate";
            public const string BankAccountNo = "BankAccountNo";
            public const string BankName = "BankName";
            public const string ChartOfAccountIdPOReturn = "ChartOfAccountIdPOReturn";
            public const string SubledgerIdPOReturn = "SubledgerIdPOReturn";
            public const string SRApAgingDateType = "SRApAgingDateType";
            public const string ChartOfAccountIdAPNonMedic = "ChartOfAccountIdAPNonMedic";
            public const string SubledgerIdAPNonMedic = "SubledgerIdAPNonMedic";
            public const string ChartOfAccountIdAPTemporaryNonMedic = "ChartOfAccountIdAPTemporaryNonMedic";
            public const string SubledgerIdAPTemporaryNonMedic = "SubledgerIdAPTemporaryNonMedic";
            public const string ChartOfAccountIdPOReturnNonMedic = "ChartOfAccountIdPOReturnNonMedic";
            public const string SubledgerIdPOReturnNonMedic = "SubledgerIdPOReturnNonMedic";
            public const string Branch = "Branch";
            public const string ChartOfAccountIdGrantReceive = "ChartOfAccountIdGrantReceive";
            public const string SubledgerIdGrantReceive = "SubledgerIdGrantReceive";
            public const string ChartOfAccountIdGrantReceiveNmed = "ChartOfAccountIdGrantReceiveNmed";
            public const string SubledgerIdGrantReceiveNmed = "SubledgerIdGrantReceiveNmed";
            public const string IsUsingRounding = "IsUsingRounding";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SupplierID = "SupplierID";
            public const string SupplierName = "SupplierName";
            public const string ShortName = "ShortName";
            public const string SRSupplierType = "SRSupplierType";
            public const string ContractNumber = "ContractNumber";
            public const string ContractStart = "ContractStart";
            public const string ContractEnd = "ContractEnd";
            public const string ContractSummary = "ContractSummary";
            public const string ContactPerson = "ContactPerson";
            public const string IsPKP = "IsPKP";
            public const string TaxRegistrationNo = "TaxRegistrationNo";
            public const string TermOfPayment = "TermOfPayment";
            public const string LeadTime = "LeadTime";
            public const string CreditLimit = "CreditLimit";
            public const string IsActive = "IsActive";
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
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ChartOfAccountIdAP = "ChartOfAccountIdAP";
            public const string SubledgerIdAP = "SubledgerIdAP";
            public const string ChartOfAccountIdAPInProcess = "ChartOfAccountIdAPInProcess";
            public const string SubledgerIdAPInProcess = "SubledgerIdAPInProcess";
            public const string TaxPercentage = "TaxPercentage";
            public const string ChartOfAccountIdAPTemporary = "ChartOfAccountIdAPTemporary";
            public const string SubledgerIdAPTemporary = "SubledgerIdAPTemporary";
            public const string ChartOfAccountIdAPCost = "ChartOfAccountIdAPCost";
            public const string SubledgerIdAPCost = "SubledgerIdAPCost";
            public const string PBFLicenseNo = "PBFLicenseNo";
            public const string PBFLicenseValidDate = "PBFLicenseValidDate";
            public const string BankAccountNo = "BankAccountNo";
            public const string BankName = "BankName";
            public const string ChartOfAccountIdPOReturn = "ChartOfAccountIdPOReturn";
            public const string SubledgerIdPOReturn = "SubledgerIdPOReturn";
            public const string SRApAgingDateType = "SRApAgingDateType";
            public const string ChartOfAccountIdAPNonMedic = "ChartOfAccountIdAPNonMedic";
            public const string SubledgerIdAPNonMedic = "SubledgerIdAPNonMedic";
            public const string ChartOfAccountIdAPTemporaryNonMedic = "ChartOfAccountIdAPTemporaryNonMedic";
            public const string SubledgerIdAPTemporaryNonMedic = "SubledgerIdAPTemporaryNonMedic";
            public const string ChartOfAccountIdPOReturnNonMedic = "ChartOfAccountIdPOReturnNonMedic";
            public const string SubledgerIdPOReturnNonMedic = "SubledgerIdPOReturnNonMedic";
            public const string Branch = "Branch";
            public const string ChartOfAccountIdGrantReceive = "ChartOfAccountIdGrantReceive";
            public const string SubledgerIdGrantReceive = "SubledgerIdGrantReceive";
            public const string ChartOfAccountIdGrantReceiveNmed = "ChartOfAccountIdGrantReceiveNmed";
            public const string SubledgerIdGrantReceiveNmed = "SubledgerIdGrantReceiveNmed";
            public const string IsUsingRounding = "IsUsingRounding";
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
            lock (typeof(SupplierMetadata))
            {
                if (SupplierMetadata.mapDelegates == null)
                {
                    SupplierMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SupplierMetadata.meta == null)
                {
                    SupplierMetadata.meta = new SupplierMetadata();
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

                meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SupplierName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRSupplierType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContractNumber", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ContractStart", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("ContractEnd", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("ContractSummary", new esTypeMap("text", "System.String"));
                meta.AddTypeMap("ContactPerson", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPKP", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("TaxRegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TermOfPayment", new esTypeMap("decimal", "System.Decimal"));
                meta.AddTypeMap("LeadTime", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("CreditLimit", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
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
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChartOfAccountIdAP", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdAP", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChartOfAccountIdAPInProcess", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdAPInProcess", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TaxPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ChartOfAccountIdAPTemporary", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdAPTemporary", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChartOfAccountIdAPCost", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdAPCost", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PBFLicenseNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PBFLicenseValidDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BankName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChartOfAccountIdPOReturn", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdPOReturn", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRApAgingDateType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChartOfAccountIdAPNonMedic", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdAPNonMedic", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChartOfAccountIdAPTemporaryNonMedic", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdAPTemporaryNonMedic", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChartOfAccountIdPOReturnNonMedic", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdPOReturnNonMedic", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Branch", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChartOfAccountIdGrantReceive", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdGrantReceive", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ChartOfAccountIdGrantReceiveNmed", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SubledgerIdGrantReceiveNmed", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IsUsingRounding", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "Supplier";
                meta.Destination = "Supplier";
                meta.spInsert = "proc_SupplierInsert";
                meta.spUpdate = "proc_SupplierUpdate";
                meta.spDelete = "proc_SupplierDelete";
                meta.spLoadAll = "proc_SupplierLoadAll";
                meta.spLoadByPrimaryKey = "proc_SupplierLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SupplierMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
