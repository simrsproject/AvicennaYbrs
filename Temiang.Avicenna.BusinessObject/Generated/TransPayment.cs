/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/26/2023 11:08:56 AM
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
	abstract public class esTransPaymentCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransPaymentCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPaymentQuery query)
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
			this.InitQuery(query as esTransPaymentQuery);
		}
		#endregion

		virtual public TransPayment DetachEntity(TransPayment entity)
		{
			return base.DetachEntity(entity) as TransPayment;
		}

		virtual public TransPayment AttachEntity(TransPayment entity)
		{
			return base.AttachEntity(entity) as TransPayment;
		}

		virtual public void Combine(TransPaymentCollection collection)
		{
			base.Combine(collection);
		}

		new public TransPayment this[int index]
		{
			get
			{
				return base[index] as TransPayment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPayment);
		}
	}

	[Serializable]
	abstract public class esTransPayment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPayment()
		{
		}

		public esTransPayment(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(String paymentNo)
		{
			esTransPaymentQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String paymentNo)
		{
			esParameters parms = new esParameters();
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
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "TransactionCode": this.str.TransactionCode = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "PaymentReferenceNo": this.str.PaymentReferenceNo = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "PaymentTime": this.str.PaymentTime = (string)value; break;
						case "PrintReceiptAsName": this.str.PrintReceiptAsName = (string)value; break;
						case "TotalPaymentAmount": this.str.TotalPaymentAmount = (string)value; break;
						case "RemainingAmount": this.str.RemainingAmount = (string)value; break;
						case "PrintNumber": this.str.PrintNumber = (string)value; break;
						case "PaymentReceiptNo": this.str.PaymentReceiptNo = (string)value; break;
						case "CreatedBy": this.str.CreatedBy = (string)value; break;
						case "IsPrinted": this.str.IsPrinted = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsVisiteDownPayment": this.str.IsVisiteDownPayment = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "IsToGuarantor": this.str.IsToGuarantor = (string)value; break;
						case "SRPromotion": this.str.SRPromotion = (string)value; break;
						case "Initial": this.str.Initial = (string)value; break;
						case "ReceiptIsReturned": this.str.ReceiptIsReturned = (string)value; break;
						case "ApproveDate": this.str.ApproveDate = (string)value; break;
						case "ApproveByUserID": this.str.ApproveByUserID = (string)value; break;
						case "VoidDate": this.str.VoidDate = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastPrintedDateTime": this.str.LastPrintedDateTime = (string)value; break;
						case "LastPrintedByUserID": this.str.LastPrintedByUserID = (string)value; break;
						case "IsGuarantorVerified": this.str.IsGuarantorVerified = (string)value; break;
						case "IsPackagePaymentPerVisit": this.str.IsPackagePaymentPerVisit = (string)value; break;
						case "CashManagementNo": this.str.CashManagementNo = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "VoidReason": this.str.VoidReason = (string)value; break;
						case "IsExternalPayment": this.str.IsExternalPayment = (string)value; break;
						case "IsClosedVisiteDownPayment": this.str.IsClosedVisiteDownPayment = (string)value; break;
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
						case "TotalPaymentAmount":

							if (value == null || value is System.Decimal)
								this.TotalPaymentAmount = (System.Decimal?)value;
							break;
						case "RemainingAmount":

							if (value == null || value is System.Decimal)
								this.RemainingAmount = (System.Decimal?)value;
							break;
						case "PrintNumber":

							if (value == null || value is System.Byte)
								this.PrintNumber = (System.Byte?)value;
							break;
						case "IsPrinted":

							if (value == null || value is System.Boolean)
								this.IsPrinted = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsVisiteDownPayment":

							if (value == null || value is System.Boolean)
								this.IsVisiteDownPayment = (System.Boolean?)value;
							break;
						case "IsToGuarantor":

							if (value == null || value is System.Boolean)
								this.IsToGuarantor = (System.Boolean?)value;
							break;
						case "ReceiptIsReturned":

							if (value == null || value is System.Boolean)
								this.ReceiptIsReturned = (System.Boolean?)value;
							break;
						case "ApproveDate":

							if (value == null || value is System.DateTime)
								this.ApproveDate = (System.DateTime?)value;
							break;
						case "VoidDate":

							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						case "LastPrintedDateTime":

							if (value == null || value is System.DateTime)
								this.LastPrintedDateTime = (System.DateTime?)value;
							break;
						case "IsGuarantorVerified":

							if (value == null || value is System.Boolean)
								this.IsGuarantorVerified = (System.Boolean?)value;
							break;
						case "IsPackagePaymentPerVisit":

							if (value == null || value is System.Boolean)
								this.IsPackagePaymentPerVisit = (System.Boolean?)value;
							break;
						case "IsExternalPayment":

							if (value == null || value is System.Boolean)
								this.IsExternalPayment = (System.Boolean?)value;
							break;
						case "IsClosedVisiteDownPayment":

							if (value == null || value is System.Boolean)
								this.IsClosedVisiteDownPayment = (System.Boolean?)value;
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
		/// Maps to TransPayment.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.TransactionCode);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.TransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.PaymentReferenceNo
		/// </summary>
		virtual public System.String PaymentReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.PaymentReferenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.PaymentReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentMetadata.ColumnNames.PaymentDate);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.PaymentTime
		/// </summary>
		virtual public System.String PaymentTime
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.PaymentTime);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.PaymentTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.PrintReceiptAsName
		/// </summary>
		virtual public System.String PrintReceiptAsName
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.PrintReceiptAsName);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.PrintReceiptAsName, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.TotalPaymentAmount
		/// </summary>
		virtual public System.Decimal? TotalPaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentMetadata.ColumnNames.TotalPaymentAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentMetadata.ColumnNames.TotalPaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.RemainingAmount
		/// </summary>
		virtual public System.Decimal? RemainingAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentMetadata.ColumnNames.RemainingAmount);
			}

			set
			{
				base.SetSystemDecimal(TransPaymentMetadata.ColumnNames.RemainingAmount, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.PrintNumber
		/// </summary>
		virtual public System.Byte? PrintNumber
		{
			get
			{
				return base.GetSystemByte(TransPaymentMetadata.ColumnNames.PrintNumber);
			}

			set
			{
				base.SetSystemByte(TransPaymentMetadata.ColumnNames.PrintNumber, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.PaymentReceiptNo
		/// </summary>
		virtual public System.String PaymentReceiptNo
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.PaymentReceiptNo);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.PaymentReceiptNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.CreatedBy);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.CreatedBy, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsPrinted
		/// </summary>
		virtual public System.Boolean? IsPrinted
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsPrinted);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsPrinted, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsVisiteDownPayment
		/// </summary>
		virtual public System.Boolean? IsVisiteDownPayment
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsVisiteDownPayment);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsVisiteDownPayment, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.GuarantorID);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsToGuarantor
		/// </summary>
		virtual public System.Boolean? IsToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsToGuarantor);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsToGuarantor, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.SRPromotion
		/// </summary>
		virtual public System.String SRPromotion
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.SRPromotion);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.SRPromotion, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.Initial
		/// </summary>
		virtual public System.String Initial
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.Initial);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.Initial, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.ReceiptIsReturned
		/// </summary>
		virtual public System.Boolean? ReceiptIsReturned
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.ReceiptIsReturned);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.ReceiptIsReturned, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.ApproveDate
		/// </summary>
		virtual public System.DateTime? ApproveDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentMetadata.ColumnNames.ApproveDate);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentMetadata.ColumnNames.ApproveDate, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.ApproveByUserID
		/// </summary>
		virtual public System.String ApproveByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.ApproveByUserID);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.ApproveByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentMetadata.ColumnNames.VoidDate);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentMetadata.ColumnNames.VoidDate, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.LastPrintedDateTime
		/// </summary>
		virtual public System.DateTime? LastPrintedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentMetadata.ColumnNames.LastPrintedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPaymentMetadata.ColumnNames.LastPrintedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.LastPrintedByUserID
		/// </summary>
		virtual public System.String LastPrintedByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.LastPrintedByUserID);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.LastPrintedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsGuarantorVerified
		/// </summary>
		virtual public System.Boolean? IsGuarantorVerified
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsGuarantorVerified);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsGuarantorVerified, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsPackagePaymentPerVisit
		/// </summary>
		virtual public System.Boolean? IsPackagePaymentPerVisit
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsPackagePaymentPerVisit);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsPackagePaymentPerVisit, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.CashManagementNo
		/// </summary>
		virtual public System.String CashManagementNo
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.CashManagementNo);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.CashManagementNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.VoidReason
		/// </summary>
		virtual public System.String VoidReason
		{
			get
			{
				return base.GetSystemString(TransPaymentMetadata.ColumnNames.VoidReason);
			}

			set
			{
				base.SetSystemString(TransPaymentMetadata.ColumnNames.VoidReason, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsExternalPayment
		/// </summary>
		virtual public System.Boolean? IsExternalPayment
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsExternalPayment);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsExternalPayment, value);
			}
		}
		/// <summary>
		/// Maps to TransPayment.IsClosedVisiteDownPayment
		/// </summary>
		virtual public System.Boolean? IsClosedVisiteDownPayment
		{
			get
			{
				return base.GetSystemBoolean(TransPaymentMetadata.ColumnNames.IsClosedVisiteDownPayment);
			}

			set
			{
				base.SetSystemBoolean(TransPaymentMetadata.ColumnNames.IsClosedVisiteDownPayment, value);
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
			public esStrings(esTransPayment entity)
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
			public System.String PaymentReferenceNo
			{
				get
				{
					System.String data = entity.PaymentReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReferenceNo = null;
					else entity.PaymentReferenceNo = Convert.ToString(value);
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
			public System.String PaymentTime
			{
				get
				{
					System.String data = entity.PaymentTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentTime = null;
					else entity.PaymentTime = Convert.ToString(value);
				}
			}
			public System.String PrintReceiptAsName
			{
				get
				{
					System.String data = entity.PrintReceiptAsName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintReceiptAsName = null;
					else entity.PrintReceiptAsName = Convert.ToString(value);
				}
			}
			public System.String TotalPaymentAmount
			{
				get
				{
					System.Decimal? data = entity.TotalPaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalPaymentAmount = null;
					else entity.TotalPaymentAmount = Convert.ToDecimal(value);
				}
			}
			public System.String RemainingAmount
			{
				get
				{
					System.Decimal? data = entity.RemainingAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemainingAmount = null;
					else entity.RemainingAmount = Convert.ToDecimal(value);
				}
			}
			public System.String PrintNumber
			{
				get
				{
					System.Byte? data = entity.PrintNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintNumber = null;
					else entity.PrintNumber = Convert.ToByte(value);
				}
			}
			public System.String PaymentReceiptNo
			{
				get
				{
					System.String data = entity.PaymentReceiptNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReceiptNo = null;
					else entity.PaymentReceiptNo = Convert.ToString(value);
				}
			}
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
				}
			}
			public System.String IsPrinted
			{
				get
				{
					System.Boolean? data = entity.IsPrinted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrinted = null;
					else entity.IsPrinted = Convert.ToBoolean(value);
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
			public System.String IsVisiteDownPayment
			{
				get
				{
					System.Boolean? data = entity.IsVisiteDownPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisiteDownPayment = null;
					else entity.IsVisiteDownPayment = Convert.ToBoolean(value);
				}
			}
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
			public System.String IsToGuarantor
			{
				get
				{
					System.Boolean? data = entity.IsToGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsToGuarantor = null;
					else entity.IsToGuarantor = Convert.ToBoolean(value);
				}
			}
			public System.String SRPromotion
			{
				get
				{
					System.String data = entity.SRPromotion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPromotion = null;
					else entity.SRPromotion = Convert.ToString(value);
				}
			}
			public System.String Initial
			{
				get
				{
					System.String data = entity.Initial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Initial = null;
					else entity.Initial = Convert.ToString(value);
				}
			}
			public System.String ReceiptIsReturned
			{
				get
				{
					System.Boolean? data = entity.ReceiptIsReturned;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceiptIsReturned = null;
					else entity.ReceiptIsReturned = Convert.ToBoolean(value);
				}
			}
			public System.String ApproveDate
			{
				get
				{
					System.DateTime? data = entity.ApproveDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproveDate = null;
					else entity.ApproveDate = Convert.ToDateTime(value);
				}
			}
			public System.String ApproveByUserID
			{
				get
				{
					System.String data = entity.ApproveByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproveByUserID = null;
					else entity.ApproveByUserID = Convert.ToString(value);
				}
			}
			public System.String VoidDate
			{
				get
				{
					System.DateTime? data = entity.VoidDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDate = null;
					else entity.VoidDate = Convert.ToDateTime(value);
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
			public System.String LastPrintedDateTime
			{
				get
				{
					System.DateTime? data = entity.LastPrintedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPrintedDateTime = null;
					else entity.LastPrintedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastPrintedByUserID
			{
				get
				{
					System.String data = entity.LastPrintedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastPrintedByUserID = null;
					else entity.LastPrintedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsGuarantorVerified
			{
				get
				{
					System.Boolean? data = entity.IsGuarantorVerified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGuarantorVerified = null;
					else entity.IsGuarantorVerified = Convert.ToBoolean(value);
				}
			}
			public System.String IsPackagePaymentPerVisit
			{
				get
				{
					System.Boolean? data = entity.IsPackagePaymentPerVisit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackagePaymentPerVisit = null;
					else entity.IsPackagePaymentPerVisit = Convert.ToBoolean(value);
				}
			}
			public System.String CashManagementNo
			{
				get
				{
					System.String data = entity.CashManagementNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashManagementNo = null;
					else entity.CashManagementNo = Convert.ToString(value);
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
			public System.String VoidReason
			{
				get
				{
					System.String data = entity.VoidReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidReason = null;
					else entity.VoidReason = Convert.ToString(value);
				}
			}
			public System.String IsExternalPayment
			{
				get
				{
					System.Boolean? data = entity.IsExternalPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExternalPayment = null;
					else entity.IsExternalPayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsClosedVisiteDownPayment
			{
				get
				{
					System.Boolean? data = entity.IsClosedVisiteDownPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosedVisiteDownPayment = null;
					else entity.IsClosedVisiteDownPayment = Convert.ToBoolean(value);
				}
			}
			private esTransPayment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentQuery query)
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
				throw new Exception("esTransPayment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPayment : esTransPayment
	{
	}

	[Serializable]
	abstract public class esTransPaymentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentMetadata.Meta();
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PaymentReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PaymentTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PaymentTime, esSystemType.String);
			}
		}

		public esQueryItem PrintReceiptAsName
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PrintReceiptAsName, esSystemType.String);
			}
		}

		public esQueryItem TotalPaymentAmount
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.TotalPaymentAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem RemainingAmount
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.RemainingAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem PrintNumber
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PrintNumber, esSystemType.Byte);
			}
		}

		public esQueryItem PaymentReceiptNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PaymentReceiptNo, esSystemType.String);
			}
		}

		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		}

		public esQueryItem IsPrinted
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsPrinted, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVisiteDownPayment
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsVisiteDownPayment, esSystemType.Boolean);
			}
		}

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		}

		public esQueryItem IsToGuarantor
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsToGuarantor, esSystemType.Boolean);
			}
		}

		public esQueryItem SRPromotion
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.SRPromotion, esSystemType.String);
			}
		}

		public esQueryItem Initial
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.Initial, esSystemType.String);
			}
		}

		public esQueryItem ReceiptIsReturned
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.ReceiptIsReturned, esSystemType.Boolean);
			}
		}

		public esQueryItem ApproveDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.ApproveDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ApproveByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.ApproveByUserID, esSystemType.String);
			}
		}

		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastPrintedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.LastPrintedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastPrintedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.LastPrintedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsGuarantorVerified
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsGuarantorVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPackagePaymentPerVisit
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsPackagePaymentPerVisit, esSystemType.Boolean);
			}
		}

		public esQueryItem CashManagementNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.CashManagementNo, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem VoidReason
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.VoidReason, esSystemType.String);
			}
		}

		public esQueryItem IsExternalPayment
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsExternalPayment, esSystemType.Boolean);
			}
		}

		public esQueryItem IsClosedVisiteDownPayment
		{
			get
			{
				return new esQueryItem(this, TransPaymentMetadata.ColumnNames.IsClosedVisiteDownPayment, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentCollection")]
	public partial class TransPaymentCollection : esTransPaymentCollection, IEnumerable<TransPayment>
	{
		public TransPaymentCollection()
		{

		}

		public static implicit operator List<TransPayment>(TransPaymentCollection coll)
		{
			List<TransPayment> list = new List<TransPayment>();

			foreach (TransPayment emp in coll)
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
				return TransPaymentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPayment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPayment();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentQuery();
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
		public bool Load(TransPaymentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPayment AddNew()
		{
			TransPayment entity = base.AddNewEntity() as TransPayment;

			return entity;
		}
		public TransPayment FindByPrimaryKey(String paymentNo)
		{
			return base.FindByPrimaryKey(paymentNo) as TransPayment;
		}

		#region IEnumerable< TransPayment> Members

		IEnumerator<TransPayment> IEnumerable<TransPayment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransPayment;
			}
		}

		#endregion

		private TransPaymentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPayment' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPayment ({PaymentNo})")]
	[Serializable]
	public partial class TransPayment : esTransPayment
	{
		public TransPayment()
		{
		}

		public TransPayment(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentMetadata.Meta();
			}
		}

		override protected esTransPaymentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransPaymentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentQuery();
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
		public bool Load(TransPaymentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransPaymentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPaymentQuery : esTransPaymentQuery
	{
		public TransPaymentQuery()
		{

		}

		public TransPaymentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransPaymentQuery";
		}
	}

	[Serializable]
	public partial class TransPaymentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.TransactionCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PaymentReferenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PaymentReferenceNo;
			c.CharacterMaxLength = 1000;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.ReferenceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PaymentDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PaymentDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PaymentTime, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PaymentTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PrintReceiptAsName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PrintReceiptAsName;
			c.CharacterMaxLength = 200;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.TotalPaymentAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentMetadata.PropertyNames.TotalPaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.RemainingAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentMetadata.PropertyNames.RemainingAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PrintNumber, 10, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PrintNumber;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PaymentReceiptNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PaymentReceiptNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.CreatedBy, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 256;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsPrinted, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsPrinted;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsApproved, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsApproved;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.Notes, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsVisiteDownPayment, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsVisiteDownPayment;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.GuarantorID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsToGuarantor, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsToGuarantor;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.SRPromotion, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.SRPromotion;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.Initial, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.Initial;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.ReceiptIsReturned, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.ReceiptIsReturned;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.ApproveDate, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentMetadata.PropertyNames.ApproveDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.ApproveByUserID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.ApproveByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.VoidDate, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.VoidByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.LastPrintedDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentMetadata.PropertyNames.LastPrintedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.LastPrintedByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.LastPrintedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsGuarantorVerified, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsGuarantorVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsPackagePaymentPerVisit, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsPackagePaymentPerVisit;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.CashManagementNo, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.CashManagementNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.PatientID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.VoidReason, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentMetadata.PropertyNames.VoidReason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsExternalPayment, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsExternalPayment;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPaymentMetadata.ColumnNames.IsClosedVisiteDownPayment, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPaymentMetadata.PropertyNames.IsClosedVisiteDownPayment;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransPaymentMetadata Meta()
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
			public const string TransactionCode = "TransactionCode";
			public const string RegistrationNo = "RegistrationNo";
			public const string PaymentReferenceNo = "PaymentReferenceNo";
			public const string ReferenceNo = "ReferenceNo";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentTime = "PaymentTime";
			public const string PrintReceiptAsName = "PrintReceiptAsName";
			public const string TotalPaymentAmount = "TotalPaymentAmount";
			public const string RemainingAmount = "RemainingAmount";
			public const string PrintNumber = "PrintNumber";
			public const string PaymentReceiptNo = "PaymentReceiptNo";
			public const string CreatedBy = "CreatedBy";
			public const string IsPrinted = "IsPrinted";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsVisiteDownPayment = "IsVisiteDownPayment";
			public const string GuarantorID = "GuarantorID";
			public const string IsToGuarantor = "IsToGuarantor";
			public const string SRPromotion = "SRPromotion";
			public const string Initial = "Initial";
			public const string ReceiptIsReturned = "ReceiptIsReturned";
			public const string ApproveDate = "ApproveDate";
			public const string ApproveByUserID = "ApproveByUserID";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastPrintedDateTime = "LastPrintedDateTime";
			public const string LastPrintedByUserID = "LastPrintedByUserID";
			public const string IsGuarantorVerified = "IsGuarantorVerified";
			public const string IsPackagePaymentPerVisit = "IsPackagePaymentPerVisit";
			public const string CashManagementNo = "CashManagementNo";
			public const string PatientID = "PatientID";
			public const string VoidReason = "VoidReason";
			public const string IsExternalPayment = "IsExternalPayment";
			public const string IsClosedVisiteDownPayment = "IsClosedVisiteDownPayment";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PaymentNo = "PaymentNo";
			public const string TransactionCode = "TransactionCode";
			public const string RegistrationNo = "RegistrationNo";
			public const string PaymentReferenceNo = "PaymentReferenceNo";
			public const string ReferenceNo = "ReferenceNo";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentTime = "PaymentTime";
			public const string PrintReceiptAsName = "PrintReceiptAsName";
			public const string TotalPaymentAmount = "TotalPaymentAmount";
			public const string RemainingAmount = "RemainingAmount";
			public const string PrintNumber = "PrintNumber";
			public const string PaymentReceiptNo = "PaymentReceiptNo";
			public const string CreatedBy = "CreatedBy";
			public const string IsPrinted = "IsPrinted";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsVisiteDownPayment = "IsVisiteDownPayment";
			public const string GuarantorID = "GuarantorID";
			public const string IsToGuarantor = "IsToGuarantor";
			public const string SRPromotion = "SRPromotion";
			public const string Initial = "Initial";
			public const string ReceiptIsReturned = "ReceiptIsReturned";
			public const string ApproveDate = "ApproveDate";
			public const string ApproveByUserID = "ApproveByUserID";
			public const string VoidDate = "VoidDate";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastPrintedDateTime = "LastPrintedDateTime";
			public const string LastPrintedByUserID = "LastPrintedByUserID";
			public const string IsGuarantorVerified = "IsGuarantorVerified";
			public const string IsPackagePaymentPerVisit = "IsPackagePaymentPerVisit";
			public const string CashManagementNo = "CashManagementNo";
			public const string PatientID = "PatientID";
			public const string VoidReason = "VoidReason";
			public const string IsExternalPayment = "IsExternalPayment";
			public const string IsClosedVisiteDownPayment = "IsClosedVisiteDownPayment";
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
			lock (typeof(TransPaymentMetadata))
			{
				if (TransPaymentMetadata.mapDelegates == null)
				{
					TransPaymentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransPaymentMetadata.meta == null)
				{
					TransPaymentMetadata.meta = new TransPaymentMetadata();
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
				meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("PaymentTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PrintReceiptAsName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TotalPaymentAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RemainingAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrintNumber", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("PaymentReceiptNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPrinted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVisiteDownPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRPromotion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Initial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceiptIsReturned", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApproveDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApproveByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastPrintedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastPrintedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGuarantorVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPackagePaymentPerVisit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CashManagementNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsExternalPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClosedVisiteDownPayment", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "TransPayment";
				meta.Destination = "TransPayment";
				meta.spInsert = "proc_TransPaymentInsert";
				meta.spUpdate = "proc_TransPaymentUpdate";
				meta.spDelete = "proc_TransPaymentDelete";
				meta.spLoadAll = "proc_TransPaymentLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
