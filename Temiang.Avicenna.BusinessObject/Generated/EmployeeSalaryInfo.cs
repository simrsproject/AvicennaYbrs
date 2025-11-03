/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/16/2022 1:59:51 PM
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
	abstract public class esEmployeeSalaryInfoCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSalaryInfoCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeSalaryInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSalaryInfoQuery query)
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
			this.InitQuery(query as esEmployeeSalaryInfoQuery);
		}
		#endregion

		virtual public EmployeeSalaryInfo DetachEntity(EmployeeSalaryInfo entity)
		{
			return base.DetachEntity(entity) as EmployeeSalaryInfo;
		}

		virtual public EmployeeSalaryInfo AttachEntity(EmployeeSalaryInfo entity)
		{
			return base.AttachEntity(entity) as EmployeeSalaryInfo;
		}

		virtual public void Combine(EmployeeSalaryInfoCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeSalaryInfo this[int index]
		{
			get
			{
				return base[index] as EmployeeSalaryInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSalaryInfo);
		}
	}

	[Serializable]
	abstract public class esEmployeeSalaryInfo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSalaryInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSalaryInfo()
		{
		}

		public esEmployeeSalaryInfo(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personID)
		{
			esEmployeeSalaryInfoQuery query = this.GetDynamicQuery();
			query.Where(query.PersonID == personID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonID", personID);
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
						case "PersonID": this.str.PersonID = (string)value; break;
						case "Npwp": this.str.Npwp = (string)value; break;
						case "SRPaymentFrequency": this.str.SRPaymentFrequency = (string)value; break;
						case "SRTaxStatus": this.str.SRTaxStatus = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "BankAccountNo": this.str.BankAccountNo = (string)value; break;
						case "NoOfDependent": this.str.NoOfDependent = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "JamsostekNo": this.str.JamsostekNo = (string)value; break;
						case "SREmployeeType": this.str.SREmployeeType = (string)value; break;
						case "IsSalaryManaged": this.str.IsSalaryManaged = (string)value; break;
						case "BankAccountName": this.str.BankAccountName = (string)value; break;
						case "SRRemunerationType": this.str.SRRemunerationType = (string)value; break;
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
						case "NoOfDependent":

							if (value == null || value is System.Int32)
								this.NoOfDependent = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsSalaryManaged":

							if (value == null || value is System.Boolean)
								this.IsSalaryManaged = (System.Boolean?)value;
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
		/// Maps to EmployeeSalaryInfo.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSalaryInfoMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSalaryInfoMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.Npwp
		/// </summary>
		virtual public System.String Npwp
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.Npwp);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.Npwp, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.SRPaymentFrequency
		/// </summary>
		virtual public System.String SRPaymentFrequency
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SRPaymentFrequency);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SRPaymentFrequency, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.SRTaxStatus
		/// </summary>
		virtual public System.String SRTaxStatus
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SRTaxStatus);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SRTaxStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.BankID);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.BankAccountNo
		/// </summary>
		virtual public System.String BankAccountNo
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.BankAccountNo);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.BankAccountNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.NoOfDependent
		/// </summary>
		virtual public System.Int32? NoOfDependent
		{
			get
			{
				return base.GetSystemInt32(EmployeeSalaryInfoMetadata.ColumnNames.NoOfDependent);
			}

			set
			{
				base.SetSystemInt32(EmployeeSalaryInfoMetadata.ColumnNames.NoOfDependent, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.JamsostekNo
		/// </summary>
		virtual public System.String JamsostekNo
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.JamsostekNo);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.JamsostekNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.SREmployeeType
		/// </summary>
		virtual public System.String SREmployeeType
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SREmployeeType);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SREmployeeType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.IsSalaryManaged
		/// </summary>
		virtual public System.Boolean? IsSalaryManaged
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSalaryInfoMetadata.ColumnNames.IsSalaryManaged);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSalaryInfoMetadata.ColumnNames.IsSalaryManaged, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.BankAccountName
		/// </summary>
		virtual public System.String BankAccountName
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.BankAccountName);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.BankAccountName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSalaryInfo.SRRemunerationType
		/// </summary>
		virtual public System.String SRRemunerationType
		{
			get
			{
				return base.GetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SRRemunerationType);
			}

			set
			{
				base.SetSystemString(EmployeeSalaryInfoMetadata.ColumnNames.SRRemunerationType, value);
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
			public esStrings(esEmployeeSalaryInfo entity)
			{
				this.entity = entity;
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
			public System.String Npwp
			{
				get
				{
					System.String data = entity.Npwp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Npwp = null;
					else entity.Npwp = Convert.ToString(value);
				}
			}
			public System.String SRPaymentFrequency
			{
				get
				{
					System.String data = entity.SRPaymentFrequency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentFrequency = null;
					else entity.SRPaymentFrequency = Convert.ToString(value);
				}
			}
			public System.String SRTaxStatus
			{
				get
				{
					System.String data = entity.SRTaxStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTaxStatus = null;
					else entity.SRTaxStatus = Convert.ToString(value);
				}
			}
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
			public System.String BankAccountNo
			{
				get
				{
					System.String data = entity.BankAccountNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankAccountNo = null;
					else entity.BankAccountNo = Convert.ToString(value);
				}
			}
			public System.String NoOfDependent
			{
				get
				{
					System.Int32? data = entity.NoOfDependent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoOfDependent = null;
					else entity.NoOfDependent = Convert.ToInt32(value);
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
			public System.String JamsostekNo
			{
				get
				{
					System.String data = entity.JamsostekNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JamsostekNo = null;
					else entity.JamsostekNo = Convert.ToString(value);
				}
			}
			public System.String SREmployeeType
			{
				get
				{
					System.String data = entity.SREmployeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeType = null;
					else entity.SREmployeeType = Convert.ToString(value);
				}
			}
			public System.String IsSalaryManaged
			{
				get
				{
					System.Boolean? data = entity.IsSalaryManaged;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSalaryManaged = null;
					else entity.IsSalaryManaged = Convert.ToBoolean(value);
				}
			}
			public System.String BankAccountName
			{
				get
				{
					System.String data = entity.BankAccountName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankAccountName = null;
					else entity.BankAccountName = Convert.ToString(value);
				}
			}
			public System.String SRRemunerationType
			{
				get
				{
					System.String data = entity.SRRemunerationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRemunerationType = null;
					else entity.SRRemunerationType = Convert.ToString(value);
				}
			}
			private esEmployeeSalaryInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSalaryInfoQuery query)
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
				throw new Exception("esEmployeeSalaryInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeSalaryInfo : esEmployeeSalaryInfo
	{
	}

	[Serializable]
	abstract public class esEmployeeSalaryInfoQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSalaryInfoMetadata.Meta();
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem Npwp
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.Npwp, esSystemType.String);
			}
		}

		public esQueryItem SRPaymentFrequency
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.SRPaymentFrequency, esSystemType.String);
			}
		}

		public esQueryItem SRTaxStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.SRTaxStatus, esSystemType.String);
			}
		}

		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.BankID, esSystemType.String);
			}
		}

		public esQueryItem BankAccountNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.BankAccountNo, esSystemType.String);
			}
		}

		public esQueryItem NoOfDependent
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.NoOfDependent, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem JamsostekNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.JamsostekNo, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeType
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.SREmployeeType, esSystemType.String);
			}
		}

		public esQueryItem IsSalaryManaged
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.IsSalaryManaged, esSystemType.Boolean);
			}
		}

		public esQueryItem BankAccountName
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.BankAccountName, esSystemType.String);
			}
		}

		public esQueryItem SRRemunerationType
		{
			get
			{
				return new esQueryItem(this, EmployeeSalaryInfoMetadata.ColumnNames.SRRemunerationType, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSalaryInfoCollection")]
	public partial class EmployeeSalaryInfoCollection : esEmployeeSalaryInfoCollection, IEnumerable<EmployeeSalaryInfo>
	{
		public EmployeeSalaryInfoCollection()
		{

		}

		public static implicit operator List<EmployeeSalaryInfo>(EmployeeSalaryInfoCollection coll)
		{
			List<EmployeeSalaryInfo> list = new List<EmployeeSalaryInfo>();

			foreach (EmployeeSalaryInfo emp in coll)
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
				return EmployeeSalaryInfoMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSalaryInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSalaryInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSalaryInfo();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSalaryInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSalaryInfoQuery();
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
		public bool Load(EmployeeSalaryInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeSalaryInfo AddNew()
		{
			EmployeeSalaryInfo entity = base.AddNewEntity() as EmployeeSalaryInfo;

			return entity;
		}
		public EmployeeSalaryInfo FindByPrimaryKey(Int32 personID)
		{
			return base.FindByPrimaryKey(personID) as EmployeeSalaryInfo;
		}

		#region IEnumerable< EmployeeSalaryInfo> Members

		IEnumerator<EmployeeSalaryInfo> IEnumerable<EmployeeSalaryInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSalaryInfo;
			}
		}

		#endregion

		private EmployeeSalaryInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSalaryInfo' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeSalaryInfo ({PersonID})")]
	[Serializable]
	public partial class EmployeeSalaryInfo : esEmployeeSalaryInfo
	{
		public EmployeeSalaryInfo()
		{
		}

		public EmployeeSalaryInfo(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSalaryInfoMetadata.Meta();
			}
		}

		override protected esEmployeeSalaryInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSalaryInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSalaryInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSalaryInfoQuery();
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
		public bool Load(EmployeeSalaryInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeSalaryInfoQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeSalaryInfoQuery : esEmployeeSalaryInfoQuery
	{
		public EmployeeSalaryInfoQuery()
		{

		}

		public EmployeeSalaryInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeSalaryInfoQuery";
		}
	}

	[Serializable]
	public partial class EmployeeSalaryInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSalaryInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.Npwp, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.Npwp;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.SRPaymentFrequency, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.SRPaymentFrequency;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.SRTaxStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.SRTaxStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.BankID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.BankAccountNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.BankAccountNo;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.NoOfDependent, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.NoOfDependent;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.JamsostekNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.JamsostekNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.SREmployeeType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.SREmployeeType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.IsSalaryManaged, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.IsSalaryManaged;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.BankAccountName, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.BankAccountName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSalaryInfoMetadata.ColumnNames.SRRemunerationType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSalaryInfoMetadata.PropertyNames.SRRemunerationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeSalaryInfoMetadata Meta()
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
			public const string PersonID = "PersonID";
			public const string Npwp = "Npwp";
			public const string SRPaymentFrequency = "SRPaymentFrequency";
			public const string SRTaxStatus = "SRTaxStatus";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string NoOfDependent = "NoOfDependent";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string JamsostekNo = "JamsostekNo";
			public const string SREmployeeType = "SREmployeeType";
			public const string IsSalaryManaged = "IsSalaryManaged";
			public const string BankAccountName = "BankAccountName";
			public const string SRRemunerationType = "SRRemunerationType";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonID = "PersonID";
			public const string Npwp = "Npwp";
			public const string SRPaymentFrequency = "SRPaymentFrequency";
			public const string SRTaxStatus = "SRTaxStatus";
			public const string BankID = "BankID";
			public const string BankAccountNo = "BankAccountNo";
			public const string NoOfDependent = "NoOfDependent";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string JamsostekNo = "JamsostekNo";
			public const string SREmployeeType = "SREmployeeType";
			public const string IsSalaryManaged = "IsSalaryManaged";
			public const string BankAccountName = "BankAccountName";
			public const string SRRemunerationType = "SRRemunerationType";
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
			lock (typeof(EmployeeSalaryInfoMetadata))
			{
				if (EmployeeSalaryInfoMetadata.mapDelegates == null)
				{
					EmployeeSalaryInfoMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeSalaryInfoMetadata.meta == null)
				{
					EmployeeSalaryInfoMetadata.meta = new EmployeeSalaryInfoMetadata();
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

				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Npwp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentFrequency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTaxStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoOfDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JamsostekNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSalaryManaged", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BankAccountName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRemunerationType", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeSalaryInfo";
				meta.Destination = "EmployeeSalaryInfo";
				meta.spInsert = "proc_EmployeeSalaryInfoInsert";
				meta.spUpdate = "proc_EmployeeSalaryInfoUpdate";
				meta.spDelete = "proc_EmployeeSalaryInfoDelete";
				meta.spLoadAll = "proc_EmployeeSalaryInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSalaryInfoLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSalaryInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
