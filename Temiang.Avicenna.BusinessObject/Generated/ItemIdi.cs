/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/1/2020 9:10:00 PM
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
	abstract public class esItemIdiCollection : esEntityCollectionWAuditLog
	{
		public esItemIdiCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemIdiCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemIdiQuery query)
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
			this.InitQuery(query as esItemIdiQuery);
		}
		#endregion

		virtual public ItemIdi DetachEntity(ItemIdi entity)
		{
			return base.DetachEntity(entity) as ItemIdi;
		}

		virtual public ItemIdi AttachEntity(ItemIdi entity)
		{
			return base.AttachEntity(entity) as ItemIdi;
		}

		virtual public void Combine(ItemIdiCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemIdi this[int index]
		{
			get
			{
				return base[index] as ItemIdi;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemIdi);
		}
	}

	[Serializable]
	abstract public class esItemIdi : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemIdiQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemIdi()
		{
		}

		public esItemIdi(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String idiCode)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(idiCode);
			else
				return LoadByPrimaryKeyStoredProcedure(idiCode);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String idiCode)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(idiCode);
			else
				return LoadByPrimaryKeyStoredProcedure(idiCode);
		}

		private bool LoadByPrimaryKeyDynamic(String idiCode)
		{
			esItemIdiQuery query = this.GetDynamicQuery();
			query.Where(query.IdiCode == idiCode);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String idiCode)
		{
			esParameters parms = new esParameters();
			parms.Add("IdiCode", idiCode);
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
						case "IdiCode": this.str.IdiCode = (string)value; break;
						case "IdiName": this.str.IdiName = (string)value; break;
						case "Icd9Cm": this.str.Icd9Cm = (string)value; break;
						case "F1": this.str.F1 = (string)value; break;
						case "F21": this.str.F21 = (string)value; break;
						case "F22": this.str.F22 = (string)value; break;
						case "F23": this.str.F23 = (string)value; break;
						case "F3": this.str.F3 = (string)value; break;
						case "F4": this.str.F4 = (string)value; break;
						case "Rvu": this.str.Rvu = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "Specialist": this.str.Specialist = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "F_1":

							if (value == null || value is System.Decimal)
								this.F1 = (System.Decimal?)value;
							break;
						case "F_2_1":

							if (value == null || value is System.Decimal)
								this.F21 = (System.Decimal?)value;
							break;
						case "F_2_2":

							if (value == null || value is System.Decimal)
								this.F22 = (System.Decimal?)value;
							break;
						case "F_2_3":

							if (value == null || value is System.Decimal)
								this.F23 = (System.Decimal?)value;
							break;
						case "F_3":

							if (value == null || value is System.Decimal)
								this.F3 = (System.Decimal?)value;
							break;
						case "F_4":

							if (value == null || value is System.Decimal)
								this.F4 = (System.Decimal?)value;
							break;
						case "Rvu":

							if (value == null || value is System.Decimal)
								this.Rvu = (System.Decimal?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
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
		/// Maps to ItemIdi.IdiCode
		/// </summary>
		virtual public System.String IdiCode
		{
			get
			{
				return base.GetSystemString(ItemIdiMetadata.ColumnNames.IdiCode);
			}

			set
			{
				base.SetSystemString(ItemIdiMetadata.ColumnNames.IdiCode, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.IdiName
		/// </summary>
		virtual public System.String IdiName
		{
			get
			{
				return base.GetSystemString(ItemIdiMetadata.ColumnNames.IdiName);
			}

			set
			{
				base.SetSystemString(ItemIdiMetadata.ColumnNames.IdiName, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.Icd9Cm
		/// </summary>
		virtual public System.String Icd9Cm
		{
			get
			{
				return base.GetSystemString(ItemIdiMetadata.ColumnNames.Icd9Cm);
			}

			set
			{
				base.SetSystemString(ItemIdiMetadata.ColumnNames.Icd9Cm, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.F_1
		/// </summary>
		virtual public System.Decimal? F1
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.F1);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.F1, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.F_2_1
		/// </summary>
		virtual public System.Decimal? F21
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.F21);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.F21, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.F_2_2
		/// </summary>
		virtual public System.Decimal? F22
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.F22);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.F22, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.F_2_3
		/// </summary>
		virtual public System.Decimal? F23
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.F23);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.F23, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.F_3
		/// </summary>
		virtual public System.Decimal? F3
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.F3);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.F3, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.F_4
		/// </summary>
		virtual public System.Decimal? F4
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.F4);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.F4, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.Rvu
		/// </summary>
		virtual public System.Decimal? Rvu
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.Rvu);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.Rvu, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemIdiMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(ItemIdiMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.Specialist
		/// </summary>
		virtual public System.String Specialist
		{
			get
			{
				return base.GetSystemString(ItemIdiMetadata.ColumnNames.Specialist);
			}

			set
			{
				base.SetSystemString(ItemIdiMetadata.ColumnNames.Specialist, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemIdiMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemIdiMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemIdi.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemIdiMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemIdiMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemIdi entity)
			{
				this.entity = entity;
			}
			public System.String IdiCode
			{
				get
				{
					System.String data = entity.IdiCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdiCode = null;
					else entity.IdiCode = Convert.ToString(value);
				}
			}
			public System.String IdiName
			{
				get
				{
					System.String data = entity.IdiName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdiName = null;
					else entity.IdiName = Convert.ToString(value);
				}
			}
			public System.String Icd9Cm
			{
				get
				{
					System.String data = entity.Icd9Cm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Icd9Cm = null;
					else entity.Icd9Cm = Convert.ToString(value);
				}
			}
			public System.String F1
			{
				get
				{
					System.Decimal? data = entity.F1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F1 = null;
					else entity.F1 = Convert.ToDecimal(value);
				}
			}
			public System.String F21
			{
				get
				{
					System.Decimal? data = entity.F21;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F21 = null;
					else entity.F21 = Convert.ToDecimal(value);
				}
			}
			public System.String F22
			{
				get
				{
					System.Decimal? data = entity.F22;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F22 = null;
					else entity.F22 = Convert.ToDecimal(value);
				}
			}
			public System.String F23
			{
				get
				{
					System.Decimal? data = entity.F23;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F23 = null;
					else entity.F23 = Convert.ToDecimal(value);
				}
			}
			public System.String F3
			{
				get
				{
					System.Decimal? data = entity.F3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F3 = null;
					else entity.F3 = Convert.ToDecimal(value);
				}
			}
			public System.String F4
			{
				get
				{
					System.Decimal? data = entity.F4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.F4 = null;
					else entity.F4 = Convert.ToDecimal(value);
				}
			}
			public System.String Rvu
			{
				get
				{
					System.Decimal? data = entity.Rvu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rvu = null;
					else entity.Rvu = Convert.ToDecimal(value);
				}
			}
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
			public System.String Specialist
			{
				get
				{
					System.String data = entity.Specialist;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Specialist = null;
					else entity.Specialist = Convert.ToString(value);
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
			private esItemIdi entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemIdiQuery query)
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
				throw new Exception("esItemIdi can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemIdi : esItemIdi
	{
	}

	[Serializable]
	abstract public class esItemIdiQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemIdiMetadata.Meta();
			}
		}

		public esQueryItem IdiCode
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.IdiCode, esSystemType.String);
			}
		}

		public esQueryItem IdiName
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.IdiName, esSystemType.String);
			}
		}

		public esQueryItem Icd9Cm
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.Icd9Cm, esSystemType.String);
			}
		}

		public esQueryItem F1
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.F1, esSystemType.Decimal);
			}
		}

		public esQueryItem F21
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.F21, esSystemType.Decimal);
			}
		}

		public esQueryItem F22
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.F22, esSystemType.Decimal);
			}
		}

		public esQueryItem F23
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.F23, esSystemType.Decimal);
			}
		}

		public esQueryItem F3
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.F3, esSystemType.Decimal);
			}
		}

		public esQueryItem F4
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.F4, esSystemType.Decimal);
			}
		}

		public esQueryItem Rvu
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.Rvu, esSystemType.Decimal);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem Specialist
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.Specialist, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemIdiMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemIdiCollection")]
	public partial class ItemIdiCollection : esItemIdiCollection, IEnumerable<ItemIdi>
	{
		public ItemIdiCollection()
		{

		}

		public static implicit operator List<ItemIdi>(ItemIdiCollection coll)
		{
			List<ItemIdi> list = new List<ItemIdi>();

			foreach (ItemIdi emp in coll)
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
				return ItemIdiMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemIdiQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemIdi(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemIdi();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemIdiQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemIdiQuery();
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
		public bool Load(ItemIdiQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemIdi AddNew()
		{
			ItemIdi entity = base.AddNewEntity() as ItemIdi;

			return entity;
		}
		public ItemIdi FindByPrimaryKey(String idiCode)
		{
			return base.FindByPrimaryKey(idiCode) as ItemIdi;
		}

		#region IEnumerable< ItemIdi> Members

		IEnumerator<ItemIdi> IEnumerable<ItemIdi>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemIdi;
			}
		}

		#endregion

		private ItemIdiQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemIdi' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemIdi ({IdiCode})")]
	[Serializable]
	public partial class ItemIdi : esItemIdi
	{
		public ItemIdi()
		{
		}

		public ItemIdi(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemIdiMetadata.Meta();
			}
		}

		override protected esItemIdiQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemIdiQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemIdiQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemIdiQuery();
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
		public bool Load(ItemIdiQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemIdiQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemIdiQuery : esItemIdiQuery
	{
		public ItemIdiQuery()
		{

		}

		public ItemIdiQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemIdiQuery";
		}
	}

	[Serializable]
	public partial class ItemIdiMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemIdiMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.IdiCode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiMetadata.PropertyNames.IdiCode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.IdiName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiMetadata.PropertyNames.IdiName;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.Icd9Cm, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiMetadata.PropertyNames.Icd9Cm;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.F1, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.F1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.F21, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.F21;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.F22, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.F22;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.F23, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.F23;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.F3, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.F3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.F4, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.F4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.Rvu, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.Rvu;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.Price, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemIdiMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.Specialist, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiMetadata.PropertyNames.Specialist;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemIdiMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemIdiMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemIdiMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemIdiMetadata Meta()
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
			public const string IdiCode = "IdiCode";
			public const string IdiName = "IdiName";
			public const string Icd9Cm = "Icd9Cm";
			public const string F1 = "F_1";
			public const string F21 = "F_2_1";
			public const string F22 = "F_2_2";
			public const string F23 = "F_2_3";
			public const string F3 = "F_3";
			public const string F4 = "F_4";
			public const string Rvu = "Rvu";
			public const string Price = "Price";
			public const string Specialist = "Specialist";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string IdiCode = "IdiCode";
			public const string IdiName = "IdiName";
			public const string Icd9Cm = "Icd9Cm";
			public const string F1 = "F1";
			public const string F21 = "F21";
			public const string F22 = "F22";
			public const string F23 = "F23";
			public const string F3 = "F3";
			public const string F4 = "F4";
			public const string Rvu = "Rvu";
			public const string Price = "Price";
			public const string Specialist = "Specialist";
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
			lock (typeof(ItemIdiMetadata))
			{
				if (ItemIdiMetadata.mapDelegates == null)
				{
					ItemIdiMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemIdiMetadata.meta == null)
				{
					ItemIdiMetadata.meta = new ItemIdiMetadata();
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

				meta.AddTypeMap("IdiCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IdiName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Icd9Cm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("F1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("F21", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("F22", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("F23", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("F3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("F4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Rvu", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Specialist", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemIdi";
				meta.Destination = "ItemIdi";
				meta.spInsert = "proc_ItemIdiInsert";
				meta.spUpdate = "proc_ItemIdiUpdate";
				meta.spDelete = "proc_ItemIdiDelete";
				meta.spLoadAll = "proc_ItemIdiLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemIdiLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemIdiMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
