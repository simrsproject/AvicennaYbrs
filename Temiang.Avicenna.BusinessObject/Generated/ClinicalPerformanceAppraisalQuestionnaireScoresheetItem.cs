/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/27/2022 10:32:23 PM
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
	abstract public class esClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection : esEntityCollectionWAuditLog
	{
		public esClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery query)
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
			this.InitQuery(query as esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery);
		}
		#endregion

		virtual public ClinicalPerformanceAppraisalQuestionnaireScoresheetItem DetachEntity(ClinicalPerformanceAppraisalQuestionnaireScoresheetItem entity)
		{
			return base.DetachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaireScoresheetItem;
		}

		virtual public ClinicalPerformanceAppraisalQuestionnaireScoresheetItem AttachEntity(ClinicalPerformanceAppraisalQuestionnaireScoresheetItem entity)
		{
			return base.AttachEntity(entity) as ClinicalPerformanceAppraisalQuestionnaireScoresheetItem;
		}

		virtual public void Combine(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ClinicalPerformanceAppraisalQuestionnaireScoresheetItem this[int index]
		{
			get
			{
				return base[index] as ClinicalPerformanceAppraisalQuestionnaireScoresheetItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClinicalPerformanceAppraisalQuestionnaireScoresheetItem);
		}
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaireScoresheetItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esClinicalPerformanceAppraisalQuestionnaireScoresheetItem()
		{
		}

		public esClinicalPerformanceAppraisalQuestionnaireScoresheetItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String scoresheetNo, Int32 questionnaireItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoresheetNo, questionnaireItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(scoresheetNo, questionnaireItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String scoresheetNo, Int32 questionnaireItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoresheetNo, questionnaireItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(scoresheetNo, questionnaireItemID);
		}

		private bool LoadByPrimaryKeyDynamic(String scoresheetNo, Int32 questionnaireItemID)
		{
			esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery query = this.GetDynamicQuery();
			query.Where(query.ScoresheetNo == scoresheetNo, query.QuestionnaireItemID == questionnaireItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String scoresheetNo, Int32 questionnaireItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ScoresheetNo", scoresheetNo);
			parms.Add("QuestionnaireItemID", questionnaireItemID);
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
						case "ScoresheetNo": this.str.ScoresheetNo = (string)value; break;
						case "QuestionnaireItemID": this.str.QuestionnaireItemID = (string)value; break;
						case "Score": this.str.Score = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "QuestionnaireItemID":

							if (value == null || value is System.Int32)
								this.QuestionnaireItemID = (System.Int32?)value;
							break;
						case "Score":

							if (value == null || value is System.Int16)
								this.Score = (System.Int16?)value;
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
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheetItem.ScoresheetNo
		/// </summary>
		virtual public System.String ScoresheetNo
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.ScoresheetNo);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.ScoresheetNo, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheetItem.QuestionnaireItemID
		/// </summary>
		virtual public System.Int32? QuestionnaireItemID
		{
			get
			{
				return base.GetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.QuestionnaireItemID);
			}

			set
			{
				base.SetSystemInt32(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.QuestionnaireItemID, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheetItem.Score
		/// </summary>
		virtual public System.Int16? Score
		{
			get
			{
				return base.GetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.Score);
			}

			set
			{
				base.SetSystemInt16(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.Score, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheetItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClinicalPerformanceAppraisalQuestionnaireScoresheetItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClinicalPerformanceAppraisalQuestionnaireScoresheetItem entity)
			{
				this.entity = entity;
			}
			public System.String ScoresheetNo
			{
				get
				{
					System.String data = entity.ScoresheetNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScoresheetNo = null;
					else entity.ScoresheetNo = Convert.ToString(value);
				}
			}
			public System.String QuestionnaireItemID
			{
				get
				{
					System.Int32? data = entity.QuestionnaireItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionnaireItemID = null;
					else entity.QuestionnaireItemID = Convert.ToInt32(value);
				}
			}
			public System.String Score
			{
				get
				{
					System.Int16? data = entity.Score;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score = null;
					else entity.Score = Convert.ToInt16(value);
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
			private esClinicalPerformanceAppraisalQuestionnaireScoresheetItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery query)
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
				throw new Exception("esClinicalPerformanceAppraisalQuestionnaireScoresheetItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetItem : esClinicalPerformanceAppraisalQuestionnaireScoresheetItem
	{
	}

	[Serializable]
	abstract public class esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.Meta();
			}
		}

		public esQueryItem ScoresheetNo
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.ScoresheetNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireItemID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.QuestionnaireItemID, esSystemType.Int32);
			}
		}

		public esQueryItem Score
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.Score, esSystemType.Int16);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection")]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection : esClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection, IEnumerable<ClinicalPerformanceAppraisalQuestionnaireScoresheetItem>
	{
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection()
		{

		}

		public static implicit operator List<ClinicalPerformanceAppraisalQuestionnaireScoresheetItem>(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection coll)
		{
			List<ClinicalPerformanceAppraisalQuestionnaireScoresheetItem> list = new List<ClinicalPerformanceAppraisalQuestionnaireScoresheetItem>();

			foreach (ClinicalPerformanceAppraisalQuestionnaireScoresheetItem emp in coll)
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
				return ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClinicalPerformanceAppraisalQuestionnaireScoresheetItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClinicalPerformanceAppraisalQuestionnaireScoresheetItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItem AddNew()
		{
			ClinicalPerformanceAppraisalQuestionnaireScoresheetItem entity = base.AddNewEntity() as ClinicalPerformanceAppraisalQuestionnaireScoresheetItem;

			return entity;
		}
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItem FindByPrimaryKey(String scoresheetNo, Int32 questionnaireItemID)
		{
			return base.FindByPrimaryKey(scoresheetNo, questionnaireItemID) as ClinicalPerformanceAppraisalQuestionnaireScoresheetItem;
		}

		#region IEnumerable< ClinicalPerformanceAppraisalQuestionnaireScoresheetItem> Members

		IEnumerator<ClinicalPerformanceAppraisalQuestionnaireScoresheetItem> IEnumerable<ClinicalPerformanceAppraisalQuestionnaireScoresheetItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ClinicalPerformanceAppraisalQuestionnaireScoresheetItem;
			}
		}

		#endregion

		private ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClinicalPerformanceAppraisalQuestionnaireScoresheetItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ClinicalPerformanceAppraisalQuestionnaireScoresheetItem ({ScoresheetNo, QuestionnaireItemID})")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetItem : esClinicalPerformanceAppraisalQuestionnaireScoresheetItem
	{
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItem()
		{
		}

		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.Meta();
			}
		}

		override protected esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery();
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
		public bool Load(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery : esClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery
	{
		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery()
		{

		}

		public ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ClinicalPerformanceAppraisalQuestionnaireScoresheetItemQuery";
		}
	}

	[Serializable]
	public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.ScoresheetNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.PropertyNames.ScoresheetNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.QuestionnaireItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.PropertyNames.QuestionnaireItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.Score, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.PropertyNames.Score;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata Meta()
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
			public const string ScoresheetNo = "ScoresheetNo";
			public const string QuestionnaireItemID = "QuestionnaireItemID";
			public const string Score = "Score";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ScoresheetNo = "ScoresheetNo";
			public const string QuestionnaireItemID = "QuestionnaireItemID";
			public const string Score = "Score";
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
			lock (typeof(ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata))
			{
				if (ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.mapDelegates == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.meta == null)
				{
					ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata.meta = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata();
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

				meta.AddTypeMap("ScoresheetNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionnaireItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Score", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ClinicalPerformanceAppraisalQuestionnaireScoresheetItem";
				meta.Destination = "ClinicalPerformanceAppraisalQuestionnaireScoresheetItem";
				meta.spInsert = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetItemInsert";
				meta.spUpdate = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetItemUpdate";
				meta.spDelete = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetItemDelete";
				meta.spLoadAll = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClinicalPerformanceAppraisalQuestionnaireScoresheetItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClinicalPerformanceAppraisalQuestionnaireScoresheetItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
