/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/31/2021 7:40:15 PM
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
	abstract public class esLaundryWashingProgramTypeItemConsumptionCollection : esEntityCollectionWAuditLog
	{
		public esLaundryWashingProgramTypeItemConsumptionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryWashingProgramTypeItemConsumptionCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryWashingProgramTypeItemConsumptionQuery query)
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
			this.InitQuery(query as esLaundryWashingProgramTypeItemConsumptionQuery);
		}
		#endregion

		virtual public LaundryWashingProgramTypeItemConsumption DetachEntity(LaundryWashingProgramTypeItemConsumption entity)
		{
			return base.DetachEntity(entity) as LaundryWashingProgramTypeItemConsumption;
		}

		virtual public LaundryWashingProgramTypeItemConsumption AttachEntity(LaundryWashingProgramTypeItemConsumption entity)
		{
			return base.AttachEntity(entity) as LaundryWashingProgramTypeItemConsumption;
		}

		virtual public void Combine(LaundryWashingProgramTypeItemConsumptionCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryWashingProgramTypeItemConsumption this[int index]
		{
			get
			{
				return base[index] as LaundryWashingProgramTypeItemConsumption;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryWashingProgramTypeItemConsumption);
		}
	}

	[Serializable]
	abstract public class esLaundryWashingProgramTypeItemConsumption : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryWashingProgramTypeItemConsumptionQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryWashingProgramTypeItemConsumption()
		{
		}

		public esLaundryWashingProgramTypeItemConsumption(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRLaundryProcessType, String sRLaundryProgram, String sRLaundryType, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRLaundryProcessType, sRLaundryProgram, sRLaundryType, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRLaundryProcessType, sRLaundryProgram, sRLaundryType, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRLaundryProcessType, String sRLaundryProgram, String sRLaundryType, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRLaundryProcessType, sRLaundryProgram, sRLaundryType, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRLaundryProcessType, sRLaundryProgram, sRLaundryType, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String sRLaundryProcessType, String sRLaundryProgram, String sRLaundryType, String itemID)
		{
			esLaundryWashingProgramTypeItemConsumptionQuery query = this.GetDynamicQuery();
			query.Where(query.SRLaundryProcessType == sRLaundryProcessType, query.SRLaundryProgram == sRLaundryProgram, query.SRLaundryType == sRLaundryType, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRLaundryProcessType, String sRLaundryProgram, String sRLaundryType, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRLaundryProcessType", sRLaundryProcessType);
			parms.Add("SRLaundryProgram", sRLaundryProgram);
			parms.Add("SRLaundryType", sRLaundryType);
			parms.Add("ItemID", itemID);
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
						case "SRLaundryProcessType": this.str.SRLaundryProcessType = (string)value; break;
						case "SRLaundryProgram": this.str.SRLaundryProgram = (string)value; break;
						case "SRLaundryType": this.str.SRLaundryType = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
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
		/// Maps to LaundryWashingProgramTypeItemConsumption.SRLaundryProcessType
		/// </summary>
		virtual public System.String SRLaundryProcessType
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProcessType);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProcessType, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramTypeItemConsumption.SRLaundryProgram
		/// </summary>
		virtual public System.String SRLaundryProgram
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProgram);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProgram, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramTypeItemConsumption.SRLaundryType
		/// </summary>
		virtual public System.String SRLaundryType
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryType);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryType, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramTypeItemConsumption.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramTypeItemConsumption.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramTypeItemConsumption.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramTypeItemConsumption.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramTypeItemConsumption.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryWashingProgramTypeItemConsumption entity)
			{
				this.entity = entity;
			}
			public System.String SRLaundryProcessType
			{
				get
				{
					System.String data = entity.SRLaundryProcessType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryProcessType = null;
					else entity.SRLaundryProcessType = Convert.ToString(value);
				}
			}
			public System.String SRLaundryProgram
			{
				get
				{
					System.String data = entity.SRLaundryProgram;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryProgram = null;
					else entity.SRLaundryProgram = Convert.ToString(value);
				}
			}
			public System.String SRLaundryType
			{
				get
				{
					System.String data = entity.SRLaundryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryType = null;
					else entity.SRLaundryType = Convert.ToString(value);
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
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
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
			private esLaundryWashingProgramTypeItemConsumption entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryWashingProgramTypeItemConsumptionQuery query)
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
				throw new Exception("esLaundryWashingProgramTypeItemConsumption can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryWashingProgramTypeItemConsumption : esLaundryWashingProgramTypeItemConsumption
	{
	}

	[Serializable]
	abstract public class esLaundryWashingProgramTypeItemConsumptionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryWashingProgramTypeItemConsumptionMetadata.Meta();
			}
		}

		public esQueryItem SRLaundryProcessType
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProcessType, esSystemType.String);
			}
		}

		public esQueryItem SRLaundryProgram
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProgram, esSystemType.String);
			}
		}

		public esQueryItem SRLaundryType
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryType, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryWashingProgramTypeItemConsumptionCollection")]
	public partial class LaundryWashingProgramTypeItemConsumptionCollection : esLaundryWashingProgramTypeItemConsumptionCollection, IEnumerable<LaundryWashingProgramTypeItemConsumption>
	{
		public LaundryWashingProgramTypeItemConsumptionCollection()
		{

		}

		public static implicit operator List<LaundryWashingProgramTypeItemConsumption>(LaundryWashingProgramTypeItemConsumptionCollection coll)
		{
			List<LaundryWashingProgramTypeItemConsumption> list = new List<LaundryWashingProgramTypeItemConsumption>();

			foreach (LaundryWashingProgramTypeItemConsumption emp in coll)
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
				return LaundryWashingProgramTypeItemConsumptionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryWashingProgramTypeItemConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryWashingProgramTypeItemConsumption(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryWashingProgramTypeItemConsumption();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryWashingProgramTypeItemConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryWashingProgramTypeItemConsumptionQuery();
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
		public bool Load(LaundryWashingProgramTypeItemConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryWashingProgramTypeItemConsumption AddNew()
		{
			LaundryWashingProgramTypeItemConsumption entity = base.AddNewEntity() as LaundryWashingProgramTypeItemConsumption;

			return entity;
		}
		public LaundryWashingProgramTypeItemConsumption FindByPrimaryKey(String sRLaundryProcessType, String sRLaundryProgram, String sRLaundryType, String itemID)
		{
			return base.FindByPrimaryKey(sRLaundryProcessType, sRLaundryProgram, sRLaundryType, itemID) as LaundryWashingProgramTypeItemConsumption;
		}

		#region IEnumerable< LaundryWashingProgramTypeItemConsumption> Members

		IEnumerator<LaundryWashingProgramTypeItemConsumption> IEnumerable<LaundryWashingProgramTypeItemConsumption>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryWashingProgramTypeItemConsumption;
			}
		}

		#endregion

		private LaundryWashingProgramTypeItemConsumptionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryWashingProgramTypeItemConsumption' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryWashingProgramTypeItemConsumption ({SRLaundryProcessType, SRLaundryProgram, SRLaundryType, ItemID})")]
	[Serializable]
	public partial class LaundryWashingProgramTypeItemConsumption : esLaundryWashingProgramTypeItemConsumption
	{
		public LaundryWashingProgramTypeItemConsumption()
		{
		}

		public LaundryWashingProgramTypeItemConsumption(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryWashingProgramTypeItemConsumptionMetadata.Meta();
			}
		}

		override protected esLaundryWashingProgramTypeItemConsumptionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryWashingProgramTypeItemConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryWashingProgramTypeItemConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryWashingProgramTypeItemConsumptionQuery();
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
		public bool Load(LaundryWashingProgramTypeItemConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryWashingProgramTypeItemConsumptionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryWashingProgramTypeItemConsumptionQuery : esLaundryWashingProgramTypeItemConsumptionQuery
	{
		public LaundryWashingProgramTypeItemConsumptionQuery()
		{

		}

		public LaundryWashingProgramTypeItemConsumptionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryWashingProgramTypeItemConsumptionQuery";
		}
	}

	[Serializable]
	public partial class LaundryWashingProgramTypeItemConsumptionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryWashingProgramTypeItemConsumptionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProcessType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.SRLaundryProcessType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryProgram, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.SRLaundryProgram;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRLaundryType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.SRLaundryType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.SRItemUnit, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeItemConsumptionMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeItemConsumptionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryWashingProgramTypeItemConsumptionMetadata Meta()
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
			public const string SRLaundryProcessType = "SRLaundryProcessType";
			public const string SRLaundryProgram = "SRLaundryProgram";
			public const string SRLaundryType = "SRLaundryType";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRLaundryProcessType = "SRLaundryProcessType";
			public const string SRLaundryProgram = "SRLaundryProgram";
			public const string SRLaundryType = "SRLaundryType";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
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
			lock (typeof(LaundryWashingProgramTypeItemConsumptionMetadata))
			{
				if (LaundryWashingProgramTypeItemConsumptionMetadata.mapDelegates == null)
				{
					LaundryWashingProgramTypeItemConsumptionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryWashingProgramTypeItemConsumptionMetadata.meta == null)
				{
					LaundryWashingProgramTypeItemConsumptionMetadata.meta = new LaundryWashingProgramTypeItemConsumptionMetadata();
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

				meta.AddTypeMap("SRLaundryProcessType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryProgram", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryWashingProgramTypeItemConsumption";
				meta.Destination = "LaundryWashingProgramTypeItemConsumption";
				meta.spInsert = "proc_LaundryWashingProgramTypeItemConsumptionInsert";
				meta.spUpdate = "proc_LaundryWashingProgramTypeItemConsumptionUpdate";
				meta.spDelete = "proc_LaundryWashingProgramTypeItemConsumptionDelete";
				meta.spLoadAll = "proc_LaundryWashingProgramTypeItemConsumptionLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryWashingProgramTypeItemConsumptionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryWashingProgramTypeItemConsumptionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
