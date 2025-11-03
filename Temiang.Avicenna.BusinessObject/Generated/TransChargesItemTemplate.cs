/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/22/2022 11:44:18 AM
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
	abstract public class esTransChargesItemTemplateCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesItemTemplateCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransChargesItemTemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesItemTemplateQuery query)
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
			this.InitQuery(query as esTransChargesItemTemplateQuery);
		}
		#endregion

		virtual public TransChargesItemTemplate DetachEntity(TransChargesItemTemplate entity)
		{
			return base.DetachEntity(entity) as TransChargesItemTemplate;
		}

		virtual public TransChargesItemTemplate AttachEntity(TransChargesItemTemplate entity)
		{
			return base.AttachEntity(entity) as TransChargesItemTemplate;
		}

		virtual public void Combine(TransChargesItemTemplateCollection collection)
		{
			base.Combine(collection);
		}

		new public TransChargesItemTemplate this[int index]
		{
			get
			{
				return base[index] as TransChargesItemTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesItemTemplate);
		}
	}

	[Serializable]
	abstract public class esTransChargesItemTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesItemTemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesItemTemplate()
		{
		}

		public esTransChargesItemTemplate(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String templateNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(templateNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String templateNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(templateNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String templateNo, String sequenceNo)
		{
			esTransChargesItemTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateNo == templateNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String templateNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateNo", templateNo);
			parms.Add("SequenceNo", sequenceNo);
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
						case "TemplateNo": this.str.TemplateNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ChargeQuantity": this.str.ChargeQuantity = (string)value; break;
						case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;
						case "LastCreateUserID": this.str.LastCreateUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ChargeQuantity":

							if (value == null || value is System.Decimal)
								this.ChargeQuantity = (System.Decimal?)value;
							break;
						case "LastCreateDateTime":

							if (value == null || value is System.DateTime)
								this.LastCreateDateTime = (System.DateTime?)value;
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
		/// Maps to TransChargesItemTemplate.TemplateNo
		/// </summary>
		virtual public System.String TemplateNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemTemplateMetadata.ColumnNames.TemplateNo);
			}

			set
			{
				base.SetSystemString(TransChargesItemTemplateMetadata.ColumnNames.TemplateNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemTemplate.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemTemplateMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(TransChargesItemTemplateMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemTemplate.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TransChargesItemTemplateMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(TransChargesItemTemplateMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemTemplate.ChargeQuantity
		/// </summary>
		virtual public System.Decimal? ChargeQuantity
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemTemplateMetadata.ColumnNames.ChargeQuantity);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemTemplateMetadata.ColumnNames.ChargeQuantity, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemTemplate.LastCreateDateTime
		/// </summary>
		virtual public System.DateTime? LastCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesItemTemplateMetadata.ColumnNames.LastCreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesItemTemplateMetadata.ColumnNames.LastCreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemTemplate.LastCreateUserID
		/// </summary>
		virtual public System.String LastCreateUserID
		{
			get
			{
				return base.GetSystemString(TransChargesItemTemplateMetadata.ColumnNames.LastCreateUserID);
			}

			set
			{
				base.SetSystemString(TransChargesItemTemplateMetadata.ColumnNames.LastCreateUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesItemTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesItemTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesItemTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesItemTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTransChargesItemTemplate entity)
			{
				this.entity = entity;
			}
			public System.String TemplateNo
			{
				get
				{
					System.String data = entity.TemplateNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateNo = null;
					else entity.TemplateNo = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
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
			public System.String ChargeQuantity
			{
				get
				{
					System.Decimal? data = entity.ChargeQuantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChargeQuantity = null;
					else entity.ChargeQuantity = Convert.ToDecimal(value);
				}
			}
			public System.String LastCreateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
					else entity.LastCreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastCreateUserID
			{
				get
				{
					System.String data = entity.LastCreateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateUserID = null;
					else entity.LastCreateUserID = Convert.ToString(value);
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
			private esTransChargesItemTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesItemTemplateQuery query)
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
				throw new Exception("esTransChargesItemTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransChargesItemTemplate : esTransChargesItemTemplate
	{
	}

	[Serializable]
	abstract public class esTransChargesItemTemplateQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemTemplateMetadata.Meta();
			}
		}

		public esQueryItem TemplateNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.TemplateNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ChargeQuantity
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.ChargeQuantity, esSystemType.Decimal);
			}
		}

		public esQueryItem LastCreateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastCreateUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.LastCreateUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesItemTemplateCollection")]
	public partial class TransChargesItemTemplateCollection : esTransChargesItemTemplateCollection, IEnumerable<TransChargesItemTemplate>
	{
		public TransChargesItemTemplateCollection()
		{

		}

		public static implicit operator List<TransChargesItemTemplate>(TransChargesItemTemplateCollection coll)
		{
			List<TransChargesItemTemplate> list = new List<TransChargesItemTemplate>();

			foreach (TransChargesItemTemplate emp in coll)
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
				return TransChargesItemTemplateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesItemTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesItemTemplate();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransChargesItemTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemTemplateQuery();
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
		public bool Load(TransChargesItemTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransChargesItemTemplate AddNew()
		{
			TransChargesItemTemplate entity = base.AddNewEntity() as TransChargesItemTemplate;

			return entity;
		}
		public TransChargesItemTemplate FindByPrimaryKey(String templateNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(templateNo, sequenceNo) as TransChargesItemTemplate;
		}

		#region IEnumerable< TransChargesItemTemplate> Members

		IEnumerator<TransChargesItemTemplate> IEnumerable<TransChargesItemTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesItemTemplate;
			}
		}

		#endregion

		private TransChargesItemTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesItemTemplate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransChargesItemTemplate ({TemplateNo, SequenceNo})")]
	[Serializable]
	public partial class TransChargesItemTemplate : esTransChargesItemTemplate
	{
		public TransChargesItemTemplate()
		{
		}

		public TransChargesItemTemplate(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemTemplateMetadata.Meta();
			}
		}

		override protected esTransChargesItemTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransChargesItemTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemTemplateQuery();
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
		public bool Load(TransChargesItemTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransChargesItemTemplateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransChargesItemTemplateQuery : esTransChargesItemTemplateQuery
	{
		public TransChargesItemTemplateQuery()
		{

		}

		public TransChargesItemTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransChargesItemTemplateQuery";
		}
	}

	[Serializable]
	public partial class TransChargesItemTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesItemTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.TemplateNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.TemplateNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.ChargeQuantity, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.ChargeQuantity;
			c.NumericPrecision = 12;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.LastCreateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.LastCreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.LastCreateUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.LastCreateUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemTemplateMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransChargesItemTemplateMetadata Meta()
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
			public const string TemplateNo = "TemplateNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string ChargeQuantity = "ChargeQuantity";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateUserID = "LastCreateUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TemplateNo = "TemplateNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string ChargeQuantity = "ChargeQuantity";
			public const string LastCreateDateTime = "LastCreateDateTime";
			public const string LastCreateUserID = "LastCreateUserID";
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
			lock (typeof(TransChargesItemTemplateMetadata))
			{
				if (TransChargesItemTemplateMetadata.mapDelegates == null)
				{
					TransChargesItemTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransChargesItemTemplateMetadata.meta == null)
				{
					TransChargesItemTemplateMetadata.meta = new TransChargesItemTemplateMetadata();
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

				meta.AddTypeMap("TemplateNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChargeQuantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCreateUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "TransChargesItemTemplate";
				meta.Destination = "TransChargesItemTemplate";
				meta.spInsert = "proc_TransChargesItemTemplateInsert";
				meta.spUpdate = "proc_TransChargesItemTemplateUpdate";
				meta.spDelete = "proc_TransChargesItemTemplateDelete";
				meta.spLoadAll = "proc_TransChargesItemTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesItemTemplateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesItemTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
