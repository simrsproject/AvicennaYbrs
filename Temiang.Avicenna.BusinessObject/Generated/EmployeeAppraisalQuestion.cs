/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2022 5:00:38 PM
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
	abstract public class esEmployeeAppraisalQuestionCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeAppraisalQuestionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeAppraisalQuestionCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeAppraisalQuestionQuery query)
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
			this.InitQuery(query as esEmployeeAppraisalQuestionQuery);
		}
		#endregion

		virtual public EmployeeAppraisalQuestion DetachEntity(EmployeeAppraisalQuestion entity)
		{
			return base.DetachEntity(entity) as EmployeeAppraisalQuestion;
		}

		virtual public EmployeeAppraisalQuestion AttachEntity(EmployeeAppraisalQuestion entity)
		{
			return base.AttachEntity(entity) as EmployeeAppraisalQuestion;
		}

		virtual public void Combine(EmployeeAppraisalQuestionCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeAppraisalQuestion this[int index]
		{
			get
			{
				return base[index] as EmployeeAppraisalQuestion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeAppraisalQuestion);
		}
	}

	[Serializable]
	abstract public class esEmployeeAppraisalQuestion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeAppraisalQuestionQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeAppraisalQuestion()
		{
		}

		public esEmployeeAppraisalQuestion(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeAppraisalQuestionerID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeAppraisalQuestionerID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeAppraisalQuestionerID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeAppraisalQuestionerID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeAppraisalQuestionerID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeAppraisalQuestionerID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeAppraisalQuestionerID)
		{
			esEmployeeAppraisalQuestionQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeAppraisalQuestionerID == employeeAppraisalQuestionerID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeAppraisalQuestionerID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeAppraisalQuestionerID", employeeAppraisalQuestionerID);
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
						case "EmployeeAppraisalQuestionerID": this.str.EmployeeAppraisalQuestionerID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "QuestionerID": this.str.QuestionerID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeAppraisalQuestionerID":

							if (value == null || value is System.Int32)
								this.EmployeeAppraisalQuestionerID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "QuestionerID":

							if (value == null || value is System.Int32)
								this.QuestionerID = (System.Int32?)value;
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
		/// Maps to EmployeeAppraisalQuestion.EmployeeAppraisalQuestionerID
		/// </summary>
		virtual public System.Int32? EmployeeAppraisalQuestionerID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID);
			}

			set
			{
				base.SetSystemInt32(EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAppraisalQuestion.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAppraisalQuestionMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeAppraisalQuestionMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAppraisalQuestion.QuestionerID
		/// </summary>
		virtual public System.Int32? QuestionerID
		{
			get
			{
				return base.GetSystemInt32(EmployeeAppraisalQuestionMetadata.ColumnNames.QuestionerID);
			}

			set
			{
				base.SetSystemInt32(EmployeeAppraisalQuestionMetadata.ColumnNames.QuestionerID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAppraisalQuestion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeAppraisalQuestion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeAppraisalQuestion entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeAppraisalQuestionerID
			{
				get
				{
					System.Int32? data = entity.EmployeeAppraisalQuestionerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeAppraisalQuestionerID = null;
					else entity.EmployeeAppraisalQuestionerID = Convert.ToInt32(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
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
			private esEmployeeAppraisalQuestion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeAppraisalQuestionQuery query)
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
				throw new Exception("esEmployeeAppraisalQuestion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeAppraisalQuestion : esEmployeeAppraisalQuestion
	{
	}

	[Serializable]
	abstract public class esEmployeeAppraisalQuestionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeAppraisalQuestionMetadata.Meta();
			}
		}

		public esQueryItem EmployeeAppraisalQuestionerID
		{
			get
			{
				return new esQueryItem(this, EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeAppraisalQuestionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionerID
		{
			get
			{
				return new esQueryItem(this, EmployeeAppraisalQuestionMetadata.ColumnNames.QuestionerID, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeAppraisalQuestionCollection")]
	public partial class EmployeeAppraisalQuestionCollection : esEmployeeAppraisalQuestionCollection, IEnumerable<EmployeeAppraisalQuestion>
	{
		public EmployeeAppraisalQuestionCollection()
		{

		}

		public static implicit operator List<EmployeeAppraisalQuestion>(EmployeeAppraisalQuestionCollection coll)
		{
			List<EmployeeAppraisalQuestion> list = new List<EmployeeAppraisalQuestion>();

			foreach (EmployeeAppraisalQuestion emp in coll)
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
				return EmployeeAppraisalQuestionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeAppraisalQuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeAppraisalQuestion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeAppraisalQuestion();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeAppraisalQuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeAppraisalQuestionQuery();
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
		public bool Load(EmployeeAppraisalQuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeAppraisalQuestion AddNew()
		{
			EmployeeAppraisalQuestion entity = base.AddNewEntity() as EmployeeAppraisalQuestion;

			return entity;
		}
		public EmployeeAppraisalQuestion FindByPrimaryKey(Int32 employeeAppraisalQuestionerID)
		{
			return base.FindByPrimaryKey(employeeAppraisalQuestionerID) as EmployeeAppraisalQuestion;
		}

		#region IEnumerable< EmployeeAppraisalQuestion> Members

		IEnumerator<EmployeeAppraisalQuestion> IEnumerable<EmployeeAppraisalQuestion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeAppraisalQuestion;
			}
		}

		#endregion

		private EmployeeAppraisalQuestionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeAppraisalQuestion' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeAppraisalQuestion ({EmployeeAppraisalQuestionerID})")]
	[Serializable]
	public partial class EmployeeAppraisalQuestion : esEmployeeAppraisalQuestion
	{
		public EmployeeAppraisalQuestion()
		{
		}

		public EmployeeAppraisalQuestion(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeAppraisalQuestionMetadata.Meta();
			}
		}

		override protected esEmployeeAppraisalQuestionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeAppraisalQuestionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeAppraisalQuestionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeAppraisalQuestionQuery();
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
		public bool Load(EmployeeAppraisalQuestionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeAppraisalQuestionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeAppraisalQuestionQuery : esEmployeeAppraisalQuestionQuery
	{
		public EmployeeAppraisalQuestionQuery()
		{

		}

		public EmployeeAppraisalQuestionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeAppraisalQuestionQuery";
		}
	}

	[Serializable]
	public partial class EmployeeAppraisalQuestionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeAppraisalQuestionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeAppraisalQuestionMetadata.ColumnNames.EmployeeAppraisalQuestionerID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAppraisalQuestionMetadata.PropertyNames.EmployeeAppraisalQuestionerID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAppraisalQuestionMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAppraisalQuestionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAppraisalQuestionMetadata.ColumnNames.QuestionerID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeAppraisalQuestionMetadata.PropertyNames.QuestionerID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeAppraisalQuestionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeAppraisalQuestionMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeAppraisalQuestionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeAppraisalQuestionMetadata Meta()
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
			public const string EmployeeAppraisalQuestionerID = "EmployeeAppraisalQuestionerID";
			public const string PersonID = "PersonID";
			public const string QuestionerID = "QuestionerID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeAppraisalQuestionerID = "EmployeeAppraisalQuestionerID";
			public const string PersonID = "PersonID";
			public const string QuestionerID = "QuestionerID";
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
			lock (typeof(EmployeeAppraisalQuestionMetadata))
			{
				if (EmployeeAppraisalQuestionMetadata.mapDelegates == null)
				{
					EmployeeAppraisalQuestionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeAppraisalQuestionMetadata.meta == null)
				{
					EmployeeAppraisalQuestionMetadata.meta = new EmployeeAppraisalQuestionMetadata();
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

				meta.AddTypeMap("EmployeeAppraisalQuestionerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeAppraisalQuestion";
				meta.Destination = "EmployeeAppraisalQuestion";
				meta.spInsert = "proc_EmployeeAppraisalQuestionInsert";
				meta.spUpdate = "proc_EmployeeAppraisalQuestionUpdate";
				meta.spDelete = "proc_EmployeeAppraisalQuestionDelete";
				meta.spLoadAll = "proc_EmployeeAppraisalQuestionLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeAppraisalQuestionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeAppraisalQuestionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
