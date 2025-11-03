/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/26/2023 1:10:24 PM
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
	abstract public class esServiceUnitBookingCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitBookingCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ServiceUnitBookingCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitBookingQuery query)
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
			this.InitQuery(query as esServiceUnitBookingQuery);
		}
		#endregion

		virtual public ServiceUnitBooking DetachEntity(ServiceUnitBooking entity)
		{
			return base.DetachEntity(entity) as ServiceUnitBooking;
		}

		virtual public ServiceUnitBooking AttachEntity(ServiceUnitBooking entity)
		{
			return base.AttachEntity(entity) as ServiceUnitBooking;
		}

		virtual public void Combine(ServiceUnitBookingCollection collection)
		{
			base.Combine(collection);
		}

		new public ServiceUnitBooking this[int index]
		{
			get
			{
				return base[index] as ServiceUnitBooking;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitBooking);
		}
	}

	[Serializable]
	abstract public class esServiceUnitBooking : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitBookingQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitBooking()
		{
		}

		public esServiceUnitBooking(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String bookingNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bookingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bookingNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bookingNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bookingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bookingNo);
		}

		private bool LoadByPrimaryKeyDynamic(String bookingNo)
		{
			esServiceUnitBookingQuery query = this.GetDynamicQuery();
			query.Where(query.BookingNo == bookingNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String bookingNo)
		{
			esParameters parms = new esParameters();
			parms.Add("BookingNo", bookingNo);
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
						case "BookingNo": this.str.BookingNo = (string)value; break;
						case "BookingDateTimeFrom": this.str.BookingDateTimeFrom = (string)value; break;
						case "BookingDateTimeTo": this.str.BookingDateTimeTo = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;
						case "LastCreateByUserID": this.str.LastCreateByUserID = (string)value; break;
						case "SRAnestesiPlan": this.str.SRAnestesiPlan = (string)value; break;
						case "ParamedicID2": this.str.ParamedicID2 = (string)value; break;
						case "ParamedicID3": this.str.ParamedicID3 = (string)value; break;
						case "ParamedicID4": this.str.ParamedicID4 = (string)value; break;
						case "ParamedicIDAnestesi": this.str.ParamedicIDAnestesi = (string)value; break;
						case "AssistantID1": this.str.AssistantID1 = (string)value; break;
						case "AssistantID2": this.str.AssistantID2 = (string)value; break;
						case "AssistantID3": this.str.AssistantID3 = (string)value; break;
						case "AssistantID4": this.str.AssistantID4 = (string)value; break;
						case "AssistantIDAnestesi": this.str.AssistantIDAnestesi = (string)value; break;
						case "Diagnose": this.str.Diagnose = (string)value; break;
						case "OperationType": this.str.OperationType = (string)value; break;
						case "IsCito": this.str.IsCito = (string)value; break;
						case "ProcedureChargeClassID": this.str.ProcedureChargeClassID = (string)value; break;
						case "OperatingNotes": this.str.OperatingNotes = (string)value; break;
						case "Instrumentator1": this.str.Instrumentator1 = (string)value; break;
						case "Instrumentator2": this.str.Instrumentator2 = (string)value; break;
						case "SRProcedureCategory": this.str.SRProcedureCategory = (string)value; break;
						case "RealizationDateTimeFrom": this.str.RealizationDateTimeFrom = (string)value; break;
						case "RealizationDateTimeTo": this.str.RealizationDateTimeTo = (string)value; break;
						case "Resident": this.str.Resident = (string)value; break;
						case "AssistantIDInstrumentator": this.str.AssistantIDInstrumentator = (string)value; break;
						case "SmfID": this.str.SmfID = (string)value; break;
						case "IsExtendedSurgery": this.str.IsExtendedSurgery = (string)value; break;
						case "SRIndication": this.str.SRIndication = (string)value; break;
						case "IsNeedPa": this.str.IsNeedPa = (string)value; break;
						case "AssistantID5": this.str.AssistantID5 = (string)value; break;
						case "AssistantIDInstrumentator2": this.str.AssistantIDInstrumentator2 = (string)value; break;
						case "AssistantIDInstrumentator3": this.str.AssistantIDInstrumentator3 = (string)value; break;
						case "AssistantIDInstrumentator4": this.str.AssistantIDInstrumentator4 = (string)value; break;
						case "AssistantIDInstrumentator5": this.str.AssistantIDInstrumentator5 = (string)value; break;
						case "Instrumentator3": this.str.Instrumentator3 = (string)value; break;
						case "Instrumentator4": this.str.Instrumentator4 = (string)value; break;
						case "Instrumentator5": this.str.Instrumentator5 = (string)value; break;
						case "AnestesyNotes": this.str.AnestesyNotes = (string)value; break;
						case "SRProcedure1": this.str.SRProcedure1 = (string)value; break;
						case "SRProcedure2": this.str.SRProcedure2 = (string)value; break;
						case "PostDiagnosis": this.str.PostDiagnosis = (string)value; break;
						case "PaDate": this.str.PaDate = (string)value; break;
						case "SourceOfTissue": this.str.SourceOfTissue = (string)value; break;
						case "ArrivedDateTime": this.str.ArrivedDateTime = (string)value; break;
						case "IsAnestheticConversion": this.str.IsAnestheticConversion = (string)value; break;
						case "IsValidate": this.str.IsValidate = (string)value; break;
						case "ValidateDateTime": this.str.ValidateDateTime = (string)value; break;
						case "ValidateByUserID": this.str.ValidateByUserID = (string)value; break;
						case "PreSurgeryDateTime": this.str.PreSurgeryDateTime = (string)value; break;
						case "PreSurgeryByUserID": this.str.PreSurgeryByUserID = (string)value; break;
						case "AnesthesiaDateTime": this.str.AnesthesiaDateTime = (string)value; break;
						case "AnesthesiaByUserID": this.str.AnesthesiaByUserID = (string)value; break;
						case "SurgeryDateTime": this.str.SurgeryDateTime = (string)value; break;
						case "SurgeryByUserID": this.str.SurgeryByUserID = (string)value; break;
						case "PostSurgeryDateTime": this.str.PostSurgeryDateTime = (string)value; break;
						case "PostSurgeryByUserID": this.str.PostSurgeryByUserID = (string)value; break;
						case "VoidReason": this.str.VoidReason = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "IsInsertionImplant": this.str.IsInsertionImplant = (string)value; break;
						case "IncisionDateTime": this.str.IncisionDateTime = (string)value; break;
						case "SRProcedureDiagnoseType": this.str.SRProcedureDiagnoseType = (string)value; break;
						case "MoveToTheWardDateTime": this.str.MoveToTheWardDateTime = (string)value; break;
						case "MoveToTheWardByUserID": this.str.MoveToTheWardByUserID = (string)value; break;
						case "AnestPostSurgeryInstructions": this.str.AnestPostSurgeryInstructions = (string)value; break;
						case "AmountOfBleeding": this.str.AmountOfBleeding = (string)value; break;
						case "AmountOfTransfusions": this.str.AmountOfTransfusions = (string)value; break;
						case "AssistantIDAnestesi2": this.str.AssistantIDAnestesi2 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "BookingDateTimeFrom":

							if (value == null || value is System.DateTime)
								this.BookingDateTimeFrom = (System.DateTime?)value;
							break;
						case "BookingDateTimeTo":

							if (value == null || value is System.DateTime)
								this.BookingDateTimeTo = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LastCreateDateTime":

							if (value == null || value is System.DateTime)
								this.LastCreateDateTime = (System.DateTime?)value;
							break;
						case "IsCito":

							if (value == null || value is System.Boolean)
								this.IsCito = (System.Boolean?)value;
							break;
						case "RealizationDateTimeFrom":

							if (value == null || value is System.DateTime)
								this.RealizationDateTimeFrom = (System.DateTime?)value;
							break;
						case "RealizationDateTimeTo":

							if (value == null || value is System.DateTime)
								this.RealizationDateTimeTo = (System.DateTime?)value;
							break;
						case "IsExtendedSurgery":

							if (value == null || value is System.Boolean)
								this.IsExtendedSurgery = (System.Boolean?)value;
							break;
						case "IsNeedPa":

							if (value == null || value is System.Boolean)
								this.IsNeedPa = (System.Boolean?)value;
							break;
						case "PaDate":

							if (value == null || value is System.DateTime)
								this.PaDate = (System.DateTime?)value;
							break;
						case "ArrivedDateTime":

							if (value == null || value is System.DateTime)
								this.ArrivedDateTime = (System.DateTime?)value;
							break;
						case "IsAnestheticConversion":

							if (value == null || value is System.Boolean)
								this.IsAnestheticConversion = (System.Boolean?)value;
							break;
						case "IsValidate":

							if (value == null || value is System.Boolean)
								this.IsValidate = (System.Boolean?)value;
							break;
						case "ValidateDateTime":

							if (value == null || value is System.DateTime)
								this.ValidateDateTime = (System.DateTime?)value;
							break;
						case "PreSurgeryDateTime":

							if (value == null || value is System.DateTime)
								this.PreSurgeryDateTime = (System.DateTime?)value;
							break;
						case "AnesthesiaDateTime":

							if (value == null || value is System.DateTime)
								this.AnesthesiaDateTime = (System.DateTime?)value;
							break;
						case "SurgeryDateTime":

							if (value == null || value is System.DateTime)
								this.SurgeryDateTime = (System.DateTime?)value;
							break;
						case "PostSurgeryDateTime":

							if (value == null || value is System.DateTime)
								this.PostSurgeryDateTime = (System.DateTime?)value;
							break;
						case "IsInsertionImplant":

							if (value == null || value is System.Boolean)
								this.IsInsertionImplant = (System.Boolean?)value;
							break;
						case "IncisionDateTime":

							if (value == null || value is System.DateTime)
								this.IncisionDateTime = (System.DateTime?)value;
							break;
						case "MoveToTheWardDateTime":

							if (value == null || value is System.DateTime)
								this.MoveToTheWardDateTime = (System.DateTime?)value;
							break;
						case "AnesthesiologistSign":

							if (value == null || value is System.Byte[])
								this.AnesthesiologistSign = (System.Byte[])value;
							break;
						case "AmountOfBleeding":

							if (value == null || value is System.Decimal)
								this.AmountOfBleeding = (System.Decimal?)value;
							break;
						case "AmountOfTransfusions":

							if (value == null || value is System.Decimal)
								this.AmountOfTransfusions = (System.Decimal?)value;
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
		/// Maps to ServiceUnitBooking.BookingNo
		/// </summary>
		virtual public System.String BookingNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.BookingNo);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.BookingNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.BookingDateTimeFrom
		/// </summary>
		virtual public System.DateTime? BookingDateTimeFrom
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeFrom);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeFrom, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.BookingDateTimeTo
		/// </summary>
		virtual public System.DateTime? BookingDateTimeTo
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeTo);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeTo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.LastCreateDateTime
		/// </summary>
		virtual public System.DateTime? LastCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.LastCreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.LastCreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.LastCreateByUserID
		/// </summary>
		virtual public System.String LastCreateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.LastCreateByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.LastCreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SRAnestesiPlan
		/// </summary>
		virtual public System.String SRAnestesiPlan
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRAnestesiPlan);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRAnestesiPlan, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ParamedicID2
		/// </summary>
		virtual public System.String ParamedicID2
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID2);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID2, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ParamedicID3
		/// </summary>
		virtual public System.String ParamedicID3
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID3);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID3, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ParamedicID4
		/// </summary>
		virtual public System.String ParamedicID4
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID4);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicID4, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ParamedicIDAnestesi
		/// </summary>
		virtual public System.String ParamedicIDAnestesi
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicIDAnestesi);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ParamedicIDAnestesi, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantID1
		/// </summary>
		virtual public System.String AssistantID1
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID1);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID1, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantID2
		/// </summary>
		virtual public System.String AssistantID2
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID2);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID2, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantID3
		/// </summary>
		virtual public System.String AssistantID3
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID3);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID3, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantID4
		/// </summary>
		virtual public System.String AssistantID4
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID4);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID4, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantIDAnestesi
		/// </summary>
		virtual public System.String AssistantIDAnestesi
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Diagnose
		/// </summary>
		virtual public System.String Diagnose
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Diagnose);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Diagnose, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.OperationType
		/// </summary>
		virtual public System.String OperationType
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.OperationType);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.OperationType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsCito
		/// </summary>
		virtual public System.Boolean? IsCito
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsCito);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsCito, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ProcedureChargeClassID
		/// </summary>
		virtual public System.String ProcedureChargeClassID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ProcedureChargeClassID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ProcedureChargeClassID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.OperatingNotes
		/// </summary>
		virtual public System.String OperatingNotes
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.OperatingNotes);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.OperatingNotes, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Instrumentator1
		/// </summary>
		virtual public System.String Instrumentator1
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator1);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator1, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Instrumentator2
		/// </summary>
		virtual public System.String Instrumentator2
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator2);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator2, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SRProcedureCategory
		/// </summary>
		virtual public System.String SRProcedureCategory
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedureCategory);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedureCategory, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.RealizationDateTimeFrom
		/// </summary>
		virtual public System.DateTime? RealizationDateTimeFrom
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeFrom);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeFrom, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.RealizationDateTimeTo
		/// </summary>
		virtual public System.DateTime? RealizationDateTimeTo
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeTo);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeTo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Resident
		/// </summary>
		virtual public System.String Resident
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Resident);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Resident, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantIDInstrumentator
		/// </summary>
		virtual public System.String AssistantIDInstrumentator
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SmfID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsExtendedSurgery
		/// </summary>
		virtual public System.Boolean? IsExtendedSurgery
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsExtendedSurgery);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsExtendedSurgery, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SRIndication
		/// </summary>
		virtual public System.String SRIndication
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRIndication);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRIndication, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsNeedPa
		/// </summary>
		virtual public System.Boolean? IsNeedPa
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsNeedPa);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsNeedPa, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantID5
		/// </summary>
		virtual public System.String AssistantID5
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID5);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantID5, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantIDInstrumentator2
		/// </summary>
		virtual public System.String AssistantIDInstrumentator2
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator2);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator2, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantIDInstrumentator3
		/// </summary>
		virtual public System.String AssistantIDInstrumentator3
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator3);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator3, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantIDInstrumentator4
		/// </summary>
		virtual public System.String AssistantIDInstrumentator4
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator4);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator4, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantIDInstrumentator5
		/// </summary>
		virtual public System.String AssistantIDInstrumentator5
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator5);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator5, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Instrumentator3
		/// </summary>
		virtual public System.String Instrumentator3
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator3);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator3, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Instrumentator4
		/// </summary>
		virtual public System.String Instrumentator4
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator4);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator4, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.Instrumentator5
		/// </summary>
		virtual public System.String Instrumentator5
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator5);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.Instrumentator5, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AnestesyNotes
		/// </summary>
		virtual public System.String AnestesyNotes
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AnestesyNotes);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AnestesyNotes, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SRProcedure1
		/// </summary>
		virtual public System.String SRProcedure1
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedure1);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedure1, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SRProcedure2
		/// </summary>
		virtual public System.String SRProcedure2
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedure2);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedure2, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.PostDiagnosis
		/// </summary>
		virtual public System.String PostDiagnosis
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.PostDiagnosis);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.PostDiagnosis, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.PaDate
		/// </summary>
		virtual public System.DateTime? PaDate
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.PaDate);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.PaDate, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SourceOfTissue
		/// </summary>
		virtual public System.String SourceOfTissue
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SourceOfTissue);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SourceOfTissue, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ArrivedDateTime
		/// </summary>
		virtual public System.DateTime? ArrivedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.ArrivedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.ArrivedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsAnestheticConversion
		/// </summary>
		virtual public System.Boolean? IsAnestheticConversion
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsAnestheticConversion);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsAnestheticConversion, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsValidate
		/// </summary>
		virtual public System.Boolean? IsValidate
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsValidate);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsValidate, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ValidateDateTime
		/// </summary>
		virtual public System.DateTime? ValidateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.ValidateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.ValidateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.ValidateByUserID
		/// </summary>
		virtual public System.String ValidateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.ValidateByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.ValidateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.PreSurgeryDateTime
		/// </summary>
		virtual public System.DateTime? PreSurgeryDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.PreSurgeryDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.PreSurgeryDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.PreSurgeryByUserID
		/// </summary>
		virtual public System.String PreSurgeryByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.PreSurgeryByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.PreSurgeryByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AnesthesiaDateTime
		/// </summary>
		virtual public System.DateTime? AnesthesiaDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.AnesthesiaDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.AnesthesiaDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AnesthesiaByUserID
		/// </summary>
		virtual public System.String AnesthesiaByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AnesthesiaByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AnesthesiaByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SurgeryDateTime
		/// </summary>
		virtual public System.DateTime? SurgeryDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.SurgeryDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.SurgeryDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SurgeryByUserID
		/// </summary>
		virtual public System.String SurgeryByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SurgeryByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SurgeryByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.PostSurgeryDateTime
		/// </summary>
		virtual public System.DateTime? PostSurgeryDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.PostSurgeryDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.PostSurgeryDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.PostSurgeryByUserID
		/// </summary>
		virtual public System.String PostSurgeryByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.PostSurgeryByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.PostSurgeryByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.VoidReason
		/// </summary>
		virtual public System.String VoidReason
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.VoidReason);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.VoidReason, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IsInsertionImplant
		/// </summary>
		virtual public System.Boolean? IsInsertionImplant
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsInsertionImplant);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitBookingMetadata.ColumnNames.IsInsertionImplant, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.IncisionDateTime
		/// </summary>
		virtual public System.DateTime? IncisionDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.IncisionDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.IncisionDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.SRProcedureDiagnoseType
		/// </summary>
		virtual public System.String SRProcedureDiagnoseType
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedureDiagnoseType);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.SRProcedureDiagnoseType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.MoveToTheWardDateTime
		/// </summary>
		virtual public System.DateTime? MoveToTheWardDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.MoveToTheWardByUserID
		/// </summary>
		virtual public System.String MoveToTheWardByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AnesthesiologistSign
		/// </summary>
		virtual public System.Byte[] AnesthesiologistSign
		{
			get
			{
				return base.GetSystemByteArray(ServiceUnitBookingMetadata.ColumnNames.AnesthesiologistSign);
			}

			set
			{
				base.SetSystemByteArray(ServiceUnitBookingMetadata.ColumnNames.AnesthesiologistSign, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AnestPostSurgeryInstructions
		/// </summary>
		virtual public System.String AnestPostSurgeryInstructions
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AnestPostSurgeryInstructions);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AnestPostSurgeryInstructions, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AmountOfBleeding
		/// </summary>
		virtual public System.Decimal? AmountOfBleeding
		{
			get
			{
				return base.GetSystemDecimal(ServiceUnitBookingMetadata.ColumnNames.AmountOfBleeding);
			}

			set
			{
				base.SetSystemDecimal(ServiceUnitBookingMetadata.ColumnNames.AmountOfBleeding, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AmountOfTransfusions
		/// </summary>
		virtual public System.Decimal? AmountOfTransfusions
		{
			get
			{
				return base.GetSystemDecimal(ServiceUnitBookingMetadata.ColumnNames.AmountOfTransfusions);
			}

			set
			{
				base.SetSystemDecimal(ServiceUnitBookingMetadata.ColumnNames.AmountOfTransfusions, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitBooking.AssistantIDAnestesi2
		/// </summary>
		virtual public System.String AssistantIDAnestesi2
		{
			get
			{
				return base.GetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi2);
			}

			set
			{
				base.SetSystemString(ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi2, value);
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
			public esStrings(esServiceUnitBooking entity)
			{
				this.entity = entity;
			}
			public System.String BookingNo
			{
				get
				{
					System.String data = entity.BookingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingNo = null;
					else entity.BookingNo = Convert.ToString(value);
				}
			}
			public System.String BookingDateTimeFrom
			{
				get
				{
					System.DateTime? data = entity.BookingDateTimeFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingDateTimeFrom = null;
					else entity.BookingDateTimeFrom = Convert.ToDateTime(value);
				}
			}
			public System.String BookingDateTimeTo
			{
				get
				{
					System.DateTime? data = entity.BookingDateTimeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingDateTimeTo = null;
					else entity.BookingDateTimeTo = Convert.ToDateTime(value);
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
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
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
			public System.String LastCreateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
					else entity.LastCreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastCreateByUserID
			{
				get
				{
					System.String data = entity.LastCreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateByUserID = null;
					else entity.LastCreateByUserID = Convert.ToString(value);
				}
			}
			public System.String SRAnestesiPlan
			{
				get
				{
					System.String data = entity.SRAnestesiPlan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAnestesiPlan = null;
					else entity.SRAnestesiPlan = Convert.ToString(value);
				}
			}
			public System.String ParamedicID2
			{
				get
				{
					System.String data = entity.ParamedicID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID2 = null;
					else entity.ParamedicID2 = Convert.ToString(value);
				}
			}
			public System.String ParamedicID3
			{
				get
				{
					System.String data = entity.ParamedicID3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID3 = null;
					else entity.ParamedicID3 = Convert.ToString(value);
				}
			}
			public System.String ParamedicID4
			{
				get
				{
					System.String data = entity.ParamedicID4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID4 = null;
					else entity.ParamedicID4 = Convert.ToString(value);
				}
			}
			public System.String ParamedicIDAnestesi
			{
				get
				{
					System.String data = entity.ParamedicIDAnestesi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicIDAnestesi = null;
					else entity.ParamedicIDAnestesi = Convert.ToString(value);
				}
			}
			public System.String AssistantID1
			{
				get
				{
					System.String data = entity.AssistantID1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantID1 = null;
					else entity.AssistantID1 = Convert.ToString(value);
				}
			}
			public System.String AssistantID2
			{
				get
				{
					System.String data = entity.AssistantID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantID2 = null;
					else entity.AssistantID2 = Convert.ToString(value);
				}
			}
			public System.String AssistantID3
			{
				get
				{
					System.String data = entity.AssistantID3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantID3 = null;
					else entity.AssistantID3 = Convert.ToString(value);
				}
			}
			public System.String AssistantID4
			{
				get
				{
					System.String data = entity.AssistantID4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantID4 = null;
					else entity.AssistantID4 = Convert.ToString(value);
				}
			}
			public System.String AssistantIDAnestesi
			{
				get
				{
					System.String data = entity.AssistantIDAnestesi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDAnestesi = null;
					else entity.AssistantIDAnestesi = Convert.ToString(value);
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
			public System.String OperationType
			{
				get
				{
					System.String data = entity.OperationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationType = null;
					else entity.OperationType = Convert.ToString(value);
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
			public System.String ProcedureChargeClassID
			{
				get
				{
					System.String data = entity.ProcedureChargeClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcedureChargeClassID = null;
					else entity.ProcedureChargeClassID = Convert.ToString(value);
				}
			}
			public System.String OperatingNotes
			{
				get
				{
					System.String data = entity.OperatingNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperatingNotes = null;
					else entity.OperatingNotes = Convert.ToString(value);
				}
			}
			public System.String Instrumentator1
			{
				get
				{
					System.String data = entity.Instrumentator1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Instrumentator1 = null;
					else entity.Instrumentator1 = Convert.ToString(value);
				}
			}
			public System.String Instrumentator2
			{
				get
				{
					System.String data = entity.Instrumentator2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Instrumentator2 = null;
					else entity.Instrumentator2 = Convert.ToString(value);
				}
			}
			public System.String SRProcedureCategory
			{
				get
				{
					System.String data = entity.SRProcedureCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedureCategory = null;
					else entity.SRProcedureCategory = Convert.ToString(value);
				}
			}
			public System.String RealizationDateTimeFrom
			{
				get
				{
					System.DateTime? data = entity.RealizationDateTimeFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationDateTimeFrom = null;
					else entity.RealizationDateTimeFrom = Convert.ToDateTime(value);
				}
			}
			public System.String RealizationDateTimeTo
			{
				get
				{
					System.DateTime? data = entity.RealizationDateTimeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationDateTimeTo = null;
					else entity.RealizationDateTimeTo = Convert.ToDateTime(value);
				}
			}
			public System.String Resident
			{
				get
				{
					System.String data = entity.Resident;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Resident = null;
					else entity.Resident = Convert.ToString(value);
				}
			}
			public System.String AssistantIDInstrumentator
			{
				get
				{
					System.String data = entity.AssistantIDInstrumentator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDInstrumentator = null;
					else entity.AssistantIDInstrumentator = Convert.ToString(value);
				}
			}
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
			public System.String IsExtendedSurgery
			{
				get
				{
					System.Boolean? data = entity.IsExtendedSurgery;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExtendedSurgery = null;
					else entity.IsExtendedSurgery = Convert.ToBoolean(value);
				}
			}
			public System.String SRIndication
			{
				get
				{
					System.String data = entity.SRIndication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIndication = null;
					else entity.SRIndication = Convert.ToString(value);
				}
			}
			public System.String IsNeedPa
			{
				get
				{
					System.Boolean? data = entity.IsNeedPa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedPa = null;
					else entity.IsNeedPa = Convert.ToBoolean(value);
				}
			}
			public System.String AssistantID5
			{
				get
				{
					System.String data = entity.AssistantID5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantID5 = null;
					else entity.AssistantID5 = Convert.ToString(value);
				}
			}
			public System.String AssistantIDInstrumentator2
			{
				get
				{
					System.String data = entity.AssistantIDInstrumentator2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDInstrumentator2 = null;
					else entity.AssistantIDInstrumentator2 = Convert.ToString(value);
				}
			}
			public System.String AssistantIDInstrumentator3
			{
				get
				{
					System.String data = entity.AssistantIDInstrumentator3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDInstrumentator3 = null;
					else entity.AssistantIDInstrumentator3 = Convert.ToString(value);
				}
			}
			public System.String AssistantIDInstrumentator4
			{
				get
				{
					System.String data = entity.AssistantIDInstrumentator4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDInstrumentator4 = null;
					else entity.AssistantIDInstrumentator4 = Convert.ToString(value);
				}
			}
			public System.String AssistantIDInstrumentator5
			{
				get
				{
					System.String data = entity.AssistantIDInstrumentator5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDInstrumentator5 = null;
					else entity.AssistantIDInstrumentator5 = Convert.ToString(value);
				}
			}
			public System.String Instrumentator3
			{
				get
				{
					System.String data = entity.Instrumentator3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Instrumentator3 = null;
					else entity.Instrumentator3 = Convert.ToString(value);
				}
			}
			public System.String Instrumentator4
			{
				get
				{
					System.String data = entity.Instrumentator4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Instrumentator4 = null;
					else entity.Instrumentator4 = Convert.ToString(value);
				}
			}
			public System.String Instrumentator5
			{
				get
				{
					System.String data = entity.Instrumentator5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Instrumentator5 = null;
					else entity.Instrumentator5 = Convert.ToString(value);
				}
			}
			public System.String AnestesyNotes
			{
				get
				{
					System.String data = entity.AnestesyNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnestesyNotes = null;
					else entity.AnestesyNotes = Convert.ToString(value);
				}
			}
			public System.String SRProcedure1
			{
				get
				{
					System.String data = entity.SRProcedure1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedure1 = null;
					else entity.SRProcedure1 = Convert.ToString(value);
				}
			}
			public System.String SRProcedure2
			{
				get
				{
					System.String data = entity.SRProcedure2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedure2 = null;
					else entity.SRProcedure2 = Convert.ToString(value);
				}
			}
			public System.String PostDiagnosis
			{
				get
				{
					System.String data = entity.PostDiagnosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostDiagnosis = null;
					else entity.PostDiagnosis = Convert.ToString(value);
				}
			}
			public System.String PaDate
			{
				get
				{
					System.DateTime? data = entity.PaDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaDate = null;
					else entity.PaDate = Convert.ToDateTime(value);
				}
			}
			public System.String SourceOfTissue
			{
				get
				{
					System.String data = entity.SourceOfTissue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SourceOfTissue = null;
					else entity.SourceOfTissue = Convert.ToString(value);
				}
			}
			public System.String ArrivedDateTime
			{
				get
				{
					System.DateTime? data = entity.ArrivedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ArrivedDateTime = null;
					else entity.ArrivedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsAnestheticConversion
			{
				get
				{
					System.Boolean? data = entity.IsAnestheticConversion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnestheticConversion = null;
					else entity.IsAnestheticConversion = Convert.ToBoolean(value);
				}
			}
			public System.String IsValidate
			{
				get
				{
					System.Boolean? data = entity.IsValidate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidate = null;
					else entity.IsValidate = Convert.ToBoolean(value);
				}
			}
			public System.String ValidateDateTime
			{
				get
				{
					System.DateTime? data = entity.ValidateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidateDateTime = null;
					else entity.ValidateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidateByUserID
			{
				get
				{
					System.String data = entity.ValidateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidateByUserID = null;
					else entity.ValidateByUserID = Convert.ToString(value);
				}
			}
			public System.String PreSurgeryDateTime
			{
				get
				{
					System.DateTime? data = entity.PreSurgeryDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreSurgeryDateTime = null;
					else entity.PreSurgeryDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String PreSurgeryByUserID
			{
				get
				{
					System.String data = entity.PreSurgeryByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreSurgeryByUserID = null;
					else entity.PreSurgeryByUserID = Convert.ToString(value);
				}
			}
			public System.String AnesthesiaDateTime
			{
				get
				{
					System.DateTime? data = entity.AnesthesiaDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnesthesiaDateTime = null;
					else entity.AnesthesiaDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String AnesthesiaByUserID
			{
				get
				{
					System.String data = entity.AnesthesiaByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnesthesiaByUserID = null;
					else entity.AnesthesiaByUserID = Convert.ToString(value);
				}
			}
			public System.String SurgeryDateTime
			{
				get
				{
					System.DateTime? data = entity.SurgeryDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SurgeryDateTime = null;
					else entity.SurgeryDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SurgeryByUserID
			{
				get
				{
					System.String data = entity.SurgeryByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SurgeryByUserID = null;
					else entity.SurgeryByUserID = Convert.ToString(value);
				}
			}
			public System.String PostSurgeryDateTime
			{
				get
				{
					System.DateTime? data = entity.PostSurgeryDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostSurgeryDateTime = null;
					else entity.PostSurgeryDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String PostSurgeryByUserID
			{
				get
				{
					System.String data = entity.PostSurgeryByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostSurgeryByUserID = null;
					else entity.PostSurgeryByUserID = Convert.ToString(value);
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
			public System.String IsInsertionImplant
			{
				get
				{
					System.Boolean? data = entity.IsInsertionImplant;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInsertionImplant = null;
					else entity.IsInsertionImplant = Convert.ToBoolean(value);
				}
			}
			public System.String IncisionDateTime
			{
				get
				{
					System.DateTime? data = entity.IncisionDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncisionDateTime = null;
					else entity.IncisionDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SRProcedureDiagnoseType
			{
				get
				{
					System.String data = entity.SRProcedureDiagnoseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProcedureDiagnoseType = null;
					else entity.SRProcedureDiagnoseType = Convert.ToString(value);
				}
			}
			public System.String MoveToTheWardDateTime
			{
				get
				{
					System.DateTime? data = entity.MoveToTheWardDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MoveToTheWardDateTime = null;
					else entity.MoveToTheWardDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String MoveToTheWardByUserID
			{
				get
				{
					System.String data = entity.MoveToTheWardByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MoveToTheWardByUserID = null;
					else entity.MoveToTheWardByUserID = Convert.ToString(value);
				}
			}
			public System.String AnestPostSurgeryInstructions
			{
				get
				{
					System.String data = entity.AnestPostSurgeryInstructions;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnestPostSurgeryInstructions = null;
					else entity.AnestPostSurgeryInstructions = Convert.ToString(value);
				}
			}
			public System.String AmountOfBleeding
			{
				get
				{
					System.Decimal? data = entity.AmountOfBleeding;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountOfBleeding = null;
					else entity.AmountOfBleeding = Convert.ToDecimal(value);
				}
			}
			public System.String AmountOfTransfusions
			{
				get
				{
					System.Decimal? data = entity.AmountOfTransfusions;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountOfTransfusions = null;
					else entity.AmountOfTransfusions = Convert.ToDecimal(value);
				}
			}
			public System.String AssistantIDAnestesi2
			{
				get
				{
					System.String data = entity.AssistantIDAnestesi2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistantIDAnestesi2 = null;
					else entity.AssistantIDAnestesi2 = Convert.ToString(value);
				}
			}
			private esServiceUnitBooking entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitBookingQuery query)
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
				throw new Exception("esServiceUnitBooking can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnitBooking : esServiceUnitBooking
	{
	}

	[Serializable]
	abstract public class esServiceUnitBookingQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitBookingMetadata.Meta();
			}
		}

		public esQueryItem BookingNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.BookingNo, esSystemType.String);
			}
		}

		public esQueryItem BookingDateTimeFrom
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem BookingDateTimeTo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeTo, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastCreateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastCreateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.LastCreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRAnestesiPlan
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SRAnestesiPlan, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID2
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ParamedicID2, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID3
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ParamedicID3, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID4
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ParamedicID4, esSystemType.String);
			}
		}

		public esQueryItem ParamedicIDAnestesi
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ParamedicIDAnestesi, esSystemType.String);
			}
		}

		public esQueryItem AssistantID1
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantID1, esSystemType.String);
			}
		}

		public esQueryItem AssistantID2
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantID2, esSystemType.String);
			}
		}

		public esQueryItem AssistantID3
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantID3, esSystemType.String);
			}
		}

		public esQueryItem AssistantID4
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantID4, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDAnestesi
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi, esSystemType.String);
			}
		}

		public esQueryItem Diagnose
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Diagnose, esSystemType.String);
			}
		}

		public esQueryItem OperationType
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.OperationType, esSystemType.String);
			}
		}

		public esQueryItem IsCito
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsCito, esSystemType.Boolean);
			}
		}

		public esQueryItem ProcedureChargeClassID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ProcedureChargeClassID, esSystemType.String);
			}
		}

		public esQueryItem OperatingNotes
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.OperatingNotes, esSystemType.String);
			}
		}

		public esQueryItem Instrumentator1
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Instrumentator1, esSystemType.String);
			}
		}

		public esQueryItem Instrumentator2
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Instrumentator2, esSystemType.String);
			}
		}

		public esQueryItem SRProcedureCategory
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SRProcedureCategory, esSystemType.String);
			}
		}

		public esQueryItem RealizationDateTimeFrom
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationDateTimeTo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeTo, esSystemType.DateTime);
			}
		}

		public esQueryItem Resident
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Resident, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDInstrumentator
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator, esSystemType.String);
			}
		}

		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		}

		public esQueryItem IsExtendedSurgery
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsExtendedSurgery, esSystemType.Boolean);
			}
		}

		public esQueryItem SRIndication
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SRIndication, esSystemType.String);
			}
		}

		public esQueryItem IsNeedPa
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsNeedPa, esSystemType.Boolean);
			}
		}

		public esQueryItem AssistantID5
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantID5, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDInstrumentator2
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator2, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDInstrumentator3
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator3, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDInstrumentator4
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator4, esSystemType.String);
			}
		}

		public esQueryItem AssistantIDInstrumentator5
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator5, esSystemType.String);
			}
		}

		public esQueryItem Instrumentator3
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Instrumentator3, esSystemType.String);
			}
		}

		public esQueryItem Instrumentator4
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Instrumentator4, esSystemType.String);
			}
		}

		public esQueryItem Instrumentator5
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.Instrumentator5, esSystemType.String);
			}
		}

		public esQueryItem AnestesyNotes
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AnestesyNotes, esSystemType.String);
			}
		}

		public esQueryItem SRProcedure1
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SRProcedure1, esSystemType.String);
			}
		}

		public esQueryItem SRProcedure2
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SRProcedure2, esSystemType.String);
			}
		}

		public esQueryItem PostDiagnosis
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.PostDiagnosis, esSystemType.String);
			}
		}

		public esQueryItem PaDate
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.PaDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SourceOfTissue
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SourceOfTissue, esSystemType.String);
			}
		}

		public esQueryItem ArrivedDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ArrivedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsAnestheticConversion
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsAnestheticConversion, esSystemType.Boolean);
			}
		}

		public esQueryItem IsValidate
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsValidate, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ValidateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.ValidateByUserID, esSystemType.String);
			}
		}

		public esQueryItem PreSurgeryDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.PreSurgeryDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem PreSurgeryByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.PreSurgeryByUserID, esSystemType.String);
			}
		}

		public esQueryItem AnesthesiaDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AnesthesiaDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem AnesthesiaByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AnesthesiaByUserID, esSystemType.String);
			}
		}

		public esQueryItem SurgeryDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SurgeryDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SurgeryByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SurgeryByUserID, esSystemType.String);
			}
		}

		public esQueryItem PostSurgeryDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.PostSurgeryDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem PostSurgeryByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.PostSurgeryByUserID, esSystemType.String);
			}
		}

		public esQueryItem VoidReason
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.VoidReason, esSystemType.String);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem IsInsertionImplant
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IsInsertionImplant, esSystemType.Boolean);
			}
		}

		public esQueryItem IncisionDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.IncisionDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SRProcedureDiagnoseType
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.SRProcedureDiagnoseType, esSystemType.String);
			}
		}

		public esQueryItem MoveToTheWardDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem MoveToTheWardByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardByUserID, esSystemType.String);
			}
		}

		public esQueryItem AnesthesiologistSign
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AnesthesiologistSign, esSystemType.ByteArray);
			}
		}

		public esQueryItem AnestPostSurgeryInstructions
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AnestPostSurgeryInstructions, esSystemType.String);
			}
		}

		public esQueryItem AmountOfBleeding
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AmountOfBleeding, esSystemType.Decimal);
			}
		}

		public esQueryItem AmountOfTransfusions
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AmountOfTransfusions, esSystemType.Decimal);
			}
		}

		public esQueryItem AssistantIDAnestesi2
		{
			get
			{
				return new esQueryItem(this, ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi2, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitBookingCollection")]
	public partial class ServiceUnitBookingCollection : esServiceUnitBookingCollection, IEnumerable<ServiceUnitBooking>
	{
		public ServiceUnitBookingCollection()
		{

		}

		public static implicit operator List<ServiceUnitBooking>(ServiceUnitBookingCollection coll)
		{
			List<ServiceUnitBooking> list = new List<ServiceUnitBooking>();

			foreach (ServiceUnitBooking emp in coll)
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
				return ServiceUnitBookingMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitBookingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitBooking(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitBooking();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitBookingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitBookingQuery();
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
		public bool Load(ServiceUnitBookingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnitBooking AddNew()
		{
			ServiceUnitBooking entity = base.AddNewEntity() as ServiceUnitBooking;

			return entity;
		}
		public ServiceUnitBooking FindByPrimaryKey(String bookingNo)
		{
			return base.FindByPrimaryKey(bookingNo) as ServiceUnitBooking;
		}

		#region IEnumerable< ServiceUnitBooking> Members

		IEnumerator<ServiceUnitBooking> IEnumerable<ServiceUnitBooking>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitBooking;
			}
		}

		#endregion

		private ServiceUnitBookingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitBooking' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnitBooking ({BookingNo})")]
	[Serializable]
	public partial class ServiceUnitBooking : esServiceUnitBooking
	{
		public ServiceUnitBooking()
		{
		}

		public ServiceUnitBooking(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitBookingMetadata.Meta();
			}
		}

		override protected esServiceUnitBookingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitBookingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitBookingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitBookingQuery();
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
		public bool Load(ServiceUnitBookingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ServiceUnitBookingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitBookingQuery : esServiceUnitBookingQuery
	{
		public ServiceUnitBookingQuery()
		{

		}

		public ServiceUnitBookingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ServiceUnitBookingQuery";
		}
	}

	[Serializable]
	public partial class ServiceUnitBookingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitBookingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.BookingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.BookingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeFrom, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.BookingDateTimeFrom;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.BookingDateTimeTo, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.BookingDateTimeTo;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.RoomID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.PatientID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.RegistrationNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ParamedicID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Notes, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.LastCreateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.LastCreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.LastCreateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.LastCreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SRAnestesiPlan, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SRAnestesiPlan;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ParamedicID2, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ParamedicID2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ParamedicID3, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ParamedicID3;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ParamedicID4, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ParamedicID4;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ParamedicIDAnestesi, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ParamedicIDAnestesi;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantID1, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantID1;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantID2, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantID2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantID3, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantID3;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantID4, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantID4;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantIDAnestesi;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Diagnose, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Diagnose;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.OperationType, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.OperationType;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsCito, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsCito;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ProcedureChargeClassID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ProcedureChargeClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.OperatingNotes, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.OperatingNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Instrumentator1, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Instrumentator1;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Instrumentator2, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Instrumentator2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SRProcedureCategory, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SRProcedureCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeFrom, 33, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.RealizationDateTimeFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.RealizationDateTimeTo, 34, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.RealizationDateTimeTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Resident, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Resident;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantIDInstrumentator;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SmfID, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SmfID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsExtendedSurgery, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsExtendedSurgery;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SRIndication, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SRIndication;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsNeedPa, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsNeedPa;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantID5, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantID5;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator2, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantIDInstrumentator2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator3, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantIDInstrumentator3;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator4, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantIDInstrumentator4;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantIDInstrumentator5, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantIDInstrumentator5;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Instrumentator3, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Instrumentator3;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Instrumentator4, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Instrumentator4;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.Instrumentator5, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.Instrumentator5;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AnestesyNotes, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AnestesyNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SRProcedure1, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SRProcedure1;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SRProcedure2, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SRProcedure2;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.PostDiagnosis, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.PostDiagnosis;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.PaDate, 53, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.PaDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SourceOfTissue, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SourceOfTissue;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ArrivedDateTime, 55, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ArrivedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsAnestheticConversion, 56, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsAnestheticConversion;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsValidate, 57, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsValidate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ValidateDateTime, 58, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ValidateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.ValidateByUserID, 59, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.ValidateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.PreSurgeryDateTime, 60, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.PreSurgeryDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.PreSurgeryByUserID, 61, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.PreSurgeryByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AnesthesiaDateTime, 62, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AnesthesiaDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AnesthesiaByUserID, 63, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AnesthesiaByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SurgeryDateTime, 64, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SurgeryDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SurgeryByUserID, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SurgeryByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.PostSurgeryDateTime, 66, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.PostSurgeryDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.PostSurgeryByUserID, 67, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.PostSurgeryByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.VoidReason, 68, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.VoidReason;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.FromServiceUnitID, 69, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IsInsertionImplant, 70, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IsInsertionImplant;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.IncisionDateTime, 71, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.IncisionDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.SRProcedureDiagnoseType, 72, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.SRProcedureDiagnoseType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardDateTime, 73, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.MoveToTheWardDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.MoveToTheWardByUserID, 74, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.MoveToTheWardByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AnesthesiologistSign, 75, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AnesthesiologistSign;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AnestPostSurgeryInstructions, 76, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AnestPostSurgeryInstructions;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AmountOfBleeding, 77, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AmountOfBleeding;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AmountOfTransfusions, 78, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AmountOfTransfusions;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitBookingMetadata.ColumnNames.AssistantIDAnestesi2, 79, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitBookingMetadata.PropertyNames.AssistantIDAnestesi2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ServiceUnitBookingMetadata Meta()
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
			public const string BookingNo = "BookingNo";
			public const string BookingDateTimeFrom = "BookingDateTimeFrom";
			public const string BookingDateTimeTo = "BookingDateTimeTo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string PatientID = "PatientID";
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateByUserID = "LastCreateByUserID";
			public const string SRAnestesiPlan = "SRAnestesiPlan";
			public const string ParamedicID2 = "ParamedicID2";
			public const string ParamedicID3 = "ParamedicID3";
			public const string ParamedicID4 = "ParamedicID4";
			public const string ParamedicIDAnestesi = "ParamedicIDAnestesi";
			public const string AssistantID1 = "AssistantID1";
			public const string AssistantID2 = "AssistantID2";
			public const string AssistantID3 = "AssistantID3";
			public const string AssistantID4 = "AssistantID4";
			public const string AssistantIDAnestesi = "AssistantIDAnestesi";
			public const string Diagnose = "Diagnose";
			public const string OperationType = "OperationType";
			public const string IsCito = "IsCito";
			public const string ProcedureChargeClassID = "ProcedureChargeClassID";
			public const string OperatingNotes = "OperatingNotes";
			public const string Instrumentator1 = "Instrumentator1";
			public const string Instrumentator2 = "Instrumentator2";
			public const string SRProcedureCategory = "SRProcedureCategory";
			public const string RealizationDateTimeFrom = "RealizationDateTimeFrom";
			public const string RealizationDateTimeTo = "RealizationDateTimeTo";
			public const string Resident = "Resident";
			public const string AssistantIDInstrumentator = "AssistantIDInstrumentator";
			public const string SmfID = "SmfID";
			public const string IsExtendedSurgery = "IsExtendedSurgery";
			public const string SRIndication = "SRIndication";
			public const string IsNeedPa = "IsNeedPa";
			public const string AssistantID5 = "AssistantID5";
			public const string AssistantIDInstrumentator2 = "AssistantIDInstrumentator2";
			public const string AssistantIDInstrumentator3 = "AssistantIDInstrumentator3";
			public const string AssistantIDInstrumentator4 = "AssistantIDInstrumentator4";
			public const string AssistantIDInstrumentator5 = "AssistantIDInstrumentator5";
			public const string Instrumentator3 = "Instrumentator3";
			public const string Instrumentator4 = "Instrumentator4";
			public const string Instrumentator5 = "Instrumentator5";
			public const string AnestesyNotes = "AnestesyNotes";
			public const string SRProcedure1 = "SRProcedure1";
			public const string SRProcedure2 = "SRProcedure2";
			public const string PostDiagnosis = "PostDiagnosis";
			public const string PaDate = "PaDate";
			public const string SourceOfTissue = "SourceOfTissue";
			public const string ArrivedDateTime = "ArrivedDateTime";
			public const string IsAnestheticConversion = "IsAnestheticConversion";
			public const string IsValidate = "IsValidate";
			public const string ValidateDateTime = "ValidateDateTime";
			public const string ValidateByUserID = "ValidateByUserID";
			public const string PreSurgeryDateTime = "PreSurgeryDateTime";
			public const string PreSurgeryByUserID = "PreSurgeryByUserID";
			public const string AnesthesiaDateTime = "AnesthesiaDateTime";
			public const string AnesthesiaByUserID = "AnesthesiaByUserID";
			public const string SurgeryDateTime = "SurgeryDateTime";
			public const string SurgeryByUserID = "SurgeryByUserID";
			public const string PostSurgeryDateTime = "PostSurgeryDateTime";
			public const string PostSurgeryByUserID = "PostSurgeryByUserID";
			public const string VoidReason = "VoidReason";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string IsInsertionImplant = "IsInsertionImplant";
			public const string IncisionDateTime = "IncisionDateTime";
			public const string SRProcedureDiagnoseType = "SRProcedureDiagnoseType";
			public const string MoveToTheWardDateTime = "MoveToTheWardDateTime";
			public const string MoveToTheWardByUserID = "MoveToTheWardByUserID";
			public const string AnesthesiologistSign = "AnesthesiologistSign";
			public const string AnestPostSurgeryInstructions = "AnestPostSurgeryInstructions";
			public const string AmountOfBleeding = "AmountOfBleeding";
			public const string AmountOfTransfusions = "AmountOfTransfusions";
			public const string AssistantIDAnestesi2 = "AssistantIDAnestesi2";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string BookingNo = "BookingNo";
			public const string BookingDateTimeFrom = "BookingDateTimeFrom";
			public const string BookingDateTimeTo = "BookingDateTimeTo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string PatientID = "PatientID";
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string IsApproved = "IsApproved";
			public const string IsVoid = "IsVoid";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateByUserID = "LastCreateByUserID";
			public const string SRAnestesiPlan = "SRAnestesiPlan";
			public const string ParamedicID2 = "ParamedicID2";
			public const string ParamedicID3 = "ParamedicID3";
			public const string ParamedicID4 = "ParamedicID4";
			public const string ParamedicIDAnestesi = "ParamedicIDAnestesi";
			public const string AssistantID1 = "AssistantID1";
			public const string AssistantID2 = "AssistantID2";
			public const string AssistantID3 = "AssistantID3";
			public const string AssistantID4 = "AssistantID4";
			public const string AssistantIDAnestesi = "AssistantIDAnestesi";
			public const string Diagnose = "Diagnose";
			public const string OperationType = "OperationType";
			public const string IsCito = "IsCito";
			public const string ProcedureChargeClassID = "ProcedureChargeClassID";
			public const string OperatingNotes = "OperatingNotes";
			public const string Instrumentator1 = "Instrumentator1";
			public const string Instrumentator2 = "Instrumentator2";
			public const string SRProcedureCategory = "SRProcedureCategory";
			public const string RealizationDateTimeFrom = "RealizationDateTimeFrom";
			public const string RealizationDateTimeTo = "RealizationDateTimeTo";
			public const string Resident = "Resident";
			public const string AssistantIDInstrumentator = "AssistantIDInstrumentator";
			public const string SmfID = "SmfID";
			public const string IsExtendedSurgery = "IsExtendedSurgery";
			public const string SRIndication = "SRIndication";
			public const string IsNeedPa = "IsNeedPa";
			public const string AssistantID5 = "AssistantID5";
			public const string AssistantIDInstrumentator2 = "AssistantIDInstrumentator2";
			public const string AssistantIDInstrumentator3 = "AssistantIDInstrumentator3";
			public const string AssistantIDInstrumentator4 = "AssistantIDInstrumentator4";
			public const string AssistantIDInstrumentator5 = "AssistantIDInstrumentator5";
			public const string Instrumentator3 = "Instrumentator3";
			public const string Instrumentator4 = "Instrumentator4";
			public const string Instrumentator5 = "Instrumentator5";
			public const string AnestesyNotes = "AnestesyNotes";
			public const string SRProcedure1 = "SRProcedure1";
			public const string SRProcedure2 = "SRProcedure2";
			public const string PostDiagnosis = "PostDiagnosis";
			public const string PaDate = "PaDate";
			public const string SourceOfTissue = "SourceOfTissue";
			public const string ArrivedDateTime = "ArrivedDateTime";
			public const string IsAnestheticConversion = "IsAnestheticConversion";
			public const string IsValidate = "IsValidate";
			public const string ValidateDateTime = "ValidateDateTime";
			public const string ValidateByUserID = "ValidateByUserID";
			public const string PreSurgeryDateTime = "PreSurgeryDateTime";
			public const string PreSurgeryByUserID = "PreSurgeryByUserID";
			public const string AnesthesiaDateTime = "AnesthesiaDateTime";
			public const string AnesthesiaByUserID = "AnesthesiaByUserID";
			public const string SurgeryDateTime = "SurgeryDateTime";
			public const string SurgeryByUserID = "SurgeryByUserID";
			public const string PostSurgeryDateTime = "PostSurgeryDateTime";
			public const string PostSurgeryByUserID = "PostSurgeryByUserID";
			public const string VoidReason = "VoidReason";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string IsInsertionImplant = "IsInsertionImplant";
			public const string IncisionDateTime = "IncisionDateTime";
			public const string SRProcedureDiagnoseType = "SRProcedureDiagnoseType";
			public const string MoveToTheWardDateTime = "MoveToTheWardDateTime";
			public const string MoveToTheWardByUserID = "MoveToTheWardByUserID";
			public const string AnesthesiologistSign = "AnesthesiologistSign";
			public const string AnestPostSurgeryInstructions = "AnestPostSurgeryInstructions";
			public const string AmountOfBleeding = "AmountOfBleeding";
			public const string AmountOfTransfusions = "AmountOfTransfusions";
			public const string AssistantIDAnestesi2 = "AssistantIDAnestesi2";
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
			lock (typeof(ServiceUnitBookingMetadata))
			{
				if (ServiceUnitBookingMetadata.mapDelegates == null)
				{
					ServiceUnitBookingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ServiceUnitBookingMetadata.meta == null)
				{
					ServiceUnitBookingMetadata.meta = new ServiceUnitBookingMetadata();
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

				meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BookingDateTimeFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BookingDateTimeTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAnestesiPlan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicIDAnestesi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantID1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantID3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantID4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDAnestesi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Diagnose", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OperationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCito", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ProcedureChargeClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OperatingNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Instrumentator1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Instrumentator2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcedureCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationDateTimeFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RealizationDateTimeTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Resident", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDInstrumentator", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsExtendedSurgery", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRIndication", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNeedPa", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AssistantID5", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDInstrumentator2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDInstrumentator3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDInstrumentator4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistantIDInstrumentator5", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Instrumentator3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Instrumentator4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Instrumentator5", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnestesyNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcedure1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProcedure2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PostDiagnosis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SourceOfTissue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ArrivedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsAnestheticConversion", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsValidate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreSurgeryDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PreSurgeryByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnesthesiaDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AnesthesiaByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SurgeryDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SurgeryByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PostSurgeryDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PostSurgeryByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInsertionImplant", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IncisionDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRProcedureDiagnoseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MoveToTheWardDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("MoveToTheWardByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnesthesiologistSign", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("AnestPostSurgeryInstructions", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AmountOfBleeding", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AmountOfTransfusions", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AssistantIDAnestesi2", new esTypeMap("varchar", "System.String"));


				meta.Source = "ServiceUnitBooking";
				meta.Destination = "ServiceUnitBooking";
				meta.spInsert = "proc_ServiceUnitBookingInsert";
				meta.spUpdate = "proc_ServiceUnitBookingUpdate";
				meta.spDelete = "proc_ServiceUnitBookingDelete";
				meta.spLoadAll = "proc_ServiceUnitBookingLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitBookingLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitBookingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
