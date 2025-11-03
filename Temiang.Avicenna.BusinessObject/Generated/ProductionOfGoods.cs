/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/16/2023 9:59:38 AM
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
	abstract public class esProductionOfGoodsCollection : esEntityCollectionWAuditLog
	{
		public esProductionOfGoodsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ProductionOfGoodsCollection";
		}

		#region Query Logic
		protected void InitQuery(esProductionOfGoodsQuery query)
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
			this.InitQuery(query as esProductionOfGoodsQuery);
		}
		#endregion

		virtual public ProductionOfGoods DetachEntity(ProductionOfGoods entity)
		{
			return base.DetachEntity(entity) as ProductionOfGoods;
		}

		virtual public ProductionOfGoods AttachEntity(ProductionOfGoods entity)
		{
			return base.AttachEntity(entity) as ProductionOfGoods;
		}

		virtual public void Combine(ProductionOfGoodsCollection collection)
		{
			base.Combine(collection);
		}

		new public ProductionOfGoods this[int index]
		{
			get
			{
				return base[index] as ProductionOfGoods;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ProductionOfGoods);
		}
	}

	[Serializable]
	abstract public class esProductionOfGoods : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esProductionOfGoodsQuery GetDynamicQuery()
		{
			return null;
		}

		public esProductionOfGoods()
		{
		}

		public esProductionOfGoods(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String productionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(productionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String productionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(productionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String productionNo)
		{
			esProductionOfGoodsQuery query = this.GetDynamicQuery();
			query.Where(query.ProductionNo == productionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String productionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ProductionNo", productionNo);
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
						case "ProductionNo": this.str.ProductionNo = (string)value; break;
						case "ProductionDate": this.str.ProductionDate = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
						case "FormulaID": this.str.FormulaID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "CostAmount": this.str.CostAmount = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "BatchNumber": this.str.BatchNumber = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ProductionDate":

							if (value == null || value is System.DateTime)
								this.ProductionDate = (System.DateTime?)value;
							break;
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "CostAmount":

							if (value == null || value is System.Decimal)
								this.CostAmount = (System.Decimal?)value;
							break;
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to ProductionOfGoods.ProductionNo
		/// </summary>
		virtual public System.String ProductionNo
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.ProductionNo);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.ProductionNo, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.ProductionDate
		/// </summary>
		virtual public System.DateTime? ProductionDate
		{
			get
			{
				return base.GetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.ProductionDate);
			}

			set
			{
				base.SetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.ProductionDate, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.FormulaID
		/// </summary>
		virtual public System.String FormulaID
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.FormulaID);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.FormulaID, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(ProductionOfGoodsMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(ProductionOfGoodsMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ProductionOfGoodsMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(ProductionOfGoodsMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.CostAmount
		/// </summary>
		virtual public System.Decimal? CostAmount
		{
			get
			{
				return base.GetSystemDecimal(ProductionOfGoodsMetadata.ColumnNames.CostAmount);
			}

			set
			{
				base.SetSystemDecimal(ProductionOfGoodsMetadata.ColumnNames.CostAmount, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ProductionOfGoodsMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ProductionOfGoodsMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ProductionOfGoodsMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ProductionOfGoodsMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ProductionOfGoodsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ProductionOfGoods.BatchNumber
		/// </summary>
		virtual public System.String BatchNumber
		{
			get
			{
				return base.GetSystemString(ProductionOfGoodsMetadata.ColumnNames.BatchNumber);
			}

			set
			{
				base.SetSystemString(ProductionOfGoodsMetadata.ColumnNames.BatchNumber, value);
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
			public esStrings(esProductionOfGoods entity)
			{
				this.entity = entity;
			}
			public System.String ProductionNo
			{
				get
				{
					System.String data = entity.ProductionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductionNo = null;
					else entity.ProductionNo = Convert.ToString(value);
				}
			}
			public System.String ProductionDate
			{
				get
				{
					System.DateTime? data = entity.ProductionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductionDate = null;
					else entity.ProductionDate = Convert.ToDateTime(value);
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
			public System.String FormulaID
			{
				get
				{
					System.String data = entity.FormulaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaID = null;
					else entity.FormulaID = Convert.ToString(value);
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
			public System.String CostAmount
			{
				get
				{
					System.Decimal? data = entity.CostAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostAmount = null;
					else entity.CostAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
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
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String BatchNumber
			{
				get
				{
					System.String data = entity.BatchNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BatchNumber = null;
					else entity.BatchNumber = Convert.ToString(value);
				}
			}
			private esProductionOfGoods entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esProductionOfGoodsQuery query)
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
				throw new Exception("esProductionOfGoods can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ProductionOfGoods : esProductionOfGoods
	{
	}

	[Serializable]
	abstract public class esProductionOfGoodsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ProductionOfGoodsMetadata.Meta();
			}
		}

		public esQueryItem ProductionNo
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.ProductionNo, esSystemType.String);
			}
		}

		public esQueryItem ProductionDate
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.ProductionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem FormulaID
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.FormulaID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem CostAmount
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.CostAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem BatchNumber
		{
			get
			{
				return new esQueryItem(this, ProductionOfGoodsMetadata.ColumnNames.BatchNumber, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ProductionOfGoodsCollection")]
	public partial class ProductionOfGoodsCollection : esProductionOfGoodsCollection, IEnumerable<ProductionOfGoods>
	{
		public ProductionOfGoodsCollection()
		{

		}

		public static implicit operator List<ProductionOfGoods>(ProductionOfGoodsCollection coll)
		{
			List<ProductionOfGoods> list = new List<ProductionOfGoods>();

			foreach (ProductionOfGoods emp in coll)
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
				return ProductionOfGoodsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductionOfGoodsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ProductionOfGoods(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ProductionOfGoods();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ProductionOfGoodsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductionOfGoodsQuery();
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
		public bool Load(ProductionOfGoodsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ProductionOfGoods AddNew()
		{
			ProductionOfGoods entity = base.AddNewEntity() as ProductionOfGoods;

			return entity;
		}
		public ProductionOfGoods FindByPrimaryKey(String productionNo)
		{
			return base.FindByPrimaryKey(productionNo) as ProductionOfGoods;
		}

		#region IEnumerable< ProductionOfGoods> Members

		IEnumerator<ProductionOfGoods> IEnumerable<ProductionOfGoods>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ProductionOfGoods;
			}
		}

		#endregion

		private ProductionOfGoodsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ProductionOfGoods' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ProductionOfGoods ({ProductionNo})")]
	[Serializable]
	public partial class ProductionOfGoods : esProductionOfGoods
	{
		public ProductionOfGoods()
		{
		}

		public ProductionOfGoods(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductionOfGoodsMetadata.Meta();
			}
		}

		override protected esProductionOfGoodsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductionOfGoodsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ProductionOfGoodsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductionOfGoodsQuery();
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
		public bool Load(ProductionOfGoodsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ProductionOfGoodsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ProductionOfGoodsQuery : esProductionOfGoodsQuery
	{
		public ProductionOfGoodsQuery()
		{

		}

		public ProductionOfGoodsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ProductionOfGoodsQuery";
		}
	}

	[Serializable]
	public partial class ProductionOfGoodsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductionOfGoodsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.ProductionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.ProductionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.ProductionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.ProductionDate;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.LocationID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.FormulaID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.FormulaID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.Qty, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.Price, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.CostAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.CostAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.ExpiredDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.ExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.Notes, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.IsApproved, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.ApprovedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.ApprovedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.VoidDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.VoidByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.ToServiceUnitID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ProductionOfGoodsMetadata.ColumnNames.BatchNumber, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionOfGoodsMetadata.PropertyNames.BatchNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ProductionOfGoodsMetadata Meta()
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
			public const string ProductionNo = "ProductionNo";
			public const string ProductionDate = "ProductionDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LocationID = "LocationID";
			public const string FormulaID = "FormulaID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string CostAmount = "CostAmount";
			public const string ExpiredDate = "ExpiredDate";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string BatchNumber = "BatchNumber";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ProductionNo = "ProductionNo";
			public const string ProductionDate = "ProductionDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LocationID = "LocationID";
			public const string FormulaID = "FormulaID";
			public const string Qty = "Qty";
			public const string Price = "Price";
			public const string CostAmount = "CostAmount";
			public const string ExpiredDate = "ExpiredDate";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string BatchNumber = "BatchNumber";
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
			lock (typeof(ProductionOfGoodsMetadata))
			{
				if (ProductionOfGoodsMetadata.mapDelegates == null)
				{
					ProductionOfGoodsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ProductionOfGoodsMetadata.meta == null)
				{
					ProductionOfGoodsMetadata.meta = new ProductionOfGoodsMetadata();
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

				meta.AddTypeMap("ProductionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProductionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CostAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));


				meta.Source = "ProductionOfGoods";
				meta.Destination = "ProductionOfGoods";
				meta.spInsert = "proc_ProductionOfGoodsInsert";
				meta.spUpdate = "proc_ProductionOfGoodsUpdate";
				meta.spDelete = "proc_ProductionOfGoodsDelete";
				meta.spLoadAll = "proc_ProductionOfGoodsLoadAll";
				meta.spLoadByPrimaryKey = "proc_ProductionOfGoodsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductionOfGoodsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
