/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/12/2023 12:28:04 PM
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
	abstract public class esItemProductLogCollection : esEntityCollectionWAuditLog
	{
		public esItemProductLogCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemProductLogCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductLogQuery query)
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
			this.InitQuery(query as esItemProductLogQuery);
		}
		#endregion

		virtual public ItemProductLog DetachEntity(ItemProductLog entity)
		{
			return base.DetachEntity(entity) as ItemProductLog;
		}

		virtual public ItemProductLog AttachEntity(ItemProductLog entity)
		{
			return base.AttachEntity(entity) as ItemProductLog;
		}

		virtual public void Combine(ItemProductLogCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemProductLog this[int index]
		{
			get
			{
				return base[index] as ItemProductLog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductLog);
		}
	}

	[Serializable]
	abstract public class esItemProductLog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductLogQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductLog()
		{
		}

		public esItemProductLog(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String tariffRequestNo, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String tariffRequestNo, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String tariffRequestNo, String itemID)
		{
			esItemProductLogQuery query = this.GetDynamicQuery();
			query.Where(query.TariffRequestNo == tariffRequestNo, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String tariffRequestNo, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("TariffRequestNo", tariffRequestNo);
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
						case "TariffRequestNo": this.str.TariffRequestNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "PriceInPurchaseUnitOld": this.str.PriceInPurchaseUnitOld = (string)value; break;
						case "PriceInPurchaseUnitNew": this.str.PriceInPurchaseUnitNew = (string)value; break;
						case "PriceInBaseUnitOld": this.str.PriceInBaseUnitOld = (string)value; break;
						case "PriceInBaseUnitNew": this.str.PriceInBaseUnitNew = (string)value; break;
						case "PriceInBaseUnitWVatOld": this.str.PriceInBaseUnitWVatOld = (string)value; break;
						case "PriceInBaseUnitWVatNew": this.str.PriceInBaseUnitWVatNew = (string)value; break;
						case "CostPriceOld": this.str.CostPriceOld = (string)value; break;
						case "CostPriceNew": this.str.CostPriceNew = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SalesDiscountOld": this.str.SalesDiscountOld = (string)value; break;
						case "SaledDiscountNew": this.str.SaledDiscountNew = (string)value; break;
						case "HighestPriceInBasedUnitOld": this.str.HighestPriceInBasedUnitOld = (string)value; break;
						case "HighestPriceInBasedUnitNew": this.str.HighestPriceInBasedUnitNew = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PriceInPurchaseUnitOld":

							if (value == null || value is System.Decimal)
								this.PriceInPurchaseUnitOld = (System.Decimal?)value;
							break;
						case "PriceInPurchaseUnitNew":

							if (value == null || value is System.Decimal)
								this.PriceInPurchaseUnitNew = (System.Decimal?)value;
							break;
						case "PriceInBaseUnitOld":

							if (value == null || value is System.Decimal)
								this.PriceInBaseUnitOld = (System.Decimal?)value;
							break;
						case "PriceInBaseUnitNew":

							if (value == null || value is System.Decimal)
								this.PriceInBaseUnitNew = (System.Decimal?)value;
							break;
						case "PriceInBaseUnitWVatOld":

							if (value == null || value is System.Decimal)
								this.PriceInBaseUnitWVatOld = (System.Decimal?)value;
							break;
						case "PriceInBaseUnitWVatNew":

							if (value == null || value is System.Decimal)
								this.PriceInBaseUnitWVatNew = (System.Decimal?)value;
							break;
						case "CostPriceOld":

							if (value == null || value is System.Decimal)
								this.CostPriceOld = (System.Decimal?)value;
							break;
						case "CostPriceNew":

							if (value == null || value is System.Decimal)
								this.CostPriceNew = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SalesDiscountOld":

							if (value == null || value is System.Decimal)
								this.SalesDiscountOld = (System.Decimal?)value;
							break;
						case "SaledDiscountNew":

							if (value == null || value is System.Decimal)
								this.SaledDiscountNew = (System.Decimal?)value;
							break;
						case "HighestPriceInBasedUnitOld":

							if (value == null || value is System.Decimal)
								this.HighestPriceInBasedUnitOld = (System.Decimal?)value;
							break;
						case "HighestPriceInBasedUnitNew":

							if (value == null || value is System.Decimal)
								this.HighestPriceInBasedUnitNew = (System.Decimal?)value;
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
		/// Maps to ItemProductLog.TariffRequestNo
		/// </summary>
		virtual public System.String TariffRequestNo
		{
			get
			{
				return base.GetSystemString(ItemProductLogMetadata.ColumnNames.TariffRequestNo);
			}

			set
			{
				base.SetSystemString(ItemProductLogMetadata.ColumnNames.TariffRequestNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemProductLogMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemProductLogMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.PriceInPurchaseUnitOld
		/// </summary>
		virtual public System.Decimal? PriceInPurchaseUnitOld
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitOld);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitOld, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.PriceInPurchaseUnitNew
		/// </summary>
		virtual public System.Decimal? PriceInPurchaseUnitNew
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitNew);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitNew, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.PriceInBaseUnitOld
		/// </summary>
		virtual public System.Decimal? PriceInBaseUnitOld
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitOld);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitOld, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.PriceInBaseUnitNew
		/// </summary>
		virtual public System.Decimal? PriceInBaseUnitNew
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitNew);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitNew, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.PriceInBaseUnitWVatOld
		/// </summary>
		virtual public System.Decimal? PriceInBaseUnitWVatOld
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatOld);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatOld, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.PriceInBaseUnitWVatNew
		/// </summary>
		virtual public System.Decimal? PriceInBaseUnitWVatNew
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatNew);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatNew, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.CostPriceOld
		/// </summary>
		virtual public System.Decimal? CostPriceOld
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.CostPriceOld);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.CostPriceOld, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.CostPriceNew
		/// </summary>
		virtual public System.Decimal? CostPriceNew
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.CostPriceNew);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.CostPriceNew, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductLogMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemProductLogMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductLogMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemProductLogMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.SalesDiscountOld
		/// </summary>
		virtual public System.Decimal? SalesDiscountOld
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.SalesDiscountOld);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.SalesDiscountOld, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.SaledDiscountNew
		/// </summary>
		virtual public System.Decimal? SaledDiscountNew
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.SaledDiscountNew);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.SaledDiscountNew, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.HighestPriceInBasedUnitOld
		/// </summary>
		virtual public System.Decimal? HighestPriceInBasedUnitOld
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitOld);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitOld, value);
			}
		}
		/// <summary>
		/// Maps to ItemProductLog.HighestPriceInBasedUnitNew
		/// </summary>
		virtual public System.Decimal? HighestPriceInBasedUnitNew
		{
			get
			{
				return base.GetSystemDecimal(ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitNew);
			}

			set
			{
				base.SetSystemDecimal(ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitNew, value);
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
			public esStrings(esItemProductLog entity)
			{
				this.entity = entity;
			}
			public System.String TariffRequestNo
			{
				get
				{
					System.String data = entity.TariffRequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffRequestNo = null;
					else entity.TariffRequestNo = Convert.ToString(value);
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
			public System.String PriceInPurchaseUnitOld
			{
				get
				{
					System.Decimal? data = entity.PriceInPurchaseUnitOld;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInPurchaseUnitOld = null;
					else entity.PriceInPurchaseUnitOld = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInPurchaseUnitNew
			{
				get
				{
					System.Decimal? data = entity.PriceInPurchaseUnitNew;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInPurchaseUnitNew = null;
					else entity.PriceInPurchaseUnitNew = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInBaseUnitOld
			{
				get
				{
					System.Decimal? data = entity.PriceInBaseUnitOld;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInBaseUnitOld = null;
					else entity.PriceInBaseUnitOld = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInBaseUnitNew
			{
				get
				{
					System.Decimal? data = entity.PriceInBaseUnitNew;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInBaseUnitNew = null;
					else entity.PriceInBaseUnitNew = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInBaseUnitWVatOld
			{
				get
				{
					System.Decimal? data = entity.PriceInBaseUnitWVatOld;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInBaseUnitWVatOld = null;
					else entity.PriceInBaseUnitWVatOld = Convert.ToDecimal(value);
				}
			}
			public System.String PriceInBaseUnitWVatNew
			{
				get
				{
					System.Decimal? data = entity.PriceInBaseUnitWVatNew;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInBaseUnitWVatNew = null;
					else entity.PriceInBaseUnitWVatNew = Convert.ToDecimal(value);
				}
			}
			public System.String CostPriceOld
			{
				get
				{
					System.Decimal? data = entity.CostPriceOld;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostPriceOld = null;
					else entity.CostPriceOld = Convert.ToDecimal(value);
				}
			}
			public System.String CostPriceNew
			{
				get
				{
					System.Decimal? data = entity.CostPriceNew;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostPriceNew = null;
					else entity.CostPriceNew = Convert.ToDecimal(value);
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
			public System.String SalesDiscountOld
			{
				get
				{
					System.Decimal? data = entity.SalesDiscountOld;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesDiscountOld = null;
					else entity.SalesDiscountOld = Convert.ToDecimal(value);
				}
			}
			public System.String SaledDiscountNew
			{
				get
				{
					System.Decimal? data = entity.SaledDiscountNew;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SaledDiscountNew = null;
					else entity.SaledDiscountNew = Convert.ToDecimal(value);
				}
			}
			public System.String HighestPriceInBasedUnitOld
			{
				get
				{
					System.Decimal? data = entity.HighestPriceInBasedUnitOld;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HighestPriceInBasedUnitOld = null;
					else entity.HighestPriceInBasedUnitOld = Convert.ToDecimal(value);
				}
			}
			public System.String HighestPriceInBasedUnitNew
			{
				get
				{
					System.Decimal? data = entity.HighestPriceInBasedUnitNew;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HighestPriceInBasedUnitNew = null;
					else entity.HighestPriceInBasedUnitNew = Convert.ToDecimal(value);
				}
			}
			private esItemProductLog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductLogQuery query)
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
				throw new Exception("esItemProductLog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemProductLog : esItemProductLog
	{
	}

	[Serializable]
	abstract public class esItemProductLogQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemProductLogMetadata.Meta();
			}
		}

		public esQueryItem TariffRequestNo
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem PriceInPurchaseUnitOld
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitOld, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInPurchaseUnitNew
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitNew, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBaseUnitOld
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.PriceInBaseUnitOld, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBaseUnitNew
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.PriceInBaseUnitNew, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBaseUnitWVatOld
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatOld, esSystemType.Decimal);
			}
		}

		public esQueryItem PriceInBaseUnitWVatNew
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatNew, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPriceOld
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.CostPriceOld, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPriceNew
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.CostPriceNew, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SalesDiscountOld
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.SalesDiscountOld, esSystemType.Decimal);
			}
		}

		public esQueryItem SaledDiscountNew
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.SaledDiscountNew, esSystemType.Decimal);
			}
		}

		public esQueryItem HighestPriceInBasedUnitOld
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitOld, esSystemType.Decimal);
			}
		}

		public esQueryItem HighestPriceInBasedUnitNew
		{
			get
			{
				return new esQueryItem(this, ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitNew, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductLogCollection")]
	public partial class ItemProductLogCollection : esItemProductLogCollection, IEnumerable<ItemProductLog>
	{
		public ItemProductLogCollection()
		{

		}

		public static implicit operator List<ItemProductLog>(ItemProductLogCollection coll)
		{
			List<ItemProductLog> list = new List<ItemProductLog>();

			foreach (ItemProductLog emp in coll)
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
				return ItemProductLogMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductLog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductLog();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemProductLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductLogQuery();
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
		public bool Load(ItemProductLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemProductLog AddNew()
		{
			ItemProductLog entity = base.AddNewEntity() as ItemProductLog;

			return entity;
		}
		public ItemProductLog FindByPrimaryKey(String tariffRequestNo, String itemID)
		{
			return base.FindByPrimaryKey(tariffRequestNo, itemID) as ItemProductLog;
		}

		#region IEnumerable< ItemProductLog> Members

		IEnumerator<ItemProductLog> IEnumerable<ItemProductLog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductLog;
			}
		}

		#endregion

		private ItemProductLogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductLog' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemProductLog ({TariffRequestNo, ItemID})")]
	[Serializable]
	public partial class ItemProductLog : esItemProductLog
	{
		public ItemProductLog()
		{
		}

		public ItemProductLog(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductLogMetadata.Meta();
			}
		}

		override protected esItemProductLogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemProductLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductLogQuery();
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
		public bool Load(ItemProductLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemProductLogQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemProductLogQuery : esItemProductLogQuery
	{
		public ItemProductLogQuery()
		{

		}

		public ItemProductLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemProductLogQuery";
		}
	}

	[Serializable]
	public partial class ItemProductLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductLogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.TariffRequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitOld, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.PriceInPurchaseUnitOld;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.PriceInPurchaseUnitNew, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.PriceInPurchaseUnitNew;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitOld, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.PriceInBaseUnitOld;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitNew, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.PriceInBaseUnitNew;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatOld, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.PriceInBaseUnitWVatOld;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.PriceInBaseUnitWVatNew, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.PriceInBaseUnitWVatNew;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.CostPriceOld, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.CostPriceOld;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.CostPriceNew, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.CostPriceNew;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.SalesDiscountOld, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.SalesDiscountOld;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.SaledDiscountNew, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.SaledDiscountNew;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitOld, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.HighestPriceInBasedUnitOld;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemProductLogMetadata.ColumnNames.HighestPriceInBasedUnitNew, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductLogMetadata.PropertyNames.HighestPriceInBasedUnitNew;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemProductLogMetadata Meta()
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
			public const string TariffRequestNo = "TariffRequestNo";
			public const string ItemID = "ItemID";
			public const string PriceInPurchaseUnitOld = "PriceInPurchaseUnitOld";
			public const string PriceInPurchaseUnitNew = "PriceInPurchaseUnitNew";
			public const string PriceInBaseUnitOld = "PriceInBaseUnitOld";
			public const string PriceInBaseUnitNew = "PriceInBaseUnitNew";
			public const string PriceInBaseUnitWVatOld = "PriceInBaseUnitWVatOld";
			public const string PriceInBaseUnitWVatNew = "PriceInBaseUnitWVatNew";
			public const string CostPriceOld = "CostPriceOld";
			public const string CostPriceNew = "CostPriceNew";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SalesDiscountOld = "SalesDiscountOld";
			public const string SaledDiscountNew = "SaledDiscountNew";
			public const string HighestPriceInBasedUnitOld = "HighestPriceInBasedUnitOld";
			public const string HighestPriceInBasedUnitNew = "HighestPriceInBasedUnitNew";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TariffRequestNo = "TariffRequestNo";
			public const string ItemID = "ItemID";
			public const string PriceInPurchaseUnitOld = "PriceInPurchaseUnitOld";
			public const string PriceInPurchaseUnitNew = "PriceInPurchaseUnitNew";
			public const string PriceInBaseUnitOld = "PriceInBaseUnitOld";
			public const string PriceInBaseUnitNew = "PriceInBaseUnitNew";
			public const string PriceInBaseUnitWVatOld = "PriceInBaseUnitWVatOld";
			public const string PriceInBaseUnitWVatNew = "PriceInBaseUnitWVatNew";
			public const string CostPriceOld = "CostPriceOld";
			public const string CostPriceNew = "CostPriceNew";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SalesDiscountOld = "SalesDiscountOld";
			public const string SaledDiscountNew = "SaledDiscountNew";
			public const string HighestPriceInBasedUnitOld = "HighestPriceInBasedUnitOld";
			public const string HighestPriceInBasedUnitNew = "HighestPriceInBasedUnitNew";
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
			lock (typeof(ItemProductLogMetadata))
			{
				if (ItemProductLogMetadata.mapDelegates == null)
				{
					ItemProductLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemProductLogMetadata.meta == null)
				{
					ItemProductLogMetadata.meta = new ItemProductLogMetadata();
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

				meta.AddTypeMap("TariffRequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PriceInPurchaseUnitOld", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInPurchaseUnitNew", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBaseUnitOld", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBaseUnitNew", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBaseUnitWVatOld", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInBaseUnitWVatNew", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostPriceOld", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostPriceNew", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalesDiscountOld", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SaledDiscountNew", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HighestPriceInBasedUnitOld", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HighestPriceInBasedUnitNew", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "ItemProductLog";
				meta.Destination = "ItemProductLog";
				meta.spInsert = "proc_ItemProductLogInsert";
				meta.spUpdate = "proc_ItemProductLogUpdate";
				meta.spDelete = "proc_ItemProductLogDelete";
				meta.spLoadAll = "proc_ItemProductLogLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductLogLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
