/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/21/2022 3:19:00 PM
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
	abstract public class esRegistrationEsoScaleCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationEsoScaleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationEsoScaleCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationEsoScaleQuery query)
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
			this.InitQuery(query as esRegistrationEsoScaleQuery);
		}
		#endregion

		virtual public RegistrationEsoScale DetachEntity(RegistrationEsoScale entity)
		{
			return base.DetachEntity(entity) as RegistrationEsoScale;
		}

		virtual public RegistrationEsoScale AttachEntity(RegistrationEsoScale entity)
		{
			return base.AttachEntity(entity) as RegistrationEsoScale;
		}

		virtual public void Combine(RegistrationEsoScaleCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationEsoScale this[int index]
		{
			get
			{
				return base[index] as RegistrationEsoScale;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationEsoScale);
		}
	}

	[Serializable]
	abstract public class esRegistrationEsoScale : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationEsoScaleQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationEsoScale()
		{
		}

		public esRegistrationEsoScale(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 esoNo, String sREsoScale)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, esoNo, sREsoScale);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, esoNo, sREsoScale);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 esoNo, String sREsoScale)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, esoNo, sREsoScale);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, esoNo, sREsoScale);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 esoNo, String sREsoScale)
		{
			esRegistrationEsoScaleQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.EsoNo == esoNo, query.SREsoScale == sREsoScale);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 esoNo, String sREsoScale)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("EsoNo", esoNo);
			parms.Add("SREsoScale", sREsoScale);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "EsoNo": this.str.EsoNo = (string)value; break;
						case "SREsoScale": this.str.SREsoScale = (string)value; break;
						case "ScaleStatus": this.str.ScaleStatus = (string)value; break;
						case "ScaleValue": this.str.ScaleValue = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EsoNo":

							if (value == null || value is System.Int32)
								this.EsoNo = (System.Int32?)value;
							break;
						case "ScaleValue":

							if (value == null || value is System.Int32)
								this.ScaleValue = (System.Int32?)value;
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
		/// Maps to RegistrationEsoScale.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationEsoScaleMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationEsoScaleMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoScale.EsoNo
		/// </summary>
		virtual public System.Int32? EsoNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationEsoScaleMetadata.ColumnNames.EsoNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationEsoScaleMetadata.ColumnNames.EsoNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoScale.SREsoScale
		/// </summary>
		virtual public System.String SREsoScale
		{
			get
			{
				return base.GetSystemString(RegistrationEsoScaleMetadata.ColumnNames.SREsoScale);
			}

			set
			{
				base.SetSystemString(RegistrationEsoScaleMetadata.ColumnNames.SREsoScale, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoScale.ScaleStatus
		/// </summary>
		virtual public System.String ScaleStatus
		{
			get
			{
				return base.GetSystemString(RegistrationEsoScaleMetadata.ColumnNames.ScaleStatus);
			}

			set
			{
				base.SetSystemString(RegistrationEsoScaleMetadata.ColumnNames.ScaleStatus, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoScale.ScaleValue
		/// </summary>
		virtual public System.Int32? ScaleValue
		{
			get
			{
				return base.GetSystemInt32(RegistrationEsoScaleMetadata.ColumnNames.ScaleValue);
			}

			set
			{
				base.SetSystemInt32(RegistrationEsoScaleMetadata.ColumnNames.ScaleValue, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoScale.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationEsoScaleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationEsoScaleMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoScale.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoScaleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoScaleMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRegistrationEsoScale entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String EsoNo
			{
				get
				{
					System.Int32? data = entity.EsoNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoNo = null;
					else entity.EsoNo = Convert.ToInt32(value);
				}
			}
			public System.String SREsoScale
			{
				get
				{
					System.String data = entity.SREsoScale;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREsoScale = null;
					else entity.SREsoScale = Convert.ToString(value);
				}
			}
			public System.String ScaleStatus
			{
				get
				{
					System.String data = entity.ScaleStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScaleStatus = null;
					else entity.ScaleStatus = Convert.ToString(value);
				}
			}
			public System.String ScaleValue
			{
				get
				{
					System.Int32? data = entity.ScaleValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScaleValue = null;
					else entity.ScaleValue = Convert.ToInt32(value);
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
			private esRegistrationEsoScale entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationEsoScaleQuery query)
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
				throw new Exception("esRegistrationEsoScale can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationEsoScale : esRegistrationEsoScale
	{
	}

	[Serializable]
	abstract public class esRegistrationEsoScaleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationEsoScaleMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoScaleMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem EsoNo
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoScaleMetadata.ColumnNames.EsoNo, esSystemType.Int32);
			}
		}

		public esQueryItem SREsoScale
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoScaleMetadata.ColumnNames.SREsoScale, esSystemType.String);
			}
		}

		public esQueryItem ScaleStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoScaleMetadata.ColumnNames.ScaleStatus, esSystemType.String);
			}
		}

		public esQueryItem ScaleValue
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoScaleMetadata.ColumnNames.ScaleValue, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoScaleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoScaleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationEsoScaleCollection")]
	public partial class RegistrationEsoScaleCollection : esRegistrationEsoScaleCollection, IEnumerable<RegistrationEsoScale>
	{
		public RegistrationEsoScaleCollection()
		{

		}

		public static implicit operator List<RegistrationEsoScale>(RegistrationEsoScaleCollection coll)
		{
			List<RegistrationEsoScale> list = new List<RegistrationEsoScale>();

			foreach (RegistrationEsoScale emp in coll)
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
				return RegistrationEsoScaleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationEsoScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationEsoScale(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationEsoScale();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationEsoScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationEsoScaleQuery();
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
		public bool Load(RegistrationEsoScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationEsoScale AddNew()
		{
			RegistrationEsoScale entity = base.AddNewEntity() as RegistrationEsoScale;

			return entity;
		}
		public RegistrationEsoScale FindByPrimaryKey(String registrationNo, Int32 esoNo, String sREsoScale)
		{
			return base.FindByPrimaryKey(registrationNo, esoNo, sREsoScale) as RegistrationEsoScale;
		}

		#region IEnumerable< RegistrationEsoScale> Members

		IEnumerator<RegistrationEsoScale> IEnumerable<RegistrationEsoScale>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationEsoScale;
			}
		}

		#endregion

		private RegistrationEsoScaleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationEsoScale' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationEsoScale ({RegistrationNo, EsoNo, SREsoScale})")]
	[Serializable]
	public partial class RegistrationEsoScale : esRegistrationEsoScale
	{
		public RegistrationEsoScale()
		{
		}

		public RegistrationEsoScale(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationEsoScaleMetadata.Meta();
			}
		}

		override protected esRegistrationEsoScaleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationEsoScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationEsoScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationEsoScaleQuery();
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
		public bool Load(RegistrationEsoScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationEsoScaleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationEsoScaleQuery : esRegistrationEsoScaleQuery
	{
		public RegistrationEsoScaleQuery()
		{

		}

		public RegistrationEsoScaleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationEsoScaleQuery";
		}
	}

	[Serializable]
	public partial class RegistrationEsoScaleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationEsoScaleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationEsoScaleMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoScaleMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoScaleMetadata.ColumnNames.EsoNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationEsoScaleMetadata.PropertyNames.EsoNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoScaleMetadata.ColumnNames.SREsoScale, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoScaleMetadata.PropertyNames.SREsoScale;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoScaleMetadata.ColumnNames.ScaleStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoScaleMetadata.PropertyNames.ScaleStatus;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoScaleMetadata.ColumnNames.ScaleValue, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationEsoScaleMetadata.PropertyNames.ScaleValue;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoScaleMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoScaleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoScaleMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoScaleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationEsoScaleMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string EsoNo = "EsoNo";
			public const string SREsoScale = "SREsoScale";
			public const string ScaleStatus = "ScaleStatus";
			public const string ScaleValue = "ScaleValue";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string EsoNo = "EsoNo";
			public const string SREsoScale = "SREsoScale";
			public const string ScaleStatus = "ScaleStatus";
			public const string ScaleValue = "ScaleValue";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(RegistrationEsoScaleMetadata))
			{
				if (RegistrationEsoScaleMetadata.mapDelegates == null)
				{
					RegistrationEsoScaleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationEsoScaleMetadata.meta == null)
				{
					RegistrationEsoScaleMetadata.meta = new RegistrationEsoScaleMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EsoNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREsoScale", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScaleStatus", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ScaleValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "RegistrationEsoScale";
				meta.Destination = "RegistrationEsoScale";
				meta.spInsert = "proc_RegistrationEsoScaleInsert";
				meta.spUpdate = "proc_RegistrationEsoScaleUpdate";
				meta.spDelete = "proc_RegistrationEsoScaleDelete";
				meta.spLoadAll = "proc_RegistrationEsoScaleLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationEsoScaleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationEsoScaleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
