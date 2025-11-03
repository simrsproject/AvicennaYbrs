/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/27/2017 12:36:09 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esAppointmentLokadokCollection : esEntityCollectionWAuditLog
	{
		public esAppointmentLokadokCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AppointmentLokadokCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppointmentLokadokQuery query)
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
			this.InitQuery(query as esAppointmentLokadokQuery);
		}
		#endregion
		
		virtual public AppointmentLokadok DetachEntity(AppointmentLokadok entity)
		{
			return base.DetachEntity(entity) as AppointmentLokadok;
		}
		
		virtual public AppointmentLokadok AttachEntity(AppointmentLokadok entity)
		{
			return base.AttachEntity(entity) as AppointmentLokadok;
		}
		
		virtual public void Combine(AppointmentLokadokCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppointmentLokadok this[int index]
		{
			get
			{
				return base[index] as AppointmentLokadok;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppointmentLokadok);
		}
	}



	[Serializable]
	abstract public class esAppointmentLokadok : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppointmentLokadokQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppointmentLokadok()
		{

		}

		public esAppointmentLokadok(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 apptId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(apptId);
			else
				return LoadByPrimaryKeyStoredProcedure(apptId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 apptId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(apptId);
			else
				return LoadByPrimaryKeyStoredProcedure(apptId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 apptId)
		{
			esAppointmentLokadokQuery query = this.GetDynamicQuery();
			query.Where(query.ApptId == apptId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 apptId)
		{
			esParameters parms = new esParameters();
			parms.Add("appt_id",apptId);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "ApptId": this.str.ApptId = (string)value; break;							
						case "StartDate": this.str.StartDate = (string)value; break;							
						case "EndDate": this.str.EndDate = (string)value; break;							
						case "Place": this.str.Place = (string)value; break;							
						case "PId": this.str.PId = (string)value; break;							
						case "PName": this.str.PName = (string)value; break;							
						case "PDob": this.str.PDob = (string)value; break;							
						case "PGender": this.str.PGender = (string)value; break;							
						case "PMobile": this.str.PMobile = (string)value; break;							
						case "PInsurance": this.str.PInsurance = (string)value; break;							
						case "PInsId": this.str.PInsId = (string)value; break;							
						case "PInsNumber": this.str.PInsNumber = (string)value; break;							
						case "LkdPid": this.str.LkdPid = (string)value; break;							
						case "DocId": this.str.DocId = (string)value; break;							
						case "Doctor": this.str.Doctor = (string)value; break;							
						case "DocSpc": this.str.DocSpc = (string)value; break;							
						case "PolyId": this.str.PolyId = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "ReasonVisit": this.str.ReasonVisit = (string)value; break;							
						case "Confirmed": this.str.Confirmed = (string)value; break;							
						case "CheckedIn": this.str.CheckedIn = (string)value; break;							
						case "NewPatient": this.str.NewPatient = (string)value; break;							
						case "BookingCode": this.str.BookingCode = (string)value; break;							
						case "Status": this.str.Status = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "RegistrationNoSender": this.str.RegistrationNoSender = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApptId":
						
							if (value == null || value is System.Int64)
								this.ApptId = (System.Int64?)value;
							break;
						
						case "StartDate":
						
							if (value == null || value is System.Int64)
								this.StartDate = (System.Int64?)value;
							break;
						
						case "EndDate":
						
							if (value == null || value is System.Int64)
								this.EndDate = (System.Int64?)value;
							break;
						
						case "Place":
						
							if (value == null || value is System.Int32)
								this.Place = (System.Int32?)value;
							break;
						
						case "PDob":
						
							if (value == null || value is System.Int64)
								this.PDob = (System.Int64?)value;
							break;
						
						case "NewPatient":
						
							if (value == null || value is System.Boolean)
								this.NewPatient = (System.Boolean?)value;
							break;
					

						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}
		
		
		/// <summary>
		/// Maps to AppointmentLokadok.appt_id
		/// </summary>
		virtual public System.Int64? ApptId
		{
			get
			{
				return base.GetSystemInt64(AppointmentLokadokMetadata.ColumnNames.ApptId);
			}
			
			set
			{
				base.SetSystemInt64(AppointmentLokadokMetadata.ColumnNames.ApptId, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.start_date
		/// </summary>
		virtual public System.Int64? StartDate
		{
			get
			{
				return base.GetSystemInt64(AppointmentLokadokMetadata.ColumnNames.StartDate);
			}
			
			set
			{
				base.SetSystemInt64(AppointmentLokadokMetadata.ColumnNames.StartDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.end_date
		/// </summary>
		virtual public System.Int64? EndDate
		{
			get
			{
				return base.GetSystemInt64(AppointmentLokadokMetadata.ColumnNames.EndDate);
			}
			
			set
			{
				base.SetSystemInt64(AppointmentLokadokMetadata.ColumnNames.EndDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.place
		/// </summary>
		virtual public System.Int32? Place
		{
			get
			{
				return base.GetSystemInt32(AppointmentLokadokMetadata.ColumnNames.Place);
			}
			
			set
			{
				base.SetSystemInt32(AppointmentLokadokMetadata.ColumnNames.Place, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_id
		/// </summary>
		virtual public System.String PId
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PId);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PId, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_name
		/// </summary>
		virtual public System.String PName
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PName);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PName, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_dob
		/// </summary>
		virtual public System.Int64? PDob
		{
			get
			{
				return base.GetSystemInt64(AppointmentLokadokMetadata.ColumnNames.PDob);
			}
			
			set
			{
				base.SetSystemInt64(AppointmentLokadokMetadata.ColumnNames.PDob, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_gender
		/// </summary>
		virtual public System.String PGender
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PGender);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PGender, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_mobile
		/// </summary>
		virtual public System.String PMobile
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PMobile);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PMobile, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_insurance
		/// </summary>
		virtual public System.String PInsurance
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PInsurance);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PInsurance, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_ins_id
		/// </summary>
		virtual public System.String PInsId
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PInsId);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PInsId, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.p_ins_number
		/// </summary>
		virtual public System.String PInsNumber
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PInsNumber);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PInsNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.lkd_pid
		/// </summary>
		virtual public System.String LkdPid
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.LkdPid);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.LkdPid, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.doc_id
		/// </summary>
		virtual public System.String DocId
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.DocId);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.DocId, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.doctor
		/// </summary>
		virtual public System.String Doctor
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.Doctor);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.Doctor, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.doc_spc
		/// </summary>
		virtual public System.String DocSpc
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.DocSpc);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.DocSpc, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.poly_id
		/// </summary>
		virtual public System.String PolyId
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.PolyId);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.PolyId, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.reason_visit
		/// </summary>
		virtual public System.String ReasonVisit
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.ReasonVisit);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.ReasonVisit, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.confirmed
		/// </summary>
		virtual public System.String Confirmed
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.Confirmed);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.Confirmed, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.checked_in
		/// </summary>
		virtual public System.String CheckedIn
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.CheckedIn);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.CheckedIn, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.new_patient
		/// </summary>
		virtual public System.Boolean? NewPatient
		{
			get
			{
				return base.GetSystemBoolean(AppointmentLokadokMetadata.ColumnNames.NewPatient);
			}
			
			set
			{
				base.SetSystemBoolean(AppointmentLokadokMetadata.ColumnNames.NewPatient, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.booking_code
		/// </summary>
		virtual public System.String BookingCode
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.BookingCode);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.BookingCode, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.status
		/// </summary>
		virtual public System.String Status
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.Status);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.Status, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AppointmentLokadok.RegistrationNoSender
		/// </summary>
		virtual public System.String RegistrationNoSender
		{
			get
			{
				return base.GetSystemString(AppointmentLokadokMetadata.ColumnNames.RegistrationNoSender);
			}
			
			set
			{
				base.SetSystemString(AppointmentLokadokMetadata.ColumnNames.RegistrationNoSender, value);
			}
		}
		
		#endregion	

		#region String Properties


		[BrowsableAttribute( false )]
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
			public esStrings(esAppointmentLokadok entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApptId
			{
				get
				{
					System.Int64? data = entity.ApptId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApptId = null;
					else entity.ApptId = Convert.ToInt64(value);
				}
			}
				
			public System.String StartDate
			{
				get
				{
					System.Int64? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToInt64(value);
				}
			}
				
			public System.String EndDate
			{
				get
				{
					System.Int64? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToInt64(value);
				}
			}
				
			public System.String Place
			{
				get
				{
					System.Int32? data = entity.Place;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Place = null;
					else entity.Place = Convert.ToInt32(value);
				}
			}
				
			public System.String PId
			{
				get
				{
					System.String data = entity.PId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PId = null;
					else entity.PId = Convert.ToString(value);
				}
			}
				
			public System.String PName
			{
				get
				{
					System.String data = entity.PName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PName = null;
					else entity.PName = Convert.ToString(value);
				}
			}
				
			public System.String PDob
			{
				get
				{
					System.Int64? data = entity.PDob;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PDob = null;
					else entity.PDob = Convert.ToInt64(value);
				}
			}
				
			public System.String PGender
			{
				get
				{
					System.String data = entity.PGender;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PGender = null;
					else entity.PGender = Convert.ToString(value);
				}
			}
				
			public System.String PMobile
			{
				get
				{
					System.String data = entity.PMobile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PMobile = null;
					else entity.PMobile = Convert.ToString(value);
				}
			}
				
			public System.String PInsurance
			{
				get
				{
					System.String data = entity.PInsurance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PInsurance = null;
					else entity.PInsurance = Convert.ToString(value);
				}
			}
				
			public System.String PInsId
			{
				get
				{
					System.String data = entity.PInsId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PInsId = null;
					else entity.PInsId = Convert.ToString(value);
				}
			}
				
			public System.String PInsNumber
			{
				get
				{
					System.String data = entity.PInsNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PInsNumber = null;
					else entity.PInsNumber = Convert.ToString(value);
				}
			}
				
			public System.String LkdPid
			{
				get
				{
					System.String data = entity.LkdPid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LkdPid = null;
					else entity.LkdPid = Convert.ToString(value);
				}
			}
				
			public System.String DocId
			{
				get
				{
					System.String data = entity.DocId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocId = null;
					else entity.DocId = Convert.ToString(value);
				}
			}
				
			public System.String Doctor
			{
				get
				{
					System.String data = entity.Doctor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Doctor = null;
					else entity.Doctor = Convert.ToString(value);
				}
			}
				
			public System.String DocSpc
			{
				get
				{
					System.String data = entity.DocSpc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocSpc = null;
					else entity.DocSpc = Convert.ToString(value);
				}
			}
				
			public System.String PolyId
			{
				get
				{
					System.String data = entity.PolyId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PolyId = null;
					else entity.PolyId = Convert.ToString(value);
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
				
			public System.String ReasonVisit
			{
				get
				{
					System.String data = entity.ReasonVisit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonVisit = null;
					else entity.ReasonVisit = Convert.ToString(value);
				}
			}
				
			public System.String Confirmed
			{
				get
				{
					System.String data = entity.Confirmed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Confirmed = null;
					else entity.Confirmed = Convert.ToString(value);
				}
			}
				
			public System.String CheckedIn
			{
				get
				{
					System.String data = entity.CheckedIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckedIn = null;
					else entity.CheckedIn = Convert.ToString(value);
				}
			}
				
			public System.String NewPatient
			{
				get
				{
					System.Boolean? data = entity.NewPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewPatient = null;
					else entity.NewPatient = Convert.ToBoolean(value);
				}
			}
				
			public System.String BookingCode
			{
				get
				{
					System.String data = entity.BookingCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingCode = null;
					else entity.BookingCode = Convert.ToString(value);
				}
			}
				
			public System.String Status
			{
				get
				{
					System.String data = entity.Status;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Status = null;
					else entity.Status = Convert.ToString(value);
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
				
			public System.String RegistrationNoSender
			{
				get
				{
					System.String data = entity.RegistrationNoSender;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNoSender = null;
					else entity.RegistrationNoSender = Convert.ToString(value);
				}
			}
			

			private esAppointmentLokadok entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppointmentLokadokQuery query)
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
				throw new Exception("esAppointmentLokadok can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esAppointmentLokadokQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppointmentLokadokMetadata.Meta();
			}
		}	
		

		public esQueryItem ApptId
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.ApptId, esSystemType.Int64);
			}
		} 
		
		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.StartDate, esSystemType.Int64);
			}
		} 
		
		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.EndDate, esSystemType.Int64);
			}
		} 
		
		public esQueryItem Place
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.Place, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PId
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PId, esSystemType.String);
			}
		} 
		
		public esQueryItem PName
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PName, esSystemType.String);
			}
		} 
		
		public esQueryItem PDob
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PDob, esSystemType.Int64);
			}
		} 
		
		public esQueryItem PGender
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PGender, esSystemType.String);
			}
		} 
		
		public esQueryItem PMobile
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PMobile, esSystemType.String);
			}
		} 
		
		public esQueryItem PInsurance
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PInsurance, esSystemType.String);
			}
		} 
		
		public esQueryItem PInsId
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PInsId, esSystemType.String);
			}
		} 
		
		public esQueryItem PInsNumber
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PInsNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem LkdPid
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.LkdPid, esSystemType.String);
			}
		} 
		
		public esQueryItem DocId
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.DocId, esSystemType.String);
			}
		} 
		
		public esQueryItem Doctor
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.Doctor, esSystemType.String);
			}
		} 
		
		public esQueryItem DocSpc
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.DocSpc, esSystemType.String);
			}
		} 
		
		public esQueryItem PolyId
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.PolyId, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem ReasonVisit
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.ReasonVisit, esSystemType.String);
			}
		} 
		
		public esQueryItem Confirmed
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.Confirmed, esSystemType.String);
			}
		} 
		
		public esQueryItem CheckedIn
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.CheckedIn, esSystemType.String);
			}
		} 
		
		public esQueryItem NewPatient
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.NewPatient, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem BookingCode
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.BookingCode, esSystemType.String);
			}
		} 
		
		public esQueryItem Status
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.Status, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNoSender
		{
			get
			{
				return new esQueryItem(this, AppointmentLokadokMetadata.ColumnNames.RegistrationNoSender, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppointmentLokadokCollection")]
	public partial class AppointmentLokadokCollection : esAppointmentLokadokCollection, IEnumerable<AppointmentLokadok>
	{
		public AppointmentLokadokCollection()
		{

		}
		
		public static implicit operator List<AppointmentLokadok>(AppointmentLokadokCollection coll)
		{
			List<AppointmentLokadok> list = new List<AppointmentLokadok>();
			
			foreach (AppointmentLokadok emp in coll)
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
				return  AppointmentLokadokMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentLokadokQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppointmentLokadok(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppointmentLokadok();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AppointmentLokadokQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentLokadokQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AppointmentLokadokQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AppointmentLokadok AddNew()
		{
			AppointmentLokadok entity = base.AddNewEntity() as AppointmentLokadok;
			
			return entity;
		}

		public AppointmentLokadok FindByPrimaryKey(System.Int64 apptId)
		{
			return base.FindByPrimaryKey(apptId) as AppointmentLokadok;
		}


		#region IEnumerable<AppointmentLokadok> Members

		IEnumerator<AppointmentLokadok> IEnumerable<AppointmentLokadok>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppointmentLokadok;
			}
		}

		#endregion
		
		private AppointmentLokadokQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppointmentLokadok' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AppointmentLokadok ({ApptId})")]
	[Serializable]
	public partial class AppointmentLokadok : esAppointmentLokadok
	{
		public AppointmentLokadok()
		{

		}
	
		public AppointmentLokadok(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppointmentLokadokMetadata.Meta();
			}
		}
		
		
		
		override protected esAppointmentLokadokQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentLokadokQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AppointmentLokadokQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentLokadokQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AppointmentLokadokQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AppointmentLokadokQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AppointmentLokadokQuery : esAppointmentLokadokQuery
	{
		public AppointmentLokadokQuery()
		{

		}		
		
		public AppointmentLokadokQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AppointmentLokadokQuery";
        }
		
			
	}


	[Serializable]
	public partial class AppointmentLokadokMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppointmentLokadokMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.ApptId, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.ApptId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.StartDate, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.StartDate;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.EndDate, 2, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.EndDate;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.Place, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.Place;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PId, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PId;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PDob, 6, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PDob;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PGender, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PGender;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PMobile, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PMobile;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PInsurance, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PInsurance;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PInsId, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PInsId;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PInsNumber, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PInsNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.LkdPid, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.LkdPid;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.DocId, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.DocId;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.Doctor, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.Doctor;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.DocSpc, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.DocSpc;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.PolyId, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.PolyId;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.Notes, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.ReasonVisit, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.ReasonVisit;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.Confirmed, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.Confirmed;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.CheckedIn, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.CheckedIn;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.NewPatient, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.NewPatient;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.BookingCode, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.BookingCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.Status, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.Status;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.RegistrationNo, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppointmentLokadokMetadata.ColumnNames.RegistrationNoSender, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentLokadokMetadata.PropertyNames.RegistrationNoSender;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AppointmentLokadokMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string ApptId = "appt_id";
			 public const string StartDate = "start_date";
			 public const string EndDate = "end_date";
			 public const string Place = "place";
			 public const string PId = "p_id";
			 public const string PName = "p_name";
			 public const string PDob = "p_dob";
			 public const string PGender = "p_gender";
			 public const string PMobile = "p_mobile";
			 public const string PInsurance = "p_insurance";
			 public const string PInsId = "p_ins_id";
			 public const string PInsNumber = "p_ins_number";
			 public const string LkdPid = "lkd_pid";
			 public const string DocId = "doc_id";
			 public const string Doctor = "doctor";
			 public const string DocSpc = "doc_spc";
			 public const string PolyId = "poly_id";
			 public const string Notes = "notes";
			 public const string ReasonVisit = "reason_visit";
			 public const string Confirmed = "confirmed";
			 public const string CheckedIn = "checked_in";
			 public const string NewPatient = "new_patient";
			 public const string BookingCode = "booking_code";
			 public const string Status = "status";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string RegistrationNoSender = "RegistrationNoSender";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApptId = "ApptId";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string Place = "Place";
			 public const string PId = "PId";
			 public const string PName = "PName";
			 public const string PDob = "PDob";
			 public const string PGender = "PGender";
			 public const string PMobile = "PMobile";
			 public const string PInsurance = "PInsurance";
			 public const string PInsId = "PInsId";
			 public const string PInsNumber = "PInsNumber";
			 public const string LkdPid = "LkdPid";
			 public const string DocId = "DocId";
			 public const string Doctor = "Doctor";
			 public const string DocSpc = "DocSpc";
			 public const string PolyId = "PolyId";
			 public const string Notes = "Notes";
			 public const string ReasonVisit = "ReasonVisit";
			 public const string Confirmed = "Confirmed";
			 public const string CheckedIn = "CheckedIn";
			 public const string NewPatient = "NewPatient";
			 public const string BookingCode = "BookingCode";
			 public const string Status = "Status";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string RegistrationNoSender = "RegistrationNoSender";
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
			lock (typeof(AppointmentLokadokMetadata))
			{
				if(AppointmentLokadokMetadata.mapDelegates == null)
				{
					AppointmentLokadokMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppointmentLokadokMetadata.meta == null)
				{
					AppointmentLokadokMetadata.meta = new AppointmentLokadokMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				

				meta.AddTypeMap("ApptId", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("StartDate", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("EndDate", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("Place", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PDob", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PGender", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PMobile", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PInsurance", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PInsId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PInsNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LkdPid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Doctor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocSpc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PolyId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReasonVisit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Confirmed", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckedIn", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NewPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BookingCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Status", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNoSender", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AppointmentLokadok";
				meta.Destination = "AppointmentLokadok";
				
				meta.spInsert = "proc_AppointmentLokadokInsert";				
				meta.spUpdate = "proc_AppointmentLokadokUpdate";		
				meta.spDelete = "proc_AppointmentLokadokDelete";
				meta.spLoadAll = "proc_AppointmentLokadokLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppointmentLokadokLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppointmentLokadokMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
