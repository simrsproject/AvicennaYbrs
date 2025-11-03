/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/18/2021 10:42:24 PM
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
	abstract public class esQuestionFormCollection : esEntityCollectionWAuditLog
	{
		public esQuestionFormCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "QuestionFormCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esQuestionFormQuery query)
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
			this.InitQuery(query as esQuestionFormQuery);
		}
		#endregion
			
		virtual public QuestionForm DetachEntity(QuestionForm entity)
		{
			return base.DetachEntity(entity) as QuestionForm;
		}
		
		virtual public QuestionForm AttachEntity(QuestionForm entity)
		{
			return base.AttachEntity(entity) as QuestionForm;
		}
		
		virtual public void Combine(QuestionFormCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QuestionForm this[int index]
		{
			get
			{
				return base[index] as QuestionForm;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QuestionForm);
		}
	}

	[Serializable]
	abstract public class esQuestionForm : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQuestionFormQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esQuestionForm()
		{
		}
	
		public esQuestionForm(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String questionFormID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionFormID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String questionFormID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionFormID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String questionFormID)
		{
			esQuestionFormQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionFormID==questionFormID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String questionFormID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionFormID",questionFormID);
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
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "QuestionFormName": this.str.QuestionFormName = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "IsMCUForm": this.str.IsMCUForm = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ReportProgramID": this.str.ReportProgramID = (string)value; break;
						case "IsAskepForm": this.str.IsAskepForm = (string)value; break;
						case "IsVSignForm": this.str.IsVSignForm = (string)value; break;
						case "IsSingleEntry": this.str.IsSingleEntry = (string)value; break;
						case "RmNO": this.str.RmNO = (string)value; break;
						case "IsInitialAssessment": this.str.IsInitialAssessment = (string)value; break;
						case "IsContinuedAssessment": this.str.IsContinuedAssessment = (string)value; break;
						case "IsGeneralForm": this.str.IsGeneralForm = (string)value; break;
						case "IsNutritionCareForm": this.str.IsNutritionCareForm = (string)value; break;
						case "IsSoapForm": this.str.IsSoapForm = (string)value; break;
						case "SRQuestionFormType": this.str.SRQuestionFormType = (string)value; break;
						case "SRNsType": this.str.SRNsType = (string)value; break;
						case "RestrictionUserType": this.str.RestrictionUserType = (string)value; break;
						case "IsSharingEdit": this.str.IsSharingEdit = (string)value; break;
						case "IsUsingApproval": this.str.IsUsingApproval = (string)value; break;
						case "SRAutoNumber": this.str.SRAutoNumber = (string)value; break;
						case "IsModeMapping": this.str.IsModeMapping = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "IsMCUForm":
						
							if (value == null || value is System.Boolean)
								this.IsMCUForm = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsAskepForm":
						
							if (value == null || value is System.Boolean)
								this.IsAskepForm = (System.Boolean?)value;
							break;
						case "IsVSignForm":
						
							if (value == null || value is System.Boolean)
								this.IsVSignForm = (System.Boolean?)value;
							break;
						case "IsSingleEntry":
						
							if (value == null || value is System.Boolean)
								this.IsSingleEntry = (System.Boolean?)value;
							break;
						case "IsInitialAssessment":
						
							if (value == null || value is System.Boolean)
								this.IsInitialAssessment = (System.Boolean?)value;
							break;
						case "IsContinuedAssessment":
						
							if (value == null || value is System.Boolean)
								this.IsContinuedAssessment = (System.Boolean?)value;
							break;
						case "IsGeneralForm":
						
							if (value == null || value is System.Boolean)
								this.IsGeneralForm = (System.Boolean?)value;
							break;
						case "IsNutritionCareForm":
						
							if (value == null || value is System.Boolean)
								this.IsNutritionCareForm = (System.Boolean?)value;
							break;
						case "IsSoapForm":
						
							if (value == null || value is System.Boolean)
								this.IsSoapForm = (System.Boolean?)value;
							break;
						case "IsSharingEdit":
						
							if (value == null || value is System.Boolean)
								this.IsSharingEdit = (System.Boolean?)value;
							break;
						case "IsUsingApproval":
						
							if (value == null || value is System.Boolean)
								this.IsUsingApproval = (System.Boolean?)value;
							break;
						case "IsModeMapping":
						
							if (value == null || value is System.Boolean)
								this.IsModeMapping = (System.Boolean?)value;
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
		/// Maps to QuestionForm.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.QuestionFormName
		/// </summary>
		virtual public System.String QuestionFormName
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.QuestionFormName);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.QuestionFormName, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsMCUForm
		/// </summary>
		virtual public System.Boolean? IsMCUForm
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsMCUForm);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsMCUForm, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QuestionFormMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QuestionFormMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.ReportProgramID
		/// </summary>
		virtual public System.String ReportProgramID
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.ReportProgramID);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.ReportProgramID, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsAskepForm
		/// </summary>
		virtual public System.Boolean? IsAskepForm
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsAskepForm);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsAskepForm, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsVSignForm
		/// </summary>
		virtual public System.Boolean? IsVSignForm
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsVSignForm);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsVSignForm, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsSingleEntry
		/// </summary>
		virtual public System.Boolean? IsSingleEntry
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsSingleEntry);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsSingleEntry, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.RmNO
		/// </summary>
		virtual public System.String RmNO
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.RmNO);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.RmNO, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsInitialAssessment
		/// </summary>
		virtual public System.Boolean? IsInitialAssessment
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsInitialAssessment);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsInitialAssessment, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsContinuedAssessment
		/// </summary>
		virtual public System.Boolean? IsContinuedAssessment
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsContinuedAssessment);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsContinuedAssessment, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsGeneralForm
		/// </summary>
		virtual public System.Boolean? IsGeneralForm
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsGeneralForm);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsGeneralForm, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsNutritionCareForm
		/// </summary>
		virtual public System.Boolean? IsNutritionCareForm
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsNutritionCareForm);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsNutritionCareForm, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsSoapForm
		/// </summary>
		virtual public System.Boolean? IsSoapForm
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsSoapForm);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsSoapForm, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.SRQuestionFormType
		/// </summary>
		virtual public System.String SRQuestionFormType
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.SRQuestionFormType);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.SRQuestionFormType, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.SRNsType
		/// </summary>
		virtual public System.String SRNsType
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.SRNsType);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.SRNsType, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.RestrictionUserType
		/// </summary>
		virtual public System.String RestrictionUserType
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.RestrictionUserType);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.RestrictionUserType, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsSharingEdit
		/// </summary>
		virtual public System.Boolean? IsSharingEdit
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsSharingEdit);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsSharingEdit, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsUsingApproval
		/// </summary>
		virtual public System.Boolean? IsUsingApproval
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsUsingApproval);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsUsingApproval, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.SRAutoNumber
		/// </summary>
		virtual public System.String SRAutoNumber
		{
			get
			{
				return base.GetSystemString(QuestionFormMetadata.ColumnNames.SRAutoNumber);
			}
			
			set
			{
				base.SetSystemString(QuestionFormMetadata.ColumnNames.SRAutoNumber, value);
			}
		}
		/// <summary>
		/// Maps to QuestionForm.IsModeMapping
		/// </summary>
		virtual public System.Boolean? IsModeMapping
		{
			get
			{
				return base.GetSystemBoolean(QuestionFormMetadata.ColumnNames.IsModeMapping);
			}
			
			set
			{
				base.SetSystemBoolean(QuestionFormMetadata.ColumnNames.IsModeMapping, value);
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
			public esStrings(esQuestionForm entity)
			{
				this.entity = entity;
			}
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
				}
			}
			public System.String QuestionFormName
			{
				get
				{
					System.String data = entity.QuestionFormName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormName = null;
					else entity.QuestionFormName = Convert.ToString(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
				}
			}
			public System.String IsMCUForm
			{
				get
				{
					System.Boolean? data = entity.IsMCUForm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMCUForm = null;
					else entity.IsMCUForm = Convert.ToBoolean(value);
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
			public System.String ReportProgramID
			{
				get
				{
					System.String data = entity.ReportProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReportProgramID = null;
					else entity.ReportProgramID = Convert.ToString(value);
				}
			}
			public System.String IsAskepForm
			{
				get
				{
					System.Boolean? data = entity.IsAskepForm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAskepForm = null;
					else entity.IsAskepForm = Convert.ToBoolean(value);
				}
			}
			public System.String IsVSignForm
			{
				get
				{
					System.Boolean? data = entity.IsVSignForm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVSignForm = null;
					else entity.IsVSignForm = Convert.ToBoolean(value);
				}
			}
			public System.String IsSingleEntry
			{
				get
				{
					System.Boolean? data = entity.IsSingleEntry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSingleEntry = null;
					else entity.IsSingleEntry = Convert.ToBoolean(value);
				}
			}
			public System.String RmNO
			{
				get
				{
					System.String data = entity.RmNO;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmNO = null;
					else entity.RmNO = Convert.ToString(value);
				}
			}
			public System.String IsInitialAssessment
			{
				get
				{
					System.Boolean? data = entity.IsInitialAssessment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInitialAssessment = null;
					else entity.IsInitialAssessment = Convert.ToBoolean(value);
				}
			}
			public System.String IsContinuedAssessment
			{
				get
				{
					System.Boolean? data = entity.IsContinuedAssessment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsContinuedAssessment = null;
					else entity.IsContinuedAssessment = Convert.ToBoolean(value);
				}
			}
			public System.String IsGeneralForm
			{
				get
				{
					System.Boolean? data = entity.IsGeneralForm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGeneralForm = null;
					else entity.IsGeneralForm = Convert.ToBoolean(value);
				}
			}
			public System.String IsNutritionCareForm
			{
				get
				{
					System.Boolean? data = entity.IsNutritionCareForm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNutritionCareForm = null;
					else entity.IsNutritionCareForm = Convert.ToBoolean(value);
				}
			}
			public System.String IsSoapForm
			{
				get
				{
					System.Boolean? data = entity.IsSoapForm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSoapForm = null;
					else entity.IsSoapForm = Convert.ToBoolean(value);
				}
			}
			public System.String SRQuestionFormType
			{
				get
				{
					System.String data = entity.SRQuestionFormType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQuestionFormType = null;
					else entity.SRQuestionFormType = Convert.ToString(value);
				}
			}
			public System.String SRNsType
			{
				get
				{
					System.String data = entity.SRNsType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRNsType = null;
					else entity.SRNsType = Convert.ToString(value);
				}
			}
			public System.String RestrictionUserType
			{
				get
				{
					System.String data = entity.RestrictionUserType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RestrictionUserType = null;
					else entity.RestrictionUserType = Convert.ToString(value);
				}
			}
			public System.String IsSharingEdit
			{
				get
				{
					System.Boolean? data = entity.IsSharingEdit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSharingEdit = null;
					else entity.IsSharingEdit = Convert.ToBoolean(value);
				}
			}
			public System.String IsUsingApproval
			{
				get
				{
					System.Boolean? data = entity.IsUsingApproval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingApproval = null;
					else entity.IsUsingApproval = Convert.ToBoolean(value);
				}
			}
			public System.String SRAutoNumber
			{
				get
				{
					System.String data = entity.SRAutoNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAutoNumber = null;
					else entity.SRAutoNumber = Convert.ToString(value);
				}
			}
			public System.String IsModeMapping
			{
				get
				{
					System.Boolean? data = entity.IsModeMapping;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsModeMapping = null;
					else entity.IsModeMapping = Convert.ToBoolean(value);
				}
			}
			private esQuestionForm entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQuestionFormQuery query)
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
				throw new Exception("esQuestionForm can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class QuestionForm : esQuestionForm
	{	
	}

	[Serializable]
	abstract public class esQuestionFormQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return QuestionFormMetadata.Meta();
			}
		}	
			
		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionFormName
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.QuestionFormName, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsMCUForm
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsMCUForm, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReportProgramID
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.ReportProgramID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAskepForm
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsAskepForm, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVSignForm
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsVSignForm, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSingleEntry
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsSingleEntry, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem RmNO
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.RmNO, esSystemType.String);
			}
		} 
			
		public esQueryItem IsInitialAssessment
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsInitialAssessment, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsContinuedAssessment
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsContinuedAssessment, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsGeneralForm
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsGeneralForm, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsNutritionCareForm
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsNutritionCareForm, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSoapForm
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsSoapForm, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRQuestionFormType
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.SRQuestionFormType, esSystemType.String);
			}
		} 
			
		public esQueryItem SRNsType
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.SRNsType, esSystemType.String);
			}
		} 
			
		public esQueryItem RestrictionUserType
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.RestrictionUserType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsSharingEdit
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsSharingEdit, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsUsingApproval
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsUsingApproval, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRAutoNumber
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.SRAutoNumber, esSystemType.String);
			}
		} 
			
		public esQueryItem IsModeMapping
		{
			get
			{
				return new esQueryItem(this, QuestionFormMetadata.ColumnNames.IsModeMapping, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QuestionFormCollection")]
	public partial class QuestionFormCollection : esQuestionFormCollection, IEnumerable< QuestionForm>
	{
		public QuestionFormCollection()
		{

		}	
		
		public static implicit operator List< QuestionForm>(QuestionFormCollection coll)
		{
			List< QuestionForm> list = new List< QuestionForm>();
			
			foreach (QuestionForm emp in coll)
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
				return  QuestionFormMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QuestionForm(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QuestionForm();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public QuestionFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionFormQuery();
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
		public bool Load(QuestionFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public QuestionForm AddNew()
		{
			QuestionForm entity = base.AddNewEntity() as QuestionForm;
			
			return entity;		
		}
		public QuestionForm FindByPrimaryKey(String questionFormID)
		{
			return base.FindByPrimaryKey(questionFormID) as QuestionForm;
		}

		#region IEnumerable< QuestionForm> Members

		IEnumerator< QuestionForm> IEnumerable< QuestionForm>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QuestionForm;
			}
		}

		#endregion
		
		private QuestionFormQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QuestionForm' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("QuestionForm ({QuestionFormID})")]
	[Serializable]
	public partial class QuestionForm : esQuestionForm
	{
		public QuestionForm()
		{
		}	
	
		public QuestionForm(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QuestionFormMetadata.Meta();
			}
		}	
	
		override protected esQuestionFormQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public QuestionFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionFormQuery();
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
		public bool Load(QuestionFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private QuestionFormQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class QuestionFormQuery : esQuestionFormQuery
	{
		public QuestionFormQuery()
		{

		}		
		
		public QuestionFormQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "QuestionFormQuery";
        }
	}

	[Serializable]
	public partial class QuestionFormMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QuestionFormMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.QuestionFormID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.QuestionFormName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.QuestionFormName;
			c.CharacterMaxLength = 400;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsMCUForm, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsMCUForm;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QuestionFormMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.ReportProgramID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.ReportProgramID;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsAskepForm, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsAskepForm;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsVSignForm, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsVSignForm;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsSingleEntry, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsSingleEntry;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.RmNO, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.RmNO;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsInitialAssessment, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsInitialAssessment;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsContinuedAssessment, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsContinuedAssessment;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsGeneralForm, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsGeneralForm;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsNutritionCareForm, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsNutritionCareForm;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsSoapForm, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsSoapForm;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.SRQuestionFormType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.SRQuestionFormType;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.SRNsType, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.SRNsType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.RestrictionUserType, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.RestrictionUserType;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsSharingEdit, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsSharingEdit;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsUsingApproval, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsUsingApproval;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.SRAutoNumber, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormMetadata.PropertyNames.SRAutoNumber;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(QuestionFormMetadata.ColumnNames.IsModeMapping, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionFormMetadata.PropertyNames.IsModeMapping;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public QuestionFormMetadata Meta()
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
			public const string QuestionFormID = "QuestionFormID";
			public const string QuestionFormName = "QuestionFormName";
			public const string IsActive = "IsActive";
			public const string IsMCUForm = "IsMCUForm";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReportProgramID = "ReportProgramID";
			public const string IsAskepForm = "IsAskepForm";
			public const string IsVSignForm = "IsVSignForm";
			public const string IsSingleEntry = "IsSingleEntry";
			public const string RmNO = "RmNO";
			public const string IsInitialAssessment = "IsInitialAssessment";
			public const string IsContinuedAssessment = "IsContinuedAssessment";
			public const string IsGeneralForm = "IsGeneralForm";
			public const string IsNutritionCareForm = "IsNutritionCareForm";
			public const string IsSoapForm = "IsSoapForm";
			public const string SRQuestionFormType = "SRQuestionFormType";
			public const string SRNsType = "SRNsType";
			public const string RestrictionUserType = "RestrictionUserType";
			public const string IsSharingEdit = "IsSharingEdit";
			public const string IsUsingApproval = "IsUsingApproval";
			public const string SRAutoNumber = "SRAutoNumber";
			public const string IsModeMapping = "IsModeMapping";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string QuestionFormID = "QuestionFormID";
			public const string QuestionFormName = "QuestionFormName";
			public const string IsActive = "IsActive";
			public const string IsMCUForm = "IsMCUForm";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReportProgramID = "ReportProgramID";
			public const string IsAskepForm = "IsAskepForm";
			public const string IsVSignForm = "IsVSignForm";
			public const string IsSingleEntry = "IsSingleEntry";
			public const string RmNO = "RmNO";
			public const string IsInitialAssessment = "IsInitialAssessment";
			public const string IsContinuedAssessment = "IsContinuedAssessment";
			public const string IsGeneralForm = "IsGeneralForm";
			public const string IsNutritionCareForm = "IsNutritionCareForm";
			public const string IsSoapForm = "IsSoapForm";
			public const string SRQuestionFormType = "SRQuestionFormType";
			public const string SRNsType = "SRNsType";
			public const string RestrictionUserType = "RestrictionUserType";
			public const string IsSharingEdit = "IsSharingEdit";
			public const string IsUsingApproval = "IsUsingApproval";
			public const string SRAutoNumber = "SRAutoNumber";
			public const string IsModeMapping = "IsModeMapping";
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
			lock (typeof(QuestionFormMetadata))
			{
				if(QuestionFormMetadata.mapDelegates == null)
				{
					QuestionFormMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QuestionFormMetadata.meta == null)
				{
					QuestionFormMetadata.meta = new QuestionFormMetadata();
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
				
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMCUForm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReportProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAskepForm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVSignForm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSingleEntry", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RmNO", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInitialAssessment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsContinuedAssessment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGeneralForm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNutritionCareForm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSoapForm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRQuestionFormType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRNsType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RestrictionUserType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSharingEdit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUsingApproval", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRAutoNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsModeMapping", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "QuestionForm";
				meta.Destination = "QuestionForm";
				meta.spInsert = "proc_QuestionFormInsert";				
				meta.spUpdate = "proc_QuestionFormUpdate";		
				meta.spDelete = "proc_QuestionFormDelete";
				meta.spLoadAll = "proc_QuestionFormLoadAll";
				meta.spLoadByPrimaryKey = "proc_QuestionFormLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QuestionFormMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
