/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/2/2019 5:53:49 PM
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
	abstract public class esNursingAssessmentQuestionCollection : esEntityCollectionWAuditLog
	{
		public esNursingAssessmentQuestionCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NursingAssessmentQuestionCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNursingAssessmentQuestionQuery query)
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
			this.InitQuery(query as esNursingAssessmentQuestionQuery);
		}
		#endregion
			
		virtual public NursingAssessmentQuestion DetachEntity(NursingAssessmentQuestion entity)
		{
			return base.DetachEntity(entity) as NursingAssessmentQuestion;
		}
		
		virtual public NursingAssessmentQuestion AttachEntity(NursingAssessmentQuestion entity)
		{
			return base.AttachEntity(entity) as NursingAssessmentQuestion;
		}
		
		virtual public void Combine(NursingAssessmentQuestionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingAssessmentQuestion this[int index]
		{
			get
			{
				return base[index] as NursingAssessmentQuestion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingAssessmentQuestion);
		}
	}

	[Serializable]
	abstract public class esNursingAssessmentQuestion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingAssessmentQuestionQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNursingAssessmentQuestion()
		{
		}
	
		public esNursingAssessmentQuestion(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String questionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String questionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String questionID)
		{
			esNursingAssessmentQuestionQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionID==questionID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String questionID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionID",questionID);
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
						case "QuestionID": this.str.QuestionID = (string)value; break;
						case "ParentQuestionID": this.str.ParentQuestionID = (string)value; break;
						case "IndexNo": this.str.IndexNo = (string)value; break;
						case "QuestionLevel": this.str.QuestionLevel = (string)value; break;
						case "QuestionText": this.str.QuestionText = (string)value; break;
						case "QuestionShortText": this.str.QuestionShortText = (string)value; break;
						case "SRAnswerType": this.str.SRAnswerType = (string)value; break;
						case "AnswerDecimalDigit": this.str.AnswerDecimalDigit = (string)value; break;
						case "AnswerPrefix": this.str.AnswerPrefix = (string)value; break;
						case "AnswerSuffix": this.str.AnswerSuffix = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "AnswerWidth": this.str.AnswerWidth = (string)value; break;
						case "AnswerWidth2": this.str.AnswerWidth2 = (string)value; break;
						case "QuestionAnswerSelectionID": this.str.QuestionAnswerSelectionID = (string)value; break;
						case "QuestionAnswerDefaultSelectionID": this.str.QuestionAnswerDefaultSelectionID = (string)value; break;
						case "QuestionAnswerSelectionID2": this.str.QuestionAnswerSelectionID2 = (string)value; break;
						case "QuestionAnswerDefaultSelectionID2": this.str.QuestionAnswerDefaultSelectionID2 = (string)value; break;
						case "Formula": this.str.Formula = (string)value; break;
						case "IsAlwaysPrint": this.str.IsAlwaysPrint = (string)value; break;
						case "IsMandatory": this.str.IsMandatory = (string)value; break;
						case "IsSubjective": this.str.IsSubjective = (string)value; break;
						case "IsObjective": this.str.IsObjective = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "RelatedQuestionID": this.str.RelatedQuestionID = (string)value; break;
						case "EquivalentQuestionID": this.str.EquivalentQuestionID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IndexNo":
						
							if (value == null || value is System.Int32)
								this.IndexNo = (System.Int32?)value;
							break;
						case "QuestionLevel":
						
							if (value == null || value is System.Int32)
								this.QuestionLevel = (System.Int32?)value;
							break;
						case "AnswerDecimalDigit":
						
							if (value == null || value is System.Int32)
								this.AnswerDecimalDigit = (System.Int32?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "AnswerWidth":
						
							if (value == null || value is System.Int32)
								this.AnswerWidth = (System.Int32?)value;
							break;
						case "AnswerWidth2":
						
							if (value == null || value is System.Int32)
								this.AnswerWidth2 = (System.Int32?)value;
							break;
						case "IsAlwaysPrint":
						
							if (value == null || value is System.Boolean)
								this.IsAlwaysPrint = (System.Boolean?)value;
							break;
						case "IsMandatory":
						
							if (value == null || value is System.Boolean)
								this.IsMandatory = (System.Boolean?)value;
							break;
						case "IsSubjective":
						
							if (value == null || value is System.Boolean)
								this.IsSubjective = (System.Boolean?)value;
							break;
						case "IsObjective":
						
							if (value == null || value is System.Boolean)
								this.IsObjective = (System.Boolean?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to NursingAssessmentQuestion.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.ParentQuestionID
		/// </summary>
		virtual public System.String ParentQuestionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.ParentQuestionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.ParentQuestionID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.IndexNo
		/// </summary>
		virtual public System.Int32? IndexNo
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.IndexNo);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.IndexNo, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.QuestionLevel
		/// </summary>
		virtual public System.Int32? QuestionLevel
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.QuestionLevel);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.QuestionLevel, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.QuestionText
		/// </summary>
		virtual public System.String QuestionText
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionText);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionText, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.QuestionShortText
		/// </summary>
		virtual public System.String QuestionShortText
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionShortText);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionShortText, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.SRAnswerType
		/// </summary>
		virtual public System.String SRAnswerType
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.SRAnswerType);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.SRAnswerType, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.AnswerDecimalDigit
		/// </summary>
		virtual public System.Int32? AnswerDecimalDigit
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.AnswerPrefix
		/// </summary>
		virtual public System.String AnswerPrefix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.AnswerPrefix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.AnswerPrefix, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.AnswerSuffix
		/// </summary>
		virtual public System.String AnswerSuffix
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.AnswerSuffix);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.AnswerSuffix, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.AnswerWidth
		/// </summary>
		virtual public System.Int32? AnswerWidth
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.AnswerWidth2
		/// </summary>
		virtual public System.Int32? AnswerWidth2
		{
			get
			{
				return base.GetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth2);
			}
			
			set
			{
				base.SetSystemInt32(NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth2, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.QuestionAnswerSelectionID
		/// </summary>
		virtual public System.String QuestionAnswerSelectionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.QuestionAnswerDefaultSelectionID
		/// </summary>
		virtual public System.String QuestionAnswerDefaultSelectionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.QuestionAnswerSelectionID2
		/// </summary>
		virtual public System.String QuestionAnswerSelectionID2
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.QuestionAnswerDefaultSelectionID2
		/// </summary>
		virtual public System.String QuestionAnswerDefaultSelectionID2
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.Formula
		/// </summary>
		virtual public System.String Formula
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.Formula);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.Formula, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.IsAlwaysPrint
		/// </summary>
		virtual public System.Boolean? IsAlwaysPrint
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.IsMandatory
		/// </summary>
		virtual public System.Boolean? IsMandatory
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsMandatory);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsMandatory, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.IsSubjective
		/// </summary>
		virtual public System.Boolean? IsSubjective
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsSubjective);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsSubjective, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.IsObjective
		/// </summary>
		virtual public System.Boolean? IsObjective
		{
			get
			{
				return base.GetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsObjective);
			}
			
			set
			{
				base.SetSystemBoolean(NursingAssessmentQuestionMetadata.ColumnNames.IsObjective, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentQuestionMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentQuestionMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.RelatedQuestionID
		/// </summary>
		virtual public System.String RelatedQuestionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID, value);
			}
		}
		/// <summary>
		/// Maps to NursingAssessmentQuestion.EquivalentQuestionID
		/// </summary>
		virtual public System.String EquivalentQuestionID
		{
			get
			{
				return base.GetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.EquivalentQuestionID);
			}
			
			set
			{
				base.SetSystemString(NursingAssessmentQuestionMetadata.ColumnNames.EquivalentQuestionID, value);
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
			public esStrings(esNursingAssessmentQuestion entity)
			{
				this.entity = entity;
			}
			public System.String QuestionID
			{
				get
				{
					System.String data = entity.QuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionID = null;
					else entity.QuestionID = Convert.ToString(value);
				}
			}
			public System.String ParentQuestionID
			{
				get
				{
					System.String data = entity.ParentQuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentQuestionID = null;
					else entity.ParentQuestionID = Convert.ToString(value);
				}
			}
			public System.String IndexNo
			{
				get
				{
					System.Int32? data = entity.IndexNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndexNo = null;
					else entity.IndexNo = Convert.ToInt32(value);
				}
			}
			public System.String QuestionLevel
			{
				get
				{
					System.Int32? data = entity.QuestionLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionLevel = null;
					else entity.QuestionLevel = Convert.ToInt32(value);
				}
			}
			public System.String QuestionText
			{
				get
				{
					System.String data = entity.QuestionText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionText = null;
					else entity.QuestionText = Convert.ToString(value);
				}
			}
			public System.String QuestionShortText
			{
				get
				{
					System.String data = entity.QuestionShortText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionShortText = null;
					else entity.QuestionShortText = Convert.ToString(value);
				}
			}
			public System.String SRAnswerType
			{
				get
				{
					System.String data = entity.SRAnswerType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAnswerType = null;
					else entity.SRAnswerType = Convert.ToString(value);
				}
			}
			public System.String AnswerDecimalDigit
			{
				get
				{
					System.Int32? data = entity.AnswerDecimalDigit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerDecimalDigit = null;
					else entity.AnswerDecimalDigit = Convert.ToInt32(value);
				}
			}
			public System.String AnswerPrefix
			{
				get
				{
					System.String data = entity.AnswerPrefix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerPrefix = null;
					else entity.AnswerPrefix = Convert.ToString(value);
				}
			}
			public System.String AnswerSuffix
			{
				get
				{
					System.String data = entity.AnswerSuffix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerSuffix = null;
					else entity.AnswerSuffix = Convert.ToString(value);
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
			public System.String AnswerWidth
			{
				get
				{
					System.Int32? data = entity.AnswerWidth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerWidth = null;
					else entity.AnswerWidth = Convert.ToInt32(value);
				}
			}
			public System.String AnswerWidth2
			{
				get
				{
					System.Int32? data = entity.AnswerWidth2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerWidth2 = null;
					else entity.AnswerWidth2 = Convert.ToInt32(value);
				}
			}
			public System.String QuestionAnswerSelectionID
			{
				get
				{
					System.String data = entity.QuestionAnswerSelectionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSelectionID = null;
					else entity.QuestionAnswerSelectionID = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerDefaultSelectionID
			{
				get
				{
					System.String data = entity.QuestionAnswerDefaultSelectionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerDefaultSelectionID = null;
					else entity.QuestionAnswerDefaultSelectionID = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerSelectionID2
			{
				get
				{
					System.String data = entity.QuestionAnswerSelectionID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSelectionID2 = null;
					else entity.QuestionAnswerSelectionID2 = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerDefaultSelectionID2
			{
				get
				{
					System.String data = entity.QuestionAnswerDefaultSelectionID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerDefaultSelectionID2 = null;
					else entity.QuestionAnswerDefaultSelectionID2 = Convert.ToString(value);
				}
			}
			public System.String Formula
			{
				get
				{
					System.String data = entity.Formula;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Formula = null;
					else entity.Formula = Convert.ToString(value);
				}
			}
			public System.String IsAlwaysPrint
			{
				get
				{
					System.Boolean? data = entity.IsAlwaysPrint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAlwaysPrint = null;
					else entity.IsAlwaysPrint = Convert.ToBoolean(value);
				}
			}
			public System.String IsMandatory
			{
				get
				{
					System.Boolean? data = entity.IsMandatory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMandatory = null;
					else entity.IsMandatory = Convert.ToBoolean(value);
				}
			}
			public System.String IsSubjective
			{
				get
				{
					System.Boolean? data = entity.IsSubjective;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSubjective = null;
					else entity.IsSubjective = Convert.ToBoolean(value);
				}
			}
			public System.String IsObjective
			{
				get
				{
					System.Boolean? data = entity.IsObjective;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsObjective = null;
					else entity.IsObjective = Convert.ToBoolean(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
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
			public System.String RelatedQuestionID
			{
				get
				{
					System.String data = entity.RelatedQuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RelatedQuestionID = null;
					else entity.RelatedQuestionID = Convert.ToString(value);
				}
			}
			public System.String EquivalentQuestionID
			{
				get
				{
					System.String data = entity.EquivalentQuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EquivalentQuestionID = null;
					else entity.EquivalentQuestionID = Convert.ToString(value);
				}
			}
			private esNursingAssessmentQuestion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingAssessmentQuestionQuery query)
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
				throw new Exception("esNursingAssessmentQuestion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NursingAssessmentQuestion : esNursingAssessmentQuestion
	{	
	}

	[Serializable]
	abstract public class esNursingAssessmentQuestionQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentQuestionMetadata.Meta();
			}
		}	
			
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParentQuestionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.ParentQuestionID, esSystemType.String);
			}
		} 
			
		public esQueryItem IndexNo
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.IndexNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem QuestionLevel
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionLevel, esSystemType.Int32);
			}
		} 
			
		public esQueryItem QuestionText
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionText, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionShortText
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionShortText, esSystemType.String);
			}
		} 
			
		public esQueryItem SRAnswerType
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.SRAnswerType, esSystemType.String);
			}
		} 
			
		public esQueryItem AnswerDecimalDigit
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AnswerPrefix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.AnswerPrefix, esSystemType.String);
			}
		} 
			
		public esQueryItem AnswerSuffix
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.AnswerSuffix, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem AnswerWidth
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AnswerWidth2
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth2, esSystemType.Int32);
			}
		} 
			
		public esQueryItem QuestionAnswerSelectionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerDefaultSelectionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerSelectionID2
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, esSystemType.String);
			}
		} 
			
		public esQueryItem QuestionAnswerDefaultSelectionID2
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, esSystemType.String);
			}
		} 
			
		public esQueryItem Formula
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.Formula, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAlwaysPrint
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsMandatory
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.IsMandatory, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSubjective
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.IsSubjective, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsObjective
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.IsObjective, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RelatedQuestionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID, esSystemType.String);
			}
		} 
			
		public esQueryItem EquivalentQuestionID
		{
			get
			{
				return new esQueryItem(this, NursingAssessmentQuestionMetadata.ColumnNames.EquivalentQuestionID, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingAssessmentQuestionCollection")]
	public partial class NursingAssessmentQuestionCollection : esNursingAssessmentQuestionCollection, IEnumerable< NursingAssessmentQuestion>
	{
		public NursingAssessmentQuestionCollection()
		{

		}	
		
		public static implicit operator List< NursingAssessmentQuestion>(NursingAssessmentQuestionCollection coll)
		{
			List< NursingAssessmentQuestion> list = new List< NursingAssessmentQuestion>();
			
			foreach (NursingAssessmentQuestion emp in coll)
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
				return  NursingAssessmentQuestionMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentQuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingAssessmentQuestion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingAssessmentQuestion();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NursingAssessmentQuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentQuestionQuery();
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
		public bool Load(NursingAssessmentQuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NursingAssessmentQuestion AddNew()
		{
			NursingAssessmentQuestion entity = base.AddNewEntity() as NursingAssessmentQuestion;
			
			return entity;		
		}
		public NursingAssessmentQuestion FindByPrimaryKey(String questionID)
		{
			return base.FindByPrimaryKey(questionID) as NursingAssessmentQuestion;
		}

		#region IEnumerable< NursingAssessmentQuestion> Members

		IEnumerator< NursingAssessmentQuestion> IEnumerable< NursingAssessmentQuestion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingAssessmentQuestion;
			}
		}

		#endregion
		
		private NursingAssessmentQuestionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingAssessmentQuestion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NursingAssessmentQuestion ({QuestionID})")]
	[Serializable]
	public partial class NursingAssessmentQuestion : esNursingAssessmentQuestion
	{
		public NursingAssessmentQuestion()
		{
		}	
	
		public NursingAssessmentQuestion(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingAssessmentQuestionMetadata.Meta();
			}
		}	
	
		override protected esNursingAssessmentQuestionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingAssessmentQuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NursingAssessmentQuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingAssessmentQuestionQuery();
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
		public bool Load(NursingAssessmentQuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NursingAssessmentQuestionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NursingAssessmentQuestionQuery : esNursingAssessmentQuestionQuery
	{
		public NursingAssessmentQuestionQuery()
		{

		}		
		
		public NursingAssessmentQuestionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NursingAssessmentQuestionQuery";
        }
	}

	[Serializable]
	public partial class NursingAssessmentQuestionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingAssessmentQuestionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.ParentQuestionID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.ParentQuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.IndexNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.IndexNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionLevel, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionLevel;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionText, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionText;
			c.CharacterMaxLength = 500;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionShortText, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionShortText;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.SRAnswerType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.SRAnswerType;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.AnswerDecimalDigit, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.AnswerDecimalDigit;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.AnswerPrefix, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.AnswerPrefix;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.AnswerSuffix, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.AnswerSuffix;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.IsActive, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.AnswerWidth;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.AnswerWidth2, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.AnswerWidth2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionAnswerSelectionID;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionAnswerDefaultSelectionID;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionAnswerSelectionID2;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.QuestionAnswerDefaultSelectionID2;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.Formula, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.Formula;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.IsAlwaysPrint, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.IsAlwaysPrint;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.IsMandatory, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.IsMandatory;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.IsSubjective, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.IsSubjective;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.IsObjective, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.IsObjective;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.CreateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.CreateDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateByUserID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.LastUpdateDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.RelatedQuestionID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.RelatedQuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NursingAssessmentQuestionMetadata.ColumnNames.EquivalentQuestionID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingAssessmentQuestionMetadata.PropertyNames.EquivalentQuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NursingAssessmentQuestionMetadata Meta()
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
			public const string QuestionID = "QuestionID";
			public const string ParentQuestionID = "ParentQuestionID";
			public const string IndexNo = "IndexNo";
			public const string QuestionLevel = "QuestionLevel";
			public const string QuestionText = "QuestionText";
			public const string QuestionShortText = "QuestionShortText";
			public const string SRAnswerType = "SRAnswerType";
			public const string AnswerDecimalDigit = "AnswerDecimalDigit";
			public const string AnswerPrefix = "AnswerPrefix";
			public const string AnswerSuffix = "AnswerSuffix";
			public const string IsActive = "IsActive";
			public const string AnswerWidth = "AnswerWidth";
			public const string AnswerWidth2 = "AnswerWidth2";
			public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
			public const string QuestionAnswerDefaultSelectionID = "QuestionAnswerDefaultSelectionID";
			public const string QuestionAnswerSelectionID2 = "QuestionAnswerSelectionID2";
			public const string QuestionAnswerDefaultSelectionID2 = "QuestionAnswerDefaultSelectionID2";
			public const string Formula = "Formula";
			public const string IsAlwaysPrint = "IsAlwaysPrint";
			public const string IsMandatory = "IsMandatory";
			public const string IsSubjective = "IsSubjective";
			public const string IsObjective = "IsObjective";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string RelatedQuestionID = "RelatedQuestionID";
			public const string EquivalentQuestionID = "EquivalentQuestionID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string QuestionID = "QuestionID";
			public const string ParentQuestionID = "ParentQuestionID";
			public const string IndexNo = "IndexNo";
			public const string QuestionLevel = "QuestionLevel";
			public const string QuestionText = "QuestionText";
			public const string QuestionShortText = "QuestionShortText";
			public const string SRAnswerType = "SRAnswerType";
			public const string AnswerDecimalDigit = "AnswerDecimalDigit";
			public const string AnswerPrefix = "AnswerPrefix";
			public const string AnswerSuffix = "AnswerSuffix";
			public const string IsActive = "IsActive";
			public const string AnswerWidth = "AnswerWidth";
			public const string AnswerWidth2 = "AnswerWidth2";
			public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
			public const string QuestionAnswerDefaultSelectionID = "QuestionAnswerDefaultSelectionID";
			public const string QuestionAnswerSelectionID2 = "QuestionAnswerSelectionID2";
			public const string QuestionAnswerDefaultSelectionID2 = "QuestionAnswerDefaultSelectionID2";
			public const string Formula = "Formula";
			public const string IsAlwaysPrint = "IsAlwaysPrint";
			public const string IsMandatory = "IsMandatory";
			public const string IsSubjective = "IsSubjective";
			public const string IsObjective = "IsObjective";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string RelatedQuestionID = "RelatedQuestionID";
			public const string EquivalentQuestionID = "EquivalentQuestionID";
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
			lock (typeof(NursingAssessmentQuestionMetadata))
			{
				if(NursingAssessmentQuestionMetadata.mapDelegates == null)
				{
					NursingAssessmentQuestionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingAssessmentQuestionMetadata.meta == null)
				{
					NursingAssessmentQuestionMetadata.meta = new NursingAssessmentQuestionMetadata();
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
				
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentQuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionShortText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAnswerType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerDecimalDigit", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AnswerPrefix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerSuffix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AnswerWidth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AnswerWidth2", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionAnswerSelectionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerDefaultSelectionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerSelectionID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerDefaultSelectionID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Formula", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAlwaysPrint", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMandatory", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSubjective", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsObjective", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RelatedQuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EquivalentQuestionID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "NursingAssessmentQuestion";
				meta.Destination = "NursingAssessmentQuestion";
				meta.spInsert = "proc_NursingAssessmentQuestionInsert";				
				meta.spUpdate = "proc_NursingAssessmentQuestionUpdate";		
				meta.spDelete = "proc_NursingAssessmentQuestionDelete";
				meta.spLoadAll = "proc_NursingAssessmentQuestionLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingAssessmentQuestionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingAssessmentQuestionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
