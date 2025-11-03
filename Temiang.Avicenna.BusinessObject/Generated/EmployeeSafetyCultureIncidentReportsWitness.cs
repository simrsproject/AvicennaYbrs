/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/15/2022 2:34:44 PM
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
	abstract public class esEmployeeSafetyCultureIncidentReportsWitnessCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSafetyCultureIncidentReportsWitnessCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeSafetyCultureIncidentReportsWitnessCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsWitnessQuery query)
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
			this.InitQuery(query as esEmployeeSafetyCultureIncidentReportsWitnessQuery);
		}
		#endregion

		virtual public EmployeeSafetyCultureIncidentReportsWitness DetachEntity(EmployeeSafetyCultureIncidentReportsWitness entity)
		{
			return base.DetachEntity(entity) as EmployeeSafetyCultureIncidentReportsWitness;
		}

		virtual public EmployeeSafetyCultureIncidentReportsWitness AttachEntity(EmployeeSafetyCultureIncidentReportsWitness entity)
		{
			return base.AttachEntity(entity) as EmployeeSafetyCultureIncidentReportsWitness;
		}

		virtual public void Combine(EmployeeSafetyCultureIncidentReportsWitnessCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeSafetyCultureIncidentReportsWitness this[int index]
		{
			get
			{
				return base[index] as EmployeeSafetyCultureIncidentReportsWitness;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSafetyCultureIncidentReportsWitness);
		}
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsWitness : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSafetyCultureIncidentReportsWitnessQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSafetyCultureIncidentReportsWitness()
		{
		}

		public esEmployeeSafetyCultureIncidentReportsWitness(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, Int32 witnessPersonID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, witnessPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, witnessPersonID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 witnessPersonID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, witnessPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, witnessPersonID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 witnessPersonID)
		{
			esEmployeeSafetyCultureIncidentReportsWitnessQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.WitnessPersonID == witnessPersonID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 witnessPersonID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("WitnessPersonID", witnessPersonID);
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
						case "WitnessPersonID": this.str.WitnessPersonID = (string)value; break;
						case "WitnessSRProfessionType": this.str.WitnessSRProfessionType = (string)value; break;
						case "WitnessOrganizationID": this.str.WitnessOrganizationID = (string)value; break;
						case "WitnessSubOrganizationID": this.str.WitnessSubOrganizationID = (string)value; break;
						case "WitnessSubDivisonID": this.str.WitnessSubDivisonID = (string)value; break;
						case "WitnessServiceUnitID": this.str.WitnessServiceUnitID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "WitnessPersonID":

							if (value == null || value is System.Int32)
								this.WitnessPersonID = (System.Int32?)value;
							break;
						case "WitnessOrganizationID":

							if (value == null || value is System.Int32)
								this.WitnessOrganizationID = (System.Int32?)value;
							break;
						case "WitnessSubOrganizationID":

							if (value == null || value is System.Int32)
								this.WitnessSubOrganizationID = (System.Int32?)value;
							break;
						case "WitnessSubDivisonID":

							if (value == null || value is System.Int32)
								this.WitnessSubDivisonID = (System.Int32?)value;
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
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.WitnessPersonID
		/// </summary>
		virtual public System.Int32? WitnessPersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.WitnessSRProfessionType
		/// </summary>
		virtual public System.String WitnessSRProfessionType
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSRProfessionType);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSRProfessionType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.WitnessOrganizationID
		/// </summary>
		virtual public System.Int32? WitnessOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessOrganizationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessOrganizationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.WitnessSubOrganizationID
		/// </summary>
		virtual public System.Int32? WitnessSubOrganizationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubOrganizationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubOrganizationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.WitnessSubDivisonID
		/// </summary>
		virtual public System.Int32? WitnessSubDivisonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubDivisonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubDivisonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.WitnessServiceUnitID
		/// </summary>
		virtual public System.String WitnessServiceUnitID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessServiceUnitID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsWitness.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeSafetyCultureIncidentReportsWitness entity)
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
			public System.String WitnessPersonID
			{
				get
				{
					System.Int32? data = entity.WitnessPersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WitnessPersonID = null;
					else entity.WitnessPersonID = Convert.ToInt32(value);
				}
			}
			public System.String WitnessSRProfessionType
			{
				get
				{
					System.String data = entity.WitnessSRProfessionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WitnessSRProfessionType = null;
					else entity.WitnessSRProfessionType = Convert.ToString(value);
				}
			}
			public System.String WitnessOrganizationID
			{
				get
				{
					System.Int32? data = entity.WitnessOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WitnessOrganizationID = null;
					else entity.WitnessOrganizationID = Convert.ToInt32(value);
				}
			}
			public System.String WitnessSubOrganizationID
			{
				get
				{
					System.Int32? data = entity.WitnessSubOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WitnessSubOrganizationID = null;
					else entity.WitnessSubOrganizationID = Convert.ToInt32(value);
				}
			}
			public System.String WitnessSubDivisonID
			{
				get
				{
					System.Int32? data = entity.WitnessSubDivisonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WitnessSubDivisonID = null;
					else entity.WitnessSubDivisonID = Convert.ToInt32(value);
				}
			}
			public System.String WitnessServiceUnitID
			{
				get
				{
					System.String data = entity.WitnessServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WitnessServiceUnitID = null;
					else entity.WitnessServiceUnitID = Convert.ToString(value);
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
			private esEmployeeSafetyCultureIncidentReportsWitness entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsWitnessQuery query)
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
				throw new Exception("esEmployeeSafetyCultureIncidentReportsWitness can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeSafetyCultureIncidentReportsWitness : esEmployeeSafetyCultureIncidentReportsWitness
	{
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsWitnessQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsWitnessMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem WitnessPersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID, esSystemType.Int32);
			}
		}

		public esQueryItem WitnessSRProfessionType
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSRProfessionType, esSystemType.String);
			}
		}

		public esQueryItem WitnessOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessOrganizationID, esSystemType.Int32);
			}
		}

		public esQueryItem WitnessSubOrganizationID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubOrganizationID, esSystemType.Int32);
			}
		}

		public esQueryItem WitnessSubDivisonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubDivisonID, esSystemType.Int32);
			}
		}

		public esQueryItem WitnessServiceUnitID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSafetyCultureIncidentReportsWitnessCollection")]
	public partial class EmployeeSafetyCultureIncidentReportsWitnessCollection : esEmployeeSafetyCultureIncidentReportsWitnessCollection, IEnumerable<EmployeeSafetyCultureIncidentReportsWitness>
	{
		public EmployeeSafetyCultureIncidentReportsWitnessCollection()
		{

		}

		public static implicit operator List<EmployeeSafetyCultureIncidentReportsWitness>(EmployeeSafetyCultureIncidentReportsWitnessCollection coll)
		{
			List<EmployeeSafetyCultureIncidentReportsWitness> list = new List<EmployeeSafetyCultureIncidentReportsWitness>();

			foreach (EmployeeSafetyCultureIncidentReportsWitness emp in coll)
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
				return EmployeeSafetyCultureIncidentReportsWitnessMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsWitnessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSafetyCultureIncidentReportsWitness(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSafetyCultureIncidentReportsWitness();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsWitnessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsWitnessQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsWitnessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeSafetyCultureIncidentReportsWitness AddNew()
		{
			EmployeeSafetyCultureIncidentReportsWitness entity = base.AddNewEntity() as EmployeeSafetyCultureIncidentReportsWitness;

			return entity;
		}
		public EmployeeSafetyCultureIncidentReportsWitness FindByPrimaryKey(String transactionNo, Int32 witnessPersonID)
		{
			return base.FindByPrimaryKey(transactionNo, witnessPersonID) as EmployeeSafetyCultureIncidentReportsWitness;
		}

		#region IEnumerable< EmployeeSafetyCultureIncidentReportsWitness> Members

		IEnumerator<EmployeeSafetyCultureIncidentReportsWitness> IEnumerable<EmployeeSafetyCultureIncidentReportsWitness>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSafetyCultureIncidentReportsWitness;
			}
		}

		#endregion

		private EmployeeSafetyCultureIncidentReportsWitnessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSafetyCultureIncidentReportsWitness' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeSafetyCultureIncidentReportsWitness ({TransactionNo, WitnessPersonID})")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsWitness : esEmployeeSafetyCultureIncidentReportsWitness
	{
		public EmployeeSafetyCultureIncidentReportsWitness()
		{
		}

		public EmployeeSafetyCultureIncidentReportsWitness(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsWitnessMetadata.Meta();
			}
		}

		override protected esEmployeeSafetyCultureIncidentReportsWitnessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsWitnessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsWitnessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsWitnessQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsWitnessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeSafetyCultureIncidentReportsWitnessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsWitnessQuery : esEmployeeSafetyCultureIncidentReportsWitnessQuery
	{
		public EmployeeSafetyCultureIncidentReportsWitnessQuery()
		{

		}

		public EmployeeSafetyCultureIncidentReportsWitnessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeSafetyCultureIncidentReportsWitnessQuery";
		}
	}

	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsWitnessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSafetyCultureIncidentReportsWitnessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.WitnessPersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSRProfessionType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.WitnessSRProfessionType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessOrganizationID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.WitnessOrganizationID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubOrganizationID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.WitnessSubOrganizationID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubDivisonID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.WitnessSubDivisonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.WitnessServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsWitnessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeSafetyCultureIncidentReportsWitnessMetadata Meta()
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
			public const string WitnessPersonID = "WitnessPersonID";
			public const string WitnessSRProfessionType = "WitnessSRProfessionType";
			public const string WitnessOrganizationID = "WitnessOrganizationID";
			public const string WitnessSubOrganizationID = "WitnessSubOrganizationID";
			public const string WitnessSubDivisonID = "WitnessSubDivisonID";
			public const string WitnessServiceUnitID = "WitnessServiceUnitID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string WitnessPersonID = "WitnessPersonID";
			public const string WitnessSRProfessionType = "WitnessSRProfessionType";
			public const string WitnessOrganizationID = "WitnessOrganizationID";
			public const string WitnessSubOrganizationID = "WitnessSubOrganizationID";
			public const string WitnessSubDivisonID = "WitnessSubDivisonID";
			public const string WitnessServiceUnitID = "WitnessServiceUnitID";
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
			lock (typeof(EmployeeSafetyCultureIncidentReportsWitnessMetadata))
			{
				if (EmployeeSafetyCultureIncidentReportsWitnessMetadata.mapDelegates == null)
				{
					EmployeeSafetyCultureIncidentReportsWitnessMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeSafetyCultureIncidentReportsWitnessMetadata.meta == null)
				{
					EmployeeSafetyCultureIncidentReportsWitnessMetadata.meta = new EmployeeSafetyCultureIncidentReportsWitnessMetadata();
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
				meta.AddTypeMap("WitnessPersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WitnessSRProfessionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WitnessOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WitnessSubOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WitnessSubDivisonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WitnessServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeSafetyCultureIncidentReportsWitness";
				meta.Destination = "EmployeeSafetyCultureIncidentReportsWitness";
				meta.spInsert = "proc_EmployeeSafetyCultureIncidentReportsWitnessInsert";
				meta.spUpdate = "proc_EmployeeSafetyCultureIncidentReportsWitnessUpdate";
				meta.spDelete = "proc_EmployeeSafetyCultureIncidentReportsWitnessDelete";
				meta.spLoadAll = "proc_EmployeeSafetyCultureIncidentReportsWitnessLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSafetyCultureIncidentReportsWitnessLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSafetyCultureIncidentReportsWitnessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
