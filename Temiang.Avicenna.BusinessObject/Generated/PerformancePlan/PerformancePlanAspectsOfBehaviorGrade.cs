/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2023 3:55:12 PM
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
	abstract public class esPerformancePlanAspectsOfBehaviorGradeCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanAspectsOfBehaviorGradeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanAspectsOfBehaviorGradeCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanAspectsOfBehaviorGradeQuery query)
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
			this.InitQuery(query as esPerformancePlanAspectsOfBehaviorGradeQuery);
		}
		#endregion

		virtual public PerformancePlanAspectsOfBehaviorGrade DetachEntity(PerformancePlanAspectsOfBehaviorGrade entity)
		{
			return base.DetachEntity(entity) as PerformancePlanAspectsOfBehaviorGrade;
		}

		virtual public PerformancePlanAspectsOfBehaviorGrade AttachEntity(PerformancePlanAspectsOfBehaviorGrade entity)
		{
			return base.AttachEntity(entity) as PerformancePlanAspectsOfBehaviorGrade;
		}

		virtual public void Combine(PerformancePlanAspectsOfBehaviorGradeCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanAspectsOfBehaviorGrade this[int index]
		{
			get
			{
				return base[index] as PerformancePlanAspectsOfBehaviorGrade;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanAspectsOfBehaviorGrade);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanAspectsOfBehaviorGrade : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanAspectsOfBehaviorGradeQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanAspectsOfBehaviorGrade()
		{
		}

		public esPerformancePlanAspectsOfBehaviorGrade(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 gradeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(gradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(gradeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 gradeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(gradeID);
			else
				return LoadByPrimaryKeyStoredProcedure(gradeID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 gradeID)
		{
			esPerformancePlanAspectsOfBehaviorGradeQuery query = this.GetDynamicQuery();
			query.Where(query.GradeID == gradeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 gradeID)
		{
			esParameters parms = new esParameters();
			parms.Add("GradeID", gradeID);
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
						case "GradeID": this.str.GradeID = (string)value; break;
						case "GradeValue": this.str.GradeValue = (string)value; break;
						case "Explanation": this.str.Explanation = (string)value; break;
						case "Categorization": this.str.Categorization = (string)value; break;
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
						case "GradeID":

							if (value == null || value is System.Int32)
								this.GradeID = (System.Int32?)value;
							break;
						case "GradeValue":

							if (value == null || value is System.Int32)
								this.GradeValue = (System.Int32?)value;
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
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.GradeID
		/// </summary>
		virtual public System.Int32? GradeID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.GradeValue
		/// </summary>
		virtual public System.Int32? GradeValue
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeValue);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeValue, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.Explanation
		/// </summary>
		virtual public System.String Explanation
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Explanation);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Explanation, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.Categorization
		/// </summary>
		virtual public System.String Categorization
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Categorization);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Categorization, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorGrade.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanAspectsOfBehaviorGrade entity)
			{
				this.entity = entity;
			}
			public System.String GradeID
			{
				get
				{
					System.Int32? data = entity.GradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GradeID = null;
					else entity.GradeID = Convert.ToInt32(value);
				}
			}
			public System.String GradeValue
			{
				get
				{
					System.Int32? data = entity.GradeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GradeValue = null;
					else entity.GradeValue = Convert.ToInt32(value);
				}
			}
			public System.String Explanation
			{
				get
				{
					System.String data = entity.Explanation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Explanation = null;
					else entity.Explanation = Convert.ToString(value);
				}
			}
			public System.String Categorization
			{
				get
				{
					System.String data = entity.Categorization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Categorization = null;
					else entity.Categorization = Convert.ToString(value);
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
			private esPerformancePlanAspectsOfBehaviorGrade entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanAspectsOfBehaviorGradeQuery query)
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
				throw new Exception("esPerformancePlanAspectsOfBehaviorGrade can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanAspectsOfBehaviorGrade : esPerformancePlanAspectsOfBehaviorGrade
	{
	}

	[Serializable]
	abstract public class esPerformancePlanAspectsOfBehaviorGradeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanAspectsOfBehaviorGradeMetadata.Meta();
			}
		}

		public esQueryItem GradeID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeID, esSystemType.Int32);
			}
		}

		public esQueryItem GradeValue
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeValue, esSystemType.Int32);
			}
		}

		public esQueryItem Explanation
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Explanation, esSystemType.String);
			}
		}

		public esQueryItem Categorization
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Categorization, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanAspectsOfBehaviorGradeCollection")]
	public partial class PerformancePlanAspectsOfBehaviorGradeCollection : esPerformancePlanAspectsOfBehaviorGradeCollection, IEnumerable<PerformancePlanAspectsOfBehaviorGrade>
	{
		public PerformancePlanAspectsOfBehaviorGradeCollection()
		{

		}

		public static implicit operator List<PerformancePlanAspectsOfBehaviorGrade>(PerformancePlanAspectsOfBehaviorGradeCollection coll)
		{
			List<PerformancePlanAspectsOfBehaviorGrade> list = new List<PerformancePlanAspectsOfBehaviorGrade>();

			foreach (PerformancePlanAspectsOfBehaviorGrade emp in coll)
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
				return PerformancePlanAspectsOfBehaviorGradeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanAspectsOfBehaviorGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanAspectsOfBehaviorGrade(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanAspectsOfBehaviorGrade();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanAspectsOfBehaviorGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanAspectsOfBehaviorGradeQuery();
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
		public bool Load(PerformancePlanAspectsOfBehaviorGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanAspectsOfBehaviorGrade AddNew()
		{
			PerformancePlanAspectsOfBehaviorGrade entity = base.AddNewEntity() as PerformancePlanAspectsOfBehaviorGrade;

			return entity;
		}
		public PerformancePlanAspectsOfBehaviorGrade FindByPrimaryKey(Int32 gradeID)
		{
			return base.FindByPrimaryKey(gradeID) as PerformancePlanAspectsOfBehaviorGrade;
		}

		#region IEnumerable< PerformancePlanAspectsOfBehaviorGrade> Members

		IEnumerator<PerformancePlanAspectsOfBehaviorGrade> IEnumerable<PerformancePlanAspectsOfBehaviorGrade>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanAspectsOfBehaviorGrade;
			}
		}

		#endregion

		private PerformancePlanAspectsOfBehaviorGradeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanAspectsOfBehaviorGrade' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanAspectsOfBehaviorGrade ({GradeID})")]
	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorGrade : esPerformancePlanAspectsOfBehaviorGrade
	{
		public PerformancePlanAspectsOfBehaviorGrade()
		{
		}

		public PerformancePlanAspectsOfBehaviorGrade(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanAspectsOfBehaviorGradeMetadata.Meta();
			}
		}

		override protected esPerformancePlanAspectsOfBehaviorGradeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanAspectsOfBehaviorGradeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanAspectsOfBehaviorGradeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanAspectsOfBehaviorGradeQuery();
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
		public bool Load(PerformancePlanAspectsOfBehaviorGradeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanAspectsOfBehaviorGradeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorGradeQuery : esPerformancePlanAspectsOfBehaviorGradeQuery
	{
		public PerformancePlanAspectsOfBehaviorGradeQuery()
		{

		}

		public PerformancePlanAspectsOfBehaviorGradeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanAspectsOfBehaviorGradeQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorGradeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanAspectsOfBehaviorGradeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.GradeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.GradeValue, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.GradeValue;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Explanation, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.Explanation;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.Categorization, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.Categorization;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorGradeMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorGradeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanAspectsOfBehaviorGradeMetadata Meta()
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
			public const string GradeID = "GradeID";
			public const string GradeValue = "GradeValue";
			public const string Explanation = "Explanation";
			public const string Categorization = "Categorization";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string GradeID = "GradeID";
			public const string GradeValue = "GradeValue";
			public const string Explanation = "Explanation";
			public const string Categorization = "Categorization";
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
			lock (typeof(PerformancePlanAspectsOfBehaviorGradeMetadata))
			{
				if (PerformancePlanAspectsOfBehaviorGradeMetadata.mapDelegates == null)
				{
					PerformancePlanAspectsOfBehaviorGradeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanAspectsOfBehaviorGradeMetadata.meta == null)
				{
					PerformancePlanAspectsOfBehaviorGradeMetadata.meta = new PerformancePlanAspectsOfBehaviorGradeMetadata();
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

				meta.AddTypeMap("GradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GradeValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Explanation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Categorization", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanAspectsOfBehaviorGrade";
				meta.Destination = "PerformancePlanAspectsOfBehaviorGrade";
				meta.spInsert = "proc_PerformancePlanAspectsOfBehaviorGradeInsert";
				meta.spUpdate = "proc_PerformancePlanAspectsOfBehaviorGradeUpdate";
				meta.spDelete = "proc_PerformancePlanAspectsOfBehaviorGradeDelete";
				meta.spLoadAll = "proc_PerformancePlanAspectsOfBehaviorGradeLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanAspectsOfBehaviorGradeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanAspectsOfBehaviorGradeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
