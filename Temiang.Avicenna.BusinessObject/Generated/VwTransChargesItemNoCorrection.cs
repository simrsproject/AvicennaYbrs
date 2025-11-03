/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/17/2012 5:18:15 PM
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
	abstract public class esVwTransChargesItemNoCorrectionCollection : esEntityCollectionWAuditLog
	{
		public esVwTransChargesItemNoCorrectionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwTransChargesItemNoCorrectionCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwTransChargesItemNoCorrectionQuery query)
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
			this.InitQuery(query as esVwTransChargesItemNoCorrectionQuery);
		}
		#endregion
		
		virtual public VwTransChargesItemNoCorrection DetachEntity(VwTransChargesItemNoCorrection entity)
		{
			return base.DetachEntity(entity) as VwTransChargesItemNoCorrection;
		}
		
		virtual public VwTransChargesItemNoCorrection AttachEntity(VwTransChargesItemNoCorrection entity)
		{
			return base.AttachEntity(entity) as VwTransChargesItemNoCorrection;
		}
		
		virtual public void Combine(VwTransChargesItemNoCorrectionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwTransChargesItemNoCorrection this[int index]
		{
			get
			{
				return base[index] as VwTransChargesItemNoCorrection;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwTransChargesItemNoCorrection);
		}
	}



	[Serializable]
	abstract public class esVwTransChargesItemNoCorrection : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwTransChargesItemNoCorrectionQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwTransChargesItemNoCorrection()
		{

		}

		public esVwTransChargesItemNoCorrection(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
		/// Maps to vw_TransChargesItemNoCorrection.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceSequenceNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceSequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.ChargeClassID
		/// </summary>
		virtual public System.String ChargeClassID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeClassID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.SecondParamedicID
		/// </summary>
		virtual public System.String SecondParamedicID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SecondParamedicID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SecondParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsAdminCalculation
		/// </summary>
		virtual public System.Boolean? IsAdminCalculation
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAdminCalculation);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAdminCalculation, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsVariable
		/// </summary>
		virtual public System.Boolean? IsVariable
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVariable);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVariable, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsCito
		/// </summary>
		virtual public System.Boolean? IsCito
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsCito);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsCito, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.ChargeQuantity
		/// </summary>
		virtual public System.Decimal? ChargeQuantity
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeQuantity);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeQuantity, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.StockQuantity
		/// </summary>
		virtual public System.Decimal? StockQuantity
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.StockQuantity);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.StockQuantity, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.CostPrice);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.CostPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.CitoAmount
		/// </summary>
		virtual public System.Decimal? CitoAmount
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.CitoAmount);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.CitoAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.RoundingAmount
		/// </summary>
		virtual public System.Decimal? RoundingAmount
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.RoundingAmount);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.RoundingAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRDiscountReason);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsAssetUtilization
		/// </summary>
		virtual public System.Boolean? IsAssetUtilization
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAssetUtilization);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAssetUtilization, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsBillProceed);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsOrderRealization
		/// </summary>
		virtual public System.Boolean? IsOrderRealization
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsOrderRealization);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsOrderRealization, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsPackage, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsApprove, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.FilmNo
		/// </summary>
		virtual public System.String FilmNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.FilmNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.FilmNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParentNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.SRCenterID
		/// </summary>
		virtual public System.String SRCenterID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRCenterID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRCenterID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemNoCorrection.AutoProcessCalculation
		/// </summary>
		virtual public System.Decimal? AutoProcessCalculation
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.AutoProcessCalculation);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemNoCorrectionMetadata.ColumnNames.AutoProcessCalculation, value);
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
			public esStrings(esVwTransChargesItemNoCorrection entity)
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
			

			private esVwTransChargesItemNoCorrection entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwTransChargesItemNoCorrectionQuery query)
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
				throw new Exception("esVwTransChargesItemNoCorrection can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwTransChargesItemNoCorrectionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesItemNoCorrectionMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ChargeClassID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SecondParamedicID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.SecondParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsAdminCalculation
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVariable
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVariable, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCito
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsCito, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ChargeQuantity
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeQuantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem StockQuantity
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.StockQuantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CitoAmount
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.CitoAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem RoundingAmount
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.RoundingAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		} 
		
		public esQueryItem IsAssetUtilization
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAssetUtilization, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsOrderRealization
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsOrderRealization, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem FilmNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.FilmNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCenterID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRCenterID, esSystemType.String);
			}
		} 
		
		public esQueryItem AutoProcessCalculation
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemNoCorrectionMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwTransChargesItemNoCorrectionCollection")]
	public partial class VwTransChargesItemNoCorrectionCollection : esVwTransChargesItemNoCorrectionCollection, IEnumerable<VwTransChargesItemNoCorrection>
	{
		public VwTransChargesItemNoCorrectionCollection()
		{

		}
		
		public static implicit operator List<VwTransChargesItemNoCorrection>(VwTransChargesItemNoCorrectionCollection coll)
		{
			List<VwTransChargesItemNoCorrection> list = new List<VwTransChargesItemNoCorrection>();
			
			foreach (VwTransChargesItemNoCorrection emp in coll)
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
				return  VwTransChargesItemNoCorrectionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesItemNoCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwTransChargesItemNoCorrection(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwTransChargesItemNoCorrection();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwTransChargesItemNoCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesItemNoCorrectionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwTransChargesItemNoCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwTransChargesItemNoCorrection AddNew()
		{
			VwTransChargesItemNoCorrection entity = base.AddNewEntity() as VwTransChargesItemNoCorrection;
			
			return entity;
		}


		#region IEnumerable<VwTransChargesItemNoCorrection> Members

		IEnumerator<VwTransChargesItemNoCorrection> IEnumerable<VwTransChargesItemNoCorrection>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwTransChargesItemNoCorrection;
			}
		}

		#endregion
		
		private VwTransChargesItemNoCorrectionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_TransChargesItemNoCorrection' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwTransChargesItemNoCorrection ()")]
	[Serializable]
	public partial class VwTransChargesItemNoCorrection : esVwTransChargesItemNoCorrection
	{
		public VwTransChargesItemNoCorrection()
		{

		}
	
		public VwTransChargesItemNoCorrection(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesItemNoCorrectionMetadata.Meta();
			}
		}
		
		
		
		override protected esVwTransChargesItemNoCorrectionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesItemNoCorrectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwTransChargesItemNoCorrectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesItemNoCorrectionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwTransChargesItemNoCorrectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwTransChargesItemNoCorrectionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwTransChargesItemNoCorrectionQuery : esVwTransChargesItemNoCorrectionQuery
	{
		public VwTransChargesItemNoCorrectionQuery()
		{

		}		
		
		public VwTransChargesItemNoCorrectionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwTransChargesItemNoCorrectionQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwTransChargesItemNoCorrectionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwTransChargesItemNoCorrectionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ReferenceSequenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ItemID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeClassID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.ChargeClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SecondParamedicID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.SecondParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAdminCalculation, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsAdminCalculation;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVariable, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsVariable;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsCito, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsCito;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ChargeQuantity, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.ChargeQuantity;
			c.NumericPrecision = 38;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.StockQuantity, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.StockQuantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRItemUnit, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.CostPrice, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.Price, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.DiscountAmount, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.CitoAmount, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.CitoAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.RoundingAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.RoundingAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRDiscountReason, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsAssetUtilization, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsAssetUtilization;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.AssetID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsBillProceed, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsBillProceed;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsOrderRealization, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsOrderRealization;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsPackage, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsPackage;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsApprove, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsApprove;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.IsVoid, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.IsVoid;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.Notes, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.FilmNo, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.FilmNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.LastUpdateByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.ParentNo, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 7;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.SRCenterID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.SRCenterID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemNoCorrectionMetadata.ColumnNames.AutoProcessCalculation, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemNoCorrectionMetadata.PropertyNames.AutoProcessCalculation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwTransChargesItemNoCorrectionMetadata Meta()
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
			lock (typeof(VwTransChargesItemNoCorrectionMetadata))
			{
				if(VwTransChargesItemNoCorrectionMetadata.mapDelegates == null)
				{
					VwTransChargesItemNoCorrectionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwTransChargesItemNoCorrectionMetadata.meta == null)
				{
					VwTransChargesItemNoCorrectionMetadata.meta = new VwTransChargesItemNoCorrectionMetadata();
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
				
				
				
				meta.Source = "vw_TransChargesItemNoCorrection";
				meta.Destination = "vw_TransChargesItemNoCorrection";
				
				meta.spInsert = "proc_vw_TransChargesItemNoCorrectionInsert";				
				meta.spUpdate = "proc_vw_TransChargesItemNoCorrectionUpdate";		
				meta.spDelete = "proc_vw_TransChargesItemNoCorrectionDelete";
				meta.spLoadAll = "proc_vw_TransChargesItemNoCorrectionLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_TransChargesItemNoCorrectionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwTransChargesItemNoCorrectionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
