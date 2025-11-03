/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/11/2020 11:35:46 AM
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
	abstract public class esFoodCollection : esEntityCollectionWAuditLog
	{
		public esFoodCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "FoodCollection";
		}

		#region Query Logic
		protected void InitQuery(esFoodQuery query)
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
			this.InitQuery(query as esFoodQuery);
		}
		#endregion

		virtual public Food DetachEntity(Food entity)
		{
			return base.DetachEntity(entity) as Food;
		}

		virtual public Food AttachEntity(Food entity)
		{
			return base.AttachEntity(entity) as Food;
		}

		virtual public void Combine(FoodCollection collection)
		{
			base.Combine(collection);
		}

		new public Food this[int index]
		{
			get
			{
				return base[index] as Food;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Food);
		}
	}

	[Serializable]
	abstract public class esFood : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esFoodQuery GetDynamicQuery()
		{
			return null;
		}

		public esFood()
		{
		}

		public esFood(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String foodID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(foodID);
			else
				return LoadByPrimaryKeyStoredProcedure(foodID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String foodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(foodID);
			else
				return LoadByPrimaryKeyStoredProcedure(foodID);
		}

		private bool LoadByPrimaryKeyDynamic(String foodID)
		{
			esFoodQuery query = this.GetDynamicQuery();
			query.Where(query.FoodID == foodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String foodID)
		{
			esParameters parms = new esParameters();
			parms.Add("FoodID", foodID);
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
						case "FoodID": this.str.FoodID = (string)value; break;
						case "FoodName": this.str.FoodName = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Weight": this.str.Weight = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "QtyPortion": this.str.QtyPortion = (string)value; break;
						case "SRFoodGroup1": this.str.SRFoodGroup1 = (string)value; break;
						case "SRFoodGroup2": this.str.SRFoodGroup2 = (string)value; break;
						case "IsForSpecialCondition": this.str.IsForSpecialCondition = (string)value; break;
						case "IsSalesAvailable": this.str.IsSalesAvailable = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPackage": this.str.IsPackage = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Weight":

							if (value == null || value is System.Decimal)
								this.Weight = (System.Decimal?)value;
							break;
						case "QtyPortion":

							if (value == null || value is System.Decimal)
								this.QtyPortion = (System.Decimal?)value;
							break;
						case "IsForSpecialCondition":

							if (value == null || value is System.Boolean)
								this.IsForSpecialCondition = (System.Boolean?)value;
							break;
						case "IsSalesAvailable":

							if (value == null || value is System.Boolean)
								this.IsSalesAvailable = (System.Boolean?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPackage":

							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
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
		/// Maps to Food.FoodID
		/// </summary>
		virtual public System.String FoodID
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.FoodID);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.FoodID, value);
			}
		}
		/// <summary>
		/// Maps to Food.FoodName
		/// </summary>
		virtual public System.String FoodName
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.FoodName);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.FoodName, value);
			}
		}
		/// <summary>
		/// Maps to Food.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Food.Weight
		/// </summary>
		virtual public System.Decimal? Weight
		{
			get
			{
				return base.GetSystemDecimal(FoodMetadata.ColumnNames.Weight);
			}

			set
			{
				base.SetSystemDecimal(FoodMetadata.ColumnNames.Weight, value);
			}
		}
		/// <summary>
		/// Maps to Food.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to Food.QtyPortion
		/// </summary>
		virtual public System.Decimal? QtyPortion
		{
			get
			{
				return base.GetSystemDecimal(FoodMetadata.ColumnNames.QtyPortion);
			}

			set
			{
				base.SetSystemDecimal(FoodMetadata.ColumnNames.QtyPortion, value);
			}
		}
		/// <summary>
		/// Maps to Food.SRFoodGroup1
		/// </summary>
		virtual public System.String SRFoodGroup1
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.SRFoodGroup1);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.SRFoodGroup1, value);
			}
		}
		/// <summary>
		/// Maps to Food.SRFoodGroup2
		/// </summary>
		virtual public System.String SRFoodGroup2
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.SRFoodGroup2);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.SRFoodGroup2, value);
			}
		}
		/// <summary>
		/// Maps to Food.IsForSpecialCondition
		/// </summary>
		virtual public System.Boolean? IsForSpecialCondition
		{
			get
			{
				return base.GetSystemBoolean(FoodMetadata.ColumnNames.IsForSpecialCondition);
			}

			set
			{
				base.SetSystemBoolean(FoodMetadata.ColumnNames.IsForSpecialCondition, value);
			}
		}
		/// <summary>
		/// Maps to Food.IsSalesAvailable
		/// </summary>
		virtual public System.Boolean? IsSalesAvailable
		{
			get
			{
				return base.GetSystemBoolean(FoodMetadata.ColumnNames.IsSalesAvailable);
			}

			set
			{
				base.SetSystemBoolean(FoodMetadata.ColumnNames.IsSalesAvailable, value);
			}
		}
		/// <summary>
		/// Maps to Food.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to Food.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(FoodMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(FoodMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Food.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(FoodMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(FoodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Food.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(FoodMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(FoodMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Food.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(FoodMetadata.ColumnNames.IsPackage);
			}

			set
			{
				base.SetSystemBoolean(FoodMetadata.ColumnNames.IsPackage, value);
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
			public esStrings(esFood entity)
			{
				this.entity = entity;
			}
			public System.String FoodID
			{
				get
				{
					System.String data = entity.FoodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoodID = null;
					else entity.FoodID = Convert.ToString(value);
				}
			}
			public System.String FoodName
			{
				get
				{
					System.String data = entity.FoodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoodName = null;
					else entity.FoodName = Convert.ToString(value);
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
			public System.String Weight
			{
				get
				{
					System.Decimal? data = entity.Weight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Weight = null;
					else entity.Weight = Convert.ToDecimal(value);
				}
			}
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
				}
			}
			public System.String QtyPortion
			{
				get
				{
					System.Decimal? data = entity.QtyPortion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyPortion = null;
					else entity.QtyPortion = Convert.ToDecimal(value);
				}
			}
			public System.String SRFoodGroup1
			{
				get
				{
					System.String data = entity.SRFoodGroup1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFoodGroup1 = null;
					else entity.SRFoodGroup1 = Convert.ToString(value);
				}
			}
			public System.String SRFoodGroup2
			{
				get
				{
					System.String data = entity.SRFoodGroup2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFoodGroup2 = null;
					else entity.SRFoodGroup2 = Convert.ToString(value);
				}
			}
			public System.String IsForSpecialCondition
			{
				get
				{
					System.Boolean? data = entity.IsForSpecialCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsForSpecialCondition = null;
					else entity.IsForSpecialCondition = Convert.ToBoolean(value);
				}
			}
			public System.String IsSalesAvailable
			{
				get
				{
					System.Boolean? data = entity.IsSalesAvailable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSalesAvailable = null;
					else entity.IsSalesAvailable = Convert.ToBoolean(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			public System.String IsPackage
			{
				get
				{
					System.Boolean? data = entity.IsPackage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackage = null;
					else entity.IsPackage = Convert.ToBoolean(value);
				}
			}
			private esFood entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esFoodQuery query)
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
				throw new Exception("esFood can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Food : esFood
	{
	}

	[Serializable]
	abstract public class esFoodQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return FoodMetadata.Meta();
			}
		}

		public esQueryItem FoodID
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.FoodID, esSystemType.String);
			}
		}

		public esQueryItem FoodName
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.FoodName, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Weight
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.Weight, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem QtyPortion
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.QtyPortion, esSystemType.Decimal);
			}
		}

		public esQueryItem SRFoodGroup1
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.SRFoodGroup1, esSystemType.String);
			}
		}

		public esQueryItem SRFoodGroup2
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.SRFoodGroup2, esSystemType.String);
			}
		}

		public esQueryItem IsForSpecialCondition
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.IsForSpecialCondition, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSalesAvailable
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.IsSalesAvailable, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, FoodMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("FoodCollection")]
	public partial class FoodCollection : esFoodCollection, IEnumerable<Food>
	{
		public FoodCollection()
		{

		}

		public static implicit operator List<Food>(FoodCollection coll)
		{
			List<Food> list = new List<Food>();

			foreach (Food emp in coll)
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
				return FoodMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new FoodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Food(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Food();
		}

		#endregion

		[BrowsableAttribute(false)]
		public FoodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new FoodQuery();
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
		public bool Load(FoodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Food AddNew()
		{
			Food entity = base.AddNewEntity() as Food;

			return entity;
		}
		public Food FindByPrimaryKey(String foodID)
		{
			return base.FindByPrimaryKey(foodID) as Food;
		}

		#region IEnumerable< Food> Members

		IEnumerator<Food> IEnumerable<Food>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Food;
			}
		}

		#endregion

		private FoodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Food' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Food ({FoodID})")]
	[Serializable]
	public partial class Food : esFood
	{
		public Food()
		{
		}

		public Food(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return FoodMetadata.Meta();
			}
		}

		override protected esFoodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new FoodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public FoodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new FoodQuery();
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
		public bool Load(FoodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private FoodQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class FoodQuery : esFoodQuery
	{
		public FoodQuery()
		{

		}

		public FoodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "FoodQuery";
		}
	}

	[Serializable]
	public partial class FoodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected FoodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(FoodMetadata.ColumnNames.FoodID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.FoodID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.FoodName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.FoodName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.Weight, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = FoodMetadata.PropertyNames.Weight;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.QtyPortion, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = FoodMetadata.PropertyNames.QtyPortion;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.SRFoodGroup1, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.SRFoodGroup1;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.SRFoodGroup2, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.SRFoodGroup2;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.IsForSpecialCondition, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = FoodMetadata.PropertyNames.IsForSpecialCondition;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.IsSalesAvailable, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = FoodMetadata.PropertyNames.IsSalesAvailable;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.ItemID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.IsActive, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = FoodMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = FoodMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = FoodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(FoodMetadata.ColumnNames.IsPackage, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = FoodMetadata.PropertyNames.IsPackage;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public FoodMetadata Meta()
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
			public const string FoodID = "FoodID";
			public const string FoodName = "FoodName";
			public const string Notes = "Notes";
			public const string Weight = "Weight";
			public const string SRItemUnit = "SRItemUnit";
			public const string QtyPortion = "QtyPortion";
			public const string SRFoodGroup1 = "SRFoodGroup1";
			public const string SRFoodGroup2 = "SRFoodGroup2";
			public const string IsForSpecialCondition = "IsForSpecialCondition";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string ItemID = "ItemID";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPackage = "IsPackage";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string FoodID = "FoodID";
			public const string FoodName = "FoodName";
			public const string Notes = "Notes";
			public const string Weight = "Weight";
			public const string SRItemUnit = "SRItemUnit";
			public const string QtyPortion = "QtyPortion";
			public const string SRFoodGroup1 = "SRFoodGroup1";
			public const string SRFoodGroup2 = "SRFoodGroup2";
			public const string IsForSpecialCondition = "IsForSpecialCondition";
			public const string IsSalesAvailable = "IsSalesAvailable";
			public const string ItemID = "ItemID";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPackage = "IsPackage";
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
			lock (typeof(FoodMetadata))
			{
				if (FoodMetadata.mapDelegates == null)
				{
					FoodMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (FoodMetadata.meta == null)
				{
					FoodMetadata.meta = new FoodMetadata();
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

				meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoodName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyPortion", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRFoodGroup1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFoodGroup2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsForSpecialCondition", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSalesAvailable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "Food";
				meta.Destination = "Food";
				meta.spInsert = "proc_FoodInsert";
				meta.spUpdate = "proc_FoodUpdate";
				meta.spDelete = "proc_FoodDelete";
				meta.spLoadAll = "proc_FoodLoadAll";
				meta.spLoadByPrimaryKey = "proc_FoodLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private FoodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
