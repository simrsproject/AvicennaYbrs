/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/28/2022 1:58:54 PM
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
	abstract public class esEmployeeTrainingExternalTrainerCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTrainingExternalTrainerCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTrainingExternalTrainerCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingExternalTrainerQuery query)
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
			this.InitQuery(query as esEmployeeTrainingExternalTrainerQuery);
		}
		#endregion

		virtual public EmployeeTrainingExternalTrainer DetachEntity(EmployeeTrainingExternalTrainer entity)
		{
			return base.DetachEntity(entity) as EmployeeTrainingExternalTrainer;
		}

		virtual public EmployeeTrainingExternalTrainer AttachEntity(EmployeeTrainingExternalTrainer entity)
		{
			return base.AttachEntity(entity) as EmployeeTrainingExternalTrainer;
		}

		virtual public void Combine(EmployeeTrainingExternalTrainerCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTrainingExternalTrainer this[int index]
		{
			get
			{
				return base[index] as EmployeeTrainingExternalTrainer;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTrainingExternalTrainer);
		}
	}

	[Serializable]
	abstract public class esEmployeeTrainingExternalTrainer : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTrainingExternalTrainerQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTrainingExternalTrainer()
		{
		}

		public esEmployeeTrainingExternalTrainer(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeTrainingID, String externalTrainerSeqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingID, externalTrainerSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingID, externalTrainerSeqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeTrainingID, String externalTrainerSeqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeTrainingID, externalTrainerSeqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeTrainingID, externalTrainerSeqNo);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeTrainingID, String externalTrainerSeqNo)
		{
			esEmployeeTrainingExternalTrainerQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeTrainingID == employeeTrainingID, query.ExternalTrainerSeqNo == externalTrainerSeqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeTrainingID, String externalTrainerSeqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeTrainingID", employeeTrainingID);
			parms.Add("ExternalTrainerSeqNo", externalTrainerSeqNo);
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
						case "EmployeeTrainingID": this.str.EmployeeTrainingID = (string)value; break;
						case "ExternalTrainerSeqNo": this.str.ExternalTrainerSeqNo = (string)value; break;
						case "ExternalTrainerName": this.str.ExternalTrainerName = (string)value; break;
						case "PositionAs": this.str.PositionAs = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeTrainingID":

							if (value == null || value is System.Int32)
								this.EmployeeTrainingID = (System.Int32?)value;
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
		/// Maps to EmployeeTrainingExternalTrainer.EmployeeTrainingID
		/// </summary>
		virtual public System.Int32? EmployeeTrainingID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTrainingExternalTrainerMetadata.ColumnNames.EmployeeTrainingID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTrainingExternalTrainerMetadata.ColumnNames.EmployeeTrainingID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingExternalTrainer.ExternalTrainerSeqNo
		/// </summary>
		virtual public System.String ExternalTrainerSeqNo
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerSeqNo);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingExternalTrainer.ExternalTrainerName
		/// </summary>
		virtual public System.String ExternalTrainerName
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerName);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingExternalTrainer.PositionAs
		/// </summary>
		virtual public System.String PositionAs
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.PositionAs);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.PositionAs, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingExternalTrainer.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingExternalTrainer.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTrainingExternalTrainer.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeTrainingExternalTrainer entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeTrainingID
			{
				get
				{
					System.Int32? data = entity.EmployeeTrainingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeTrainingID = null;
					else entity.EmployeeTrainingID = Convert.ToInt32(value);
				}
			}
			public System.String ExternalTrainerSeqNo
			{
				get
				{
					System.String data = entity.ExternalTrainerSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExternalTrainerSeqNo = null;
					else entity.ExternalTrainerSeqNo = Convert.ToString(value);
				}
			}
			public System.String ExternalTrainerName
			{
				get
				{
					System.String data = entity.ExternalTrainerName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExternalTrainerName = null;
					else entity.ExternalTrainerName = Convert.ToString(value);
				}
			}
			public System.String PositionAs
			{
				get
				{
					System.String data = entity.PositionAs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionAs = null;
					else entity.PositionAs = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			private esEmployeeTrainingExternalTrainer entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTrainingExternalTrainerQuery query)
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
				throw new Exception("esEmployeeTrainingExternalTrainer can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTrainingExternalTrainer : esEmployeeTrainingExternalTrainer
	{
	}

	[Serializable]
	abstract public class esEmployeeTrainingExternalTrainerQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingExternalTrainerMetadata.Meta();
			}
		}

		public esQueryItem EmployeeTrainingID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingExternalTrainerMetadata.ColumnNames.EmployeeTrainingID, esSystemType.Int32);
			}
		}

		public esQueryItem ExternalTrainerSeqNo
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerSeqNo, esSystemType.String);
			}
		}

		public esQueryItem ExternalTrainerName
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerName, esSystemType.String);
			}
		}

		public esQueryItem PositionAs
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingExternalTrainerMetadata.ColumnNames.PositionAs, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingExternalTrainerMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTrainingExternalTrainerCollection")]
	public partial class EmployeeTrainingExternalTrainerCollection : esEmployeeTrainingExternalTrainerCollection, IEnumerable<EmployeeTrainingExternalTrainer>
	{
		public EmployeeTrainingExternalTrainerCollection()
		{

		}

		public static implicit operator List<EmployeeTrainingExternalTrainer>(EmployeeTrainingExternalTrainerCollection coll)
		{
			List<EmployeeTrainingExternalTrainer> list = new List<EmployeeTrainingExternalTrainer>();

			foreach (EmployeeTrainingExternalTrainer emp in coll)
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
				return EmployeeTrainingExternalTrainerMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingExternalTrainerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTrainingExternalTrainer(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTrainingExternalTrainer();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingExternalTrainerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingExternalTrainerQuery();
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
		public bool Load(EmployeeTrainingExternalTrainerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTrainingExternalTrainer AddNew()
		{
			EmployeeTrainingExternalTrainer entity = base.AddNewEntity() as EmployeeTrainingExternalTrainer;

			return entity;
		}
		public EmployeeTrainingExternalTrainer FindByPrimaryKey(Int32 employeeTrainingID, String externalTrainerSeqNo)
		{
			return base.FindByPrimaryKey(employeeTrainingID, externalTrainerSeqNo) as EmployeeTrainingExternalTrainer;
		}

		#region IEnumerable< EmployeeTrainingExternalTrainer> Members

		IEnumerator<EmployeeTrainingExternalTrainer> IEnumerable<EmployeeTrainingExternalTrainer>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTrainingExternalTrainer;
			}
		}

		#endregion

		private EmployeeTrainingExternalTrainerQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTrainingExternalTrainer' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTrainingExternalTrainer ({EmployeeTrainingID, ExternalTrainerSeqNo})")]
	[Serializable]
	public partial class EmployeeTrainingExternalTrainer : esEmployeeTrainingExternalTrainer
	{
		public EmployeeTrainingExternalTrainer()
		{
		}

		public EmployeeTrainingExternalTrainer(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTrainingExternalTrainerMetadata.Meta();
			}
		}

		override protected esEmployeeTrainingExternalTrainerQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTrainingExternalTrainerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTrainingExternalTrainerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTrainingExternalTrainerQuery();
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
		public bool Load(EmployeeTrainingExternalTrainerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTrainingExternalTrainerQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTrainingExternalTrainerQuery : esEmployeeTrainingExternalTrainerQuery
	{
		public EmployeeTrainingExternalTrainerQuery()
		{

		}

		public EmployeeTrainingExternalTrainerQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTrainingExternalTrainerQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTrainingExternalTrainerMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTrainingExternalTrainerMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTrainingExternalTrainerMetadata.ColumnNames.EmployeeTrainingID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTrainingExternalTrainerMetadata.PropertyNames.EmployeeTrainingID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerSeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingExternalTrainerMetadata.PropertyNames.ExternalTrainerSeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingExternalTrainerMetadata.ColumnNames.ExternalTrainerName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingExternalTrainerMetadata.PropertyNames.ExternalTrainerName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingExternalTrainerMetadata.ColumnNames.PositionAs, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingExternalTrainerMetadata.PropertyNames.PositionAs;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingExternalTrainerMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingExternalTrainerMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTrainingExternalTrainerMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTrainingExternalTrainerMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTrainingExternalTrainerMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeTrainingExternalTrainerMetadata Meta()
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
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string ExternalTrainerSeqNo = "ExternalTrainerSeqNo";
			public const string ExternalTrainerName = "ExternalTrainerName";
			public const string PositionAs = "PositionAs";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeTrainingID = "EmployeeTrainingID";
			public const string ExternalTrainerSeqNo = "ExternalTrainerSeqNo";
			public const string ExternalTrainerName = "ExternalTrainerName";
			public const string PositionAs = "PositionAs";
			public const string Notes = "Notes";
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
			lock (typeof(EmployeeTrainingExternalTrainerMetadata))
			{
				if (EmployeeTrainingExternalTrainerMetadata.mapDelegates == null)
				{
					EmployeeTrainingExternalTrainerMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTrainingExternalTrainerMetadata.meta == null)
				{
					EmployeeTrainingExternalTrainerMetadata.meta = new EmployeeTrainingExternalTrainerMetadata();
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

				meta.AddTypeMap("EmployeeTrainingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ExternalTrainerSeqNo", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ExternalTrainerName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionAs", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeTrainingExternalTrainer";
				meta.Destination = "EmployeeTrainingExternalTrainer";
				meta.spInsert = "proc_EmployeeTrainingExternalTrainerInsert";
				meta.spUpdate = "proc_EmployeeTrainingExternalTrainerUpdate";
				meta.spDelete = "proc_EmployeeTrainingExternalTrainerDelete";
				meta.spLoadAll = "proc_EmployeeTrainingExternalTrainerLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTrainingExternalTrainerLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTrainingExternalTrainerMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
