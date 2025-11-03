/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/26/2023 3:03:09 PM
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
	abstract public class esItemMovementCollection : esEntityCollectionWAuditLog
	{
		public esItemMovementCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemMovementCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemMovementQuery query)
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
			this.InitQuery(query as esItemMovementQuery);
		}
		#endregion

		virtual public ItemMovement DetachEntity(ItemMovement entity)
		{
			return base.DetachEntity(entity) as ItemMovement;
		}

		virtual public ItemMovement AttachEntity(ItemMovement entity)
		{
			return base.AttachEntity(entity) as ItemMovement;
		}

		virtual public void Combine(ItemMovementCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemMovement this[int index]
		{
			get
			{
				return base[index] as ItemMovement;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemMovement);
		}
	}

	[Serializable]
	abstract public class esItemMovement : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemMovementQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemMovement()
		{
		}

		public esItemMovement(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Guid movementID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(movementID);
			else
				return LoadByPrimaryKeyStoredProcedure(movementID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Guid movementID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(movementID);
			else
				return LoadByPrimaryKeyStoredProcedure(movementID);
		}

		private bool LoadByPrimaryKeyDynamic(Guid movementID)
		{
			esItemMovementQuery query = this.GetDynamicQuery();
			query.Where(query.MovementID == movementID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Guid movementID)
		{
			esParameters parms = new esParameters();
			parms.Add("MovementID", movementID);
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
						case "MovementID": this.str.MovementID = (string)value; break;
						case "MovementDate": this.str.MovementDate = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
						case "TransactionCode": this.str.TransactionCode = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "InitialStock": this.str.InitialStock = (string)value; break;
						case "QuantityIn": this.str.QuantityIn = (string)value; break;
						case "QuantityOut": this.str.QuantityOut = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "CostPrice": this.str.CostPrice = (string)value; break;
						case "SalesPrice": this.str.SalesPrice = (string)value; break;
						case "PurchasePrice": this.str.PurchasePrice = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastPriceInBaseUnit": this.str.LastPriceInBaseUnit = (string)value; break;
						case "BatchNumber": this.str.BatchNumber = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MovementID":

							if (value == null || value is System.Guid)
								this.MovementID = (System.Guid?)value;
							break;
						case "MovementDate":

							if (value == null || value is System.DateTime)
								this.MovementDate = (System.DateTime?)value;
							break;
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "InitialStock":

							if (value == null || value is System.Decimal)
								this.InitialStock = (System.Decimal?)value;
							break;
						case "QuantityIn":

							if (value == null || value is System.Decimal)
								this.QuantityIn = (System.Decimal?)value;
							break;
						case "QuantityOut":

							if (value == null || value is System.Decimal)
								this.QuantityOut = (System.Decimal?)value;
							break;
						case "CostPrice":

							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						case "SalesPrice":

							if (value == null || value is System.Decimal)
								this.SalesPrice = (System.Decimal?)value;
							break;
						case "PurchasePrice":

							if (value == null || value is System.Decimal)
								this.PurchasePrice = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LastPriceInBaseUnit":

							if (value == null || value is System.Decimal)
								this.LastPriceInBaseUnit = (System.Decimal?)value;
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
		/// Maps to ItemMovement.MovementID
		/// </summary>
		virtual public System.Guid? MovementID
		{
			get
			{
				return base.GetSystemGuid(ItemMovementMetadata.ColumnNames.MovementID);
			}

			set
			{
				base.SetSystemGuid(ItemMovementMetadata.ColumnNames.MovementID, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.MovementDate
		/// </summary>
		virtual public System.DateTime? MovementDate
		{
			get
			{
				return base.GetSystemDateTime(ItemMovementMetadata.ColumnNames.MovementDate);
			}

			set
			{
				base.SetSystemDateTime(ItemMovementMetadata.ColumnNames.MovementDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.TransactionCode);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.TransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ItemMovementMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(ItemMovementMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.InitialStock
		/// </summary>
		virtual public System.Decimal? InitialStock
		{
			get
			{
				return base.GetSystemDecimal(ItemMovementMetadata.ColumnNames.InitialStock);
			}

			set
			{
				base.SetSystemDecimal(ItemMovementMetadata.ColumnNames.InitialStock, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.QuantityIn
		/// </summary>
		virtual public System.Decimal? QuantityIn
		{
			get
			{
				return base.GetSystemDecimal(ItemMovementMetadata.ColumnNames.QuantityIn);
			}

			set
			{
				base.SetSystemDecimal(ItemMovementMetadata.ColumnNames.QuantityIn, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.QuantityOut
		/// </summary>
		virtual public System.Decimal? QuantityOut
		{
			get
			{
				return base.GetSystemDecimal(ItemMovementMetadata.ColumnNames.QuantityOut);
			}

			set
			{
				base.SetSystemDecimal(ItemMovementMetadata.ColumnNames.QuantityOut, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemMovementMetadata.ColumnNames.CostPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemMovementMetadata.ColumnNames.CostPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.SalesPrice
		/// </summary>
		virtual public System.Decimal? SalesPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemMovementMetadata.ColumnNames.SalesPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemMovementMetadata.ColumnNames.SalesPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.PurchasePrice
		/// </summary>
		virtual public System.Decimal? PurchasePrice
		{
			get
			{
				return base.GetSystemDecimal(ItemMovementMetadata.ColumnNames.PurchasePrice);
			}

			set
			{
				base.SetSystemDecimal(ItemMovementMetadata.ColumnNames.PurchasePrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemMovementMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemMovementMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.LastPriceInBaseUnit
		/// </summary>
		virtual public System.Decimal? LastPriceInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemMovementMetadata.ColumnNames.LastPriceInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemMovementMetadata.ColumnNames.LastPriceInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemMovement.BatchNumber
		/// </summary>
		virtual public System.String BatchNumber
		{
			get
			{
				return base.GetSystemString(ItemMovementMetadata.ColumnNames.BatchNumber);
			}

			set
			{
				base.SetSystemString(ItemMovementMetadata.ColumnNames.BatchNumber, value);
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
			public esStrings(esItemMovement entity)
			{
				this.entity = entity;
			}
			public System.String MovementID
			{
				get
				{
					System.Guid? data = entity.MovementID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MovementID = null;
					else entity.MovementID = new Guid(value);
				}
			}
			public System.String MovementDate
			{
				get
				{
					System.DateTime? data = entity.MovementDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MovementDate = null;
					else entity.MovementDate = Convert.ToDateTime(value);
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
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
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
			public System.String InitialStock
			{
				get
				{
					System.Decimal? data = entity.InitialStock;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialStock = null;
					else entity.InitialStock = Convert.ToDecimal(value);
				}
			}
			public System.String QuantityIn
			{
				get
				{
					System.Decimal? data = entity.QuantityIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityIn = null;
					else entity.QuantityIn = Convert.ToDecimal(value);
				}
			}
			public System.String QuantityOut
			{
				get
				{
					System.Decimal? data = entity.QuantityOut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityOut = null;
					else entity.QuantityOut = Convert.ToDecimal(value);
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
			public System.String PurchasePrice
			{
				get
				{
					System.Decimal? data = entity.PurchasePrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchasePrice = null;
					else entity.PurchasePrice = Convert.ToDecimal(value);
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
			private esItemMovement entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemMovementQuery query)
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
				throw new Exception("esItemMovement can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemMovement : esItemMovement
	{
	}

	[Serializable]
	abstract public class esItemMovementQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemMovementMetadata.Meta();
			}
		}

		public esQueryItem MovementID
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.MovementID, esSystemType.Guid);
			}
		}

		public esQueryItem MovementDate
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.MovementDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem InitialStock
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.InitialStock, esSystemType.Decimal);
			}
		}

		public esQueryItem QuantityIn
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.QuantityIn, esSystemType.Decimal);
			}
		}

		public esQueryItem QuantityOut
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.QuantityOut, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem SalesPrice
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.SalesPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem PurchasePrice
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.PurchasePrice, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastPriceInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.LastPriceInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem BatchNumber
		{
			get
			{
				return new esQueryItem(this, ItemMovementMetadata.ColumnNames.BatchNumber, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemMovementCollection")]
	public partial class ItemMovementCollection : esItemMovementCollection, IEnumerable<ItemMovement>
	{
		public ItemMovementCollection()
		{

		}

		public static implicit operator List<ItemMovement>(ItemMovementCollection coll)
		{
			List<ItemMovement> list = new List<ItemMovement>();

			foreach (ItemMovement emp in coll)
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
				return ItemMovementMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemMovement(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemMovement();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemMovementQuery();
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
		public bool Load(ItemMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemMovement AddNew()
		{
			ItemMovement entity = base.AddNewEntity() as ItemMovement;

			return entity;
		}
		public ItemMovement FindByPrimaryKey(Guid movementID)
		{
			return base.FindByPrimaryKey(movementID) as ItemMovement;
		}

		#region IEnumerable< ItemMovement> Members

		IEnumerator<ItemMovement> IEnumerable<ItemMovement>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemMovement;
			}
		}

		#endregion

		private ItemMovementQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemMovement' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemMovement ({MovementID})")]
	[Serializable]
	public partial class ItemMovement : esItemMovement
	{
		public ItemMovement()
		{
		}

		public ItemMovement(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemMovementMetadata.Meta();
			}
		}

		override protected esItemMovementQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemMovementQuery();
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
		public bool Load(ItemMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemMovementQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemMovementQuery : esItemMovementQuery
	{
		public ItemMovementQuery()
		{

		}

		public ItemMovementQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemMovementQuery";
		}
	}

	[Serializable]
	public partial class ItemMovementMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemMovementMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.MovementID, 0, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = ItemMovementMetadata.PropertyNames.MovementID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 0;
			c.HasDefault = true;
			c.Default = @"(newid())";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.MovementDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemMovementMetadata.PropertyNames.MovementDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.LocationID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.TransactionCode, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.TransactionNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.SequenceNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 6;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.ItemID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.ExpiredDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemMovementMetadata.PropertyNames.ExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.InitialStock, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMovementMetadata.PropertyNames.InitialStock;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.QuantityIn, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMovementMetadata.PropertyNames.QuantityIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.QuantityOut, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMovementMetadata.PropertyNames.QuantityOut;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.SRItemUnit, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.CostPrice, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMovementMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.SalesPrice, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMovementMetadata.PropertyNames.SalesPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.PurchasePrice, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMovementMetadata.PropertyNames.PurchasePrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemMovementMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.LastPriceInBaseUnit, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemMovementMetadata.PropertyNames.LastPriceInBaseUnit;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemMovementMetadata.ColumnNames.BatchNumber, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemMovementMetadata.PropertyNames.BatchNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemMovementMetadata Meta()
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
			public const string MovementID = "MovementID";
			public const string MovementDate = "MovementDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LocationID = "LocationID";
			public const string TransactionCode = "TransactionCode";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string ExpiredDate = "ExpiredDate";
			public const string InitialStock = "InitialStock";
			public const string QuantityIn = "QuantityIn";
			public const string QuantityOut = "QuantityOut";
			public const string SRItemUnit = "SRItemUnit";
			public const string CostPrice = "CostPrice";
			public const string SalesPrice = "SalesPrice";
			public const string PurchasePrice = "PurchasePrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
			public const string BatchNumber = "BatchNumber";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MovementID = "MovementID";
			public const string MovementDate = "MovementDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LocationID = "LocationID";
			public const string TransactionCode = "TransactionCode";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string ExpiredDate = "ExpiredDate";
			public const string InitialStock = "InitialStock";
			public const string QuantityIn = "QuantityIn";
			public const string QuantityOut = "QuantityOut";
			public const string SRItemUnit = "SRItemUnit";
			public const string CostPrice = "CostPrice";
			public const string SalesPrice = "SalesPrice";
			public const string PurchasePrice = "PurchasePrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastPriceInBaseUnit = "LastPriceInBaseUnit";
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
			lock (typeof(ItemMovementMetadata))
			{
				if (ItemMovementMetadata.mapDelegates == null)
				{
					ItemMovementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemMovementMetadata.meta == null)
				{
					ItemMovementMetadata.meta = new ItemMovementMetadata();
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

				meta.AddTypeMap("MovementID", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("MovementDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InitialStock", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SalesPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PurchasePrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastPriceInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemMovement";
				meta.Destination = "ItemMovement";
				meta.spInsert = "proc_ItemMovementInsert";
				meta.spUpdate = "proc_ItemMovementUpdate";
				meta.spDelete = "proc_ItemMovementDelete";
				meta.spLoadAll = "proc_ItemMovementLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemMovementLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemMovementMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
