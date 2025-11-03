/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/19/2021 8:27:25 PM
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
	abstract public class esEmployeeNeedleStickInjuryCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeNeedleStickInjuryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeNeedleStickInjuryCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeNeedleStickInjuryQuery query)
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
			this.InitQuery(query as esEmployeeNeedleStickInjuryQuery);
		}
		#endregion

		virtual public EmployeeNeedleStickInjury DetachEntity(EmployeeNeedleStickInjury entity)
		{
			return base.DetachEntity(entity) as EmployeeNeedleStickInjury;
		}

		virtual public EmployeeNeedleStickInjury AttachEntity(EmployeeNeedleStickInjury entity)
		{
			return base.AttachEntity(entity) as EmployeeNeedleStickInjury;
		}

		virtual public void Combine(EmployeeNeedleStickInjuryCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeNeedleStickInjury this[int index]
		{
			get
			{
				return base[index] as EmployeeNeedleStickInjury;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeNeedleStickInjury);
		}
	}

	[Serializable]
	abstract public class esEmployeeNeedleStickInjury : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeNeedleStickInjuryQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeNeedleStickInjury()
		{
		}

		public esEmployeeNeedleStickInjury(DataRow row)
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
			esEmployeeNeedleStickInjuryQuery query = this.GetDynamicQuery();
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
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ExposedArea": this.str.ExposedArea = (string)value; break;
						case "Reason": this.str.Reason = (string)value; break;
						case "IsBlood": this.str.IsBlood = (string)value; break;
						case "IsFluidSperm": this.str.IsFluidSperm = (string)value; break;
						case "IsVaginalSecretions": this.str.IsVaginalSecretions = (string)value; break;
						case "IsCerebrospinal": this.str.IsCerebrospinal = (string)value; break;
						case "IsUrine": this.str.IsUrine = (string)value; break;
						case "IsFaeces": this.str.IsFaeces = (string)value; break;
						case "IsOfficerHiv": this.str.IsOfficerHiv = (string)value; break;
						case "IsOfficerHbv": this.str.IsOfficerHbv = (string)value; break;
						case "IsOfficerHcv": this.str.IsOfficerHcv = (string)value; break;
						case "OfficerImunizationHistory": this.str.OfficerImunizationHistory = (string)value; break;
						case "Chronology": this.str.Chronology = (string)value; break;
						case "Recomendation": this.str.Recomendation = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "Diagnose": this.str.Diagnose = (string)value; break;
						case "IsPatientHiv": this.str.IsPatientHiv = (string)value; break;
						case "IsPatientHbv": this.str.IsPatientHbv = (string)value; break;
						case "IsPatientHcv": this.str.IsPatientHcv = (string)value; break;
						case "PatientImunizationHistory": this.str.PatientImunizationHistory = (string)value; break;
						case "IsSpo": this.str.IsSpo = (string)value; break;
						case "IsUsingApd": this.str.IsUsingApd = (string)value; break;
						case "KnownBy": this.str.KnownBy = (string)value; break;
						case "FollowUpDate": this.str.FollowUpDate = (string)value; break;
						case "FollowUpNotes": this.str.FollowUpNotes = (string)value; break;
						case "FollowUpBy": this.str.FollowUpBy = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
						case "IsBlood":

							if (value == null || value is System.Boolean)
								this.IsBlood = (System.Boolean?)value;
							break;
						case "IsFluidSperm":

							if (value == null || value is System.Boolean)
								this.IsFluidSperm = (System.Boolean?)value;
							break;
						case "IsVaginalSecretions":

							if (value == null || value is System.Boolean)
								this.IsVaginalSecretions = (System.Boolean?)value;
							break;
						case "IsCerebrospinal":

							if (value == null || value is System.Boolean)
								this.IsCerebrospinal = (System.Boolean?)value;
							break;
						case "IsUrine":

							if (value == null || value is System.Boolean)
								this.IsUrine = (System.Boolean?)value;
							break;
						case "IsFaeces":

							if (value == null || value is System.Boolean)
								this.IsFaeces = (System.Boolean?)value;
							break;
						case "IsOfficerHiv":

							if (value == null || value is System.Boolean)
								this.IsOfficerHiv = (System.Boolean?)value;
							break;
						case "IsOfficerHbv":

							if (value == null || value is System.Boolean)
								this.IsOfficerHbv = (System.Boolean?)value;
							break;
						case "IsOfficerHcv":

							if (value == null || value is System.Boolean)
								this.IsOfficerHcv = (System.Boolean?)value;
							break;
						case "IsPatientHiv":

							if (value == null || value is System.Boolean)
								this.IsPatientHiv = (System.Boolean?)value;
							break;
						case "IsPatientHbv":

							if (value == null || value is System.Boolean)
								this.IsPatientHbv = (System.Boolean?)value;
							break;
						case "IsPatientHcv":

							if (value == null || value is System.Boolean)
								this.IsPatientHcv = (System.Boolean?)value;
							break;
						case "IsSpo":

							if (value == null || value is System.Boolean)
								this.IsSpo = (System.Boolean?)value;
							break;
						case "IsUsingApd":

							if (value == null || value is System.Boolean)
								this.IsUsingApd = (System.Boolean?)value;
							break;
						case "FollowUpDate":

							if (value == null || value is System.DateTime)
								this.FollowUpDate = (System.DateTime?)value;
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
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeNeedleStickInjury.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.ExposedArea
		/// </summary>
		virtual public System.String ExposedArea
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.ExposedArea);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.ExposedArea, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Reason);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Reason, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsBlood
		/// </summary>
		virtual public System.Boolean? IsBlood
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsBlood);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsBlood, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsFluidSperm
		/// </summary>
		virtual public System.Boolean? IsFluidSperm
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFluidSperm);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFluidSperm, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsVaginalSecretions
		/// </summary>
		virtual public System.Boolean? IsVaginalSecretions
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVaginalSecretions);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVaginalSecretions, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsCerebrospinal
		/// </summary>
		virtual public System.Boolean? IsCerebrospinal
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsCerebrospinal);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsCerebrospinal, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsUrine
		/// </summary>
		virtual public System.Boolean? IsUrine
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUrine);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUrine, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsFaeces
		/// </summary>
		virtual public System.Boolean? IsFaeces
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFaeces);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFaeces, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsOfficerHiv
		/// </summary>
		virtual public System.Boolean? IsOfficerHiv
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHiv);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHiv, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsOfficerHbv
		/// </summary>
		virtual public System.Boolean? IsOfficerHbv
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHbv);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHbv, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsOfficerHcv
		/// </summary>
		virtual public System.Boolean? IsOfficerHcv
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHcv);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHcv, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.OfficerImunizationHistory
		/// </summary>
		virtual public System.String OfficerImunizationHistory
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.OfficerImunizationHistory);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.OfficerImunizationHistory, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.Chronology
		/// </summary>
		virtual public System.String Chronology
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Chronology);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Chronology, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.Recomendation
		/// </summary>
		virtual public System.String Recomendation
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Recomendation);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Recomendation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.Diagnose
		/// </summary>
		virtual public System.String Diagnose
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Diagnose);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.Diagnose, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsPatientHiv
		/// </summary>
		virtual public System.Boolean? IsPatientHiv
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHiv);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHiv, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsPatientHbv
		/// </summary>
		virtual public System.Boolean? IsPatientHbv
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHbv);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHbv, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsPatientHcv
		/// </summary>
		virtual public System.Boolean? IsPatientHcv
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHcv);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHcv, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.PatientImunizationHistory
		/// </summary>
		virtual public System.String PatientImunizationHistory
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientImunizationHistory);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientImunizationHistory, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsSpo
		/// </summary>
		virtual public System.Boolean? IsSpo
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsSpo);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsSpo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsUsingApd
		/// </summary>
		virtual public System.Boolean? IsUsingApd
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUsingApd);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUsingApd, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.KnownBy
		/// </summary>
		virtual public System.String KnownBy
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.KnownBy);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.KnownBy, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.FollowUpDate
		/// </summary>
		virtual public System.DateTime? FollowUpDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.FollowUpNotes
		/// </summary>
		virtual public System.String FollowUpNotes
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpNotes);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpNotes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.FollowUpBy
		/// </summary>
		virtual public System.String FollowUpBy
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpBy);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpBy, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeNeedleStickInjury.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeNeedleStickInjury entity)
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
			public System.String ExposedArea
			{
				get
				{
					System.String data = entity.ExposedArea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExposedArea = null;
					else entity.ExposedArea = Convert.ToString(value);
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
			public System.String IsBlood
			{
				get
				{
					System.Boolean? data = entity.IsBlood;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBlood = null;
					else entity.IsBlood = Convert.ToBoolean(value);
				}
			}
			public System.String IsFluidSperm
			{
				get
				{
					System.Boolean? data = entity.IsFluidSperm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFluidSperm = null;
					else entity.IsFluidSperm = Convert.ToBoolean(value);
				}
			}
			public System.String IsVaginalSecretions
			{
				get
				{
					System.Boolean? data = entity.IsVaginalSecretions;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVaginalSecretions = null;
					else entity.IsVaginalSecretions = Convert.ToBoolean(value);
				}
			}
			public System.String IsCerebrospinal
			{
				get
				{
					System.Boolean? data = entity.IsCerebrospinal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCerebrospinal = null;
					else entity.IsCerebrospinal = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrine
			{
				get
				{
					System.Boolean? data = entity.IsUrine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrine = null;
					else entity.IsUrine = Convert.ToBoolean(value);
				}
			}
			public System.String IsFaeces
			{
				get
				{
					System.Boolean? data = entity.IsFaeces;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFaeces = null;
					else entity.IsFaeces = Convert.ToBoolean(value);
				}
			}
			public System.String IsOfficerHiv
			{
				get
				{
					System.Boolean? data = entity.IsOfficerHiv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOfficerHiv = null;
					else entity.IsOfficerHiv = Convert.ToBoolean(value);
				}
			}
			public System.String IsOfficerHbv
			{
				get
				{
					System.Boolean? data = entity.IsOfficerHbv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOfficerHbv = null;
					else entity.IsOfficerHbv = Convert.ToBoolean(value);
				}
			}
			public System.String IsOfficerHcv
			{
				get
				{
					System.Boolean? data = entity.IsOfficerHcv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOfficerHcv = null;
					else entity.IsOfficerHcv = Convert.ToBoolean(value);
				}
			}
			public System.String OfficerImunizationHistory
			{
				get
				{
					System.String data = entity.OfficerImunizationHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OfficerImunizationHistory = null;
					else entity.OfficerImunizationHistory = Convert.ToString(value);
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
			public System.String Recomendation
			{
				get
				{
					System.String data = entity.Recomendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Recomendation = null;
					else entity.Recomendation = Convert.ToString(value);
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
			public System.String IsPatientHiv
			{
				get
				{
					System.Boolean? data = entity.IsPatientHiv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPatientHiv = null;
					else entity.IsPatientHiv = Convert.ToBoolean(value);
				}
			}
			public System.String IsPatientHbv
			{
				get
				{
					System.Boolean? data = entity.IsPatientHbv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPatientHbv = null;
					else entity.IsPatientHbv = Convert.ToBoolean(value);
				}
			}
			public System.String IsPatientHcv
			{
				get
				{
					System.Boolean? data = entity.IsPatientHcv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPatientHcv = null;
					else entity.IsPatientHcv = Convert.ToBoolean(value);
				}
			}
			public System.String PatientImunizationHistory
			{
				get
				{
					System.String data = entity.PatientImunizationHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientImunizationHistory = null;
					else entity.PatientImunizationHistory = Convert.ToString(value);
				}
			}
			public System.String IsSpo
			{
				get
				{
					System.Boolean? data = entity.IsSpo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSpo = null;
					else entity.IsSpo = Convert.ToBoolean(value);
				}
			}
			public System.String IsUsingApd
			{
				get
				{
					System.Boolean? data = entity.IsUsingApd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingApd = null;
					else entity.IsUsingApd = Convert.ToBoolean(value);
				}
			}
			public System.String KnownBy
			{
				get
				{
					System.String data = entity.KnownBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KnownBy = null;
					else entity.KnownBy = Convert.ToString(value);
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
			public System.String FollowUpNotes
			{
				get
				{
					System.String data = entity.FollowUpNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FollowUpNotes = null;
					else entity.FollowUpNotes = Convert.ToString(value);
				}
			}
			public System.String FollowUpBy
			{
				get
				{
					System.String data = entity.FollowUpBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FollowUpBy = null;
					else entity.FollowUpBy = Convert.ToString(value);
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
			private esEmployeeNeedleStickInjury entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeNeedleStickInjuryQuery query)
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
				throw new Exception("esEmployeeNeedleStickInjury can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeNeedleStickInjury : esEmployeeNeedleStickInjury
	{
	}

	[Serializable]
	abstract public class esEmployeeNeedleStickInjuryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeNeedleStickInjuryMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem ExposedArea
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.ExposedArea, esSystemType.String);
			}
		}

		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.Reason, esSystemType.String);
			}
		}

		public esQueryItem IsBlood
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsBlood, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFluidSperm
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFluidSperm, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVaginalSecretions
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVaginalSecretions, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCerebrospinal
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsCerebrospinal, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrine
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUrine, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFaeces
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFaeces, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOfficerHiv
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHiv, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOfficerHbv
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHbv, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOfficerHcv
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHcv, esSystemType.Boolean);
			}
		}

		public esQueryItem OfficerImunizationHistory
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.OfficerImunizationHistory, esSystemType.String);
			}
		}

		public esQueryItem Chronology
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.Chronology, esSystemType.String);
			}
		}

		public esQueryItem Recomendation
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.Recomendation, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem Diagnose
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.Diagnose, esSystemType.String);
			}
		}

		public esQueryItem IsPatientHiv
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHiv, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPatientHbv
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHbv, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPatientHcv
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHcv, esSystemType.Boolean);
			}
		}

		public esQueryItem PatientImunizationHistory
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientImunizationHistory, esSystemType.String);
			}
		}

		public esQueryItem IsSpo
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsSpo, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUsingApd
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUsingApd, esSystemType.Boolean);
			}
		}

		public esQueryItem KnownBy
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.KnownBy, esSystemType.String);
			}
		}

		public esQueryItem FollowUpDate
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpDate, esSystemType.DateTime);
			}
		}

		public esQueryItem FollowUpNotes
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpNotes, esSystemType.String);
			}
		}

		public esQueryItem FollowUpBy
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpBy, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeNeedleStickInjuryCollection")]
	public partial class EmployeeNeedleStickInjuryCollection : esEmployeeNeedleStickInjuryCollection, IEnumerable<EmployeeNeedleStickInjury>
	{
		public EmployeeNeedleStickInjuryCollection()
		{

		}

		public static implicit operator List<EmployeeNeedleStickInjury>(EmployeeNeedleStickInjuryCollection coll)
		{
			List<EmployeeNeedleStickInjury> list = new List<EmployeeNeedleStickInjury>();

			foreach (EmployeeNeedleStickInjury emp in coll)
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
				return EmployeeNeedleStickInjuryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeNeedleStickInjuryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeNeedleStickInjury(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeNeedleStickInjury();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeNeedleStickInjuryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeNeedleStickInjuryQuery();
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
		public bool Load(EmployeeNeedleStickInjuryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeNeedleStickInjury AddNew()
		{
			EmployeeNeedleStickInjury entity = base.AddNewEntity() as EmployeeNeedleStickInjury;

			return entity;
		}
		public EmployeeNeedleStickInjury FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as EmployeeNeedleStickInjury;
		}

		#region IEnumerable< EmployeeNeedleStickInjury> Members

		IEnumerator<EmployeeNeedleStickInjury> IEnumerable<EmployeeNeedleStickInjury>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeNeedleStickInjury;
			}
		}

		#endregion

		private EmployeeNeedleStickInjuryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeNeedleStickInjury' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeNeedleStickInjury ({TransactionNo})")]
	[Serializable]
	public partial class EmployeeNeedleStickInjury : esEmployeeNeedleStickInjury
	{
		public EmployeeNeedleStickInjury()
		{
		}

		public EmployeeNeedleStickInjury(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeNeedleStickInjuryMetadata.Meta();
			}
		}

		override protected esEmployeeNeedleStickInjuryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeNeedleStickInjuryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeNeedleStickInjuryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeNeedleStickInjuryQuery();
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
		public bool Load(EmployeeNeedleStickInjuryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeNeedleStickInjuryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeNeedleStickInjuryQuery : esEmployeeNeedleStickInjuryQuery
	{
		public EmployeeNeedleStickInjuryQuery()
		{

		}

		public EmployeeNeedleStickInjuryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeNeedleStickInjuryQuery";
		}
	}

	[Serializable]
	public partial class EmployeeNeedleStickInjuryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeNeedleStickInjuryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.ReferenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.ExposedArea, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.ExposedArea;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.Reason, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsBlood, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsBlood;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFluidSperm, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsFluidSperm;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVaginalSecretions, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsVaginalSecretions;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsCerebrospinal, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsCerebrospinal;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUrine, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsUrine;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsFaeces, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsFaeces;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHiv, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsOfficerHiv;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHbv, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsOfficerHbv;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsOfficerHcv, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsOfficerHcv;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.OfficerImunizationHistory, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.OfficerImunizationHistory;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.Chronology, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.Chronology;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.Recomendation, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.Recomendation;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.RegistrationNo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.Diagnose, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.Diagnose;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHiv, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsPatientHiv;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHbv, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsPatientHbv;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsPatientHcv, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsPatientHcv;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.PatientImunizationHistory, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.PatientImunizationHistory;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsSpo, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsSpo;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsUsingApd, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsUsingApd;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.KnownBy, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.KnownBy;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpDate, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.FollowUpDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpNotes, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.FollowUpNotes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.FollowUpBy, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.FollowUpBy;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsApproved, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.ApprovedByUserID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVoid, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidDateTime, 34, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.VoidByUserID, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.IsVerified, 36, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedDateTime, 37, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.VerifiedByUserID, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedDateTime, 39, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.CreatedByUserID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateDateTime, 41, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeNeedleStickInjuryMetadata.ColumnNames.LastUpdateByUserID, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeNeedleStickInjuryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeNeedleStickInjuryMetadata Meta()
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
			public const string ReferenceNo = "ReferenceNo";
			public const string ExposedArea = "ExposedArea";
			public const string Reason = "Reason";
			public const string IsBlood = "IsBlood";
			public const string IsFluidSperm = "IsFluidSperm";
			public const string IsVaginalSecretions = "IsVaginalSecretions";
			public const string IsCerebrospinal = "IsCerebrospinal";
			public const string IsUrine = "IsUrine";
			public const string IsFaeces = "IsFaeces";
			public const string IsOfficerHiv = "IsOfficerHiv";
			public const string IsOfficerHbv = "IsOfficerHbv";
			public const string IsOfficerHcv = "IsOfficerHcv";
			public const string OfficerImunizationHistory = "OfficerImunizationHistory";
			public const string Chronology = "Chronology";
			public const string Recomendation = "Recomendation";
			public const string PatientID = "PatientID";
			public const string RegistrationNo = "RegistrationNo";
			public const string Diagnose = "Diagnose";
			public const string IsPatientHiv = "IsPatientHiv";
			public const string IsPatientHbv = "IsPatientHbv";
			public const string IsPatientHcv = "IsPatientHcv";
			public const string PatientImunizationHistory = "PatientImunizationHistory";
			public const string IsSpo = "IsSpo";
			public const string IsUsingApd = "IsUsingApd";
			public const string KnownBy = "KnownBy";
			public const string FollowUpDate = "FollowUpDate";
			public const string FollowUpNotes = "FollowUpNotes";
			public const string FollowUpBy = "FollowUpBy";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string ReferenceNo = "ReferenceNo";
			public const string ExposedArea = "ExposedArea";
			public const string Reason = "Reason";
			public const string IsBlood = "IsBlood";
			public const string IsFluidSperm = "IsFluidSperm";
			public const string IsVaginalSecretions = "IsVaginalSecretions";
			public const string IsCerebrospinal = "IsCerebrospinal";
			public const string IsUrine = "IsUrine";
			public const string IsFaeces = "IsFaeces";
			public const string IsOfficerHiv = "IsOfficerHiv";
			public const string IsOfficerHbv = "IsOfficerHbv";
			public const string IsOfficerHcv = "IsOfficerHcv";
			public const string OfficerImunizationHistory = "OfficerImunizationHistory";
			public const string Chronology = "Chronology";
			public const string Recomendation = "Recomendation";
			public const string PatientID = "PatientID";
			public const string RegistrationNo = "RegistrationNo";
			public const string Diagnose = "Diagnose";
			public const string IsPatientHiv = "IsPatientHiv";
			public const string IsPatientHbv = "IsPatientHbv";
			public const string IsPatientHcv = "IsPatientHcv";
			public const string PatientImunizationHistory = "PatientImunizationHistory";
			public const string IsSpo = "IsSpo";
			public const string IsUsingApd = "IsUsingApd";
			public const string KnownBy = "KnownBy";
			public const string FollowUpDate = "FollowUpDate";
			public const string FollowUpNotes = "FollowUpNotes";
			public const string FollowUpBy = "FollowUpBy";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(EmployeeNeedleStickInjuryMetadata))
			{
				if (EmployeeNeedleStickInjuryMetadata.mapDelegates == null)
				{
					EmployeeNeedleStickInjuryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeNeedleStickInjuryMetadata.meta == null)
				{
					EmployeeNeedleStickInjuryMetadata.meta = new EmployeeNeedleStickInjuryMetadata();
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
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExposedArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsBlood", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFluidSperm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVaginalSecretions", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCerebrospinal", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrine", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFaeces", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOfficerHiv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOfficerHbv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOfficerHcv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OfficerImunizationHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Chronology", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Recomendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Diagnose", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPatientHiv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPatientHbv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPatientHcv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PatientImunizationHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSpo", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUsingApd", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("KnownBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FollowUpDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FollowUpNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FollowUpBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeNeedleStickInjury";
				meta.Destination = "EmployeeNeedleStickInjury";
				meta.spInsert = "proc_EmployeeNeedleStickInjuryInsert";
				meta.spUpdate = "proc_EmployeeNeedleStickInjuryUpdate";
				meta.spDelete = "proc_EmployeeNeedleStickInjuryDelete";
				meta.spLoadAll = "proc_EmployeeNeedleStickInjuryLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeNeedleStickInjuryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeNeedleStickInjuryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
