/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/28/2021 9:45:03 AM
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
	abstract public class esPatientIncidentCauseAnalysisCollection : esEntityCollectionWAuditLog
	{
		public esPatientIncidentCauseAnalysisCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientIncidentCauseAnalysisCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientIncidentCauseAnalysisQuery query)
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
			this.InitQuery(query as esPatientIncidentCauseAnalysisQuery);
		}
		#endregion

		virtual public PatientIncidentCauseAnalysis DetachEntity(PatientIncidentCauseAnalysis entity)
		{
			return base.DetachEntity(entity) as PatientIncidentCauseAnalysis;
		}

		virtual public PatientIncidentCauseAnalysis AttachEntity(PatientIncidentCauseAnalysis entity)
		{
			return base.AttachEntity(entity) as PatientIncidentCauseAnalysis;
		}

		virtual public void Combine(PatientIncidentCauseAnalysisCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientIncidentCauseAnalysis this[int index]
		{
			get
			{
				return base[index] as PatientIncidentCauseAnalysis;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientIncidentCauseAnalysis);
		}
	}

	[Serializable]
	abstract public class esPatientIncidentCauseAnalysis : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientIncidentCauseAnalysisQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientIncidentCauseAnalysis()
		{
		}

		public esPatientIncidentCauseAnalysis(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String patientIncidentNo, String sRIncidentCauseAnalysis)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientIncidentNo, sRIncidentCauseAnalysis);
			else
				return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRIncidentCauseAnalysis);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientIncidentNo, String sRIncidentCauseAnalysis)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientIncidentNo, sRIncidentCauseAnalysis);
			else
				return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRIncidentCauseAnalysis);
		}

		private bool LoadByPrimaryKeyDynamic(String patientIncidentNo, String sRIncidentCauseAnalysis)
		{
			esPatientIncidentCauseAnalysisQuery query = this.GetDynamicQuery();
			query.Where(query.PatientIncidentNo == patientIncidentNo, query.SRIncidentCauseAnalysis == sRIncidentCauseAnalysis);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String patientIncidentNo, String sRIncidentCauseAnalysis)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientIncidentNo", patientIncidentNo);
			parms.Add("SRIncidentCauseAnalysis", sRIncidentCauseAnalysis);
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
						case "PatientIncidentNo": this.str.PatientIncidentNo = (string)value; break;
						case "SRIncidentCauseAnalysis": this.str.SRIncidentCauseAnalysis = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to PatientIncidentCauseAnalysis.PatientIncidentNo
		/// </summary>
		virtual public System.String PatientIncidentNo
		{
			get
			{
				return base.GetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.PatientIncidentNo);
			}

			set
			{
				base.SetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.PatientIncidentNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncidentCauseAnalysis.SRIncidentCauseAnalysis
		/// </summary>
		virtual public System.String SRIncidentCauseAnalysis
		{
			get
			{
				return base.GetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.SRIncidentCauseAnalysis);
			}

			set
			{
				base.SetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.SRIncidentCauseAnalysis, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncidentCauseAnalysis.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncidentCauseAnalysis.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientIncidentCauseAnalysis.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPatientIncidentCauseAnalysis entity)
			{
				this.entity = entity;
			}
			public System.String PatientIncidentNo
			{
				get
				{
					System.String data = entity.PatientIncidentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientIncidentNo = null;
					else entity.PatientIncidentNo = Convert.ToString(value);
				}
			}
			public System.String SRIncidentCauseAnalysis
			{
				get
				{
					System.String data = entity.SRIncidentCauseAnalysis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentCauseAnalysis = null;
					else entity.SRIncidentCauseAnalysis = Convert.ToString(value);
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
			private esPatientIncidentCauseAnalysis entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientIncidentCauseAnalysisQuery query)
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
				throw new Exception("esPatientIncidentCauseAnalysis can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientIncidentCauseAnalysis : esPatientIncidentCauseAnalysis
	{
	}

	[Serializable]
	abstract public class esPatientIncidentCauseAnalysisQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientIncidentCauseAnalysisMetadata.Meta();
			}
		}

		public esQueryItem PatientIncidentNo
		{
			get
			{
				return new esQueryItem(this, PatientIncidentCauseAnalysisMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
			}
		}

		public esQueryItem SRIncidentCauseAnalysis
		{
			get
			{
				return new esQueryItem(this, PatientIncidentCauseAnalysisMetadata.ColumnNames.SRIncidentCauseAnalysis, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PatientIncidentCauseAnalysisMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientIncidentCauseAnalysisCollection")]
	public partial class PatientIncidentCauseAnalysisCollection : esPatientIncidentCauseAnalysisCollection, IEnumerable<PatientIncidentCauseAnalysis>
	{
		public PatientIncidentCauseAnalysisCollection()
		{

		}

		public static implicit operator List<PatientIncidentCauseAnalysis>(PatientIncidentCauseAnalysisCollection coll)
		{
			List<PatientIncidentCauseAnalysis> list = new List<PatientIncidentCauseAnalysis>();

			foreach (PatientIncidentCauseAnalysis emp in coll)
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
				return PatientIncidentCauseAnalysisMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientIncidentCauseAnalysisQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientIncidentCauseAnalysis(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientIncidentCauseAnalysis();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientIncidentCauseAnalysisQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientIncidentCauseAnalysisQuery();
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
		public bool Load(PatientIncidentCauseAnalysisQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientIncidentCauseAnalysis AddNew()
		{
			PatientIncidentCauseAnalysis entity = base.AddNewEntity() as PatientIncidentCauseAnalysis;

			return entity;
		}
		public PatientIncidentCauseAnalysis FindByPrimaryKey(String patientIncidentNo, String sRIncidentCauseAnalysis)
		{
			return base.FindByPrimaryKey(patientIncidentNo, sRIncidentCauseAnalysis) as PatientIncidentCauseAnalysis;
		}

		#region IEnumerable< PatientIncidentCauseAnalysis> Members

		IEnumerator<PatientIncidentCauseAnalysis> IEnumerable<PatientIncidentCauseAnalysis>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientIncidentCauseAnalysis;
			}
		}

		#endregion

		private PatientIncidentCauseAnalysisQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientIncidentCauseAnalysis' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientIncidentCauseAnalysis ({PatientIncidentNo, SRIncidentCauseAnalysis})")]
	[Serializable]
	public partial class PatientIncidentCauseAnalysis : esPatientIncidentCauseAnalysis
	{
		public PatientIncidentCauseAnalysis()
		{
		}

		public PatientIncidentCauseAnalysis(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientIncidentCauseAnalysisMetadata.Meta();
			}
		}

		override protected esPatientIncidentCauseAnalysisQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientIncidentCauseAnalysisQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientIncidentCauseAnalysisQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientIncidentCauseAnalysisQuery();
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
		public bool Load(PatientIncidentCauseAnalysisQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientIncidentCauseAnalysisQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientIncidentCauseAnalysisQuery : esPatientIncidentCauseAnalysisQuery
	{
		public PatientIncidentCauseAnalysisQuery()
		{

		}

		public PatientIncidentCauseAnalysisQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientIncidentCauseAnalysisQuery";
		}
	}

	[Serializable]
	public partial class PatientIncidentCauseAnalysisMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientIncidentCauseAnalysisMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientIncidentCauseAnalysisMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentCauseAnalysisMetadata.PropertyNames.PatientIncidentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentCauseAnalysisMetadata.ColumnNames.SRIncidentCauseAnalysis, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentCauseAnalysisMetadata.PropertyNames.SRIncidentCauseAnalysis;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentCauseAnalysisMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentCauseAnalysisMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentCauseAnalysisMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientIncidentCauseAnalysisMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentCauseAnalysisMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientIncidentCauseAnalysisMetadata Meta()
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
			public const string PatientIncidentNo = "PatientIncidentNo";
			public const string SRIncidentCauseAnalysis = "SRIncidentCauseAnalysis";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PatientIncidentNo = "PatientIncidentNo";
			public const string SRIncidentCauseAnalysis = "SRIncidentCauseAnalysis";
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
			lock (typeof(PatientIncidentCauseAnalysisMetadata))
			{
				if (PatientIncidentCauseAnalysisMetadata.mapDelegates == null)
				{
					PatientIncidentCauseAnalysisMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientIncidentCauseAnalysisMetadata.meta == null)
				{
					PatientIncidentCauseAnalysisMetadata.meta = new PatientIncidentCauseAnalysisMetadata();
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

				meta.AddTypeMap("PatientIncidentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentCauseAnalysis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientIncidentCauseAnalysis";
				meta.Destination = "PatientIncidentCauseAnalysis";
				meta.spInsert = "proc_PatientIncidentCauseAnalysisInsert";
				meta.spUpdate = "proc_PatientIncidentCauseAnalysisUpdate";
				meta.spDelete = "proc_PatientIncidentCauseAnalysisDelete";
				meta.spLoadAll = "proc_PatientIncidentCauseAnalysisLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientIncidentCauseAnalysisLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientIncidentCauseAnalysisMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
