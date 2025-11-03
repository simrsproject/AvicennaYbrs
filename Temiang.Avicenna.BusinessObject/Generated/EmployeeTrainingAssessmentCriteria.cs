/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/22/2023 4:24:50 PM
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
	abstract public class esEmployeeTrainingAssessmentCriteriaCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTrainingAssessmentCriteriaCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTrainingAssessmentCriteriaCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingAssessmentCriteriaQuery query)
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
			this.InitQuery(query as esEmployeeTrainingAssessmentCriteriaQuery);
		}
		#endregion

		virtual public EmployeeTrainingAssessmentCriteria DetachEntity(EmployeeTrainingAssessmentCriteria entity)
		{
			return base.DetachEntity(entity) as EmployeeTrainingAssessmentCriteria;
		}

		virtual public EmployeeTrainingAssessmentCriteria AttachEntity(EmployeeTrainingAssessmentCriteria entity)
		{
			return base.AttachEntity(entity) as EmployeeTrainingAssessmentCriteria;
		}

		virtual public void Combine(EmployeeTrainingAssessmentCriteriaCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTrainingAssessmentCriteria this[int index]
		{
			get
			{
				return base[index] as EmployeeTrainingAssessmentCriteria;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTrainingAssessmentCriteria);
		}
	}

	[Serializable]
	abstract public class esEmployeeTrainingAssessmentCriteria : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTrainingAssessmentCriteriaQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTrainingAssessmentCriteria()
		{
		}

		public esEmployeeTrainingAssessmentCriteria(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 assessmentCriteriaID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assessmentCriteriaID);
			else
				return LoadByPrimaryKeyStoredProcedure(assessmentCriteriaID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 assessmentCriteriaID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assessmentCriteriaID);
			else
				return LoadByPrimaryKeyStoredProcedure(assessmentCriteriaID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 assessmentCriteriaID)
		{
			esEmployeeTrainingAssessmentCriteriaQuery query = this.GetDynamicQuery();
			query.Where(query.AssessmentCriteriaID == assessmentCriteriaID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 assessmentCriteriaID)
		{
			esParameters parms = new esParameters();
			parms.Add("AssessmentCriteriaID", assessmentCriteriaID);
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
						case "AssessmentCriteriaID": this.str.AssessmentCriteriaID = (string)value; break;
						case "AssessmentCriteriaName": this.str.AssessmentCriteriaName = (string)value; break;
						case "Recommendation": this.str.Recommendation = (string)value; break;
						case "MinValue": this.str.MinValue = (string)value; break;
						case "MaxValue": this.str.MaxValue = (string)value; break;
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
						case "AssessmentCriteriaID":

							if (value == null || value is System.Int32)
								this.AssessmentCriteriaID = (System.Int32?)value;
							break;
						case "MinValue":

							if (value == null || value is System.Decimal)
								this.MinValue = (System.Decimal?)value;
							break;
						case "MaxValue":

							if (value == null || value is System.Decimal)
								this.MaxValue = (System.Decimal?)value;
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
		/// Maps to EmployeeTrainingAssessmentCriteria.AssessmentCriteriaID
		/// </summary>
		virtual public System.Int32? AssessmentCriteriaID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.AssessmentCriteriaName
		/// </summary>
		virtual public System.String AssessmentCriteriaName
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaName);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.Recommendation
		/// </summary>
		virtual public System.String Recommendation
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.Recommendation);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.Recommendation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.MinValue
		/// </summary>
		virtual public System.Decimal? MinValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MinValue);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MinValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.MaxValue
		/// </summary>
		virtual public System.Decimal? MaxValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MaxValue);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MaxValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingAssessmentCriteria.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeTrainingAssessmentCriteria entity)
			{
				this.entity = entity;
			}
			public System.String AssessmentCriteriaID
			{
				get
				{
					System.Int32? data = entity.AssessmentCriteriaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssessmentCriteriaID = null;
					else entity.AssessmentCriteriaID = Convert.ToInt32(value);
				}
			}
			public System.String AssessmentCriteriaName
			{
				get
				{
					System.String data = entity.AssessmentCriteriaName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssessmentCriteriaName = null;
					else entity.AssessmentCriteriaName = Convert.ToString(value);
				}
			}
			public System.String Recommendation
			{
				get
				{
					System.String data = entity.Recommendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Recommendation = null;
					else entity.Recommendation = Convert.ToString(value);
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
			private esEmployeeTrainingAssessmentCriteria entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingAssessmentCriteriaQuery query)
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
				throw new Exception("esEmployeeTrainingAssessmentCriteria can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTrainingAssessmentCriteria : esEmployeeTrainingAssessmentCriteria
	{
	}

	[Serializable]
	abstract public class esEmployeeTrainingAssessmentCriteriaQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingAssessmentCriteriaMetadata.Meta();
			}
		}

		public esQueryItem AssessmentCriteriaID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaID, esSystemType.Int32);
			}
		}

		public esQueryItem AssessmentCriteriaName
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaName, esSystemType.String);
			}
		}

		public esQueryItem Recommendation
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.Recommendation, esSystemType.String);
			}
		}

		public esQueryItem MinValue
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MinValue, esSystemType.Decimal);
			}
		}

		public esQueryItem MaxValue
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MaxValue, esSystemType.Decimal);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTrainingAssessmentCriteriaCollection")]
	public partial class EmployeeTrainingAssessmentCriteriaCollection : esEmployeeTrainingAssessmentCriteriaCollection, IEnumerable<EmployeeTrainingAssessmentCriteria>
	{
		public EmployeeTrainingAssessmentCriteriaCollection()
		{

		}

		public static implicit operator List<EmployeeTrainingAssessmentCriteria>(EmployeeTrainingAssessmentCriteriaCollection coll)
		{
			List<EmployeeTrainingAssessmentCriteria> list = new List<EmployeeTrainingAssessmentCriteria>();

			foreach (EmployeeTrainingAssessmentCriteria emp in coll)
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
				return EmployeeTrainingAssessmentCriteriaMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingAssessmentCriteriaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTrainingAssessmentCriteria(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTrainingAssessmentCriteria();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingAssessmentCriteriaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingAssessmentCriteriaQuery();
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
		public bool Load(EmployeeTrainingAssessmentCriteriaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTrainingAssessmentCriteria AddNew()
		{
			EmployeeTrainingAssessmentCriteria entity = base.AddNewEntity() as EmployeeTrainingAssessmentCriteria;

			return entity;
		}
		public EmployeeTrainingAssessmentCriteria FindByPrimaryKey(Int32 assessmentCriteriaID)
		{
			return base.FindByPrimaryKey(assessmentCriteriaID) as EmployeeTrainingAssessmentCriteria;
		}

		#region IEnumerable< EmployeeTrainingAssessmentCriteria> Members

		IEnumerator<EmployeeTrainingAssessmentCriteria> IEnumerable<EmployeeTrainingAssessmentCriteria>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTrainingAssessmentCriteria;
			}
		}

		#endregion

		private EmployeeTrainingAssessmentCriteriaQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTrainingAssessmentCriteria' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTrainingAssessmentCriteria ({AssessmentCriteriaID})")]
	[Serializable]
	public partial class EmployeeTrainingAssessmentCriteria : esEmployeeTrainingAssessmentCriteria
	{
		public EmployeeTrainingAssessmentCriteria()
		{
		}

		public EmployeeTrainingAssessmentCriteria(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingAssessmentCriteriaMetadata.Meta();
			}
		}

		override protected esEmployeeTrainingAssessmentCriteriaQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingAssessmentCriteriaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingAssessmentCriteriaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingAssessmentCriteriaQuery();
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
		public bool Load(EmployeeTrainingAssessmentCriteriaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTrainingAssessmentCriteriaQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTrainingAssessmentCriteriaQuery : esEmployeeTrainingAssessmentCriteriaQuery
	{
		public EmployeeTrainingAssessmentCriteriaQuery()
		{

		}

		public EmployeeTrainingAssessmentCriteriaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTrainingAssessmentCriteriaQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTrainingAssessmentCriteriaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTrainingAssessmentCriteriaMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.AssessmentCriteriaID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.AssessmentCriteriaName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.AssessmentCriteriaName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.Recommendation, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.Recommendation;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MinValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.MinValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.MaxValue, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.MaxValue;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingAssessmentCriteriaMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingAssessmentCriteriaMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeTrainingAssessmentCriteriaMetadata Meta()
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
			public const string AssessmentCriteriaID = "AssessmentCriteriaID";
			public const string AssessmentCriteriaName = "AssessmentCriteriaName";
			public const string Recommendation = "Recommendation";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AssessmentCriteriaID = "AssessmentCriteriaID";
			public const string AssessmentCriteriaName = "AssessmentCriteriaName";
			public const string Recommendation = "Recommendation";
			public const string MinValue = "MinValue";
			public const string MaxValue = "MaxValue";
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
			lock (typeof(EmployeeTrainingAssessmentCriteriaMetadata))
			{
				if (EmployeeTrainingAssessmentCriteriaMetadata.mapDelegates == null)
				{
					EmployeeTrainingAssessmentCriteriaMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTrainingAssessmentCriteriaMetadata.meta == null)
				{
					EmployeeTrainingAssessmentCriteriaMetadata.meta = new EmployeeTrainingAssessmentCriteriaMetadata();
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

				meta.AddTypeMap("AssessmentCriteriaID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssessmentCriteriaName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Recommendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeTrainingAssessmentCriteria";
				meta.Destination = "EmployeeTrainingAssessmentCriteria";
				meta.spInsert = "proc_EmployeeTrainingAssessmentCriteriaInsert";
				meta.spUpdate = "proc_EmployeeTrainingAssessmentCriteriaUpdate";
				meta.spDelete = "proc_EmployeeTrainingAssessmentCriteriaDelete";
				meta.spLoadAll = "proc_EmployeeTrainingAssessmentCriteriaLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTrainingAssessmentCriteriaLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTrainingAssessmentCriteriaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
