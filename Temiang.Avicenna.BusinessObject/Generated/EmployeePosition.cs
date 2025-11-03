/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/18/2022 9:42:43 PM
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
	abstract public class esEmployeePositionCollection : esEntityCollectionWAuditLog
	{
		public esEmployeePositionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeePositionCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeePositionQuery query)
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
			this.InitQuery(query as esEmployeePositionQuery);
		}
		#endregion

		virtual public EmployeePosition DetachEntity(EmployeePosition entity)
		{
			return base.DetachEntity(entity) as EmployeePosition;
		}

		virtual public EmployeePosition AttachEntity(EmployeePosition entity)
		{
			return base.AttachEntity(entity) as EmployeePosition;
		}

		virtual public void Combine(EmployeePositionCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeePosition this[int index]
		{
			get
			{
				return base[index] as EmployeePosition;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeePosition);
		}
	}

	[Serializable]
	abstract public class esEmployeePosition : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeePositionQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeePosition()
		{
		}

		public esEmployeePosition(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeePositionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeePositionID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeePositionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeePositionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeePositionID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeePositionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeePositionID)
		{
			esEmployeePositionQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeePositionID == employeePositionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeePositionID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeePositionID", employeePositionID);
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
						case "EmployeePositionID": this.str.EmployeePositionID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "IsPrimaryPosition": this.str.IsPrimaryPosition = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "AssignmentNo": this.str.AssignmentNo = (string)value; break;
						case "ResignmentNo": this.str.ResignmentNo = (string)value; break;
						case "PositionDescription": this.str.PositionDescription = (string)value; break;
						case "CoorporateGradeID": this.str.CoorporateGradeID = (string)value; break;
						case "CoorporateGradeValue": this.str.CoorporateGradeValue = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeePositionID":

							if (value == null || value is System.Int32)
								this.EmployeePositionID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "IsPrimaryPosition":

							if (value == null || value is System.Boolean)
								this.IsPrimaryPosition = (System.Boolean?)value;
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
						case "CoorporateGradeID":

							if (value == null || value is System.Int32)
								this.CoorporateGradeID = (System.Int32?)value;
							break;
						case "CoorporateGradeValue":

							if (value == null || value is System.Decimal)
								this.CoorporateGradeValue = (System.Decimal?)value;
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
		/// Maps to EmployeePosition.EmployeePositionID
		/// </summary>
		virtual public System.Int32? EmployeePositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionMetadata.ColumnNames.EmployeePositionID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionMetadata.ColumnNames.EmployeePositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.IsPrimaryPosition
		/// </summary>
		virtual public System.Boolean? IsPrimaryPosition
		{
			get
			{
				return base.GetSystemBoolean(EmployeePositionMetadata.ColumnNames.IsPrimaryPosition);
			}

			set
			{
				base.SetSystemBoolean(EmployeePositionMetadata.ColumnNames.IsPrimaryPosition, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeePositionMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeePositionMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeePositionMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeePositionMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePositionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePositionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePositionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePositionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.AssignmentNo
		/// </summary>
		virtual public System.String AssignmentNo
		{
			get
			{
				return base.GetSystemString(EmployeePositionMetadata.ColumnNames.AssignmentNo);
			}

			set
			{
				base.SetSystemString(EmployeePositionMetadata.ColumnNames.AssignmentNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.ResignmentNo
		/// </summary>
		virtual public System.String ResignmentNo
		{
			get
			{
				return base.GetSystemString(EmployeePositionMetadata.ColumnNames.ResignmentNo);
			}

			set
			{
				base.SetSystemString(EmployeePositionMetadata.ColumnNames.ResignmentNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.PositionDescription
		/// </summary>
		virtual public System.String PositionDescription
		{
			get
			{
				return base.GetSystemString(EmployeePositionMetadata.ColumnNames.PositionDescription);
			}

			set
			{
				base.SetSystemString(EmployeePositionMetadata.ColumnNames.PositionDescription, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.CoorporateGradeID
		/// </summary>
		virtual public System.Int32? CoorporateGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeePositionMetadata.ColumnNames.CoorporateGradeID);
			}

			set
			{
				base.SetSystemInt32(EmployeePositionMetadata.ColumnNames.CoorporateGradeID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePosition.CoorporateGradeValue
		/// </summary>
		virtual public System.Decimal? CoorporateGradeValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeePositionMetadata.ColumnNames.CoorporateGradeValue);
			}

			set
			{
				base.SetSystemDecimal(EmployeePositionMetadata.ColumnNames.CoorporateGradeValue, value);
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
			public esStrings(esEmployeePosition entity)
			{
				this.entity = entity;
			}
			public System.String EmployeePositionID
			{
				get
				{
					System.Int32? data = entity.EmployeePositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeePositionID = null;
					else entity.EmployeePositionID = Convert.ToInt32(value);
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
			public System.String IsPrimaryPosition
			{
				get
				{
					System.Boolean? data = entity.IsPrimaryPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPrimaryPosition = null;
					else entity.IsPrimaryPosition = Convert.ToBoolean(value);
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
			public System.String AssignmentNo
			{
				get
				{
					System.String data = entity.AssignmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssignmentNo = null;
					else entity.AssignmentNo = Convert.ToString(value);
				}
			}
			public System.String ResignmentNo
			{
				get
				{
					System.String data = entity.ResignmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResignmentNo = null;
					else entity.ResignmentNo = Convert.ToString(value);
				}
			}
			public System.String PositionDescription
			{
				get
				{
					System.String data = entity.PositionDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionDescription = null;
					else entity.PositionDescription = Convert.ToString(value);
				}
			}
			public System.String CoorporateGradeID
			{
				get
				{
					System.Int32? data = entity.CoorporateGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeID = null;
					else entity.CoorporateGradeID = Convert.ToInt32(value);
				}
			}
			public System.String CoorporateGradeValue
			{
				get
				{
					System.Decimal? data = entity.CoorporateGradeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeValue = null;
					else entity.CoorporateGradeValue = Convert.ToDecimal(value);
				}
			}
			private esEmployeePosition entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeePositionQuery query)
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
				throw new Exception("esEmployeePosition can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeePosition : esEmployeePosition
	{
	}

	[Serializable]
	abstract public class esEmployeePositionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeePositionMetadata.Meta();
			}
		}

		public esQueryItem EmployeePositionID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.EmployeePositionID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem IsPrimaryPosition
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.IsPrimaryPosition, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem AssignmentNo
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.AssignmentNo, esSystemType.String);
			}
		}

		public esQueryItem ResignmentNo
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.ResignmentNo, esSystemType.String);
			}
		}

		public esQueryItem PositionDescription
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.PositionDescription, esSystemType.String);
			}
		}

		public esQueryItem CoorporateGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.CoorporateGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem CoorporateGradeValue
		{
			get
			{
				return new esQueryItem(this, EmployeePositionMetadata.ColumnNames.CoorporateGradeValue, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeePositionCollection")]
	public partial class EmployeePositionCollection : esEmployeePositionCollection, IEnumerable<EmployeePosition>
	{
		public EmployeePositionCollection()
		{

		}

		public static implicit operator List<EmployeePosition>(EmployeePositionCollection coll)
		{
			List<EmployeePosition> list = new List<EmployeePosition>();

			foreach (EmployeePosition emp in coll)
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
				return EmployeePositionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeePosition(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeePosition();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeePositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePositionQuery();
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
		public bool Load(EmployeePositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeePosition AddNew()
		{
			EmployeePosition entity = base.AddNewEntity() as EmployeePosition;

			return entity;
		}
		public EmployeePosition FindByPrimaryKey(Int32 employeePositionID)
		{
			return base.FindByPrimaryKey(employeePositionID) as EmployeePosition;
		}

		#region IEnumerable< EmployeePosition> Members

		IEnumerator<EmployeePosition> IEnumerable<EmployeePosition>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeePosition;
			}
		}

		#endregion

		private EmployeePositionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeePosition' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeePosition ({EmployeePositionID})")]
	[Serializable]
	public partial class EmployeePosition : esEmployeePosition
	{
		public EmployeePosition()
		{
		}

		public EmployeePosition(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeePositionMetadata.Meta();
			}
		}

		override protected esEmployeePositionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePositionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeePositionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePositionQuery();
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
		public bool Load(EmployeePositionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeePositionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeePositionQuery : esEmployeePositionQuery
	{
		public EmployeePositionQuery()
		{

		}

		public EmployeePositionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeePositionQuery";
		}
	}

	[Serializable]
	public partial class EmployeePositionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeePositionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.EmployeePositionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.EmployeePositionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.PositionID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.IsPrimaryPosition, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.IsPrimaryPosition;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.ValidFrom, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.ValidTo, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.AssignmentNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.AssignmentNo;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.ResignmentNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.ResignmentNo;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.PositionDescription, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.PositionDescription;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.CoorporateGradeID, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.CoorporateGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePositionMetadata.ColumnNames.CoorporateGradeValue, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeePositionMetadata.PropertyNames.CoorporateGradeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeePositionMetadata Meta()
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
			public const string EmployeePositionID = "EmployeePositionID";
			public const string PersonID = "PersonID";
			public const string PositionID = "PositionID";
			public const string IsPrimaryPosition = "IsPrimaryPosition";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AssignmentNo = "AssignmentNo";
			public const string ResignmentNo = "ResignmentNo";
			public const string PositionDescription = "PositionDescription";
			public const string CoorporateGradeID = "CoorporateGradeID";
			public const string CoorporateGradeValue = "CoorporateGradeValue";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeePositionID = "EmployeePositionID";
			public const string PersonID = "PersonID";
			public const string PositionID = "PositionID";
			public const string IsPrimaryPosition = "IsPrimaryPosition";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string AssignmentNo = "AssignmentNo";
			public const string ResignmentNo = "ResignmentNo";
			public const string PositionDescription = "PositionDescription";
			public const string CoorporateGradeID = "CoorporateGradeID";
			public const string CoorporateGradeValue = "CoorporateGradeValue";
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
			lock (typeof(EmployeePositionMetadata))
			{
				if (EmployeePositionMetadata.mapDelegates == null)
				{
					EmployeePositionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeePositionMetadata.meta == null)
				{
					EmployeePositionMetadata.meta = new EmployeePositionMetadata();
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

				meta.AddTypeMap("EmployeePositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsPrimaryPosition", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssignmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResignmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoorporateGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CoorporateGradeValue", new esTypeMap("decimal", "System.Decimal"));


				meta.Source = "EmployeePosition";
				meta.Destination = "EmployeePosition";
				meta.spInsert = "proc_EmployeePositionInsert";
				meta.spUpdate = "proc_EmployeePositionUpdate";
				meta.spDelete = "proc_EmployeePositionDelete";
				meta.spLoadAll = "proc_EmployeePositionLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeePositionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeePositionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
