/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/22/2023 4:25:26 PM
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
	abstract public class esEmployeeTrainingEvaluationCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTrainingEvaluationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTrainingEvaluationCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingEvaluationQuery query)
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
			this.InitQuery(query as esEmployeeTrainingEvaluationQuery);
		}
		#endregion

		virtual public EmployeeTrainingEvaluation DetachEntity(EmployeeTrainingEvaluation entity)
		{
			return base.DetachEntity(entity) as EmployeeTrainingEvaluation;
		}

		virtual public EmployeeTrainingEvaluation AttachEntity(EmployeeTrainingEvaluation entity)
		{
			return base.AttachEntity(entity) as EmployeeTrainingEvaluation;
		}

		virtual public void Combine(EmployeeTrainingEvaluationCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTrainingEvaluation this[int index]
		{
			get
			{
				return base[index] as EmployeeTrainingEvaluation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTrainingEvaluation);
		}
	}

	[Serializable]
	abstract public class esEmployeeTrainingEvaluation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTrainingEvaluationQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTrainingEvaluation()
		{
		}

		public esEmployeeTrainingEvaluation(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeTrainingHistoryID, String assessmentAspectID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingHistoryID, assessmentAspectID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingHistoryID, assessmentAspectID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeTrainingHistoryID, String assessmentAspectID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingHistoryID, assessmentAspectID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingHistoryID, assessmentAspectID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeTrainingHistoryID, String assessmentAspectID)
		{
			esEmployeeTrainingEvaluationQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeTrainingHistoryID == employeeTrainingHistoryID, query.AssessmentAspectID == assessmentAspectID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeTrainingHistoryID, String assessmentAspectID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeTrainingHistoryID", employeeTrainingHistoryID);
			parms.Add("AssessmentAspectID", assessmentAspectID);
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
						case "EmployeeTrainingHistoryID": this.str.EmployeeTrainingHistoryID = (string)value; break;
						case "AssessmentAspectID": this.str.AssessmentAspectID = (string)value; break;
						case "RatingResult": this.str.RatingResult = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeTrainingHistoryID":

							if (value == null || value is System.Int32)
								this.EmployeeTrainingHistoryID = (System.Int32?)value;
							break;
						case "RatingResult":

							if (value == null || value is System.Decimal)
								this.RatingResult = (System.Decimal?)value;
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
		/// Maps to EmployeeTrainingEvaluation.EmployeeTrainingHistoryID
		/// </summary>
		virtual public System.Int32? EmployeeTrainingHistoryID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingEvaluationMetadata.ColumnNames.EmployeeTrainingHistoryID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingEvaluationMetadata.ColumnNames.EmployeeTrainingHistoryID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingEvaluation.AssessmentAspectID
		/// </summary>
		virtual public System.String AssessmentAspectID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingEvaluationMetadata.ColumnNames.AssessmentAspectID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingEvaluationMetadata.ColumnNames.AssessmentAspectID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingEvaluation.RatingResult
		/// </summary>
		virtual public System.Decimal? RatingResult
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingEvaluationMetadata.ColumnNames.RatingResult);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingEvaluationMetadata.ColumnNames.RatingResult, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingEvaluation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingEvaluation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeTrainingEvaluation entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeTrainingHistoryID
			{
				get
				{
					System.Int32? data = entity.EmployeeTrainingHistoryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeTrainingHistoryID = null;
					else entity.EmployeeTrainingHistoryID = Convert.ToInt32(value);
				}
			}
			public System.String AssessmentAspectID
			{
				get
				{
					System.String data = entity.AssessmentAspectID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssessmentAspectID = null;
					else entity.AssessmentAspectID = Convert.ToString(value);
				}
			}
			public System.String RatingResult
			{
				get
				{
					System.Decimal? data = entity.RatingResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingResult = null;
					else entity.RatingResult = Convert.ToDecimal(value);
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
			private esEmployeeTrainingEvaluation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingEvaluationQuery query)
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
				throw new Exception("esEmployeeTrainingEvaluation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTrainingEvaluation : esEmployeeTrainingEvaluation
	{
	}

	[Serializable]
	abstract public class esEmployeeTrainingEvaluationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingEvaluationMetadata.Meta();
			}
		}

		public esQueryItem EmployeeTrainingHistoryID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingEvaluationMetadata.ColumnNames.EmployeeTrainingHistoryID, esSystemType.Int32);
			}
		}

		public esQueryItem AssessmentAspectID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingEvaluationMetadata.ColumnNames.AssessmentAspectID, esSystemType.String);
			}
		}

		public esQueryItem RatingResult
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingEvaluationMetadata.ColumnNames.RatingResult, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTrainingEvaluationCollection")]
	public partial class EmployeeTrainingEvaluationCollection : esEmployeeTrainingEvaluationCollection, IEnumerable<EmployeeTrainingEvaluation>
	{
		public EmployeeTrainingEvaluationCollection()
		{

		}

		public static implicit operator List<EmployeeTrainingEvaluation>(EmployeeTrainingEvaluationCollection coll)
		{
			List<EmployeeTrainingEvaluation> list = new List<EmployeeTrainingEvaluation>();

			foreach (EmployeeTrainingEvaluation emp in coll)
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
				return EmployeeTrainingEvaluationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingEvaluationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTrainingEvaluation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTrainingEvaluation();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingEvaluationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingEvaluationQuery();
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
		public bool Load(EmployeeTrainingEvaluationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTrainingEvaluation AddNew()
		{
			EmployeeTrainingEvaluation entity = base.AddNewEntity() as EmployeeTrainingEvaluation;

			return entity;
		}
		public EmployeeTrainingEvaluation FindByPrimaryKey(Int32 employeeTrainingHistoryID, String assessmentAspectID)
		{
			return base.FindByPrimaryKey(employeeTrainingHistoryID, assessmentAspectID) as EmployeeTrainingEvaluation;
		}

		#region IEnumerable< EmployeeTrainingEvaluation> Members

		IEnumerator<EmployeeTrainingEvaluation> IEnumerable<EmployeeTrainingEvaluation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTrainingEvaluation;
			}
		}

		#endregion

		private EmployeeTrainingEvaluationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTrainingEvaluation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTrainingEvaluation ({EmployeeTrainingHistoryID, AssessmentAspectID})")]
	[Serializable]
	public partial class EmployeeTrainingEvaluation : esEmployeeTrainingEvaluation
	{
		public EmployeeTrainingEvaluation()
		{
		}

		public EmployeeTrainingEvaluation(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingEvaluationMetadata.Meta();
			}
		}

		override protected esEmployeeTrainingEvaluationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingEvaluationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingEvaluationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingEvaluationQuery();
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
		public bool Load(EmployeeTrainingEvaluationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTrainingEvaluationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTrainingEvaluationQuery : esEmployeeTrainingEvaluationQuery
	{
		public EmployeeTrainingEvaluationQuery()
		{

		}

		public EmployeeTrainingEvaluationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTrainingEvaluationQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTrainingEvaluationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTrainingEvaluationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTrainingEvaluationMetadata.ColumnNames.EmployeeTrainingHistoryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingEvaluationMetadata.PropertyNames.EmployeeTrainingHistoryID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingEvaluationMetadata.ColumnNames.AssessmentAspectID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingEvaluationMetadata.PropertyNames.AssessmentAspectID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingEvaluationMetadata.ColumnNames.RatingResult, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingEvaluationMetadata.PropertyNames.RatingResult;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingEvaluationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingEvaluationMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingEvaluationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeTrainingEvaluationMetadata Meta()
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
			public const string EmployeeTrainingHistoryID = "EmployeeTrainingHistoryID";
			public const string AssessmentAspectID = "AssessmentAspectID";
			public const string RatingResult = "RatingResult";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeTrainingHistoryID = "EmployeeTrainingHistoryID";
			public const string AssessmentAspectID = "AssessmentAspectID";
			public const string RatingResult = "RatingResult";
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
			lock (typeof(EmployeeTrainingEvaluationMetadata))
			{
				if (EmployeeTrainingEvaluationMetadata.mapDelegates == null)
				{
					EmployeeTrainingEvaluationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTrainingEvaluationMetadata.meta == null)
				{
					EmployeeTrainingEvaluationMetadata.meta = new EmployeeTrainingEvaluationMetadata();
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

				meta.AddTypeMap("EmployeeTrainingHistoryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssessmentAspectID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RatingResult", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeTrainingEvaluation";
				meta.Destination = "EmployeeTrainingEvaluation";
				meta.spInsert = "proc_EmployeeTrainingEvaluationInsert";
				meta.spUpdate = "proc_EmployeeTrainingEvaluationUpdate";
				meta.spDelete = "proc_EmployeeTrainingEvaluationDelete";
				meta.spLoadAll = "proc_EmployeeTrainingEvaluationLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTrainingEvaluationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTrainingEvaluationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
