/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/23/2021 3:21:24 PM
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
	abstract public class esAssetCollection : esEntityCollectionWAuditLog
	{
		public esAssetCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AssetCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetQuery query)
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
			this.InitQuery(query as esAssetQuery);
		}
		#endregion

		virtual public Asset DetachEntity(Asset entity)
		{
			return base.DetachEntity(entity) as Asset;
		}

		virtual public Asset AttachEntity(Asset entity)
		{
			return base.AttachEntity(entity) as Asset;
		}

		virtual public void Combine(AssetCollection collection)
		{
			base.Combine(collection);
		}

		new public Asset this[int index]
		{
			get
			{
				return base[index] as Asset;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Asset);
		}
	}

	[Serializable]
	abstract public class esAsset : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetQuery GetDynamicQuery()
		{
			return null;
		}

		public esAsset()
		{
		}

		public esAsset(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String assetID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String assetID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetID);
			else
				return LoadByPrimaryKeyStoredProcedure(assetID);
		}

		private bool LoadByPrimaryKeyDynamic(String assetID)
		{
			esAssetQuery query = this.GetDynamicQuery();
			query.Where(query.AssetID == assetID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String assetID)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetID", assetID);
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
						case "AssetID": this.str.AssetID = (string)value; break;
						case "AssetGroupID": this.str.AssetGroupID = (string)value; break;
						case "AssetLocationID": this.str.AssetLocationID = (string)value; break;
						case "DepartmentID": this.str.DepartmentID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "AssetName": this.str.AssetName = (string)value; break;
						case "AssetBookID": this.str.AssetBookID = (string)value; break;
						case "SRAssetType": this.str.SRAssetType = (string)value; break;
						case "SRManufacturer": this.str.SRManufacturer = (string)value; break;
						case "BrandName": this.str.BrandName = (string)value; break;
						case "SerialNumber": this.str.SerialNumber = (string)value; break;
						case "DepreciationMethodID": this.str.DepreciationMethodID = (string)value; break;
						case "ItemUnit": this.str.ItemUnit = (string)value; break;
						case "PurchaseOrderNumber": this.str.PurchaseOrderNumber = (string)value; break;
						case "PurchasedDate": this.str.PurchasedDate = (string)value; break;
						case "StartDepreciationDate": this.str.StartDepreciationDate = (string)value; break;
						case "StartUsingDate": this.str.StartUsingDate = (string)value; break;
						case "PurchasedPrice": this.str.PurchasedPrice = (string)value; break;
						case "UsageTimeEstimation": this.str.UsageTimeEstimation = (string)value; break;
						case "AgeOfDepreciation": this.str.AgeOfDepreciation = (string)value; break;
						case "SalesPrice": this.str.SalesPrice = (string)value; break;
						case "CurrentValue": this.str.CurrentValue = (string)value; break;
						case "CurrentCondition": this.str.CurrentCondition = (string)value; break;
						case "CurrentUsageTimeEstimation": this.str.CurrentUsageTimeEstimation = (string)value; break;
						case "ResidualValue": this.str.ResidualValue = (string)value; break;
						case "InsuranceID": this.str.InsuranceID = (string)value; break;
						case "InsurancePolicyNo": this.str.InsurancePolicyNo = (string)value; break;
						case "InsuranceAmount": this.str.InsuranceAmount = (string)value; break;
						case "GuaranteeExpiredDate": this.str.GuaranteeExpiredDate = (string)value; break;
						case "LastInventoriedDate": this.str.LastInventoriedDate = (string)value; break;
						case "LastInventoriedBy": this.str.LastInventoriedBy = (string)value; break;
						case "LastMaintenanceDate": this.str.LastMaintenanceDate = (string)value; break;
						case "NextMaintenanceDate": this.str.NextMaintenanceDate = (string)value; break;
						case "MaintenanceInterval": this.str.MaintenanceInterval = (string)value; break;
						case "MaintenanceIntervalIn": this.str.MaintenanceIntervalIn = (string)value; break;
						case "MaintenanceServiceUnitID": this.str.MaintenanceServiceUnitID = (string)value; break;
						case "IssuedDate": this.str.IssuedDate = (string)value; break;
						case "IssuedBy": this.str.IssuedBy = (string)value; break;
						case "SRIssuedReason": this.str.SRIssuedReason = (string)value; break;
						case "IntervalUnit": this.str.IntervalUnit = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "IsAudiometer": this.str.IsAudiometer = (string)value; break;
						case "IsSpirometer": this.str.IsSpirometer = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "SRAssetsStatus": this.str.SRAssetsStatus = (string)value; break;
						case "SRAssetsCondition": this.str.SRAssetsCondition = (string)value; break;
						case "SRAssetsCriticality": this.str.SRAssetsCriticality = (string)value; break;
						case "NotesToTechnician": this.str.NotesToTechnician = (string)value; break;
						case "SupplierID": this.str.SupplierID = (string)value; break;
						case "SRAssetsWarrantyContract": this.str.SRAssetsWarrantyContract = (string)value; break;
						case "WarrantyContractNotes": this.str.WarrantyContractNotes = (string)value; break;
						case "DateDisposed": this.str.DateDisposed = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "IsContinuousMaintenanceSchedule": this.str.IsContinuousMaintenanceSchedule = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "AssetSubGroupId": this.str.AssetSubGroupId = (string)value; break;
						case "IsFixedAsset": this.str.IsFixedAsset = (string)value; break;
						case "ValueDisposed": this.str.ValueDisposed = (string)value; break;
						case "SRAssetUsageTimeEstimation": this.str.SRAssetUsageTimeEstimation = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PurchasedDate":

							if (value == null || value is System.DateTime)
								this.PurchasedDate = (System.DateTime?)value;
							break;
						case "StartDepreciationDate":

							if (value == null || value is System.DateTime)
								this.StartDepreciationDate = (System.DateTime?)value;
							break;
						case "StartUsingDate":

							if (value == null || value is System.DateTime)
								this.StartUsingDate = (System.DateTime?)value;
							break;
						case "PurchasedPrice":

							if (value == null || value is System.Decimal)
								this.PurchasedPrice = (System.Decimal?)value;
							break;
						case "UsageTimeEstimation":

							if (value == null || value is System.Int16)
								this.UsageTimeEstimation = (System.Int16?)value;
							break;
						case "AgeOfDepreciation":

							if (value == null || value is System.Int16)
								this.AgeOfDepreciation = (System.Int16?)value;
							break;
						case "SalesPrice":

							if (value == null || value is System.Decimal)
								this.SalesPrice = (System.Decimal?)value;
							break;
						case "CurrentValue":

							if (value == null || value is System.Decimal)
								this.CurrentValue = (System.Decimal?)value;
							break;
						case "CurrentCondition":

							if (value == null || value is System.Decimal)
								this.CurrentCondition = (System.Decimal?)value;
							break;
						case "CurrentUsageTimeEstimation":

							if (value == null || value is System.Byte)
								this.CurrentUsageTimeEstimation = (System.Byte?)value;
							break;
						case "ResidualValue":

							if (value == null || value is System.Decimal)
								this.ResidualValue = (System.Decimal?)value;
							break;
						case "InsuranceAmount":

							if (value == null || value is System.Decimal)
								this.InsuranceAmount = (System.Decimal?)value;
							break;
						case "GuaranteeExpiredDate":

							if (value == null || value is System.DateTime)
								this.GuaranteeExpiredDate = (System.DateTime?)value;
							break;
						case "LastInventoriedDate":

							if (value == null || value is System.DateTime)
								this.LastInventoriedDate = (System.DateTime?)value;
							break;
						case "LastMaintenanceDate":

							if (value == null || value is System.DateTime)
								this.LastMaintenanceDate = (System.DateTime?)value;
							break;
						case "NextMaintenanceDate":

							if (value == null || value is System.DateTime)
								this.NextMaintenanceDate = (System.DateTime?)value;
							break;
						case "MaintenanceInterval":

							if (value == null || value is System.Byte)
								this.MaintenanceInterval = (System.Byte?)value;
							break;
						case "IssuedDate":

							if (value == null || value is System.DateTime)
								this.IssuedDate = (System.DateTime?)value;
							break;
						case "IsAudiometer":

							if (value == null || value is System.Boolean)
								this.IsAudiometer = (System.Boolean?)value;
							break;
						case "IsSpirometer":

							if (value == null || value is System.Boolean)
								this.IsSpirometer = (System.Boolean?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "DateDisposed":

							if (value == null || value is System.DateTime)
								this.DateDisposed = (System.DateTime?)value;
							break;
						case "IsContinuousMaintenanceSchedule":

							if (value == null || value is System.Boolean)
								this.IsContinuousMaintenanceSchedule = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsFixedAsset":

							if (value == null || value is System.Boolean)
								this.IsFixedAsset = (System.Boolean?)value;
							break;
						case "ValueDisposed":

							if (value == null || value is System.Decimal)
								this.ValueDisposed = (System.Decimal?)value;
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
		/// Maps to Asset.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.AssetID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.AssetID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.AssetGroupID
		/// </summary>
		virtual public System.String AssetGroupID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.AssetGroupID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.AssetGroupID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.AssetLocationID
		/// </summary>
		virtual public System.String AssetLocationID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.AssetLocationID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.AssetLocationID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.DepartmentID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.DepartmentID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.AssetName
		/// </summary>
		virtual public System.String AssetName
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.AssetName);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.AssetName, value);
			}
		}
		/// <summary>
		/// Maps to Asset.AssetBookID
		/// </summary>
		virtual public System.String AssetBookID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.AssetBookID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.AssetBookID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRAssetType
		/// </summary>
		virtual public System.String SRAssetType
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRAssetType);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRAssetType, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRManufacturer
		/// </summary>
		virtual public System.String SRManufacturer
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRManufacturer);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRManufacturer, value);
			}
		}
		/// <summary>
		/// Maps to Asset.BrandName
		/// </summary>
		virtual public System.String BrandName
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.BrandName);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.BrandName, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SerialNumber
		/// </summary>
		virtual public System.String SerialNumber
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SerialNumber);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SerialNumber, value);
			}
		}
		/// <summary>
		/// Maps to Asset.DepreciationMethodID
		/// </summary>
		virtual public System.String DepreciationMethodID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.DepreciationMethodID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.DepreciationMethodID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.ItemUnit
		/// </summary>
		virtual public System.String ItemUnit
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.ItemUnit);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.ItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to Asset.PurchaseOrderNumber
		/// </summary>
		virtual public System.String PurchaseOrderNumber
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.PurchaseOrderNumber);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.PurchaseOrderNumber, value);
			}
		}
		/// <summary>
		/// Maps to Asset.PurchasedDate
		/// </summary>
		virtual public System.DateTime? PurchasedDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.PurchasedDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.PurchasedDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.StartDepreciationDate
		/// </summary>
		virtual public System.DateTime? StartDepreciationDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.StartDepreciationDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.StartDepreciationDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.StartUsingDate
		/// </summary>
		virtual public System.DateTime? StartUsingDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.StartUsingDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.StartUsingDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.PurchasedPrice
		/// </summary>
		virtual public System.Decimal? PurchasedPrice
		{
			get
			{
				return base.GetSystemDecimal(AssetMetadata.ColumnNames.PurchasedPrice);
			}

			set
			{
				base.SetSystemDecimal(AssetMetadata.ColumnNames.PurchasedPrice, value);
			}
		}
		/// <summary>
		/// Maps to Asset.UsageTimeEstimation
		/// </summary>
		virtual public System.Int16? UsageTimeEstimation
		{
			get
			{
				return base.GetSystemInt16(AssetMetadata.ColumnNames.UsageTimeEstimation);
			}

			set
			{
				base.SetSystemInt16(AssetMetadata.ColumnNames.UsageTimeEstimation, value);
			}
		}
		/// <summary>
		/// Maps to Asset.AgeOfDepreciation
		/// </summary>
		virtual public System.Int16? AgeOfDepreciation
		{
			get
			{
				return base.GetSystemInt16(AssetMetadata.ColumnNames.AgeOfDepreciation);
			}

			set
			{
				base.SetSystemInt16(AssetMetadata.ColumnNames.AgeOfDepreciation, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SalesPrice
		/// </summary>
		virtual public System.Decimal? SalesPrice
		{
			get
			{
				return base.GetSystemDecimal(AssetMetadata.ColumnNames.SalesPrice);
			}

			set
			{
				base.SetSystemDecimal(AssetMetadata.ColumnNames.SalesPrice, value);
			}
		}
		/// <summary>
		/// Maps to Asset.CurrentValue
		/// </summary>
		virtual public System.Decimal? CurrentValue
		{
			get
			{
				return base.GetSystemDecimal(AssetMetadata.ColumnNames.CurrentValue);
			}

			set
			{
				base.SetSystemDecimal(AssetMetadata.ColumnNames.CurrentValue, value);
			}
		}
		/// <summary>
		/// Maps to Asset.CurrentCondition
		/// </summary>
		virtual public System.Decimal? CurrentCondition
		{
			get
			{
				return base.GetSystemDecimal(AssetMetadata.ColumnNames.CurrentCondition);
			}

			set
			{
				base.SetSystemDecimal(AssetMetadata.ColumnNames.CurrentCondition, value);
			}
		}
		/// <summary>
		/// Maps to Asset.CurrentUsageTimeEstimation
		/// </summary>
		virtual public System.Byte? CurrentUsageTimeEstimation
		{
			get
			{
				return base.GetSystemByte(AssetMetadata.ColumnNames.CurrentUsageTimeEstimation);
			}

			set
			{
				base.SetSystemByte(AssetMetadata.ColumnNames.CurrentUsageTimeEstimation, value);
			}
		}
		/// <summary>
		/// Maps to Asset.ResidualValue
		/// </summary>
		virtual public System.Decimal? ResidualValue
		{
			get
			{
				return base.GetSystemDecimal(AssetMetadata.ColumnNames.ResidualValue);
			}

			set
			{
				base.SetSystemDecimal(AssetMetadata.ColumnNames.ResidualValue, value);
			}
		}
		/// <summary>
		/// Maps to Asset.InsuranceID
		/// </summary>
		virtual public System.String InsuranceID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.InsuranceID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.InsuranceID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.InsurancePolicyNo
		/// </summary>
		virtual public System.String InsurancePolicyNo
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.InsurancePolicyNo);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.InsurancePolicyNo, value);
			}
		}
		/// <summary>
		/// Maps to Asset.InsuranceAmount
		/// </summary>
		virtual public System.Decimal? InsuranceAmount
		{
			get
			{
				return base.GetSystemDecimal(AssetMetadata.ColumnNames.InsuranceAmount);
			}

			set
			{
				base.SetSystemDecimal(AssetMetadata.ColumnNames.InsuranceAmount, value);
			}
		}
		/// <summary>
		/// Maps to Asset.GuaranteeExpiredDate
		/// </summary>
		virtual public System.DateTime? GuaranteeExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.GuaranteeExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.GuaranteeExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.LastInventoriedDate
		/// </summary>
		virtual public System.DateTime? LastInventoriedDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.LastInventoriedDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.LastInventoriedDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.LastInventoriedBy
		/// </summary>
		virtual public System.String LastInventoriedBy
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.LastInventoriedBy);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.LastInventoriedBy, value);
			}
		}
		/// <summary>
		/// Maps to Asset.LastMaintenanceDate
		/// </summary>
		virtual public System.DateTime? LastMaintenanceDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.LastMaintenanceDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.LastMaintenanceDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.NextMaintenanceDate
		/// </summary>
		virtual public System.DateTime? NextMaintenanceDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.NextMaintenanceDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.NextMaintenanceDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.MaintenanceInterval
		/// </summary>
		virtual public System.Byte? MaintenanceInterval
		{
			get
			{
				return base.GetSystemByte(AssetMetadata.ColumnNames.MaintenanceInterval);
			}

			set
			{
				base.SetSystemByte(AssetMetadata.ColumnNames.MaintenanceInterval, value);
			}
		}
		/// <summary>
		/// Maps to Asset.MaintenanceIntervalIn
		/// </summary>
		virtual public System.String MaintenanceIntervalIn
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.MaintenanceIntervalIn);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.MaintenanceIntervalIn, value);
			}
		}
		/// <summary>
		/// Maps to Asset.MaintenanceServiceUnitID
		/// </summary>
		virtual public System.String MaintenanceServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.MaintenanceServiceUnitID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.MaintenanceServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IssuedDate
		/// </summary>
		virtual public System.DateTime? IssuedDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.IssuedDate);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.IssuedDate, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IssuedBy
		/// </summary>
		virtual public System.String IssuedBy
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.IssuedBy);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.IssuedBy, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRIssuedReason
		/// </summary>
		virtual public System.String SRIssuedReason
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRIssuedReason);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRIssuedReason, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IntervalUnit
		/// </summary>
		virtual public System.String IntervalUnit
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.IntervalUnit);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.IntervalUnit, value);
			}
		}
		/// <summary>
		/// Maps to Asset.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IsAudiometer
		/// </summary>
		virtual public System.Boolean? IsAudiometer
		{
			get
			{
				return base.GetSystemBoolean(AssetMetadata.ColumnNames.IsAudiometer);
			}

			set
			{
				base.SetSystemBoolean(AssetMetadata.ColumnNames.IsAudiometer, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IsSpirometer
		/// </summary>
		virtual public System.Boolean? IsSpirometer
		{
			get
			{
				return base.GetSystemBoolean(AssetMetadata.ColumnNames.IsSpirometer);
			}

			set
			{
				base.SetSystemBoolean(AssetMetadata.ColumnNames.IsSpirometer, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(AssetMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(AssetMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Asset.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRAssetsStatus
		/// </summary>
		virtual public System.String SRAssetsStatus
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRAssetsStatus);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRAssetsStatus, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRAssetsCondition
		/// </summary>
		virtual public System.String SRAssetsCondition
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRAssetsCondition);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRAssetsCondition, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRAssetsCriticality
		/// </summary>
		virtual public System.String SRAssetsCriticality
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRAssetsCriticality);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRAssetsCriticality, value);
			}
		}
		/// <summary>
		/// Maps to Asset.NotesToTechnician
		/// </summary>
		virtual public System.String NotesToTechnician
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.NotesToTechnician);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.NotesToTechnician, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SupplierID
		/// </summary>
		virtual public System.String SupplierID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SupplierID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SupplierID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRAssetsWarrantyContract
		/// </summary>
		virtual public System.String SRAssetsWarrantyContract
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRAssetsWarrantyContract);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRAssetsWarrantyContract, value);
			}
		}
		/// <summary>
		/// Maps to Asset.WarrantyContractNotes
		/// </summary>
		virtual public System.String WarrantyContractNotes
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.WarrantyContractNotes);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.WarrantyContractNotes, value);
			}
		}
		/// <summary>
		/// Maps to Asset.DateDisposed
		/// </summary>
		virtual public System.DateTime? DateDisposed
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.DateDisposed);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.DateDisposed, value);
			}
		}
		/// <summary>
		/// Maps to Asset.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IsContinuousMaintenanceSchedule
		/// </summary>
		virtual public System.Boolean? IsContinuousMaintenanceSchedule
		{
			get
			{
				return base.GetSystemBoolean(AssetMetadata.ColumnNames.IsContinuousMaintenanceSchedule);
			}

			set
			{
				base.SetSystemBoolean(AssetMetadata.ColumnNames.IsContinuousMaintenanceSchedule, value);
			}
		}
		/// <summary>
		/// Maps to Asset.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Asset.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Asset.AssetSubGroupId
		/// </summary>
		virtual public System.String AssetSubGroupId
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.AssetSubGroupId);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.AssetSubGroupId, value);
			}
		}
		/// <summary>
		/// Maps to Asset.IsFixedAsset
		/// </summary>
		virtual public System.Boolean? IsFixedAsset
		{
			get
			{
				return base.GetSystemBoolean(AssetMetadata.ColumnNames.IsFixedAsset);
			}

			set
			{
				base.SetSystemBoolean(AssetMetadata.ColumnNames.IsFixedAsset, value);
			}
		}
		/// <summary>
		/// Maps to Asset.ValueDisposed
		/// </summary>
		virtual public System.Decimal? ValueDisposed
		{
			get
			{
				return base.GetSystemDecimal(AssetMetadata.ColumnNames.ValueDisposed);
			}

			set
			{
				base.SetSystemDecimal(AssetMetadata.ColumnNames.ValueDisposed, value);
			}
		}
		/// <summary>
		/// Maps to Asset.SRAssetUsageTimeEstimation
		/// </summary>
		virtual public System.String SRAssetUsageTimeEstimation
		{
			get
			{
				return base.GetSystemString(AssetMetadata.ColumnNames.SRAssetUsageTimeEstimation);
			}

			set
			{
				base.SetSystemString(AssetMetadata.ColumnNames.SRAssetUsageTimeEstimation, value);
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
			public esStrings(esAsset entity)
			{
				this.entity = entity;
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
			public System.String AssetLocationID
			{
				get
				{
					System.String data = entity.AssetLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetLocationID = null;
					else entity.AssetLocationID = Convert.ToString(value);
				}
			}
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String AssetName
			{
				get
				{
					System.String data = entity.AssetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetName = null;
					else entity.AssetName = Convert.ToString(value);
				}
			}
			public System.String AssetBookID
			{
				get
				{
					System.String data = entity.AssetBookID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetBookID = null;
					else entity.AssetBookID = Convert.ToString(value);
				}
			}
			public System.String SRAssetType
			{
				get
				{
					System.String data = entity.SRAssetType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetType = null;
					else entity.SRAssetType = Convert.ToString(value);
				}
			}
			public System.String SRManufacturer
			{
				get
				{
					System.String data = entity.SRManufacturer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRManufacturer = null;
					else entity.SRManufacturer = Convert.ToString(value);
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
			public System.String SerialNumber
			{
				get
				{
					System.String data = entity.SerialNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SerialNumber = null;
					else entity.SerialNumber = Convert.ToString(value);
				}
			}
			public System.String DepreciationMethodID
			{
				get
				{
					System.String data = entity.DepreciationMethodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepreciationMethodID = null;
					else entity.DepreciationMethodID = Convert.ToString(value);
				}
			}
			public System.String ItemUnit
			{
				get
				{
					System.String data = entity.ItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemUnit = null;
					else entity.ItemUnit = Convert.ToString(value);
				}
			}
			public System.String PurchaseOrderNumber
			{
				get
				{
					System.String data = entity.PurchaseOrderNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchaseOrderNumber = null;
					else entity.PurchaseOrderNumber = Convert.ToString(value);
				}
			}
			public System.String PurchasedDate
			{
				get
				{
					System.DateTime? data = entity.PurchasedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchasedDate = null;
					else entity.PurchasedDate = Convert.ToDateTime(value);
				}
			}
			public System.String StartDepreciationDate
			{
				get
				{
					System.DateTime? data = entity.StartDepreciationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDepreciationDate = null;
					else entity.StartDepreciationDate = Convert.ToDateTime(value);
				}
			}
			public System.String StartUsingDate
			{
				get
				{
					System.DateTime? data = entity.StartUsingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartUsingDate = null;
					else entity.StartUsingDate = Convert.ToDateTime(value);
				}
			}
			public System.String PurchasedPrice
			{
				get
				{
					System.Decimal? data = entity.PurchasedPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchasedPrice = null;
					else entity.PurchasedPrice = Convert.ToDecimal(value);
				}
			}
			public System.String UsageTimeEstimation
			{
				get
				{
					System.Int16? data = entity.UsageTimeEstimation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UsageTimeEstimation = null;
					else entity.UsageTimeEstimation = Convert.ToInt16(value);
				}
			}
			public System.String AgeOfDepreciation
			{
				get
				{
					System.Int16? data = entity.AgeOfDepreciation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeOfDepreciation = null;
					else entity.AgeOfDepreciation = Convert.ToInt16(value);
				}
			}
			public System.String SalesPrice
			{
				get
				{
					System.Decimal? data = entity.SalesPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesPrice = null;
					else entity.SalesPrice = Convert.ToDecimal(value);
				}
			}
			public System.String CurrentValue
			{
				get
				{
					System.Decimal? data = entity.CurrentValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentValue = null;
					else entity.CurrentValue = Convert.ToDecimal(value);
				}
			}
			public System.String CurrentCondition
			{
				get
				{
					System.Decimal? data = entity.CurrentCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentCondition = null;
					else entity.CurrentCondition = Convert.ToDecimal(value);
				}
			}
			public System.String CurrentUsageTimeEstimation
			{
				get
				{
					System.Byte? data = entity.CurrentUsageTimeEstimation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentUsageTimeEstimation = null;
					else entity.CurrentUsageTimeEstimation = Convert.ToByte(value);
				}
			}
			public System.String ResidualValue
			{
				get
				{
					System.Decimal? data = entity.ResidualValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResidualValue = null;
					else entity.ResidualValue = Convert.ToDecimal(value);
				}
			}
			public System.String InsuranceID
			{
				get
				{
					System.String data = entity.InsuranceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsuranceID = null;
					else entity.InsuranceID = Convert.ToString(value);
				}
			}
			public System.String InsurancePolicyNo
			{
				get
				{
					System.String data = entity.InsurancePolicyNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsurancePolicyNo = null;
					else entity.InsurancePolicyNo = Convert.ToString(value);
				}
			}
			public System.String InsuranceAmount
			{
				get
				{
					System.Decimal? data = entity.InsuranceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsuranceAmount = null;
					else entity.InsuranceAmount = Convert.ToDecimal(value);
				}
			}
			public System.String GuaranteeExpiredDate
			{
				get
				{
					System.DateTime? data = entity.GuaranteeExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuaranteeExpiredDate = null;
					else entity.GuaranteeExpiredDate = Convert.ToDateTime(value);
				}
			}
			public System.String LastInventoriedDate
			{
				get
				{
					System.DateTime? data = entity.LastInventoriedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastInventoriedDate = null;
					else entity.LastInventoriedDate = Convert.ToDateTime(value);
				}
			}
			public System.String LastInventoriedBy
			{
				get
				{
					System.String data = entity.LastInventoriedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastInventoriedBy = null;
					else entity.LastInventoriedBy = Convert.ToString(value);
				}
			}
			public System.String LastMaintenanceDate
			{
				get
				{
					System.DateTime? data = entity.LastMaintenanceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastMaintenanceDate = null;
					else entity.LastMaintenanceDate = Convert.ToDateTime(value);
				}
			}
			public System.String NextMaintenanceDate
			{
				get
				{
					System.DateTime? data = entity.NextMaintenanceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NextMaintenanceDate = null;
					else entity.NextMaintenanceDate = Convert.ToDateTime(value);
				}
			}
			public System.String MaintenanceInterval
			{
				get
				{
					System.Byte? data = entity.MaintenanceInterval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaintenanceInterval = null;
					else entity.MaintenanceInterval = Convert.ToByte(value);
				}
			}
			public System.String MaintenanceIntervalIn
			{
				get
				{
					System.String data = entity.MaintenanceIntervalIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaintenanceIntervalIn = null;
					else entity.MaintenanceIntervalIn = Convert.ToString(value);
				}
			}
			public System.String MaintenanceServiceUnitID
			{
				get
				{
					System.String data = entity.MaintenanceServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaintenanceServiceUnitID = null;
					else entity.MaintenanceServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String IssuedDate
			{
				get
				{
					System.DateTime? data = entity.IssuedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IssuedDate = null;
					else entity.IssuedDate = Convert.ToDateTime(value);
				}
			}
			public System.String IssuedBy
			{
				get
				{
					System.String data = entity.IssuedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IssuedBy = null;
					else entity.IssuedBy = Convert.ToString(value);
				}
			}
			public System.String SRIssuedReason
			{
				get
				{
					System.String data = entity.SRIssuedReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIssuedReason = null;
					else entity.SRIssuedReason = Convert.ToString(value);
				}
			}
			public System.String IntervalUnit
			{
				get
				{
					System.String data = entity.IntervalUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IntervalUnit = null;
					else entity.IntervalUnit = Convert.ToString(value);
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
			public System.String IsAudiometer
			{
				get
				{
					System.Boolean? data = entity.IsAudiometer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAudiometer = null;
					else entity.IsAudiometer = Convert.ToBoolean(value);
				}
			}
			public System.String IsSpirometer
			{
				get
				{
					System.Boolean? data = entity.IsSpirometer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSpirometer = null;
					else entity.IsSpirometer = Convert.ToBoolean(value);
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
			public System.String SRAssetsStatus
			{
				get
				{
					System.String data = entity.SRAssetsStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetsStatus = null;
					else entity.SRAssetsStatus = Convert.ToString(value);
				}
			}
			public System.String SRAssetsCondition
			{
				get
				{
					System.String data = entity.SRAssetsCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetsCondition = null;
					else entity.SRAssetsCondition = Convert.ToString(value);
				}
			}
			public System.String SRAssetsCriticality
			{
				get
				{
					System.String data = entity.SRAssetsCriticality;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetsCriticality = null;
					else entity.SRAssetsCriticality = Convert.ToString(value);
				}
			}
			public System.String NotesToTechnician
			{
				get
				{
					System.String data = entity.NotesToTechnician;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NotesToTechnician = null;
					else entity.NotesToTechnician = Convert.ToString(value);
				}
			}
			public System.String SupplierID
			{
				get
				{
					System.String data = entity.SupplierID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupplierID = null;
					else entity.SupplierID = Convert.ToString(value);
				}
			}
			public System.String SRAssetsWarrantyContract
			{
				get
				{
					System.String data = entity.SRAssetsWarrantyContract;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetsWarrantyContract = null;
					else entity.SRAssetsWarrantyContract = Convert.ToString(value);
				}
			}
			public System.String WarrantyContractNotes
			{
				get
				{
					System.String data = entity.WarrantyContractNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WarrantyContractNotes = null;
					else entity.WarrantyContractNotes = Convert.ToString(value);
				}
			}
			public System.String DateDisposed
			{
				get
				{
					System.DateTime? data = entity.DateDisposed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateDisposed = null;
					else entity.DateDisposed = Convert.ToDateTime(value);
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
			public System.String IsContinuousMaintenanceSchedule
			{
				get
				{
					System.Boolean? data = entity.IsContinuousMaintenanceSchedule;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsContinuousMaintenanceSchedule = null;
					else entity.IsContinuousMaintenanceSchedule = Convert.ToBoolean(value);
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
			public System.String AssetSubGroupId
			{
				get
				{
					System.String data = entity.AssetSubGroupId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetSubGroupId = null;
					else entity.AssetSubGroupId = Convert.ToString(value);
				}
			}
			public System.String IsFixedAsset
			{
				get
				{
					System.Boolean? data = entity.IsFixedAsset;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFixedAsset = null;
					else entity.IsFixedAsset = Convert.ToBoolean(value);
				}
			}
			public System.String ValueDisposed
			{
				get
				{
					System.Decimal? data = entity.ValueDisposed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueDisposed = null;
					else entity.ValueDisposed = Convert.ToDecimal(value);
				}
			}
			public System.String SRAssetUsageTimeEstimation
			{
				get
				{
					System.String data = entity.SRAssetUsageTimeEstimation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetUsageTimeEstimation = null;
					else entity.SRAssetUsageTimeEstimation = Convert.ToString(value);
				}
			}
			private esAsset entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetQuery query)
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
				throw new Exception("esAsset can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Asset : esAsset
	{
	}

	[Serializable]
	abstract public class esAssetQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AssetMetadata.Meta();
			}
		}

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		}

		public esQueryItem AssetGroupID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.AssetGroupID, esSystemType.String);
			}
		}

		public esQueryItem AssetLocationID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.AssetLocationID, esSystemType.String);
			}
		}

		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem AssetName
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.AssetName, esSystemType.String);
			}
		}

		public esQueryItem AssetBookID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.AssetBookID, esSystemType.String);
			}
		}

		public esQueryItem SRAssetType
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRAssetType, esSystemType.String);
			}
		}

		public esQueryItem SRManufacturer
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRManufacturer, esSystemType.String);
			}
		}

		public esQueryItem BrandName
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.BrandName, esSystemType.String);
			}
		}

		public esQueryItem SerialNumber
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SerialNumber, esSystemType.String);
			}
		}

		public esQueryItem DepreciationMethodID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.DepreciationMethodID, esSystemType.String);
			}
		}

		public esQueryItem ItemUnit
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.ItemUnit, esSystemType.String);
			}
		}

		public esQueryItem PurchaseOrderNumber
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.PurchaseOrderNumber, esSystemType.String);
			}
		}

		public esQueryItem PurchasedDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.PurchasedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem StartDepreciationDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.StartDepreciationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem StartUsingDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.StartUsingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PurchasedPrice
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.PurchasedPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem UsageTimeEstimation
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.UsageTimeEstimation, esSystemType.Int16);
			}
		}

		public esQueryItem AgeOfDepreciation
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.AgeOfDepreciation, esSystemType.Int16);
			}
		}

		public esQueryItem SalesPrice
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SalesPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem CurrentValue
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.CurrentValue, esSystemType.Decimal);
			}
		}

		public esQueryItem CurrentCondition
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.CurrentCondition, esSystemType.Decimal);
			}
		}

		public esQueryItem CurrentUsageTimeEstimation
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.CurrentUsageTimeEstimation, esSystemType.Byte);
			}
		}

		public esQueryItem ResidualValue
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.ResidualValue, esSystemType.Decimal);
			}
		}

		public esQueryItem InsuranceID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.InsuranceID, esSystemType.String);
			}
		}

		public esQueryItem InsurancePolicyNo
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.InsurancePolicyNo, esSystemType.String);
			}
		}

		public esQueryItem InsuranceAmount
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.InsuranceAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem GuaranteeExpiredDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.GuaranteeExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LastInventoriedDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.LastInventoriedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LastInventoriedBy
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.LastInventoriedBy, esSystemType.String);
			}
		}

		public esQueryItem LastMaintenanceDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.LastMaintenanceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem NextMaintenanceDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.NextMaintenanceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem MaintenanceInterval
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.MaintenanceInterval, esSystemType.Byte);
			}
		}

		public esQueryItem MaintenanceIntervalIn
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.MaintenanceIntervalIn, esSystemType.String);
			}
		}

		public esQueryItem MaintenanceServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.MaintenanceServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem IssuedDate
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IssuedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IssuedBy
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IssuedBy, esSystemType.String);
			}
		}

		public esQueryItem SRIssuedReason
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRIssuedReason, esSystemType.String);
			}
		}

		public esQueryItem IntervalUnit
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IntervalUnit, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem IsAudiometer
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IsAudiometer, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSpirometer
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IsSpirometer, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem SRAssetsStatus
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRAssetsStatus, esSystemType.String);
			}
		}

		public esQueryItem SRAssetsCondition
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRAssetsCondition, esSystemType.String);
			}
		}

		public esQueryItem SRAssetsCriticality
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRAssetsCriticality, esSystemType.String);
			}
		}

		public esQueryItem NotesToTechnician
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.NotesToTechnician, esSystemType.String);
			}
		}

		public esQueryItem SupplierID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SupplierID, esSystemType.String);
			}
		}

		public esQueryItem SRAssetsWarrantyContract
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRAssetsWarrantyContract, esSystemType.String);
			}
		}

		public esQueryItem WarrantyContractNotes
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.WarrantyContractNotes, esSystemType.String);
			}
		}

		public esQueryItem DateDisposed
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.DateDisposed, esSystemType.DateTime);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem IsContinuousMaintenanceSchedule
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IsContinuousMaintenanceSchedule, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem AssetSubGroupId
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.AssetSubGroupId, esSystemType.String);
			}
		}

		public esQueryItem IsFixedAsset
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.IsFixedAsset, esSystemType.Boolean);
			}
		}

		public esQueryItem ValueDisposed
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.ValueDisposed, esSystemType.Decimal);
			}
		}

		public esQueryItem SRAssetUsageTimeEstimation
		{
			get
			{
				return new esQueryItem(this, AssetMetadata.ColumnNames.SRAssetUsageTimeEstimation, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetCollection")]
	public partial class AssetCollection : esAssetCollection, IEnumerable<Asset>
	{
		public AssetCollection()
		{

		}

		public static implicit operator List<Asset>(AssetCollection coll)
		{
			List<Asset> list = new List<Asset>();

			foreach (Asset emp in coll)
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
				return AssetMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Asset(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Asset();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AssetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetQuery();
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
		public bool Load(AssetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Asset AddNew()
		{
			Asset entity = base.AddNewEntity() as Asset;

			return entity;
		}
		public Asset FindByPrimaryKey(String assetID)
		{
			return base.FindByPrimaryKey(assetID) as Asset;
		}

		#region IEnumerable< Asset> Members

		IEnumerator<Asset> IEnumerable<Asset>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Asset;
			}
		}

		#endregion

		private AssetQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Asset' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Asset ({AssetID})")]
	[Serializable]
	public partial class Asset : esAsset
	{
		public Asset()
		{
		}

		public Asset(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetMetadata.Meta();
			}
		}

		override protected esAssetQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AssetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetQuery();
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
		public bool Load(AssetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AssetQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AssetQuery : esAssetQuery
	{
		public AssetQuery()
		{

		}

		public AssetQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AssetQuery";
		}
	}

	[Serializable]
	public partial class AssetMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetMetadata.ColumnNames.AssetID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.AssetID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.AssetGroupID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.AssetGroupID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.AssetLocationID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.AssetLocationID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.DepartmentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.AssetName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.AssetName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.AssetBookID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.AssetBookID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRAssetType, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRAssetType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRManufacturer, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRManufacturer;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.BrandName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.BrandName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SerialNumber, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SerialNumber;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.DepreciationMethodID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.DepreciationMethodID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.ItemUnit, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.ItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.PurchaseOrderNumber, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.PurchaseOrderNumber;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.PurchasedDate, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.PurchasedDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.StartDepreciationDate, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.StartDepreciationDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.StartUsingDate, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.StartUsingDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.PurchasedPrice, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMetadata.PropertyNames.PurchasedPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.UsageTimeEstimation, 18, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = AssetMetadata.PropertyNames.UsageTimeEstimation;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.AgeOfDepreciation, 19, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = AssetMetadata.PropertyNames.AgeOfDepreciation;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SalesPrice, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMetadata.PropertyNames.SalesPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.CurrentValue, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMetadata.PropertyNames.CurrentValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.CurrentCondition, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMetadata.PropertyNames.CurrentCondition;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.CurrentUsageTimeEstimation, 23, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = AssetMetadata.PropertyNames.CurrentUsageTimeEstimation;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.ResidualValue, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMetadata.PropertyNames.ResidualValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.InsuranceID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.InsuranceID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.InsurancePolicyNo, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.InsurancePolicyNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.InsuranceAmount, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMetadata.PropertyNames.InsuranceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.GuaranteeExpiredDate, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.GuaranteeExpiredDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.LastInventoriedDate, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.LastInventoriedDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.LastInventoriedBy, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.LastInventoriedBy;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.LastMaintenanceDate, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.LastMaintenanceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.NextMaintenanceDate, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.NextMaintenanceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.MaintenanceInterval, 33, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = AssetMetadata.PropertyNames.MaintenanceInterval;
			c.NumericPrecision = 3;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.MaintenanceIntervalIn, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.MaintenanceIntervalIn;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.MaintenanceServiceUnitID, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.MaintenanceServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IssuedDate, 36, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.IssuedDate;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IssuedBy, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.IssuedBy;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRIssuedReason, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRIssuedReason;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IntervalUnit, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.IntervalUnit;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.ReferenceNo, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IsAudiometer, 41, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMetadata.PropertyNames.IsAudiometer;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IsSpirometer, 42, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMetadata.PropertyNames.IsSpirometer;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IsActive, 43, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.Notes, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRAssetsStatus, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRAssetsStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRAssetsCondition, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRAssetsCondition;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRAssetsCriticality, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRAssetsCriticality;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.NotesToTechnician, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.NotesToTechnician;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SupplierID, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SupplierID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRAssetsWarrantyContract, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRAssetsWarrantyContract;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.WarrantyContractNotes, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.WarrantyContractNotes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.DateDisposed, 52, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.DateDisposed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.ItemID, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IsContinuousMaintenanceSchedule, 54, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMetadata.PropertyNames.IsContinuousMaintenanceSchedule;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.LastUpdateDateTime, 55, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.LastUpdateByUserID, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.AssetSubGroupId, 57, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.AssetSubGroupId;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.IsFixedAsset, 58, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMetadata.PropertyNames.IsFixedAsset;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.ValueDisposed, 59, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMetadata.PropertyNames.ValueDisposed;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetMetadata.ColumnNames.SRAssetUsageTimeEstimation, 60, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMetadata.PropertyNames.SRAssetUsageTimeEstimation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AssetMetadata Meta()
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
			public const string AssetID = "AssetID";
			public const string AssetGroupID = "AssetGroupID";
			public const string AssetLocationID = "AssetLocationID";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string AssetName = "AssetName";
			public const string AssetBookID = "AssetBookID";
			public const string SRAssetType = "SRAssetType";
			public const string SRManufacturer = "SRManufacturer";
			public const string BrandName = "BrandName";
			public const string SerialNumber = "SerialNumber";
			public const string DepreciationMethodID = "DepreciationMethodID";
			public const string ItemUnit = "ItemUnit";
			public const string PurchaseOrderNumber = "PurchaseOrderNumber";
			public const string PurchasedDate = "PurchasedDate";
			public const string StartDepreciationDate = "StartDepreciationDate";
			public const string StartUsingDate = "StartUsingDate";
			public const string PurchasedPrice = "PurchasedPrice";
			public const string UsageTimeEstimation = "UsageTimeEstimation";
			public const string AgeOfDepreciation = "AgeOfDepreciation";
			public const string SalesPrice = "SalesPrice";
			public const string CurrentValue = "CurrentValue";
			public const string CurrentCondition = "CurrentCondition";
			public const string CurrentUsageTimeEstimation = "CurrentUsageTimeEstimation";
			public const string ResidualValue = "ResidualValue";
			public const string InsuranceID = "InsuranceID";
			public const string InsurancePolicyNo = "InsurancePolicyNo";
			public const string InsuranceAmount = "InsuranceAmount";
			public const string GuaranteeExpiredDate = "GuaranteeExpiredDate";
			public const string LastInventoriedDate = "LastInventoriedDate";
			public const string LastInventoriedBy = "LastInventoriedBy";
			public const string LastMaintenanceDate = "LastMaintenanceDate";
			public const string NextMaintenanceDate = "NextMaintenanceDate";
			public const string MaintenanceInterval = "MaintenanceInterval";
			public const string MaintenanceIntervalIn = "MaintenanceIntervalIn";
			public const string MaintenanceServiceUnitID = "MaintenanceServiceUnitID";
			public const string IssuedDate = "IssuedDate";
			public const string IssuedBy = "IssuedBy";
			public const string SRIssuedReason = "SRIssuedReason";
			public const string IntervalUnit = "IntervalUnit";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsAudiometer = "IsAudiometer";
			public const string IsSpirometer = "IsSpirometer";
			public const string IsActive = "IsActive";
			public const string Notes = "Notes";
			public const string SRAssetsStatus = "SRAssetsStatus";
			public const string SRAssetsCondition = "SRAssetsCondition";
			public const string SRAssetsCriticality = "SRAssetsCriticality";
			public const string NotesToTechnician = "NotesToTechnician";
			public const string SupplierID = "SupplierID";
			public const string SRAssetsWarrantyContract = "SRAssetsWarrantyContract";
			public const string WarrantyContractNotes = "WarrantyContractNotes";
			public const string DateDisposed = "DateDisposed";
			public const string ItemID = "ItemID";
			public const string IsContinuousMaintenanceSchedule = "IsContinuousMaintenanceSchedule";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AssetSubGroupId = "AssetSubGroupId";
			public const string IsFixedAsset = "IsFixedAsset";
			public const string ValueDisposed = "ValueDisposed";
			public const string SRAssetUsageTimeEstimation = "SRAssetUsageTimeEstimation";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AssetID = "AssetID";
			public const string AssetGroupID = "AssetGroupID";
			public const string AssetLocationID = "AssetLocationID";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string AssetName = "AssetName";
			public const string AssetBookID = "AssetBookID";
			public const string SRAssetType = "SRAssetType";
			public const string SRManufacturer = "SRManufacturer";
			public const string BrandName = "BrandName";
			public const string SerialNumber = "SerialNumber";
			public const string DepreciationMethodID = "DepreciationMethodID";
			public const string ItemUnit = "ItemUnit";
			public const string PurchaseOrderNumber = "PurchaseOrderNumber";
			public const string PurchasedDate = "PurchasedDate";
			public const string StartDepreciationDate = "StartDepreciationDate";
			public const string StartUsingDate = "StartUsingDate";
			public const string PurchasedPrice = "PurchasedPrice";
			public const string UsageTimeEstimation = "UsageTimeEstimation";
			public const string AgeOfDepreciation = "AgeOfDepreciation";
			public const string SalesPrice = "SalesPrice";
			public const string CurrentValue = "CurrentValue";
			public const string CurrentCondition = "CurrentCondition";
			public const string CurrentUsageTimeEstimation = "CurrentUsageTimeEstimation";
			public const string ResidualValue = "ResidualValue";
			public const string InsuranceID = "InsuranceID";
			public const string InsurancePolicyNo = "InsurancePolicyNo";
			public const string InsuranceAmount = "InsuranceAmount";
			public const string GuaranteeExpiredDate = "GuaranteeExpiredDate";
			public const string LastInventoriedDate = "LastInventoriedDate";
			public const string LastInventoriedBy = "LastInventoriedBy";
			public const string LastMaintenanceDate = "LastMaintenanceDate";
			public const string NextMaintenanceDate = "NextMaintenanceDate";
			public const string MaintenanceInterval = "MaintenanceInterval";
			public const string MaintenanceIntervalIn = "MaintenanceIntervalIn";
			public const string MaintenanceServiceUnitID = "MaintenanceServiceUnitID";
			public const string IssuedDate = "IssuedDate";
			public const string IssuedBy = "IssuedBy";
			public const string SRIssuedReason = "SRIssuedReason";
			public const string IntervalUnit = "IntervalUnit";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsAudiometer = "IsAudiometer";
			public const string IsSpirometer = "IsSpirometer";
			public const string IsActive = "IsActive";
			public const string Notes = "Notes";
			public const string SRAssetsStatus = "SRAssetsStatus";
			public const string SRAssetsCondition = "SRAssetsCondition";
			public const string SRAssetsCriticality = "SRAssetsCriticality";
			public const string NotesToTechnician = "NotesToTechnician";
			public const string SupplierID = "SupplierID";
			public const string SRAssetsWarrantyContract = "SRAssetsWarrantyContract";
			public const string WarrantyContractNotes = "WarrantyContractNotes";
			public const string DateDisposed = "DateDisposed";
			public const string ItemID = "ItemID";
			public const string IsContinuousMaintenanceSchedule = "IsContinuousMaintenanceSchedule";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AssetSubGroupId = "AssetSubGroupId";
			public const string IsFixedAsset = "IsFixedAsset";
			public const string ValueDisposed = "ValueDisposed";
			public const string SRAssetUsageTimeEstimation = "SRAssetUsageTimeEstimation";
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
			lock (typeof(AssetMetadata))
			{
				if (AssetMetadata.mapDelegates == null)
				{
					AssetMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AssetMetadata.meta == null)
				{
					AssetMetadata.meta = new AssetMetadata();
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

				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetBookID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRManufacturer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BrandName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SerialNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepreciationMethodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PurchaseOrderNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PurchasedDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("StartDepreciationDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("StartUsingDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("PurchasedPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("UsageTimeEstimation", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("AgeOfDepreciation", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SalesPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CurrentValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CurrentCondition", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CurrentUsageTimeEstimation", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("ResidualValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("InsuranceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsurancePolicyNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsuranceAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("GuaranteeExpiredDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("LastInventoriedDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("LastInventoriedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastMaintenanceDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("NextMaintenanceDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("MaintenanceInterval", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("MaintenanceIntervalIn", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaintenanceServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IssuedDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("IssuedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIssuedReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IntervalUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAudiometer", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSpirometer", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetsStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetsCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetsCriticality", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NotesToTechnician", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupplierID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetsWarrantyContract", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WarrantyContractNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateDisposed", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsContinuousMaintenanceSchedule", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetSubGroupId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFixedAsset", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValueDisposed", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRAssetUsageTimeEstimation", new esTypeMap("varchar", "System.String"));


				meta.Source = "Asset";
				meta.Destination = "Asset";
				meta.spInsert = "proc_AssetInsert";
				meta.spUpdate = "proc_AssetUpdate";
				meta.spDelete = "proc_AssetDelete";
				meta.spLoadAll = "proc_AssetLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
