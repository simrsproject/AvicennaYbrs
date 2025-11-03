/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/27/2023 3:51:59 PM
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
	abstract public class esOperationNotesTemplateCollection : esEntityCollectionWAuditLog
	{
		public esOperationNotesTemplateCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "OperationNotesTemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esOperationNotesTemplateQuery query)
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
			this.InitQuery(query as esOperationNotesTemplateQuery);
		}
		#endregion

		virtual public OperationNotesTemplate DetachEntity(OperationNotesTemplate entity)
		{
			return base.DetachEntity(entity) as OperationNotesTemplate;
		}

		virtual public OperationNotesTemplate AttachEntity(OperationNotesTemplate entity)
		{
			return base.AttachEntity(entity) as OperationNotesTemplate;
		}

		virtual public void Combine(OperationNotesTemplateCollection collection)
		{
			base.Combine(collection);
		}

		new public OperationNotesTemplate this[int index]
		{
			get
			{
				return base[index] as OperationNotesTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OperationNotesTemplate);
		}
	}

	[Serializable]
	abstract public class esOperationNotesTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOperationNotesTemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esOperationNotesTemplate()
		{
		}

		public esOperationNotesTemplate(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 templateID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 templateID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 templateID)
		{
			esOperationNotesTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateID == templateID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 templateID)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateID", templateID);
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
						case "TemplateID": this.str.TemplateID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "TemplateName": this.str.TemplateName = (string)value; break;
						case "TemplateText": this.str.TemplateText = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPostOp": this.str.IsPostOp = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TemplateID":

							if (value == null || value is System.Int32)
								this.TemplateID = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPostOp":

							if (value == null || value is System.Boolean)
								this.IsPostOp = (System.Boolean?)value;
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
		/// Maps to OperationNotesTemplate.TemplateID
		/// </summary>
		virtual public System.Int32? TemplateID
		{
			get
			{
				return base.GetSystemInt32(OperationNotesTemplateMetadata.ColumnNames.TemplateID);
			}

			set
			{
				base.SetSystemInt32(OperationNotesTemplateMetadata.ColumnNames.TemplateID, value);
			}
		}
		/// <summary>
		/// Maps to OperationNotesTemplate.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(OperationNotesTemplateMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(OperationNotesTemplateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to OperationNotesTemplate.TemplateName
		/// </summary>
		virtual public System.String TemplateName
		{
			get
			{
				return base.GetSystemString(OperationNotesTemplateMetadata.ColumnNames.TemplateName);
			}

			set
			{
				base.SetSystemString(OperationNotesTemplateMetadata.ColumnNames.TemplateName, value);
			}
		}
		/// <summary>
		/// Maps to OperationNotesTemplate.TemplateText
		/// </summary>
		virtual public System.String TemplateText
		{
			get
			{
				return base.GetSystemString(OperationNotesTemplateMetadata.ColumnNames.TemplateText);
			}

			set
			{
				base.SetSystemString(OperationNotesTemplateMetadata.ColumnNames.TemplateText, value);
			}
		}
		/// <summary>
		/// Maps to OperationNotesTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(OperationNotesTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(OperationNotesTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to OperationNotesTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(OperationNotesTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(OperationNotesTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to OperationNotesTemplate.IsPostOp
		/// </summary>
		virtual public System.Boolean? IsPostOp
		{
			get
			{
				return base.GetSystemBoolean(OperationNotesTemplateMetadata.ColumnNames.IsPostOp);
			}

			set
			{
				base.SetSystemBoolean(OperationNotesTemplateMetadata.ColumnNames.IsPostOp, value);
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
			public esStrings(esOperationNotesTemplate entity)
			{
				this.entity = entity;
			}
			public System.String TemplateID
			{
				get
				{
					System.Int32? data = entity.TemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateID = null;
					else entity.TemplateID = Convert.ToInt32(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String TemplateName
			{
				get
				{
					System.String data = entity.TemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateName = null;
					else entity.TemplateName = Convert.ToString(value);
				}
			}
			public System.String TemplateText
			{
				get
				{
					System.String data = entity.TemplateText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateText = null;
					else entity.TemplateText = Convert.ToString(value);
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
			public System.String IsPostOp
			{
				get
				{
					System.Boolean? data = entity.IsPostOp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPostOp = null;
					else entity.IsPostOp = Convert.ToBoolean(value);
				}
			}
			private esOperationNotesTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOperationNotesTemplateQuery query)
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
				throw new Exception("esOperationNotesTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class OperationNotesTemplate : esOperationNotesTemplate
	{
	}

	[Serializable]
	abstract public class esOperationNotesTemplateQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return OperationNotesTemplateMetadata.Meta();
			}
		}

		public esQueryItem TemplateID
		{
			get
			{
				return new esQueryItem(this, OperationNotesTemplateMetadata.ColumnNames.TemplateID, esSystemType.Int32);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, OperationNotesTemplateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem TemplateName
		{
			get
			{
				return new esQueryItem(this, OperationNotesTemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
			}
		}

		public esQueryItem TemplateText
		{
			get
			{
				return new esQueryItem(this, OperationNotesTemplateMetadata.ColumnNames.TemplateText, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, OperationNotesTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, OperationNotesTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPostOp
		{
			get
			{
				return new esQueryItem(this, OperationNotesTemplateMetadata.ColumnNames.IsPostOp, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OperationNotesTemplateCollection")]
	public partial class OperationNotesTemplateCollection : esOperationNotesTemplateCollection, IEnumerable<OperationNotesTemplate>
	{
		public OperationNotesTemplateCollection()
		{

		}

		public static implicit operator List<OperationNotesTemplate>(OperationNotesTemplateCollection coll)
		{
			List<OperationNotesTemplate> list = new List<OperationNotesTemplate>();

			foreach (OperationNotesTemplate emp in coll)
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
				return OperationNotesTemplateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OperationNotesTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OperationNotesTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OperationNotesTemplate();
		}

		#endregion

		[BrowsableAttribute(false)]
		public OperationNotesTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OperationNotesTemplateQuery();
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
		public bool Load(OperationNotesTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public OperationNotesTemplate AddNew()
		{
			OperationNotesTemplate entity = base.AddNewEntity() as OperationNotesTemplate;

			return entity;
		}
		public OperationNotesTemplate FindByPrimaryKey(Int32 templateID)
		{
			return base.FindByPrimaryKey(templateID) as OperationNotesTemplate;
		}

		#region IEnumerable< OperationNotesTemplate> Members

		IEnumerator<OperationNotesTemplate> IEnumerable<OperationNotesTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as OperationNotesTemplate;
			}
		}

		#endregion

		private OperationNotesTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'OperationNotesTemplate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("OperationNotesTemplate ({TemplateID})")]
	[Serializable]
	public partial class OperationNotesTemplate : esOperationNotesTemplate
	{
		public OperationNotesTemplate()
		{
		}

		public OperationNotesTemplate(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OperationNotesTemplateMetadata.Meta();
			}
		}

		override protected esOperationNotesTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OperationNotesTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public OperationNotesTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OperationNotesTemplateQuery();
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
		public bool Load(OperationNotesTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private OperationNotesTemplateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class OperationNotesTemplateQuery : esOperationNotesTemplateQuery
	{
		public OperationNotesTemplateQuery()
		{

		}

		public OperationNotesTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "OperationNotesTemplateQuery";
		}
	}

	[Serializable]
	public partial class OperationNotesTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OperationNotesTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OperationNotesTemplateMetadata.ColumnNames.TemplateID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OperationNotesTemplateMetadata.PropertyNames.TemplateID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(OperationNotesTemplateMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationNotesTemplateMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OperationNotesTemplateMetadata.ColumnNames.TemplateName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationNotesTemplateMetadata.PropertyNames.TemplateName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OperationNotesTemplateMetadata.ColumnNames.TemplateText, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationNotesTemplateMetadata.PropertyNames.TemplateText;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OperationNotesTemplateMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OperationNotesTemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OperationNotesTemplateMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = OperationNotesTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OperationNotesTemplateMetadata.ColumnNames.IsPostOp, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = OperationNotesTemplateMetadata.PropertyNames.IsPostOp;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public OperationNotesTemplateMetadata Meta()
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
			public const string TemplateID = "TemplateID";
			public const string ParamedicID = "ParamedicID";
			public const string TemplateName = "TemplateName";
			public const string TemplateText = "TemplateText";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPostOp = "IsPostOp";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TemplateID = "TemplateID";
			public const string ParamedicID = "ParamedicID";
			public const string TemplateName = "TemplateName";
			public const string TemplateText = "TemplateText";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPostOp = "IsPostOp";
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
			lock (typeof(OperationNotesTemplateMetadata))
			{
				if (OperationNotesTemplateMetadata.mapDelegates == null)
				{
					OperationNotesTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (OperationNotesTemplateMetadata.meta == null)
				{
					OperationNotesTemplateMetadata.meta = new OperationNotesTemplateMetadata();
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

				meta.AddTypeMap("TemplateID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TemplateText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPostOp", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "OperationNotesTemplate";
				meta.Destination = "OperationNotesTemplate";
				meta.spInsert = "proc_OperationNotesTemplateInsert";
				meta.spUpdate = "proc_OperationNotesTemplateUpdate";
				meta.spDelete = "proc_OperationNotesTemplateDelete";
				meta.spLoadAll = "proc_OperationNotesTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_OperationNotesTemplateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OperationNotesTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
