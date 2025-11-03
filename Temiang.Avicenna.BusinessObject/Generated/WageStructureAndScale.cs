/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/7/2022 3:42:08 PM
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
	abstract public class esWageStructureAndScaleCollection : esEntityCollectionWAuditLog
	{
		public esWageStructureAndScaleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "WageStructureAndScaleCollection";
		}

		#region Query Logic
		protected void InitQuery(esWageStructureAndScaleQuery query)
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
			this.InitQuery(query as esWageStructureAndScaleQuery);
		}
		#endregion

		virtual public WageStructureAndScale DetachEntity(WageStructureAndScale entity)
		{
			return base.DetachEntity(entity) as WageStructureAndScale;
		}

		virtual public WageStructureAndScale AttachEntity(WageStructureAndScale entity)
		{
			return base.AttachEntity(entity) as WageStructureAndScale;
		}

		virtual public void Combine(WageStructureAndScaleCollection collection)
		{
			base.Combine(collection);
		}

		new public WageStructureAndScale this[int index]
		{
			get
			{
				return base[index] as WageStructureAndScale;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WageStructureAndScale);
		}
	}

	[Serializable]
	abstract public class esWageStructureAndScale : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWageStructureAndScaleQuery GetDynamicQuery()
		{
			return null;
		}

		public esWageStructureAndScale()
		{
		}

		public esWageStructureAndScale(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 wageStructureAndScaleID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScaleID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScaleID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 wageStructureAndScaleID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageStructureAndScaleID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageStructureAndScaleID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 wageStructureAndScaleID)
		{
			esWageStructureAndScaleQuery query = this.GetDynamicQuery();
			query.Where(query.WageStructureAndScaleID == wageStructureAndScaleID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 wageStructureAndScaleID)
		{
			esParameters parms = new esParameters();
			parms.Add("WageStructureAndScaleID", wageStructureAndScaleID);
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
						case "WageStructureAndScaleID": this.str.WageStructureAndScaleID = (string)value; break;
						case "SRWageStructureAndScaleType": this.str.SRWageStructureAndScaleType = (string)value; break;
						case "WageStructureAndScaleCode": this.str.WageStructureAndScaleCode = (string)value; break;
						case "WageStructureAndScaleName": this.str.WageStructureAndScaleName = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "WageStructureAndScaleID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScaleID = (System.Int32?)value;
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
		/// Maps to WageStructureAndScale.WageStructureAndScaleID
		/// </summary>
		virtual public System.Int32? WageStructureAndScaleID
		{
			get
			{
				return base.GetSystemInt32(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleID);
			}

			set
			{
				base.SetSystemInt32(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleID, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScale.SRWageStructureAndScaleType
		/// </summary>
		virtual public System.String SRWageStructureAndScaleType
		{
			get
			{
				return base.GetSystemString(WageStructureAndScaleMetadata.ColumnNames.SRWageStructureAndScaleType);
			}

			set
			{
				base.SetSystemString(WageStructureAndScaleMetadata.ColumnNames.SRWageStructureAndScaleType, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScale.WageStructureAndScaleCode
		/// </summary>
		virtual public System.String WageStructureAndScaleCode
		{
			get
			{
				return base.GetSystemString(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleCode);
			}

			set
			{
				base.SetSystemString(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleCode, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScale.WageStructureAndScaleName
		/// </summary>
		virtual public System.String WageStructureAndScaleName
		{
			get
			{
				return base.GetSystemString(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleName);
			}

			set
			{
				base.SetSystemString(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleName, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScale.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(WageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to WageStructureAndScale.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(WageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(WageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esWageStructureAndScale entity)
			{
				this.entity = entity;
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
			public System.String WageStructureAndScaleCode
			{
				get
				{
					System.String data = entity.WageStructureAndScaleCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScaleCode = null;
					else entity.WageStructureAndScaleCode = Convert.ToString(value);
				}
			}
			public System.String WageStructureAndScaleName
			{
				get
				{
					System.String data = entity.WageStructureAndScaleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScaleName = null;
					else entity.WageStructureAndScaleName = Convert.ToString(value);
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
			private esWageStructureAndScale entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWageStructureAndScaleQuery query)
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
				throw new Exception("esWageStructureAndScale can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class WageStructureAndScale : esWageStructureAndScale
	{
	}

	[Serializable]
	abstract public class esWageStructureAndScaleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return WageStructureAndScaleMetadata.Meta();
			}
		}

		public esQueryItem WageStructureAndScaleID
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem SRWageStructureAndScaleType
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleMetadata.ColumnNames.SRWageStructureAndScaleType, esSystemType.String);
			}
		}

		public esQueryItem WageStructureAndScaleCode
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleCode, esSystemType.String);
			}
		}

		public esQueryItem WageStructureAndScaleName
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleName, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, WageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WageStructureAndScaleCollection")]
	public partial class WageStructureAndScaleCollection : esWageStructureAndScaleCollection, IEnumerable<WageStructureAndScale>
	{
		public WageStructureAndScaleCollection()
		{

		}

		public static implicit operator List<WageStructureAndScale>(WageStructureAndScaleCollection coll)
		{
			List<WageStructureAndScale> list = new List<WageStructureAndScale>();

			foreach (WageStructureAndScale emp in coll)
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
				return WageStructureAndScaleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageStructureAndScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WageStructureAndScale(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WageStructureAndScale();
		}

		#endregion

		[BrowsableAttribute(false)]
		public WageStructureAndScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageStructureAndScaleQuery();
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
		public bool Load(WageStructureAndScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public WageStructureAndScale AddNew()
		{
			WageStructureAndScale entity = base.AddNewEntity() as WageStructureAndScale;

			return entity;
		}
		public WageStructureAndScale FindByPrimaryKey(Int32 wageStructureAndScaleID)
		{
			return base.FindByPrimaryKey(wageStructureAndScaleID) as WageStructureAndScale;
		}

		#region IEnumerable< WageStructureAndScale> Members

		IEnumerator<WageStructureAndScale> IEnumerable<WageStructureAndScale>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as WageStructureAndScale;
			}
		}

		#endregion

		private WageStructureAndScaleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WageStructureAndScale' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("WageStructureAndScale ({WageStructureAndScaleID})")]
	[Serializable]
	public partial class WageStructureAndScale : esWageStructureAndScale
	{
		public WageStructureAndScale()
		{
		}

		public WageStructureAndScale(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WageStructureAndScaleMetadata.Meta();
			}
		}

		override protected esWageStructureAndScaleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageStructureAndScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public WageStructureAndScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageStructureAndScaleQuery();
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
		public bool Load(WageStructureAndScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private WageStructureAndScaleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class WageStructureAndScaleQuery : esWageStructureAndScaleQuery
	{
		public WageStructureAndScaleQuery()
		{

		}

		public WageStructureAndScaleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "WageStructureAndScaleQuery";
		}
	}

	[Serializable]
	public partial class WageStructureAndScaleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WageStructureAndScaleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageStructureAndScaleMetadata.PropertyNames.WageStructureAndScaleID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleMetadata.ColumnNames.SRWageStructureAndScaleType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = WageStructureAndScaleMetadata.PropertyNames.SRWageStructureAndScaleType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = WageStructureAndScaleMetadata.PropertyNames.WageStructureAndScaleCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = WageStructureAndScaleMetadata.PropertyNames.WageStructureAndScaleName;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WageStructureAndScaleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(WageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WageStructureAndScaleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public WageStructureAndScaleMetadata Meta()
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
			public const string WageStructureAndScaleID = "WageStructureAndScaleID";
			public const string SRWageStructureAndScaleType = "SRWageStructureAndScaleType";
			public const string WageStructureAndScaleCode = "WageStructureAndScaleCode";
			public const string WageStructureAndScaleName = "WageStructureAndScaleName";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string WageStructureAndScaleID = "WageStructureAndScaleID";
			public const string SRWageStructureAndScaleType = "SRWageStructureAndScaleType";
			public const string WageStructureAndScaleCode = "WageStructureAndScaleCode";
			public const string WageStructureAndScaleName = "WageStructureAndScaleName";
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
			lock (typeof(WageStructureAndScaleMetadata))
			{
				if (WageStructureAndScaleMetadata.mapDelegates == null)
				{
					WageStructureAndScaleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (WageStructureAndScaleMetadata.meta == null)
				{
					WageStructureAndScaleMetadata.meta = new WageStructureAndScaleMetadata();
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

				meta.AddTypeMap("WageStructureAndScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRWageStructureAndScaleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WageStructureAndScaleCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WageStructureAndScaleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "WageStructureAndScale";
				meta.Destination = "WageStructureAndScale";
				meta.spInsert = "proc_WageStructureAndScaleInsert";
				meta.spUpdate = "proc_WageStructureAndScaleUpdate";
				meta.spDelete = "proc_WageStructureAndScaleDelete";
				meta.spLoadAll = "proc_WageStructureAndScaleLoadAll";
				meta.spLoadByPrimaryKey = "proc_WageStructureAndScaleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WageStructureAndScaleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
