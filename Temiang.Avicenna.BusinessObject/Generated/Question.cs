/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/18/2022 11:45:16 PM
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
	abstract public class esQuestionCollection : esEntityCollectionWAuditLog
	{
		public esQuestionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "QuestionCollection";
		}

		#region Query Logic
		protected void InitQuery(esQuestionQuery query)
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
			this.InitQuery(query as esQuestionQuery);
		}
		#endregion

		virtual public Question DetachEntity(Question entity)
		{
			return base.DetachEntity(entity) as Question;
		}

		virtual public Question AttachEntity(Question entity)
		{
			return base.AttachEntity(entity) as Question;
		}

		virtual public void Combine(QuestionCollection collection)
		{
			base.Combine(collection);
		}

		new public Question this[int index]
		{
			get
			{
				return base[index] as Question;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Question);
		}
	}

	[Serializable]
	abstract public class esQuestion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQuestionQuery GetDynamicQuery()
		{
			return null;
		}

		public esQuestion()
		{
		}

		public esQuestion(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String questionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
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
			esQuestionQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionID == questionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String questionID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionID", questionID);
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
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsMandatory": this.str.IsMandatory = (string)value; break;
						case "ReferenceQuestionID": this.str.ReferenceQuestionID = (string)value; break;
						case "VitalSignID": this.str.VitalSignID = (string)value; break;
						case "BodyID": this.str.BodyID = (string)value; break;
						case "RelatedEntityName": this.str.RelatedEntityName = (string)value; break;
						case "RelatedColumnName": this.str.RelatedColumnName = (string)value; break;
						case "LookUpID": this.str.LookUpID = (string)value; break;
						case "IsUpdateRelatedEntity": this.str.IsUpdateRelatedEntity = (string)value; break;
						case "IsReadOnly": this.str.IsReadOnly = (string)value; break;
						case "NursingDisplayAs": this.str.NursingDisplayAs = (string)value; break;
						case "EquivalentQuestionID": this.str.EquivalentQuestionID = (string)value; break;
						case "IsEmptyDefault": this.str.IsEmptyDefault = (string)value; break;
						case "IsNotOverWriteRelatedEntity": this.str.IsNotOverWriteRelatedEntity = (string)value; break;
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
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsMandatory":

							if (value == null || value is System.Boolean)
								this.IsMandatory = (System.Boolean?)value;
							break;
						case "IsUpdateRelatedEntity":

							if (value == null || value is System.Boolean)
								this.IsUpdateRelatedEntity = (System.Boolean?)value;
							break;
						case "IsReadOnly":

							if (value == null || value is System.Boolean)
								this.IsReadOnly = (System.Boolean?)value;
							break;
						case "IsEmptyDefault":

							if (value == null || value is System.Boolean)
								this.IsEmptyDefault = (System.Boolean?)value;
							break;
						case "IsNotOverWriteRelatedEntity":

							if (value == null || value is System.Boolean)
								this.IsNotOverWriteRelatedEntity = (System.Boolean?)value;
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
		/// Maps to Question.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.QuestionID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.QuestionID, value);
			}
		}
		/// <summary>
		/// Maps to Question.ParentQuestionID
		/// </summary>
		virtual public System.String ParentQuestionID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.ParentQuestionID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.ParentQuestionID, value);
			}
		}
		/// <summary>
		/// Maps to Question.IndexNo
		/// </summary>
		virtual public System.Int32? IndexNo
		{
			get
			{
				return base.GetSystemInt32(QuestionMetadata.ColumnNames.IndexNo);
			}

			set
			{
				base.SetSystemInt32(QuestionMetadata.ColumnNames.IndexNo, value);
			}
		}
		/// <summary>
		/// Maps to Question.QuestionLevel
		/// </summary>
		virtual public System.Int32? QuestionLevel
		{
			get
			{
				return base.GetSystemInt32(QuestionMetadata.ColumnNames.QuestionLevel);
			}

			set
			{
				base.SetSystemInt32(QuestionMetadata.ColumnNames.QuestionLevel, value);
			}
		}
		/// <summary>
		/// Maps to Question.QuestionText
		/// </summary>
		virtual public System.String QuestionText
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.QuestionText);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.QuestionText, value);
			}
		}
		/// <summary>
		/// Maps to Question.QuestionShortText
		/// </summary>
		virtual public System.String QuestionShortText
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.QuestionShortText);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.QuestionShortText, value);
			}
		}
		/// <summary>
		/// Maps to Question.SRAnswerType
		/// </summary>
		virtual public System.String SRAnswerType
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.SRAnswerType);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.SRAnswerType, value);
			}
		}
		/// <summary>
		/// Maps to Question.AnswerDecimalDigit
		/// </summary>
		virtual public System.Int32? AnswerDecimalDigit
		{
			get
			{
				return base.GetSystemInt32(QuestionMetadata.ColumnNames.AnswerDecimalDigit);
			}

			set
			{
				base.SetSystemInt32(QuestionMetadata.ColumnNames.AnswerDecimalDigit, value);
			}
		}
		/// <summary>
		/// Maps to Question.AnswerPrefix
		/// </summary>
		virtual public System.String AnswerPrefix
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.AnswerPrefix);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.AnswerPrefix, value);
			}
		}
		/// <summary>
		/// Maps to Question.AnswerSuffix
		/// </summary>
		virtual public System.String AnswerSuffix
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.AnswerSuffix);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.AnswerSuffix, value);
			}
		}
		/// <summary>
		/// Maps to Question.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(QuestionMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(QuestionMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Question.AnswerWidth
		/// </summary>
		virtual public System.Int32? AnswerWidth
		{
			get
			{
				return base.GetSystemInt32(QuestionMetadata.ColumnNames.AnswerWidth);
			}

			set
			{
				base.SetSystemInt32(QuestionMetadata.ColumnNames.AnswerWidth, value);
			}
		}
		/// <summary>
		/// Maps to Question.AnswerWidth2
		/// </summary>
		virtual public System.Int32? AnswerWidth2
		{
			get
			{
				return base.GetSystemInt32(QuestionMetadata.ColumnNames.AnswerWidth2);
			}

			set
			{
				base.SetSystemInt32(QuestionMetadata.ColumnNames.AnswerWidth2, value);
			}
		}
		/// <summary>
		/// Maps to Question.QuestionAnswerSelectionID
		/// </summary>
		virtual public System.String QuestionAnswerSelectionID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerSelectionID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerSelectionID, value);
			}
		}
		/// <summary>
		/// Maps to Question.QuestionAnswerDefaultSelectionID
		/// </summary>
		virtual public System.String QuestionAnswerDefaultSelectionID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, value);
			}
		}
		/// <summary>
		/// Maps to Question.QuestionAnswerSelectionID2
		/// </summary>
		virtual public System.String QuestionAnswerSelectionID2
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerSelectionID2);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, value);
			}
		}
		/// <summary>
		/// Maps to Question.QuestionAnswerDefaultSelectionID2
		/// </summary>
		virtual public System.String QuestionAnswerDefaultSelectionID2
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, value);
			}
		}
		/// <summary>
		/// Maps to Question.Formula
		/// </summary>
		virtual public System.String Formula
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.Formula);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.Formula, value);
			}
		}
		/// <summary>
		/// Maps to Question.IsAlwaysPrint
		/// </summary>
		virtual public System.Boolean? IsAlwaysPrint
		{
			get
			{
				return base.GetSystemBoolean(QuestionMetadata.ColumnNames.IsAlwaysPrint);
			}

			set
			{
				base.SetSystemBoolean(QuestionMetadata.ColumnNames.IsAlwaysPrint, value);
			}
		}
		/// <summary>
		/// Maps to Question.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QuestionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(QuestionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Question.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Question.IsMandatory
		/// </summary>
		virtual public System.Boolean? IsMandatory
		{
			get
			{
				return base.GetSystemBoolean(QuestionMetadata.ColumnNames.IsMandatory);
			}

			set
			{
				base.SetSystemBoolean(QuestionMetadata.ColumnNames.IsMandatory, value);
			}
		}
		/// <summary>
		/// Maps to Question.ReferenceQuestionID
		/// </summary>
		virtual public System.String ReferenceQuestionID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.ReferenceQuestionID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.ReferenceQuestionID, value);
			}
		}
		/// <summary>
		/// Maps to Question.VitalSignID
		/// </summary>
		virtual public System.String VitalSignID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.VitalSignID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.VitalSignID, value);
			}
		}
		/// <summary>
		/// Maps to Question.BodyID
		/// </summary>
		virtual public System.String BodyID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.BodyID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.BodyID, value);
			}
		}
		/// <summary>
		/// Maps to Question.RelatedEntityName
		/// </summary>
		virtual public System.String RelatedEntityName
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.RelatedEntityName);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.RelatedEntityName, value);
			}
		}
		/// <summary>
		/// Maps to Question.RelatedColumnName
		/// </summary>
		virtual public System.String RelatedColumnName
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.RelatedColumnName);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.RelatedColumnName, value);
			}
		}
		/// <summary>
		/// Maps to Question.LookUpID
		/// </summary>
		virtual public System.String LookUpID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.LookUpID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.LookUpID, value);
			}
		}
		/// <summary>
		/// Maps to Question.IsUpdateRelatedEntity
		/// </summary>
		virtual public System.Boolean? IsUpdateRelatedEntity
		{
			get
			{
				return base.GetSystemBoolean(QuestionMetadata.ColumnNames.IsUpdateRelatedEntity);
			}

			set
			{
				base.SetSystemBoolean(QuestionMetadata.ColumnNames.IsUpdateRelatedEntity, value);
			}
		}
		/// <summary>
		/// Maps to Question.IsReadOnly
		/// </summary>
		virtual public System.Boolean? IsReadOnly
		{
			get
			{
				return base.GetSystemBoolean(QuestionMetadata.ColumnNames.IsReadOnly);
			}

			set
			{
				base.SetSystemBoolean(QuestionMetadata.ColumnNames.IsReadOnly, value);
			}
		}
		/// <summary>
		/// Maps to Question.NursingDisplayAs
		/// </summary>
		virtual public System.String NursingDisplayAs
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.NursingDisplayAs);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.NursingDisplayAs, value);
			}
		}
		/// <summary>
		/// Maps to Question.EquivalentQuestionID
		/// </summary>
		virtual public System.String EquivalentQuestionID
		{
			get
			{
				return base.GetSystemString(QuestionMetadata.ColumnNames.EquivalentQuestionID);
			}

			set
			{
				base.SetSystemString(QuestionMetadata.ColumnNames.EquivalentQuestionID, value);
			}
		}
		/// <summary>
		/// Maps to Question.IsEmptyDefault
		/// </summary>
		virtual public System.Boolean? IsEmptyDefault
		{
			get
			{
				return base.GetSystemBoolean(QuestionMetadata.ColumnNames.IsEmptyDefault);
			}

			set
			{
				base.SetSystemBoolean(QuestionMetadata.ColumnNames.IsEmptyDefault, value);
			}
		}
		/// <summary>
		/// Maps to Question.IsNotOverWriteRelatedEntity
		/// </summary>
		virtual public System.Boolean? IsNotOverWriteRelatedEntity
		{
			get
			{
				return base.GetSystemBoolean(QuestionMetadata.ColumnNames.IsNotOverWriteRelatedEntity);
			}

			set
			{
				base.SetSystemBoolean(QuestionMetadata.ColumnNames.IsNotOverWriteRelatedEntity, value);
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
			public esStrings(esQuestion entity)
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
			public System.String ReferenceQuestionID
			{
				get
				{
					System.String data = entity.ReferenceQuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceQuestionID = null;
					else entity.ReferenceQuestionID = Convert.ToString(value);
				}
			}
			public System.String VitalSignID
			{
				get
				{
					System.String data = entity.VitalSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignID = null;
					else entity.VitalSignID = Convert.ToString(value);
				}
			}
			public System.String BodyID
			{
				get
				{
					System.String data = entity.BodyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyID = null;
					else entity.BodyID = Convert.ToString(value);
				}
			}
			public System.String RelatedEntityName
			{
				get
				{
					System.String data = entity.RelatedEntityName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RelatedEntityName = null;
					else entity.RelatedEntityName = Convert.ToString(value);
				}
			}
			public System.String RelatedColumnName
			{
				get
				{
					System.String data = entity.RelatedColumnName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RelatedColumnName = null;
					else entity.RelatedColumnName = Convert.ToString(value);
				}
			}
			public System.String LookUpID
			{
				get
				{
					System.String data = entity.LookUpID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LookUpID = null;
					else entity.LookUpID = Convert.ToString(value);
				}
			}
			public System.String IsUpdateRelatedEntity
			{
				get
				{
					System.Boolean? data = entity.IsUpdateRelatedEntity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUpdateRelatedEntity = null;
					else entity.IsUpdateRelatedEntity = Convert.ToBoolean(value);
				}
			}
			public System.String IsReadOnly
			{
				get
				{
					System.Boolean? data = entity.IsReadOnly;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReadOnly = null;
					else entity.IsReadOnly = Convert.ToBoolean(value);
				}
			}
			public System.String NursingDisplayAs
			{
				get
				{
					System.String data = entity.NursingDisplayAs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDisplayAs = null;
					else entity.NursingDisplayAs = Convert.ToString(value);
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
			public System.String IsEmptyDefault
			{
				get
				{
					System.Boolean? data = entity.IsEmptyDefault;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmptyDefault = null;
					else entity.IsEmptyDefault = Convert.ToBoolean(value);
				}
			}
			public System.String IsNotOverWriteRelatedEntity
			{
				get
				{
					System.Boolean? data = entity.IsNotOverWriteRelatedEntity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNotOverWriteRelatedEntity = null;
					else entity.IsNotOverWriteRelatedEntity = Convert.ToBoolean(value);
				}
			}
			private esQuestion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQuestionQuery query)
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
				throw new Exception("esQuestion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Question : esQuestion
	{
	}

	[Serializable]
	abstract public class esQuestionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return QuestionMetadata.Meta();
			}
		}

		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		}

		public esQueryItem ParentQuestionID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.ParentQuestionID, esSystemType.String);
			}
		}

		public esQueryItem IndexNo
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IndexNo, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionLevel
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionLevel, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionText
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionText, esSystemType.String);
			}
		}

		public esQueryItem QuestionShortText
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionShortText, esSystemType.String);
			}
		}

		public esQueryItem SRAnswerType
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.SRAnswerType, esSystemType.String);
			}
		}

		public esQueryItem AnswerDecimalDigit
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.AnswerDecimalDigit, esSystemType.Int32);
			}
		}

		public esQueryItem AnswerPrefix
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.AnswerPrefix, esSystemType.String);
			}
		}

		public esQueryItem AnswerSuffix
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.AnswerSuffix, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem AnswerWidth
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.AnswerWidth, esSystemType.Int32);
			}
		}

		public esQueryItem AnswerWidth2
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.AnswerWidth2, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionAnswerSelectionID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionAnswerSelectionID, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerDefaultSelectionID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerSelectionID2
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerDefaultSelectionID2
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, esSystemType.String);
			}
		}

		public esQueryItem Formula
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.Formula, esSystemType.String);
			}
		}

		public esQueryItem IsAlwaysPrint
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IsAlwaysPrint, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsMandatory
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IsMandatory, esSystemType.Boolean);
			}
		}

		public esQueryItem ReferenceQuestionID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.ReferenceQuestionID, esSystemType.String);
			}
		}

		public esQueryItem VitalSignID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.VitalSignID, esSystemType.String);
			}
		}

		public esQueryItem BodyID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.BodyID, esSystemType.String);
			}
		}

		public esQueryItem RelatedEntityName
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.RelatedEntityName, esSystemType.String);
			}
		}

		public esQueryItem RelatedColumnName
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.RelatedColumnName, esSystemType.String);
			}
		}

		public esQueryItem LookUpID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.LookUpID, esSystemType.String);
			}
		}

		public esQueryItem IsUpdateRelatedEntity
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IsUpdateRelatedEntity, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReadOnly
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IsReadOnly, esSystemType.Boolean);
			}
		}

		public esQueryItem NursingDisplayAs
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.NursingDisplayAs, esSystemType.String);
			}
		}

		public esQueryItem EquivalentQuestionID
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.EquivalentQuestionID, esSystemType.String);
			}
		}

		public esQueryItem IsEmptyDefault
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IsEmptyDefault, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNotOverWriteRelatedEntity
		{
			get
			{
				return new esQueryItem(this, QuestionMetadata.ColumnNames.IsNotOverWriteRelatedEntity, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QuestionCollection")]
	public partial class QuestionCollection : esQuestionCollection, IEnumerable<Question>
	{
		public QuestionCollection()
		{

		}

		public static implicit operator List<Question>(QuestionCollection coll)
		{
			List<Question> list = new List<Question>();

			foreach (Question emp in coll)
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
				return QuestionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Question(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Question();
		}

		#endregion

		[BrowsableAttribute(false)]
		public QuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionQuery();
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
		public bool Load(QuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Question AddNew()
		{
			Question entity = base.AddNewEntity() as Question;

			return entity;
		}
		public Question FindByPrimaryKey(String questionID)
		{
			return base.FindByPrimaryKey(questionID) as Question;
		}

		#region IEnumerable< Question> Members

		IEnumerator<Question> IEnumerable<Question>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Question;
			}
		}

		#endregion

		private QuestionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Question' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Question ({QuestionID})")]
	[Serializable]
	public partial class Question : esQuestion
	{
		public Question()
		{
		}

		public Question(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QuestionMetadata.Meta();
			}
		}

		override protected esQuestionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public QuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionQuery();
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
		public bool Load(QuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private QuestionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class QuestionQuery : esQuestionQuery
	{
		public QuestionQuery()
		{

		}

		public QuestionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "QuestionQuery";
		}
	}

	[Serializable]
	public partial class QuestionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QuestionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.ParentQuestionID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.ParentQuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IndexNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionMetadata.PropertyNames.IndexNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionLevel, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionLevel;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionText, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionText;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionShortText, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionShortText;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.SRAnswerType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.SRAnswerType;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.AnswerDecimalDigit, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionMetadata.PropertyNames.AnswerDecimalDigit;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.AnswerPrefix, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.AnswerPrefix;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.AnswerSuffix, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.AnswerSuffix;
			c.CharacterMaxLength = 60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IsActive, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.AnswerWidth, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionMetadata.PropertyNames.AnswerWidth;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.AnswerWidth2, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionMetadata.PropertyNames.AnswerWidth2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionAnswerSelectionID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionAnswerSelectionID;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionAnswerDefaultSelectionID;
			c.CharacterMaxLength = 2000;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionAnswerSelectionID2, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionAnswerSelectionID2;
			c.CharacterMaxLength = 225;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.QuestionAnswerDefaultSelectionID2, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.QuestionAnswerDefaultSelectionID2;
			c.CharacterMaxLength = 225;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.Formula, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.Formula;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IsAlwaysPrint, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionMetadata.PropertyNames.IsAlwaysPrint;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QuestionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IsMandatory, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionMetadata.PropertyNames.IsMandatory;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.ReferenceQuestionID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.ReferenceQuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.VitalSignID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.VitalSignID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.BodyID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.BodyID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.RelatedEntityName, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.RelatedEntityName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.RelatedColumnName, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.RelatedColumnName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.LookUpID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.LookUpID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IsUpdateRelatedEntity, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionMetadata.PropertyNames.IsUpdateRelatedEntity;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IsReadOnly, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionMetadata.PropertyNames.IsReadOnly;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.NursingDisplayAs, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.NursingDisplayAs;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.EquivalentQuestionID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionMetadata.PropertyNames.EquivalentQuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IsEmptyDefault, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionMetadata.PropertyNames.IsEmptyDefault;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(QuestionMetadata.ColumnNames.IsNotOverWriteRelatedEntity, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = QuestionMetadata.PropertyNames.IsNotOverWriteRelatedEntity;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public QuestionMetadata Meta()
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
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsMandatory = "IsMandatory";
			public const string ReferenceQuestionID = "ReferenceQuestionID";
			public const string VitalSignID = "VitalSignID";
			public const string BodyID = "BodyID";
			public const string RelatedEntityName = "RelatedEntityName";
			public const string RelatedColumnName = "RelatedColumnName";
			public const string LookUpID = "LookUpID";
			public const string IsUpdateRelatedEntity = "IsUpdateRelatedEntity";
			public const string IsReadOnly = "IsReadOnly";
			public const string NursingDisplayAs = "NursingDisplayAs";
			public const string EquivalentQuestionID = "EquivalentQuestionID";
			public const string IsEmptyDefault = "IsEmptyDefault";
			public const string IsNotOverWriteRelatedEntity = "IsNotOverWriteRelatedEntity";
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
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsMandatory = "IsMandatory";
			public const string ReferenceQuestionID = "ReferenceQuestionID";
			public const string VitalSignID = "VitalSignID";
			public const string BodyID = "BodyID";
			public const string RelatedEntityName = "RelatedEntityName";
			public const string RelatedColumnName = "RelatedColumnName";
			public const string LookUpID = "LookUpID";
			public const string IsUpdateRelatedEntity = "IsUpdateRelatedEntity";
			public const string IsReadOnly = "IsReadOnly";
			public const string NursingDisplayAs = "NursingDisplayAs";
			public const string EquivalentQuestionID = "EquivalentQuestionID";
			public const string IsEmptyDefault = "IsEmptyDefault";
			public const string IsNotOverWriteRelatedEntity = "IsNotOverWriteRelatedEntity";
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
			lock (typeof(QuestionMetadata))
			{
				if (QuestionMetadata.mapDelegates == null)
				{
					QuestionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (QuestionMetadata.meta == null)
				{
					QuestionMetadata.meta = new QuestionMetadata();
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
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMandatory", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceQuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RelatedEntityName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RelatedColumnName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LookUpID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUpdateRelatedEntity", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReadOnly", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NursingDisplayAs", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EquivalentQuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEmptyDefault", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNotOverWriteRelatedEntity", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "Question";
				meta.Destination = "Question";
				meta.spInsert = "proc_QuestionInsert";
				meta.spUpdate = "proc_QuestionUpdate";
				meta.spDelete = "proc_QuestionDelete";
				meta.spLoadAll = "proc_QuestionLoadAll";
				meta.spLoadByPrimaryKey = "proc_QuestionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QuestionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
