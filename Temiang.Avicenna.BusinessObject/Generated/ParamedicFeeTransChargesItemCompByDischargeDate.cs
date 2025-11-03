/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/1/2023 11:49:09 AM
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
	abstract public class esParamedicFeeTransChargesItemCompByDischargeDateCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeTransChargesItemCompByDischargeDateCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeTransChargesItemCompByDischargeDateCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompByDischargeDateQuery query)
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
			this.InitQuery(query as esParamedicFeeTransChargesItemCompByDischargeDateQuery);
		}
		#endregion
			
		virtual public ParamedicFeeTransChargesItemCompByDischargeDate DetachEntity(ParamedicFeeTransChargesItemCompByDischargeDate entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeTransChargesItemCompByDischargeDate;
		}
		
		virtual public ParamedicFeeTransChargesItemCompByDischargeDate AttachEntity(ParamedicFeeTransChargesItemCompByDischargeDate entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeTransChargesItemCompByDischargeDate;
		}
		
		virtual public void Combine(ParamedicFeeTransChargesItemCompByDischargeDateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeTransChargesItemCompByDischargeDate this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeTransChargesItemCompByDischargeDate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeTransChargesItemCompByDischargeDate);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeTransChargesItemCompByDischargeDate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeTransChargesItemCompByDischargeDateQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeTransChargesItemCompByDischargeDate()
		{
		}
	
		public esParamedicFeeTransChargesItemCompByDischargeDate(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			esParamedicFeeTransChargesItemCompByDischargeDateQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.SequenceNo==sequenceNo, query.TariffComponentID==tariffComponentID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("SequenceNo",sequenceNo);
			parms.Add("TariffComponentID",tariffComponentID);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "DischargeDate": this.str.DischargeDate = (string)value; break;
						case "IsOrderRealization": this.str.IsOrderRealization = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "Discount": this.str.Discount = (string)value; break;
						case "FeeAmount": this.str.FeeAmount = (string)value; break;
						case "IsRefferal": this.str.IsRefferal = (string)value; break;
						case "IsCalculatedInPercent": this.str.IsCalculatedInPercent = (string)value; break;
						case "CalculatedAmount": this.str.CalculatedAmount = (string)value; break;
						case "IsFree": this.str.IsFree = (string)value; break;
						case "LastCalculatedDateTime": this.str.LastCalculatedDateTime = (string)value; break;
						case "LastCalculatedByUserID": this.str.LastCalculatedByUserID = (string)value; break;
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsCalcDeductionInPercent": this.str.IsCalcDeductionInPercent = (string)value; break;
						case "CalcDeductionAmount": this.str.CalcDeductionAmount = (string)value; break;
						case "DeductionAmount": this.str.DeductionAmount = (string)value; break;
						case "JournalId": this.str.JournalId = (string)value; break;
						case "OldParamedicID": this.str.OldParamedicID = (string)value; break;
						case "IsModified": this.str.IsModified = (string)value; break;
						case "PriceItem": this.str.PriceItem = (string)value; break;
						case "DiscountItem": this.str.DiscountItem = (string)value; break;
						case "TransactionNoRef": this.str.TransactionNoRef = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "RegistrationNoMergeTo": this.str.RegistrationNoMergeTo = (string)value; break;
						case "DischargeDateMergeTo": this.str.DischargeDateMergeTo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "PaymentMethodName": this.str.PaymentMethodName = (string)value; break;
						case "SRPhysicianFeeCategory": this.str.SRPhysicianFeeCategory = (string)value; break;
						case "SRParamedicFeeCaseType": this.str.SRParamedicFeeCaseType = (string)value; break;
						case "SRParamedicFeeTeamStatus": this.str.SRParamedicFeeTeamStatus = (string)value; break;
						case "SRParamedicFeeIsTeam": this.str.SRParamedicFeeIsTeam = (string)value; break;
						case "SumDeductionAmount": this.str.SumDeductionAmount = (string)value; break;
						case "ParamedicFeeByServiceSettingID": this.str.ParamedicFeeByServiceSettingID = (string)value; break;
						case "SmfID": this.str.SmfID = (string)value; break;
						case "IsGuarantorVerified": this.str.IsGuarantorVerified = (string)value; break;
						case "PerformanceGross": this.str.PerformanceGross = (string)value; break;
						case "AdditionalSum": this.str.AdditionalSum = (string)value; break;
						case "DeductionConvertion": this.str.DeductionConvertion = (string)value; break;
						case "DeductionAnesthetic": this.str.DeductionAnesthetic = (string)value; break;
						case "DeductionResult": this.str.DeductionResult = (string)value; break;
						case "Performance": this.str.Performance = (string)value; break;
						case "DiscountExtra": this.str.DiscountExtra = (string)value; break;
						case "PaymentNoCash": this.str.PaymentNoCash = (string)value; break;
						case "PaymentNoAR": this.str.PaymentNoAR = (string)value; break;
						case "InvoicePaymentNo": this.str.InvoicePaymentNo = (string)value; break;
						case "IsWriteOff": this.str.IsWriteOff = (string)value; break;
						case "InvoiceWriteOffNo": this.str.InvoiceWriteOffNo = (string)value; break;
						case "PercentagePayment": this.str.PercentagePayment = (string)value; break;
						case "LastPaymentDate": this.str.LastPaymentDate = (string)value; break;
						case "PaymentNoGuarAR": this.str.PaymentNoGuarAR = (string)value; break;
						case "InvoicePaymentNoGuar": this.str.InvoicePaymentNoGuar = (string)value; break;
						case "PercentagePaymentAR": this.str.PercentagePaymentAR = (string)value; break;
						case "PercentagePaymentGuarAR": this.str.PercentagePaymentGuarAR = (string)value; break;
						case "ChangeNote": this.str.ChangeNote = (string)value; break;
						case "SumDeductionAmountAfterTax": this.str.SumDeductionAmountAfterTax = (string)value; break;
						case "FeeAmountToBePaid": this.str.FeeAmountToBePaid = (string)value; break;
						case "FeeAmountUpdateByUserID": this.str.FeeAmountUpdateByUserID = (string)value; break;
						case "FeeAmountUpdateDateTime": this.str.FeeAmountUpdateDateTime = (string)value; break;
						case "FeeAmountToBePaidChangeNote": this.str.FeeAmountToBePaidChangeNote = (string)value; break;
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
						case "IsCustom": this.str.IsCustom = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "SRProcedure1": this.str.SRProcedure1 = (string)value; break;
						case "ExecutedFormula": this.str.ExecutedFormula = (string)value; break;
						case "ExecutedMessage": this.str.ExecutedMessage = (string)value; break;
						case "IsForTakeOneHighest": this.str.IsForTakeOneHighest = (string)value; break;
						case "PctgPropCash": this.str.PctgPropCash = (string)value; break;
						case "PctgPropAR": this.str.PctgPropAR = (string)value; break;
						case "PctgPropARGuar": this.str.PctgPropARGuar = (string)value; break;
						case "RemunByIdiID": this.str.RemunByIdiID = (string)value; break;
						case "TotalBill": this.str.TotalBill = (string)value; break;
						case "IsBrutoFromFeeAmount": this.str.IsBrutoFromFeeAmount = (string)value; break;
						case "FeeAmountBruto": this.str.FeeAmountBruto = (string)value; break;
						case "IsFeeTeam": this.str.IsFeeTeam = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DischargeDate":
						
							if (value == null || value is System.DateTime)
								this.DischargeDate = (System.DateTime?)value;
							break;
						case "IsOrderRealization":
						
							if (value == null || value is System.Boolean)
								this.IsOrderRealization = (System.Boolean?)value;
							break;
						case "Qty":
						
							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "Discount":
						
							if (value == null || value is System.Decimal)
								this.Discount = (System.Decimal?)value;
							break;
						case "FeeAmount":
						
							if (value == null || value is System.Decimal)
								this.FeeAmount = (System.Decimal?)value;
							break;
						case "IsRefferal":
						
							if (value == null || value is System.Boolean)
								this.IsRefferal = (System.Boolean?)value;
							break;
						case "IsCalculatedInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsCalculatedInPercent = (System.Boolean?)value;
							break;
						case "CalculatedAmount":
						
							if (value == null || value is System.Decimal)
								this.CalculatedAmount = (System.Decimal?)value;
							break;
						case "IsFree":
						
							if (value == null || value is System.Boolean)
								this.IsFree = (System.Boolean?)value;
							break;
						case "LastCalculatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastCalculatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsCalcDeductionInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsCalcDeductionInPercent = (System.Boolean?)value;
							break;
						case "CalcDeductionAmount":
						
							if (value == null || value is System.Decimal)
								this.CalcDeductionAmount = (System.Decimal?)value;
							break;
						case "DeductionAmount":
						
							if (value == null || value is System.Decimal)
								this.DeductionAmount = (System.Decimal?)value;
							break;
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
							break;
						case "IsModified":
						
							if (value == null || value is System.Boolean)
								this.IsModified = (System.Boolean?)value;
							break;
						case "PriceItem":
						
							if (value == null || value is System.Decimal)
								this.PriceItem = (System.Decimal?)value;
							break;
						case "DiscountItem":
						
							if (value == null || value is System.Decimal)
								this.DiscountItem = (System.Decimal?)value;
							break;
						case "DischargeDateMergeTo":
						
							if (value == null || value is System.DateTime)
								this.DischargeDateMergeTo = (System.DateTime?)value;
							break;
						case "SumDeductionAmount":
						
							if (value == null || value is System.Decimal)
								this.SumDeductionAmount = (System.Decimal?)value;
							break;
						case "ParamedicFeeByServiceSettingID":
						
							if (value == null || value is System.Int32)
								this.ParamedicFeeByServiceSettingID = (System.Int32?)value;
							break;
						case "IsGuarantorVerified":
						
							if (value == null || value is System.Boolean)
								this.IsGuarantorVerified = (System.Boolean?)value;
							break;
						case "PerformanceGross":
						
							if (value == null || value is System.Decimal)
								this.PerformanceGross = (System.Decimal?)value;
							break;
						case "AdditionalSum":
						
							if (value == null || value is System.Decimal)
								this.AdditionalSum = (System.Decimal?)value;
							break;
						case "DeductionConvertion":
						
							if (value == null || value is System.Decimal)
								this.DeductionConvertion = (System.Decimal?)value;
							break;
						case "DeductionAnesthetic":
						
							if (value == null || value is System.Decimal)
								this.DeductionAnesthetic = (System.Decimal?)value;
							break;
						case "DeductionResult":
						
							if (value == null || value is System.Decimal)
								this.DeductionResult = (System.Decimal?)value;
							break;
						case "Performance":
						
							if (value == null || value is System.Decimal)
								this.Performance = (System.Decimal?)value;
							break;
						case "DiscountExtra":
						
							if (value == null || value is System.Decimal)
								this.DiscountExtra = (System.Decimal?)value;
							break;
						case "IsWriteOff":
						
							if (value == null || value is System.Boolean)
								this.IsWriteOff = (System.Boolean?)value;
							break;
						case "PercentagePayment":
						
							if (value == null || value is System.Decimal)
								this.PercentagePayment = (System.Decimal?)value;
							break;
						case "LastPaymentDate":
						
							if (value == null || value is System.DateTime)
								this.LastPaymentDate = (System.DateTime?)value;
							break;
						case "PercentagePaymentAR":
						
							if (value == null || value is System.Decimal)
								this.PercentagePaymentAR = (System.Decimal?)value;
							break;
						case "PercentagePaymentGuarAR":
						
							if (value == null || value is System.Decimal)
								this.PercentagePaymentGuarAR = (System.Decimal?)value;
							break;
						case "SumDeductionAmountAfterTax":
						
							if (value == null || value is System.Decimal)
								this.SumDeductionAmountAfterTax = (System.Decimal?)value;
							break;
						case "FeeAmountToBePaid":
						
							if (value == null || value is System.Decimal)
								this.FeeAmountToBePaid = (System.Decimal?)value;
							break;
						case "FeeAmountUpdateByUserID":
						
							if (value == null || value is System.Decimal)
								this.FeeAmountUpdateByUserID = (System.Decimal?)value;
							break;
						case "FeeAmountUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.FeeAmountUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsCustom":
						
							if (value == null || value is System.Boolean)
								this.IsCustom = (System.Boolean?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "IsForTakeOneHighest":
						
							if (value == null || value is System.Boolean)
								this.IsForTakeOneHighest = (System.Boolean?)value;
							break;
						case "PctgPropCash":
						
							if (value == null || value is System.Decimal)
								this.PctgPropCash = (System.Decimal?)value;
							break;
						case "PctgPropAR":
						
							if (value == null || value is System.Decimal)
								this.PctgPropAR = (System.Decimal?)value;
							break;
						case "PctgPropARGuar":
						
							if (value == null || value is System.Decimal)
								this.PctgPropARGuar = (System.Decimal?)value;
							break;
						case "RemunByIdiID":
						
							if (value == null || value is System.Int32)
								this.RemunByIdiID = (System.Int32?)value;
							break;
						case "TotalBill":
						
							if (value == null || value is System.Decimal)
								this.TotalBill = (System.Decimal?)value;
							break;
						case "IsBrutoFromFeeAmount":
						
							if (value == null || value is System.Boolean)
								this.IsBrutoFromFeeAmount = (System.Boolean?)value;
							break;
						case "FeeAmountBruto":
						
							if (value == null || value is System.Decimal)
								this.FeeAmountBruto = (System.Decimal?)value;
							break;
						case "IsFeeTeam":
						
							if (value == null || value is System.Boolean)
								this.IsFeeTeam = (System.Boolean?)value;
							break;
					
						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DischargeDate
		/// </summary>
		virtual public System.DateTime? DischargeDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsOrderRealization
		/// </summary>
		virtual public System.Boolean? IsOrderRealization
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsOrderRealization);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsOrderRealization, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Discount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.FeeAmount
		/// </summary>
		virtual public System.Decimal? FeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsRefferal
		/// </summary>
		virtual public System.Boolean? IsRefferal
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsRefferal);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsRefferal, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsCalculatedInPercent
		/// </summary>
		virtual public System.Boolean? IsCalculatedInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalculatedInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalculatedInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.CalculatedAmount
		/// </summary>
		virtual public System.Decimal? CalculatedAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalculatedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalculatedAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsFree
		/// </summary>
		virtual public System.Boolean? IsFree
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFree);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFree, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.LastCalculatedDateTime
		/// </summary>
		virtual public System.DateTime? LastCalculatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.LastCalculatedByUserID
		/// </summary>
		virtual public System.String LastCalculatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsCalcDeductionInPercent
		/// </summary>
		virtual public System.Boolean? IsCalcDeductionInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalcDeductionInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalcDeductionInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.CalcDeductionAmount
		/// </summary>
		virtual public System.Decimal? CalcDeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalcDeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalcDeductionAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DeductionAmount
		/// </summary>
		virtual public System.Decimal? DeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.JournalId, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.OldParamedicID
		/// </summary>
		virtual public System.String OldParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.OldParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.OldParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsModified
		/// </summary>
		virtual public System.Boolean? IsModified
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsModified);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsModified, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PriceItem
		/// </summary>
		virtual public System.Decimal? PriceItem
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PriceItem);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PriceItem, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DiscountItem
		/// </summary>
		virtual public System.Decimal? DiscountItem
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountItem);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountItem, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.TransactionNoRef
		/// </summary>
		virtual public System.String TransactionNoRef
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNoRef);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNoRef, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.RegistrationNoMergeTo
		/// </summary>
		virtual public System.String RegistrationNoMergeTo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNoMergeTo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNoMergeTo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DischargeDateMergeTo
		/// </summary>
		virtual public System.DateTime? DischargeDateMergeTo
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDateMergeTo);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDateMergeTo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PaymentMethodName
		/// </summary>
		virtual public System.String PaymentMethodName
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentMethodName);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentMethodName, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SRPhysicianFeeCategory
		/// </summary>
		virtual public System.String SRPhysicianFeeCategory
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRPhysicianFeeCategory);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRPhysicianFeeCategory, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SRParamedicFeeCaseType
		/// </summary>
		virtual public System.String SRParamedicFeeCaseType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeCaseType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeCaseType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SRParamedicFeeTeamStatus
		/// </summary>
		virtual public System.String SRParamedicFeeTeamStatus
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeTeamStatus);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeTeamStatus, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SRParamedicFeeIsTeam
		/// </summary>
		virtual public System.String SRParamedicFeeIsTeam
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeIsTeam);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeIsTeam, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SumDeductionAmount
		/// </summary>
		virtual public System.Decimal? SumDeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.ParamedicFeeByServiceSettingID
		/// </summary>
		virtual public System.Int32? ParamedicFeeByServiceSettingID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicFeeByServiceSettingID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicFeeByServiceSettingID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SmfID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsGuarantorVerified
		/// </summary>
		virtual public System.Boolean? IsGuarantorVerified
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsGuarantorVerified);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsGuarantorVerified, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PerformanceGross
		/// </summary>
		virtual public System.Decimal? PerformanceGross
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PerformanceGross);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PerformanceGross, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.AdditionalSum
		/// </summary>
		virtual public System.Decimal? AdditionalSum
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.AdditionalSum);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.AdditionalSum, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DeductionConvertion
		/// </summary>
		virtual public System.Decimal? DeductionConvertion
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionConvertion);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionConvertion, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DeductionAnesthetic
		/// </summary>
		virtual public System.Decimal? DeductionAnesthetic
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAnesthetic);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAnesthetic, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DeductionResult
		/// </summary>
		virtual public System.Decimal? DeductionResult
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionResult);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionResult, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.Performance
		/// </summary>
		virtual public System.Decimal? Performance
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Performance);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Performance, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.DiscountExtra
		/// </summary>
		virtual public System.Decimal? DiscountExtra
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountExtra);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountExtra, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PaymentNoCash
		/// </summary>
		virtual public System.String PaymentNoCash
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoCash);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoCash, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PaymentNoAR
		/// </summary>
		virtual public System.String PaymentNoAR
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoAR);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoAR, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.InvoicePaymentNo
		/// </summary>
		virtual public System.String InvoicePaymentNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsWriteOff
		/// </summary>
		virtual public System.Boolean? IsWriteOff
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsWriteOff);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsWriteOff, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.InvoiceWriteOffNo
		/// </summary>
		virtual public System.String InvoiceWriteOffNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoiceWriteOffNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoiceWriteOffNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PercentagePayment
		/// </summary>
		virtual public System.Decimal? PercentagePayment
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePayment);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePayment, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.LastPaymentDate
		/// </summary>
		virtual public System.DateTime? LastPaymentDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastPaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastPaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PaymentNoGuarAR
		/// </summary>
		virtual public System.String PaymentNoGuarAR
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoGuarAR);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoGuarAR, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.InvoicePaymentNoGuar
		/// </summary>
		virtual public System.String InvoicePaymentNoGuar
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNoGuar);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNoGuar, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PercentagePaymentAR
		/// </summary>
		virtual public System.Decimal? PercentagePaymentAR
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentAR);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentAR, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PercentagePaymentGuarAR
		/// </summary>
		virtual public System.Decimal? PercentagePaymentGuarAR
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentGuarAR);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentGuarAR, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.ChangeNote
		/// </summary>
		virtual public System.String ChangeNote
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ChangeNote);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ChangeNote, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SumDeductionAmountAfterTax
		/// </summary>
		virtual public System.Decimal? SumDeductionAmountAfterTax
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmountAfterTax);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmountAfterTax, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.FeeAmountToBePaid
		/// </summary>
		virtual public System.Decimal? FeeAmountToBePaid
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaid);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaid, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.FeeAmountUpdateByUserID
		/// </summary>
		virtual public System.Decimal? FeeAmountUpdateByUserID
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateByUserID);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.FeeAmountUpdateDateTime
		/// </summary>
		virtual public System.DateTime? FeeAmountUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.FeeAmountToBePaidChangeNote
		/// </summary>
		virtual public System.String FeeAmountToBePaidChangeNote
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaidChangeNote);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaidChangeNote, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentGroupNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsCustom
		/// </summary>
		virtual public System.Boolean? IsCustom
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCustom);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCustom, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.SRProcedure1
		/// </summary>
		virtual public System.String SRProcedure1
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRProcedure1);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRProcedure1, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.ExecutedFormula
		/// </summary>
		virtual public System.String ExecutedFormula
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedFormula);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedFormula, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.ExecutedMessage
		/// </summary>
		virtual public System.String ExecutedMessage
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedMessage);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedMessage, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsForTakeOneHighest
		/// </summary>
		virtual public System.Boolean? IsForTakeOneHighest
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsForTakeOneHighest);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsForTakeOneHighest, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PctgPropCash
		/// </summary>
		virtual public System.Decimal? PctgPropCash
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropCash);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropCash, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PctgPropAR
		/// </summary>
		virtual public System.Decimal? PctgPropAR
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropAR);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropAR, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.PctgPropARGuar
		/// </summary>
		virtual public System.Decimal? PctgPropARGuar
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropARGuar);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropARGuar, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.RemunByIdiID
		/// </summary>
		virtual public System.Int32? RemunByIdiID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RemunByIdiID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RemunByIdiID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.TotalBill
		/// </summary>
		virtual public System.Decimal? TotalBill
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TotalBill);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TotalBill, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsBrutoFromFeeAmount
		/// </summary>
		virtual public System.Boolean? IsBrutoFromFeeAmount
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsBrutoFromFeeAmount);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsBrutoFromFeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.FeeAmountBruto
		/// </summary>
		virtual public System.Decimal? FeeAmountBruto
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountBruto);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountBruto, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByDischargeDate.IsFeeTeam
		/// </summary>
		virtual public System.Boolean? IsFeeTeam
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFeeTeam);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFeeTeam, value);
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
		[BrowsableAttribute( false )]		
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
			public esStrings(esParamedicFeeTransChargesItemCompByDischargeDate entity)
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
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
			public System.String DischargeDate
			{
				get
				{
					System.DateTime? data = entity.DischargeDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDate = null;
					else entity.DischargeDate = Convert.ToDateTime(value);
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
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
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
			public System.String Discount
			{
				get
				{
					System.Decimal? data = entity.Discount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount = null;
					else entity.Discount = Convert.ToDecimal(value);
				}
			}
			public System.String FeeAmount
			{
				get
				{
					System.Decimal? data = entity.FeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmount = null;
					else entity.FeeAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsRefferal
			{
				get
				{
					System.Boolean? data = entity.IsRefferal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRefferal = null;
					else entity.IsRefferal = Convert.ToBoolean(value);
				}
			}
			public System.String IsCalculatedInPercent
			{
				get
				{
					System.Boolean? data = entity.IsCalculatedInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCalculatedInPercent = null;
					else entity.IsCalculatedInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String CalculatedAmount
			{
				get
				{
					System.Decimal? data = entity.CalculatedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CalculatedAmount = null;
					else entity.CalculatedAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsFree
			{
				get
				{
					System.Boolean? data = entity.IsFree;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFree = null;
					else entity.IsFree = Convert.ToBoolean(value);
				}
			}
			public System.String LastCalculatedDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCalculatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCalculatedDateTime = null;
					else entity.LastCalculatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastCalculatedByUserID
			{
				get
				{
					System.String data = entity.LastCalculatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCalculatedByUserID = null;
					else entity.LastCalculatedByUserID = Convert.ToString(value);
				}
			}
			public System.String VerificationNo
			{
				get
				{
					System.String data = entity.VerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNo = null;
					else entity.VerificationNo = Convert.ToString(value);
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
			public System.String IsCalcDeductionInPercent
			{
				get
				{
					System.Boolean? data = entity.IsCalcDeductionInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCalcDeductionInPercent = null;
					else entity.IsCalcDeductionInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String CalcDeductionAmount
			{
				get
				{
					System.Decimal? data = entity.CalcDeductionAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CalcDeductionAmount = null;
					else entity.CalcDeductionAmount = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionAmount
			{
				get
				{
					System.Decimal? data = entity.DeductionAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionAmount = null;
					else entity.DeductionAmount = Convert.ToDecimal(value);
				}
			}
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
				}
			}
			public System.String OldParamedicID
			{
				get
				{
					System.String data = entity.OldParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldParamedicID = null;
					else entity.OldParamedicID = Convert.ToString(value);
				}
			}
			public System.String IsModified
			{
				get
				{
					System.Boolean? data = entity.IsModified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsModified = null;
					else entity.IsModified = Convert.ToBoolean(value);
				}
			}
			public System.String PriceItem
			{
				get
				{
					System.Decimal? data = entity.PriceItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceItem = null;
					else entity.PriceItem = Convert.ToDecimal(value);
				}
			}
			public System.String DiscountItem
			{
				get
				{
					System.Decimal? data = entity.DiscountItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountItem = null;
					else entity.DiscountItem = Convert.ToDecimal(value);
				}
			}
			public System.String TransactionNoRef
			{
				get
				{
					System.String data = entity.TransactionNoRef;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNoRef = null;
					else entity.TransactionNoRef = Convert.ToString(value);
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
			public System.String RegistrationNoMergeTo
			{
				get
				{
					System.String data = entity.RegistrationNoMergeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNoMergeTo = null;
					else entity.RegistrationNoMergeTo = Convert.ToString(value);
				}
			}
			public System.String DischargeDateMergeTo
			{
				get
				{
					System.DateTime? data = entity.DischargeDateMergeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDateMergeTo = null;
					else entity.DischargeDateMergeTo = Convert.ToDateTime(value);
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
			public System.String PaymentMethodName
			{
				get
				{
					System.String data = entity.PaymentMethodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentMethodName = null;
					else entity.PaymentMethodName = Convert.ToString(value);
				}
			}
			public System.String SRPhysicianFeeCategory
			{
				get
				{
					System.String data = entity.SRPhysicianFeeCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicianFeeCategory = null;
					else entity.SRPhysicianFeeCategory = Convert.ToString(value);
				}
			}
			public System.String SRParamedicFeeCaseType
			{
				get
				{
					System.String data = entity.SRParamedicFeeCaseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeCaseType = null;
					else entity.SRParamedicFeeCaseType = Convert.ToString(value);
				}
			}
			public System.String SRParamedicFeeTeamStatus
			{
				get
				{
					System.String data = entity.SRParamedicFeeTeamStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeTeamStatus = null;
					else entity.SRParamedicFeeTeamStatus = Convert.ToString(value);
				}
			}
			public System.String SRParamedicFeeIsTeam
			{
				get
				{
					System.String data = entity.SRParamedicFeeIsTeam;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeIsTeam = null;
					else entity.SRParamedicFeeIsTeam = Convert.ToString(value);
				}
			}
			public System.String SumDeductionAmount
			{
				get
				{
					System.Decimal? data = entity.SumDeductionAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SumDeductionAmount = null;
					else entity.SumDeductionAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ParamedicFeeByServiceSettingID
			{
				get
				{
					System.Int32? data = entity.ParamedicFeeByServiceSettingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicFeeByServiceSettingID = null;
					else entity.ParamedicFeeByServiceSettingID = Convert.ToInt32(value);
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
			public System.String IsGuarantorVerified
			{
				get
				{
					System.Boolean? data = entity.IsGuarantorVerified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGuarantorVerified = null;
					else entity.IsGuarantorVerified = Convert.ToBoolean(value);
				}
			}
			public System.String PerformanceGross
			{
				get
				{
					System.Decimal? data = entity.PerformanceGross;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformanceGross = null;
					else entity.PerformanceGross = Convert.ToDecimal(value);
				}
			}
			public System.String AdditionalSum
			{
				get
				{
					System.Decimal? data = entity.AdditionalSum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdditionalSum = null;
					else entity.AdditionalSum = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionConvertion
			{
				get
				{
					System.Decimal? data = entity.DeductionConvertion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionConvertion = null;
					else entity.DeductionConvertion = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionAnesthetic
			{
				get
				{
					System.Decimal? data = entity.DeductionAnesthetic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionAnesthetic = null;
					else entity.DeductionAnesthetic = Convert.ToDecimal(value);
				}
			}
			public System.String DeductionResult
			{
				get
				{
					System.Decimal? data = entity.DeductionResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionResult = null;
					else entity.DeductionResult = Convert.ToDecimal(value);
				}
			}
			public System.String Performance
			{
				get
				{
					System.Decimal? data = entity.Performance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Performance = null;
					else entity.Performance = Convert.ToDecimal(value);
				}
			}
			public System.String DiscountExtra
			{
				get
				{
					System.Decimal? data = entity.DiscountExtra;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountExtra = null;
					else entity.DiscountExtra = Convert.ToDecimal(value);
				}
			}
			public System.String PaymentNoCash
			{
				get
				{
					System.String data = entity.PaymentNoCash;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNoCash = null;
					else entity.PaymentNoCash = Convert.ToString(value);
				}
			}
			public System.String PaymentNoAR
			{
				get
				{
					System.String data = entity.PaymentNoAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNoAR = null;
					else entity.PaymentNoAR = Convert.ToString(value);
				}
			}
			public System.String InvoicePaymentNo
			{
				get
				{
					System.String data = entity.InvoicePaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoicePaymentNo = null;
					else entity.InvoicePaymentNo = Convert.ToString(value);
				}
			}
			public System.String IsWriteOff
			{
				get
				{
					System.Boolean? data = entity.IsWriteOff;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWriteOff = null;
					else entity.IsWriteOff = Convert.ToBoolean(value);
				}
			}
			public System.String InvoiceWriteOffNo
			{
				get
				{
					System.String data = entity.InvoiceWriteOffNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceWriteOffNo = null;
					else entity.InvoiceWriteOffNo = Convert.ToString(value);
				}
			}
			public System.String PercentagePayment
			{
				get
				{
					System.Decimal? data = entity.PercentagePayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentagePayment = null;
					else entity.PercentagePayment = Convert.ToDecimal(value);
				}
			}
			public System.String LastPaymentDate
			{
				get
				{
					System.DateTime? data = entity.LastPaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPaymentDate = null;
					else entity.LastPaymentDate = Convert.ToDateTime(value);
				}
			}
			public System.String PaymentNoGuarAR
			{
				get
				{
					System.String data = entity.PaymentNoGuarAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNoGuarAR = null;
					else entity.PaymentNoGuarAR = Convert.ToString(value);
				}
			}
			public System.String InvoicePaymentNoGuar
			{
				get
				{
					System.String data = entity.InvoicePaymentNoGuar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoicePaymentNoGuar = null;
					else entity.InvoicePaymentNoGuar = Convert.ToString(value);
				}
			}
			public System.String PercentagePaymentAR
			{
				get
				{
					System.Decimal? data = entity.PercentagePaymentAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentagePaymentAR = null;
					else entity.PercentagePaymentAR = Convert.ToDecimal(value);
				}
			}
			public System.String PercentagePaymentGuarAR
			{
				get
				{
					System.Decimal? data = entity.PercentagePaymentGuarAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentagePaymentGuarAR = null;
					else entity.PercentagePaymentGuarAR = Convert.ToDecimal(value);
				}
			}
			public System.String ChangeNote
			{
				get
				{
					System.String data = entity.ChangeNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChangeNote = null;
					else entity.ChangeNote = Convert.ToString(value);
				}
			}
			public System.String SumDeductionAmountAfterTax
			{
				get
				{
					System.Decimal? data = entity.SumDeductionAmountAfterTax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SumDeductionAmountAfterTax = null;
					else entity.SumDeductionAmountAfterTax = Convert.ToDecimal(value);
				}
			}
			public System.String FeeAmountToBePaid
			{
				get
				{
					System.Decimal? data = entity.FeeAmountToBePaid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmountToBePaid = null;
					else entity.FeeAmountToBePaid = Convert.ToDecimal(value);
				}
			}
			public System.String FeeAmountUpdateByUserID
			{
				get
				{
					System.Decimal? data = entity.FeeAmountUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmountUpdateByUserID = null;
					else entity.FeeAmountUpdateByUserID = Convert.ToDecimal(value);
				}
			}
			public System.String FeeAmountUpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.FeeAmountUpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmountUpdateDateTime = null;
					else entity.FeeAmountUpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String FeeAmountToBePaidChangeNote
			{
				get
				{
					System.String data = entity.FeeAmountToBePaidChangeNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmountToBePaidChangeNote = null;
					else entity.FeeAmountToBePaidChangeNote = Convert.ToString(value);
				}
			}
			public System.String PaymentGroupNo
			{
				get
				{
					System.String data = entity.PaymentGroupNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentGroupNo = null;
					else entity.PaymentGroupNo = Convert.ToString(value);
				}
			}
			public System.String IsCustom
			{
				get
				{
					System.Boolean? data = entity.IsCustom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCustom = null;
					else entity.IsCustom = Convert.ToBoolean(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
				}
			}
			public System.String SRProcedure1
			{
				get
				{
					System.String data = entity.SRProcedure1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedure1 = null;
					else entity.SRProcedure1 = Convert.ToString(value);
				}
			}
			public System.String ExecutedFormula
			{
				get
				{
					System.String data = entity.ExecutedFormula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExecutedFormula = null;
					else entity.ExecutedFormula = Convert.ToString(value);
				}
			}
			public System.String ExecutedMessage
			{
				get
				{
					System.String data = entity.ExecutedMessage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExecutedMessage = null;
					else entity.ExecutedMessage = Convert.ToString(value);
				}
			}
			public System.String IsForTakeOneHighest
			{
				get
				{
					System.Boolean? data = entity.IsForTakeOneHighest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsForTakeOneHighest = null;
					else entity.IsForTakeOneHighest = Convert.ToBoolean(value);
				}
			}
			public System.String PctgPropCash
			{
				get
				{
					System.Decimal? data = entity.PctgPropCash;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PctgPropCash = null;
					else entity.PctgPropCash = Convert.ToDecimal(value);
				}
			}
			public System.String PctgPropAR
			{
				get
				{
					System.Decimal? data = entity.PctgPropAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PctgPropAR = null;
					else entity.PctgPropAR = Convert.ToDecimal(value);
				}
			}
			public System.String PctgPropARGuar
			{
				get
				{
					System.Decimal? data = entity.PctgPropARGuar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PctgPropARGuar = null;
					else entity.PctgPropARGuar = Convert.ToDecimal(value);
				}
			}
			public System.String RemunByIdiID
			{
				get
				{
					System.Int32? data = entity.RemunByIdiID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunByIdiID = null;
					else entity.RemunByIdiID = Convert.ToInt32(value);
				}
			}
			public System.String TotalBill
			{
				get
				{
					System.Decimal? data = entity.TotalBill;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalBill = null;
					else entity.TotalBill = Convert.ToDecimal(value);
				}
			}
			public System.String IsBrutoFromFeeAmount
			{
				get
				{
					System.Boolean? data = entity.IsBrutoFromFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBrutoFromFeeAmount = null;
					else entity.IsBrutoFromFeeAmount = Convert.ToBoolean(value);
				}
			}
			public System.String FeeAmountBruto
			{
				get
				{
					System.Decimal? data = entity.FeeAmountBruto;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmountBruto = null;
					else entity.FeeAmountBruto = Convert.ToDecimal(value);
				}
			}
			public System.String IsFeeTeam
			{
				get
				{
					System.Boolean? data = entity.IsFeeTeam;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeeTeam = null;
					else entity.IsFeeTeam = Convert.ToBoolean(value);
				}
			}
			private esParamedicFeeTransChargesItemCompByDischargeDate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompByDischargeDateQuery query)
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
				throw new Exception("esParamedicFeeTransChargesItemCompByDischargeDate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeTransChargesItemCompByDischargeDate : esParamedicFeeTransChargesItemCompByDischargeDate
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeTransChargesItemCompByDischargeDateQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompByDischargeDateMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsOrderRealization
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsOrderRealization, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsRefferal
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsRefferal, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsCalculatedInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalculatedInPercent, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CalculatedAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalculatedAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsFree
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFree, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastCalculatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastCalculatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsCalcDeductionInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalcDeductionInPercent, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CalcDeductionAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalcDeductionAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem OldParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.OldParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsModified
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsModified, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PriceItem
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PriceItem, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DiscountItem
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountItem, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TransactionNoRef
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNoRef, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNoMergeTo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNoMergeTo, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDateMergeTo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDateMergeTo, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentMethodName
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentMethodName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPhysicianFeeCategory
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRPhysicianFeeCategory, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicFeeCaseType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeCaseType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicFeeTeamStatus
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeTeamStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicFeeIsTeam
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeIsTeam, esSystemType.String);
			}
		} 
			
		public esQueryItem SumDeductionAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ParamedicFeeByServiceSettingID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicFeeByServiceSettingID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsGuarantorVerified
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsGuarantorVerified, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PerformanceGross
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PerformanceGross, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AdditionalSum
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.AdditionalSum, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionConvertion
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionConvertion, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionAnesthetic
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAnesthetic, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DeductionResult
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionResult, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Performance
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Performance, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DiscountExtra
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountExtra, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PaymentNoCash
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoCash, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentNoAR
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoAR, esSystemType.String);
			}
		} 
			
		public esQueryItem InvoicePaymentNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsWriteOff
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsWriteOff, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem InvoiceWriteOffNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoiceWriteOffNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PercentagePayment
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePayment, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastPaymentDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastPaymentDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PaymentNoGuarAR
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoGuarAR, esSystemType.String);
			}
		} 
			
		public esQueryItem InvoicePaymentNoGuar
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNoGuar, esSystemType.String);
			}
		} 
			
		public esQueryItem PercentagePaymentAR
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentAR, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PercentagePaymentGuarAR
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentGuarAR, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ChangeNote
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ChangeNote, esSystemType.String);
			}
		} 
			
		public esQueryItem SumDeductionAmountAfterTax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmountAfterTax, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeAmountToBePaid
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaid, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeAmountUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateByUserID, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeAmountUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem FeeAmountToBePaidChangeNote
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaidChangeNote, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsCustom
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCustom, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRProcedure1
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRProcedure1, esSystemType.String);
			}
		} 
			
		public esQueryItem ExecutedFormula
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedFormula, esSystemType.String);
			}
		} 
			
		public esQueryItem ExecutedMessage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedMessage, esSystemType.String);
			}
		} 
			
		public esQueryItem IsForTakeOneHighest
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsForTakeOneHighest, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PctgPropCash
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropCash, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PctgPropAR
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropAR, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PctgPropARGuar
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropARGuar, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem RemunByIdiID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RemunByIdiID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TotalBill
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TotalBill, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsBrutoFromFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsBrutoFromFeeAmount, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem FeeAmountBruto
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountBruto, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsFeeTeam
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFeeTeam, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeTransChargesItemCompByDischargeDateCollection")]
	public partial class ParamedicFeeTransChargesItemCompByDischargeDateCollection : esParamedicFeeTransChargesItemCompByDischargeDateCollection, IEnumerable< ParamedicFeeTransChargesItemCompByDischargeDate>
	{
		public ParamedicFeeTransChargesItemCompByDischargeDateCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeTransChargesItemCompByDischargeDate>(ParamedicFeeTransChargesItemCompByDischargeDateCollection coll)
		{
			List< ParamedicFeeTransChargesItemCompByDischargeDate> list = new List< ParamedicFeeTransChargesItemCompByDischargeDate>();
			
			foreach (ParamedicFeeTransChargesItemCompByDischargeDate emp in coll)
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
				return  ParamedicFeeTransChargesItemCompByDischargeDateMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeTransChargesItemCompByDischargeDate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeTransChargesItemCompByDischargeDate();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompByDischargeDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
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
		public bool Load(ParamedicFeeTransChargesItemCompByDischargeDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeTransChargesItemCompByDischargeDate AddNew()
		{
			ParamedicFeeTransChargesItemCompByDischargeDate entity = base.AddNewEntity() as ParamedicFeeTransChargesItemCompByDischargeDate;
			
			return entity;		
		}
		public ParamedicFeeTransChargesItemCompByDischargeDate FindByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID) as ParamedicFeeTransChargesItemCompByDischargeDate;
		}

		#region IEnumerable< ParamedicFeeTransChargesItemCompByDischargeDate> Members

		IEnumerator< ParamedicFeeTransChargesItemCompByDischargeDate> IEnumerable< ParamedicFeeTransChargesItemCompByDischargeDate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeTransChargesItemCompByDischargeDate;
			}
		}

		#endregion
		
		private ParamedicFeeTransChargesItemCompByDischargeDateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeTransChargesItemCompByDischargeDate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeTransChargesItemCompByDischargeDate ({TransactionNo, SequenceNo, TariffComponentID})")]
	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompByDischargeDate : esParamedicFeeTransChargesItemCompByDischargeDate
	{
		public ParamedicFeeTransChargesItemCompByDischargeDate()
		{
		}	
	
		public ParamedicFeeTransChargesItemCompByDischargeDate(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompByDischargeDateMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeTransChargesItemCompByDischargeDateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompByDischargeDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
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
		public bool Load(ParamedicFeeTransChargesItemCompByDischargeDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeTransChargesItemCompByDischargeDateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompByDischargeDateQuery : esParamedicFeeTransChargesItemCompByDischargeDateQuery
	{
		public ParamedicFeeTransChargesItemCompByDischargeDateQuery()
		{

		}		
		
		public ParamedicFeeTransChargesItemCompByDischargeDateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeTransChargesItemCompByDischargeDateQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompByDischargeDateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeTransChargesItemCompByDischargeDateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DischargeDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsOrderRealization, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsOrderRealization;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ItemID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Qty, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Price, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Discount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.FeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsRefferal, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsRefferal;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalculatedInPercent, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsCalculatedInPercent;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalculatedAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.CalculatedAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFree, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsFree;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.LastCalculatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastCalculatedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.LastCalculatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.VerificationNo, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCalcDeductionInPercent, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsCalcDeductionInPercent;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CalcDeductionAmount, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.CalcDeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAmount, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.JournalId, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.OldParamedicID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.OldParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsModified, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsModified;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PriceItem, 26, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PriceItem;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountItem, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DiscountItem;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TransactionNoRef, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.TransactionNoRef;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNo, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RegistrationNoMergeTo, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.RegistrationNoMergeTo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DischargeDateMergeTo, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DischargeDateMergeTo;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Notes, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentMethodName, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PaymentMethodName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRPhysicianFeeCategory, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SRPhysicianFeeCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeCaseType, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SRParamedicFeeCaseType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeTeamStatus, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SRParamedicFeeTeamStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRParamedicFeeIsTeam, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SRParamedicFeeIsTeam;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmount, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SumDeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ParamedicFeeByServiceSettingID, 39, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.ParamedicFeeByServiceSettingID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SmfID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SmfID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsGuarantorVerified, 41, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsGuarantorVerified;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PerformanceGross, 42, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PerformanceGross;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.AdditionalSum, 43, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.AdditionalSum;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionConvertion, 44, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DeductionConvertion;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionAnesthetic, 45, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DeductionAnesthetic;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DeductionResult, 46, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DeductionResult;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.Performance, 47, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.Performance;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.DiscountExtra, 48, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.DiscountExtra;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoCash, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PaymentNoCash;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoAR, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PaymentNoAR;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNo, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.InvoicePaymentNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsWriteOff, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsWriteOff;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoiceWriteOffNo, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.InvoiceWriteOffNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePayment, 54, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PercentagePayment;
			c.NumericPrecision = 7;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.LastPaymentDate, 55, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.LastPaymentDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentNoGuarAR, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PaymentNoGuarAR;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.InvoicePaymentNoGuar, 57, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.InvoicePaymentNoGuar;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentAR, 58, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PercentagePaymentAR;
			c.NumericPrecision = 7;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PercentagePaymentGuarAR, 59, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PercentagePaymentGuarAR;
			c.NumericPrecision = 7;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ChangeNote, 60, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.ChangeNote;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SumDeductionAmountAfterTax, 61, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SumDeductionAmountAfterTax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaid, 62, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.FeeAmountToBePaid;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateByUserID, 63, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.FeeAmountUpdateByUserID;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountUpdateDateTime, 64, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.FeeAmountUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountToBePaidChangeNote, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.FeeAmountToBePaidChangeNote;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PaymentGroupNo, 66, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PaymentGroupNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsCustom, 67, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsCustom;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateDateTime, 68, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.CreateByUserID, 69, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.SRProcedure1, 70, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.SRProcedure1;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedFormula, 71, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.ExecutedFormula;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.ExecutedMessage, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.ExecutedMessage;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsForTakeOneHighest, 73, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsForTakeOneHighest;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropCash, 74, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PctgPropCash;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropAR, 75, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PctgPropAR;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.PctgPropARGuar, 76, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.PctgPropARGuar;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.RemunByIdiID, 77, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.RemunByIdiID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.TotalBill, 78, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.TotalBill;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsBrutoFromFeeAmount, 79, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsBrutoFromFeeAmount;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.FeeAmountBruto, 80, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.FeeAmountBruto;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.ColumnNames.IsFeeTeam, 81, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByDischargeDateMetadata.PropertyNames.IsFeeTeam;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeTransChargesItemCompByDischargeDateMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string DischargeDate = "DischargeDate";
			public const string IsOrderRealization = "IsOrderRealization";
			public const string ParamedicID = "ParamedicID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string Discount = "Discount";
			public const string FeeAmount = "FeeAmount";
			public const string IsRefferal = "IsRefferal";
			public const string IsCalculatedInPercent = "IsCalculatedInPercent";
			public const string CalculatedAmount = "CalculatedAmount";
			public const string IsFree = "IsFree";
			public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			public const string VerificationNo = "VerificationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsCalcDeductionInPercent = "IsCalcDeductionInPercent";
			public const string CalcDeductionAmount = "CalcDeductionAmount";
			public const string DeductionAmount = "DeductionAmount";
			public const string JournalId = "JournalId";
			public const string OldParamedicID = "OldParamedicID";
			public const string IsModified = "IsModified";
			public const string PriceItem = "PriceItem";
			public const string DiscountItem = "DiscountItem";
			public const string TransactionNoRef = "TransactionNoRef";
			public const string RegistrationNo = "RegistrationNo";
			public const string RegistrationNoMergeTo = "RegistrationNoMergeTo";
			public const string DischargeDateMergeTo = "DischargeDateMergeTo";
			public const string Notes = "Notes";
			public const string PaymentMethodName = "PaymentMethodName";
			public const string SRPhysicianFeeCategory = "SRPhysicianFeeCategory";
			public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			public const string SRParamedicFeeTeamStatus = "SRParamedicFeeTeamStatus";
			public const string SRParamedicFeeIsTeam = "SRParamedicFeeIsTeam";
			public const string SumDeductionAmount = "SumDeductionAmount";
			public const string ParamedicFeeByServiceSettingID = "ParamedicFeeByServiceSettingID";
			public const string SmfID = "SmfID";
			public const string IsGuarantorVerified = "IsGuarantorVerified";
			public const string PerformanceGross = "PerformanceGross";
			public const string AdditionalSum = "AdditionalSum";
			public const string DeductionConvertion = "DeductionConvertion";
			public const string DeductionAnesthetic = "DeductionAnesthetic";
			public const string DeductionResult = "DeductionResult";
			public const string Performance = "Performance";
			public const string DiscountExtra = "DiscountExtra";
			public const string PaymentNoCash = "PaymentNoCash";
			public const string PaymentNoAR = "PaymentNoAR";
			public const string InvoicePaymentNo = "InvoicePaymentNo";
			public const string IsWriteOff = "IsWriteOff";
			public const string InvoiceWriteOffNo = "InvoiceWriteOffNo";
			public const string PercentagePayment = "PercentagePayment";
			public const string LastPaymentDate = "LastPaymentDate";
			public const string PaymentNoGuarAR = "PaymentNoGuarAR";
			public const string InvoicePaymentNoGuar = "InvoicePaymentNoGuar";
			public const string PercentagePaymentAR = "PercentagePaymentAR";
			public const string PercentagePaymentGuarAR = "PercentagePaymentGuarAR";
			public const string ChangeNote = "ChangeNote";
			public const string SumDeductionAmountAfterTax = "SumDeductionAmountAfterTax";
			public const string FeeAmountToBePaid = "FeeAmountToBePaid";
			public const string FeeAmountUpdateByUserID = "FeeAmountUpdateByUserID";
			public const string FeeAmountUpdateDateTime = "FeeAmountUpdateDateTime";
			public const string FeeAmountToBePaidChangeNote = "FeeAmountToBePaidChangeNote";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string IsCustom = "IsCustom";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string SRProcedure1 = "SRProcedure1";
			public const string ExecutedFormula = "ExecutedFormula";
			public const string ExecutedMessage = "ExecutedMessage";
			public const string IsForTakeOneHighest = "IsForTakeOneHighest";
			public const string PctgPropCash = "PctgPropCash";
			public const string PctgPropAR = "PctgPropAR";
			public const string PctgPropARGuar = "PctgPropARGuar";
			public const string RemunByIdiID = "RemunByIdiID";
			public const string TotalBill = "TotalBill";
			public const string IsBrutoFromFeeAmount = "IsBrutoFromFeeAmount";
			public const string FeeAmountBruto = "FeeAmountBruto";
			public const string IsFeeTeam = "IsFeeTeam";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string DischargeDate = "DischargeDate";
			public const string IsOrderRealization = "IsOrderRealization";
			public const string ParamedicID = "ParamedicID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string Discount = "Discount";
			public const string FeeAmount = "FeeAmount";
			public const string IsRefferal = "IsRefferal";
			public const string IsCalculatedInPercent = "IsCalculatedInPercent";
			public const string CalculatedAmount = "CalculatedAmount";
			public const string IsFree = "IsFree";
			public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			public const string VerificationNo = "VerificationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsCalcDeductionInPercent = "IsCalcDeductionInPercent";
			public const string CalcDeductionAmount = "CalcDeductionAmount";
			public const string DeductionAmount = "DeductionAmount";
			public const string JournalId = "JournalId";
			public const string OldParamedicID = "OldParamedicID";
			public const string IsModified = "IsModified";
			public const string PriceItem = "PriceItem";
			public const string DiscountItem = "DiscountItem";
			public const string TransactionNoRef = "TransactionNoRef";
			public const string RegistrationNo = "RegistrationNo";
			public const string RegistrationNoMergeTo = "RegistrationNoMergeTo";
			public const string DischargeDateMergeTo = "DischargeDateMergeTo";
			public const string Notes = "Notes";
			public const string PaymentMethodName = "PaymentMethodName";
			public const string SRPhysicianFeeCategory = "SRPhysicianFeeCategory";
			public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			public const string SRParamedicFeeTeamStatus = "SRParamedicFeeTeamStatus";
			public const string SRParamedicFeeIsTeam = "SRParamedicFeeIsTeam";
			public const string SumDeductionAmount = "SumDeductionAmount";
			public const string ParamedicFeeByServiceSettingID = "ParamedicFeeByServiceSettingID";
			public const string SmfID = "SmfID";
			public const string IsGuarantorVerified = "IsGuarantorVerified";
			public const string PerformanceGross = "PerformanceGross";
			public const string AdditionalSum = "AdditionalSum";
			public const string DeductionConvertion = "DeductionConvertion";
			public const string DeductionAnesthetic = "DeductionAnesthetic";
			public const string DeductionResult = "DeductionResult";
			public const string Performance = "Performance";
			public const string DiscountExtra = "DiscountExtra";
			public const string PaymentNoCash = "PaymentNoCash";
			public const string PaymentNoAR = "PaymentNoAR";
			public const string InvoicePaymentNo = "InvoicePaymentNo";
			public const string IsWriteOff = "IsWriteOff";
			public const string InvoiceWriteOffNo = "InvoiceWriteOffNo";
			public const string PercentagePayment = "PercentagePayment";
			public const string LastPaymentDate = "LastPaymentDate";
			public const string PaymentNoGuarAR = "PaymentNoGuarAR";
			public const string InvoicePaymentNoGuar = "InvoicePaymentNoGuar";
			public const string PercentagePaymentAR = "PercentagePaymentAR";
			public const string PercentagePaymentGuarAR = "PercentagePaymentGuarAR";
			public const string ChangeNote = "ChangeNote";
			public const string SumDeductionAmountAfterTax = "SumDeductionAmountAfterTax";
			public const string FeeAmountToBePaid = "FeeAmountToBePaid";
			public const string FeeAmountUpdateByUserID = "FeeAmountUpdateByUserID";
			public const string FeeAmountUpdateDateTime = "FeeAmountUpdateDateTime";
			public const string FeeAmountToBePaidChangeNote = "FeeAmountToBePaidChangeNote";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string IsCustom = "IsCustom";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string SRProcedure1 = "SRProcedure1";
			public const string ExecutedFormula = "ExecutedFormula";
			public const string ExecutedMessage = "ExecutedMessage";
			public const string IsForTakeOneHighest = "IsForTakeOneHighest";
			public const string PctgPropCash = "PctgPropCash";
			public const string PctgPropAR = "PctgPropAR";
			public const string PctgPropARGuar = "PctgPropARGuar";
			public const string RemunByIdiID = "RemunByIdiID";
			public const string TotalBill = "TotalBill";
			public const string IsBrutoFromFeeAmount = "IsBrutoFromFeeAmount";
			public const string FeeAmountBruto = "FeeAmountBruto";
			public const string IsFeeTeam = "IsFeeTeam";
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
			lock (typeof(ParamedicFeeTransChargesItemCompByDischargeDateMetadata))
			{
				if(ParamedicFeeTransChargesItemCompByDischargeDateMetadata.mapDelegates == null)
				{
					ParamedicFeeTransChargesItemCompByDischargeDateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeTransChargesItemCompByDischargeDateMetadata.meta == null)
				{
					ParamedicFeeTransChargesItemCompByDischargeDateMetadata.meta = new ParamedicFeeTransChargesItemCompByDischargeDateMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsOrderRealization", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsRefferal", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCalculatedInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CalculatedAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsFree", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastCalculatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCalculatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCalcDeductionInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CalcDeductionAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OldParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsModified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PriceItem", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountItem", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TransactionNoRef", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNoMergeTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDateMergeTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentMethodName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPhysicianFeeCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeCaseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeTeamStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeIsTeam", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SumDeductionAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicFeeByServiceSettingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGuarantorVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PerformanceGross", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AdditionalSum", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DeductionConvertion", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DeductionAnesthetic", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DeductionResult", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Performance", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DiscountExtra", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PaymentNoCash", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNoAR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoicePaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsWriteOff", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InvoiceWriteOffNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PercentagePayment", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("LastPaymentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentNoGuarAR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvoicePaymentNoGuar", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PercentagePaymentAR", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PercentagePaymentGuarAR", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ChangeNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SumDeductionAmountAfterTax", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FeeAmountToBePaid", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FeeAmountUpdateByUserID", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FeeAmountUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FeeAmountToBePaidChangeNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCustom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcedure1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExecutedFormula", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExecutedMessage", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsForTakeOneHighest", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PctgPropCash", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PctgPropAR", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PctgPropARGuar", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("RemunByIdiID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalBill", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("IsBrutoFromFeeAmount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FeeAmountBruto", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("IsFeeTeam", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "ParamedicFeeTransChargesItemCompByDischargeDate";
				meta.Destination = "ParamedicFeeTransChargesItemCompByDischargeDate";
				meta.spInsert = "proc_ParamedicFeeTransChargesItemCompByDischargeDateInsert";				
				meta.spUpdate = "proc_ParamedicFeeTransChargesItemCompByDischargeDateUpdate";		
				meta.spDelete = "proc_ParamedicFeeTransChargesItemCompByDischargeDateDelete";
				meta.spLoadAll = "proc_ParamedicFeeTransChargesItemCompByDischargeDateLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeTransChargesItemCompByDischargeDateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeTransChargesItemCompByDischargeDateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
