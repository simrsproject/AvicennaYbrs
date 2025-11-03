/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/30/2020 9:27:20 PM
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
	abstract public class esEmployeeOvertimeCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeOvertimeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeOvertimeCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeOvertimeQuery query)
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
			this.InitQuery(query as esEmployeeOvertimeQuery);
		}
		#endregion

		virtual public EmployeeOvertime DetachEntity(EmployeeOvertime entity)
		{
			return base.DetachEntity(entity) as EmployeeOvertime;
		}

		virtual public EmployeeOvertime AttachEntity(EmployeeOvertime entity)
		{
			return base.AttachEntity(entity) as EmployeeOvertime;
		}

		virtual public void Combine(EmployeeOvertimeCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeOvertime this[int index]
		{
			get
			{
				return base[index] as EmployeeOvertime;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeOvertime);
		}
	}

	[Serializable]
	abstract public class esEmployeeOvertime : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeOvertimeQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeOvertime()
		{
		}

		public esEmployeeOvertime(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esEmployeeOvertimeQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;
						case "SupervisorID": this.str.SupervisorID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsValidated": this.str.IsValidated = (string)value; break;
						case "ValidatedDateTime": this.str.ValidatedDateTime = (string)value; break;
						case "ValidatedByUserID": this.str.ValidatedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "PayrollPeriodID":

							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						case "SupervisorID":

							if (value == null || value is System.Int32)
								this.SupervisorID = (System.Int32?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsValidated":

							if (value == null || value is System.Boolean)
								this.IsValidated = (System.Boolean?)value;
							break;
						case "ValidatedDateTime":

							if (value == null || value is System.DateTime)
								this.ValidatedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeOvertime.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeOvertimeMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeOvertimeMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOvertimeMetadata.ColumnNames.PayrollPeriodID);
			}

			set
			{
				base.SetSystemInt32(EmployeeOvertimeMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.SupervisorID
		/// </summary>
		virtual public System.Int32? SupervisorID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOvertimeMetadata.ColumnNames.SupervisorID);
			}

			set
			{
				base.SetSystemInt32(EmployeeOvertimeMetadata.ColumnNames.SupervisorID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOvertimeMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeOvertimeMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOvertimeMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeOvertimeMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.IsValidated
		/// </summary>
		virtual public System.Boolean? IsValidated
		{
			get
			{
				return base.GetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsValidated);
			}

			set
			{
				base.SetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsValidated, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.ValidatedDateTime
		/// </summary>
		virtual public System.DateTime? ValidatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.ValidatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.ValidatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.ValidatedByUserID
		/// </summary>
		virtual public System.String ValidatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOvertimeMetadata.ColumnNames.ValidatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeOvertimeMetadata.ColumnNames.ValidatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOvertimeMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeOvertimeMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(EmployeeOvertimeMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOvertimeMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeOvertimeMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOvertimeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOvertime.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOvertimeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeOvertimeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeOvertime entity)
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
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String PayrollPeriodID
			{
				get
				{
					System.Int32? data = entity.PayrollPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
					else entity.PayrollPeriodID = Convert.ToInt32(value);
				}
			}
			public System.String SupervisorID
			{
				get
				{
					System.Int32? data = entity.SupervisorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorID = null;
					else entity.SupervisorID = Convert.ToInt32(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsValidated
			{
				get
				{
					System.Boolean? data = entity.IsValidated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValidated = null;
					else entity.IsValidated = Convert.ToBoolean(value);
				}
			}
			public System.String ValidatedDateTime
			{
				get
				{
					System.DateTime? data = entity.ValidatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedDateTime = null;
					else entity.ValidatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ValidatedByUserID
			{
				get
				{
					System.String data = entity.ValidatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidatedByUserID = null;
					else entity.ValidatedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVerified
			{
				get
				{
					System.Boolean? data = entity.IsVerified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerified = null;
					else entity.IsVerified = Convert.ToBoolean(value);
				}
			}
			public System.String VerifiedDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
					else entity.VerifiedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerifiedByUserID
			{
				get
				{
					System.String data = entity.VerifiedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
					else entity.VerifiedByUserID = Convert.ToString(value);
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
			private esEmployeeOvertime entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeOvertimeQuery query)
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
				throw new Exception("esEmployeeOvertime can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeOvertime : esEmployeeOvertime
	{
	}

	[Serializable]
	abstract public class esEmployeeOvertimeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeOvertimeMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		}

		public esQueryItem SupervisorID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.SupervisorID, esSystemType.Int32);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsValidated
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.IsValidated, esSystemType.Boolean);
			}
		}

		public esQueryItem ValidatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.ValidatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.ValidatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOvertimeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeOvertimeCollection")]
	public partial class EmployeeOvertimeCollection : esEmployeeOvertimeCollection, IEnumerable<EmployeeOvertime>
	{
		public EmployeeOvertimeCollection()
		{

		}

		public static implicit operator List<EmployeeOvertime>(EmployeeOvertimeCollection coll)
		{
			List<EmployeeOvertime> list = new List<EmployeeOvertime>();

			foreach (EmployeeOvertime emp in coll)
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
				return EmployeeOvertimeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeOvertimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeOvertime(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeOvertime();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeOvertimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeOvertimeQuery();
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
		public bool Load(EmployeeOvertimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeOvertime AddNew()
		{
			EmployeeOvertime entity = base.AddNewEntity() as EmployeeOvertime;

			return entity;
		}
		public EmployeeOvertime FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as EmployeeOvertime;
		}

		#region IEnumerable< EmployeeOvertime> Members

		IEnumerator<EmployeeOvertime> IEnumerable<EmployeeOvertime>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeOvertime;
			}
		}

		#endregion

		private EmployeeOvertimeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeOvertime' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeOvertime ({TransactionNo})")]
	[Serializable]
	public partial class EmployeeOvertime : esEmployeeOvertime
	{
		public EmployeeOvertime()
		{
		}

		public EmployeeOvertime(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeOvertimeMetadata.Meta();
			}
		}

		override protected esEmployeeOvertimeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeOvertimeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeOvertimeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeOvertimeQuery();
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
		public bool Load(EmployeeOvertimeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeOvertimeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeOvertimeQuery : esEmployeeOvertimeQuery
	{
		public EmployeeOvertimeQuery()
		{

		}

		public EmployeeOvertimeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeOvertimeQuery";
		}
	}

	[Serializable]
	public partial class EmployeeOvertimeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeOvertimeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.PayrollPeriodID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.SupervisorID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.SupervisorID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.ApprovedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.IsValidated, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.IsValidated;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.ValidatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.ValidatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.ValidatedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.ValidatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.VoidDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.IsVerified, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.VerifiedDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.VerifiedByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOvertimeMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOvertimeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeOvertimeMetadata Meta()
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
			public const string TransactionDate = "TransactionDate";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string SupervisorID = "SupervisorID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsValidated = "IsValidated";
			public const string ValidatedDateTime = "ValidatedDateTime";
			public const string ValidatedByUserID = "ValidatedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string SupervisorID = "SupervisorID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsValidated = "IsValidated";
			public const string ValidatedDateTime = "ValidatedDateTime";
			public const string ValidatedByUserID = "ValidatedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
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
			lock (typeof(EmployeeOvertimeMetadata))
			{
				if (EmployeeOvertimeMetadata.mapDelegates == null)
				{
					EmployeeOvertimeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeOvertimeMetadata.meta == null)
				{
					EmployeeOvertimeMetadata.meta = new EmployeeOvertimeMetadata();
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
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SupervisorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsValidated", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeOvertime";
				meta.Destination = "EmployeeOvertime";
				meta.spInsert = "proc_EmployeeOvertimeInsert";
				meta.spUpdate = "proc_EmployeeOvertimeUpdate";
				meta.spDelete = "proc_EmployeeOvertimeDelete";
				meta.spLoadAll = "proc_EmployeeOvertimeLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeOvertimeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeOvertimeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
