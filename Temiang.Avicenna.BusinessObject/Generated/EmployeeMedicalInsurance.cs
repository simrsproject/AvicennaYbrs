/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/24/2023 10:01:44 AM
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
	abstract public class esEmployeeMedicalInsuranceCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeMedicalInsuranceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeMedicalInsuranceCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeMedicalInsuranceQuery query)
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
			this.InitQuery(query as esEmployeeMedicalInsuranceQuery);
		}
		#endregion

		virtual public EmployeeMedicalInsurance DetachEntity(EmployeeMedicalInsurance entity)
		{
			return base.DetachEntity(entity) as EmployeeMedicalInsurance;
		}

		virtual public EmployeeMedicalInsurance AttachEntity(EmployeeMedicalInsurance entity)
		{
			return base.AttachEntity(entity) as EmployeeMedicalInsurance;
		}

		virtual public void Combine(EmployeeMedicalInsuranceCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeMedicalInsurance this[int index]
		{
			get
			{
				return base[index] as EmployeeMedicalInsurance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeMedicalInsurance);
		}
	}

	[Serializable]
	abstract public class esEmployeeMedicalInsurance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeMedicalInsuranceQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeMedicalInsurance()
		{
		}

		public esEmployeeMedicalInsurance(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String medicalInsuranceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalInsuranceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalInsuranceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String medicalInsuranceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalInsuranceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalInsuranceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String medicalInsuranceNo)
		{
			esEmployeeMedicalInsuranceQuery query = this.GetDynamicQuery();
			query.Where(query.MedicalInsuranceNo == medicalInsuranceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String medicalInsuranceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicalInsuranceNo", medicalInsuranceNo);
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
						case "MedicalInsuranceNo": this.str.MedicalInsuranceNo = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PersonalFamilyID": this.str.PersonalFamilyID = (string)value; break;
						case "ForTreatmentDate": this.str.ForTreatmentDate = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "SRMedicalInsuranceType": this.str.SRMedicalInsuranceType = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Complaint": this.str.Complaint = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PersonalFamilyID":

							if (value == null || value is System.Int32)
								this.PersonalFamilyID = (System.Int32?)value;
							break;
						case "ForTreatmentDate":

							if (value == null || value is System.DateTime)
								this.ForTreatmentDate = (System.DateTime?)value;
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
		/// Maps to EmployeeMedicalInsurance.MedicalInsuranceNo
		/// </summary>
		virtual public System.String MedicalInsuranceNo
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.MedicalInsuranceNo);
			}

			set
			{
				base.SetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.MedicalInsuranceNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalInsuranceMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeMedicalInsuranceMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.PersonalFamilyID
		/// </summary>
		virtual public System.Int32? PersonalFamilyID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMedicalInsuranceMetadata.ColumnNames.PersonalFamilyID);
			}

			set
			{
				base.SetSystemInt32(EmployeeMedicalInsuranceMetadata.ColumnNames.PersonalFamilyID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.ForTreatmentDate
		/// </summary>
		virtual public System.DateTime? ForTreatmentDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMedicalInsuranceMetadata.ColumnNames.ForTreatmentDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeMedicalInsuranceMetadata.ColumnNames.ForTreatmentDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.SRMedicalInsuranceType
		/// </summary>
		virtual public System.String SRMedicalInsuranceType
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.SRMedicalInsuranceType);
			}

			set
			{
				base.SetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.SRMedicalInsuranceType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeMedicalInsurance.Complaint
		/// </summary>
		virtual public System.String Complaint
		{
			get
			{
				return base.GetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.Complaint);
			}

			set
			{
				base.SetSystemString(EmployeeMedicalInsuranceMetadata.ColumnNames.Complaint, value);
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
			public esStrings(esEmployeeMedicalInsurance entity)
			{
				this.entity = entity;
			}
			public System.String MedicalInsuranceNo
			{
				get
				{
					System.String data = entity.MedicalInsuranceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalInsuranceNo = null;
					else entity.MedicalInsuranceNo = Convert.ToString(value);
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
			public System.String PersonalFamilyID
			{
				get
				{
					System.Int32? data = entity.PersonalFamilyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalFamilyID = null;
					else entity.PersonalFamilyID = Convert.ToInt32(value);
				}
			}
			public System.String ForTreatmentDate
			{
				get
				{
					System.DateTime? data = entity.ForTreatmentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ForTreatmentDate = null;
					else entity.ForTreatmentDate = Convert.ToDateTime(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String SRMedicalInsuranceType
			{
				get
				{
					System.String data = entity.SRMedicalInsuranceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalInsuranceType = null;
					else entity.SRMedicalInsuranceType = Convert.ToString(value);
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
			public System.String Complaint
			{
				get
				{
					System.String data = entity.Complaint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Complaint = null;
					else entity.Complaint = Convert.ToString(value);
				}
			}
			private esEmployeeMedicalInsurance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeMedicalInsuranceQuery query)
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
				throw new Exception("esEmployeeMedicalInsurance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeMedicalInsurance : esEmployeeMedicalInsurance
	{
	}

	[Serializable]
	abstract public class esEmployeeMedicalInsuranceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMedicalInsuranceMetadata.Meta();
			}
		}

		public esQueryItem MedicalInsuranceNo
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.MedicalInsuranceNo, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonalFamilyID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.PersonalFamilyID, esSystemType.Int32);
			}
		}

		public esQueryItem ForTreatmentDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.ForTreatmentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem SRMedicalInsuranceType
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.SRMedicalInsuranceType, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Complaint
		{
			get
			{
				return new esQueryItem(this, EmployeeMedicalInsuranceMetadata.ColumnNames.Complaint, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeMedicalInsuranceCollection")]
	public partial class EmployeeMedicalInsuranceCollection : esEmployeeMedicalInsuranceCollection, IEnumerable<EmployeeMedicalInsurance>
	{
		public EmployeeMedicalInsuranceCollection()
		{

		}

		public static implicit operator List<EmployeeMedicalInsurance>(EmployeeMedicalInsuranceCollection coll)
		{
			List<EmployeeMedicalInsurance> list = new List<EmployeeMedicalInsurance>();

			foreach (EmployeeMedicalInsurance emp in coll)
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
				return EmployeeMedicalInsuranceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMedicalInsuranceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeMedicalInsurance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeMedicalInsurance();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeMedicalInsuranceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMedicalInsuranceQuery();
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
		public bool Load(EmployeeMedicalInsuranceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeMedicalInsurance AddNew()
		{
			EmployeeMedicalInsurance entity = base.AddNewEntity() as EmployeeMedicalInsurance;

			return entity;
		}
		public EmployeeMedicalInsurance FindByPrimaryKey(String medicalInsuranceNo)
		{
			return base.FindByPrimaryKey(medicalInsuranceNo) as EmployeeMedicalInsurance;
		}

		#region IEnumerable< EmployeeMedicalInsurance> Members

		IEnumerator<EmployeeMedicalInsurance> IEnumerable<EmployeeMedicalInsurance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeMedicalInsurance;
			}
		}

		#endregion

		private EmployeeMedicalInsuranceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeMedicalInsurance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeMedicalInsurance ({MedicalInsuranceNo})")]
	[Serializable]
	public partial class EmployeeMedicalInsurance : esEmployeeMedicalInsurance
	{
		public EmployeeMedicalInsurance()
		{
		}

		public EmployeeMedicalInsurance(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMedicalInsuranceMetadata.Meta();
			}
		}

		override protected esEmployeeMedicalInsuranceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMedicalInsuranceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeMedicalInsuranceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMedicalInsuranceQuery();
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
		public bool Load(EmployeeMedicalInsuranceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeMedicalInsuranceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeMedicalInsuranceQuery : esEmployeeMedicalInsuranceQuery
	{
		public EmployeeMedicalInsuranceQuery()
		{

		}

		public EmployeeMedicalInsuranceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeMedicalInsuranceQuery";
		}
	}

	[Serializable]
	public partial class EmployeeMedicalInsuranceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeMedicalInsuranceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.MedicalInsuranceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.MedicalInsuranceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.PersonalFamilyID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.PersonalFamilyID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.ForTreatmentDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.ForTreatmentDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.SRMedicalInsuranceType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.SRMedicalInsuranceType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeMedicalInsuranceMetadata.ColumnNames.Complaint, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMedicalInsuranceMetadata.PropertyNames.Complaint;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeMedicalInsuranceMetadata Meta()
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
			public const string MedicalInsuranceNo = "MedicalInsuranceNo";
			public const string PersonID = "PersonID";
			public const string PersonalFamilyID = "PersonalFamilyID";
			public const string ForTreatmentDate = "ForTreatmentDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SRMedicalInsuranceType = "SRMedicalInsuranceType";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Complaint = "Complaint";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MedicalInsuranceNo = "MedicalInsuranceNo";
			public const string PersonID = "PersonID";
			public const string PersonalFamilyID = "PersonalFamilyID";
			public const string ForTreatmentDate = "ForTreatmentDate";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SRMedicalInsuranceType = "SRMedicalInsuranceType";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Complaint = "Complaint";
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
			lock (typeof(EmployeeMedicalInsuranceMetadata))
			{
				if (EmployeeMedicalInsuranceMetadata.mapDelegates == null)
				{
					EmployeeMedicalInsuranceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeMedicalInsuranceMetadata.meta == null)
				{
					EmployeeMedicalInsuranceMetadata.meta = new EmployeeMedicalInsuranceMetadata();
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

				meta.AddTypeMap("MedicalInsuranceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonalFamilyID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ForTreatmentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalInsuranceType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Complaint", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeMedicalInsurance";
				meta.Destination = "EmployeeMedicalInsurance";
				meta.spInsert = "proc_EmployeeMedicalInsuranceInsert";
				meta.spUpdate = "proc_EmployeeMedicalInsuranceUpdate";
				meta.spDelete = "proc_EmployeeMedicalInsuranceDelete";
				meta.spLoadAll = "proc_EmployeeMedicalInsuranceLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeMedicalInsuranceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeMedicalInsuranceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
