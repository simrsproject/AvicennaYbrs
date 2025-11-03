/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/5/2022 2:08:31 PM
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
	abstract public class esPatientResearchCollection : esEntityCollectionWAuditLog
	{
		public esPatientResearchCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientResearchCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientResearchQuery query)
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
			this.InitQuery(query as esPatientResearchQuery);
		}
		#endregion

		virtual public PatientResearch DetachEntity(PatientResearch entity)
		{
			return base.DetachEntity(entity) as PatientResearch;
		}

		virtual public PatientResearch AttachEntity(PatientResearch entity)
		{
			return base.AttachEntity(entity) as PatientResearch;
		}

		virtual public void Combine(PatientResearchCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientResearch this[int index]
		{
			get
			{
				return base[index] as PatientResearch;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientResearch);
		}
	}

	[Serializable]
	abstract public class esPatientResearch : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientResearchQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientResearch()
		{
		}

		public esPatientResearch(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 researchID, String patientID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(researchID, patientID);
			else
				return LoadByPrimaryKeyStoredProcedure(researchID, patientID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 researchID, String patientID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(researchID, patientID);
			else
				return LoadByPrimaryKeyStoredProcedure(researchID, patientID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 researchID, String patientID)
		{
			esPatientResearchQuery query = this.GetDynamicQuery();
			query.Where(query.ResearchID == researchID, query.PatientID == patientID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 researchID, String patientID)
		{
			esParameters parms = new esParameters();
			parms.Add("ResearchID", researchID);
			parms.Add("PatientID", patientID);
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
						case "ResearchID": this.str.ResearchID = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "ResearchTitle": this.str.ResearchTitle = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ResearchID":

							if (value == null || value is System.Int32)
								this.ResearchID = (System.Int32?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
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
		/// Maps to PatientResearch.ResearchID
		/// </summary>
		virtual public System.Int32? ResearchID
		{
			get
			{
				return base.GetSystemInt32(PatientResearchMetadata.ColumnNames.ResearchID);
			}

			set
			{
				base.SetSystemInt32(PatientResearchMetadata.ColumnNames.ResearchID, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientResearchMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(PatientResearchMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.ResearchTitle
		/// </summary>
		virtual public System.String ResearchTitle
		{
			get
			{
				return base.GetSystemString(PatientResearchMetadata.ColumnNames.ResearchTitle);
			}

			set
			{
				base.SetSystemString(PatientResearchMetadata.ColumnNames.ResearchTitle, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(PatientResearchMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(PatientResearchMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(PatientResearchMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(PatientResearchMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PatientResearchMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(PatientResearchMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PatientResearchMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PatientResearchMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientResearchMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientResearchMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientResearch.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientResearchMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientResearchMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPatientResearch entity)
			{
				this.entity = entity;
			}
			public System.String ResearchID
			{
				get
				{
					System.Int32? data = entity.ResearchID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResearchID = null;
					else entity.ResearchID = Convert.ToInt32(value);
				}
			}
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
			public System.String ResearchTitle
			{
				get
				{
					System.String data = entity.ResearchTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResearchTitle = null;
					else entity.ResearchTitle = Convert.ToString(value);
				}
			}
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
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
			private esPatientResearch entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientResearchQuery query)
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
				throw new Exception("esPatientResearch can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientResearch : esPatientResearch
	{
	}

	[Serializable]
	abstract public class esPatientResearchQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientResearchMetadata.Meta();
			}
		}

		public esQueryItem ResearchID
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.ResearchID, esSystemType.Int32);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem ResearchTitle
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.ResearchTitle, esSystemType.String);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientResearchMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientResearchCollection")]
	public partial class PatientResearchCollection : esPatientResearchCollection, IEnumerable<PatientResearch>
	{
		public PatientResearchCollection()
		{

		}

		public static implicit operator List<PatientResearch>(PatientResearchCollection coll)
		{
			List<PatientResearch> list = new List<PatientResearch>();

			foreach (PatientResearch emp in coll)
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
				return PatientResearchMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientResearchQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientResearch(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientResearch();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientResearchQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientResearchQuery();
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
		public bool Load(PatientResearchQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientResearch AddNew()
		{
			PatientResearch entity = base.AddNewEntity() as PatientResearch;

			return entity;
		}
		public PatientResearch FindByPrimaryKey(Int32 researchID, String patientID)
		{
			return base.FindByPrimaryKey(researchID, patientID) as PatientResearch;
		}

		#region IEnumerable< PatientResearch> Members

		IEnumerator<PatientResearch> IEnumerable<PatientResearch>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientResearch;
			}
		}

		#endregion

		private PatientResearchQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientResearch' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientResearch ({ResearchID, PatientID})")]
	[Serializable]
	public partial class PatientResearch : esPatientResearch
	{
		public PatientResearch()
		{
		}

		public PatientResearch(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientResearchMetadata.Meta();
			}
		}

		override protected esPatientResearchQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientResearchQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientResearchQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientResearchQuery();
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
		public bool Load(PatientResearchQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientResearchQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientResearchQuery : esPatientResearchQuery
	{
		public PatientResearchQuery()
		{

		}

		public PatientResearchQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientResearchQuery";
		}
	}

	[Serializable]
	public partial class PatientResearchMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientResearchMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.ResearchID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PatientResearchMetadata.PropertyNames.ResearchID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.PatientID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientResearchMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.ResearchTitle, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientResearchMetadata.PropertyNames.ResearchTitle;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientResearchMetadata.PropertyNames.StartDate;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientResearchMetadata.PropertyNames.EndDate;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.ParamedicID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientResearchMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientResearchMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2000;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientResearchMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientResearchMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientResearchMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientResearchMetadata Meta()
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
			public const string ResearchID = "ResearchID";
			public const string PatientID = "PatientID";
			public const string ResearchTitle = "ResearchTitle";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string ParamedicID = "ParamedicID";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ResearchID = "ResearchID";
			public const string PatientID = "PatientID";
			public const string ResearchTitle = "ResearchTitle";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string ParamedicID = "ParamedicID";
			public const string Notes = "Notes";
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
			lock (typeof(PatientResearchMetadata))
			{
				if (PatientResearchMetadata.mapDelegates == null)
				{
					PatientResearchMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientResearchMetadata.meta == null)
				{
					PatientResearchMetadata.meta = new PatientResearchMetadata();
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

				meta.AddTypeMap("ResearchID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResearchTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientResearch";
				meta.Destination = "PatientResearch";
				meta.spInsert = "proc_PatientResearchInsert";
				meta.spUpdate = "proc_PatientResearchUpdate";
				meta.spDelete = "proc_PatientResearchDelete";
				meta.spLoadAll = "proc_PatientResearchLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientResearchLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientResearchMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
