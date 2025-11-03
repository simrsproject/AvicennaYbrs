/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/13/2020 1:46:23 PM
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
	abstract public class esZipCodeCollection : esEntityCollectionWAuditLog
	{
		public esZipCodeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ZipCodeCollection";
		}

		#region Query Logic
		protected void InitQuery(esZipCodeQuery query)
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
			this.InitQuery(query as esZipCodeQuery);
		}
		#endregion

		virtual public ZipCode DetachEntity(ZipCode entity)
		{
			return base.DetachEntity(entity) as ZipCode;
		}

		virtual public ZipCode AttachEntity(ZipCode entity)
		{
			return base.AttachEntity(entity) as ZipCode;
		}

		virtual public void Combine(ZipCodeCollection collection)
		{
			base.Combine(collection);
		}

		new public ZipCode this[int index]
		{
			get
			{
				return base[index] as ZipCode;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ZipCode);
		}
	}

	[Serializable]
	abstract public class esZipCode : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esZipCodeQuery GetDynamicQuery()
		{
			return null;
		}

		public esZipCode()
		{
		}

		public esZipCode(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String zipCode)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(zipCode);
			else
				return LoadByPrimaryKeyStoredProcedure(zipCode);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String zipCode)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(zipCode);
			else
				return LoadByPrimaryKeyStoredProcedure(zipCode);
		}

		private bool LoadByPrimaryKeyDynamic(String zipCode)
		{
			esZipCodeQuery query = this.GetDynamicQuery();
			query.Where(query.ZipCode == zipCode);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String zipCode)
		{
			esParameters parms = new esParameters();
			parms.Add("ZipCode", zipCode);
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
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "StreetName": this.str.StreetName = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "SRProvince": this.str.SRProvince = (string)value; break;
						case "Latitude": this.str.Latitude = (string)value; break;
						case "Longitude": this.str.Longitude = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ZipPostalCode": this.str.ZipPostalCode = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Latitude":

							if (value == null || value is System.Decimal)
								this.Latitude = (System.Decimal?)value;
							break;
						case "Longitude":

							if (value == null || value is System.Decimal)
								this.Longitude = (System.Decimal?)value;
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
		/// Maps to ZipCode.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.StreetName);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.StreetName, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.SRProvince
		/// </summary>
		virtual public System.String SRProvince
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.SRProvince);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.SRProvince, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.Latitude
		/// </summary>
		virtual public System.Decimal? Latitude
		{
			get
			{
				return base.GetSystemDecimal(ZipCodeMetadata.ColumnNames.Latitude);
			}

			set
			{
				base.SetSystemDecimal(ZipCodeMetadata.ColumnNames.Latitude, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.Longitude
		/// </summary>
		virtual public System.Decimal? Longitude
		{
			get
			{
				return base.GetSystemDecimal(ZipCodeMetadata.ColumnNames.Longitude);
			}

			set
			{
				base.SetSystemDecimal(ZipCodeMetadata.ColumnNames.Longitude, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ZipCodeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ZipCodeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ZipCode.ZipPostalCode
		/// </summary>
		virtual public System.String ZipPostalCode
		{
			get
			{
				return base.GetSystemString(ZipCodeMetadata.ColumnNames.ZipPostalCode);
			}

			set
			{
				base.SetSystemString(ZipCodeMetadata.ColumnNames.ZipPostalCode, value);
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
			public esStrings(esZipCode entity)
			{
				this.entity = entity;
			}
			public System.String ZipCode
			{
				get
				{
					System.String data = entity.ZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipCode = null;
					else entity.ZipCode = Convert.ToString(value);
				}
			}
			public System.String StreetName
			{
				get
				{
					System.String data = entity.StreetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StreetName = null;
					else entity.StreetName = Convert.ToString(value);
				}
			}
			public System.String District
			{
				get
				{
					System.String data = entity.District;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.District = null;
					else entity.District = Convert.ToString(value);
				}
			}
			public System.String County
			{
				get
				{
					System.String data = entity.County;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.County = null;
					else entity.County = Convert.ToString(value);
				}
			}
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
			public System.String SRProvince
			{
				get
				{
					System.String data = entity.SRProvince;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProvince = null;
					else entity.SRProvince = Convert.ToString(value);
				}
			}
			public System.String Latitude
			{
				get
				{
					System.Decimal? data = entity.Latitude;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Latitude = null;
					else entity.Latitude = Convert.ToDecimal(value);
				}
			}
			public System.String Longitude
			{
				get
				{
					System.Decimal? data = entity.Longitude;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Longitude = null;
					else entity.Longitude = Convert.ToDecimal(value);
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
			public System.String ZipPostalCode
			{
				get
				{
					System.String data = entity.ZipPostalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipPostalCode = null;
					else entity.ZipPostalCode = Convert.ToString(value);
				}
			}
			private esZipCode entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esZipCodeQuery query)
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
				throw new Exception("esZipCode can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ZipCode : esZipCode
	{
	}

	[Serializable]
	abstract public class esZipCodeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ZipCodeMetadata.Meta();
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem SRProvince
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.SRProvince, esSystemType.String);
			}
		}

		public esQueryItem Latitude
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.Latitude, esSystemType.Decimal);
			}
		}

		public esQueryItem Longitude
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.Longitude, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ZipPostalCode
		{
			get
			{
				return new esQueryItem(this, ZipCodeMetadata.ColumnNames.ZipPostalCode, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ZipCodeCollection")]
	public partial class ZipCodeCollection : esZipCodeCollection, IEnumerable<ZipCode>
	{
		public ZipCodeCollection()
		{

		}

		public static implicit operator List<ZipCode>(ZipCodeCollection coll)
		{
			List<ZipCode> list = new List<ZipCode>();

			foreach (ZipCode emp in coll)
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
				return ZipCodeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ZipCodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ZipCode(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ZipCode();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ZipCodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ZipCodeQuery();
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
		public bool Load(ZipCodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ZipCode AddNew()
		{
			ZipCode entity = base.AddNewEntity() as ZipCode;

			return entity;
		}
		public ZipCode FindByPrimaryKey(String zipCode)
		{
			return base.FindByPrimaryKey(zipCode) as ZipCode;
		}

		#region IEnumerable< ZipCode> Members

		IEnumerator<ZipCode> IEnumerable<ZipCode>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ZipCode;
			}
		}

		#endregion

		private ZipCodeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ZipCode' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ZipCode ({ZipCode})")]
	[Serializable]
	public partial class ZipCode : esZipCode
	{
		public ZipCode()
		{
		}

		public ZipCode(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ZipCodeMetadata.Meta();
			}
		}

		override protected esZipCodeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ZipCodeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ZipCodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ZipCodeQuery();
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
		public bool Load(ZipCodeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ZipCodeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ZipCodeQuery : esZipCodeQuery
	{
		public ZipCodeQuery()
		{

		}

		public ZipCodeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ZipCodeQuery";
		}
	}

	[Serializable]
	public partial class ZipCodeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ZipCodeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.ZipCode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.ZipCode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"(' ')";
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.StreetName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"(' ')";
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.District, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"(' ')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.County, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"(' ')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.City, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.SRProvince, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.SRProvince;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"(' ')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.Latitude, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ZipCodeMetadata.PropertyNames.Latitude;
			c.NumericPrecision = 18;
			c.NumericScale = 12;
			c.HasDefault = true;
			c.Default = @"((0.0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.Longitude, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ZipCodeMetadata.PropertyNames.Longitude;
			c.NumericPrecision = 18;
			c.NumericScale = 12;
			c.HasDefault = true;
			c.Default = @"((0.0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ZipCodeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ZipCodeMetadata.ColumnNames.ZipPostalCode, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ZipCodeMetadata.PropertyNames.ZipPostalCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ZipCodeMetadata Meta()
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
			public const string ZipCode = "ZipCode";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
			public const string SRProvince = "SRProvince";
			public const string Latitude = "Latitude";
			public const string Longitude = "Longitude";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ZipPostalCode = "ZipPostalCode";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ZipCode = "ZipCode";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
			public const string SRProvince = "SRProvince";
			public const string Latitude = "Latitude";
			public const string Longitude = "Longitude";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ZipPostalCode = "ZipPostalCode";
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
			lock (typeof(ZipCodeMetadata))
			{
				if (ZipCodeMetadata.mapDelegates == null)
				{
					ZipCodeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ZipCodeMetadata.meta == null)
				{
					ZipCodeMetadata.meta = new ZipCodeMetadata();
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

				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProvince", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Latitude", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Longitude", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipPostalCode", new esTypeMap("varchar", "System.String"));


				meta.Source = "ZipCode";
				meta.Destination = "ZipCode";
				meta.spInsert = "proc_ZipCodeInsert";
				meta.spUpdate = "proc_ZipCodeUpdate";
				meta.spDelete = "proc_ZipCodeDelete";
				meta.spLoadAll = "proc_ZipCodeLoadAll";
				meta.spLoadByPrimaryKey = "proc_ZipCodeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ZipCodeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
