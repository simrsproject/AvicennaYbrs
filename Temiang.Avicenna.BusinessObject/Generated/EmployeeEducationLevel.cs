/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/5/2020 10:46:25 AM
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
	abstract public class esEmployeeEducationLevelCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeEducationLevelCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeEducationLevelCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeEducationLevelQuery query)
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
			this.InitQuery(query as esEmployeeEducationLevelQuery);
		}
		#endregion

		virtual public EmployeeEducationLevel DetachEntity(EmployeeEducationLevel entity)
		{
			return base.DetachEntity(entity) as EmployeeEducationLevel;
		}

		virtual public EmployeeEducationLevel AttachEntity(EmployeeEducationLevel entity)
		{
			return base.AttachEntity(entity) as EmployeeEducationLevel;
		}

		virtual public void Combine(EmployeeEducationLevelCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeEducationLevel this[int index]
		{
			get
			{
				return base[index] as EmployeeEducationLevel;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeEducationLevel);
		}
	}

	[Serializable]
	abstract public class esEmployeeEducationLevel : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeEducationLevelQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeEducationLevel()
		{
		}

		public esEmployeeEducationLevel(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeEducationLevelID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeEducationLevelID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeEducationLevelID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeEducationLevelID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeEducationLevelID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeEducationLevelID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeEducationLevelID)
		{
			esEmployeeEducationLevelQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeEducationLevelID == employeeEducationLevelID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeEducationLevelID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeEducationLevelID", employeeEducationLevelID);
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
						case "EmployeeEducationLevelID": this.str.EmployeeEducationLevelID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeEducationLevelID":

							if (value == null || value is System.Int32)
								this.EmployeeEducationLevelID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to EmployeeEducationLevel.EmployeeEducationLevelID
		/// </summary>
		virtual public System.Int32? EmployeeEducationLevelID
		{
			get
			{
				return base.GetSystemInt32(EmployeeEducationLevelMetadata.ColumnNames.EmployeeEducationLevelID);
			}

			set
			{
				base.SetSystemInt32(EmployeeEducationLevelMetadata.ColumnNames.EmployeeEducationLevelID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducationLevel.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeEducationLevelMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeEducationLevelMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducationLevel.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(EmployeeEducationLevelMetadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(EmployeeEducationLevelMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducationLevel.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEducationLevelMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEducationLevelMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducationLevel.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEducationLevelMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEducationLevelMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducationLevel.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEducationLevelMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEducationLevelMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducationLevel.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeEducationLevelMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeEducationLevelMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeEducationLevel entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeEducationLevelID
			{
				get
				{
					System.Int32? data = entity.EmployeeEducationLevelID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeEducationLevelID = null;
					else entity.EmployeeEducationLevelID = Convert.ToInt32(value);
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
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
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
			private esEmployeeEducationLevel entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeEducationLevelQuery query)
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
				throw new Exception("esEmployeeEducationLevel can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeEducationLevel : esEmployeeEducationLevel
	{
	}

	[Serializable]
	abstract public class esEmployeeEducationLevelQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeEducationLevelMetadata.Meta();
			}
		}

		public esQueryItem EmployeeEducationLevelID
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationLevelMetadata.ColumnNames.EmployeeEducationLevelID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationLevelMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationLevelMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationLevelMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationLevelMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationLevelMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationLevelMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeEducationLevelCollection")]
	public partial class EmployeeEducationLevelCollection : esEmployeeEducationLevelCollection, IEnumerable<EmployeeEducationLevel>
	{
		public EmployeeEducationLevelCollection()
		{

		}

		public static implicit operator List<EmployeeEducationLevel>(EmployeeEducationLevelCollection coll)
		{
			List<EmployeeEducationLevel> list = new List<EmployeeEducationLevel>();

			foreach (EmployeeEducationLevel emp in coll)
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
				return EmployeeEducationLevelMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeEducationLevelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeEducationLevel(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeEducationLevel();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeEducationLevelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeEducationLevelQuery();
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
		public bool Load(EmployeeEducationLevelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeEducationLevel AddNew()
		{
			EmployeeEducationLevel entity = base.AddNewEntity() as EmployeeEducationLevel;

			return entity;
		}
		public EmployeeEducationLevel FindByPrimaryKey(Int32 employeeEducationLevelID)
		{
			return base.FindByPrimaryKey(employeeEducationLevelID) as EmployeeEducationLevel;
		}

		#region IEnumerable< EmployeeEducationLevel> Members

		IEnumerator<EmployeeEducationLevel> IEnumerable<EmployeeEducationLevel>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeEducationLevel;
			}
		}

		#endregion

		private EmployeeEducationLevelQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeEducationLevel' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeEducationLevel ({EmployeeEducationLevelID})")]
	[Serializable]
	public partial class EmployeeEducationLevel : esEmployeeEducationLevel
	{
		public EmployeeEducationLevel()
		{
		}

		public EmployeeEducationLevel(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeEducationLevelMetadata.Meta();
			}
		}

		override protected esEmployeeEducationLevelQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeEducationLevelQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeEducationLevelQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeEducationLevelQuery();
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
		public bool Load(EmployeeEducationLevelQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeEducationLevelQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeEducationLevelQuery : esEmployeeEducationLevelQuery
	{
		public EmployeeEducationLevelQuery()
		{

		}

		public EmployeeEducationLevelQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeEducationLevelQuery";
		}
	}

	[Serializable]
	public partial class EmployeeEducationLevelMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeEducationLevelMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeEducationLevelMetadata.ColumnNames.EmployeeEducationLevelID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeEducationLevelMetadata.PropertyNames.EmployeeEducationLevelID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationLevelMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeEducationLevelMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationLevelMetadata.ColumnNames.SREducationLevel, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationLevelMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationLevelMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEducationLevelMetadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationLevelMetadata.ColumnNames.ValidTo, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEducationLevelMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationLevelMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEducationLevelMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationLevelMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationLevelMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeEducationLevelMetadata Meta()
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
			public const string EmployeeEducationLevelID = "EmployeeEducationLevelID";
			public const string PersonID = "PersonID";
			public const string SREducationLevel = "SREducationLevel";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeEducationLevelID = "EmployeeEducationLevelID";
			public const string PersonID = "PersonID";
			public const string SREducationLevel = "SREducationLevel";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
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
			lock (typeof(EmployeeEducationLevelMetadata))
			{
				if (EmployeeEducationLevelMetadata.mapDelegates == null)
				{
					EmployeeEducationLevelMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeEducationLevelMetadata.meta == null)
				{
					EmployeeEducationLevelMetadata.meta = new EmployeeEducationLevelMetadata();
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

				meta.AddTypeMap("EmployeeEducationLevelID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeEducationLevel";
				meta.Destination = "EmployeeEducationLevel";
				meta.spInsert = "proc_EmployeeEducationLevelInsert";
				meta.spUpdate = "proc_EmployeeEducationLevelUpdate";
				meta.spDelete = "proc_EmployeeEducationLevelDelete";
				meta.spLoadAll = "proc_EmployeeEducationLevelLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeEducationLevelLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeEducationLevelMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
