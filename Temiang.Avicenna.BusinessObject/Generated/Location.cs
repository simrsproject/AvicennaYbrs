/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/19/2021 5:10:13 PM
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
	abstract public class esLocationCollection : esEntityCollectionWAuditLog
	{
		public esLocationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LocationCollection";
		}

		#region Query Logic
		protected void InitQuery(esLocationQuery query)
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
			this.InitQuery(query as esLocationQuery);
		}
		#endregion

		virtual public Location DetachEntity(Location entity)
		{
			return base.DetachEntity(entity) as Location;
		}

		virtual public Location AttachEntity(Location entity)
		{
			return base.AttachEntity(entity) as Location;
		}

		virtual public void Combine(LocationCollection collection)
		{
			base.Combine(collection);
		}

		new public Location this[int index]
		{
			get
			{
				return base[index] as Location;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Location);
		}
	}

	[Serializable]
	abstract public class esLocation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLocationQuery GetDynamicQuery()
		{
			return null;
		}

		public esLocation()
		{
		}

		public esLocation(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String locationID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String locationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID);
		}

		private bool LoadByPrimaryKeyDynamic(String locationID)
		{
			esLocationQuery query = this.GetDynamicQuery();
			query.Where(query.LocationID == locationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String locationID)
		{
			esParameters parms = new esParameters();
			parms.Add("LocationID", locationID);
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
						case "LocationID": this.str.LocationID = (string)value; break;
						case "LocationName": this.str.LocationName = (string)value; break;
						case "ShortName": this.str.ShortName = (string)value; break;
						case "ParentID": this.str.ParentID = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "PermitID": this.str.PermitID = (string)value; break;
						case "IsHeader": this.str.IsHeader = (string)value; break;
						case "IsHoldForTransaction": this.str.IsHoldForTransaction = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "ChartOfAccountIdIncome": this.str.ChartOfAccountIdIncome = (string)value; break;
						case "SubledgerIdIncome": this.str.SubledgerIdIncome = (string)value; break;
						case "ChartOfAccountIdInventory": this.str.ChartOfAccountIdInventory = (string)value; break;
						case "SubledgerIdInventory": this.str.SubledgerIdInventory = (string)value; break;
						case "ChartOfAccountIdCOGS": this.str.ChartOfAccountIdCOGS = (string)value; break;
						case "SubledgerIdCOGS": this.str.SubledgerIdCOGS = (string)value; break;
						case "ChartOfAccountIdSalesReturn": this.str.ChartOfAccountIdSalesReturn = (string)value; break;
						case "SubledgerIdSalesReturn": this.str.SubledgerIdSalesReturn = (string)value; break;
						case "ChartOfAccountIdPurchaseReturn": this.str.ChartOfAccountIdPurchaseReturn = (string)value; break;
						case "SubledgerIdPurchaseReturn": this.str.SubledgerIdPurchaseReturn = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ChartOfAccountIdAcrual": this.str.ChartOfAccountIdAcrual = (string)value; break;
						case "SubledgerIdAcrual": this.str.SubledgerIdAcrual = (string)value; break;
						case "ChartOfAccountIdDiscount": this.str.ChartOfAccountIdDiscount = (string)value; break;
						case "SubledgerIdDiscount": this.str.SubledgerIdDiscount = (string)value; break;
						case "ChartOfAccountIdCost": this.str.ChartOfAccountIdCost = (string)value; break;
						case "SubledgerIdCost": this.str.SubledgerIdCost = (string)value; break;
						case "SRTypeOfInventory": this.str.SRTypeOfInventory = (string)value; break;
						case "IsAllowedToStockGoods": this.str.IsAllowedToStockGoods = (string)value; break;
						case "IsConsignment": this.str.IsConsignment = (string)value; break;
						case "IsValidateMaxValueOnDistReqForIpm": this.str.IsValidateMaxValueOnDistReqForIpm = (string)value; break;
						case "IsValidateMaxValueOnDistReqForIpnm": this.str.IsValidateMaxValueOnDistReqForIpnm = (string)value; break;
						case "IsValidateMaxValueOnDistReqForIk": this.str.IsValidateMaxValueOnDistReqForIk = (string)value; break;
						case "LastHoldForTransactionDateTime": this.str.LastHoldForTransactionDateTime = (string)value; break;
						case "LastHoldForTransactionByUserID": this.str.LastHoldForTransactionByUserID = (string)value; break;
						case "SRStockGroup": this.str.SRStockGroup = (string)value; break;
						case "IsAutoUpdateStockMinMax": this.str.IsAutoUpdateStockMinMax = (string)value; break;
						case "IsValidateMaxValueOnPurcReqForIpm": this.str.IsValidateMaxValueOnPurcReqForIpm = (string)value; break;
						case "IsValidateMaxValueOnPurcReqForIpnm": this.str.IsValidateMaxValueOnPurcReqForIpnm = (string)value; break;
						case "IsValidateMaxValueOnPurcReqForIk": this.str.IsValidateMaxValueOnPurcReqForIk = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsHeader":

							if (value == null || value is System.Boolean)
								this.IsHeader = (System.Boolean?)value;
							break;
						case "IsHoldForTransaction":

							if (value == null || value is System.Boolean)
								this.IsHoldForTransaction = (System.Boolean?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdIncome":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdIncome = (System.Int32?)value;
							break;
						case "SubledgerIdIncome":

							if (value == null || value is System.Int32)
								this.SubledgerIdIncome = (System.Int32?)value;
							break;
						case "ChartOfAccountIdInventory":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdInventory = (System.Int32?)value;
							break;
						case "SubledgerIdInventory":

							if (value == null || value is System.Int32)
								this.SubledgerIdInventory = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCOGS":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCOGS = (System.Int32?)value;
							break;
						case "SubledgerIdCOGS":

							if (value == null || value is System.Int32)
								this.SubledgerIdCOGS = (System.Int32?)value;
							break;
						case "ChartOfAccountIdSalesReturn":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdSalesReturn = (System.Int32?)value;
							break;
						case "SubledgerIdSalesReturn":

							if (value == null || value is System.Int32)
								this.SubledgerIdSalesReturn = (System.Int32?)value;
							break;
						case "ChartOfAccountIdPurchaseReturn":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdPurchaseReturn = (System.Int32?)value;
							break;
						case "SubledgerIdPurchaseReturn":

							if (value == null || value is System.Int32)
								this.SubledgerIdPurchaseReturn = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ChartOfAccountIdAcrual":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdAcrual = (System.Int32?)value;
							break;
						case "SubledgerIdAcrual":

							if (value == null || value is System.Int32)
								this.SubledgerIdAcrual = (System.Int32?)value;
							break;
						case "ChartOfAccountIdDiscount":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdDiscount = (System.Int32?)value;
							break;
						case "SubledgerIdDiscount":

							if (value == null || value is System.Int32)
								this.SubledgerIdDiscount = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCost":

							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCost = (System.Int32?)value;
							break;
						case "SubledgerIdCost":

							if (value == null || value is System.Int32)
								this.SubledgerIdCost = (System.Int32?)value;
							break;
						case "IsAllowedToStockGoods":

							if (value == null || value is System.Boolean)
								this.IsAllowedToStockGoods = (System.Boolean?)value;
							break;
						case "IsConsignment":

							if (value == null || value is System.Boolean)
								this.IsConsignment = (System.Boolean?)value;
							break;
						case "IsValidateMaxValueOnDistReqForIpm":

							if (value == null || value is System.Boolean)
								this.IsValidateMaxValueOnDistReqForIpm = (System.Boolean?)value;
							break;
						case "IsValidateMaxValueOnDistReqForIpnm":

							if (value == null || value is System.Boolean)
								this.IsValidateMaxValueOnDistReqForIpnm = (System.Boolean?)value;
							break;
						case "IsValidateMaxValueOnDistReqForIk":

							if (value == null || value is System.Boolean)
								this.IsValidateMaxValueOnDistReqForIk = (System.Boolean?)value;
							break;
						case "LastHoldForTransactionDateTime":

							if (value == null || value is System.DateTime)
								this.LastHoldForTransactionDateTime = (System.DateTime?)value;
							break;
						case "IsAutoUpdateStockMinMax":

							if (value == null || value is System.Boolean)
								this.IsAutoUpdateStockMinMax = (System.Boolean?)value;
							break;
						case "IsValidateMaxValueOnPurcReqForIpm":

							if (value == null || value is System.Boolean)
								this.IsValidateMaxValueOnPurcReqForIpm = (System.Boolean?)value;
							break;
						case "IsValidateMaxValueOnPurcReqForIpnm":

							if (value == null || value is System.Boolean)
								this.IsValidateMaxValueOnPurcReqForIpnm = (System.Boolean?)value;
							break;
						case "IsValidateMaxValueOnPurcReqForIk":

							if (value == null || value is System.Boolean)
								this.IsValidateMaxValueOnPurcReqForIk = (System.Boolean?)value;
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
		/// Maps to Location.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to Location.LocationName
		/// </summary>
		virtual public System.String LocationName
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.LocationName);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.LocationName, value);
			}
		}
		/// <summary>
		/// Maps to Location.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.ShortName);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.ShortName, value);
			}
		}
		/// <summary>
		/// Maps to Location.ParentID
		/// </summary>
		virtual public System.String ParentID
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.ParentID);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.ParentID, value);
			}
		}
		/// <summary>
		/// Maps to Location.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.ItemGroupID);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to Location.PermitID
		/// </summary>
		virtual public System.String PermitID
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.PermitID);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.PermitID, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsHeader
		/// </summary>
		virtual public System.Boolean? IsHeader
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsHeader);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsHeader, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsHoldForTransaction
		/// </summary>
		virtual public System.Boolean? IsHoldForTransaction
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsHoldForTransaction);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsHoldForTransaction, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdIncome
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncome
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdIncome);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdIncome
		/// </summary>
		virtual public System.Int32? SubledgerIdIncome
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdIncome);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdInventory
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdInventory
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdInventory);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdInventory
		/// </summary>
		virtual public System.Int32? SubledgerIdInventory
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdInventory);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdInventory, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdCOGS
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCOGS
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdCOGS);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdCOGS
		/// </summary>
		virtual public System.Int32? SubledgerIdCOGS
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdCOGS);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdCOGS, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdSalesReturn
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdSalesReturn
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdSalesReturn);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdSalesReturn, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdSalesReturn
		/// </summary>
		virtual public System.Int32? SubledgerIdSalesReturn
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdSalesReturn);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdSalesReturn, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdPurchaseReturn
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdPurchaseReturn
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdPurchaseReturn
		/// </summary>
		virtual public System.Int32? SubledgerIdPurchaseReturn
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdPurchaseReturn);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdPurchaseReturn, value);
			}
		}
		/// <summary>
		/// Maps to Location.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LocationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LocationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Location.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdAcrual
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrual
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdAcrual);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdAcrual
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrual
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdAcrual);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdDiscount
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscount
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdDiscount);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdDiscount
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscount
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdDiscount);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to Location.ChartOfAccountIdCost
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCost
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdCost);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.ChartOfAccountIdCost, value);
			}
		}
		/// <summary>
		/// Maps to Location.SubledgerIdCost
		/// </summary>
		virtual public System.Int32? SubledgerIdCost
		{
			get
			{
				return base.GetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdCost);
			}

			set
			{
				base.SetSystemInt32(LocationMetadata.ColumnNames.SubledgerIdCost, value);
			}
		}
		/// <summary>
		/// Maps to Location.SRTypeOfInventory
		/// </summary>
		virtual public System.String SRTypeOfInventory
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.SRTypeOfInventory);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.SRTypeOfInventory, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsAllowedToStockGoods
		/// </summary>
		virtual public System.Boolean? IsAllowedToStockGoods
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsAllowedToStockGoods);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsAllowedToStockGoods, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsConsignment
		/// </summary>
		virtual public System.Boolean? IsConsignment
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsConsignment);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsConsignment, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsValidateMaxValueOnDistReqForIpm
		/// </summary>
		virtual public System.Boolean? IsValidateMaxValueOnDistReqForIpm
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpm);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpm, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsValidateMaxValueOnDistReqForIpnm
		/// </summary>
		virtual public System.Boolean? IsValidateMaxValueOnDistReqForIpnm
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpnm);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpnm, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsValidateMaxValueOnDistReqForIk
		/// </summary>
		virtual public System.Boolean? IsValidateMaxValueOnDistReqForIk
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIk);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIk, value);
			}
		}
		/// <summary>
		/// Maps to Location.LastHoldForTransactionDateTime
		/// </summary>
		virtual public System.DateTime? LastHoldForTransactionDateTime
		{
			get
			{
				return base.GetSystemDateTime(LocationMetadata.ColumnNames.LastHoldForTransactionDateTime);
			}

			set
			{
				base.SetSystemDateTime(LocationMetadata.ColumnNames.LastHoldForTransactionDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Location.LastHoldForTransactionByUserID
		/// </summary>
		virtual public System.String LastHoldForTransactionByUserID
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.LastHoldForTransactionByUserID);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.LastHoldForTransactionByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Location.SRStockGroup
		/// </summary>
		virtual public System.String SRStockGroup
		{
			get
			{
				return base.GetSystemString(LocationMetadata.ColumnNames.SRStockGroup);
			}

			set
			{
				base.SetSystemString(LocationMetadata.ColumnNames.SRStockGroup, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsAutoUpdateStockMinMax
		/// </summary>
		virtual public System.Boolean? IsAutoUpdateStockMinMax
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsAutoUpdateStockMinMax);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsAutoUpdateStockMinMax, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsValidateMaxValueOnPurcReqForIpm
		/// </summary>
		virtual public System.Boolean? IsValidateMaxValueOnPurcReqForIpm
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpm);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpm, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsValidateMaxValueOnPurcReqForIpnm
		/// </summary>
		virtual public System.Boolean? IsValidateMaxValueOnPurcReqForIpnm
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpnm);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpnm, value);
			}
		}
		/// <summary>
		/// Maps to Location.IsValidateMaxValueOnPurcReqForIk
		/// </summary>
		virtual public System.Boolean? IsValidateMaxValueOnPurcReqForIk
		{
			get
			{
				return base.GetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIk);
			}

			set
			{
				base.SetSystemBoolean(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIk, value);
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
			public esStrings(esLocation entity)
			{
				this.entity = entity;
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
			public System.String LocationName
			{
				get
				{
					System.String data = entity.LocationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationName = null;
					else entity.LocationName = Convert.ToString(value);
				}
			}
			public System.String ShortName
			{
				get
				{
					System.String data = entity.ShortName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShortName = null;
					else entity.ShortName = Convert.ToString(value);
				}
			}
			public System.String ParentID
			{
				get
				{
					System.String data = entity.ParentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentID = null;
					else entity.ParentID = Convert.ToString(value);
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
			public System.String PermitID
			{
				get
				{
					System.String data = entity.PermitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermitID = null;
					else entity.PermitID = Convert.ToString(value);
				}
			}
			public System.String IsHeader
			{
				get
				{
					System.Boolean? data = entity.IsHeader;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeader = null;
					else entity.IsHeader = Convert.ToBoolean(value);
				}
			}
			public System.String IsHoldForTransaction
			{
				get
				{
					System.Boolean? data = entity.IsHoldForTransaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHoldForTransaction = null;
					else entity.IsHoldForTransaction = Convert.ToBoolean(value);
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
			public System.String ChartOfAccountIdIncome
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdIncome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdIncome = null;
					else entity.ChartOfAccountIdIncome = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdIncome
			{
				get
				{
					System.Int32? data = entity.SubledgerIdIncome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdIncome = null;
					else entity.SubledgerIdIncome = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdInventory
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdInventory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdInventory = null;
					else entity.ChartOfAccountIdInventory = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdInventory
			{
				get
				{
					System.Int32? data = entity.SubledgerIdInventory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdInventory = null;
					else entity.SubledgerIdInventory = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCOGS
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCOGS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCOGS = null;
					else entity.ChartOfAccountIdCOGS = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCOGS
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCOGS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCOGS = null;
					else entity.SubledgerIdCOGS = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdSalesReturn
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdSalesReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdSalesReturn = null;
					else entity.ChartOfAccountIdSalesReturn = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdSalesReturn
			{
				get
				{
					System.Int32? data = entity.SubledgerIdSalesReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdSalesReturn = null;
					else entity.SubledgerIdSalesReturn = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdPurchaseReturn
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdPurchaseReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdPurchaseReturn = null;
					else entity.ChartOfAccountIdPurchaseReturn = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdPurchaseReturn
			{
				get
				{
					System.Int32? data = entity.SubledgerIdPurchaseReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdPurchaseReturn = null;
					else entity.SubledgerIdPurchaseReturn = Convert.ToInt32(value);
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
			public System.String ChartOfAccountIdAcrual
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdAcrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdAcrual = null;
					else entity.ChartOfAccountIdAcrual = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdAcrual
			{
				get
				{
					System.Int32? data = entity.SubledgerIdAcrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdAcrual = null;
					else entity.SubledgerIdAcrual = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdDiscount
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdDiscount = null;
					else entity.ChartOfAccountIdDiscount = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdDiscount
			{
				get
				{
					System.Int32? data = entity.SubledgerIdDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdDiscount = null;
					else entity.SubledgerIdDiscount = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdCost
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCost = null;
					else entity.ChartOfAccountIdCost = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCost
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCost = null;
					else entity.SubledgerIdCost = Convert.ToInt32(value);
				}
			}
			public System.String SRTypeOfInventory
			{
				get
				{
					System.String data = entity.SRTypeOfInventory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTypeOfInventory = null;
					else entity.SRTypeOfInventory = Convert.ToString(value);
				}
			}
			public System.String IsAllowedToStockGoods
			{
				get
				{
					System.Boolean? data = entity.IsAllowedToStockGoods;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowedToStockGoods = null;
					else entity.IsAllowedToStockGoods = Convert.ToBoolean(value);
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
			public System.String IsValidateMaxValueOnDistReqForIpm
			{
				get
				{
					System.Boolean? data = entity.IsValidateMaxValueOnDistReqForIpm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidateMaxValueOnDistReqForIpm = null;
					else entity.IsValidateMaxValueOnDistReqForIpm = Convert.ToBoolean(value);
				}
			}
			public System.String IsValidateMaxValueOnDistReqForIpnm
			{
				get
				{
					System.Boolean? data = entity.IsValidateMaxValueOnDistReqForIpnm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidateMaxValueOnDistReqForIpnm = null;
					else entity.IsValidateMaxValueOnDistReqForIpnm = Convert.ToBoolean(value);
				}
			}
			public System.String IsValidateMaxValueOnDistReqForIk
			{
				get
				{
					System.Boolean? data = entity.IsValidateMaxValueOnDistReqForIk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidateMaxValueOnDistReqForIk = null;
					else entity.IsValidateMaxValueOnDistReqForIk = Convert.ToBoolean(value);
				}
			}
			public System.String LastHoldForTransactionDateTime
			{
				get
				{
					System.DateTime? data = entity.LastHoldForTransactionDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastHoldForTransactionDateTime = null;
					else entity.LastHoldForTransactionDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastHoldForTransactionByUserID
			{
				get
				{
					System.String data = entity.LastHoldForTransactionByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastHoldForTransactionByUserID = null;
					else entity.LastHoldForTransactionByUserID = Convert.ToString(value);
				}
			}
			public System.String SRStockGroup
			{
				get
				{
					System.String data = entity.SRStockGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRStockGroup = null;
					else entity.SRStockGroup = Convert.ToString(value);
				}
			}
			public System.String IsAutoUpdateStockMinMax
			{
				get
				{
					System.Boolean? data = entity.IsAutoUpdateStockMinMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAutoUpdateStockMinMax = null;
					else entity.IsAutoUpdateStockMinMax = Convert.ToBoolean(value);
				}
			}
			public System.String IsValidateMaxValueOnPurcReqForIpm
			{
				get
				{
					System.Boolean? data = entity.IsValidateMaxValueOnPurcReqForIpm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidateMaxValueOnPurcReqForIpm = null;
					else entity.IsValidateMaxValueOnPurcReqForIpm = Convert.ToBoolean(value);
				}
			}
			public System.String IsValidateMaxValueOnPurcReqForIpnm
			{
				get
				{
					System.Boolean? data = entity.IsValidateMaxValueOnPurcReqForIpnm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidateMaxValueOnPurcReqForIpnm = null;
					else entity.IsValidateMaxValueOnPurcReqForIpnm = Convert.ToBoolean(value);
				}
			}
			public System.String IsValidateMaxValueOnPurcReqForIk
			{
				get
				{
					System.Boolean? data = entity.IsValidateMaxValueOnPurcReqForIk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidateMaxValueOnPurcReqForIk = null;
					else entity.IsValidateMaxValueOnPurcReqForIk = Convert.ToBoolean(value);
				}
			}
			private esLocation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLocationQuery query)
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
				throw new Exception("esLocation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Location : esLocation
	{
	}

	[Serializable]
	abstract public class esLocationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LocationMetadata.Meta();
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem LocationName
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.LocationName, esSystemType.String);
			}
		}

		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		}

		public esQueryItem ParentID
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ParentID, esSystemType.String);
			}
		}

		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		}

		public esQueryItem PermitID
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.PermitID, esSystemType.String);
			}
		}

		public esQueryItem IsHeader
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsHeader, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHoldForTransaction
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsHoldForTransaction, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem ChartOfAccountIdIncome
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdIncome
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdInventory
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdInventory, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdInventory
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdInventory, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdCOGS
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdCOGS, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdCOGS
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdCOGS, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdSalesReturn
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdSalesReturn, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdSalesReturn
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdSalesReturn, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdPurchaseReturn
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdPurchaseReturn
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdPurchaseReturn, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountIdAcrual
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdAcrual, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdAcrual
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdAcrual, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdDiscount
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdDiscount
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountIdCost
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.ChartOfAccountIdCost, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerIdCost
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SubledgerIdCost, esSystemType.Int32);
			}
		}

		public esQueryItem SRTypeOfInventory
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SRTypeOfInventory, esSystemType.String);
			}
		}

		public esQueryItem IsAllowedToStockGoods
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsAllowedToStockGoods, esSystemType.Boolean);
			}
		}

		public esQueryItem IsConsignment
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsConsignment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidateMaxValueOnDistReqForIpm
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpm, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidateMaxValueOnDistReqForIpnm
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpnm, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidateMaxValueOnDistReqForIk
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIk, esSystemType.Boolean);
			}
		}

		public esQueryItem LastHoldForTransactionDateTime
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.LastHoldForTransactionDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastHoldForTransactionByUserID
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.LastHoldForTransactionByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRStockGroup
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.SRStockGroup, esSystemType.String);
			}
		}

		public esQueryItem IsAutoUpdateStockMinMax
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsAutoUpdateStockMinMax, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidateMaxValueOnPurcReqForIpm
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpm, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidateMaxValueOnPurcReqForIpnm
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpnm, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidateMaxValueOnPurcReqForIk
		{
			get
			{
				return new esQueryItem(this, LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIk, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LocationCollection")]
	public partial class LocationCollection : esLocationCollection, IEnumerable<Location>
	{
		public LocationCollection()
		{

		}

		public static implicit operator List<Location>(LocationCollection coll)
		{
			List<Location> list = new List<Location>();

			foreach (Location emp in coll)
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
				return LocationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Location(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Location();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LocationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationQuery();
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
		public bool Load(LocationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Location AddNew()
		{
			Location entity = base.AddNewEntity() as Location;

			return entity;
		}
		public Location FindByPrimaryKey(String locationID)
		{
			return base.FindByPrimaryKey(locationID) as Location;
		}

		#region IEnumerable< Location> Members

		IEnumerator<Location> IEnumerable<Location>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Location;
			}
		}

		#endregion

		private LocationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Location' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Location ({LocationID})")]
	[Serializable]
	public partial class Location : esLocation
	{
		public Location()
		{
		}

		public Location(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LocationMetadata.Meta();
			}
		}

		override protected esLocationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LocationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LocationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LocationQuery();
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
		public bool Load(LocationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LocationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LocationQuery : esLocationQuery
	{
		public LocationQuery()
		{

		}

		public LocationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LocationQuery";
		}
	}

	[Serializable]
	public partial class LocationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LocationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LocationMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.LocationName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.LocationName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ParentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.ParentID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ItemGroupID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.PermitID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.PermitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsHeader, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsHeader;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsHoldForTransaction, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsHoldForTransaction;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsActive, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdIncome, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdIncome, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdInventory, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdInventory, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdInventory;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdCOGS, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdCOGS, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdCOGS;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdSalesReturn, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdSalesReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdSalesReturn, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdSalesReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdPurchaseReturn, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdPurchaseReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdPurchaseReturn, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdPurchaseReturn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LocationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdAcrual, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdAcrual, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdDiscount, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdDiscount, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.ChartOfAccountIdCost, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.ChartOfAccountIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SubledgerIdCost, 26, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LocationMetadata.PropertyNames.SubledgerIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SRTypeOfInventory, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.SRTypeOfInventory;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsAllowedToStockGoods, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsAllowedToStockGoods;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsConsignment, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsConsignment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpm, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsValidateMaxValueOnDistReqForIpm;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIpnm, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsValidateMaxValueOnDistReqForIpnm;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsValidateMaxValueOnDistReqForIk, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsValidateMaxValueOnDistReqForIk;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.LastHoldForTransactionDateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LocationMetadata.PropertyNames.LastHoldForTransactionDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.LastHoldForTransactionByUserID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.LastHoldForTransactionByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.SRStockGroup, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = LocationMetadata.PropertyNames.SRStockGroup;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsAutoUpdateStockMinMax, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsAutoUpdateStockMinMax;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpm, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsValidateMaxValueOnPurcReqForIpm;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIpnm, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsValidateMaxValueOnPurcReqForIpnm;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LocationMetadata.ColumnNames.IsValidateMaxValueOnPurcReqForIk, 39, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LocationMetadata.PropertyNames.IsValidateMaxValueOnPurcReqForIk;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LocationMetadata Meta()
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
			public const string LocationID = "LocationID";
			public const string LocationName = "LocationName";
			public const string ShortName = "ShortName";
			public const string ParentID = "ParentID";
			public const string ItemGroupID = "ItemGroupID";
			public const string PermitID = "PermitID";
			public const string IsHeader = "IsHeader";
			public const string IsHoldForTransaction = "IsHoldForTransaction";
			public const string IsActive = "IsActive";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdSalesReturn = "ChartOfAccountIdSalesReturn";
			public const string SubledgerIdSalesReturn = "SubledgerIdSalesReturn";
			public const string ChartOfAccountIdPurchaseReturn = "ChartOfAccountIdPurchaseReturn";
			public const string SubledgerIdPurchaseReturn = "SubledgerIdPurchaseReturn";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			public const string SubledgerIdCost = "SubledgerIdCost";
			public const string SRTypeOfInventory = "SRTypeOfInventory";
			public const string IsAllowedToStockGoods = "IsAllowedToStockGoods";
			public const string IsConsignment = "IsConsignment";
			public const string IsValidateMaxValueOnDistReqForIpm = "IsValidateMaxValueOnDistReqForIpm";
			public const string IsValidateMaxValueOnDistReqForIpnm = "IsValidateMaxValueOnDistReqForIpnm";
			public const string IsValidateMaxValueOnDistReqForIk = "IsValidateMaxValueOnDistReqForIk";
			public const string LastHoldForTransactionDateTime = "LastHoldForTransactionDateTime";
			public const string LastHoldForTransactionByUserID = "LastHoldForTransactionByUserID";
			public const string SRStockGroup = "SRStockGroup";
			public const string IsAutoUpdateStockMinMax = "IsAutoUpdateStockMinMax";
			public const string IsValidateMaxValueOnPurcReqForIpm = "IsValidateMaxValueOnPurcReqForIpm";
			public const string IsValidateMaxValueOnPurcReqForIpnm = "IsValidateMaxValueOnPurcReqForIpnm";
			public const string IsValidateMaxValueOnPurcReqForIk = "IsValidateMaxValueOnPurcReqForIk";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LocationID = "LocationID";
			public const string LocationName = "LocationName";
			public const string ShortName = "ShortName";
			public const string ParentID = "ParentID";
			public const string ItemGroupID = "ItemGroupID";
			public const string PermitID = "PermitID";
			public const string IsHeader = "IsHeader";
			public const string IsHoldForTransaction = "IsHoldForTransaction";
			public const string IsActive = "IsActive";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdInventory = "ChartOfAccountIdInventory";
			public const string SubledgerIdInventory = "SubledgerIdInventory";
			public const string ChartOfAccountIdCOGS = "ChartOfAccountIdCOGS";
			public const string SubledgerIdCOGS = "SubledgerIdCOGS";
			public const string ChartOfAccountIdSalesReturn = "ChartOfAccountIdSalesReturn";
			public const string SubledgerIdSalesReturn = "SubledgerIdSalesReturn";
			public const string ChartOfAccountIdPurchaseReturn = "ChartOfAccountIdPurchaseReturn";
			public const string SubledgerIdPurchaseReturn = "SubledgerIdPurchaseReturn";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			public const string SubledgerIdCost = "SubledgerIdCost";
			public const string SRTypeOfInventory = "SRTypeOfInventory";
			public const string IsAllowedToStockGoods = "IsAllowedToStockGoods";
			public const string IsConsignment = "IsConsignment";
			public const string IsValidateMaxValueOnDistReqForIpm = "IsValidateMaxValueOnDistReqForIpm";
			public const string IsValidateMaxValueOnDistReqForIpnm = "IsValidateMaxValueOnDistReqForIpnm";
			public const string IsValidateMaxValueOnDistReqForIk = "IsValidateMaxValueOnDistReqForIk";
			public const string LastHoldForTransactionDateTime = "LastHoldForTransactionDateTime";
			public const string LastHoldForTransactionByUserID = "LastHoldForTransactionByUserID";
			public const string SRStockGroup = "SRStockGroup";
			public const string IsAutoUpdateStockMinMax = "IsAutoUpdateStockMinMax";
			public const string IsValidateMaxValueOnPurcReqForIpm = "IsValidateMaxValueOnPurcReqForIpm";
			public const string IsValidateMaxValueOnPurcReqForIpnm = "IsValidateMaxValueOnPurcReqForIpnm";
			public const string IsValidateMaxValueOnPurcReqForIk = "IsValidateMaxValueOnPurcReqForIk";
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
			lock (typeof(LocationMetadata))
			{
				if (LocationMetadata.mapDelegates == null)
				{
					LocationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LocationMetadata.meta == null)
				{
					LocationMetadata.meta = new LocationMetadata();
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

				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PermitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsHeader", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHoldForTransaction", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdInventory", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCOGS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdSalesReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdSalesReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdPurchaseReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdPurchaseReturn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRTypeOfInventory", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("IsAllowedToStockGoods", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsConsignment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidateMaxValueOnDistReqForIpm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidateMaxValueOnDistReqForIpnm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidateMaxValueOnDistReqForIk", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastHoldForTransactionDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastHoldForTransactionByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRStockGroup", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("IsAutoUpdateStockMinMax", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidateMaxValueOnPurcReqForIpm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidateMaxValueOnPurcReqForIpnm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidateMaxValueOnPurcReqForIk", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "Location";
				meta.Destination = "Location";
				meta.spInsert = "proc_LocationInsert";
				meta.spUpdate = "proc_LocationUpdate";
				meta.spDelete = "proc_LocationDelete";
				meta.spLoadAll = "proc_LocationLoadAll";
				meta.spLoadByPrimaryKey = "proc_LocationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LocationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
