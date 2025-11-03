/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/18/2023 7:39:49 PM
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
	abstract public class esPatientEducationCollection : esEntityCollectionWAuditLog
	{
		public esPatientEducationCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "PatientEducationCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esPatientEducationQuery query)
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
			this.InitQuery(query as esPatientEducationQuery);
		}
		#endregion
			
		virtual public PatientEducation DetachEntity(PatientEducation entity)
		{
			return base.DetachEntity(entity) as PatientEducation;
		}
		
		virtual public PatientEducation AttachEntity(PatientEducation entity)
		{
			return base.AttachEntity(entity) as PatientEducation;
		}
		
		virtual public void Combine(PatientEducationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientEducation this[int index]
		{
			get
			{
				return base[index] as PatientEducation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientEducation);
		}
	}

	[Serializable]
	abstract public class esPatientEducation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientEducationQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esPatientEducation()
		{
		}
	
		public esPatientEducation(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 seqNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 seqNo)
		{
			esPatientEducationQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("SeqNo",seqNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "EducationDateTime": this.str.EducationDateTime = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "SRUserType": this.str.SRUserType = (string)value; break;
						case "SRPatientEducationProblem": this.str.SRPatientEducationProblem = (string)value; break;
						case "SRPatientEducationMethod": this.str.SRPatientEducationMethod = (string)value; break;
						case "MethodOther": this.str.MethodOther = (string)value; break;
						case "SRPatientEducationRecipient": this.str.SRPatientEducationRecipient = (string)value; break;
						case "RecipientName": this.str.RecipientName = (string)value; break;
						case "SRPatientEducationEvaluation": this.str.SRPatientEducationEvaluation = (string)value; break;
						case "Duration": this.str.Duration = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "EducationByUserID": this.str.EducationByUserID = (string)value; break;
						case "EducationType": this.str.EducationType = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ReferenceType": this.str.ReferenceType = (string)value; break;
						case "PatientEducationEvaluationOth": this.str.PatientEducationEvaluationOth = (string)value; break;
						case "SRPatientEducationGoal": this.str.SRPatientEducationGoal = (string)value; break;
						case "PatientEducationGoalOth": this.str.PatientEducationGoalOth = (string)value; break;
						case "Verificator": this.str.Verificator = (string)value; break;
						case "VerifyByUserID": this.str.VerifyByUserID = (string)value; break;
						case "VerifyDateTime": this.str.VerifyDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SeqNo":
						
							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
							break;
						case "EducationDateTime":
						
							if (value == null || value is System.DateTime)
								this.EducationDateTime = (System.DateTime?)value;
							break;
						case "Duration":
						
							if (value == null || value is System.Int32)
								this.Duration = (System.Int32?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "FmSign":
						
							if (value == null || value is System.Byte[])
								this.FmSign = (System.Byte[])value;
							break;
						case "PsSign":
						
							if (value == null || value is System.Byte[])
								this.PsSign = (System.Byte[])value;
							break;
						case "VerifyDateTime":
						
							if (value == null || value is System.DateTime)
								this.VerifyDateTime = (System.DateTime?)value;
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
		/// Maps to PatientEducation.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(PatientEducationMetadata.ColumnNames.SeqNo);
			}
			
			set
			{
				base.SetSystemInt32(PatientEducationMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.EducationDateTime
		/// </summary>
		virtual public System.DateTime? EducationDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientEducationMetadata.ColumnNames.EducationDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientEducationMetadata.ColumnNames.EducationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.SRUserType
		/// </summary>
		virtual public System.String SRUserType
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.SRUserType);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.SRUserType, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.SRPatientEducationProblem
		/// </summary>
		virtual public System.String SRPatientEducationProblem
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationProblem);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationProblem, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.SRPatientEducationMethod
		/// </summary>
		virtual public System.String SRPatientEducationMethod
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationMethod);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationMethod, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.MethodOther
		/// </summary>
		virtual public System.String MethodOther
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.MethodOther);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.MethodOther, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.SRPatientEducationRecipient
		/// </summary>
		virtual public System.String SRPatientEducationRecipient
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationRecipient);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationRecipient, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.RecipientName
		/// </summary>
		virtual public System.String RecipientName
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.RecipientName);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.RecipientName, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.SRPatientEducationEvaluation
		/// </summary>
		virtual public System.String SRPatientEducationEvaluation
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationEvaluation);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationEvaluation, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.Duration
		/// </summary>
		virtual public System.Int32? Duration
		{
			get
			{
				return base.GetSystemInt32(PatientEducationMetadata.ColumnNames.Duration);
			}
			
			set
			{
				base.SetSystemInt32(PatientEducationMetadata.ColumnNames.Duration, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientEducationMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientEducationMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientEducationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientEducationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.EducationByUserID
		/// </summary>
		virtual public System.String EducationByUserID
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.EducationByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.EducationByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.EducationType
		/// </summary>
		virtual public System.String EducationType
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.EducationType);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.EducationType, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.ReferenceType
		/// </summary>
		virtual public System.String ReferenceType
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.ReferenceType);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.ReferenceType, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.FmSign
		/// </summary>
		virtual public System.Byte[] FmSign
		{
			get
			{
				return base.GetSystemByteArray(PatientEducationMetadata.ColumnNames.FmSign);
			}
			
			set
			{
				base.SetSystemByteArray(PatientEducationMetadata.ColumnNames.FmSign, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.PsSign
		/// </summary>
		virtual public System.Byte[] PsSign
		{
			get
			{
				return base.GetSystemByteArray(PatientEducationMetadata.ColumnNames.PsSign);
			}
			
			set
			{
				base.SetSystemByteArray(PatientEducationMetadata.ColumnNames.PsSign, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.PatientEducationEvaluationOth
		/// </summary>
		virtual public System.String PatientEducationEvaluationOth
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.PatientEducationEvaluationOth);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.PatientEducationEvaluationOth, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.SRPatientEducationGoal
		/// </summary>
		virtual public System.String SRPatientEducationGoal
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationGoal);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.SRPatientEducationGoal, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.PatientEducationGoalOth
		/// </summary>
		virtual public System.String PatientEducationGoalOth
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.PatientEducationGoalOth);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.PatientEducationGoalOth, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.Verificator
		/// </summary>
		virtual public System.String Verificator
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.Verificator);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.Verificator, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.VerifyByUserID
		/// </summary>
		virtual public System.String VerifyByUserID
		{
			get
			{
				return base.GetSystemString(PatientEducationMetadata.ColumnNames.VerifyByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientEducationMetadata.ColumnNames.VerifyByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientEducation.VerifyDateTime
		/// </summary>
		virtual public System.DateTime? VerifyDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientEducationMetadata.ColumnNames.VerifyDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientEducationMetadata.ColumnNames.VerifyDateTime, value);
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
			public esStrings(esPatientEducation entity)
			{
				this.entity = entity;
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
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
				}
			}
			public System.String EducationDateTime
			{
				get
				{
					System.DateTime? data = entity.EducationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EducationDateTime = null;
					else entity.EducationDateTime = Convert.ToDateTime(value);
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
			public System.String SRUserType
			{
				get
				{
					System.String data = entity.SRUserType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRUserType = null;
					else entity.SRUserType = Convert.ToString(value);
				}
			}
			public System.String SRPatientEducationProblem
			{
				get
				{
					System.String data = entity.SRPatientEducationProblem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientEducationProblem = null;
					else entity.SRPatientEducationProblem = Convert.ToString(value);
				}
			}
			public System.String SRPatientEducationMethod
			{
				get
				{
					System.String data = entity.SRPatientEducationMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientEducationMethod = null;
					else entity.SRPatientEducationMethod = Convert.ToString(value);
				}
			}
			public System.String MethodOther
			{
				get
				{
					System.String data = entity.MethodOther;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MethodOther = null;
					else entity.MethodOther = Convert.ToString(value);
				}
			}
			public System.String SRPatientEducationRecipient
			{
				get
				{
					System.String data = entity.SRPatientEducationRecipient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientEducationRecipient = null;
					else entity.SRPatientEducationRecipient = Convert.ToString(value);
				}
			}
			public System.String RecipientName
			{
				get
				{
					System.String data = entity.RecipientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecipientName = null;
					else entity.RecipientName = Convert.ToString(value);
				}
			}
			public System.String SRPatientEducationEvaluation
			{
				get
				{
					System.String data = entity.SRPatientEducationEvaluation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientEducationEvaluation = null;
					else entity.SRPatientEducationEvaluation = Convert.ToString(value);
				}
			}
			public System.String Duration
			{
				get
				{
					System.Int32? data = entity.Duration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Duration = null;
					else entity.Duration = Convert.ToInt32(value);
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
			public System.String EducationByUserID
			{
				get
				{
					System.String data = entity.EducationByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EducationByUserID = null;
					else entity.EducationByUserID = Convert.ToString(value);
				}
			}
			public System.String EducationType
			{
				get
				{
					System.String data = entity.EducationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EducationType = null;
					else entity.EducationType = Convert.ToString(value);
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
			public System.String ReferenceType
			{
				get
				{
					System.String data = entity.ReferenceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceType = null;
					else entity.ReferenceType = Convert.ToString(value);
				}
			}
			public System.String PatientEducationEvaluationOth
			{
				get
				{
					System.String data = entity.PatientEducationEvaluationOth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientEducationEvaluationOth = null;
					else entity.PatientEducationEvaluationOth = Convert.ToString(value);
				}
			}
			public System.String SRPatientEducationGoal
			{
				get
				{
					System.String data = entity.SRPatientEducationGoal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPatientEducationGoal = null;
					else entity.SRPatientEducationGoal = Convert.ToString(value);
				}
			}
			public System.String PatientEducationGoalOth
			{
				get
				{
					System.String data = entity.PatientEducationGoalOth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientEducationGoalOth = null;
					else entity.PatientEducationGoalOth = Convert.ToString(value);
				}
			}
			public System.String Verificator
			{
				get
				{
					System.String data = entity.Verificator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Verificator = null;
					else entity.Verificator = Convert.ToString(value);
				}
			}
			public System.String VerifyByUserID
			{
				get
				{
					System.String data = entity.VerifyByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifyByUserID = null;
					else entity.VerifyByUserID = Convert.ToString(value);
				}
			}
			public System.String VerifyDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifyDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifyDateTime = null;
					else entity.VerifyDateTime = Convert.ToDateTime(value);
				}
			}
			private esPatientEducation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientEducationQuery query)
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
				throw new Exception("esPatientEducation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientEducation : esPatientEducation
	{	
	}

	[Serializable]
	abstract public class esPatientEducationQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return PatientEducationMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem EducationDateTime
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.EducationDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRUserType
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.SRUserType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPatientEducationProblem
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.SRPatientEducationProblem, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPatientEducationMethod
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.SRPatientEducationMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem MethodOther
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.MethodOther, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPatientEducationRecipient
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.SRPatientEducationRecipient, esSystemType.String);
			}
		} 
			
		public esQueryItem RecipientName
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.RecipientName, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPatientEducationEvaluation
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.SRPatientEducationEvaluation, esSystemType.String);
			}
		} 
			
		public esQueryItem Duration
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.Duration, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem EducationByUserID
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.EducationByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem EducationType
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.EducationType, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceType
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.ReferenceType, esSystemType.String);
			}
		} 
			
		public esQueryItem FmSign
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.FmSign, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem PsSign
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.PsSign, esSystemType.ByteArray);
			}
		} 
			
		public esQueryItem PatientEducationEvaluationOth
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.PatientEducationEvaluationOth, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPatientEducationGoal
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.SRPatientEducationGoal, esSystemType.String);
			}
		} 
			
		public esQueryItem PatientEducationGoalOth
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.PatientEducationGoalOth, esSystemType.String);
			}
		} 
			
		public esQueryItem Verificator
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.Verificator, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifyByUserID
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.VerifyByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerifyDateTime
		{
			get
			{
				return new esQueryItem(this, PatientEducationMetadata.ColumnNames.VerifyDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientEducationCollection")]
	public partial class PatientEducationCollection : esPatientEducationCollection, IEnumerable< PatientEducation>
	{
		public PatientEducationCollection()
		{

		}	
		
		public static implicit operator List< PatientEducation>(PatientEducationCollection coll)
		{
			List< PatientEducation> list = new List< PatientEducation>();
			
			foreach (PatientEducation emp in coll)
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
				return  PatientEducationMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientEducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientEducation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientEducation();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public PatientEducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientEducationQuery();
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
		public bool Load(PatientEducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientEducation AddNew()
		{
			PatientEducation entity = base.AddNewEntity() as PatientEducation;
			
			return entity;		
		}
		public PatientEducation FindByPrimaryKey(String registrationNo, Int32 seqNo)
		{
			return base.FindByPrimaryKey(registrationNo, seqNo) as PatientEducation;
		}

		#region IEnumerable< PatientEducation> Members

		IEnumerator< PatientEducation> IEnumerable< PatientEducation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientEducation;
			}
		}

		#endregion
		
		private PatientEducationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientEducation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientEducation ({RegistrationNo, SeqNo})")]
	[Serializable]
	public partial class PatientEducation : esPatientEducation
	{
		public PatientEducation()
		{
		}	
	
		public PatientEducation(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientEducationMetadata.Meta();
			}
		}	
	
		override protected esPatientEducationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientEducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public PatientEducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientEducationQuery();
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
		public bool Load(PatientEducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private PatientEducationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientEducationQuery : esPatientEducationQuery
	{
		public PatientEducationQuery()
		{

		}		
		
		public PatientEducationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "PatientEducationQuery";
        }
	}

	[Serializable]
	public partial class PatientEducationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientEducationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientEducationMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.EducationDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientEducationMetadata.PropertyNames.EducationDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.ParamedicID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.SRUserType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.SRUserType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.SRPatientEducationProblem, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.SRPatientEducationProblem;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.SRPatientEducationMethod, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.SRPatientEducationMethod;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.MethodOther, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.MethodOther;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.SRPatientEducationRecipient, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.SRPatientEducationRecipient;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.RecipientName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.RecipientName;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.SRPatientEducationEvaluation, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.SRPatientEducationEvaluation;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.Duration, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientEducationMetadata.PropertyNames.Duration;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.CreatedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.CreatedDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientEducationMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientEducationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.EducationByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.EducationByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.EducationType, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.EducationType;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.ReferenceNo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.ReferenceType, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.ReferenceType;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.FmSign, 20, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PatientEducationMetadata.PropertyNames.FmSign;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.PsSign, 21, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PatientEducationMetadata.PropertyNames.PsSign;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.PatientEducationEvaluationOth, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.PatientEducationEvaluationOth;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.SRPatientEducationGoal, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.SRPatientEducationGoal;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.PatientEducationGoalOth, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.PatientEducationGoalOth;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.Verificator, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.Verificator;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.VerifyByUserID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientEducationMetadata.PropertyNames.VerifyByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(PatientEducationMetadata.ColumnNames.VerifyDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientEducationMetadata.PropertyNames.VerifyDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public PatientEducationMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string EducationDateTime = "EducationDateTime";
			public const string ParamedicID = "ParamedicID";
			public const string SRUserType = "SRUserType";
			public const string SRPatientEducationProblem = "SRPatientEducationProblem";
			public const string SRPatientEducationMethod = "SRPatientEducationMethod";
			public const string MethodOther = "MethodOther";
			public const string SRPatientEducationRecipient = "SRPatientEducationRecipient";
			public const string RecipientName = "RecipientName";
			public const string SRPatientEducationEvaluation = "SRPatientEducationEvaluation";
			public const string Duration = "Duration";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string EducationByUserID = "EducationByUserID";
			public const string EducationType = "EducationType";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceType = "ReferenceType";
			public const string FmSign = "FmSign";
			public const string PsSign = "PsSign";
			public const string PatientEducationEvaluationOth = "PatientEducationEvaluationOth";
			public const string SRPatientEducationGoal = "SRPatientEducationGoal";
			public const string PatientEducationGoalOth = "PatientEducationGoalOth";
			public const string Verificator = "Verificator";
			public const string VerifyByUserID = "VerifyByUserID";
			public const string VerifyDateTime = "VerifyDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string EducationDateTime = "EducationDateTime";
			public const string ParamedicID = "ParamedicID";
			public const string SRUserType = "SRUserType";
			public const string SRPatientEducationProblem = "SRPatientEducationProblem";
			public const string SRPatientEducationMethod = "SRPatientEducationMethod";
			public const string MethodOther = "MethodOther";
			public const string SRPatientEducationRecipient = "SRPatientEducationRecipient";
			public const string RecipientName = "RecipientName";
			public const string SRPatientEducationEvaluation = "SRPatientEducationEvaluation";
			public const string Duration = "Duration";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string EducationByUserID = "EducationByUserID";
			public const string EducationType = "EducationType";
			public const string ReferenceNo = "ReferenceNo";
			public const string ReferenceType = "ReferenceType";
			public const string FmSign = "FmSign";
			public const string PsSign = "PsSign";
			public const string PatientEducationEvaluationOth = "PatientEducationEvaluationOth";
			public const string SRPatientEducationGoal = "SRPatientEducationGoal";
			public const string PatientEducationGoalOth = "PatientEducationGoalOth";
			public const string Verificator = "Verificator";
			public const string VerifyByUserID = "VerifyByUserID";
			public const string VerifyDateTime = "VerifyDateTime";
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
			lock (typeof(PatientEducationMetadata))
			{
				if(PatientEducationMetadata.mapDelegates == null)
				{
					PatientEducationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientEducationMetadata.meta == null)
				{
					PatientEducationMetadata.meta = new PatientEducationMetadata();
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
				
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EducationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRUserType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientEducationProblem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientEducationMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MethodOther", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientEducationRecipient", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecipientName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientEducationEvaluation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Duration", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EducationByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EducationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FmSign", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("PsSign", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("PatientEducationEvaluationOth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPatientEducationGoal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientEducationGoalOth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Verificator", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifyByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerifyDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "PatientEducation";
				meta.Destination = "PatientEducation";
				meta.spInsert = "proc_PatientEducationInsert";				
				meta.spUpdate = "proc_PatientEducationUpdate";		
				meta.spDelete = "proc_PatientEducationDelete";
				meta.spLoadAll = "proc_PatientEducationLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientEducationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientEducationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
