/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2020 1:27:00 PM
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
	abstract public class esPositionResponsibilityCollection : esEntityCollectionWAuditLog
	{
		public esPositionResponsibilityCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PositionResponsibilityCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionResponsibilityQuery query)
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
			this.InitQuery(query as esPositionResponsibilityQuery);
		}
		#endregion

		virtual public PositionResponsibility DetachEntity(PositionResponsibility entity)
		{
			return base.DetachEntity(entity) as PositionResponsibility;
		}

		virtual public PositionResponsibility AttachEntity(PositionResponsibility entity)
		{
			return base.AttachEntity(entity) as PositionResponsibility;
		}

		virtual public void Combine(PositionResponsibilityCollection collection)
		{
			base.Combine(collection);
		}

		new public PositionResponsibility this[int index]
		{
			get
			{
				return base[index] as PositionResponsibility;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionResponsibility);
		}
	}

	[Serializable]
	abstract public class esPositionResponsibility : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionResponsibilityQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionResponsibility()
		{
		}

		public esPositionResponsibility(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 positionResponsibilityID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionResponsibilityID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionResponsibilityID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 positionResponsibilityID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionResponsibilityID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionResponsibilityID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 positionResponsibilityID)
		{
			esPositionResponsibilityQuery query = this.GetDynamicQuery();
			query.Where(query.PositionResponsibilityID == positionResponsibilityID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 positionResponsibilityID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionResponsibilityID", positionResponsibilityID);
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
						case "PositionResponsibilityID": this.str.PositionResponsibilityID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "ResponsibilityName": this.str.ResponsibilityName = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PositionResponsibilityID":

							if (value == null || value is System.Int32)
								this.PositionResponsibilityID = (System.Int32?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
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
		/// Maps to PositionResponsibility.PositionResponsibilityID
		/// </summary>
		virtual public System.Int32? PositionResponsibilityID
		{
			get
			{
				return base.GetSystemInt32(PositionResponsibilityMetadata.ColumnNames.PositionResponsibilityID);
			}

			set
			{
				base.SetSystemInt32(PositionResponsibilityMetadata.ColumnNames.PositionResponsibilityID, value);
			}
		}
		/// <summary>
		/// Maps to PositionResponsibility.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionResponsibilityMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PositionResponsibilityMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PositionResponsibility.ResponsibilityName
		/// </summary>
		virtual public System.String ResponsibilityName
		{
			get
			{
				return base.GetSystemString(PositionResponsibilityMetadata.ColumnNames.ResponsibilityName);
			}

			set
			{
				base.SetSystemString(PositionResponsibilityMetadata.ColumnNames.ResponsibilityName, value);
			}
		}
		/// <summary>
		/// Maps to PositionResponsibility.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(PositionResponsibilityMetadata.ColumnNames.Description);
			}

			set
			{
				base.SetSystemString(PositionResponsibilityMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to PositionResponsibility.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionResponsibilityMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PositionResponsibilityMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PositionResponsibility.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionResponsibilityMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PositionResponsibilityMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionResponsibility entity)
			{
				this.entity = entity;
			}
			public System.String PositionResponsibilityID
			{
				get
				{
					System.Int32? data = entity.PositionResponsibilityID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionResponsibilityID = null;
					else entity.PositionResponsibilityID = Convert.ToInt32(value);
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
			public System.String ResponsibilityName
			{
				get
				{
					System.String data = entity.ResponsibilityName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResponsibilityName = null;
					else entity.ResponsibilityName = Convert.ToString(value);
				}
			}
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
			private esPositionResponsibility entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionResponsibilityQuery query)
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
				throw new Exception("esPositionResponsibility can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PositionResponsibility : esPositionResponsibility
	{
	}

	[Serializable]
	abstract public class esPositionResponsibilityQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PositionResponsibilityMetadata.Meta();
			}
		}

		public esQueryItem PositionResponsibilityID
		{
			get
			{
				return new esQueryItem(this, PositionResponsibilityMetadata.ColumnNames.PositionResponsibilityID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionResponsibilityMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem ResponsibilityName
		{
			get
			{
				return new esQueryItem(this, PositionResponsibilityMetadata.ColumnNames.ResponsibilityName, esSystemType.String);
			}
		}

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, PositionResponsibilityMetadata.ColumnNames.Description, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionResponsibilityMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionResponsibilityMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionResponsibilityCollection")]
	public partial class PositionResponsibilityCollection : esPositionResponsibilityCollection, IEnumerable<PositionResponsibility>
	{
		public PositionResponsibilityCollection()
		{

		}

		public static implicit operator List<PositionResponsibility>(PositionResponsibilityCollection coll)
		{
			List<PositionResponsibility> list = new List<PositionResponsibility>();

			foreach (PositionResponsibility emp in coll)
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
				return PositionResponsibilityMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionResponsibilityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionResponsibility(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionResponsibility();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PositionResponsibilityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionResponsibilityQuery();
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
		public bool Load(PositionResponsibilityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PositionResponsibility AddNew()
		{
			PositionResponsibility entity = base.AddNewEntity() as PositionResponsibility;

			return entity;
		}
		public PositionResponsibility FindByPrimaryKey(Int32 positionResponsibilityID)
		{
			return base.FindByPrimaryKey(positionResponsibilityID) as PositionResponsibility;
		}

		#region IEnumerable< PositionResponsibility> Members

		IEnumerator<PositionResponsibility> IEnumerable<PositionResponsibility>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PositionResponsibility;
			}
		}

		#endregion

		private PositionResponsibilityQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionResponsibility' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PositionResponsibility ({PositionResponsibilityID})")]
	[Serializable]
	public partial class PositionResponsibility : esPositionResponsibility
	{
		public PositionResponsibility()
		{
		}

		public PositionResponsibility(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionResponsibilityMetadata.Meta();
			}
		}

		override protected esPositionResponsibilityQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionResponsibilityQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PositionResponsibilityQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionResponsibilityQuery();
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
		public bool Load(PositionResponsibilityQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PositionResponsibilityQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PositionResponsibilityQuery : esPositionResponsibilityQuery
	{
		public PositionResponsibilityQuery()
		{

		}

		public PositionResponsibilityQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PositionResponsibilityQuery";
		}
	}

	[Serializable]
	public partial class PositionResponsibilityMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionResponsibilityMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionResponsibilityMetadata.ColumnNames.PositionResponsibilityID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionResponsibilityMetadata.PropertyNames.PositionResponsibilityID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionResponsibilityMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionResponsibilityMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionResponsibilityMetadata.ColumnNames.ResponsibilityName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionResponsibilityMetadata.PropertyNames.ResponsibilityName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PositionResponsibilityMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionResponsibilityMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionResponsibilityMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionResponsibilityMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PositionResponsibilityMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionResponsibilityMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public PositionResponsibilityMetadata Meta()
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
			public const string PositionResponsibilityID = "PositionResponsibilityID";
			public const string PositionID = "PositionID";
			public const string ResponsibilityName = "ResponsibilityName";
			public const string Description = "Description";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PositionResponsibilityID = "PositionResponsibilityID";
			public const string PositionID = "PositionID";
			public const string ResponsibilityName = "ResponsibilityName";
			public const string Description = "Description";
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
			lock (typeof(PositionResponsibilityMetadata))
			{
				if (PositionResponsibilityMetadata.mapDelegates == null)
				{
					PositionResponsibilityMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PositionResponsibilityMetadata.meta == null)
				{
					PositionResponsibilityMetadata.meta = new PositionResponsibilityMetadata();
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

				meta.AddTypeMap("PositionResponsibilityID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ResponsibilityName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PositionResponsibility";
				meta.Destination = "PositionResponsibility";
				meta.spInsert = "proc_PositionResponsibilityInsert";
				meta.spUpdate = "proc_PositionResponsibilityUpdate";
				meta.spDelete = "proc_PositionResponsibilityDelete";
				meta.spLoadAll = "proc_PositionResponsibilityLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionResponsibilityLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionResponsibilityMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
