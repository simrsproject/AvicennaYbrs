/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2022 3:25:33 PM
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
	abstract public class esPatientIncidentCollection : esEntityCollectionWAuditLog
	{
		public esPatientIncidentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientIncidentCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientIncidentQuery query)
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
			this.InitQuery(query as esPatientIncidentQuery);
		}
		#endregion

		virtual public PatientIncident DetachEntity(PatientIncident entity)
		{
			return base.DetachEntity(entity) as PatientIncident;
		}

		virtual public PatientIncident AttachEntity(PatientIncident entity)
		{
			return base.AttachEntity(entity) as PatientIncident;
		}

		virtual public void Combine(PatientIncidentCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientIncident this[int index]
		{
			get
			{
				return base[index] as PatientIncident;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientIncident);
		}
	}

	[Serializable]
	abstract public class esPatientIncident : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientIncidentQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientIncident()
		{
		}

		public esPatientIncident(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String patientIncidentNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientIncidentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(patientIncidentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientIncidentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientIncidentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(patientIncidentNo);
		}

		private bool LoadByPrimaryKeyDynamic(String patientIncidentNo)
		{
			esPatientIncidentQuery query = this.GetDynamicQuery();
			query.Where(query.PatientIncidentNo == patientIncidentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String patientIncidentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientIncidentNo", patientIncidentNo);
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
						case "PatientIncidentNo": this.str.PatientIncidentNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
						case "IncidentLocation": this.str.IncidentLocation = (string)value; break;
						case "ServiceUnitIDInCharge": this.str.ServiceUnitIDInCharge = (string)value; break;
						case "IncidentDateTime": this.str.IncidentDateTime = (string)value; break;
						case "ReportingDateTime": this.str.ReportingDateTime = (string)value; break;
						case "IncidentName": this.str.IncidentName = (string)value; break;
						case "Chronology": this.str.Chronology = (string)value; break;
						case "SRIncidentType": this.str.SRIncidentType = (string)value; break;
						case "SRIncidentGroup": this.str.SRIncidentGroup = (string)value; break;
						case "SRClinicalImpact": this.str.SRClinicalImpact = (string)value; break;
						case "SRIncidentProbabilityFrequency": this.str.SRIncidentProbabilityFrequency = (string)value; break;
						case "Handling": this.str.Handling = (string)value; break;
						case "SRIncidentHandledBy": this.str.SRIncidentHandledBy = (string)value; break;
						case "SRIncidentFollowUp": this.str.SRIncidentFollowUp = (string)value; break;
						case "FollowUpDate": this.str.FollowUpDate = (string)value; break;
						case "ReportedByUserID": this.str.ReportedByUserID = (string)value; break;
						case "InsertByUserID": this.str.InsertByUserID = (string)value; break;
						case "InsertDateTime": this.str.InsertDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "SRIncidentOccurredOn": this.str.SRIncidentOccurredOn = (string)value; break;
						case "IncidentOccurredOnName": this.str.IncidentOccurredOnName = (string)value; break;
						case "SRIncidentOccurredInPatientsWith": this.str.SRIncidentOccurredInPatientsWith = (string)value; break;
						case "IncidentOccurredInPatientsWithName": this.str.IncidentOccurredInPatientsWithName = (string)value; break;
						case "IsOccurInOtherUnits": this.str.IsOccurInOtherUnits = (string)value; break;
						case "OccurInOtherUnitsNotes": this.str.OccurInOtherUnitsNotes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "FirstName": this.str.FirstName = (string)value; break;
						case "MiddleName": this.str.MiddleName = (string)value; break;
						case "LastName": this.str.LastName = (string)value; break;
						case "Sex": this.str.Sex = (string)value; break;
						case "DateOfBirth": this.str.DateOfBirth = (string)value; break;
						case "Address": this.str.Address = (string)value; break;
						case "NonPatient": this.str.NonPatient = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "ServiceUnitIncidentLocationID": this.str.ServiceUnitIncidentLocationID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "IsRiskManagement": this.str.IsRiskManagement = (string)value; break;
						case "SRIncidentGroupPrev": this.str.SRIncidentGroupPrev = (string)value; break;
						case "SRClinicalImpactPrev": this.str.SRClinicalImpactPrev = (string)value; break;
						case "SRIncidentProbabilityFrequencyPrev": this.str.SRIncidentProbabilityFrequencyPrev = (string)value; break;
						case "SRIncidentFollowUpPrev": this.str.SRIncidentFollowUpPrev = (string)value; break;
						case "InitialName": this.str.InitialName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IncidentDateTime":

							if (value == null || value is System.DateTime)
								this.IncidentDateTime = (System.DateTime?)value;
							break;
						case "ReportingDateTime":

							if (value == null || value is System.DateTime)
								this.ReportingDateTime = (System.DateTime?)value;
							break;
						case "FollowUpDate":

							if (value == null || value is System.DateTime)
								this.FollowUpDate = (System.DateTime?)value;
							break;
						case "InsertDateTime":

							if (value == null || value is System.DateTime)
								this.InsertDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsOccurInOtherUnits":

							if (value == null || value is System.Boolean)
								this.IsOccurInOtherUnits = (System.Boolean?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "DateOfBirth":

							if (value == null || value is System.DateTime)
								this.DateOfBirth = (System.DateTime?)value;
							break;
						case "NonPatient":

							if (value == null || value is System.Boolean)
								this.NonPatient = (System.Boolean?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "IsRiskManagement":

							if (value == null || value is System.Boolean)
								this.IsRiskManagement = (System.Boolean?)value;
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
		/// Maps to PatientIncident.PatientIncidentNo
		/// </summary>
		virtual public System.String PatientIncidentNo
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.PatientIncidentNo);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.PatientIncidentNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IncidentLocation
		/// </summary>
		virtual public System.String IncidentLocation
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.IncidentLocation);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.IncidentLocation, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ServiceUnitIDInCharge
		/// </summary>
		virtual public System.String ServiceUnitIDInCharge
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.ServiceUnitIDInCharge);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.ServiceUnitIDInCharge, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IncidentDateTime
		/// </summary>
		virtual public System.DateTime? IncidentDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.IncidentDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.IncidentDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ReportingDateTime
		/// </summary>
		virtual public System.DateTime? ReportingDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.ReportingDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.ReportingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IncidentName
		/// </summary>
		virtual public System.String IncidentName
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.IncidentName);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.IncidentName, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.Chronology
		/// </summary>
		virtual public System.String Chronology
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.Chronology);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.Chronology, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentType
		/// </summary>
		virtual public System.String SRIncidentType
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentType);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentType, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentGroup
		/// </summary>
		virtual public System.String SRIncidentGroup
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentGroup);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentGroup, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRClinicalImpact
		/// </summary>
		virtual public System.String SRClinicalImpact
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRClinicalImpact);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRClinicalImpact, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentProbabilityFrequency
		/// </summary>
		virtual public System.String SRIncidentProbabilityFrequency
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequency);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequency, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.Handling
		/// </summary>
		virtual public System.String Handling
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.Handling);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.Handling, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentHandledBy
		/// </summary>
		virtual public System.String SRIncidentHandledBy
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentHandledBy);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentHandledBy, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentFollowUp
		/// </summary>
		virtual public System.String SRIncidentFollowUp
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentFollowUp);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentFollowUp, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.FollowUpDate
		/// </summary>
		virtual public System.DateTime? FollowUpDate
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.FollowUpDate);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.FollowUpDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ReportedByUserID
		/// </summary>
		virtual public System.String ReportedByUserID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.ReportedByUserID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.ReportedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.InsertByUserID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.InsertDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentOccurredOn
		/// </summary>
		virtual public System.String SRIncidentOccurredOn
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentOccurredOn);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentOccurredOn, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IncidentOccurredOnName
		/// </summary>
		virtual public System.String IncidentOccurredOnName
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.IncidentOccurredOnName);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.IncidentOccurredOnName, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentOccurredInPatientsWith
		/// </summary>
		virtual public System.String SRIncidentOccurredInPatientsWith
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentOccurredInPatientsWith);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentOccurredInPatientsWith, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IncidentOccurredInPatientsWithName
		/// </summary>
		virtual public System.String IncidentOccurredInPatientsWithName
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.IncidentOccurredInPatientsWithName);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.IncidentOccurredInPatientsWithName, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IsOccurInOtherUnits
		/// </summary>
		virtual public System.Boolean? IsOccurInOtherUnits
		{
			get
			{
				return base.GetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsOccurInOtherUnits);
			}

			set
			{
				base.SetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsOccurInOtherUnits, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.OccurInOtherUnitsNotes
		/// </summary>
		virtual public System.String OccurInOtherUnitsNotes
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.OccurInOtherUnitsNotes);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.OccurInOtherUnitsNotes, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.FirstName
		/// </summary>
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.FirstName);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.FirstName, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.MiddleName
		/// </summary>
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.MiddleName);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.MiddleName, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.LastName
		/// </summary>
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.LastName);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.LastName, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.Sex
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.Sex);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.Sex, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.DateOfBirth
		/// </summary>
		virtual public System.DateTime? DateOfBirth
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.DateOfBirth);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.DateOfBirth, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.Address);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.Address, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.NonPatient
		/// </summary>
		virtual public System.Boolean? NonPatient
		{
			get
			{
				return base.GetSystemBoolean(PatientIncidentMetadata.ColumnNames.NonPatient);
			}

			set
			{
				base.SetSystemBoolean(PatientIncidentMetadata.ColumnNames.NonPatient, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ServiceUnitIncidentLocationID
		/// </summary>
		virtual public System.String ServiceUnitIncidentLocationID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.ServiceUnitIncidentLocationID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.ServiceUnitIncidentLocationID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.IsRiskManagement
		/// </summary>
		virtual public System.Boolean? IsRiskManagement
		{
			get
			{
				return base.GetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsRiskManagement);
			}

			set
			{
				base.SetSystemBoolean(PatientIncidentMetadata.ColumnNames.IsRiskManagement, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentGroupPrev
		/// </summary>
		virtual public System.String SRIncidentGroupPrev
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentGroupPrev);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentGroupPrev, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRClinicalImpactPrev
		/// </summary>
		virtual public System.String SRClinicalImpactPrev
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRClinicalImpactPrev);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRClinicalImpactPrev, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentProbabilityFrequencyPrev
		/// </summary>
		virtual public System.String SRIncidentProbabilityFrequencyPrev
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequencyPrev);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequencyPrev, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.SRIncidentFollowUpPrev
		/// </summary>
		virtual public System.String SRIncidentFollowUpPrev
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentFollowUpPrev);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.SRIncidentFollowUpPrev, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncident.InitialName
		/// </summary>
		virtual public System.String InitialName
		{
			get
			{
				return base.GetSystemString(PatientIncidentMetadata.ColumnNames.InitialName);
			}

			set
			{
				base.SetSystemString(PatientIncidentMetadata.ColumnNames.InitialName, value);
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
			public esStrings(esPatientIncident entity)
			{
				this.entity = entity;
			}
			public System.String PatientIncidentNo
			{
				get
				{
					System.String data = entity.PatientIncidentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientIncidentNo = null;
					else entity.PatientIncidentNo = Convert.ToString(value);
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
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			public System.String IncidentLocation
			{
				get
				{
					System.String data = entity.IncidentLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentLocation = null;
					else entity.IncidentLocation = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitIDInCharge
			{
				get
				{
					System.String data = entity.ServiceUnitIDInCharge;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitIDInCharge = null;
					else entity.ServiceUnitIDInCharge = Convert.ToString(value);
				}
			}
			public System.String IncidentDateTime
			{
				get
				{
					System.DateTime? data = entity.IncidentDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentDateTime = null;
					else entity.IncidentDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReportingDateTime
			{
				get
				{
					System.DateTime? data = entity.ReportingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportingDateTime = null;
					else entity.ReportingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IncidentName
			{
				get
				{
					System.String data = entity.IncidentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentName = null;
					else entity.IncidentName = Convert.ToString(value);
				}
			}
			public System.String Chronology
			{
				get
				{
					System.String data = entity.Chronology;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Chronology = null;
					else entity.Chronology = Convert.ToString(value);
				}
			}
			public System.String SRIncidentType
			{
				get
				{
					System.String data = entity.SRIncidentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentType = null;
					else entity.SRIncidentType = Convert.ToString(value);
				}
			}
			public System.String SRIncidentGroup
			{
				get
				{
					System.String data = entity.SRIncidentGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentGroup = null;
					else entity.SRIncidentGroup = Convert.ToString(value);
				}
			}
			public System.String SRClinicalImpact
			{
				get
				{
					System.String data = entity.SRClinicalImpact;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalImpact = null;
					else entity.SRClinicalImpact = Convert.ToString(value);
				}
			}
			public System.String SRIncidentProbabilityFrequency
			{
				get
				{
					System.String data = entity.SRIncidentProbabilityFrequency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentProbabilityFrequency = null;
					else entity.SRIncidentProbabilityFrequency = Convert.ToString(value);
				}
			}
			public System.String Handling
			{
				get
				{
					System.String data = entity.Handling;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Handling = null;
					else entity.Handling = Convert.ToString(value);
				}
			}
			public System.String SRIncidentHandledBy
			{
				get
				{
					System.String data = entity.SRIncidentHandledBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentHandledBy = null;
					else entity.SRIncidentHandledBy = Convert.ToString(value);
				}
			}
			public System.String SRIncidentFollowUp
			{
				get
				{
					System.String data = entity.SRIncidentFollowUp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentFollowUp = null;
					else entity.SRIncidentFollowUp = Convert.ToString(value);
				}
			}
			public System.String FollowUpDate
			{
				get
				{
					System.DateTime? data = entity.FollowUpDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FollowUpDate = null;
					else entity.FollowUpDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReportedByUserID
			{
				get
				{
					System.String data = entity.ReportedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportedByUserID = null;
					else entity.ReportedByUserID = Convert.ToString(value);
				}
			}
			public System.String InsertByUserID
			{
				get
				{
					System.String data = entity.InsertByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertByUserID = null;
					else entity.InsertByUserID = Convert.ToString(value);
				}
			}
			public System.String InsertDateTime
			{
				get
				{
					System.DateTime? data = entity.InsertDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertDateTime = null;
					else entity.InsertDateTime = Convert.ToDateTime(value);
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
			public System.String SRIncidentOccurredOn
			{
				get
				{
					System.String data = entity.SRIncidentOccurredOn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentOccurredOn = null;
					else entity.SRIncidentOccurredOn = Convert.ToString(value);
				}
			}
			public System.String IncidentOccurredOnName
			{
				get
				{
					System.String data = entity.IncidentOccurredOnName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentOccurredOnName = null;
					else entity.IncidentOccurredOnName = Convert.ToString(value);
				}
			}
			public System.String SRIncidentOccurredInPatientsWith
			{
				get
				{
					System.String data = entity.SRIncidentOccurredInPatientsWith;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentOccurredInPatientsWith = null;
					else entity.SRIncidentOccurredInPatientsWith = Convert.ToString(value);
				}
			}
			public System.String IncidentOccurredInPatientsWithName
			{
				get
				{
					System.String data = entity.IncidentOccurredInPatientsWithName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentOccurredInPatientsWithName = null;
					else entity.IncidentOccurredInPatientsWithName = Convert.ToString(value);
				}
			}
			public System.String IsOccurInOtherUnits
			{
				get
				{
					System.Boolean? data = entity.IsOccurInOtherUnits;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOccurInOtherUnits = null;
					else entity.IsOccurInOtherUnits = Convert.ToBoolean(value);
				}
			}
			public System.String OccurInOtherUnitsNotes
			{
				get
				{
					System.String data = entity.OccurInOtherUnitsNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OccurInOtherUnitsNotes = null;
					else entity.OccurInOtherUnitsNotes = Convert.ToString(value);
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
			public System.String Address
			{
				get
				{
					System.String data = entity.Address;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address = null;
					else entity.Address = Convert.ToString(value);
				}
			}
			public System.String NonPatient
			{
				get
				{
					System.Boolean? data = entity.NonPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NonPatient = null;
					else entity.NonPatient = Convert.ToBoolean(value);
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
			public System.String ServiceUnitIncidentLocationID
			{
				get
				{
					System.String data = entity.ServiceUnitIncidentLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitIncidentLocationID = null;
					else entity.ServiceUnitIncidentLocationID = Convert.ToString(value);
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
			public System.String IsRiskManagement
			{
				get
				{
					System.Boolean? data = entity.IsRiskManagement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRiskManagement = null;
					else entity.IsRiskManagement = Convert.ToBoolean(value);
				}
			}
			public System.String SRIncidentGroupPrev
			{
				get
				{
					System.String data = entity.SRIncidentGroupPrev;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentGroupPrev = null;
					else entity.SRIncidentGroupPrev = Convert.ToString(value);
				}
			}
			public System.String SRClinicalImpactPrev
			{
				get
				{
					System.String data = entity.SRClinicalImpactPrev;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalImpactPrev = null;
					else entity.SRClinicalImpactPrev = Convert.ToString(value);
				}
			}
			public System.String SRIncidentProbabilityFrequencyPrev
			{
				get
				{
					System.String data = entity.SRIncidentProbabilityFrequencyPrev;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentProbabilityFrequencyPrev = null;
					else entity.SRIncidentProbabilityFrequencyPrev = Convert.ToString(value);
				}
			}
			public System.String SRIncidentFollowUpPrev
			{
				get
				{
					System.String data = entity.SRIncidentFollowUpPrev;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentFollowUpPrev = null;
					else entity.SRIncidentFollowUpPrev = Convert.ToString(value);
				}
			}
			public System.String InitialName
			{
				get
				{
					System.String data = entity.InitialName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialName = null;
					else entity.InitialName = Convert.ToString(value);
				}
			}
			private esPatientIncident entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientIncidentQuery query)
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
				throw new Exception("esPatientIncident can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientIncident : esPatientIncident
	{
	}

	[Serializable]
	abstract public class esPatientIncidentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientIncidentMetadata.Meta();
			}
		}

		public esQueryItem PatientIncidentNo
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem IncidentLocation
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IncidentLocation, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitIDInCharge
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ServiceUnitIDInCharge, esSystemType.String);
			}
		}

		public esQueryItem IncidentDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IncidentDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ReportingDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ReportingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IncidentName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IncidentName, esSystemType.String);
			}
		}

		public esQueryItem Chronology
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.Chronology, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentType
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentType, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentGroup
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentGroup, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalImpact
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRClinicalImpact, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentProbabilityFrequency
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequency, esSystemType.String);
			}
		}

		public esQueryItem Handling
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.Handling, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentHandledBy
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentHandledBy, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentFollowUp
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentFollowUp, esSystemType.String);
			}
		}

		public esQueryItem FollowUpDate
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.FollowUpDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReportedByUserID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ReportedByUserID, esSystemType.String);
			}
		}

		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		}

		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem SRIncidentOccurredOn
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentOccurredOn, esSystemType.String);
			}
		}

		public esQueryItem IncidentOccurredOnName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IncidentOccurredOnName, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentOccurredInPatientsWith
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentOccurredInPatientsWith, esSystemType.String);
			}
		}

		public esQueryItem IncidentOccurredInPatientsWithName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IncidentOccurredInPatientsWithName, esSystemType.String);
			}
		}

		public esQueryItem IsOccurInOtherUnits
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IsOccurInOtherUnits, esSystemType.Boolean);
			}
		}

		public esQueryItem OccurInOtherUnitsNotes
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.OccurInOtherUnitsNotes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem FirstName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.FirstName, esSystemType.String);
			}
		}

		public esQueryItem MiddleName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.MiddleName, esSystemType.String);
			}
		}

		public esQueryItem LastName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.LastName, esSystemType.String);
			}
		}

		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.Sex, esSystemType.String);
			}
		}

		public esQueryItem DateOfBirth
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
			}
		}

		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.Address, esSystemType.String);
			}
		}

		public esQueryItem NonPatient
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.NonPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitIncidentLocationID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ServiceUnitIncidentLocationID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem IsRiskManagement
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.IsRiskManagement, esSystemType.Boolean);
			}
		}

		public esQueryItem SRIncidentGroupPrev
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentGroupPrev, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalImpactPrev
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRClinicalImpactPrev, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentProbabilityFrequencyPrev
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequencyPrev, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentFollowUpPrev
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.SRIncidentFollowUpPrev, esSystemType.String);
			}
		}

		public esQueryItem InitialName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentMetadata.ColumnNames.InitialName, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientIncidentCollection")]
	public partial class PatientIncidentCollection : esPatientIncidentCollection, IEnumerable<PatientIncident>
	{
		public PatientIncidentCollection()
		{

		}

		public static implicit operator List<PatientIncident>(PatientIncidentCollection coll)
		{
			List<PatientIncident> list = new List<PatientIncident>();

			foreach (PatientIncident emp in coll)
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
				return PatientIncidentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientIncidentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientIncident(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientIncident();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientIncidentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientIncidentQuery();
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
		public bool Load(PatientIncidentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientIncident AddNew()
		{
			PatientIncident entity = base.AddNewEntity() as PatientIncident;

			return entity;
		}
		public PatientIncident FindByPrimaryKey(String patientIncidentNo)
		{
			return base.FindByPrimaryKey(patientIncidentNo) as PatientIncident;
		}

		#region IEnumerable< PatientIncident> Members

		IEnumerator<PatientIncident> IEnumerable<PatientIncident>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientIncident;
			}
		}

		#endregion

		private PatientIncidentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientIncident' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientIncident ({PatientIncidentNo})")]
	[Serializable]
	public partial class PatientIncident : esPatientIncident
	{
		public PatientIncident()
		{
		}

		public PatientIncident(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientIncidentMetadata.Meta();
			}
		}

		override protected esPatientIncidentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientIncidentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientIncidentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientIncidentQuery();
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
		public bool Load(PatientIncidentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientIncidentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientIncidentQuery : esPatientIncidentQuery
	{
		public PatientIncidentQuery()
		{

		}

		public PatientIncidentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientIncidentQuery";
		}
	}

	[Serializable]
	public partial class PatientIncidentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientIncidentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.PatientIncidentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.RoomID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.BedID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IncidentLocation, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IncidentLocation;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ServiceUnitIDInCharge, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ServiceUnitIDInCharge;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IncidentDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IncidentDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ReportingDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ReportingDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IncidentName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IncidentName;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.Chronology, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.Chronology;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentType, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentType;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentGroup, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentGroup;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRClinicalImpact, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRClinicalImpact;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequency, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentProbabilityFrequency;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.Handling, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.Handling;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentHandledBy, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentHandledBy;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentFollowUp, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentFollowUp;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.FollowUpDate, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.FollowUpDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ReportedByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ReportedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.InsertByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.InsertDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.InsertDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.LastUpdateDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentOccurredOn, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentOccurredOn;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IncidentOccurredOnName, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IncidentOccurredOnName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentOccurredInPatientsWith, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentOccurredInPatientsWith;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IncidentOccurredInPatientsWithName, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IncidentOccurredInPatientsWithName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IsOccurInOtherUnits, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IsOccurInOtherUnits;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.OccurInOtherUnitsNotes, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.OccurInOtherUnitsNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IsApproved, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ApprovedByUserID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ApprovedDateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.PatientID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.FirstName, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.MiddleName, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.LastName, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.Sex, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.DateOfBirth, 38, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.DateOfBirth;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.Address, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.NonPatient, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.NonPatient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IsVerified, 41, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.VerifiedByUserID, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.VerifiedDateTime, 43, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ServiceUnitIncidentLocationID, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ServiceUnitIncidentLocationID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.ParamedicID, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.IsRiskManagement, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.IsRiskManagement;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentGroupPrev, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentGroupPrev;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRClinicalImpactPrev, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRClinicalImpactPrev;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentProbabilityFrequencyPrev, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentProbabilityFrequencyPrev;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.SRIncidentFollowUpPrev, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.SRIncidentFollowUpPrev;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentMetadata.ColumnNames.InitialName, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentMetadata.PropertyNames.InitialName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientIncidentMetadata Meta()
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
			public const string PatientIncidentNo = "PatientIncidentNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
			public const string IncidentLocation = "IncidentLocation";
			public const string ServiceUnitIDInCharge = "ServiceUnitIDInCharge";
			public const string IncidentDateTime = "IncidentDateTime";
			public const string ReportingDateTime = "ReportingDateTime";
			public const string IncidentName = "IncidentName";
			public const string Chronology = "Chronology";
			public const string SRIncidentType = "SRIncidentType";
			public const string SRIncidentGroup = "SRIncidentGroup";
			public const string SRClinicalImpact = "SRClinicalImpact";
			public const string SRIncidentProbabilityFrequency = "SRIncidentProbabilityFrequency";
			public const string Handling = "Handling";
			public const string SRIncidentHandledBy = "SRIncidentHandledBy";
			public const string SRIncidentFollowUp = "SRIncidentFollowUp";
			public const string FollowUpDate = "FollowUpDate";
			public const string ReportedByUserID = "ReportedByUserID";
			public const string InsertByUserID = "InsertByUserID";
			public const string InsertDateTime = "InsertDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRIncidentOccurredOn = "SRIncidentOccurredOn";
			public const string IncidentOccurredOnName = "IncidentOccurredOnName";
			public const string SRIncidentOccurredInPatientsWith = "SRIncidentOccurredInPatientsWith";
			public const string IncidentOccurredInPatientsWithName = "IncidentOccurredInPatientsWithName";
			public const string IsOccurInOtherUnits = "IsOccurInOtherUnits";
			public const string OccurInOtherUnitsNotes = "OccurInOtherUnitsNotes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string PatientID = "PatientID";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string Sex = "Sex";
			public const string DateOfBirth = "DateOfBirth";
			public const string Address = "Address";
			public const string NonPatient = "NonPatient";
			public const string IsVerified = "IsVerified";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string ServiceUnitIncidentLocationID = "ServiceUnitIncidentLocationID";
			public const string ParamedicID = "ParamedicID";
			public const string IsRiskManagement = "IsRiskManagement";
			public const string SRIncidentGroupPrev = "SRIncidentGroupPrev";
			public const string SRClinicalImpactPrev = "SRClinicalImpactPrev";
			public const string SRIncidentProbabilityFrequencyPrev = "SRIncidentProbabilityFrequencyPrev";
			public const string SRIncidentFollowUpPrev = "SRIncidentFollowUpPrev";
			public const string InitialName = "InitialName";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PatientIncidentNo = "PatientIncidentNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string BedID = "BedID";
			public const string IncidentLocation = "IncidentLocation";
			public const string ServiceUnitIDInCharge = "ServiceUnitIDInCharge";
			public const string IncidentDateTime = "IncidentDateTime";
			public const string ReportingDateTime = "ReportingDateTime";
			public const string IncidentName = "IncidentName";
			public const string Chronology = "Chronology";
			public const string SRIncidentType = "SRIncidentType";
			public const string SRIncidentGroup = "SRIncidentGroup";
			public const string SRClinicalImpact = "SRClinicalImpact";
			public const string SRIncidentProbabilityFrequency = "SRIncidentProbabilityFrequency";
			public const string Handling = "Handling";
			public const string SRIncidentHandledBy = "SRIncidentHandledBy";
			public const string SRIncidentFollowUp = "SRIncidentFollowUp";
			public const string FollowUpDate = "FollowUpDate";
			public const string ReportedByUserID = "ReportedByUserID";
			public const string InsertByUserID = "InsertByUserID";
			public const string InsertDateTime = "InsertDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string SRIncidentOccurredOn = "SRIncidentOccurredOn";
			public const string IncidentOccurredOnName = "IncidentOccurredOnName";
			public const string SRIncidentOccurredInPatientsWith = "SRIncidentOccurredInPatientsWith";
			public const string IncidentOccurredInPatientsWithName = "IncidentOccurredInPatientsWithName";
			public const string IsOccurInOtherUnits = "IsOccurInOtherUnits";
			public const string OccurInOtherUnitsNotes = "OccurInOtherUnitsNotes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string PatientID = "PatientID";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string Sex = "Sex";
			public const string DateOfBirth = "DateOfBirth";
			public const string Address = "Address";
			public const string NonPatient = "NonPatient";
			public const string IsVerified = "IsVerified";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string ServiceUnitIncidentLocationID = "ServiceUnitIncidentLocationID";
			public const string ParamedicID = "ParamedicID";
			public const string IsRiskManagement = "IsRiskManagement";
			public const string SRIncidentGroupPrev = "SRIncidentGroupPrev";
			public const string SRClinicalImpactPrev = "SRClinicalImpactPrev";
			public const string SRIncidentProbabilityFrequencyPrev = "SRIncidentProbabilityFrequencyPrev";
			public const string SRIncidentFollowUpPrev = "SRIncidentFollowUpPrev";
			public const string InitialName = "InitialName";
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
			lock (typeof(PatientIncidentMetadata))
			{
				if (PatientIncidentMetadata.mapDelegates == null)
				{
					PatientIncidentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientIncidentMetadata.meta == null)
				{
					PatientIncidentMetadata.meta = new PatientIncidentMetadata();
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

				meta.AddTypeMap("PatientIncidentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitIDInCharge", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReportingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IncidentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Chronology", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalImpact", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentProbabilityFrequency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Handling", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentHandledBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentFollowUp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FollowUpDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("ReportedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRIncidentOccurredOn", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentOccurredOnName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentOccurredInPatientsWith", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentOccurredInPatientsWithName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOccurInOtherUnits", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OccurInOtherUnitsNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("DateOfBirth", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NonPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitIncidentLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRiskManagement", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRIncidentGroupPrev", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalImpactPrev", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentProbabilityFrequencyPrev", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentFollowUpPrev", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InitialName", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientIncident";
				meta.Destination = "PatientIncident";
				meta.spInsert = "proc_PatientIncidentInsert";
				meta.spUpdate = "proc_PatientIncidentUpdate";
				meta.spDelete = "proc_PatientIncidentDelete";
				meta.spLoadAll = "proc_PatientIncidentLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientIncidentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientIncidentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
