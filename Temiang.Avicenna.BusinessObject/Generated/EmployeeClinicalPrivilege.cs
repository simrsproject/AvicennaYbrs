/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/21/2022 2:20:00 PM
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
	abstract public class esEmployeeClinicalPrivilegeCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeClinicalPrivilegeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeClinicalPrivilegeCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeClinicalPrivilegeQuery query)
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
			this.InitQuery(query as esEmployeeClinicalPrivilegeQuery);
		}
		#endregion

		virtual public EmployeeClinicalPrivilege DetachEntity(EmployeeClinicalPrivilege entity)
		{
			return base.DetachEntity(entity) as EmployeeClinicalPrivilege;
		}

		virtual public EmployeeClinicalPrivilege AttachEntity(EmployeeClinicalPrivilege entity)
		{
			return base.AttachEntity(entity) as EmployeeClinicalPrivilege;
		}

		virtual public void Combine(EmployeeClinicalPrivilegeCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeClinicalPrivilege this[int index]
		{
			get
			{
				return base[index] as EmployeeClinicalPrivilege;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeClinicalPrivilege);
		}
	}

	[Serializable]
	abstract public class esEmployeeClinicalPrivilege : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeClinicalPrivilegeQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeClinicalPrivilege()
		{
		}

		public esEmployeeClinicalPrivilege(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeClinicalPrivilegeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeClinicalPrivilegeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeClinicalPrivilegeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeClinicalPrivilegeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeClinicalPrivilegeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeClinicalPrivilegeID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeClinicalPrivilegeID)
		{
			esEmployeeClinicalPrivilegeQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeClinicalPrivilegeID == employeeClinicalPrivilegeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeClinicalPrivilegeID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeClinicalPrivilegeID", employeeClinicalPrivilegeID);
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
						case "EmployeeClinicalPrivilegeID": this.str.EmployeeClinicalPrivilegeID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRProfessionGroup": this.str.SRProfessionGroup = (string)value; break;
						case "SRClinicalWorkArea": this.str.SRClinicalWorkArea = (string)value; break;
						case "SRClinicalAuthorityLevel": this.str.SRClinicalAuthorityLevel = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "DecreeNo": this.str.DecreeNo = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeClinicalPrivilegeID":

							if (value == null || value is System.Int32)
								this.EmployeeClinicalPrivilegeID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to EmployeeClinicalPrivilege.EmployeeClinicalPrivilegeID
		/// </summary>
		virtual public System.Int32? EmployeeClinicalPrivilegeID
		{
			get
			{
				return base.GetSystemInt32(EmployeeClinicalPrivilegeMetadata.ColumnNames.EmployeeClinicalPrivilegeID);
			}

			set
			{
				base.SetSystemInt32(EmployeeClinicalPrivilegeMetadata.ColumnNames.EmployeeClinicalPrivilegeID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeClinicalPrivilegeMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeClinicalPrivilegeMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.SRProfessionGroup
		/// </summary>
		virtual public System.String SRProfessionGroup
		{
			get
			{
				return base.GetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRProfessionGroup);
			}

			set
			{
				base.SetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRProfessionGroup, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.SRClinicalWorkArea
		/// </summary>
		virtual public System.String SRClinicalWorkArea
		{
			get
			{
				return base.GetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalWorkArea);
			}

			set
			{
				base.SetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalWorkArea, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.SRClinicalAuthorityLevel
		/// </summary>
		virtual public System.String SRClinicalAuthorityLevel
		{
			get
			{
				return base.GetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalAuthorityLevel);
			}

			set
			{
				base.SetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalAuthorityLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.DecreeNo
		/// </summary>
		virtual public System.String DecreeNo
		{
			get
			{
				return base.GetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.DecreeNo);
			}

			set
			{
				base.SetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.DecreeNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeClinicalPrivilege.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeClinicalPrivilege entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeClinicalPrivilegeID
			{
				get
				{
					System.Int32? data = entity.EmployeeClinicalPrivilegeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeClinicalPrivilegeID = null;
					else entity.EmployeeClinicalPrivilegeID = Convert.ToInt32(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String SRProfessionGroup
			{
				get
				{
					System.String data = entity.SRProfessionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProfessionGroup = null;
					else entity.SRProfessionGroup = Convert.ToString(value);
				}
			}
			public System.String SRClinicalWorkArea
			{
				get
				{
					System.String data = entity.SRClinicalWorkArea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalWorkArea = null;
					else entity.SRClinicalWorkArea = Convert.ToString(value);
				}
			}
			public System.String SRClinicalAuthorityLevel
			{
				get
				{
					System.String data = entity.SRClinicalAuthorityLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalAuthorityLevel = null;
					else entity.SRClinicalAuthorityLevel = Convert.ToString(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
				}
			}
			public System.String DecreeNo
			{
				get
				{
					System.String data = entity.DecreeNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecreeNo = null;
					else entity.DecreeNo = Convert.ToString(value);
				}
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
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
			private esEmployeeClinicalPrivilege entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeClinicalPrivilegeQuery query)
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
				throw new Exception("esEmployeeClinicalPrivilege can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeClinicalPrivilege : esEmployeeClinicalPrivilege
	{
	}

	[Serializable]
	abstract public class esEmployeeClinicalPrivilegeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeClinicalPrivilegeMetadata.Meta();
			}
		}

		public esQueryItem EmployeeClinicalPrivilegeID
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.EmployeeClinicalPrivilegeID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRProfessionGroup
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRProfessionGroup, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalWorkArea
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalWorkArea, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalAuthorityLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalAuthorityLevel, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem DecreeNo
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.DecreeNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeClinicalPrivilegeCollection")]
	public partial class EmployeeClinicalPrivilegeCollection : esEmployeeClinicalPrivilegeCollection, IEnumerable<EmployeeClinicalPrivilege>
	{
		public EmployeeClinicalPrivilegeCollection()
		{

		}

		public static implicit operator List<EmployeeClinicalPrivilege>(EmployeeClinicalPrivilegeCollection coll)
		{
			List<EmployeeClinicalPrivilege> list = new List<EmployeeClinicalPrivilege>();

			foreach (EmployeeClinicalPrivilege emp in coll)
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
				return EmployeeClinicalPrivilegeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeClinicalPrivilegeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeClinicalPrivilege(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeClinicalPrivilege();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeClinicalPrivilegeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeClinicalPrivilegeQuery();
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
		public bool Load(EmployeeClinicalPrivilegeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeClinicalPrivilege AddNew()
		{
			EmployeeClinicalPrivilege entity = base.AddNewEntity() as EmployeeClinicalPrivilege;

			return entity;
		}
		public EmployeeClinicalPrivilege FindByPrimaryKey(Int32 employeeClinicalPrivilegeID)
		{
			return base.FindByPrimaryKey(employeeClinicalPrivilegeID) as EmployeeClinicalPrivilege;
		}

		#region IEnumerable< EmployeeClinicalPrivilege> Members

		IEnumerator<EmployeeClinicalPrivilege> IEnumerable<EmployeeClinicalPrivilege>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeClinicalPrivilege;
			}
		}

		#endregion

		private EmployeeClinicalPrivilegeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeClinicalPrivilege' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeClinicalPrivilege ({EmployeeClinicalPrivilegeID})")]
	[Serializable]
	public partial class EmployeeClinicalPrivilege : esEmployeeClinicalPrivilege
	{
		public EmployeeClinicalPrivilege()
		{
		}

		public EmployeeClinicalPrivilege(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeClinicalPrivilegeMetadata.Meta();
			}
		}

		override protected esEmployeeClinicalPrivilegeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeClinicalPrivilegeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeClinicalPrivilegeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeClinicalPrivilegeQuery();
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
		public bool Load(EmployeeClinicalPrivilegeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeClinicalPrivilegeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeClinicalPrivilegeQuery : esEmployeeClinicalPrivilegeQuery
	{
		public EmployeeClinicalPrivilegeQuery()
		{

		}

		public EmployeeClinicalPrivilegeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeClinicalPrivilegeQuery";
		}
	}

	[Serializable]
	public partial class EmployeeClinicalPrivilegeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeClinicalPrivilegeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.EmployeeClinicalPrivilegeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.EmployeeClinicalPrivilegeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRProfessionGroup, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.SRProfessionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalWorkArea, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.SRClinicalWorkArea;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.SRClinicalAuthorityLevel, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.SRClinicalAuthorityLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidFrom, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.ValidTo, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.ValidTo;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.DecreeNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.DecreeNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.TransactionNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.Notes, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeClinicalPrivilegeMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeClinicalPrivilegeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeClinicalPrivilegeMetadata Meta()
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
			public const string EmployeeClinicalPrivilegeID = "EmployeeClinicalPrivilegeID";
			public const string PersonID = "PersonID";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string DecreeNo = "DecreeNo";
			public const string TransactionNo = "TransactionNo";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeClinicalPrivilegeID = "EmployeeClinicalPrivilegeID";
			public const string PersonID = "PersonID";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string DecreeNo = "DecreeNo";
			public const string TransactionNo = "TransactionNo";
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
			lock (typeof(EmployeeClinicalPrivilegeMetadata))
			{
				if (EmployeeClinicalPrivilegeMetadata.mapDelegates == null)
				{
					EmployeeClinicalPrivilegeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeClinicalPrivilegeMetadata.meta == null)
				{
					EmployeeClinicalPrivilegeMetadata.meta = new EmployeeClinicalPrivilegeMetadata();
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

				meta.AddTypeMap("EmployeeClinicalPrivilegeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRProfessionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalWorkArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalAuthorityLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DecreeNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeClinicalPrivilege";
				meta.Destination = "EmployeeClinicalPrivilege";
				meta.spInsert = "proc_EmployeeClinicalPrivilegeInsert";
				meta.spUpdate = "proc_EmployeeClinicalPrivilegeUpdate";
				meta.spDelete = "proc_EmployeeClinicalPrivilegeDelete";
				meta.spLoadAll = "proc_EmployeeClinicalPrivilegeLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeClinicalPrivilegeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeClinicalPrivilegeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
