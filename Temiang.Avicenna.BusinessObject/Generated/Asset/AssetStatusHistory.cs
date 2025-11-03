/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/7/2022 10:42:45 AM
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
	abstract public class esAssetStatusHistoryCollection : esEntityCollectionWAuditLog
	{
		public esAssetStatusHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AssetStatusHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetStatusHistoryQuery query)
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
			this.InitQuery(query as esAssetStatusHistoryQuery);
		}
		#endregion

		virtual public AssetStatusHistory DetachEntity(AssetStatusHistory entity)
		{
			return base.DetachEntity(entity) as AssetStatusHistory;
		}

		virtual public AssetStatusHistory AttachEntity(AssetStatusHistory entity)
		{
			return base.AttachEntity(entity) as AssetStatusHistory;
		}

		virtual public void Combine(AssetStatusHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public AssetStatusHistory this[int index]
		{
			get
			{
				return base[index] as AssetStatusHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetStatusHistory);
		}
	}

	[Serializable]
	abstract public class esAssetStatusHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetStatusHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetStatusHistory()
		{
		}

		public esAssetStatusHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 seqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 seqNo)
		{
			esAssetStatusHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 seqNo)
		{
			esParameters parms = new esParameters();
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
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "AssetID": this.str.AssetID = (string)value; break;
						case "SRAssetsStatusFrom": this.str.SRAssetsStatusFrom = (string)value; break;
						case "SRAssetsStatusTo": this.str.SRAssetsStatusTo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsFixedAssetFrom": this.str.IsFixedAssetFrom = (string)value; break;
						case "IsFixedAssetTo": this.str.IsFixedAssetTo = (string)value; break;
						case "CurrentValue": this.str.CurrentValue = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "BuyersName": this.str.BuyersName = (string)value; break;
						case "BuyersAddress": this.str.BuyersAddress = (string)value; break;
						case "BuyersPhoneNo": this.str.BuyersPhoneNo = (string)value; break;
						case "BuyersTaxRegister": this.str.BuyersTaxRegister = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "DepreciationAccValue": this.str.DepreciationAccValue = (string)value; break;
						case "SalesPrice": this.str.SalesPrice = (string)value; break;
						case "TaxStatus": this.str.TaxStatus = (string)value; break;
						case "TaxPercentage": this.str.TaxPercentage = (string)value; break;
						case "TaxAmount": this.str.TaxAmount = (string)value; break;
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
						case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SeqNo":

							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsFixedAssetFrom":

							if (value == null || value is System.Boolean)
								this.IsFixedAssetFrom = (System.Boolean?)value;
							break;
						case "IsFixedAssetTo":

							if (value == null || value is System.Boolean)
								this.IsFixedAssetTo = (System.Boolean?)value;
							break;
						case "CurrentValue":

							if (value == null || value is System.Decimal)
								this.CurrentValue = (System.Decimal?)value;
							break;
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
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
						case "DepreciationAccValue":

							if (value == null || value is System.Decimal)
								this.DepreciationAccValue = (System.Decimal?)value;
							break;
						case "SalesPrice":

							if (value == null || value is System.Decimal)
								this.SalesPrice = (System.Decimal?)value;
							break;
						case "TaxPercentage":

							if (value == null || value is System.Decimal)
								this.TaxPercentage = (System.Decimal?)value;
							break;
						case "TaxAmount":

							if (value == null || value is System.Decimal)
								this.TaxAmount = (System.Decimal?)value;
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
		/// Maps to AssetStatusHistory.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(AssetStatusHistoryMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemInt32(AssetStatusHistoryMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.AssetID);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.AssetID, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.SRAssetsStatusFrom
		/// </summary>
		virtual public System.String SRAssetsStatusFrom
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusFrom);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusFrom, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.SRAssetsStatusTo
		/// </summary>
		virtual public System.String SRAssetsStatusTo
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusTo);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusTo, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.IsFixedAssetFrom
		/// </summary>
		virtual public System.Boolean? IsFixedAssetFrom
		{
			get
			{
				return base.GetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetFrom);
			}

			set
			{
				base.SetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetFrom, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.IsFixedAssetTo
		/// </summary>
		virtual public System.Boolean? IsFixedAssetTo
		{
			get
			{
				return base.GetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetTo);
			}

			set
			{
				base.SetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetTo, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.CurrentValue
		/// </summary>
		virtual public System.Decimal? CurrentValue
		{
			get
			{
				return base.GetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.CurrentValue);
			}

			set
			{
				base.SetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.CurrentValue, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.BuyersName
		/// </summary>
		virtual public System.String BuyersName
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersName);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersName, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.BuyersAddress
		/// </summary>
		virtual public System.String BuyersAddress
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersAddress);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersAddress, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.BuyersPhoneNo
		/// </summary>
		virtual public System.String BuyersPhoneNo
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersPhoneNo);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersPhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.BuyersTaxRegister
		/// </summary>
		virtual public System.String BuyersTaxRegister
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersTaxRegister);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.BuyersTaxRegister, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(AssetStatusHistoryMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(AssetStatusHistoryMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.DepreciationAccValue
		/// </summary>
		virtual public System.Decimal? DepreciationAccValue
		{
			get
			{
				return base.GetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.DepreciationAccValue);
			}

			set
			{
				base.SetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.DepreciationAccValue, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.SalesPrice
		/// </summary>
		virtual public System.Decimal? SalesPrice
		{
			get
			{
				return base.GetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.SalesPrice);
			}

			set
			{
				base.SetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.SalesPrice, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.TaxStatus
		/// </summary>
		virtual public System.String TaxStatus
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.TaxStatus);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.TaxStatus, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.TaxPercentage
		/// </summary>
		virtual public System.Decimal? TaxPercentage
		{
			get
			{
				return base.GetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.TaxPercentage);
			}

			set
			{
				base.SetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.TaxPercentage, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.TaxAmount
		/// </summary>
		virtual public System.Decimal? TaxAmount
		{
			get
			{
				return base.GetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.TaxAmount);
			}

			set
			{
				base.SetSystemDecimal(AssetStatusHistoryMetadata.ColumnNames.TaxAmount, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRPaymentType);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.SRPaymentMethod
		/// </summary>
		virtual public System.String SRPaymentMethod
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRPaymentMethod);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.SRPaymentMethod, value);
			}
		}
		/// <summary>
		/// Maps to AssetStatusHistory.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(AssetStatusHistoryMetadata.ColumnNames.BankID);
			}

			set
			{
				base.SetSystemString(AssetStatusHistoryMetadata.ColumnNames.BankID, value);
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
			public esStrings(esAssetStatusHistory entity)
			{
				this.entity = entity;
			}
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
				}
			}
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
				}
			}
			public System.String SRAssetsStatusFrom
			{
				get
				{
					System.String data = entity.SRAssetsStatusFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetsStatusFrom = null;
					else entity.SRAssetsStatusFrom = Convert.ToString(value);
				}
			}
			public System.String SRAssetsStatusTo
			{
				get
				{
					System.String data = entity.SRAssetsStatusTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssetsStatusTo = null;
					else entity.SRAssetsStatusTo = Convert.ToString(value);
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
			public System.String IsFixedAssetFrom
			{
				get
				{
					System.Boolean? data = entity.IsFixedAssetFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFixedAssetFrom = null;
					else entity.IsFixedAssetFrom = Convert.ToBoolean(value);
				}
			}
			public System.String IsFixedAssetTo
			{
				get
				{
					System.Boolean? data = entity.IsFixedAssetTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFixedAssetTo = null;
					else entity.IsFixedAssetTo = Convert.ToBoolean(value);
				}
			}
			public System.String CurrentValue
			{
				get
				{
					System.Decimal? data = entity.CurrentValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentValue = null;
					else entity.CurrentValue = Convert.ToDecimal(value);
				}
			}
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String BuyersName
			{
				get
				{
					System.String data = entity.BuyersName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BuyersName = null;
					else entity.BuyersName = Convert.ToString(value);
				}
			}
			public System.String BuyersAddress
			{
				get
				{
					System.String data = entity.BuyersAddress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BuyersAddress = null;
					else entity.BuyersAddress = Convert.ToString(value);
				}
			}
			public System.String BuyersPhoneNo
			{
				get
				{
					System.String data = entity.BuyersPhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BuyersPhoneNo = null;
					else entity.BuyersPhoneNo = Convert.ToString(value);
				}
			}
			public System.String BuyersTaxRegister
			{
				get
				{
					System.String data = entity.BuyersTaxRegister;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BuyersTaxRegister = null;
					else entity.BuyersTaxRegister = Convert.ToString(value);
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
			public System.String DepreciationAccValue
			{
				get
				{
					System.Decimal? data = entity.DepreciationAccValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepreciationAccValue = null;
					else entity.DepreciationAccValue = Convert.ToDecimal(value);
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
			public System.String TaxStatus
			{
				get
				{
					System.String data = entity.TaxStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxStatus = null;
					else entity.TaxStatus = Convert.ToString(value);
				}
			}
			public System.String TaxPercentage
			{
				get
				{
					System.Decimal? data = entity.TaxPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxPercentage = null;
					else entity.TaxPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String TaxAmount
			{
				get
				{
					System.Decimal? data = entity.TaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxAmount = null;
					else entity.TaxAmount = Convert.ToDecimal(value);
				}
			}
			public System.String SRPaymentType
			{
				get
				{
					System.String data = entity.SRPaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentType = null;
					else entity.SRPaymentType = Convert.ToString(value);
				}
			}
			public System.String SRPaymentMethod
			{
				get
				{
					System.String data = entity.SRPaymentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentMethod = null;
					else entity.SRPaymentMethod = Convert.ToString(value);
				}
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
			private esAssetStatusHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetStatusHistoryQuery query)
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
				throw new Exception("esAssetStatusHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AssetStatusHistory : esAssetStatusHistory
	{
	}

	[Serializable]
	abstract public class esAssetStatusHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AssetStatusHistoryMetadata.Meta();
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		}

		public esQueryItem SRAssetsStatusFrom
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusFrom, esSystemType.String);
			}
		}

		public esQueryItem SRAssetsStatusTo
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusTo, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsFixedAssetFrom
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetFrom, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFixedAssetTo
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetTo, esSystemType.Boolean);
			}
		}

		public esQueryItem CurrentValue
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.CurrentValue, esSystemType.Decimal);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem BuyersName
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.BuyersName, esSystemType.String);
			}
		}

		public esQueryItem BuyersAddress
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.BuyersAddress, esSystemType.String);
			}
		}

		public esQueryItem BuyersPhoneNo
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.BuyersPhoneNo, esSystemType.String);
			}
		}

		public esQueryItem BuyersTaxRegister
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.BuyersTaxRegister, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem DepreciationAccValue
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.DepreciationAccValue, esSystemType.Decimal);
			}
		}

		public esQueryItem SalesPrice
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.SalesPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem TaxStatus
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.TaxStatus, esSystemType.String);
			}
		}

		public esQueryItem TaxPercentage
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.TaxPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem TaxAmount
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.TaxAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		}

		public esQueryItem SRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
			}
		}

		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, AssetStatusHistoryMetadata.ColumnNames.BankID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetStatusHistoryCollection")]
	public partial class AssetStatusHistoryCollection : esAssetStatusHistoryCollection, IEnumerable<AssetStatusHistory>
	{
		public AssetStatusHistoryCollection()
		{

		}

		public static implicit operator List<AssetStatusHistory>(AssetStatusHistoryCollection coll)
		{
			List<AssetStatusHistory> list = new List<AssetStatusHistory>();

			foreach (AssetStatusHistory emp in coll)
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
				return AssetStatusHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetStatusHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetStatusHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetStatusHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AssetStatusHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetStatusHistoryQuery();
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
		public bool Load(AssetStatusHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AssetStatusHistory AddNew()
		{
			AssetStatusHistory entity = base.AddNewEntity() as AssetStatusHistory;

			return entity;
		}
		public AssetStatusHistory FindByPrimaryKey(Int32 seqNo)
		{
			return base.FindByPrimaryKey(seqNo) as AssetStatusHistory;
		}

		#region IEnumerable< AssetStatusHistory> Members

		IEnumerator<AssetStatusHistory> IEnumerable<AssetStatusHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AssetStatusHistory;
			}
		}

		#endregion

		private AssetStatusHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetStatusHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AssetStatusHistory ({SeqNo})")]
	[Serializable]
	public partial class AssetStatusHistory : esAssetStatusHistory
	{
		public AssetStatusHistory()
		{
		}

		public AssetStatusHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetStatusHistoryMetadata.Meta();
			}
		}

		override protected esAssetStatusHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetStatusHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AssetStatusHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetStatusHistoryQuery();
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
		public bool Load(AssetStatusHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AssetStatusHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AssetStatusHistoryQuery : esAssetStatusHistoryQuery
	{
		public AssetStatusHistoryQuery()
		{

		}

		public AssetStatusHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AssetStatusHistoryQuery";
		}
	}

	[Serializable]
	public partial class AssetStatusHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetStatusHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.SeqNo, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.AssetID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusFrom, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.SRAssetsStatusFrom;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.SRAssetsStatusTo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.SRAssetsStatusTo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetFrom, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.IsFixedAssetFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.IsFixedAssetTo, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.IsFixedAssetTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.CurrentValue, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.CurrentValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.TransactionDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.TransactionDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.BuyersName, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.BuyersName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.BuyersAddress, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.BuyersAddress;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.BuyersPhoneNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.BuyersPhoneNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.BuyersTaxRegister, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.BuyersTaxRegister;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.IsApproved, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.ApprovedDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.ApprovedByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.IsVoid, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.VoidDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.VoidByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.DepreciationAccValue, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.DepreciationAccValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.SalesPrice, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.SalesPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.TaxStatus, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.TaxStatus;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.TaxPercentage, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.TaxPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.TaxAmount, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.TaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.SRPaymentType, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.SRPaymentMethod, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.SRPaymentMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AssetStatusHistoryMetadata.ColumnNames.BankID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetStatusHistoryMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AssetStatusHistoryMetadata Meta()
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
			public const string SeqNo = "SeqNo";
			public const string AssetID = "AssetID";
			public const string SRAssetsStatusFrom = "SRAssetsStatusFrom";
			public const string SRAssetsStatusTo = "SRAssetsStatusTo";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsFixedAssetFrom = "IsFixedAssetFrom";
			public const string IsFixedAssetTo = "IsFixedAssetTo";
			public const string CurrentValue = "CurrentValue";
			public const string TransactionDate = "TransactionDate";
			public const string BuyersName = "BuyersName";
			public const string BuyersAddress = "BuyersAddress";
			public const string BuyersPhoneNo = "BuyersPhoneNo";
			public const string BuyersTaxRegister = "BuyersTaxRegister";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string DepreciationAccValue = "DepreciationAccValue";
			public const string SalesPrice = "SalesPrice";
			public const string TaxStatus = "TaxStatus";
			public const string TaxPercentage = "TaxPercentage";
			public const string TaxAmount = "TaxAmount";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string BankID = "BankID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SeqNo = "SeqNo";
			public const string AssetID = "AssetID";
			public const string SRAssetsStatusFrom = "SRAssetsStatusFrom";
			public const string SRAssetsStatusTo = "SRAssetsStatusTo";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsFixedAssetFrom = "IsFixedAssetFrom";
			public const string IsFixedAssetTo = "IsFixedAssetTo";
			public const string CurrentValue = "CurrentValue";
			public const string TransactionDate = "TransactionDate";
			public const string BuyersName = "BuyersName";
			public const string BuyersAddress = "BuyersAddress";
			public const string BuyersPhoneNo = "BuyersPhoneNo";
			public const string BuyersTaxRegister = "BuyersTaxRegister";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string DepreciationAccValue = "DepreciationAccValue";
			public const string SalesPrice = "SalesPrice";
			public const string TaxStatus = "TaxStatus";
			public const string TaxPercentage = "TaxPercentage";
			public const string TaxAmount = "TaxAmount";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string BankID = "BankID";
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
			lock (typeof(AssetStatusHistoryMetadata))
			{
				if (AssetStatusHistoryMetadata.mapDelegates == null)
				{
					AssetStatusHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AssetStatusHistoryMetadata.meta == null)
				{
					AssetStatusHistoryMetadata.meta = new AssetStatusHistoryMetadata();
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

				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetsStatusFrom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssetsStatusTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFixedAssetFrom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFixedAssetTo", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CurrentValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BuyersName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BuyersAddress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BuyersPhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BuyersTaxRegister", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepreciationAccValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SalesPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TaxStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TaxPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));


				meta.Source = "AssetStatusHistory";
				meta.Destination = "AssetStatusHistory";
				meta.spInsert = "proc_AssetStatusHistoryInsert";
				meta.spUpdate = "proc_AssetStatusHistoryUpdate";
				meta.spDelete = "proc_AssetStatusHistoryDelete";
				meta.spLoadAll = "proc_AssetStatusHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetStatusHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetStatusHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
