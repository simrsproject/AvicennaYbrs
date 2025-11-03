/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2022 3:41:09 PM
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
	abstract public class esAppraisalQuestionCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalQuestionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalQuestionCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalQuestionQuery query)
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
			this.InitQuery(query as esAppraisalQuestionQuery);
		}
		#endregion

		virtual public AppraisalQuestion DetachEntity(AppraisalQuestion entity)
		{
			return base.DetachEntity(entity) as AppraisalQuestion;
		}

		virtual public AppraisalQuestion AttachEntity(AppraisalQuestion entity)
		{
			return base.AttachEntity(entity) as AppraisalQuestion;
		}

		virtual public void Combine(AppraisalQuestionCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalQuestion this[int index]
		{
			get
			{
				return base[index] as AppraisalQuestion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalQuestion);
		}
	}

	[Serializable]
	abstract public class esAppraisalQuestion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalQuestionQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalQuestion()
		{
		}

		public esAppraisalQuestion(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 questionerID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionerID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionerID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 questionerID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionerID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionerID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 questionerID)
		{
			esAppraisalQuestionQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionerID == questionerID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 questionerID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionerID", questionerID);
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
						case "QuestionerID": this.str.QuestionerID = (string)value; break;
						case "QuestionerNo": this.str.QuestionerNo = (string)value; break;
						case "QuestionerName": this.str.QuestionerName = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "LoadScore": this.str.LoadScore = (string)value; break;
						case "SRAppraisalType": this.str.SRAppraisalType = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsScoringRecapitulation": this.str.IsScoringRecapitulation = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "QuestionerID":

							if (value == null || value is System.Int32)
								this.QuestionerID = (System.Int32?)value;
							break;
						case "LoadScore":

							if (value == null || value is System.Decimal)
								this.LoadScore = (System.Decimal?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsScoringRecapitulation":

							if (value == null || value is System.Boolean)
								this.IsScoringRecapitulation = (System.Boolean?)value;
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
		/// Maps to AppraisalQuestion.QuestionerID
		/// </summary>
		virtual public System.Int32? QuestionerID
		{
			get
			{
				return base.GetSystemInt32(AppraisalQuestionMetadata.ColumnNames.QuestionerID);
			}

			set
			{
				base.SetSystemInt32(AppraisalQuestionMetadata.ColumnNames.QuestionerID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.QuestionerNo
		/// </summary>
		virtual public System.String QuestionerNo
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionMetadata.ColumnNames.QuestionerNo);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionMetadata.ColumnNames.QuestionerNo, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.QuestionerName
		/// </summary>
		virtual public System.String QuestionerName
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionMetadata.ColumnNames.QuestionerName);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionMetadata.ColumnNames.QuestionerName, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionMetadata.ColumnNames.PeriodYear);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.LoadScore
		/// </summary>
		virtual public System.Decimal? LoadScore
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionMetadata.ColumnNames.LoadScore);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionMetadata.ColumnNames.LoadScore, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.SRAppraisalType
		/// </summary>
		virtual public System.String SRAppraisalType
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionMetadata.ColumnNames.SRAppraisalType);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionMetadata.ColumnNames.SRAppraisalType, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(AppraisalQuestionMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(AppraisalQuestionMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestion.IsScoringRecapitulation
		/// </summary>
		virtual public System.Boolean? IsScoringRecapitulation
		{
			get
			{
				return base.GetSystemBoolean(AppraisalQuestionMetadata.ColumnNames.IsScoringRecapitulation);
			}

			set
			{
				base.SetSystemBoolean(AppraisalQuestionMetadata.ColumnNames.IsScoringRecapitulation, value);
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
			public esStrings(esAppraisalQuestion entity)
			{
				this.entity = entity;
			}
			public System.String QuestionerID
			{
				get
				{
					System.Int32? data = entity.QuestionerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionerID = null;
					else entity.QuestionerID = Convert.ToInt32(value);
				}
			}
			public System.String QuestionerNo
			{
				get
				{
					System.String data = entity.QuestionerNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionerNo = null;
					else entity.QuestionerNo = Convert.ToString(value);
				}
			}
			public System.String QuestionerName
			{
				get
				{
					System.String data = entity.QuestionerName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionerName = null;
					else entity.QuestionerName = Convert.ToString(value);
				}
			}
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
				}
			}
			public System.String LoadScore
			{
				get
				{
					System.Decimal? data = entity.LoadScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LoadScore = null;
					else entity.LoadScore = Convert.ToDecimal(value);
				}
			}
			public System.String SRAppraisalType
			{
				get
				{
					System.String data = entity.SRAppraisalType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAppraisalType = null;
					else entity.SRAppraisalType = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			public System.String IsScoringRecapitulation
			{
				get
				{
					System.Boolean? data = entity.IsScoringRecapitulation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScoringRecapitulation = null;
					else entity.IsScoringRecapitulation = Convert.ToBoolean(value);
				}
			}
			private esAppraisalQuestion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalQuestionQuery query)
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
				throw new Exception("esAppraisalQuestion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalQuestion : esAppraisalQuestion
	{
	}

	[Serializable]
	abstract public class esAppraisalQuestionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalQuestionMetadata.Meta();
			}
		}

		public esQueryItem QuestionerID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.QuestionerID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionerNo
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.QuestionerNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionerName
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.QuestionerName, esSystemType.String);
			}
		}

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		}

		public esQueryItem LoadScore
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.LoadScore, esSystemType.Decimal);
			}
		}

		public esQueryItem SRAppraisalType
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.SRAppraisalType, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsScoringRecapitulation
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionMetadata.ColumnNames.IsScoringRecapitulation, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalQuestionCollection")]
	public partial class AppraisalQuestionCollection : esAppraisalQuestionCollection, IEnumerable<AppraisalQuestion>
	{
		public AppraisalQuestionCollection()
		{

		}

		public static implicit operator List<AppraisalQuestion>(AppraisalQuestionCollection coll)
		{
			List<AppraisalQuestion> list = new List<AppraisalQuestion>();

			foreach (AppraisalQuestion emp in coll)
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
				return AppraisalQuestionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalQuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalQuestion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalQuestion();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalQuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalQuestionQuery();
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
		public bool Load(AppraisalQuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalQuestion AddNew()
		{
			AppraisalQuestion entity = base.AddNewEntity() as AppraisalQuestion;

			return entity;
		}
		public AppraisalQuestion FindByPrimaryKey(Int32 questionerID)
		{
			return base.FindByPrimaryKey(questionerID) as AppraisalQuestion;
		}

		#region IEnumerable< AppraisalQuestion> Members

		IEnumerator<AppraisalQuestion> IEnumerable<AppraisalQuestion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalQuestion;
			}
		}

		#endregion

		private AppraisalQuestionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalQuestion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalQuestion ({QuestionerID})")]
	[Serializable]
	public partial class AppraisalQuestion : esAppraisalQuestion
	{
		public AppraisalQuestion()
		{
		}

		public AppraisalQuestion(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalQuestionMetadata.Meta();
			}
		}

		override protected esAppraisalQuestionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalQuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalQuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalQuestionQuery();
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
		public bool Load(AppraisalQuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalQuestionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalQuestionQuery : esAppraisalQuestionQuery
	{
		public AppraisalQuestionQuery()
		{

		}

		public AppraisalQuestionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalQuestionQuery";
		}
	}

	[Serializable]
	public partial class AppraisalQuestionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalQuestionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.QuestionerID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.QuestionerID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.QuestionerNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.QuestionerNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.QuestionerName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.QuestionerName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.PeriodYear, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.PeriodYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.LoadScore, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.LoadScore;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.SRAppraisalType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.SRAppraisalType;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionMetadata.ColumnNames.IsScoringRecapitulation, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppraisalQuestionMetadata.PropertyNames.IsScoringRecapitulation;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalQuestionMetadata Meta()
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
			public const string QuestionerID = "QuestionerID";
			public const string QuestionerNo = "QuestionerNo";
			public const string QuestionerName = "QuestionerName";
			public const string PeriodYear = "PeriodYear";
			public const string LoadScore = "LoadScore";
			public const string SRAppraisalType = "SRAppraisalType";
			public const string Notes = "Notes";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsScoringRecapitulation = "IsScoringRecapitulation";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string QuestionerID = "QuestionerID";
			public const string QuestionerNo = "QuestionerNo";
			public const string QuestionerName = "QuestionerName";
			public const string PeriodYear = "PeriodYear";
			public const string LoadScore = "LoadScore";
			public const string SRAppraisalType = "SRAppraisalType";
			public const string Notes = "Notes";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsScoringRecapitulation = "IsScoringRecapitulation";
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
			lock (typeof(AppraisalQuestionMetadata))
			{
				if (AppraisalQuestionMetadata.mapDelegates == null)
				{
					AppraisalQuestionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalQuestionMetadata.meta == null)
				{
					AppraisalQuestionMetadata.meta = new AppraisalQuestionMetadata();
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

				meta.AddTypeMap("QuestionerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionerNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionerName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LoadScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRAppraisalType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsScoringRecapitulation", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "AppraisalQuestion";
				meta.Destination = "AppraisalQuestion";
				meta.spInsert = "proc_AppraisalQuestionInsert";
				meta.spUpdate = "proc_AppraisalQuestionUpdate";
				meta.spDelete = "proc_AppraisalQuestionDelete";
				meta.spLoadAll = "proc_AppraisalQuestionLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalQuestionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalQuestionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
