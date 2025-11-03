/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/2/2023 2:21:56 PM
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
	abstract public class esParamedicFeeTransChargesItemCompByTeamCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeTransChargesItemCompByTeamCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeTransChargesItemCompByTeamCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompByTeamQuery query)
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
			this.InitQuery(query as esParamedicFeeTransChargesItemCompByTeamQuery);
		}
		#endregion
			
		virtual public ParamedicFeeTransChargesItemCompByTeam DetachEntity(ParamedicFeeTransChargesItemCompByTeam entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeTransChargesItemCompByTeam;
		}
		
		virtual public ParamedicFeeTransChargesItemCompByTeam AttachEntity(ParamedicFeeTransChargesItemCompByTeam entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeTransChargesItemCompByTeam;
		}
		
		virtual public void Combine(ParamedicFeeTransChargesItemCompByTeamCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeTransChargesItemCompByTeam this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeTransChargesItemCompByTeam;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeTransChargesItemCompByTeam);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeTransChargesItemCompByTeam : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeTransChargesItemCompByTeamQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeTransChargesItemCompByTeam()
		{
		}
	
		public esParamedicFeeTransChargesItemCompByTeam(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID, String paramedicID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID, paramedicID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String tariffComponentID, String paramedicID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, tariffComponentID, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, tariffComponentID, paramedicID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String tariffComponentID, String paramedicID)
		{
			esParamedicFeeTransChargesItemCompByTeamQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo, query.SequenceNo==sequenceNo, query.TariffComponentID==tariffComponentID, query.ParamedicID==paramedicID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String tariffComponentID, String paramedicID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
			parms.Add("SequenceNo",sequenceNo);
			parms.Add("TariffComponentID",tariffComponentID);
			parms.Add("ParamedicID",paramedicID);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "Discount": this.str.Discount = (string)value; break;
						case "FeeAmount": this.str.FeeAmount = (string)value; break;
						case "IsCalculatedInPercent": this.str.IsCalculatedInPercent = (string)value; break;
						case "CalculatedAmount": this.str.CalculatedAmount = (string)value; break;
						case "LastCalculatedDateTime": this.str.LastCalculatedDateTime = (string)value; break;
						case "LastCalculatedByUserID": this.str.LastCalculatedByUserID = (string)value; break;
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "JournalId": this.str.JournalId = (string)value; break;
						case "PriceItem": this.str.PriceItem = (string)value; break;
						case "DiscountItem": this.str.DiscountItem = (string)value; break;
						case "TransactionNoRef": this.str.TransactionNoRef = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "RegistrationNoMergeTo": this.str.RegistrationNoMergeTo = (string)value; break;
						case "DischargeDateMergeTo": this.str.DischargeDateMergeTo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "ParamedicFeeByServiceSettingID": this.str.ParamedicFeeByServiceSettingID = (string)value; break;
						case "IsWriteOff": this.str.IsWriteOff = (string)value; break;
						case "InvoiceWriteOffNo": this.str.InvoiceWriteOffNo = (string)value; break;
						case "ChangeNote": this.str.ChangeNote = (string)value; break;
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "FeeAmountBruto": this.str.FeeAmountBruto = (string)value; break;
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
						case "IsCalculatedInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsCalculatedInPercent = (System.Boolean?)value;
							break;
						case "CalculatedAmount":
						
							if (value == null || value is System.Decimal)
								this.CalculatedAmount = (System.Decimal?)value;
							break;
						case "LastCalculatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastCalculatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
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
						case "ParamedicFeeByServiceSettingID":
						
							if (value == null || value is System.Int32)
								this.ParamedicFeeByServiceSettingID = (System.Int32?)value;
							break;
						case "IsWriteOff":
						
							if (value == null || value is System.Boolean)
								this.IsWriteOff = (System.Boolean?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "FeeAmountBruto":
						
							if (value == null || value is System.Decimal)
								this.FeeAmountBruto = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.DischargeDate
		/// </summary>
		virtual public System.DateTime? DischargeDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Discount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.FeeAmount
		/// </summary>
		virtual public System.Decimal? FeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.IsCalculatedInPercent
		/// </summary>
		virtual public System.Boolean? IsCalculatedInPercent
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsCalculatedInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsCalculatedInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.CalculatedAmount
		/// </summary>
		virtual public System.Decimal? CalculatedAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CalculatedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CalculatedAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.LastCalculatedDateTime
		/// </summary>
		virtual public System.DateTime? LastCalculatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.LastCalculatedByUserID
		/// </summary>
		virtual public System.String LastCalculatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.JournalId, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.PriceItem
		/// </summary>
		virtual public System.Decimal? PriceItem
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PriceItem);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PriceItem, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.DiscountItem
		/// </summary>
		virtual public System.Decimal? DiscountItem
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DiscountItem);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DiscountItem, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.TransactionNoRef
		/// </summary>
		virtual public System.String TransactionNoRef
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNoRef);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNoRef, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.RegistrationNoMergeTo
		/// </summary>
		virtual public System.String RegistrationNoMergeTo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNoMergeTo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNoMergeTo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.DischargeDateMergeTo
		/// </summary>
		virtual public System.DateTime? DischargeDateMergeTo
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDateMergeTo);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDateMergeTo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.ParamedicFeeByServiceSettingID
		/// </summary>
		virtual public System.Int32? ParamedicFeeByServiceSettingID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicFeeByServiceSettingID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicFeeByServiceSettingID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.IsWriteOff
		/// </summary>
		virtual public System.Boolean? IsWriteOff
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsWriteOff);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsWriteOff, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.InvoiceWriteOffNo
		/// </summary>
		virtual public System.String InvoiceWriteOffNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.InvoiceWriteOffNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.InvoiceWriteOffNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.ChangeNote
		/// </summary>
		virtual public System.String ChangeNote
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ChangeNote);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ChangeNote, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PaymentGroupNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransChargesItemCompByTeam.FeeAmountBruto
		/// </summary>
		virtual public System.Decimal? FeeAmountBruto
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmountBruto);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmountBruto, value);
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
			public esStrings(esParamedicFeeTransChargesItemCompByTeam entity)
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
			private esParamedicFeeTransChargesItemCompByTeam entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeTransChargesItemCompByTeamQuery query)
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
				throw new Exception("esParamedicFeeTransChargesItemCompByTeam can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeTransChargesItemCompByTeam : esParamedicFeeTransChargesItemCompByTeam
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeTransChargesItemCompByTeamQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompByTeamMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsCalculatedInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsCalculatedInPercent, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CalculatedAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CalculatedAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastCalculatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastCalculatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PriceItem
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PriceItem, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DiscountItem
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DiscountItem, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TransactionNoRef
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNoRef, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNoMergeTo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNoMergeTo, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDateMergeTo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDateMergeTo, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicFeeByServiceSettingID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicFeeByServiceSettingID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsWriteOff
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsWriteOff, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem InvoiceWriteOffNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.InvoiceWriteOffNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ChangeNote
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ChangeNote, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem FeeAmountBruto
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmountBruto, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeTransChargesItemCompByTeamCollection")]
	public partial class ParamedicFeeTransChargesItemCompByTeamCollection : esParamedicFeeTransChargesItemCompByTeamCollection, IEnumerable< ParamedicFeeTransChargesItemCompByTeam>
	{
		public ParamedicFeeTransChargesItemCompByTeamCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeTransChargesItemCompByTeam>(ParamedicFeeTransChargesItemCompByTeamCollection coll)
		{
			List< ParamedicFeeTransChargesItemCompByTeam> list = new List< ParamedicFeeTransChargesItemCompByTeam>();
			
			foreach (ParamedicFeeTransChargesItemCompByTeam emp in coll)
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
				return  ParamedicFeeTransChargesItemCompByTeamMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompByTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeTransChargesItemCompByTeam(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeTransChargesItemCompByTeam();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompByTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompByTeamQuery();
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
		public bool Load(ParamedicFeeTransChargesItemCompByTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeTransChargesItemCompByTeam AddNew()
		{
			ParamedicFeeTransChargesItemCompByTeam entity = base.AddNewEntity() as ParamedicFeeTransChargesItemCompByTeam;
			
			return entity;		
		}
		public ParamedicFeeTransChargesItemCompByTeam FindByPrimaryKey(String transactionNo, String sequenceNo, String tariffComponentID, String paramedicID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, tariffComponentID, paramedicID) as ParamedicFeeTransChargesItemCompByTeam;
		}

		#region IEnumerable< ParamedicFeeTransChargesItemCompByTeam> Members

		IEnumerator< ParamedicFeeTransChargesItemCompByTeam> IEnumerable< ParamedicFeeTransChargesItemCompByTeam>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeTransChargesItemCompByTeam;
			}
		}

		#endregion
		
		private ParamedicFeeTransChargesItemCompByTeamQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeTransChargesItemCompByTeam' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeTransChargesItemCompByTeam ({TransactionNo, SequenceNo, TariffComponentID, ParamedicID})")]
	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompByTeam : esParamedicFeeTransChargesItemCompByTeam
	{
		public ParamedicFeeTransChargesItemCompByTeam()
		{
		}	
	
		public ParamedicFeeTransChargesItemCompByTeam(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransChargesItemCompByTeamMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeTransChargesItemCompByTeamQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransChargesItemCompByTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeTransChargesItemCompByTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransChargesItemCompByTeamQuery();
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
		public bool Load(ParamedicFeeTransChargesItemCompByTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeTransChargesItemCompByTeamQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompByTeamQuery : esParamedicFeeTransChargesItemCompByTeamQuery
	{
		public ParamedicFeeTransChargesItemCompByTeamQuery()
		{

		}		
		
		public ParamedicFeeTransChargesItemCompByTeamQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeTransChargesItemCompByTeamQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeTransChargesItemCompByTeamMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeTransChargesItemCompByTeamMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.DischargeDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ItemID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Qty, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Price, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Discount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.FeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsCalculatedInPercent, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.IsCalculatedInPercent;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CalculatedAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.CalculatedAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.LastCalculatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastCalculatedByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.LastCalculatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.VerificationNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.JournalId, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PriceItem, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.PriceItem;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DiscountItem, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.DiscountItem;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.TransactionNoRef, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.TransactionNoRef;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNo, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.RegistrationNoMergeTo, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.RegistrationNoMergeTo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.DischargeDateMergeTo, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.DischargeDateMergeTo;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.Notes, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ParamedicFeeByServiceSettingID, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.ParamedicFeeByServiceSettingID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.IsWriteOff, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.IsWriteOff;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.InvoiceWriteOffNo, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.InvoiceWriteOffNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.ChangeNote, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.ChangeNote;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.PaymentGroupNo, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.PaymentGroupNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateDateTime, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.CreateByUserID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransChargesItemCompByTeamMetadata.ColumnNames.FeeAmountBruto, 32, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransChargesItemCompByTeamMetadata.PropertyNames.FeeAmountBruto;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeTransChargesItemCompByTeamMetadata Meta()
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
			public const string ParamedicID = "ParamedicID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string Discount = "Discount";
			public const string FeeAmount = "FeeAmount";
			public const string IsCalculatedInPercent = "IsCalculatedInPercent";
			public const string CalculatedAmount = "CalculatedAmount";
			public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			public const string VerificationNo = "VerificationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string JournalId = "JournalId";
			public const string PriceItem = "PriceItem";
			public const string DiscountItem = "DiscountItem";
			public const string TransactionNoRef = "TransactionNoRef";
			public const string RegistrationNo = "RegistrationNo";
			public const string RegistrationNoMergeTo = "RegistrationNoMergeTo";
			public const string DischargeDateMergeTo = "DischargeDateMergeTo";
			public const string Notes = "Notes";
			public const string ParamedicFeeByServiceSettingID = "ParamedicFeeByServiceSettingID";
			public const string IsWriteOff = "IsWriteOff";
			public const string InvoiceWriteOffNo = "InvoiceWriteOffNo";
			public const string ChangeNote = "ChangeNote";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string FeeAmountBruto = "FeeAmountBruto";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string DischargeDate = "DischargeDate";
			public const string ParamedicID = "ParamedicID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string Discount = "Discount";
			public const string FeeAmount = "FeeAmount";
			public const string IsCalculatedInPercent = "IsCalculatedInPercent";
			public const string CalculatedAmount = "CalculatedAmount";
			public const string LastCalculatedDateTime = "LastCalculatedDateTime";
			public const string LastCalculatedByUserID = "LastCalculatedByUserID";
			public const string VerificationNo = "VerificationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string JournalId = "JournalId";
			public const string PriceItem = "PriceItem";
			public const string DiscountItem = "DiscountItem";
			public const string TransactionNoRef = "TransactionNoRef";
			public const string RegistrationNo = "RegistrationNo";
			public const string RegistrationNoMergeTo = "RegistrationNoMergeTo";
			public const string DischargeDateMergeTo = "DischargeDateMergeTo";
			public const string Notes = "Notes";
			public const string ParamedicFeeByServiceSettingID = "ParamedicFeeByServiceSettingID";
			public const string IsWriteOff = "IsWriteOff";
			public const string InvoiceWriteOffNo = "InvoiceWriteOffNo";
			public const string ChangeNote = "ChangeNote";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string FeeAmountBruto = "FeeAmountBruto";
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
			lock (typeof(ParamedicFeeTransChargesItemCompByTeamMetadata))
			{
				if(ParamedicFeeTransChargesItemCompByTeamMetadata.mapDelegates == null)
				{
					ParamedicFeeTransChargesItemCompByTeamMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeTransChargesItemCompByTeamMetadata.meta == null)
				{
					ParamedicFeeTransChargesItemCompByTeamMetadata.meta = new ParamedicFeeTransChargesItemCompByTeamMetadata();
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
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsCalculatedInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CalculatedAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastCalculatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCalculatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PriceItem", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountItem", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TransactionNoRef", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNoMergeTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDateMergeTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicFeeByServiceSettingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsWriteOff", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InvoiceWriteOffNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChangeNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FeeAmountBruto", new esTypeMap("decimal", "System.Decimal"));
		

				meta.Source = "ParamedicFeeTransChargesItemCompByTeam";
				meta.Destination = "ParamedicFeeTransChargesItemCompByTeam";
				meta.spInsert = "proc_ParamedicFeeTransChargesItemCompByTeamInsert";				
				meta.spUpdate = "proc_ParamedicFeeTransChargesItemCompByTeamUpdate";		
				meta.spDelete = "proc_ParamedicFeeTransChargesItemCompByTeamDelete";
				meta.spLoadAll = "proc_ParamedicFeeTransChargesItemCompByTeamLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeTransChargesItemCompByTeamLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeTransChargesItemCompByTeamMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
