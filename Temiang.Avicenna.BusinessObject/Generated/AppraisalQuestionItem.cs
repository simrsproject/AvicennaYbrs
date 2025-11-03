/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/26/2022 4:32:27 PM
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
	abstract public class esAppraisalQuestionItemCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalQuestionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalQuestionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalQuestionItemQuery query)
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
			this.InitQuery(query as esAppraisalQuestionItemQuery);
		}
		#endregion

		virtual public AppraisalQuestionItem DetachEntity(AppraisalQuestionItem entity)
		{
			return base.DetachEntity(entity) as AppraisalQuestionItem;
		}

		virtual public AppraisalQuestionItem AttachEntity(AppraisalQuestionItem entity)
		{
			return base.AttachEntity(entity) as AppraisalQuestionItem;
		}

		virtual public void Combine(AppraisalQuestionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalQuestionItem this[int index]
		{
			get
			{
				return base[index] as AppraisalQuestionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalQuestionItem);
		}
	}

	[Serializable]
	abstract public class esAppraisalQuestionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalQuestionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalQuestionItem()
		{
		}

		public esAppraisalQuestionItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 questionerItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionerItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionerItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 questionerItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionerItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionerItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 questionerItemID)
		{
			esAppraisalQuestionItemQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionerItemID == questionerItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 questionerItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionerItemID", questionerItemID);
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
						case "QuestionerItemID": this.str.QuestionerItemID = (string)value; break;
						case "QuestionerID": this.str.QuestionerID = (string)value; break;
						case "QuestionCode": this.str.QuestionCode = (string)value; break;
						case "QuestionName": this.str.QuestionName = (string)value; break;
						case "QuestionGroupName": this.str.QuestionGroupName = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Rating": this.str.Rating = (string)value; break;
						case "Benchmark": this.str.Benchmark = (string)value; break;
						case "MinValue": this.str.MinValue = (string)value; break;
						case "MaxValue": this.str.MaxValue = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Target": this.str.Target = (string)value; break;
						case "Achievements": this.str.Achievements = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "QuestionerItemID":

							if (value == null || value is System.Int32)
								this.QuestionerItemID = (System.Int32?)value;
							break;
						case "QuestionerID":

							if (value == null || value is System.Int32)
								this.QuestionerID = (System.Int32?)value;
							break;
						case "Rating":

							if (value == null || value is System.Decimal)
								this.Rating = (System.Decimal?)value;
							break;
						case "Benchmark":

							if (value == null || value is System.Decimal)
								this.Benchmark = (System.Decimal?)value;
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
		/// Maps to AppraisalQuestionItem.QuestionerItemID
		/// </summary>
		virtual public System.Int32? QuestionerItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalQuestionItemMetadata.ColumnNames.QuestionerItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalQuestionItemMetadata.ColumnNames.QuestionerItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.QuestionerID
		/// </summary>
		virtual public System.Int32? QuestionerID
		{
			get
			{
				return base.GetSystemInt32(AppraisalQuestionItemMetadata.ColumnNames.QuestionerID);
			}

			set
			{
				base.SetSystemInt32(AppraisalQuestionItemMetadata.ColumnNames.QuestionerID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.QuestionCode
		/// </summary>
		virtual public System.String QuestionCode
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionItemMetadata.ColumnNames.QuestionCode);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionItemMetadata.ColumnNames.QuestionCode, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.QuestionName
		/// </summary>
		virtual public System.String QuestionName
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionItemMetadata.ColumnNames.QuestionName);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionItemMetadata.ColumnNames.QuestionName, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.QuestionGroupName
		/// </summary>
		virtual public System.String QuestionGroupName
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionItemMetadata.ColumnNames.QuestionGroupName);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionItemMetadata.ColumnNames.QuestionGroupName, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.Rating
		/// </summary>
		virtual public System.Decimal? Rating
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.Rating);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.Rating, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.Benchmark
		/// </summary>
		virtual public System.Decimal? Benchmark
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.Benchmark);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.Benchmark, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.MinValue
		/// </summary>
		virtual public System.Decimal? MinValue
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.MinValue);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.MinValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.MaxValue
		/// </summary>
		virtual public System.Decimal? MaxValue
		{
			get
			{
				return base.GetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.MaxValue);
			}

			set
			{
				base.SetSystemDecimal(AppraisalQuestionItemMetadata.ColumnNames.MaxValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalQuestionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalQuestionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.Target
		/// </summary>
		virtual public System.String Target
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionItemMetadata.ColumnNames.Target);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionItemMetadata.ColumnNames.Target, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalQuestionItem.Achievements
		/// </summary>
		virtual public System.String Achievements
		{
			get
			{
				return base.GetSystemString(AppraisalQuestionItemMetadata.ColumnNames.Achievements);
			}

			set
			{
				base.SetSystemString(AppraisalQuestionItemMetadata.ColumnNames.Achievements, value);
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
			public esStrings(esAppraisalQuestionItem entity)
			{
				this.entity = entity;
			}
			public System.String QuestionerItemID
			{
				get
				{
					System.Int32? data = entity.QuestionerItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionerItemID = null;
					else entity.QuestionerItemID = Convert.ToInt32(value);
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
			public System.String QuestionCode
			{
				get
				{
					System.String data = entity.QuestionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionCode = null;
					else entity.QuestionCode = Convert.ToString(value);
				}
			}
			public System.String QuestionName
			{
				get
				{
					System.String data = entity.QuestionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionName = null;
					else entity.QuestionName = Convert.ToString(value);
				}
			}
			public System.String QuestionGroupName
			{
				get
				{
					System.String data = entity.QuestionGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionGroupName = null;
					else entity.QuestionGroupName = Convert.ToString(value);
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
			public System.String Rating
			{
				get
				{
					System.Decimal? data = entity.Rating;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rating = null;
					else entity.Rating = Convert.ToDecimal(value);
				}
			}
			public System.String Benchmark
			{
				get
				{
					System.Decimal? data = entity.Benchmark;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Benchmark = null;
					else entity.Benchmark = Convert.ToDecimal(value);
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
			public System.String Target
			{
				get
				{
					System.String data = entity.Target;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Target = null;
					else entity.Target = Convert.ToString(value);
				}
			}
			public System.String Achievements
			{
				get
				{
					System.String data = entity.Achievements;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Achievements = null;
					else entity.Achievements = Convert.ToString(value);
				}
			}
			private esAppraisalQuestionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalQuestionItemQuery query)
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
				throw new Exception("esAppraisalQuestionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalQuestionItem : esAppraisalQuestionItem
	{
	}

	[Serializable]
	abstract public class esAppraisalQuestionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalQuestionItemMetadata.Meta();
			}
		}

		public esQueryItem QuestionerItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.QuestionerItemID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionerID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.QuestionerID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionCode
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.QuestionCode, esSystemType.String);
			}
		}

		public esQueryItem QuestionName
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.QuestionName, esSystemType.String);
			}
		}

		public esQueryItem QuestionGroupName
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.QuestionGroupName, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Rating
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.Rating, esSystemType.Decimal);
			}
		}

		public esQueryItem Benchmark
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.Benchmark, esSystemType.Decimal);
			}
		}

		public esQueryItem MinValue
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.MinValue, esSystemType.Decimal);
			}
		}

		public esQueryItem MaxValue
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.MaxValue, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Target
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.Target, esSystemType.String);
			}
		}

		public esQueryItem Achievements
		{
			get
			{
				return new esQueryItem(this, AppraisalQuestionItemMetadata.ColumnNames.Achievements, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalQuestionItemCollection")]
	public partial class AppraisalQuestionItemCollection : esAppraisalQuestionItemCollection, IEnumerable<AppraisalQuestionItem>
	{
		public AppraisalQuestionItemCollection()
		{

		}

		public static implicit operator List<AppraisalQuestionItem>(AppraisalQuestionItemCollection coll)
		{
			List<AppraisalQuestionItem> list = new List<AppraisalQuestionItem>();

			foreach (AppraisalQuestionItem emp in coll)
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
				return AppraisalQuestionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalQuestionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalQuestionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalQuestionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalQuestionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalQuestionItemQuery();
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
		public bool Load(AppraisalQuestionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalQuestionItem AddNew()
		{
			AppraisalQuestionItem entity = base.AddNewEntity() as AppraisalQuestionItem;

			return entity;
		}
		public AppraisalQuestionItem FindByPrimaryKey(Int32 questionerItemID)
		{
			return base.FindByPrimaryKey(questionerItemID) as AppraisalQuestionItem;
		}

		#region IEnumerable< AppraisalQuestionItem> Members

		IEnumerator<AppraisalQuestionItem> IEnumerable<AppraisalQuestionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalQuestionItem;
			}
		}

		#endregion

		private AppraisalQuestionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalQuestionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalQuestionItem ({QuestionerItemID})")]
	[Serializable]
	public partial class AppraisalQuestionItem : esAppraisalQuestionItem
	{
		public AppraisalQuestionItem()
		{
		}

		public AppraisalQuestionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalQuestionItemMetadata.Meta();
			}
		}

		override protected esAppraisalQuestionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalQuestionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalQuestionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalQuestionItemQuery();
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
		public bool Load(AppraisalQuestionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalQuestionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalQuestionItemQuery : esAppraisalQuestionItemQuery
	{
		public AppraisalQuestionItemQuery()
		{

		}

		public AppraisalQuestionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalQuestionItemQuery";
		}
	}

	[Serializable]
	public partial class AppraisalQuestionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalQuestionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.QuestionerItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.QuestionerItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.QuestionerID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.QuestionerID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.QuestionCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.QuestionCode;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.QuestionName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.QuestionName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.QuestionGroupName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.QuestionGroupName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.Rating, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.Rating;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.Benchmark, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.Benchmark;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.MinValue, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.MinValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.MaxValue, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.MaxValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.Target, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.Target;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalQuestionItemMetadata.ColumnNames.Achievements, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalQuestionItemMetadata.PropertyNames.Achievements;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalQuestionItemMetadata Meta()
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
			public const string QuestionerItemID = "QuestionerItemID";
			public const string QuestionerID = "QuestionerID";
			public const string QuestionCode = "QuestionCode";
			public const string QuestionName = "QuestionName";
			public const string QuestionGroupName = "QuestionGroupName";
			public const string Notes = "Notes";
			public const string Rating = "Rating";
			public const string Benchmark = "Benchmark";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Target = "Target";
			public const string Achievements = "Achievements";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string QuestionerItemID = "QuestionerItemID";
			public const string QuestionerID = "QuestionerID";
			public const string QuestionCode = "QuestionCode";
			public const string QuestionName = "QuestionName";
			public const string QuestionGroupName = "QuestionGroupName";
			public const string Notes = "Notes";
			public const string Rating = "Rating";
			public const string Benchmark = "Benchmark";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Target = "Target";
			public const string Achievements = "Achievements";
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
			lock (typeof(AppraisalQuestionItemMetadata))
			{
				if (AppraisalQuestionItemMetadata.mapDelegates == null)
				{
					AppraisalQuestionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalQuestionItemMetadata.meta == null)
				{
					AppraisalQuestionItemMetadata.meta = new AppraisalQuestionItemMetadata();
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

				meta.AddTypeMap("QuestionerItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Rating", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Benchmark", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MinValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Target", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Achievements", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppraisalQuestionItem";
				meta.Destination = "AppraisalQuestionItem";
				meta.spInsert = "proc_AppraisalQuestionItemInsert";
				meta.spUpdate = "proc_AppraisalQuestionItemUpdate";
				meta.spDelete = "proc_AppraisalQuestionItemDelete";
				meta.spLoadAll = "proc_AppraisalQuestionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalQuestionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalQuestionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
