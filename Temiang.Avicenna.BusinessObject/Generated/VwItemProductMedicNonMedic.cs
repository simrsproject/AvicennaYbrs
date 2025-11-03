/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/8/2023 2:21:43 PM
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
	abstract public class esVwItemProductMedicNonMedicCollection : esEntityCollection
	{
		public esVwItemProductMedicNonMedicCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "VwItemProductMedicNonMedicCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemProductMedicNonMedicQuery query)
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
			this.InitQuery(query as esVwItemProductMedicNonMedicQuery);
		}
		#endregion

		virtual public VwItemProductMedicNonMedic DetachEntity(VwItemProductMedicNonMedic entity)
		{
			return base.DetachEntity(entity) as VwItemProductMedicNonMedic;
		}

		virtual public VwItemProductMedicNonMedic AttachEntity(VwItemProductMedicNonMedic entity)
		{
			return base.AttachEntity(entity) as VwItemProductMedicNonMedic;
		}

		virtual public void Combine(VwItemProductMedicNonMedicCollection collection)
		{
			base.Combine(collection);
		}

		new public VwItemProductMedicNonMedic this[int index]
		{
			get
			{
				return base[index] as VwItemProductMedicNonMedic;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemProductMedicNonMedic);
		}
	}

	[Serializable]
	abstract public class esVwItemProductMedicNonMedic : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemProductMedicNonMedicQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemProductMedicNonMedic()
		{
		}

		public esVwItemProductMedicNonMedic(DataRow row)
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
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "SRPurchaseUnit": this.str.SRPurchaseUnit = (string)value; break;
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
						case "IsInventoryItem": this.str.IsInventoryItem = (string)value; break;
						case "PriceInPurchaseUnit": this.str.PriceInPurchaseUnit = (string)value; break;
						case "PriceInBaseUnit": this.str.PriceInBaseUnit = (string)value; break;
						case "PriceInBasedUnitWVat": this.str.PriceInBasedUnitWVat = (string)value; break;
						case "HighestPriceInBasedUnit": this.str.HighestPriceInBasedUnit = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "IsRegularItem": this.str.IsRegularItem = (string)value; break;
						case "IsSalesAvailable": this.str.IsSalesAvailable = (string)value; break;
						case "BrandName": this.str.BrandName = (string)value; break;
						case "Dosage": this.str.Dosage = (string)value; break;
						case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;
						case "IsUsingCigna": this.str.IsUsingCigna = (string)value; break;
						case "SRDrugLabelType": this.str.SRDrugLabelType = (string)value; break;
						case "GenericFlag": this.str.GenericFlag = (string)value; break;
						case "IsControlExpired": this.str.IsControlExpired = (string)value; break;
						case "IsFornas": this.str.IsFornas = (string)value; break;
						case "IsFormularium": this.str.IsFormularium = (string)value; break;
						case "Barcode": this.str.Barcode = (string)value; break;
						case "IsAntibiotic": this.str.IsAntibiotic = (string)value; break;
						case "IsMedication": this.str.IsMedication = (string)value; break;
						case "IsGeneric": this.str.IsGeneric = (string)value; break;
						case "IsNonGeneric": this.str.IsNonGeneric = (string)value; break;
						case "IsNonGenericLimited": this.str.IsNonGenericLimited = (string)value; break;
						case "SRAntibioticLine": this.str.SRAntibioticLine = (string)value; break;
						case "SpecificInfo": this.str.SpecificInfo = (string)value; break;
						case "FornasRestrictionNotes": this.str.FornasRestrictionNotes = (string)value; break;
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
						case "IsInventoryItem":

							if (value == null || value is System.Boolean)
								this.IsInventoryItem = (System.Boolean?)value;
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
						case "IsRegularItem":

							if (value == null || value is System.Int32)
								this.IsRegularItem = (System.Int32?)value;
							break;
						case "IsSalesAvailable":

							if (value == null || value is System.Boolean)
								this.IsSalesAvailable = (System.Boolean?)value;
							break;
						case "Dosage":

							if (value == null || value is System.Decimal)
								this.Dosage = (System.Decimal?)value;
							break;
						case "IsUsingCigna":

							if (value == null || value is System.Int32)
								this.IsUsingCigna = (System.Int32?)value;
							break;
						case "IsControlExpired":

							if (value == null || value is System.Boolean)
								this.IsControlExpired = (System.Boolean?)value;
							break;
						case "IsFornas":

							if (value == null || value is System.Int32)
								this.IsFornas = (System.Int32?)value;
							break;
						case "IsFormularium":

							if (value == null || value is System.Int32)
								this.IsFormularium = (System.Int32?)value;
							break;
						case "IsAntibiotic":

							if (value == null || value is System.Int32)
								this.IsAntibiotic = (System.Int32?)value;
							break;
						case "IsMedication":

							if (value == null || value is System.Int32)
								this.IsMedication = (System.Int32?)value;
							break;
						case "IsGeneric":

							if (value == null || value is System.Int32)
								this.IsGeneric = (System.Int32?)value;
							break;
						case "IsNonGeneric":

							if (value == null || value is System.Int32)
								this.IsNonGeneric = (System.Int32?)value;
							break;
						case "IsNonGenericLimited":

							if (value == null || value is System.Int32)
								this.IsNonGenericLimited = (System.Int32?)value;
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
		/// Maps to VwItemProductMedicNonMedic.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.SRPurchaseUnit
		/// </summary>
		virtual public System.String SRPurchaseUnit
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRPurchaseUnit);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.ConversionFactor);
			}

			set
			{
				base.SetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsInventoryItem
		/// </summary>
		virtual public System.Boolean? IsInventoryItem
		{
			get
			{
				return base.GetSystemBoolean(VwItemProductMedicNonMedicMetadata.ColumnNames.IsInventoryItem);
			}

			set
			{
				base.SetSystemBoolean(VwItemProductMedicNonMedicMetadata.ColumnNames.IsInventoryItem, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.PriceInPurchaseUnit
		/// </summary>
		virtual public System.Decimal? PriceInPurchaseUnit
		{
			get
			{
				return base.GetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInPurchaseUnit);
			}

			set
			{
				base.SetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.PriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? PriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.PriceInBasedUnitWVat
		/// </summary>
		virtual public System.Decimal? PriceInBasedUnitWVat
		{
			get
			{
				return base.GetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat);
			}

			set
			{
				base.SetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.HighestPriceInBasedUnit
		/// </summary>
		virtual public System.Decimal? HighestPriceInBasedUnit
		{
			get
			{
				return base.GetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit);
			}

			set
			{
				base.SetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsRegularItem
		/// </summary>
		virtual public System.Int32? IsRegularItem
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsRegularItem);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsRegularItem, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsSalesAvailable
		/// </summary>
		virtual public System.Boolean? IsSalesAvailable
		{
			get
			{
				return base.GetSystemBoolean(VwItemProductMedicNonMedicMetadata.ColumnNames.IsSalesAvailable);
			}

			set
			{
				base.SetSystemBoolean(VwItemProductMedicNonMedicMetadata.ColumnNames.IsSalesAvailable, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.BrandName
		/// </summary>
		virtual public System.String BrandName
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.BrandName);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.BrandName, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.Dosage
		/// </summary>
		virtual public System.Decimal? Dosage
		{
			get
			{
				return base.GetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.Dosage);
			}

			set
			{
				base.SetSystemDecimal(VwItemProductMedicNonMedicMetadata.ColumnNames.Dosage, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRDosageUnit);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsUsingCigna
		/// </summary>
		virtual public System.Int32? IsUsingCigna
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsUsingCigna);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsUsingCigna, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.SRDrugLabelType
		/// </summary>
		virtual public System.String SRDrugLabelType
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRDrugLabelType);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRDrugLabelType, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.GenericFlag
		/// </summary>
		virtual public System.String GenericFlag
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.GenericFlag);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.GenericFlag, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsControlExpired
		/// </summary>
		virtual public System.Boolean? IsControlExpired
		{
			get
			{
				return base.GetSystemBoolean(VwItemProductMedicNonMedicMetadata.ColumnNames.IsControlExpired);
			}

			set
			{
				base.SetSystemBoolean(VwItemProductMedicNonMedicMetadata.ColumnNames.IsControlExpired, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsFornas
		/// </summary>
		virtual public System.Int32? IsFornas
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsFornas);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsFornas, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsFormularium
		/// </summary>
		virtual public System.Int32? IsFormularium
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsFormularium);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsFormularium, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.Barcode
		/// </summary>
		virtual public System.String Barcode
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.Barcode);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.Barcode, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsAntibiotic
		/// </summary>
		virtual public System.Int32? IsAntibiotic
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsAntibiotic);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsAntibiotic, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsMedication
		/// </summary>
		virtual public System.Int32? IsMedication
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsMedication);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsMedication, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsGeneric
		/// </summary>
		virtual public System.Int32? IsGeneric
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsGeneric);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsGeneric, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsNonGeneric
		/// </summary>
		virtual public System.Int32? IsNonGeneric
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGeneric);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGeneric, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.IsNonGenericLimited
		/// </summary>
		virtual public System.Int32? IsNonGenericLimited
		{
			get
			{
				return base.GetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGenericLimited);
			}

			set
			{
				base.SetSystemInt32(VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGenericLimited, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.SRAntibioticLine
		/// </summary>
		virtual public System.String SRAntibioticLine
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRAntibioticLine);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SRAntibioticLine, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.SpecificInfo
		/// </summary>
		virtual public System.String SpecificInfo
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SpecificInfo);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.SpecificInfo, value);
			}
		}
		/// <summary>
		/// Maps to VwItemProductMedicNonMedic.FornasRestrictionNotes
		/// </summary>
		virtual public System.String FornasRestrictionNotes
		{
			get
			{
				return base.GetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.FornasRestrictionNotes);
			}

			set
			{
				base.SetSystemString(VwItemProductMedicNonMedicMetadata.ColumnNames.FornasRestrictionNotes, value);
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
			public esStrings(esVwItemProductMedicNonMedic entity)
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
			public System.String IsRegularItem
			{
				get
				{
					System.Int32? data = entity.IsRegularItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRegularItem = null;
					else entity.IsRegularItem = Convert.ToInt32(value);
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
			public System.String IsUsingCigna
			{
				get
				{
					System.Int32? data = entity.IsUsingCigna;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingCigna = null;
					else entity.IsUsingCigna = Convert.ToInt32(value);
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
			public System.String GenericFlag
			{
				get
				{
					System.String data = entity.GenericFlag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GenericFlag = null;
					else entity.GenericFlag = Convert.ToString(value);
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
			public System.String IsFornas
			{
				get
				{
					System.Int32? data = entity.IsFornas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFornas = null;
					else entity.IsFornas = Convert.ToInt32(value);
				}
			}
			public System.String IsFormularium
			{
				get
				{
					System.Int32? data = entity.IsFormularium;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFormularium = null;
					else entity.IsFormularium = Convert.ToInt32(value);
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
			public System.String IsAntibiotic
			{
				get
				{
					System.Int32? data = entity.IsAntibiotic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAntibiotic = null;
					else entity.IsAntibiotic = Convert.ToInt32(value);
				}
			}
			public System.String IsMedication
			{
				get
				{
					System.Int32? data = entity.IsMedication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMedication = null;
					else entity.IsMedication = Convert.ToInt32(value);
				}
			}
			public System.String IsGeneric
			{
				get
				{
					System.Int32? data = entity.IsGeneric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGeneric = null;
					else entity.IsGeneric = Convert.ToInt32(value);
				}
			}
			public System.String IsNonGeneric
			{
				get
				{
					System.Int32? data = entity.IsNonGeneric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonGeneric = null;
					else entity.IsNonGeneric = Convert.ToInt32(value);
				}
			}
			public System.String IsNonGenericLimited
			{
				get
				{
					System.Int32? data = entity.IsNonGenericLimited;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNonGenericLimited = null;
					else entity.IsNonGenericLimited = Convert.ToInt32(value);
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
			private esVwItemProductMedicNonMedic entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemProductMedicNonMedicQuery query)
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
				throw new Exception("esVwItemProductMedicNonMedic can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VwItemProductMedicNonMedic : esVwItemProductMedicNonMedic
	{
	}

	[Serializable]
	abstract public class esVwItemProductMedicNonMedicQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return VwItemProductMedicNonMedicMetadata.Meta();
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem SRPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.SRPurchaseUnit, esSystemType.String);
			}
		}

		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		}

		public esQueryItem IsInventoryItem
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsInventoryItem, esSystemType.Boolean);
			}
		}

		public esQueryItem PriceInPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInPurchaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBasedUnitWVat
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat, esSystemType.Decimal);
			}
		}

		public esQueryItem HighestPriceInBasedUnit
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem IsRegularItem
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsRegularItem, esSystemType.Int32);
			}
		}

		public esQueryItem IsSalesAvailable
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsSalesAvailable, esSystemType.Boolean);
			}
		}

		public esQueryItem BrandName
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.BrandName, esSystemType.String);
			}
		}

		public esQueryItem Dosage
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.Dosage, esSystemType.Decimal);
			}
		}

		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		}

		public esQueryItem IsUsingCigna
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsUsingCigna, esSystemType.Int32);
			}
		}

		public esQueryItem SRDrugLabelType
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.SRDrugLabelType, esSystemType.String);
			}
		}

		public esQueryItem GenericFlag
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.GenericFlag, esSystemType.String);
			}
		}

		public esQueryItem IsControlExpired
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsControlExpired, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFornas
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsFornas, esSystemType.Int32);
			}
		}

		public esQueryItem IsFormularium
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsFormularium, esSystemType.Int32);
			}
		}

		public esQueryItem Barcode
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.Barcode, esSystemType.String);
			}
		}

		public esQueryItem IsAntibiotic
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsAntibiotic, esSystemType.Int32);
			}
		}

		public esQueryItem IsMedication
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsMedication, esSystemType.Int32);
			}
		}

		public esQueryItem IsGeneric
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsGeneric, esSystemType.Int32);
			}
		}

		public esQueryItem IsNonGeneric
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGeneric, esSystemType.Int32);
			}
		}

		public esQueryItem IsNonGenericLimited
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGenericLimited, esSystemType.Int32);
			}
		}

		public esQueryItem SRAntibioticLine
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.SRAntibioticLine, esSystemType.String);
			}
		}

		public esQueryItem SpecificInfo
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.SpecificInfo, esSystemType.String);
			}
		}

		public esQueryItem FornasRestrictionNotes
		{
			get
			{
				return new esQueryItem(this, VwItemProductMedicNonMedicMetadata.ColumnNames.FornasRestrictionNotes, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemProductMedicNonMedicCollection")]
	public partial class VwItemProductMedicNonMedicCollection : esVwItemProductMedicNonMedicCollection, IEnumerable<VwItemProductMedicNonMedic>
	{
		public VwItemProductMedicNonMedicCollection()
		{

		}

		public static implicit operator List<VwItemProductMedicNonMedic>(VwItemProductMedicNonMedicCollection coll)
		{
			List<VwItemProductMedicNonMedic> list = new List<VwItemProductMedicNonMedic>();

			foreach (VwItemProductMedicNonMedic emp in coll)
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
				return VwItemProductMedicNonMedicMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemProductMedicNonMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemProductMedicNonMedic(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemProductMedicNonMedic();
		}

		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}

		#endregion

		[BrowsableAttribute(false)]
		public VwItemProductMedicNonMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemProductMedicNonMedicQuery();
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
		public bool Load(VwItemProductMedicNonMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VwItemProductMedicNonMedic AddNew()
		{
			VwItemProductMedicNonMedic entity = base.AddNewEntity() as VwItemProductMedicNonMedic;

			return entity;
		}

		#region IEnumerable< VwItemProductMedicNonMedic> Members

		IEnumerator<VwItemProductMedicNonMedic> IEnumerable<VwItemProductMedicNonMedic>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as VwItemProductMedicNonMedic;
			}
		}

		#endregion

		private VwItemProductMedicNonMedicQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VwItemProductMedicNonMedic' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VwItemProductMedicNonMedic ()")]
	[Serializable]
	public partial class VwItemProductMedicNonMedic : esVwItemProductMedicNonMedic
	{
		public VwItemProductMedicNonMedic()
		{
		}

		public VwItemProductMedicNonMedic(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemProductMedicNonMedicMetadata.Meta();
			}
		}

		override protected esVwItemProductMedicNonMedicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemProductMedicNonMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public VwItemProductMedicNonMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemProductMedicNonMedicQuery();
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
		public bool Load(VwItemProductMedicNonMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private VwItemProductMedicNonMedicQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VwItemProductMedicNonMedicQuery : esVwItemProductMedicNonMedicQuery
	{
		public VwItemProductMedicNonMedicQuery()
		{

		}

		public VwItemProductMedicNonMedicQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "VwItemProductMedicNonMedicQuery";
		}
	}

	[Serializable]
	public partial class VwItemProductMedicNonMedicMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemProductMedicNonMedicMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.SRItemUnit, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.SRPurchaseUnit, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.SRPurchaseUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.ConversionFactor, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsInventoryItem, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsInventoryItem;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInPurchaseUnit, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.PriceInPurchaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBaseUnit, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.PriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.PriceInBasedUnitWVat, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.PriceInBasedUnitWVat;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.HighestPriceInBasedUnit, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.HighestPriceInBasedUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.CostPrice, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsRegularItem, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsRegularItem;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsSalesAvailable, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsSalesAvailable;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.BrandName, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.BrandName;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.Dosage, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.Dosage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.SRDosageUnit, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.SRDosageUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsUsingCigna, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsUsingCigna;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.SRDrugLabelType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.SRDrugLabelType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.GenericFlag, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.GenericFlag;
			c.CharacterMaxLength = 11;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsControlExpired, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsControlExpired;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsFornas, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsFornas;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsFormularium, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsFormularium;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.Barcode, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.Barcode;
			c.CharacterMaxLength = 35;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsAntibiotic, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsAntibiotic;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsMedication, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsMedication;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsGeneric, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsGeneric;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGeneric, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsNonGeneric;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.IsNonGenericLimited, 26, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.IsNonGenericLimited;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.SRAntibioticLine, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.SRAntibioticLine;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.SpecificInfo, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.SpecificInfo;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(VwItemProductMedicNonMedicMetadata.ColumnNames.FornasRestrictionNotes, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductMedicNonMedicMetadata.PropertyNames.FornasRestrictionNotes;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);


		}
		#endregion

		static public VwItemProductMedicNonMedicMetadata Meta()
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
			public const string SRItemUnit = "SRItemUnit";
			public const string SRPurchaseUnit = "SRPurchaseUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string IsInventoryItem = "IsInventoryItem";
			public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
			public const string PriceInBaseUnit = "PriceInBaseUnit";
			public const string PriceInBasedUnitWVat = "PriceInBasedUnitWVat";
			public const string HighestPriceInBasedUnit = "HighestPriceInBasedUnit";
			public const string CostPrice = "CostPrice";
			public const string IsRegularItem = "IsRegularItem";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string BrandName = "BrandName";
			public const string Dosage = "Dosage";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string IsUsingCigna = "IsUsingCigna";
			public const string SRDrugLabelType = "SRDrugLabelType";
			public const string GenericFlag = "GenericFlag";
			public const string IsControlExpired = "IsControlExpired";
			public const string IsFornas = "IsFornas";
			public const string IsFormularium = "IsFormularium";
			public const string Barcode = "Barcode";
			public const string IsAntibiotic = "IsAntibiotic";
			public const string IsMedication = "IsMedication";
			public const string IsGeneric = "IsGeneric";
			public const string IsNonGeneric = "IsNonGeneric";
			public const string IsNonGenericLimited = "IsNonGenericLimited";
			public const string SRAntibioticLine = "SRAntibioticLine";
			public const string SpecificInfo = "SpecificInfo";
			public const string FornasRestrictionNotes = "FornasRestrictionNotes";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ItemID = "ItemID";
			public const string SRItemUnit = "SRItemUnit";
			public const string SRPurchaseUnit = "SRPurchaseUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string IsInventoryItem = "IsInventoryItem";
			public const string PriceInPurchaseUnit = "PriceInPurchaseUnit";
			public const string PriceInBaseUnit = "PriceInBaseUnit";
			public const string PriceInBasedUnitWVat = "PriceInBasedUnitWVat";
			public const string HighestPriceInBasedUnit = "HighestPriceInBasedUnit";
			public const string CostPrice = "CostPrice";
			public const string IsRegularItem = "IsRegularItem";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string BrandName = "BrandName";
			public const string Dosage = "Dosage";
			public const string SRDosageUnit = "SRDosageUnit";
			public const string IsUsingCigna = "IsUsingCigna";
			public const string SRDrugLabelType = "SRDrugLabelType";
			public const string GenericFlag = "GenericFlag";
			public const string IsControlExpired = "IsControlExpired";
			public const string IsFornas = "IsFornas";
			public const string IsFormularium = "IsFormularium";
			public const string Barcode = "Barcode";
			public const string IsAntibiotic = "IsAntibiotic";
			public const string IsMedication = "IsMedication";
			public const string IsGeneric = "IsGeneric";
			public const string IsNonGeneric = "IsNonGeneric";
			public const string IsNonGenericLimited = "IsNonGenericLimited";
			public const string SRAntibioticLine = "SRAntibioticLine";
			public const string SpecificInfo = "SpecificInfo";
			public const string FornasRestrictionNotes = "FornasRestrictionNotes";
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
			lock (typeof(VwItemProductMedicNonMedicMetadata))
			{
				if (VwItemProductMedicNonMedicMetadata.mapDelegates == null)
				{
					VwItemProductMedicNonMedicMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (VwItemProductMedicNonMedicMetadata.meta == null)
				{
					VwItemProductMedicNonMedicMetadata.meta = new VwItemProductMedicNonMedicMetadata();
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
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPurchaseUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsInventoryItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBasedUnitWVat", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HighestPriceInBasedUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsRegularItem", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsSalesAvailable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BrandName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Dosage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsingCigna", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRDrugLabelType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GenericFlag", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsControlExpired", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFornas", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsFormularium", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Barcode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAntibiotic", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsMedication", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsGeneric", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsNonGeneric", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsNonGenericLimited", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRAntibioticLine", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecificInfo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FornasRestrictionNotes", new esTypeMap("varchar", "System.String"));


				meta.Source = "vw_ItemProductMedicNonMedic";
				meta.Destination = "vw_ItemProductMedicNonMedic";
				meta.spInsert = "proc_vw_ItemProductMedicNonMedicInsert";
				meta.spUpdate = "proc_vw_ItemProductMedicNonMedicUpdate";
				meta.spDelete = "proc_vw_ItemProductMedicNonMedicDelete";
				meta.spLoadAll = "proc_vw_ItemProductMedicNonMedicLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemProductMedicNonMedicLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemProductMedicNonMedicMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
