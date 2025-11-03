/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2020 5:13:34 PM
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
	abstract public class esMembershipItemRedemptionDetailCollection : esEntityCollectionWAuditLog
	{
		public esMembershipItemRedemptionDetailCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MembershipItemRedemptionDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esMembershipItemRedemptionDetailQuery query)
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
			this.InitQuery(query as esMembershipItemRedemptionDetailQuery);
		}
		#endregion

		virtual public MembershipItemRedemptionDetail DetachEntity(MembershipItemRedemptionDetail entity)
		{
			return base.DetachEntity(entity) as MembershipItemRedemptionDetail;
		}

		virtual public MembershipItemRedemptionDetail AttachEntity(MembershipItemRedemptionDetail entity)
		{
			return base.AttachEntity(entity) as MembershipItemRedemptionDetail;
		}

		virtual public void Combine(MembershipItemRedemptionDetailCollection collection)
		{
			base.Combine(collection);
		}

		new public MembershipItemRedemptionDetail this[int index]
		{
			get
			{
				return base[index] as MembershipItemRedemptionDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MembershipItemRedemptionDetail);
		}
	}

	[Serializable]
	abstract public class esMembershipItemRedemptionDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMembershipItemRedemptionDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esMembershipItemRedemptionDetail()
		{
		}

		public esMembershipItemRedemptionDetail(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, Int64 membershipDetailID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, membershipDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, membershipDetailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int64 membershipDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, membershipDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, membershipDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, Int64 membershipDetailID)
		{
			esMembershipItemRedemptionDetailQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.MembershipDetailID == membershipDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int64 membershipDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("MembershipDetailID", membershipDetailID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "MembershipDetailID": this.str.MembershipDetailID = (string)value; break;
						case "ClaimedPoint": this.str.ClaimedPoint = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MembershipDetailID":

							if (value == null || value is System.Int64)
								this.MembershipDetailID = (System.Int64?)value;
							break;
						case "ClaimedPoint":

							if (value == null || value is System.Decimal)
								this.ClaimedPoint = (System.Decimal?)value;
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
		/// Maps to MembershipItemRedemptionDetail.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(MembershipItemRedemptionDetailMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(MembershipItemRedemptionDetailMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionDetail.MembershipDetailID
		/// </summary>
		virtual public System.Int64? MembershipDetailID
		{
			get
			{
				return base.GetSystemInt64(MembershipItemRedemptionDetailMetadata.ColumnNames.MembershipDetailID);
			}

			set
			{
				base.SetSystemInt64(MembershipItemRedemptionDetailMetadata.ColumnNames.MembershipDetailID, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionDetail.ClaimedPoint
		/// </summary>
		virtual public System.Decimal? ClaimedPoint
		{
			get
			{
				return base.GetSystemDecimal(MembershipItemRedemptionDetailMetadata.ColumnNames.ClaimedPoint);
			}

			set
			{
				base.SetSystemDecimal(MembershipItemRedemptionDetailMetadata.ColumnNames.ClaimedPoint, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MembershipItemRedemptionDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMembershipItemRedemptionDetail entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String MembershipDetailID
			{
				get
				{
					System.Int64? data = entity.MembershipDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MembershipDetailID = null;
					else entity.MembershipDetailID = Convert.ToInt64(value);
				}
			}
			public System.String ClaimedPoint
			{
				get
				{
					System.Decimal? data = entity.ClaimedPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClaimedPoint = null;
					else entity.ClaimedPoint = Convert.ToDecimal(value);
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
			private esMembershipItemRedemptionDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMembershipItemRedemptionDetailQuery query)
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
				throw new Exception("esMembershipItemRedemptionDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MembershipItemRedemptionDetail : esMembershipItemRedemptionDetail
	{
	}

	[Serializable]
	abstract public class esMembershipItemRedemptionDetailQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MembershipItemRedemptionDetailMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionDetailMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem MembershipDetailID
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionDetailMetadata.ColumnNames.MembershipDetailID, esSystemType.Int64);
			}
		}

		public esQueryItem ClaimedPoint
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionDetailMetadata.ColumnNames.ClaimedPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MembershipItemRedemptionDetailCollection")]
	public partial class MembershipItemRedemptionDetailCollection : esMembershipItemRedemptionDetailCollection, IEnumerable<MembershipItemRedemptionDetail>
	{
		public MembershipItemRedemptionDetailCollection()
		{

		}

		public static implicit operator List<MembershipItemRedemptionDetail>(MembershipItemRedemptionDetailCollection coll)
		{
			List<MembershipItemRedemptionDetail> list = new List<MembershipItemRedemptionDetail>();

			foreach (MembershipItemRedemptionDetail emp in coll)
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
				return MembershipItemRedemptionDetailMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipItemRedemptionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MembershipItemRedemptionDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MembershipItemRedemptionDetail();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MembershipItemRedemptionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipItemRedemptionDetailQuery();
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
		public bool Load(MembershipItemRedemptionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MembershipItemRedemptionDetail AddNew()
		{
			MembershipItemRedemptionDetail entity = base.AddNewEntity() as MembershipItemRedemptionDetail;

			return entity;
		}
		public MembershipItemRedemptionDetail FindByPrimaryKey(String transactionNo, Int64 membershipDetailID)
		{
			return base.FindByPrimaryKey(transactionNo, membershipDetailID) as MembershipItemRedemptionDetail;
		}

		#region IEnumerable< MembershipItemRedemptionDetail> Members

		IEnumerator<MembershipItemRedemptionDetail> IEnumerable<MembershipItemRedemptionDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MembershipItemRedemptionDetail;
			}
		}

		#endregion

		private MembershipItemRedemptionDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MembershipItemRedemptionDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MembershipItemRedemptionDetail ({TransactionNo, MembershipDetailID})")]
	[Serializable]
	public partial class MembershipItemRedemptionDetail : esMembershipItemRedemptionDetail
	{
		public MembershipItemRedemptionDetail()
		{
		}

		public MembershipItemRedemptionDetail(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MembershipItemRedemptionDetailMetadata.Meta();
			}
		}

		override protected esMembershipItemRedemptionDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipItemRedemptionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MembershipItemRedemptionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipItemRedemptionDetailQuery();
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
		public bool Load(MembershipItemRedemptionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MembershipItemRedemptionDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MembershipItemRedemptionDetailQuery : esMembershipItemRedemptionDetailQuery
	{
		public MembershipItemRedemptionDetailQuery()
		{

		}

		public MembershipItemRedemptionDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MembershipItemRedemptionDetailQuery";
		}
	}

	[Serializable]
	public partial class MembershipItemRedemptionDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MembershipItemRedemptionDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MembershipItemRedemptionDetailMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedemptionDetailMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionDetailMetadata.ColumnNames.MembershipDetailID, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MembershipItemRedemptionDetailMetadata.PropertyNames.MembershipDetailID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionDetailMetadata.ColumnNames.ClaimedPoint, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipItemRedemptionDetailMetadata.PropertyNames.ClaimedPoint;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipItemRedemptionDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipItemRedemptionDetailMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipItemRedemptionDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MembershipItemRedemptionDetailMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string MembershipDetailID = "MembershipDetailID";
			public const string ClaimedPoint = "ClaimedPoint";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string MembershipDetailID = "MembershipDetailID";
			public const string ClaimedPoint = "ClaimedPoint";
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
			lock (typeof(MembershipItemRedemptionDetailMetadata))
			{
				if (MembershipItemRedemptionDetailMetadata.mapDelegates == null)
				{
					MembershipItemRedemptionDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MembershipItemRedemptionDetailMetadata.meta == null)
				{
					MembershipItemRedemptionDetailMetadata.meta = new MembershipItemRedemptionDetailMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MembershipDetailID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ClaimedPoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MembershipItemRedemptionDetail";
				meta.Destination = "MembershipItemRedemptionDetail";
				meta.spInsert = "proc_MembershipItemRedemptionDetailInsert";
				meta.spUpdate = "proc_MembershipItemRedemptionDetailUpdate";
				meta.spDelete = "proc_MembershipItemRedemptionDetailDelete";
				meta.spLoadAll = "proc_MembershipItemRedemptionDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_MembershipItemRedemptionDetailLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MembershipItemRedemptionDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
