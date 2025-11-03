/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/25/2022 3:42:00 PM
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
	abstract public class esItemTariffCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTariffCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffQuery query)
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
			this.InitQuery(query as esItemTariffQuery);
		}
		#endregion

		virtual public ItemTariff DetachEntity(ItemTariff entity)
		{
			return base.DetachEntity(entity) as ItemTariff;
		}

		virtual public ItemTariff AttachEntity(ItemTariff entity)
		{
			return base.AttachEntity(entity) as ItemTariff;
		}

		virtual public void Combine(ItemTariffCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemTariff this[int index]
		{
			get
			{
				return base[index] as ItemTariff;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariff);
		}
	}

	[Serializable]
	abstract public class esItemTariff : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariff()
		{
		}

		public esItemTariff(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRTariffType, String itemID, String classID, DateTime startingDate)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTariffType, itemID, classID, startingDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTariffType, itemID, classID, startingDate);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRTariffType, String itemID, String classID, DateTime startingDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTariffType, itemID, classID, startingDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTariffType, itemID, classID, startingDate);
		}

		private bool LoadByPrimaryKeyDynamic(String sRTariffType, String itemID, String classID, DateTime startingDate)
		{
			esItemTariffQuery query = this.GetDynamicQuery();
			query.Where(query.SRTariffType == sRTariffType, query.ItemID == itemID, query.ClassID == classID, query.StartingDate == startingDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRTariffType, String itemID, String classID, DateTime startingDate)
		{
			esParameters parms = new esParameters();
			parms.Add("SRTariffType", sRTariffType);
			parms.Add("ItemID", itemID);
			parms.Add("ClassID", classID);
			parms.Add("StartingDate", startingDate);
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
						case "SRTariffType": this.str.SRTariffType = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "StartingDate": this.str.StartingDate = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "IsAdminCalculation": this.str.IsAdminCalculation = (string)value; break;
						case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;
						case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;
						case "IsAllowCito": this.str.IsAllowCito = (string)value; break;
						case "IsCitoInPercent": this.str.IsCitoInPercent = (string)value; break;
						case "CitoValue": this.str.CitoValue = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ReferenceTransactionCode": this.str.ReferenceTransactionCode = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DiscPercentage": this.str.DiscPercentage = (string)value; break;
						case "IsCitoFromStandardReference": this.str.IsCitoFromStandardReference = (string)value; break;
						case "Ppn": this.str.Ppn = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "StartingDate":

							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "IsAdminCalculation":

							if (value == null || value is System.Boolean)
								this.IsAdminCalculation = (System.Boolean?)value;
							break;
						case "IsAllowDiscount":

							if (value == null || value is System.Boolean)
								this.IsAllowDiscount = (System.Boolean?)value;
							break;
						case "IsAllowVariable":

							if (value == null || value is System.Boolean)
								this.IsAllowVariable = (System.Boolean?)value;
							break;
						case "IsAllowCito":

							if (value == null || value is System.Boolean)
								this.IsAllowCito = (System.Boolean?)value;
							break;
						case "IsCitoInPercent":

							if (value == null || value is System.Boolean)
								this.IsCitoInPercent = (System.Boolean?)value;
							break;
						case "CitoValue":

							if (value == null || value is System.Decimal)
								this.CitoValue = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "DiscPercentage":

							if (value == null || value is System.Decimal)
								this.DiscPercentage = (System.Decimal?)value;
							break;
						case "IsCitoFromStandardReference":

							if (value == null || value is System.Boolean)
								this.IsCitoFromStandardReference = (System.Boolean?)value;
							break;
						case "Ppn":

							if (value == null || value is System.Decimal)
								this.Ppn = (System.Decimal?)value;
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
		/// Maps to ItemTariff.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffMetadata.ColumnNames.SRTariffType);
			}

			set
			{
				base.SetSystemString(ItemTariffMetadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTariffMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemTariffMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemTariffMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(ItemTariffMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffMetadata.ColumnNames.StartingDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffMetadata.ColumnNames.StartingDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.IsAdminCalculation
		/// </summary>
		virtual public System.Boolean? IsAdminCalculation
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAdminCalculation);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAdminCalculation, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.IsAllowDiscount
		/// </summary>
		virtual public System.Boolean? IsAllowDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAllowDiscount);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAllowDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.IsAllowVariable
		/// </summary>
		virtual public System.Boolean? IsAllowVariable
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAllowVariable);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAllowVariable, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.IsAllowCito
		/// </summary>
		virtual public System.Boolean? IsAllowCito
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAllowCito);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffMetadata.ColumnNames.IsAllowCito, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.IsCitoInPercent
		/// </summary>
		virtual public System.Boolean? IsCitoInPercent
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffMetadata.ColumnNames.IsCitoInPercent);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffMetadata.ColumnNames.IsCitoInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.CitoValue
		/// </summary>
		virtual public System.Decimal? CitoValue
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffMetadata.ColumnNames.CitoValue);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffMetadata.ColumnNames.CitoValue, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTariffMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(ItemTariffMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.ReferenceTransactionCode
		/// </summary>
		virtual public System.String ReferenceTransactionCode
		{
			get
			{
				return base.GetSystemString(ItemTariffMetadata.ColumnNames.ReferenceTransactionCode);
			}

			set
			{
				base.SetSystemString(ItemTariffMetadata.ColumnNames.ReferenceTransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.DiscPercentage
		/// </summary>
		virtual public System.Decimal? DiscPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffMetadata.ColumnNames.DiscPercentage);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffMetadata.ColumnNames.DiscPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.IsCitoFromStandardReference
		/// </summary>
		virtual public System.Boolean? IsCitoFromStandardReference
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffMetadata.ColumnNames.IsCitoFromStandardReference);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffMetadata.ColumnNames.IsCitoFromStandardReference, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariff.Ppn
		/// </summary>
		virtual public System.Decimal? Ppn
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffMetadata.ColumnNames.Ppn);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffMetadata.ColumnNames.Ppn, value);
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
			public esStrings(esItemTariff entity)
			{
				this.entity = entity;
			}
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
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
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
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
			public System.String IsAdminCalculation
			{
				get
				{
					System.Boolean? data = entity.IsAdminCalculation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAdminCalculation = null;
					else entity.IsAdminCalculation = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowDiscount
			{
				get
				{
					System.Boolean? data = entity.IsAllowDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowDiscount = null;
					else entity.IsAllowDiscount = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowVariable
			{
				get
				{
					System.Boolean? data = entity.IsAllowVariable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowVariable = null;
					else entity.IsAllowVariable = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowCito
			{
				get
				{
					System.Boolean? data = entity.IsAllowCito;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowCito = null;
					else entity.IsAllowCito = Convert.ToBoolean(value);
				}
			}
			public System.String IsCitoInPercent
			{
				get
				{
					System.Boolean? data = entity.IsCitoInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCitoInPercent = null;
					else entity.IsCitoInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String CitoValue
			{
				get
				{
					System.Decimal? data = entity.CitoValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CitoValue = null;
					else entity.CitoValue = Convert.ToDecimal(value);
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
			public System.String ReferenceTransactionCode
			{
				get
				{
					System.String data = entity.ReferenceTransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceTransactionCode = null;
					else entity.ReferenceTransactionCode = Convert.ToString(value);
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
			public System.String DiscPercentage
			{
				get
				{
					System.Decimal? data = entity.DiscPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscPercentage = null;
					else entity.DiscPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsCitoFromStandardReference
			{
				get
				{
					System.Boolean? data = entity.IsCitoFromStandardReference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCitoFromStandardReference = null;
					else entity.IsCitoFromStandardReference = Convert.ToBoolean(value);
				}
			}
			public System.String Ppn
			{
				get
				{
					System.Decimal? data = entity.Ppn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ppn = null;
					else entity.Ppn = Convert.ToDecimal(value);
				}
			}
			private esItemTariff entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffQuery query)
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
				throw new Exception("esItemTariff can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTariff : esItemTariff
	{
	}

	[Serializable]
	abstract public class esItemTariffQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffMetadata.Meta();
			}
		}

		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem IsAdminCalculation
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowVariable
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowCito
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCitoInPercent
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.IsCitoInPercent, esSystemType.Boolean);
			}
		}

		public esQueryItem CitoValue
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.CitoValue, esSystemType.Decimal);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem ReferenceTransactionCode
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.ReferenceTransactionCode, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem DiscPercentage
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.DiscPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsCitoFromStandardReference
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.IsCitoFromStandardReference, esSystemType.Boolean);
			}
		}

		public esQueryItem Ppn
		{
			get
			{
				return new esQueryItem(this, ItemTariffMetadata.ColumnNames.Ppn, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffCollection")]
	public partial class ItemTariffCollection : esItemTariffCollection, IEnumerable<ItemTariff>
	{
		public ItemTariffCollection()
		{

		}

		public static implicit operator List<ItemTariff>(ItemTariffCollection coll)
		{
			List<ItemTariff> list = new List<ItemTariff>();

			foreach (ItemTariff emp in coll)
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
				return ItemTariffMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariff(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariff();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffQuery();
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
		public bool Load(ItemTariffQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTariff AddNew()
		{
			ItemTariff entity = base.AddNewEntity() as ItemTariff;

			return entity;
		}
		public ItemTariff FindByPrimaryKey(String sRTariffType, String itemID, String classID, DateTime startingDate)
		{
			return base.FindByPrimaryKey(sRTariffType, itemID, classID, startingDate) as ItemTariff;
		}

		#region IEnumerable< ItemTariff> Members

		IEnumerator<ItemTariff> IEnumerable<ItemTariff>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariff;
			}
		}

		#endregion

		private ItemTariffQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariff' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTariff ({SRTariffType, ItemID, ClassID, StartingDate})")]
	[Serializable]
	public partial class ItemTariff : esItemTariff
	{
		public ItemTariff()
		{
		}

		public ItemTariff(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffMetadata.Meta();
			}
		}

		override protected esItemTariffQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffQuery();
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
		public bool Load(ItemTariffQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTariffQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTariffQuery : esItemTariffQuery
	{
		public ItemTariffQuery()
		{

		}

		public ItemTariffQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTariffQuery";
		}
	}

	[Serializable]
	public partial class ItemTariffMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.SRTariffType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffMetadata.PropertyNames.SRTariffType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.StartingDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffMetadata.PropertyNames.StartingDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.Price, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.IsAdminCalculation, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffMetadata.PropertyNames.IsAdminCalculation;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.IsAllowDiscount, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffMetadata.PropertyNames.IsAllowDiscount;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.IsAllowVariable, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffMetadata.PropertyNames.IsAllowVariable;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.IsAllowCito, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffMetadata.PropertyNames.IsAllowCito;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.IsCitoInPercent, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffMetadata.PropertyNames.IsCitoInPercent;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.CitoValue, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffMetadata.PropertyNames.CitoValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.ReferenceNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.ReferenceTransactionCode, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffMetadata.PropertyNames.ReferenceTransactionCode;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.DiscPercentage, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffMetadata.PropertyNames.DiscPercentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.IsCitoFromStandardReference, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffMetadata.PropertyNames.IsCitoFromStandardReference;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffMetadata.ColumnNames.Ppn, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffMetadata.PropertyNames.Ppn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTariffMetadata Meta()
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
			public const string SRTariffType = "SRTariffType";
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string StartingDate = "StartingDate";
			public const string Price = "Price";
			public const string IsAdminCalculation = "IsAdminCalculation";
			public const string IsAllowDiscount = "IsAllowDiscount";
			public const string IsAllowVariable = "IsAllowVariable";
			public const string IsAllowCito = "IsAllowCito";
			public const string IsCitoInPercent = "IsCitoInPercent";
			public const string CitoValue = "CitoValue";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceTransactionCode = "ReferenceTransactionCode";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DiscPercentage = "DiscPercentage";
			public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
			public const string Ppn = "Ppn";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRTariffType = "SRTariffType";
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string StartingDate = "StartingDate";
			public const string Price = "Price";
			public const string IsAdminCalculation = "IsAdminCalculation";
			public const string IsAllowDiscount = "IsAllowDiscount";
			public const string IsAllowVariable = "IsAllowVariable";
			public const string IsAllowCito = "IsAllowCito";
			public const string IsCitoInPercent = "IsCitoInPercent";
			public const string CitoValue = "CitoValue";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceTransactionCode = "ReferenceTransactionCode";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DiscPercentage = "DiscPercentage";
			public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
			public const string Ppn = "Ppn";
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
			lock (typeof(ItemTariffMetadata))
			{
				if (ItemTariffMetadata.mapDelegates == null)
				{
					ItemTariffMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTariffMetadata.meta == null)
				{
					ItemTariffMetadata.meta = new ItemTariffMetadata();
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

				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowCito", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCitoInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CitoValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceTransactionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiscPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsCitoFromStandardReference", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Ppn", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "ItemTariff";
				meta.Destination = "ItemTariff";
				meta.spInsert = "proc_ItemTariffInsert";
				meta.spUpdate = "proc_ItemTariffUpdate";
				meta.spDelete = "proc_ItemTariffDelete";
				meta.spLoadAll = "proc_ItemTariffLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
