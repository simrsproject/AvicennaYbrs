/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/2/2022 12:28:32 PM
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
	abstract public class esRegistrationGyssensLineCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationGyssensLineCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationGyssensLineCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationGyssensLineQuery query)
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
			this.InitQuery(query as esRegistrationGyssensLineQuery);
		}
		#endregion

		virtual public RegistrationGyssensLine DetachEntity(RegistrationGyssensLine entity)
		{
			return base.DetachEntity(entity) as RegistrationGyssensLine;
		}

		virtual public RegistrationGyssensLine AttachEntity(RegistrationGyssensLine entity)
		{
			return base.AttachEntity(entity) as RegistrationGyssensLine;
		}

		virtual public void Combine(RegistrationGyssensLineCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationGyssensLine this[int index]
		{
			get
			{
				return base[index] as RegistrationGyssensLine;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationGyssensLine);
		}
	}

	[Serializable]
	abstract public class esRegistrationGyssensLine : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationGyssensLineQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationGyssensLine()
		{
		}

		public esRegistrationGyssensLine(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo, rasproLineID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo, rasproLineID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 seqNo, String rasproLineID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo, rasproLineID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo, rasproLineID);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			esRegistrationGyssensLineQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo, query.RasproLineID == rasproLineID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("SeqNo", seqNo);
			parms.Add("RasproLineID", rasproLineID);
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
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "RasproLineID": this.str.RasproLineID = (string)value; break;
						case "Condition": this.str.Condition = (string)value; break;
						case "GyssensCategory": this.str.GyssensCategory = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SeqNo":

							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
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
		/// Maps to RegistrationGyssensLine.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensLineMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensLineMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssensLine.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationGyssensLineMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationGyssensLineMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssensLine.RasproLineID
		/// </summary>
		virtual public System.String RasproLineID
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensLineMetadata.ColumnNames.RasproLineID);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensLineMetadata.ColumnNames.RasproLineID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssensLine.Condition
		/// </summary>
		virtual public System.String Condition
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensLineMetadata.ColumnNames.Condition);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensLineMetadata.ColumnNames.Condition, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssensLine.GyssensCategory
		/// </summary>
		virtual public System.String GyssensCategory
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensLineMetadata.ColumnNames.GyssensCategory);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensLineMetadata.ColumnNames.GyssensCategory, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssensLine.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationGyssensLineMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationGyssensLineMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssensLine.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensLineMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensLineMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationGyssensLine entity)
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
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
				}
			}
			public System.String RasproLineID
			{
				get
				{
					System.String data = entity.RasproLineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasproLineID = null;
					else entity.RasproLineID = Convert.ToString(value);
				}
			}
			public System.String Condition
			{
				get
				{
					System.String data = entity.Condition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Condition = null;
					else entity.Condition = Convert.ToString(value);
				}
			}
			public System.String GyssensCategory
			{
				get
				{
					System.String data = entity.GyssensCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GyssensCategory = null;
					else entity.GyssensCategory = Convert.ToString(value);
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
			private esRegistrationGyssensLine entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationGyssensLineQuery query)
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
				throw new Exception("esRegistrationGyssensLine can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationGyssensLine : esRegistrationGyssensLine
	{
	}

	[Serializable]
	abstract public class esRegistrationGyssensLineQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGyssensLineMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensLineMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensLineMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem RasproLineID
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensLineMetadata.ColumnNames.RasproLineID, esSystemType.String);
			}
		}

		public esQueryItem Condition
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensLineMetadata.ColumnNames.Condition, esSystemType.String);
			}
		}

		public esQueryItem GyssensCategory
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensLineMetadata.ColumnNames.GyssensCategory, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationGyssensLineCollection")]
	public partial class RegistrationGyssensLineCollection : esRegistrationGyssensLineCollection, IEnumerable<RegistrationGyssensLine>
	{
		public RegistrationGyssensLineCollection()
		{

		}

		public static implicit operator List<RegistrationGyssensLine>(RegistrationGyssensLineCollection coll)
		{
			List<RegistrationGyssensLine> list = new List<RegistrationGyssensLine>();

			foreach (RegistrationGyssensLine emp in coll)
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
				return RegistrationGyssensLineMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGyssensLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationGyssensLine(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationGyssensLine();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationGyssensLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGyssensLineQuery();
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
		public bool Load(RegistrationGyssensLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationGyssensLine AddNew()
		{
			RegistrationGyssensLine entity = base.AddNewEntity() as RegistrationGyssensLine;

			return entity;
		}
		public RegistrationGyssensLine FindByPrimaryKey(String registrationNo, Int32 seqNo, String rasproLineID)
		{
			return base.FindByPrimaryKey(registrationNo, seqNo, rasproLineID) as RegistrationGyssensLine;
		}

		#region IEnumerable< RegistrationGyssensLine> Members

		IEnumerator<RegistrationGyssensLine> IEnumerable<RegistrationGyssensLine>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationGyssensLine;
			}
		}

		#endregion

		private RegistrationGyssensLineQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationGyssensLine' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationGyssensLine ({RegistrationNo, SeqNo, RasproLineID})")]
	[Serializable]
	public partial class RegistrationGyssensLine : esRegistrationGyssensLine
	{
		public RegistrationGyssensLine()
		{
		}

		public RegistrationGyssensLine(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGyssensLineMetadata.Meta();
			}
		}

		override protected esRegistrationGyssensLineQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGyssensLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationGyssensLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGyssensLineQuery();
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
		public bool Load(RegistrationGyssensLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationGyssensLineQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationGyssensLineQuery : esRegistrationGyssensLineQuery
	{
		public RegistrationGyssensLineQuery()
		{

		}

		public RegistrationGyssensLineQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationGyssensLineQuery";
		}
	}

	[Serializable]
	public partial class RegistrationGyssensLineMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationGyssensLineMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationGyssensLineMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensLineMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensLineMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationGyssensLineMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensLineMetadata.ColumnNames.RasproLineID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensLineMetadata.PropertyNames.RasproLineID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensLineMetadata.ColumnNames.Condition, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensLineMetadata.PropertyNames.Condition;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensLineMetadata.ColumnNames.GyssensCategory, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensLineMetadata.PropertyNames.GyssensCategory;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensLineMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationGyssensLineMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensLineMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensLineMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationGyssensLineMetadata Meta()
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
			public const string SeqNo = "SeqNo";
			public const string RasproLineID = "RasproLineID";
			public const string Condition = "Condition";
			public const string GyssensCategory = "GyssensCategory";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string RasproLineID = "RasproLineID";
			public const string Condition = "Condition";
			public const string GyssensCategory = "GyssensCategory";
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
			lock (typeof(RegistrationGyssensLineMetadata))
			{
				if (RegistrationGyssensLineMetadata.mapDelegates == null)
				{
					RegistrationGyssensLineMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationGyssensLineMetadata.meta == null)
				{
					RegistrationGyssensLineMetadata.meta = new RegistrationGyssensLineMetadata();
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
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RasproLineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Condition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GyssensCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "RegistrationGyssensLine";
				meta.Destination = "RegistrationGyssensLine";
				meta.spInsert = "proc_RegistrationGyssensLineInsert";
				meta.spUpdate = "proc_RegistrationGyssensLineUpdate";
				meta.spDelete = "proc_RegistrationGyssensLineDelete";
				meta.spLoadAll = "proc_RegistrationGyssensLineLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationGyssensLineLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationGyssensLineMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
