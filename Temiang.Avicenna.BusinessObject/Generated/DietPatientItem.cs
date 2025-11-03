/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/23/2022 2:55:09 PM
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
	abstract public class esDietPatientItemCollection : esEntityCollectionWAuditLog
	{
		public esDietPatientItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "DietPatientItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esDietPatientItemQuery query)
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
			this.InitQuery(query as esDietPatientItemQuery);
		}
		#endregion

		virtual public DietPatientItem DetachEntity(DietPatientItem entity)
		{
			return base.DetachEntity(entity) as DietPatientItem;
		}

		virtual public DietPatientItem AttachEntity(DietPatientItem entity)
		{
			return base.AttachEntity(entity) as DietPatientItem;
		}

		virtual public void Combine(DietPatientItemCollection collection)
		{
			base.Combine(collection);
		}

		new public DietPatientItem this[int index]
		{
			get
			{
				return base[index] as DietPatientItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DietPatientItem);
		}
	}

	[Serializable]
	abstract public class esDietPatientItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDietPatientItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esDietPatientItem()
		{
		}

		public esDietPatientItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String dietID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, dietID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, dietID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String dietID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, dietID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, dietID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String dietID)
		{
			esDietPatientItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.DietID == dietID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String dietID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("DietID", dietID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "DietID": this.str.DietID = (string)value; break;
						case "Calorie": this.str.Calorie = (string)value; break;
						case "Protein": this.str.Protein = (string)value; break;
						case "Fat": this.str.Fat = (string)value; break;
						case "Carbohydrate": this.str.Carbohydrate = (string)value; break;
						case "Salt": this.str.Salt = (string)value; break;
						case "Fiber": this.str.Fiber = (string)value; break;
						case "ExtraQty": this.str.ExtraQty = (string)value; break;
						case "LiquidTime": this.str.LiquidTime = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Calorie":

							if (value == null || value is System.Decimal)
								this.Calorie = (System.Decimal?)value;
							break;
						case "Protein":

							if (value == null || value is System.Decimal)
								this.Protein = (System.Decimal?)value;
							break;
						case "Fat":

							if (value == null || value is System.Decimal)
								this.Fat = (System.Decimal?)value;
							break;
						case "Carbohydrate":

							if (value == null || value is System.Decimal)
								this.Carbohydrate = (System.Decimal?)value;
							break;
						case "Salt":

							if (value == null || value is System.Decimal)
								this.Salt = (System.Decimal?)value;
							break;
						case "Fiber":

							if (value == null || value is System.Decimal)
								this.Fiber = (System.Decimal?)value;
							break;
						case "ExtraQty":

							if (value == null || value is System.Int16)
								this.ExtraQty = (System.Int16?)value;
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
		/// Maps to DietPatientItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(DietPatientItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(DietPatientItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.DietID
		/// </summary>
		virtual public System.String DietID
		{
			get
			{
				return base.GetSystemString(DietPatientItemMetadata.ColumnNames.DietID);
			}

			set
			{
				base.SetSystemString(DietPatientItemMetadata.ColumnNames.DietID, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.Calorie
		/// </summary>
		virtual public System.Decimal? Calorie
		{
			get
			{
				return base.GetSystemDecimal(DietPatientItemMetadata.ColumnNames.Calorie);
			}

			set
			{
				base.SetSystemDecimal(DietPatientItemMetadata.ColumnNames.Calorie, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.Protein
		/// </summary>
		virtual public System.Decimal? Protein
		{
			get
			{
				return base.GetSystemDecimal(DietPatientItemMetadata.ColumnNames.Protein);
			}

			set
			{
				base.SetSystemDecimal(DietPatientItemMetadata.ColumnNames.Protein, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.Fat
		/// </summary>
		virtual public System.Decimal? Fat
		{
			get
			{
				return base.GetSystemDecimal(DietPatientItemMetadata.ColumnNames.Fat);
			}

			set
			{
				base.SetSystemDecimal(DietPatientItemMetadata.ColumnNames.Fat, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.Carbohydrate
		/// </summary>
		virtual public System.Decimal? Carbohydrate
		{
			get
			{
				return base.GetSystemDecimal(DietPatientItemMetadata.ColumnNames.Carbohydrate);
			}

			set
			{
				base.SetSystemDecimal(DietPatientItemMetadata.ColumnNames.Carbohydrate, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.Salt
		/// </summary>
		virtual public System.Decimal? Salt
		{
			get
			{
				return base.GetSystemDecimal(DietPatientItemMetadata.ColumnNames.Salt);
			}

			set
			{
				base.SetSystemDecimal(DietPatientItemMetadata.ColumnNames.Salt, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.Fiber
		/// </summary>
		virtual public System.Decimal? Fiber
		{
			get
			{
				return base.GetSystemDecimal(DietPatientItemMetadata.ColumnNames.Fiber);
			}

			set
			{
				base.SetSystemDecimal(DietPatientItemMetadata.ColumnNames.Fiber, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.ExtraQty
		/// </summary>
		virtual public System.Int16? ExtraQty
		{
			get
			{
				return base.GetSystemInt16(DietPatientItemMetadata.ColumnNames.ExtraQty);
			}

			set
			{
				base.SetSystemInt16(DietPatientItemMetadata.ColumnNames.ExtraQty, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.LiquidTime
		/// </summary>
		virtual public System.String LiquidTime
		{
			get
			{
				return base.GetSystemString(DietPatientItemMetadata.ColumnNames.LiquidTime);
			}

			set
			{
				base.SetSystemString(DietPatientItemMetadata.ColumnNames.LiquidTime, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(DietPatientItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(DietPatientItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DietPatientItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(DietPatientItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to DietPatientItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DietPatientItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(DietPatientItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDietPatientItem entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String DietID
			{
				get
				{
					System.String data = entity.DietID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietID = null;
					else entity.DietID = Convert.ToString(value);
				}
			}
			public System.String Calorie
			{
				get
				{
					System.Decimal? data = entity.Calorie;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Calorie = null;
					else entity.Calorie = Convert.ToDecimal(value);
				}
			}
			public System.String Protein
			{
				get
				{
					System.Decimal? data = entity.Protein;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Protein = null;
					else entity.Protein = Convert.ToDecimal(value);
				}
			}
			public System.String Fat
			{
				get
				{
					System.Decimal? data = entity.Fat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Fat = null;
					else entity.Fat = Convert.ToDecimal(value);
				}
			}
			public System.String Carbohydrate
			{
				get
				{
					System.Decimal? data = entity.Carbohydrate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Carbohydrate = null;
					else entity.Carbohydrate = Convert.ToDecimal(value);
				}
			}
			public System.String Salt
			{
				get
				{
					System.Decimal? data = entity.Salt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Salt = null;
					else entity.Salt = Convert.ToDecimal(value);
				}
			}
			public System.String Fiber
			{
				get
				{
					System.Decimal? data = entity.Fiber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Fiber = null;
					else entity.Fiber = Convert.ToDecimal(value);
				}
			}
			public System.String ExtraQty
			{
				get
				{
					System.Int16? data = entity.ExtraQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExtraQty = null;
					else entity.ExtraQty = Convert.ToInt16(value);
				}
			}
			public System.String LiquidTime
			{
				get
				{
					System.String data = entity.LiquidTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LiquidTime = null;
					else entity.LiquidTime = Convert.ToString(value);
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
			private esDietPatientItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDietPatientItemQuery query)
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
				throw new Exception("esDietPatientItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class DietPatientItem : esDietPatientItem
	{
	}

	[Serializable]
	abstract public class esDietPatientItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return DietPatientItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem DietID
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.DietID, esSystemType.String);
			}
		}

		public esQueryItem Calorie
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.Calorie, esSystemType.Decimal);
			}
		}

		public esQueryItem Protein
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.Protein, esSystemType.Decimal);
			}
		}

		public esQueryItem Fat
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.Fat, esSystemType.Decimal);
			}
		}

		public esQueryItem Carbohydrate
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.Carbohydrate, esSystemType.Decimal);
			}
		}

		public esQueryItem Salt
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.Salt, esSystemType.Decimal);
			}
		}

		public esQueryItem Fiber
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.Fiber, esSystemType.Decimal);
			}
		}

		public esQueryItem ExtraQty
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.ExtraQty, esSystemType.Int16);
			}
		}

		public esQueryItem LiquidTime
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.LiquidTime, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DietPatientItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DietPatientItemCollection")]
	public partial class DietPatientItemCollection : esDietPatientItemCollection, IEnumerable<DietPatientItem>
	{
		public DietPatientItemCollection()
		{

		}

		public static implicit operator List<DietPatientItem>(DietPatientItemCollection coll)
		{
			List<DietPatientItem> list = new List<DietPatientItem>();

			foreach (DietPatientItem emp in coll)
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
				return DietPatientItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DietPatientItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DietPatientItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DietPatientItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public DietPatientItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DietPatientItemQuery();
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
		public bool Load(DietPatientItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public DietPatientItem AddNew()
		{
			DietPatientItem entity = base.AddNewEntity() as DietPatientItem;

			return entity;
		}
		public DietPatientItem FindByPrimaryKey(String transactionNo, String dietID)
		{
			return base.FindByPrimaryKey(transactionNo, dietID) as DietPatientItem;
		}

		#region IEnumerable< DietPatientItem> Members

		IEnumerator<DietPatientItem> IEnumerable<DietPatientItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as DietPatientItem;
			}
		}

		#endregion

		private DietPatientItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DietPatientItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("DietPatientItem ({TransactionNo, DietID})")]
	[Serializable]
	public partial class DietPatientItem : esDietPatientItem
	{
		public DietPatientItem()
		{
		}

		public DietPatientItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DietPatientItemMetadata.Meta();
			}
		}

		override protected esDietPatientItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DietPatientItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public DietPatientItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DietPatientItemQuery();
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
		public bool Load(DietPatientItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private DietPatientItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class DietPatientItemQuery : esDietPatientItemQuery
	{
		public DietPatientItemQuery()
		{

		}

		public DietPatientItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "DietPatientItemQuery";
		}
	}

	[Serializable]
	public partial class DietPatientItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DietPatientItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.DietID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.DietID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.Calorie, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.Calorie;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.Protein, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.Protein;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.Fat, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.Fat;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.Carbohydrate, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.Carbohydrate;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.Salt, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.Salt;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.Fiber, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.Fiber;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.ExtraQty, 8, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.ExtraQty;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.LiquidTime, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.LiquidTime;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.Notes, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DietPatientItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = DietPatientItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public DietPatientItemMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string DietID = "DietID";
			public const string Calorie = "Calorie";
			public const string Protein = "Protein";
			public const string Fat = "Fat";
			public const string Carbohydrate = "Carbohydrate";
			public const string Salt = "Salt";
			public const string Fiber = "Fiber";
			public const string ExtraQty = "ExtraQty";
			public const string LiquidTime = "LiquidTime";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string DietID = "DietID";
			public const string Calorie = "Calorie";
			public const string Protein = "Protein";
			public const string Fat = "Fat";
			public const string Carbohydrate = "Carbohydrate";
			public const string Salt = "Salt";
			public const string Fiber = "Fiber";
			public const string ExtraQty = "ExtraQty";
			public const string LiquidTime = "LiquidTime";
			public const string Notes = "Notes";
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
			lock (typeof(DietPatientItemMetadata))
			{
				if (DietPatientItemMetadata.mapDelegates == null)
				{
					DietPatientItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (DietPatientItemMetadata.meta == null)
				{
					DietPatientItemMetadata.meta = new DietPatientItemMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Calorie", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Protein", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Fat", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Carbohydrate", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Salt", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Fiber", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ExtraQty", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LiquidTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "DietPatientItem";
				meta.Destination = "DietPatientItem";
				meta.spInsert = "proc_DietPatientItemInsert";
				meta.spUpdate = "proc_DietPatientItemUpdate";
				meta.spDelete = "proc_DietPatientItemDelete";
				meta.spLoadAll = "proc_DietPatientItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_DietPatientItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DietPatientItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
