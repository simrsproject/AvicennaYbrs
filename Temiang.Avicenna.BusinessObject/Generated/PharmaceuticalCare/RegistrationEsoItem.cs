/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/21/2022 3:17:43 PM
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
	abstract public class esRegistrationEsoItemCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationEsoItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationEsoItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationEsoItemQuery query)
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
			this.InitQuery(query as esRegistrationEsoItemQuery);
		}
		#endregion

		virtual public RegistrationEsoItem DetachEntity(RegistrationEsoItem entity)
		{
			return base.DetachEntity(entity) as RegistrationEsoItem;
		}

		virtual public RegistrationEsoItem AttachEntity(RegistrationEsoItem entity)
		{
			return base.AttachEntity(entity) as RegistrationEsoItem;
		}

		virtual public void Combine(RegistrationEsoItemCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationEsoItem this[int index]
		{
			get
			{
				return base[index] as RegistrationEsoItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationEsoItem);
		}
	}

	[Serializable]
	abstract public class esRegistrationEsoItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationEsoItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationEsoItem()
		{
		}

		public esRegistrationEsoItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 esoNo, Int64 medicationReceiveNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, esoNo, medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, esoNo, medicationReceiveNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 esoNo, Int64 medicationReceiveNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, esoNo, medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, esoNo, medicationReceiveNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 esoNo, Int64 medicationReceiveNo)
		{
			esRegistrationEsoItemQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.EsoNo == esoNo, query.MedicationReceiveNo == medicationReceiveNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 esoNo, Int64 medicationReceiveNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("EsoNo", esoNo);
			parms.Add("MedicationReceiveNo", medicationReceiveNo);
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
						case "EsoNo": this.str.EsoNo = (string)value; break;
						case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
						case "IsSuspect": this.str.IsSuspect = (string)value; break;
						case "StartConsumeDateTime": this.str.StartConsumeDateTime = (string)value; break;
						case "EndConsumeDateTime": this.str.EndConsumeDateTime = (string)value; break;
						case "ConsumeIndication": this.str.ConsumeIndication = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EsoNo":

							if (value == null || value is System.Int32)
								this.EsoNo = (System.Int32?)value;
							break;
						case "MedicationReceiveNo":

							if (value == null || value is System.Int64)
								this.MedicationReceiveNo = (System.Int64?)value;
							break;
						case "IsSuspect":

							if (value == null || value is System.Boolean)
								this.IsSuspect = (System.Boolean?)value;
							break;
						case "StartConsumeDateTime":

							if (value == null || value is System.DateTime)
								this.StartConsumeDateTime = (System.DateTime?)value;
							break;
						case "EndConsumeDateTime":

							if (value == null || value is System.DateTime)
								this.EndConsumeDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationEsoItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationEsoItemMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationEsoItemMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.EsoNo
		/// </summary>
		virtual public System.Int32? EsoNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationEsoItemMetadata.ColumnNames.EsoNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationEsoItemMetadata.ColumnNames.EsoNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(RegistrationEsoItemMetadata.ColumnNames.MedicationReceiveNo);
			}

			set
			{
				base.SetSystemInt64(RegistrationEsoItemMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.IsSuspect
		/// </summary>
		virtual public System.Boolean? IsSuspect
		{
			get
			{
				return base.GetSystemBoolean(RegistrationEsoItemMetadata.ColumnNames.IsSuspect);
			}

			set
			{
				base.SetSystemBoolean(RegistrationEsoItemMetadata.ColumnNames.IsSuspect, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.StartConsumeDateTime
		/// </summary>
		virtual public System.DateTime? StartConsumeDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoItemMetadata.ColumnNames.StartConsumeDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoItemMetadata.ColumnNames.StartConsumeDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.EndConsumeDateTime
		/// </summary>
		virtual public System.DateTime? EndConsumeDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoItemMetadata.ColumnNames.EndConsumeDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoItemMetadata.ColumnNames.EndConsumeDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.ConsumeIndication
		/// </summary>
		virtual public System.String ConsumeIndication
		{
			get
			{
				return base.GetSystemString(RegistrationEsoItemMetadata.ColumnNames.ConsumeIndication);
			}

			set
			{
				base.SetSystemString(RegistrationEsoItemMetadata.ColumnNames.ConsumeIndication, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationEsoItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationEsoItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationEsoItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationEsoItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationEsoItemMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRegistrationEsoItem entity)
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
			public System.String EsoNo
			{
				get
				{
					System.Int32? data = entity.EsoNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EsoNo = null;
					else entity.EsoNo = Convert.ToInt32(value);
				}
			}
			public System.String MedicationReceiveNo
			{
				get
				{
					System.Int64? data = entity.MedicationReceiveNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicationReceiveNo = null;
					else entity.MedicationReceiveNo = Convert.ToInt64(value);
				}
			}
			public System.String IsSuspect
			{
				get
				{
					System.Boolean? data = entity.IsSuspect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSuspect = null;
					else entity.IsSuspect = Convert.ToBoolean(value);
				}
			}
			public System.String StartConsumeDateTime
			{
				get
				{
					System.DateTime? data = entity.StartConsumeDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartConsumeDateTime = null;
					else entity.StartConsumeDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String EndConsumeDateTime
			{
				get
				{
					System.DateTime? data = entity.EndConsumeDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndConsumeDateTime = null;
					else entity.EndConsumeDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ConsumeIndication
			{
				get
				{
					System.String data = entity.ConsumeIndication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeIndication = null;
					else entity.ConsumeIndication = Convert.ToString(value);
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
			private esRegistrationEsoItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationEsoItemQuery query)
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
				throw new Exception("esRegistrationEsoItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationEsoItem : esRegistrationEsoItem
	{
	}

	[Serializable]
	abstract public class esRegistrationEsoItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationEsoItemMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem EsoNo
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.EsoNo, esSystemType.Int32);
			}
		}

		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		}

		public esQueryItem IsSuspect
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.IsSuspect, esSystemType.Boolean);
			}
		}

		public esQueryItem StartConsumeDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.StartConsumeDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem EndConsumeDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.EndConsumeDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ConsumeIndication
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.ConsumeIndication, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationEsoItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationEsoItemCollection")]
	public partial class RegistrationEsoItemCollection : esRegistrationEsoItemCollection, IEnumerable<RegistrationEsoItem>
	{
		public RegistrationEsoItemCollection()
		{

		}

		public static implicit operator List<RegistrationEsoItem>(RegistrationEsoItemCollection coll)
		{
			List<RegistrationEsoItem> list = new List<RegistrationEsoItem>();

			foreach (RegistrationEsoItem emp in coll)
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
				return RegistrationEsoItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationEsoItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationEsoItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationEsoItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationEsoItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationEsoItemQuery();
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
		public bool Load(RegistrationEsoItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationEsoItem AddNew()
		{
			RegistrationEsoItem entity = base.AddNewEntity() as RegistrationEsoItem;

			return entity;
		}
		public RegistrationEsoItem FindByPrimaryKey(String registrationNo, Int32 esoNo, Int64 medicationReceiveNo)
		{
			return base.FindByPrimaryKey(registrationNo, esoNo, medicationReceiveNo) as RegistrationEsoItem;
		}

		#region IEnumerable< RegistrationEsoItem> Members

		IEnumerator<RegistrationEsoItem> IEnumerable<RegistrationEsoItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationEsoItem;
			}
		}

		#endregion

		private RegistrationEsoItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationEsoItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationEsoItem ({RegistrationNo, EsoNo, MedicationReceiveNo})")]
	[Serializable]
	public partial class RegistrationEsoItem : esRegistrationEsoItem
	{
		public RegistrationEsoItem()
		{
		}

		public RegistrationEsoItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationEsoItemMetadata.Meta();
			}
		}

		override protected esRegistrationEsoItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationEsoItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationEsoItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationEsoItemQuery();
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
		public bool Load(RegistrationEsoItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationEsoItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationEsoItemQuery : esRegistrationEsoItemQuery
	{
		public RegistrationEsoItemQuery()
		{

		}

		public RegistrationEsoItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationEsoItemQuery";
		}
	}

	[Serializable]
	public partial class RegistrationEsoItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationEsoItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.EsoNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.EsoNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.MedicationReceiveNo, 2, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.MedicationReceiveNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.IsSuspect, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.IsSuspect;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.StartConsumeDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.StartConsumeDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.EndConsumeDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.EndConsumeDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.ConsumeIndication, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.ConsumeIndication;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationEsoItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationEsoItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationEsoItemMetadata Meta()
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
			public const string EsoNo = "EsoNo";
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string IsSuspect = "IsSuspect";
			public const string StartConsumeDateTime = "StartConsumeDateTime";
			public const string EndConsumeDateTime = "EndConsumeDateTime";
			public const string ConsumeIndication = "ConsumeIndication";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string EsoNo = "EsoNo";
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string IsSuspect = "IsSuspect";
			public const string StartConsumeDateTime = "StartConsumeDateTime";
			public const string EndConsumeDateTime = "EndConsumeDateTime";
			public const string ConsumeIndication = "ConsumeIndication";
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
			lock (typeof(RegistrationEsoItemMetadata))
			{
				if (RegistrationEsoItemMetadata.mapDelegates == null)
				{
					RegistrationEsoItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationEsoItemMetadata.meta == null)
				{
					RegistrationEsoItemMetadata.meta = new RegistrationEsoItemMetadata();
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
				meta.AddTypeMap("EsoNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("IsSuspect", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("StartConsumeDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndConsumeDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ConsumeIndication", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "RegistrationEsoItem";
				meta.Destination = "RegistrationEsoItem";
				meta.spInsert = "proc_RegistrationEsoItemInsert";
				meta.spUpdate = "proc_RegistrationEsoItemUpdate";
				meta.spDelete = "proc_RegistrationEsoItemDelete";
				meta.spLoadAll = "proc_RegistrationEsoItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationEsoItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationEsoItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
