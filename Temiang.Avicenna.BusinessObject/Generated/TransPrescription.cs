/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/9/2022 9:18:33 PM
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
	abstract public class esTransPrescriptionCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransPrescriptionCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPrescriptionQuery query)
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
			this.InitQuery(query as esTransPrescriptionQuery);
		}
		#endregion

		virtual public TransPrescription DetachEntity(TransPrescription entity)
		{
			return base.DetachEntity(entity) as TransPrescription;
		}

		virtual public TransPrescription AttachEntity(TransPrescription entity)
		{
			return base.AttachEntity(entity) as TransPrescription;
		}

		virtual public void Combine(TransPrescriptionCollection collection)
		{
			base.Combine(collection);
		}

		new public TransPrescription this[int index]
		{
			get
			{
				return base[index] as TransPrescription;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescription);
		}
	}

	[Serializable]
	abstract public class esTransPrescription : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPrescription()
		{
		}

		public esTransPrescription(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String prescriptionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(prescriptionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(prescriptionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String prescriptionNo)
		{
			esTransPrescriptionQuery query = this.GetDynamicQuery();
			query.Where(query.PrescriptionNo == prescriptionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PrescriptionNo", prescriptionNo);
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
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
						case "PrescriptionDate": this.str.PrescriptionDate = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "IsApproval": this.str.IsApproval = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPrescriptionReturn": this.str.IsPrescriptionReturn = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "IsFromSOAP": this.str.IsFromSOAP = (string)value; break;
						case "IsBillProceed": this.str.IsBillProceed = (string)value; break;
						case "IsUnitDosePrescription": this.str.IsUnitDosePrescription = (string)value; break;
						case "IsCito": this.str.IsCito = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ApprovalDateTime": this.str.ApprovalDateTime = (string)value; break;
						case "DeliverDateTime": this.str.DeliverDateTime = (string)value; break;
						case "TextPrescription": this.str.TextPrescription = (string)value; break;
						case "IsDirect": this.str.IsDirect = (string)value; break;
						case "IsPaid": this.str.IsPaid = (string)value; break;
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "IsProceedByPharmacist": this.str.IsProceedByPharmacist = (string)value; break;
						case "FullAddress": this.str.FullAddress = (string)value; break;
						case "NoTelp": this.str.NoTelp = (string)value; break;
						case "AdditionalNote": this.str.AdditionalNote = (string)value; break;
						case "SRFloor": this.str.SRFloor = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "QtyR": this.str.QtyR = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsPrinted": this.str.IsPrinted = (string)value; break;
						case "FloorSeqNo": this.str.FloorSeqNo = (string)value; break;
						case "Rtype": this.str.Rtype = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "FromRoomID": this.str.FromRoomID = (string)value; break;
						case "FromBedID": this.str.FromBedID = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
						case "CompleteDateTime": this.str.CompleteDateTime = (string)value; break;
						case "VoidReason": this.str.VoidReason = (string)value; break;
						case "ExecutionDate": this.str.ExecutionDate = (string)value; break;
						case "IsPos": this.str.IsPos = (string)value; break;
						case "IsForTakeItHome": this.str.IsForTakeItHome = (string)value; break;
						case "PatientEducationSeqNo": this.str.PatientEducationSeqNo = (string)value; break;
						case "ReviewByUserID": this.str.ReviewByUserID = (string)value; break;
						case "IsUnapproved": this.str.IsUnapproved = (string)value; break;
						case "UnapprovedDateTime": this.str.UnapprovedDateTime = (string)value; break;
						case "UnapprovedByUserID": this.str.UnapprovedByUserID = (string)value; break;
						case "SRKioskQueueType": this.str.SRKioskQueueType = (string)value; break;
						case "KioskQueueNo": this.str.KioskQueueNo = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "SRPrescriptionCategory": this.str.SRPrescriptionCategory = (string)value; break;
						case "RasproSeqNo": this.str.RasproSeqNo = (string)value; break;
						case "IsReviewed": this.str.IsReviewed = (string)value; break;
						case "ReviewedByUserID": this.str.ReviewedByUserID = (string)value; break;
						case "ReviewedDateTime": this.str.ReviewedDateTime = (string)value; break;
						case "Is23Days": this.str.Is23Days = (string)value; break;
						case "IsSplitBill": this.str.IsSplitBill = (string)value; break;
						case "IsCash": this.str.IsCash = (string)value; break;
						case "DeliverByUserID": this.str.DeliverByUserID = (string)value; break;
						case "CompleteByUserID": this.str.CompleteByUserID = (string)value; break;
						case "InProgressByUserID": this.str.InProgressByUserID = (string)value; break;
						case "InProgressDateTime": this.str.InProgressDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PrescriptionDate":

							if (value == null || value is System.DateTime)
								this.PrescriptionDate = (System.DateTime?)value;
							break;
						case "IsApproval":

							if (value == null || value is System.Boolean)
								this.IsApproval = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPrescriptionReturn":

							if (value == null || value is System.Boolean)
								this.IsPrescriptionReturn = (System.Boolean?)value;
							break;
						case "IsFromSOAP":

							if (value == null || value is System.Boolean)
								this.IsFromSOAP = (System.Boolean?)value;
							break;
						case "IsBillProceed":

							if (value == null || value is System.Boolean)
								this.IsBillProceed = (System.Boolean?)value;
							break;
						case "IsUnitDosePrescription":

							if (value == null || value is System.Boolean)
								this.IsUnitDosePrescription = (System.Boolean?)value;
							break;
						case "IsCito":

							if (value == null || value is System.Boolean)
								this.IsCito = (System.Boolean?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "ApprovalDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovalDateTime = (System.DateTime?)value;
							break;
						case "DeliverDateTime":

							if (value == null || value is System.DateTime)
								this.DeliverDateTime = (System.DateTime?)value;
							break;
						case "IsDirect":

							if (value == null || value is System.Boolean)
								this.IsDirect = (System.Boolean?)value;
							break;
						case "IsPaid":

							if (value == null || value is System.Boolean)
								this.IsPaid = (System.Boolean?)value;
							break;
						case "IsProceedByPharmacist":

							if (value == null || value is System.Boolean)
								this.IsProceedByPharmacist = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "QtyR":

							if (value == null || value is System.Int16)
								this.QtyR = (System.Int16?)value;
							break;
						case "IsPrinted":

							if (value == null || value is System.Boolean)
								this.IsPrinted = (System.Boolean?)value;
							break;
						case "FloorSeqNo":

							if (value == null || value is System.Int32)
								this.FloorSeqNo = (System.Int32?)value;
							break;
						case "CompleteDateTime":

							if (value == null || value is System.DateTime)
								this.CompleteDateTime = (System.DateTime?)value;
							break;
						case "ExecutionDate":

							if (value == null || value is System.DateTime)
								this.ExecutionDate = (System.DateTime?)value;
							break;
						case "IsPos":

							if (value == null || value is System.Boolean)
								this.IsPos = (System.Boolean?)value;
							break;
						case "IsForTakeItHome":

							if (value == null || value is System.Boolean)
								this.IsForTakeItHome = (System.Boolean?)value;
							break;
						case "PatientEducationSeqNo":

							if (value == null || value is System.Int32)
								this.PatientEducationSeqNo = (System.Int32?)value;
							break;
						case "IsUnapproved":

							if (value == null || value is System.Boolean)
								this.IsUnapproved = (System.Boolean?)value;
							break;
						case "UnapprovedDateTime":

							if (value == null || value is System.DateTime)
								this.UnapprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "RasproSeqNo":

							if (value == null || value is System.Int32)
								this.RasproSeqNo = (System.Int32?)value;
							break;
						case "IsReviewed":

							if (value == null || value is System.Boolean)
								this.IsReviewed = (System.Boolean?)value;
							break;
						case "ReviewedDateTime":

							if (value == null || value is System.DateTime)
								this.ReviewedDateTime = (System.DateTime?)value;
							break;
						case "Is23Days":

							if (value == null || value is System.Boolean)
								this.Is23Days = (System.Boolean?)value;
							break;
						case "IsSplitBill":

							if (value == null || value is System.Boolean)
								this.IsSplitBill = (System.Boolean?)value;
							break;
						case "IsCash":

							if (value == null || value is System.Boolean)
								this.IsCash = (System.Boolean?)value;
							break;
						case "InProgressDateTime":

							if (value == null || value is System.DateTime)
								this.InProgressDateTime = (System.DateTime?)value;
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
		/// Maps to TransPrescription.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.PrescriptionNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.PrescriptionDate
		/// </summary>
		virtual public System.DateTime? PrescriptionDate
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.PrescriptionDate);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.PrescriptionDate, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsApproval
		/// </summary>
		virtual public System.Boolean? IsApproval
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsApproval);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsApproval, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsPrescriptionReturn
		/// </summary>
		virtual public System.Boolean? IsPrescriptionReturn
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPrescriptionReturn);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPrescriptionReturn, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsFromSOAP
		/// </summary>
		virtual public System.Boolean? IsFromSOAP
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsFromSOAP);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsFromSOAP, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsBillProceed
		/// </summary>
		virtual public System.Boolean? IsBillProceed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsBillProceed);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsBillProceed, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsUnitDosePrescription
		/// </summary>
		virtual public System.Boolean? IsUnitDosePrescription
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsUnitDosePrescription);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsUnitDosePrescription, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsCito
		/// </summary>
		virtual public System.Boolean? IsCito
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsCito);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsCito, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ApprovalDateTime
		/// </summary>
		virtual public System.DateTime? ApprovalDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.ApprovalDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.ApprovalDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.DeliverDateTime
		/// </summary>
		virtual public System.DateTime? DeliverDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.DeliverDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.DeliverDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.TextPrescription
		/// </summary>
		virtual public System.String TextPrescription
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.TextPrescription);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.TextPrescription, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsDirect
		/// </summary>
		virtual public System.Boolean? IsDirect
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsDirect);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsDirect, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsPaid
		/// </summary>
		virtual public System.Boolean? IsPaid
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPaid);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPaid, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.OrderNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsProceedByPharmacist
		/// </summary>
		virtual public System.Boolean? IsProceedByPharmacist
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsProceedByPharmacist);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsProceedByPharmacist, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.FullAddress
		/// </summary>
		virtual public System.String FullAddress
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.FullAddress);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.FullAddress, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.NoTelp
		/// </summary>
		virtual public System.String NoTelp
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.NoTelp);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.NoTelp, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.AdditionalNote
		/// </summary>
		virtual public System.String AdditionalNote
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.AdditionalNote);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.AdditionalNote, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.SRFloor
		/// </summary>
		virtual public System.String SRFloor
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.SRFloor);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.SRFloor, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.QtyR
		/// </summary>
		virtual public System.Int16? QtyR
		{
			get
			{
				return base.GetSystemInt16(TransPrescriptionMetadata.ColumnNames.QtyR);
			}

			set
			{
				base.SetSystemInt16(TransPrescriptionMetadata.ColumnNames.QtyR, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsPrinted
		/// </summary>
		virtual public System.Boolean? IsPrinted
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPrinted);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPrinted, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.FloorSeqNo
		/// </summary>
		virtual public System.Int32? FloorSeqNo
		{
			get
			{
				return base.GetSystemInt32(TransPrescriptionMetadata.ColumnNames.FloorSeqNo);
			}

			set
			{
				base.SetSystemInt32(TransPrescriptionMetadata.ColumnNames.FloorSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.Rtype
		/// </summary>
		virtual public System.String Rtype
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.Rtype);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.Rtype, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.FromRoomID
		/// </summary>
		virtual public System.String FromRoomID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.FromRoomID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.FromRoomID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.FromBedID
		/// </summary>
		virtual public System.String FromBedID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.FromBedID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.FromBedID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.CompleteDateTime
		/// </summary>
		virtual public System.DateTime? CompleteDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.CompleteDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.CompleteDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.VoidReason
		/// </summary>
		virtual public System.String VoidReason
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.VoidReason);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.VoidReason, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ExecutionDate
		/// </summary>
		virtual public System.DateTime? ExecutionDate
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.ExecutionDate);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.ExecutionDate, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsPos
		/// </summary>
		virtual public System.Boolean? IsPos
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPos);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsPos, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsForTakeItHome
		/// </summary>
		virtual public System.Boolean? IsForTakeItHome
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsForTakeItHome);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsForTakeItHome, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.PatientEducationSeqNo
		/// </summary>
		virtual public System.Int32? PatientEducationSeqNo
		{
			get
			{
				return base.GetSystemInt32(TransPrescriptionMetadata.ColumnNames.PatientEducationSeqNo);
			}

			set
			{
				base.SetSystemInt32(TransPrescriptionMetadata.ColumnNames.PatientEducationSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ReviewByUserID
		/// </summary>
		virtual public System.String ReviewByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.ReviewByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.ReviewByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsUnapproved
		/// </summary>
		virtual public System.Boolean? IsUnapproved
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsUnapproved);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsUnapproved, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.UnapprovedDateTime
		/// </summary>
		virtual public System.DateTime? UnapprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.UnapprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.UnapprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.UnapprovedByUserID
		/// </summary>
		virtual public System.String UnapprovedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.UnapprovedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.UnapprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.SRKioskQueueType
		/// </summary>
		virtual public System.String SRKioskQueueType
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.SRKioskQueueType);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.SRKioskQueueType, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.KioskQueueNo
		/// </summary>
		virtual public System.String KioskQueueNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.KioskQueueNo);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.KioskQueueNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.SRPrescriptionCategory
		/// </summary>
		virtual public System.String SRPrescriptionCategory
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.SRPrescriptionCategory);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.SRPrescriptionCategory, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.RasproSeqNo
		/// </summary>
		virtual public System.Int32? RasproSeqNo
		{
			get
			{
				return base.GetSystemInt32(TransPrescriptionMetadata.ColumnNames.RasproSeqNo);
			}

			set
			{
				base.SetSystemInt32(TransPrescriptionMetadata.ColumnNames.RasproSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsReviewed
		/// </summary>
		virtual public System.Boolean? IsReviewed
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsReviewed);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsReviewed, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ReviewedByUserID
		/// </summary>
		virtual public System.String ReviewedByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.ReviewedByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.ReviewedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.ReviewedDateTime
		/// </summary>
		virtual public System.DateTime? ReviewedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.ReviewedDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.ReviewedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.Is23Days
		/// </summary>
		virtual public System.Boolean? Is23Days
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.Is23Days);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.Is23Days, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsSplitBill
		/// </summary>
		virtual public System.Boolean? IsSplitBill
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsSplitBill);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsSplitBill, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.IsCash
		/// </summary>
		virtual public System.Boolean? IsCash
		{
			get
			{
				return base.GetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsCash);
			}

			set
			{
				base.SetSystemBoolean(TransPrescriptionMetadata.ColumnNames.IsCash, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.DeliverByUserID
		/// </summary>
		virtual public System.String DeliverByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.DeliverByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.DeliverByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.CompleteByUserID
		/// </summary>
		virtual public System.String CompleteByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.CompleteByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.CompleteByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.InProgressByUserID
		/// </summary>
		virtual public System.String InProgressByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionMetadata.ColumnNames.InProgressByUserID);
			}

			set
			{
				base.SetSystemString(TransPrescriptionMetadata.ColumnNames.InProgressByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransPrescription.InProgressDateTime
		/// </summary>
		virtual public System.DateTime? InProgressDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionMetadata.ColumnNames.InProgressDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransPrescriptionMetadata.ColumnNames.InProgressDateTime, value);
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
			public esStrings(esTransPrescription entity)
			{
				this.entity = entity;
			}
			public System.String PrescriptionNo
			{
				get
				{
					System.String data = entity.PrescriptionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionNo = null;
					else entity.PrescriptionNo = Convert.ToString(value);
				}
			}
			public System.String PrescriptionDate
			{
				get
				{
					System.DateTime? data = entity.PrescriptionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionDate = null;
					else entity.PrescriptionDate = Convert.ToDateTime(value);
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
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String IsApproval
			{
				get
				{
					System.Boolean? data = entity.IsApproval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproval = null;
					else entity.IsApproval = Convert.ToBoolean(value);
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
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			public System.String IsPrescriptionReturn
			{
				get
				{
					System.Boolean? data = entity.IsPrescriptionReturn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrescriptionReturn = null;
					else entity.IsPrescriptionReturn = Convert.ToBoolean(value);
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
			public System.String IsFromSOAP
			{
				get
				{
					System.Boolean? data = entity.IsFromSOAP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromSOAP = null;
					else entity.IsFromSOAP = Convert.ToBoolean(value);
				}
			}
			public System.String IsBillProceed
			{
				get
				{
					System.Boolean? data = entity.IsBillProceed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBillProceed = null;
					else entity.IsBillProceed = Convert.ToBoolean(value);
				}
			}
			public System.String IsUnitDosePrescription
			{
				get
				{
					System.Boolean? data = entity.IsUnitDosePrescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUnitDosePrescription = null;
					else entity.IsUnitDosePrescription = Convert.ToBoolean(value);
				}
			}
			public System.String IsCito
			{
				get
				{
					System.Boolean? data = entity.IsCito;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCito = null;
					else entity.IsCito = Convert.ToBoolean(value);
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
			public System.String ApprovalDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovalDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovalDateTime = null;
					else entity.ApprovalDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String DeliverDateTime
			{
				get
				{
					System.DateTime? data = entity.DeliverDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeliverDateTime = null;
					else entity.DeliverDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String TextPrescription
			{
				get
				{
					System.String data = entity.TextPrescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TextPrescription = null;
					else entity.TextPrescription = Convert.ToString(value);
				}
			}
			public System.String IsDirect
			{
				get
				{
					System.Boolean? data = entity.IsDirect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDirect = null;
					else entity.IsDirect = Convert.ToBoolean(value);
				}
			}
			public System.String IsPaid
			{
				get
				{
					System.Boolean? data = entity.IsPaid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPaid = null;
					else entity.IsPaid = Convert.ToBoolean(value);
				}
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String IsProceedByPharmacist
			{
				get
				{
					System.Boolean? data = entity.IsProceedByPharmacist;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProceedByPharmacist = null;
					else entity.IsProceedByPharmacist = Convert.ToBoolean(value);
				}
			}
			public System.String FullAddress
			{
				get
				{
					System.String data = entity.FullAddress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FullAddress = null;
					else entity.FullAddress = Convert.ToString(value);
				}
			}
			public System.String NoTelp
			{
				get
				{
					System.String data = entity.NoTelp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoTelp = null;
					else entity.NoTelp = Convert.ToString(value);
				}
			}
			public System.String AdditionalNote
			{
				get
				{
					System.String data = entity.AdditionalNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdditionalNote = null;
					else entity.AdditionalNote = Convert.ToString(value);
				}
			}
			public System.String SRFloor
			{
				get
				{
					System.String data = entity.SRFloor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFloor = null;
					else entity.SRFloor = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String QtyR
			{
				get
				{
					System.Int16? data = entity.QtyR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyR = null;
					else entity.QtyR = Convert.ToInt16(value);
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
			public System.String FloorSeqNo
			{
				get
				{
					System.Int32? data = entity.FloorSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FloorSeqNo = null;
					else entity.FloorSeqNo = Convert.ToInt32(value);
				}
			}
			public System.String Rtype
			{
				get
				{
					System.String data = entity.Rtype;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rtype = null;
					else entity.Rtype = Convert.ToString(value);
				}
			}
			public System.String FromServiceUnitID
			{
				get
				{
					System.String data = entity.FromServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
					else entity.FromServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String FromRoomID
			{
				get
				{
					System.String data = entity.FromRoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRoomID = null;
					else entity.FromRoomID = Convert.ToString(value);
				}
			}
			public System.String FromBedID
			{
				get
				{
					System.String data = entity.FromBedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromBedID = null;
					else entity.FromBedID = Convert.ToString(value);
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
			public System.String CompleteDateTime
			{
				get
				{
					System.DateTime? data = entity.CompleteDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompleteDateTime = null;
					else entity.CompleteDateTime = Convert.ToDateTime(value);
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
			public System.String ExecutionDate
			{
				get
				{
					System.DateTime? data = entity.ExecutionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExecutionDate = null;
					else entity.ExecutionDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsPos
			{
				get
				{
					System.Boolean? data = entity.IsPos;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPos = null;
					else entity.IsPos = Convert.ToBoolean(value);
				}
			}
			public System.String IsForTakeItHome
			{
				get
				{
					System.Boolean? data = entity.IsForTakeItHome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsForTakeItHome = null;
					else entity.IsForTakeItHome = Convert.ToBoolean(value);
				}
			}
			public System.String PatientEducationSeqNo
			{
				get
				{
					System.Int32? data = entity.PatientEducationSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientEducationSeqNo = null;
					else entity.PatientEducationSeqNo = Convert.ToInt32(value);
				}
			}
			public System.String ReviewByUserID
			{
				get
				{
					System.String data = entity.ReviewByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReviewByUserID = null;
					else entity.ReviewByUserID = Convert.ToString(value);
				}
			}
			public System.String IsUnapproved
			{
				get
				{
					System.Boolean? data = entity.IsUnapproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUnapproved = null;
					else entity.IsUnapproved = Convert.ToBoolean(value);
				}
			}
			public System.String UnapprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.UnapprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnapprovedDateTime = null;
					else entity.UnapprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String UnapprovedByUserID
			{
				get
				{
					System.String data = entity.UnapprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnapprovedByUserID = null;
					else entity.UnapprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String SRKioskQueueType
			{
				get
				{
					System.String data = entity.SRKioskQueueType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRKioskQueueType = null;
					else entity.SRKioskQueueType = Convert.ToString(value);
				}
			}
			public System.String KioskQueueNo
			{
				get
				{
					System.String data = entity.KioskQueueNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KioskQueueNo = null;
					else entity.KioskQueueNo = Convert.ToString(value);
				}
			}
			public System.String IsVerified
			{
				get
				{
					System.Boolean? data = entity.IsVerified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerified = null;
					else entity.IsVerified = Convert.ToBoolean(value);
				}
			}
			public System.String VerifiedByUserID
			{
				get
				{
					System.String data = entity.VerifiedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
					else entity.VerifiedByUserID = Convert.ToString(value);
				}
			}
			public System.String VerifiedDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
					else entity.VerifiedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SRPrescriptionCategory
			{
				get
				{
					System.String data = entity.SRPrescriptionCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPrescriptionCategory = null;
					else entity.SRPrescriptionCategory = Convert.ToString(value);
				}
			}
			public System.String RasproSeqNo
			{
				get
				{
					System.Int32? data = entity.RasproSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasproSeqNo = null;
					else entity.RasproSeqNo = Convert.ToInt32(value);
				}
			}
			public System.String IsReviewed
			{
				get
				{
					System.Boolean? data = entity.IsReviewed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReviewed = null;
					else entity.IsReviewed = Convert.ToBoolean(value);
				}
			}
			public System.String ReviewedByUserID
			{
				get
				{
					System.String data = entity.ReviewedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReviewedByUserID = null;
					else entity.ReviewedByUserID = Convert.ToString(value);
				}
			}
			public System.String ReviewedDateTime
			{
				get
				{
					System.DateTime? data = entity.ReviewedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReviewedDateTime = null;
					else entity.ReviewedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Is23Days
			{
				get
				{
					System.Boolean? data = entity.Is23Days;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Is23Days = null;
					else entity.Is23Days = Convert.ToBoolean(value);
				}
			}
			public System.String IsSplitBill
			{
				get
				{
					System.Boolean? data = entity.IsSplitBill;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSplitBill = null;
					else entity.IsSplitBill = Convert.ToBoolean(value);
				}
			}
			public System.String IsCash
			{
				get
				{
					System.Boolean? data = entity.IsCash;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCash = null;
					else entity.IsCash = Convert.ToBoolean(value);
				}
			}
			public System.String DeliverByUserID
			{
				get
				{
					System.String data = entity.DeliverByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeliverByUserID = null;
					else entity.DeliverByUserID = Convert.ToString(value);
				}
			}
			public System.String CompleteByUserID
			{
				get
				{
					System.String data = entity.CompleteByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompleteByUserID = null;
					else entity.CompleteByUserID = Convert.ToString(value);
				}
			}
			public System.String InProgressByUserID
			{
				get
				{
					System.String data = entity.InProgressByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InProgressByUserID = null;
					else entity.InProgressByUserID = Convert.ToString(value);
				}
			}
			public System.String InProgressDateTime
			{
				get
				{
					System.DateTime? data = entity.InProgressDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InProgressDateTime = null;
					else entity.InProgressDateTime = Convert.ToDateTime(value);
				}
			}
			private esTransPrescription entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionQuery query)
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
				throw new Exception("esTransPrescription can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransPrescription : esTransPrescription
	{
	}

	[Serializable]
	abstract public class esTransPrescriptionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionMetadata.Meta();
			}
		}

		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionDate
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.PrescriptionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem IsApproval
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsApproval, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPrescriptionReturn
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsPrescriptionReturn, esSystemType.Boolean);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem IsFromSOAP
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsFromSOAP, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBillProceed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsBillProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUnitDosePrescription
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsUnitDosePrescription, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCito
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsCito, esSystemType.Boolean);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovalDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ApprovalDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem DeliverDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.DeliverDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem TextPrescription
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.TextPrescription, esSystemType.String);
			}
		}

		public esQueryItem IsDirect
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsDirect, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPaid
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsPaid, esSystemType.Boolean);
			}
		}

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		}

		public esQueryItem IsProceedByPharmacist
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsProceedByPharmacist, esSystemType.Boolean);
			}
		}

		public esQueryItem FullAddress
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.FullAddress, esSystemType.String);
			}
		}

		public esQueryItem NoTelp
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.NoTelp, esSystemType.String);
			}
		}

		public esQueryItem AdditionalNote
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.AdditionalNote, esSystemType.String);
			}
		}

		public esQueryItem SRFloor
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.SRFloor, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem QtyR
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.QtyR, esSystemType.Int16);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPrinted
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsPrinted, esSystemType.Boolean);
			}
		}

		public esQueryItem FloorSeqNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.FloorSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem Rtype
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.Rtype, esSystemType.String);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem FromRoomID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.FromRoomID, esSystemType.String);
			}
		}

		public esQueryItem FromBedID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.FromBedID, esSystemType.String);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem CompleteDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.CompleteDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidReason
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.VoidReason, esSystemType.String);
			}
		}

		public esQueryItem ExecutionDate
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ExecutionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsPos
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsPos, esSystemType.Boolean);
			}
		}

		public esQueryItem IsForTakeItHome
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsForTakeItHome, esSystemType.Boolean);
			}
		}

		public esQueryItem PatientEducationSeqNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.PatientEducationSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem ReviewByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ReviewByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsUnapproved
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsUnapproved, esSystemType.Boolean);
			}
		}

		public esQueryItem UnapprovedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.UnapprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem UnapprovedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.UnapprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRKioskQueueType
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.SRKioskQueueType, esSystemType.String);
			}
		}

		public esQueryItem KioskQueueNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.KioskQueueNo, esSystemType.String);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SRPrescriptionCategory
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.SRPrescriptionCategory, esSystemType.String);
			}
		}

		public esQueryItem RasproSeqNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.RasproSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem IsReviewed
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsReviewed, esSystemType.Boolean);
			}
		}

		public esQueryItem ReviewedByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ReviewedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReviewedDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.ReviewedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem Is23Days
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.Is23Days, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSplitBill
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsSplitBill, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCash
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.IsCash, esSystemType.Boolean);
			}
		}

		public esQueryItem DeliverByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.DeliverByUserID, esSystemType.String);
			}
		}

		public esQueryItem CompleteByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.CompleteByUserID, esSystemType.String);
			}
		}

		public esQueryItem InProgressByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.InProgressByUserID, esSystemType.String);
			}
		}

		public esQueryItem InProgressDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionMetadata.ColumnNames.InProgressDateTime, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionCollection")]
	public partial class TransPrescriptionCollection : esTransPrescriptionCollection, IEnumerable<TransPrescription>
	{
		public TransPrescriptionCollection()
		{

		}

		public static implicit operator List<TransPrescription>(TransPrescriptionCollection coll)
		{
			List<TransPrescription> list = new List<TransPrescription>();

			foreach (TransPrescription emp in coll)
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
				return TransPrescriptionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescription(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescription();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransPrescriptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionQuery();
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
		public bool Load(TransPrescriptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransPrescription AddNew()
		{
			TransPrescription entity = base.AddNewEntity() as TransPrescription;

			return entity;
		}
		public TransPrescription FindByPrimaryKey(String prescriptionNo)
		{
			return base.FindByPrimaryKey(prescriptionNo) as TransPrescription;
		}

		#region IEnumerable< TransPrescription> Members

		IEnumerator<TransPrescription> IEnumerable<TransPrescription>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescription;
			}
		}

		#endregion

		private TransPrescriptionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescription' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransPrescription ({PrescriptionNo})")]
	[Serializable]
	public partial class TransPrescription : esTransPrescription
	{
		public TransPrescription()
		{
		}

		public TransPrescription(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionMetadata.Meta();
			}
		}

		override protected esTransPrescriptionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransPrescriptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionQuery();
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
		public bool Load(TransPrescriptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransPrescriptionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransPrescriptionQuery : esTransPrescriptionQuery
	{
		public TransPrescriptionQuery()
		{

		}

		public TransPrescriptionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransPrescriptionQuery";
		}
	}

	[Serializable]
	public partial class TransPrescriptionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.PrescriptionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.PrescriptionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.PrescriptionDate;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ClassID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ParamedicID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsApproval, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsApproval;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.Note, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsPrescriptionReturn, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsPrescriptionReturn;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ReferenceNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsFromSOAP, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsFromSOAP;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsBillProceed, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsBillProceed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsUnitDosePrescription, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsUnitDosePrescription;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsCito, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsCito;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsClosed, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsClosed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ApprovalDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ApprovalDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.DeliverDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.DeliverDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.TextPrescription, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.TextPrescription;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsDirect, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsDirect;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsPaid, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsPaid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.OrderNo, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.OrderNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsProceedByPharmacist, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsProceedByPharmacist;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.FullAddress, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.FullAddress;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.NoTelp, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.NoTelp;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.AdditionalNote, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.AdditionalNote;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.SRFloor, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.SRFloor;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.CreatedDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.CreatedByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.QtyR, 31, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.QtyR;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ApprovedByUserID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsPrinted, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsPrinted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.FloorSeqNo, 34, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.FloorSeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.Rtype, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.Rtype;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.FromServiceUnitID, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.FromRoomID, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.FromRoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.FromBedID, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.FromBedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.LocationID, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.CompleteDateTime, 40, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.CompleteDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.VoidReason, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.VoidReason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ExecutionDate, 42, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ExecutionDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsPos, 43, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsPos;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsForTakeItHome, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsForTakeItHome;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.PatientEducationSeqNo, 45, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.PatientEducationSeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ReviewByUserID, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ReviewByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsUnapproved, 47, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsUnapproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.UnapprovedDateTime, 48, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.UnapprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.UnapprovedByUserID, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.UnapprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.SRKioskQueueType, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.SRKioskQueueType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.KioskQueueNo, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.KioskQueueNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsVerified, 52, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.VerifiedByUserID, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.VerifiedDateTime, 54, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.SRPrescriptionCategory, 55, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.SRPrescriptionCategory;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.RasproSeqNo, 56, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.RasproSeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsReviewed, 57, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsReviewed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ReviewedByUserID, 58, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ReviewedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.ReviewedDateTime, 59, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.ReviewedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.Is23Days, 60, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.Is23Days;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsSplitBill, 61, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsSplitBill;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.IsCash, 62, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.IsCash;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.DeliverByUserID, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.DeliverByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.CompleteByUserID, 64, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.CompleteByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.InProgressByUserID, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.InProgressByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransPrescriptionMetadata.ColumnNames.InProgressDateTime, 66, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionMetadata.PropertyNames.InProgressDateTime;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransPrescriptionMetadata Meta()
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
			public const string PrescriptionNo = "PrescriptionNo";
			public const string PrescriptionDate = "PrescriptionDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ClassID = "ClassID";
			public const string ParamedicID = "ParamedicID";
			public const string IsApproval = "IsApproval";
			public const string IsVoid = "IsVoid";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPrescriptionReturn = "IsPrescriptionReturn";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsFromSOAP = "IsFromSOAP";
			public const string IsBillProceed = "IsBillProceed";
			public const string IsUnitDosePrescription = "IsUnitDosePrescription";
			public const string IsCito = "IsCito";
			public const string IsClosed = "IsClosed";
			public const string ApprovalDateTime = "ApprovalDateTime";
			public const string DeliverDateTime = "DeliverDateTime";
			public const string TextPrescription = "TextPrescription";
			public const string IsDirect = "IsDirect";
			public const string IsPaid = "IsPaid";
			public const string OrderNo = "OrderNo";
			public const string IsProceedByPharmacist = "IsProceedByPharmacist";
			public const string FullAddress = "FullAddress";
			public const string NoTelp = "NoTelp";
			public const string AdditionalNote = "AdditionalNote";
			public const string SRFloor = "SRFloor";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string QtyR = "QtyR";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsPrinted = "IsPrinted";
			public const string FloorSeqNo = "FloorSeqNo";
			public const string Rtype = "Rtype";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string FromBedID = "FromBedID";
			public const string LocationID = "LocationID";
			public const string CompleteDateTime = "CompleteDateTime";
			public const string VoidReason = "VoidReason";
			public const string ExecutionDate = "ExecutionDate";
			public const string IsPos = "IsPos";
			public const string IsForTakeItHome = "IsForTakeItHome";
			public const string PatientEducationSeqNo = "PatientEducationSeqNo";
			public const string ReviewByUserID = "ReviewByUserID";
			public const string IsUnapproved = "IsUnapproved";
			public const string UnapprovedDateTime = "UnapprovedDateTime";
			public const string UnapprovedByUserID = "UnapprovedByUserID";
			public const string SRKioskQueueType = "SRKioskQueueType";
			public const string KioskQueueNo = "KioskQueueNo";
			public const string IsVerified = "IsVerified";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string SRPrescriptionCategory = "SRPrescriptionCategory";
			public const string RasproSeqNo = "RasproSeqNo";
			public const string IsReviewed = "IsReviewed";
			public const string ReviewedByUserID = "ReviewedByUserID";
			public const string ReviewedDateTime = "ReviewedDateTime";
			public const string Is23Days = "Is23Days";
			public const string IsSplitBill = "IsSplitBill";
			public const string IsCash = "IsCash";
			public const string DeliverByUserID = "DeliverByUserID";
			public const string CompleteByUserID = "CompleteByUserID";
			public const string InProgressByUserID = "InProgressByUserID";
			public const string InProgressDateTime = "InProgressDateTime";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PrescriptionNo = "PrescriptionNo";
			public const string PrescriptionDate = "PrescriptionDate";
			public const string RegistrationNo = "RegistrationNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ClassID = "ClassID";
			public const string ParamedicID = "ParamedicID";
			public const string IsApproval = "IsApproval";
			public const string IsVoid = "IsVoid";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPrescriptionReturn = "IsPrescriptionReturn";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsFromSOAP = "IsFromSOAP";
			public const string IsBillProceed = "IsBillProceed";
			public const string IsUnitDosePrescription = "IsUnitDosePrescription";
			public const string IsCito = "IsCito";
			public const string IsClosed = "IsClosed";
			public const string ApprovalDateTime = "ApprovalDateTime";
			public const string DeliverDateTime = "DeliverDateTime";
			public const string TextPrescription = "TextPrescription";
			public const string IsDirect = "IsDirect";
			public const string IsPaid = "IsPaid";
			public const string OrderNo = "OrderNo";
			public const string IsProceedByPharmacist = "IsProceedByPharmacist";
			public const string FullAddress = "FullAddress";
			public const string NoTelp = "NoTelp";
			public const string AdditionalNote = "AdditionalNote";
			public const string SRFloor = "SRFloor";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string QtyR = "QtyR";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsPrinted = "IsPrinted";
			public const string FloorSeqNo = "FloorSeqNo";
			public const string Rtype = "Rtype";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string FromBedID = "FromBedID";
			public const string LocationID = "LocationID";
			public const string CompleteDateTime = "CompleteDateTime";
			public const string VoidReason = "VoidReason";
			public const string ExecutionDate = "ExecutionDate";
			public const string IsPos = "IsPos";
			public const string IsForTakeItHome = "IsForTakeItHome";
			public const string PatientEducationSeqNo = "PatientEducationSeqNo";
			public const string ReviewByUserID = "ReviewByUserID";
			public const string IsUnapproved = "IsUnapproved";
			public const string UnapprovedDateTime = "UnapprovedDateTime";
			public const string UnapprovedByUserID = "UnapprovedByUserID";
			public const string SRKioskQueueType = "SRKioskQueueType";
			public const string KioskQueueNo = "KioskQueueNo";
			public const string IsVerified = "IsVerified";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string SRPrescriptionCategory = "SRPrescriptionCategory";
			public const string RasproSeqNo = "RasproSeqNo";
			public const string IsReviewed = "IsReviewed";
			public const string ReviewedByUserID = "ReviewedByUserID";
			public const string ReviewedDateTime = "ReviewedDateTime";
			public const string Is23Days = "Is23Days";
			public const string IsSplitBill = "IsSplitBill";
			public const string IsCash = "IsCash";
			public const string DeliverByUserID = "DeliverByUserID";
			public const string CompleteByUserID = "CompleteByUserID";
			public const string InProgressByUserID = "InProgressByUserID";
			public const string InProgressDateTime = "InProgressDateTime";
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
			lock (typeof(TransPrescriptionMetadata))
			{
				if (TransPrescriptionMetadata.mapDelegates == null)
				{
					TransPrescriptionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransPrescriptionMetadata.meta == null)
				{
					TransPrescriptionMetadata.meta = new TransPrescriptionMetadata();
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

				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproval", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPrescriptionReturn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsFromSOAP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBillProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUnitDosePrescription", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCito", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovalDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DeliverDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TextPrescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDirect", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPaid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProceedByPharmacist", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FullAddress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoTelp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdditionalNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFloor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyR", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPrinted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FloorSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Rtype", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromBedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CompleteDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExecutionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsPos", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsForTakeItHome", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PatientEducationSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ReviewByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUnapproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("UnapprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("UnapprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRKioskQueueType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KioskQueueNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRPrescriptionCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RasproSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsReviewed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReviewedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReviewedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Is23Days", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSplitBill", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCash", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeliverByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CompleteByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InProgressByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InProgressDateTime", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "TransPrescription";
				meta.Destination = "TransPrescription";
				meta.spInsert = "proc_TransPrescriptionInsert";
				meta.spUpdate = "proc_TransPrescriptionUpdate";
				meta.spDelete = "proc_TransPrescriptionDelete";
				meta.spLoadAll = "proc_TransPrescriptionLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
