/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2012 12:50:44 PM
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
	abstract public class esTransChargesItemHistoryCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesItemHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransChargesItemHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesItemHistoryQuery query)
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
			this.InitQuery(query as esTransChargesItemHistoryQuery);
		}
		#endregion
		
		virtual public TransChargesItemHistory DetachEntity(TransChargesItemHistory entity)
		{
			return base.DetachEntity(entity) as TransChargesItemHistory;
		}
		
		virtual public TransChargesItemHistory AttachEntity(TransChargesItemHistory entity)
		{
			return base.AttachEntity(entity) as TransChargesItemHistory;
		}
		
		virtual public void Combine(TransChargesItemHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransChargesItemHistory this[int index]
		{
			get
			{
				return base[index] as TransChargesItemHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesItemHistory);
		}
	}



	[Serializable]
	abstract public class esTransChargesItemHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesItemHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesItemHistory()
		{

		}

		public esTransChargesItemHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, transactionNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo)
		{
			esTransChargesItemHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RecalculationProcessNo == recalculationProcessNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RecalculationProcessNo",recalculationProcessNo);			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "RecalculationProcessNo": this.str.RecalculationProcessNo = (string)value; break;							
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
		/// Maps to TransChargesItemHistory.RecalculationProcessNo
		/// </summary>
		virtual public System.String RecalculationProcessNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.RecalculationProcessNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.RecalculationProcessNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ReferenceSequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ReferenceSequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.ChargeClassID
		/// </summary>
		virtual public System.String ChargeClassID
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ChargeClassID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ChargeClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.SecondParamedicID
		/// </summary>
		virtual public System.String SecondParamedicID
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SecondParamedicID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SecondParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsAdminCalculation
		/// </summary>
		virtual public System.Boolean? IsAdminCalculation
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsAdminCalculation);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsAdminCalculation, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsVariable
		/// </summary>
		virtual public System.Boolean? IsVariable
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsVariable);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsVariable, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsCito
		/// </summary>
		virtual public System.Boolean? IsCito
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsCito);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsCito, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.ChargeQuantity
		/// </summary>
		virtual public System.Decimal? ChargeQuantity
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.ChargeQuantity);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.ChargeQuantity, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.StockQuantity
		/// </summary>
		virtual public System.Decimal? StockQuantity
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.StockQuantity);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.StockQuantity, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.CostPrice);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.CostPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.CitoAmount
		/// </summary>
		virtual public System.Decimal? CitoAmount
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.CitoAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.CitoAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.RoundingAmount
		/// </summary>
		virtual public System.Decimal? RoundingAmount
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.RoundingAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.RoundingAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SRDiscountReason);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsAssetUtilization
		/// </summary>
		virtual public System.Boolean? IsAssetUtilization
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsAssetUtilization);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsAssetUtilization, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsBillProceed);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsOrderRealization
		/// </summary>
		virtual public System.Boolean? IsOrderRealization
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsOrderRealization);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsOrderRealization, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsPackage, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsApprove, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(TransChargesItemHistoryMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.FilmNo
		/// </summary>
		virtual public System.String FilmNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.FilmNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.FilmNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesItemHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransChargesItemHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ParentNo);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.ParentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.SRCenterID
		/// </summary>
		virtual public System.String SRCenterID
		{
			get
			{
				return base.GetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SRCenterID);
			}
			
			set
			{
				base.SetSystemString(TransChargesItemHistoryMetadata.ColumnNames.SRCenterID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransChargesItemHistory.AutoProcessCalculation
		/// </summary>
		virtual public System.Decimal? AutoProcessCalculation
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.AutoProcessCalculation);
			}
			
			set
			{
				base.SetSystemDecimal(TransChargesItemHistoryMetadata.ColumnNames.AutoProcessCalculation, value);
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
			public esStrings(esTransChargesItemHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RecalculationProcessNo
			{
				get
				{
					System.String data = entity.RecalculationProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecalculationProcessNo = null;
					else entity.RecalculationProcessNo = Convert.ToString(value);
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
			

			private esTransChargesItemHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesItemHistoryQuery query)
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
				throw new Exception("esTransChargesItemHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransChargesItemHistory : esTransChargesItemHistory
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esTransChargesItemHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RecalculationProcessNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.RecalculationProcessNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChargeClassID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.ChargeClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SecondParamedicID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.SecondParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsAdminCalculation
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVariable
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsVariable, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCito
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsCito, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ChargeQuantity
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.ChargeQuantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem StockQuantity
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.StockQuantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CitoAmount
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.CitoAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem RoundingAmount
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.RoundingAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		} 
		
		public esQueryItem IsAssetUtilization
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsAssetUtilization, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsOrderRealization
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsOrderRealization, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem FilmNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.FilmNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCenterID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.SRCenterID, esSystemType.String);
			}
		} 
		
		public esQueryItem AutoProcessCalculation
		{
			get
			{
				return new esQueryItem(this, TransChargesItemHistoryMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesItemHistoryCollection")]
	public partial class TransChargesItemHistoryCollection : esTransChargesItemHistoryCollection, IEnumerable<TransChargesItemHistory>
	{
		public TransChargesItemHistoryCollection()
		{

		}
		
		public static implicit operator List<TransChargesItemHistory>(TransChargesItemHistoryCollection coll)
		{
			List<TransChargesItemHistory> list = new List<TransChargesItemHistory>();
			
			foreach (TransChargesItemHistory emp in coll)
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
				return  TransChargesItemHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesItemHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesItemHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransChargesItemHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransChargesItemHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransChargesItemHistory AddNew()
		{
			TransChargesItemHistory entity = base.AddNewEntity() as TransChargesItemHistory;
			
			return entity;
		}

		public TransChargesItemHistory FindByPrimaryKey(System.String recalculationProcessNo, System.String transactionNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(recalculationProcessNo, transactionNo, sequenceNo) as TransChargesItemHistory;
		}


		#region IEnumerable<TransChargesItemHistory> Members

		IEnumerator<TransChargesItemHistory> IEnumerable<TransChargesItemHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesItemHistory;
			}
		}

		#endregion
		
		private TransChargesItemHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesItemHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransChargesItemHistory ({RecalculationProcessNo},{TransactionNo},{SequenceNo})")]
	[Serializable]
	public partial class TransChargesItemHistory : esTransChargesItemHistory
	{
		public TransChargesItemHistory()
		{

		}
	
		public TransChargesItemHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esTransChargesItemHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransChargesItemHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransChargesItemHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransChargesItemHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransChargesItemHistoryQuery : esTransChargesItemHistoryQuery
	{
		public TransChargesItemHistoryQuery()
		{

		}		
		
		public TransChargesItemHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransChargesItemHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransChargesItemHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesItemHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.RecalculationProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.RecalculationProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.ReferenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.ReferenceSequenceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.ItemID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.ChargeClassID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.ChargeClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.ParamedicID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.SecondParamedicID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.SecondParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsAdminCalculation, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsAdminCalculation;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsVariable, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsVariable;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsCito, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsCito;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.ChargeQuantity, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.ChargeQuantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.StockQuantity, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.StockQuantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.SRItemUnit, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.CostPrice, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.Price, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.DiscountAmount, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.CitoAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.CitoAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.RoundingAmount, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.RoundingAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.SRDiscountReason, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsAssetUtilization, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsAssetUtilization;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.AssetID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsBillProceed, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsBillProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsOrderRealization, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsOrderRealization;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsPackage, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsPackage;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsApprove, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsApprove;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.IsVoid, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.Notes, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.FilmNo, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.FilmNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.LastUpdateDateTime, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.LastUpdateByUserID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.ParentNo, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 7;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.SRCenterID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.SRCenterID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransChargesItemHistoryMetadata.ColumnNames.AutoProcessCalculation, 34, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemHistoryMetadata.PropertyNames.AutoProcessCalculation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransChargesItemHistoryMetadata Meta()
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
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
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
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
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
			lock (typeof(TransChargesItemHistoryMetadata))
			{
				if(TransChargesItemHistoryMetadata.mapDelegates == null)
				{
					TransChargesItemHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransChargesItemHistoryMetadata.meta == null)
				{
					TransChargesItemHistoryMetadata.meta = new TransChargesItemHistoryMetadata();
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
				

				meta.AddTypeMap("RecalculationProcessNo", new esTypeMap("varchar", "System.String"));
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
				
				
				
				meta.Source = "TransChargesItemHistory";
				meta.Destination = "TransChargesItemHistory";
				
				meta.spInsert = "proc_TransChargesItemHistoryInsert";				
				meta.spUpdate = "proc_TransChargesItemHistoryUpdate";		
				meta.spDelete = "proc_TransChargesItemHistoryDelete";
				meta.spLoadAll = "proc_TransChargesItemHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesItemHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesItemHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
