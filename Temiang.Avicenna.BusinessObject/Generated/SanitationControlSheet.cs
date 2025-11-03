/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/20/2022 5:24:00 PM
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
	abstract public class esSanitationControlSheetCollection : esEntityCollectionWAuditLog
	{
		public esSanitationControlSheetCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationControlSheetCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationControlSheetQuery query)
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
			this.InitQuery(query as esSanitationControlSheetQuery);
		}
		#endregion

		virtual public SanitationControlSheet DetachEntity(SanitationControlSheet entity)
		{
			return base.DetachEntity(entity) as SanitationControlSheet;
		}

		virtual public SanitationControlSheet AttachEntity(SanitationControlSheet entity)
		{
			return base.AttachEntity(entity) as SanitationControlSheet;
		}

		virtual public void Combine(SanitationControlSheetCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationControlSheet this[int index]
		{
			get
			{
				return base[index] as SanitationControlSheet;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationControlSheet);
		}
	}

	[Serializable]
	abstract public class esSanitationControlSheet : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationControlSheetQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationControlSheet()
		{
		}

		public esSanitationControlSheet(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String controlSheetNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(controlSheetNo);
			else
				return LoadByPrimaryKeyStoredProcedure(controlSheetNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String controlSheetNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(controlSheetNo);
			else
				return LoadByPrimaryKeyStoredProcedure(controlSheetNo);
		}

		private bool LoadByPrimaryKeyDynamic(String controlSheetNo)
		{
			esSanitationControlSheetQuery query = this.GetDynamicQuery();
			query.Where(query.ControlSheetNo == controlSheetNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String controlSheetNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ControlSheetNo", controlSheetNo);
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
						case "ControlSheetNo": this.str.ControlSheetNo = (string)value; break;
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "ControlDate": this.str.ControlDate = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ControlDate":

							if (value == null || value is System.DateTime)
								this.ControlDate = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
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
		/// Maps to SanitationControlSheet.ControlSheetNo
		/// </summary>
		virtual public System.String ControlSheetNo
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetMetadata.ColumnNames.ControlSheetNo);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetMetadata.ColumnNames.ControlSheetNo, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetMetadata.ColumnNames.QuestionFormID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.ControlDate
		/// </summary>
		virtual public System.DateTime? ControlDate
		{
			get
			{
				return base.GetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.ControlDate);
			}

			set
			{
				base.SetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.ControlDate, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(SanitationControlSheetMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(SanitationControlSheetMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(SanitationControlSheetMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(SanitationControlSheetMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationControlSheetMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheet.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationControlSheet entity)
			{
				this.entity = entity;
			}
			public System.String ControlSheetNo
			{
				get
				{
					System.String data = entity.ControlSheetNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControlSheetNo = null;
					else entity.ControlSheetNo = Convert.ToString(value);
				}
			}
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
				}
			}
			public System.String ControlDate
			{
				get
				{
					System.DateTime? data = entity.ControlDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControlDate = null;
					else entity.ControlDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			private esSanitationControlSheet entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationControlSheetQuery query)
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
				throw new Exception("esSanitationControlSheet can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationControlSheet : esSanitationControlSheet
	{
	}

	[Serializable]
	abstract public class esSanitationControlSheetQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationControlSheetMetadata.Meta();
			}
		}

		public esQueryItem ControlSheetNo
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.ControlSheetNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		}

		public esQueryItem ControlDate
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.ControlDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationControlSheetCollection")]
	public partial class SanitationControlSheetCollection : esSanitationControlSheetCollection, IEnumerable<SanitationControlSheet>
	{
		public SanitationControlSheetCollection()
		{

		}

		public static implicit operator List<SanitationControlSheet>(SanitationControlSheetCollection coll)
		{
			List<SanitationControlSheet> list = new List<SanitationControlSheet>();

			foreach (SanitationControlSheet emp in coll)
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
				return SanitationControlSheetMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationControlSheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationControlSheet(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationControlSheet();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationControlSheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationControlSheetQuery();
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
		public bool Load(SanitationControlSheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationControlSheet AddNew()
		{
			SanitationControlSheet entity = base.AddNewEntity() as SanitationControlSheet;

			return entity;
		}
		public SanitationControlSheet FindByPrimaryKey(String controlSheetNo)
		{
			return base.FindByPrimaryKey(controlSheetNo) as SanitationControlSheet;
		}

		#region IEnumerable< SanitationControlSheet> Members

		IEnumerator<SanitationControlSheet> IEnumerable<SanitationControlSheet>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationControlSheet;
			}
		}

		#endregion

		private SanitationControlSheetQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationControlSheet' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationControlSheet ({ControlSheetNo})")]
	[Serializable]
	public partial class SanitationControlSheet : esSanitationControlSheet
	{
		public SanitationControlSheet()
		{
		}

		public SanitationControlSheet(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationControlSheetMetadata.Meta();
			}
		}

		override protected esSanitationControlSheetQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationControlSheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationControlSheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationControlSheetQuery();
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
		public bool Load(SanitationControlSheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationControlSheetQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationControlSheetQuery : esSanitationControlSheetQuery
	{
		public SanitationControlSheetQuery()
		{

		}

		public SanitationControlSheetQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationControlSheetQuery";
		}
	}

	[Serializable]
	public partial class SanitationControlSheetMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationControlSheetMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.ControlSheetNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.ControlSheetNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.QuestionFormID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.ControlDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.ControlDate;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.IsApproved, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.ApprovedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.ApprovedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.VoidDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.VoidByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationControlSheetMetadata Meta()
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
			public const string ControlSheetNo = "ControlSheetNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string ControlDate = "ControlDate";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ControlSheetNo = "ControlSheetNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string ControlDate = "ControlDate";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
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
			lock (typeof(SanitationControlSheetMetadata))
			{
				if (SanitationControlSheetMetadata.mapDelegates == null)
				{
					SanitationControlSheetMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationControlSheetMetadata.meta == null)
				{
					SanitationControlSheetMetadata.meta = new SanitationControlSheetMetadata();
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

				meta.AddTypeMap("ControlSheetNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ControlDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationControlSheet";
				meta.Destination = "SanitationControlSheet";
				meta.spInsert = "proc_SanitationControlSheetInsert";
				meta.spUpdate = "proc_SanitationControlSheetUpdate";
				meta.spDelete = "proc_SanitationControlSheetDelete";
				meta.spLoadAll = "proc_SanitationControlSheetLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationControlSheetLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationControlSheetMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
