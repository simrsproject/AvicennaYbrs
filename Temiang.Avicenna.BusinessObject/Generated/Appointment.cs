/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/5/2024 4:05:05 PM
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
	abstract public class esAppointmentCollection : esEntityCollectionWAuditLog
	{
		public esAppointmentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppointmentCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppointmentQuery query)
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
			this.InitQuery(query as esAppointmentQuery);
		}
		#endregion

		virtual public Appointment DetachEntity(Appointment entity)
		{
			return base.DetachEntity(entity) as Appointment;
		}

		virtual public Appointment AttachEntity(Appointment entity)
		{
			return base.AttachEntity(entity) as Appointment;
		}

		virtual public void Combine(AppointmentCollection collection)
		{
			base.Combine(collection);
		}

		new public Appointment this[int index]
		{
			get
			{
				return base[index] as Appointment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Appointment);
		}
	}

	[Serializable]
	abstract public class esAppointment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppointmentQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppointment()
		{
		}

		public esAppointment(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String appointmentNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(appointmentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(appointmentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String appointmentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(appointmentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(appointmentNo);
		}

		private bool LoadByPrimaryKeyDynamic(String appointmentNo)
		{
			esAppointmentQuery query = this.GetDynamicQuery();
			query.Where(query.AppointmentNo == appointmentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String appointmentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("AppointmentNo", appointmentNo);
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
						case "AppointmentNo": this.str.AppointmentNo = (string)value; break;
						case "AppointmentQue": this.str.AppointmentQue = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "AppointmentDate": this.str.AppointmentDate = (string)value; break;
						case "AppointmentTime": this.str.AppointmentTime = (string)value; break;
						case "VisitTypeID": this.str.VisitTypeID = (string)value; break;
						case "VisitDuration": this.str.VisitDuration = (string)value; break;
						case "SRAppointmentStatus": this.str.SRAppointmentStatus = (string)value; break;
						case "FirstName": this.str.FirstName = (string)value; break;
						case "MiddleName": this.str.MiddleName = (string)value; break;
						case "LastName": this.str.LastName = (string)value; break;
						case "StreetName": this.str.StreetName = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "State": this.str.State = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "FaxNo": this.str.FaxNo = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "PatientPIC": this.str.PatientPIC = (string)value; break;
						case "OfficerPIC": this.str.OfficerPIC = (string)value; break;
						case "FollowUpDateTime": this.str.FollowUpDateTime = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;
						case "LastCreateByUserID": this.str.LastCreateByUserID = (string)value; break;
						case "DateOfBirth": this.str.DateOfBirth = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "FromRegistrationNo": this.str.FromRegistrationNo = (string)value; break;
						case "EmployeeNo": this.str.EmployeeNo = (string)value; break;
						case "EmployeeJobTitleName": this.str.EmployeeJobTitleName = (string)value; break;
						case "EmployeeJobDepartementName": this.str.EmployeeJobDepartementName = (string)value; break;
						case "Sex": this.str.Sex = (string)value; break;
						case "BirthPlace": this.str.BirthPlace = (string)value; break;
						case "Ssn": this.str.Ssn = (string)value; break;
						case "SRSalutation": this.str.SRSalutation = (string)value; break;
						case "SRNationality": this.str.SRNationality = (string)value; break;
						case "SROccupation": this.str.SROccupation = (string)value; break;
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "SRReferralGroup": this.str.SRReferralGroup = (string)value; break;
						case "ReferralName": this.str.ReferralName = (string)value; break;
						case "GuarantorCardNo": this.str.GuarantorCardNo = (string)value; break;
						case "ReferenceNumber": this.str.ReferenceNumber = (string)value; break;
						case "ReferenceType": this.str.ReferenceType = (string)value; break;
						case "SRAppoinmentType": this.str.SRAppoinmentType = (string)value; break;
						case "FromRegistrationNoMds": this.str.FromRegistrationNoMds = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "AppointmentQue":

							if (value == null || value is System.Int32)
								this.AppointmentQue = (System.Int32?)value;
							break;
						case "AppointmentDate":

							if (value == null || value is System.DateTime)
								this.AppointmentDate = (System.DateTime?)value;
							break;
						case "VisitDuration":

							if (value == null || value is System.Byte)
								this.VisitDuration = (System.Byte?)value;
							break;
						case "FollowUpDateTime":

							if (value == null || value is System.DateTime)
								this.FollowUpDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "LastCreateDateTime":

							if (value == null || value is System.DateTime)
								this.LastCreateDateTime = (System.DateTime?)value;
							break;
						case "DateOfBirth":

							if (value == null || value is System.DateTime)
								this.DateOfBirth = (System.DateTime?)value;
							break;
						case "ReferenceType":

							if (value == null || value is System.Int32)
								this.ReferenceType = (System.Int32?)value;
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
		/// Maps to Appointment.AppointmentNo
		/// </summary>
		virtual public System.String AppointmentNo
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.AppointmentNo);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.AppointmentNo, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.AppointmentQue
		/// </summary>
		virtual public System.Int32? AppointmentQue
		{
			get
			{
				return base.GetSystemInt32(AppointmentMetadata.ColumnNames.AppointmentQue);
			}

			set
			{
				base.SetSystemInt32(AppointmentMetadata.ColumnNames.AppointmentQue, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.AppointmentDate
		/// </summary>
		virtual public System.DateTime? AppointmentDate
		{
			get
			{
				return base.GetSystemDateTime(AppointmentMetadata.ColumnNames.AppointmentDate);
			}

			set
			{
				base.SetSystemDateTime(AppointmentMetadata.ColumnNames.AppointmentDate, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.AppointmentTime
		/// </summary>
		virtual public System.String AppointmentTime
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.AppointmentTime);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.AppointmentTime, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.VisitTypeID
		/// </summary>
		virtual public System.String VisitTypeID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.VisitTypeID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.VisitTypeID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.VisitDuration
		/// </summary>
		virtual public System.Byte? VisitDuration
		{
			get
			{
				return base.GetSystemByte(AppointmentMetadata.ColumnNames.VisitDuration);
			}

			set
			{
				base.SetSystemByte(AppointmentMetadata.ColumnNames.VisitDuration, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.SRAppointmentStatus
		/// </summary>
		virtual public System.String SRAppointmentStatus
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.SRAppointmentStatus);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.SRAppointmentStatus, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.FirstName
		/// </summary>
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.FirstName);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.FirstName, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.MiddleName
		/// </summary>
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.MiddleName);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.MiddleName, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.LastName
		/// </summary>
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.LastName);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.LastName, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.StreetName);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.StreetName, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.State);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.State, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.FaxNo);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.FaxNo, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.Email);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.MobilePhoneNo);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.PatientPIC
		/// </summary>
		virtual public System.String PatientPIC
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.PatientPIC);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.PatientPIC, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.OfficerPIC
		/// </summary>
		virtual public System.String OfficerPIC
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.OfficerPIC);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.OfficerPIC, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.FollowUpDateTime
		/// </summary>
		virtual public System.DateTime? FollowUpDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppointmentMetadata.ColumnNames.FollowUpDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppointmentMetadata.ColumnNames.FollowUpDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppointmentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppointmentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.LastCreateDateTime
		/// </summary>
		virtual public System.DateTime? LastCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppointmentMetadata.ColumnNames.LastCreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppointmentMetadata.ColumnNames.LastCreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.LastCreateByUserID
		/// </summary>
		virtual public System.String LastCreateByUserID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.LastCreateByUserID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.LastCreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.DateOfBirth
		/// </summary>
		virtual public System.DateTime? DateOfBirth
		{
			get
			{
				return base.GetSystemDateTime(AppointmentMetadata.ColumnNames.DateOfBirth);
			}

			set
			{
				base.SetSystemDateTime(AppointmentMetadata.ColumnNames.DateOfBirth, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.GuarantorID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.FromRegistrationNo
		/// </summary>
		virtual public System.String FromRegistrationNo
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.FromRegistrationNo);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.FromRegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.EmployeeNo
		/// </summary>
		virtual public System.String EmployeeNo
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.EmployeeNo);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.EmployeeNo, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.EmployeeJobTitleName
		/// </summary>
		virtual public System.String EmployeeJobTitleName
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.EmployeeJobTitleName);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.EmployeeJobTitleName, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.EmployeeJobDepartementName
		/// </summary>
		virtual public System.String EmployeeJobDepartementName
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.EmployeeJobDepartementName);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.EmployeeJobDepartementName, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.Sex
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.Sex);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.Sex, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.BirthPlace
		/// </summary>
		virtual public System.String BirthPlace
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.BirthPlace);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.BirthPlace, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.Ssn
		/// </summary>
		virtual public System.String Ssn
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.Ssn);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.Ssn, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.SRSalutation
		/// </summary>
		virtual public System.String SRSalutation
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.SRSalutation);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.SRSalutation, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.SRNationality
		/// </summary>
		virtual public System.String SRNationality
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.SRNationality);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.SRNationality, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.SROccupation
		/// </summary>
		virtual public System.String SROccupation
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.SROccupation);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.SROccupation, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.SRMaritalStatus);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.SRReferralGroup
		/// </summary>
		virtual public System.String SRReferralGroup
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.SRReferralGroup);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.SRReferralGroup, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.ReferralName
		/// </summary>
		virtual public System.String ReferralName
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.ReferralName);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.ReferralName, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.GuarantorCardNo
		/// </summary>
		virtual public System.String GuarantorCardNo
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.GuarantorCardNo);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.GuarantorCardNo, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.ReferenceNumber
		/// </summary>
		virtual public System.String ReferenceNumber
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.ReferenceNumber);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.ReferenceNumber, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.ReferenceType
		/// </summary>
		virtual public System.Int32? ReferenceType
		{
			get
			{
				return base.GetSystemInt32(AppointmentMetadata.ColumnNames.ReferenceType);
			}

			set
			{
				base.SetSystemInt32(AppointmentMetadata.ColumnNames.ReferenceType, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.SRAppoinmentType
		/// </summary>
		virtual public System.String SRAppoinmentType
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.SRAppoinmentType);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.SRAppoinmentType, value);
			}
		}
		/// <summary>
		/// Maps to Appointment.FromRegistrationNoMds
		/// </summary>
		virtual public System.String FromRegistrationNoMds
		{
			get
			{
				return base.GetSystemString(AppointmentMetadata.ColumnNames.FromRegistrationNoMds);
			}

			set
			{
				base.SetSystemString(AppointmentMetadata.ColumnNames.FromRegistrationNoMds, value);
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
			public esStrings(esAppointment entity)
			{
				this.entity = entity;
			}
			public System.String AppointmentNo
			{
				get
				{
					System.String data = entity.AppointmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentNo = null;
					else entity.AppointmentNo = Convert.ToString(value);
				}
			}
			public System.String AppointmentQue
			{
				get
				{
					System.Int32? data = entity.AppointmentQue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentQue = null;
					else entity.AppointmentQue = Convert.ToInt32(value);
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
			public System.String AppointmentDate
			{
				get
				{
					System.DateTime? data = entity.AppointmentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentDate = null;
					else entity.AppointmentDate = Convert.ToDateTime(value);
				}
			}
			public System.String AppointmentTime
			{
				get
				{
					System.String data = entity.AppointmentTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentTime = null;
					else entity.AppointmentTime = Convert.ToString(value);
				}
			}
			public System.String VisitTypeID
			{
				get
				{
					System.String data = entity.VisitTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitTypeID = null;
					else entity.VisitTypeID = Convert.ToString(value);
				}
			}
			public System.String VisitDuration
			{
				get
				{
					System.Byte? data = entity.VisitDuration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitDuration = null;
					else entity.VisitDuration = Convert.ToByte(value);
				}
			}
			public System.String SRAppointmentStatus
			{
				get
				{
					System.String data = entity.SRAppointmentStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAppointmentStatus = null;
					else entity.SRAppointmentStatus = Convert.ToString(value);
				}
			}
			public System.String FirstName
			{
				get
				{
					System.String data = entity.FirstName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FirstName = null;
					else entity.FirstName = Convert.ToString(value);
				}
			}
			public System.String MiddleName
			{
				get
				{
					System.String data = entity.MiddleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MiddleName = null;
					else entity.MiddleName = Convert.ToString(value);
				}
			}
			public System.String LastName
			{
				get
				{
					System.String data = entity.LastName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastName = null;
					else entity.LastName = Convert.ToString(value);
				}
			}
			public System.String StreetName
			{
				get
				{
					System.String data = entity.StreetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StreetName = null;
					else entity.StreetName = Convert.ToString(value);
				}
			}
			public System.String District
			{
				get
				{
					System.String data = entity.District;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.District = null;
					else entity.District = Convert.ToString(value);
				}
			}
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
			public System.String County
			{
				get
				{
					System.String data = entity.County;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.County = null;
					else entity.County = Convert.ToString(value);
				}
			}
			public System.String State
			{
				get
				{
					System.String data = entity.State;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.State = null;
					else entity.State = Convert.ToString(value);
				}
			}
			public System.String ZipCode
			{
				get
				{
					System.String data = entity.ZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipCode = null;
					else entity.ZipCode = Convert.ToString(value);
				}
			}
			public System.String PhoneNo
			{
				get
				{
					System.String data = entity.PhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNo = null;
					else entity.PhoneNo = Convert.ToString(value);
				}
			}
			public System.String FaxNo
			{
				get
				{
					System.String data = entity.FaxNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaxNo = null;
					else entity.FaxNo = Convert.ToString(value);
				}
			}
			public System.String Email
			{
				get
				{
					System.String data = entity.Email;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Email = null;
					else entity.Email = Convert.ToString(value);
				}
			}
			public System.String MobilePhoneNo
			{
				get
				{
					System.String data = entity.MobilePhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MobilePhoneNo = null;
					else entity.MobilePhoneNo = Convert.ToString(value);
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
			public System.String PatientPIC
			{
				get
				{
					System.String data = entity.PatientPIC;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientPIC = null;
					else entity.PatientPIC = Convert.ToString(value);
				}
			}
			public System.String OfficerPIC
			{
				get
				{
					System.String data = entity.OfficerPIC;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OfficerPIC = null;
					else entity.OfficerPIC = Convert.ToString(value);
				}
			}
			public System.String FollowUpDateTime
			{
				get
				{
					System.DateTime? data = entity.FollowUpDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FollowUpDateTime = null;
					else entity.FollowUpDateTime = Convert.ToDateTime(value);
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
			public System.String DateOfBirth
			{
				get
				{
					System.DateTime? data = entity.DateOfBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateOfBirth = null;
					else entity.DateOfBirth = Convert.ToDateTime(value);
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
			public System.String FromRegistrationNo
			{
				get
				{
					System.String data = entity.FromRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRegistrationNo = null;
					else entity.FromRegistrationNo = Convert.ToString(value);
				}
			}
			public System.String EmployeeNo
			{
				get
				{
					System.String data = entity.EmployeeNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeNo = null;
					else entity.EmployeeNo = Convert.ToString(value);
				}
			}
			public System.String EmployeeJobTitleName
			{
				get
				{
					System.String data = entity.EmployeeJobTitleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeJobTitleName = null;
					else entity.EmployeeJobTitleName = Convert.ToString(value);
				}
			}
			public System.String EmployeeJobDepartementName
			{
				get
				{
					System.String data = entity.EmployeeJobDepartementName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeJobDepartementName = null;
					else entity.EmployeeJobDepartementName = Convert.ToString(value);
				}
			}
			public System.String Sex
			{
				get
				{
					System.String data = entity.Sex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sex = null;
					else entity.Sex = Convert.ToString(value);
				}
			}
			public System.String BirthPlace
			{
				get
				{
					System.String data = entity.BirthPlace;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthPlace = null;
					else entity.BirthPlace = Convert.ToString(value);
				}
			}
			public System.String Ssn
			{
				get
				{
					System.String data = entity.Ssn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ssn = null;
					else entity.Ssn = Convert.ToString(value);
				}
			}
			public System.String SRSalutation
			{
				get
				{
					System.String data = entity.SRSalutation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSalutation = null;
					else entity.SRSalutation = Convert.ToString(value);
				}
			}
			public System.String SRNationality
			{
				get
				{
					System.String data = entity.SRNationality;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNationality = null;
					else entity.SRNationality = Convert.ToString(value);
				}
			}
			public System.String SROccupation
			{
				get
				{
					System.String data = entity.SROccupation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROccupation = null;
					else entity.SROccupation = Convert.ToString(value);
				}
			}
			public System.String SRMaritalStatus
			{
				get
				{
					System.String data = entity.SRMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
					else entity.SRMaritalStatus = Convert.ToString(value);
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
			public System.String SRReferralGroup
			{
				get
				{
					System.String data = entity.SRReferralGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReferralGroup = null;
					else entity.SRReferralGroup = Convert.ToString(value);
				}
			}
			public System.String ReferralName
			{
				get
				{
					System.String data = entity.ReferralName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralName = null;
					else entity.ReferralName = Convert.ToString(value);
				}
			}
			public System.String GuarantorCardNo
			{
				get
				{
					System.String data = entity.GuarantorCardNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorCardNo = null;
					else entity.GuarantorCardNo = Convert.ToString(value);
				}
			}
			public System.String ReferenceNumber
			{
				get
				{
					System.String data = entity.ReferenceNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNumber = null;
					else entity.ReferenceNumber = Convert.ToString(value);
				}
			}
			public System.String ReferenceType
			{
				get
				{
					System.Int32? data = entity.ReferenceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceType = null;
					else entity.ReferenceType = Convert.ToInt32(value);
				}
			}
			public System.String SRAppoinmentType
			{
				get
				{
					System.String data = entity.SRAppoinmentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAppoinmentType = null;
					else entity.SRAppoinmentType = Convert.ToString(value);
				}
			}
			public System.String FromRegistrationNoMds
			{
				get
				{
					System.String data = entity.FromRegistrationNoMds;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRegistrationNoMds = null;
					else entity.FromRegistrationNoMds = Convert.ToString(value);
				}
			}
			private esAppointment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppointmentQuery query)
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
				throw new Exception("esAppointment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Appointment : esAppointment
	{
	}

	[Serializable]
	abstract public class esAppointmentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppointmentMetadata.Meta();
			}
		}

		public esQueryItem AppointmentNo
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.AppointmentNo, esSystemType.String);
			}
		}

		public esQueryItem AppointmentQue
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.AppointmentQue, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem AppointmentDate
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.AppointmentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem AppointmentTime
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.AppointmentTime, esSystemType.String);
			}
		}

		public esQueryItem VisitTypeID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.VisitTypeID, esSystemType.String);
			}
		}

		public esQueryItem VisitDuration
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.VisitDuration, esSystemType.Byte);
			}
		}

		public esQueryItem SRAppointmentStatus
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.SRAppointmentStatus, esSystemType.String);
			}
		}

		public esQueryItem FirstName
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.FirstName, esSystemType.String);
			}
		}

		public esQueryItem MiddleName
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.MiddleName, esSystemType.String);
			}
		}

		public esQueryItem LastName
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.LastName, esSystemType.String);
			}
		}

		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.State, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		}

		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.Email, esSystemType.String);
			}
		}

		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem PatientPIC
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.PatientPIC, esSystemType.String);
			}
		}

		public esQueryItem OfficerPIC
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.OfficerPIC, esSystemType.String);
			}
		}

		public esQueryItem FollowUpDateTime
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.FollowUpDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastCreateDateTime
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastCreateByUserID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.LastCreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem DateOfBirth
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
			}
		}

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		}

		public esQueryItem FromRegistrationNo
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.FromRegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem EmployeeNo
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.EmployeeNo, esSystemType.String);
			}
		}

		public esQueryItem EmployeeJobTitleName
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.EmployeeJobTitleName, esSystemType.String);
			}
		}

		public esQueryItem EmployeeJobDepartementName
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.EmployeeJobDepartementName, esSystemType.String);
			}
		}

		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.Sex, esSystemType.String);
			}
		}

		public esQueryItem BirthPlace
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.BirthPlace, esSystemType.String);
			}
		}

		public esQueryItem Ssn
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.Ssn, esSystemType.String);
			}
		}

		public esQueryItem SRSalutation
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.SRSalutation, esSystemType.String);
			}
		}

		public esQueryItem SRNationality
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.SRNationality, esSystemType.String);
			}
		}

		public esQueryItem SROccupation
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.SROccupation, esSystemType.String);
			}
		}

		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem SRReferralGroup
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.SRReferralGroup, esSystemType.String);
			}
		}

		public esQueryItem ReferralName
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.ReferralName, esSystemType.String);
			}
		}

		public esQueryItem GuarantorCardNo
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.GuarantorCardNo, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNumber
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.ReferenceNumber, esSystemType.String);
			}
		}

		public esQueryItem ReferenceType
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.ReferenceType, esSystemType.Int32);
			}
		}

		public esQueryItem SRAppoinmentType
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.SRAppoinmentType, esSystemType.String);
			}
		}

		public esQueryItem FromRegistrationNoMds
		{
			get
			{
				return new esQueryItem(this, AppointmentMetadata.ColumnNames.FromRegistrationNoMds, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppointmentCollection")]
	public partial class AppointmentCollection : esAppointmentCollection, IEnumerable<Appointment>
	{
		public AppointmentCollection()
		{

		}

		public static implicit operator List<Appointment>(AppointmentCollection coll)
		{
			List<Appointment> list = new List<Appointment>();

			foreach (Appointment emp in coll)
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
				return AppointmentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Appointment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Appointment();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppointmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentQuery();
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
		public bool Load(AppointmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Appointment AddNew()
		{
			Appointment entity = base.AddNewEntity() as Appointment;

			return entity;
		}
		public Appointment FindByPrimaryKey(String appointmentNo)
		{
			return base.FindByPrimaryKey(appointmentNo) as Appointment;
		}

		#region IEnumerable< Appointment> Members

		IEnumerator<Appointment> IEnumerable<Appointment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Appointment;
			}
		}

		#endregion

		private AppointmentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Appointment' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Appointment ({AppointmentNo})")]
	[Serializable]
	public partial class Appointment : esAppointment
	{
		public Appointment()
		{
		}

		public Appointment(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppointmentMetadata.Meta();
			}
		}

		override protected esAppointmentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppointmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentQuery();
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
		public bool Load(AppointmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppointmentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppointmentQuery : esAppointmentQuery
	{
		public AppointmentQuery()
		{

		}

		public AppointmentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppointmentQuery";
		}
	}

	[Serializable]
	public partial class AppointmentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppointmentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.AppointmentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.AppointmentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.AppointmentQue, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppointmentMetadata.PropertyNames.AppointmentQue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.ParamedicID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.PatientID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.AppointmentDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentMetadata.PropertyNames.AppointmentDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.AppointmentTime, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.AppointmentTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.VisitTypeID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.VisitTypeID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.VisitDuration, 8, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = AppointmentMetadata.PropertyNames.VisitDuration;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.SRAppointmentStatus, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.SRAppointmentStatus;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.FirstName, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"(' ')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.MiddleName, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.LastName, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"(' ')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.StreetName, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.District, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.City, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.County, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.State, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.ZipCode, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.PhoneNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.FaxNo, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.Email, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.MobilePhoneNo, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.Notes, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.PatientPIC, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.PatientPIC;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.OfficerPIC, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.OfficerPIC;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.FollowUpDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentMetadata.PropertyNames.FollowUpDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.LastUpdateDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.LastUpdateByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.LastCreateDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentMetadata.PropertyNames.LastCreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.LastCreateByUserID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.LastCreateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.DateOfBirth, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentMetadata.PropertyNames.DateOfBirth;
			c.HasDefault = true;
			c.Default = @"(CONVERT([smalldatetime],'19000101',(0)))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.GuarantorID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.FromRegistrationNo, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.FromRegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.EmployeeNo, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.EmployeeNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.EmployeeJobTitleName, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.EmployeeJobTitleName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.EmployeeJobDepartementName, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.EmployeeJobDepartementName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.Sex, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.BirthPlace, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.BirthPlace;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.Ssn, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.Ssn;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.SRSalutation, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.SRSalutation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.SRNationality, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.SRNationality;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.SROccupation, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.SROccupation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.SRMaritalStatus, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.ItemID, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.SRReferralGroup, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.SRReferralGroup;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.ReferralName, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.ReferralName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.GuarantorCardNo, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.GuarantorCardNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.ReferenceNumber, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.ReferenceNumber;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.ReferenceType, 49, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppointmentMetadata.PropertyNames.ReferenceType;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.SRAppoinmentType, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.SRAppoinmentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentMetadata.ColumnNames.FromRegistrationNoMds, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentMetadata.PropertyNames.FromRegistrationNoMds;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppointmentMetadata Meta()
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
			public const string AppointmentNo = "AppointmentNo";
			public const string AppointmentQue = "AppointmentQue";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string PatientID = "PatientID";
			public const string AppointmentDate = "AppointmentDate";
			public const string AppointmentTime = "AppointmentTime";
			public const string VisitTypeID = "VisitTypeID";
			public const string VisitDuration = "VisitDuration";
			public const string SRAppointmentStatus = "SRAppointmentStatus";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Notes = "Notes";
			public const string PatientPIC = "PatientPIC";
			public const string OfficerPIC = "OfficerPIC";
			public const string FollowUpDateTime = "FollowUpDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateByUserID = "LastCreateByUserID";
			public const string DateOfBirth = "DateOfBirth";
			public const string GuarantorID = "GuarantorID";
			public const string FromRegistrationNo = "FromRegistrationNo";
			public const string EmployeeNo = "EmployeeNo";
			public const string EmployeeJobTitleName = "EmployeeJobTitleName";
			public const string EmployeeJobDepartementName = "EmployeeJobDepartementName";
			public const string Sex = "Sex";
			public const string BirthPlace = "BirthPlace";
			public const string Ssn = "Ssn";
			public const string SRSalutation = "SRSalutation";
			public const string SRNationality = "SRNationality";
			public const string SROccupation = "SROccupation";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string ItemID = "ItemID";
			public const string SRReferralGroup = "SRReferralGroup";
			public const string ReferralName = "ReferralName";
			public const string GuarantorCardNo = "GuarantorCardNo";
			public const string ReferenceNumber = "ReferenceNumber";
			public const string ReferenceType = "ReferenceType";
			public const string SRAppoinmentType = "SRAppoinmentType";
			public const string FromRegistrationNoMds = "FromRegistrationNoMds";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AppointmentNo = "AppointmentNo";
			public const string AppointmentQue = "AppointmentQue";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string PatientID = "PatientID";
			public const string AppointmentDate = "AppointmentDate";
			public const string AppointmentTime = "AppointmentTime";
			public const string VisitTypeID = "VisitTypeID";
			public const string VisitDuration = "VisitDuration";
			public const string SRAppointmentStatus = "SRAppointmentStatus";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Notes = "Notes";
			public const string PatientPIC = "PatientPIC";
			public const string OfficerPIC = "OfficerPIC";
			public const string FollowUpDateTime = "FollowUpDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateByUserID = "LastCreateByUserID";
			public const string DateOfBirth = "DateOfBirth";
			public const string GuarantorID = "GuarantorID";
			public const string FromRegistrationNo = "FromRegistrationNo";
			public const string EmployeeNo = "EmployeeNo";
			public const string EmployeeJobTitleName = "EmployeeJobTitleName";
			public const string EmployeeJobDepartementName = "EmployeeJobDepartementName";
			public const string Sex = "Sex";
			public const string BirthPlace = "BirthPlace";
			public const string Ssn = "Ssn";
			public const string SRSalutation = "SRSalutation";
			public const string SRNationality = "SRNationality";
			public const string SROccupation = "SROccupation";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string ItemID = "ItemID";
			public const string SRReferralGroup = "SRReferralGroup";
			public const string ReferralName = "ReferralName";
			public const string GuarantorCardNo = "GuarantorCardNo";
			public const string ReferenceNumber = "ReferenceNumber";
			public const string ReferenceType = "ReferenceType";
			public const string SRAppoinmentType = "SRAppoinmentType";
			public const string FromRegistrationNoMds = "FromRegistrationNoMds";
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
			lock (typeof(AppointmentMetadata))
			{
				if (AppointmentMetadata.mapDelegates == null)
				{
					AppointmentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppointmentMetadata.meta == null)
				{
					AppointmentMetadata.meta = new AppointmentMetadata();
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

				meta.AddTypeMap("AppointmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AppointmentQue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AppointmentDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("AppointmentTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("VisitTypeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VisitDuration", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("SRAppointmentStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientPIC", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OfficerPIC", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FollowUpDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateOfBirth", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeJobTitleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeJobDepartementName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("BirthPlace", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ssn", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSalutation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNationality", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReferralGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorCardNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceType", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRAppoinmentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRegistrationNoMds", new esTypeMap("varchar", "System.String"));


				meta.Source = "Appointment";
				meta.Destination = "Appointment";
				meta.spInsert = "proc_AppointmentInsert";
				meta.spUpdate = "proc_AppointmentUpdate";
				meta.spDelete = "proc_AppointmentDelete";
				meta.spLoadAll = "proc_AppointmentLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppointmentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppointmentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
