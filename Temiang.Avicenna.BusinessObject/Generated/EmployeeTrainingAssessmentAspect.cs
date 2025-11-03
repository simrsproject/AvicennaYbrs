/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/22/2023 4:24:13 PM
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
	abstract public class esEmployeeTrainingAssessmentAspectCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTrainingAssessmentAspectCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTrainingAssessmentAspectCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingAssessmentAspectQuery query)
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
			this.InitQuery(query as esEmployeeTrainingAssessmentAspectQuery);
		}
		#endregion

		virtual public EmployeeTrainingAssessmentAspect DetachEntity(EmployeeTrainingAssessmentAspect entity)
		{
			return base.DetachEntity(entity) as EmployeeTrainingAssessmentAspect;
		}

		virtual public EmployeeTrainingAssessmentAspect AttachEntity(EmployeeTrainingAssessmentAspect entity)
		{
			return base.AttachEntity(entity) as EmployeeTrainingAssessmentAspect;
		}

		virtual public void Combine(EmployeeTrainingAssessmentAspectCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTrainingAssessmentAspect this[int index]
		{
			get
			{
				return base[index] as EmployeeTrainingAssessmentAspect;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTrainingAssessmentAspect);
		}
	}

	[Serializable]
	abstract public class esEmployeeTrainingAssessmentAspect : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTrainingAssessmentAspectQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTrainingAssessmentAspect()
		{
		}

		public esEmployeeTrainingAssessmentAspect(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String assessmentAspectID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assessmentAspectID);
			else
				return LoadByPrimaryKeyStoredProcedure(assessmentAspectID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String assessmentAspectID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assessmentAspectID);
			else
				return LoadByPrimaryKeyStoredProcedure(assessmentAspectID);
		}

		private bool LoadByPrimaryKeyDynamic(String assessmentAspectID)
		{
			esEmployeeTrainingAssessmentAspectQuery query = this.GetDynamicQuery();
			query.Where(query.AssessmentAspectID == assessmentAspectID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String assessmentAspectID)
		{
			esParameters parms = new esParameters();
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
						case "AssessmentAspectID": this.str.AssessmentAspectID = (string)value; break;
						case "AssessmentAspectName": this.str.AssessmentAspectName = (string)value; break;
						case "MinValue": this.str.MinValue = (string)value; break;
						case "MaxValue": this.str.MaxValue = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
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
						case "MinValue":

							if (value == null || value is System.Decimal)
								this.MinValue = (System.Decimal?)value;
							break;
						case "MaxValue":

							if (value == null || value is System.Decimal)
								this.MaxValue = (System.Decimal?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to EmployeeTrainingAssessmentAspect.AssessmentAspectID
		/// </summary>
		virtual public System.String AssessmentAspectID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.AssessmentAspectName
		/// </summary>
		virtual public System.String AssessmentAspectName
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectName);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.MinValue
		/// </summary>
		virtual public System.Decimal? MinValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MinValue);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MinValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.MaxValue
		/// </summary>
		virtual public System.Decimal? MaxValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MaxValue);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MaxValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentAspect.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeTrainingAssessmentAspect entity)
			{
				this.entity = entity;
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
			public System.String AssessmentAspectName
			{
				get
				{
					System.String data = entity.AssessmentAspectName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssessmentAspectName = null;
					else entity.AssessmentAspectName = Convert.ToString(value);
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
			private esEmployeeTrainingAssessmentAspect entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingAssessmentAspectQuery query)
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
				throw new Exception("esEmployeeTrainingAssessmentAspect can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTrainingAssessmentAspect : esEmployeeTrainingAssessmentAspect
	{
	}

	[Serializable]
	abstract public class esEmployeeTrainingAssessmentAspectQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingAssessmentAspectMetadata.Meta();
			}
		}

		public esQueryItem AssessmentAspectID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectID, esSystemType.String);
			}
		}

		public esQueryItem AssessmentAspectName
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectName, esSystemType.String);
			}
		}

		public esQueryItem MinValue
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MinValue, esSystemType.Decimal);
			}
		}

		public esQueryItem MaxValue
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MaxValue, esSystemType.Decimal);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTrainingAssessmentAspectCollection")]
	public partial class EmployeeTrainingAssessmentAspectCollection : esEmployeeTrainingAssessmentAspectCollection, IEnumerable<EmployeeTrainingAssessmentAspect>
	{
		public EmployeeTrainingAssessmentAspectCollection()
		{

		}

		public static implicit operator List<EmployeeTrainingAssessmentAspect>(EmployeeTrainingAssessmentAspectCollection coll)
		{
			List<EmployeeTrainingAssessmentAspect> list = new List<EmployeeTrainingAssessmentAspect>();

			foreach (EmployeeTrainingAssessmentAspect emp in coll)
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
				return EmployeeTrainingAssessmentAspectMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingAssessmentAspectQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTrainingAssessmentAspect(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTrainingAssessmentAspect();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingAssessmentAspectQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingAssessmentAspectQuery();
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
		public bool Load(EmployeeTrainingAssessmentAspectQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTrainingAssessmentAspect AddNew()
		{
			EmployeeTrainingAssessmentAspect entity = base.AddNewEntity() as EmployeeTrainingAssessmentAspect;

			return entity;
		}
		public EmployeeTrainingAssessmentAspect FindByPrimaryKey(String assessmentAspectID)
		{
			return base.FindByPrimaryKey(assessmentAspectID) as EmployeeTrainingAssessmentAspect;
		}

		#region IEnumerable< EmployeeTrainingAssessmentAspect> Members

		IEnumerator<EmployeeTrainingAssessmentAspect> IEnumerable<EmployeeTrainingAssessmentAspect>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTrainingAssessmentAspect;
			}
		}

		#endregion

		private EmployeeTrainingAssessmentAspectQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTrainingAssessmentAspect' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTrainingAssessmentAspect ({AssessmentAspectID})")]
	[Serializable]
	public partial class EmployeeTrainingAssessmentAspect : esEmployeeTrainingAssessmentAspect
	{
		public EmployeeTrainingAssessmentAspect()
		{
		}

		public EmployeeTrainingAssessmentAspect(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingAssessmentAspectMetadata.Meta();
			}
		}

		override protected esEmployeeTrainingAssessmentAspectQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingAssessmentAspectQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingAssessmentAspectQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingAssessmentAspectQuery();
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
		public bool Load(EmployeeTrainingAssessmentAspectQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTrainingAssessmentAspectQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTrainingAssessmentAspectQuery : esEmployeeTrainingAssessmentAspectQuery
	{
		public EmployeeTrainingAssessmentAspectQuery()
		{

		}

		public EmployeeTrainingAssessmentAspectQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTrainingAssessmentAspectQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTrainingAssessmentAspectMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTrainingAssessmentAspectMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.AssessmentAspectID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.AssessmentAspectName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.AssessmentAspectName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MinValue, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.MinValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.MaxValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.MaxValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentAspectMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentAspectMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeTrainingAssessmentAspectMetadata Meta()
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
			public const string AssessmentAspectID = "AssessmentAspectID";
			public const string AssessmentAspectName = "AssessmentAspectName";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
			public const string IsActive = "IsActive";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AssessmentAspectID = "AssessmentAspectID";
			public const string AssessmentAspectName = "AssessmentAspectName";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
			public const string IsActive = "IsActive";
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
			lock (typeof(EmployeeTrainingAssessmentAspectMetadata))
			{
				if (EmployeeTrainingAssessmentAspectMetadata.mapDelegates == null)
				{
					EmployeeTrainingAssessmentAspectMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTrainingAssessmentAspectMetadata.meta == null)
				{
					EmployeeTrainingAssessmentAspectMetadata.meta = new EmployeeTrainingAssessmentAspectMetadata();
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

				meta.AddTypeMap("AssessmentAspectID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssessmentAspectName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeTrainingAssessmentAspect";
				meta.Destination = "EmployeeTrainingAssessmentAspect";
				meta.spInsert = "proc_EmployeeTrainingAssessmentAspectInsert";
				meta.spUpdate = "proc_EmployeeTrainingAssessmentAspectUpdate";
				meta.spDelete = "proc_EmployeeTrainingAssessmentAspectDelete";
				meta.spLoadAll = "proc_EmployeeTrainingAssessmentAspectLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTrainingAssessmentAspectLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTrainingAssessmentAspectMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
