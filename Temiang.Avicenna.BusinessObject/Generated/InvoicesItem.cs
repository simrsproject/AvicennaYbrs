/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/6/2023 2:10:49 PM
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
	abstract public class esInvoicesItemCollection : esEntityCollectionWAuditLog
	{
		public esInvoicesItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "InvoicesItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esInvoicesItemQuery query)
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
			this.InitQuery(query as esInvoicesItemQuery);
		}
		#endregion

		virtual public InvoicesItem DetachEntity(InvoicesItem entity)
		{
			return base.DetachEntity(entity) as InvoicesItem;
		}

		virtual public InvoicesItem AttachEntity(InvoicesItem entity)
		{
			return base.AttachEntity(entity) as InvoicesItem;
		}

		virtual public void Combine(InvoicesItemCollection collection)
		{
			base.Combine(collection);
		}

		new public InvoicesItem this[int index]
		{
			get
			{
				return base[index] as InvoicesItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InvoicesItem);
		}
	}

	[Serializable]
	abstract public class esInvoicesItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInvoicesItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esInvoicesItem()
		{
		}

		public esInvoicesItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String invoiceNo, String paymentNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo, paymentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invoiceNo, String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invoiceNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invoiceNo, paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(String invoiceNo, String paymentNo)
		{
			esInvoicesItemQuery query = this.GetDynamicQuery();
			query.Where(query.InvoiceNo == invoiceNo, query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String invoiceNo, String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("InvoiceNo", invoiceNo);
			parms.Add("PaymentNo", paymentNo);
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
						case "InvoiceNo": this.str.InvoiceNo = (string)value; break;
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "PatientName": this.str.PatientName = (string)value; break;
						case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "AccountID": this.str.AccountID = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "VerifyAmount": this.str.VerifyAmount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "OtherAmount": this.str.OtherAmount = (string)value; break;
						case "InvoiceReferenceNo": this.str.InvoiceReferenceNo = (string)value; break;
						case "BankCost": this.str.BankCost = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "SRDiscountReason": this.str.SRDiscountReason = (string)value; break;
						case "IsDiscountInPercent": this.str.IsDiscountInPercent = (string)value; break;
						case "DiscountPercentage": this.str.DiscountPercentage = (string)value; break;
						case "PpnAmount": this.str.PpnAmount = (string)value; break;
						case "PphAmount": this.str.PphAmount = (string)value; break;
						case "IsPpn": this.str.IsPpn = (string)value; break;
						case "PpnPercentage": this.str.PpnPercentage = (string)value; break;
						case "IsPph": this.str.IsPph = (string)value; break;
						case "SRPph": this.str.SRPph = (string)value; break;
						case "PphPercentage": this.str.PphPercentage = (string)value; break;
						case "ClaimDifferenceAmount": this.str.ClaimDifferenceAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PaymentDate":

							if (value == null || value is System.DateTime)
								this.PaymentDate = (System.DateTime?)value;
							break;
						case "PaymentAmount":

							if (value == null || value is System.Decimal)
								this.PaymentAmount = (System.Decimal?)value;
							break;
						case "Amount":

							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "VerifyAmount":

							if (value == null || value is System.Decimal)
								this.VerifyAmount = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "OtherAmount":

							if (value == null || value is System.Decimal)
								this.OtherAmount = (System.Decimal?)value;
							break;
						case "BankCost":

							if (value == null || value is System.Decimal)
								this.BankCost = (System.Decimal?)value;
							break;
						case "ChartOfAccountId":

							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubLedgerId":

							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "IsDiscountInPercent":

							if (value == null || value is System.Boolean)
								this.IsDiscountInPercent = (System.Boolean?)value;
							break;
						case "DiscountPercentage":

							if (value == null || value is System.Decimal)
								this.DiscountPercentage = (System.Decimal?)value;
							break;
						case "PpnAmount":

							if (value == null || value is System.Decimal)
								this.PpnAmount = (System.Decimal?)value;
							break;
						case "PphAmount":

							if (value == null || value is System.Decimal)
								this.PphAmount = (System.Decimal?)value;
							break;
						case "IsPpn":

							if (value == null || value is System.Boolean)
								this.IsPpn = (System.Boolean?)value;
							break;
						case "PpnPercentage":

							if (value == null || value is System.Decimal)
								this.PpnPercentage = (System.Decimal?)value;
							break;
						case "IsPph":

							if (value == null || value is System.Boolean)
								this.IsPph = (System.Boolean?)value;
							break;
						case "PphPercentage":

							if (value == null || value is System.Decimal)
								this.PphPercentage = (System.Decimal?)value;
							break;
						case "ClaimDifferenceAmount":

							if (value == null || value is System.Decimal)
								this.ClaimDifferenceAmount = (System.Decimal?)value;
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
		/// Maps to InvoicesItem.InvoiceNo
		/// </summary>
		virtual public System.String InvoiceNo
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.InvoiceNo);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.InvoiceNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(InvoicesItemMetadata.ColumnNames.PaymentDate);
			}

			set
			{
				base.SetSystemDateTime(InvoicesItemMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PatientName
		/// </summary>
		virtual public System.String PatientName
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.PatientName);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.PatientName, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PaymentAmount
		/// </summary>
		virtual public System.Decimal? PaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.PaymentAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.PaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.AccountID
		/// </summary>
		virtual public System.String AccountID
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.AccountID);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.AccountID, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.Amount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.VerifyAmount
		/// </summary>
		virtual public System.Decimal? VerifyAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.VerifyAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.VerifyAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(InvoicesItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(InvoicesItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.OtherAmount
		/// </summary>
		virtual public System.Decimal? OtherAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.OtherAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.OtherAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.InvoiceReferenceNo
		/// </summary>
		virtual public System.String InvoiceReferenceNo
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.InvoiceReferenceNo);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.InvoiceReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.BankCost
		/// </summary>
		virtual public System.Decimal? BankCost
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.BankCost);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.BankCost, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(InvoicesItemMetadata.ColumnNames.ChartOfAccountId);
			}

			set
			{
				base.SetSystemInt32(InvoicesItemMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(InvoicesItemMetadata.ColumnNames.SubLedgerId);
			}

			set
			{
				base.SetSystemInt32(InvoicesItemMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.SRDiscountReason
		/// </summary>
		virtual public System.String SRDiscountReason
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.SRDiscountReason);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.SRDiscountReason, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.IsDiscountInPercent
		/// </summary>
		virtual public System.Boolean? IsDiscountInPercent
		{
			get
			{
				return base.GetSystemBoolean(InvoicesItemMetadata.ColumnNames.IsDiscountInPercent);
			}

			set
			{
				base.SetSystemBoolean(InvoicesItemMetadata.ColumnNames.IsDiscountInPercent, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.DiscountPercentage
		/// </summary>
		virtual public System.Decimal? DiscountPercentage
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.DiscountPercentage);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.DiscountPercentage, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PpnAmount
		/// </summary>
		virtual public System.Decimal? PpnAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.PpnAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.PpnAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PphAmount
		/// </summary>
		virtual public System.Decimal? PphAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.PphAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.PphAmount, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.IsPpn
		/// </summary>
		virtual public System.Boolean? IsPpn
		{
			get
			{
				return base.GetSystemBoolean(InvoicesItemMetadata.ColumnNames.IsPpn);
			}

			set
			{
				base.SetSystemBoolean(InvoicesItemMetadata.ColumnNames.IsPpn, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PpnPercentage
		/// </summary>
		virtual public System.Decimal? PpnPercentage
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.PpnPercentage);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.PpnPercentage, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.IsPph
		/// </summary>
		virtual public System.Boolean? IsPph
		{
			get
			{
				return base.GetSystemBoolean(InvoicesItemMetadata.ColumnNames.IsPph);
			}

			set
			{
				base.SetSystemBoolean(InvoicesItemMetadata.ColumnNames.IsPph, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.SRPph
		/// </summary>
		virtual public System.String SRPph
		{
			get
			{
				return base.GetSystemString(InvoicesItemMetadata.ColumnNames.SRPph);
			}

			set
			{
				base.SetSystemString(InvoicesItemMetadata.ColumnNames.SRPph, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.PphPercentage
		/// </summary>
		virtual public System.Decimal? PphPercentage
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.PphPercentage);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.PphPercentage, value);
			}
		}
		/// <summary>
		/// Maps to InvoicesItem.ClaimDifferenceAmount
		/// </summary>
		virtual public System.Decimal? ClaimDifferenceAmount
		{
			get
			{
				return base.GetSystemDecimal(InvoicesItemMetadata.ColumnNames.ClaimDifferenceAmount);
			}

			set
			{
				base.SetSystemDecimal(InvoicesItemMetadata.ColumnNames.ClaimDifferenceAmount, value);
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
			public esStrings(esInvoicesItem entity)
			{
				this.entity = entity;
			}
			public System.String InvoiceNo
			{
				get
				{
					System.String data = entity.InvoiceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceNo = null;
					else entity.InvoiceNo = Convert.ToString(value);
				}
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
			public System.String PaymentDate
			{
				get
				{
					System.DateTime? data = entity.PaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentDate = null;
					else entity.PaymentDate = Convert.ToDateTime(value);
				}
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
			public System.String PatientName
			{
				get
				{
					System.String data = entity.PatientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientName = null;
					else entity.PatientName = Convert.ToString(value);
				}
			}
			public System.String PaymentAmount
			{
				get
				{
					System.Decimal? data = entity.PaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentAmount = null;
					else entity.PaymentAmount = Convert.ToDecimal(value);
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
			public System.String AccountID
			{
				get
				{
					System.String data = entity.AccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccountID = null;
					else entity.AccountID = Convert.ToString(value);
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
			public System.String VerifyAmount
			{
				get
				{
					System.Decimal? data = entity.VerifyAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifyAmount = null;
					else entity.VerifyAmount = Convert.ToDecimal(value);
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
			public System.String OtherAmount
			{
				get
				{
					System.Decimal? data = entity.OtherAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherAmount = null;
					else entity.OtherAmount = Convert.ToDecimal(value);
				}
			}
			public System.String InvoiceReferenceNo
			{
				get
				{
					System.String data = entity.InvoiceReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvoiceReferenceNo = null;
					else entity.InvoiceReferenceNo = Convert.ToString(value);
				}
			}
			public System.String BankCost
			{
				get
				{
					System.Decimal? data = entity.BankCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankCost = null;
					else entity.BankCost = Convert.ToDecimal(value);
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
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
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
			public System.String IsDiscountInPercent
			{
				get
				{
					System.Boolean? data = entity.IsDiscountInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscountInPercent = null;
					else entity.IsDiscountInPercent = Convert.ToBoolean(value);
				}
			}
			public System.String DiscountPercentage
			{
				get
				{
					System.Decimal? data = entity.DiscountPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountPercentage = null;
					else entity.DiscountPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String PpnAmount
			{
				get
				{
					System.Decimal? data = entity.PpnAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PpnAmount = null;
					else entity.PpnAmount = Convert.ToDecimal(value);
				}
			}
			public System.String PphAmount
			{
				get
				{
					System.Decimal? data = entity.PphAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PphAmount = null;
					else entity.PphAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsPpn
			{
				get
				{
					System.Boolean? data = entity.IsPpn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPpn = null;
					else entity.IsPpn = Convert.ToBoolean(value);
				}
			}
			public System.String PpnPercentage
			{
				get
				{
					System.Decimal? data = entity.PpnPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PpnPercentage = null;
					else entity.PpnPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsPph
			{
				get
				{
					System.Boolean? data = entity.IsPph;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPph = null;
					else entity.IsPph = Convert.ToBoolean(value);
				}
			}
			public System.String SRPph
			{
				get
				{
					System.String data = entity.SRPph;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPph = null;
					else entity.SRPph = Convert.ToString(value);
				}
			}
			public System.String PphPercentage
			{
				get
				{
					System.Decimal? data = entity.PphPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PphPercentage = null;
					else entity.PphPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String ClaimDifferenceAmount
			{
				get
				{
					System.Decimal? data = entity.ClaimDifferenceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClaimDifferenceAmount = null;
					else entity.ClaimDifferenceAmount = Convert.ToDecimal(value);
				}
			}
			private esInvoicesItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInvoicesItemQuery query)
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
				throw new Exception("esInvoicesItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class InvoicesItem : esInvoicesItem
	{
	}

	[Serializable]
	abstract public class esInvoicesItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return InvoicesItemMetadata.Meta();
			}
		}

		public esQueryItem InvoiceNo
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.InvoiceNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem PatientName
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PatientName, esSystemType.String);
			}
		}

		public esQueryItem PaymentAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem AccountID
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.AccountID, esSystemType.String);
			}
		}

		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		}

		public esQueryItem VerifyAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.VerifyAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem OtherAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.OtherAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem InvoiceReferenceNo
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.InvoiceReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem BankCost
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.BankCost, esSystemType.Decimal);
			}
		}

		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem SRDiscountReason
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.SRDiscountReason, esSystemType.String);
			}
		}

		public esQueryItem IsDiscountInPercent
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.IsDiscountInPercent, esSystemType.Boolean);
			}
		}

		public esQueryItem DiscountPercentage
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.DiscountPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem PpnAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PpnAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem PphAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PphAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem IsPpn
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.IsPpn, esSystemType.Boolean);
			}
		}

		public esQueryItem PpnPercentage
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PpnPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem IsPph
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.IsPph, esSystemType.Boolean);
			}
		}

		public esQueryItem SRPph
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.SRPph, esSystemType.String);
			}
		}

		public esQueryItem PphPercentage
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.PphPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem ClaimDifferenceAmount
		{
			get
			{
				return new esQueryItem(this, InvoicesItemMetadata.ColumnNames.ClaimDifferenceAmount, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InvoicesItemCollection")]
	public partial class InvoicesItemCollection : esInvoicesItemCollection, IEnumerable<InvoicesItem>
	{
		public InvoicesItemCollection()
		{

		}

		public static implicit operator List<InvoicesItem>(InvoicesItemCollection coll)
		{
			List<InvoicesItem> list = new List<InvoicesItem>();

			foreach (InvoicesItem emp in coll)
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
				return InvoicesItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoicesItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InvoicesItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InvoicesItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public InvoicesItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoicesItemQuery();
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
		public bool Load(InvoicesItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public InvoicesItem AddNew()
		{
			InvoicesItem entity = base.AddNewEntity() as InvoicesItem;

			return entity;
		}
		public InvoicesItem FindByPrimaryKey(String invoiceNo, String paymentNo)
		{
			return base.FindByPrimaryKey(invoiceNo, paymentNo) as InvoicesItem;
		}

		#region IEnumerable< InvoicesItem> Members

		IEnumerator<InvoicesItem> IEnumerable<InvoicesItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as InvoicesItem;
			}
		}

		#endregion

		private InvoicesItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InvoicesItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("InvoicesItem ({InvoiceNo, PaymentNo})")]
	[Serializable]
	public partial class InvoicesItem : esInvoicesItem
	{
		public InvoicesItem()
		{
		}

		public InvoicesItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InvoicesItemMetadata.Meta();
			}
		}

		override protected esInvoicesItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InvoicesItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public InvoicesItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InvoicesItemQuery();
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
		public bool Load(InvoicesItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private InvoicesItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class InvoicesItemQuery : esInvoicesItemQuery
	{
		public InvoicesItemQuery()
		{

		}

		public InvoicesItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "InvoicesItemQuery";
		}
	}

	[Serializable]
	public partial class InvoicesItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InvoicesItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.InvoiceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.InvoiceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PaymentDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PaymentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.RegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PatientID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PatientName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PatientName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PaymentAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.AccountID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.AccountID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.Amount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.VerifyAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.VerifyAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.OtherAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.OtherAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.InvoiceReferenceNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.InvoiceReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.BankCost, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.BankCost;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.ChartOfAccountId, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.SubLedgerId, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.SRDiscountReason, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.SRDiscountReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.IsDiscountInPercent, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.IsDiscountInPercent;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.DiscountPercentage, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.DiscountPercentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PpnAmount, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PpnAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PphAmount, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PphAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.IsPpn, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.IsPpn;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PpnPercentage, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PpnPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.IsPph, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.IsPph;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.SRPph, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.SRPph;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.PphPercentage, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.PphPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(InvoicesItemMetadata.ColumnNames.ClaimDifferenceAmount, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = InvoicesItemMetadata.PropertyNames.ClaimDifferenceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public InvoicesItemMetadata Meta()
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
			public const string InvoiceNo = "InvoiceNo";
			public const string PaymentNo = "PaymentNo";
			public const string PaymentDate = "PaymentDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string PatientID = "PatientID";
			public const string PatientName = "PatientName";
			public const string PaymentAmount = "PaymentAmount";
			public const string Notes = "Notes";
			public const string AccountID = "AccountID";
			public const string Amount = "Amount";
			public const string VerifyAmount = "VerifyAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OtherAmount = "OtherAmount";
			public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			public const string BankCost = "BankCost";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string IsDiscountInPercent = "IsDiscountInPercent";
			public const string DiscountPercentage = "DiscountPercentage";
			public const string PpnAmount = "PpnAmount";
			public const string PphAmount = "PphAmount";
			public const string IsPpn = "IsPpn";
			public const string PpnPercentage = "PpnPercentage";
			public const string IsPph = "IsPph";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string ClaimDifferenceAmount = "ClaimDifferenceAmount";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string InvoiceNo = "InvoiceNo";
			public const string PaymentNo = "PaymentNo";
			public const string PaymentDate = "PaymentDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string PatientID = "PatientID";
			public const string PatientName = "PatientName";
			public const string PaymentAmount = "PaymentAmount";
			public const string Notes = "Notes";
			public const string AccountID = "AccountID";
			public const string Amount = "Amount";
			public const string VerifyAmount = "VerifyAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string OtherAmount = "OtherAmount";
			public const string InvoiceReferenceNo = "InvoiceReferenceNo";
			public const string BankCost = "BankCost";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubLedgerId = "SubLedgerId";
			public const string SRDiscountReason = "SRDiscountReason";
			public const string IsDiscountInPercent = "IsDiscountInPercent";
			public const string DiscountPercentage = "DiscountPercentage";
			public const string PpnAmount = "PpnAmount";
			public const string PphAmount = "PphAmount";
			public const string IsPpn = "IsPpn";
			public const string PpnPercentage = "PpnPercentage";
			public const string IsPph = "IsPph";
			public const string SRPph = "SRPph";
			public const string PphPercentage = "PphPercentage";
			public const string ClaimDifferenceAmount = "ClaimDifferenceAmount";
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
			lock (typeof(InvoicesItemMetadata))
			{
				if (InvoicesItemMetadata.mapDelegates == null)
				{
					InvoicesItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (InvoicesItemMetadata.meta == null)
				{
					InvoicesItemMetadata.meta = new InvoicesItemMetadata();
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

				meta.AddTypeMap("InvoiceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AccountID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("VerifyAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InvoiceReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankCost", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRDiscountReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDiscountInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DiscountPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PpnAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PphAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPpn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PpnPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPph", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRPph", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PphPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ClaimDifferenceAmount", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "InvoicesItem";
				meta.Destination = "InvoicesItem";
				meta.spInsert = "proc_InvoicesItemInsert";
				meta.spUpdate = "proc_InvoicesItemUpdate";
				meta.spDelete = "proc_InvoicesItemDelete";
				meta.spLoadAll = "proc_InvoicesItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_InvoicesItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InvoicesItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
