/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/4/2023 1:34:37 PM
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
	abstract public class esEDCMachineTariffCollection : esEntityCollectionWAuditLog
	{
		public esEDCMachineTariffCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EDCMachineTariffCollection";
		}

		#region Query Logic
		protected void InitQuery(esEDCMachineTariffQuery query)
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
			this.InitQuery(query as esEDCMachineTariffQuery);
		}
		#endregion

		virtual public EDCMachineTariff DetachEntity(EDCMachineTariff entity)
		{
			return base.DetachEntity(entity) as EDCMachineTariff;
		}

		virtual public EDCMachineTariff AttachEntity(EDCMachineTariff entity)
		{
			return base.AttachEntity(entity) as EDCMachineTariff;
		}

		virtual public void Combine(EDCMachineTariffCollection collection)
		{
			base.Combine(collection);
		}

		new public EDCMachineTariff this[int index]
		{
			get
			{
				return base[index] as EDCMachineTariff;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EDCMachineTariff);
		}
	}

	[Serializable]
	abstract public class esEDCMachineTariff : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEDCMachineTariffQuery GetDynamicQuery()
		{
			return null;
		}

		public esEDCMachineTariff()
		{
		}

		public esEDCMachineTariff(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String eDCMachineID, String sRCardType)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(eDCMachineID, sRCardType);
			else
				return LoadByPrimaryKeyStoredProcedure(eDCMachineID, sRCardType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String eDCMachineID, String sRCardType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(eDCMachineID, sRCardType);
			else
				return LoadByPrimaryKeyStoredProcedure(eDCMachineID, sRCardType);
		}

		private bool LoadByPrimaryKeyDynamic(String eDCMachineID, String sRCardType)
		{
			esEDCMachineTariffQuery query = this.GetDynamicQuery();
			query.Where(query.EDCMachineID == eDCMachineID, query.SRCardType == sRCardType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String eDCMachineID, String sRCardType)
		{
			esParameters parms = new esParameters();
			parms.Add("EDCMachineID", eDCMachineID);
			parms.Add("SRCardType", sRCardType);
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
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;
						case "SRCardType": this.str.SRCardType = (string)value; break;
						case "EDCMachineTariff": this.str.EDCMachineTariff = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsChargedToPatient": this.str.IsChargedToPatient = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubledgerID": this.str.SubledgerID = (string)value; break;
						case "AddFeeAmount": this.str.AddFeeAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EDCMachineTariff":

							if (value == null || value is System.Decimal)
								this.EDCMachineTariff = (System.Decimal?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsChargedToPatient":

							if (value == null || value is System.Boolean)
								this.IsChargedToPatient = (System.Boolean?)value;
							break;
						case "ChartOfAccountId":

							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubledgerID":

							if (value == null || value is System.Int32)
								this.SubledgerID = (System.Int32?)value;
							break;
						case "AddFeeAmount":

							if (value == null || value is System.Decimal)
								this.AddFeeAmount = (System.Decimal?)value;
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
		/// Maps to EDCMachineTariff.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(EDCMachineTariffMetadata.ColumnNames.EDCMachineID);
			}

			set
			{
				base.SetSystemString(EDCMachineTariffMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.SRCardType
		/// </summary>
		virtual public System.String SRCardType
		{
			get
			{
				return base.GetSystemString(EDCMachineTariffMetadata.ColumnNames.SRCardType);
			}

			set
			{
				base.SetSystemString(EDCMachineTariffMetadata.ColumnNames.SRCardType, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.EDCMachineTariff
		/// </summary>
		virtual public System.Decimal? EDCMachineTariff
		{
			get
			{
				return base.GetSystemDecimal(EDCMachineTariffMetadata.ColumnNames.EDCMachineTariff);
			}

			set
			{
				base.SetSystemDecimal(EDCMachineTariffMetadata.ColumnNames.EDCMachineTariff, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EDCMachineTariffMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(EDCMachineTariffMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EDCMachineTariffMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EDCMachineTariffMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EDCMachineTariffMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EDCMachineTariffMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.IsChargedToPatient
		/// </summary>
		virtual public System.Boolean? IsChargedToPatient
		{
			get
			{
				return base.GetSystemBoolean(EDCMachineTariffMetadata.ColumnNames.IsChargedToPatient);
			}

			set
			{
				base.SetSystemBoolean(EDCMachineTariffMetadata.ColumnNames.IsChargedToPatient, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(EDCMachineTariffMetadata.ColumnNames.ChartOfAccountId);
			}

			set
			{
				base.SetSystemInt32(EDCMachineTariffMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.SubledgerID
		/// </summary>
		virtual public System.Int32? SubledgerID
		{
			get
			{
				return base.GetSystemInt32(EDCMachineTariffMetadata.ColumnNames.SubledgerID);
			}

			set
			{
				base.SetSystemInt32(EDCMachineTariffMetadata.ColumnNames.SubledgerID, value);
			}
		}
		/// <summary>
		/// Maps to EDCMachineTariff.AddFeeAmount
		/// </summary>
		virtual public System.Decimal? AddFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(EDCMachineTariffMetadata.ColumnNames.AddFeeAmount);
			}

			set
			{
				base.SetSystemDecimal(EDCMachineTariffMetadata.ColumnNames.AddFeeAmount, value);
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
			public esStrings(esEDCMachineTariff entity)
			{
				this.entity = entity;
			}
			public System.String EDCMachineID
			{
				get
				{
					System.String data = entity.EDCMachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineID = null;
					else entity.EDCMachineID = Convert.ToString(value);
				}
			}
			public System.String SRCardType
			{
				get
				{
					System.String data = entity.SRCardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardType = null;
					else entity.SRCardType = Convert.ToString(value);
				}
			}
			public System.String EDCMachineTariff
			{
				get
				{
					System.Decimal? data = entity.EDCMachineTariff;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineTariff = null;
					else entity.EDCMachineTariff = Convert.ToDecimal(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			public System.String IsChargedToPatient
			{
				get
				{
					System.Boolean? data = entity.IsChargedToPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsChargedToPatient = null;
					else entity.IsChargedToPatient = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerID
			{
				get
				{
					System.Int32? data = entity.SubledgerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerID = null;
					else entity.SubledgerID = Convert.ToInt32(value);
				}
			}
			public System.String AddFeeAmount
			{
				get
				{
					System.Decimal? data = entity.AddFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddFeeAmount = null;
					else entity.AddFeeAmount = Convert.ToDecimal(value);
				}
			}
			private esEDCMachineTariff entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEDCMachineTariffQuery query)
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
				throw new Exception("esEDCMachineTariff can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EDCMachineTariff : esEDCMachineTariff
	{
	}

	[Serializable]
	abstract public class esEDCMachineTariffQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EDCMachineTariffMetadata.Meta();
			}
		}

		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		}

		public esQueryItem SRCardType
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.SRCardType, esSystemType.String);
			}
		}

		public esQueryItem EDCMachineTariff
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.EDCMachineTariff, esSystemType.Decimal);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsChargedToPatient
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.IsChargedToPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerID
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.SubledgerID, esSystemType.Int32);
			}
		}

		public esQueryItem AddFeeAmount
		{
			get
			{
				return new esQueryItem(this, EDCMachineTariffMetadata.ColumnNames.AddFeeAmount, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EDCMachineTariffCollection")]
	public partial class EDCMachineTariffCollection : esEDCMachineTariffCollection, IEnumerable<EDCMachineTariff>
	{
		public EDCMachineTariffCollection()
		{

		}

		public static implicit operator List<EDCMachineTariff>(EDCMachineTariffCollection coll)
		{
			List<EDCMachineTariff> list = new List<EDCMachineTariff>();

			foreach (EDCMachineTariff emp in coll)
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
				return EDCMachineTariffMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EDCMachineTariffQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EDCMachineTariff(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EDCMachineTariff();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EDCMachineTariffQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EDCMachineTariffQuery();
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
		public bool Load(EDCMachineTariffQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EDCMachineTariff AddNew()
		{
			EDCMachineTariff entity = base.AddNewEntity() as EDCMachineTariff;

			return entity;
		}
		public EDCMachineTariff FindByPrimaryKey(String eDCMachineID, String sRCardType)
		{
			return base.FindByPrimaryKey(eDCMachineID, sRCardType) as EDCMachineTariff;
		}

		#region IEnumerable< EDCMachineTariff> Members

		IEnumerator<EDCMachineTariff> IEnumerable<EDCMachineTariff>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EDCMachineTariff;
			}
		}

		#endregion

		private EDCMachineTariffQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EDCMachineTariff' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EDCMachineTariff ({EDCMachineID, SRCardType})")]
	[Serializable]
	public partial class EDCMachineTariff : esEDCMachineTariff
	{
		public EDCMachineTariff()
		{
		}

		public EDCMachineTariff(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EDCMachineTariffMetadata.Meta();
			}
		}

		override protected esEDCMachineTariffQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EDCMachineTariffQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EDCMachineTariffQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EDCMachineTariffQuery();
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
		public bool Load(EDCMachineTariffQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EDCMachineTariffQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EDCMachineTariffQuery : esEDCMachineTariffQuery
	{
		public EDCMachineTariffQuery()
		{

		}

		public EDCMachineTariffQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EDCMachineTariffQuery";
		}
	}

	[Serializable]
	public partial class EDCMachineTariffMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EDCMachineTariffMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.EDCMachineID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.EDCMachineID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.SRCardType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.SRCardType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.EDCMachineTariff, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.EDCMachineTariff;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.IsChargedToPatient, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.IsChargedToPatient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.ChartOfAccountId, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.SubledgerID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.SubledgerID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EDCMachineTariffMetadata.ColumnNames.AddFeeAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EDCMachineTariffMetadata.PropertyNames.AddFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EDCMachineTariffMetadata Meta()
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
			public const string EDCMachineID = "EDCMachineID";
			public const string SRCardType = "SRCardType";
			public const string EDCMachineTariff = "EDCMachineTariff";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsChargedToPatient = "IsChargedToPatient";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerID = "SubledgerID";
			public const string AddFeeAmount = "AddFeeAmount";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EDCMachineID = "EDCMachineID";
			public const string SRCardType = "SRCardType";
			public const string EDCMachineTariff = "EDCMachineTariff";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsChargedToPatient = "IsChargedToPatient";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerID = "SubledgerID";
			public const string AddFeeAmount = "AddFeeAmount";
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
			lock (typeof(EDCMachineTariffMetadata))
			{
				if (EDCMachineTariffMetadata.mapDelegates == null)
				{
					EDCMachineTariffMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EDCMachineTariffMetadata.meta == null)
				{
					EDCMachineTariffMetadata.meta = new EDCMachineTariffMetadata();
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

				meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EDCMachineTariff", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsChargedToPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AddFeeAmount", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "EDCMachineTariff";
				meta.Destination = "EDCMachineTariff";
				meta.spInsert = "proc_EDCMachineTariffInsert";
				meta.spUpdate = "proc_EDCMachineTariffUpdate";
				meta.spDelete = "proc_EDCMachineTariffDelete";
				meta.spLoadAll = "proc_EDCMachineTariffLoadAll";
				meta.spLoadByPrimaryKey = "proc_EDCMachineTariffLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EDCMachineTariffMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
