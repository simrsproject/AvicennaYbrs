/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2023 11:22:07 AM
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
	abstract public class esPerformancePlanAspectsOfBehaviorItemCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanAspectsOfBehaviorItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanAspectsOfBehaviorItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanAspectsOfBehaviorItemQuery query)
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
			this.InitQuery(query as esPerformancePlanAspectsOfBehaviorItemQuery);
		}
		#endregion

		virtual public PerformancePlanAspectsOfBehaviorItem DetachEntity(PerformancePlanAspectsOfBehaviorItem entity)
		{
			return base.DetachEntity(entity) as PerformancePlanAspectsOfBehaviorItem;
		}

		virtual public PerformancePlanAspectsOfBehaviorItem AttachEntity(PerformancePlanAspectsOfBehaviorItem entity)
		{
			return base.AttachEntity(entity) as PerformancePlanAspectsOfBehaviorItem;
		}

		virtual public void Combine(PerformancePlanAspectsOfBehaviorItemCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanAspectsOfBehaviorItem this[int index]
		{
			get
			{
				return base[index] as PerformancePlanAspectsOfBehaviorItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanAspectsOfBehaviorItem);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanAspectsOfBehaviorItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanAspectsOfBehaviorItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanAspectsOfBehaviorItem()
		{
		}

		public esPerformancePlanAspectsOfBehaviorItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 aspectsOfBehaviorItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(aspectsOfBehaviorItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(aspectsOfBehaviorItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 aspectsOfBehaviorItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(aspectsOfBehaviorItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(aspectsOfBehaviorItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 aspectsOfBehaviorItemID)
		{
			esPerformancePlanAspectsOfBehaviorItemQuery query = this.GetDynamicQuery();
			query.Where(query.AspectsOfBehaviorItemID == aspectsOfBehaviorItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 aspectsOfBehaviorItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("AspectsOfBehaviorItemID", aspectsOfBehaviorItemID);
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
						case "AspectsOfBehaviorItemID": this.str.AspectsOfBehaviorItemID = (string)value; break;
						case "AspectsOfBehaviorID": this.str.AspectsOfBehaviorID = (string)value; break;
						case "RatedAspectCode": this.str.RatedAspectCode = (string)value; break;
						case "RatedAspectName": this.str.RatedAspectName = (string)value; break;
						case "RatedAspectDescription": this.str.RatedAspectDescription = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "AspectsOfBehaviorItemID":

							if (value == null || value is System.Int32)
								this.AspectsOfBehaviorItemID = (System.Int32?)value;
							break;
						case "AspectsOfBehaviorID":

							if (value == null || value is System.Int32)
								this.AspectsOfBehaviorID = (System.Int32?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to PerformancePlanAspectsOfBehaviorItem.AspectsOfBehaviorItemID
		/// </summary>
		virtual public System.Int32? AspectsOfBehaviorItemID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorItemID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorItemID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.AspectsOfBehaviorID
		/// </summary>
		virtual public System.Int32? AspectsOfBehaviorID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.RatedAspectCode
		/// </summary>
		virtual public System.String RatedAspectCode
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectCode);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectCode, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.RatedAspectName
		/// </summary>
		virtual public System.String RatedAspectName
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectName);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectName, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.RatedAspectDescription
		/// </summary>
		virtual public System.String RatedAspectDescription
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectDescription);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectDescription, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehaviorItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanAspectsOfBehaviorItem entity)
			{
				this.entity = entity;
			}
			public System.String AspectsOfBehaviorItemID
			{
				get
				{
					System.Int32? data = entity.AspectsOfBehaviorItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AspectsOfBehaviorItemID = null;
					else entity.AspectsOfBehaviorItemID = Convert.ToInt32(value);
				}
			}
			public System.String AspectsOfBehaviorID
			{
				get
				{
					System.Int32? data = entity.AspectsOfBehaviorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AspectsOfBehaviorID = null;
					else entity.AspectsOfBehaviorID = Convert.ToInt32(value);
				}
			}
			public System.String RatedAspectCode
			{
				get
				{
					System.String data = entity.RatedAspectCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatedAspectCode = null;
					else entity.RatedAspectCode = Convert.ToString(value);
				}
			}
			public System.String RatedAspectName
			{
				get
				{
					System.String data = entity.RatedAspectName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatedAspectName = null;
					else entity.RatedAspectName = Convert.ToString(value);
				}
			}
			public System.String RatedAspectDescription
			{
				get
				{
					System.String data = entity.RatedAspectDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RatedAspectDescription = null;
					else entity.RatedAspectDescription = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			private esPerformancePlanAspectsOfBehaviorItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanAspectsOfBehaviorItemQuery query)
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
				throw new Exception("esPerformancePlanAspectsOfBehaviorItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanAspectsOfBehaviorItem : esPerformancePlanAspectsOfBehaviorItem
	{
	}

	[Serializable]
	abstract public class esPerformancePlanAspectsOfBehaviorItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanAspectsOfBehaviorItemMetadata.Meta();
			}
		}

		public esQueryItem AspectsOfBehaviorItemID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorItemID, esSystemType.Int32);
			}
		}

		public esQueryItem AspectsOfBehaviorID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorID, esSystemType.Int32);
			}
		}

		public esQueryItem RatedAspectCode
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectCode, esSystemType.String);
			}
		}

		public esQueryItem RatedAspectName
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectName, esSystemType.String);
			}
		}

		public esQueryItem RatedAspectDescription
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectDescription, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanAspectsOfBehaviorItemCollection")]
	public partial class PerformancePlanAspectsOfBehaviorItemCollection : esPerformancePlanAspectsOfBehaviorItemCollection, IEnumerable<PerformancePlanAspectsOfBehaviorItem>
	{
		public PerformancePlanAspectsOfBehaviorItemCollection()
		{

		}

		public static implicit operator List<PerformancePlanAspectsOfBehaviorItem>(PerformancePlanAspectsOfBehaviorItemCollection coll)
		{
			List<PerformancePlanAspectsOfBehaviorItem> list = new List<PerformancePlanAspectsOfBehaviorItem>();

			foreach (PerformancePlanAspectsOfBehaviorItem emp in coll)
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
				return PerformancePlanAspectsOfBehaviorItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanAspectsOfBehaviorItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanAspectsOfBehaviorItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanAspectsOfBehaviorItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanAspectsOfBehaviorItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanAspectsOfBehaviorItemQuery();
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
		public bool Load(PerformancePlanAspectsOfBehaviorItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanAspectsOfBehaviorItem AddNew()
		{
			PerformancePlanAspectsOfBehaviorItem entity = base.AddNewEntity() as PerformancePlanAspectsOfBehaviorItem;

			return entity;
		}
		public PerformancePlanAspectsOfBehaviorItem FindByPrimaryKey(Int32 aspectsOfBehaviorItemID)
		{
			return base.FindByPrimaryKey(aspectsOfBehaviorItemID) as PerformancePlanAspectsOfBehaviorItem;
		}

		#region IEnumerable< PerformancePlanAspectsOfBehaviorItem> Members

		IEnumerator<PerformancePlanAspectsOfBehaviorItem> IEnumerable<PerformancePlanAspectsOfBehaviorItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanAspectsOfBehaviorItem;
			}
		}

		#endregion

		private PerformancePlanAspectsOfBehaviorItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanAspectsOfBehaviorItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanAspectsOfBehaviorItem ({AspectsOfBehaviorItemID})")]
	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorItem : esPerformancePlanAspectsOfBehaviorItem
	{
		public PerformancePlanAspectsOfBehaviorItem()
		{
		}

		public PerformancePlanAspectsOfBehaviorItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanAspectsOfBehaviorItemMetadata.Meta();
			}
		}

		override protected esPerformancePlanAspectsOfBehaviorItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanAspectsOfBehaviorItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanAspectsOfBehaviorItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanAspectsOfBehaviorItemQuery();
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
		public bool Load(PerformancePlanAspectsOfBehaviorItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanAspectsOfBehaviorItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorItemQuery : esPerformancePlanAspectsOfBehaviorItemQuery
	{
		public PerformancePlanAspectsOfBehaviorItemQuery()
		{

		}

		public PerformancePlanAspectsOfBehaviorItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanAspectsOfBehaviorItemQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanAspectsOfBehaviorItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.AspectsOfBehaviorItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.AspectsOfBehaviorID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.AspectsOfBehaviorID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.RatedAspectCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.RatedAspectName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.RatedAspectDescription, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.RatedAspectDescription;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.CreatedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanAspectsOfBehaviorItemMetadata Meta()
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
			public const string AspectsOfBehaviorItemID = "AspectsOfBehaviorItemID";
			public const string AspectsOfBehaviorID = "AspectsOfBehaviorID";
			public const string RatedAspectCode = "RatedAspectCode";
			public const string RatedAspectName = "RatedAspectName";
			public const string RatedAspectDescription = "RatedAspectDescription";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AspectsOfBehaviorItemID = "AspectsOfBehaviorItemID";
			public const string AspectsOfBehaviorID = "AspectsOfBehaviorID";
			public const string RatedAspectCode = "RatedAspectCode";
			public const string RatedAspectName = "RatedAspectName";
			public const string RatedAspectDescription = "RatedAspectDescription";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(PerformancePlanAspectsOfBehaviorItemMetadata))
			{
				if (PerformancePlanAspectsOfBehaviorItemMetadata.mapDelegates == null)
				{
					PerformancePlanAspectsOfBehaviorItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanAspectsOfBehaviorItemMetadata.meta == null)
				{
					PerformancePlanAspectsOfBehaviorItemMetadata.meta = new PerformancePlanAspectsOfBehaviorItemMetadata();
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

				meta.AddTypeMap("AspectsOfBehaviorItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AspectsOfBehaviorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RatedAspectCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RatedAspectName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RatedAspectDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanAspectsOfBehaviorItem";
				meta.Destination = "PerformancePlanAspectsOfBehaviorItem";
				meta.spInsert = "proc_PerformancePlanAspectsOfBehaviorItemInsert";
				meta.spUpdate = "proc_PerformancePlanAspectsOfBehaviorItemUpdate";
				meta.spDelete = "proc_PerformancePlanAspectsOfBehaviorItemDelete";
				meta.spLoadAll = "proc_PerformancePlanAspectsOfBehaviorItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanAspectsOfBehaviorItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanAspectsOfBehaviorItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
