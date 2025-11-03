/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/18/2021 1:31:18 PM
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
	abstract public class esBloodReceivedItemCollection : esEntityCollectionWAuditLog
	{
		public esBloodReceivedItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BloodReceivedItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esBloodReceivedItemQuery query)
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
			this.InitQuery(query as esBloodReceivedItemQuery);
		}
		#endregion

		virtual public BloodReceivedItem DetachEntity(BloodReceivedItem entity)
		{
			return base.DetachEntity(entity) as BloodReceivedItem;
		}

		virtual public BloodReceivedItem AttachEntity(BloodReceivedItem entity)
		{
			return base.AttachEntity(entity) as BloodReceivedItem;
		}

		virtual public void Combine(BloodReceivedItemCollection collection)
		{
			base.Combine(collection);
		}

		new public BloodReceivedItem this[int index]
		{
			get
			{
				return base[index] as BloodReceivedItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BloodReceivedItem);
		}
	}

	[Serializable]
	abstract public class esBloodReceivedItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBloodReceivedItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esBloodReceivedItem()
		{
		}

		public esBloodReceivedItem(DataRow row)
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
			esBloodReceivedItemQuery query = this.GetDynamicQuery();
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
						case "SRBloodType": this.str.SRBloodType = (string)value; break;
						case "BloodRhesus": this.str.BloodRhesus = (string)value; break;
						case "SRBloodGroup": this.str.SRBloodGroup = (string)value; break;
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
		/// Maps to BloodReceivedItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(BloodReceivedItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(BloodReceivedItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.BagNo
		/// </summary>
		virtual public System.String BagNo
		{
			get
			{
				return base.GetSystemString(BloodReceivedItemMetadata.ColumnNames.BagNo);
			}

			set
			{
				base.SetSystemString(BloodReceivedItemMetadata.ColumnNames.BagNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.SRBloodType
		/// </summary>
		virtual public System.String SRBloodType
		{
			get
			{
				return base.GetSystemString(BloodReceivedItemMetadata.ColumnNames.SRBloodType);
			}

			set
			{
				base.SetSystemString(BloodReceivedItemMetadata.ColumnNames.SRBloodType, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.BloodRhesus
		/// </summary>
		virtual public System.String BloodRhesus
		{
			get
			{
				return base.GetSystemString(BloodReceivedItemMetadata.ColumnNames.BloodRhesus);
			}

			set
			{
				base.SetSystemString(BloodReceivedItemMetadata.ColumnNames.BloodRhesus, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.SRBloodGroup
		/// </summary>
		virtual public System.String SRBloodGroup
		{
			get
			{
				return base.GetSystemString(BloodReceivedItemMetadata.ColumnNames.SRBloodGroup);
			}

			set
			{
				base.SetSystemString(BloodReceivedItemMetadata.ColumnNames.SRBloodGroup, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.ExpiredDateTime
		/// </summary>
		virtual public System.DateTime? ExpiredDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodReceivedItemMetadata.ColumnNames.ExpiredDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodReceivedItemMetadata.ColumnNames.ExpiredDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodReceivedItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodReceivedItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BloodReceivedItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BloodReceivedItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodReceivedItem.VolumeBag
		/// </summary>
		virtual public System.Decimal? VolumeBag
		{
			get
			{
				return base.GetSystemDecimal(BloodReceivedItemMetadata.ColumnNames.VolumeBag);
			}

			set
			{
				base.SetSystemDecimal(BloodReceivedItemMetadata.ColumnNames.VolumeBag, value);
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
			public esStrings(esBloodReceivedItem entity)
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
			public System.String SRBloodType
			{
				get
				{
					System.String data = entity.SRBloodType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodType = null;
					else entity.SRBloodType = Convert.ToString(value);
				}
			}
			public System.String BloodRhesus
			{
				get
				{
					System.String data = entity.BloodRhesus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodRhesus = null;
					else entity.BloodRhesus = Convert.ToString(value);
				}
			}
			public System.String SRBloodGroup
			{
				get
				{
					System.String data = entity.SRBloodGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodGroup = null;
					else entity.SRBloodGroup = Convert.ToString(value);
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
			private esBloodReceivedItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBloodReceivedItemQuery query)
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
				throw new Exception("esBloodReceivedItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BloodReceivedItem : esBloodReceivedItem
	{
	}

	[Serializable]
	abstract public class esBloodReceivedItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BloodReceivedItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem BagNo
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.BagNo, esSystemType.String);
			}
		}

		public esQueryItem SRBloodType
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.SRBloodType, esSystemType.String);
			}
		}

		public esQueryItem BloodRhesus
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.BloodRhesus, esSystemType.String);
			}
		}

		public esQueryItem SRBloodGroup
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.SRBloodGroup, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDateTime
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.ExpiredDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem VolumeBag
		{
			get
			{
				return new esQueryItem(this, BloodReceivedItemMetadata.ColumnNames.VolumeBag, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BloodReceivedItemCollection")]
	public partial class BloodReceivedItemCollection : esBloodReceivedItemCollection, IEnumerable<BloodReceivedItem>
	{
		public BloodReceivedItemCollection()
		{

		}

		public static implicit operator List<BloodReceivedItem>(BloodReceivedItemCollection coll)
		{
			List<BloodReceivedItem> list = new List<BloodReceivedItem>();

			foreach (BloodReceivedItem emp in coll)
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
				return BloodReceivedItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodReceivedItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BloodReceivedItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BloodReceivedItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BloodReceivedItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodReceivedItemQuery();
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
		public bool Load(BloodReceivedItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BloodReceivedItem AddNew()
		{
			BloodReceivedItem entity = base.AddNewEntity() as BloodReceivedItem;

			return entity;
		}
		public BloodReceivedItem FindByPrimaryKey(String transactionNo, String bagNo)
		{
			return base.FindByPrimaryKey(transactionNo, bagNo) as BloodReceivedItem;
		}

		#region IEnumerable< BloodReceivedItem> Members

		IEnumerator<BloodReceivedItem> IEnumerable<BloodReceivedItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as BloodReceivedItem;
			}
		}

		#endregion

		private BloodReceivedItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BloodReceivedItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BloodReceivedItem ({TransactionNo, BagNo})")]
	[Serializable]
	public partial class BloodReceivedItem : esBloodReceivedItem
	{
		public BloodReceivedItem()
		{
		}

		public BloodReceivedItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BloodReceivedItemMetadata.Meta();
			}
		}

		override protected esBloodReceivedItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodReceivedItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BloodReceivedItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodReceivedItemQuery();
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
		public bool Load(BloodReceivedItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BloodReceivedItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BloodReceivedItemQuery : esBloodReceivedItemQuery
	{
		public BloodReceivedItemQuery()
		{

		}

		public BloodReceivedItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BloodReceivedItemQuery";
		}
	}

	[Serializable]
	public partial class BloodReceivedItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BloodReceivedItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.BagNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.BagNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.SRBloodType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.SRBloodType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.BloodRhesus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.BloodRhesus;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.SRBloodGroup, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.SRBloodGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.ExpiredDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.ExpiredDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodReceivedItemMetadata.ColumnNames.VolumeBag, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodReceivedItemMetadata.PropertyNames.VolumeBag;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BloodReceivedItemMetadata Meta()
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
			public const string SRBloodType = "SRBloodType";
			public const string BloodRhesus = "BloodRhesus";
			public const string SRBloodGroup = "SRBloodGroup";
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
			public const string SRBloodType = "SRBloodType";
			public const string BloodRhesus = "BloodRhesus";
			public const string SRBloodGroup = "SRBloodGroup";
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
			lock (typeof(BloodReceivedItemMetadata))
			{
				if (BloodReceivedItemMetadata.mapDelegates == null)
				{
					BloodReceivedItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BloodReceivedItemMetadata.meta == null)
				{
					BloodReceivedItemMetadata.meta = new BloodReceivedItemMetadata();
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
				meta.AddTypeMap("SRBloodType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BloodRhesus", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("SRBloodGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VolumeBag", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "BloodReceivedItem";
				meta.Destination = "BloodReceivedItem";
				meta.spInsert = "proc_BloodReceivedItemInsert";
				meta.spUpdate = "proc_BloodReceivedItemUpdate";
				meta.spDelete = "proc_BloodReceivedItemDelete";
				meta.spLoadAll = "proc_BloodReceivedItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_BloodReceivedItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BloodReceivedItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
