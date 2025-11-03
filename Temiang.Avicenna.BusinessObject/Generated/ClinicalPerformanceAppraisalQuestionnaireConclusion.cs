/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/27/2022 4:41:59 PM
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
	abstract public class esClinicalPerformanceAppraisalQuestionnaireConclusionCollection : esEntityCollectionWAuditLog
	{
		public esClinicalPerformanceAppraisalQuestionnaireConclusionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireConclusionCollection";
		}

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireConclusionQuery query)
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
			this.InitQuery(query as esClinicalPerformanceAppraisalQuestionnaireConclusionQuery);
		}
		#endregion

		virtual public ClinicalPerformanceAppraisalQuestionnaireConclusion DetachEntity(ClinicalPerformanceAppraisalQuestionnaireConclusion entity)
		{
			return base.DetachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaireConclusion;
		}

		virtual public ClinicalPerformanceAppraisalQuestionnaireConclusion AttachEntity(ClinicalPerformanceAppraisalQuestionnaireConclusion entity)
		{
			return base.AttachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaireConclusion;
		}

		virtual public void Combine(ClinicalPerformanceAppraisalQuestionnaireConclusionCollection collection)
		{
			base.Combine(collection);
		}

		new public ClinicalPerformanceAppraisalQuestionnaireConclusion this[int index]
		{
			get
			{
				return base[index] as ClinicalPerformanceAppraisalQuestionnaireConclusion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClinicalPerformanceAppraisalQuestionnaireConclusion);
		}
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaireConclusion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClinicalPerformanceAppraisalQuestionnaireConclusionQuery GetDynamicQuery()
		{
			return null;
		}

		public esClinicalPerformanceAppraisalQuestionnaireConclusion()
		{
		}

		public esClinicalPerformanceAppraisalQuestionnaireConclusion(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 conclusionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(conclusionID);
			else
				return LoadByPrimaryKeyStoredProcedure(conclusionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 conclusionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(conclusionID);
			else
				return LoadByPrimaryKeyStoredProcedure(conclusionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 conclusionID)
		{
			esClinicalPerformanceAppraisalQuestionnaireConclusionQuery query = this.GetDynamicQuery();
			query.Where(query.ConclusionID == conclusionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 conclusionID)
		{
			esParameters parms = new esParameters();
			parms.Add("ConclusionID", conclusionID);
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
						case "ConclusionID": this.str.ConclusionID = (string)value; break;
						case "QuestionnaireID": this.str.QuestionnaireID = (string)value; break;
						case "ConclusionGrade": this.str.ConclusionGrade = (string)value; break;
						case "ConclusionGradeName": this.str.ConclusionGradeName = (string)value; break;
						case "MinValue": this.str.MinValue = (string)value; break;
						case "MaxValue": this.str.MaxValue = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ConclusionID":

							if (value == null || value is System.Int32)
								this.ConclusionID = (System.Int32?)value;
							break;
						case "QuestionnaireID":

							if (value == null || value is System.Int32)
								this.QuestionnaireID = (System.Int32?)value;
							break;
						case "MinValue":

							if (value == null || value is System.Decimal)
								this.MinValue = (System.Decimal?)value;
							break;
						case "MaxValue":

							if (value == null || value is System.Decimal)
								this.MaxValue = (System.Decimal?)value;
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
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.ConclusionID
		/// </summary>
		virtual public System.Int32? ConclusionID
		{
			get
			{
				return base.GetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionID);
			}

			set
			{
				base.SetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.QuestionnaireID
		/// </summary>
		virtual public System.Int32? QuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.QuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.QuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.ConclusionGrade
		/// </summary>
		virtual public System.String ConclusionGrade
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGrade);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGrade, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.ConclusionGradeName
		/// </summary>
		virtual public System.String ConclusionGradeName
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGradeName);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGradeName, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.MinValue
		/// </summary>
		virtual public System.Decimal? MinValue
		{
			get
			{
				return base.GetSystemDecimal(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MinValue);
			}

			set
			{
				base.SetSystemDecimal(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MinValue, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.MaxValue
		/// </summary>
		virtual public System.Decimal? MaxValue
		{
			get
			{
				return base.GetSystemDecimal(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MaxValue);
			}

			set
			{
				base.SetSystemDecimal(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MaxValue, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireConclusion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClinicalPerformanceAppraisalQuestionnaireConclusion entity)
			{
				this.entity = entity;
			}
			public System.String ConclusionID
			{
				get
				{
					System.Int32? data = entity.ConclusionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionID = null;
					else entity.ConclusionID = Convert.ToInt32(value);
				}
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
			public System.String ConclusionGrade
			{
				get
				{
					System.String data = entity.ConclusionGrade;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionGrade = null;
					else entity.ConclusionGrade = Convert.ToString(value);
				}
			}
			public System.String ConclusionGradeName
			{
				get
				{
					System.String data = entity.ConclusionGradeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionGradeName = null;
					else entity.ConclusionGradeName = Convert.ToString(value);
				}
			}
			public System.String MinValue
			{
				get
				{
					System.Decimal? data = entity.MinValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinValue = null;
					else entity.MinValue = Convert.ToDecimal(value);
				}
			}
			public System.String MaxValue
			{
				get
				{
					System.Decimal? data = entity.MaxValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxValue = null;
					else entity.MaxValue = Convert.ToDecimal(value);
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
			private esClinicalPerformanceAppraisalQuestionnaireConclusion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireConclusionQuery query)
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
				throw new Exception("esClinicalPerformanceAppraisalQuestionnaireConclusion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ClinicalPerformanceAppraisalQuestionnaireConclusion : esClinicalPerformanceAppraisalQuestionnaireConclusion
	{
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaireConclusionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.Meta();
			}
		}

		public esQueryItem ConclusionID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionnaireID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.QuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem ConclusionGrade
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGrade, esSystemType.String);
			}
		}

		public esQueryItem ConclusionGradeName
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGradeName, esSystemType.String);
			}
		}

		public esQueryItem MinValue
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MinValue, esSystemType.Decimal);
			}
		}

		public esQueryItem MaxValue
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MaxValue, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClinicalPerformanceAppraisalQuestionnaireConclusionCollection")]
	public partial class ClinicalPerformanceAppraisalQuestionnaireConclusionCollection : esClinicalPerformanceAppraisalQuestionnaireConclusionCollection, IEnumerable<ClinicalPerformanceAppraisalQuestionnaireConclusion>
	{
		public ClinicalPerformanceAppraisalQuestionnaireConclusionCollection()
		{

		}

		public static implicit operator List<ClinicalPerformanceAppraisalQuestionnaireConclusion>(ClinicalPerformanceAppraisalQuestionnaireConclusionCollection coll)
		{
			List<ClinicalPerformanceAppraisalQuestionnaireConclusion> list = new List<ClinicalPerformanceAppraisalQuestionnaireConclusion>();

			foreach (ClinicalPerformanceAppraisalQuestionnaireConclusion emp in coll)
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
				return ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireConclusionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClinicalPerformanceAppraisalQuestionnaireConclusion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClinicalPerformanceAppraisalQuestionnaireConclusion();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireConclusionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireConclusionQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireConclusionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ClinicalPerformanceAppraisalQuestionnaireConclusion AddNew()
		{
			ClinicalPerformanceAppraisalQuestionnaireConclusion entity = base.AddNewEntity() as ClinicalPerformanceAppraisalQuestionnaireConclusion;

			return entity;
		}
		public ClinicalPerformanceAppraisalQuestionnaireConclusion FindByPrimaryKey(Int32 conclusionID)
		{
			return base.FindByPrimaryKey(conclusionID) as ClinicalPerformanceAppraisalQuestionnaireConclusion;
		}

		#region IEnumerable< ClinicalPerformanceAppraisalQuestionnaireConclusion> Members

		IEnumerator<ClinicalPerformanceAppraisalQuestionnaireConclusion> IEnumerable<ClinicalPerformanceAppraisalQuestionnaireConclusion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ClinicalPerformanceAppraisalQuestionnaireConclusion;
			}
		}

		#endregion

		private ClinicalPerformanceAppraisalQuestionnaireConclusionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClinicalPerformanceAppraisalQuestionnaireConclusion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ClinicalPerformanceAppraisalQuestionnaireConclusion ({ConclusionID})")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireConclusion : esClinicalPerformanceAppraisalQuestionnaireConclusion
	{
		public ClinicalPerformanceAppraisalQuestionnaireConclusion()
		{
		}

		public ClinicalPerformanceAppraisalQuestionnaireConclusion(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.Meta();
			}
		}

		override protected esClinicalPerformanceAppraisalQuestionnaireConclusionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireConclusionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireConclusionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireConclusionQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireConclusionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ClinicalPerformanceAppraisalQuestionnaireConclusionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireConclusionQuery : esClinicalPerformanceAppraisalQuestionnaireConclusionQuery
	{
		public ClinicalPerformanceAppraisalQuestionnaireConclusionQuery()
		{

		}

		public ClinicalPerformanceAppraisalQuestionnaireConclusionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireConclusionQuery";
		}
	}

	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.ConclusionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.QuestionnaireID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.QuestionnaireID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGrade, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.ConclusionGrade;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.ConclusionGradeName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.ConclusionGradeName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MinValue, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.MinValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.MaxValue, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.MaxValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata Meta()
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
			public const string ConclusionID = "ConclusionID";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string ConclusionGrade = "ConclusionGrade";
			public const string ConclusionGradeName = "ConclusionGradeName";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ConclusionID = "ConclusionID";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string ConclusionGrade = "ConclusionGrade";
			public const string ConclusionGradeName = "ConclusionGradeName";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
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
			lock (typeof(ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata))
			{
				if (ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.mapDelegates == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.meta == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata.meta = new ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata();
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

				meta.AddTypeMap("ConclusionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionnaireID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ConclusionGrade", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConclusionGradeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ClinicalPerformanceAppraisalQuestionnaireConclusion";
				meta.Destination = "ClinicalPerformanceAppraisalQuestionnaireConclusion";
				meta.spInsert = "proc_ClinicalPerformanceAppraisalQuestionnaireConclusionInsert";
				meta.spUpdate = "proc_ClinicalPerformanceAppraisalQuestionnaireConclusionUpdate";
				meta.spDelete = "proc_ClinicalPerformanceAppraisalQuestionnaireConclusionDelete";
				meta.spLoadAll = "proc_ClinicalPerformanceAppraisalQuestionnaireConclusionLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClinicalPerformanceAppraisalQuestionnaireConclusionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClinicalPerformanceAppraisalQuestionnaireConclusionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
