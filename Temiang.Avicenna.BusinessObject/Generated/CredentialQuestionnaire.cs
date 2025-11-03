/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/29/2022 10:05:19 PM
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
	abstract public class esCredentialQuestionnaireCollection : esEntityCollectionWAuditLog
	{
		public esCredentialQuestionnaireCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialQuestionnaireCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialQuestionnaireQuery query)
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
			this.InitQuery(query as esCredentialQuestionnaireQuery);
		}
		#endregion

		virtual public CredentialQuestionnaire DetachEntity(CredentialQuestionnaire entity)
		{
			return base.DetachEntity(entity) as CredentialQuestionnaire;
		}

		virtual public CredentialQuestionnaire AttachEntity(CredentialQuestionnaire entity)
		{
			return base.AttachEntity(entity) as CredentialQuestionnaire;
		}

		virtual public void Combine(CredentialQuestionnaireCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialQuestionnaire this[int index]
		{
			get
			{
				return base[index] as CredentialQuestionnaire;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialQuestionnaire);
		}
	}

	[Serializable]
	abstract public class esCredentialQuestionnaire : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialQuestionnaireQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialQuestionnaire()
		{
		}

		public esCredentialQuestionnaire(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 questionnaireID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionnaireID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionnaireID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 questionnaireID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionnaireID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionnaireID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 questionnaireID)
		{
			esCredentialQuestionnaireQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionnaireID == questionnaireID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 questionnaireID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionnaireID", questionnaireID);
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
						case "QuestionnaireID": this.str.QuestionnaireID = (string)value; break;
						case "QuestionnaireCode": this.str.QuestionnaireCode = (string)value; break;
						case "QuestionnaireName": this.str.QuestionnaireName = (string)value; break;
						case "SRProfessionGroup": this.str.SRProfessionGroup = (string)value; break;
						case "SRClinicalWorkArea": this.str.SRClinicalWorkArea = (string)value; break;
						case "SRClinicalAuthorityLevel": this.str.SRClinicalAuthorityLevel = (string)value; break;
						case "Info1": this.str.Info1 = (string)value; break;
						case "Info2": this.str.Info2 = (string)value; break;
						case "Info3": this.str.Info3 = (string)value; break;
						case "Info4": this.str.Info4 = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
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
						case "QuestionnaireID":

							if (value == null || value is System.Int32)
								this.QuestionnaireID = (System.Int32?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to CredentialQuestionnaire.QuestionnaireID
		/// </summary>
		virtual public System.Int32? QuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.QuestionnaireCode
		/// </summary>
		virtual public System.String QuestionnaireCode
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireCode);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireCode, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.QuestionnaireName
		/// </summary>
		virtual public System.String QuestionnaireName
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireName);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.SRProfessionGroup
		/// </summary>
		virtual public System.String SRProfessionGroup
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.SRProfessionGroup);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.SRProfessionGroup, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.SRClinicalWorkArea
		/// </summary>
		virtual public System.String SRClinicalWorkArea
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.SRClinicalAuthorityLevel
		/// </summary>
		virtual public System.String SRClinicalAuthorityLevel
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.SRClinicalAuthorityLevel);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.SRClinicalAuthorityLevel, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.Info1
		/// </summary>
		virtual public System.String Info1
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info1);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info1, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.Info2
		/// </summary>
		virtual public System.String Info2
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info2);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.Info3
		/// </summary>
		virtual public System.String Info3
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info3);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info3, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.Info4
		/// </summary>
		virtual public System.String Info4
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info4);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.Info4, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CredentialQuestionnaireMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(CredentialQuestionnaireMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialQuestionnaireMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialQuestionnaireMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialQuestionnaireMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaire.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialQuestionnaire entity)
			{
				this.entity = entity;
			}
			public System.String QuestionnaireID
			{
				get
				{
					System.Int32? data = entity.QuestionnaireID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionnaireID = null;
					else entity.QuestionnaireID = Convert.ToInt32(value);
				}
			}
			public System.String QuestionnaireCode
			{
				get
				{
					System.String data = entity.QuestionnaireCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionnaireCode = null;
					else entity.QuestionnaireCode = Convert.ToString(value);
				}
			}
			public System.String QuestionnaireName
			{
				get
				{
					System.String data = entity.QuestionnaireName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionnaireName = null;
					else entity.QuestionnaireName = Convert.ToString(value);
				}
			}
			public System.String SRProfessionGroup
			{
				get
				{
					System.String data = entity.SRProfessionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProfessionGroup = null;
					else entity.SRProfessionGroup = Convert.ToString(value);
				}
			}
			public System.String SRClinicalWorkArea
			{
				get
				{
					System.String data = entity.SRClinicalWorkArea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalWorkArea = null;
					else entity.SRClinicalWorkArea = Convert.ToString(value);
				}
			}
			public System.String SRClinicalAuthorityLevel
			{
				get
				{
					System.String data = entity.SRClinicalAuthorityLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalAuthorityLevel = null;
					else entity.SRClinicalAuthorityLevel = Convert.ToString(value);
				}
			}
			public System.String Info1
			{
				get
				{
					System.String data = entity.Info1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info1 = null;
					else entity.Info1 = Convert.ToString(value);
				}
			}
			public System.String Info2
			{
				get
				{
					System.String data = entity.Info2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info2 = null;
					else entity.Info2 = Convert.ToString(value);
				}
			}
			public System.String Info3
			{
				get
				{
					System.String data = entity.Info3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info3 = null;
					else entity.Info3 = Convert.ToString(value);
				}
			}
			public System.String Info4
			{
				get
				{
					System.String data = entity.Info4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info4 = null;
					else entity.Info4 = Convert.ToString(value);
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
			private esCredentialQuestionnaire entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialQuestionnaireQuery query)
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
				throw new Exception("esCredentialQuestionnaire can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialQuestionnaire : esCredentialQuestionnaire
	{
	}

	[Serializable]
	abstract public class esCredentialQuestionnaireQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialQuestionnaireMetadata.Meta();
			}
		}

		public esQueryItem QuestionnaireID
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionnaireCode
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireCode, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireName
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireName, esSystemType.String);
			}
		}

		public esQueryItem SRProfessionGroup
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.SRProfessionGroup, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalWorkArea
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalAuthorityLevel
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.SRClinicalAuthorityLevel, esSystemType.String);
			}
		}

		public esQueryItem Info1
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.Info1, esSystemType.String);
			}
		}

		public esQueryItem Info2
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.Info2, esSystemType.String);
			}
		}

		public esQueryItem Info3
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.Info3, esSystemType.String);
			}
		}

		public esQueryItem Info4
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.Info4, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialQuestionnaireCollection")]
	public partial class CredentialQuestionnaireCollection : esCredentialQuestionnaireCollection, IEnumerable<CredentialQuestionnaire>
	{
		public CredentialQuestionnaireCollection()
		{

		}

		public static implicit operator List<CredentialQuestionnaire>(CredentialQuestionnaireCollection coll)
		{
			List<CredentialQuestionnaire> list = new List<CredentialQuestionnaire>();

			foreach (CredentialQuestionnaire emp in coll)
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
				return CredentialQuestionnaireMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialQuestionnaireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialQuestionnaire(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialQuestionnaire();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialQuestionnaireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialQuestionnaireQuery();
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
		public bool Load(CredentialQuestionnaireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialQuestionnaire AddNew()
		{
			CredentialQuestionnaire entity = base.AddNewEntity() as CredentialQuestionnaire;

			return entity;
		}
		public CredentialQuestionnaire FindByPrimaryKey(Int32 questionnaireID)
		{
			return base.FindByPrimaryKey(questionnaireID) as CredentialQuestionnaire;
		}

		#region IEnumerable< CredentialQuestionnaire> Members

		IEnumerator<CredentialQuestionnaire> IEnumerable<CredentialQuestionnaire>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialQuestionnaire;
			}
		}

		#endregion

		private CredentialQuestionnaireQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialQuestionnaire' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialQuestionnaire ({QuestionnaireID})")]
	[Serializable]
	public partial class CredentialQuestionnaire : esCredentialQuestionnaire
	{
		public CredentialQuestionnaire()
		{
		}

		public CredentialQuestionnaire(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialQuestionnaireMetadata.Meta();
			}
		}

		override protected esCredentialQuestionnaireQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialQuestionnaireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialQuestionnaireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialQuestionnaireQuery();
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
		public bool Load(CredentialQuestionnaireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialQuestionnaireQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialQuestionnaireQuery : esCredentialQuestionnaireQuery
	{
		public CredentialQuestionnaireQuery()
		{

		}

		public CredentialQuestionnaireQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialQuestionnaireQuery";
		}
	}

	[Serializable]
	public partial class CredentialQuestionnaireMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialQuestionnaireMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.QuestionnaireID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.QuestionnaireCode;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.QuestionnaireName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.QuestionnaireName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.SRProfessionGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.SRProfessionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.SRClinicalWorkArea;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.SRClinicalAuthorityLevel, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.SRClinicalAuthorityLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.Info1, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.Info1;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.Info2, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.Info2;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.Info3, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.Info3;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.Info4, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.Info4;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.IsActive, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.CreatedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.CreatedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialQuestionnaireMetadata Meta()
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
			public const string QuestionnaireID = "QuestionnaireID";
			public const string QuestionnaireCode = "QuestionnaireCode";
			public const string QuestionnaireName = "QuestionnaireName";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string Info1 = "Info1";
			public const string Info2 = "Info2";
			public const string Info3 = "Info3";
			public const string Info4 = "Info4";
			public const string IsActive = "IsActive";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string QuestionnaireID = "QuestionnaireID";
			public const string QuestionnaireCode = "QuestionnaireCode";
			public const string QuestionnaireName = "QuestionnaireName";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string Info1 = "Info1";
			public const string Info2 = "Info2";
			public const string Info3 = "Info3";
			public const string Info4 = "Info4";
			public const string IsActive = "IsActive";
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
			lock (typeof(CredentialQuestionnaireMetadata))
			{
				if (CredentialQuestionnaireMetadata.mapDelegates == null)
				{
					CredentialQuestionnaireMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialQuestionnaireMetadata.meta == null)
				{
					CredentialQuestionnaireMetadata.meta = new CredentialQuestionnaireMetadata();
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

				meta.AddTypeMap("QuestionnaireID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionnaireCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionnaireName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProfessionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalWorkArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalAuthorityLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialQuestionnaire";
				meta.Destination = "CredentialQuestionnaire";
				meta.spInsert = "proc_CredentialQuestionnaireInsert";
				meta.spUpdate = "proc_CredentialQuestionnaireUpdate";
				meta.spDelete = "proc_CredentialQuestionnaireDelete";
				meta.spLoadAll = "proc_CredentialQuestionnaireLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialQuestionnaireLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialQuestionnaireMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
