/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/12/2022 12:35:46 PM
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
	abstract public class esEmployeeWageStructureAndScalePositionItemCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeWageStructureAndScalePositionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeWageStructureAndScalePositionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeWageStructureAndScalePositionItemQuery query)
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
			this.InitQuery(query as esEmployeeWageStructureAndScalePositionItemQuery);
		}
		#endregion

		virtual public EmployeeWageStructureAndScalePositionItem DetachEntity(EmployeeWageStructureAndScalePositionItem entity)
		{
			return base.DetachEntity(entity) as EmployeeWageStructureAndScalePositionItem;
		}

		virtual public EmployeeWageStructureAndScalePositionItem AttachEntity(EmployeeWageStructureAndScalePositionItem entity)
		{
			return base.AttachEntity(entity) as EmployeeWageStructureAndScalePositionItem;
		}

		virtual public void Combine(EmployeeWageStructureAndScalePositionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeWageStructureAndScalePositionItem this[int index]
		{
			get
			{
				return base[index] as EmployeeWageStructureAndScalePositionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeWageStructureAndScalePositionItem);
		}
	}

	[Serializable]
	abstract public class esEmployeeWageStructureAndScalePositionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeWageStructureAndScalePositionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeWageStructureAndScalePositionItem()
		{
		}

		public esEmployeeWageStructureAndScalePositionItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 wageStructureAndScalePositionItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScalePositionItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScalePositionItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 wageStructureAndScalePositionItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScalePositionItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScalePositionItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 wageStructureAndScalePositionItemID)
		{
			esEmployeeWageStructureAndScalePositionItemQuery query = this.GetDynamicQuery();
			query.Where(query.WageStructureAndScalePositionItemID == wageStructureAndScalePositionItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 wageStructureAndScalePositionItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("WageStructureAndScalePositionItemID", wageStructureAndScalePositionItemID);
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
						case "WageStructureAndScalePositionItemID": this.str.WageStructureAndScalePositionItemID = (string)value; break;
						case "WageStructureAndScalePositionID": this.str.WageStructureAndScalePositionID = (string)value; break;
						case "SRWageStructureAndScaleType": this.str.SRWageStructureAndScaleType = (string)value; break;
						case "WageStructureAndScaleID": this.str.WageStructureAndScaleID = (string)value; break;
						case "WageStructureAndScaleItemID": this.str.WageStructureAndScaleItemID = (string)value; break;
						case "LoadPoint": this.str.LoadPoint = (string)value; break;
						case "BasePoint": this.str.BasePoint = (string)value; break;
						case "Points": this.str.Points = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "WageStructureAndScalePositionItemID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScalePositionItemID = (System.Int32?)value;
							break;
						case "WageStructureAndScalePositionID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScalePositionID = (System.Int32?)value;
							break;
						case "WageStructureAndScaleID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScaleID = (System.Int32?)value;
							break;
						case "WageStructureAndScaleItemID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScaleItemID = (System.Int32?)value;
							break;
						case "LoadPoint":

							if (value == null || value is System.Decimal)
								this.LoadPoint = (System.Decimal?)value;
							break;
						case "BasePoint":

							if (value == null || value is System.Decimal)
								this.BasePoint = (System.Decimal?)value;
							break;
						case "Points":

							if (value == null || value is System.Decimal)
								this.Points = (System.Decimal?)value;
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
		/// Maps to EmployeeWageStructureAndScalePositionItem.WageStructureAndScalePositionItemID
		/// </summary>
		virtual public System.Int32? WageStructureAndScalePositionItemID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionItemID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionItemID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.WageStructureAndScalePositionID
		/// </summary>
		virtual public System.Int32? WageStructureAndScalePositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.SRWageStructureAndScaleType
		/// </summary>
		virtual public System.String SRWageStructureAndScaleType
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.SRWageStructureAndScaleType);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.SRWageStructureAndScaleType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.WageStructureAndScaleID
		/// </summary>
		virtual public System.Int32? WageStructureAndScaleID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.WageStructureAndScaleItemID
		/// </summary>
		virtual public System.Int32? WageStructureAndScaleItemID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleItemID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleItemID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.LoadPoint
		/// </summary>
		virtual public System.Decimal? LoadPoint
		{
			get
			{
				return base.GetSystemDecimal(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LoadPoint);
			}

			set
			{
				base.SetSystemDecimal(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LoadPoint, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.BasePoint
		/// </summary>
		virtual public System.Decimal? BasePoint
		{
			get
			{
				return base.GetSystemDecimal(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.BasePoint);
			}

			set
			{
				base.SetSystemDecimal(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.BasePoint, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.Points
		/// </summary>
		virtual public System.Decimal? Points
		{
			get
			{
				return base.GetSystemDecimal(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.Points);
			}

			set
			{
				base.SetSystemDecimal(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.Points, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScalePositionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeWageStructureAndScalePositionItem entity)
			{
				this.entity = entity;
			}
			public System.String WageStructureAndScalePositionItemID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScalePositionItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScalePositionItemID = null;
					else entity.WageStructureAndScalePositionItemID = Convert.ToInt32(value);
				}
			}
			public System.String WageStructureAndScalePositionID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScalePositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScalePositionID = null;
					else entity.WageStructureAndScalePositionID = Convert.ToInt32(value);
				}
			}
			public System.String SRWageStructureAndScaleType
			{
				get
				{
					System.String data = entity.SRWageStructureAndScaleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWageStructureAndScaleType = null;
					else entity.SRWageStructureAndScaleType = Convert.ToString(value);
				}
			}
			public System.String WageStructureAndScaleID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScaleID = null;
					else entity.WageStructureAndScaleID = Convert.ToInt32(value);
				}
			}
			public System.String WageStructureAndScaleItemID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScaleItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScaleItemID = null;
					else entity.WageStructureAndScaleItemID = Convert.ToInt32(value);
				}
			}
			public System.String LoadPoint
			{
				get
				{
					System.Decimal? data = entity.LoadPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LoadPoint = null;
					else entity.LoadPoint = Convert.ToDecimal(value);
				}
			}
			public System.String BasePoint
			{
				get
				{
					System.Decimal? data = entity.BasePoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BasePoint = null;
					else entity.BasePoint = Convert.ToDecimal(value);
				}
			}
			public System.String Points
			{
				get
				{
					System.Decimal? data = entity.Points;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Points = null;
					else entity.Points = Convert.ToDecimal(value);
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
			private esEmployeeWageStructureAndScalePositionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeWageStructureAndScalePositionItemQuery query)
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
				throw new Exception("esEmployeeWageStructureAndScalePositionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeWageStructureAndScalePositionItem : esEmployeeWageStructureAndScalePositionItem
	{
	}

	[Serializable]
	abstract public class esEmployeeWageStructureAndScalePositionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWageStructureAndScalePositionItemMetadata.Meta();
			}
		}

		public esQueryItem WageStructureAndScalePositionItemID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionItemID, esSystemType.Int32);
			}
		}

		public esQueryItem WageStructureAndScalePositionID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionID, esSystemType.Int32);
			}
		}

		public esQueryItem SRWageStructureAndScaleType
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.SRWageStructureAndScaleType, esSystemType.String);
			}
		}

		public esQueryItem WageStructureAndScaleID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem WageStructureAndScaleItemID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleItemID, esSystemType.Int32);
			}
		}

		public esQueryItem LoadPoint
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LoadPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem BasePoint
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.BasePoint, esSystemType.Decimal);
			}
		}

		public esQueryItem Points
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.Points, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeWageStructureAndScalePositionItemCollection")]
	public partial class EmployeeWageStructureAndScalePositionItemCollection : esEmployeeWageStructureAndScalePositionItemCollection, IEnumerable<EmployeeWageStructureAndScalePositionItem>
	{
		public EmployeeWageStructureAndScalePositionItemCollection()
		{

		}

		public static implicit operator List<EmployeeWageStructureAndScalePositionItem>(EmployeeWageStructureAndScalePositionItemCollection coll)
		{
			List<EmployeeWageStructureAndScalePositionItem> list = new List<EmployeeWageStructureAndScalePositionItem>();

			foreach (EmployeeWageStructureAndScalePositionItem emp in coll)
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
				return EmployeeWageStructureAndScalePositionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWageStructureAndScalePositionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeWageStructureAndScalePositionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeWageStructureAndScalePositionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWageStructureAndScalePositionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWageStructureAndScalePositionItemQuery();
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
		public bool Load(EmployeeWageStructureAndScalePositionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeWageStructureAndScalePositionItem AddNew()
		{
			EmployeeWageStructureAndScalePositionItem entity = base.AddNewEntity() as EmployeeWageStructureAndScalePositionItem;

			return entity;
		}
		public EmployeeWageStructureAndScalePositionItem FindByPrimaryKey(Int32 wageStructureAndScalePositionItemID)
		{
			return base.FindByPrimaryKey(wageStructureAndScalePositionItemID) as EmployeeWageStructureAndScalePositionItem;
		}

		#region IEnumerable< EmployeeWageStructureAndScalePositionItem> Members

		IEnumerator<EmployeeWageStructureAndScalePositionItem> IEnumerable<EmployeeWageStructureAndScalePositionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeWageStructureAndScalePositionItem;
			}
		}

		#endregion

		private EmployeeWageStructureAndScalePositionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeWageStructureAndScalePositionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeWageStructureAndScalePositionItem ({WageStructureAndScalePositionItemID})")]
	[Serializable]
	public partial class EmployeeWageStructureAndScalePositionItem : esEmployeeWageStructureAndScalePositionItem
	{
		public EmployeeWageStructureAndScalePositionItem()
		{
		}

		public EmployeeWageStructureAndScalePositionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWageStructureAndScalePositionItemMetadata.Meta();
			}
		}

		override protected esEmployeeWageStructureAndScalePositionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWageStructureAndScalePositionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWageStructureAndScalePositionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWageStructureAndScalePositionItemQuery();
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
		public bool Load(EmployeeWageStructureAndScalePositionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeWageStructureAndScalePositionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeWageStructureAndScalePositionItemQuery : esEmployeeWageStructureAndScalePositionItemQuery
	{
		public EmployeeWageStructureAndScalePositionItemQuery()
		{

		}

		public EmployeeWageStructureAndScalePositionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeWageStructureAndScalePositionItemQuery";
		}
	}

	[Serializable]
	public partial class EmployeeWageStructureAndScalePositionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeWageStructureAndScalePositionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.WageStructureAndScalePositionItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScalePositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.WageStructureAndScalePositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.SRWageStructureAndScaleType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.SRWageStructureAndScaleType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.WageStructureAndScaleID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.WageStructureAndScaleItemID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.WageStructureAndScaleItemID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LoadPoint, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.LoadPoint;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.BasePoint, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.BasePoint;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.Points, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.Points;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScalePositionItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScalePositionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeWageStructureAndScalePositionItemMetadata Meta()
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
			public const string WageStructureAndScalePositionItemID = "WageStructureAndScalePositionItemID";
			public const string WageStructureAndScalePositionID = "WageStructureAndScalePositionID";
			public const string SRWageStructureAndScaleType = "SRWageStructureAndScaleType";
			public const string WageStructureAndScaleID = "WageStructureAndScaleID";
			public const string WageStructureAndScaleItemID = "WageStructureAndScaleItemID";
			public const string LoadPoint = "LoadPoint";
			public const string BasePoint = "BasePoint";
			public const string Points = "Points";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string WageStructureAndScalePositionItemID = "WageStructureAndScalePositionItemID";
			public const string WageStructureAndScalePositionID = "WageStructureAndScalePositionID";
			public const string SRWageStructureAndScaleType = "SRWageStructureAndScaleType";
			public const string WageStructureAndScaleID = "WageStructureAndScaleID";
			public const string WageStructureAndScaleItemID = "WageStructureAndScaleItemID";
			public const string LoadPoint = "LoadPoint";
			public const string BasePoint = "BasePoint";
			public const string Points = "Points";
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
			lock (typeof(EmployeeWageStructureAndScalePositionItemMetadata))
			{
				if (EmployeeWageStructureAndScalePositionItemMetadata.mapDelegates == null)
				{
					EmployeeWageStructureAndScalePositionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeWageStructureAndScalePositionItemMetadata.meta == null)
				{
					EmployeeWageStructureAndScalePositionItemMetadata.meta = new EmployeeWageStructureAndScalePositionItemMetadata();
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

				meta.AddTypeMap("WageStructureAndScalePositionItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WageStructureAndScalePositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRWageStructureAndScaleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WageStructureAndScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WageStructureAndScaleItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LoadPoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BasePoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Points", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeWageStructureAndScalePositionItem";
				meta.Destination = "EmployeeWageStructureAndScalePositionItem";
				meta.spInsert = "proc_EmployeeWageStructureAndScalePositionItemInsert";
				meta.spUpdate = "proc_EmployeeWageStructureAndScalePositionItemUpdate";
				meta.spDelete = "proc_EmployeeWageStructureAndScalePositionItemDelete";
				meta.spLoadAll = "proc_EmployeeWageStructureAndScalePositionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeWageStructureAndScalePositionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeWageStructureAndScalePositionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
