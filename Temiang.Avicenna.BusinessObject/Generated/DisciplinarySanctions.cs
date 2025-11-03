/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/25/2020 3:36:34 PM
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
	abstract public class esDisciplinarySanctionsCollection : esEntityCollectionWAuditLog
	{
		public esDisciplinarySanctionsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "DisciplinarySanctionsCollection";
		}

		#region Query Logic
		protected void InitQuery(esDisciplinarySanctionsQuery query)
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
			this.InitQuery(query as esDisciplinarySanctionsQuery);
		}
		#endregion

		virtual public DisciplinarySanctions DetachEntity(DisciplinarySanctions entity)
		{
			return base.DetachEntity(entity) as DisciplinarySanctions;
		}

		virtual public DisciplinarySanctions AttachEntity(DisciplinarySanctions entity)
		{
			return base.AttachEntity(entity) as DisciplinarySanctions;
		}

		virtual public void Combine(DisciplinarySanctionsCollection collection)
		{
			base.Combine(collection);
		}

		new public DisciplinarySanctions this[int index]
		{
			get
			{
				return base[index] as DisciplinarySanctions;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DisciplinarySanctions);
		}
	}

	[Serializable]
	abstract public class esDisciplinarySanctions : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDisciplinarySanctionsQuery GetDynamicQuery()
		{
			return null;
		}

		public esDisciplinarySanctions()
		{
		}

		public esDisciplinarySanctions(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 disciplinarySanctionsID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(disciplinarySanctionsID);
			else
				return LoadByPrimaryKeyStoredProcedure(disciplinarySanctionsID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 disciplinarySanctionsID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(disciplinarySanctionsID);
			else
				return LoadByPrimaryKeyStoredProcedure(disciplinarySanctionsID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 disciplinarySanctionsID)
		{
			esDisciplinarySanctionsQuery query = this.GetDynamicQuery();
			query.Where(query.DisciplinarySanctionsID == disciplinarySanctionsID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 disciplinarySanctionsID)
		{
			esParameters parms = new esParameters();
			parms.Add("DisciplinarySanctionsID", disciplinarySanctionsID);
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
						case "DisciplinarySanctionsID": this.str.DisciplinarySanctionsID = (string)value; break;
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;
						case "StartValue": this.str.StartValue = (string)value; break;
						case "EndValue": this.str.EndValue = (string)value; break;
						case "CutPercentage": this.str.CutPercentage = (string)value; break;
						case "ValidFromDate": this.str.ValidFromDate = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DisciplinarySanctionsID":

							if (value == null || value is System.Int32)
								this.DisciplinarySanctionsID = (System.Int32?)value;
							break;
						case "StartValue":

							if (value == null || value is System.Int16)
								this.StartValue = (System.Int16?)value;
							break;
						case "EndValue":

							if (value == null || value is System.Int16)
								this.EndValue = (System.Int16?)value;
							break;
						case "CutPercentage":

							if (value == null || value is System.Decimal)
								this.CutPercentage = (System.Decimal?)value;
							break;
						case "ValidFromDate":

							if (value == null || value is System.DateTime)
								this.ValidFromDate = (System.DateTime?)value;
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
		/// Maps to DisciplinarySanctions.DisciplinarySanctionsID
		/// </summary>
		virtual public System.Int32? DisciplinarySanctionsID
		{
			get
			{
				return base.GetSystemInt32(DisciplinarySanctionsMetadata.ColumnNames.DisciplinarySanctionsID);
			}

			set
			{
				base.SetSystemInt32(DisciplinarySanctionsMetadata.ColumnNames.DisciplinarySanctionsID, value);
			}
		}
		/// <summary>
		/// Maps to DisciplinarySanctions.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(DisciplinarySanctionsMetadata.ColumnNames.SREmploymentType);
			}

			set
			{
				base.SetSystemString(DisciplinarySanctionsMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to DisciplinarySanctions.StartValue
		/// </summary>
		virtual public System.Int16? StartValue
		{
			get
			{
				return base.GetSystemInt16(DisciplinarySanctionsMetadata.ColumnNames.StartValue);
			}

			set
			{
				base.SetSystemInt16(DisciplinarySanctionsMetadata.ColumnNames.StartValue, value);
			}
		}
		/// <summary>
		/// Maps to DisciplinarySanctions.EndValue
		/// </summary>
		virtual public System.Int16? EndValue
		{
			get
			{
				return base.GetSystemInt16(DisciplinarySanctionsMetadata.ColumnNames.EndValue);
			}

			set
			{
				base.SetSystemInt16(DisciplinarySanctionsMetadata.ColumnNames.EndValue, value);
			}
		}
		/// <summary>
		/// Maps to DisciplinarySanctions.CutPercentage
		/// </summary>
		virtual public System.Decimal? CutPercentage
		{
			get
			{
				return base.GetSystemDecimal(DisciplinarySanctionsMetadata.ColumnNames.CutPercentage);
			}

			set
			{
				base.SetSystemDecimal(DisciplinarySanctionsMetadata.ColumnNames.CutPercentage, value);
			}
		}
		/// <summary>
		/// Maps to DisciplinarySanctions.ValidFromDate
		/// </summary>
		virtual public System.DateTime? ValidFromDate
		{
			get
			{
				return base.GetSystemDateTime(DisciplinarySanctionsMetadata.ColumnNames.ValidFromDate);
			}

			set
			{
				base.SetSystemDateTime(DisciplinarySanctionsMetadata.ColumnNames.ValidFromDate, value);
			}
		}
		/// <summary>
		/// Maps to DisciplinarySanctions.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DisciplinarySanctionsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(DisciplinarySanctionsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to DisciplinarySanctions.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DisciplinarySanctionsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(DisciplinarySanctionsMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDisciplinarySanctions entity)
			{
				this.entity = entity;
			}
			public System.String DisciplinarySanctionsID
			{
				get
				{
					System.Int32? data = entity.DisciplinarySanctionsID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisciplinarySanctionsID = null;
					else entity.DisciplinarySanctionsID = Convert.ToInt32(value);
				}
			}
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
				}
			}
			public System.String StartValue
			{
				get
				{
					System.Int16? data = entity.StartValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartValue = null;
					else entity.StartValue = Convert.ToInt16(value);
				}
			}
			public System.String EndValue
			{
				get
				{
					System.Int16? data = entity.EndValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndValue = null;
					else entity.EndValue = Convert.ToInt16(value);
				}
			}
			public System.String CutPercentage
			{
				get
				{
					System.Decimal? data = entity.CutPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CutPercentage = null;
					else entity.CutPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String ValidFromDate
			{
				get
				{
					System.DateTime? data = entity.ValidFromDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFromDate = null;
					else entity.ValidFromDate = Convert.ToDateTime(value);
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
			private esDisciplinarySanctions entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDisciplinarySanctionsQuery query)
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
				throw new Exception("esDisciplinarySanctions can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class DisciplinarySanctions : esDisciplinarySanctions
	{
	}

	[Serializable]
	abstract public class esDisciplinarySanctionsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return DisciplinarySanctionsMetadata.Meta();
			}
		}

		public esQueryItem DisciplinarySanctionsID
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.DisciplinarySanctionsID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		}

		public esQueryItem StartValue
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.StartValue, esSystemType.Int16);
			}
		}

		public esQueryItem EndValue
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.EndValue, esSystemType.Int16);
			}
		}

		public esQueryItem CutPercentage
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.CutPercentage, esSystemType.Decimal);
			}
		}

		public esQueryItem ValidFromDate
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.ValidFromDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DisciplinarySanctionsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DisciplinarySanctionsCollection")]
	public partial class DisciplinarySanctionsCollection : esDisciplinarySanctionsCollection, IEnumerable<DisciplinarySanctions>
	{
		public DisciplinarySanctionsCollection()
		{

		}

		public static implicit operator List<DisciplinarySanctions>(DisciplinarySanctionsCollection coll)
		{
			List<DisciplinarySanctions> list = new List<DisciplinarySanctions>();

			foreach (DisciplinarySanctions emp in coll)
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
				return DisciplinarySanctionsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DisciplinarySanctionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DisciplinarySanctions(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DisciplinarySanctions();
		}

		#endregion

		[BrowsableAttribute(false)]
		public DisciplinarySanctionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DisciplinarySanctionsQuery();
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
		public bool Load(DisciplinarySanctionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public DisciplinarySanctions AddNew()
		{
			DisciplinarySanctions entity = base.AddNewEntity() as DisciplinarySanctions;

			return entity;
		}
		public DisciplinarySanctions FindByPrimaryKey(Int32 disciplinarySanctionsID)
		{
			return base.FindByPrimaryKey(disciplinarySanctionsID) as DisciplinarySanctions;
		}

		#region IEnumerable< DisciplinarySanctions> Members

		IEnumerator<DisciplinarySanctions> IEnumerable<DisciplinarySanctions>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as DisciplinarySanctions;
			}
		}

		#endregion

		private DisciplinarySanctionsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DisciplinarySanctions' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("DisciplinarySanctions ({DisciplinarySanctionsID})")]
	[Serializable]
	public partial class DisciplinarySanctions : esDisciplinarySanctions
	{
		public DisciplinarySanctions()
		{
		}

		public DisciplinarySanctions(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DisciplinarySanctionsMetadata.Meta();
			}
		}

		override protected esDisciplinarySanctionsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DisciplinarySanctionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public DisciplinarySanctionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DisciplinarySanctionsQuery();
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
		public bool Load(DisciplinarySanctionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private DisciplinarySanctionsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class DisciplinarySanctionsQuery : esDisciplinarySanctionsQuery
	{
		public DisciplinarySanctionsQuery()
		{

		}

		public DisciplinarySanctionsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "DisciplinarySanctionsQuery";
		}
	}

	[Serializable]
	public partial class DisciplinarySanctionsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DisciplinarySanctionsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.DisciplinarySanctionsID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.DisciplinarySanctionsID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.SREmploymentType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.StartValue, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.StartValue;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.EndValue, 3, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.EndValue;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.CutPercentage, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.CutPercentage;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.ValidFromDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.ValidFromDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(DisciplinarySanctionsMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DisciplinarySanctionsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public DisciplinarySanctionsMetadata Meta()
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
			public const string DisciplinarySanctionsID = "DisciplinarySanctionsID";
			public const string SREmploymentType = "SREmploymentType";
			public const string StartValue = "StartValue";
			public const string EndValue = "EndValue";
			public const string CutPercentage = "CutPercentage";
			public const string ValidFromDate = "ValidFromDate";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string DisciplinarySanctionsID = "DisciplinarySanctionsID";
			public const string SREmploymentType = "SREmploymentType";
			public const string StartValue = "StartValue";
			public const string EndValue = "EndValue";
			public const string CutPercentage = "CutPercentage";
			public const string ValidFromDate = "ValidFromDate";
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
			lock (typeof(DisciplinarySanctionsMetadata))
			{
				if (DisciplinarySanctionsMetadata.mapDelegates == null)
				{
					DisciplinarySanctionsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (DisciplinarySanctionsMetadata.meta == null)
				{
					DisciplinarySanctionsMetadata.meta = new DisciplinarySanctionsMetadata();
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

				meta.AddTypeMap("DisciplinarySanctionsID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartValue", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("EndValue", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("CutPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ValidFromDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "DisciplinarySanctions";
				meta.Destination = "DisciplinarySanctions";
				meta.spInsert = "proc_DisciplinarySanctionsInsert";
				meta.spUpdate = "proc_DisciplinarySanctionsUpdate";
				meta.spDelete = "proc_DisciplinarySanctionsDelete";
				meta.spLoadAll = "proc_DisciplinarySanctionsLoadAll";
				meta.spLoadByPrimaryKey = "proc_DisciplinarySanctionsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DisciplinarySanctionsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
