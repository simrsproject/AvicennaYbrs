/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/25/2023 10:55:28 AM
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
	abstract public class esItemTransactionItemCollection : esEntityCollectionWAuditLog
	{
		public esItemTransactionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTransactionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTransactionItemQuery query)
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
			this.InitQuery(query as esItemTransactionItemQuery);
		}
		#endregion

		virtual public ItemTransactionItem DetachEntity(ItemTransactionItem entity)
		{
			return base.DetachEntity(entity) as ItemTransactionItem;
		}

		virtual public ItemTransactionItem AttachEntity(ItemTransactionItem entity)
		{
			return base.AttachEntity(entity) as ItemTransactionItem;
		}

		virtual public void Combine(ItemTransactionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemTransactionItem this[int index]
		{
			get
			{
				return base[index] as ItemTransactionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTransactionItem);
		}
	}

	[Serializable]
	abstract public class esItemTransactionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTransactionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTransactionItem()
		{
		}

		public esItemTransactionItem(DataRow row)
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
			esItemTransactionItemQuery query = this.GetDynamicQuery();
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
						case "Quantity": this.str.Quantity = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
						case "QuantityFinishInBaseUnit": this.str.QuantityFinishInBaseUnit = (string)value; break;
						case "PageNo": this.str.PageNo = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "PriceInCurrency": this.str.PriceInCurrency = (string)value; break;
						case "Discount1Percentage": this.str.Discount1Percentage = (string)value; break;
						case "Discount2Percentage": this.str.Discount2Percentage = (string)value; break;
						case "BatchNumber": this.str.BatchNumber = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "IsPackage": this.str.IsPackage = (string)value; break;
						case "IsBonusItem": this.str.IsBonusItem = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "RequestQty": this.str.RequestQty = (string)value; break;
						case "Discount": this.str.Discount = (string)value; break;
						case "DiscountInCurrency": this.str.DiscountInCurrency = (string)value; break;
						case "IsDiscountInPercent": this.str.IsDiscountInPercent = (string)value; break;
						case "IsInvoiceUpdate": this.str.IsInvoiceUpdate = (string)value; break;
						case "PriorPrice": this.str.PriorPrice = (string)value; break;
						case "PriorPriceInCurrency": this.str.PriorPriceInCurrency = (string)value; break;
						case "PriorDiscount1Percentage": this.str.PriorDiscount1Percentage = (string)value; break;
						case "PriorDiscount2Percentage": this.str.PriorDiscount2Percentage = (string)value; break;
						case "PriorDiscount": this.str.PriorDiscount = (string)value; break;
						case "PriorDiscountInCurrency": this.str.PriorDiscountInCurrency = (string)value; break;
						case "LastInvoiceUpdateDateTime": this.str.LastInvoiceUpdateDateTime = (string)value; break;
						case "LastInvoiceUpdateByUserID": this.str.LastInvoiceUpdateByUserID = (string)value; break;
						case "HistoryPrice": this.str.HistoryPrice = (string)value; break;
						case "HistoryPriceInCurrency": this.str.HistoryPriceInCurrency = (string)value; break;
						case "HistoryDiscount1Percentage": this.str.HistoryDiscount1Percentage = (string)value; break;
						case "HistoryDiscount2Percentage": this.str.HistoryDiscount2Percentage = (string)value; break;
						case "HistoryDiscount": this.str.HistoryDiscount = (string)value; break;
						case "HistoryDiscountInCurrency": this.str.HistoryDiscountInCurrency = (string)value; break;
						case "Specification": this.str.Specification = (string)value; break;
						case "IsAccEd": this.str.IsAccEd = (string)value; break;
						case "IsAccPrice": this.str.IsAccPrice = (string)value; break;
						case "IsAccQty": this.str.IsAccQty = (string)value; break;
						case "IsTaxable": this.str.IsTaxable = (string)value; break;
						case "IsTaxablePph": this.str.IsTaxablePph = (string)value; break;
						case "SRPph": this.str.SRPph = (string)value; break;
						case "PphPercentage": this.str.PphPercentage = (string)value; break;
						case "PphAmount": this.str.PphAmount = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "FabricID": this.str.FabricID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Quantity":

							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						case "ConversionFactor":

							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
							break;
						case "QuantityFinishInBaseUnit":

							if (value == null || value is System.Decimal)
								this.QuantityFinishInBaseUnit = (System.Decimal?)value;
							break;
						case "PageNo":

							if (value == null || value is System.Int32)
								this.PageNo = (System.Int32?)value;
							break;
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "PriceInCurrency":

							if (value == null || value is System.Decimal)
								this.PriceInCurrency = (System.Decimal?)value;
							break;
						case "Discount1Percentage":

							if (value == null || value is System.Decimal)
								this.Discount1Percentage = (System.Decimal?)value;
							break;
						case "Discount2Percentage":

							if (value == null || value is System.Decimal)
								this.Discount2Percentage = (System.Decimal?)value;
							break;
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "IsPackage":

							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
							break;
						case "IsBonusItem":

							if (value == null || value is System.Boolean)
								this.IsBonusItem = (System.Boolean?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "RequestQty":

							if (value == null || value is System.Decimal)
								this.RequestQty = (System.Decimal?)value;
							break;
						case "Discount":

							if (value == null || value is System.Decimal)
								this.Discount = (System.Decimal?)value;
							break;
						case "DiscountInCurrency":

							if (value == null || value is System.Decimal)
								this.DiscountInCurrency = (System.Decimal?)value;
							break;
						case "IsDiscountInPercent":

							if (value == null || value is System.Boolean)
								this.IsDiscountInPercent = (System.Boolean?)value;
							break;
						case "IsInvoiceUpdate":

							if (value == null || value is System.Boolean)
								this.IsInvoiceUpdate = (System.Boolean?)value;
							break;
						case "PriorPrice":

							if (value == null || value is System.Decimal)
								this.PriorPrice = (System.Decimal?)value;
							break;
						case "PriorPriceInCurrency":

							if (value == null || value is System.Decimal)
								this.PriorPriceInCurrency = (System.Decimal?)value;
							break;
						case "PriorDiscount1Percentage":

							if (value == null || value is System.Decimal)
								this.PriorDiscount1Percentage = (System.Decimal?)value;
							break;
						case "PriorDiscount2Percentage":

							if (value == null || value is System.Decimal)
								this.PriorDiscount2Percentage = (System.Decimal?)value;
							break;
						case "PriorDiscount":

							if (value == null || value is System.Decimal)
								this.PriorDiscount = (System.Decimal?)value;
							break;
						case "PriorDiscountInCurrency":

							if (value == null || value is System.Decimal)
								this.PriorDiscountInCurrency = (System.Decimal?)value;
							break;
						case "LastInvoiceUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastInvoiceUpdateDateTime = (System.DateTime?)value;
							break;
						case "HistoryPrice":

							if (value == null || value is System.Decimal)
								this.HistoryPrice = (System.Decimal?)value;
							break;
						case "HistoryPriceInCurrency":

							if (value == null || value is System.Decimal)
								this.HistoryPriceInCurrency = (System.Decimal?)value;
							break;
						case "HistoryDiscount1Percentage":

							if (value == null || value is System.Decimal)
								this.HistoryDiscount1Percentage = (System.Decimal?)value;
							break;
						case "HistoryDiscount2Percentage":

							if (value == null || value is System.Decimal)
								this.HistoryDiscount2Percentage = (System.Decimal?)value;
							break;
						case "HistoryDiscount":

							if (value == null || value is System.Decimal)
								this.HistoryDiscount = (System.Decimal?)value;
							break;
						case "HistoryDiscountInCurrency":

							if (value == null || value is System.Decimal)
								this.HistoryDiscountInCurrency = (System.Decimal?)value;
							break;
						case "IsAccEd":

							if (value == null || value is System.Boolean)
								this.IsAccEd = (System.Boolean?)value;
							break;
						case "IsAccPrice":

							if (value == null || value is System.Boolean)
								this.IsAccPrice = (System.Boolean?)value;
							break;
						case "IsAccQty":

							if (value == null || value is System.Boolean)
								this.IsAccQty = (System.Boolean?)value;
							break;
						case "IsTaxable":

							if (value == null || value is System.Boolean)
								this.IsTaxable = (System.Boolean?)value;
							break;
						case "IsTaxablePph":

							if (value == null || value is System.Boolean)
								this.IsTaxablePph = (System.Boolean?)value;
							break;
						case "PphPercentage":

							if (value == null || value is System.Decimal)
								this.PphPercentage = (System.Decimal?)value;
							break;
						case "PphAmount":

							if (value == null || value is System.Decimal)
								this.PphAmount = (System.Decimal?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
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
		/// Maps to ItemTransactionItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Quantity);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Quantity, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.ConversionFactor);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.QuantityFinishInBaseUnit
		/// </summary>
		virtual public System.Decimal? QuantityFinishInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.QuantityFinishInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.QuantityFinishInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PageNo
		/// </summary>
		virtual public System.Int32? PageNo
		{
			get
			{
				return base.GetSystemInt32(ItemTransactionItemMetadata.ColumnNames.PageNo);
			}

			set
			{
				base.SetSystemInt32(ItemTransactionItemMetadata.ColumnNames.PageNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PriceInCurrency
		/// </summary>
		virtual public System.Decimal? PriceInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriceInCurrency);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriceInCurrency, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Discount1Percentage
		/// </summary>
		virtual public System.Decimal? Discount1Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Discount1Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Discount1Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Discount2Percentage
		/// </summary>
		virtual public System.Decimal? Discount2Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Discount2Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Discount2Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.BatchNumber
		/// </summary>
		virtual public System.String BatchNumber
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.BatchNumber);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.BatchNumber, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsPackage);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsPackage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsBonusItem
		/// </summary>
		virtual public System.Boolean? IsBonusItem
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsBonusItem);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsBonusItem, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.Description);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.RequestQty
		/// </summary>
		virtual public System.Decimal? RequestQty
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.RequestQty);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.RequestQty, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Discount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.Discount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.DiscountInCurrency
		/// </summary>
		virtual public System.Decimal? DiscountInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.DiscountInCurrency);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.DiscountInCurrency, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsDiscountInPercent
		/// </summary>
		virtual public System.Boolean? IsDiscountInPercent
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsInvoiceUpdate
		/// </summary>
		virtual public System.Boolean? IsInvoiceUpdate
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsInvoiceUpdate);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsInvoiceUpdate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PriorPrice
		/// </summary>
		virtual public System.Decimal? PriorPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PriorPriceInCurrency
		/// </summary>
		virtual public System.Decimal? PriorPriceInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorPriceInCurrency);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorPriceInCurrency, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PriorDiscount1Percentage
		/// </summary>
		virtual public System.Decimal? PriorDiscount1Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscount1Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscount1Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PriorDiscount2Percentage
		/// </summary>
		virtual public System.Decimal? PriorDiscount2Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscount2Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscount2Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PriorDiscount
		/// </summary>
		virtual public System.Decimal? PriorDiscount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PriorDiscountInCurrency
		/// </summary>
		virtual public System.Decimal? PriorDiscountInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscountInCurrency);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PriorDiscountInCurrency, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.LastInvoiceUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastInvoiceUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.LastInvoiceUpdateByUserID
		/// </summary>
		virtual public System.String LastInvoiceUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.HistoryPrice
		/// </summary>
		virtual public System.Decimal? HistoryPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.HistoryPriceInCurrency
		/// </summary>
		virtual public System.Decimal? HistoryPriceInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryPriceInCurrency);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryPriceInCurrency, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.HistoryDiscount1Percentage
		/// </summary>
		virtual public System.Decimal? HistoryDiscount1Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount1Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount1Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.HistoryDiscount2Percentage
		/// </summary>
		virtual public System.Decimal? HistoryDiscount2Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount2Percentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount2Percentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.HistoryDiscount
		/// </summary>
		virtual public System.Decimal? HistoryDiscount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.HistoryDiscountInCurrency
		/// </summary>
		virtual public System.Decimal? HistoryDiscountInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscountInCurrency);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.HistoryDiscountInCurrency, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Specification
		/// </summary>
		virtual public System.String Specification
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.Specification);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.Specification, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsAccEd
		/// </summary>
		virtual public System.Boolean? IsAccEd
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsAccEd);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsAccEd, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsAccPrice
		/// </summary>
		virtual public System.Boolean? IsAccPrice
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsAccPrice);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsAccPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsAccQty
		/// </summary>
		virtual public System.Boolean? IsAccQty
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsAccQty);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsAccQty, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsTaxable
		/// </summary>
		virtual public System.Boolean? IsTaxable
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsTaxable);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsTaxable, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.IsTaxablePph
		/// </summary>
		virtual public System.Boolean? IsTaxablePph
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsTaxablePph);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemMetadata.ColumnNames.IsTaxablePph, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.SRPph
		/// </summary>
		virtual public System.String SRPph
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.SRPph);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.SRPph, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PphPercentage
		/// </summary>
		virtual public System.Decimal? PphPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PphPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PphPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.PphAmount
		/// </summary>
		virtual public System.Decimal? PphAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PphAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemMetadata.ColumnNames.PphAmount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionItemMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItem.FabricID
		/// </summary>
		virtual public System.String FabricID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemMetadata.ColumnNames.FabricID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemMetadata.ColumnNames.FabricID, value);
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
			public esStrings(esItemTransactionItem entity)
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
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
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
			public System.String ConversionFactor
			{
				get
				{
					System.Decimal? data = entity.ConversionFactor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConversionFactor = null;
					else entity.ConversionFactor = Convert.ToDecimal(value);
				}
			}
			public System.String QuantityFinishInBaseUnit
			{
				get
				{
					System.Decimal? data = entity.QuantityFinishInBaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityFinishInBaseUnit = null;
					else entity.QuantityFinishInBaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String PageNo
			{
				get
				{
					System.Int32? data = entity.PageNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PageNo = null;
					else entity.PageNo = Convert.ToInt32(value);
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
			public System.String PriceInCurrency
			{
				get
				{
					System.Decimal? data = entity.PriceInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInCurrency = null;
					else entity.PriceInCurrency = Convert.ToDecimal(value);
				}
			}
			public System.String Discount1Percentage
			{
				get
				{
					System.Decimal? data = entity.Discount1Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount1Percentage = null;
					else entity.Discount1Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String Discount2Percentage
			{
				get
				{
					System.Decimal? data = entity.Discount2Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount2Percentage = null;
					else entity.Discount2Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String BatchNumber
			{
				get
				{
					System.String data = entity.BatchNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BatchNumber = null;
					else entity.BatchNumber = Convert.ToString(value);
				}
			}
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
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
			public System.String IsBonusItem
			{
				get
				{
					System.Boolean? data = entity.IsBonusItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBonusItem = null;
					else entity.IsBonusItem = Convert.ToBoolean(value);
				}
			}
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
			public System.String RequestQty
			{
				get
				{
					System.Decimal? data = entity.RequestQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestQty = null;
					else entity.RequestQty = Convert.ToDecimal(value);
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
			public System.String DiscountInCurrency
			{
				get
				{
					System.Decimal? data = entity.DiscountInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountInCurrency = null;
					else entity.DiscountInCurrency = Convert.ToDecimal(value);
				}
			}
			public System.String IsDiscountInPercent
			{
				get
				{
					System.Boolean? data = entity.IsDiscountInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscountInPercent = null;
					else entity.IsDiscountInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String IsInvoiceUpdate
			{
				get
				{
					System.Boolean? data = entity.IsInvoiceUpdate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInvoiceUpdate = null;
					else entity.IsInvoiceUpdate = Convert.ToBoolean(value);
				}
			}
			public System.String PriorPrice
			{
				get
				{
					System.Decimal? data = entity.PriorPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorPrice = null;
					else entity.PriorPrice = Convert.ToDecimal(value);
				}
			}
			public System.String PriorPriceInCurrency
			{
				get
				{
					System.Decimal? data = entity.PriorPriceInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorPriceInCurrency = null;
					else entity.PriorPriceInCurrency = Convert.ToDecimal(value);
				}
			}
			public System.String PriorDiscount1Percentage
			{
				get
				{
					System.Decimal? data = entity.PriorDiscount1Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscount1Percentage = null;
					else entity.PriorDiscount1Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String PriorDiscount2Percentage
			{
				get
				{
					System.Decimal? data = entity.PriorDiscount2Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscount2Percentage = null;
					else entity.PriorDiscount2Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String PriorDiscount
			{
				get
				{
					System.Decimal? data = entity.PriorDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscount = null;
					else entity.PriorDiscount = Convert.ToDecimal(value);
				}
			}
			public System.String PriorDiscountInCurrency
			{
				get
				{
					System.Decimal? data = entity.PriorDiscountInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscountInCurrency = null;
					else entity.PriorDiscountInCurrency = Convert.ToDecimal(value);
				}
			}
			public System.String LastInvoiceUpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastInvoiceUpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastInvoiceUpdateDateTime = null;
					else entity.LastInvoiceUpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastInvoiceUpdateByUserID
			{
				get
				{
					System.String data = entity.LastInvoiceUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastInvoiceUpdateByUserID = null;
					else entity.LastInvoiceUpdateByUserID = Convert.ToString(value);
				}
			}
			public System.String HistoryPrice
			{
				get
				{
					System.Decimal? data = entity.HistoryPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryPrice = null;
					else entity.HistoryPrice = Convert.ToDecimal(value);
				}
			}
			public System.String HistoryPriceInCurrency
			{
				get
				{
					System.Decimal? data = entity.HistoryPriceInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryPriceInCurrency = null;
					else entity.HistoryPriceInCurrency = Convert.ToDecimal(value);
				}
			}
			public System.String HistoryDiscount1Percentage
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscount1Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscount1Percentage = null;
					else entity.HistoryDiscount1Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String HistoryDiscount2Percentage
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscount2Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscount2Percentage = null;
					else entity.HistoryDiscount2Percentage = Convert.ToDecimal(value);
				}
			}
			public System.String HistoryDiscount
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscount = null;
					else entity.HistoryDiscount = Convert.ToDecimal(value);
				}
			}
			public System.String HistoryDiscountInCurrency
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscountInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscountInCurrency = null;
					else entity.HistoryDiscountInCurrency = Convert.ToDecimal(value);
				}
			}
			public System.String Specification
			{
				get
				{
					System.String data = entity.Specification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Specification = null;
					else entity.Specification = Convert.ToString(value);
				}
			}
			public System.String IsAccEd
			{
				get
				{
					System.Boolean? data = entity.IsAccEd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAccEd = null;
					else entity.IsAccEd = Convert.ToBoolean(value);
				}
			}
			public System.String IsAccPrice
			{
				get
				{
					System.Boolean? data = entity.IsAccPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAccPrice = null;
					else entity.IsAccPrice = Convert.ToBoolean(value);
				}
			}
			public System.String IsAccQty
			{
				get
				{
					System.Boolean? data = entity.IsAccQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAccQty = null;
					else entity.IsAccQty = Convert.ToBoolean(value);
				}
			}
			public System.String IsTaxable
			{
				get
				{
					System.Boolean? data = entity.IsTaxable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTaxable = null;
					else entity.IsTaxable = Convert.ToBoolean(value);
				}
			}
			public System.String IsTaxablePph
			{
				get
				{
					System.Boolean? data = entity.IsTaxablePph;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTaxablePph = null;
					else entity.IsTaxablePph = Convert.ToBoolean(value);
				}
			}
			public System.String SRPph
			{
				get
				{
					System.String data = entity.SRPph;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPph = null;
					else entity.SRPph = Convert.ToString(value);
				}
			}
			public System.String PphPercentage
			{
				get
				{
					System.Decimal? data = entity.PphPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PphPercentage = null;
					else entity.PphPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String PphAmount
			{
				get
				{
					System.Decimal? data = entity.PphAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PphAmount = null;
					else entity.PphAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
				}
			}
			public System.String FabricID
			{
				get
				{
					System.String data = entity.FabricID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FabricID = null;
					else entity.FabricID = Convert.ToString(value);
				}
			}
			private esItemTransactionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTransactionItemQuery query)
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
				throw new Exception("esItemTransactionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTransactionItem : esItemTransactionItem
	{
	}

	[Serializable]
	abstract public class esItemTransactionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		}

		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		}

		public esQueryItem QuantityFinishInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.QuantityFinishInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem PageNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PageNo, esSystemType.Int32);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PriceInCurrency, esSystemType.Decimal);
			}
		}

		public esQueryItem Discount1Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Discount1Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem Discount2Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Discount2Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem BatchNumber
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.BatchNumber, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBonusItem
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsBonusItem, esSystemType.Boolean);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Description, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem RequestQty
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.RequestQty, esSystemType.Decimal);
			}
		}

		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		}

		public esQueryItem DiscountInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.DiscountInCurrency, esSystemType.Decimal);
			}
		}

		public esQueryItem IsDiscountInPercent
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent, esSystemType.Boolean);
			}
		}

		public esQueryItem IsInvoiceUpdate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsInvoiceUpdate, esSystemType.Boolean);
			}
		}

		public esQueryItem PriorPrice
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PriorPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem PriorPriceInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PriorPriceInCurrency, esSystemType.Decimal);
			}
		}

		public esQueryItem PriorDiscount1Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PriorDiscount1Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem PriorDiscount2Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PriorDiscount2Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem PriorDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PriorDiscount, esSystemType.Decimal);
			}
		}

		public esQueryItem PriorDiscountInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PriorDiscountInCurrency, esSystemType.Decimal);
			}
		}

		public esQueryItem LastInvoiceUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastInvoiceUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem HistoryPrice
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.HistoryPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem HistoryPriceInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.HistoryPriceInCurrency, esSystemType.Decimal);
			}
		}

		public esQueryItem HistoryDiscount1Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.HistoryDiscount1Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem HistoryDiscount2Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.HistoryDiscount2Percentage, esSystemType.Decimal);
			}
		}

		public esQueryItem HistoryDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.HistoryDiscount, esSystemType.Decimal);
			}
		}

		public esQueryItem HistoryDiscountInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.HistoryDiscountInCurrency, esSystemType.Decimal);
			}
		}

		public esQueryItem Specification
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Specification, esSystemType.String);
			}
		}

		public esQueryItem IsAccEd
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsAccEd, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAccPrice
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsAccPrice, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAccQty
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsAccQty, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTaxable
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsTaxable, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTaxablePph
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.IsTaxablePph, esSystemType.Boolean);
			}
		}

		public esQueryItem SRPph
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.SRPph, esSystemType.String);
			}
		}

		public esQueryItem PphPercentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PphPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem PphAmount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.PphAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem FabricID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemMetadata.ColumnNames.FabricID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTransactionItemCollection")]
	public partial class ItemTransactionItemCollection : esItemTransactionItemCollection, IEnumerable<ItemTransactionItem>
	{
		public ItemTransactionItemCollection()
		{

		}

		public static implicit operator List<ItemTransactionItem>(ItemTransactionItemCollection coll)
		{
			List<ItemTransactionItem> list = new List<ItemTransactionItem>();

			foreach (ItemTransactionItem emp in coll)
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
				return ItemTransactionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTransactionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTransactionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTransactionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionItemQuery();
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
		public bool Load(ItemTransactionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTransactionItem AddNew()
		{
			ItemTransactionItem entity = base.AddNewEntity() as ItemTransactionItem;

			return entity;
		}
		public ItemTransactionItem FindByPrimaryKey(String transactionNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo) as ItemTransactionItem;
		}

		#region IEnumerable< ItemTransactionItem> Members

		IEnumerator<ItemTransactionItem> IEnumerable<ItemTransactionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTransactionItem;
			}
		}

		#endregion

		private ItemTransactionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTransactionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTransactionItem ({TransactionNo, SequenceNo})")]
	[Serializable]
	public partial class ItemTransactionItem : esItemTransactionItem
	{
		public ItemTransactionItem()
		{
		}

		public ItemTransactionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionItemMetadata.Meta();
			}
		}

		override protected esItemTransactionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTransactionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionItemQuery();
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
		public bool Load(ItemTransactionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTransactionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTransactionItemQuery : esItemTransactionItemQuery
	{
		public ItemTransactionItemQuery()
		{

		}

		public ItemTransactionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTransactionItemQuery";
		}
	}

	[Serializable]
	public partial class ItemTransactionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTransactionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.ReferenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.ReferenceSequenceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Quantity, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.SRItemUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.ConversionFactor, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.QuantityFinishInBaseUnit, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.QuantityFinishInBaseUnit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PageNo, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PageNo;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.CostPrice, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Price, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PriceInCurrency, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PriceInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Discount1Percentage, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Discount1Percentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Discount2Percentage, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Discount2Percentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.BatchNumber, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.BatchNumber;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.ExpiredDate, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.ExpiredDate;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsPackage, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsPackage;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsBonusItem, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsBonusItem;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsClosed, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsClosed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Description, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.RequestQty, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.RequestQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Discount, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.DiscountInCurrency, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.DiscountInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsDiscountInPercent, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsDiscountInPercent;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsInvoiceUpdate, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsInvoiceUpdate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PriorPrice, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PriorPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PriorPriceInCurrency, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PriorPriceInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PriorDiscount1Percentage, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PriorDiscount1Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PriorDiscount2Percentage, 31, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PriorDiscount2Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PriorDiscount, 32, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PriorDiscount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PriorDiscountInCurrency, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PriorDiscountInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateDateTime, 34, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.LastInvoiceUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.LastInvoiceUpdateByUserID, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.LastInvoiceUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.HistoryPrice, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.HistoryPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.HistoryPriceInCurrency, 37, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.HistoryPriceInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount1Percentage, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.HistoryDiscount1Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount2Percentage, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.HistoryDiscount2Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.HistoryDiscount, 40, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.HistoryDiscount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.HistoryDiscountInCurrency, 41, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.HistoryDiscountInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Specification, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Specification;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsAccEd, 43, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsAccEd;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsAccPrice, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsAccPrice;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsAccQty, 45, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsAccQty;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsTaxable, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsTaxable;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.IsTaxablePph, 47, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.IsTaxablePph;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.SRPph, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.SRPph;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PphPercentage, 49, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PphPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.PphAmount, 50, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.PphAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.ApprovedByUserID, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.ApprovedDateTime, 52, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.Note, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemMetadata.ColumnNames.FabricID, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemMetadata.PropertyNames.FabricID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTransactionItemMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string QuantityFinishInBaseUnit = "QuantityFinishInBaseUnit";
			public const string PageNo = "PageNo";
			public const string CostPrice = "CostPrice";
			public const string Price = "Price";
			public const string PriceInCurrency = "PriceInCurrency";
			public const string Discount1Percentage = "Discount1Percentage";
			public const string Discount2Percentage = "Discount2Percentage";
			public const string BatchNumber = "BatchNumber";
			public const string ExpiredDate = "ExpiredDate";
			public const string IsPackage = "IsPackage";
			public const string IsBonusItem = "IsBonusItem";
			public const string IsClosed = "IsClosed";
			public const string Description = "Description";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RequestQty = "RequestQty";
			public const string Discount = "Discount";
			public const string DiscountInCurrency = "DiscountInCurrency";
			public const string IsDiscountInPercent = "IsDiscountInPercent";
			public const string IsInvoiceUpdate = "IsInvoiceUpdate";
			public const string PriorPrice = "PriorPrice";
			public const string PriorPriceInCurrency = "PriorPriceInCurrency";
			public const string PriorDiscount1Percentage = "PriorDiscount1Percentage";
			public const string PriorDiscount2Percentage = "PriorDiscount2Percentage";
			public const string PriorDiscount = "PriorDiscount";
			public const string PriorDiscountInCurrency = "PriorDiscountInCurrency";
			public const string LastInvoiceUpdateDateTime = "LastInvoiceUpdateDateTime";
			public const string LastInvoiceUpdateByUserID = "LastInvoiceUpdateByUserID";
			public const string HistoryPrice = "HistoryPrice";
			public const string HistoryPriceInCurrency = "HistoryPriceInCurrency";
			public const string HistoryDiscount1Percentage = "HistoryDiscount1Percentage";
			public const string HistoryDiscount2Percentage = "HistoryDiscount2Percentage";
			public const string HistoryDiscount = "HistoryDiscount";
			public const string HistoryDiscountInCurrency = "HistoryDiscountInCurrency";
			public const string Specification = "Specification";
			public const string IsAccEd = "IsAccEd";
			public const string IsAccPrice = "IsAccPrice";
			public const string IsAccQty = "IsAccQty";
			public const string IsTaxable = "IsTaxable";
			public const string IsTaxablePph = "IsTaxablePph";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string PphAmount = "PphAmount";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string Note = "Note";
			public const string FabricID = "FabricID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string QuantityFinishInBaseUnit = "QuantityFinishInBaseUnit";
			public const string PageNo = "PageNo";
			public const string CostPrice = "CostPrice";
			public const string Price = "Price";
			public const string PriceInCurrency = "PriceInCurrency";
			public const string Discount1Percentage = "Discount1Percentage";
			public const string Discount2Percentage = "Discount2Percentage";
			public const string BatchNumber = "BatchNumber";
			public const string ExpiredDate = "ExpiredDate";
			public const string IsPackage = "IsPackage";
			public const string IsBonusItem = "IsBonusItem";
			public const string IsClosed = "IsClosed";
			public const string Description = "Description";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RequestQty = "RequestQty";
			public const string Discount = "Discount";
			public const string DiscountInCurrency = "DiscountInCurrency";
			public const string IsDiscountInPercent = "IsDiscountInPercent";
			public const string IsInvoiceUpdate = "IsInvoiceUpdate";
			public const string PriorPrice = "PriorPrice";
			public const string PriorPriceInCurrency = "PriorPriceInCurrency";
			public const string PriorDiscount1Percentage = "PriorDiscount1Percentage";
			public const string PriorDiscount2Percentage = "PriorDiscount2Percentage";
			public const string PriorDiscount = "PriorDiscount";
			public const string PriorDiscountInCurrency = "PriorDiscountInCurrency";
			public const string LastInvoiceUpdateDateTime = "LastInvoiceUpdateDateTime";
			public const string LastInvoiceUpdateByUserID = "LastInvoiceUpdateByUserID";
			public const string HistoryPrice = "HistoryPrice";
			public const string HistoryPriceInCurrency = "HistoryPriceInCurrency";
			public const string HistoryDiscount1Percentage = "HistoryDiscount1Percentage";
			public const string HistoryDiscount2Percentage = "HistoryDiscount2Percentage";
			public const string HistoryDiscount = "HistoryDiscount";
			public const string HistoryDiscountInCurrency = "HistoryDiscountInCurrency";
			public const string Specification = "Specification";
			public const string IsAccEd = "IsAccEd";
			public const string IsAccPrice = "IsAccPrice";
			public const string IsAccQty = "IsAccQty";
			public const string IsTaxable = "IsTaxable";
			public const string IsTaxablePph = "IsTaxablePph";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string PphAmount = "PphAmount";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string Note = "Note";
			public const string FabricID = "FabricID";
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
			lock (typeof(ItemTransactionItemMetadata))
			{
				if (ItemTransactionItemMetadata.mapDelegates == null)
				{
					ItemTransactionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTransactionItemMetadata.meta == null)
				{
					ItemTransactionItemMetadata.meta = new ItemTransactionItemMetadata();
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
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityFinishInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PageNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount1Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount2Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBonusItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsDiscountInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInvoiceUpdate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PriorPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorPriceInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscount1Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscount2Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscountInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastInvoiceUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastInvoiceUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HistoryPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryPriceInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscount1Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscount2Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscountInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Specification", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAccEd", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAccPrice", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAccQty", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTaxable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTaxablePph", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRPph", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PphPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PphAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FabricID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemTransactionItem";
				meta.Destination = "ItemTransactionItem";
				meta.spInsert = "proc_ItemTransactionItemInsert";
				meta.spUpdate = "proc_ItemTransactionItemUpdate";
				meta.spDelete = "proc_ItemTransactionItemDelete";
				meta.spLoadAll = "proc_ItemTransactionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTransactionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTransactionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
