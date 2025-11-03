/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/14/2022 9:52:34 AM
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
	abstract public class esAveragePriceHistoryCollection : esEntityCollectionWAuditLog
	{
		public esAveragePriceHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AveragePriceHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esAveragePriceHistoryQuery query)
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
			this.InitQuery(query as esAveragePriceHistoryQuery);
		}
		#endregion

		virtual public AveragePriceHistory DetachEntity(AveragePriceHistory entity)
		{
			return base.DetachEntity(entity) as AveragePriceHistory;
		}

		virtual public AveragePriceHistory AttachEntity(AveragePriceHistory entity)
		{
			return base.AttachEntity(entity) as AveragePriceHistory;
		}

		virtual public void Combine(AveragePriceHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public AveragePriceHistory this[int index]
		{
			get
			{
				return base[index] as AveragePriceHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AveragePriceHistory);
		}
	}

	[Serializable]
	abstract public class esAveragePriceHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAveragePriceHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esAveragePriceHistory()
		{
		}

		public esAveragePriceHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String itemID, Decimal oldAveragePrice)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemID, oldAveragePrice);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID, oldAveragePrice);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String itemID, Decimal oldAveragePrice)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemID, oldAveragePrice);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID, oldAveragePrice);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String itemID, Decimal oldAveragePrice)
		{
			esAveragePriceHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.ItemID == itemID, query.OldAveragePrice == oldAveragePrice);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String itemID, Decimal oldAveragePrice)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("ItemID", itemID);
			parms.Add("OldAveragePrice", oldAveragePrice);
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ItemUnit": this.str.ItemUnit = (string)value; break;
						case "ChangedDate": this.str.ChangedDate = (string)value; break;
						case "TransactionCode": this.str.TransactionCode = (string)value; break;
						case "OldAveragePrice": this.str.OldAveragePrice = (string)value; break;
						case "NewAveragePrice": this.str.NewAveragePrice = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "OldPriceInBaseUnit": this.str.OldPriceInBaseUnit = (string)value; break;
						case "OldPriceInPurchaseUnit": this.str.OldPriceInPurchaseUnit = (string)value; break;
						case "OldPurchaseDiscount1": this.str.OldPurchaseDiscount1 = (string)value; break;
						case "OldPurchaseDiscount2": this.str.OldPurchaseDiscount2 = (string)value; break;
						case "NewPriceInBaseUnit": this.str.NewPriceInBaseUnit = (string)value; break;
						case "NewPriceInPurchaseUnit": this.str.NewPriceInPurchaseUnit = (string)value; break;
						case "NewPurchaseDiscount1": this.str.NewPurchaseDiscount1 = (string)value; break;
						case "NewPurchaseDiscount2": this.str.NewPurchaseDiscount2 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ChangedDate":

							if (value == null || value is System.DateTime)
								this.ChangedDate = (System.DateTime?)value;
							break;
						case "OldAveragePrice":

							if (value == null || value is System.Decimal)
								this.OldAveragePrice = (System.Decimal?)value;
							break;
						case "NewAveragePrice":

							if (value == null || value is System.Decimal)
								this.NewAveragePrice = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "OldPriceInBaseUnit":

							if (value == null || value is System.Decimal)
								this.OldPriceInBaseUnit = (System.Decimal?)value;
							break;
						case "OldPriceInPurchaseUnit":

							if (value == null || value is System.Decimal)
								this.OldPriceInPurchaseUnit = (System.Decimal?)value;
							break;
						case "OldPurchaseDiscount1":

							if (value == null || value is System.Decimal)
								this.OldPurchaseDiscount1 = (System.Decimal?)value;
							break;
						case "OldPurchaseDiscount2":

							if (value == null || value is System.Decimal)
								this.OldPurchaseDiscount2 = (System.Decimal?)value;
							break;
						case "NewPriceInBaseUnit":

							if (value == null || value is System.Decimal)
								this.NewPriceInBaseUnit = (System.Decimal?)value;
							break;
						case "NewPriceInPurchaseUnit":

							if (value == null || value is System.Decimal)
								this.NewPriceInPurchaseUnit = (System.Decimal?)value;
							break;
						case "NewPurchaseDiscount1":

							if (value == null || value is System.Decimal)
								this.NewPurchaseDiscount1 = (System.Decimal?)value;
							break;
						case "NewPurchaseDiscount2":

							if (value == null || value is System.Decimal)
								this.NewPurchaseDiscount2 = (System.Decimal?)value;
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
		/// Maps to AveragePriceHistory.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(AveragePriceHistoryMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(AveragePriceHistoryMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AveragePriceHistoryMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(AveragePriceHistoryMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.ItemUnit
		/// </summary>
		virtual public System.String ItemUnit
		{
			get
			{
				return base.GetSystemString(AveragePriceHistoryMetadata.ColumnNames.ItemUnit);
			}

			set
			{
				base.SetSystemString(AveragePriceHistoryMetadata.ColumnNames.ItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.ChangedDate
		/// </summary>
		virtual public System.DateTime? ChangedDate
		{
			get
			{
				return base.GetSystemDateTime(AveragePriceHistoryMetadata.ColumnNames.ChangedDate);
			}

			set
			{
				base.SetSystemDateTime(AveragePriceHistoryMetadata.ColumnNames.ChangedDate, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(AveragePriceHistoryMetadata.ColumnNames.TransactionCode);
			}

			set
			{
				base.SetSystemString(AveragePriceHistoryMetadata.ColumnNames.TransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.OldAveragePrice
		/// </summary>
		virtual public System.Decimal? OldAveragePrice
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldAveragePrice);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldAveragePrice, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.NewAveragePrice
		/// </summary>
		virtual public System.Decimal? NewAveragePrice
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewAveragePrice);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewAveragePrice, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AveragePriceHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AveragePriceHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AveragePriceHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AveragePriceHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.OldPriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? OldPriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.OldPriceInPurchaseUnit
		/// </summary>
		virtual public System.Decimal? OldPriceInPurchaseUnit
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPriceInPurchaseUnit);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPriceInPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.OldPurchaseDiscount1
		/// </summary>
		virtual public System.Decimal? OldPurchaseDiscount1
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount1);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount1, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.OldPurchaseDiscount2
		/// </summary>
		virtual public System.Decimal? OldPurchaseDiscount2
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount2);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount2, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.NewPriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? NewPriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.NewPriceInPurchaseUnit
		/// </summary>
		virtual public System.Decimal? NewPriceInPurchaseUnit
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPriceInPurchaseUnit);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPriceInPurchaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.NewPurchaseDiscount1
		/// </summary>
		virtual public System.Decimal? NewPurchaseDiscount1
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount1);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount1, value);
			}
		}
		/// <summary>
		/// Maps to AveragePriceHistory.NewPurchaseDiscount2
		/// </summary>
		virtual public System.Decimal? NewPurchaseDiscount2
		{
			get
			{
				return base.GetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount2);
			}

			set
			{
				base.SetSystemDecimal(AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount2, value);
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
			public esStrings(esAveragePriceHistory entity)
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
			public System.String ChangedDate
			{
				get
				{
					System.DateTime? data = entity.ChangedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChangedDate = null;
					else entity.ChangedDate = Convert.ToDateTime(value);
				}
			}
			public System.String TransactionCode
			{
				get
				{
					System.String data = entity.TransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionCode = null;
					else entity.TransactionCode = Convert.ToString(value);
				}
			}
			public System.String OldAveragePrice
			{
				get
				{
					System.Decimal? data = entity.OldAveragePrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldAveragePrice = null;
					else entity.OldAveragePrice = Convert.ToDecimal(value);
				}
			}
			public System.String NewAveragePrice
			{
				get
				{
					System.Decimal? data = entity.NewAveragePrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewAveragePrice = null;
					else entity.NewAveragePrice = Convert.ToDecimal(value);
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
			public System.String OldPriceInBaseUnit
			{
				get
				{
					System.Decimal? data = entity.OldPriceInBaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldPriceInBaseUnit = null;
					else entity.OldPriceInBaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String OldPriceInPurchaseUnit
			{
				get
				{
					System.Decimal? data = entity.OldPriceInPurchaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldPriceInPurchaseUnit = null;
					else entity.OldPriceInPurchaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String OldPurchaseDiscount1
			{
				get
				{
					System.Decimal? data = entity.OldPurchaseDiscount1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldPurchaseDiscount1 = null;
					else entity.OldPurchaseDiscount1 = Convert.ToDecimal(value);
				}
			}
			public System.String OldPurchaseDiscount2
			{
				get
				{
					System.Decimal? data = entity.OldPurchaseDiscount2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldPurchaseDiscount2 = null;
					else entity.OldPurchaseDiscount2 = Convert.ToDecimal(value);
				}
			}
			public System.String NewPriceInBaseUnit
			{
				get
				{
					System.Decimal? data = entity.NewPriceInBaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewPriceInBaseUnit = null;
					else entity.NewPriceInBaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String NewPriceInPurchaseUnit
			{
				get
				{
					System.Decimal? data = entity.NewPriceInPurchaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewPriceInPurchaseUnit = null;
					else entity.NewPriceInPurchaseUnit = Convert.ToDecimal(value);
				}
			}
			public System.String NewPurchaseDiscount1
			{
				get
				{
					System.Decimal? data = entity.NewPurchaseDiscount1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewPurchaseDiscount1 = null;
					else entity.NewPurchaseDiscount1 = Convert.ToDecimal(value);
				}
			}
			public System.String NewPurchaseDiscount2
			{
				get
				{
					System.Decimal? data = entity.NewPurchaseDiscount2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewPurchaseDiscount2 = null;
					else entity.NewPurchaseDiscount2 = Convert.ToDecimal(value);
				}
			}
			private esAveragePriceHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAveragePriceHistoryQuery query)
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
				throw new Exception("esAveragePriceHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AveragePriceHistory : esAveragePriceHistory
	{
	}

	[Serializable]
	abstract public class esAveragePriceHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AveragePriceHistoryMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ItemUnit
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.ItemUnit, esSystemType.String);
			}
		}

		public esQueryItem ChangedDate
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.ChangedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		}

		public esQueryItem OldAveragePrice
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.OldAveragePrice, esSystemType.Decimal);
			}
		}

		public esQueryItem NewAveragePrice
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.NewAveragePrice, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem OldPriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.OldPriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem OldPriceInPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.OldPriceInPurchaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem OldPurchaseDiscount1
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount1, esSystemType.Decimal);
			}
		}

		public esQueryItem OldPurchaseDiscount2
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount2, esSystemType.Decimal);
			}
		}

		public esQueryItem NewPriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.NewPriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem NewPriceInPurchaseUnit
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.NewPriceInPurchaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem NewPurchaseDiscount1
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount1, esSystemType.Decimal);
			}
		}

		public esQueryItem NewPurchaseDiscount2
		{
			get
			{
				return new esQueryItem(this, AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount2, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AveragePriceHistoryCollection")]
	public partial class AveragePriceHistoryCollection : esAveragePriceHistoryCollection, IEnumerable<AveragePriceHistory>
	{
		public AveragePriceHistoryCollection()
		{

		}

		public static implicit operator List<AveragePriceHistory>(AveragePriceHistoryCollection coll)
		{
			List<AveragePriceHistory> list = new List<AveragePriceHistory>();

			foreach (AveragePriceHistory emp in coll)
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
				return AveragePriceHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AveragePriceHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AveragePriceHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AveragePriceHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AveragePriceHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AveragePriceHistoryQuery();
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
		public bool Load(AveragePriceHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AveragePriceHistory AddNew()
		{
			AveragePriceHistory entity = base.AddNewEntity() as AveragePriceHistory;

			return entity;
		}
		public AveragePriceHistory FindByPrimaryKey(String transactionNo, String itemID, Decimal oldAveragePrice)
		{
			return base.FindByPrimaryKey(transactionNo, itemID, oldAveragePrice) as AveragePriceHistory;
		}

		#region IEnumerable< AveragePriceHistory> Members

		IEnumerator<AveragePriceHistory> IEnumerable<AveragePriceHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AveragePriceHistory;
			}
		}

		#endregion

		private AveragePriceHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AveragePriceHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AveragePriceHistory ({TransactionNo, ItemID, OldAveragePrice})")]
	[Serializable]
	public partial class AveragePriceHistory : esAveragePriceHistory
	{
		public AveragePriceHistory()
		{
		}

		public AveragePriceHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AveragePriceHistoryMetadata.Meta();
			}
		}

		override protected esAveragePriceHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AveragePriceHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AveragePriceHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AveragePriceHistoryQuery();
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
		public bool Load(AveragePriceHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AveragePriceHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AveragePriceHistoryQuery : esAveragePriceHistoryQuery
	{
		public AveragePriceHistoryQuery()
		{

		}

		public AveragePriceHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AveragePriceHistoryQuery";
		}
	}

	[Serializable]
	public partial class AveragePriceHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AveragePriceHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.ItemUnit, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.ItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.ChangedDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.ChangedDate;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.TransactionCode, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.OldAveragePrice, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.OldAveragePrice;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.NewAveragePrice, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.NewAveragePrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.OldPriceInBaseUnit, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.OldPriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.OldPriceInPurchaseUnit, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.OldPriceInPurchaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount1, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.OldPurchaseDiscount1;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.OldPurchaseDiscount2, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.OldPurchaseDiscount2;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.NewPriceInBaseUnit, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.NewPriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.NewPriceInPurchaseUnit, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.NewPriceInPurchaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount1, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.NewPurchaseDiscount1;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AveragePriceHistoryMetadata.ColumnNames.NewPurchaseDiscount2, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AveragePriceHistoryMetadata.PropertyNames.NewPurchaseDiscount2;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AveragePriceHistoryMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string ItemUnit = "ItemUnit";
			public const string ChangedDate = "ChangedDate";
			public const string TransactionCode = "TransactionCode";
			public const string OldAveragePrice = "OldAveragePrice";
			public const string NewAveragePrice = "NewAveragePrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OldPriceInBaseUnit = "OldPriceInBaseUnit";
			public const string OldPriceInPurchaseUnit = "OldPriceInPurchaseUnit";
			public const string OldPurchaseDiscount1 = "OldPurchaseDiscount1";
			public const string OldPurchaseDiscount2 = "OldPurchaseDiscount2";
			public const string NewPriceInBaseUnit = "NewPriceInBaseUnit";
			public const string NewPriceInPurchaseUnit = "NewPriceInPurchaseUnit";
			public const string NewPurchaseDiscount1 = "NewPurchaseDiscount1";
			public const string NewPurchaseDiscount2 = "NewPurchaseDiscount2";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string ItemID = "ItemID";
			public const string ItemUnit = "ItemUnit";
			public const string ChangedDate = "ChangedDate";
			public const string TransactionCode = "TransactionCode";
			public const string OldAveragePrice = "OldAveragePrice";
			public const string NewAveragePrice = "NewAveragePrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OldPriceInBaseUnit = "OldPriceInBaseUnit";
			public const string OldPriceInPurchaseUnit = "OldPriceInPurchaseUnit";
			public const string OldPurchaseDiscount1 = "OldPurchaseDiscount1";
			public const string OldPurchaseDiscount2 = "OldPurchaseDiscount2";
			public const string NewPriceInBaseUnit = "NewPriceInBaseUnit";
			public const string NewPriceInPurchaseUnit = "NewPriceInPurchaseUnit";
			public const string NewPurchaseDiscount1 = "NewPurchaseDiscount1";
			public const string NewPurchaseDiscount2 = "NewPurchaseDiscount2";
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
			lock (typeof(AveragePriceHistoryMetadata))
			{
				if (AveragePriceHistoryMetadata.mapDelegates == null)
				{
					AveragePriceHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AveragePriceHistoryMetadata.meta == null)
				{
					AveragePriceHistoryMetadata.meta = new AveragePriceHistoryMetadata();
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
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChangedDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("OldAveragePrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NewAveragePrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OldPriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("OldPriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("OldPurchaseDiscount1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("OldPurchaseDiscount2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NewPriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NewPriceInPurchaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NewPurchaseDiscount1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NewPurchaseDiscount2", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "AveragePriceHistory";
				meta.Destination = "AveragePriceHistory";
				meta.spInsert = "proc_AveragePriceHistoryInsert";
				meta.spUpdate = "proc_AveragePriceHistoryUpdate";
				meta.spDelete = "proc_AveragePriceHistoryDelete";
				meta.spLoadAll = "proc_AveragePriceHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_AveragePriceHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AveragePriceHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
