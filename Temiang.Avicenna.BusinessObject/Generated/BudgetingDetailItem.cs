/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/9/2023 10:41:36 AM
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
	abstract public class esBudgetingDetailItemCollection : esEntityCollectionWAuditLog
	{
		public esBudgetingDetailItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BudgetingDetailItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esBudgetingDetailItemQuery query)
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
			this.InitQuery(query as esBudgetingDetailItemQuery);
		}
		#endregion

		virtual public BudgetingDetailItem DetachEntity(BudgetingDetailItem entity)
		{
			return base.DetachEntity(entity) as BudgetingDetailItem;
		}

		virtual public BudgetingDetailItem AttachEntity(BudgetingDetailItem entity)
		{
			return base.AttachEntity(entity) as BudgetingDetailItem;
		}

		virtual public void Combine(BudgetingDetailItemCollection collection)
		{
			base.Combine(collection);
		}

		new public BudgetingDetailItem this[int index]
		{
			get
			{
				return base[index] as BudgetingDetailItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BudgetingDetailItem);
		}
	}

	[Serializable]
	abstract public class esBudgetingDetailItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBudgetingDetailItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esBudgetingDetailItem()
		{
		}

		public esBudgetingDetailItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision, chartOfAccountID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision, chartOfAccountID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(budgetingNo, revision, chartOfAccountID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(budgetingNo, revision, chartOfAccountID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemID)
		{
			esBudgetingDetailItemQuery query = this.GetDynamicQuery();
			query.Where(query.BudgetingNo == budgetingNo, query.Revision == revision, query.ChartOfAccountID == chartOfAccountID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("BudgetingNo", budgetingNo);
			parms.Add("Revision", revision);
			parms.Add("ChartOfAccountID", chartOfAccountID);
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
						case "BudgetingNo": this.str.BudgetingNo = (string)value; break;
						case "Revision": this.str.Revision = (string)value; break;
						case "ChartOfAccountID": this.str.ChartOfAccountID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "QtyMonth01": this.str.QtyMonth01 = (string)value; break;
						case "QtyMonth02": this.str.QtyMonth02 = (string)value; break;
						case "QtyMonth03": this.str.QtyMonth03 = (string)value; break;
						case "QtyMonth04": this.str.QtyMonth04 = (string)value; break;
						case "QtyMonth05": this.str.QtyMonth05 = (string)value; break;
						case "QtyMonth06": this.str.QtyMonth06 = (string)value; break;
						case "QtyMonth07": this.str.QtyMonth07 = (string)value; break;
						case "QtyMonth08": this.str.QtyMonth08 = (string)value; break;
						case "QtyMonth09": this.str.QtyMonth09 = (string)value; break;
						case "QtyMonth10": this.str.QtyMonth10 = (string)value; break;
						case "QtyMonth11": this.str.QtyMonth11 = (string)value; break;
						case "QtyMonth12": this.str.QtyMonth12 = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "IsAsset": this.str.IsAsset = (string)value; break;
						case "IsAssetApproved": this.str.IsAssetApproved = (string)value; break;
						case "AssetApprovedBy": this.str.AssetApprovedBy = (string)value; break;
						case "AssetApprovedDateTime": this.str.AssetApprovedDateTime = (string)value; break;
						case "IsAssetRejected": this.str.IsAssetRejected = (string)value; break;
						case "AssetRejectedBy": this.str.AssetRejectedBy = (string)value; break;
						case "AssetRejectedDateTime": this.str.AssetRejectedDateTime = (string)value; break;
						case "RejectNotes": this.str.RejectNotes = (string)value; break;
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Revision":

							if (value == null || value is System.Int32)
								this.Revision = (System.Int32?)value;
							break;
						case "ChartOfAccountID":

							if (value == null || value is System.Int32)
								this.ChartOfAccountID = (System.Int32?)value;
							break;
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "QtyMonth01":

							if (value == null || value is System.Decimal)
								this.QtyMonth01 = (System.Decimal?)value;
							break;
						case "QtyMonth02":

							if (value == null || value is System.Decimal)
								this.QtyMonth02 = (System.Decimal?)value;
							break;
						case "QtyMonth03":

							if (value == null || value is System.Decimal)
								this.QtyMonth03 = (System.Decimal?)value;
							break;
						case "QtyMonth04":

							if (value == null || value is System.Decimal)
								this.QtyMonth04 = (System.Decimal?)value;
							break;
						case "QtyMonth05":

							if (value == null || value is System.Decimal)
								this.QtyMonth05 = (System.Decimal?)value;
							break;
						case "QtyMonth06":

							if (value == null || value is System.Decimal)
								this.QtyMonth06 = (System.Decimal?)value;
							break;
						case "QtyMonth07":

							if (value == null || value is System.Decimal)
								this.QtyMonth07 = (System.Decimal?)value;
							break;
						case "QtyMonth08":

							if (value == null || value is System.Decimal)
								this.QtyMonth08 = (System.Decimal?)value;
							break;
						case "QtyMonth09":

							if (value == null || value is System.Decimal)
								this.QtyMonth09 = (System.Decimal?)value;
							break;
						case "QtyMonth10":

							if (value == null || value is System.Decimal)
								this.QtyMonth10 = (System.Decimal?)value;
							break;
						case "QtyMonth11":

							if (value == null || value is System.Decimal)
								this.QtyMonth11 = (System.Decimal?)value;
							break;
						case "QtyMonth12":

							if (value == null || value is System.Decimal)
								this.QtyMonth12 = (System.Decimal?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "IsAsset":

							if (value == null || value is System.Boolean)
								this.IsAsset = (System.Boolean?)value;
							break;
						case "IsAssetApproved":

							if (value == null || value is System.Boolean)
								this.IsAssetApproved = (System.Boolean?)value;
							break;
						case "AssetApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.AssetApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsAssetRejected":

							if (value == null || value is System.Boolean)
								this.IsAssetRejected = (System.Boolean?)value;
							break;
						case "AssetRejectedDateTime":

							if (value == null || value is System.DateTime)
								this.AssetRejectedDateTime = (System.DateTime?)value;
							break;
						case "ConversionFactor":

							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
							break;
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
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
		/// Maps to BudgetingDetailItem.BudgetingNo
		/// </summary>
		virtual public System.String BudgetingNo
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.BudgetingNo);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.BudgetingNo, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.Revision
		/// </summary>
		virtual public System.Int32? Revision
		{
			get
			{
				return base.GetSystemInt32(BudgetingDetailItemMetadata.ColumnNames.Revision);
			}

			set
			{
				base.SetSystemInt32(BudgetingDetailItemMetadata.ColumnNames.Revision, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.ChartOfAccountID
		/// </summary>
		virtual public System.Int32? ChartOfAccountID
		{
			get
			{
				return base.GetSystemInt32(BudgetingDetailItemMetadata.ColumnNames.ChartOfAccountID);
			}

			set
			{
				base.SetSystemInt32(BudgetingDetailItemMetadata.ColumnNames.ChartOfAccountID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth01
		/// </summary>
		virtual public System.Decimal? QtyMonth01
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth01);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth01, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth02
		/// </summary>
		virtual public System.Decimal? QtyMonth02
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth02);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth02, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth03
		/// </summary>
		virtual public System.Decimal? QtyMonth03
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth03);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth03, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth04
		/// </summary>
		virtual public System.Decimal? QtyMonth04
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth04);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth04, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth05
		/// </summary>
		virtual public System.Decimal? QtyMonth05
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth05);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth05, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth06
		/// </summary>
		virtual public System.Decimal? QtyMonth06
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth06);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth06, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth07
		/// </summary>
		virtual public System.Decimal? QtyMonth07
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth07);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth07, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth08
		/// </summary>
		virtual public System.Decimal? QtyMonth08
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth08);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth08, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth09
		/// </summary>
		virtual public System.Decimal? QtyMonth09
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth09);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth09, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth10
		/// </summary>
		virtual public System.Decimal? QtyMonth10
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth10);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth10, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth11
		/// </summary>
		virtual public System.Decimal? QtyMonth11
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth11);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth11, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.QtyMonth12
		/// </summary>
		virtual public System.Decimal? QtyMonth12
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth12);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.QtyMonth12, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.IsAsset
		/// </summary>
		virtual public System.Boolean? IsAsset
		{
			get
			{
				return base.GetSystemBoolean(BudgetingDetailItemMetadata.ColumnNames.IsAsset);
			}

			set
			{
				base.SetSystemBoolean(BudgetingDetailItemMetadata.ColumnNames.IsAsset, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.IsAssetApproved
		/// </summary>
		virtual public System.Boolean? IsAssetApproved
		{
			get
			{
				return base.GetSystemBoolean(BudgetingDetailItemMetadata.ColumnNames.IsAssetApproved);
			}

			set
			{
				base.SetSystemBoolean(BudgetingDetailItemMetadata.ColumnNames.IsAssetApproved, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.AssetApprovedBy
		/// </summary>
		virtual public System.String AssetApprovedBy
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.AssetApprovedBy);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.AssetApprovedBy, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.AssetApprovedDateTime
		/// </summary>
		virtual public System.DateTime? AssetApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.AssetApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.AssetApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.IsAssetRejected
		/// </summary>
		virtual public System.Boolean? IsAssetRejected
		{
			get
			{
				return base.GetSystemBoolean(BudgetingDetailItemMetadata.ColumnNames.IsAssetRejected);
			}

			set
			{
				base.SetSystemBoolean(BudgetingDetailItemMetadata.ColumnNames.IsAssetRejected, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.AssetRejectedBy
		/// </summary>
		virtual public System.String AssetRejectedBy
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.AssetRejectedBy);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.AssetRejectedBy, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.AssetRejectedDateTime
		/// </summary>
		virtual public System.DateTime? AssetRejectedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.AssetRejectedDateTime);
			}

			set
			{
				base.SetSystemDateTime(BudgetingDetailItemMetadata.ColumnNames.AssetRejectedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.RejectNotes
		/// </summary>
		virtual public System.String RejectNotes
		{
			get
			{
				return base.GetSystemString(BudgetingDetailItemMetadata.ColumnNames.RejectNotes);
			}

			set
			{
				base.SetSystemString(BudgetingDetailItemMetadata.ColumnNames.RejectNotes, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.ConversionFactor);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		/// <summary>
		/// Maps to BudgetingDetailItem.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(BudgetingDetailItemMetadata.ColumnNames.CostPrice, value);
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
			public esStrings(esBudgetingDetailItem entity)
			{
				this.entity = entity;
			}
			public System.String BudgetingNo
			{
				get
				{
					System.String data = entity.BudgetingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BudgetingNo = null;
					else entity.BudgetingNo = Convert.ToString(value);
				}
			}
			public System.String Revision
			{
				get
				{
					System.Int32? data = entity.Revision;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Revision = null;
					else entity.Revision = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountID
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountID = null;
					else entity.ChartOfAccountID = Convert.ToInt32(value);
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
			public System.String QtyMonth01
			{
				get
				{
					System.Decimal? data = entity.QtyMonth01;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth01 = null;
					else entity.QtyMonth01 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth02
			{
				get
				{
					System.Decimal? data = entity.QtyMonth02;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth02 = null;
					else entity.QtyMonth02 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth03
			{
				get
				{
					System.Decimal? data = entity.QtyMonth03;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth03 = null;
					else entity.QtyMonth03 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth04
			{
				get
				{
					System.Decimal? data = entity.QtyMonth04;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth04 = null;
					else entity.QtyMonth04 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth05
			{
				get
				{
					System.Decimal? data = entity.QtyMonth05;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth05 = null;
					else entity.QtyMonth05 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth06
			{
				get
				{
					System.Decimal? data = entity.QtyMonth06;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth06 = null;
					else entity.QtyMonth06 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth07
			{
				get
				{
					System.Decimal? data = entity.QtyMonth07;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth07 = null;
					else entity.QtyMonth07 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth08
			{
				get
				{
					System.Decimal? data = entity.QtyMonth08;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth08 = null;
					else entity.QtyMonth08 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth09
			{
				get
				{
					System.Decimal? data = entity.QtyMonth09;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth09 = null;
					else entity.QtyMonth09 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth10
			{
				get
				{
					System.Decimal? data = entity.QtyMonth10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth10 = null;
					else entity.QtyMonth10 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth11
			{
				get
				{
					System.Decimal? data = entity.QtyMonth11;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth11 = null;
					else entity.QtyMonth11 = Convert.ToDecimal(value);
				}
			}
			public System.String QtyMonth12
			{
				get
				{
					System.Decimal? data = entity.QtyMonth12;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyMonth12 = null;
					else entity.QtyMonth12 = Convert.ToDecimal(value);
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
			public System.String IsAssetApproved
			{
				get
				{
					System.Boolean? data = entity.IsAssetApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAssetApproved = null;
					else entity.IsAssetApproved = Convert.ToBoolean(value);
				}
			}
			public System.String AssetApprovedBy
			{
				get
				{
					System.String data = entity.AssetApprovedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetApprovedBy = null;
					else entity.AssetApprovedBy = Convert.ToString(value);
				}
			}
			public System.String AssetApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.AssetApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetApprovedDateTime = null;
					else entity.AssetApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsAssetRejected
			{
				get
				{
					System.Boolean? data = entity.IsAssetRejected;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAssetRejected = null;
					else entity.IsAssetRejected = Convert.ToBoolean(value);
				}
			}
			public System.String AssetRejectedBy
			{
				get
				{
					System.String data = entity.AssetRejectedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetRejectedBy = null;
					else entity.AssetRejectedBy = Convert.ToString(value);
				}
			}
			public System.String AssetRejectedDateTime
			{
				get
				{
					System.DateTime? data = entity.AssetRejectedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetRejectedDateTime = null;
					else entity.AssetRejectedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RejectNotes
			{
				get
				{
					System.String data = entity.RejectNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RejectNotes = null;
					else entity.RejectNotes = Convert.ToString(value);
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
			private esBudgetingDetailItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBudgetingDetailItemQuery query)
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
				throw new Exception("esBudgetingDetailItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BudgetingDetailItem : esBudgetingDetailItem
	{
	}

	[Serializable]
	abstract public class esBudgetingDetailItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BudgetingDetailItemMetadata.Meta();
			}
		}

		public esQueryItem BudgetingNo
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.BudgetingNo, esSystemType.String);
			}
		}

		public esQueryItem Revision
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.Revision, esSystemType.Int32);
			}
		}

		public esQueryItem ChartOfAccountID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.ChartOfAccountID, esSystemType.Int32);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem QtyMonth01
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth01, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth02
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth02, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth03
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth03, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth04
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth04, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth05
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth05, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth06
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth06, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth07
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth07, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth08
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth08, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth09
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth09, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth10
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth10, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth11
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth11, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyMonth12
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.QtyMonth12, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem IsAsset
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.IsAsset, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAssetApproved
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.IsAssetApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem AssetApprovedBy
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.AssetApprovedBy, esSystemType.String);
			}
		}

		public esQueryItem AssetApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.AssetApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsAssetRejected
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.IsAssetRejected, esSystemType.Boolean);
			}
		}

		public esQueryItem AssetRejectedBy
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.AssetRejectedBy, esSystemType.String);
			}
		}

		public esQueryItem AssetRejectedDateTime
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.AssetRejectedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RejectNotes
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.RejectNotes, esSystemType.String);
			}
		}

		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, BudgetingDetailItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BudgetingDetailItemCollection")]
	public partial class BudgetingDetailItemCollection : esBudgetingDetailItemCollection, IEnumerable<BudgetingDetailItem>
	{
		public BudgetingDetailItemCollection()
		{

		}

		public static implicit operator List<BudgetingDetailItem>(BudgetingDetailItemCollection coll)
		{
			List<BudgetingDetailItem> list = new List<BudgetingDetailItem>();

			foreach (BudgetingDetailItem emp in coll)
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
				return BudgetingDetailItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingDetailItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BudgetingDetailItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BudgetingDetailItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BudgetingDetailItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingDetailItemQuery();
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
		public bool Load(BudgetingDetailItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BudgetingDetailItem AddNew()
		{
			BudgetingDetailItem entity = base.AddNewEntity() as BudgetingDetailItem;

			return entity;
		}
		public BudgetingDetailItem FindByPrimaryKey(String budgetingNo, Int32 revision, Int32 chartOfAccountID, String itemID)
		{
			return base.FindByPrimaryKey(budgetingNo, revision, chartOfAccountID, itemID) as BudgetingDetailItem;
		}

		#region IEnumerable< BudgetingDetailItem> Members

		IEnumerator<BudgetingDetailItem> IEnumerable<BudgetingDetailItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as BudgetingDetailItem;
			}
		}

		#endregion

		private BudgetingDetailItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BudgetingDetailItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BudgetingDetailItem ({BudgetingNo, Revision, ChartOfAccountID, ItemID})")]
	[Serializable]
	public partial class BudgetingDetailItem : esBudgetingDetailItem
	{
		public BudgetingDetailItem()
		{
		}

		public BudgetingDetailItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BudgetingDetailItemMetadata.Meta();
			}
		}

		override protected esBudgetingDetailItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BudgetingDetailItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BudgetingDetailItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BudgetingDetailItemQuery();
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
		public bool Load(BudgetingDetailItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BudgetingDetailItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BudgetingDetailItemQuery : esBudgetingDetailItemQuery
	{
		public BudgetingDetailItemQuery()
		{

		}

		public BudgetingDetailItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BudgetingDetailItemQuery";
		}
	}

	[Serializable]
	public partial class BudgetingDetailItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BudgetingDetailItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.BudgetingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.BudgetingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.Revision, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.Revision;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.ChartOfAccountID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.ChartOfAccountID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.SRItemUnit, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.CreatedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.CreatedDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth01, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth01;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth02, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth02;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth03, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth03;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth04, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth04;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth05, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth05;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth06, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth06;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth07, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth07;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth08, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth08;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth09, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth09;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth10, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth11, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth11;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.QtyMonth12, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.QtyMonth12;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.Price, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.Price;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.IsAsset, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.IsAsset;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.IsAssetApproved, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.IsAssetApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.AssetApprovedBy, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.AssetApprovedBy;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.AssetApprovedDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.AssetApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.IsAssetRejected, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.IsAssetRejected;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.AssetRejectedBy, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.AssetRejectedBy;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.AssetRejectedDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.AssetRejectedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.RejectNotes, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.RejectNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.ConversionFactor, 31, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BudgetingDetailItemMetadata.ColumnNames.CostPrice, 32, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BudgetingDetailItemMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BudgetingDetailItemMetadata Meta()
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
			public const string BudgetingNo = "BudgetingNo";
			public const string Revision = "Revision";
			public const string ChartOfAccountID = "ChartOfAccountID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string QtyMonth01 = "QtyMonth01";
			public const string QtyMonth02 = "QtyMonth02";
			public const string QtyMonth03 = "QtyMonth03";
			public const string QtyMonth04 = "QtyMonth04";
			public const string QtyMonth05 = "QtyMonth05";
			public const string QtyMonth06 = "QtyMonth06";
			public const string QtyMonth07 = "QtyMonth07";
			public const string QtyMonth08 = "QtyMonth08";
			public const string QtyMonth09 = "QtyMonth09";
			public const string QtyMonth10 = "QtyMonth10";
			public const string QtyMonth11 = "QtyMonth11";
			public const string QtyMonth12 = "QtyMonth12";
			public const string Price = "Price";
			public const string IsAsset = "IsAsset";
			public const string IsAssetApproved = "IsAssetApproved";
			public const string AssetApprovedBy = "AssetApprovedBy";
			public const string AssetApprovedDateTime = "AssetApprovedDateTime";
			public const string IsAssetRejected = "IsAssetRejected";
			public const string AssetRejectedBy = "AssetRejectedBy";
			public const string AssetRejectedDateTime = "AssetRejectedDateTime";
			public const string RejectNotes = "RejectNotes";
			public const string ConversionFactor = "ConversionFactor";
			public const string CostPrice = "CostPrice";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string BudgetingNo = "BudgetingNo";
			public const string Revision = "Revision";
			public const string ChartOfAccountID = "ChartOfAccountID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string QtyMonth01 = "QtyMonth01";
			public const string QtyMonth02 = "QtyMonth02";
			public const string QtyMonth03 = "QtyMonth03";
			public const string QtyMonth04 = "QtyMonth04";
			public const string QtyMonth05 = "QtyMonth05";
			public const string QtyMonth06 = "QtyMonth06";
			public const string QtyMonth07 = "QtyMonth07";
			public const string QtyMonth08 = "QtyMonth08";
			public const string QtyMonth09 = "QtyMonth09";
			public const string QtyMonth10 = "QtyMonth10";
			public const string QtyMonth11 = "QtyMonth11";
			public const string QtyMonth12 = "QtyMonth12";
			public const string Price = "Price";
			public const string IsAsset = "IsAsset";
			public const string IsAssetApproved = "IsAssetApproved";
			public const string AssetApprovedBy = "AssetApprovedBy";
			public const string AssetApprovedDateTime = "AssetApprovedDateTime";
			public const string IsAssetRejected = "IsAssetRejected";
			public const string AssetRejectedBy = "AssetRejectedBy";
			public const string AssetRejectedDateTime = "AssetRejectedDateTime";
			public const string RejectNotes = "RejectNotes";
			public const string ConversionFactor = "ConversionFactor";
			public const string CostPrice = "CostPrice";
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
			lock (typeof(BudgetingDetailItemMetadata))
			{
				if (BudgetingDetailItemMetadata.mapDelegates == null)
				{
					BudgetingDetailItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BudgetingDetailItemMetadata.meta == null)
				{
					BudgetingDetailItemMetadata.meta = new BudgetingDetailItemMetadata();
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

				meta.AddTypeMap("BudgetingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Revision", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("QtyMonth01", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth02", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth03", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth04", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth05", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth06", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth07", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth08", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth09", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth10", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth11", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("QtyMonth12", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("IsAsset", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAssetApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AssetApprovedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsAssetRejected", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AssetRejectedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetRejectedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RejectNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CostPrice", new esTypeMap("decimal", "System.Decimal"));


				meta.Source = "BudgetingDetailItem";
				meta.Destination = "BudgetingDetailItem";
				meta.spInsert = "proc_BudgetingDetailItemInsert";
				meta.spUpdate = "proc_BudgetingDetailItemUpdate";
				meta.spDelete = "proc_BudgetingDetailItemDelete";
				meta.spLoadAll = "proc_BudgetingDetailItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_BudgetingDetailItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BudgetingDetailItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
