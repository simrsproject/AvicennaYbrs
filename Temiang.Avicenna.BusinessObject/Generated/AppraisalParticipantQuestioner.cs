/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/14/2020 12:48:05 PM
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
	abstract public class esAppraisalParticipantQuestionerCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalParticipantQuestionerCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalParticipantQuestionerCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantQuestionerQuery query)
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
			this.InitQuery(query as esAppraisalParticipantQuestionerQuery);
		}
		#endregion

		virtual public AppraisalParticipantQuestioner DetachEntity(AppraisalParticipantQuestioner entity)
		{
			return base.DetachEntity(entity) as AppraisalParticipantQuestioner;
		}

		virtual public AppraisalParticipantQuestioner AttachEntity(AppraisalParticipantQuestioner entity)
		{
			return base.AttachEntity(entity) as AppraisalParticipantQuestioner;
		}

		virtual public void Combine(AppraisalParticipantQuestionerCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalParticipantQuestioner this[int index]
		{
			get
			{
				return base[index] as AppraisalParticipantQuestioner;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalParticipantQuestioner);
		}
	}

	[Serializable]
	abstract public class esAppraisalParticipantQuestioner : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalParticipantQuestionerQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalParticipantQuestioner()
		{
		}

		public esAppraisalParticipantQuestioner(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 participantQuestionerID, Int32 participantItemID, Int32 questionerID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantQuestionerID, participantItemID, questionerID);
			else
				return LoadByPrimaryKeyStoredProcedure(participantQuestionerID, participantItemID, questionerID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 participantQuestionerID, Int32 participantItemID, Int32 questionerID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantQuestionerID, participantItemID, questionerID);
			else
				return LoadByPrimaryKeyStoredProcedure(participantQuestionerID, participantItemID, questionerID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 participantQuestionerID, Int32 participantItemID, Int32 questionerID)
		{
			esAppraisalParticipantQuestionerQuery query = this.GetDynamicQuery();
			query.Where(query.ParticipantQuestionerID == participantQuestionerID, query.ParticipantItemID == participantItemID, query.QuestionerID == questionerID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 participantQuestionerID, Int32 participantItemID, Int32 questionerID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParticipantQuestionerID", participantQuestionerID);
			parms.Add("ParticipantItemID", participantItemID);
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
						case "ParticipantQuestionerID": this.str.ParticipantQuestionerID = (string)value; break;
						case "ParticipantItemID": this.str.ParticipantItemID = (string)value; break;
						case "QuestionerID": this.str.QuestionerID = (string)value; break;
						case "EvaluatorID": this.str.EvaluatorID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ParticipantQuestionerID":

							if (value == null || value is System.Int32)
								this.ParticipantQuestionerID = (System.Int32?)value;
							break;
						case "ParticipantItemID":

							if (value == null || value is System.Int32)
								this.ParticipantItemID = (System.Int32?)value;
							break;
						case "QuestionerID":

							if (value == null || value is System.Int32)
								this.QuestionerID = (System.Int32?)value;
							break;
						case "EvaluatorID":

							if (value == null || value is System.Int32)
								this.EvaluatorID = (System.Int32?)value;
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
		/// Maps to AppraisalParticipantQuestioner.ParticipantQuestionerID
		/// </summary>
		virtual public System.Int32? ParticipantQuestionerID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantQuestionerID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantQuestionerID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantQuestioner.ParticipantItemID
		/// </summary>
		virtual public System.Int32? ParticipantItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantQuestioner.QuestionerID
		/// </summary>
		virtual public System.Int32? QuestionerID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.QuestionerID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.QuestionerID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantQuestioner.EvaluatorID
		/// </summary>
		virtual public System.Int32? EvaluatorID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.EvaluatorID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantQuestionerMetadata.ColumnNames.EvaluatorID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantQuestioner.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantQuestioner.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAppraisalParticipantQuestioner entity)
			{
				this.entity = entity;
			}
			public System.String ParticipantQuestionerID
			{
				get
				{
					System.Int32? data = entity.ParticipantQuestionerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantQuestionerID = null;
					else entity.ParticipantQuestionerID = Convert.ToInt32(value);
				}
			}
			public System.String ParticipantItemID
			{
				get
				{
					System.Int32? data = entity.ParticipantItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantItemID = null;
					else entity.ParticipantItemID = Convert.ToInt32(value);
				}
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
			public System.String EvaluatorID
			{
				get
				{
					System.Int32? data = entity.EvaluatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluatorID = null;
					else entity.EvaluatorID = Convert.ToInt32(value);
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
			private esAppraisalParticipantQuestioner entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantQuestionerQuery query)
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
				throw new Exception("esAppraisalParticipantQuestioner can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalParticipantQuestioner : esAppraisalParticipantQuestioner
	{
	}

	[Serializable]
	abstract public class esAppraisalParticipantQuestionerQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantQuestionerMetadata.Meta();
			}
		}

		public esQueryItem ParticipantQuestionerID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantQuestionerID, esSystemType.Int32);
			}
		}

		public esQueryItem ParticipantItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantItemID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionerID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantQuestionerMetadata.ColumnNames.QuestionerID, esSystemType.Int32);
			}
		}

		public esQueryItem EvaluatorID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantQuestionerMetadata.ColumnNames.EvaluatorID, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalParticipantQuestionerCollection")]
	public partial class AppraisalParticipantQuestionerCollection : esAppraisalParticipantQuestionerCollection, IEnumerable<AppraisalParticipantQuestioner>
	{
		public AppraisalParticipantQuestionerCollection()
		{

		}

		public static implicit operator List<AppraisalParticipantQuestioner>(AppraisalParticipantQuestionerCollection coll)
		{
			List<AppraisalParticipantQuestioner> list = new List<AppraisalParticipantQuestioner>();

			foreach (AppraisalParticipantQuestioner emp in coll)
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
				return AppraisalParticipantQuestionerMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantQuestionerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalParticipantQuestioner(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalParticipantQuestioner();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantQuestionerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantQuestionerQuery();
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
		public bool Load(AppraisalParticipantQuestionerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalParticipantQuestioner AddNew()
		{
			AppraisalParticipantQuestioner entity = base.AddNewEntity() as AppraisalParticipantQuestioner;

			return entity;
		}
		public AppraisalParticipantQuestioner FindByPrimaryKey(Int32 participantQuestionerID, Int32 participantItemID, Int32 questionerID)
		{
			return base.FindByPrimaryKey(participantQuestionerID, participantItemID, questionerID) as AppraisalParticipantQuestioner;
		}

		#region IEnumerable< AppraisalParticipantQuestioner> Members

		IEnumerator<AppraisalParticipantQuestioner> IEnumerable<AppraisalParticipantQuestioner>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalParticipantQuestioner;
			}
		}

		#endregion

		private AppraisalParticipantQuestionerQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalParticipantQuestioner' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalParticipantQuestioner ({ParticipantQuestionerID, ParticipantItemID, QuestionerID})")]
	[Serializable]
	public partial class AppraisalParticipantQuestioner : esAppraisalParticipantQuestioner
	{
		public AppraisalParticipantQuestioner()
		{
		}

		public AppraisalParticipantQuestioner(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantQuestionerMetadata.Meta();
			}
		}

		override protected esAppraisalParticipantQuestionerQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantQuestionerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantQuestionerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantQuestionerQuery();
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
		public bool Load(AppraisalParticipantQuestionerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalParticipantQuestionerQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalParticipantQuestionerQuery : esAppraisalParticipantQuestionerQuery
	{
		public AppraisalParticipantQuestionerQuery()
		{

		}

		public AppraisalParticipantQuestionerQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalParticipantQuestionerQuery";
		}
	}

	[Serializable]
	public partial class AppraisalParticipantQuestionerMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalParticipantQuestionerMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantQuestionerID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantQuestionerMetadata.PropertyNames.ParticipantQuestionerID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantQuestionerMetadata.ColumnNames.ParticipantItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantQuestionerMetadata.PropertyNames.ParticipantItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantQuestionerMetadata.ColumnNames.QuestionerID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantQuestionerMetadata.PropertyNames.QuestionerID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantQuestionerMetadata.ColumnNames.EvaluatorID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantQuestionerMetadata.PropertyNames.EvaluatorID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantQuestionerMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantQuestionerMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantQuestionerMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalParticipantQuestionerMetadata Meta()
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
			public const string ParticipantQuestionerID = "ParticipantQuestionerID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string QuestionerID = "QuestionerID";
			public const string EvaluatorID = "EvaluatorID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ParticipantQuestionerID = "ParticipantQuestionerID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string QuestionerID = "QuestionerID";
			public const string EvaluatorID = "EvaluatorID";
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
			lock (typeof(AppraisalParticipantQuestionerMetadata))
			{
				if (AppraisalParticipantQuestionerMetadata.mapDelegates == null)
				{
					AppraisalParticipantQuestionerMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalParticipantQuestionerMetadata.meta == null)
				{
					AppraisalParticipantQuestionerMetadata.meta = new AppraisalParticipantQuestionerMetadata();
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

				meta.AddTypeMap("ParticipantQuestionerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParticipantItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EvaluatorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppraisalParticipantQuestioner";
				meta.Destination = "AppraisalParticipantQuestioner";
				meta.spInsert = "proc_AppraisalParticipantQuestionerInsert";
				meta.spUpdate = "proc_AppraisalParticipantQuestionerUpdate";
				meta.spDelete = "proc_AppraisalParticipantQuestionerDelete";
				meta.spLoadAll = "proc_AppraisalParticipantQuestionerLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalParticipantQuestionerLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalParticipantQuestionerMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
