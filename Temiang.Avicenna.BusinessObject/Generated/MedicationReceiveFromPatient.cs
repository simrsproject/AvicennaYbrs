/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/22/2022 10:41:48 AM
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
	abstract public class esMedicationReceiveFromPatientCollection : esEntityCollectionWAuditLog
	{
		public esMedicationReceiveFromPatientCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicationReceiveFromPatientCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicationReceiveFromPatientQuery query)
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
			this.InitQuery(query as esMedicationReceiveFromPatientQuery);
		}
		#endregion

		virtual public MedicationReceiveFromPatient DetachEntity(MedicationReceiveFromPatient entity)
		{
			return base.DetachEntity(entity) as MedicationReceiveFromPatient;
		}

		virtual public MedicationReceiveFromPatient AttachEntity(MedicationReceiveFromPatient entity)
		{
			return base.AttachEntity(entity) as MedicationReceiveFromPatient;
		}

		virtual public void Combine(MedicationReceiveFromPatientCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicationReceiveFromPatient this[int index]
		{
			get
			{
				return base[index] as MedicationReceiveFromPatient;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicationReceiveFromPatient);
		}
	}

	[Serializable]
	abstract public class esMedicationReceiveFromPatient : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicationReceiveFromPatientQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicationReceiveFromPatient()
		{
		}

		public esMedicationReceiveFromPatient(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 medicationReceiveNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medicationReceiveNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 medicationReceiveNo)
		{
			esMedicationReceiveFromPatientQuery query = this.GetDynamicQuery();
			query.Where(query.MedicationReceiveNo == medicationReceiveNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 medicationReceiveNo)
		{
			esParameters parms = new esParameters();
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
						case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
						case "LastConsumeDateTime": this.str.LastConsumeDateTime = (string)value; break;
						case "Condition": this.str.Condition = (string)value; break;
						case "ExpireDate": this.str.ExpireDate = (string)value; break;
						case "ApprovedByParamedicID": this.str.ApprovedByParamedicID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Reason": this.str.Reason = (string)value; break;
						case "Duration": this.str.Duration = (string)value; break;
						case "Temp": this.str.Temp = (string)value; break;
						case "BeyondUseDate": this.str.BeyondUseDate = (string)value; break;
						case "RegPom": this.str.RegPom = (string)value; break;
						case "IsAppropriate": this.str.IsAppropriate = (string)value; break;
						case "IsManagedByPatient": this.str.IsManagedByPatient = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MedicationReceiveNo":

							if (value == null || value is System.Int64)
								this.MedicationReceiveNo = (System.Int64?)value;
							break;
						case "LastConsumeDateTime":

							if (value == null || value is System.DateTime)
								this.LastConsumeDateTime = (System.DateTime?)value;
							break;
						case "ExpireDate":

							if (value == null || value is System.DateTime)
								this.ExpireDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "BeyondUseDate":

							if (value == null || value is System.DateTime)
								this.BeyondUseDate = (System.DateTime?)value;
							break;
						case "IsAppropriate":

							if (value == null || value is System.Boolean)
								this.IsAppropriate = (System.Boolean?)value;
							break;
						case "IsManagedByPatient":

							if (value == null || value is System.Boolean)
								this.IsManagedByPatient = (System.Boolean?)value;
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
		/// Maps to MedicationReceiveFromPatient.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(MedicationReceiveFromPatientMetadata.ColumnNames.MedicationReceiveNo);
			}

			set
			{
				base.SetSystemInt64(MedicationReceiveFromPatientMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.LastConsumeDateTime
		/// </summary>
		virtual public System.DateTime? LastConsumeDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.LastConsumeDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.LastConsumeDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.Condition
		/// </summary>
		virtual public System.String Condition
		{
			get
			{
				return base.GetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Condition);
			}

			set
			{
				base.SetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Condition, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.ExpireDate
		/// </summary>
		virtual public System.DateTime? ExpireDate
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.ExpireDate);
			}

			set
			{
				base.SetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.ExpireDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.ApprovedByParamedicID
		/// </summary>
		virtual public System.String ApprovedByParamedicID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.ApprovedByParamedicID);
			}

			set
			{
				base.SetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.ApprovedByParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Reason);
			}

			set
			{
				base.SetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Reason, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.Duration
		/// </summary>
		virtual public System.String Duration
		{
			get
			{
				return base.GetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Duration);
			}

			set
			{
				base.SetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Duration, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.Temp
		/// </summary>
		virtual public System.String Temp
		{
			get
			{
				return base.GetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Temp);
			}

			set
			{
				base.SetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.Temp, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.BeyondUseDate
		/// </summary>
		virtual public System.DateTime? BeyondUseDate
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.BeyondUseDate);
			}

			set
			{
				base.SetSystemDateTime(MedicationReceiveFromPatientMetadata.ColumnNames.BeyondUseDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.RegPom
		/// </summary>
		virtual public System.String RegPom
		{
			get
			{
				return base.GetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.RegPom);
			}

			set
			{
				base.SetSystemString(MedicationReceiveFromPatientMetadata.ColumnNames.RegPom, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.IsAppropriate
		/// </summary>
		virtual public System.Boolean? IsAppropriate
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceiveFromPatientMetadata.ColumnNames.IsAppropriate);
			}

			set
			{
				base.SetSystemBoolean(MedicationReceiveFromPatientMetadata.ColumnNames.IsAppropriate, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveFromPatient.IsManagedByPatient
		/// </summary>
		virtual public System.Boolean? IsManagedByPatient
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceiveFromPatientMetadata.ColumnNames.IsManagedByPatient);
			}

			set
			{
				base.SetSystemBoolean(MedicationReceiveFromPatientMetadata.ColumnNames.IsManagedByPatient, value);
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
			public esStrings(esMedicationReceiveFromPatient entity)
			{
				this.entity = entity;
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
			public System.String LastConsumeDateTime
			{
				get
				{
					System.DateTime? data = entity.LastConsumeDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastConsumeDateTime = null;
					else entity.LastConsumeDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Condition
			{
				get
				{
					System.String data = entity.Condition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Condition = null;
					else entity.Condition = Convert.ToString(value);
				}
			}
			public System.String ExpireDate
			{
				get
				{
					System.DateTime? data = entity.ExpireDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpireDate = null;
					else entity.ExpireDate = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByParamedicID
			{
				get
				{
					System.String data = entity.ApprovedByParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByParamedicID = null;
					else entity.ApprovedByParamedicID = Convert.ToString(value);
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
			public System.String Reason
			{
				get
				{
					System.String data = entity.Reason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reason = null;
					else entity.Reason = Convert.ToString(value);
				}
			}
			public System.String Duration
			{
				get
				{
					System.String data = entity.Duration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Duration = null;
					else entity.Duration = Convert.ToString(value);
				}
			}
			public System.String Temp
			{
				get
				{
					System.String data = entity.Temp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temp = null;
					else entity.Temp = Convert.ToString(value);
				}
			}
			public System.String BeyondUseDate
			{
				get
				{
					System.DateTime? data = entity.BeyondUseDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BeyondUseDate = null;
					else entity.BeyondUseDate = Convert.ToDateTime(value);
				}
			}
			public System.String RegPom
			{
				get
				{
					System.String data = entity.RegPom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegPom = null;
					else entity.RegPom = Convert.ToString(value);
				}
			}
			public System.String IsAppropriate
			{
				get
				{
					System.Boolean? data = entity.IsAppropriate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAppropriate = null;
					else entity.IsAppropriate = Convert.ToBoolean(value);
				}
			}
			public System.String IsManagedByPatient
			{
				get
				{
					System.Boolean? data = entity.IsManagedByPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsManagedByPatient = null;
					else entity.IsManagedByPatient = Convert.ToBoolean(value);
				}
			}
			private esMedicationReceiveFromPatient entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicationReceiveFromPatientQuery query)
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
				throw new Exception("esMedicationReceiveFromPatient can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicationReceiveFromPatient : esMedicationReceiveFromPatient
	{
	}

	[Serializable]
	abstract public class esMedicationReceiveFromPatientQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceiveFromPatientMetadata.Meta();
			}
		}

		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		}

		public esQueryItem LastConsumeDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.LastConsumeDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem Condition
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.Condition, esSystemType.String);
			}
		}

		public esQueryItem ExpireDate
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.ExpireDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByParamedicID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.ApprovedByParamedicID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.Reason, esSystemType.String);
			}
		}

		public esQueryItem Duration
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.Duration, esSystemType.String);
			}
		}

		public esQueryItem Temp
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.Temp, esSystemType.String);
			}
		}

		public esQueryItem BeyondUseDate
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.BeyondUseDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RegPom
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.RegPom, esSystemType.String);
			}
		}

		public esQueryItem IsAppropriate
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.IsAppropriate, esSystemType.Boolean);
			}
		}

		public esQueryItem IsManagedByPatient
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveFromPatientMetadata.ColumnNames.IsManagedByPatient, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicationReceiveFromPatientCollection")]
	public partial class MedicationReceiveFromPatientCollection : esMedicationReceiveFromPatientCollection, IEnumerable<MedicationReceiveFromPatient>
	{
		public MedicationReceiveFromPatientCollection()
		{

		}

		public static implicit operator List<MedicationReceiveFromPatient>(MedicationReceiveFromPatientCollection coll)
		{
			List<MedicationReceiveFromPatient> list = new List<MedicationReceiveFromPatient>();

			foreach (MedicationReceiveFromPatient emp in coll)
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
				return MedicationReceiveFromPatientMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceiveFromPatientQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicationReceiveFromPatient(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicationReceiveFromPatient();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicationReceiveFromPatientQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceiveFromPatientQuery();
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
		public bool Load(MedicationReceiveFromPatientQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicationReceiveFromPatient AddNew()
		{
			MedicationReceiveFromPatient entity = base.AddNewEntity() as MedicationReceiveFromPatient;

			return entity;
		}
		public MedicationReceiveFromPatient FindByPrimaryKey(Int64 medicationReceiveNo)
		{
			return base.FindByPrimaryKey(medicationReceiveNo) as MedicationReceiveFromPatient;
		}

		#region IEnumerable< MedicationReceiveFromPatient> Members

		IEnumerator<MedicationReceiveFromPatient> IEnumerable<MedicationReceiveFromPatient>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicationReceiveFromPatient;
			}
		}

		#endregion

		private MedicationReceiveFromPatientQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicationReceiveFromPatient' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicationReceiveFromPatient ({MedicationReceiveNo})")]
	[Serializable]
	public partial class MedicationReceiveFromPatient : esMedicationReceiveFromPatient
	{
		public MedicationReceiveFromPatient()
		{
		}

		public MedicationReceiveFromPatient(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceiveFromPatientMetadata.Meta();
			}
		}

		override protected esMedicationReceiveFromPatientQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceiveFromPatientQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicationReceiveFromPatientQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceiveFromPatientQuery();
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
		public bool Load(MedicationReceiveFromPatientQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicationReceiveFromPatientQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicationReceiveFromPatientQuery : esMedicationReceiveFromPatientQuery
	{
		public MedicationReceiveFromPatientQuery()
		{

		}

		public MedicationReceiveFromPatientQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicationReceiveFromPatientQuery";
		}
	}

	[Serializable]
	public partial class MedicationReceiveFromPatientMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicationReceiveFromPatientMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.MedicationReceiveNo, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.MedicationReceiveNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.LastConsumeDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.LastConsumeDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.Condition, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.Condition;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.ExpireDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.ExpireDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.ApprovedByParamedicID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.ApprovedByParamedicID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.Reason, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.Duration, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.Duration;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.Temp, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.Temp;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.BeyondUseDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.BeyondUseDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.RegPom, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.RegPom;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.IsAppropriate, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.IsAppropriate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveFromPatientMetadata.ColumnNames.IsManagedByPatient, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceiveFromPatientMetadata.PropertyNames.IsManagedByPatient;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicationReceiveFromPatientMetadata Meta()
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
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string LastConsumeDateTime = "LastConsumeDateTime";
			public const string Condition = "Condition";
			public const string ExpireDate = "ExpireDate";
			public const string ApprovedByParamedicID = "ApprovedByParamedicID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Reason = "Reason";
			public const string Duration = "Duration";
			public const string Temp = "Temp";
			public const string BeyondUseDate = "BeyondUseDate";
			public const string RegPom = "RegPom";
			public const string IsAppropriate = "IsAppropriate";
			public const string IsManagedByPatient = "IsManagedByPatient";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string LastConsumeDateTime = "LastConsumeDateTime";
			public const string Condition = "Condition";
			public const string ExpireDate = "ExpireDate";
			public const string ApprovedByParamedicID = "ApprovedByParamedicID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Reason = "Reason";
			public const string Duration = "Duration";
			public const string Temp = "Temp";
			public const string BeyondUseDate = "BeyondUseDate";
			public const string RegPom = "RegPom";
			public const string IsAppropriate = "IsAppropriate";
			public const string IsManagedByPatient = "IsManagedByPatient";
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
			lock (typeof(MedicationReceiveFromPatientMetadata))
			{
				if (MedicationReceiveFromPatientMetadata.mapDelegates == null)
				{
					MedicationReceiveFromPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicationReceiveFromPatientMetadata.meta == null)
				{
					MedicationReceiveFromPatientMetadata.meta = new MedicationReceiveFromPatientMetadata();
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

				meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("LastConsumeDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Condition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpireDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Duration", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Temp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BeyondUseDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegPom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAppropriate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsManagedByPatient", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "MedicationReceiveFromPatient";
				meta.Destination = "MedicationReceiveFromPatient";
				meta.spInsert = "proc_MedicationReceiveFromPatientInsert";
				meta.spUpdate = "proc_MedicationReceiveFromPatientUpdate";
				meta.spDelete = "proc_MedicationReceiveFromPatientDelete";
				meta.spLoadAll = "proc_MedicationReceiveFromPatientLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicationReceiveFromPatientLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicationReceiveFromPatientMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
