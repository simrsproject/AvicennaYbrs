/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/7/2022 6:03:44 PM
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
	abstract public class esAppraisalScoresheetItemCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalScoresheetItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalScoresheetItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalScoresheetItemQuery query)
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
			this.InitQuery(query as esAppraisalScoresheetItemQuery);
		}
		#endregion

		virtual public AppraisalScoresheetItem DetachEntity(AppraisalScoresheetItem entity)
		{
			return base.DetachEntity(entity) as AppraisalScoresheetItem;
		}

		virtual public AppraisalScoresheetItem AttachEntity(AppraisalScoresheetItem entity)
		{
			return base.AttachEntity(entity) as AppraisalScoresheetItem;
		}

		virtual public void Combine(AppraisalScoresheetItemCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalScoresheetItem this[int index]
		{
			get
			{
				return base[index] as AppraisalScoresheetItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalScoresheetItem);
		}
	}

	[Serializable]
	abstract public class esAppraisalScoresheetItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalScoresheetItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalScoresheetItem()
		{
		}

		public esAppraisalScoresheetItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 scoresheetItemID, Int32 scoresheetID, Int32 questionerItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoresheetItemID, scoresheetID, questionerItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(scoresheetItemID, scoresheetID, questionerItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 scoresheetItemID, Int32 scoresheetID, Int32 questionerItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoresheetItemID, scoresheetID, questionerItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(scoresheetItemID, scoresheetID, questionerItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 scoresheetItemID, Int32 scoresheetID, Int32 questionerItemID)
		{
			esAppraisalScoresheetItemQuery query = this.GetDynamicQuery();
			query.Where(query.ScoresheetItemID == scoresheetItemID, query.ScoresheetID == scoresheetID, query.QuestionerItemID == questionerItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 scoresheetItemID, Int32 scoresheetID, Int32 questionerItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ScoresheetItemID", scoresheetItemID);
			parms.Add("ScoresheetID", scoresheetID);
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
						case "ScoresheetItemID": this.str.ScoresheetItemID = (string)value; break;
						case "ScoresheetID": this.str.ScoresheetID = (string)value; break;
						case "QuestionerItemID": this.str.QuestionerItemID = (string)value; break;
						case "MarkValue": this.str.MarkValue = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Score": this.str.Score = (string)value; break;
						case "RealizationValue": this.str.RealizationValue = (string)value; break;
						case "TotalScore": this.str.TotalScore = (string)value; break;
						case "RatingValue": this.str.RatingValue = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "RatingID": this.str.RatingID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ScoresheetItemID":

							if (value == null || value is System.Int32)
								this.ScoresheetItemID = (System.Int32?)value;
							break;
						case "ScoresheetID":

							if (value == null || value is System.Int32)
								this.ScoresheetID = (System.Int32?)value;
							break;
						case "QuestionerItemID":

							if (value == null || value is System.Int32)
								this.QuestionerItemID = (System.Int32?)value;
							break;
						case "Score":

							if (value == null || value is System.Decimal)
								this.Score = (System.Decimal?)value;
							break;
						case "RealizationValue":

							if (value == null || value is System.Decimal)
								this.RealizationValue = (System.Decimal?)value;
							break;
						case "TotalScore":

							if (value == null || value is System.Decimal)
								this.TotalScore = (System.Decimal?)value;
							break;
						case "RatingValue":

							if (value == null || value is System.Decimal)
								this.RatingValue = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "RatingID":

							if (value == null || value is System.Int32)
								this.RatingID = (System.Int32?)value;
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
		/// Maps to AppraisalScoresheetItem.ScoresheetItemID
		/// </summary>
		virtual public System.Int32? ScoresheetItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.ScoresheetID
		/// </summary>
		virtual public System.Int32? ScoresheetID
		{
			get
			{
				return base.GetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetID);
			}

			set
			{
				base.SetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.QuestionerItemID
		/// </summary>
		virtual public System.Int32? QuestionerItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.QuestionerItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.QuestionerItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.MarkValue
		/// </summary>
		virtual public System.String MarkValue
		{
			get
			{
				return base.GetSystemString(AppraisalScoresheetItemMetadata.ColumnNames.MarkValue);
			}

			set
			{
				base.SetSystemString(AppraisalScoresheetItemMetadata.ColumnNames.MarkValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AppraisalScoresheetItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AppraisalScoresheetItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.Score
		/// </summary>
		virtual public System.Decimal? Score
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.Score);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.Score, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.RealizationValue
		/// </summary>
		virtual public System.Decimal? RealizationValue
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.RealizationValue);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.RealizationValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.TotalScore
		/// </summary>
		virtual public System.Decimal? TotalScore
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.TotalScore);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.TotalScore, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.RatingValue
		/// </summary>
		virtual public System.Decimal? RatingValue
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.RatingValue);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoresheetItemMetadata.ColumnNames.RatingValue, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoresheetItem.RatingID
		/// </summary>
		virtual public System.Int32? RatingID
		{
			get
			{
				return base.GetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.RatingID);
			}

			set
			{
				base.SetSystemInt32(AppraisalScoresheetItemMetadata.ColumnNames.RatingID, value);
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
			public esStrings(esAppraisalScoresheetItem entity)
			{
				this.entity = entity;
			}
			public System.String ScoresheetItemID
			{
				get
				{
					System.Int32? data = entity.ScoresheetItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScoresheetItemID = null;
					else entity.ScoresheetItemID = Convert.ToInt32(value);
				}
			}
			public System.String ScoresheetID
			{
				get
				{
					System.Int32? data = entity.ScoresheetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScoresheetID = null;
					else entity.ScoresheetID = Convert.ToInt32(value);
				}
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
			public System.String MarkValue
			{
				get
				{
					System.String data = entity.MarkValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarkValue = null;
					else entity.MarkValue = Convert.ToString(value);
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
			public System.String Score
			{
				get
				{
					System.Decimal? data = entity.Score;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score = null;
					else entity.Score = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationValue
			{
				get
				{
					System.Decimal? data = entity.RealizationValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationValue = null;
					else entity.RealizationValue = Convert.ToDecimal(value);
				}
			}
			public System.String TotalScore
			{
				get
				{
					System.Decimal? data = entity.TotalScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalScore = null;
					else entity.TotalScore = Convert.ToDecimal(value);
				}
			}
			public System.String RatingValue
			{
				get
				{
					System.Decimal? data = entity.RatingValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingValue = null;
					else entity.RatingValue = Convert.ToDecimal(value);
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
			public System.String RatingID
			{
				get
				{
					System.Int32? data = entity.RatingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatingID = null;
					else entity.RatingID = Convert.ToInt32(value);
				}
			}
			private esAppraisalScoresheetItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalScoresheetItemQuery query)
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
				throw new Exception("esAppraisalScoresheetItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalScoresheetItem : esAppraisalScoresheetItem
	{
	}

	[Serializable]
	abstract public class esAppraisalScoresheetItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalScoresheetItemMetadata.Meta();
			}
		}

		public esQueryItem ScoresheetItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetItemID, esSystemType.Int32);
			}
		}

		public esQueryItem ScoresheetID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionerItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.QuestionerItemID, esSystemType.Int32);
			}
		}

		public esQueryItem MarkValue
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.MarkValue, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Score
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.Score, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationValue
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.RealizationValue, esSystemType.Decimal);
			}
		}

		public esQueryItem TotalScore
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.TotalScore, esSystemType.Decimal);
			}
		}

		public esQueryItem RatingValue
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.RatingValue, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem RatingID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoresheetItemMetadata.ColumnNames.RatingID, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalScoresheetItemCollection")]
	public partial class AppraisalScoresheetItemCollection : esAppraisalScoresheetItemCollection, IEnumerable<AppraisalScoresheetItem>
	{
		public AppraisalScoresheetItemCollection()
		{

		}

		public static implicit operator List<AppraisalScoresheetItem>(AppraisalScoresheetItemCollection coll)
		{
			List<AppraisalScoresheetItem> list = new List<AppraisalScoresheetItem>();

			foreach (AppraisalScoresheetItem emp in coll)
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
				return AppraisalScoresheetItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalScoresheetItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalScoresheetItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalScoresheetItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalScoresheetItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalScoresheetItemQuery();
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
		public bool Load(AppraisalScoresheetItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalScoresheetItem AddNew()
		{
			AppraisalScoresheetItem entity = base.AddNewEntity() as AppraisalScoresheetItem;

			return entity;
		}
		public AppraisalScoresheetItem FindByPrimaryKey(Int32 scoresheetItemID, Int32 scoresheetID, Int32 questionerItemID)
		{
			return base.FindByPrimaryKey(scoresheetItemID, scoresheetID, questionerItemID) as AppraisalScoresheetItem;
		}

		#region IEnumerable< AppraisalScoresheetItem> Members

		IEnumerator<AppraisalScoresheetItem> IEnumerable<AppraisalScoresheetItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalScoresheetItem;
			}
		}

		#endregion

		private AppraisalScoresheetItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalScoresheetItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalScoresheetItem ({ScoresheetItemID, ScoresheetID, QuestionerItemID})")]
	[Serializable]
	public partial class AppraisalScoresheetItem : esAppraisalScoresheetItem
	{
		public AppraisalScoresheetItem()
		{
		}

		public AppraisalScoresheetItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalScoresheetItemMetadata.Meta();
			}
		}

		override protected esAppraisalScoresheetItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalScoresheetItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalScoresheetItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalScoresheetItemQuery();
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
		public bool Load(AppraisalScoresheetItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalScoresheetItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalScoresheetItemQuery : esAppraisalScoresheetItemQuery
	{
		public AppraisalScoresheetItemQuery()
		{

		}

		public AppraisalScoresheetItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalScoresheetItemQuery";
		}
	}

	[Serializable]
	public partial class AppraisalScoresheetItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalScoresheetItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.ScoresheetItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.ScoresheetID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.ScoresheetID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.QuestionerItemID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.QuestionerItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.MarkValue, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.MarkValue;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.Score, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.Score;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.RealizationValue, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.RealizationValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.TotalScore, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.TotalScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.RatingValue, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.RatingValue;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoresheetItemMetadata.ColumnNames.RatingID, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalScoresheetItemMetadata.PropertyNames.RatingID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalScoresheetItemMetadata Meta()
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
			public const string ScoresheetItemID = "ScoresheetItemID";
			public const string ScoresheetID = "ScoresheetID";
			public const string QuestionerItemID = "QuestionerItemID";
			public const string MarkValue = "MarkValue";
			public const string Notes = "Notes";
			public const string Score = "Score";
			public const string RealizationValue = "RealizationValue";
			public const string TotalScore = "TotalScore";
			public const string RatingValue = "RatingValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RatingID = "RatingID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ScoresheetItemID = "ScoresheetItemID";
			public const string ScoresheetID = "ScoresheetID";
			public const string QuestionerItemID = "QuestionerItemID";
			public const string MarkValue = "MarkValue";
			public const string Notes = "Notes";
			public const string Score = "Score";
			public const string RealizationValue = "RealizationValue";
			public const string TotalScore = "TotalScore";
			public const string RatingValue = "RatingValue";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RatingID = "RatingID";
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
			lock (typeof(AppraisalScoresheetItemMetadata))
			{
				if (AppraisalScoresheetItemMetadata.mapDelegates == null)
				{
					AppraisalScoresheetItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalScoresheetItemMetadata.meta == null)
				{
					AppraisalScoresheetItemMetadata.meta = new AppraisalScoresheetItemMetadata();
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

				meta.AddTypeMap("ScoresheetItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ScoresheetID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionerItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MarkValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Score", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RealizationValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TotalScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RatingValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RatingID", new esTypeMap("int", "System.Int32"));


				meta.Source = "AppraisalScoresheetItem";
				meta.Destination = "AppraisalScoresheetItem";
				meta.spInsert = "proc_AppraisalScoresheetItemInsert";
				meta.spUpdate = "proc_AppraisalScoresheetItemUpdate";
				meta.spDelete = "proc_AppraisalScoresheetItemDelete";
				meta.spLoadAll = "proc_AppraisalScoresheetItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalScoresheetItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalScoresheetItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
