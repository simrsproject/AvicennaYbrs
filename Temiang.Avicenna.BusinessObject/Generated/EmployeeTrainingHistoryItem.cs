/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2023 4:33:05 PM
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
	abstract public class esEmployeeTrainingHistoryItemCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTrainingHistoryItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTrainingHistoryItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingHistoryItemQuery query)
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
			this.InitQuery(query as esEmployeeTrainingHistoryItemQuery);
		}
		#endregion

		virtual public EmployeeTrainingHistoryItem DetachEntity(EmployeeTrainingHistoryItem entity)
		{
			return base.DetachEntity(entity) as EmployeeTrainingHistoryItem;
		}

		virtual public EmployeeTrainingHistoryItem AttachEntity(EmployeeTrainingHistoryItem entity)
		{
			return base.AttachEntity(entity) as EmployeeTrainingHistoryItem;
		}

		virtual public void Combine(EmployeeTrainingHistoryItemCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTrainingHistoryItem this[int index]
		{
			get
			{
				return base[index] as EmployeeTrainingHistoryItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTrainingHistoryItem);
		}
	}

	[Serializable]
	abstract public class esEmployeeTrainingHistoryItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTrainingHistoryItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTrainingHistoryItem()
		{
		}

		public esEmployeeTrainingHistoryItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 EmployeeTrainingID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(EmployeeTrainingID);
			else
				return LoadByPrimaryKeyStoredProcedure(EmployeeTrainingID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 EmployeeTrainingID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(EmployeeTrainingID);
			else
				return LoadByPrimaryKeyStoredProcedure(EmployeeTrainingID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 EmployeeTrainingID)
		{
			esEmployeeTrainingHistoryItemQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeTrainingID == EmployeeTrainingID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 EmployeeTrainingID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeTrainingID", EmployeeTrainingID);
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
						case "EmployeeTrainingID": this.str.EmployeeTrainingID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SubComponentID": this.str.SubComponentID = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;

                    }
				}
				else
				{
					switch (name)
					{
						case "EmployeeTrainingID":

							if (value == null || value is System.Int32)
								this.EmployeeTrainingID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
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
		/// Maps to EmployeeTrainingHistoryItem.EmployeeTrainingID
		/// </summary>
		virtual public System.Int32? EmployeeTrainingID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingHistoryItemMetadata.ColumnNames.EmployeeTrainingID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingHistoryItemMetadata.ColumnNames.EmployeeTrainingID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistoryItem.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingHistoryMetadata.ColumnNames.PersonID, value);
			}
		}

		/// <summary>
		/// Maps to EmployeeTrainingHistoryItem.SubComponentID
		/// </summary>
		virtual public System.String SubComponentID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryItemMetadata.ColumnNames.SubComponentID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryItemMetadata.ColumnNames.SubComponentID, value);
			}
		}

		/// <summary>
		/// Maps to EmployeeTrainingHistory.StartDate
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(EmployeeTrainingHistoryItemMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(EmployeeTrainingHistoryItemMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.EndDate
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingHistoryItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingHistoryItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.TrainingLocation
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingHistoryItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingHistoryItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingHistory.TrainingInstitution
		/// </summary>
	

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
			public esStrings(esEmployeeTrainingHistoryItem entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeTrainingID
			{
				get
				{
					System.Int32? data = entity.EmployeeTrainingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeTrainingID = null;
					else entity.EmployeeTrainingID = Convert.ToInt32(value);
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
			public System.String SubComponentID
			{
				get
				{
					System.String data = entity.SubComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubComponentID = null;
					else entity.SubComponentID = Convert.ToString(value);
				}
			}
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
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


			private esEmployeeTrainingHistoryItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingHistoryItemQuery query)
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
				throw new Exception("esEmployeeTrainingHistoryItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTrainingHistoryItem : esEmployeeTrainingHistoryItem
	{
	}

	[Serializable]
	abstract public class esEmployeeTrainingHistoryItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingHistoryItemMetadata.Meta();
			}
		}

		public esQueryItem EmployeeTrainingID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryItemMetadata.ColumnNames.EmployeeTrainingID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryItemMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SubComponentID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryItemMetadata.ColumnNames.SubComponentID, esSystemType.String);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryItemMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}


		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingHistoryItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}



	

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTrainingHistoryCollection")]
	public partial class EmployeeTrainingHistoryItemCollection : esEmployeeTrainingHistoryItemCollection, IEnumerable<EmployeeTrainingHistoryItem>
	{
		public EmployeeTrainingHistoryItemCollection()
		{

		}

		public static implicit operator List<EmployeeTrainingHistoryItem>(EmployeeTrainingHistoryItemCollection coll)
		{
			List<EmployeeTrainingHistoryItem> list = new List<EmployeeTrainingHistoryItem>();

			foreach (EmployeeTrainingHistoryItem emp in coll)
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
				return EmployeeTrainingHistoryItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingHistoryItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTrainingHistoryItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTrainingHistoryItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingHistoryItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingHistoryItemQuery();
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
		public bool Load(EmployeeTrainingHistoryItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTrainingHistoryItem AddNew()
		{
			EmployeeTrainingHistoryItem entity = base.AddNewEntity() as EmployeeTrainingHistoryItem;

			return entity;
		}
		public EmployeeTrainingHistoryItem FindByPrimaryKey(Int32 employeeTrainingHistoryID)
		{
			return base.FindByPrimaryKey(employeeTrainingHistoryID) as EmployeeTrainingHistoryItem;
		}

		#region IEnumerable< EmployeeTrainingHistoryItem> Members

		IEnumerator<EmployeeTrainingHistoryItem> IEnumerable<EmployeeTrainingHistoryItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTrainingHistoryItem;
			}
		}

		#endregion

		private EmployeeTrainingHistoryItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTrainingHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTrainingHistoryItem ({EmployeeTrainingHistoryID})")]
	[Serializable]
	public partial class EmployeeTrainingHistoryItem : esEmployeeTrainingHistoryItem
	{
		public EmployeeTrainingHistoryItem()
		{
		}

		public EmployeeTrainingHistoryItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingHistoryItemMetadata.Meta();
			}
		}

		override protected esEmployeeTrainingHistoryItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingHistoryItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingHistoryItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingHistoryItemQuery();
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
		public bool Load(EmployeeTrainingHistoryItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTrainingHistoryItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTrainingHistoryItemQuery : esEmployeeTrainingHistoryItemQuery
	{
		public EmployeeTrainingHistoryItemQuery()
		{

		}

		public EmployeeTrainingHistoryItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTrainingHistoryItemQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTrainingHistoryItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTrainingHistoryItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTrainingHistoryItemMetadata.ColumnNames.EmployeeTrainingID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingHistoryItemMetadata.PropertyNames.EmployeeTrainingID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryItemMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingHistoryItemMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryItemMetadata.ColumnNames.SubComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryItemMetadata.PropertyNames.SubComponentID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryItemMetadata.ColumnNames.Price, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeTrainingHistoryItemMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingHistoryMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 255;
			_columns.Add(c);



		}
		#endregion

		static public EmployeeTrainingHistoryItemMetadata Meta()
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
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string PersonID = "PersonID";
			public const string SubComponentID = "SubComponentID";
			public const string Price = "Price";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string PersonID = "PersonID";
			public const string SubComponentID = "SubComponentID";
			public const string Price = "Price";
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
			lock (typeof(EmployeeTrainingHistoryItemMetadata))
			{
				if (EmployeeTrainingHistoryItemMetadata.mapDelegates == null)
				{
					EmployeeTrainingHistoryItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTrainingHistoryItemMetadata.meta == null)
				{
					EmployeeTrainingHistoryItemMetadata.meta = new EmployeeTrainingHistoryItemMetadata();
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

				meta.AddTypeMap("EmployeeTrainingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("decimal", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				


				meta.Source = "EmployeeTrainingHistoryItem";
				meta.Destination = "EmployeeTrainingHistoryItem";
				meta.spInsert = "proc_EmployeeTrainingHistoryItemInsert";
				meta.spUpdate = "proc_EmployeeTrainingHistoryItemUpdate";
				meta.spDelete = "proc_EmployeeTrainingHistoryItemDelete";
				meta.spLoadAll = "proc_EmployeeTrainingHistoryItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTrainingHistoryItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTrainingHistoryItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
