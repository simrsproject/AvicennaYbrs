/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/26/2023 1:31:57 PM
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
	abstract public class esItemTransactionItemEdCollection : esEntityCollectionWAuditLog
	{
		public esItemTransactionItemEdCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTransactionItemEdCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTransactionItemEdQuery query)
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
			this.InitQuery(query as esItemTransactionItemEdQuery);
		}
		#endregion

		virtual public ItemTransactionItemEd DetachEntity(ItemTransactionItemEd entity)
		{
			return base.DetachEntity(entity) as ItemTransactionItemEd;
		}

		virtual public ItemTransactionItemEd AttachEntity(ItemTransactionItemEd entity)
		{
			return base.AttachEntity(entity) as ItemTransactionItemEd;
		}

		virtual public void Combine(ItemTransactionItemEdCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemTransactionItemEd this[int index]
		{
			get
			{
				return base[index] as ItemTransactionItemEd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTransactionItemEd);
		}
	}

	[Serializable]
	abstract public class esItemTransactionItemEd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTransactionItemEdQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTransactionItemEd()
		{
		}

		public esItemTransactionItemEd(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, DateTime expiredDate, String batchNumber)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, expiredDate, batchNumber);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, expiredDate, batchNumber);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, DateTime expiredDate, String batchNumber)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, expiredDate, batchNumber);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, expiredDate, batchNumber);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, DateTime expiredDate, String batchNumber)
		{
			esItemTransactionItemEdQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.ExpiredDate == expiredDate, query.BatchNumber == batchNumber);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, DateTime expiredDate, String batchNumber)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SequenceNo", sequenceNo);
			parms.Add("ExpiredDate", expiredDate);
			parms.Add("BatchNumber", batchNumber);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
						case "BatchNumber": this.str.BatchNumber = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Quantity": this.str.Quantity = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
						case "QuantityFinishInBaseUnit": this.str.QuantityFinishInBaseUnit = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ClosedDateTime": this.str.ClosedDateTime = (string)value; break;
						case "ClosedByUserID": this.str.ClosedByUserID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						case "Quantity":

							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						case "ConversionFactor":

							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
							break;
						case "QuantityFinishInBaseUnit":

							if (value == null || value is System.Decimal)
								this.QuantityFinishInBaseUnit = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "ClosedDateTime":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime = (System.DateTime?)value;
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
		/// Maps to ItemTransactionItemEd.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.BatchNumber
		/// </summary>
		virtual public System.String BatchNumber
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.BatchNumber);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.BatchNumber, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemEdMetadata.ColumnNames.Quantity);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemEdMetadata.ColumnNames.Quantity, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemEdMetadata.ColumnNames.ConversionFactor);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemEdMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.QuantityFinishInBaseUnit
		/// </summary>
		virtual public System.Decimal? QuantityFinishInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemEdMetadata.ColumnNames.QuantityFinishInBaseUnit);
			}

			set
			{
				base.SetSystemDecimal(ItemTransactionItemEdMetadata.ColumnNames.QuantityFinishInBaseUnit, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemEdMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionItemEdMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemEdMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(ItemTransactionItemEdMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.ClosedDateTime
		/// </summary>
		virtual public System.DateTime? ClosedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemEdMetadata.ColumnNames.ClosedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTransactionItemEdMetadata.ColumnNames.ClosedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.ClosedByUserID
		/// </summary>
		virtual public System.String ClosedByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ClosedByUserID);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ClosedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTransactionItemEd.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ReferenceSequenceNo);
			}

			set
			{
				base.SetSystemString(ItemTransactionItemEdMetadata.ColumnNames.ReferenceSequenceNo, value);
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
			public esStrings(esItemTransactionItemEd entity)
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
			public System.String QuantityFinishInBaseUnit
			{
				get
				{
					System.Decimal? data = entity.QuantityFinishInBaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityFinishInBaseUnit = null;
					else entity.QuantityFinishInBaseUnit = Convert.ToDecimal(value);
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
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime = null;
					else entity.ClosedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedByUserID
			{
				get
				{
					System.String data = entity.ClosedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedByUserID = null;
					else entity.ClosedByUserID = Convert.ToString(value);
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
			public System.String ReferenceSequenceNo
			{
				get
				{
					System.String data = entity.ReferenceSequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceSequenceNo = null;
					else entity.ReferenceSequenceNo = Convert.ToString(value);
				}
			}
			private esItemTransactionItemEd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTransactionItemEdQuery query)
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
				throw new Exception("esItemTransactionItemEd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTransactionItemEd : esItemTransactionItemEd
	{
	}

	[Serializable]
	abstract public class esItemTransactionItemEdQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionItemEdMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

		public esQueryItem BatchNumber
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.BatchNumber, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		}

		public esQueryItem QuantityFinishInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.QuantityFinishInBaseUnit, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.ClosedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.ClosedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemEdMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTransactionItemEdCollection")]
	public partial class ItemTransactionItemEdCollection : esItemTransactionItemEdCollection, IEnumerable<ItemTransactionItemEd>
	{
		public ItemTransactionItemEdCollection()
		{

		}

		public static implicit operator List<ItemTransactionItemEd>(ItemTransactionItemEdCollection coll)
		{
			List<ItemTransactionItemEd> list = new List<ItemTransactionItemEd>();

			foreach (ItemTransactionItemEd emp in coll)
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
				return ItemTransactionItemEdMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionItemEdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTransactionItemEd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTransactionItemEd();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTransactionItemEdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionItemEdQuery();
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
		public bool Load(ItemTransactionItemEdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTransactionItemEd AddNew()
		{
			ItemTransactionItemEd entity = base.AddNewEntity() as ItemTransactionItemEd;

			return entity;
		}
		public ItemTransactionItemEd FindByPrimaryKey(String transactionNo, String sequenceNo, DateTime expiredDate, String batchNumber)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, expiredDate, batchNumber) as ItemTransactionItemEd;
		}

		#region IEnumerable< ItemTransactionItemEd> Members

		IEnumerator<ItemTransactionItemEd> IEnumerable<ItemTransactionItemEd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTransactionItemEd;
			}
		}

		#endregion

		private ItemTransactionItemEdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTransactionItemEd' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTransactionItemEd ({TransactionNo, SequenceNo, ExpiredDate, BatchNumber})")]
	[Serializable]
	public partial class ItemTransactionItemEd : esItemTransactionItemEd
	{
		public ItemTransactionItemEd()
		{
		}

		public ItemTransactionItemEd(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionItemEdMetadata.Meta();
			}
		}

		override protected esItemTransactionItemEdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionItemEdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTransactionItemEdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionItemEdQuery();
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
		public bool Load(ItemTransactionItemEdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTransactionItemEdQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTransactionItemEdQuery : esItemTransactionItemEdQuery
	{
		public ItemTransactionItemEdQuery()
		{

		}

		public ItemTransactionItemEdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTransactionItemEdQuery";
		}
	}

	[Serializable]
	public partial class ItemTransactionItemEdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTransactionItemEdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.ExpiredDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.ExpiredDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.BatchNumber, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.BatchNumber;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.ItemID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.Quantity, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.SRItemUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.ConversionFactor, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.QuantityFinishInBaseUnit, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.QuantityFinishInBaseUnit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.IsClosed, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.ClosedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.ClosedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.ClosedByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.ClosedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.ReferenceNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTransactionItemEdMetadata.ColumnNames.ReferenceSequenceNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemEdMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTransactionItemEdMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string ExpiredDate = "ExpiredDate";
			public const string BatchNumber = "BatchNumber";
			public const string ItemID = "ItemID";
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string QuantityFinishInBaseUnit = "QuantityFinishInBaseUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ExpiredDate = "ExpiredDate";
			public const string BatchNumber = "BatchNumber";
			public const string ItemID = "ItemID";
			public const string Quantity = "Quantity";
			public const string SRItemUnit = "SRItemUnit";
			public const string ConversionFactor = "ConversionFactor";
			public const string QuantityFinishInBaseUnit = "QuantityFinishInBaseUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
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
			lock (typeof(ItemTransactionItemEdMetadata))
			{
				if (ItemTransactionItemEdMetadata.mapDelegates == null)
				{
					ItemTransactionItemEdMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTransactionItemEdMetadata.meta == null)
				{
					ItemTransactionItemEdMetadata.meta = new ItemTransactionItemEdMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityFinishInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemTransactionItemEd";
				meta.Destination = "ItemTransactionItemEd";
				meta.spInsert = "proc_ItemTransactionItemEdInsert";
				meta.spUpdate = "proc_ItemTransactionItemEdUpdate";
				meta.spDelete = "proc_ItemTransactionItemEdDelete";
				meta.spLoadAll = "proc_ItemTransactionItemEdLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTransactionItemEdLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTransactionItemEdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
