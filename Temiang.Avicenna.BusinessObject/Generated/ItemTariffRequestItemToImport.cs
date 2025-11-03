/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/19/2021 11:48:33 AM
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
	abstract public class esItemTariffRequestItemToImportCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffRequestItemToImportCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTariffRequestItemToImportCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffRequestItemToImportQuery query)
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
			this.InitQuery(query as esItemTariffRequestItemToImportQuery);
		}
		#endregion

		virtual public ItemTariffRequestItemToImport DetachEntity(ItemTariffRequestItemToImport entity)
		{
			return base.DetachEntity(entity) as ItemTariffRequestItemToImport;
		}

		virtual public ItemTariffRequestItemToImport AttachEntity(ItemTariffRequestItemToImport entity)
		{
			return base.AttachEntity(entity) as ItemTariffRequestItemToImport;
		}

		virtual public void Combine(ItemTariffRequestItemToImportCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemTariffRequestItemToImport this[int index]
		{
			get
			{
				return base[index] as ItemTariffRequestItemToImport;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffRequestItemToImport);
		}
	}

	[Serializable]
	abstract public class esItemTariffRequestItemToImport : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffRequestItemToImportQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffRequestItemToImport()
		{
		}

		public esItemTariffRequestItemToImport(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String referenceNo, DateTime startingDate, String sRTariffType, String itemID, String classID, String tariffComponentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceNo, startingDate, sRTariffType, itemID, classID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceNo, startingDate, sRTariffType, itemID, classID, tariffComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String referenceNo, DateTime startingDate, String sRTariffType, String itemID, String classID, String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceNo, startingDate, sRTariffType, itemID, classID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceNo, startingDate, sRTariffType, itemID, classID, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(String referenceNo, DateTime startingDate, String sRTariffType, String itemID, String classID, String tariffComponentID)
		{
			esItemTariffRequestItemToImportQuery query = this.GetDynamicQuery();
			query.Where(query.ReferenceNo == referenceNo, query.StartingDate == startingDate, query.SRTariffType == sRTariffType, query.ItemID == itemID, query.ClassID == classID, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String referenceNo, DateTime startingDate, String sRTariffType, String itemID, String classID, String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("ReferenceNo", referenceNo);
			parms.Add("StartingDate", startingDate);
			parms.Add("SRTariffType", sRTariffType);
			parms.Add("ItemID", itemID);
			parms.Add("ClassID", classID);
			parms.Add("TariffComponentID", tariffComponentID);
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
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "StartingDate": this.str.StartingDate = (string)value; break;
						case "SRTariffType": this.str.SRTariffType = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "OldPrice": this.str.OldPrice = (string)value; break;
						case "NewPrice": this.str.NewPrice = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "GeneralPrice": this.str.GeneralPrice = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "ItemName": this.str.ItemName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "StartingDate":

							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						case "OldPrice":

							if (value == null || value is System.Decimal)
								this.OldPrice = (System.Decimal?)value;
							break;
						case "NewPrice":

							if (value == null || value is System.Decimal)
								this.NewPrice = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "GeneralPrice":

							if (value == null || value is System.Decimal)
								this.GeneralPrice = (System.Decimal?)value;
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
		/// Maps to ItemTariffRequestItemToImport.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestItemToImportMetadata.ColumnNames.StartingDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestItemToImportMetadata.ColumnNames.StartingDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.SRTariffType);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.TariffComponentID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.OldPrice
		/// </summary>
		virtual public System.Decimal? OldPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffRequestItemToImportMetadata.ColumnNames.OldPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffRequestItemToImportMetadata.ColumnNames.OldPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.NewPrice
		/// </summary>
		virtual public System.Decimal? NewPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffRequestItemToImportMetadata.ColumnNames.NewPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffRequestItemToImportMetadata.ColumnNames.NewPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.GeneralPrice
		/// </summary>
		virtual public System.Decimal? GeneralPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffRequestItemToImportMetadata.ColumnNames.GeneralPrice);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffRequestItemToImportMetadata.ColumnNames.GeneralPrice, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemGroupID);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffRequestItemToImport.ItemName
		/// </summary>
		virtual public System.String ItemName
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemName);
			}

			set
			{
				base.SetSystemString(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemName, value);
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
			public esStrings(esItemTariffRequestItemToImport entity)
			{
				this.entity = entity;
			}
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
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
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
			public System.String OldPrice
			{
				get
				{
					System.Decimal? data = entity.OldPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldPrice = null;
					else entity.OldPrice = Convert.ToDecimal(value);
				}
			}
			public System.String NewPrice
			{
				get
				{
					System.Decimal? data = entity.NewPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewPrice = null;
					else entity.NewPrice = Convert.ToDecimal(value);
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
			public System.String GeneralPrice
			{
				get
				{
					System.Decimal? data = entity.GeneralPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GeneralPrice = null;
					else entity.GeneralPrice = Convert.ToDecimal(value);
				}
			}
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String ItemName
			{
				get
				{
					System.String data = entity.ItemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemName = null;
					else entity.ItemName = Convert.ToString(value);
				}
			}
			private esItemTariffRequestItemToImport entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffRequestItemToImportQuery query)
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
				throw new Exception("esItemTariffRequestItemToImport can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTariffRequestItemToImport : esItemTariffRequestItemToImport
	{
	}

	[Serializable]
	abstract public class esItemTariffRequestItemToImportQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestItemToImportMetadata.Meta();
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		}

		public esQueryItem OldPrice
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.OldPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem NewPrice
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.NewPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem GeneralPrice
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.GeneralPrice, esSystemType.Decimal);
			}
		}

		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		}

		public esQueryItem ItemName
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemToImportMetadata.ColumnNames.ItemName, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffRequestItemToImportCollection")]
	public partial class ItemTariffRequestItemToImportCollection : esItemTariffRequestItemToImportCollection, IEnumerable<ItemTariffRequestItemToImport>
	{
		public ItemTariffRequestItemToImportCollection()
		{

		}

		public static implicit operator List<ItemTariffRequestItemToImport>(ItemTariffRequestItemToImportCollection coll)
		{
			List<ItemTariffRequestItemToImport> list = new List<ItemTariffRequestItemToImport>();

			foreach (ItemTariffRequestItemToImport emp in coll)
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
				return ItemTariffRequestItemToImportMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestItemToImportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffRequestItemToImport(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffRequestItemToImport();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffRequestItemToImportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestItemToImportQuery();
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
		public bool Load(ItemTariffRequestItemToImportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTariffRequestItemToImport AddNew()
		{
			ItemTariffRequestItemToImport entity = base.AddNewEntity() as ItemTariffRequestItemToImport;

			return entity;
		}
		public ItemTariffRequestItemToImport FindByPrimaryKey(String referenceNo, DateTime startingDate, String sRTariffType, String itemID, String classID, String tariffComponentID)
		{
			return base.FindByPrimaryKey(referenceNo, startingDate, sRTariffType, itemID, classID, tariffComponentID) as ItemTariffRequestItemToImport;
		}

		#region IEnumerable< ItemTariffRequestItemToImport> Members

		IEnumerator<ItemTariffRequestItemToImport> IEnumerable<ItemTariffRequestItemToImport>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffRequestItemToImport;
			}
		}

		#endregion

		private ItemTariffRequestItemToImportQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffRequestItemToImport' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTariffRequestItemToImport ({ReferenceNo, StartingDate, SRTariffType, ItemID, ClassID, TariffComponentID})")]
	[Serializable]
	public partial class ItemTariffRequestItemToImport : esItemTariffRequestItemToImport
	{
		public ItemTariffRequestItemToImport()
		{
		}

		public ItemTariffRequestItemToImport(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestItemToImportMetadata.Meta();
			}
		}

		override protected esItemTariffRequestItemToImportQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestItemToImportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffRequestItemToImportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestItemToImportQuery();
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
		public bool Load(ItemTariffRequestItemToImportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTariffRequestItemToImportQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTariffRequestItemToImportQuery : esItemTariffRequestItemToImportQuery
	{
		public ItemTariffRequestItemToImportQuery()
		{

		}

		public ItemTariffRequestItemToImportQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTariffRequestItemToImportQuery";
		}
	}

	[Serializable]
	public partial class ItemTariffRequestItemToImportMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffRequestItemToImportMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.ReferenceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.ReferenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.StartingDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.StartingDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.SRTariffType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.SRTariffType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.ClassID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.TariffComponentID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.OldPrice, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.OldPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.NewPrice, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.NewPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.GeneralPrice, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.GeneralPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemGroupID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffRequestItemToImportMetadata.ColumnNames.ItemName, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemToImportMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTariffRequestItemToImportMetadata Meta()
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
			public const string ReferenceNo = "ReferenceNo";
			public const string StartingDate = "StartingDate";
			public const string SRTariffType = "SRTariffType";
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string TariffComponentID = "TariffComponentID";
			public const string OldPrice = "OldPrice";
			public const string NewPrice = "NewPrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string GeneralPrice = "GeneralPrice";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemName = "ItemName";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ReferenceNo = "ReferenceNo";
			public const string StartingDate = "StartingDate";
			public const string SRTariffType = "SRTariffType";
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string TariffComponentID = "TariffComponentID";
			public const string OldPrice = "OldPrice";
			public const string NewPrice = "NewPrice";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string GeneralPrice = "GeneralPrice";
			public const string ItemGroupID = "ItemGroupID";
			public const string ItemName = "ItemName";
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
			lock (typeof(ItemTariffRequestItemToImportMetadata))
			{
				if (ItemTariffRequestItemToImportMetadata.mapDelegates == null)
				{
					ItemTariffRequestItemToImportMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTariffRequestItemToImportMetadata.meta == null)
				{
					ItemTariffRequestItemToImportMetadata.meta = new ItemTariffRequestItemToImportMetadata();
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

				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OldPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("NewPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GeneralPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemTariffRequestItemToImport";
				meta.Destination = "ItemTariffRequestItemToImport";
				meta.spInsert = "proc_ItemTariffRequestItemToImportInsert";
				meta.spUpdate = "proc_ItemTariffRequestItemToImportUpdate";
				meta.spDelete = "proc_ItemTariffRequestItemToImportDelete";
				meta.spLoadAll = "proc_ItemTariffRequestItemToImportLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffRequestItemToImportLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffRequestItemToImportMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
