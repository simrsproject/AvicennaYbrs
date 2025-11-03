/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:31:47 PM
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
	abstract public class esCredentialObservationInstrumentQuestionnaireCollection : esEntityCollectionWAuditLog
	{
		public esCredentialObservationInstrumentQuestionnaireCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialObservationInstrumentQuestionnaireCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialObservationInstrumentQuestionnaireQuery query)
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
			this.InitQuery(query as esCredentialObservationInstrumentQuestionnaireQuery);
		}
		#endregion

		virtual public CredentialObservationInstrumentQuestionnaire DetachEntity(CredentialObservationInstrumentQuestionnaire entity)
		{
			return base.DetachEntity(entity) as CredentialObservationInstrumentQuestionnaire;
		}

		virtual public CredentialObservationInstrumentQuestionnaire AttachEntity(CredentialObservationInstrumentQuestionnaire entity)
		{
			return base.AttachEntity(entity) as CredentialObservationInstrumentQuestionnaire;
		}

		virtual public void Combine(CredentialObservationInstrumentQuestionnaireCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialObservationInstrumentQuestionnaire this[int index]
		{
			get
			{
				return base[index] as CredentialObservationInstrumentQuestionnaire;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialObservationInstrumentQuestionnaire);
		}
	}

	[Serializable]
	abstract public class esCredentialObservationInstrumentQuestionnaire : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialObservationInstrumentQuestionnaireQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialObservationInstrumentQuestionnaire()
		{
		}

		public esCredentialObservationInstrumentQuestionnaire(DataRow row)
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
			esCredentialObservationInstrumentQuestionnaireQuery query = this.GetDynamicQuery();
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
						case "SRClinicalWorkArea": this.str.SRClinicalWorkArea = (string)value; break;
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
		/// Maps to CredentialObservationInstrumentQuestionnaire.QuestionnaireID
		/// </summary>
		virtual public System.Int32? QuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.QuestionnaireCode
		/// </summary>
		virtual public System.String QuestionnaireCode
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireCode);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireCode, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.QuestionnaireName
		/// </summary>
		virtual public System.String QuestionnaireName
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireName);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.SRClinicalWorkArea
		/// </summary>
		virtual public System.String SRClinicalWorkArea
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentQuestionnaire.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialObservationInstrumentQuestionnaire entity)
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
			private esCredentialObservationInstrumentQuestionnaire entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialObservationInstrumentQuestionnaireQuery query)
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
				throw new Exception("esCredentialObservationInstrumentQuestionnaire can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialObservationInstrumentQuestionnaire : esCredentialObservationInstrumentQuestionnaire
	{
	}

	[Serializable]
	abstract public class esCredentialObservationInstrumentQuestionnaireQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialObservationInstrumentQuestionnaireMetadata.Meta();
			}
		}

		public esQueryItem QuestionnaireID
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionnaireCode
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireCode, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireName
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireName, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalWorkArea
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialObservationInstrumentQuestionnaireCollection")]
	public partial class CredentialObservationInstrumentQuestionnaireCollection : esCredentialObservationInstrumentQuestionnaireCollection, IEnumerable<CredentialObservationInstrumentQuestionnaire>
	{
		public CredentialObservationInstrumentQuestionnaireCollection()
		{

		}

		public static implicit operator List<CredentialObservationInstrumentQuestionnaire>(CredentialObservationInstrumentQuestionnaireCollection coll)
		{
			List<CredentialObservationInstrumentQuestionnaire> list = new List<CredentialObservationInstrumentQuestionnaire>();

			foreach (CredentialObservationInstrumentQuestionnaire emp in coll)
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
				return CredentialObservationInstrumentQuestionnaireMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialObservationInstrumentQuestionnaireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialObservationInstrumentQuestionnaire(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialObservationInstrumentQuestionnaire();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialObservationInstrumentQuestionnaireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialObservationInstrumentQuestionnaireQuery();
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
		public bool Load(CredentialObservationInstrumentQuestionnaireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialObservationInstrumentQuestionnaire AddNew()
		{
			CredentialObservationInstrumentQuestionnaire entity = base.AddNewEntity() as CredentialObservationInstrumentQuestionnaire;

			return entity;
		}
		public CredentialObservationInstrumentQuestionnaire FindByPrimaryKey(Int32 questionnaireID)
		{
			return base.FindByPrimaryKey(questionnaireID) as CredentialObservationInstrumentQuestionnaire;
		}

		#region IEnumerable< CredentialObservationInstrumentQuestionnaire> Members

		IEnumerator<CredentialObservationInstrumentQuestionnaire> IEnumerable<CredentialObservationInstrumentQuestionnaire>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialObservationInstrumentQuestionnaire;
			}
		}

		#endregion

		private CredentialObservationInstrumentQuestionnaireQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialObservationInstrumentQuestionnaire' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialObservationInstrumentQuestionnaire ({QuestionnaireID})")]
	[Serializable]
	public partial class CredentialObservationInstrumentQuestionnaire : esCredentialObservationInstrumentQuestionnaire
	{
		public CredentialObservationInstrumentQuestionnaire()
		{
		}

		public CredentialObservationInstrumentQuestionnaire(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialObservationInstrumentQuestionnaireMetadata.Meta();
			}
		}

		override protected esCredentialObservationInstrumentQuestionnaireQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialObservationInstrumentQuestionnaireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialObservationInstrumentQuestionnaireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialObservationInstrumentQuestionnaireQuery();
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
		public bool Load(CredentialObservationInstrumentQuestionnaireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialObservationInstrumentQuestionnaireQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialObservationInstrumentQuestionnaireQuery : esCredentialObservationInstrumentQuestionnaireQuery
	{
		public CredentialObservationInstrumentQuestionnaireQuery()
		{

		}

		public CredentialObservationInstrumentQuestionnaireQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialObservationInstrumentQuestionnaireQuery";
		}
	}

	[Serializable]
	public partial class CredentialObservationInstrumentQuestionnaireMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialObservationInstrumentQuestionnaireMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.QuestionnaireID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.QuestionnaireCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.QuestionnaireName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.QuestionnaireName;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.SRClinicalWorkArea, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.SRClinicalWorkArea;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentQuestionnaireMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialObservationInstrumentQuestionnaireMetadata Meta()
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
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
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
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
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
			lock (typeof(CredentialObservationInstrumentQuestionnaireMetadata))
			{
				if (CredentialObservationInstrumentQuestionnaireMetadata.mapDelegates == null)
				{
					CredentialObservationInstrumentQuestionnaireMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialObservationInstrumentQuestionnaireMetadata.meta == null)
				{
					CredentialObservationInstrumentQuestionnaireMetadata.meta = new CredentialObservationInstrumentQuestionnaireMetadata();
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
				meta.AddTypeMap("SRClinicalWorkArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialObservationInstrumentQuestionnaire";
				meta.Destination = "CredentialObservationInstrumentQuestionnaire";
				meta.spInsert = "proc_CredentialObservationInstrumentQuestionnaireInsert";
				meta.spUpdate = "proc_CredentialObservationInstrumentQuestionnaireUpdate";
				meta.spDelete = "proc_CredentialObservationInstrumentQuestionnaireDelete";
				meta.spLoadAll = "proc_CredentialObservationInstrumentQuestionnaireLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialObservationInstrumentQuestionnaireLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialObservationInstrumentQuestionnaireMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
