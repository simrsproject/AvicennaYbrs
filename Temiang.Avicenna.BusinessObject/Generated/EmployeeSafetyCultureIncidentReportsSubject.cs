/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/21/2022 11:51:20 AM
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
	abstract public class esEmployeeSafetyCultureIncidentReportsSubjectCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSafetyCultureIncidentReportsSubjectCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeSafetyCultureIncidentReportsSubjectCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsSubjectQuery query)
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
			this.InitQuery(query as esEmployeeSafetyCultureIncidentReportsSubjectQuery);
		}
		#endregion

		virtual public EmployeeSafetyCultureIncidentReportsSubject DetachEntity(EmployeeSafetyCultureIncidentReportsSubject entity)
		{
			return base.DetachEntity(entity) as EmployeeSafetyCultureIncidentReportsSubject;
		}

		virtual public EmployeeSafetyCultureIncidentReportsSubject AttachEntity(EmployeeSafetyCultureIncidentReportsSubject entity)
		{
			return base.AttachEntity(entity) as EmployeeSafetyCultureIncidentReportsSubject;
		}

		virtual public void Combine(EmployeeSafetyCultureIncidentReportsSubjectCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeSafetyCultureIncidentReportsSubject this[int index]
		{
			get
			{
				return base[index] as EmployeeSafetyCultureIncidentReportsSubject;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSafetyCultureIncidentReportsSubject);
		}
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsSubject : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSafetyCultureIncidentReportsSubjectQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSafetyCultureIncidentReportsSubject()
		{
		}

		public esEmployeeSafetyCultureIncidentReportsSubject(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, Int32 subjectPersonID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, subjectPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, subjectPersonID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 subjectPersonID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, subjectPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, subjectPersonID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 subjectPersonID)
		{
			esEmployeeSafetyCultureIncidentReportsSubjectQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SubjectPersonID == subjectPersonID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 subjectPersonID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SubjectPersonID", subjectPersonID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SubjectPersonID": this.str.SubjectPersonID = (string)value; break;
						case "SubjectSRProfessionType": this.str.SubjectSRProfessionType = (string)value; break;
						case "SubjectOrganizationID": this.str.SubjectOrganizationID = (string)value; break;
						case "SubjectSubOrganizationID": this.str.SubjectSubOrganizationID = (string)value; break;
						case "SubjectSubDivisonID": this.str.SubjectSubDivisonID = (string)value; break;
						case "SubjectServiceUnitID": this.str.SubjectServiceUnitID = (string)value; break;
						case "IsMainSubject": this.str.IsMainSubject = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SubjectPersonID":

							if (value == null || value is System.Int32)
								this.SubjectPersonID = (System.Int32?)value;
							break;
						case "SubjectOrganizationID":

							if (value == null || value is System.Int32)
								this.SubjectOrganizationID = (System.Int32?)value;
							break;
						case "SubjectSubOrganizationID":

							if (value == null || value is System.Int32)
								this.SubjectSubOrganizationID = (System.Int32?)value;
							break;
						case "SubjectSubDivisonID":

							if (value == null || value is System.Int32)
								this.SubjectSubDivisonID = (System.Int32?)value;
							break;
						case "IsMainSubject":

							if (value == null || value is System.Boolean)
								this.IsMainSubject = (System.Boolean?)value;
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
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.SubjectPersonID
		/// </summary>
		virtual public System.Int32? SubjectPersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectPersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectPersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.SubjectSRProfessionType
		/// </summary>
		virtual public System.String SubjectSRProfessionType
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSRProfessionType);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSRProfessionType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.SubjectOrganizationID
		/// </summary>
		virtual public System.Int32? SubjectOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectOrganizationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectOrganizationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.SubjectSubOrganizationID
		/// </summary>
		virtual public System.Int32? SubjectSubOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubOrganizationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubOrganizationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.SubjectSubDivisonID
		/// </summary>
		virtual public System.Int32? SubjectSubDivisonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubDivisonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubDivisonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.SubjectServiceUnitID
		/// </summary>
		virtual public System.String SubjectServiceUnitID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectServiceUnitID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.IsMainSubject
		/// </summary>
		virtual public System.Boolean? IsMainSubject
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.IsMainSubject);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.IsMainSubject, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsSubject.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeSafetyCultureIncidentReportsSubject entity)
			{
				this.entity = entity;
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
			public System.String SubjectPersonID
			{
				get
				{
					System.Int32? data = entity.SubjectPersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectPersonID = null;
					else entity.SubjectPersonID = Convert.ToInt32(value);
				}
			}
			public System.String SubjectSRProfessionType
			{
				get
				{
					System.String data = entity.SubjectSRProfessionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectSRProfessionType = null;
					else entity.SubjectSRProfessionType = Convert.ToString(value);
				}
			}
			public System.String SubjectOrganizationID
			{
				get
				{
					System.Int32? data = entity.SubjectOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectOrganizationID = null;
					else entity.SubjectOrganizationID = Convert.ToInt32(value);
				}
			}
			public System.String SubjectSubOrganizationID
			{
				get
				{
					System.Int32? data = entity.SubjectSubOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectSubOrganizationID = null;
					else entity.SubjectSubOrganizationID = Convert.ToInt32(value);
				}
			}
			public System.String SubjectSubDivisonID
			{
				get
				{
					System.Int32? data = entity.SubjectSubDivisonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectSubDivisonID = null;
					else entity.SubjectSubDivisonID = Convert.ToInt32(value);
				}
			}
			public System.String SubjectServiceUnitID
			{
				get
				{
					System.String data = entity.SubjectServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectServiceUnitID = null;
					else entity.SubjectServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String IsMainSubject
			{
				get
				{
					System.Boolean? data = entity.IsMainSubject;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMainSubject = null;
					else entity.IsMainSubject = Convert.ToBoolean(value);
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
			private esEmployeeSafetyCultureIncidentReportsSubject entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsSubjectQuery query)
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
				throw new Exception("esEmployeeSafetyCultureIncidentReportsSubject can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeSafetyCultureIncidentReportsSubject : esEmployeeSafetyCultureIncidentReportsSubject
	{
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsSubjectQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsSubjectMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SubjectPersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectPersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SubjectSRProfessionType
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSRProfessionType, esSystemType.String);
			}
		}

		public esQueryItem SubjectOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectOrganizationID, esSystemType.Int32);
			}
		}

		public esQueryItem SubjectSubOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubOrganizationID, esSystemType.Int32);
			}
		}

		public esQueryItem SubjectSubDivisonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubDivisonID, esSystemType.Int32);
			}
		}

		public esQueryItem SubjectServiceUnitID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem IsMainSubject
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.IsMainSubject, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSafetyCultureIncidentReportsSubjectCollection")]
	public partial class EmployeeSafetyCultureIncidentReportsSubjectCollection : esEmployeeSafetyCultureIncidentReportsSubjectCollection, IEnumerable<EmployeeSafetyCultureIncidentReportsSubject>
	{
		public EmployeeSafetyCultureIncidentReportsSubjectCollection()
		{

		}

		public static implicit operator List<EmployeeSafetyCultureIncidentReportsSubject>(EmployeeSafetyCultureIncidentReportsSubjectCollection coll)
		{
			List<EmployeeSafetyCultureIncidentReportsSubject> list = new List<EmployeeSafetyCultureIncidentReportsSubject>();

			foreach (EmployeeSafetyCultureIncidentReportsSubject emp in coll)
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
				return EmployeeSafetyCultureIncidentReportsSubjectMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsSubjectQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSafetyCultureIncidentReportsSubject(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSafetyCultureIncidentReportsSubject();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsSubjectQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsSubjectQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsSubjectQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeSafetyCultureIncidentReportsSubject AddNew()
		{
			EmployeeSafetyCultureIncidentReportsSubject entity = base.AddNewEntity() as EmployeeSafetyCultureIncidentReportsSubject;

			return entity;
		}
		public EmployeeSafetyCultureIncidentReportsSubject FindByPrimaryKey(String transactionNo, Int32 subjectPersonID)
		{
			return base.FindByPrimaryKey(transactionNo, subjectPersonID) as EmployeeSafetyCultureIncidentReportsSubject;
		}

		#region IEnumerable< EmployeeSafetyCultureIncidentReportsSubject> Members

		IEnumerator<EmployeeSafetyCultureIncidentReportsSubject> IEnumerable<EmployeeSafetyCultureIncidentReportsSubject>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSafetyCultureIncidentReportsSubject;
			}
		}

		#endregion

		private EmployeeSafetyCultureIncidentReportsSubjectQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSafetyCultureIncidentReportsSubject' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeSafetyCultureIncidentReportsSubject ({TransactionNo, SubjectPersonID})")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsSubject : esEmployeeSafetyCultureIncidentReportsSubject
	{
		public EmployeeSafetyCultureIncidentReportsSubject()
		{
		}

		public EmployeeSafetyCultureIncidentReportsSubject(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsSubjectMetadata.Meta();
			}
		}

		override protected esEmployeeSafetyCultureIncidentReportsSubjectQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsSubjectQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsSubjectQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsSubjectQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsSubjectQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeSafetyCultureIncidentReportsSubjectQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsSubjectQuery : esEmployeeSafetyCultureIncidentReportsSubjectQuery
	{
		public EmployeeSafetyCultureIncidentReportsSubjectQuery()
		{

		}

		public EmployeeSafetyCultureIncidentReportsSubjectQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeSafetyCultureIncidentReportsSubjectQuery";
		}
	}

	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsSubjectMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSafetyCultureIncidentReportsSubjectMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectPersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.SubjectPersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSRProfessionType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.SubjectSRProfessionType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectOrganizationID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.SubjectOrganizationID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubOrganizationID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.SubjectSubOrganizationID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectSubDivisonID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.SubjectSubDivisonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.SubjectServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.IsMainSubject, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.IsMainSubject;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsSubjectMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeSafetyCultureIncidentReportsSubjectMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SubjectPersonID = "SubjectPersonID";
			public const string SubjectSRProfessionType = "SubjectSRProfessionType";
			public const string SubjectOrganizationID = "SubjectOrganizationID";
			public const string SubjectSubOrganizationID = "SubjectSubOrganizationID";
			public const string SubjectSubDivisonID = "SubjectSubDivisonID";
			public const string SubjectServiceUnitID = "SubjectServiceUnitID";
			public const string IsMainSubject = "IsMainSubject";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SubjectPersonID = "SubjectPersonID";
			public const string SubjectSRProfessionType = "SubjectSRProfessionType";
			public const string SubjectOrganizationID = "SubjectOrganizationID";
			public const string SubjectSubOrganizationID = "SubjectSubOrganizationID";
			public const string SubjectSubDivisonID = "SubjectSubDivisonID";
			public const string SubjectServiceUnitID = "SubjectServiceUnitID";
			public const string IsMainSubject = "IsMainSubject";
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
			lock (typeof(EmployeeSafetyCultureIncidentReportsSubjectMetadata))
			{
				if (EmployeeSafetyCultureIncidentReportsSubjectMetadata.mapDelegates == null)
				{
					EmployeeSafetyCultureIncidentReportsSubjectMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeSafetyCultureIncidentReportsSubjectMetadata.meta == null)
				{
					EmployeeSafetyCultureIncidentReportsSubjectMetadata.meta = new EmployeeSafetyCultureIncidentReportsSubjectMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubjectPersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubjectSRProfessionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubjectOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubjectSubOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubjectSubDivisonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubjectServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMainSubject", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeSafetyCultureIncidentReportsSubject";
				meta.Destination = "EmployeeSafetyCultureIncidentReportsSubject";
				meta.spInsert = "proc_EmployeeSafetyCultureIncidentReportsSubjectInsert";
				meta.spUpdate = "proc_EmployeeSafetyCultureIncidentReportsSubjectUpdate";
				meta.spDelete = "proc_EmployeeSafetyCultureIncidentReportsSubjectDelete";
				meta.spLoadAll = "proc_EmployeeSafetyCultureIncidentReportsSubjectLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSafetyCultureIncidentReportsSubjectLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSafetyCultureIncidentReportsSubjectMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
