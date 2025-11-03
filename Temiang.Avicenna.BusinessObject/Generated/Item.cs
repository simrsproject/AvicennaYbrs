/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/16/2023 3:56:54 PM
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
	abstract public class esItemCollection : esEntityCollectionWAuditLog
	{
		public esItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemQuery query)
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
			this.InitQuery(query as esItemQuery);
		}
		#endregion

		virtual public Item DetachEntity(Item entity)
		{
			return base.DetachEntity(entity) as Item;
		}

		virtual public Item AttachEntity(Item entity)
		{
			return base.AttachEntity(entity) as Item;
		}

		virtual public void Combine(ItemCollection collection)
		{
			base.Combine(collection);
		}

		new public Item this[int index]
		{
			get
			{
				return base[index] as Item;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Item);
		}
	}

	[Serializable]
	abstract public class esItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esItem()
		{
		}

		public esItem(DataRow row)
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
			esItemQuery query = this.GetDynamicQuery();
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
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "SRItemType": this.str.SRItemType = (string)value; break;
						case "ItemName": this.str.ItemName = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "ItemIDExternal": this.str.ItemIDExternal = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRBillingGroup": this.str.SRBillingGroup = (string)value; break;
						case "ProductAccountID": this.str.ProductAccountID = (string)value; break;
						case "IsItemProduction": this.str.IsItemProduction = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsHasTestResults": this.str.IsHasTestResults = (string)value; break;
						case "IsNeedToBeSterilized": this.str.IsNeedToBeSterilized = (string)value; break;
						case "SRBridgingType": this.str.SRBridgingType = (string)value; break;
						case "SRCssdItemGroup": this.str.SRCssdItemGroup = (string)value; break;
						case "SRBpjsItemGroup": this.str.SRBpjsItemGroup = (string)value; break;
						case "SREklaimTariffGroup": this.str.SREklaimTariffGroup = (string)value; break;
						case "Barcode": this.str.Barcode = (string)value; break;
						case "SRItemCategory": this.str.SRItemCategory = (string)value; break;
						case "CssdPackagingCostAmount": this.str.CssdPackagingCostAmount = (string)value; break;
						case "IntervalOrderWarning": this.str.IntervalOrderWarning = (string)value; break;
						case "SREklaimFactorGroup": this.str.SREklaimFactorGroup = (string)value; break;
						case "SRItemSubGroup": this.str.SRItemSubGroup = (string)value; break;
						case "ValidityPeriodFrom": this.str.ValidityPeriodFrom = (string)value; break;
						case "ValidityPeriodTo": this.str.ValidityPeriodTo = (string)value; break;
						case "IsAsset": this.str.IsAsset = (string)value; break;
						case "AssetGroupID": this.str.AssetGroupID = (string)value; break;
						case "IsDelegationToNurse": this.str.IsDelegationToNurse = (string)value; break;
						case "IsNewUpload": this.str.IsNewUpload = (string)value; break;
						case "EconomicLifeInYear": this.str.EconomicLifeInYear = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsItemProduction":

							if (value == null || value is System.Boolean)
								this.IsItemProduction = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsHasTestResults":

							if (value == null || value is System.Boolean)
								this.IsHasTestResults = (System.Boolean?)value;
							break;
						case "IsNeedToBeSterilized":

							if (value == null || value is System.Boolean)
								this.IsNeedToBeSterilized = (System.Boolean?)value;
							break;
						case "Photo":

							if (value == null || value is System.Byte[])
								this.Photo = (System.Byte[])value;
							break;
						case "CssdPackagingCostAmount":

							if (value == null || value is System.Decimal)
								this.CssdPackagingCostAmount = (System.Decimal?)value;
							break;
						case "IntervalOrderWarning":

							if (value == null || value is System.Int16)
								this.IntervalOrderWarning = (System.Int16?)value;
							break;
						case "ValidityPeriodFrom":

							if (value == null || value is System.DateTime)
								this.ValidityPeriodFrom = (System.DateTime?)value;
							break;
						case "ValidityPeriodTo":

							if (value == null || value is System.DateTime)
								this.ValidityPeriodTo = (System.DateTime?)value;
							break;
						case "IsAsset":

							if (value == null || value is System.Boolean)
								this.IsAsset = (System.Boolean?)value;
							break;
						case "IsDelegationToNurse":

							if (value == null || value is System.Boolean)
								this.IsDelegationToNurse = (System.Boolean?)value;
							break;
						case "IsNewUpload":

							if (value == null || value is System.Boolean)
								this.IsNewUpload = (System.Boolean?)value;
							break;
						case "EconomicLifeInYear":

							if (value == null || value is System.Int32)
								this.EconomicLifeInYear = (System.Int32?)value;
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
		/// Maps to Item.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to Item.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.ItemGroupID);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to Item.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SRItemType);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SRItemType, value);
			}
		}
		/// <summary>
		/// Maps to Item.ItemName
		/// </summary>
		virtual public System.String ItemName
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.ItemName);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.ItemName, value);
			}
		}
		/// <summary>
		/// Maps to Item.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ItemMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(ItemMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Item.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Item.ItemIDExternal
		/// </summary>
		virtual public System.String ItemIDExternal
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.ItemIDExternal);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.ItemIDExternal, value);
			}
		}
		/// <summary>
		/// Maps to Item.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Item.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Item.SRBillingGroup
		/// </summary>
		virtual public System.String SRBillingGroup
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SRBillingGroup);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SRBillingGroup, value);
			}
		}
		/// <summary>
		/// Maps to Item.ProductAccountID
		/// </summary>
		virtual public System.String ProductAccountID
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.ProductAccountID);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.ProductAccountID, value);
			}
		}
		/// <summary>
		/// Maps to Item.IsItemProduction
		/// </summary>
		virtual public System.Boolean? IsItemProduction
		{
			get
			{
				return base.GetSystemBoolean(ItemMetadata.ColumnNames.IsItemProduction);
			}

			set
			{
				base.SetSystemBoolean(ItemMetadata.ColumnNames.IsItemProduction, value);
			}
		}
		/// <summary>
		/// Maps to Item.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.GuarantorID);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to Item.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Item.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Item.IsHasTestResults
		/// </summary>
		virtual public System.Boolean? IsHasTestResults
		{
			get
			{
				return base.GetSystemBoolean(ItemMetadata.ColumnNames.IsHasTestResults);
			}

			set
			{
				base.SetSystemBoolean(ItemMetadata.ColumnNames.IsHasTestResults, value);
			}
		}
		/// <summary>
		/// Maps to Item.IsNeedToBeSterilized
		/// </summary>
		virtual public System.Boolean? IsNeedToBeSterilized
		{
			get
			{
				return base.GetSystemBoolean(ItemMetadata.ColumnNames.IsNeedToBeSterilized);
			}

			set
			{
				base.SetSystemBoolean(ItemMetadata.ColumnNames.IsNeedToBeSterilized, value);
			}
		}
		/// <summary>
		/// Maps to Item.SRBridgingType
		/// </summary>
		virtual public System.String SRBridgingType
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SRBridgingType);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SRBridgingType, value);
			}
		}
		/// <summary>
		/// Maps to Item.SRCssdItemGroup
		/// </summary>
		virtual public System.String SRCssdItemGroup
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SRCssdItemGroup);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SRCssdItemGroup, value);
			}
		}
		/// <summary>
		/// Maps to Item.SRBpjsItemGroup
		/// </summary>
		virtual public System.String SRBpjsItemGroup
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SRBpjsItemGroup);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SRBpjsItemGroup, value);
			}
		}
		/// <summary>
		/// Maps to Item.SREklaimTariffGroup
		/// </summary>
		virtual public System.String SREklaimTariffGroup
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SREklaimTariffGroup);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SREklaimTariffGroup, value);
			}
		}
		/// <summary>
		/// Maps to Item.Barcode
		/// </summary>
		virtual public System.String Barcode
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.Barcode);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.Barcode, value);
			}
		}
		/// <summary>
		/// Maps to Item.Photo
		/// </summary>
		virtual public System.Byte[] Photo
		{
			get
			{
				return base.GetSystemByteArray(ItemMetadata.ColumnNames.Photo);
			}

			set
			{
				base.SetSystemByteArray(ItemMetadata.ColumnNames.Photo, value);
			}
		}
		/// <summary>
		/// Maps to Item.SRItemCategory
		/// </summary>
		virtual public System.String SRItemCategory
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SRItemCategory);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SRItemCategory, value);
			}
		}
		/// <summary>
		/// Maps to Item.CssdPackagingCostAmount
		/// </summary>
		virtual public System.Decimal? CssdPackagingCostAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemMetadata.ColumnNames.CssdPackagingCostAmount);
			}

			set
			{
				base.SetSystemDecimal(ItemMetadata.ColumnNames.CssdPackagingCostAmount, value);
			}
		}
		/// <summary>
		/// Maps to Item.IntervalOrderWarning
		/// </summary>
		virtual public System.Int16? IntervalOrderWarning
		{
			get
			{
				return base.GetSystemInt16(ItemMetadata.ColumnNames.IntervalOrderWarning);
			}

			set
			{
				base.SetSystemInt16(ItemMetadata.ColumnNames.IntervalOrderWarning, value);
			}
		}
		/// <summary>
		/// Maps to Item.SREklaimFactorGroup
		/// </summary>
		virtual public System.String SREklaimFactorGroup
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SREklaimFactorGroup);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SREklaimFactorGroup, value);
			}
		}
		/// <summary>
		/// Maps to Item.SRItemSubGroup
		/// </summary>
		virtual public System.String SRItemSubGroup
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.SRItemSubGroup);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.SRItemSubGroup, value);
			}
		}
		/// <summary>
		/// Maps to Item.ValidityPeriodFrom
		/// </summary>
		virtual public System.DateTime? ValidityPeriodFrom
		{
			get
			{
				return base.GetSystemDateTime(ItemMetadata.ColumnNames.ValidityPeriodFrom);
			}

			set
			{
				base.SetSystemDateTime(ItemMetadata.ColumnNames.ValidityPeriodFrom, value);
			}
		}
		/// <summary>
		/// Maps to Item.ValidityPeriodTo
		/// </summary>
		virtual public System.DateTime? ValidityPeriodTo
		{
			get
			{
				return base.GetSystemDateTime(ItemMetadata.ColumnNames.ValidityPeriodTo);
			}

			set
			{
				base.SetSystemDateTime(ItemMetadata.ColumnNames.ValidityPeriodTo, value);
			}
		}
		/// <summary>
		/// Maps to Item.IsAsset
		/// </summary>
		virtual public System.Boolean? IsAsset
		{
			get
			{
				return base.GetSystemBoolean(ItemMetadata.ColumnNames.IsAsset);
			}

			set
			{
				base.SetSystemBoolean(ItemMetadata.ColumnNames.IsAsset, value);
			}
		}
		/// <summary>
		/// Maps to Item.AssetGroupID
		/// </summary>
		virtual public System.String AssetGroupID
		{
			get
			{
				return base.GetSystemString(ItemMetadata.ColumnNames.AssetGroupID);
			}

			set
			{
				base.SetSystemString(ItemMetadata.ColumnNames.AssetGroupID, value);
			}
		}
		/// <summary>
		/// Maps to Item.IsDelegationToNurse
		/// </summary>
		virtual public System.Boolean? IsDelegationToNurse
		{
			get
			{
				return base.GetSystemBoolean(ItemMetadata.ColumnNames.IsDelegationToNurse);
			}

			set
			{
				base.SetSystemBoolean(ItemMetadata.ColumnNames.IsDelegationToNurse, value);
			}
		}
		/// <summary>
		/// Maps to Item.IsNewUpload
		/// </summary>
		virtual public System.Boolean? IsNewUpload
		{
			get
			{
				return base.GetSystemBoolean(ItemMetadata.ColumnNames.IsNewUpload);
			}

			set
			{
				base.SetSystemBoolean(ItemMetadata.ColumnNames.IsNewUpload, value);
			}
		}
		/// <summary>
		/// Maps to Item.EconomicLifeInYear
		/// </summary>
		virtual public System.Int32? EconomicLifeInYear
		{
			get
			{
				return base.GetSystemInt32(ItemMetadata.ColumnNames.EconomicLifeInYear);
			}

			set
			{
				base.SetSystemInt32(ItemMetadata.ColumnNames.EconomicLifeInYear, value);
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
			public esStrings(esItem entity)
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
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String SRItemType
			{
				get
				{
					System.String data = entity.SRItemType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemType = null;
					else entity.SRItemType = Convert.ToString(value);
				}
			}
			public System.String ItemName
			{
				get
				{
					System.String data = entity.ItemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemName = null;
					else entity.ItemName = Convert.ToString(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			public System.String ItemIDExternal
			{
				get
				{
					System.String data = entity.ItemIDExternal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemIDExternal = null;
					else entity.ItemIDExternal = Convert.ToString(value);
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
			public System.String SRBillingGroup
			{
				get
				{
					System.String data = entity.SRBillingGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBillingGroup = null;
					else entity.SRBillingGroup = Convert.ToString(value);
				}
			}
			public System.String ProductAccountID
			{
				get
				{
					System.String data = entity.ProductAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductAccountID = null;
					else entity.ProductAccountID = Convert.ToString(value);
				}
			}
			public System.String IsItemProduction
			{
				get
				{
					System.Boolean? data = entity.IsItemProduction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsItemProduction = null;
					else entity.IsItemProduction = Convert.ToBoolean(value);
				}
			}
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
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
			public System.String IsHasTestResults
			{
				get
				{
					System.Boolean? data = entity.IsHasTestResults;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHasTestResults = null;
					else entity.IsHasTestResults = Convert.ToBoolean(value);
				}
			}
			public System.String IsNeedToBeSterilized
			{
				get
				{
					System.Boolean? data = entity.IsNeedToBeSterilized;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedToBeSterilized = null;
					else entity.IsNeedToBeSterilized = Convert.ToBoolean(value);
				}
			}
			public System.String SRBridgingType
			{
				get
				{
					System.String data = entity.SRBridgingType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBridgingType = null;
					else entity.SRBridgingType = Convert.ToString(value);
				}
			}
			public System.String SRCssdItemGroup
			{
				get
				{
					System.String data = entity.SRCssdItemGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCssdItemGroup = null;
					else entity.SRCssdItemGroup = Convert.ToString(value);
				}
			}
			public System.String SRBpjsItemGroup
			{
				get
				{
					System.String data = entity.SRBpjsItemGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBpjsItemGroup = null;
					else entity.SRBpjsItemGroup = Convert.ToString(value);
				}
			}
			public System.String SREklaimTariffGroup
			{
				get
				{
					System.String data = entity.SREklaimTariffGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREklaimTariffGroup = null;
					else entity.SREklaimTariffGroup = Convert.ToString(value);
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
			public System.String SRItemCategory
			{
				get
				{
					System.String data = entity.SRItemCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemCategory = null;
					else entity.SRItemCategory = Convert.ToString(value);
				}
			}
			public System.String CssdPackagingCostAmount
			{
				get
				{
					System.Decimal? data = entity.CssdPackagingCostAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CssdPackagingCostAmount = null;
					else entity.CssdPackagingCostAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IntervalOrderWarning
			{
				get
				{
					System.Int16? data = entity.IntervalOrderWarning;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IntervalOrderWarning = null;
					else entity.IntervalOrderWarning = Convert.ToInt16(value);
				}
			}
			public System.String SREklaimFactorGroup
			{
				get
				{
					System.String data = entity.SREklaimFactorGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREklaimFactorGroup = null;
					else entity.SREklaimFactorGroup = Convert.ToString(value);
				}
			}
			public System.String SRItemSubGroup
			{
				get
				{
					System.String data = entity.SRItemSubGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemSubGroup = null;
					else entity.SRItemSubGroup = Convert.ToString(value);
				}
			}
			public System.String ValidityPeriodFrom
			{
				get
				{
					System.DateTime? data = entity.ValidityPeriodFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidityPeriodFrom = null;
					else entity.ValidityPeriodFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ValidityPeriodTo
			{
				get
				{
					System.DateTime? data = entity.ValidityPeriodTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidityPeriodTo = null;
					else entity.ValidityPeriodTo = Convert.ToDateTime(value);
				}
			}
			public System.String IsAsset
			{
				get
				{
					System.Boolean? data = entity.IsAsset;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAsset = null;
					else entity.IsAsset = Convert.ToBoolean(value);
				}
			}
			public System.String AssetGroupID
			{
				get
				{
					System.String data = entity.AssetGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetGroupID = null;
					else entity.AssetGroupID = Convert.ToString(value);
				}
			}
			public System.String IsDelegationToNurse
			{
				get
				{
					System.Boolean? data = entity.IsDelegationToNurse;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDelegationToNurse = null;
					else entity.IsDelegationToNurse = Convert.ToBoolean(value);
				}
			}
			public System.String IsNewUpload
			{
				get
				{
					System.Boolean? data = entity.IsNewUpload;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNewUpload = null;
					else entity.IsNewUpload = Convert.ToBoolean(value);
				}
			}
			public System.String EconomicLifeInYear
			{
				get
				{
					System.Int32? data = entity.EconomicLifeInYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EconomicLifeInYear = null;
					else entity.EconomicLifeInYear = Convert.ToInt32(value);
				}
			}
			private esItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemQuery query)
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
				throw new Exception("esItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Item : esItem
	{
	}

	[Serializable]
	abstract public class esItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemMetadata.Meta();
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		}

		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		}

		public esQueryItem ItemName
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.ItemName, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem ItemIDExternal
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.ItemIDExternal, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRBillingGroup
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SRBillingGroup, esSystemType.String);
			}
		}

		public esQueryItem ProductAccountID
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.ProductAccountID, esSystemType.String);
			}
		}

		public esQueryItem IsItemProduction
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IsItemProduction, esSystemType.Boolean);
			}
		}

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsHasTestResults
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IsHasTestResults, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNeedToBeSterilized
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IsNeedToBeSterilized, esSystemType.Boolean);
			}
		}

		public esQueryItem SRBridgingType
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SRBridgingType, esSystemType.String);
			}
		}

		public esQueryItem SRCssdItemGroup
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SRCssdItemGroup, esSystemType.String);
			}
		}

		public esQueryItem SRBpjsItemGroup
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SRBpjsItemGroup, esSystemType.String);
			}
		}

		public esQueryItem SREklaimTariffGroup
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SREklaimTariffGroup, esSystemType.String);
			}
		}

		public esQueryItem Barcode
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.Barcode, esSystemType.String);
			}
		}

		public esQueryItem Photo
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.Photo, esSystemType.ByteArray);
			}
		}

		public esQueryItem SRItemCategory
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SRItemCategory, esSystemType.String);
			}
		}

		public esQueryItem CssdPackagingCostAmount
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.CssdPackagingCostAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IntervalOrderWarning
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IntervalOrderWarning, esSystemType.Int16);
			}
		}

		public esQueryItem SREklaimFactorGroup
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SREklaimFactorGroup, esSystemType.String);
			}
		}

		public esQueryItem SRItemSubGroup
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.SRItemSubGroup, esSystemType.String);
			}
		}

		public esQueryItem ValidityPeriodFrom
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.ValidityPeriodFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidityPeriodTo
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.ValidityPeriodTo, esSystemType.DateTime);
			}
		}

		public esQueryItem IsAsset
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IsAsset, esSystemType.Boolean);
			}
		}

		public esQueryItem AssetGroupID
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.AssetGroupID, esSystemType.String);
			}
		}

		public esQueryItem IsDelegationToNurse
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IsDelegationToNurse, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNewUpload
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.IsNewUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem EconomicLifeInYear
		{
			get
			{
				return new esQueryItem(this, ItemMetadata.ColumnNames.EconomicLifeInYear, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemCollection")]
	public partial class ItemCollection : esItemCollection, IEnumerable<Item>
	{
		public ItemCollection()
		{

		}

		public static implicit operator List<Item>(ItemCollection coll)
		{
			List<Item> list = new List<Item>();

			foreach (Item emp in coll)
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
				return ItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Item(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Item();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemQuery();
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
		public bool Load(ItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Item AddNew()
		{
			Item entity = base.AddNewEntity() as Item;

			return entity;
		}
		public Item FindByPrimaryKey(String itemID)
		{
			return base.FindByPrimaryKey(itemID) as Item;
		}

		#region IEnumerable< Item> Members

		IEnumerator<Item> IEnumerable<Item>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Item;
			}
		}

		#endregion

		private ItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Item' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Item ({ItemID})")]
	[Serializable]
	public partial class Item : esItem
	{
		public Item()
		{
		}

		public Item(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemMetadata.Meta();
			}
		}

		override protected esItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemQuery();
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
		public bool Load(ItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemQuery : esItemQuery
	{
		public ItemQuery()
		{

		}

		public ItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemQuery";
		}
	}

	[Serializable]
	public partial class ItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.ItemGroupID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SRItemType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.ItemName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 200;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.ItemIDExternal, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.ItemIDExternal;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SRBillingGroup, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SRBillingGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.ProductAccountID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.ProductAccountID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IsItemProduction, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemMetadata.PropertyNames.IsItemProduction;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.GuarantorID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.CreatedDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.CreatedByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IsHasTestResults, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemMetadata.PropertyNames.IsHasTestResults;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IsNeedToBeSterilized, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemMetadata.PropertyNames.IsNeedToBeSterilized;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SRBridgingType, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SRBridgingType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SRCssdItemGroup, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SRCssdItemGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SRBpjsItemGroup, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SRBpjsItemGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SREklaimTariffGroup, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SREklaimTariffGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.Barcode, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.Barcode;
			c.CharacterMaxLength = 35;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.Photo, 22, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ItemMetadata.PropertyNames.Photo;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SRItemCategory, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SRItemCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.CssdPackagingCostAmount, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMetadata.PropertyNames.CssdPackagingCostAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IntervalOrderWarning, 25, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ItemMetadata.PropertyNames.IntervalOrderWarning;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SREklaimFactorGroup, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SREklaimFactorGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.SRItemSubGroup, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.SRItemSubGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.ValidityPeriodFrom, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemMetadata.PropertyNames.ValidityPeriodFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.ValidityPeriodTo, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemMetadata.PropertyNames.ValidityPeriodTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IsAsset, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemMetadata.PropertyNames.IsAsset;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.AssetGroupID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMetadata.PropertyNames.AssetGroupID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IsDelegationToNurse, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemMetadata.PropertyNames.IsDelegationToNurse;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.IsNewUpload, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemMetadata.PropertyNames.IsNewUpload;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMetadata.ColumnNames.EconomicLifeInYear, 34, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemMetadata.PropertyNames.EconomicLifeInYear;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemMetadata Meta()
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
			public const string ItemGroupID = "ItemGroupID";
			public const string SRItemType = "SRItemType";
			public const string ItemName = "ItemName";
			public const string IsActive = "IsActive";
			public const string Notes = "Notes";
			public const string ItemIDExternal = "ItemIDExternal";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRBillingGroup = "SRBillingGroup";
			public const string ProductAccountID = "ProductAccountID";
			public const string IsItemProduction = "IsItemProduction";
			public const string GuarantorID = "GuarantorID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsHasTestResults = "IsHasTestResults";
			public const string IsNeedToBeSterilized = "IsNeedToBeSterilized";
			public const string SRBridgingType = "SRBridgingType";
			public const string SRCssdItemGroup = "SRCssdItemGroup";
			public const string SRBpjsItemGroup = "SRBpjsItemGroup";
			public const string SREklaimTariffGroup = "SREklaimTariffGroup";
			public const string Barcode = "Barcode";
			public const string Photo = "Photo";
			public const string SRItemCategory = "SRItemCategory";
			public const string CssdPackagingCostAmount = "CssdPackagingCostAmount";
			public const string IntervalOrderWarning = "IntervalOrderWarning";
			public const string SREklaimFactorGroup = "SREklaimFactorGroup";
			public const string SRItemSubGroup = "SRItemSubGroup";
			public const string ValidityPeriodFrom = "ValidityPeriodFrom";
			public const string ValidityPeriodTo = "ValidityPeriodTo";
			public const string IsAsset = "IsAsset";
			public const string AssetGroupID = "AssetGroupID";
			public const string IsDelegationToNurse = "IsDelegationToNurse";
			public const string IsNewUpload = "IsNewUpload";
			public const string EconomicLifeInYear = "EconomicLifeInYear";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ItemID = "ItemID";
			public const string ItemGroupID = "ItemGroupID";
			public const string SRItemType = "SRItemType";
			public const string ItemName = "ItemName";
			public const string IsActive = "IsActive";
			public const string Notes = "Notes";
			public const string ItemIDExternal = "ItemIDExternal";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRBillingGroup = "SRBillingGroup";
			public const string ProductAccountID = "ProductAccountID";
			public const string IsItemProduction = "IsItemProduction";
			public const string GuarantorID = "GuarantorID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsHasTestResults = "IsHasTestResults";
			public const string IsNeedToBeSterilized = "IsNeedToBeSterilized";
			public const string SRBridgingType = "SRBridgingType";
			public const string SRCssdItemGroup = "SRCssdItemGroup";
			public const string SRBpjsItemGroup = "SRBpjsItemGroup";
			public const string SREklaimTariffGroup = "SREklaimTariffGroup";
			public const string Barcode = "Barcode";
			public const string Photo = "Photo";
			public const string SRItemCategory = "SRItemCategory";
			public const string CssdPackagingCostAmount = "CssdPackagingCostAmount";
			public const string IntervalOrderWarning = "IntervalOrderWarning";
			public const string SREklaimFactorGroup = "SREklaimFactorGroup";
			public const string SRItemSubGroup = "SRItemSubGroup";
			public const string ValidityPeriodFrom = "ValidityPeriodFrom";
			public const string ValidityPeriodTo = "ValidityPeriodTo";
			public const string IsAsset = "IsAsset";
			public const string AssetGroupID = "AssetGroupID";
			public const string IsDelegationToNurse = "IsDelegationToNurse";
			public const string IsNewUpload = "IsNewUpload";
			public const string EconomicLifeInYear = "EconomicLifeInYear";
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
			lock (typeof(ItemMetadata))
			{
				if (ItemMetadata.mapDelegates == null)
				{
					ItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemMetadata.meta == null)
				{
					ItemMetadata.meta = new ItemMetadata();
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
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemIDExternal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBillingGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProductAccountID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsItemProduction", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsHasTestResults", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNeedToBeSterilized", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRBridgingType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCssdItemGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBpjsItemGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREklaimTariffGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Barcode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Photo", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("SRItemCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CssdPackagingCostAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IntervalOrderWarning", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SREklaimFactorGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemSubGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidityPeriodFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidityPeriodTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsAsset", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AssetGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDelegationToNurse", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNewUpload", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("EconomicLifeInYear", new esTypeMap("int", "System.Int32"));


				meta.Source = "Item";
				meta.Destination = "Item";
				meta.spInsert = "proc_ItemInsert";
				meta.spUpdate = "proc_ItemUpdate";
				meta.spDelete = "proc_ItemDelete";
				meta.spLoadAll = "proc_ItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
