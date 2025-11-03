/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2020 1:25:02 PM
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
	abstract public class esPositionFunctionalAreaCollection : esEntityCollectionWAuditLog
	{
		public esPositionFunctionalAreaCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PositionFunctionalAreaCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionFunctionalAreaQuery query)
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
			this.InitQuery(query as esPositionFunctionalAreaQuery);
		}
		#endregion

		virtual public PositionFunctionalArea DetachEntity(PositionFunctionalArea entity)
		{
			return base.DetachEntity(entity) as PositionFunctionalArea;
		}

		virtual public PositionFunctionalArea AttachEntity(PositionFunctionalArea entity)
		{
			return base.AttachEntity(entity) as PositionFunctionalArea;
		}

		virtual public void Combine(PositionFunctionalAreaCollection collection)
		{
			base.Combine(collection);
		}

		new public PositionFunctionalArea this[int index]
		{
			get
			{
				return base[index] as PositionFunctionalArea;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionFunctionalArea);
		}
	}

	[Serializable]
	abstract public class esPositionFunctionalArea : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionFunctionalAreaQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionFunctionalArea()
		{
		}

		public esPositionFunctionalArea(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 positionFunctionalAreaID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionFunctionalAreaID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionFunctionalAreaID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 positionFunctionalAreaID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionFunctionalAreaID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionFunctionalAreaID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 positionFunctionalAreaID)
		{
			esPositionFunctionalAreaQuery query = this.GetDynamicQuery();
			query.Where(query.PositionFunctionalAreaID == positionFunctionalAreaID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 positionFunctionalAreaID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionFunctionalAreaID", positionFunctionalAreaID);
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
						case "PositionFunctionalAreaID": this.str.PositionFunctionalAreaID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "SRPositionFunctionalArea": this.str.SRPositionFunctionalArea = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PositionFunctionalAreaID":

							if (value == null || value is System.Int32)
								this.PositionFunctionalAreaID = (System.Int32?)value;
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
		/// Maps to PositionFunctionalArea.PositionFunctionalAreaID
		/// </summary>
		virtual public System.Int32? PositionFunctionalAreaID
		{
			get
			{
				return base.GetSystemInt32(PositionFunctionalAreaMetadata.ColumnNames.PositionFunctionalAreaID);
			}

			set
			{
				base.SetSystemInt32(PositionFunctionalAreaMetadata.ColumnNames.PositionFunctionalAreaID, value);
			}
		}
		/// <summary>
		/// Maps to PositionFunctionalArea.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionFunctionalAreaMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PositionFunctionalAreaMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PositionFunctionalArea.SRPositionFunctionalArea
		/// </summary>
		virtual public System.String SRPositionFunctionalArea
		{
			get
			{
				return base.GetSystemString(PositionFunctionalAreaMetadata.ColumnNames.SRPositionFunctionalArea);
			}

			set
			{
				base.SetSystemString(PositionFunctionalAreaMetadata.ColumnNames.SRPositionFunctionalArea, value);
			}
		}
		/// <summary>
		/// Maps to PositionFunctionalArea.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(PositionFunctionalAreaMetadata.ColumnNames.Description);
			}

			set
			{
				base.SetSystemString(PositionFunctionalAreaMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to PositionFunctionalArea.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionFunctionalAreaMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PositionFunctionalAreaMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PositionFunctionalArea.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionFunctionalAreaMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PositionFunctionalAreaMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionFunctionalArea entity)
			{
				this.entity = entity;
			}
			public System.String PositionFunctionalAreaID
			{
				get
				{
					System.Int32? data = entity.PositionFunctionalAreaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionFunctionalAreaID = null;
					else entity.PositionFunctionalAreaID = Convert.ToInt32(value);
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
			public System.String SRPositionFunctionalArea
			{
				get
				{
					System.String data = entity.SRPositionFunctionalArea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPositionFunctionalArea = null;
					else entity.SRPositionFunctionalArea = Convert.ToString(value);
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
			private esPositionFunctionalArea entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionFunctionalAreaQuery query)
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
				throw new Exception("esPositionFunctionalArea can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PositionFunctionalArea : esPositionFunctionalArea
	{
	}

	[Serializable]
	abstract public class esPositionFunctionalAreaQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PositionFunctionalAreaMetadata.Meta();
			}
		}

		public esQueryItem PositionFunctionalAreaID
		{
			get
			{
				return new esQueryItem(this, PositionFunctionalAreaMetadata.ColumnNames.PositionFunctionalAreaID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionFunctionalAreaMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem SRPositionFunctionalArea
		{
			get
			{
				return new esQueryItem(this, PositionFunctionalAreaMetadata.ColumnNames.SRPositionFunctionalArea, esSystemType.String);
			}
		}

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, PositionFunctionalAreaMetadata.ColumnNames.Description, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionFunctionalAreaMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionFunctionalAreaMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionFunctionalAreaCollection")]
	public partial class PositionFunctionalAreaCollection : esPositionFunctionalAreaCollection, IEnumerable<PositionFunctionalArea>
	{
		public PositionFunctionalAreaCollection()
		{

		}

		public static implicit operator List<PositionFunctionalArea>(PositionFunctionalAreaCollection coll)
		{
			List<PositionFunctionalArea> list = new List<PositionFunctionalArea>();

			foreach (PositionFunctionalArea emp in coll)
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
				return PositionFunctionalAreaMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionFunctionalAreaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionFunctionalArea(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionFunctionalArea();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PositionFunctionalAreaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionFunctionalAreaQuery();
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
		public bool Load(PositionFunctionalAreaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PositionFunctionalArea AddNew()
		{
			PositionFunctionalArea entity = base.AddNewEntity() as PositionFunctionalArea;

			return entity;
		}
		public PositionFunctionalArea FindByPrimaryKey(Int32 positionFunctionalAreaID)
		{
			return base.FindByPrimaryKey(positionFunctionalAreaID) as PositionFunctionalArea;
		}

		#region IEnumerable< PositionFunctionalArea> Members

		IEnumerator<PositionFunctionalArea> IEnumerable<PositionFunctionalArea>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PositionFunctionalArea;
			}
		}

		#endregion

		private PositionFunctionalAreaQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionFunctionalArea' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PositionFunctionalArea ({PositionFunctionalAreaID})")]
	[Serializable]
	public partial class PositionFunctionalArea : esPositionFunctionalArea
	{
		public PositionFunctionalArea()
		{
		}

		public PositionFunctionalArea(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionFunctionalAreaMetadata.Meta();
			}
		}

		override protected esPositionFunctionalAreaQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionFunctionalAreaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PositionFunctionalAreaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionFunctionalAreaQuery();
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
		public bool Load(PositionFunctionalAreaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PositionFunctionalAreaQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PositionFunctionalAreaQuery : esPositionFunctionalAreaQuery
	{
		public PositionFunctionalAreaQuery()
		{

		}

		public PositionFunctionalAreaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PositionFunctionalAreaQuery";
		}
	}

	[Serializable]
	public partial class PositionFunctionalAreaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionFunctionalAreaMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionFunctionalAreaMetadata.ColumnNames.PositionFunctionalAreaID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionFunctionalAreaMetadata.PropertyNames.PositionFunctionalAreaID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionFunctionalAreaMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionFunctionalAreaMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionFunctionalAreaMetadata.ColumnNames.SRPositionFunctionalArea, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionFunctionalAreaMetadata.PropertyNames.SRPositionFunctionalArea;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PositionFunctionalAreaMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionFunctionalAreaMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionFunctionalAreaMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionFunctionalAreaMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PositionFunctionalAreaMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionFunctionalAreaMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public PositionFunctionalAreaMetadata Meta()
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
			public const string PositionFunctionalAreaID = "PositionFunctionalAreaID";
			public const string PositionID = "PositionID";
			public const string SRPositionFunctionalArea = "SRPositionFunctionalArea";
			public const string Description = "Description";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PositionFunctionalAreaID = "PositionFunctionalAreaID";
			public const string PositionID = "PositionID";
			public const string SRPositionFunctionalArea = "SRPositionFunctionalArea";
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
			lock (typeof(PositionFunctionalAreaMetadata))
			{
				if (PositionFunctionalAreaMetadata.mapDelegates == null)
				{
					PositionFunctionalAreaMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PositionFunctionalAreaMetadata.meta == null)
				{
					PositionFunctionalAreaMetadata.meta = new PositionFunctionalAreaMetadata();
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

				meta.AddTypeMap("PositionFunctionalAreaID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPositionFunctionalArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PositionFunctionalArea";
				meta.Destination = "PositionFunctionalArea";
				meta.spInsert = "proc_PositionFunctionalAreaInsert";
				meta.spUpdate = "proc_PositionFunctionalAreaUpdate";
				meta.spDelete = "proc_PositionFunctionalAreaDelete";
				meta.spLoadAll = "proc_PositionFunctionalAreaLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionFunctionalAreaLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionFunctionalAreaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
