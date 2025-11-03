/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/28/2024 4:14:18 PM
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
	abstract public class esItemProductMedicCollection : esEntityCollectionWAuditLog
	{
		public esItemProductMedicCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemProductMedicCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductMedicQuery query)
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
			this.InitQuery(query as esItemProductMedicQuery);
		}
		#endregion

		virtual public ItemProductMedic DetachEntity(ItemProductMedic entity)
		{
			return base.DetachEntity(entity) as ItemProductMedic;
		}

		virtual public ItemProductMedic AttachEntity(ItemProductMedic entity)
		{
			return base.AttachEntity(entity) as ItemProductMedic;
		}

		virtual public void Combine(ItemProductMedicCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemProductMedic this[int index]
		{
			get
			{
				return base[index] as ItemProductMedic;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductMedic);
		}
	}

	[Serializable]
	abstract public class esItemProductMedic : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductMedicQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductMedic()
		{
		}

		public esItemProductMedic(DataRow row)
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
			esItemProductMedicQuery query = this.GetDynamicQuery();
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
						case "IsUsingCigna": this.str.IsUsingCigna = (string)value; break;
						case "Barcode": this.str.Barcode = (string)value; break;
						case "SRItemBin": this.str.SRItemBin = (string)value; break;
						case "SRDrugLabelType": this.str.SRDrugLabelType = (string)value; break;
						case "IsConsignment": this.str.IsConsignment = (string)value; break;
						case "TherapyID": this.str.TherapyID = (string)value; break;
						case "IsActualDeduct": this.str.IsActualDeduct = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsGeneric": this.str.IsGeneric = (string)value; break;
						case "PremiPharmaciesPercentage": this.str.PremiPharmaciesPercentage = (string)value; break;
						case "PremiPhysicianPercentage": this.str.PremiPhysicianPercentage = (string)value; break;
						case "HET": this.str.HET = (string)value; break;
						case "SRProductCategory": this.str.SRProductCategory = (string)value; break;
						case "LastPriceInBaseUnit": this.str.LastPriceInBaseUnit = (string)value; break;
						case "IsNarcotic": this.str.IsNarcotic = (string)value; break;
						case "IsPsychotropic": this.str.IsPsychotropic = (string)value; break;
						case "IsAntibiotic": this.str.IsAntibiotic = (string)value; break;
						case "IsRegularItem": this.str.IsRegularItem = (string)value; break;
						case "IsSalesAvailable": this.str.IsSalesAvailable = (string)value; break;
						case "IsDirectPurchase": this.str.IsDirectPurchase = (string)value; break;
						case "PriceWVat": this.str.PriceWVat = (string)value; break;
						case "IsPrecursor": this.str.IsPrecursor = (string)value; break;
						case "SRTherapyGroup": this.str.SRTherapyGroup = (string)value; break;
						case "Keeping": this.str.Keeping = (string)value; break;
						case "IsMorphine": this.str.IsMorphine = (string)value; break;
						case "SRKeeping": this.str.SRKeeping = (string)value; break;
						case "VENClass": this.str.VENClass = (string)value; break;
						case "IsHam": this.str.IsHam = (string)value; break;
						case "IsLasa": this.str.IsLasa = (string)value; break;
						case "IsOot": this.str.IsOot = (string)value; break;
						case "IsSharePurchaseDiscToPatient": this.str.IsSharePurchaseDiscToPatient = (string)value; break;
						case "IsFornas": this.str.IsFornas = (string)value; break;
						case "IsOtc": this.str.IsOtc = (string)value; break;
						case "IsHardDrug": this.str.IsHardDrug = (string)value; break;
						case "IsTraditionalMedicine": this.str.IsTraditionalMedicine = (string)value; break;
						case "IsSupplement": this.str.IsSupplement = (string)value; break;
						case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
						case "IsMedication": this.str.IsMedication = (string)value; break;
						case "ConsumptionLimitInDay": this.str.ConsumptionLimitInDay = (string)value; break;
						case "IsNonGeneric": this.str.IsNonGeneric = (string)value; break;
						case "IsAso": this.str.IsAso = (string)value; break;
						case "SRRoute": this.str.SRRoute = (string)value; break;
						case "IsNoPrescriptionFee": this.str.IsNoPrescriptionFee = (string)value; break;
						case "IsPethidine": this.str.IsPethidine = (string)value; break;
						case "SRAntibioticLine": this.str.SRAntibioticLine = (string)value; break;
						case "IsNonGenericLimited": this.str.IsNonGenericLimited = (string)value; break;
						case "SpecificInfo": this.str.SpecificInfo = (string)value; break;
						case "FornasRestrictionNotes": this.str.FornasRestrictionNotes = (string)value; break;
						case "IsChronic": this.str.IsChronic = (string)value; break;
						case "BpjsMaxQtyOrderIpr": this.str.BpjsMaxQtyOrderIpr = (string)value; break;
						case "BpjsMaxQtyOrderOpr": this.str.BpjsMaxQtyOrderOpr = (string)value; break;
						case "BpjsMaxQtyOrderEmr": this.str.BpjsMaxQtyOrderEmr = (string)value; break;
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
						case "IsUsingCigna":

							if (value == null || value is System.Boolean)
								this.IsUsingCigna = (System.Boolean?)value;
							break;
						case "IsConsignment":

							if (value == null || value is System.Boolean)
								this.IsConsignment = (System.Boolean?)value;
							break;
						case "IsActualDeduct":

							if (value == null || value is System.Boolean)
								this.IsActualDeduct = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "isGeneric":

							if (value == null || value is System.Boolean)
								this.IsGeneric = (System.Boolean?)value;
							break;
						case "PremiPharmaciesPercentage":

							if (value == null || value is System.Decimal)
								this.PremiPharmaciesPercentage = (System.Decimal?)value;
							break;
						case "PremiPhysicianPercentage":

							if (value == null || value is System.Decimal)
								this.PremiPhysicianPercentage = (System.Decimal?)value;
							break;
						case "HET":

							if (value == null || value is System.Decimal)
								this.HET = (System.Decimal?)value;
							break;
						case "LastPriceInBaseUnit":

							if (value == null || value is System.Decimal)
								this.LastPriceInBaseUnit = (System.Decimal?)value;
							break;
						case "IsNarcotic":

							if (value == null || value is System.Boolean)
								this.IsNarcotic = (System.Boolean?)value;
							break;
						case "IsPsychotropic":

							if (value == null || value is System.Boolean)
								this.IsPsychotropic = (System.Boolean?)value;
							break;
						case "IsAntibiotic":

							if (value == null || value is System.Boolean)
								this.IsAntibiotic = (System.Boolean?)value;
							break;
						case "IsRegularItem":

							if (value == null || value is System.Boolean)
								this.IsRegularItem = (System.Boolean?)value;
							break;
						case "IsSalesAvailable":

							if (value == null || value is System.Boolean)
								this.IsSalesAvailable = (System.Boolean?)value;
							break;
						case "IsDirectPurchase":

							if (value == null || value is System.Boolean)
								this.IsDirectPurchase = (System.Boolean?)value;
							break;
						case "PriceWVat":

							if (value == null || value is System.Decimal)
								this.PriceWVat = (System.Decimal?)value;
							break;
						case "IsPrecursor":

							if (value == null || value is System.Boolean)
								this.IsPrecursor = (System.Boolean?)value;
							break;
						case "IsMorphine":

							if (value == null || value is System.Boolean)
								this.IsMorphine = (System.Boolean?)value;
							break;
						case "IsHam":

							if (value == null || value is System.Boolean)
								this.IsHam = (System.Boolean?)value;
							break;
						case "IsLasa":

							if (value == null || value is System.Boolean)
								this.IsLasa = (System.Boolean?)value;
							break;
						case "IsOot":

							if (value == null || value is System.Boolean)
								this.IsOot = (System.Boolean?)value;
							break;
						case "IsSharePurchaseDiscToPatient":

							if (value == null || value is System.Boolean)
								this.IsSharePurchaseDiscToPatient = (System.Boolean?)value;
							break;
						case "IsFornas":

							if (value == null || value is System.Boolean)
								this.IsFornas = (System.Boolean?)value;
							break;
						case "IsOtc":

							if (value == null || value is System.Boolean)
								this.IsOtc = (System.Boolean?)value;
							break;
						case "IsHardDrug":

							if (value == null || value is System.Boolean)
								this.IsHardDrug = (System.Boolean?)value;
							break;
						case "IsTraditionalMedicine":

							if (value == null || value is System.Boolean)
								this.IsTraditionalMedicine = (System.Boolean?)value;
							break;
						case "IsSupplement":

							if (value == null || value is System.Boolean)
								this.IsSupplement = (System.Boolean?)value;
							break;
						case "IsMedication":

							if (value == null || value is System.Boolean)
								this.IsMedication = (System.Boolean?)value;
							break;
						case "ConsumptionLimitInDay":

							if (value == null || value is System.Int32)
								this.ConsumptionLimitInDay = (System.Int32?)value;
							break;
						case "IsNonGeneric":

							if (value == null || value is System.Boolean)
								this.IsNonGeneric = (System.Boolean?)value;
							break;
						case "IsAso":

							if (value == null || value is System.Boolean)
								this.IsAso = (System.Boolean?)value;
							break;
						case "IsNoPrescriptionFee":

							if (value == null || value is System.Boolean)
								this.IsNoPrescriptionFee = (System.Boolean?)value;
							break;
						case "IsPethidine":

							if (value == null || value is System.Boolean)
								this.IsPethidine = (System.Boolean?)value;
							break;
						case "IsNonGenericLimited":

							if (value == null || value is System.Boolean)
								this.IsNonGenericLimited = (System.Boolean?)value;
							break;
						case "IsChronic":

							if (value == null || value is System.Boolean)
								this.IsChronic = (System.Boolean?)value;
							break;
						case "BpjsMaxQtyOrderIpr":

							if (value == null || value is System.Int32)
								this.BpjsMaxQtyOrderIpr = (System.Int32?)value;
							break;
						case "BpjsMaxQtyOrderOpr":

							if (value == null || value is System.Int32)
								this.BpjsMaxQtyOrderOpr = (System.Int32?)value;
							break;
						case "BpjsMaxQtyOrderEmr":

							if (value == null || value is System.Int32)
								this.BpjsMaxQtyOrderEmr = (System.Int32?)value;
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
		/// Maps to ItemProductMedic.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.MarginID
		/// </summary>
		virtual public System.String MarginID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.MarginID);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.MarginID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRProductType
		/// </summary>
		virtual public System.String SRProductType
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRProductType);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRProductType, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.ABCClass
		/// </summary>
		virtual public System.String ABCClass
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.ABCClass);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.ABCClass, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.BrandName
		/// </summary>
		virtual public System.String BrandName
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.BrandName);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.BrandName, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRPurchaseUnit
		/// </summary>
		virtual public System.String SRPurchaseUnit
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRPurchaseUnit);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.ConversionFactor);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.Dosage
		/// </summary>
		virtual public System.Decimal? Dosage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.Dosage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.Dosage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRDosageUnit);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsFormularium
		/// </summary>
		virtual public System.Boolean? IsFormularium
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsFormularium);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsFormularium, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsInventoryItem
		/// </summary>
		virtual public System.Boolean? IsInventoryItem
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsInventoryItem);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsInventoryItem, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsControlExpired
		/// </summary>
		virtual public System.Boolean? IsControlExpired
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsControlExpired);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsControlExpired, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.FabricID
		/// </summary>
		virtual public System.String FabricID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.FabricID);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.FabricID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SalesFixedPrice
		/// </summary>
		virtual public System.Decimal? SalesFixedPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.SalesFixedPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.SalesFixedPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.MarginPercentage
		/// </summary>
		virtual public System.Decimal? MarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.MarginPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.MarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SalesDiscount
		/// </summary>
		virtual public System.Decimal? SalesDiscount
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.SalesDiscount);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.SalesDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PriceInPurchaseUnit
		/// </summary>
		virtual public System.Decimal? PriceInPurchaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceInPurchaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceInPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? PriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PriceInBasedUnitWVat
		/// </summary>
		virtual public System.Decimal? PriceInBasedUnitWVat
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceInBasedUnitWVat);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceInBasedUnitWVat, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.HighestPriceInBasedUnit
		/// </summary>
		virtual public System.Decimal? HighestPriceInBasedUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.HighestPriceInBasedUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.HighestPriceInBasedUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PurchaseDiscount1
		/// </summary>
		virtual public System.Decimal? PurchaseDiscount1
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PurchaseDiscount1);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PurchaseDiscount1, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PurchaseDiscount2
		/// </summary>
		virtual public System.Decimal? PurchaseDiscount2
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PurchaseDiscount2);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PurchaseDiscount2, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SafetyStock
		/// </summary>
		virtual public System.Decimal? SafetyStock
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.SafetyStock);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.SafetyStock, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SafetyTime
		/// </summary>
		virtual public System.Byte? SafetyTime
		{
			get
			{
				return base.GetSystemByte(ItemProductMedicMetadata.ColumnNames.SafetyTime);
			}

			set
			{
				base.SetSystemByte(ItemProductMedicMetadata.ColumnNames.SafetyTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.LeadTime
		/// </summary>
		virtual public System.Byte? LeadTime
		{
			get
			{
				return base.GetSystemByte(ItemProductMedicMetadata.ColumnNames.LeadTime);
			}

			set
			{
				base.SetSystemByte(ItemProductMedicMetadata.ColumnNames.LeadTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.TolerancePercentage
		/// </summary>
		virtual public System.Decimal? TolerancePercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.TolerancePercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.TolerancePercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsUsingCigna
		/// </summary>
		virtual public System.Boolean? IsUsingCigna
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsUsingCigna);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsUsingCigna, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.Barcode
		/// </summary>
		virtual public System.String Barcode
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.Barcode);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.Barcode, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRItemBin
		/// </summary>
		virtual public System.String SRItemBin
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRItemBin);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRItemBin, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRDrugLabelType
		/// </summary>
		virtual public System.String SRDrugLabelType
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRDrugLabelType);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRDrugLabelType, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsConsignment
		/// </summary>
		virtual public System.Boolean? IsConsignment
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsConsignment);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsConsignment, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.TherapyID
		/// </summary>
		virtual public System.String TherapyID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.TherapyID);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.TherapyID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsActualDeduct
		/// </summary>
		virtual public System.Boolean? IsActualDeduct
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsActualDeduct);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsActualDeduct, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMedicMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemProductMedicMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.isGeneric
		/// </summary>
		virtual public System.Boolean? IsGeneric
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsGeneric);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsGeneric, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PremiPharmaciesPercentage
		/// </summary>
		virtual public System.Decimal? PremiPharmaciesPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PremiPharmaciesPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PremiPharmaciesPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PremiPhysicianPercentage
		/// </summary>
		virtual public System.Decimal? PremiPhysicianPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PremiPhysicianPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PremiPhysicianPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.HET
		/// </summary>
		virtual public System.Decimal? HET
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.HET);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.HET, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRProductCategory
		/// </summary>
		virtual public System.String SRProductCategory
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRProductCategory);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRProductCategory, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.LastPriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? LastPriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.LastPriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.LastPriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsNarcotic
		/// </summary>
		virtual public System.Boolean? IsNarcotic
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNarcotic);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNarcotic, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsPsychotropic
		/// </summary>
		virtual public System.Boolean? IsPsychotropic
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsPsychotropic);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsPsychotropic, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsAntibiotic
		/// </summary>
		virtual public System.Boolean? IsAntibiotic
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsAntibiotic);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsAntibiotic, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsRegularItem
		/// </summary>
		virtual public System.Boolean? IsRegularItem
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsRegularItem);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsRegularItem, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsSalesAvailable
		/// </summary>
		virtual public System.Boolean? IsSalesAvailable
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsSalesAvailable);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsSalesAvailable, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsDirectPurchase
		/// </summary>
		virtual public System.Boolean? IsDirectPurchase
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsDirectPurchase);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsDirectPurchase, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.PriceWVat
		/// </summary>
		virtual public System.Decimal? PriceWVat
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceWVat);
			}

			set
			{
				base.SetSystemDecimal(ItemProductMedicMetadata.ColumnNames.PriceWVat, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsPrecursor
		/// </summary>
		virtual public System.Boolean? IsPrecursor
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsPrecursor);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsPrecursor, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRTherapyGroup
		/// </summary>
		virtual public System.String SRTherapyGroup
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRTherapyGroup);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRTherapyGroup, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.Keeping
		/// </summary>
		virtual public System.String Keeping
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.Keeping);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.Keeping, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsMorphine
		/// </summary>
		virtual public System.Boolean? IsMorphine
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsMorphine);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsMorphine, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRKeeping
		/// </summary>
		virtual public System.String SRKeeping
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRKeeping);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRKeeping, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.VENClass
		/// </summary>
		virtual public System.String VENClass
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.VENClass);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.VENClass, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsHam
		/// </summary>
		virtual public System.Boolean? IsHam
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsHam);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsHam, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsLasa
		/// </summary>
		virtual public System.Boolean? IsLasa
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsLasa);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsLasa, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsOot
		/// </summary>
		virtual public System.Boolean? IsOot
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsOot);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsOot, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsSharePurchaseDiscToPatient
		/// </summary>
		virtual public System.Boolean? IsSharePurchaseDiscToPatient
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsFornas
		/// </summary>
		virtual public System.Boolean? IsFornas
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsFornas);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsFornas, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsOtc
		/// </summary>
		virtual public System.Boolean? IsOtc
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsOtc);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsOtc, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsHardDrug
		/// </summary>
		virtual public System.Boolean? IsHardDrug
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsHardDrug);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsHardDrug, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsTraditionalMedicine
		/// </summary>
		virtual public System.Boolean? IsTraditionalMedicine
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsTraditionalMedicine);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsTraditionalMedicine, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsSupplement
		/// </summary>
		virtual public System.Boolean? IsSupplement
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsSupplement);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsSupplement, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRConsumeMethod
		/// </summary>
		virtual public System.String SRConsumeMethod
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRConsumeMethod);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsMedication
		/// </summary>
		virtual public System.Boolean? IsMedication
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsMedication);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsMedication, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.ConsumptionLimitInDay
		/// </summary>
		virtual public System.Int32? ConsumptionLimitInDay
		{
			get
			{
				return base.GetSystemInt32(ItemProductMedicMetadata.ColumnNames.ConsumptionLimitInDay);
			}

			set
			{
				base.SetSystemInt32(ItemProductMedicMetadata.ColumnNames.ConsumptionLimitInDay, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsNonGeneric
		/// </summary>
		virtual public System.Boolean? IsNonGeneric
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNonGeneric);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNonGeneric, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsAso
		/// </summary>
		virtual public System.Boolean? IsAso
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsAso);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsAso, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRRoute
		/// </summary>
		virtual public System.String SRRoute
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRRoute);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRRoute, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsNoPrescriptionFee
		/// </summary>
		virtual public System.Boolean? IsNoPrescriptionFee
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNoPrescriptionFee);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNoPrescriptionFee, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsPethidine
		/// </summary>
		virtual public System.Boolean? IsPethidine
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsPethidine);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsPethidine, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SRAntibioticLine
		/// </summary>
		virtual public System.String SRAntibioticLine
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SRAntibioticLine);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SRAntibioticLine, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsNonGenericLimited
		/// </summary>
		virtual public System.Boolean? IsNonGenericLimited
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNonGenericLimited);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsNonGenericLimited, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.SpecificInfo
		/// </summary>
		virtual public System.String SpecificInfo
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.SpecificInfo);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.SpecificInfo, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.FornasRestrictionNotes
		/// </summary>
		virtual public System.String FornasRestrictionNotes
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMetadata.ColumnNames.FornasRestrictionNotes);
			}

			set
			{
				base.SetSystemString(ItemProductMedicMetadata.ColumnNames.FornasRestrictionNotes, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.IsChronic
		/// </summary>
		virtual public System.Boolean? IsChronic
		{
			get
			{
				return base.GetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsChronic);
			}

			set
			{
				base.SetSystemBoolean(ItemProductMedicMetadata.ColumnNames.IsChronic, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.BpjsMaxQtyOrderIpr
		/// </summary>
		virtual public System.Int32? BpjsMaxQtyOrderIpr
		{
			get
			{
				return base.GetSystemInt32(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderIpr);
			}

			set
			{
				base.SetSystemInt32(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderIpr, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.BpjsMaxQtyOrderOpr
		/// </summary>
		virtual public System.Int32? BpjsMaxQtyOrderOpr
		{
			get
			{
				return base.GetSystemInt32(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderOpr);
			}

			set
			{
				base.SetSystemInt32(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderOpr, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductMedic.BpjsMaxQtyOrderEmr
		/// </summary>
		virtual public System.Int32? BpjsMaxQtyOrderEmr
		{
			get
			{
				return base.GetSystemInt32(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderEmr);
			}

			set
			{
				base.SetSystemInt32(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderEmr, value);
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
			public esStrings(esItemProductMedic entity)
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
			public System.String IsUsingCigna
			{
				get
				{
					System.Boolean? data = entity.IsUsingCigna;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingCigna = null;
					else entity.IsUsingCigna = Convert.ToBoolean(value);
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
			public System.String SRDrugLabelType
			{
				get
				{
					System.String data = entity.SRDrugLabelType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDrugLabelType = null;
					else entity.SRDrugLabelType = Convert.ToString(value);
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
			public System.String TherapyID
			{
				get
				{
					System.String data = entity.TherapyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TherapyID = null;
					else entity.TherapyID = Convert.ToString(value);
				}
			}
			public System.String IsActualDeduct
			{
				get
				{
					System.Boolean? data = entity.IsActualDeduct;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActualDeduct = null;
					else entity.IsActualDeduct = Convert.ToBoolean(value);
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
			public System.String IsGeneric
			{
				get
				{
					System.Boolean? data = entity.IsGeneric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGeneric = null;
					else entity.IsGeneric = Convert.ToBoolean(value);
				}
			}
			public System.String PremiPharmaciesPercentage
			{
				get
				{
					System.Decimal? data = entity.PremiPharmaciesPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PremiPharmaciesPercentage = null;
					else entity.PremiPharmaciesPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String PremiPhysicianPercentage
			{
				get
				{
					System.Decimal? data = entity.PremiPhysicianPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PremiPhysicianPercentage = null;
					else entity.PremiPhysicianPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String HET
			{
				get
				{
					System.Decimal? data = entity.HET;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HET = null;
					else entity.HET = Convert.ToDecimal(value);
				}
			}
			public System.String SRProductCategory
			{
				get
				{
					System.String data = entity.SRProductCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProductCategory = null;
					else entity.SRProductCategory = Convert.ToString(value);
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
			public System.String IsNarcotic
			{
				get
				{
					System.Boolean? data = entity.IsNarcotic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNarcotic = null;
					else entity.IsNarcotic = Convert.ToBoolean(value);
				}
			}
			public System.String IsPsychotropic
			{
				get
				{
					System.Boolean? data = entity.IsPsychotropic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPsychotropic = null;
					else entity.IsPsychotropic = Convert.ToBoolean(value);
				}
			}
			public System.String IsAntibiotic
			{
				get
				{
					System.Boolean? data = entity.IsAntibiotic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAntibiotic = null;
					else entity.IsAntibiotic = Convert.ToBoolean(value);
				}
			}
			public System.String IsRegularItem
			{
				get
				{
					System.Boolean? data = entity.IsRegularItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRegularItem = null;
					else entity.IsRegularItem = Convert.ToBoolean(value);
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
			public System.String IsDirectPurchase
			{
				get
				{
					System.Boolean? data = entity.IsDirectPurchase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDirectPurchase = null;
					else entity.IsDirectPurchase = Convert.ToBoolean(value);
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
			public System.String IsPrecursor
			{
				get
				{
					System.Boolean? data = entity.IsPrecursor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrecursor = null;
					else entity.IsPrecursor = Convert.ToBoolean(value);
				}
			}
			public System.String SRTherapyGroup
			{
				get
				{
					System.String data = entity.SRTherapyGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTherapyGroup = null;
					else entity.SRTherapyGroup = Convert.ToString(value);
				}
			}
			public System.String Keeping
			{
				get
				{
					System.String data = entity.Keeping;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Keeping = null;
					else entity.Keeping = Convert.ToString(value);
				}
			}
			public System.String IsMorphine
			{
				get
				{
					System.Boolean? data = entity.IsMorphine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMorphine = null;
					else entity.IsMorphine = Convert.ToBoolean(value);
				}
			}
			public System.String SRKeeping
			{
				get
				{
					System.String data = entity.SRKeeping;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRKeeping = null;
					else entity.SRKeeping = Convert.ToString(value);
				}
			}
			public System.String VENClass
			{
				get
				{
					System.String data = entity.VENClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VENClass = null;
					else entity.VENClass = Convert.ToString(value);
				}
			}
			public System.String IsHam
			{
				get
				{
					System.Boolean? data = entity.IsHam;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHam = null;
					else entity.IsHam = Convert.ToBoolean(value);
				}
			}
			public System.String IsLasa
			{
				get
				{
					System.Boolean? data = entity.IsLasa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLasa = null;
					else entity.IsLasa = Convert.ToBoolean(value);
				}
			}
			public System.String IsOot
			{
				get
				{
					System.Boolean? data = entity.IsOot;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOot = null;
					else entity.IsOot = Convert.ToBoolean(value);
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
			public System.String IsFornas
			{
				get
				{
					System.Boolean? data = entity.IsFornas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFornas = null;
					else entity.IsFornas = Convert.ToBoolean(value);
				}
			}
			public System.String IsOtc
			{
				get
				{
					System.Boolean? data = entity.IsOtc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOtc = null;
					else entity.IsOtc = Convert.ToBoolean(value);
				}
			}
			public System.String IsHardDrug
			{
				get
				{
					System.Boolean? data = entity.IsHardDrug;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHardDrug = null;
					else entity.IsHardDrug = Convert.ToBoolean(value);
				}
			}
			public System.String IsTraditionalMedicine
			{
				get
				{
					System.Boolean? data = entity.IsTraditionalMedicine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTraditionalMedicine = null;
					else entity.IsTraditionalMedicine = Convert.ToBoolean(value);
				}
			}
			public System.String IsSupplement
			{
				get
				{
					System.Boolean? data = entity.IsSupplement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSupplement = null;
					else entity.IsSupplement = Convert.ToBoolean(value);
				}
			}
			public System.String SRConsumeMethod
			{
				get
				{
					System.String data = entity.SRConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
					else entity.SRConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String IsMedication
			{
				get
				{
					System.Boolean? data = entity.IsMedication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMedication = null;
					else entity.IsMedication = Convert.ToBoolean(value);
				}
			}
			public System.String ConsumptionLimitInDay
			{
				get
				{
					System.Int32? data = entity.ConsumptionLimitInDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumptionLimitInDay = null;
					else entity.ConsumptionLimitInDay = Convert.ToInt32(value);
				}
			}
			public System.String IsNonGeneric
			{
				get
				{
					System.Boolean? data = entity.IsNonGeneric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonGeneric = null;
					else entity.IsNonGeneric = Convert.ToBoolean(value);
				}
			}
			public System.String IsAso
			{
				get
				{
					System.Boolean? data = entity.IsAso;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAso = null;
					else entity.IsAso = Convert.ToBoolean(value);
				}
			}
			public System.String SRRoute
			{
				get
				{
					System.String data = entity.SRRoute;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRoute = null;
					else entity.SRRoute = Convert.ToString(value);
				}
			}
			public System.String IsNoPrescriptionFee
			{
				get
				{
					System.Boolean? data = entity.IsNoPrescriptionFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNoPrescriptionFee = null;
					else entity.IsNoPrescriptionFee = Convert.ToBoolean(value);
				}
			}
			public System.String IsPethidine
			{
				get
				{
					System.Boolean? data = entity.IsPethidine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPethidine = null;
					else entity.IsPethidine = Convert.ToBoolean(value);
				}
			}
			public System.String SRAntibioticLine
			{
				get
				{
					System.String data = entity.SRAntibioticLine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAntibioticLine = null;
					else entity.SRAntibioticLine = Convert.ToString(value);
				}
			}
			public System.String IsNonGenericLimited
			{
				get
				{
					System.Boolean? data = entity.IsNonGenericLimited;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonGenericLimited = null;
					else entity.IsNonGenericLimited = Convert.ToBoolean(value);
				}
			}
			public System.String SpecificInfo
			{
				get
				{
					System.String data = entity.SpecificInfo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecificInfo = null;
					else entity.SpecificInfo = Convert.ToString(value);
				}
			}
			public System.String FornasRestrictionNotes
			{
				get
				{
					System.String data = entity.FornasRestrictionNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FornasRestrictionNotes = null;
					else entity.FornasRestrictionNotes = Convert.ToString(value);
				}
			}
			public System.String IsChronic
			{
				get
				{
					System.Boolean? data = entity.IsChronic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsChronic = null;
					else entity.IsChronic = Convert.ToBoolean(value);
				}
			}
			public System.String BpjsMaxQtyOrderIpr
			{
				get
				{
					System.Int32? data = entity.BpjsMaxQtyOrderIpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsMaxQtyOrderIpr = null;
					else entity.BpjsMaxQtyOrderIpr = Convert.ToInt32(value);
				}
			}
			public System.String BpjsMaxQtyOrderOpr
			{
				get
				{
					System.Int32? data = entity.BpjsMaxQtyOrderOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsMaxQtyOrderOpr = null;
					else entity.BpjsMaxQtyOrderOpr = Convert.ToInt32(value);
				}
			}
			public System.String BpjsMaxQtyOrderEmr
			{
				get
				{
					System.Int32? data = entity.BpjsMaxQtyOrderEmr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsMaxQtyOrderEmr = null;
					else entity.BpjsMaxQtyOrderEmr = Convert.ToInt32(value);
				}
			}
			private esItemProductMedic entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductMedicQuery query)
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
				throw new Exception("esItemProductMedic can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemProductMedic : esItemProductMedic
	{
	}

	[Serializable]
	abstract public class esItemProductMedicQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicMetadata.Meta();
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem MarginID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.MarginID, esSystemType.String);
			}
		}

		public esQueryItem SRProductType
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRProductType, esSystemType.String);
			}
		}

		public esQueryItem ABCClass
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.ABCClass, esSystemType.String);
			}
		}

		public esQueryItem BrandName
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.BrandName, esSystemType.String);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem SRPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRPurchaseUnit, esSystemType.String);
			}
		}

		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		}

		public esQueryItem Dosage
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.Dosage, esSystemType.Decimal);
			}
		}

		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		}

		public esQueryItem IsFormularium
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsFormularium, esSystemType.Boolean);
			}
		}

		public esQueryItem IsInventoryItem
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsInventoryItem, esSystemType.Boolean);
			}
		}

		public esQueryItem IsControlExpired
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsControlExpired, esSystemType.Boolean);
			}
		}

		public esQueryItem FabricID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.FabricID, esSystemType.String);
			}
		}

		public esQueryItem SalesFixedPrice
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SalesFixedPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem MarginPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.MarginPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem SalesDiscount
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SalesDiscount, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBasedUnitWVat
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PriceInBasedUnitWVat, esSystemType.Decimal);
			}
		}

		public esQueryItem HighestPriceInBasedUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.HighestPriceInBasedUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem PurchaseDiscount1
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PurchaseDiscount1, esSystemType.Decimal);
			}
		}

		public esQueryItem PurchaseDiscount2
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PurchaseDiscount2, esSystemType.Decimal);
			}
		}

		public esQueryItem SafetyStock
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SafetyStock, esSystemType.Decimal);
			}
		}

		public esQueryItem SafetyTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SafetyTime, esSystemType.Byte);
			}
		}

		public esQueryItem LeadTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.LeadTime, esSystemType.Byte);
			}
		}

		public esQueryItem TolerancePercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.TolerancePercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsUsingCigna
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsUsingCigna, esSystemType.Boolean);
			}
		}

		public esQueryItem Barcode
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.Barcode, esSystemType.String);
			}
		}

		public esQueryItem SRItemBin
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRItemBin, esSystemType.String);
			}
		}

		public esQueryItem SRDrugLabelType
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRDrugLabelType, esSystemType.String);
			}
		}

		public esQueryItem IsConsignment
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsConsignment, esSystemType.Boolean);
			}
		}

		public esQueryItem TherapyID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.TherapyID, esSystemType.String);
			}
		}

		public esQueryItem IsActualDeduct
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsActualDeduct, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsGeneric
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsGeneric, esSystemType.Boolean);
			}
		}

		public esQueryItem PremiPharmaciesPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PremiPharmaciesPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem PremiPhysicianPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PremiPhysicianPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem HET
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.HET, esSystemType.Decimal);
			}
		}

		public esQueryItem SRProductCategory
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRProductCategory, esSystemType.String);
			}
		}

		public esQueryItem LastPriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.LastPriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem IsNarcotic
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsNarcotic, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPsychotropic
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsPsychotropic, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAntibiotic
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsAntibiotic, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRegularItem
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsRegularItem, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSalesAvailable
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsSalesAvailable, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDirectPurchase
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsDirectPurchase, esSystemType.Boolean);
			}
		}

		public esQueryItem PriceWVat
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.PriceWVat, esSystemType.Decimal);
			}
		}

		public esQueryItem IsPrecursor
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsPrecursor, esSystemType.Boolean);
			}
		}

		public esQueryItem SRTherapyGroup
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRTherapyGroup, esSystemType.String);
			}
		}

		public esQueryItem Keeping
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.Keeping, esSystemType.String);
			}
		}

		public esQueryItem IsMorphine
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsMorphine, esSystemType.Boolean);
			}
		}

		public esQueryItem SRKeeping
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRKeeping, esSystemType.String);
			}
		}

		public esQueryItem VENClass
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.VENClass, esSystemType.String);
			}
		}

		public esQueryItem IsHam
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsHam, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLasa
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsLasa, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOot
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsOot, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSharePurchaseDiscToPatient
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFornas
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsFornas, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOtc
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsOtc, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHardDrug
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsHardDrug, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTraditionalMedicine
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsTraditionalMedicine, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSupplement
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsSupplement, esSystemType.Boolean);
			}
		}

		public esQueryItem SRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem IsMedication
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsMedication, esSystemType.Boolean);
			}
		}

		public esQueryItem ConsumptionLimitInDay
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.ConsumptionLimitInDay, esSystemType.Int32);
			}
		}

		public esQueryItem IsNonGeneric
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsNonGeneric, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAso
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsAso, esSystemType.Boolean);
			}
		}

		public esQueryItem SRRoute
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRRoute, esSystemType.String);
			}
		}

		public esQueryItem IsNoPrescriptionFee
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsNoPrescriptionFee, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPethidine
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsPethidine, esSystemType.Boolean);
			}
		}

		public esQueryItem SRAntibioticLine
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SRAntibioticLine, esSystemType.String);
			}
		}

		public esQueryItem IsNonGenericLimited
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsNonGenericLimited, esSystemType.Boolean);
			}
		}

		public esQueryItem SpecificInfo
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.SpecificInfo, esSystemType.String);
			}
		}

		public esQueryItem FornasRestrictionNotes
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.FornasRestrictionNotes, esSystemType.String);
			}
		}

		public esQueryItem IsChronic
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.IsChronic, esSystemType.Boolean);
			}
		}

		public esQueryItem BpjsMaxQtyOrderIpr
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderIpr, esSystemType.Int32);
			}
		}

		public esQueryItem BpjsMaxQtyOrderOpr
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderOpr, esSystemType.Int32);
			}
		}

		public esQueryItem BpjsMaxQtyOrderEmr
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderEmr, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductMedicCollection")]
	public partial class ItemProductMedicCollection : esItemProductMedicCollection, IEnumerable<ItemProductMedic>
	{
		public ItemProductMedicCollection()
		{

		}

		public static implicit operator List<ItemProductMedic>(ItemProductMedicCollection coll)
		{
			List<ItemProductMedic> list = new List<ItemProductMedic>();

			foreach (ItemProductMedic emp in coll)
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
				return ItemProductMedicMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductMedic(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductMedic();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemProductMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicQuery();
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
		public bool Load(ItemProductMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemProductMedic AddNew()
		{
			ItemProductMedic entity = base.AddNewEntity() as ItemProductMedic;

			return entity;
		}
		public ItemProductMedic FindByPrimaryKey(String itemID)
		{
			return base.FindByPrimaryKey(itemID) as ItemProductMedic;
		}

		#region IEnumerable< ItemProductMedic> Members

		IEnumerator<ItemProductMedic> IEnumerable<ItemProductMedic>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductMedic;
			}
		}

		#endregion

		private ItemProductMedicQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductMedic' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemProductMedic ({ItemID})")]
	[Serializable]
	public partial class ItemProductMedic : esItemProductMedic
	{
		public ItemProductMedic()
		{
		}

		public ItemProductMedic(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicMetadata.Meta();
			}
		}

		override protected esItemProductMedicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemProductMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicQuery();
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
		public bool Load(ItemProductMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemProductMedicQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemProductMedicQuery : esItemProductMedicQuery
	{
		public ItemProductMedicQuery()
		{

		}

		public ItemProductMedicQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemProductMedicQuery";
		}
	}

	[Serializable]
	public partial class ItemProductMedicMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductMedicMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.MarginID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.MarginID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRProductType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRProductType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.ABCClass, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.ABCClass;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.BrandName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.BrandName;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRItemUnit, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRPurchaseUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRPurchaseUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.ConversionFactor, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.Dosage, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.Dosage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRDosageUnit, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRDosageUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsFormularium, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsFormularium;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsInventoryItem, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsInventoryItem;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsControlExpired, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsControlExpired;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.FabricID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.FabricID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SalesFixedPrice, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SalesFixedPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.MarginPercentage, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.MarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SalesDiscount, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SalesDiscount;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PriceInPurchaseUnit, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PriceInPurchaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PriceInBaseUnit, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PriceInBasedUnitWVat, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PriceInBasedUnitWVat;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.HighestPriceInBasedUnit, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.HighestPriceInBasedUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.CostPrice, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PurchaseDiscount1, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PurchaseDiscount1;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PurchaseDiscount2, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PurchaseDiscount2;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SafetyStock, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SafetyStock;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SafetyTime, 25, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SafetyTime;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.LeadTime, 26, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.LeadTime;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.TolerancePercentage, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.TolerancePercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsUsingCigna, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsUsingCigna;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.Barcode, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.Barcode;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRItemBin, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRItemBin;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRDrugLabelType, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRDrugLabelType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsConsignment, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsConsignment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.TherapyID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.TherapyID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsActualDeduct, 34, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsActualDeduct;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.LastUpdateDateTime, 35, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.LastUpdateByUserID, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsGeneric, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsGeneric;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PremiPharmaciesPercentage, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PremiPharmaciesPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PremiPhysicianPercentage, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PremiPhysicianPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.HET, 40, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.HET;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRProductCategory, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRProductCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.LastPriceInBaseUnit, 42, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.LastPriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsNarcotic, 43, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsNarcotic;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsPsychotropic, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsPsychotropic;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsAntibiotic, 45, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsAntibiotic;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsRegularItem, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsRegularItem;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsSalesAvailable, 47, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsSalesAvailable;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsDirectPurchase, 48, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsDirectPurchase;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.PriceWVat, 49, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.PriceWVat;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsPrecursor, 50, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsPrecursor;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRTherapyGroup, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRTherapyGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.Keeping, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.Keeping;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsMorphine, 53, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsMorphine;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRKeeping, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRKeeping;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.VENClass, 55, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.VENClass;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsHam, 56, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsHam;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsLasa, 57, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsLasa;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsOot, 58, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsOot;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsSharePurchaseDiscToPatient, 59, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsSharePurchaseDiscToPatient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsFornas, 60, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsFornas;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsOtc, 61, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsOtc;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsHardDrug, 62, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsHardDrug;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsTraditionalMedicine, 63, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsTraditionalMedicine;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsSupplement, 64, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsSupplement;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRConsumeMethod, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRConsumeMethod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsMedication, 66, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsMedication;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.ConsumptionLimitInDay, 67, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.ConsumptionLimitInDay;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsNonGeneric, 68, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsNonGeneric;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsAso, 69, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsAso;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRRoute, 70, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRRoute;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsNoPrescriptionFee, 71, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsNoPrescriptionFee;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsPethidine, 72, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsPethidine;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SRAntibioticLine, 73, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SRAntibioticLine;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsNonGenericLimited, 74, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsNonGenericLimited;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.SpecificInfo, 75, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.SpecificInfo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.FornasRestrictionNotes, 76, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.FornasRestrictionNotes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.IsChronic, 77, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.IsChronic;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderIpr, 78, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.BpjsMaxQtyOrderIpr;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderOpr, 79, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.BpjsMaxQtyOrderOpr;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductMedicMetadata.ColumnNames.BpjsMaxQtyOrderEmr, 80, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemProductMedicMetadata.PropertyNames.BpjsMaxQtyOrderEmr;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemProductMedicMetadata Meta()
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
			public const string IsUsingCigna = "IsUsingCigna";
			public const string Barcode = "Barcode";
			public const string SRItemBin = "SRItemBin";
			public const string SRDrugLabelType = "SRDrugLabelType";
			public const string IsConsignment = "IsConsignment";
			public const string TherapyID = "TherapyID";
			public const string IsActualDeduct = "IsActualDeduct";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsGeneric = "isGeneric";
			public const string PremiPharmaciesPercentage = "PremiPharmaciesPercentage";
			public const string PremiPhysicianPercentage = "PremiPhysicianPercentage";
			public const string HET = "HET";
			public const string SRProductCategory = "SRProductCategory";
			public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
			public const string IsNarcotic = "IsNarcotic";
			public const string IsPsychotropic = "IsPsychotropic";
			public const string IsAntibiotic = "IsAntibiotic";
			public const string IsRegularItem = "IsRegularItem";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string IsDirectPurchase = "IsDirectPurchase";
			public const string PriceWVat = "PriceWVat";
			public const string IsPrecursor = "IsPrecursor";
			public const string SRTherapyGroup = "SRTherapyGroup";
			public const string Keeping = "Keeping";
			public const string IsMorphine = "IsMorphine";
			public const string SRKeeping = "SRKeeping";
			public const string VENClass = "VENClass";
			public const string IsHam = "IsHam";
			public const string IsLasa = "IsLasa";
			public const string IsOot = "IsOot";
			public const string IsSharePurchaseDiscToPatient = "IsSharePurchaseDiscToPatient";
			public const string IsFornas = "IsFornas";
			public const string IsOtc = "IsOtc";
			public const string IsHardDrug = "IsHardDrug";
			public const string IsTraditionalMedicine = "IsTraditionalMedicine";
			public const string IsSupplement = "IsSupplement";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string IsMedication = "IsMedication";
			public const string ConsumptionLimitInDay = "ConsumptionLimitInDay";
			public const string IsNonGeneric = "IsNonGeneric";
			public const string IsAso = "IsAso";
			public const string SRRoute = "SRRoute";
			public const string IsNoPrescriptionFee = "IsNoPrescriptionFee";
			public const string IsPethidine = "IsPethidine";
			public const string SRAntibioticLine = "SRAntibioticLine";
			public const string IsNonGenericLimited = "IsNonGenericLimited";
			public const string SpecificInfo = "SpecificInfo";
			public const string FornasRestrictionNotes = "FornasRestrictionNotes";
			public const string IsChronic = "IsChronic";
			public const string BpjsMaxQtyOrderIpr = "BpjsMaxQtyOrderIpr";
			public const string BpjsMaxQtyOrderOpr = "BpjsMaxQtyOrderOpr";
			public const string BpjsMaxQtyOrderEmr = "BpjsMaxQtyOrderEmr";
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
			public const string IsUsingCigna = "IsUsingCigna";
			public const string Barcode = "Barcode";
			public const string SRItemBin = "SRItemBin";
			public const string SRDrugLabelType = "SRDrugLabelType";
			public const string IsConsignment = "IsConsignment";
			public const string TherapyID = "TherapyID";
			public const string IsActualDeduct = "IsActualDeduct";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsGeneric = "IsGeneric";
			public const string PremiPharmaciesPercentage = "PremiPharmaciesPercentage";
			public const string PremiPhysicianPercentage = "PremiPhysicianPercentage";
			public const string HET = "HET";
			public const string SRProductCategory = "SRProductCategory";
			public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
			public const string IsNarcotic = "IsNarcotic";
			public const string IsPsychotropic = "IsPsychotropic";
			public const string IsAntibiotic = "IsAntibiotic";
			public const string IsRegularItem = "IsRegularItem";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string IsDirectPurchase = "IsDirectPurchase";
			public const string PriceWVat = "PriceWVat";
			public const string IsPrecursor = "IsPrecursor";
			public const string SRTherapyGroup = "SRTherapyGroup";
			public const string Keeping = "Keeping";
			public const string IsMorphine = "IsMorphine";
			public const string SRKeeping = "SRKeeping";
			public const string VENClass = "VENClass";
			public const string IsHam = "IsHam";
			public const string IsLasa = "IsLasa";
			public const string IsOot = "IsOot";
			public const string IsSharePurchaseDiscToPatient = "IsSharePurchaseDiscToPatient";
			public const string IsFornas = "IsFornas";
			public const string IsOtc = "IsOtc";
			public const string IsHardDrug = "IsHardDrug";
			public const string IsTraditionalMedicine = "IsTraditionalMedicine";
			public const string IsSupplement = "IsSupplement";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string IsMedication = "IsMedication";
			public const string ConsumptionLimitInDay = "ConsumptionLimitInDay";
			public const string IsNonGeneric = "IsNonGeneric";
			public const string IsAso = "IsAso";
			public const string SRRoute = "SRRoute";
			public const string IsNoPrescriptionFee = "IsNoPrescriptionFee";
			public const string IsPethidine = "IsPethidine";
			public const string SRAntibioticLine = "SRAntibioticLine";
			public const string IsNonGenericLimited = "IsNonGenericLimited";
			public const string SpecificInfo = "SpecificInfo";
			public const string FornasRestrictionNotes = "FornasRestrictionNotes";
			public const string IsChronic = "IsChronic";
			public const string BpjsMaxQtyOrderIpr = "BpjsMaxQtyOrderIpr";
			public const string BpjsMaxQtyOrderOpr = "BpjsMaxQtyOrderOpr";
			public const string BpjsMaxQtyOrderEmr = "BpjsMaxQtyOrderEmr";
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
			lock (typeof(ItemProductMedicMetadata))
			{
				if (ItemProductMedicMetadata.mapDelegates == null)
				{
					ItemProductMedicMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemProductMedicMetadata.meta == null)
				{
					ItemProductMedicMetadata.meta = new ItemProductMedicMetadata();
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
				meta.AddTypeMap("IsUsingCigna", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Barcode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemBin", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDrugLabelType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConsignment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TherapyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActualDeduct", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGeneric", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PremiPharmaciesPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PremiPhysicianPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HET", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRProductCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastPriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsNarcotic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPsychotropic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAntibiotic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRegularItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSalesAvailable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDirectPurchase", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PriceWVat", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPrecursor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRTherapyGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Keeping", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMorphine", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRKeeping", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VENClass", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("IsHam", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLasa", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOot", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSharePurchaseDiscToPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFornas", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOtc", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHardDrug", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTraditionalMedicine", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSupplement", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMedication", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ConsumptionLimitInDay", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsNonGeneric", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAso", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRRoute", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNoPrescriptionFee", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPethidine", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRAntibioticLine", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNonGenericLimited", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SpecificInfo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FornasRestrictionNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsChronic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BpjsMaxQtyOrderIpr", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BpjsMaxQtyOrderOpr", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BpjsMaxQtyOrderEmr", new esTypeMap("int", "System.Int32"));


				meta.Source = "ItemProductMedic";
				meta.Destination = "ItemProductMedic";
				meta.spInsert = "proc_ItemProductMedicInsert";
				meta.spUpdate = "proc_ItemProductMedicUpdate";
				meta.spDelete = "proc_ItemProductMedicDelete";
				meta.spLoadAll = "proc_ItemProductMedicLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductMedicLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductMedicMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
