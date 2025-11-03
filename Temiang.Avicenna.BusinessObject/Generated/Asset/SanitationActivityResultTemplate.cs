/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/26/2021 11:22:09 AM
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
	abstract public class esSanitationActivityResultTemplateCollection : esEntityCollectionWAuditLog
	{
		public esSanitationActivityResultTemplateCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationActivityResultTemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationActivityResultTemplateQuery query)
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
			this.InitQuery(query as esSanitationActivityResultTemplateQuery);
		}
		#endregion

		virtual public SanitationActivityResultTemplate DetachEntity(SanitationActivityResultTemplate entity)
		{
			return base.DetachEntity(entity) as SanitationActivityResultTemplate;
		}

		virtual public SanitationActivityResultTemplate AttachEntity(SanitationActivityResultTemplate entity)
		{
			return base.AttachEntity(entity) as SanitationActivityResultTemplate;
		}

		virtual public void Combine(SanitationActivityResultTemplateCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationActivityResultTemplate this[int index]
		{
			get
			{
				return base[index] as SanitationActivityResultTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationActivityResultTemplate);
		}
	}

	[Serializable]
	abstract public class esSanitationActivityResultTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationActivityResultTemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationActivityResultTemplate()
		{
		}

		public esSanitationActivityResultTemplate(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 sanitationActivityResultID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sanitationActivityResultID);
			else
				return LoadByPrimaryKeyStoredProcedure(sanitationActivityResultID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 sanitationActivityResultID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sanitationActivityResultID);
			else
				return LoadByPrimaryKeyStoredProcedure(sanitationActivityResultID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 sanitationActivityResultID)
		{
			esSanitationActivityResultTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.SanitationActivityResultID == sanitationActivityResultID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 sanitationActivityResultID)
		{
			esParameters parms = new esParameters();
			parms.Add("SanitationActivityResultID", sanitationActivityResultID);
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
						case "SanitationActivityResultID": this.str.SanitationActivityResultID = (string)value; break;
						case "SRWorkTradeItem": this.str.SRWorkTradeItem = (string)value; break;
						case "ResultTemplateName": this.str.ResultTemplateName = (string)value; break;
						case "Result": this.str.Result = (string)value; break;
						case "Summary": this.str.Summary = (string)value; break;
						case "Suggest": this.str.Suggest = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SanitationActivityResultID":

							if (value == null || value is System.Int32)
								this.SanitationActivityResultID = (System.Int32?)value;
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
		/// Maps to SanitationActivityResultTemplate.SanitationActivityResultID
		/// </summary>
		virtual public System.Int32? SanitationActivityResultID
		{
			get
			{
				return base.GetSystemInt32(SanitationActivityResultTemplateMetadata.ColumnNames.SanitationActivityResultID);
			}

			set
			{
				base.SetSystemInt32(SanitationActivityResultTemplateMetadata.ColumnNames.SanitationActivityResultID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResultTemplate.SRWorkTradeItem
		/// </summary>
		virtual public System.String SRWorkTradeItem
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.SRWorkTradeItem);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.SRWorkTradeItem, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResultTemplate.ResultTemplateName
		/// </summary>
		virtual public System.String ResultTemplateName
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.ResultTemplateName);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.ResultTemplateName, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResultTemplate.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.Result);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.Result, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResultTemplate.Summary
		/// </summary>
		virtual public System.String Summary
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.Summary);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.Summary, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResultTemplate.Suggest
		/// </summary>
		virtual public System.String Suggest
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.Suggest);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.Suggest, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResultTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationActivityResultTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationActivityResultTemplate entity)
			{
				this.entity = entity;
			}
			public System.String SanitationActivityResultID
			{
				get
				{
					System.Int32? data = entity.SanitationActivityResultID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SanitationActivityResultID = null;
					else entity.SanitationActivityResultID = Convert.ToInt32(value);
				}
			}
			public System.String SRWorkTradeItem
			{
				get
				{
					System.String data = entity.SRWorkTradeItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkTradeItem = null;
					else entity.SRWorkTradeItem = Convert.ToString(value);
				}
			}
			public System.String ResultTemplateName
			{
				get
				{
					System.String data = entity.ResultTemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultTemplateName = null;
					else entity.ResultTemplateName = Convert.ToString(value);
				}
			}
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
			public System.String Summary
			{
				get
				{
					System.String data = entity.Summary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Summary = null;
					else entity.Summary = Convert.ToString(value);
				}
			}
			public System.String Suggest
			{
				get
				{
					System.String data = entity.Suggest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Suggest = null;
					else entity.Suggest = Convert.ToString(value);
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
			private esSanitationActivityResultTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationActivityResultTemplateQuery query)
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
				throw new Exception("esSanitationActivityResultTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationActivityResultTemplate : esSanitationActivityResultTemplate
	{
	}

	[Serializable]
	abstract public class esSanitationActivityResultTemplateQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationActivityResultTemplateMetadata.Meta();
			}
		}

		public esQueryItem SanitationActivityResultID
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.SanitationActivityResultID, esSystemType.Int32);
			}
		}

		public esQueryItem SRWorkTradeItem
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.SRWorkTradeItem, esSystemType.String);
			}
		}

		public esQueryItem ResultTemplateName
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.ResultTemplateName, esSystemType.String);
			}
		}

		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.Result, esSystemType.String);
			}
		}

		public esQueryItem Summary
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.Summary, esSystemType.String);
			}
		}

		public esQueryItem Suggest
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.Suggest, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationActivityResultTemplateCollection")]
	public partial class SanitationActivityResultTemplateCollection : esSanitationActivityResultTemplateCollection, IEnumerable<SanitationActivityResultTemplate>
	{
		public SanitationActivityResultTemplateCollection()
		{

		}

		public static implicit operator List<SanitationActivityResultTemplate>(SanitationActivityResultTemplateCollection coll)
		{
			List<SanitationActivityResultTemplate> list = new List<SanitationActivityResultTemplate>();

			foreach (SanitationActivityResultTemplate emp in coll)
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
				return SanitationActivityResultTemplateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationActivityResultTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationActivityResultTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationActivityResultTemplate();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationActivityResultTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationActivityResultTemplateQuery();
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
		public bool Load(SanitationActivityResultTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationActivityResultTemplate AddNew()
		{
			SanitationActivityResultTemplate entity = base.AddNewEntity() as SanitationActivityResultTemplate;

			return entity;
		}
		public SanitationActivityResultTemplate FindByPrimaryKey(Int32 sanitationActivityResultID)
		{
			return base.FindByPrimaryKey(sanitationActivityResultID) as SanitationActivityResultTemplate;
		}

		#region IEnumerable< SanitationActivityResultTemplate> Members

		IEnumerator<SanitationActivityResultTemplate> IEnumerable<SanitationActivityResultTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationActivityResultTemplate;
			}
		}

		#endregion

		private SanitationActivityResultTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationActivityResultTemplate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationActivityResultTemplate ({SanitationActivityResultID})")]
	[Serializable]
	public partial class SanitationActivityResultTemplate : esSanitationActivityResultTemplate
	{
		public SanitationActivityResultTemplate()
		{
		}

		public SanitationActivityResultTemplate(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationActivityResultTemplateMetadata.Meta();
			}
		}

		override protected esSanitationActivityResultTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationActivityResultTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationActivityResultTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationActivityResultTemplateQuery();
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
		public bool Load(SanitationActivityResultTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationActivityResultTemplateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationActivityResultTemplateQuery : esSanitationActivityResultTemplateQuery
	{
		public SanitationActivityResultTemplateQuery()
		{

		}

		public SanitationActivityResultTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationActivityResultTemplateQuery";
		}
	}

	[Serializable]
	public partial class SanitationActivityResultTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationActivityResultTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.SanitationActivityResultID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.SanitationActivityResultID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.SRWorkTradeItem, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.SRWorkTradeItem;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.ResultTemplateName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.ResultTemplateName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.Result, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.Summary, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.Summary;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.Suggest, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.Suggest;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationActivityResultTemplateMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationActivityResultTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationActivityResultTemplateMetadata Meta()
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
			public const string SanitationActivityResultID = "SanitationActivityResultID";
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string ResultTemplateName = "ResultTemplateName";
			public const string Result = "Result";
			public const string Summary = "Summary";
			public const string Suggest = "Suggest";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SanitationActivityResultID = "SanitationActivityResultID";
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string ResultTemplateName = "ResultTemplateName";
			public const string Result = "Result";
			public const string Summary = "Summary";
			public const string Suggest = "Suggest";
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
			lock (typeof(SanitationActivityResultTemplateMetadata))
			{
				if (SanitationActivityResultTemplateMetadata.mapDelegates == null)
				{
					SanitationActivityResultTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationActivityResultTemplateMetadata.meta == null)
				{
					SanitationActivityResultTemplateMetadata.meta = new SanitationActivityResultTemplateMetadata();
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

				meta.AddTypeMap("SanitationActivityResultID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRWorkTradeItem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultTemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Summary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Suggest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationActivityResultTemplate";
				meta.Destination = "SanitationActivityResultTemplate";
				meta.spInsert = "proc_SanitationActivityResultTemplateInsert";
				meta.spUpdate = "proc_SanitationActivityResultTemplateUpdate";
				meta.spDelete = "proc_SanitationActivityResultTemplateDelete";
				meta.spLoadAll = "proc_SanitationActivityResultTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationActivityResultTemplateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationActivityResultTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
