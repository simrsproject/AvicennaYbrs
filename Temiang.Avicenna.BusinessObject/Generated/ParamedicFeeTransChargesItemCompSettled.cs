/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 12/18/2014 12:03:28 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esParamedicFeeTransChargesItemCompSettledCollection : esEntityCollection
	{
		public esParamedicFeeTransChargesItemCompSettledCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeTransChargesItemCompSettledCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompSettledQuery query)
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
			this.InitQuery(query as esParamedicFeeTransChargesItemCompSettledQuery);
		}
		#endregion
		
		virtual public ParamedicFeeTransChargesItemCompSettled DetachEntity(ParamedicFeeTransChargesItemCompSettled entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeTransChargesItemCompSettled;
		}
		
		virtual public ParamedicFeeTransChargesItemCompSettled AttachEntity(ParamedicFeeTransChargesItemCompSettled entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeTransChargesItemCompSettled;
		}
		
		virtual public void Combine(ParamedicFeeTransChargesItemCompSettledCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeTransChargesItemCompSettled this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeTransChargesItemCompSettled;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeTransChargesItemCompSettled);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeTransChargesItemCompSettled : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeTransChargesItemCompSettledQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeTransChargesItemCompSettled()
		{

		}

		public esParamedicFeeTransChargesItemCompSettled(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paymentNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.Boolean isFromAr, System.Boolean isReturn)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, transactionNo, sequenceNo, tariffComponentID, isFromAr, isReturn);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, transactionNo, sequenceNo, tariffComponentID, isFromAr, isReturn);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paymentNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.Boolean isFromAr, System.Boolean isReturn)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, transactionNo, sequenceNo, tariffComponentID, isFromAr, isReturn);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, transactionNo, sequenceNo, tariffComponentID, isFromAr, isReturn);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paymentNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.Boolean isFromAr, System.Boolean isReturn)
		{
			esParamedicFeeTransChargesItemCompSettledQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.TariffComponentID == tariffComponentID, query.IsFromAr == isFromAr, query.IsReturn == isReturn);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paymentNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.Boolean isFromAr, System.Boolean isReturn)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo",paymentNo);			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("TariffComponentID",tariffComponentID);			parms.Add("IsFromAr",isFromAr);			parms.Add("IsReturn",isReturn);
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
						case "PaymentNo": this.str.PaymentNo = (string)value; break;							
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "PaymentDate": this.str.PaymentDate = (string)value; break;							
						case "IsFromAr": this.str.IsFromAr = (string)value; break;							
						case "IsReturn": this.str.IsReturn = (string)value; break;							
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
					}
				}
				else
				{
					switch (name)
					{	
						case "PaymentDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentDate = (System.DateTime?)value;
							break;
						
						case "IsFromAr":
						
							if (value == null || value is System.Boolean)
								this.IsFromAr = (System.Boolean?)value;
							break;
						
						case "IsReturn":
						
							if (value == null || value is System.Boolean)
								this.IsReturn = (System.Boolean?)value;
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
		/// Maps to ParamedicFeeTransChargesItemCompSettled.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.IsFromAr
		/// </summary>
		virtual public System.Boolean? IsFromAr
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFromAr);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFromAr, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.IsReturn
		/// </summary>
		virtual public System.Boolean? IsReturn
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsReturn);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsReturn, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.IsOrderRealization
		/// </summary>
		virtual public System.Boolean? IsOrderRealization
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsOrderRealization);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsOrderRealization, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Qty, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Discount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.FeeAmount
		/// </summary>
		virtual public System.Decimal? FeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.FeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.FeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.IsRefferal
		/// </summary>
		virtual public System.Boolean? IsRefferal
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsRefferal);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsRefferal, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.IsCalculatedInPercent
		/// </summary>
		virtual public System.Boolean? IsCalculatedInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalculatedInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalculatedInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.CalculatedAmount
		/// </summary>
		virtual public System.Decimal? CalculatedAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalculatedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalculatedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.IsFree
		/// </summary>
		virtual public System.Boolean? IsFree
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFree);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFree, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.LastCalculatedDateTime
		/// </summary>
		virtual public System.DateTime? LastCalculatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.LastCalculatedByUserID
		/// </summary>
		virtual public System.String LastCalculatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.VerificationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.IsCalcDeductionInPercent
		/// </summary>
		virtual public System.Boolean? IsCalcDeductionInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalcDeductionInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalcDeductionInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.CalcDeductionAmount
		/// </summary>
		virtual public System.Decimal? CalcDeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalcDeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalcDeductionAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.DeductionAmount
		/// </summary>
		virtual public System.Decimal? DeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.DeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.DeductionAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompSettled.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.JournalId, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esParamedicFeeTransChargesItemCompSettled entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
				}
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
				
			public System.String PaymentDate
			{
				get
				{
					System.DateTime? data = entity.PaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentDate = null;
					else entity.PaymentDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String IsFromAr
			{
				get
				{
					System.Boolean? data = entity.IsFromAr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromAr = null;
					else entity.IsFromAr = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsReturn
			{
				get
				{
					System.Boolean? data = entity.IsReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReturn = null;
					else entity.IsReturn = Convert.ToBoolean(value);
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
			

			private esParamedicFeeTransChargesItemCompSettled entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompSettledQuery query)
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
				throw new Exception("esParamedicFeeTransChargesItemCompSettled can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esParamedicFeeTransChargesItemCompSettledQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompSettledMetadata.Meta();
			}
		}	
		

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsFromAr
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFromAr, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsReturn
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsReturn, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsOrderRealization
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsOrderRealization, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.FeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsRefferal
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsRefferal, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCalculatedInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalculatedInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CalculatedAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalculatedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsFree
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFree, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastCalculatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastCalculatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsCalcDeductionInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalcDeductionInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CalcDeductionAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalcDeductionAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeductionAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.DeductionAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeTransChargesItemCompSettledCollection")]
	public partial class ParamedicFeeTransChargesItemCompSettledCollection : esParamedicFeeTransChargesItemCompSettledCollection, IEnumerable<ParamedicFeeTransChargesItemCompSettled>
	{
		public ParamedicFeeTransChargesItemCompSettledCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeTransChargesItemCompSettled>(ParamedicFeeTransChargesItemCompSettledCollection coll)
		{
			List<ParamedicFeeTransChargesItemCompSettled> list = new List<ParamedicFeeTransChargesItemCompSettled>();
			
			foreach (ParamedicFeeTransChargesItemCompSettled emp in coll)
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
				return  ParamedicFeeTransChargesItemCompSettledMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompSettledQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeTransChargesItemCompSettled(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeTransChargesItemCompSettled();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompSettledQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompSettledQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeTransChargesItemCompSettledQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeTransChargesItemCompSettled AddNew()
		{
			ParamedicFeeTransChargesItemCompSettled entity = base.AddNewEntity() as ParamedicFeeTransChargesItemCompSettled;
			
			return entity;
		}

		public ParamedicFeeTransChargesItemCompSettled FindByPrimaryKey(System.String paymentNo, System.String transactionNo, System.String sequenceNo, System.String tariffComponentID, System.Boolean isFromAr, System.Boolean isReturn)
		{
			return base.FindByPrimaryKey(paymentNo, transactionNo, sequenceNo, tariffComponentID, isFromAr, isReturn) as ParamedicFeeTransChargesItemCompSettled;
		}


		#region IEnumerable<ParamedicFeeTransChargesItemCompSettled> Members

		IEnumerator<ParamedicFeeTransChargesItemCompSettled> IEnumerable<ParamedicFeeTransChargesItemCompSettled>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeTransChargesItemCompSettled;
			}
		}

		#endregion
		
		private ParamedicFeeTransChargesItemCompSettledQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeTransChargesItemCompSettled' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeTransChargesItemCompSettled ({PaymentNo},{TransactionNo},{SequenceNo},{TariffComponentID},{IsFromAr},{IsReturn})")]
	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompSettled : esParamedicFeeTransChargesItemCompSettled
	{
		public ParamedicFeeTransChargesItemCompSettled()
		{

		}
	
		public ParamedicFeeTransChargesItemCompSettled(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompSettledMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeTransChargesItemCompSettledQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompSettledQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompSettledQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompSettledQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeTransChargesItemCompSettledQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeTransChargesItemCompSettledQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeTransChargesItemCompSettledQuery : esParamedicFeeTransChargesItemCompSettledQuery
	{
		public ParamedicFeeTransChargesItemCompSettledQuery()
		{

		}		
		
		public ParamedicFeeTransChargesItemCompSettledQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeTransChargesItemCompSettledQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompSettledMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeTransChargesItemCompSettledMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.PaymentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.PaymentDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFromAr, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.IsFromAr;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsReturn, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.IsReturn;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsOrderRealization, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.IsOrderRealization;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ParamedicID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.ItemID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Qty, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Price, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.Discount, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.FeeAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.FeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsRefferal, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.IsRefferal;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalculatedInPercent, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.IsCalculatedInPercent;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalculatedAmount, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.CalculatedAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsFree, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.IsFree;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.LastCalculatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastCalculatedByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.LastCalculatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.VerificationNo, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.IsCalcDeductionInPercent, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.IsCalcDeductionInPercent;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.CalcDeductionAmount, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.CalcDeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.DeductionAmount, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.DeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompSettledMetadata.ColumnNames.JournalId, 26, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeTransChargesItemCompSettledMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeTransChargesItemCompSettledMetadata Meta()
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
			 public const string PaymentNo = "PaymentNo";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string PaymentDate = "PaymentDate";
			 public const string IsFromAr = "IsFromAr";
			 public const string IsReturn = "IsReturn";
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
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PaymentNo = "PaymentNo";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string PaymentDate = "PaymentDate";
			 public const string IsFromAr = "IsFromAr";
			 public const string IsReturn = "IsReturn";
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
			lock (typeof(ParamedicFeeTransChargesItemCompSettledMetadata))
			{
				if(ParamedicFeeTransChargesItemCompSettledMetadata.mapDelegates == null)
				{
					ParamedicFeeTransChargesItemCompSettledMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeTransChargesItemCompSettledMetadata.meta == null)
				{
					ParamedicFeeTransChargesItemCompSettledMetadata.meta = new ParamedicFeeTransChargesItemCompSettledMetadata();
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
				

				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsFromAr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReturn", new esTypeMap("bit", "System.Boolean"));
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
				
				
				
				meta.Source = "ParamedicFeeTransChargesItemCompSettled";
				meta.Destination = "ParamedicFeeTransChargesItemCompSettled";
				
				meta.spInsert = "proc_ParamedicFeeTransChargesItemCompSettledInsert";				
				meta.spUpdate = "proc_ParamedicFeeTransChargesItemCompSettledUpdate";		
				meta.spDelete = "proc_ParamedicFeeTransChargesItemCompSettledDelete";
				meta.spLoadAll = "proc_ParamedicFeeTransChargesItemCompSettledLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeTransChargesItemCompSettledLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeTransChargesItemCompSettledMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
