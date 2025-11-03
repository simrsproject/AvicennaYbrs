/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/30/2022 4:50:50 PM
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
	abstract public class esPersonalRecruitmentTestEvaluatorCollection : esEntityCollectionWAuditLog
	{
		public esPersonalRecruitmentTestEvaluatorCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalRecruitmentTestEvaluatorCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalRecruitmentTestEvaluatorQuery query)
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
			this.InitQuery(query as esPersonalRecruitmentTestEvaluatorQuery);
		}
		#endregion

		virtual public PersonalRecruitmentTestEvaluator DetachEntity(PersonalRecruitmentTestEvaluator entity)
		{
			return base.DetachEntity(entity) as PersonalRecruitmentTestEvaluator;
		}

		virtual public PersonalRecruitmentTestEvaluator AttachEntity(PersonalRecruitmentTestEvaluator entity)
		{
			return base.AttachEntity(entity) as PersonalRecruitmentTestEvaluator;
		}

		virtual public void Combine(PersonalRecruitmentTestEvaluatorCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalRecruitmentTestEvaluator this[int index]
		{
			get
			{
				return base[index] as PersonalRecruitmentTestEvaluator;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalRecruitmentTestEvaluator);
		}
	}

	[Serializable]
	abstract public class esPersonalRecruitmentTestEvaluator : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalRecruitmentTestEvaluatorQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalRecruitmentTestEvaluator()
		{
		}

		public esPersonalRecruitmentTestEvaluator(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalRecruitmentTestID, Int32 evaluatorID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalRecruitmentTestID, evaluatorID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalRecruitmentTestID, evaluatorID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalRecruitmentTestID, Int32 evaluatorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalRecruitmentTestID, evaluatorID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalRecruitmentTestID, evaluatorID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalRecruitmentTestID, Int32 evaluatorID)
		{
			esPersonalRecruitmentTestEvaluatorQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalRecruitmentTestID == personalRecruitmentTestID, query.EvaluatorID == evaluatorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalRecruitmentTestID, Int32 evaluatorID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalRecruitmentTestID", personalRecruitmentTestID);
			parms.Add("EvaluatorID", evaluatorID);
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
						case "PersonalRecruitmentTestID": this.str.PersonalRecruitmentTestID = (string)value; break;
						case "EvaluatorID": this.str.EvaluatorID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "Score": this.str.Score = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalRecruitmentTestID":

							if (value == null || value is System.Int32)
								this.PersonalRecruitmentTestID = (System.Int32?)value;
							break;
						case "EvaluatorID":

							if (value == null || value is System.Int32)
								this.EvaluatorID = (System.Int32?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "Score":

							if (value == null || value is System.Decimal)
								this.Score = (System.Decimal?)value;
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
		/// Maps to PersonalRecruitmentTestEvaluator.PersonalRecruitmentTestID
		/// </summary>
		virtual public System.Int32? PersonalRecruitmentTestID
		{
			get
			{
				return base.GetSystemInt32(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PersonalRecruitmentTestID);
			}

			set
			{
				base.SetSystemInt32(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PersonalRecruitmentTestID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestEvaluator.EvaluatorID
		/// </summary>
		virtual public System.Int32? EvaluatorID
		{
			get
			{
				return base.GetSystemInt32(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID);
			}

			set
			{
				base.SetSystemInt32(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestEvaluator.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestEvaluator.Score
		/// </summary>
		virtual public System.Decimal? Score
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.Score);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.Score, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestEvaluator.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestEvaluator.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonalRecruitmentTestEvaluator entity)
			{
				this.entity = entity;
			}
			public System.String PersonalRecruitmentTestID
			{
				get
				{
					System.Int32? data = entity.PersonalRecruitmentTestID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalRecruitmentTestID = null;
					else entity.PersonalRecruitmentTestID = Convert.ToInt32(value);
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
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
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
			private esPersonalRecruitmentTestEvaluator entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalRecruitmentTestEvaluatorQuery query)
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
				throw new Exception("esPersonalRecruitmentTestEvaluator can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalRecruitmentTestEvaluator : esPersonalRecruitmentTestEvaluator
	{
	}

	[Serializable]
	abstract public class esPersonalRecruitmentTestEvaluatorQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalRecruitmentTestEvaluatorMetadata.Meta();
			}
		}

		public esQueryItem PersonalRecruitmentTestID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PersonalRecruitmentTestID, esSystemType.Int32);
			}
		}

		public esQueryItem EvaluatorID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem Score
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.Score, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalRecruitmentTestEvaluatorCollection")]
	public partial class PersonalRecruitmentTestEvaluatorCollection : esPersonalRecruitmentTestEvaluatorCollection, IEnumerable<PersonalRecruitmentTestEvaluator>
	{
		public PersonalRecruitmentTestEvaluatorCollection()
		{

		}

		public static implicit operator List<PersonalRecruitmentTestEvaluator>(PersonalRecruitmentTestEvaluatorCollection coll)
		{
			List<PersonalRecruitmentTestEvaluator> list = new List<PersonalRecruitmentTestEvaluator>();

			foreach (PersonalRecruitmentTestEvaluator emp in coll)
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
				return PersonalRecruitmentTestEvaluatorMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalRecruitmentTestEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalRecruitmentTestEvaluator(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalRecruitmentTestEvaluator();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalRecruitmentTestEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalRecruitmentTestEvaluatorQuery();
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
		public bool Load(PersonalRecruitmentTestEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalRecruitmentTestEvaluator AddNew()
		{
			PersonalRecruitmentTestEvaluator entity = base.AddNewEntity() as PersonalRecruitmentTestEvaluator;

			return entity;
		}
		public PersonalRecruitmentTestEvaluator FindByPrimaryKey(Int32 personalRecruitmentTestID, Int32 evaluatorID)
		{
			return base.FindByPrimaryKey(personalRecruitmentTestID, evaluatorID) as PersonalRecruitmentTestEvaluator;
		}

		#region IEnumerable< PersonalRecruitmentTestEvaluator> Members

		IEnumerator<PersonalRecruitmentTestEvaluator> IEnumerable<PersonalRecruitmentTestEvaluator>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalRecruitmentTestEvaluator;
			}
		}

		#endregion

		private PersonalRecruitmentTestEvaluatorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalRecruitmentTestEvaluator' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalRecruitmentTestEvaluator ({PersonalRecruitmentTestID, EvaluatorID})")]
	[Serializable]
	public partial class PersonalRecruitmentTestEvaluator : esPersonalRecruitmentTestEvaluator
	{
		public PersonalRecruitmentTestEvaluator()
		{
		}

		public PersonalRecruitmentTestEvaluator(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalRecruitmentTestEvaluatorMetadata.Meta();
			}
		}

		override protected esPersonalRecruitmentTestEvaluatorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalRecruitmentTestEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalRecruitmentTestEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalRecruitmentTestEvaluatorQuery();
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
		public bool Load(PersonalRecruitmentTestEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalRecruitmentTestEvaluatorQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalRecruitmentTestEvaluatorQuery : esPersonalRecruitmentTestEvaluatorQuery
	{
		public PersonalRecruitmentTestEvaluatorQuery()
		{

		}

		public PersonalRecruitmentTestEvaluatorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalRecruitmentTestEvaluatorQuery";
		}
	}

	[Serializable]
	public partial class PersonalRecruitmentTestEvaluatorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalRecruitmentTestEvaluatorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PersonalRecruitmentTestID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalRecruitmentTestEvaluatorMetadata.PropertyNames.PersonalRecruitmentTestID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalRecruitmentTestEvaluatorMetadata.PropertyNames.EvaluatorID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalRecruitmentTestEvaluatorMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.Score, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestEvaluatorMetadata.PropertyNames.Score;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalRecruitmentTestEvaluatorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestEvaluatorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalRecruitmentTestEvaluatorMetadata Meta()
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
			public const string PersonalRecruitmentTestID = "PersonalRecruitmentTestID";
			public const string EvaluatorID = "EvaluatorID";
			public const string PositionID = "PositionID";
			public const string Score = "Score";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalRecruitmentTestID = "PersonalRecruitmentTestID";
			public const string EvaluatorID = "EvaluatorID";
			public const string PositionID = "PositionID";
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
			lock (typeof(PersonalRecruitmentTestEvaluatorMetadata))
			{
				if (PersonalRecruitmentTestEvaluatorMetadata.mapDelegates == null)
				{
					PersonalRecruitmentTestEvaluatorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalRecruitmentTestEvaluatorMetadata.meta == null)
				{
					PersonalRecruitmentTestEvaluatorMetadata.meta = new PersonalRecruitmentTestEvaluatorMetadata();
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

				meta.AddTypeMap("PersonalRecruitmentTestID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EvaluatorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Score", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalRecruitmentTestEvaluator";
				meta.Destination = "PersonalRecruitmentTestEvaluator";
				meta.spInsert = "proc_PersonalRecruitmentTestEvaluatorInsert";
				meta.spUpdate = "proc_PersonalRecruitmentTestEvaluatorUpdate";
				meta.spDelete = "proc_PersonalRecruitmentTestEvaluatorDelete";
				meta.spLoadAll = "proc_PersonalRecruitmentTestEvaluatorLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalRecruitmentTestEvaluatorLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalRecruitmentTestEvaluatorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
