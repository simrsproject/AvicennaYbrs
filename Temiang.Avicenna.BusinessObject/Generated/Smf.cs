/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/7/2022 2:46:50 PM
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
	abstract public class esSmfCollection : esEntityCollectionWAuditLog
	{
		public esSmfCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SmfCollection";
		}

		#region Query Logic
		protected void InitQuery(esSmfQuery query)
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
			this.InitQuery(query as esSmfQuery);
		}
		#endregion

		virtual public Smf DetachEntity(Smf entity)
		{
			return base.DetachEntity(entity) as Smf;
		}

		virtual public Smf AttachEntity(Smf entity)
		{
			return base.AttachEntity(entity) as Smf;
		}

		virtual public void Combine(SmfCollection collection)
		{
			base.Combine(collection);
		}

		new public Smf this[int index]
		{
			get
			{
				return base[index] as Smf;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Smf);
		}
	}

	[Serializable]
	abstract public class esSmf : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSmfQuery GetDynamicQuery()
		{
			return null;
		}

		public esSmf()
		{
		}

		public esSmf(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String smfID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(smfID);
			else
				return LoadByPrimaryKeyStoredProcedure(smfID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String smfID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(smfID);
			else
				return LoadByPrimaryKeyStoredProcedure(smfID);
		}

		private bool LoadByPrimaryKeyDynamic(String smfID)
		{
			esSmfQuery query = this.GetDynamicQuery();
			query.Where(query.SmfID == smfID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String smfID)
		{
			esParameters parms = new esParameters();
			parms.Add("SmfID", smfID);
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
						case "SmfID": this.str.SmfID = (string)value; break;
						case "SmfName": this.str.SmfName = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRParamedicFeeCaseType": this.str.SRParamedicFeeCaseType = (string)value; break;
						case "SRAssessmentType": this.str.SRAssessmentType = (string)value; break;
						case "SmfBackcolor": this.str.SmfBackcolor = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to Smf.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(SmfMetadata.ColumnNames.SmfID);
			}

			set
			{
				base.SetSystemString(SmfMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to Smf.SmfName
		/// </summary>
		virtual public System.String SmfName
		{
			get
			{
				return base.GetSystemString(SmfMetadata.ColumnNames.SmfName);
			}

			set
			{
				base.SetSystemString(SmfMetadata.ColumnNames.SmfName, value);
			}
		}
		/// <summary>
		/// Maps to Smf.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SmfMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SmfMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Smf.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SmfMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SmfMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Smf.SRParamedicFeeCaseType
		/// </summary>
		virtual public System.String SRParamedicFeeCaseType
		{
			get
			{
				return base.GetSystemString(SmfMetadata.ColumnNames.SRParamedicFeeCaseType);
			}

			set
			{
				base.SetSystemString(SmfMetadata.ColumnNames.SRParamedicFeeCaseType, value);
			}
		}
		/// <summary>
		/// Maps to Smf.SRAssessmentType
		/// </summary>
		virtual public System.String SRAssessmentType
		{
			get
			{
				return base.GetSystemString(SmfMetadata.ColumnNames.SRAssessmentType);
			}

			set
			{
				base.SetSystemString(SmfMetadata.ColumnNames.SRAssessmentType, value);
			}
		}
		/// <summary>
		/// Maps to Smf.SmfBackcolor
		/// </summary>
		virtual public System.String SmfBackcolor
		{
			get
			{
				return base.GetSystemString(SmfMetadata.ColumnNames.SmfBackcolor);
			}

			set
			{
				base.SetSystemString(SmfMetadata.ColumnNames.SmfBackcolor, value);
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
			public esStrings(esSmf entity)
			{
				this.entity = entity;
			}
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
			public System.String SmfName
			{
				get
				{
					System.String data = entity.SmfName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfName = null;
					else entity.SmfName = Convert.ToString(value);
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
			public System.String SRParamedicFeeCaseType
			{
				get
				{
					System.String data = entity.SRParamedicFeeCaseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeCaseType = null;
					else entity.SRParamedicFeeCaseType = Convert.ToString(value);
				}
			}
			public System.String SRAssessmentType
			{
				get
				{
					System.String data = entity.SRAssessmentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssessmentType = null;
					else entity.SRAssessmentType = Convert.ToString(value);
				}
			}
			public System.String SmfBackcolor
			{
				get
				{
					System.String data = entity.SmfBackcolor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfBackcolor = null;
					else entity.SmfBackcolor = Convert.ToString(value);
				}
			}
			private esSmf entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSmfQuery query)
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
				throw new Exception("esSmf can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Smf : esSmf
	{
	}

	[Serializable]
	abstract public class esSmfQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SmfMetadata.Meta();
			}
		}

		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, SmfMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		}

		public esQueryItem SmfName
		{
			get
			{
				return new esQueryItem(this, SmfMetadata.ColumnNames.SmfName, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SmfMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SmfMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRParamedicFeeCaseType
		{
			get
			{
				return new esQueryItem(this, SmfMetadata.ColumnNames.SRParamedicFeeCaseType, esSystemType.String);
			}
		}

		public esQueryItem SRAssessmentType
		{
			get
			{
				return new esQueryItem(this, SmfMetadata.ColumnNames.SRAssessmentType, esSystemType.String);
			}
		}

		public esQueryItem SmfBackcolor
		{
			get
			{
				return new esQueryItem(this, SmfMetadata.ColumnNames.SmfBackcolor, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SmfCollection")]
	public partial class SmfCollection : esSmfCollection, IEnumerable<Smf>
	{
		public SmfCollection()
		{

		}

		public static implicit operator List<Smf>(SmfCollection coll)
		{
			List<Smf> list = new List<Smf>();

			foreach (Smf emp in coll)
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
				return SmfMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SmfQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Smf(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Smf();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SmfQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SmfQuery();
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
		public bool Load(SmfQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Smf AddNew()
		{
			Smf entity = base.AddNewEntity() as Smf;

			return entity;
		}
		public Smf FindByPrimaryKey(String smfID)
		{
			return base.FindByPrimaryKey(smfID) as Smf;
		}

		#region IEnumerable< Smf> Members

		IEnumerator<Smf> IEnumerable<Smf>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Smf;
			}
		}

		#endregion

		private SmfQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Smf' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Smf ({SmfID})")]
	[Serializable]
	public partial class Smf : esSmf
	{
		public Smf()
		{
		}

		public Smf(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SmfMetadata.Meta();
			}
		}

		override protected esSmfQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SmfQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SmfQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SmfQuery();
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
		public bool Load(SmfQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SmfQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SmfQuery : esSmfQuery
	{
		public SmfQuery()
		{

		}

		public SmfQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SmfQuery";
		}
	}

	[Serializable]
	public partial class SmfMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SmfMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SmfMetadata.ColumnNames.SmfID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfMetadata.PropertyNames.SmfID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SmfMetadata.ColumnNames.SmfName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfMetadata.PropertyNames.SmfName;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(SmfMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SmfMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SmfMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SmfMetadata.ColumnNames.SRParamedicFeeCaseType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfMetadata.PropertyNames.SRParamedicFeeCaseType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SmfMetadata.ColumnNames.SRAssessmentType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfMetadata.PropertyNames.SRAssessmentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SmfMetadata.ColumnNames.SmfBackcolor, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SmfMetadata.PropertyNames.SmfBackcolor;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SmfMetadata Meta()
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
			public const string SmfID = "SmfID";
			public const string SmfName = "SmfName";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			public const string SRAssessmentType = "SRAssessmentType";
			public const string SmfBackcolor = "SmfBackcolor";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SmfID = "SmfID";
			public const string SmfName = "SmfName";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRParamedicFeeCaseType = "SRParamedicFeeCaseType";
			public const string SRAssessmentType = "SRAssessmentType";
			public const string SmfBackcolor = "SmfBackcolor";
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
			lock (typeof(SmfMetadata))
			{
				if (SmfMetadata.mapDelegates == null)
				{
					SmfMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SmfMetadata.meta == null)
				{
					SmfMetadata.meta = new SmfMetadata();
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

				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmfName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeCaseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAssessmentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmfBackcolor", new esTypeMap("varchar", "System.String"));


				meta.Source = "Smf";
				meta.Destination = "Smf";
				meta.spInsert = "proc_SmfInsert";
				meta.spUpdate = "proc_SmfUpdate";
				meta.spDelete = "proc_SmfDelete";
				meta.spLoadAll = "proc_SmfLoadAll";
				meta.spLoadByPrimaryKey = "proc_SmfLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SmfMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
