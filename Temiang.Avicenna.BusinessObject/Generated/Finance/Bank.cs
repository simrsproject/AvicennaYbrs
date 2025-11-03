/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/8/2022 9:05:57 PM
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
	abstract public class esBankCollection : esEntityCollectionWAuditLog
	{
		public esBankCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BankCollection";
		}

		#region Query Logic
		protected void InitQuery(esBankQuery query)
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
			this.InitQuery(query as esBankQuery);
		}
		#endregion

		virtual public Bank DetachEntity(Bank entity)
		{
			return base.DetachEntity(entity) as Bank;
		}

		virtual public Bank AttachEntity(Bank entity)
		{
			return base.AttachEntity(entity) as Bank;
		}

		virtual public void Combine(BankCollection collection)
		{
			base.Combine(collection);
		}

		new public Bank this[int index]
		{
			get
			{
				return base[index] as Bank;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Bank);
		}
	}

	[Serializable]
	abstract public class esBank : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBankQuery GetDynamicQuery()
		{
			return null;
		}

		public esBank()
		{
		}

		public esBank(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String bankID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bankID);
			else
				return LoadByPrimaryKeyStoredProcedure(bankID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bankID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bankID);
			else
				return LoadByPrimaryKeyStoredProcedure(bankID);
		}

		private bool LoadByPrimaryKeyDynamic(String bankID)
		{
			esBankQuery query = this.GetDynamicQuery();
			query.Where(query.BankID == bankID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String bankID)
		{
			esParameters parms = new esParameters();
			parms.Add("BankID", bankID);
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
						case "BankID": this.str.BankID = (string)value; break;
						case "BankName": this.str.BankName = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubledgerId": this.str.SubledgerId = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "NoRek": this.str.NoRek = (string)value; break;
						case "JournalCode": this.str.JournalCode = (string)value; break;
						case "CurrencyCode": this.str.CurrencyCode = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "IsToBeCleared": this.str.IsToBeCleared = (string)value; break;
						case "IsCrossRefference": this.str.IsCrossRefference = (string)value; break;
						case "IsCashierFrontOffice": this.str.IsCashierFrontOffice = (string)value; break;
						case "IsArPayment": this.str.IsArPayment = (string)value; break;
						case "IsApPayment": this.str.IsApPayment = (string)value; break;
						case "IsFeePayment": this.str.IsFeePayment = (string)value; break;
						case "IsBKU": this.str.IsBKU = (string)value; break;
						case "IsCashierFrontOfficeDpReturn": this.str.IsCashierFrontOfficeDpReturn = (string)value; break;
						case "IsAssetAuctionPayment": this.str.IsAssetAuctionPayment = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ChartOfAccountId":

							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubledgerId":

							if (value == null || value is System.Int32)
								this.SubledgerId = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "IsToBeCleared":

							if (value == null || value is System.Boolean)
								this.IsToBeCleared = (System.Boolean?)value;
							break;
						case "IsCrossRefference":

							if (value == null || value is System.Boolean)
								this.IsCrossRefference = (System.Boolean?)value;
							break;
						case "IsCashierFrontOffice":

							if (value == null || value is System.Boolean)
								this.IsCashierFrontOffice = (System.Boolean?)value;
							break;
						case "IsArPayment":

							if (value == null || value is System.Boolean)
								this.IsArPayment = (System.Boolean?)value;
							break;
						case "IsApPayment":

							if (value == null || value is System.Boolean)
								this.IsApPayment = (System.Boolean?)value;
							break;
						case "IsFeePayment":

							if (value == null || value is System.Boolean)
								this.IsFeePayment = (System.Boolean?)value;
							break;
						case "IsBKU":

							if (value == null || value is System.Boolean)
								this.IsBKU = (System.Boolean?)value;
							break;
						case "IsCashierFrontOfficeDpReturn":

							if (value == null || value is System.Boolean)
								this.IsCashierFrontOfficeDpReturn = (System.Boolean?)value;
							break;
						case "IsAssetAuctionPayment":

							if (value == null || value is System.Boolean)
								this.IsAssetAuctionPayment = (System.Boolean?)value;
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
		/// Maps to Bank.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(BankMetadata.ColumnNames.BankID);
			}

			set
			{
				base.SetSystemString(BankMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to Bank.BankName
		/// </summary>
		virtual public System.String BankName
		{
			get
			{
				return base.GetSystemString(BankMetadata.ColumnNames.BankName);
			}

			set
			{
				base.SetSystemString(BankMetadata.ColumnNames.BankName, value);
			}
		}
		/// <summary>
		/// Maps to Bank.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(BankMetadata.ColumnNames.ChartOfAccountId);
			}

			set
			{
				base.SetSystemInt32(BankMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to Bank.SubledgerId
		/// </summary>
		virtual public System.Int32? SubledgerId
		{
			get
			{
				return base.GetSystemInt32(BankMetadata.ColumnNames.SubledgerId);
			}

			set
			{
				base.SetSystemInt32(BankMetadata.ColumnNames.SubledgerId, value);
			}
		}
		/// <summary>
		/// Maps to Bank.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BankMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Bank.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BankMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BankMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Bank.NoRek
		/// </summary>
		virtual public System.String NoRek
		{
			get
			{
				return base.GetSystemString(BankMetadata.ColumnNames.NoRek);
			}

			set
			{
				base.SetSystemString(BankMetadata.ColumnNames.NoRek, value);
			}
		}
		/// <summary>
		/// Maps to Bank.JournalCode
		/// </summary>
		virtual public System.String JournalCode
		{
			get
			{
				return base.GetSystemString(BankMetadata.ColumnNames.JournalCode);
			}

			set
			{
				base.SetSystemString(BankMetadata.ColumnNames.JournalCode, value);
			}
		}
		/// <summary>
		/// Maps to Bank.CurrencyCode
		/// </summary>
		virtual public System.String CurrencyCode
		{
			get
			{
				return base.GetSystemString(BankMetadata.ColumnNames.CurrencyCode);
			}

			set
			{
				base.SetSystemString(BankMetadata.ColumnNames.CurrencyCode, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsToBeCleared
		/// </summary>
		virtual public System.Boolean? IsToBeCleared
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsToBeCleared);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsToBeCleared, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsCrossRefference
		/// </summary>
		virtual public System.Boolean? IsCrossRefference
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsCrossRefference);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsCrossRefference, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsCashierFrontOffice
		/// </summary>
		virtual public System.Boolean? IsCashierFrontOffice
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsCashierFrontOffice);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsCashierFrontOffice, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsArPayment
		/// </summary>
		virtual public System.Boolean? IsArPayment
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsArPayment);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsArPayment, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsApPayment
		/// </summary>
		virtual public System.Boolean? IsApPayment
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsApPayment);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsApPayment, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsFeePayment
		/// </summary>
		virtual public System.Boolean? IsFeePayment
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsFeePayment);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsFeePayment, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsBKU
		/// </summary>
		virtual public System.Boolean? IsBKU
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsBKU);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsBKU, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsCashierFrontOfficeDpReturn
		/// </summary>
		virtual public System.Boolean? IsCashierFrontOfficeDpReturn
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsCashierFrontOfficeDpReturn);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsCashierFrontOfficeDpReturn, value);
			}
		}
		/// <summary>
		/// Maps to Bank.IsAssetAuctionPayment
		/// </summary>
		virtual public System.Boolean? IsAssetAuctionPayment
		{
			get
			{
				return base.GetSystemBoolean(BankMetadata.ColumnNames.IsAssetAuctionPayment);
			}

			set
			{
				base.SetSystemBoolean(BankMetadata.ColumnNames.IsAssetAuctionPayment, value);
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
			public esStrings(esBank entity)
			{
				this.entity = entity;
			}
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
			public System.String BankName
			{
				get
				{
					System.String data = entity.BankName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankName = null;
					else entity.BankName = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerId
			{
				get
				{
					System.Int32? data = entity.SubledgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerId = null;
					else entity.SubledgerId = Convert.ToInt32(value);
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
			public System.String NoRek
			{
				get
				{
					System.String data = entity.NoRek;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoRek = null;
					else entity.NoRek = Convert.ToString(value);
				}
			}
			public System.String JournalCode
			{
				get
				{
					System.String data = entity.JournalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalCode = null;
					else entity.JournalCode = Convert.ToString(value);
				}
			}
			public System.String CurrencyCode
			{
				get
				{
					System.String data = entity.CurrencyCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrencyCode = null;
					else entity.CurrencyCode = Convert.ToString(value);
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
			public System.String IsToBeCleared
			{
				get
				{
					System.Boolean? data = entity.IsToBeCleared;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsToBeCleared = null;
					else entity.IsToBeCleared = Convert.ToBoolean(value);
				}
			}
			public System.String IsCrossRefference
			{
				get
				{
					System.Boolean? data = entity.IsCrossRefference;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCrossRefference = null;
					else entity.IsCrossRefference = Convert.ToBoolean(value);
				}
			}
			public System.String IsCashierFrontOffice
			{
				get
				{
					System.Boolean? data = entity.IsCashierFrontOffice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCashierFrontOffice = null;
					else entity.IsCashierFrontOffice = Convert.ToBoolean(value);
				}
			}
			public System.String IsArPayment
			{
				get
				{
					System.Boolean? data = entity.IsArPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsArPayment = null;
					else entity.IsArPayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsApPayment
			{
				get
				{
					System.Boolean? data = entity.IsApPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApPayment = null;
					else entity.IsApPayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeePayment
			{
				get
				{
					System.Boolean? data = entity.IsFeePayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeePayment = null;
					else entity.IsFeePayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsBKU
			{
				get
				{
					System.Boolean? data = entity.IsBKU;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBKU = null;
					else entity.IsBKU = Convert.ToBoolean(value);
				}
			}
			public System.String IsCashierFrontOfficeDpReturn
			{
				get
				{
					System.Boolean? data = entity.IsCashierFrontOfficeDpReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCashierFrontOfficeDpReturn = null;
					else entity.IsCashierFrontOfficeDpReturn = Convert.ToBoolean(value);
				}
			}
			public System.String IsAssetAuctionPayment
			{
				get
				{
					System.Boolean? data = entity.IsAssetAuctionPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAssetAuctionPayment = null;
					else entity.IsAssetAuctionPayment = Convert.ToBoolean(value);
				}
			}
			private esBank entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBankQuery query)
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
				throw new Exception("esBank can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Bank : esBank
	{
	}

	[Serializable]
	abstract public class esBankQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BankMetadata.Meta();
			}
		}

		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.BankID, esSystemType.String);
			}
		}

		public esQueryItem BankName
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.BankName, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerId
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.SubledgerId, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem NoRek
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.NoRek, esSystemType.String);
			}
		}

		public esQueryItem JournalCode
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.JournalCode, esSystemType.String);
			}
		}

		public esQueryItem CurrencyCode
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.CurrencyCode, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem IsToBeCleared
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsToBeCleared, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCrossRefference
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsCrossRefference, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCashierFrontOffice
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsCashierFrontOffice, esSystemType.Boolean);
			}
		}

		public esQueryItem IsArPayment
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsArPayment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsApPayment
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsApPayment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeePayment
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsFeePayment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBKU
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsBKU, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCashierFrontOfficeDpReturn
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsCashierFrontOfficeDpReturn, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAssetAuctionPayment
		{
			get
			{
				return new esQueryItem(this, BankMetadata.ColumnNames.IsAssetAuctionPayment, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BankCollection")]
	public partial class BankCollection : esBankCollection, IEnumerable<Bank>
	{
		public BankCollection()
		{

		}

		public static implicit operator List<Bank>(BankCollection coll)
		{
			List<Bank> list = new List<Bank>();

			foreach (Bank emp in coll)
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
				return BankMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Bank(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Bank();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BankQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankQuery();
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
		public bool Load(BankQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Bank AddNew()
		{
			Bank entity = base.AddNewEntity() as Bank;

			return entity;
		}
		public Bank FindByPrimaryKey(String bankID)
		{
			return base.FindByPrimaryKey(bankID) as Bank;
		}

		#region IEnumerable< Bank> Members

		IEnumerator<Bank> IEnumerable<Bank>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Bank;
			}
		}

		#endregion

		private BankQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Bank' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Bank ({BankID})")]
	[Serializable]
	public partial class Bank : esBank
	{
		public Bank()
		{
		}

		public Bank(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BankMetadata.Meta();
			}
		}

		override protected esBankQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BankQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankQuery();
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
		public bool Load(BankQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BankQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BankQuery : esBankQuery
	{
		public BankQuery()
		{

		}

		public BankQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BankQuery";
		}
	}

	[Serializable]
	public partial class BankMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BankMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BankMetadata.ColumnNames.BankID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BankMetadata.PropertyNames.BankID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.BankName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BankMetadata.PropertyNames.BankName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.SubledgerId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BankMetadata.PropertyNames.SubledgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BankMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.NoRek, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BankMetadata.PropertyNames.NoRek;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.JournalCode, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BankMetadata.PropertyNames.JournalCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.CurrencyCode, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BankMetadata.PropertyNames.CurrencyCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsActive, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsToBeCleared, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsToBeCleared;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsCrossRefference, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsCrossRefference;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsCashierFrontOffice, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsCashierFrontOffice;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsArPayment, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsArPayment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsApPayment, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsApPayment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsFeePayment, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsFeePayment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsBKU, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsBKU;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsCashierFrontOfficeDpReturn, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsCashierFrontOfficeDpReturn;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BankMetadata.ColumnNames.IsAssetAuctionPayment, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankMetadata.PropertyNames.IsAssetAuctionPayment;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BankMetadata Meta()
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
			public const string BankID = "BankID";
			public const string BankName = "BankName";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerId = "SubledgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string NoRek = "NoRek";
			public const string JournalCode = "JournalCode";
			public const string CurrencyCode = "CurrencyCode";
			public const string IsActive = "IsActive";
			public const string IsToBeCleared = "IsToBeCleared";
			public const string IsCrossRefference = "IsCrossRefference";
			public const string IsCashierFrontOffice = "IsCashierFrontOffice";
			public const string IsArPayment = "IsArPayment";
			public const string IsApPayment = "IsApPayment";
			public const string IsFeePayment = "IsFeePayment";
			public const string IsBKU = "IsBKU";
			public const string IsCashierFrontOfficeDpReturn = "IsCashierFrontOfficeDpReturn";
			public const string IsAssetAuctionPayment = "IsAssetAuctionPayment";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string BankID = "BankID";
			public const string BankName = "BankName";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerId = "SubledgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string NoRek = "NoRek";
			public const string JournalCode = "JournalCode";
			public const string CurrencyCode = "CurrencyCode";
			public const string IsActive = "IsActive";
			public const string IsToBeCleared = "IsToBeCleared";
			public const string IsCrossRefference = "IsCrossRefference";
			public const string IsCashierFrontOffice = "IsCashierFrontOffice";
			public const string IsArPayment = "IsArPayment";
			public const string IsApPayment = "IsApPayment";
			public const string IsFeePayment = "IsFeePayment";
			public const string IsBKU = "IsBKU";
			public const string IsCashierFrontOfficeDpReturn = "IsCashierFrontOfficeDpReturn";
			public const string IsAssetAuctionPayment = "IsAssetAuctionPayment";
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
			lock (typeof(BankMetadata))
			{
				if (BankMetadata.mapDelegates == null)
				{
					BankMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BankMetadata.meta == null)
				{
					BankMetadata.meta = new BankMetadata();
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

				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoRek", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JournalCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CurrencyCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsToBeCleared", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCrossRefference", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCashierFrontOffice", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsArPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeePayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBKU", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCashierFrontOfficeDpReturn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAssetAuctionPayment", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "Bank";
				meta.Destination = "Bank";
				meta.spInsert = "proc_BankInsert";
				meta.spUpdate = "proc_BankUpdate";
				meta.spDelete = "proc_BankDelete";
				meta.spLoadAll = "proc_BankLoadAll";
				meta.spLoadByPrimaryKey = "proc_BankLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BankMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
