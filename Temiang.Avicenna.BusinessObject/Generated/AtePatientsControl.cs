/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/13/2020 2:18:21 PM
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
	abstract public class esAtePatientsControlCollection : esEntityCollectionWAuditLog
	{
		public esAtePatientsControlCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AtePatientsControlCollection";
		}

		#region Query Logic
		protected void InitQuery(esAtePatientsControlQuery query)
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
			this.InitQuery(query as esAtePatientsControlQuery);
		}
		#endregion

		virtual public AtePatientsControl DetachEntity(AtePatientsControl entity)
		{
			return base.DetachEntity(entity) as AtePatientsControl;
		}

		virtual public AtePatientsControl AttachEntity(AtePatientsControl entity)
		{
			return base.AttachEntity(entity) as AtePatientsControl;
		}

		virtual public void Combine(AtePatientsControlCollection collection)
		{
			base.Combine(collection);
		}

		new public AtePatientsControl this[int index]
		{
			get
			{
				return base[index] as AtePatientsControl;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AtePatientsControl);
		}
	}

	[Serializable]
	abstract public class esAtePatientsControl : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAtePatientsControlQuery GetDynamicQuery()
		{
			return null;
		}

		public esAtePatientsControl()
		{
		}

		public esAtePatientsControl(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String orderNo, String sRMealSet)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNo, String sRMealSet)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo, sRMealSet);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo, sRMealSet);
		}

		private bool LoadByPrimaryKeyDynamic(String orderNo, String sRMealSet)
		{
			esAtePatientsControlQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo, query.SRMealSet == sRMealSet);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String orderNo, String sRMealSet)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo", orderNo);
			parms.Add("SRMealSet", sRMealSet);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;
						case "SRMealSet": this.str.SRMealSet = (string)value; break;
						case "ControlDate": this.str.ControlDate = (string)value; break;
						case "Carbohydrate": this.str.Carbohydrate = (string)value; break;
						case "VegetableSideDish": this.str.VegetableSideDish = (string)value; break;
						case "AnimalSideDish": this.str.AnimalSideDish = (string)value; break;
						case "Vegetable": this.str.Vegetable = (string)value; break;
						case "Fruit": this.str.Fruit = (string)value; break;
						case "Beverage": this.str.Beverage = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SREatingPatientStatusReason": this.str.SREatingPatientStatusReason = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ControlDate":

							if (value == null || value is System.DateTime)
								this.ControlDate = (System.DateTime?)value;
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
		/// Maps to AtePatientsControl.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.OrderNo);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.OrderNo, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.SRMealSet
		/// </summary>
		virtual public System.String SRMealSet
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.SRMealSet);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.SRMealSet, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.ControlDate
		/// </summary>
		virtual public System.DateTime? ControlDate
		{
			get
			{
				return base.GetSystemDateTime(AtePatientsControlMetadata.ColumnNames.ControlDate);
			}

			set
			{
				base.SetSystemDateTime(AtePatientsControlMetadata.ColumnNames.ControlDate, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.Carbohydrate
		/// </summary>
		virtual public System.String Carbohydrate
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.Carbohydrate);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.Carbohydrate, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.VegetableSideDish
		/// </summary>
		virtual public System.String VegetableSideDish
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.VegetableSideDish);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.VegetableSideDish, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.AnimalSideDish
		/// </summary>
		virtual public System.String AnimalSideDish
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.AnimalSideDish);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.AnimalSideDish, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.Vegetable
		/// </summary>
		virtual public System.String Vegetable
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.Vegetable);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.Vegetable, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.Fruit
		/// </summary>
		virtual public System.String Fruit
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.Fruit);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.Fruit, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.Beverage
		/// </summary>
		virtual public System.String Beverage
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.Beverage);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.Beverage, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AtePatientsControlMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AtePatientsControlMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AtePatientsControl.SREatingPatientStatusReason
		/// </summary>
		virtual public System.String SREatingPatientStatusReason
		{
			get
			{
				return base.GetSystemString(AtePatientsControlMetadata.ColumnNames.SREatingPatientStatusReason);
			}

			set
			{
				base.SetSystemString(AtePatientsControlMetadata.ColumnNames.SREatingPatientStatusReason, value);
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
			public esStrings(esAtePatientsControl entity)
			{
				this.entity = entity;
			}
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
			public System.String SRMealSet
			{
				get
				{
					System.String data = entity.SRMealSet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMealSet = null;
					else entity.SRMealSet = Convert.ToString(value);
				}
			}
			public System.String ControlDate
			{
				get
				{
					System.DateTime? data = entity.ControlDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControlDate = null;
					else entity.ControlDate = Convert.ToDateTime(value);
				}
			}
			public System.String Carbohydrate
			{
				get
				{
					System.String data = entity.Carbohydrate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Carbohydrate = null;
					else entity.Carbohydrate = Convert.ToString(value);
				}
			}
			public System.String VegetableSideDish
			{
				get
				{
					System.String data = entity.VegetableSideDish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VegetableSideDish = null;
					else entity.VegetableSideDish = Convert.ToString(value);
				}
			}
			public System.String AnimalSideDish
			{
				get
				{
					System.String data = entity.AnimalSideDish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnimalSideDish = null;
					else entity.AnimalSideDish = Convert.ToString(value);
				}
			}
			public System.String Vegetable
			{
				get
				{
					System.String data = entity.Vegetable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Vegetable = null;
					else entity.Vegetable = Convert.ToString(value);
				}
			}
			public System.String Fruit
			{
				get
				{
					System.String data = entity.Fruit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Fruit = null;
					else entity.Fruit = Convert.ToString(value);
				}
			}
			public System.String Beverage
			{
				get
				{
					System.String data = entity.Beverage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Beverage = null;
					else entity.Beverage = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			public System.String SREatingPatientStatusReason
			{
				get
				{
					System.String data = entity.SREatingPatientStatusReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREatingPatientStatusReason = null;
					else entity.SREatingPatientStatusReason = Convert.ToString(value);
				}
			}
			private esAtePatientsControl entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAtePatientsControlQuery query)
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
				throw new Exception("esAtePatientsControl can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AtePatientsControl : esAtePatientsControl
	{
	}

	[Serializable]
	abstract public class esAtePatientsControlQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AtePatientsControlMetadata.Meta();
			}
		}

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		}

		public esQueryItem SRMealSet
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.SRMealSet, esSystemType.String);
			}
		}

		public esQueryItem ControlDate
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.ControlDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Carbohydrate
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.Carbohydrate, esSystemType.String);
			}
		}

		public esQueryItem VegetableSideDish
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.VegetableSideDish, esSystemType.String);
			}
		}

		public esQueryItem AnimalSideDish
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.AnimalSideDish, esSystemType.String);
			}
		}

		public esQueryItem Vegetable
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.Vegetable, esSystemType.String);
			}
		}

		public esQueryItem Fruit
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.Fruit, esSystemType.String);
			}
		}

		public esQueryItem Beverage
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.Beverage, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SREatingPatientStatusReason
		{
			get
			{
				return new esQueryItem(this, AtePatientsControlMetadata.ColumnNames.SREatingPatientStatusReason, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AtePatientsControlCollection")]
	public partial class AtePatientsControlCollection : esAtePatientsControlCollection, IEnumerable<AtePatientsControl>
	{
		public AtePatientsControlCollection()
		{

		}

		public static implicit operator List<AtePatientsControl>(AtePatientsControlCollection coll)
		{
			List<AtePatientsControl> list = new List<AtePatientsControl>();

			foreach (AtePatientsControl emp in coll)
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
				return AtePatientsControlMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AtePatientsControlQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AtePatientsControl(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AtePatientsControl();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AtePatientsControlQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AtePatientsControlQuery();
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
		public bool Load(AtePatientsControlQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AtePatientsControl AddNew()
		{
			AtePatientsControl entity = base.AddNewEntity() as AtePatientsControl;

			return entity;
		}
		public AtePatientsControl FindByPrimaryKey(String orderNo, String sRMealSet)
		{
			return base.FindByPrimaryKey(orderNo, sRMealSet) as AtePatientsControl;
		}

		#region IEnumerable< AtePatientsControl> Members

		IEnumerator<AtePatientsControl> IEnumerable<AtePatientsControl>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AtePatientsControl;
			}
		}

		#endregion

		private AtePatientsControlQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AtePatientsControl' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AtePatientsControl ({OrderNo, SRMealSet})")]
	[Serializable]
	public partial class AtePatientsControl : esAtePatientsControl
	{
		public AtePatientsControl()
		{
		}

		public AtePatientsControl(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AtePatientsControlMetadata.Meta();
			}
		}

		override protected esAtePatientsControlQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AtePatientsControlQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AtePatientsControlQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AtePatientsControlQuery();
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
		public bool Load(AtePatientsControlQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AtePatientsControlQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AtePatientsControlQuery : esAtePatientsControlQuery
	{
		public AtePatientsControlQuery()
		{

		}

		public AtePatientsControlQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AtePatientsControlQuery";
		}
	}

	[Serializable]
	public partial class AtePatientsControlMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AtePatientsControlMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.SRMealSet, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.SRMealSet;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.ControlDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.ControlDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.Carbohydrate, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.Carbohydrate;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.VegetableSideDish, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.VegetableSideDish;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.AnimalSideDish, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.AnimalSideDish;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.Vegetable, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.Vegetable;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.Fruit, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.Fruit;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.Beverage, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.Beverage;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.Notes, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AtePatientsControlMetadata.ColumnNames.SREatingPatientStatusReason, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AtePatientsControlMetadata.PropertyNames.SREatingPatientStatusReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AtePatientsControlMetadata Meta()
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
			public const string OrderNo = "OrderNo";
			public const string SRMealSet = "SRMealSet";
			public const string ControlDate = "ControlDate";
			public const string Carbohydrate = "Carbohydrate";
			public const string VegetableSideDish = "VegetableSideDish";
			public const string AnimalSideDish = "AnimalSideDish";
			public const string Vegetable = "Vegetable";
			public const string Fruit = "Fruit";
			public const string Beverage = "Beverage";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREatingPatientStatusReason = "SREatingPatientStatusReason";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string OrderNo = "OrderNo";
			public const string SRMealSet = "SRMealSet";
			public const string ControlDate = "ControlDate";
			public const string Carbohydrate = "Carbohydrate";
			public const string VegetableSideDish = "VegetableSideDish";
			public const string AnimalSideDish = "AnimalSideDish";
			public const string Vegetable = "Vegetable";
			public const string Fruit = "Fruit";
			public const string Beverage = "Beverage";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREatingPatientStatusReason = "SREatingPatientStatusReason";
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
			lock (typeof(AtePatientsControlMetadata))
			{
				if (AtePatientsControlMetadata.mapDelegates == null)
				{
					AtePatientsControlMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AtePatientsControlMetadata.meta == null)
				{
					AtePatientsControlMetadata.meta = new AtePatientsControlMetadata();
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

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ControlDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Carbohydrate", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("VegetableSideDish", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("AnimalSideDish", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Vegetable", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Fruit", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Beverage", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREatingPatientStatusReason", new esTypeMap("varchar", "System.String"));


				meta.Source = "AtePatientsControl";
				meta.Destination = "AtePatientsControl";
				meta.spInsert = "proc_AtePatientsControlInsert";
				meta.spUpdate = "proc_AtePatientsControlUpdate";
				meta.spDelete = "proc_AtePatientsControlDelete";
				meta.spLoadAll = "proc_AtePatientsControlLoadAll";
				meta.spLoadByPrimaryKey = "proc_AtePatientsControlLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AtePatientsControlMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
