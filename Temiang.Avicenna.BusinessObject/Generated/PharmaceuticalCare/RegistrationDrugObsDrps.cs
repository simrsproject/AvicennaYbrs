/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2022 8:42:58 PM
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
	abstract public class esRegistrationDrugObsDrpsCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationDrugObsDrpsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationDrugObsDrpsCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationDrugObsDrpsQuery query)
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
			this.InitQuery(query as esRegistrationDrugObsDrpsQuery);
		}
		#endregion

		virtual public RegistrationDrugObsDrps DetachEntity(RegistrationDrugObsDrps entity)
		{
			return base.DetachEntity(entity) as RegistrationDrugObsDrps;
		}

		virtual public RegistrationDrugObsDrps AttachEntity(RegistrationDrugObsDrps entity)
		{
			return base.AttachEntity(entity) as RegistrationDrugObsDrps;
		}

		virtual public void Combine(RegistrationDrugObsDrpsCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationDrugObsDrps this[int index]
		{
			get
			{
				return base[index] as RegistrationDrugObsDrps;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationDrugObsDrps);
		}
	}

	[Serializable]
	abstract public class esRegistrationDrugObsDrps : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationDrugObsDrpsQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationDrugObsDrps()
		{
		}

		public esRegistrationDrugObsDrps(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 drugObsNo, String sRDrps)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, drugObsNo, sRDrps);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, drugObsNo, sRDrps);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 drugObsNo, String sRDrps)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, drugObsNo, sRDrps);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, drugObsNo, sRDrps);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 drugObsNo, String sRDrps)
		{
			esRegistrationDrugObsDrpsQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.DrugObsNo == drugObsNo, query.SRDrps == sRDrps);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 drugObsNo, String sRDrps)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("DrugObsNo", drugObsNo);
			parms.Add("SRDrps", sRDrps);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "DrugObsNo": this.str.DrugObsNo = (string)value; break;
						case "SRDrps": this.str.SRDrps = (string)value; break;
						case "IsYes": this.str.IsYes = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DrugObsNo":

							if (value == null || value is System.Int32)
								this.DrugObsNo = (System.Int32?)value;
							break;
						case "IsYes":

							if (value == null || value is System.Boolean)
								this.IsYes = (System.Boolean?)value;
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
		/// Maps to RegistrationDrugObsDrps.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsDrpsMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsDrpsMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsDrps.DrugObsNo
		/// </summary>
		virtual public System.Int32? DrugObsNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationDrugObsDrpsMetadata.ColumnNames.DrugObsNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationDrugObsDrpsMetadata.ColumnNames.DrugObsNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsDrps.SRDrps
		/// </summary>
		virtual public System.String SRDrps
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsDrpsMetadata.ColumnNames.SRDrps);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsDrpsMetadata.ColumnNames.SRDrps, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsDrps.IsYes
		/// </summary>
		virtual public System.Boolean? IsYes
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsDrpsMetadata.ColumnNames.IsYes);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsDrpsMetadata.ColumnNames.IsYes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsDrps.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObsDrps.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRegistrationDrugObsDrps entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String DrugObsNo
			{
				get
				{
					System.Int32? data = entity.DrugObsNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugObsNo = null;
					else entity.DrugObsNo = Convert.ToInt32(value);
				}
			}
			public System.String SRDrps
			{
				get
				{
					System.String data = entity.SRDrps;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDrps = null;
					else entity.SRDrps = Convert.ToString(value);
				}
			}
			public System.String IsYes
			{
				get
				{
					System.Boolean? data = entity.IsYes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsYes = null;
					else entity.IsYes = Convert.ToBoolean(value);
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
			private esRegistrationDrugObsDrps entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationDrugObsDrpsQuery query)
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
				throw new Exception("esRegistrationDrugObsDrps can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationDrugObsDrps : esRegistrationDrugObsDrps
	{
	}

	[Serializable]
	abstract public class esRegistrationDrugObsDrpsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDrugObsDrpsMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsDrpsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem DrugObsNo
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsDrpsMetadata.ColumnNames.DrugObsNo, esSystemType.Int32);
			}
		}

		public esQueryItem SRDrps
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsDrpsMetadata.ColumnNames.SRDrps, esSystemType.String);
			}
		}

		public esQueryItem IsYes
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsDrpsMetadata.ColumnNames.IsYes, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationDrugObsDrpsCollection")]
	public partial class RegistrationDrugObsDrpsCollection : esRegistrationDrugObsDrpsCollection, IEnumerable<RegistrationDrugObsDrps>
	{
		public RegistrationDrugObsDrpsCollection()
		{

		}

		public static implicit operator List<RegistrationDrugObsDrps>(RegistrationDrugObsDrpsCollection coll)
		{
			List<RegistrationDrugObsDrps> list = new List<RegistrationDrugObsDrps>();

			foreach (RegistrationDrugObsDrps emp in coll)
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
				return RegistrationDrugObsDrpsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDrugObsDrpsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationDrugObsDrps(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationDrugObsDrps();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationDrugObsDrpsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDrugObsDrpsQuery();
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
		public bool Load(RegistrationDrugObsDrpsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationDrugObsDrps AddNew()
		{
			RegistrationDrugObsDrps entity = base.AddNewEntity() as RegistrationDrugObsDrps;

			return entity;
		}
		public RegistrationDrugObsDrps FindByPrimaryKey(String registrationNo, Int32 drugObsNo, String sRDrps)
		{
			return base.FindByPrimaryKey(registrationNo, drugObsNo, sRDrps) as RegistrationDrugObsDrps;
		}

		#region IEnumerable< RegistrationDrugObsDrps> Members

		IEnumerator<RegistrationDrugObsDrps> IEnumerable<RegistrationDrugObsDrps>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationDrugObsDrps;
			}
		}

		#endregion

		private RegistrationDrugObsDrpsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationDrugObsDrps' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationDrugObsDrps ({RegistrationNo, DrugObsNo, SRDrps})")]
	[Serializable]
	public partial class RegistrationDrugObsDrps : esRegistrationDrugObsDrps
	{
		public RegistrationDrugObsDrps()
		{
		}

		public RegistrationDrugObsDrps(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDrugObsDrpsMetadata.Meta();
			}
		}

		override protected esRegistrationDrugObsDrpsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDrugObsDrpsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationDrugObsDrpsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDrugObsDrpsQuery();
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
		public bool Load(RegistrationDrugObsDrpsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationDrugObsDrpsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationDrugObsDrpsQuery : esRegistrationDrugObsDrpsQuery
	{
		public RegistrationDrugObsDrpsQuery()
		{

		}

		public RegistrationDrugObsDrpsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationDrugObsDrpsQuery";
		}
	}

	[Serializable]
	public partial class RegistrationDrugObsDrpsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationDrugObsDrpsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationDrugObsDrpsMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsDrpsMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsDrpsMetadata.ColumnNames.DrugObsNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationDrugObsDrpsMetadata.PropertyNames.DrugObsNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsDrpsMetadata.ColumnNames.SRDrps, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsDrpsMetadata.PropertyNames.SRDrps;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsDrpsMetadata.ColumnNames.IsYes, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsDrpsMetadata.PropertyNames.IsYes;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsDrpsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsDrpsMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationDrugObsDrpsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationDrugObsDrpsMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string DrugObsNo = "DrugObsNo";
			public const string SRDrps = "SRDrps";
			public const string IsYes = "IsYes";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string DrugObsNo = "DrugObsNo";
			public const string SRDrps = "SRDrps";
			public const string IsYes = "IsYes";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(RegistrationDrugObsDrpsMetadata))
			{
				if (RegistrationDrugObsDrpsMetadata.mapDelegates == null)
				{
					RegistrationDrugObsDrpsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationDrugObsDrpsMetadata.meta == null)
				{
					RegistrationDrugObsDrpsMetadata.meta = new RegistrationDrugObsDrpsMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DrugObsNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRDrps", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsYes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "RegistrationDrugObsDrps";
				meta.Destination = "RegistrationDrugObsDrps";
				meta.spInsert = "proc_RegistrationDrugObsDrpsInsert";
				meta.spUpdate = "proc_RegistrationDrugObsDrpsUpdate";
				meta.spDelete = "proc_RegistrationDrugObsDrpsDelete";
				meta.spLoadAll = "proc_RegistrationDrugObsDrpsLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationDrugObsDrpsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationDrugObsDrpsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
