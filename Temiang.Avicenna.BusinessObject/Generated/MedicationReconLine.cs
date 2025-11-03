/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2022 11:00:13 AM
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
	abstract public class esMedicationReconLineCollection : esEntityCollectionWAuditLog
	{
		public esMedicationReconLineCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicationReconLineCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicationReconLineQuery query)
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
			this.InitQuery(query as esMedicationReconLineQuery);
		}
		#endregion

		virtual public MedicationReconLine DetachEntity(MedicationReconLine entity)
		{
			return base.DetachEntity(entity) as MedicationReconLine;
		}

		virtual public MedicationReconLine AttachEntity(MedicationReconLine entity)
		{
			return base.AttachEntity(entity) as MedicationReconLine;
		}

		virtual public void Combine(MedicationReconLineCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicationReconLine this[int index]
		{
			get
			{
				return base[index] as MedicationReconLine;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicationReconLine);
		}
	}

	[Serializable]
	abstract public class esMedicationReconLine : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicationReconLineQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicationReconLine()
		{
		}

		public esMedicationReconLine(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 reconSeqNo, Int64 medicationReceiveNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, reconSeqNo, medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, reconSeqNo, medicationReceiveNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 reconSeqNo, Int64 medicationReceiveNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, reconSeqNo, medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, reconSeqNo, medicationReceiveNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 reconSeqNo, Int64 medicationReceiveNo)
		{
			esMedicationReconLineQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.ReconSeqNo == reconSeqNo, query.MedicationReceiveNo == medicationReceiveNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 reconSeqNo, Int64 medicationReceiveNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("ReconSeqNo", reconSeqNo);
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
						case "ReconSeqNo": this.str.ReconSeqNo = (string)value; break;
						case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
						case "ReconStatus": this.str.ReconStatus = (string)value; break;
						case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
						case "ConsumeQty": this.str.ConsumeQty = (string)value; break;
						case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
						case "SRMedicationConsume": this.str.SRMedicationConsume = (string)value; break;
						case "IsApprove": this.str.IsApprove = (string)value; break;
						case "ApproveDateTime": this.str.ApproveDateTime = (string)value; break;
						case "ApproveByParamedicID": this.str.ApproveByParamedicID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReconSeqNo":

							if (value == null || value is System.Int32)
								this.ReconSeqNo = (System.Int32?)value;
							break;
						case "MedicationReceiveNo":

							if (value == null || value is System.Int64)
								this.MedicationReceiveNo = (System.Int64?)value;
							break;
						case "IsApprove":

							if (value == null || value is System.Boolean)
								this.IsApprove = (System.Boolean?)value;
							break;
						case "ApproveDateTime":

							if (value == null || value is System.DateTime)
								this.ApproveDateTime = (System.DateTime?)value;
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
		/// Maps to MedicationReconLine.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.ReconSeqNo
		/// </summary>
		virtual public System.Int32? ReconSeqNo
		{
			get
			{
				return base.GetSystemInt32(MedicationReconLineMetadata.ColumnNames.ReconSeqNo);
			}

			set
			{
				base.SetSystemInt32(MedicationReconLineMetadata.ColumnNames.ReconSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(MedicationReconLineMetadata.ColumnNames.MedicationReceiveNo);
			}

			set
			{
				base.SetSystemInt64(MedicationReconLineMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.ReconStatus
		/// </summary>
		virtual public System.String ReconStatus
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.ReconStatus);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.ReconStatus, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.SRConsumeMethod
		/// </summary>
		virtual public System.String SRConsumeMethod
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.SRConsumeMethod);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.SRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.ConsumeQty
		/// </summary>
		virtual public System.String ConsumeQty
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.ConsumeQty);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.ConsumeQty, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.SRConsumeUnit
		/// </summary>
		virtual public System.String SRConsumeUnit
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.SRConsumeUnit);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.SRConsumeUnit, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.SRMedicationConsume
		/// </summary>
		virtual public System.String SRMedicationConsume
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.SRMedicationConsume);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.SRMedicationConsume, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(MedicationReconLineMetadata.ColumnNames.IsApprove);
			}

			set
			{
				base.SetSystemBoolean(MedicationReconLineMetadata.ColumnNames.IsApprove, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.ApproveDateTime
		/// </summary>
		virtual public System.DateTime? ApproveDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReconLineMetadata.ColumnNames.ApproveDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReconLineMetadata.ColumnNames.ApproveDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.ApproveByParamedicID
		/// </summary>
		virtual public System.String ApproveByParamedicID
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.ApproveByParamedicID);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.ApproveByParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReconLineMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReconLineMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReconLine.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReconLineMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicationReconLineMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicationReconLine entity)
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
			public System.String ReconSeqNo
			{
				get
				{
					System.Int32? data = entity.ReconSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReconSeqNo = null;
					else entity.ReconSeqNo = Convert.ToInt32(value);
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
			public System.String ReconStatus
			{
				get
				{
					System.String data = entity.ReconStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReconStatus = null;
					else entity.ReconStatus = Convert.ToString(value);
				}
			}
			public System.String SRConsumeMethod
			{
				get
				{
					System.String data = entity.SRConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
					else entity.SRConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String ConsumeQty
			{
				get
				{
					System.String data = entity.ConsumeQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeQty = null;
					else entity.ConsumeQty = Convert.ToString(value);
				}
			}
			public System.String SRConsumeUnit
			{
				get
				{
					System.String data = entity.SRConsumeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeUnit = null;
					else entity.SRConsumeUnit = Convert.ToString(value);
				}
			}
			public System.String SRMedicationConsume
			{
				get
				{
					System.String data = entity.SRMedicationConsume;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicationConsume = null;
					else entity.SRMedicationConsume = Convert.ToString(value);
				}
			}
			public System.String IsApprove
			{
				get
				{
					System.Boolean? data = entity.IsApprove;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApprove = null;
					else entity.IsApprove = Convert.ToBoolean(value);
				}
			}
			public System.String ApproveDateTime
			{
				get
				{
					System.DateTime? data = entity.ApproveDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproveDateTime = null;
					else entity.ApproveDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApproveByParamedicID
			{
				get
				{
					System.String data = entity.ApproveByParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproveByParamedicID = null;
					else entity.ApproveByParamedicID = Convert.ToString(value);
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
			private esMedicationReconLine entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicationReconLineQuery query)
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
				throw new Exception("esMedicationReconLine can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicationReconLine : esMedicationReconLine
	{
	}

	[Serializable]
	abstract public class esMedicationReconLineQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicationReconLineMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ReconSeqNo
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.ReconSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		}

		public esQueryItem ReconStatus
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.ReconStatus, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem ConsumeQty
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.ConsumeQty, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeUnit
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
			}
		}

		public esQueryItem SRMedicationConsume
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.SRMedicationConsume, esSystemType.String);
			}
		}

		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		}

		public esQueryItem ApproveDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.ApproveDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApproveByParamedicID
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.ApproveByParamedicID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReconLineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicationReconLineCollection")]
	public partial class MedicationReconLineCollection : esMedicationReconLineCollection, IEnumerable<MedicationReconLine>
	{
		public MedicationReconLineCollection()
		{

		}

		public static implicit operator List<MedicationReconLine>(MedicationReconLineCollection coll)
		{
			List<MedicationReconLine> list = new List<MedicationReconLine>();

			foreach (MedicationReconLine emp in coll)
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
				return MedicationReconLineMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReconLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicationReconLine(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicationReconLine();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicationReconLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReconLineQuery();
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
		public bool Load(MedicationReconLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicationReconLine AddNew()
		{
			MedicationReconLine entity = base.AddNewEntity() as MedicationReconLine;

			return entity;
		}
		public MedicationReconLine FindByPrimaryKey(String registrationNo, Int32 reconSeqNo, Int64 medicationReceiveNo)
		{
			return base.FindByPrimaryKey(registrationNo, reconSeqNo, medicationReceiveNo) as MedicationReconLine;
		}

		#region IEnumerable< MedicationReconLine> Members

		IEnumerator<MedicationReconLine> IEnumerable<MedicationReconLine>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicationReconLine;
			}
		}

		#endregion

		private MedicationReconLineQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicationReconLine' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicationReconLine ({RegistrationNo, ReconSeqNo, MedicationReceiveNo})")]
	[Serializable]
	public partial class MedicationReconLine : esMedicationReconLine
	{
		public MedicationReconLine()
		{
		}

		public MedicationReconLine(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReconLineMetadata.Meta();
			}
		}

		override protected esMedicationReconLineQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReconLineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicationReconLineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReconLineQuery();
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
		public bool Load(MedicationReconLineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicationReconLineQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicationReconLineQuery : esMedicationReconLineQuery
	{
		public MedicationReconLineQuery()
		{

		}

		public MedicationReconLineQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicationReconLineQuery";
		}
	}

	[Serializable]
	public partial class MedicationReconLineMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicationReconLineMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.ReconSeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.ReconSeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.MedicationReceiveNo, 2, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.MedicationReceiveNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.ReconStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.ReconStatus;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.SRConsumeMethod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.SRConsumeMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.ConsumeQty, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.ConsumeQty;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.SRConsumeUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.SRConsumeUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.SRMedicationConsume, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.SRMedicationConsume;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.IsApprove, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.IsApprove;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.ApproveDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.ApproveDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.ApproveByParamedicID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.ApproveByParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReconLineMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReconLineMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicationReconLineMetadata Meta()
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
			public const string ReconSeqNo = "ReconSeqNo";
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string ReconStatus = "ReconStatus";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string SRMedicationConsume = "SRMedicationConsume";
			public const string IsApprove = "IsApprove";
			public const string ApproveDateTime = "ApproveDateTime";
			public const string ApproveByParamedicID = "ApproveByParamedicID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string ReconSeqNo = "ReconSeqNo";
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string ReconStatus = "ReconStatus";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string SRMedicationConsume = "SRMedicationConsume";
			public const string IsApprove = "IsApprove";
			public const string ApproveDateTime = "ApproveDateTime";
			public const string ApproveByParamedicID = "ApproveByParamedicID";
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
			lock (typeof(MedicationReconLineMetadata))
			{
				if (MedicationReconLineMetadata.mapDelegates == null)
				{
					MedicationReconLineMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicationReconLineMetadata.meta == null)
				{
					MedicationReconLineMetadata.meta = new MedicationReconLineMetadata();
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
				meta.AddTypeMap("ReconSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ReconStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsumeQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicationConsume", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApproveDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApproveByParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicationReconLine";
				meta.Destination = "MedicationReconLine";
				meta.spInsert = "proc_MedicationReconLineInsert";
				meta.spUpdate = "proc_MedicationReconLineUpdate";
				meta.spDelete = "proc_MedicationReconLineDelete";
				meta.spLoadAll = "proc_MedicationReconLineLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicationReconLineLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicationReconLineMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
