/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/25/2023 4:44:29 PM
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
	abstract public class esTransPaymentItemCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransPaymentItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPaymentItemQuery query)
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
			this.InitQuery(query as esTransPaymentItemQuery);
		}
		#endregion

		virtual public TransPaymentItem DetachEntity(TransPaymentItem entity)
		{
			return base.DetachEntity(entity) as TransPaymentItem;
		}

		virtual public TransPaymentItem AttachEntity(TransPaymentItem entity)
		{
			return base.AttachEntity(entity) as TransPaymentItem;
		}

		virtual public void Combine(TransPaymentItemCollection collection)
		{
			base.Combine(collection);
		}

		new public TransPaymentItem this[int index]
		{
			get
			{
				return base[index] as TransPaymentItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentItem);
		}
	}

	[Serializable]
	abstract public class esTransPaymentItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPaymentItem()
		{
		}

		public esTransPaymentItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String paymentNo, String sequenceNo)
		{
			esTransPaymentItemQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String paymentNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo", paymentNo);
			parms.Add("SequenceNo", sequenceNo);
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
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;
						case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;
						case "SRCardProvider": this.str.SRCardProvider = (string)value; break;
						case "SRCardType": this.str.SRCardType = (string)value; break;
						case "SRDiscountReason": this.str.SRDiscountReason = (string)value; break;
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;
						case "CardHolderName": this.str.CardHolderName = (string)value; break;
						case "CardFeeAmount": this.str.CardFeeAmount = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
						case "IsFromDownPayment": this.str.IsFromDownPayment = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPackageClosed": this.str.IsPackageClosed = (string)value; break;
						case "CardNo": this.str.CardNo = (string)value; break;
						case "RoundingAmount": this.str.RoundingAmount = (string)value; break;
						case "AmountReceived": this.str.AmountReceived = (string)value; break;
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
						case "CashTransactionReconcileId": this.str.CashTransactionReconcileId = (string)value; break;
						case "IsBackOfficeReturn": this.str.IsBackOfficeReturn = (string)value; break;
						case "BackOfficeReturnTransactionId": this.str.BackOfficeReturnTransactionId = (string)value; break;
						case "VisiteDownPaymentNotes": this.str.VisiteDownPaymentNotes = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "CardFeeAmount":

							if (value == null || value is System.Decimal)
								this.CardFeeAmount = (System.Decimal?)value;
							break;
						case "Amount":

							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "Balance":

							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
							break;
						case "IsFromDownPayment":

							if (value == null || value is System.Boolean)
								this.IsFromDownPayment = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPackageClosed":

							if (value == null || value is System.Boolean)
								this.IsPackageClosed = (System.Boolean?)value;
							break;
						case "RoundingAmount":

							if (value == null || value is System.Decimal)
								this.RoundingAmount = (System.Decimal?)value;
							break;
						case "AmountReceived":

							if (value == null || value is System.Decimal)
								this.AmountReceived = (System.Decimal?)value;
							break;
						case "CashTransactionReconcileId":

							if (value == null || value is System.Int32)
								this.CashTransactionReconcileId = (System.Int32?)value;
							break;
						case "IsBackOfficeReturn":

							if (value == null || value is System.Boolean)
								this.IsBackOfficeReturn = (System.Boolean?)value;
							break;
						case "BackOfficeReturnTransactionId":

							if (value == null || value is System.Int32)
								this.BackOfficeReturnTransactionId = (System.Int32?)value;
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
		/// Maps to TransPaymentItem.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.SRPaymentType);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.SRPaymentMethod
		/// </summary>
		virtual public System.String SRPaymentMethod
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.SRPaymentMethod);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.SRPaymentMethod, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.SRCardProvider
		/// </summary>
		virtual public System.String SRCardProvider
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.SRCardProvider);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.SRCardProvider, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.SRCardType
		/// </summary>
		virtual public System.String SRCardType
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.SRCardType);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.SRCardType, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.SRDiscountReason);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.EDCMachineID);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.CardHolderName
		/// </summary>
		virtual public System.String CardHolderName
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.CardHolderName);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.CardHolderName, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.CardFeeAmount
		/// </summary>
		virtual public System.Decimal? CardFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemMetadata.ColumnNames.CardFeeAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemMetadata.ColumnNames.CardFeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.BankID);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemMetadata.ColumnNames.Amount);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.IsFromDownPayment
		/// </summary>
		virtual public System.Boolean? IsFromDownPayment
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentItemMetadata.ColumnNames.IsFromDownPayment);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentItemMetadata.ColumnNames.IsFromDownPayment, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.IsPackageClosed
		/// </summary>
		virtual public System.Boolean? IsPackageClosed
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentItemMetadata.ColumnNames.IsPackageClosed);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentItemMetadata.ColumnNames.IsPackageClosed, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.CardNo
		/// </summary>
		virtual public System.String CardNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.CardNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.CardNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.RoundingAmount
		/// </summary>
		virtual public System.Decimal? RoundingAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemMetadata.ColumnNames.RoundingAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemMetadata.ColumnNames.RoundingAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.AmountReceived
		/// </summary>
		virtual public System.Decimal? AmountReceived
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentItemMetadata.ColumnNames.AmountReceived);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentItemMetadata.ColumnNames.AmountReceived, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.ReferenceSequenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.ReferenceSequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.CashTransactionReconcileId
		/// </summary>
		virtual public System.Int32? CashTransactionReconcileId
		{
			get
			{
				return base.GetSystemInt32(TransPaymentItemMetadata.ColumnNames.CashTransactionReconcileId);
			}

			set
			{
				base.SetSystemInt32(TransPaymentItemMetadata.ColumnNames.CashTransactionReconcileId, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.IsBackOfficeReturn
		/// </summary>
		virtual public System.Boolean? IsBackOfficeReturn
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentItemMetadata.ColumnNames.IsBackOfficeReturn);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentItemMetadata.ColumnNames.IsBackOfficeReturn, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.BackOfficeReturnTransactionId
		/// </summary>
		virtual public System.Int32? BackOfficeReturnTransactionId
		{
			get
			{
				return base.GetSystemInt32(TransPaymentItemMetadata.ColumnNames.BackOfficeReturnTransactionId);
			}

			set
			{
				base.SetSystemInt32(TransPaymentItemMetadata.ColumnNames.BackOfficeReturnTransactionId, value);
			}
		}
		/// <summary>
		/// Maps to TransPaymentItem.VisiteDownPaymentNotes
		/// </summary>
		virtual public System.String VisiteDownPaymentNotes
		{
			get
			{
				return base.GetSystemString(TransPaymentItemMetadata.ColumnNames.VisiteDownPaymentNotes);
			}

			set
			{
				base.SetSystemString(TransPaymentItemMetadata.ColumnNames.VisiteDownPaymentNotes, value);
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
			public esStrings(esTransPaymentItem entity)
			{
				this.entity = entity;
			}
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
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
			public System.String SRCardProvider
			{
				get
				{
					System.String data = entity.SRCardProvider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardProvider = null;
					else entity.SRCardProvider = Convert.ToString(value);
				}
			}
			public System.String SRCardType
			{
				get
				{
					System.String data = entity.SRCardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardType = null;
					else entity.SRCardType = Convert.ToString(value);
				}
			}
			public System.String SRDiscountReason
			{
				get
				{
					System.String data = entity.SRDiscountReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDiscountReason = null;
					else entity.SRDiscountReason = Convert.ToString(value);
				}
			}
			public System.String EDCMachineID
			{
				get
				{
					System.String data = entity.EDCMachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineID = null;
					else entity.EDCMachineID = Convert.ToString(value);
				}
			}
			public System.String CardHolderName
			{
				get
				{
					System.String data = entity.CardHolderName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardHolderName = null;
					else entity.CardHolderName = Convert.ToString(value);
				}
			}
			public System.String CardFeeAmount
			{
				get
				{
					System.Decimal? data = entity.CardFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardFeeAmount = null;
					else entity.CardFeeAmount = Convert.ToDecimal(value);
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
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
				}
			}
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
				}
			}
			public System.String IsFromDownPayment
			{
				get
				{
					System.Boolean? data = entity.IsFromDownPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromDownPayment = null;
					else entity.IsFromDownPayment = Convert.ToBoolean(value);
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
			public System.String IsPackageClosed
			{
				get
				{
					System.Boolean? data = entity.IsPackageClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackageClosed = null;
					else entity.IsPackageClosed = Convert.ToBoolean(value);
				}
			}
			public System.String CardNo
			{
				get
				{
					System.String data = entity.CardNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardNo = null;
					else entity.CardNo = Convert.ToString(value);
				}
			}
			public System.String RoundingAmount
			{
				get
				{
					System.Decimal? data = entity.RoundingAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoundingAmount = null;
					else entity.RoundingAmount = Convert.ToDecimal(value);
				}
			}
			public System.String AmountReceived
			{
				get
				{
					System.Decimal? data = entity.AmountReceived;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountReceived = null;
					else entity.AmountReceived = Convert.ToDecimal(value);
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
			public System.String CashTransactionReconcileId
			{
				get
				{
					System.Int32? data = entity.CashTransactionReconcileId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashTransactionReconcileId = null;
					else entity.CashTransactionReconcileId = Convert.ToInt32(value);
				}
			}
			public System.String IsBackOfficeReturn
			{
				get
				{
					System.Boolean? data = entity.IsBackOfficeReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBackOfficeReturn = null;
					else entity.IsBackOfficeReturn = Convert.ToBoolean(value);
				}
			}
			public System.String BackOfficeReturnTransactionId
			{
				get
				{
					System.Int32? data = entity.BackOfficeReturnTransactionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BackOfficeReturnTransactionId = null;
					else entity.BackOfficeReturnTransactionId = Convert.ToInt32(value);
				}
			}
			public System.String VisiteDownPaymentNotes
			{
				get
				{
					System.String data = entity.VisiteDownPaymentNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisiteDownPaymentNotes = null;
					else entity.VisiteDownPaymentNotes = Convert.ToString(value);
				}
			}
			private esTransPaymentItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentItemQuery query)
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
				throw new Exception("esTransPaymentItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPaymentItem : esTransPaymentItem
	{
	}

	[Serializable]
	abstract public class esTransPaymentItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemMetadata.Meta();
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		}

		public esQueryItem SRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
			}
		}

		public esQueryItem SRCardProvider
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.SRCardProvider, esSystemType.String);
			}
		}

		public esQueryItem SRCardType
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.SRCardType, esSystemType.String);
			}
		}

		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		}

		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		}

		public esQueryItem CardHolderName
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.CardHolderName, esSystemType.String);
			}
		}

		public esQueryItem CardFeeAmount
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.CardFeeAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.BankID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem IsFromDownPayment
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.IsFromDownPayment, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPackageClosed
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.IsPackageClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem CardNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.CardNo, esSystemType.String);
			}
		}

		public esQueryItem RoundingAmount
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.RoundingAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem AmountReceived
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.AmountReceived, esSystemType.Decimal);
			}
		}

		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		}

		public esQueryItem CashTransactionReconcileId
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.CashTransactionReconcileId, esSystemType.Int32);
			}
		}

		public esQueryItem IsBackOfficeReturn
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.IsBackOfficeReturn, esSystemType.Boolean);
			}
		}

		public esQueryItem BackOfficeReturnTransactionId
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.BackOfficeReturnTransactionId, esSystemType.Int32);
			}
		}

		public esQueryItem VisiteDownPaymentNotes
		{
			get
			{
				return new esQueryItem(this, TransPaymentItemMetadata.ColumnNames.VisiteDownPaymentNotes, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentItemCollection")]
	public partial class TransPaymentItemCollection : esTransPaymentItemCollection, IEnumerable<TransPaymentItem>
	{
		public TransPaymentItemCollection()
		{

		}

		public static implicit operator List<TransPaymentItem>(TransPaymentItemCollection coll)
		{
			List<TransPaymentItem> list = new List<TransPaymentItem>();

			foreach (TransPaymentItem emp in coll)
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
				return TransPaymentItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemQuery();
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
		public bool Load(TransPaymentItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPaymentItem AddNew()
		{
			TransPaymentItem entity = base.AddNewEntity() as TransPaymentItem;

			return entity;
		}
		public TransPaymentItem FindByPrimaryKey(String paymentNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(paymentNo, sequenceNo) as TransPaymentItem;
		}

		#region IEnumerable< TransPaymentItem> Members

		IEnumerator<TransPaymentItem> IEnumerable<TransPaymentItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentItem;
			}
		}

		#endregion

		private TransPaymentItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPaymentItem ({PaymentNo, SequenceNo})")]
	[Serializable]
	public partial class TransPaymentItem : esTransPaymentItem
	{
		public TransPaymentItem()
		{
		}

		public TransPaymentItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentItemMetadata.Meta();
			}
		}

		override protected esTransPaymentItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentItemQuery();
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
		public bool Load(TransPaymentItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransPaymentItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentItemQuery : esTransPaymentItemQuery
	{
		public TransPaymentItemQuery()
		{

		}

		public TransPaymentItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransPaymentItemQuery";
		}
	}

	[Serializable]
	public partial class TransPaymentItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.SRPaymentType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.SRPaymentMethod, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.SRPaymentMethod;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.SRCardProvider, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.SRCardProvider;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.SRCardType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.SRCardType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.SRDiscountReason, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.EDCMachineID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.EDCMachineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.CardHolderName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.CardHolderName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.CardFeeAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.CardFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.BankID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.ReferenceNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.Amount, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.Balance, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.Balance;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.IsFromDownPayment, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.IsFromDownPayment;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.IsPackageClosed, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.IsPackageClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.CardNo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.CardNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.RoundingAmount, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.RoundingAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.AmountReceived, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.AmountReceived;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.ReferenceSequenceNo, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.CashTransactionReconcileId, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.CashTransactionReconcileId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.IsBackOfficeReturn, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.IsBackOfficeReturn;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.BackOfficeReturnTransactionId, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.BackOfficeReturnTransactionId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentItemMetadata.ColumnNames.VisiteDownPaymentNotes, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentItemMetadata.PropertyNames.VisiteDownPaymentNotes;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransPaymentItemMetadata Meta()
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
			public const string PaymentNo = "PaymentNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardHolderName = "CardHolderName";
			public const string CardFeeAmount = "CardFeeAmount";
			public const string BankID = "BankID";
			public const string ReferenceNo = "ReferenceNo";
			public const string Amount = "Amount";
			public const string Balance = "Balance";
			public const string IsFromDownPayment = "IsFromDownPayment";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPackageClosed = "IsPackageClosed";
			public const string CardNo = "CardNo";
			public const string RoundingAmount = "RoundingAmount";
			public const string AmountReceived = "AmountReceived";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			public const string IsBackOfficeReturn = "IsBackOfficeReturn";
			public const string BackOfficeReturnTransactionId = "BackOfficeReturnTransactionId";
			public const string VisiteDownPaymentNotes = "VisiteDownPaymentNotes";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PaymentNo = "PaymentNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRPaymentType = "SRPaymentType";
			public const string SRPaymentMethod = "SRPaymentMethod";
			public const string SRCardProvider = "SRCardProvider";
			public const string SRCardType = "SRCardType";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string EDCMachineID = "EDCMachineID";
			public const string CardHolderName = "CardHolderName";
			public const string CardFeeAmount = "CardFeeAmount";
			public const string BankID = "BankID";
			public const string ReferenceNo = "ReferenceNo";
			public const string Amount = "Amount";
			public const string Balance = "Balance";
			public const string IsFromDownPayment = "IsFromDownPayment";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPackageClosed = "IsPackageClosed";
			public const string CardNo = "CardNo";
			public const string RoundingAmount = "RoundingAmount";
			public const string AmountReceived = "AmountReceived";
			public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			public const string CashTransactionReconcileId = "CashTransactionReconcileId";
			public const string IsBackOfficeReturn = "IsBackOfficeReturn";
			public const string BackOfficeReturnTransactionId = "BackOfficeReturnTransactionId";
			public const string VisiteDownPaymentNotes = "VisiteDownPaymentNotes";
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
			lock (typeof(TransPaymentItemMetadata))
			{
				if (TransPaymentItemMetadata.mapDelegates == null)
				{
					TransPaymentItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransPaymentItemMetadata.meta == null)
				{
					TransPaymentItemMetadata.meta = new TransPaymentItemMetadata();
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

				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardProvider", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDiscountReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardHolderName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsFromDownPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPackageClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CardNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoundingAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AmountReceived", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CashTransactionReconcileId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsBackOfficeReturn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BackOfficeReturnTransactionId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VisiteDownPaymentNotes", new esTypeMap("varchar", "System.String"));


				meta.Source = "TransPaymentItem";
				meta.Destination = "TransPaymentItem";
				meta.spInsert = "proc_TransPaymentItemInsert";
				meta.spUpdate = "proc_TransPaymentItemUpdate";
				meta.spDelete = "proc_TransPaymentItemDelete";
				meta.spLoadAll = "proc_TransPaymentItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
