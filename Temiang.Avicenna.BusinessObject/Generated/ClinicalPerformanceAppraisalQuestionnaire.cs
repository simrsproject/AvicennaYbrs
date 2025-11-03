/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/27/2022 4:06:57 PM
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
	abstract public class esClinicalPerformanceAppraisalQuestionnaireCollection : esEntityCollectionWAuditLog
	{
		public esClinicalPerformanceAppraisalQuestionnaireCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireCollection";
		}

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireQuery query)
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
			this.InitQuery(query as esClinicalPerformanceAppraisalQuestionnaireQuery);
		}
		#endregion

		virtual public ClinicalPerformanceAppraisalQuestionnaire DetachEntity(ClinicalPerformanceAppraisalQuestionnaire entity)
		{
			return base.DetachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaire;
		}

		virtual public ClinicalPerformanceAppraisalQuestionnaire AttachEntity(ClinicalPerformanceAppraisalQuestionnaire entity)
		{
			return base.AttachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaire;
		}

		virtual public void Combine(ClinicalPerformanceAppraisalQuestionnaireCollection collection)
		{
			base.Combine(collection);
		}

		new public ClinicalPerformanceAppraisalQuestionnaire this[int index]
		{
			get
			{
				return base[index] as ClinicalPerformanceAppraisalQuestionnaire;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClinicalPerformanceAppraisalQuestionnaire);
		}
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaire : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClinicalPerformanceAppraisalQuestionnaireQuery GetDynamicQuery()
		{
			return null;
		}

		public esClinicalPerformanceAppraisalQuestionnaire()
		{
		}

		public esClinicalPerformanceAppraisalQuestionnaire(DataRow row)
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
			esClinicalPerformanceAppraisalQuestionnaireQuery query = this.GetDynamicQuery();
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
						case "MinValue": this.str.MinValue = (string)value; break;
						case "MaxValue": this.str.MaxValue = (string)value; break;
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
						case "MinValue":

							if (value == null || value is System.Int16)
								this.MinValue = (System.Int16?)value;
							break;
						case "MaxValue":

							if (value == null || value is System.Int16)
								this.MaxValue = (System.Int16?)value;
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
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.QuestionnaireID
		/// </summary>
		virtual public System.Int32? QuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.QuestionnaireCode
		/// </summary>
		virtual public System.String QuestionnaireCode
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireCode);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireCode, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.QuestionnaireName
		/// </summary>
		virtual public System.String QuestionnaireName
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireName);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireName, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.MinValue
		/// </summary>
		virtual public System.Int16? MinValue
		{
			get
			{
				return base.GetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MinValue);
			}

			set
			{
				base.SetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MinValue, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.MaxValue
		/// </summary>
		virtual public System.Int16? MaxValue
		{
			get
			{
				return base.GetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MaxValue);
			}

			set
			{
				base.SetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MaxValue, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaire.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClinicalPerformanceAppraisalQuestionnaire entity)
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
			public System.String MinValue
			{
				get
				{
					System.Int16? data = entity.MinValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinValue = null;
					else entity.MinValue = Convert.ToInt16(value);
				}
			}
			public System.String MaxValue
			{
				get
				{
					System.Int16? data = entity.MaxValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxValue = null;
					else entity.MaxValue = Convert.ToInt16(value);
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
			private esClinicalPerformanceAppraisalQuestionnaire entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireQuery query)
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
				throw new Exception("esClinicalPerformanceAppraisalQuestionnaire can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ClinicalPerformanceAppraisalQuestionnaire : esClinicalPerformanceAppraisalQuestionnaire
	{
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaireQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireMetadata.Meta();
			}
		}

		public esQueryItem QuestionnaireID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionnaireCode
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireCode, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireName
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireName, esSystemType.String);
			}
		}

		public esQueryItem MinValue
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MinValue, esSystemType.Int16);
			}
		}

		public esQueryItem MaxValue
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MaxValue, esSystemType.Int16);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClinicalPerformanceAppraisalQuestionnaireCollection")]
	public partial class ClinicalPerformanceAppraisalQuestionnaireCollection : esClinicalPerformanceAppraisalQuestionnaireCollection, IEnumerable<ClinicalPerformanceAppraisalQuestionnaire>
	{
		public ClinicalPerformanceAppraisalQuestionnaireCollection()
		{

		}

		public static implicit operator List<ClinicalPerformanceAppraisalQuestionnaire>(ClinicalPerformanceAppraisalQuestionnaireCollection coll)
		{
			List<ClinicalPerformanceAppraisalQuestionnaire> list = new List<ClinicalPerformanceAppraisalQuestionnaire>();

			foreach (ClinicalPerformanceAppraisalQuestionnaire emp in coll)
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
				return ClinicalPerformanceAppraisalQuestionnaireMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClinicalPerformanceAppraisalQuestionnaire(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClinicalPerformanceAppraisalQuestionnaire();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ClinicalPerformanceAppraisalQuestionnaire AddNew()
		{
			ClinicalPerformanceAppraisalQuestionnaire entity = base.AddNewEntity() as ClinicalPerformanceAppraisalQuestionnaire;

			return entity;
		}
		public ClinicalPerformanceAppraisalQuestionnaire FindByPrimaryKey(Int32 questionnaireID)
		{
			return base.FindByPrimaryKey(questionnaireID) as ClinicalPerformanceAppraisalQuestionnaire;
		}

		#region IEnumerable< ClinicalPerformanceAppraisalQuestionnaire> Members

		IEnumerator<ClinicalPerformanceAppraisalQuestionnaire> IEnumerable<ClinicalPerformanceAppraisalQuestionnaire>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ClinicalPerformanceAppraisalQuestionnaire;
			}
		}

		#endregion

		private ClinicalPerformanceAppraisalQuestionnaireQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClinicalPerformanceAppraisalQuestionnaire' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ClinicalPerformanceAppraisalQuestionnaire ({QuestionnaireID})")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaire : esClinicalPerformanceAppraisalQuestionnaire
	{
		public ClinicalPerformanceAppraisalQuestionnaire()
		{
		}

		public ClinicalPerformanceAppraisalQuestionnaire(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireMetadata.Meta();
			}
		}

		override protected esClinicalPerformanceAppraisalQuestionnaireQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ClinicalPerformanceAppraisalQuestionnaireQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireQuery : esClinicalPerformanceAppraisalQuestionnaireQuery
	{
		public ClinicalPerformanceAppraisalQuestionnaireQuery()
		{

		}

		public ClinicalPerformanceAppraisalQuestionnaireQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireQuery";
		}
	}

	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClinicalPerformanceAppraisalQuestionnaireMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.QuestionnaireID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.QuestionnaireCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.QuestionnaireName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.QuestionnaireName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MinValue, 3, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.MinValue;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.MaxValue, 4, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.MaxValue;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ClinicalPerformanceAppraisalQuestionnaireMetadata Meta()
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
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
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
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
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
			lock (typeof(ClinicalPerformanceAppraisalQuestionnaireMetadata))
			{
				if (ClinicalPerformanceAppraisalQuestionnaireMetadata.mapDelegates == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ClinicalPerformanceAppraisalQuestionnaireMetadata.meta == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireMetadata.meta = new ClinicalPerformanceAppraisalQuestionnaireMetadata();
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
				meta.AddTypeMap("MinValue", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("MaxValue", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ClinicalPerformanceAppraisalQuestionnaire";
				meta.Destination = "ClinicalPerformanceAppraisalQuestionnaire";
				meta.spInsert = "proc_ClinicalPerformanceAppraisalQuestionnaireInsert";
				meta.spUpdate = "proc_ClinicalPerformanceAppraisalQuestionnaireUpdate";
				meta.spDelete = "proc_ClinicalPerformanceAppraisalQuestionnaireDelete";
				meta.spLoadAll = "proc_ClinicalPerformanceAppraisalQuestionnaireLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClinicalPerformanceAppraisalQuestionnaireLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClinicalPerformanceAppraisalQuestionnaireMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
