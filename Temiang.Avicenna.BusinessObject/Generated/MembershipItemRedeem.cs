/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2020 5:12:38 PM
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
	abstract public class esMembershipItemRedeemCollection : esEntityCollectionWAuditLog
	{
		public esMembershipItemRedeemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MembershipItemRedeemCollection";
		}

		#region Query Logic
		protected void InitQuery(esMembershipItemRedeemQuery query)
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
			this.InitQuery(query as esMembershipItemRedeemQuery);
		}
		#endregion

		virtual public MembershipItemRedeem DetachEntity(MembershipItemRedeem entity)
		{
			return base.DetachEntity(entity) as MembershipItemRedeem;
		}

		virtual public MembershipItemRedeem AttachEntity(MembershipItemRedeem entity)
		{
			return base.AttachEntity(entity) as MembershipItemRedeem;
		}

		virtual public void Combine(MembershipItemRedeemCollection collection)
		{
			base.Combine(collection);
		}

		new public MembershipItemRedeem this[int index]
		{
			get
			{
				return base[index] as MembershipItemRedeem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MembershipItemRedeem);
		}
	}

	[Serializable]
	abstract public class esMembershipItemRedeem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMembershipItemRedeemQuery GetDynamicQuery()
		{
			return null;
		}

		public esMembershipItemRedeem()
		{
		}

		public esMembershipItemRedeem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String itemReedemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemReedemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemReedemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemReedemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemReedemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemReedemID);
		}

		private bool LoadByPrimaryKeyDynamic(String itemReedemID)
		{
			esMembershipItemRedeemQuery query = this.GetDynamicQuery();
			query.Where(query.ItemReedemID == itemReedemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String itemReedemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemReedemID", itemReedemID);
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
						case "ItemReedemID": this.str.ItemReedemID = (string)value; break;
						case "ItemReedemName": this.str.ItemReedemName = (string)value; break;
						case "SRItemReedemGroup": this.str.SRItemReedemGroup = (string)value; break;
						case "PointsUsed": this.str.PointsUsed = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PointsUsed":

							if (value == null || value is System.Decimal)
								this.PointsUsed = (System.Decimal?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to MembershipItemRedeem.ItemReedemID
		/// </summary>
		virtual public System.String ItemReedemID
		{
			get
			{
				return base.GetSystemString(MembershipItemRedeemMetadata.ColumnNames.ItemReedemID);
			}

			set
			{
				base.SetSystemString(MembershipItemRedeemMetadata.ColumnNames.ItemReedemID, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedeem.ItemReedemName
		/// </summary>
		virtual public System.String ItemReedemName
		{
			get
			{
				return base.GetSystemString(MembershipItemRedeemMetadata.ColumnNames.ItemReedemName);
			}

			set
			{
				base.SetSystemString(MembershipItemRedeemMetadata.ColumnNames.ItemReedemName, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedeem.SRItemReedemGroup
		/// </summary>
		virtual public System.String SRItemReedemGroup
		{
			get
			{
				return base.GetSystemString(MembershipItemRedeemMetadata.ColumnNames.SRItemReedemGroup);
			}

			set
			{
				base.SetSystemString(MembershipItemRedeemMetadata.ColumnNames.SRItemReedemGroup, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedeem.PointsUsed
		/// </summary>
		virtual public System.Decimal? PointsUsed
		{
			get
			{
				return base.GetSystemDecimal(MembershipItemRedeemMetadata.ColumnNames.PointsUsed);
			}

			set
			{
				base.SetSystemDecimal(MembershipItemRedeemMetadata.ColumnNames.PointsUsed, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedeem.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(MembershipItemRedeemMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(MembershipItemRedeemMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedeem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MembershipItemRedeemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MembershipItemRedeemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedeem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MembershipItemRedeemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MembershipItemRedeemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMembershipItemRedeem entity)
			{
				this.entity = entity;
			}
			public System.String ItemReedemID
			{
				get
				{
					System.String data = entity.ItemReedemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemReedemID = null;
					else entity.ItemReedemID = Convert.ToString(value);
				}
			}
			public System.String ItemReedemName
			{
				get
				{
					System.String data = entity.ItemReedemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemReedemName = null;
					else entity.ItemReedemName = Convert.ToString(value);
				}
			}
			public System.String SRItemReedemGroup
			{
				get
				{
					System.String data = entity.SRItemReedemGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemReedemGroup = null;
					else entity.SRItemReedemGroup = Convert.ToString(value);
				}
			}
			public System.String PointsUsed
			{
				get
				{
					System.Decimal? data = entity.PointsUsed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PointsUsed = null;
					else entity.PointsUsed = Convert.ToDecimal(value);
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
			private esMembershipItemRedeem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMembershipItemRedeemQuery query)
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
				throw new Exception("esMembershipItemRedeem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MembershipItemRedeem : esMembershipItemRedeem
	{
	}

	[Serializable]
	abstract public class esMembershipItemRedeemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MembershipItemRedeemMetadata.Meta();
			}
		}

		public esQueryItem ItemReedemID
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedeemMetadata.ColumnNames.ItemReedemID, esSystemType.String);
			}
		}

		public esQueryItem ItemReedemName
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedeemMetadata.ColumnNames.ItemReedemName, esSystemType.String);
			}
		}

		public esQueryItem SRItemReedemGroup
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedeemMetadata.ColumnNames.SRItemReedemGroup, esSystemType.String);
			}
		}

		public esQueryItem PointsUsed
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedeemMetadata.ColumnNames.PointsUsed, esSystemType.Decimal);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedeemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedeemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedeemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MembershipItemRedeemCollection")]
	public partial class MembershipItemRedeemCollection : esMembershipItemRedeemCollection, IEnumerable<MembershipItemRedeem>
	{
		public MembershipItemRedeemCollection()
		{

		}

		public static implicit operator List<MembershipItemRedeem>(MembershipItemRedeemCollection coll)
		{
			List<MembershipItemRedeem> list = new List<MembershipItemRedeem>();

			foreach (MembershipItemRedeem emp in coll)
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
				return MembershipItemRedeemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipItemRedeemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MembershipItemRedeem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MembershipItemRedeem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MembershipItemRedeemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipItemRedeemQuery();
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
		public bool Load(MembershipItemRedeemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MembershipItemRedeem AddNew()
		{
			MembershipItemRedeem entity = base.AddNewEntity() as MembershipItemRedeem;

			return entity;
		}
		public MembershipItemRedeem FindByPrimaryKey(String itemReedemID)
		{
			return base.FindByPrimaryKey(itemReedemID) as MembershipItemRedeem;
		}

		#region IEnumerable< MembershipItemRedeem> Members

		IEnumerator<MembershipItemRedeem> IEnumerable<MembershipItemRedeem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MembershipItemRedeem;
			}
		}

		#endregion

		private MembershipItemRedeemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MembershipItemRedeem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MembershipItemRedeem ({ItemReedemID})")]
	[Serializable]
	public partial class MembershipItemRedeem : esMembershipItemRedeem
	{
		public MembershipItemRedeem()
		{
		}

		public MembershipItemRedeem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MembershipItemRedeemMetadata.Meta();
			}
		}

		override protected esMembershipItemRedeemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipItemRedeemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MembershipItemRedeemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipItemRedeemQuery();
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
		public bool Load(MembershipItemRedeemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MembershipItemRedeemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MembershipItemRedeemQuery : esMembershipItemRedeemQuery
	{
		public MembershipItemRedeemQuery()
		{

		}

		public MembershipItemRedeemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MembershipItemRedeemQuery";
		}
	}

	[Serializable]
	public partial class MembershipItemRedeemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MembershipItemRedeemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MembershipItemRedeemMetadata.ColumnNames.ItemReedemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedeemMetadata.PropertyNames.ItemReedemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedeemMetadata.ColumnNames.ItemReedemName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedeemMetadata.PropertyNames.ItemReedemName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedeemMetadata.ColumnNames.SRItemReedemGroup, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedeemMetadata.PropertyNames.SRItemReedemGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedeemMetadata.ColumnNames.PointsUsed, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipItemRedeemMetadata.PropertyNames.PointsUsed;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedeemMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MembershipItemRedeemMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedeemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipItemRedeemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedeemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedeemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MembershipItemRedeemMetadata Meta()
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
			public const string ItemReedemID = "ItemReedemID";
			public const string ItemReedemName = "ItemReedemName";
			public const string SRItemReedemGroup = "SRItemReedemGroup";
			public const string PointsUsed = "PointsUsed";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ItemReedemID = "ItemReedemID";
			public const string ItemReedemName = "ItemReedemName";
			public const string SRItemReedemGroup = "SRItemReedemGroup";
			public const string PointsUsed = "PointsUsed";
			public const string IsActive = "IsActive";
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
			lock (typeof(MembershipItemRedeemMetadata))
			{
				if (MembershipItemRedeemMetadata.mapDelegates == null)
				{
					MembershipItemRedeemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MembershipItemRedeemMetadata.meta == null)
				{
					MembershipItemRedeemMetadata.meta = new MembershipItemRedeemMetadata();
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

				meta.AddTypeMap("ItemReedemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemReedemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemReedemGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PointsUsed", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MembershipItemRedeem";
				meta.Destination = "MembershipItemRedeem";
				meta.spInsert = "proc_MembershipItemRedeemInsert";
				meta.spUpdate = "proc_MembershipItemRedeemUpdate";
				meta.spDelete = "proc_MembershipItemRedeemDelete";
				meta.spLoadAll = "proc_MembershipItemRedeemLoadAll";
				meta.spLoadByPrimaryKey = "proc_MembershipItemRedeemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MembershipItemRedeemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
