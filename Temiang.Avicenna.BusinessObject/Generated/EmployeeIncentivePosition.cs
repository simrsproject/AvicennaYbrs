/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/5/2023 9:34:30 AM
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
	abstract public class esEmployeeIncentivePositionCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeIncentivePositionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeIncentivePositionCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeIncentivePositionQuery query)
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
			this.InitQuery(query as esEmployeeIncentivePositionQuery);
		}
		#endregion

		virtual public EmployeeIncentivePosition DetachEntity(EmployeeIncentivePosition entity)
		{
			return base.DetachEntity(entity) as EmployeeIncentivePosition;
		}

		virtual public EmployeeIncentivePosition AttachEntity(EmployeeIncentivePosition entity)
		{
			return base.AttachEntity(entity) as EmployeeIncentivePosition;
		}

		virtual public void Combine(EmployeeIncentivePositionCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeIncentivePosition this[int index]
		{
			get
			{
				return base[index] as EmployeeIncentivePosition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeIncentivePosition);
		}
	}

	[Serializable]
	abstract public class esEmployeeIncentivePosition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeIncentivePositionQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeIncentivePosition()
		{
		}

		public esEmployeeIncentivePosition(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 incentivePositionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(incentivePositionID);
			else
				return LoadByPrimaryKeyStoredProcedure(incentivePositionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 incentivePositionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(incentivePositionID);
			else
				return LoadByPrimaryKeyStoredProcedure(incentivePositionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 incentivePositionID)
		{
			esEmployeeIncentivePositionQuery query = this.GetDynamicQuery();
			query.Where(query.IncentivePositionID == incentivePositionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 incentivePositionID)
		{
			esParameters parms = new esParameters();
			parms.Add("IncentivePositionID", incentivePositionID);
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
						case "IncentivePositionID": this.str.IncentivePositionID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRIncentiveServiceUnitGroup": this.str.SRIncentiveServiceUnitGroup = (string)value; break;
						case "SRIncentivePositionGroup": this.str.SRIncentivePositionGroup = (string)value; break;
						case "SRIncentivePosition": this.str.SRIncentivePosition = (string)value; break;
						case "IncentivePositionPoints": this.str.IncentivePositionPoints = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IncentivePositionID":

							if (value == null || value is System.Int32)
								this.IncentivePositionID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "IncentivePositionPoints":

							if (value == null || value is System.Decimal)
								this.IncentivePositionPoints = (System.Decimal?)value;
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
		/// Maps to EmployeeIncentivePosition.IncentivePositionID
		/// </summary>
		virtual public System.Int32? IncentivePositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionID);
			}

			set
			{
				base.SetSystemInt32(EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeIncentivePositionMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeIncentivePositionMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.SRIncentiveServiceUnitGroup
		/// </summary>
		virtual public System.String SRIncentiveServiceUnitGroup
		{
			get
			{
				return base.GetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentiveServiceUnitGroup);
			}

			set
			{
				base.SetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentiveServiceUnitGroup, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.SRIncentivePositionGroup
		/// </summary>
		virtual public System.String SRIncentivePositionGroup
		{
			get
			{
				return base.GetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePositionGroup);
			}

			set
			{
				base.SetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePositionGroup, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.SRIncentivePosition
		/// </summary>
		virtual public System.String SRIncentivePosition
		{
			get
			{
				return base.GetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePosition);
			}

			set
			{
				base.SetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePosition, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.IncentivePositionPoints
		/// </summary>
		virtual public System.Decimal? IncentivePositionPoints
		{
			get
			{
				return base.GetSystemDecimal(EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionPoints);
			}

			set
			{
				base.SetSystemDecimal(EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionPoints, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeIncentivePositionMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeIncentivePositionMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeIncentivePositionMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeeIncentivePositionMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentivePosition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeIncentivePosition entity)
			{
				this.entity = entity;
			}
			public System.String IncentivePositionID
			{
				get
				{
					System.Int32? data = entity.IncentivePositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncentivePositionID = null;
					else entity.IncentivePositionID = Convert.ToInt32(value);
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
			public System.String SRIncentiveServiceUnitGroup
			{
				get
				{
					System.String data = entity.SRIncentiveServiceUnitGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentiveServiceUnitGroup = null;
					else entity.SRIncentiveServiceUnitGroup = Convert.ToString(value);
				}
			}
			public System.String SRIncentivePositionGroup
			{
				get
				{
					System.String data = entity.SRIncentivePositionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentivePositionGroup = null;
					else entity.SRIncentivePositionGroup = Convert.ToString(value);
				}
			}
			public System.String SRIncentivePosition
			{
				get
				{
					System.String data = entity.SRIncentivePosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentivePosition = null;
					else entity.SRIncentivePosition = Convert.ToString(value);
				}
			}
			public System.String IncentivePositionPoints
			{
				get
				{
					System.Decimal? data = entity.IncentivePositionPoints;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncentivePositionPoints = null;
					else entity.IncentivePositionPoints = Convert.ToDecimal(value);
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
			private esEmployeeIncentivePosition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeIncentivePositionQuery query)
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
				throw new Exception("esEmployeeIncentivePosition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeIncentivePosition : esEmployeeIncentivePosition
	{
	}

	[Serializable]
	abstract public class esEmployeeIncentivePositionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeIncentivePositionMetadata.Meta();
			}
		}

		public esQueryItem IncentivePositionID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRIncentiveServiceUnitGroup
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.SRIncentiveServiceUnitGroup, esSystemType.String);
			}
		}

		public esQueryItem SRIncentivePositionGroup
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePositionGroup, esSystemType.String);
			}
		}

		public esQueryItem SRIncentivePosition
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePosition, esSystemType.String);
			}
		}

		public esQueryItem IncentivePositionPoints
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionPoints, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeIncentivePositionCollection")]
	public partial class EmployeeIncentivePositionCollection : esEmployeeIncentivePositionCollection, IEnumerable<EmployeeIncentivePosition>
	{
		public EmployeeIncentivePositionCollection()
		{

		}

		public static implicit operator List<EmployeeIncentivePosition>(EmployeeIncentivePositionCollection coll)
		{
			List<EmployeeIncentivePosition> list = new List<EmployeeIncentivePosition>();

			foreach (EmployeeIncentivePosition emp in coll)
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
				return EmployeeIncentivePositionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeIncentivePositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeIncentivePosition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeIncentivePosition();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeIncentivePositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeIncentivePositionQuery();
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
		public bool Load(EmployeeIncentivePositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeIncentivePosition AddNew()
		{
			EmployeeIncentivePosition entity = base.AddNewEntity() as EmployeeIncentivePosition;

			return entity;
		}
		public EmployeeIncentivePosition FindByPrimaryKey(Int32 incentivePositionID)
		{
			return base.FindByPrimaryKey(incentivePositionID) as EmployeeIncentivePosition;
		}

		#region IEnumerable< EmployeeIncentivePosition> Members

		IEnumerator<EmployeeIncentivePosition> IEnumerable<EmployeeIncentivePosition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeIncentivePosition;
			}
		}

		#endregion

		private EmployeeIncentivePositionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeIncentivePosition' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeIncentivePosition ({IncentivePositionID})")]
	[Serializable]
	public partial class EmployeeIncentivePosition : esEmployeeIncentivePosition
	{
		public EmployeeIncentivePosition()
		{
		}

		public EmployeeIncentivePosition(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeIncentivePositionMetadata.Meta();
			}
		}

		override protected esEmployeeIncentivePositionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeIncentivePositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeIncentivePositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeIncentivePositionQuery();
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
		public bool Load(EmployeeIncentivePositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeIncentivePositionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeIncentivePositionQuery : esEmployeeIncentivePositionQuery
	{
		public EmployeeIncentivePositionQuery()
		{

		}

		public EmployeeIncentivePositionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeIncentivePositionQuery";
		}
	}

	[Serializable]
	public partial class EmployeeIncentivePositionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeIncentivePositionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.IncentivePositionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentiveServiceUnitGroup, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.SRIncentiveServiceUnitGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePositionGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.SRIncentivePositionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.SRIncentivePosition, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.SRIncentivePosition;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.IncentivePositionPoints, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.IncentivePositionPoints;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.ValidFrom, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.ValidTo, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentivePositionMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentivePositionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeIncentivePositionMetadata Meta()
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
			public const string IncentivePositionID = "IncentivePositionID";
			public const string PersonID = "PersonID";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string SRIncentivePositionGroup = "SRIncentivePositionGroup";
			public const string SRIncentivePosition = "SRIncentivePosition";
			public const string IncentivePositionPoints = "IncentivePositionPoints";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string IncentivePositionID = "IncentivePositionID";
			public const string PersonID = "PersonID";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string SRIncentivePositionGroup = "SRIncentivePositionGroup";
			public const string SRIncentivePosition = "SRIncentivePosition";
			public const string IncentivePositionPoints = "IncentivePositionPoints";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
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
			lock (typeof(EmployeeIncentivePositionMetadata))
			{
				if (EmployeeIncentivePositionMetadata.mapDelegates == null)
				{
					EmployeeIncentivePositionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeIncentivePositionMetadata.meta == null)
				{
					EmployeeIncentivePositionMetadata.meta = new EmployeeIncentivePositionMetadata();
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

				meta.AddTypeMap("IncentivePositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRIncentiveServiceUnitGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncentivePositionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncentivePosition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncentivePositionPoints", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeIncentivePosition";
				meta.Destination = "EmployeeIncentivePosition";
				meta.spInsert = "proc_EmployeeIncentivePositionInsert";
				meta.spUpdate = "proc_EmployeeIncentivePositionUpdate";
				meta.spDelete = "proc_EmployeeIncentivePositionDelete";
				meta.spLoadAll = "proc_EmployeeIncentivePositionLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeIncentivePositionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeIncentivePositionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
