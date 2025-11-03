/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/22/2021 4:13:48 PM
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
	abstract public class esEmployeeHealthTestResultCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeHealthTestResultCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeHealthTestResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeHealthTestResultQuery query)
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
			this.InitQuery(query as esEmployeeHealthTestResultQuery);
		}
		#endregion

		virtual public EmployeeHealthTestResult DetachEntity(EmployeeHealthTestResult entity)
		{
			return base.DetachEntity(entity) as EmployeeHealthTestResult;
		}

		virtual public EmployeeHealthTestResult AttachEntity(EmployeeHealthTestResult entity)
		{
			return base.AttachEntity(entity) as EmployeeHealthTestResult;
		}

		virtual public void Combine(EmployeeHealthTestResultCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeHealthTestResult this[int index]
		{
			get
			{
				return base[index] as EmployeeHealthTestResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeHealthTestResult);
		}
	}

	[Serializable]
	abstract public class esEmployeeHealthTestResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeHealthTestResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeHealthTestResult()
		{
		}

		public esEmployeeHealthTestResult(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo)
		{
			esEmployeeHealthTestResultQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
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
						case "ResultDate": this.str.ResultDate = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "EmployeeServiceUnitID": this.str.EmployeeServiceUnitID = (string)value; break;
						case "Examination": this.str.Examination = (string)value; break;
						case "Result": this.str.Result = (string)value; break;
						case "SRHealthDegreeConclusion": this.str.SRHealthDegreeConclusion = (string)value; break;
						case "K3rsRecomendation": this.str.K3rsRecomendation = (string)value; break;
						case "PhysicianRecomendation": this.str.PhysicianRecomendation = (string)value; break;
						case "IsMcu": this.str.IsMcu = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ResultDate":

							if (value == null || value is System.DateTime)
								this.ResultDate = (System.DateTime?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "IsMcu":

							if (value == null || value is System.Boolean)
								this.IsMcu = (System.Boolean?)value;
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
		/// Maps to EmployeeHealthTestResult.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.ResultDate
		/// </summary>
		virtual public System.DateTime? ResultDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthTestResultMetadata.ColumnNames.ResultDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeHealthTestResultMetadata.ColumnNames.ResultDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeHealthTestResultMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeHealthTestResultMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.EmployeeServiceUnitID
		/// </summary>
		virtual public System.String EmployeeServiceUnitID
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.EmployeeServiceUnitID);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.EmployeeServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.Examination
		/// </summary>
		virtual public System.String Examination
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.Examination);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.Examination, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.Result);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.Result, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.SRHealthDegreeConclusion
		/// </summary>
		virtual public System.String SRHealthDegreeConclusion
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.SRHealthDegreeConclusion);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.SRHealthDegreeConclusion, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.K3rsRecomendation
		/// </summary>
		virtual public System.String K3rsRecomendation
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.K3rsRecomendation);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.K3rsRecomendation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.PhysicianRecomendation
		/// </summary>
		virtual public System.String PhysicianRecomendation
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.PhysicianRecomendation);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.PhysicianRecomendation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.IsMcu
		/// </summary>
		virtual public System.Boolean? IsMcu
		{
			get
			{
				return base.GetSystemBoolean(EmployeeHealthTestResultMetadata.ColumnNames.IsMcu);
			}

			set
			{
				base.SetSystemBoolean(EmployeeHealthTestResultMetadata.ColumnNames.IsMcu, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeHealthTestResult.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeHealthTestResult entity)
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
			public System.String ResultDate
			{
				get
				{
					System.DateTime? data = entity.ResultDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultDate = null;
					else entity.ResultDate = Convert.ToDateTime(value);
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
			public System.String EmployeeServiceUnitID
			{
				get
				{
					System.String data = entity.EmployeeServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeServiceUnitID = null;
					else entity.EmployeeServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String Examination
			{
				get
				{
					System.String data = entity.Examination;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Examination = null;
					else entity.Examination = Convert.ToString(value);
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
			public System.String SRHealthDegreeConclusion
			{
				get
				{
					System.String data = entity.SRHealthDegreeConclusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRHealthDegreeConclusion = null;
					else entity.SRHealthDegreeConclusion = Convert.ToString(value);
				}
			}
			public System.String K3rsRecomendation
			{
				get
				{
					System.String data = entity.K3rsRecomendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.K3rsRecomendation = null;
					else entity.K3rsRecomendation = Convert.ToString(value);
				}
			}
			public System.String PhysicianRecomendation
			{
				get
				{
					System.String data = entity.PhysicianRecomendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianRecomendation = null;
					else entity.PhysicianRecomendation = Convert.ToString(value);
				}
			}
			public System.String IsMcu
			{
				get
				{
					System.Boolean? data = entity.IsMcu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMcu = null;
					else entity.IsMcu = Convert.ToBoolean(value);
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
			private esEmployeeHealthTestResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeHealthTestResultQuery query)
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
				throw new Exception("esEmployeeHealthTestResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeHealthTestResult : esEmployeeHealthTestResult
	{
	}

	[Serializable]
	abstract public class esEmployeeHealthTestResultQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeHealthTestResultMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ResultDate
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.ResultDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeServiceUnitID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.EmployeeServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem Examination
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.Examination, esSystemType.String);
			}
		}

		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.Result, esSystemType.String);
			}
		}

		public esQueryItem SRHealthDegreeConclusion
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.SRHealthDegreeConclusion, esSystemType.String);
			}
		}

		public esQueryItem K3rsRecomendation
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.K3rsRecomendation, esSystemType.String);
			}
		}

		public esQueryItem PhysicianRecomendation
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.PhysicianRecomendation, esSystemType.String);
			}
		}

		public esQueryItem IsMcu
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.IsMcu, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeHealthTestResultCollection")]
	public partial class EmployeeHealthTestResultCollection : esEmployeeHealthTestResultCollection, IEnumerable<EmployeeHealthTestResult>
	{
		public EmployeeHealthTestResultCollection()
		{

		}

		public static implicit operator List<EmployeeHealthTestResult>(EmployeeHealthTestResultCollection coll)
		{
			List<EmployeeHealthTestResult> list = new List<EmployeeHealthTestResult>();

			foreach (EmployeeHealthTestResult emp in coll)
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
				return EmployeeHealthTestResultMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeHealthTestResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeHealthTestResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeHealthTestResult();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeHealthTestResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeHealthTestResultQuery();
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
		public bool Load(EmployeeHealthTestResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeHealthTestResult AddNew()
		{
			EmployeeHealthTestResult entity = base.AddNewEntity() as EmployeeHealthTestResult;

			return entity;
		}
		public EmployeeHealthTestResult FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as EmployeeHealthTestResult;
		}

		#region IEnumerable< EmployeeHealthTestResult> Members

		IEnumerator<EmployeeHealthTestResult> IEnumerable<EmployeeHealthTestResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeHealthTestResult;
			}
		}

		#endregion

		private EmployeeHealthTestResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeHealthTestResult' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeHealthTestResult ({RegistrationNo})")]
	[Serializable]
	public partial class EmployeeHealthTestResult : esEmployeeHealthTestResult
	{
		public EmployeeHealthTestResult()
		{
		}

		public EmployeeHealthTestResult(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeHealthTestResultMetadata.Meta();
			}
		}

		override protected esEmployeeHealthTestResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeHealthTestResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeHealthTestResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeHealthTestResultQuery();
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
		public bool Load(EmployeeHealthTestResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeHealthTestResultQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeHealthTestResultQuery : esEmployeeHealthTestResultQuery
	{
		public EmployeeHealthTestResultQuery()
		{

		}

		public EmployeeHealthTestResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeHealthTestResultQuery";
		}
	}

	[Serializable]
	public partial class EmployeeHealthTestResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeHealthTestResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.ResultDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.ResultDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.EmployeeServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.EmployeeServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.Examination, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.Examination;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.Result, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.SRHealthDegreeConclusion, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.SRHealthDegreeConclusion;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.K3rsRecomendation, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.K3rsRecomendation;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.PhysicianRecomendation, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.PhysicianRecomendation;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.IsMcu, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.IsMcu;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeHealthTestResultMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthTestResultMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeHealthTestResultMetadata Meta()
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
			public const string ResultDate = "ResultDate";
			public const string PersonID = "PersonID";
			public const string EmployeeServiceUnitID = "EmployeeServiceUnitID";
			public const string Examination = "Examination";
			public const string Result = "Result";
			public const string SRHealthDegreeConclusion = "SRHealthDegreeConclusion";
			public const string K3rsRecomendation = "K3rsRecomendation";
			public const string PhysicianRecomendation = "PhysicianRecomendation";
			public const string IsMcu = "IsMcu";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string ResultDate = "ResultDate";
			public const string PersonID = "PersonID";
			public const string EmployeeServiceUnitID = "EmployeeServiceUnitID";
			public const string Examination = "Examination";
			public const string Result = "Result";
			public const string SRHealthDegreeConclusion = "SRHealthDegreeConclusion";
			public const string K3rsRecomendation = "K3rsRecomendation";
			public const string PhysicianRecomendation = "PhysicianRecomendation";
			public const string IsMcu = "IsMcu";
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
			lock (typeof(EmployeeHealthTestResultMetadata))
			{
				if (EmployeeHealthTestResultMetadata.mapDelegates == null)
				{
					EmployeeHealthTestResultMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeHealthTestResultMetadata.meta == null)
				{
					EmployeeHealthTestResultMetadata.meta = new EmployeeHealthTestResultMetadata();
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
				meta.AddTypeMap("ResultDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Examination", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRHealthDegreeConclusion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("K3rsRecomendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianRecomendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsMcu", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeHealthTestResult";
				meta.Destination = "EmployeeHealthTestResult";
				meta.spInsert = "proc_EmployeeHealthTestResultInsert";
				meta.spUpdate = "proc_EmployeeHealthTestResultUpdate";
				meta.spDelete = "proc_EmployeeHealthTestResultDelete";
				meta.spLoadAll = "proc_EmployeeHealthTestResultLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeHealthTestResultLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeHealthTestResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
