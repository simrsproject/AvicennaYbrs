/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/21/2023 4:31:36 PM
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
	abstract public class esCssdDecontaminationItemCollection : esEntityCollectionWAuditLog
	{
		public esCssdDecontaminationItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdDecontaminationItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdDecontaminationItemQuery query)
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
			this.InitQuery(query as esCssdDecontaminationItemQuery);
		}
		#endregion

		virtual public CssdDecontaminationItem DetachEntity(CssdDecontaminationItem entity)
		{
			return base.DetachEntity(entity) as CssdDecontaminationItem;
		}

		virtual public CssdDecontaminationItem AttachEntity(CssdDecontaminationItem entity)
		{
			return base.AttachEntity(entity) as CssdDecontaminationItem;
		}

		virtual public void Combine(CssdDecontaminationItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdDecontaminationItem this[int index]
		{
			get
			{
				return base[index] as CssdDecontaminationItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdDecontaminationItem);
		}
	}

	[Serializable]
	abstract public class esCssdDecontaminationItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdDecontaminationItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdDecontaminationItem()
		{
		}

		public esCssdDecontaminationItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String decontaminationNo, String decontaminationSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(decontaminationNo, decontaminationSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(decontaminationNo, decontaminationSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String decontaminationNo, String decontaminationSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(decontaminationNo, decontaminationSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(decontaminationNo, decontaminationSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String decontaminationNo, String decontaminationSeqNo)
		{
			esCssdDecontaminationItemQuery query = this.GetDynamicQuery();
			query.Where(query.DecontaminationNo == decontaminationNo, query.DecontaminationSeqNo == decontaminationSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String decontaminationNo, String decontaminationSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("DecontaminationNo", decontaminationNo);
			parms.Add("DecontaminationSeqNo", decontaminationSeqNo);
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
						case "DecontaminationNo": this.str.DecontaminationNo = (string)value; break;
						case "DecontaminationSeqNo": this.str.DecontaminationSeqNo = (string)value; break;
						case "ReceivedNo": this.str.ReceivedNo = (string)value; break;
						case "ReceivedSeqNo": this.str.ReceivedSeqNo = (string)value; break;
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
		/// Maps to CssdDecontaminationItem.DecontaminationNo
		/// </summary>
		virtual public System.String DecontaminationNo
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationItemMetadata.ColumnNames.DecontaminationNo);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationItemMetadata.ColumnNames.DecontaminationNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontaminationItem.DecontaminationSeqNo
		/// </summary>
		virtual public System.String DecontaminationSeqNo
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationItemMetadata.ColumnNames.DecontaminationSeqNo);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationItemMetadata.ColumnNames.DecontaminationSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontaminationItem.ReceivedNo
		/// </summary>
		virtual public System.String ReceivedNo
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationItemMetadata.ColumnNames.ReceivedNo);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationItemMetadata.ColumnNames.ReceivedNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontaminationItem.ReceivedSeqNo
		/// </summary>
		virtual public System.String ReceivedSeqNo
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationItemMetadata.ColumnNames.ReceivedSeqNo);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationItemMetadata.ColumnNames.ReceivedSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontaminationItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdDecontaminationItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdDecontaminationItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDecontaminationItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdDecontaminationItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdDecontaminationItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdDecontaminationItem entity)
			{
				this.entity = entity;
			}
			public System.String DecontaminationNo
			{
				get
				{
					System.String data = entity.DecontaminationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecontaminationNo = null;
					else entity.DecontaminationNo = Convert.ToString(value);
				}
			}
			public System.String DecontaminationSeqNo
			{
				get
				{
					System.String data = entity.DecontaminationSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecontaminationSeqNo = null;
					else entity.DecontaminationSeqNo = Convert.ToString(value);
				}
			}
			public System.String ReceivedNo
			{
				get
				{
					System.String data = entity.ReceivedNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedNo = null;
					else entity.ReceivedNo = Convert.ToString(value);
				}
			}
			public System.String ReceivedSeqNo
			{
				get
				{
					System.String data = entity.ReceivedSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedSeqNo = null;
					else entity.ReceivedSeqNo = Convert.ToString(value);
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
			private esCssdDecontaminationItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdDecontaminationItemQuery query)
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
				throw new Exception("esCssdDecontaminationItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdDecontaminationItem : esCssdDecontaminationItem
	{
	}

	[Serializable]
	abstract public class esCssdDecontaminationItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdDecontaminationItemMetadata.Meta();
			}
		}

		public esQueryItem DecontaminationNo
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationItemMetadata.ColumnNames.DecontaminationNo, esSystemType.String);
			}
		}

		public esQueryItem DecontaminationSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationItemMetadata.ColumnNames.DecontaminationSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedNo
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationItemMetadata.ColumnNames.ReceivedNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedSeqNo
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationItemMetadata.ColumnNames.ReceivedSeqNo, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDecontaminationItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdDecontaminationItemCollection")]
	public partial class CssdDecontaminationItemCollection : esCssdDecontaminationItemCollection, IEnumerable<CssdDecontaminationItem>
	{
		public CssdDecontaminationItemCollection()
		{

		}

		public static implicit operator List<CssdDecontaminationItem>(CssdDecontaminationItemCollection coll)
		{
			List<CssdDecontaminationItem> list = new List<CssdDecontaminationItem>();

			foreach (CssdDecontaminationItem emp in coll)
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
				return CssdDecontaminationItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdDecontaminationItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdDecontaminationItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdDecontaminationItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdDecontaminationItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdDecontaminationItemQuery();
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
		public bool Load(CssdDecontaminationItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdDecontaminationItem AddNew()
		{
			CssdDecontaminationItem entity = base.AddNewEntity() as CssdDecontaminationItem;

			return entity;
		}
		public CssdDecontaminationItem FindByPrimaryKey(String decontaminationNo, String decontaminationSeqNo)
		{
			return base.FindByPrimaryKey(decontaminationNo, decontaminationSeqNo) as CssdDecontaminationItem;
		}

		#region IEnumerable< CssdDecontaminationItem> Members

		IEnumerator<CssdDecontaminationItem> IEnumerable<CssdDecontaminationItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdDecontaminationItem;
			}
		}

		#endregion

		private CssdDecontaminationItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdDecontaminationItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdDecontaminationItem ({DecontaminationNo, DecontaminationSeqNo})")]
	[Serializable]
	public partial class CssdDecontaminationItem : esCssdDecontaminationItem
	{
		public CssdDecontaminationItem()
		{
		}

		public CssdDecontaminationItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdDecontaminationItemMetadata.Meta();
			}
		}

		override protected esCssdDecontaminationItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdDecontaminationItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdDecontaminationItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdDecontaminationItemQuery();
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
		public bool Load(CssdDecontaminationItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdDecontaminationItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdDecontaminationItemQuery : esCssdDecontaminationItemQuery
	{
		public CssdDecontaminationItemQuery()
		{

		}

		public CssdDecontaminationItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdDecontaminationItemQuery";
		}
	}

	[Serializable]
	public partial class CssdDecontaminationItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdDecontaminationItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdDecontaminationItemMetadata.ColumnNames.DecontaminationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationItemMetadata.PropertyNames.DecontaminationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationItemMetadata.ColumnNames.DecontaminationSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationItemMetadata.PropertyNames.DecontaminationSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationItemMetadata.ColumnNames.ReceivedNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationItemMetadata.PropertyNames.ReceivedNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationItemMetadata.ColumnNames.ReceivedSeqNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationItemMetadata.PropertyNames.ReceivedSeqNo;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDecontaminationItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDecontaminationItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDecontaminationItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdDecontaminationItemMetadata Meta()
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
			public const string DecontaminationNo = "DecontaminationNo";
			public const string DecontaminationSeqNo = "DecontaminationSeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string DecontaminationNo = "DecontaminationNo";
			public const string DecontaminationSeqNo = "DecontaminationSeqNo";
			public const string ReceivedNo = "ReceivedNo";
			public const string ReceivedSeqNo = "ReceivedSeqNo";
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
			lock (typeof(CssdDecontaminationItemMetadata))
			{
				if (CssdDecontaminationItemMetadata.mapDelegates == null)
				{
					CssdDecontaminationItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdDecontaminationItemMetadata.meta == null)
				{
					CssdDecontaminationItemMetadata.meta = new CssdDecontaminationItemMetadata();
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

				meta.AddTypeMap("DecontaminationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DecontaminationSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdDecontaminationItem";
				meta.Destination = "CssdDecontaminationItem";
				meta.spInsert = "proc_CssdDecontaminationItemInsert";
				meta.spUpdate = "proc_CssdDecontaminationItemUpdate";
				meta.spDelete = "proc_CssdDecontaminationItemDelete";
				meta.spLoadAll = "proc_CssdDecontaminationItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdDecontaminationItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdDecontaminationItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
