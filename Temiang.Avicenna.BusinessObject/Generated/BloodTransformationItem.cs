/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/18/2021 3:51:02 PM
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
	abstract public class esBloodTransformationItemCollection : esEntityCollectionWAuditLog
	{
		public esBloodTransformationItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BloodTransformationItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esBloodTransformationItemQuery query)
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
			this.InitQuery(query as esBloodTransformationItemQuery);
		}
		#endregion

		virtual public BloodTransformationItem DetachEntity(BloodTransformationItem entity)
		{
			return base.DetachEntity(entity) as BloodTransformationItem;
		}

		virtual public BloodTransformationItem AttachEntity(BloodTransformationItem entity)
		{
			return base.AttachEntity(entity) as BloodTransformationItem;
		}

		virtual public void Combine(BloodTransformationItemCollection collection)
		{
			base.Combine(collection);
		}

		new public BloodTransformationItem this[int index]
		{
			get
			{
				return base[index] as BloodTransformationItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BloodTransformationItem);
		}
	}

	[Serializable]
	abstract public class esBloodTransformationItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBloodTransformationItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esBloodTransformationItem()
		{
		}

		public esBloodTransformationItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String bagNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, bagNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, bagNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String bagNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, bagNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, bagNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String bagNo)
		{
			esBloodTransformationItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.BagNo == bagNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String bagNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("BagNo", bagNo);
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
						case "BagNo": this.str.BagNo = (string)value; break;
						case "SRBloodGroupFrom": this.str.SRBloodGroupFrom = (string)value; break;
						case "SRBloodGroupTo": this.str.SRBloodGroupTo = (string)value; break;
						case "ExpiredDateTime": this.str.ExpiredDateTime = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "VolumeBag": this.str.VolumeBag = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ExpiredDateTime":

							if (value == null || value is System.DateTime)
								this.ExpiredDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "VolumeBag":

							if (value == null || value is System.Decimal)
								this.VolumeBag = (System.Decimal?)value;
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
		/// Maps to BloodTransformationItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(BloodTransformationItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(BloodTransformationItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodTransformationItem.BagNo
		/// </summary>
		virtual public System.String BagNo
		{
			get
			{
				return base.GetSystemString(BloodTransformationItemMetadata.ColumnNames.BagNo);
			}

			set
			{
				base.SetSystemString(BloodTransformationItemMetadata.ColumnNames.BagNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodTransformationItem.SRBloodGroupFrom
		/// </summary>
		virtual public System.String SRBloodGroupFrom
		{
			get
			{
				return base.GetSystemString(BloodTransformationItemMetadata.ColumnNames.SRBloodGroupFrom);
			}

			set
			{
				base.SetSystemString(BloodTransformationItemMetadata.ColumnNames.SRBloodGroupFrom, value);
			}
		}
		/// <summary>
		/// Maps to BloodTransformationItem.SRBloodGroupTo
		/// </summary>
		virtual public System.String SRBloodGroupTo
		{
			get
			{
				return base.GetSystemString(BloodTransformationItemMetadata.ColumnNames.SRBloodGroupTo);
			}

			set
			{
				base.SetSystemString(BloodTransformationItemMetadata.ColumnNames.SRBloodGroupTo, value);
			}
		}
		/// <summary>
		/// Maps to BloodTransformationItem.ExpiredDateTime
		/// </summary>
		virtual public System.DateTime? ExpiredDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodTransformationItemMetadata.ColumnNames.ExpiredDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodTransformationItemMetadata.ColumnNames.ExpiredDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodTransformationItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodTransformationItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodTransformationItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodTransformationItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BloodTransformationItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BloodTransformationItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodTransformationItem.VolumeBag
		/// </summary>
		virtual public System.Decimal? VolumeBag
		{
			get
			{
				return base.GetSystemDecimal(BloodTransformationItemMetadata.ColumnNames.VolumeBag);
			}

			set
			{
				base.SetSystemDecimal(BloodTransformationItemMetadata.ColumnNames.VolumeBag, value);
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
			public esStrings(esBloodTransformationItem entity)
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
			public System.String BagNo
			{
				get
				{
					System.String data = entity.BagNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BagNo = null;
					else entity.BagNo = Convert.ToString(value);
				}
			}
			public System.String SRBloodGroupFrom
			{
				get
				{
					System.String data = entity.SRBloodGroupFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodGroupFrom = null;
					else entity.SRBloodGroupFrom = Convert.ToString(value);
				}
			}
			public System.String SRBloodGroupTo
			{
				get
				{
					System.String data = entity.SRBloodGroupTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodGroupTo = null;
					else entity.SRBloodGroupTo = Convert.ToString(value);
				}
			}
			public System.String ExpiredDateTime
			{
				get
				{
					System.DateTime? data = entity.ExpiredDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDateTime = null;
					else entity.ExpiredDateTime = Convert.ToDateTime(value);
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
			public System.String VolumeBag
			{
				get
				{
					System.Decimal? data = entity.VolumeBag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VolumeBag = null;
					else entity.VolumeBag = Convert.ToDecimal(value);
				}
			}
			private esBloodTransformationItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBloodTransformationItemQuery query)
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
				throw new Exception("esBloodTransformationItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BloodTransformationItem : esBloodTransformationItem
	{
	}

	[Serializable]
	abstract public class esBloodTransformationItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BloodTransformationItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem BagNo
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.BagNo, esSystemType.String);
			}
		}

		public esQueryItem SRBloodGroupFrom
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.SRBloodGroupFrom, esSystemType.String);
			}
		}

		public esQueryItem SRBloodGroupTo
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.SRBloodGroupTo, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDateTime
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.ExpiredDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem VolumeBag
		{
			get
			{
				return new esQueryItem(this, BloodTransformationItemMetadata.ColumnNames.VolumeBag, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BloodTransformationItemCollection")]
	public partial class BloodTransformationItemCollection : esBloodTransformationItemCollection, IEnumerable<BloodTransformationItem>
	{
		public BloodTransformationItemCollection()
		{

		}

		public static implicit operator List<BloodTransformationItem>(BloodTransformationItemCollection coll)
		{
			List<BloodTransformationItem> list = new List<BloodTransformationItem>();

			foreach (BloodTransformationItem emp in coll)
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
				return BloodTransformationItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodTransformationItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BloodTransformationItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BloodTransformationItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BloodTransformationItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodTransformationItemQuery();
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
		public bool Load(BloodTransformationItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BloodTransformationItem AddNew()
		{
			BloodTransformationItem entity = base.AddNewEntity() as BloodTransformationItem;

			return entity;
		}
		public BloodTransformationItem FindByPrimaryKey(String transactionNo, String bagNo)
		{
			return base.FindByPrimaryKey(transactionNo, bagNo) as BloodTransformationItem;
		}

		#region IEnumerable< BloodTransformationItem> Members

		IEnumerator<BloodTransformationItem> IEnumerable<BloodTransformationItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as BloodTransformationItem;
			}
		}

		#endregion

		private BloodTransformationItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BloodTransformationItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BloodTransformationItem ({TransactionNo, BagNo})")]
	[Serializable]
	public partial class BloodTransformationItem : esBloodTransformationItem
	{
		public BloodTransformationItem()
		{
		}

		public BloodTransformationItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BloodTransformationItemMetadata.Meta();
			}
		}

		override protected esBloodTransformationItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodTransformationItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BloodTransformationItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodTransformationItemQuery();
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
		public bool Load(BloodTransformationItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BloodTransformationItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BloodTransformationItemQuery : esBloodTransformationItemQuery
	{
		public BloodTransformationItemQuery()
		{

		}

		public BloodTransformationItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BloodTransformationItemQuery";
		}
	}

	[Serializable]
	public partial class BloodTransformationItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BloodTransformationItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.BagNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.BagNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.SRBloodGroupFrom, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.SRBloodGroupFrom;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.SRBloodGroupTo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.SRBloodGroupTo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.ExpiredDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.ExpiredDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodTransformationItemMetadata.ColumnNames.VolumeBag, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodTransformationItemMetadata.PropertyNames.VolumeBag;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BloodTransformationItemMetadata Meta()
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
			public const string BagNo = "BagNo";
			public const string SRBloodGroupFrom = "SRBloodGroupFrom";
			public const string SRBloodGroupTo = "SRBloodGroupTo";
			public const string ExpiredDateTime = "ExpiredDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VolumeBag = "VolumeBag";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string BagNo = "BagNo";
			public const string SRBloodGroupFrom = "SRBloodGroupFrom";
			public const string SRBloodGroupTo = "SRBloodGroupTo";
			public const string ExpiredDateTime = "ExpiredDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VolumeBag = "VolumeBag";
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
			lock (typeof(BloodTransformationItemMetadata))
			{
				if (BloodTransformationItemMetadata.mapDelegates == null)
				{
					BloodTransformationItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BloodTransformationItemMetadata.meta == null)
				{
					BloodTransformationItemMetadata.meta = new BloodTransformationItemMetadata();
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
				meta.AddTypeMap("BagNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodGroupFrom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodGroupTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VolumeBag", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "BloodTransformationItem";
				meta.Destination = "BloodTransformationItem";
				meta.spInsert = "proc_BloodTransformationItemInsert";
				meta.spUpdate = "proc_BloodTransformationItemUpdate";
				meta.spDelete = "proc_BloodTransformationItemDelete";
				meta.spLoadAll = "proc_BloodTransformationItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_BloodTransformationItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BloodTransformationItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
