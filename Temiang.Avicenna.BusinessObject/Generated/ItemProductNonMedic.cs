/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/10/2022 1:38:07 PM
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
	abstract public class esItemProductNonMedicCollection : esEntityCollectionWAuditLog
	{
		public esItemProductNonMedicCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemProductNonMedicCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductNonMedicQuery query)
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
			this.InitQuery(query as esItemProductNonMedicQuery);
		}
		#endregion

		virtual public ItemProductNonMedic DetachEntity(ItemProductNonMedic entity)
		{
			return base.DetachEntity(entity) as ItemProductNonMedic;
		}

		virtual public ItemProductNonMedic AttachEntity(ItemProductNonMedic entity)
		{
			return base.AttachEntity(entity) as ItemProductNonMedic;
		}

		virtual public void Combine(ItemProductNonMedicCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemProductNonMedic this[int index]
		{
			get
			{
				return base[index] as ItemProductNonMedic;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductNonMedic);
		}
	}

	[Serializable]
	abstract public class esItemProductNonMedic : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductNonMedicQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductNonMedic()
		{
		}

		public esItemProductNonMedic(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String itemID)
		{
			esItemProductNonMedicQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID", itemID);
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "MarginID": this.str.MarginID = (string)value; break;
						case "SRProductType": this.str.SRProductType = (string)value; break;
						case "ABCClass": this.str.ABCClass = (string)value; break;
						case "BrandName": this.str.BrandName = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "SRPurchaseUnit": this.str.SRPurchaseUnit = (string)value; break;
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
						case "Dosage": this.str.Dosage = (string)value; break;
						case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;
						case "IsFormularium": this.str.IsFormularium = (string)value; break;
						case "IsInventoryItem": this.str.IsInventoryItem = (string)value; break;
						case "IsControlExpired": this.str.IsControlExpired = (string)value; break;
						case "FabricID": this.str.FabricID = (string)value; break;
						case "SalesFixedPrice": this.str.SalesFixedPrice = (string)value; break;
						case "MarginPercentage": this.str.MarginPercentage = (string)value; break;
						case "SalesDiscount": this.str.SalesDiscount = (string)value; break;
						case "PriceInPurchaseUnit": this.str.PriceInPurchaseUnit = (string)value; break;
						case "PriceInBaseUnit": this.str.PriceInBaseUnit = (string)value; break;
						case "PriceInBasedUnitWVat": this.str.PriceInBasedUnitWVat = (string)value; break;
						case "HighestPriceInBasedUnit": this.str.HighestPriceInBasedUnit = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "PurchaseDiscount1": this.str.PurchaseDiscount1 = (string)value; break;
						case "PurchaseDiscount2": this.str.PurchaseDiscount2 = (string)value; break;
						case "SafetyStock": this.str.SafetyStock = (string)value; break;
						case "SafetyTime": this.str.SafetyTime = (string)value; break;
						case "LeadTime": this.str.LeadTime = (string)value; break;
						case "TolerancePercentage": this.str.TolerancePercentage = (string)value; break;
						case "Barcode": this.str.Barcode = (string)value; break;
						case "SRItemBin": this.str.SRItemBin = (string)value; break;
						case "IsConsignment": this.str.IsConsignment = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastPriceInBaseUnit": this.str.LastPriceInBaseUnit = (string)value; break;
						case "IsSalesAvailable": this.str.IsSalesAvailable = (string)value; break;
						case "PriceWVat": this.str.PriceWVat = (string)value; break;
						case "IsSharePurchaseDiscToPatient": this.str.IsSharePurchaseDiscToPatient = (string)value; break;
						case "IsNeedToBeLaundered": this.str.IsNeedToBeLaundered = (string)value; break;
						case "SRPurchaseCategorization": this.str.SRPurchaseCategorization = (string)value; break;
						case "Weight": this.str.Weight = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ConversionFactor":

							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
							break;
						case "Dosage":

							if (value == null || value is System.Decimal)
								this.Dosage = (System.Decimal?)value;
							break;
						case "IsFormularium":

							if (value == null || value is System.Boolean)
								this.IsFormularium = (System.Boolean?)value;
							break;
						case "IsInventoryItem":

							if (value == null || value is System.Boolean)
								this.IsInventoryItem = (System.Boolean?)value;
							break;
						case "IsControlExpired":

							if (value == null || value is System.Boolean)
								this.IsControlExpired = (System.Boolean?)value;
							break;
						case "SalesFixedPrice":

							if (value == null || value is System.Decimal)
								this.SalesFixedPrice = (System.Decimal?)value;
							break;
						case "MarginPercentage":

							if (value == null || value is System.Decimal)
								this.MarginPercentage = (System.Decimal?)value;
							break;
						case "SalesDiscount":

							if (value == null || value is System.Decimal)
								this.SalesDiscount = (System.Decimal?)value;
							break;
						case "PriceInPurchaseUnit":

							if (value == null || value is System.Decimal)
								this.PriceInPurchaseUnit = (System.Decimal?)value;
							break;
						case "PriceInBaseUnit":

							if (value == null || value is System.Decimal)
								this.PriceInBaseUnit = (System.Decimal?)value;
							break;
						case "PriceInBasedUnitWVat":

							if (value == null || value is System.Decimal)
								this.PriceInBasedUnitWVat = (System.Decimal?)value;
							break;
						case "HighestPriceInBasedUnit":

							if (value == null || value is System.Decimal)
								this.HighestPriceInBasedUnit = (System.Decimal?)value;
							break;
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						case "PurchaseDiscount1":

							if (value == null || value is System.Decimal)
								this.PurchaseDiscount1 = (System.Decimal?)value;
							break;
						case "PurchaseDiscount2":

							if (value == null || value is System.Decimal)
								this.PurchaseDiscount2 = (System.Decimal?)value;
							break;
						case "SafetyStock":

							if (value == null || value is System.Decimal)
								this.SafetyStock = (System.Decimal?)value;
							break;
						case "SafetyTime":

							if (value == null || value is System.Byte)
								this.SafetyTime = (System.Byte?)value;
							break;
						case "LeadTime":

							if (value == null || value is System.Byte)
								this.LeadTime = (System.Byte?)value;
							break;
						case "TolerancePercentage":

							if (value == null || value is System.Decimal)
								this.TolerancePercentage = (System.Decimal?)value;
							break;
						case "IsConsignment":

							if (value == null || value is System.Boolean)
								this.IsConsignment = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LastPriceInBaseUnit":

							if (value == null || value is System.Decimal)
								this.LastPriceInBaseUnit = (System.Decimal?)value;
							break;
						case "IsSalesAvailable":

							if (value == null || value is System.Boolean)
								this.IsSalesAvailable = (System.Boolean?)value;
							break;
						case "PriceWVat":

							if (value == null || value is System.Decimal)
								this.PriceWVat = (System.Decimal?)value;
							break;
						case "IsSharePurchaseDiscToPatient":

							if (value == null || value is System.Boolean)
								this.IsSharePurchaseDiscToPatient = (System.Boolean?)value;
							break;
						case "IsNeedToBeLaundered":

							if (value == null || value is System.Boolean)
								this.IsNeedToBeLaundered = (System.Boolean?)value;
							break;
						case "Weight":

							if (value == null || value is System.Decimal)
								this.Weight = (System.Decimal?)value;
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
		/// Maps to ItemProductNonMedic.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.MarginID
		/// </summary>
		virtual public System.String MarginID
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.MarginID);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.MarginID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SRProductType
		/// </summary>
		virtual public System.String SRProductType
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRProductType);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRProductType, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.ABCClass
		/// </summary>
		virtual public System.String ABCClass
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.ABCClass);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.ABCClass, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.BrandName
		/// </summary>
		virtual public System.String BrandName
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.BrandName);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.BrandName, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SRPurchaseUnit
		/// </summary>
		virtual public System.String SRPurchaseUnit
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRPurchaseUnit);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.ConversionFactor);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.Dosage
		/// </summary>
		virtual public System.Decimal? Dosage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.Dosage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.Dosage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRDosageUnit);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.IsFormularium
		/// </summary>
		virtual public System.Boolean? IsFormularium
		{
			get
			{
				return base.GetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsFormularium);
			}

			set
			{
				base.SetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsFormularium, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.IsInventoryItem
		/// </summary>
		virtual public System.Boolean? IsInventoryItem
		{
			get
			{
				return base.GetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsInventoryItem);
			}

			set
			{
				base.SetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsInventoryItem, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.IsControlExpired
		/// </summary>
		virtual public System.Boolean? IsControlExpired
		{
			get
			{
				return base.GetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsControlExpired);
			}

			set
			{
				base.SetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsControlExpired, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.FabricID
		/// </summary>
		virtual public System.String FabricID
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.FabricID);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.FabricID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SalesFixedPrice
		/// </summary>
		virtual public System.Decimal? SalesFixedPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.SalesFixedPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.SalesFixedPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.MarginPercentage
		/// </summary>
		virtual public System.Decimal? MarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.MarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.MarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SalesDiscount
		/// </summary>
		virtual public System.Decimal? SalesDiscount
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.SalesDiscount);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.SalesDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.PriceInPurchaseUnit
		/// </summary>
		virtual public System.Decimal? PriceInPurchaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceInPurchaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceInPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.PriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? PriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.PriceInBasedUnitWVat
		/// </summary>
		virtual public System.Decimal? PriceInBasedUnitWVat
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.HighestPriceInBasedUnit
		/// </summary>
		virtual public System.Decimal? HighestPriceInBasedUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.PurchaseDiscount1
		/// </summary>
		virtual public System.Decimal? PurchaseDiscount1
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount1);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount1, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.PurchaseDiscount2
		/// </summary>
		virtual public System.Decimal? PurchaseDiscount2
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount2);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount2, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SafetyStock
		/// </summary>
		virtual public System.Decimal? SafetyStock
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.SafetyStock);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.SafetyStock, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SafetyTime
		/// </summary>
		virtual public System.Byte? SafetyTime
		{
			get
			{
				return base.GetSystemByte(ItemProductNonMedicMetadata.ColumnNames.SafetyTime);
			}

			set
			{
				base.SetSystemByte(ItemProductNonMedicMetadata.ColumnNames.SafetyTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.LeadTime
		/// </summary>
		virtual public System.Byte? LeadTime
		{
			get
			{
				return base.GetSystemByte(ItemProductNonMedicMetadata.ColumnNames.LeadTime);
			}

			set
			{
				base.SetSystemByte(ItemProductNonMedicMetadata.ColumnNames.LeadTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.TolerancePercentage
		/// </summary>
		virtual public System.Decimal? TolerancePercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.TolerancePercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.TolerancePercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.Barcode
		/// </summary>
		virtual public System.String Barcode
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.Barcode);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.Barcode, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SRItemBin
		/// </summary>
		virtual public System.String SRItemBin
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRItemBin);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRItemBin, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.IsConsignment
		/// </summary>
		virtual public System.Boolean? IsConsignment
		{
			get
			{
				return base.GetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsConsignment);
			}

			set
			{
				base.SetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsConsignment, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductNonMedicMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemProductNonMedicMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.LastPriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? LastPriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.LastPriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.LastPriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.IsSalesAvailable
		/// </summary>
		virtual public System.Boolean? IsSalesAvailable
		{
			get
			{
				return base.GetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsSalesAvailable);
			}

			set
			{
				base.SetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsSalesAvailable, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.PriceWVat
		/// </summary>
		virtual public System.Decimal? PriceWVat
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceWVat);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.PriceWVat, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.IsSharePurchaseDiscToPatient
		/// </summary>
		virtual public System.Boolean? IsSharePurchaseDiscToPatient
		{
			get
			{
				return base.GetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient);
			}

			set
			{
				base.SetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.IsNeedToBeLaundered
		/// </summary>
		virtual public System.Boolean? IsNeedToBeLaundered
		{
			get
			{
				return base.GetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsNeedToBeLaundered);
			}

			set
			{
				base.SetSystemBoolean(ItemProductNonMedicMetadata.ColumnNames.IsNeedToBeLaundered, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.SRPurchaseCategorization
		/// </summary>
		virtual public System.String SRPurchaseCategorization
		{
			get
			{
				return base.GetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRPurchaseCategorization);
			}

			set
			{
				base.SetSystemString(ItemProductNonMedicMetadata.ColumnNames.SRPurchaseCategorization, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductNonMedic.Weight
		/// </summary>
		virtual public System.Decimal? Weight
		{
			get
			{
				return base.GetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.Weight);
			}

			set
			{
				base.SetSystemDecimal(ItemProductNonMedicMetadata.ColumnNames.Weight, value);
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
			public esStrings(esItemProductNonMedic entity)
			{
				this.entity = entity;
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
			public System.String MarginID
			{
				get
				{
					System.String data = entity.MarginID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginID = null;
					else entity.MarginID = Convert.ToString(value);
				}
			}
			public System.String SRProductType
			{
				get
				{
					System.String data = entity.SRProductType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProductType = null;
					else entity.SRProductType = Convert.ToString(value);
				}
			}
			public System.String ABCClass
			{
				get
				{
					System.String data = entity.ABCClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ABCClass = null;
					else entity.ABCClass = Convert.ToString(value);
				}
			}
			public System.String BrandName
			{
				get
				{
					System.String data = entity.BrandName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BrandName = null;
					else entity.BrandName = Convert.ToString(value);
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
			public System.String SRPurchaseUnit
			{
				get
				{
					System.String data = entity.SRPurchaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPurchaseUnit = null;
					else entity.SRPurchaseUnit = Convert.ToString(value);
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
			public System.String Dosage
			{
				get
				{
					System.Decimal? data = entity.Dosage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Dosage = null;
					else entity.Dosage = Convert.ToDecimal(value);
				}
			}
			public System.String SRDosageUnit
			{
				get
				{
					System.String data = entity.SRDosageUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDosageUnit = null;
					else entity.SRDosageUnit = Convert.ToString(value);
				}
			}
			public System.String IsFormularium
			{
				get
				{
					System.Boolean? data = entity.IsFormularium;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFormularium = null;
					else entity.IsFormularium = Convert.ToBoolean(value);
				}
			}
			public System.String IsInventoryItem
			{
				get
				{
					System.Boolean? data = entity.IsInventoryItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInventoryItem = null;
					else entity.IsInventoryItem = Convert.ToBoolean(value);
				}
			}
			public System.String IsControlExpired
			{
				get
				{
					System.Boolean? data = entity.IsControlExpired;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsControlExpired = null;
					else entity.IsControlExpired = Convert.ToBoolean(value);
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
			public System.String SalesFixedPrice
			{
				get
				{
					System.Decimal? data = entity.SalesFixedPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesFixedPrice = null;
					else entity.SalesFixedPrice = Convert.ToDecimal(value);
				}
			}
			public System.String MarginPercentage
			{
				get
				{
					System.Decimal? data = entity.MarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginPercentage = null;
					else entity.MarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String SalesDiscount
			{
				get
				{
					System.Decimal? data = entity.SalesDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesDiscount = null;
					else entity.SalesDiscount = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInPurchaseUnit
			{
				get
				{
					System.Decimal? data = entity.PriceInPurchaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInPurchaseUnit = null;
					else entity.PriceInPurchaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInBaseUnit
			{
				get
				{
					System.Decimal? data = entity.PriceInBaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInBaseUnit = null;
					else entity.PriceInBaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInBasedUnitWVat
			{
				get
				{
					System.Decimal? data = entity.PriceInBasedUnitWVat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInBasedUnitWVat = null;
					else entity.PriceInBasedUnitWVat = Convert.ToDecimal(value);
				}
			}
			public System.String HighestPriceInBasedUnit
			{
				get
				{
					System.Decimal? data = entity.HighestPriceInBasedUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HighestPriceInBasedUnit = null;
					else entity.HighestPriceInBasedUnit = Convert.ToDecimal(value);
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
			public System.String PurchaseDiscount1
			{
				get
				{
					System.Decimal? data = entity.PurchaseDiscount1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchaseDiscount1 = null;
					else entity.PurchaseDiscount1 = Convert.ToDecimal(value);
				}
			}
			public System.String PurchaseDiscount2
			{
				get
				{
					System.Decimal? data = entity.PurchaseDiscount2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchaseDiscount2 = null;
					else entity.PurchaseDiscount2 = Convert.ToDecimal(value);
				}
			}
			public System.String SafetyStock
			{
				get
				{
					System.Decimal? data = entity.SafetyStock;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SafetyStock = null;
					else entity.SafetyStock = Convert.ToDecimal(value);
				}
			}
			public System.String SafetyTime
			{
				get
				{
					System.Byte? data = entity.SafetyTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SafetyTime = null;
					else entity.SafetyTime = Convert.ToByte(value);
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
			public System.String TolerancePercentage
			{
				get
				{
					System.Decimal? data = entity.TolerancePercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TolerancePercentage = null;
					else entity.TolerancePercentage = Convert.ToDecimal(value);
				}
			}
			public System.String Barcode
			{
				get
				{
					System.String data = entity.Barcode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Barcode = null;
					else entity.Barcode = Convert.ToString(value);
				}
			}
			public System.String SRItemBin
			{
				get
				{
					System.String data = entity.SRItemBin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemBin = null;
					else entity.SRItemBin = Convert.ToString(value);
				}
			}
			public System.String IsConsignment
			{
				get
				{
					System.Boolean? data = entity.IsConsignment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConsignment = null;
					else entity.IsConsignment = Convert.ToBoolean(value);
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
			public System.String LastPriceInBaseUnit
			{
				get
				{
					System.Decimal? data = entity.LastPriceInBaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPriceInBaseUnit = null;
					else entity.LastPriceInBaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String IsSalesAvailable
			{
				get
				{
					System.Boolean? data = entity.IsSalesAvailable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSalesAvailable = null;
					else entity.IsSalesAvailable = Convert.ToBoolean(value);
				}
			}
			public System.String PriceWVat
			{
				get
				{
					System.Decimal? data = entity.PriceWVat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceWVat = null;
					else entity.PriceWVat = Convert.ToDecimal(value);
				}
			}
			public System.String IsSharePurchaseDiscToPatient
			{
				get
				{
					System.Boolean? data = entity.IsSharePurchaseDiscToPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSharePurchaseDiscToPatient = null;
					else entity.IsSharePurchaseDiscToPatient = Convert.ToBoolean(value);
				}
			}
			public System.String IsNeedToBeLaundered
			{
				get
				{
					System.Boolean? data = entity.IsNeedToBeLaundered;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedToBeLaundered = null;
					else entity.IsNeedToBeLaundered = Convert.ToBoolean(value);
				}
			}
			public System.String SRPurchaseCategorization
			{
				get
				{
					System.String data = entity.SRPurchaseCategorization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPurchaseCategorization = null;
					else entity.SRPurchaseCategorization = Convert.ToString(value);
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
			private esItemProductNonMedic entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductNonMedicQuery query)
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
				throw new Exception("esItemProductNonMedic can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemProductNonMedic : esItemProductNonMedic
	{
	}

	[Serializable]
	abstract public class esItemProductNonMedicQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemProductNonMedicMetadata.Meta();
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem MarginID
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.MarginID, esSystemType.String);
			}
		}

		public esQueryItem SRProductType
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SRProductType, esSystemType.String);
			}
		}

		public esQueryItem ABCClass
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.ABCClass, esSystemType.String);
			}
		}

		public esQueryItem BrandName
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.BrandName, esSystemType.String);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem SRPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SRPurchaseUnit, esSystemType.String);
			}
		}

		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		}

		public esQueryItem Dosage
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.Dosage, esSystemType.Decimal);
			}
		}

		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		}

		public esQueryItem IsFormularium
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.IsFormularium, esSystemType.Boolean);
			}
		}

		public esQueryItem IsInventoryItem
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.IsInventoryItem, esSystemType.Boolean);
			}
		}

		public esQueryItem IsControlExpired
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.IsControlExpired, esSystemType.Boolean);
			}
		}

		public esQueryItem FabricID
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.FabricID, esSystemType.String);
			}
		}

		public esQueryItem SalesFixedPrice
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SalesFixedPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem MarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.MarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem SalesDiscount
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SalesDiscount, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.PriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBasedUnitWVat
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat, esSystemType.Decimal);
			}
		}

		public esQueryItem HighestPriceInBasedUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem PurchaseDiscount1
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount1, esSystemType.Decimal);
			}
		}

		public esQueryItem PurchaseDiscount2
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount2, esSystemType.Decimal);
			}
		}

		public esQueryItem SafetyStock
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SafetyStock, esSystemType.Decimal);
			}
		}

		public esQueryItem SafetyTime
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SafetyTime, esSystemType.Byte);
			}
		}

		public esQueryItem LeadTime
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.LeadTime, esSystemType.Byte);
			}
		}

		public esQueryItem TolerancePercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.TolerancePercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem Barcode
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.Barcode, esSystemType.String);
			}
		}

		public esQueryItem SRItemBin
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SRItemBin, esSystemType.String);
			}
		}

		public esQueryItem IsConsignment
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.IsConsignment, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastPriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.LastPriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem IsSalesAvailable
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.IsSalesAvailable, esSystemType.Boolean);
			}
		}

		public esQueryItem PriceWVat
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.PriceWVat, esSystemType.Decimal);
			}
		}

		public esQueryItem IsSharePurchaseDiscToPatient
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNeedToBeLaundered
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.IsNeedToBeLaundered, esSystemType.Boolean);
			}
		}

		public esQueryItem SRPurchaseCategorization
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.SRPurchaseCategorization, esSystemType.String);
			}
		}

		public esQueryItem Weight
		{
			get
			{
				return new esQueryItem(this, ItemProductNonMedicMetadata.ColumnNames.Weight, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductNonMedicCollection")]
	public partial class ItemProductNonMedicCollection : esItemProductNonMedicCollection, IEnumerable<ItemProductNonMedic>
	{
		public ItemProductNonMedicCollection()
		{

		}

		public static implicit operator List<ItemProductNonMedic>(ItemProductNonMedicCollection coll)
		{
			List<ItemProductNonMedic> list = new List<ItemProductNonMedic>();

			foreach (ItemProductNonMedic emp in coll)
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
				return ItemProductNonMedicMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductNonMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductNonMedic(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductNonMedic();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemProductNonMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductNonMedicQuery();
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
		public bool Load(ItemProductNonMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemProductNonMedic AddNew()
		{
			ItemProductNonMedic entity = base.AddNewEntity() as ItemProductNonMedic;

			return entity;
		}
		public ItemProductNonMedic FindByPrimaryKey(String itemID)
		{
			return base.FindByPrimaryKey(itemID) as ItemProductNonMedic;
		}

		#region IEnumerable< ItemProductNonMedic> Members

		IEnumerator<ItemProductNonMedic> IEnumerable<ItemProductNonMedic>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductNonMedic;
			}
		}

		#endregion

		private ItemProductNonMedicQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductNonMedic' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemProductNonMedic ({ItemID})")]
	[Serializable]
	public partial class ItemProductNonMedic : esItemProductNonMedic
	{
		public ItemProductNonMedic()
		{
		}

		public ItemProductNonMedic(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductNonMedicMetadata.Meta();
			}
		}

		override protected esItemProductNonMedicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductNonMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemProductNonMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductNonMedicQuery();
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
		public bool Load(ItemProductNonMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemProductNonMedicQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemProductNonMedicQuery : esItemProductNonMedicQuery
	{
		public ItemProductNonMedicQuery()
		{

		}

		public ItemProductNonMedicQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemProductNonMedicQuery";
		}
	}

	[Serializable]
	public partial class ItemProductNonMedicMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductNonMedicMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.MarginID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.MarginID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SRProductType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SRProductType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.ABCClass, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.ABCClass;
			c.CharacterMaxLength = 1;
			c.HasDefault = true;
			c.Default = @"('A')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.BrandName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.BrandName;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SRItemUnit, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SRPurchaseUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SRPurchaseUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.ConversionFactor, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 7;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.Dosage, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.Dosage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SRDosageUnit, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SRDosageUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.IsFormularium, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.IsFormularium;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.IsInventoryItem, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.IsInventoryItem;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.IsControlExpired, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.IsControlExpired;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.FabricID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.FabricID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SalesFixedPrice, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SalesFixedPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.MarginPercentage, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.MarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SalesDiscount, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SalesDiscount;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.PriceInPurchaseUnit, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.PriceInPurchaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.PriceInBaseUnit, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.PriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.PriceInBasedUnitWVat;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.HighestPriceInBasedUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.CostPrice, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount1, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.PurchaseDiscount1;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.PurchaseDiscount2, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.PurchaseDiscount2;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SafetyStock, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SafetyStock;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SafetyTime, 25, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SafetyTime;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.LeadTime, 26, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.LeadTime;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.TolerancePercentage, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.TolerancePercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.Barcode, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.Barcode;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SRItemBin, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SRItemBin;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.IsConsignment, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.IsConsignment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.LastUpdateDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.LastUpdateByUserID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.LastPriceInBaseUnit, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.LastPriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.IsSalesAvailable, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.IsSalesAvailable;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.PriceWVat, 35, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.PriceWVat;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.IsSharePurchaseDiscToPatient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.IsNeedToBeLaundered, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.IsNeedToBeLaundered;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.SRPurchaseCategorization, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.SRPurchaseCategorization;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductNonMedicMetadata.ColumnNames.Weight, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductNonMedicMetadata.PropertyNames.Weight;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemProductNonMedicMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string MarginID = "MarginID";
			public const string SRProductType = "SRProductType";
			public const string ABCClass = "ABCClass";
			public const string BrandName = "BrandName";
			public const string SRItemUnit = "SRItemUnit";
			public const string SRPurchaseUnit = "SRPurchaseUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string Dosage = "Dosage";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string IsFormularium = "IsFormularium";
			public const string IsInventoryItem = "IsInventoryItem";
			public const string IsControlExpired = "IsControlExpired";
			public const string FabricID = "FabricID";
			public const string SalesFixedPrice = "SalesFixedPrice";
			public const string MarginPercentage = "MarginPercentage";
			public const string SalesDiscount = "SalesDiscount";
			public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
			public const string PriceInBaseUnit = "PriceInBaseUnit";
			public const string PriceInBasedUnitWVat = "PriceInBasedUnitWVat";
			public const string HighestPriceInBasedUnit = "HighestPriceInBasedUnit";
			public const string CostPrice = "CostPrice";
			public const string PurchaseDiscount1 = "PurchaseDiscount1";
			public const string PurchaseDiscount2 = "PurchaseDiscount2";
			public const string SafetyStock = "SafetyStock";
			public const string SafetyTime = "SafetyTime";
			public const string LeadTime = "LeadTime";
			public const string TolerancePercentage = "TolerancePercentage";
			public const string Barcode = "Barcode";
			public const string SRItemBin = "SRItemBin";
			public const string IsConsignment = "IsConsignment";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string PriceWVat = "PriceWVat";
			public const string IsSharePurchaseDiscToPatient = "IsSharePurchaseDiscToPatient";
			public const string IsNeedToBeLaundered = "IsNeedToBeLaundered";
			public const string SRPurchaseCategorization = "SRPurchaseCategorization";
			public const string Weight = "Weight";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ItemID = "ItemID";
			public const string MarginID = "MarginID";
			public const string SRProductType = "SRProductType";
			public const string ABCClass = "ABCClass";
			public const string BrandName = "BrandName";
			public const string SRItemUnit = "SRItemUnit";
			public const string SRPurchaseUnit = "SRPurchaseUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string Dosage = "Dosage";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string IsFormularium = "IsFormularium";
			public const string IsInventoryItem = "IsInventoryItem";
			public const string IsControlExpired = "IsControlExpired";
			public const string FabricID = "FabricID";
			public const string SalesFixedPrice = "SalesFixedPrice";
			public const string MarginPercentage = "MarginPercentage";
			public const string SalesDiscount = "SalesDiscount";
			public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
			public const string PriceInBaseUnit = "PriceInBaseUnit";
			public const string PriceInBasedUnitWVat = "PriceInBasedUnitWVat";
			public const string HighestPriceInBasedUnit = "HighestPriceInBasedUnit";
			public const string CostPrice = "CostPrice";
			public const string PurchaseDiscount1 = "PurchaseDiscount1";
			public const string PurchaseDiscount2 = "PurchaseDiscount2";
			public const string SafetyStock = "SafetyStock";
			public const string SafetyTime = "SafetyTime";
			public const string LeadTime = "LeadTime";
			public const string TolerancePercentage = "TolerancePercentage";
			public const string Barcode = "Barcode";
			public const string SRItemBin = "SRItemBin";
			public const string IsConsignment = "IsConsignment";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string PriceWVat = "PriceWVat";
			public const string IsSharePurchaseDiscToPatient = "IsSharePurchaseDiscToPatient";
			public const string IsNeedToBeLaundered = "IsNeedToBeLaundered";
			public const string SRPurchaseCategorization = "SRPurchaseCategorization";
			public const string Weight = "Weight";
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
			lock (typeof(ItemProductNonMedicMetadata))
			{
				if (ItemProductNonMedicMetadata.mapDelegates == null)
				{
					ItemProductNonMedicMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemProductNonMedicMetadata.meta == null)
				{
					ItemProductNonMedicMetadata.meta = new ItemProductNonMedicMetadata();
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

				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MarginID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProductType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ABCClass", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("BrandName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPurchaseUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Dosage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFormularium", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInventoryItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsControlExpired", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FabricID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalesFixedPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SalesDiscount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBasedUnitWVat", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HighestPriceInBasedUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PurchaseDiscount1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PurchaseDiscount2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SafetyStock", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SafetyTime", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("LeadTime", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("TolerancePercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Barcode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemBin", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConsignment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastPriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsSalesAvailable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PriceWVat", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsSharePurchaseDiscToPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNeedToBeLaundered", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRPurchaseCategorization", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "ItemProductNonMedic";
				meta.Destination = "ItemProductNonMedic";
				meta.spInsert = "proc_ItemProductNonMedicInsert";
				meta.spUpdate = "proc_ItemProductNonMedicUpdate";
				meta.spDelete = "proc_ItemProductNonMedicDelete";
				meta.spLoadAll = "proc_ItemProductNonMedicLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductNonMedicLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductNonMedicMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
