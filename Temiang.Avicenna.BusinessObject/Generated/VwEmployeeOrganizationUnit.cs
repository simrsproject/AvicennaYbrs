/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/4/2023 1:52:24 PM
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
	abstract public class esVwEmployeeOrganizationUnitCollection : esEntityCollection
	{
		public esVwEmployeeOrganizationUnitCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "VwEmployeeOrganizationUnitCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwEmployeeOrganizationUnitQuery query)
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
			this.InitQuery(query as esVwEmployeeOrganizationUnitQuery);
		}
		#endregion

		virtual public VwEmployeeOrganizationUnit DetachEntity(VwEmployeeOrganizationUnit entity)
		{
			return base.DetachEntity(entity) as VwEmployeeOrganizationUnit;
		}

		virtual public VwEmployeeOrganizationUnit AttachEntity(VwEmployeeOrganizationUnit entity)
		{
			return base.AttachEntity(entity) as VwEmployeeOrganizationUnit;
		}

		virtual public void Combine(VwEmployeeOrganizationUnitCollection collection)
		{
			base.Combine(collection);
		}

		new public VwEmployeeOrganizationUnit this[int index]
		{
			get
			{
				return base[index] as VwEmployeeOrganizationUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwEmployeeOrganizationUnit);
		}
	}

	[Serializable]
	abstract public class esVwEmployeeOrganizationUnit : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwEmployeeOrganizationUnitQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwEmployeeOrganizationUnit()
		{
		}

		public esVwEmployeeOrganizationUnit(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey

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
						case "PersonID": this.str.PersonID = (string)value; break;
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
						case "SubOrganizationUnitID": this.str.SubOrganizationUnitID = (string)value; break;
						case "SubDivisonID": this.str.SubDivisonID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "OrganizationUnitID":

							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						case "SubOrganizationUnitID":

							if (value == null || value is System.Int32)
								this.SubOrganizationUnitID = (System.Int32?)value;
							break;
						case "SubDivisonID":

							if (value == null || value is System.Int32)
								this.SubDivisonID = (System.Int32?)value;
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
		/// Maps to VwEmployeeOrganizationUnit.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeOrganizationUnit.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeOrganizationUnit.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.SubOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeOrganizationUnit.SubDivisonID
		/// </summary>
		virtual public System.Int32? SubDivisonID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.SubDivisonID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeOrganizationUnitMetadata.ColumnNames.SubDivisonID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeOrganizationUnit.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwEmployeeOrganizationUnitMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(VwEmployeeOrganizationUnitMetadata.ColumnNames.ServiceUnitID, value);
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
			public esStrings(esVwEmployeeOrganizationUnit entity)
			{
				this.entity = entity;
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
			public System.String OrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.OrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
					else entity.OrganizationUnitID = Convert.ToInt32(value);
				}
			}
			public System.String SubOrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.SubOrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubOrganizationUnitID = null;
					else entity.SubOrganizationUnitID = Convert.ToInt32(value);
				}
			}
			public System.String SubDivisonID
			{
				get
				{
					System.Int32? data = entity.SubDivisonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubDivisonID = null;
					else entity.SubDivisonID = Convert.ToInt32(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			private esVwEmployeeOrganizationUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwEmployeeOrganizationUnitQuery query)
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
				throw new Exception("esVwEmployeeOrganizationUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VwEmployeeOrganizationUnit : esVwEmployeeOrganizationUnit
	{
	}

	[Serializable]
	abstract public class esVwEmployeeOrganizationUnitQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return VwEmployeeOrganizationUnitMetadata.Meta();
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeOrganizationUnitMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeOrganizationUnitMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeOrganizationUnitMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubDivisonID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeOrganizationUnitMetadata.ColumnNames.SubDivisonID, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeOrganizationUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwEmployeeOrganizationUnitCollection")]
	public partial class VwEmployeeOrganizationUnitCollection : esVwEmployeeOrganizationUnitCollection, IEnumerable<VwEmployeeOrganizationUnit>
	{
		public VwEmployeeOrganizationUnitCollection()
		{

		}

		public static implicit operator List<VwEmployeeOrganizationUnit>(VwEmployeeOrganizationUnitCollection coll)
		{
			List<VwEmployeeOrganizationUnit> list = new List<VwEmployeeOrganizationUnit>();

			foreach (VwEmployeeOrganizationUnit emp in coll)
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
				return VwEmployeeOrganizationUnitMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwEmployeeOrganizationUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwEmployeeOrganizationUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwEmployeeOrganizationUnit();
		}

		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}

		#endregion

		[BrowsableAttribute(false)]
		public VwEmployeeOrganizationUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwEmployeeOrganizationUnitQuery();
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
		public bool Load(VwEmployeeOrganizationUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VwEmployeeOrganizationUnit AddNew()
		{
			VwEmployeeOrganizationUnit entity = base.AddNewEntity() as VwEmployeeOrganizationUnit;

			return entity;
		}

		#region IEnumerable< VwEmployeeOrganizationUnit> Members

		IEnumerator<VwEmployeeOrganizationUnit> IEnumerable<VwEmployeeOrganizationUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as VwEmployeeOrganizationUnit;
			}
		}

		#endregion

		private VwEmployeeOrganizationUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VwEmployeeOrganizationUnit' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VwEmployeeOrganizationUnit ()")]
	[Serializable]
	public partial class VwEmployeeOrganizationUnit : esVwEmployeeOrganizationUnit
	{
		public VwEmployeeOrganizationUnit()
		{
		}

		public VwEmployeeOrganizationUnit(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwEmployeeOrganizationUnitMetadata.Meta();
			}
		}

		override protected esVwEmployeeOrganizationUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwEmployeeOrganizationUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public VwEmployeeOrganizationUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwEmployeeOrganizationUnitQuery();
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
		public bool Load(VwEmployeeOrganizationUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private VwEmployeeOrganizationUnitQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VwEmployeeOrganizationUnitQuery : esVwEmployeeOrganizationUnitQuery
	{
		public VwEmployeeOrganizationUnitQuery()
		{

		}

		public VwEmployeeOrganizationUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "VwEmployeeOrganizationUnitQuery";
		}
	}

	[Serializable]
	public partial class VwEmployeeOrganizationUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwEmployeeOrganizationUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwEmployeeOrganizationUnitMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeOrganizationUnitMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeOrganizationUnitMetadata.ColumnNames.OrganizationUnitID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeOrganizationUnitMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeOrganizationUnitMetadata.ColumnNames.SubOrganizationUnitID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeOrganizationUnitMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeOrganizationUnitMetadata.ColumnNames.SubDivisonID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeOrganizationUnitMetadata.PropertyNames.SubDivisonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeOrganizationUnitMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeOrganizationUnitMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);


		}
		#endregion

		static public VwEmployeeOrganizationUnitMetadata Meta()
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
			public const string PersonID = "PersonID";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string SubDivisonID = "SubDivisonID";
			public const string ServiceUnitID = "ServiceUnitID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonID = "PersonID";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string SubDivisonID = "SubDivisonID";
			public const string ServiceUnitID = "ServiceUnitID";
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
			lock (typeof(VwEmployeeOrganizationUnitMetadata))
			{
				if (VwEmployeeOrganizationUnitMetadata.mapDelegates == null)
				{
					VwEmployeeOrganizationUnitMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (VwEmployeeOrganizationUnitMetadata.meta == null)
				{
					VwEmployeeOrganizationUnitMetadata.meta = new VwEmployeeOrganizationUnitMetadata();
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

				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubDivisonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));


				meta.Source = "Vw_EmployeeOrganizationUnit";
				meta.Destination = "Vw_EmployeeOrganizationUnit";
				meta.spInsert = "proc_Vw_EmployeeOrganizationUnitInsert";
				meta.spUpdate = "proc_Vw_EmployeeOrganizationUnitUpdate";
				meta.spDelete = "proc_Vw_EmployeeOrganizationUnitDelete";
				meta.spLoadAll = "proc_Vw_EmployeeOrganizationUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_Vw_EmployeeOrganizationUnitLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwEmployeeOrganizationUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
