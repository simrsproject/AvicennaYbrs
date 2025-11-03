/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/4/2021 12:25:08 PM
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
	abstract public class esAssetWorkOrderItemCollection : esEntityCollectionWAuditLog
	{
		public esAssetWorkOrderItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AssetWorkOrderItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetWorkOrderItemQuery query)
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
			this.InitQuery(query as esAssetWorkOrderItemQuery);
		}
		#endregion

		virtual public AssetWorkOrderItem DetachEntity(AssetWorkOrderItem entity)
		{
			return base.DetachEntity(entity) as AssetWorkOrderItem;
		}

		virtual public AssetWorkOrderItem AttachEntity(AssetWorkOrderItem entity)
		{
			return base.AttachEntity(entity) as AssetWorkOrderItem;
		}

		virtual public void Combine(AssetWorkOrderItemCollection collection)
		{
			base.Combine(collection);
		}

		new public AssetWorkOrderItem this[int index]
		{
			get
			{
				return base[index] as AssetWorkOrderItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetWorkOrderItem);
		}
	}

	[Serializable]
	abstract public class esAssetWorkOrderItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetWorkOrderItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetWorkOrderItem()
		{
		}

		public esAssetWorkOrderItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String orderNo, String seqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo, String seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String orderNo, String seqNo)
		{
			esAssetWorkOrderItemQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo, query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String orderNo, String seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo", orderNo);
			parms.Add("SeqNo", seqNo);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "IsMasterItem": this.str.IsMasterItem = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ItemName": this.str.ItemName = (string)value; break;
						case "Quantity": this.str.Quantity = (string)value; break;
						case "QuantityRealization": this.str.QuantityRealization = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsGeneratePrDr": this.str.IsGeneratePrDr = (string)value; break;
						case "Specification": this.str.Specification = (string)value; break;
						case "IsGenerateIr": this.str.IsGenerateIr = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsMasterItem":

							if (value == null || value is System.Boolean)
								this.IsMasterItem = (System.Boolean?)value;
							break;
						case "Quantity":

							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						case "QuantityRealization":

							if (value == null || value is System.Decimal)
								this.QuantityRealization = (System.Decimal?)value;
							break;
						case "ConversionFactor":

							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
							break;
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsGeneratePrDr":

							if (value == null || value is System.Boolean)
								this.IsGeneratePrDr = (System.Boolean?)value;
							break;
						case "IsGenerateIr":

							if (value == null || value is System.Boolean)
								this.IsGenerateIr = (System.Boolean?)value;
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
		/// Maps to AssetWorkOrderItem.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderItemMetadata.ColumnNames.OrderNo);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderItemMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.SeqNo
		/// </summary>
		virtual public System.String SeqNo
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderItemMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderItemMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.IsMasterItem
		/// </summary>
		virtual public System.Boolean? IsMasterItem
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderItemMetadata.ColumnNames.IsMasterItem);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderItemMetadata.ColumnNames.IsMasterItem, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.ItemName
		/// </summary>
		virtual public System.String ItemName
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderItemMetadata.ColumnNames.ItemName);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderItemMetadata.ColumnNames.ItemName, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.Quantity);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.Quantity, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.QuantityRealization
		/// </summary>
		virtual public System.Decimal? QuantityRealization
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.QuantityRealization);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.QuantityRealization, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.ConversionFactor);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(AssetWorkOrderItemMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetWorkOrderItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetWorkOrderItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.IsGeneratePrDr
		/// </summary>
		virtual public System.Boolean? IsGeneratePrDr
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderItemMetadata.ColumnNames.IsGeneratePrDr);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderItemMetadata.ColumnNames.IsGeneratePrDr, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.Specification
		/// </summary>
		virtual public System.String Specification
		{
			get
			{
				return base.GetSystemString(AssetWorkOrderItemMetadata.ColumnNames.Specification);
			}

			set
			{
				base.SetSystemString(AssetWorkOrderItemMetadata.ColumnNames.Specification, value);
			}
		}
		/// <summary>
		/// Maps to AssetWorkOrderItem.IsGenerateIr
		/// </summary>
		virtual public System.Boolean? IsGenerateIr
		{
			get
			{
				return base.GetSystemBoolean(AssetWorkOrderItemMetadata.ColumnNames.IsGenerateIr);
			}

			set
			{
				base.SetSystemBoolean(AssetWorkOrderItemMetadata.ColumnNames.IsGenerateIr, value);
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
			public esStrings(esAssetWorkOrderItem entity)
			{
				this.entity = entity;
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String SeqNo
			{
				get
				{
					System.String data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToString(value);
				}
			}
			public System.String IsMasterItem
			{
				get
				{
					System.Boolean? data = entity.IsMasterItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMasterItem = null;
					else entity.IsMasterItem = Convert.ToBoolean(value);
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
			public System.String QuantityRealization
			{
				get
				{
					System.Decimal? data = entity.QuantityRealization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityRealization = null;
					else entity.QuantityRealization = Convert.ToDecimal(value);
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
			public System.String IsGeneratePrDr
			{
				get
				{
					System.Boolean? data = entity.IsGeneratePrDr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGeneratePrDr = null;
					else entity.IsGeneratePrDr = Convert.ToBoolean(value);
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
			public System.String IsGenerateIr
			{
				get
				{
					System.Boolean? data = entity.IsGenerateIr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateIr = null;
					else entity.IsGenerateIr = Convert.ToBoolean(value);
				}
			}
			private esAssetWorkOrderItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetWorkOrderItemQuery query)
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
				throw new Exception("esAssetWorkOrderItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AssetWorkOrderItem : esAssetWorkOrderItem
	{
	}

	[Serializable]
	abstract public class esAssetWorkOrderItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AssetWorkOrderItemMetadata.Meta();
			}
		}

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.SeqNo, esSystemType.String);
			}
		}

		public esQueryItem IsMasterItem
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.IsMasterItem, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ItemName
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.ItemName, esSystemType.String);
			}
		}

		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		}

		public esQueryItem QuantityRealization
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.QuantityRealization, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsGeneratePrDr
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.IsGeneratePrDr, esSystemType.Boolean);
			}
		}

		public esQueryItem Specification
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.Specification, esSystemType.String);
			}
		}

		public esQueryItem IsGenerateIr
		{
			get
			{
				return new esQueryItem(this, AssetWorkOrderItemMetadata.ColumnNames.IsGenerateIr, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetWorkOrderItemCollection")]
	public partial class AssetWorkOrderItemCollection : esAssetWorkOrderItemCollection, IEnumerable<AssetWorkOrderItem>
	{
		public AssetWorkOrderItemCollection()
		{

		}

		public static implicit operator List<AssetWorkOrderItem>(AssetWorkOrderItemCollection coll)
		{
			List<AssetWorkOrderItem> list = new List<AssetWorkOrderItem>();

			foreach (AssetWorkOrderItem emp in coll)
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
				return AssetWorkOrderItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetWorkOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetWorkOrderItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetWorkOrderItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AssetWorkOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetWorkOrderItemQuery();
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
		public bool Load(AssetWorkOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AssetWorkOrderItem AddNew()
		{
			AssetWorkOrderItem entity = base.AddNewEntity() as AssetWorkOrderItem;

			return entity;
		}
		public AssetWorkOrderItem FindByPrimaryKey(String orderNo, String seqNo)
		{
			return base.FindByPrimaryKey(orderNo, seqNo) as AssetWorkOrderItem;
		}

		#region IEnumerable< AssetWorkOrderItem> Members

		IEnumerator<AssetWorkOrderItem> IEnumerable<AssetWorkOrderItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AssetWorkOrderItem;
			}
		}

		#endregion

		private AssetWorkOrderItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetWorkOrderItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AssetWorkOrderItem ({OrderNo, SeqNo})")]
	[Serializable]
	public partial class AssetWorkOrderItem : esAssetWorkOrderItem
	{
		public AssetWorkOrderItem()
		{
		}

		public AssetWorkOrderItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetWorkOrderItemMetadata.Meta();
			}
		}

		override protected esAssetWorkOrderItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetWorkOrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AssetWorkOrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetWorkOrderItemQuery();
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
		public bool Load(AssetWorkOrderItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AssetWorkOrderItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AssetWorkOrderItemQuery : esAssetWorkOrderItemQuery
	{
		public AssetWorkOrderItemQuery()
		{

		}

		public AssetWorkOrderItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AssetWorkOrderItemQuery";
		}
	}

	[Serializable]
	public partial class AssetWorkOrderItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetWorkOrderItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.SeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.IsMasterItem, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.IsMasterItem;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.ItemName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.Quantity, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.QuantityRealization, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.QuantityRealization;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.SRItemUnit, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.ConversionFactor, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.CostPrice, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.Price, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.IsGeneratePrDr, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.IsGeneratePrDr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.Specification, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.Specification;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetWorkOrderItemMetadata.ColumnNames.IsGenerateIr, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetWorkOrderItemMetadata.PropertyNames.IsGenerateIr;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AssetWorkOrderItemMetadata Meta()
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
			public const string OrderNo = "OrderNo";
			public const string SeqNo = "SeqNo";
			public const string IsMasterItem = "IsMasterItem";
			public const string ItemID = "ItemID";
			public const string ItemName = "ItemName";
			public const string Quantity = "Quantity";
			public const string QuantityRealization = "QuantityRealization";
			public const string SRItemUnit = "SRItemUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string CostPrice = "CostPrice";
			public const string Price = "Price";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsGeneratePrDr = "IsGeneratePrDr";
			public const string Specification = "Specification";
			public const string IsGenerateIr = "IsGenerateIr";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string OrderNo = "OrderNo";
			public const string SeqNo = "SeqNo";
			public const string IsMasterItem = "IsMasterItem";
			public const string ItemID = "ItemID";
			public const string ItemName = "ItemName";
			public const string Quantity = "Quantity";
			public const string QuantityRealization = "QuantityRealization";
			public const string SRItemUnit = "SRItemUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string CostPrice = "CostPrice";
			public const string Price = "Price";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsGeneratePrDr = "IsGeneratePrDr";
			public const string Specification = "Specification";
			public const string IsGenerateIr = "IsGenerateIr";
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
			lock (typeof(AssetWorkOrderItemMetadata))
			{
				if (AssetWorkOrderItemMetadata.mapDelegates == null)
				{
					AssetWorkOrderItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AssetWorkOrderItemMetadata.meta == null)
				{
					AssetWorkOrderItemMetadata.meta = new AssetWorkOrderItemMetadata();
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

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMasterItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityRealization", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGeneratePrDr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Specification", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGenerateIr", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "AssetWorkOrderItem";
				meta.Destination = "AssetWorkOrderItem";
				meta.spInsert = "proc_AssetWorkOrderItemInsert";
				meta.spUpdate = "proc_AssetWorkOrderItemUpdate";
				meta.spDelete = "proc_AssetWorkOrderItemDelete";
				meta.spLoadAll = "proc_AssetWorkOrderItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetWorkOrderItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetWorkOrderItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
