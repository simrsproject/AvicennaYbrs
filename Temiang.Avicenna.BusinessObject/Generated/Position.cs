/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/12/2023 2:07:51 PM
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
	abstract public class esPositionCollection : esEntityCollectionWAuditLog
	{
		public esPositionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PositionCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionQuery query)
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
			this.InitQuery(query as esPositionQuery);
		}
		#endregion

		virtual public Position DetachEntity(Position entity)
		{
			return base.DetachEntity(entity) as Position;
		}

		virtual public Position AttachEntity(Position entity)
		{
			return base.AttachEntity(entity) as Position;
		}

		virtual public void Combine(PositionCollection collection)
		{
			base.Combine(collection);
		}

		new public Position this[int index]
		{
			get
			{
				return base[index] as Position;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Position);
		}
	}

	[Serializable]
	abstract public class esPosition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionQuery GetDynamicQuery()
		{
			return null;
		}

		public esPosition()
		{
		}

		public esPosition(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 positionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 positionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 positionID)
		{
			esPositionQuery query = this.GetDynamicQuery();
			query.Where(query.PositionID == positionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 positionID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionID", positionID);
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
						case "PositionID": this.str.PositionID = (string)value; break;
						case "PositionCode": this.str.PositionCode = (string)value; break;
						case "PositionName": this.str.PositionName = (string)value; break;
						case "Summary": this.str.Summary = (string)value; break;
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "PositionLevelID": this.str.PositionLevelID = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "GeneralQualification": this.str.GeneralQualification = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "PositionGradeID":

							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						case "PositionLevelID":

							if (value == null || value is System.Int32)
								this.PositionLevelID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to Position.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PositionMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to Position.PositionCode
		/// </summary>
		virtual public System.String PositionCode
		{
			get
			{
				return base.GetSystemString(PositionMetadata.ColumnNames.PositionCode);
			}

			set
			{
				base.SetSystemString(PositionMetadata.ColumnNames.PositionCode, value);
			}
		}
		/// <summary>
		/// Maps to Position.PositionName
		/// </summary>
		virtual public System.String PositionName
		{
			get
			{
				return base.GetSystemString(PositionMetadata.ColumnNames.PositionName);
			}

			set
			{
				base.SetSystemString(PositionMetadata.ColumnNames.PositionName, value);
			}
		}
		/// <summary>
		/// Maps to Position.Summary
		/// </summary>
		virtual public System.String Summary
		{
			get
			{
				return base.GetSystemString(PositionMetadata.ColumnNames.Summary);
			}

			set
			{
				base.SetSystemString(PositionMetadata.ColumnNames.Summary, value);
			}
		}
		/// <summary>
		/// Maps to Position.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(PositionMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(PositionMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to Position.PositionLevelID
		/// </summary>
		virtual public System.Int32? PositionLevelID
		{
			get
			{
				return base.GetSystemInt32(PositionMetadata.ColumnNames.PositionLevelID);
			}

			set
			{
				base.SetSystemInt32(PositionMetadata.ColumnNames.PositionLevelID, value);
			}
		}
		/// <summary>
		/// Maps to Position.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(PositionMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(PositionMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to Position.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(PositionMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(PositionMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to Position.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PositionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Position.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PositionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Position.GeneralQualification
		/// </summary>
		virtual public System.String GeneralQualification
		{
			get
			{
				return base.GetSystemString(PositionMetadata.ColumnNames.GeneralQualification);
			}

			set
			{
				base.SetSystemString(PositionMetadata.ColumnNames.GeneralQualification, value);
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
			public esStrings(esPosition entity)
			{
				this.entity = entity;
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String PositionCode
			{
				get
				{
					System.String data = entity.PositionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionCode = null;
					else entity.PositionCode = Convert.ToString(value);
				}
			}
			public System.String PositionName
			{
				get
				{
					System.String data = entity.PositionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionName = null;
					else entity.PositionName = Convert.ToString(value);
				}
			}
			public System.String Summary
			{
				get
				{
					System.String data = entity.Summary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Summary = null;
					else entity.Summary = Convert.ToString(value);
				}
			}
			public System.String PositionGradeID
			{
				get
				{
					System.Int32? data = entity.PositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeID = null;
					else entity.PositionGradeID = Convert.ToInt32(value);
				}
			}
			public System.String PositionLevelID
			{
				get
				{
					System.Int32? data = entity.PositionLevelID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionLevelID = null;
					else entity.PositionLevelID = Convert.ToInt32(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
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
			public System.String GeneralQualification
			{
				get
				{
					System.String data = entity.GeneralQualification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GeneralQualification = null;
					else entity.GeneralQualification = Convert.ToString(value);
				}
			}
			private esPosition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionQuery query)
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
				throw new Exception("esPosition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Position : esPosition
	{
	}

	[Serializable]
	abstract public class esPositionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PositionMetadata.Meta();
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionCode
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.PositionCode, esSystemType.String);
			}
		}

		public esQueryItem PositionName
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.PositionName, esSystemType.String);
			}
		}

		public esQueryItem Summary
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.Summary, esSystemType.String);
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionLevelID
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.PositionLevelID, esSystemType.Int32);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem GeneralQualification
		{
			get
			{
				return new esQueryItem(this, PositionMetadata.ColumnNames.GeneralQualification, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionCollection")]
	public partial class PositionCollection : esPositionCollection, IEnumerable<Position>
	{
		public PositionCollection()
		{

		}

		public static implicit operator List<Position>(PositionCollection coll)
		{
			List<Position> list = new List<Position>();

			foreach (Position emp in coll)
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
				return PositionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Position(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Position();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionQuery();
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
		public bool Load(PositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Position AddNew()
		{
			Position entity = base.AddNewEntity() as Position;

			return entity;
		}
		public Position FindByPrimaryKey(Int32 positionID)
		{
			return base.FindByPrimaryKey(positionID) as Position;
		}

		#region IEnumerable< Position> Members

		IEnumerator<Position> IEnumerable<Position>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Position;
			}
		}

		#endregion

		private PositionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Position' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Position ({PositionID})")]
	[Serializable]
	public partial class Position : esPosition
	{
		public Position()
		{
		}

		public Position(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionMetadata.Meta();
			}
		}

		override protected esPositionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionQuery();
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
		public bool Load(PositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PositionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PositionQuery : esPositionQuery
	{
		public PositionQuery()
		{

		}

		public PositionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PositionQuery";
		}
	}

	[Serializable]
	public partial class PositionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionMetadata.ColumnNames.PositionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionMetadata.PropertyNames.PositionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.PositionCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionMetadata.PropertyNames.PositionCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.PositionName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionMetadata.PropertyNames.PositionName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.Summary, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionMetadata.PropertyNames.Summary;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.PositionGradeID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.PositionLevelID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionMetadata.PropertyNames.PositionLevelID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.ValidFrom, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.ValidTo, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionMetadata.PropertyNames.ValidTo;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PositionMetadata.ColumnNames.GeneralQualification, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionMetadata.PropertyNames.GeneralQualification;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PositionMetadata Meta()
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
			public const string PositionID = "PositionID";
			public const string PositionCode = "PositionCode";
			public const string PositionName = "PositionName";
			public const string Summary = "Summary";
			public const string PositionGradeID = "PositionGradeID";
			public const string PositionLevelID = "PositionLevelID";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string GeneralQualification = "GeneralQualification";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PositionID = "PositionID";
			public const string PositionCode = "PositionCode";
			public const string PositionName = "PositionName";
			public const string Summary = "Summary";
			public const string PositionGradeID = "PositionGradeID";
			public const string PositionLevelID = "PositionLevelID";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string GeneralQualification = "GeneralQualification";
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
			lock (typeof(PositionMetadata))
			{
				if (PositionMetadata.mapDelegates == null)
				{
					PositionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PositionMetadata.meta == null)
				{
					PositionMetadata.meta = new PositionMetadata();
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

				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PositionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Summary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionLevelID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GeneralQualification", new esTypeMap("varchar", "System.String"));


				meta.Source = "Position";
				meta.Destination = "Position";
				meta.spInsert = "proc_PositionInsert";
				meta.spUpdate = "proc_PositionUpdate";
				meta.spDelete = "proc_PositionDelete";
				meta.spLoadAll = "proc_PositionLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
