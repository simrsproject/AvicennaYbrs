/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/26/2022 4:47:02 PM
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
	abstract public class esBloodBankTransactionCollection : esEntityCollectionWAuditLog
	{
		public esBloodBankTransactionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BloodBankTransactionCollection";
		}

		#region Query Logic
		protected void InitQuery(esBloodBankTransactionQuery query)
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
			this.InitQuery(query as esBloodBankTransactionQuery);
		}
		#endregion

		virtual public BloodBankTransaction DetachEntity(BloodBankTransaction entity)
		{
			return base.DetachEntity(entity) as BloodBankTransaction;
		}

		virtual public BloodBankTransaction AttachEntity(BloodBankTransaction entity)
		{
			return base.AttachEntity(entity) as BloodBankTransaction;
		}

		virtual public void Combine(BloodBankTransactionCollection collection)
		{
			base.Combine(collection);
		}

		new public BloodBankTransaction this[int index]
		{
			get
			{
				return base[index] as BloodBankTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BloodBankTransaction);
		}
	}

	[Serializable]
	abstract public class esBloodBankTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBloodBankTransactionQuery GetDynamicQuery()
		{
			return null;
		}

		public esBloodBankTransaction()
		{
		}

		public esBloodBankTransaction(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esBloodBankTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "RequestDate": this.str.RequestDate = (string)value; break;
						case "RequestTime": this.str.RequestTime = (string)value; break;
						case "BloodBankNo": this.str.BloodBankNo = (string)value; break;
						case "PdutNo": this.str.PdutNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SRBloodGroupRequest": this.str.SRBloodGroupRequest = (string)value; break;
						case "HbResultValue": this.str.HbResultValue = (string)value; break;
						case "QtyBagRequest": this.str.QtyBagRequest = (string)value; break;
						case "VolumeBag": this.str.VolumeBag = (string)value; break;
						case "Diagnose": this.str.Diagnose = (string)value; break;
						case "Reason": this.str.Reason = (string)value; break;
						case "OfficerByUserID": this.str.OfficerByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsValidatedByCasemix": this.str.IsValidatedByCasemix = (string)value; break;
						case "ValidatedByCasemixDateTime": this.str.ValidatedByCasemixDateTime = (string)value; break;
						case "ValidatedByCasemixUserID": this.str.ValidatedByCasemixUserID = (string)value; break;
						case "IsBloodSampleGiven": this.str.IsBloodSampleGiven = (string)value; break;
						case "BloodSampleSubmittedDateTime": this.str.BloodSampleSubmittedDateTime = (string)value; break;
						case "BloodSampleSubmittedByUserID": this.str.BloodSampleSubmittedByUserID = (string)value; break;
						case "BloodSampleReceivedByUserID": this.str.BloodSampleReceivedByUserID = (string)value; break;
						case "BloodSampleReceivedDateTime": this.str.BloodSampleReceivedDateTime = (string)value; break;
						case "BloodSampleTakenDateTime": this.str.BloodSampleTakenDateTime = (string)value; break;
						case "BloodSampleTakenByUserID": this.str.BloodSampleTakenByUserID = (string)value; break;
						case "CasemixNotes": this.str.CasemixNotes = (string)value; break;
						case "QtyBagCasemixAppr": this.str.QtyBagCasemixAppr = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "RequestDate":

							if (value == null || value is System.DateTime)
								this.RequestDate = (System.DateTime?)value;
							break;
						case "HbResultValue":

							if (value == null || value is System.Decimal)
								this.HbResultValue = (System.Decimal?)value;
							break;
						case "QtyBagRequest":

							if (value == null || value is System.Int16)
								this.QtyBagRequest = (System.Int16?)value;
							break;
						case "VolumeBag":

							if (value == null || value is System.Decimal)
								this.VolumeBag = (System.Decimal?)value;
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
						case "IsValidatedByCasemix":

							if (value == null || value is System.Boolean)
								this.IsValidatedByCasemix = (System.Boolean?)value;
							break;
						case "ValidatedByCasemixDateTime":

							if (value == null || value is System.DateTime)
								this.ValidatedByCasemixDateTime = (System.DateTime?)value;
							break;
						case "IsBloodSampleGiven":

							if (value == null || value is System.Boolean)
								this.IsBloodSampleGiven = (System.Boolean?)value;
							break;
						case "BloodSampleSubmittedDateTime":

							if (value == null || value is System.DateTime)
								this.BloodSampleSubmittedDateTime = (System.DateTime?)value;
							break;
						case "BloodSampleReceivedDateTime":

							if (value == null || value is System.DateTime)
								this.BloodSampleReceivedDateTime = (System.DateTime?)value;
							break;
						case "BloodSampleTakenDateTime":

							if (value == null || value is System.DateTime)
								this.BloodSampleTakenDateTime = (System.DateTime?)value;
							break;
						case "QtyBagCasemixAppr":

							if (value == null || value is System.Decimal)
								this.QtyBagCasemixAppr = (System.Decimal?)value;
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
		/// Maps to BloodBankTransaction.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.RequestDate
		/// </summary>
		virtual public System.DateTime? RequestDate
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.RequestDate);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.RequestDate, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.RequestTime
		/// </summary>
		virtual public System.String RequestTime
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.RequestTime);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.RequestTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.BloodBankNo
		/// </summary>
		virtual public System.String BloodBankNo
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodBankNo);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodBankNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.PdutNo
		/// </summary>
		virtual public System.String PdutNo
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.PdutNo);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.PdutNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.SRBloodGroupRequest
		/// </summary>
		virtual public System.String SRBloodGroupRequest
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.SRBloodGroupRequest);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.SRBloodGroupRequest, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.HbResultValue
		/// </summary>
		virtual public System.Decimal? HbResultValue
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionMetadata.ColumnNames.HbResultValue);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionMetadata.ColumnNames.HbResultValue, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.QtyBagRequest
		/// </summary>
		virtual public System.Int16? QtyBagRequest
		{
			get
			{
				return base.GetSystemInt16(BloodBankTransactionMetadata.ColumnNames.QtyBagRequest);
			}

			set
			{
				base.SetSystemInt16(BloodBankTransactionMetadata.ColumnNames.QtyBagRequest, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.VolumeBag
		/// </summary>
		virtual public System.Decimal? VolumeBag
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionMetadata.ColumnNames.VolumeBag);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionMetadata.ColumnNames.VolumeBag, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.Diagnose
		/// </summary>
		virtual public System.String Diagnose
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.Diagnose);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.Diagnose, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.Reason);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.Reason, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.OfficerByUserID
		/// </summary>
		virtual public System.String OfficerByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.OfficerByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.OfficerByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.IsValidatedByCasemix
		/// </summary>
		virtual public System.Boolean? IsValidatedByCasemix
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsValidatedByCasemix);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsValidatedByCasemix, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.ValidatedByCasemixDateTime
		/// </summary>
		virtual public System.DateTime? ValidatedByCasemixDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.ValidatedByCasemixUserID
		/// </summary>
		virtual public System.String ValidatedByCasemixUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.IsBloodSampleGiven
		/// </summary>
		virtual public System.Boolean? IsBloodSampleGiven
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsBloodSampleGiven);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionMetadata.ColumnNames.IsBloodSampleGiven, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.BloodSampleSubmittedDateTime
		/// </summary>
		virtual public System.DateTime? BloodSampleSubmittedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.BloodSampleSubmittedByUserID
		/// </summary>
		virtual public System.String BloodSampleSubmittedByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.BloodSampleReceivedByUserID
		/// </summary>
		virtual public System.String BloodSampleReceivedByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.BloodSampleReceivedDateTime
		/// </summary>
		virtual public System.DateTime? BloodSampleReceivedDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.BloodSampleTakenDateTime
		/// </summary>
		virtual public System.DateTime? BloodSampleTakenDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.BloodSampleTakenByUserID
		/// </summary>
		virtual public System.String BloodSampleTakenByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.CasemixNotes
		/// </summary>
		virtual public System.String CasemixNotes
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionMetadata.ColumnNames.CasemixNotes);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionMetadata.ColumnNames.CasemixNotes, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransaction.QtyBagCasemixAppr
		/// </summary>
		virtual public System.Decimal? QtyBagCasemixAppr
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionMetadata.ColumnNames.QtyBagCasemixAppr);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionMetadata.ColumnNames.QtyBagCasemixAppr, value);
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
			public esStrings(esBloodBankTransaction entity)
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
			public System.String RequestDate
			{
				get
				{
					System.DateTime? data = entity.RequestDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDate = null;
					else entity.RequestDate = Convert.ToDateTime(value);
				}
			}
			public System.String RequestTime
			{
				get
				{
					System.String data = entity.RequestTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestTime = null;
					else entity.RequestTime = Convert.ToString(value);
				}
			}
			public System.String BloodBankNo
			{
				get
				{
					System.String data = entity.BloodBankNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodBankNo = null;
					else entity.BloodBankNo = Convert.ToString(value);
				}
			}
			public System.String PdutNo
			{
				get
				{
					System.String data = entity.PdutNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PdutNo = null;
					else entity.PdutNo = Convert.ToString(value);
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
			public System.String SRBloodGroupRequest
			{
				get
				{
					System.String data = entity.SRBloodGroupRequest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodGroupRequest = null;
					else entity.SRBloodGroupRequest = Convert.ToString(value);
				}
			}
			public System.String HbResultValue
			{
				get
				{
					System.Decimal? data = entity.HbResultValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HbResultValue = null;
					else entity.HbResultValue = Convert.ToDecimal(value);
				}
			}
			public System.String QtyBagRequest
			{
				get
				{
					System.Int16? data = entity.QtyBagRequest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyBagRequest = null;
					else entity.QtyBagRequest = Convert.ToInt16(value);
				}
			}
			public System.String VolumeBag
			{
				get
				{
					System.Decimal? data = entity.VolumeBag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VolumeBag = null;
					else entity.VolumeBag = Convert.ToDecimal(value);
				}
			}
			public System.String Diagnose
			{
				get
				{
					System.String data = entity.Diagnose;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Diagnose = null;
					else entity.Diagnose = Convert.ToString(value);
				}
			}
			public System.String Reason
			{
				get
				{
					System.String data = entity.Reason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reason = null;
					else entity.Reason = Convert.ToString(value);
				}
			}
			public System.String OfficerByUserID
			{
				get
				{
					System.String data = entity.OfficerByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OfficerByUserID = null;
					else entity.OfficerByUserID = Convert.ToString(value);
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
			public System.String IsValidatedByCasemix
			{
				get
				{
					System.Boolean? data = entity.IsValidatedByCasemix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidatedByCasemix = null;
					else entity.IsValidatedByCasemix = Convert.ToBoolean(value);
				}
			}
			public System.String ValidatedByCasemixDateTime
			{
				get
				{
					System.DateTime? data = entity.ValidatedByCasemixDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedByCasemixDateTime = null;
					else entity.ValidatedByCasemixDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidatedByCasemixUserID
			{
				get
				{
					System.String data = entity.ValidatedByCasemixUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedByCasemixUserID = null;
					else entity.ValidatedByCasemixUserID = Convert.ToString(value);
				}
			}
			public System.String IsBloodSampleGiven
			{
				get
				{
					System.Boolean? data = entity.IsBloodSampleGiven;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBloodSampleGiven = null;
					else entity.IsBloodSampleGiven = Convert.ToBoolean(value);
				}
			}
			public System.String BloodSampleSubmittedDateTime
			{
				get
				{
					System.DateTime? data = entity.BloodSampleSubmittedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodSampleSubmittedDateTime = null;
					else entity.BloodSampleSubmittedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String BloodSampleSubmittedByUserID
			{
				get
				{
					System.String data = entity.BloodSampleSubmittedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodSampleSubmittedByUserID = null;
					else entity.BloodSampleSubmittedByUserID = Convert.ToString(value);
				}
			}
			public System.String BloodSampleReceivedByUserID
			{
				get
				{
					System.String data = entity.BloodSampleReceivedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodSampleReceivedByUserID = null;
					else entity.BloodSampleReceivedByUserID = Convert.ToString(value);
				}
			}
			public System.String BloodSampleReceivedDateTime
			{
				get
				{
					System.DateTime? data = entity.BloodSampleReceivedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodSampleReceivedDateTime = null;
					else entity.BloodSampleReceivedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String BloodSampleTakenDateTime
			{
				get
				{
					System.DateTime? data = entity.BloodSampleTakenDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodSampleTakenDateTime = null;
					else entity.BloodSampleTakenDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String BloodSampleTakenByUserID
			{
				get
				{
					System.String data = entity.BloodSampleTakenByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodSampleTakenByUserID = null;
					else entity.BloodSampleTakenByUserID = Convert.ToString(value);
				}
			}
			public System.String CasemixNotes
			{
				get
				{
					System.String data = entity.CasemixNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixNotes = null;
					else entity.CasemixNotes = Convert.ToString(value);
				}
			}
			public System.String QtyBagCasemixAppr
			{
				get
				{
					System.Decimal? data = entity.QtyBagCasemixAppr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyBagCasemixAppr = null;
					else entity.QtyBagCasemixAppr = Convert.ToDecimal(value);
				}
			}
			private esBloodBankTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBloodBankTransactionQuery query)
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
				throw new Exception("esBloodBankTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BloodBankTransaction : esBloodBankTransaction
	{
	}

	[Serializable]
	abstract public class esBloodBankTransactionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BloodBankTransactionMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RequestDate
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.RequestDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RequestTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.RequestTime, esSystemType.String);
			}
		}

		public esQueryItem BloodBankNo
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.BloodBankNo, esSystemType.String);
			}
		}

		public esQueryItem PdutNo
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.PdutNo, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SRBloodGroupRequest
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.SRBloodGroupRequest, esSystemType.String);
			}
		}

		public esQueryItem HbResultValue
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.HbResultValue, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyBagRequest
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.QtyBagRequest, esSystemType.Int16);
			}
		}

		public esQueryItem VolumeBag
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.VolumeBag, esSystemType.Decimal);
			}
		}

		public esQueryItem Diagnose
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.Diagnose, esSystemType.String);
			}
		}

		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.Reason, esSystemType.String);
			}
		}

		public esQueryItem OfficerByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.OfficerByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidatedByCasemix
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.IsValidatedByCasemix, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidatedByCasemixDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidatedByCasemixUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixUserID, esSystemType.String);
			}
		}

		public esQueryItem IsBloodSampleGiven
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.IsBloodSampleGiven, esSystemType.Boolean);
			}
		}

		public esQueryItem BloodSampleSubmittedDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem BloodSampleSubmittedByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedByUserID, esSystemType.String);
			}
		}

		public esQueryItem BloodSampleReceivedByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedByUserID, esSystemType.String);
			}
		}

		public esQueryItem BloodSampleReceivedDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem BloodSampleTakenDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem BloodSampleTakenByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenByUserID, esSystemType.String);
			}
		}

		public esQueryItem CasemixNotes
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.CasemixNotes, esSystemType.String);
			}
		}

		public esQueryItem QtyBagCasemixAppr
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionMetadata.ColumnNames.QtyBagCasemixAppr, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BloodBankTransactionCollection")]
	public partial class BloodBankTransactionCollection : esBloodBankTransactionCollection, IEnumerable<BloodBankTransaction>
	{
		public BloodBankTransactionCollection()
		{

		}

		public static implicit operator List<BloodBankTransaction>(BloodBankTransactionCollection coll)
		{
			List<BloodBankTransaction> list = new List<BloodBankTransaction>();

			foreach (BloodBankTransaction emp in coll)
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
				return BloodBankTransactionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodBankTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BloodBankTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BloodBankTransaction();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BloodBankTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodBankTransactionQuery();
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
		public bool Load(BloodBankTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BloodBankTransaction AddNew()
		{
			BloodBankTransaction entity = base.AddNewEntity() as BloodBankTransaction;

			return entity;
		}
		public BloodBankTransaction FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as BloodBankTransaction;
		}

		#region IEnumerable< BloodBankTransaction> Members

		IEnumerator<BloodBankTransaction> IEnumerable<BloodBankTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as BloodBankTransaction;
			}
		}

		#endregion

		private BloodBankTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BloodBankTransaction' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BloodBankTransaction ({TransactionNo})")]
	[Serializable]
	public partial class BloodBankTransaction : esBloodBankTransaction
	{
		public BloodBankTransaction()
		{
		}

		public BloodBankTransaction(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BloodBankTransactionMetadata.Meta();
			}
		}

		override protected esBloodBankTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodBankTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BloodBankTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodBankTransactionQuery();
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
		public bool Load(BloodBankTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BloodBankTransactionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BloodBankTransactionQuery : esBloodBankTransactionQuery
	{
		public BloodBankTransactionQuery()
		{

		}

		public BloodBankTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BloodBankTransactionQuery";
		}
	}

	[Serializable]
	public partial class BloodBankTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BloodBankTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.TransactionDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.RequestDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.RequestDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.RequestTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.RequestTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.BloodBankNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.BloodBankNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.PdutNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.PdutNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.RegistrationNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.SRBloodGroupRequest, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.SRBloodGroupRequest;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.HbResultValue, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.HbResultValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.QtyBagRequest, 9, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.QtyBagRequest;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.VolumeBag, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.VolumeBag;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.Diagnose, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.Diagnose;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.Reason, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.OfficerByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.OfficerByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.IsApproved, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.ApprovedDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.ApprovedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.IsVoid, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.VoidDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.VoidByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.LastUpdateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.LastUpdateByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.IsValidatedByCasemix, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.IsValidatedByCasemix;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.ValidatedByCasemixDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.ValidatedByCasemixUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.ValidatedByCasemixUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.IsBloodSampleGiven, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.IsBloodSampleGiven;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.BloodSampleSubmittedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.BloodSampleSubmittedByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.BloodSampleSubmittedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.BloodSampleReceivedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.BloodSampleReceivedDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.BloodSampleReceivedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenDateTime, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.BloodSampleTakenDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.BloodSampleTakenByUserID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.BloodSampleTakenByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.CasemixNotes, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.CasemixNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionMetadata.ColumnNames.QtyBagCasemixAppr, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionMetadata.PropertyNames.QtyBagCasemixAppr;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BloodBankTransactionMetadata Meta()
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
			public const string TransactionDate = "TransactionDate";
			public const string RequestDate = "RequestDate";
			public const string RequestTime = "RequestTime";
			public const string BloodBankNo = "BloodBankNo";
			public const string PdutNo = "PdutNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string SRBloodGroupRequest = "SRBloodGroupRequest";
			public const string HbResultValue = "HbResultValue";
			public const string QtyBagRequest = "QtyBagRequest";
			public const string VolumeBag = "VolumeBag";
			public const string Diagnose = "Diagnose";
			public const string Reason = "Reason";
			public const string OfficerByUserID = "OfficerByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsValidatedByCasemix = "IsValidatedByCasemix";
			public const string ValidatedByCasemixDateTime = "ValidatedByCasemixDateTime";
			public const string ValidatedByCasemixUserID = "ValidatedByCasemixUserID";
			public const string IsBloodSampleGiven = "IsBloodSampleGiven";
			public const string BloodSampleSubmittedDateTime = "BloodSampleSubmittedDateTime";
			public const string BloodSampleSubmittedByUserID = "BloodSampleSubmittedByUserID";
			public const string BloodSampleReceivedByUserID = "BloodSampleReceivedByUserID";
			public const string BloodSampleReceivedDateTime = "BloodSampleReceivedDateTime";
			public const string BloodSampleTakenDateTime = "BloodSampleTakenDateTime";
			public const string BloodSampleTakenByUserID = "BloodSampleTakenByUserID";
			public const string CasemixNotes = "CasemixNotes";
			public const string QtyBagCasemixAppr = "QtyBagCasemixAppr";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string RequestDate = "RequestDate";
			public const string RequestTime = "RequestTime";
			public const string BloodBankNo = "BloodBankNo";
			public const string PdutNo = "PdutNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string SRBloodGroupRequest = "SRBloodGroupRequest";
			public const string HbResultValue = "HbResultValue";
			public const string QtyBagRequest = "QtyBagRequest";
			public const string VolumeBag = "VolumeBag";
			public const string Diagnose = "Diagnose";
			public const string Reason = "Reason";
			public const string OfficerByUserID = "OfficerByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsValidatedByCasemix = "IsValidatedByCasemix";
			public const string ValidatedByCasemixDateTime = "ValidatedByCasemixDateTime";
			public const string ValidatedByCasemixUserID = "ValidatedByCasemixUserID";
			public const string IsBloodSampleGiven = "IsBloodSampleGiven";
			public const string BloodSampleSubmittedDateTime = "BloodSampleSubmittedDateTime";
			public const string BloodSampleSubmittedByUserID = "BloodSampleSubmittedByUserID";
			public const string BloodSampleReceivedByUserID = "BloodSampleReceivedByUserID";
			public const string BloodSampleReceivedDateTime = "BloodSampleReceivedDateTime";
			public const string BloodSampleTakenDateTime = "BloodSampleTakenDateTime";
			public const string BloodSampleTakenByUserID = "BloodSampleTakenByUserID";
			public const string CasemixNotes = "CasemixNotes";
			public const string QtyBagCasemixAppr = "QtyBagCasemixAppr";
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
			lock (typeof(BloodBankTransactionMetadata))
			{
				if (BloodBankTransactionMetadata.mapDelegates == null)
				{
					BloodBankTransactionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BloodBankTransactionMetadata.meta == null)
				{
					BloodBankTransactionMetadata.meta = new BloodBankTransactionMetadata();
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
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RequestDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RequestTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BloodBankNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PdutNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodGroupRequest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HbResultValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyBagRequest", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("VolumeBag", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Diagnose", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OfficerByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidatedByCasemix", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidatedByCasemixDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidatedByCasemixUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsBloodSampleGiven", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BloodSampleSubmittedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BloodSampleSubmittedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BloodSampleReceivedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BloodSampleReceivedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BloodSampleTakenDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BloodSampleTakenByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CasemixNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyBagCasemixAppr", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "BloodBankTransaction";
				meta.Destination = "BloodBankTransaction";
				meta.spInsert = "proc_BloodBankTransactionInsert";
				meta.spUpdate = "proc_BloodBankTransactionUpdate";
				meta.spDelete = "proc_BloodBankTransactionDelete";
				meta.spLoadAll = "proc_BloodBankTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_BloodBankTransactionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BloodBankTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
