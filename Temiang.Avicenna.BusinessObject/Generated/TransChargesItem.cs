/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 30/07/2024 14:11:33
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
    abstract public class esTransChargesItemCollection : esEntityCollectionWAuditLog
    {
        public esTransChargesItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransChargesItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransChargesItemQuery query)
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
            this.InitQuery(query as esTransChargesItemQuery);
        }
        #endregion

        virtual public TransChargesItem DetachEntity(TransChargesItem entity)
        {
            return base.DetachEntity(entity) as TransChargesItem;
        }

        virtual public TransChargesItem AttachEntity(TransChargesItem entity)
        {
            return base.AttachEntity(entity) as TransChargesItem;
        }

        virtual public void Combine(TransChargesItemCollection collection)
        {
            base.Combine(collection);
        }

        new public TransChargesItem this[int index]
        {
            get
            {
                return base[index] as TransChargesItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransChargesItem);
        }
    }

    [Serializable]
    abstract public class esTransChargesItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransChargesItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransChargesItem()
        {
        }

        public esTransChargesItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo)
        {
            esTransChargesItemQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
                        case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ChargeClassID": this.str.ChargeClassID = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "SecondParamedicID": this.str.SecondParamedicID = (string)value; break;
                        case "IsAdminCalculation": this.str.IsAdminCalculation = (string)value; break;
                        case "IsVariable": this.str.IsVariable = (string)value; break;
                        case "IsCito": this.str.IsCito = (string)value; break;
                        case "ChargeQuantity": this.str.ChargeQuantity = (string)value; break;
                        case "StockQuantity": this.str.StockQuantity = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "CostPrice": this.str.CostPrice = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
                        case "CitoAmount": this.str.CitoAmount = (string)value; break;
                        case "RoundingAmount": this.str.RoundingAmount = (string)value; break;
                        case "SRDiscountReason": this.str.SRDiscountReason = (string)value; break;
                        case "IsAssetUtilization": this.str.IsAssetUtilization = (string)value; break;
                        case "AssetID": this.str.AssetID = (string)value; break;
                        case "IsBillProceed": this.str.IsBillProceed = (string)value; break;
                        case "IsOrderRealization": this.str.IsOrderRealization = (string)value; break;
                        case "IsPackage": this.str.IsPackage = (string)value; break;
                        case "IsApprove": this.str.IsApprove = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "FilmNo": this.str.FilmNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ParentNo": this.str.ParentNo = (string)value; break;
                        case "SRCenterID": this.str.SRCenterID = (string)value; break;
                        case "AutoProcessCalculation": this.str.AutoProcessCalculation = (string)value; break;
                        case "ParamedicCollectionName": this.str.ParamedicCollectionName = (string)value; break;
                        case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
                        case "RealizationDateTime": this.str.RealizationDateTime = (string)value; break;
                        case "RealizationUserID": this.str.RealizationUserID = (string)value; break;
                        case "UpdateRealizationDateTime": this.str.UpdateRealizationDateTime = (string)value; break;
                        case "UpdateRealizationUserID": this.str.UpdateRealizationUserID = (string)value; break;
                        case "IsCitoInPercent": this.str.IsCitoInPercent = (string)value; break;
                        case "BasicCitoAmount": this.str.BasicCitoAmount = (string)value; break;
                        case "IsItemRoom": this.str.IsItemRoom = (string)value; break;
                        case "IsExtraItem": this.str.IsExtraItem = (string)value; break;
                        case "IsSelectedExtraItem": this.str.IsSelectedExtraItem = (string)value; break;
                        case "IsSendToLIS": this.str.IsSendToLIS = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "IsCorrection": this.str.IsCorrection = (string)value; break;
                        case "ResultValue": this.str.ResultValue = (string)value; break;
                        case "IsDuplo": this.str.IsDuplo = (string)value; break;
                        case "IsPaymentConfirmed": this.str.IsPaymentConfirmed = (string)value; break;
                        case "PaymentConfirmedDateTime": this.str.PaymentConfirmedDateTime = (string)value; break;
                        case "PaymentConfirmedBy": this.str.PaymentConfirmedBy = (string)value; break;
                        case "LastPaymentConfirmedDateTime": this.str.LastPaymentConfirmedDateTime = (string)value; break;
                        case "LastPaymentConfirmedByUserID": this.str.LastPaymentConfirmedByUserID = (string)value; break;
                        case "IsDescriptionResult": this.str.IsDescriptionResult = (string)value; break;
                        case "PriceAdjusted": this.str.PriceAdjusted = (string)value; break;
                        case "SRCitoPercentage": this.str.SRCitoPercentage = (string)value; break;
                        case "ItemConditionRuleID": this.str.ItemConditionRuleID = (string)value; break;
                        case "CommunicationID": this.str.CommunicationID = (string)value; break;
                        case "IsCasemixApproved": this.str.IsCasemixApproved = (string)value; break;
                        case "CasemixApprovedDateTime": this.str.CasemixApprovedDateTime = (string)value; break;
                        case "CasemixApprovedByUserID": this.str.CasemixApprovedByUserID = (string)value; break;
                        case "TariffDate": this.str.TariffDate = (string)value; break;
                        case "SpecimenTakenDateTime": this.str.SpecimenTakenDateTime = (string)value; break;
                        case "SpecimenTakenByUserID": this.str.SpecimenTakenByUserID = (string)value; break;
                        case "SpecimenSubmittedDateTime": this.str.SpecimenSubmittedDateTime = (string)value; break;
                        case "SpecimenSubmittedByUserID": this.str.SpecimenSubmittedByUserID = (string)value; break;
                        case "SpecimenReceivedDateTime": this.str.SpecimenReceivedDateTime = (string)value; break;
                        case "SpecimenReceivedByUserID": this.str.SpecimenReceivedByUserID = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "CasemixNotes": this.str.CasemixNotes = (string)value; break;
                        case "SpecimenCollectDateTime": this.str.SpecimenCollectDateTime = (string)value; break;
                        case "SpecimenReceiveDateTime": this.str.SpecimenReceiveDateTime = (string)value; break;
                        case "SpecimenCollectByUserID": this.str.SpecimenCollectByUserID = (string)value; break;
                        case "SpecimenReceiveByUserID": this.str.SpecimenReceiveByUserID = (string)value; break;
                        case "SRCollectMethod": this.str.SRCollectMethod = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsAdminCalculation":

                            if (value == null || value is System.Boolean)
                                this.IsAdminCalculation = (System.Boolean?)value;
                            break;
                        case "IsVariable":

                            if (value == null || value is System.Boolean)
                                this.IsVariable = (System.Boolean?)value;
                            break;
                        case "IsCito":

                            if (value == null || value is System.Boolean)
                                this.IsCito = (System.Boolean?)value;
                            break;
                        case "ChargeQuantity":

                            if (value == null || value is System.Decimal)
                                this.ChargeQuantity = (System.Decimal?)value;
                            break;
                        case "StockQuantity":

                            if (value == null || value is System.Decimal)
                                this.StockQuantity = (System.Decimal?)value;
                            break;
                        case "CostPrice":

                            if (value == null || value is System.Decimal)
                                this.CostPrice = (System.Decimal?)value;
                            break;
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;
                        case "DiscountAmount":

                            if (value == null || value is System.Decimal)
                                this.DiscountAmount = (System.Decimal?)value;
                            break;
                        case "CitoAmount":

                            if (value == null || value is System.Decimal)
                                this.CitoAmount = (System.Decimal?)value;
                            break;
                        case "RoundingAmount":

                            if (value == null || value is System.Decimal)
                                this.RoundingAmount = (System.Decimal?)value;
                            break;
                        case "IsAssetUtilization":

                            if (value == null || value is System.Boolean)
                                this.IsAssetUtilization = (System.Boolean?)value;
                            break;
                        case "IsBillProceed":

                            if (value == null || value is System.Boolean)
                                this.IsBillProceed = (System.Boolean?)value;
                            break;
                        case "IsOrderRealization":

                            if (value == null || value is System.Boolean)
                                this.IsOrderRealization = (System.Boolean?)value;
                            break;
                        case "IsPackage":

                            if (value == null || value is System.Boolean)
                                this.IsPackage = (System.Boolean?)value;
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
                        case "AutoProcessCalculation":

                            if (value == null || value is System.Decimal)
                                this.AutoProcessCalculation = (System.Decimal?)value;
                            break;
                        case "RealizationDateTime":

                            if (value == null || value is System.DateTime)
                                this.RealizationDateTime = (System.DateTime?)value;
                            break;
                        case "UpdateRealizationDateTime":

                            if (value == null || value is System.DateTime)
                                this.UpdateRealizationDateTime = (System.DateTime?)value;
                            break;
                        case "IsCitoInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsCitoInPercent = (System.Boolean?)value;
                            break;
                        case "BasicCitoAmount":

                            if (value == null || value is System.Decimal)
                                this.BasicCitoAmount = (System.Decimal?)value;
                            break;
                        case "IsItemRoom":

                            if (value == null || value is System.Boolean)
                                this.IsItemRoom = (System.Boolean?)value;
                            break;
                        case "IsExtraItem":

                            if (value == null || value is System.Boolean)
                                this.IsExtraItem = (System.Boolean?)value;
                            break;
                        case "IsSelectedExtraItem":

                            if (value == null || value is System.Boolean)
                                this.IsSelectedExtraItem = (System.Boolean?)value;
                            break;
                        case "IsSendToLIS":

                            if (value == null || value is System.Boolean)
                                this.IsSendToLIS = (System.Boolean?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
                            break;
                        case "IsCorrection":

                            if (value == null || value is System.Boolean)
                                this.IsCorrection = (System.Boolean?)value;
                            break;
                        case "IsDuplo":

                            if (value == null || value is System.Boolean)
                                this.IsDuplo = (System.Boolean?)value;
                            break;
                        case "IsPaymentConfirmed":

                            if (value == null || value is System.Boolean)
                                this.IsPaymentConfirmed = (System.Boolean?)value;
                            break;
                        case "PaymentConfirmedDateTime":

                            if (value == null || value is System.DateTime)
                                this.PaymentConfirmedDateTime = (System.DateTime?)value;
                            break;
                        case "LastPaymentConfirmedDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastPaymentConfirmedDateTime = (System.DateTime?)value;
                            break;
                        case "IsDescriptionResult":

                            if (value == null || value is System.Boolean)
                                this.IsDescriptionResult = (System.Boolean?)value;
                            break;
                        case "PriceAdjusted":

                            if (value == null || value is System.Decimal)
                                this.PriceAdjusted = (System.Decimal?)value;
                            break;
                        case "IsCasemixApproved":

                            if (value == null || value is System.Boolean)
                                this.IsCasemixApproved = (System.Boolean?)value;
                            break;
                        case "CasemixApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CasemixApprovedDateTime = (System.DateTime?)value;
                            break;
                        case "TariffDate":

                            if (value == null || value is System.DateTime)
                                this.TariffDate = (System.DateTime?)value;
                            break;
                        case "SpecimenTakenDateTime":

                            if (value == null || value is System.DateTime)
                                this.SpecimenTakenDateTime = (System.DateTime?)value;
                            break;
                        case "SpecimenSubmittedDateTime":

                            if (value == null || value is System.DateTime)
                                this.SpecimenSubmittedDateTime = (System.DateTime?)value;
                            break;
                        case "SpecimenReceivedDateTime":

                            if (value == null || value is System.DateTime)
                                this.SpecimenReceivedDateTime = (System.DateTime?)value;
                            break;
                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
                            break;
                        case "SpecimenCollectDateTime":

                            if (value == null || value is System.DateTime)
                                this.SpecimenCollectDateTime = (System.DateTime?)value;
                            break;
                        case "SpecimenReceiveDateTime":

                            if (value == null || value is System.DateTime)
                                this.SpecimenReceiveDateTime = (System.DateTime?)value;
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
        /// Maps to TransChargesItem.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ReferenceNo
        /// </summary>
        virtual public System.String ReferenceNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ReferenceNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ReferenceNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ReferenceSequenceNo
        /// </summary>
        virtual public System.String ReferenceSequenceNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ReferenceSequenceNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ReferenceSequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ChargeClassID
        /// </summary>
        virtual public System.String ChargeClassID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ChargeClassID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ChargeClassID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SecondParamedicID
        /// </summary>
        virtual public System.String SecondParamedicID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SecondParamedicID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SecondParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsAdminCalculation
        /// </summary>
        virtual public System.Boolean? IsAdminCalculation
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsAdminCalculation);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsAdminCalculation, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsVariable
        /// </summary>
        virtual public System.Boolean? IsVariable
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsVariable);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsVariable, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsCito
        /// </summary>
        virtual public System.Boolean? IsCito
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCito);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCito, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ChargeQuantity
        /// </summary>
        virtual public System.Decimal? ChargeQuantity
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.ChargeQuantity);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.ChargeQuantity, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.StockQuantity
        /// </summary>
        virtual public System.Decimal? StockQuantity
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.StockQuantity);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.StockQuantity, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CostPrice
        /// </summary>
        virtual public System.Decimal? CostPrice
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.CostPrice);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.CostPrice, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.DiscountAmount
        /// </summary>
        virtual public System.Decimal? DiscountAmount
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.DiscountAmount);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.DiscountAmount, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CitoAmount
        /// </summary>
        virtual public System.Decimal? CitoAmount
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.CitoAmount);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.CitoAmount, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.RoundingAmount
        /// </summary>
        virtual public System.Decimal? RoundingAmount
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.RoundingAmount);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.RoundingAmount, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SRDiscountReason
        /// </summary>
        virtual public System.String SRDiscountReason
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SRDiscountReason);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SRDiscountReason, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsAssetUtilization
        /// </summary>
        virtual public System.Boolean? IsAssetUtilization
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsAssetUtilization);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsAssetUtilization, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.AssetID
        /// </summary>
        virtual public System.String AssetID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.AssetID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.AssetID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsBillProceed
        /// </summary>
        virtual public System.Boolean? IsBillProceed
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsBillProceed);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsBillProceed, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsOrderRealization
        /// </summary>
        virtual public System.Boolean? IsOrderRealization
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsOrderRealization);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsOrderRealization, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsPackage
        /// </summary>
        virtual public System.Boolean? IsPackage
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsPackage);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsPackage, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsApprove
        /// </summary>
        virtual public System.Boolean? IsApprove
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsApprove);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsApprove, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsVoid, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.FilmNo
        /// </summary>
        virtual public System.String FilmNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.FilmNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.FilmNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ParentNo
        /// </summary>
        virtual public System.String ParentNo
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ParentNo);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ParentNo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SRCenterID
        /// </summary>
        virtual public System.String SRCenterID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SRCenterID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SRCenterID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.AutoProcessCalculation
        /// </summary>
        virtual public System.Decimal? AutoProcessCalculation
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.AutoProcessCalculation);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.AutoProcessCalculation, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ParamedicCollectionName
        /// </summary>
        virtual public System.String ParamedicCollectionName
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ParamedicCollectionName);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ParamedicCollectionName, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ToServiceUnitID
        /// </summary>
        virtual public System.String ToServiceUnitID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ToServiceUnitID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ToServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.RealizationDateTime
        /// </summary>
        virtual public System.DateTime? RealizationDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.RealizationDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.RealizationDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.RealizationUserID
        /// </summary>
        virtual public System.String RealizationUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.RealizationUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.RealizationUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.UpdateRealizationDateTime
        /// </summary>
        virtual public System.DateTime? UpdateRealizationDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.UpdateRealizationDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.UpdateRealizationDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.UpdateRealizationUserID
        /// </summary>
        virtual public System.String UpdateRealizationUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.UpdateRealizationUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.UpdateRealizationUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsCitoInPercent
        /// </summary>
        virtual public System.Boolean? IsCitoInPercent
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCitoInPercent);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCitoInPercent, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.BasicCitoAmount
        /// </summary>
        virtual public System.Decimal? BasicCitoAmount
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.BasicCitoAmount);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.BasicCitoAmount, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsItemRoom
        /// </summary>
        virtual public System.Boolean? IsItemRoom
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsItemRoom);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsItemRoom, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsExtraItem
        /// </summary>
        virtual public System.Boolean? IsExtraItem
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsExtraItem);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsExtraItem, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsSelectedExtraItem
        /// </summary>
        virtual public System.Boolean? IsSelectedExtraItem
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsSelectedExtraItem);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsSelectedExtraItem, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsSendToLIS
        /// </summary>
        virtual public System.Boolean? IsSendToLIS
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsSendToLIS);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsSendToLIS, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsCorrection
        /// </summary>
        virtual public System.Boolean? IsCorrection
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCorrection);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCorrection, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ResultValue
        /// </summary>
        virtual public System.String ResultValue
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ResultValue);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ResultValue, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsDuplo
        /// </summary>
        virtual public System.Boolean? IsDuplo
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsDuplo);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsDuplo, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsPaymentConfirmed
        /// </summary>
        virtual public System.Boolean? IsPaymentConfirmed
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsPaymentConfirmed);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsPaymentConfirmed, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.PaymentConfirmedDateTime
        /// </summary>
        virtual public System.DateTime? PaymentConfirmedDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.PaymentConfirmedDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.PaymentConfirmedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.PaymentConfirmedBy
        /// </summary>
        virtual public System.String PaymentConfirmedBy
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.PaymentConfirmedBy);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.PaymentConfirmedBy, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.LastPaymentConfirmedDateTime
        /// </summary>
        virtual public System.DateTime? LastPaymentConfirmedDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.LastPaymentConfirmedByUserID
        /// </summary>
        virtual public System.String LastPaymentConfirmedByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsDescriptionResult
        /// </summary>
        virtual public System.Boolean? IsDescriptionResult
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsDescriptionResult);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsDescriptionResult, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.PriceAdjusted
        /// </summary>
        virtual public System.Decimal? PriceAdjusted
        {
            get
            {
                return base.GetSystemDecimal(TransChargesItemMetadata.ColumnNames.PriceAdjusted);
            }

            set
            {
                base.SetSystemDecimal(TransChargesItemMetadata.ColumnNames.PriceAdjusted, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SRCitoPercentage
        /// </summary>
        virtual public System.String SRCitoPercentage
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SRCitoPercentage);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SRCitoPercentage, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.ItemConditionRuleID
        /// </summary>
        virtual public System.String ItemConditionRuleID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.ItemConditionRuleID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.ItemConditionRuleID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CommunicationID
        /// </summary>
        virtual public System.String CommunicationID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.CommunicationID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.CommunicationID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.IsCasemixApproved
        /// </summary>
        virtual public System.Boolean? IsCasemixApproved
        {
            get
            {
                return base.GetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCasemixApproved);
            }

            set
            {
                base.SetSystemBoolean(TransChargesItemMetadata.ColumnNames.IsCasemixApproved, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CasemixApprovedDateTime
        /// </summary>
        virtual public System.DateTime? CasemixApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.CasemixApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.CasemixApprovedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CasemixApprovedByUserID
        /// </summary>
        virtual public System.String CasemixApprovedByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.CasemixApprovedByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.CasemixApprovedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.TariffDate
        /// </summary>
        virtual public System.DateTime? TariffDate
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.TariffDate);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.TariffDate, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenTakenDateTime
        /// </summary>
        virtual public System.DateTime? SpecimenTakenDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenTakenDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenTakenDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenTakenByUserID
        /// </summary>
        virtual public System.String SpecimenTakenByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenTakenByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenTakenByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenSubmittedDateTime
        /// </summary>
        virtual public System.DateTime? SpecimenSubmittedDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenSubmittedDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenSubmittedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenSubmittedByUserID
        /// </summary>
        virtual public System.String SpecimenSubmittedByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenSubmittedByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenSubmittedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenReceivedDateTime
        /// </summary>
        virtual public System.DateTime? SpecimenReceivedDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenReceivedDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenReceivedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenReceivedByUserID
        /// </summary>
        virtual public System.String SpecimenReceivedByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenReceivedByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenReceivedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.VoidDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.VoidByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.CasemixNotes
        /// </summary>
        virtual public System.String CasemixNotes
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.CasemixNotes);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.CasemixNotes, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenCollectDateTime
        /// </summary>
        virtual public System.DateTime? SpecimenCollectDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenCollectDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenCollectDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenReceiveDateTime
        /// </summary>
        virtual public System.DateTime? SpecimenReceiveDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenReceiveDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransChargesItemMetadata.ColumnNames.SpecimenReceiveDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenCollectByUserID
        /// </summary>
        virtual public System.String SpecimenCollectByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenCollectByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenCollectByUserID, value);
            }
        }
        /// <summary>
        /// Maps to TransChargesItem.SpecimenReceiveByUserID
        /// </summary>
        virtual public System.String SpecimenReceiveByUserID
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenReceiveByUserID);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SpecimenReceiveByUserID, value);
            }
        }        
        /// <summary>
        /// Maps to TransChargesItem.SRCollectMethod
        /// </summary>
        virtual public System.String SRCollectMethod
        {
            get
            {
                return base.GetSystemString(TransChargesItemMetadata.ColumnNames.SRCollectMethod);
            }

            set
            {
                base.SetSystemString(TransChargesItemMetadata.ColumnNames.SRCollectMethod, value);
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
            public esStrings(esTransChargesItem entity)
            {
                this.entity = entity;
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
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
            public System.String ReferenceNo
            {
                get
                {
                    System.String data = entity.ReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceNo = null;
                    else entity.ReferenceNo = Convert.ToString(value);
                }
            }
            public System.String ReferenceSequenceNo
            {
                get
                {
                    System.String data = entity.ReferenceSequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceSequenceNo = null;
                    else entity.ReferenceSequenceNo = Convert.ToString(value);
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
            public System.String SecondParamedicID
            {
                get
                {
                    System.String data = entity.SecondParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SecondParamedicID = null;
                    else entity.SecondParamedicID = Convert.ToString(value);
                }
            }
            public System.String IsAdminCalculation
            {
                get
                {
                    System.Boolean? data = entity.IsAdminCalculation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAdminCalculation = null;
                    else entity.IsAdminCalculation = Convert.ToBoolean(value);
                }
            }
            public System.String IsVariable
            {
                get
                {
                    System.Boolean? data = entity.IsVariable;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVariable = null;
                    else entity.IsVariable = Convert.ToBoolean(value);
                }
            }
            public System.String IsCito
            {
                get
                {
                    System.Boolean? data = entity.IsCito;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCito = null;
                    else entity.IsCito = Convert.ToBoolean(value);
                }
            }
            public System.String ChargeQuantity
            {
                get
                {
                    System.Decimal? data = entity.ChargeQuantity;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ChargeQuantity = null;
                    else entity.ChargeQuantity = Convert.ToDecimal(value);
                }
            }
            public System.String StockQuantity
            {
                get
                {
                    System.Decimal? data = entity.StockQuantity;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StockQuantity = null;
                    else entity.StockQuantity = Convert.ToDecimal(value);
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
            public System.String CostPrice
            {
                get
                {
                    System.Decimal? data = entity.CostPrice;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CostPrice = null;
                    else entity.CostPrice = Convert.ToDecimal(value);
                }
            }
            public System.String Price
            {
                get
                {
                    System.Decimal? data = entity.Price;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Price = null;
                    else entity.Price = Convert.ToDecimal(value);
                }
            }
            public System.String DiscountAmount
            {
                get
                {
                    System.Decimal? data = entity.DiscountAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscountAmount = null;
                    else entity.DiscountAmount = Convert.ToDecimal(value);
                }
            }
            public System.String CitoAmount
            {
                get
                {
                    System.Decimal? data = entity.CitoAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CitoAmount = null;
                    else entity.CitoAmount = Convert.ToDecimal(value);
                }
            }
            public System.String RoundingAmount
            {
                get
                {
                    System.Decimal? data = entity.RoundingAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoundingAmount = null;
                    else entity.RoundingAmount = Convert.ToDecimal(value);
                }
            }
            public System.String SRDiscountReason
            {
                get
                {
                    System.String data = entity.SRDiscountReason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDiscountReason = null;
                    else entity.SRDiscountReason = Convert.ToString(value);
                }
            }
            public System.String IsAssetUtilization
            {
                get
                {
                    System.Boolean? data = entity.IsAssetUtilization;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAssetUtilization = null;
                    else entity.IsAssetUtilization = Convert.ToBoolean(value);
                }
            }
            public System.String AssetID
            {
                get
                {
                    System.String data = entity.AssetID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssetID = null;
                    else entity.AssetID = Convert.ToString(value);
                }
            }
            public System.String IsBillProceed
            {
                get
                {
                    System.Boolean? data = entity.IsBillProceed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsBillProceed = null;
                    else entity.IsBillProceed = Convert.ToBoolean(value);
                }
            }
            public System.String IsOrderRealization
            {
                get
                {
                    System.Boolean? data = entity.IsOrderRealization;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsOrderRealization = null;
                    else entity.IsOrderRealization = Convert.ToBoolean(value);
                }
            }
            public System.String IsPackage
            {
                get
                {
                    System.Boolean? data = entity.IsPackage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPackage = null;
                    else entity.IsPackage = Convert.ToBoolean(value);
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
            public System.String FilmNo
            {
                get
                {
                    System.String data = entity.FilmNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FilmNo = null;
                    else entity.FilmNo = Convert.ToString(value);
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
            public System.String SRCenterID
            {
                get
                {
                    System.String data = entity.SRCenterID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCenterID = null;
                    else entity.SRCenterID = Convert.ToString(value);
                }
            }
            public System.String AutoProcessCalculation
            {
                get
                {
                    System.Decimal? data = entity.AutoProcessCalculation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AutoProcessCalculation = null;
                    else entity.AutoProcessCalculation = Convert.ToDecimal(value);
                }
            }
            public System.String ParamedicCollectionName
            {
                get
                {
                    System.String data = entity.ParamedicCollectionName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicCollectionName = null;
                    else entity.ParamedicCollectionName = Convert.ToString(value);
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
            public System.String RealizationDateTime
            {
                get
                {
                    System.DateTime? data = entity.RealizationDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RealizationDateTime = null;
                    else entity.RealizationDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String RealizationUserID
            {
                get
                {
                    System.String data = entity.RealizationUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RealizationUserID = null;
                    else entity.RealizationUserID = Convert.ToString(value);
                }
            }
            public System.String UpdateRealizationDateTime
            {
                get
                {
                    System.DateTime? data = entity.UpdateRealizationDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpdateRealizationDateTime = null;
                    else entity.UpdateRealizationDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String UpdateRealizationUserID
            {
                get
                {
                    System.String data = entity.UpdateRealizationUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.UpdateRealizationUserID = null;
                    else entity.UpdateRealizationUserID = Convert.ToString(value);
                }
            }
            public System.String IsCitoInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsCitoInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCitoInPercent = null;
                    else entity.IsCitoInPercent = Convert.ToBoolean(value);
                }
            }
            public System.String BasicCitoAmount
            {
                get
                {
                    System.Decimal? data = entity.BasicCitoAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BasicCitoAmount = null;
                    else entity.BasicCitoAmount = Convert.ToDecimal(value);
                }
            }
            public System.String IsItemRoom
            {
                get
                {
                    System.Boolean? data = entity.IsItemRoom;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsItemRoom = null;
                    else entity.IsItemRoom = Convert.ToBoolean(value);
                }
            }
            public System.String IsExtraItem
            {
                get
                {
                    System.Boolean? data = entity.IsExtraItem;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsExtraItem = null;
                    else entity.IsExtraItem = Convert.ToBoolean(value);
                }
            }
            public System.String IsSelectedExtraItem
            {
                get
                {
                    System.Boolean? data = entity.IsSelectedExtraItem;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSelectedExtraItem = null;
                    else entity.IsSelectedExtraItem = Convert.ToBoolean(value);
                }
            }
            public System.String IsSendToLIS
            {
                get
                {
                    System.Boolean? data = entity.IsSendToLIS;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSendToLIS = null;
                    else entity.IsSendToLIS = Convert.ToBoolean(value);
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
            public System.String IsCorrection
            {
                get
                {
                    System.Boolean? data = entity.IsCorrection;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCorrection = null;
                    else entity.IsCorrection = Convert.ToBoolean(value);
                }
            }
            public System.String ResultValue
            {
                get
                {
                    System.String data = entity.ResultValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResultValue = null;
                    else entity.ResultValue = Convert.ToString(value);
                }
            }
            public System.String IsDuplo
            {
                get
                {
                    System.Boolean? data = entity.IsDuplo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDuplo = null;
                    else entity.IsDuplo = Convert.ToBoolean(value);
                }
            }
            public System.String IsPaymentConfirmed
            {
                get
                {
                    System.Boolean? data = entity.IsPaymentConfirmed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPaymentConfirmed = null;
                    else entity.IsPaymentConfirmed = Convert.ToBoolean(value);
                }
            }
            public System.String PaymentConfirmedDateTime
            {
                get
                {
                    System.DateTime? data = entity.PaymentConfirmedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentConfirmedDateTime = null;
                    else entity.PaymentConfirmedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String PaymentConfirmedBy
            {
                get
                {
                    System.String data = entity.PaymentConfirmedBy;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PaymentConfirmedBy = null;
                    else entity.PaymentConfirmedBy = Convert.ToString(value);
                }
            }
            public System.String LastPaymentConfirmedDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastPaymentConfirmedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastPaymentConfirmedDateTime = null;
                    else entity.LastPaymentConfirmedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String LastPaymentConfirmedByUserID
            {
                get
                {
                    System.String data = entity.LastPaymentConfirmedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastPaymentConfirmedByUserID = null;
                    else entity.LastPaymentConfirmedByUserID = Convert.ToString(value);
                }
            }
            public System.String IsDescriptionResult
            {
                get
                {
                    System.Boolean? data = entity.IsDescriptionResult;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDescriptionResult = null;
                    else entity.IsDescriptionResult = Convert.ToBoolean(value);
                }
            }
            public System.String PriceAdjusted
            {
                get
                {
                    System.Decimal? data = entity.PriceAdjusted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriceAdjusted = null;
                    else entity.PriceAdjusted = Convert.ToDecimal(value);
                }
            }
            public System.String SRCitoPercentage
            {
                get
                {
                    System.String data = entity.SRCitoPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCitoPercentage = null;
                    else entity.SRCitoPercentage = Convert.ToString(value);
                }
            }
            public System.String ItemConditionRuleID
            {
                get
                {
                    System.String data = entity.ItemConditionRuleID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemConditionRuleID = null;
                    else entity.ItemConditionRuleID = Convert.ToString(value);
                }
            }
            public System.String CommunicationID
            {
                get
                {
                    System.String data = entity.CommunicationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CommunicationID = null;
                    else entity.CommunicationID = Convert.ToString(value);
                }
            }
            public System.String IsCasemixApproved
            {
                get
                {
                    System.Boolean? data = entity.IsCasemixApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCasemixApproved = null;
                    else entity.IsCasemixApproved = Convert.ToBoolean(value);
                }
            }
            public System.String CasemixApprovedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CasemixApprovedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CasemixApprovedDateTime = null;
                    else entity.CasemixApprovedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CasemixApprovedByUserID
            {
                get
                {
                    System.String data = entity.CasemixApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CasemixApprovedByUserID = null;
                    else entity.CasemixApprovedByUserID = Convert.ToString(value);
                }
            }
            public System.String TariffDate
            {
                get
                {
                    System.DateTime? data = entity.TariffDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TariffDate = null;
                    else entity.TariffDate = Convert.ToDateTime(value);
                }
            }
            public System.String SpecimenTakenDateTime
            {
                get
                {
                    System.DateTime? data = entity.SpecimenTakenDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenTakenDateTime = null;
                    else entity.SpecimenTakenDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SpecimenTakenByUserID
            {
                get
                {
                    System.String data = entity.SpecimenTakenByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenTakenByUserID = null;
                    else entity.SpecimenTakenByUserID = Convert.ToString(value);
                }
            }
            public System.String SpecimenSubmittedDateTime
            {
                get
                {
                    System.DateTime? data = entity.SpecimenSubmittedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenSubmittedDateTime = null;
                    else entity.SpecimenSubmittedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SpecimenSubmittedByUserID
            {
                get
                {
                    System.String data = entity.SpecimenSubmittedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenSubmittedByUserID = null;
                    else entity.SpecimenSubmittedByUserID = Convert.ToString(value);
                }
            }
            public System.String SpecimenReceivedDateTime
            {
                get
                {
                    System.DateTime? data = entity.SpecimenReceivedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenReceivedDateTime = null;
                    else entity.SpecimenReceivedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SpecimenReceivedByUserID
            {
                get
                {
                    System.String data = entity.SpecimenReceivedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenReceivedByUserID = null;
                    else entity.SpecimenReceivedByUserID = Convert.ToString(value);
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
            public System.String CasemixNotes
            {
                get
                {
                    System.String data = entity.CasemixNotes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CasemixNotes = null;
                    else entity.CasemixNotes = Convert.ToString(value);
                }
            }
            public System.String SpecimenCollectDateTime
            {
                get
                {
                    System.DateTime? data = entity.SpecimenCollectDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenCollectDateTime = null;
                    else entity.SpecimenCollectDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SpecimenReceiveDateTime
            {
                get
                {
                    System.DateTime? data = entity.SpecimenReceiveDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenReceiveDateTime = null;
                    else entity.SpecimenReceiveDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String SpecimenCollectByUserID
            {
                get
                {
                    System.String data = entity.SpecimenCollectByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenCollectByUserID = null;
                    else entity.SpecimenCollectByUserID = Convert.ToString(value);
                }
            }
            public System.String SpecimenReceiveByUserID
            {
                get
                {
                    System.String data = entity.SpecimenReceiveByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SpecimenReceiveByUserID = null;
                    else entity.SpecimenReceiveByUserID = Convert.ToString(value);
                }
            }
            public System.String SRCollectMethod
            {
                get
                {
                    System.String data = entity.SRCollectMethod;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCollectMethod = null;
                    else entity.SRCollectMethod = Convert.ToString(value);
                }
            }
            private esTransChargesItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransChargesItemQuery query)
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
                throw new Exception("esTransChargesItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransChargesItem : esTransChargesItem
    {
    }

    [Serializable]
    abstract public class esTransChargesItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransChargesItemMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ReferenceNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem ReferenceSequenceNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ChargeClassID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ChargeClassID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem SecondParamedicID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SecondParamedicID, esSystemType.String);
            }
        }

        public esQueryItem IsAdminCalculation
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVariable
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsVariable, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCito
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsCito, esSystemType.Boolean);
            }
        }

        public esQueryItem ChargeQuantity
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ChargeQuantity, esSystemType.Decimal);
            }
        }

        public esQueryItem StockQuantity
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.StockQuantity, esSystemType.Decimal);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem CostPrice
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscountAmount
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem CitoAmount
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CitoAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem RoundingAmount
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.RoundingAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem SRDiscountReason
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
            }
        }

        public esQueryItem IsAssetUtilization
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsAssetUtilization, esSystemType.Boolean);
            }
        }

        public esQueryItem AssetID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.AssetID, esSystemType.String);
            }
        }

        public esQueryItem IsBillProceed
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
            }
        }

        public esQueryItem IsOrderRealization
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsOrderRealization, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPackage
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
            }
        }

        public esQueryItem IsApprove
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem FilmNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.FilmNo, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ParentNo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ParentNo, esSystemType.String);
            }
        }

        public esQueryItem SRCenterID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SRCenterID, esSystemType.String);
            }
        }

        public esQueryItem AutoProcessCalculation
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
            }
        }

        public esQueryItem ParamedicCollectionName
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ParamedicCollectionName, esSystemType.String);
            }
        }

        public esQueryItem ToServiceUnitID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem RealizationDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.RealizationDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem RealizationUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.RealizationUserID, esSystemType.String);
            }
        }

        public esQueryItem UpdateRealizationDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.UpdateRealizationDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem UpdateRealizationUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.UpdateRealizationUserID, esSystemType.String);
            }
        }

        public esQueryItem IsCitoInPercent
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsCitoInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem BasicCitoAmount
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.BasicCitoAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem IsItemRoom
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsItemRoom, esSystemType.Boolean);
            }
        }

        public esQueryItem IsExtraItem
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsExtraItem, esSystemType.Boolean);
            }
        }

        public esQueryItem IsSelectedExtraItem
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsSelectedExtraItem, esSystemType.Boolean);
            }
        }

        public esQueryItem IsSendToLIS
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsSendToLIS, esSystemType.Boolean);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsCorrection
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsCorrection, esSystemType.Boolean);
            }
        }

        public esQueryItem ResultValue
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ResultValue, esSystemType.String);
            }
        }

        public esQueryItem IsDuplo
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsDuplo, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPaymentConfirmed
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsPaymentConfirmed, esSystemType.Boolean);
            }
        }

        public esQueryItem PaymentConfirmedDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.PaymentConfirmedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem PaymentConfirmedBy
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.PaymentConfirmedBy, esSystemType.String);
            }
        }

        public esQueryItem LastPaymentConfirmedDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastPaymentConfirmedByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsDescriptionResult
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsDescriptionResult, esSystemType.Boolean);
            }
        }

        public esQueryItem PriceAdjusted
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.PriceAdjusted, esSystemType.Decimal);
            }
        }

        public esQueryItem SRCitoPercentage
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SRCitoPercentage, esSystemType.String);
            }
        }

        public esQueryItem ItemConditionRuleID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.ItemConditionRuleID, esSystemType.String);
            }
        }

        public esQueryItem CommunicationID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CommunicationID, esSystemType.String);
            }
        }

        public esQueryItem IsCasemixApproved
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.IsCasemixApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem CasemixApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CasemixApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CasemixApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CasemixApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem TariffDate
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.TariffDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SpecimenTakenDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenTakenDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SpecimenTakenByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenTakenByUserID, esSystemType.String);
            }
        }

        public esQueryItem SpecimenSubmittedDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenSubmittedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SpecimenSubmittedByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenSubmittedByUserID, esSystemType.String);
            }
        }

        public esQueryItem SpecimenReceivedDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenReceivedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SpecimenReceivedByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenReceivedByUserID, esSystemType.String);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem CasemixNotes
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.CasemixNotes, esSystemType.String);
            }
        }

        public esQueryItem SpecimenCollectDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenCollectDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SpecimenReceiveDateTime
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenReceiveDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SpecimenCollectByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenCollectByUserID, esSystemType.String);
            }
        }

        public esQueryItem SpecimenReceiveByUserID
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SpecimenReceiveByUserID, esSystemType.String);
            }
        }        

        public esQueryItem SRCollectMethod
        {
            get
            {
                return new esQueryItem(this, TransChargesItemMetadata.ColumnNames.SRCollectMethod, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransChargesItemCollection")]
    public partial class TransChargesItemCollection : esTransChargesItemCollection, IEnumerable<TransChargesItem>
    {
        public TransChargesItemCollection()
        {

        }

        public static implicit operator List<TransChargesItem>(TransChargesItemCollection coll)
        {
            List<TransChargesItem> list = new List<TransChargesItem>();

            foreach (TransChargesItem emp in coll)
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
                return TransChargesItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransChargesItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransChargesItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransChargesItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesItemQuery();
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
        public bool Load(TransChargesItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransChargesItem AddNew()
        {
            TransChargesItem entity = base.AddNewEntity() as TransChargesItem;

            return entity;
        }
        public TransChargesItem FindByPrimaryKey(String transactionNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(transactionNo, sequenceNo) as TransChargesItem;
        }

        #region IEnumerable< TransChargesItem> Members

        IEnumerator<TransChargesItem> IEnumerable<TransChargesItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransChargesItem;
            }
        }

        #endregion

        private TransChargesItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransChargesItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransChargesItem ({TransactionNo, SequenceNo})")]
    [Serializable]
    public partial class TransChargesItem : esTransChargesItem
    {
        public TransChargesItem()
        {
        }

        public TransChargesItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransChargesItemMetadata.Meta();
            }
        }

        override protected esTransChargesItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransChargesItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransChargesItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransChargesItemQuery();
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
        public bool Load(TransChargesItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransChargesItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransChargesItemQuery : esTransChargesItemQuery
    {
        public TransChargesItemQuery()
        {

        }

        public TransChargesItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransChargesItemQuery";
        }
    }

    [Serializable]
    public partial class TransChargesItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransChargesItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 12;
            c.HasDefault = true;
            c.Default = @"('000')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ReferenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ReferenceNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ReferenceSequenceNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ReferenceSequenceNo;
            c.CharacterMaxLength = 12;
            c.HasDefault = true;
            c.Default = @"('000')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ItemID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ChargeClassID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ChargeClassID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SecondParamedicID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SecondParamedicID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsAdminCalculation, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsAdminCalculation;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsVariable, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsVariable;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsCito, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsCito;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ChargeQuantity, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ChargeQuantity;
            c.NumericPrecision = 12;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((1))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.StockQuantity, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.StockQuantity;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SRItemUnit, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SRItemUnit;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CostPrice, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CostPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.Price, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.DiscountAmount, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.DiscountAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CitoAmount, 17, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CitoAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.RoundingAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.RoundingAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SRDiscountReason, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SRDiscountReason;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsAssetUtilization, 20, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsAssetUtilization;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.AssetID, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.AssetID;
            c.CharacterMaxLength = 30;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsBillProceed, 22, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsBillProceed;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsOrderRealization, 23, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsOrderRealization;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsPackage, 24, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsPackage;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsApprove, 25, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsApprove;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsVoid, 26, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsVoid;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.Notes, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 4000;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.FilmNo, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.FilmNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.LastUpdateDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.LastUpdateByUserID, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ParentNo, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ParentNo;
            c.CharacterMaxLength = 12;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SRCenterID, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SRCenterID;
            c.CharacterMaxLength = 30;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.AutoProcessCalculation, 33, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.AutoProcessCalculation;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ParamedicCollectionName, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ParamedicCollectionName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ToServiceUnitID, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ToServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.RealizationDateTime, 36, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.RealizationDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.RealizationUserID, 37, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.RealizationUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.UpdateRealizationDateTime, 38, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.UpdateRealizationDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.UpdateRealizationUserID, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.UpdateRealizationUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsCitoInPercent, 40, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsCitoInPercent;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.BasicCitoAmount, 41, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.BasicCitoAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsItemRoom, 42, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsItemRoom;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsExtraItem, 43, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsExtraItem;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsSelectedExtraItem, 44, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsSelectedExtraItem;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsSendToLIS, 45, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsSendToLIS;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CreatedDateTime, 46, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CreatedByUserID, 47, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsCorrection, 48, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsCorrection;
            c.HasDefault = true;
            c.Default = @"((0))";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ResultValue, 49, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ResultValue;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsDuplo, 50, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsDuplo;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsPaymentConfirmed, 51, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsPaymentConfirmed;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.PaymentConfirmedDateTime, 52, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.PaymentConfirmedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.PaymentConfirmedBy, 53, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.PaymentConfirmedBy;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedDateTime, 54, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.LastPaymentConfirmedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.LastPaymentConfirmedByUserID, 55, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.LastPaymentConfirmedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsDescriptionResult, 56, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsDescriptionResult;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.PriceAdjusted, 57, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.PriceAdjusted;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SRCitoPercentage, 58, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SRCitoPercentage;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.ItemConditionRuleID, 59, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.ItemConditionRuleID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CommunicationID, 60, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CommunicationID;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.IsCasemixApproved, 61, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.IsCasemixApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CasemixApprovedDateTime, 62, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CasemixApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CasemixApprovedByUserID, 63, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CasemixApprovedByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.TariffDate, 64, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.TariffDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenTakenDateTime, 65, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenTakenDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenTakenByUserID, 66, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenTakenByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenSubmittedDateTime, 67, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenSubmittedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenSubmittedByUserID, 68, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenSubmittedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenReceivedDateTime, 69, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenReceivedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenReceivedByUserID, 70, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenReceivedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.VoidDateTime, 71, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.VoidByUserID, 72, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.CasemixNotes, 73, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.CasemixNotes;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenCollectDateTime, 74, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenCollectDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenReceiveDateTime, 75, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenReceiveDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenCollectByUserID, 76, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenCollectByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SpecimenReceiveByUserID, 77, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SpecimenReceiveByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);            

            c = new esColumnMetadata(TransChargesItemMetadata.ColumnNames.SRCollectMethod, 78, typeof(System.String), esSystemType.String);
            c.PropertyName = TransChargesItemMetadata.PropertyNames.SRCollectMethod;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransChargesItemMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ReferenceNo = "ReferenceNo";
            public const string ReferenceSequenceNo = "ReferenceSequenceNo";
            public const string ItemID = "ItemID";
            public const string ChargeClassID = "ChargeClassID";
            public const string ParamedicID = "ParamedicID";
            public const string SecondParamedicID = "SecondParamedicID";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsVariable = "IsVariable";
            public const string IsCito = "IsCito";
            public const string ChargeQuantity = "ChargeQuantity";
            public const string StockQuantity = "StockQuantity";
            public const string SRItemUnit = "SRItemUnit";
            public const string CostPrice = "CostPrice";
            public const string Price = "Price";
            public const string DiscountAmount = "DiscountAmount";
            public const string CitoAmount = "CitoAmount";
            public const string RoundingAmount = "RoundingAmount";
            public const string SRDiscountReason = "SRDiscountReason";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string AssetID = "AssetID";
            public const string IsBillProceed = "IsBillProceed";
            public const string IsOrderRealization = "IsOrderRealization";
            public const string IsPackage = "IsPackage";
            public const string IsApprove = "IsApprove";
            public const string IsVoid = "IsVoid";
            public const string Notes = "Notes";
            public const string FilmNo = "FilmNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ParentNo = "ParentNo";
            public const string SRCenterID = "SRCenterID";
            public const string AutoProcessCalculation = "AutoProcessCalculation";
            public const string ParamedicCollectionName = "ParamedicCollectionName";
            public const string ToServiceUnitID = "ToServiceUnitID";
            public const string RealizationDateTime = "RealizationDateTime";
            public const string RealizationUserID = "RealizationUserID";
            public const string UpdateRealizationDateTime = "UpdateRealizationDateTime";
            public const string UpdateRealizationUserID = "UpdateRealizationUserID";
            public const string IsCitoInPercent = "IsCitoInPercent";
            public const string BasicCitoAmount = "BasicCitoAmount";
            public const string IsItemRoom = "IsItemRoom";
            public const string IsExtraItem = "IsExtraItem";
            public const string IsSelectedExtraItem = "IsSelectedExtraItem";
            public const string IsSendToLIS = "IsSendToLIS";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string IsCorrection = "IsCorrection";
            public const string ResultValue = "ResultValue";
            public const string IsDuplo = "IsDuplo";
            public const string IsPaymentConfirmed = "IsPaymentConfirmed";
            public const string PaymentConfirmedDateTime = "PaymentConfirmedDateTime";
            public const string PaymentConfirmedBy = "PaymentConfirmedBy";
            public const string LastPaymentConfirmedDateTime = "LastPaymentConfirmedDateTime";
            public const string LastPaymentConfirmedByUserID = "LastPaymentConfirmedByUserID";
            public const string IsDescriptionResult = "IsDescriptionResult";
            public const string PriceAdjusted = "PriceAdjusted";
            public const string SRCitoPercentage = "SRCitoPercentage";
            public const string ItemConditionRuleID = "ItemConditionRuleID";
            public const string CommunicationID = "CommunicationID";
            public const string IsCasemixApproved = "IsCasemixApproved";
            public const string CasemixApprovedDateTime = "CasemixApprovedDateTime";
            public const string CasemixApprovedByUserID = "CasemixApprovedByUserID";
            public const string TariffDate = "TariffDate";
            public const string SpecimenTakenDateTime = "SpecimenTakenDateTime";
            public const string SpecimenTakenByUserID = "SpecimenTakenByUserID";
            public const string SpecimenSubmittedDateTime = "SpecimenSubmittedDateTime";
            public const string SpecimenSubmittedByUserID = "SpecimenSubmittedByUserID";
            public const string SpecimenReceivedDateTime = "SpecimenReceivedDateTime";
            public const string SpecimenReceivedByUserID = "SpecimenReceivedByUserID";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string CasemixNotes = "CasemixNotes";
            public const string SpecimenCollectDateTime = "SpecimenCollectDateTime";
            public const string SpecimenReceiveDateTime = "SpecimenReceiveDateTime";
            public const string SpecimenCollectByUserID = "SpecimenCollectByUserID";
            public const string SpecimenReceiveByUserID = "SpecimenReceiveByUserID";
            public const string SRCollectMethod = "SRCollectMethod";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ReferenceNo = "ReferenceNo";
            public const string ReferenceSequenceNo = "ReferenceSequenceNo";
            public const string ItemID = "ItemID";
            public const string ChargeClassID = "ChargeClassID";
            public const string ParamedicID = "ParamedicID";
            public const string SecondParamedicID = "SecondParamedicID";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsVariable = "IsVariable";
            public const string IsCito = "IsCito";
            public const string ChargeQuantity = "ChargeQuantity";
            public const string StockQuantity = "StockQuantity";
            public const string SRItemUnit = "SRItemUnit";
            public const string CostPrice = "CostPrice";
            public const string Price = "Price";
            public const string DiscountAmount = "DiscountAmount";
            public const string CitoAmount = "CitoAmount";
            public const string RoundingAmount = "RoundingAmount";
            public const string SRDiscountReason = "SRDiscountReason";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string AssetID = "AssetID";
            public const string IsBillProceed = "IsBillProceed";
            public const string IsOrderRealization = "IsOrderRealization";
            public const string IsPackage = "IsPackage";
            public const string IsApprove = "IsApprove";
            public const string IsVoid = "IsVoid";
            public const string Notes = "Notes";
            public const string FilmNo = "FilmNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ParentNo = "ParentNo";
            public const string SRCenterID = "SRCenterID";
            public const string AutoProcessCalculation = "AutoProcessCalculation";
            public const string ParamedicCollectionName = "ParamedicCollectionName";
            public const string ToServiceUnitID = "ToServiceUnitID";
            public const string RealizationDateTime = "RealizationDateTime";
            public const string RealizationUserID = "RealizationUserID";
            public const string UpdateRealizationDateTime = "UpdateRealizationDateTime";
            public const string UpdateRealizationUserID = "UpdateRealizationUserID";
            public const string IsCitoInPercent = "IsCitoInPercent";
            public const string BasicCitoAmount = "BasicCitoAmount";
            public const string IsItemRoom = "IsItemRoom";
            public const string IsExtraItem = "IsExtraItem";
            public const string IsSelectedExtraItem = "IsSelectedExtraItem";
            public const string IsSendToLIS = "IsSendToLIS";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string IsCorrection = "IsCorrection";
            public const string ResultValue = "ResultValue";
            public const string IsDuplo = "IsDuplo";
            public const string IsPaymentConfirmed = "IsPaymentConfirmed";
            public const string PaymentConfirmedDateTime = "PaymentConfirmedDateTime";
            public const string PaymentConfirmedBy = "PaymentConfirmedBy";
            public const string LastPaymentConfirmedDateTime = "LastPaymentConfirmedDateTime";
            public const string LastPaymentConfirmedByUserID = "LastPaymentConfirmedByUserID";
            public const string IsDescriptionResult = "IsDescriptionResult";
            public const string PriceAdjusted = "PriceAdjusted";
            public const string SRCitoPercentage = "SRCitoPercentage";
            public const string ItemConditionRuleID = "ItemConditionRuleID";
            public const string CommunicationID = "CommunicationID";
            public const string IsCasemixApproved = "IsCasemixApproved";
            public const string CasemixApprovedDateTime = "CasemixApprovedDateTime";
            public const string CasemixApprovedByUserID = "CasemixApprovedByUserID";
            public const string TariffDate = "TariffDate";
            public const string SpecimenTakenDateTime = "SpecimenTakenDateTime";
            public const string SpecimenTakenByUserID = "SpecimenTakenByUserID";
            public const string SpecimenSubmittedDateTime = "SpecimenSubmittedDateTime";
            public const string SpecimenSubmittedByUserID = "SpecimenSubmittedByUserID";
            public const string SpecimenReceivedDateTime = "SpecimenReceivedDateTime";
            public const string SpecimenReceivedByUserID = "SpecimenReceivedByUserID";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string CasemixNotes = "CasemixNotes";
            public const string SpecimenCollectDateTime = "SpecimenCollectDateTime";
            public const string SpecimenReceiveDateTime = "SpecimenReceiveDateTime";
            public const string SpecimenCollectByUserID = "SpecimenCollectByUserID";
            public const string SpecimenReceiveByUserID = "SpecimenReceiveByUserID";
            public const string SRCollectMethod = "SRCollectMethod";
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
            lock (typeof(TransChargesItemMetadata))
            {
                if (TransChargesItemMetadata.mapDelegates == null)
                {
                    TransChargesItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransChargesItemMetadata.meta == null)
                {
                    TransChargesItemMetadata.meta = new TransChargesItemMetadata();
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

                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ChargeClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SecondParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVariable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCito", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ChargeQuantity", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("StockQuantity", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CitoAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RoundingAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SRDiscountReason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAssetUtilization", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsBillProceed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsOrderRealization", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FilmNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCenterID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AutoProcessCalculation", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ParamedicCollectionName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RealizationDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("RealizationUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("UpdateRealizationDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("UpdateRealizationUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsCitoInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("BasicCitoAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsItemRoom", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsExtraItem", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsSelectedExtraItem", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsSendToLIS", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsCorrection", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ResultValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDuplo", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPaymentConfirmed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PaymentConfirmedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("PaymentConfirmedBy", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastPaymentConfirmedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastPaymentConfirmedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDescriptionResult", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("PriceAdjusted", new esTypeMap("decimal", "System.Decimal"));
                meta.AddTypeMap("SRCitoPercentage", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemConditionRuleID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CommunicationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsCasemixApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CasemixApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CasemixApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TariffDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SpecimenTakenDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SpecimenTakenByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SpecimenSubmittedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SpecimenSubmittedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SpecimenReceivedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SpecimenReceivedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CasemixNotes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SpecimenCollectDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SpecimenReceiveDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SpecimenCollectByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SpecimenReceiveByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCollectMethod", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransChargesItem";
                meta.Destination = "TransChargesItem";
                meta.spInsert = "proc_TransChargesItemInsert";
                meta.spUpdate = "proc_TransChargesItemUpdate";
                meta.spDelete = "proc_TransChargesItemDelete";
                meta.spLoadAll = "proc_TransChargesItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransChargesItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransChargesItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
