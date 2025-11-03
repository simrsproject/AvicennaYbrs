/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/10/2022 3:25:07 PM
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
	abstract public class esCssdSterilizationProcessCollection : esEntityCollectionWAuditLog
	{
		public esCssdSterilizationProcessCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdSterilizationProcessCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdSterilizationProcessQuery query)
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
			this.InitQuery(query as esCssdSterilizationProcessQuery);
		}
		#endregion

		virtual public CssdSterilizationProcess DetachEntity(CssdSterilizationProcess entity)
		{
			return base.DetachEntity(entity) as CssdSterilizationProcess;
		}

		virtual public CssdSterilizationProcess AttachEntity(CssdSterilizationProcess entity)
		{
			return base.AttachEntity(entity) as CssdSterilizationProcess;
		}

		virtual public void Combine(CssdSterilizationProcessCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdSterilizationProcess this[int index]
		{
			get
			{
				return base[index] as CssdSterilizationProcess;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdSterilizationProcess);
		}
	}

	[Serializable]
	abstract public class esCssdSterilizationProcess : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdSterilizationProcessQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdSterilizationProcess()
		{
		}

		public esCssdSterilizationProcess(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String processNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String processNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(processNo);
			else
				return LoadByPrimaryKeyStoredProcedure(processNo);
		}

		private bool LoadByPrimaryKeyDynamic(String processNo)
		{
			esCssdSterilizationProcessQuery query = this.GetDynamicQuery();
			query.Where(query.ProcessNo == processNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String processNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ProcessNo", processNo);
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
						case "ProcessNo": this.str.ProcessNo = (string)value; break;
						case "ProcessDate": this.str.ProcessDate = (string)value; break;
						case "ProcessStartTime": this.str.ProcessStartTime = (string)value; break;
						case "ProcessEndTime": this.str.ProcessEndTime = (string)value; break;
						case "MachineID": this.str.MachineID = (string)value; break;
						case "SRCssdProcessType": this.str.SRCssdProcessType = (string)value; break;
						case "OperatorByUserID": this.str.OperatorByUserID = (string)value; break;
						case "ProcessTo": this.str.ProcessTo = (string)value; break;
						case "IsDtt": this.str.IsDtt = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ProcessDate":

							if (value == null || value is System.DateTime)
								this.ProcessDate = (System.DateTime?)value;
							break;
						case "IsDtt":

							if (value == null || value is System.Boolean)
								this.IsDtt = (System.Boolean?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ExpiredDate":

							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
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
		/// Maps to CssdSterilizationProcess.ProcessNo
		/// </summary>
		virtual public System.String ProcessNo
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessNo);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.ProcessDate
		/// </summary>
		virtual public System.DateTime? ProcessDate
		{
			get
			{
				return base.GetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.ProcessDate);
			}

			set
			{
				base.SetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.ProcessDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.ProcessStartTime
		/// </summary>
		virtual public System.String ProcessStartTime
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessStartTime);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessStartTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.ProcessEndTime
		/// </summary>
		virtual public System.String ProcessEndTime
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessEndTime);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessEndTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.MachineID
		/// </summary>
		virtual public System.String MachineID
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.MachineID);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.MachineID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.SRCssdProcessType
		/// </summary>
		virtual public System.String SRCssdProcessType
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.SRCssdProcessType);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.SRCssdProcessType, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.OperatorByUserID
		/// </summary>
		virtual public System.String OperatorByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.OperatorByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.OperatorByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.ProcessTo
		/// </summary>
		virtual public System.String ProcessTo
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessTo);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ProcessTo, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.IsDtt
		/// </summary>
		virtual public System.Boolean? IsDtt
		{
			get
			{
				return base.GetSystemBoolean(CssdSterilizationProcessMetadata.ColumnNames.IsDtt);
			}

			set
			{
				base.SetSystemBoolean(CssdSterilizationProcessMetadata.ColumnNames.IsDtt, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CssdSterilizationProcessMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CssdSterilizationProcessMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CssdSterilizationProcessMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CssdSterilizationProcessMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdSterilizationProcessMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdSterilizationProcessMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdSterilizationProcess.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.ExpiredDate);
			}

			set
			{
				base.SetSystemDateTime(CssdSterilizationProcessMetadata.ColumnNames.ExpiredDate, value);
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
			public esStrings(esCssdSterilizationProcess entity)
			{
				this.entity = entity;
			}
			public System.String ProcessNo
			{
				get
				{
					System.String data = entity.ProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessNo = null;
					else entity.ProcessNo = Convert.ToString(value);
				}
			}
			public System.String ProcessDate
			{
				get
				{
					System.DateTime? data = entity.ProcessDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessDate = null;
					else entity.ProcessDate = Convert.ToDateTime(value);
				}
			}
			public System.String ProcessStartTime
			{
				get
				{
					System.String data = entity.ProcessStartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessStartTime = null;
					else entity.ProcessStartTime = Convert.ToString(value);
				}
			}
			public System.String ProcessEndTime
			{
				get
				{
					System.String data = entity.ProcessEndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessEndTime = null;
					else entity.ProcessEndTime = Convert.ToString(value);
				}
			}
			public System.String MachineID
			{
				get
				{
					System.String data = entity.MachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MachineID = null;
					else entity.MachineID = Convert.ToString(value);
				}
			}
			public System.String SRCssdProcessType
			{
				get
				{
					System.String data = entity.SRCssdProcessType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCssdProcessType = null;
					else entity.SRCssdProcessType = Convert.ToString(value);
				}
			}
			public System.String OperatorByUserID
			{
				get
				{
					System.String data = entity.OperatorByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperatorByUserID = null;
					else entity.OperatorByUserID = Convert.ToString(value);
				}
			}
			public System.String ProcessTo
			{
				get
				{
					System.String data = entity.ProcessTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessTo = null;
					else entity.ProcessTo = Convert.ToString(value);
				}
			}
			public System.String IsDtt
			{
				get
				{
					System.Boolean? data = entity.IsDtt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDtt = null;
					else entity.IsDtt = Convert.ToBoolean(value);
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
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
				}
			}
			private esCssdSterilizationProcess entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdSterilizationProcessQuery query)
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
				throw new Exception("esCssdSterilizationProcess can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdSterilizationProcess : esCssdSterilizationProcess
	{
	}

	[Serializable]
	abstract public class esCssdSterilizationProcessQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdSterilizationProcessMetadata.Meta();
			}
		}

		public esQueryItem ProcessNo
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ProcessNo, esSystemType.String);
			}
		}

		public esQueryItem ProcessDate
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ProcessDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ProcessStartTime
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ProcessStartTime, esSystemType.String);
			}
		}

		public esQueryItem ProcessEndTime
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ProcessEndTime, esSystemType.String);
			}
		}

		public esQueryItem MachineID
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.MachineID, esSystemType.String);
			}
		}

		public esQueryItem SRCssdProcessType
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.SRCssdProcessType, esSystemType.String);
			}
		}

		public esQueryItem OperatorByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.OperatorByUserID, esSystemType.String);
			}
		}

		public esQueryItem ProcessTo
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ProcessTo, esSystemType.String);
			}
		}

		public esQueryItem IsDtt
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.IsDtt, esSystemType.Boolean);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, CssdSterilizationProcessMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdSterilizationProcessCollection")]
	public partial class CssdSterilizationProcessCollection : esCssdSterilizationProcessCollection, IEnumerable<CssdSterilizationProcess>
	{
		public CssdSterilizationProcessCollection()
		{

		}

		public static implicit operator List<CssdSterilizationProcess>(CssdSterilizationProcessCollection coll)
		{
			List<CssdSterilizationProcess> list = new List<CssdSterilizationProcess>();

			foreach (CssdSterilizationProcess emp in coll)
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
				return CssdSterilizationProcessMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterilizationProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdSterilizationProcess(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdSterilizationProcess();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdSterilizationProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterilizationProcessQuery();
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
		public bool Load(CssdSterilizationProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdSterilizationProcess AddNew()
		{
			CssdSterilizationProcess entity = base.AddNewEntity() as CssdSterilizationProcess;

			return entity;
		}
		public CssdSterilizationProcess FindByPrimaryKey(String processNo)
		{
			return base.FindByPrimaryKey(processNo) as CssdSterilizationProcess;
		}

		#region IEnumerable< CssdSterilizationProcess> Members

		IEnumerator<CssdSterilizationProcess> IEnumerable<CssdSterilizationProcess>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdSterilizationProcess;
			}
		}

		#endregion

		private CssdSterilizationProcessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdSterilizationProcess' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdSterilizationProcess ({ProcessNo})")]
	[Serializable]
	public partial class CssdSterilizationProcess : esCssdSterilizationProcess
	{
		public CssdSterilizationProcess()
		{
		}

		public CssdSterilizationProcess(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdSterilizationProcessMetadata.Meta();
			}
		}

		override protected esCssdSterilizationProcessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdSterilizationProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdSterilizationProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdSterilizationProcessQuery();
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
		public bool Load(CssdSterilizationProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdSterilizationProcessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdSterilizationProcessQuery : esCssdSterilizationProcessQuery
	{
		public CssdSterilizationProcessQuery()
		{

		}

		public CssdSterilizationProcessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdSterilizationProcessQuery";
		}
	}

	[Serializable]
	public partial class CssdSterilizationProcessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdSterilizationProcessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ProcessDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ProcessDate;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ProcessStartTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ProcessStartTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ProcessEndTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ProcessEndTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.MachineID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.MachineID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.SRCssdProcessType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.SRCssdProcessType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.OperatorByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.OperatorByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ProcessTo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ProcessTo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.IsDtt, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.IsDtt;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.IsApproved, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ApprovedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ApprovedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.IsVoid, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.VoidDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdSterilizationProcessMetadata.ColumnNames.ExpiredDate, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdSterilizationProcessMetadata.PropertyNames.ExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdSterilizationProcessMetadata Meta()
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
			public const string ProcessNo = "ProcessNo";
			public const string ProcessDate = "ProcessDate";
			public const string ProcessStartTime = "ProcessStartTime";
			public const string ProcessEndTime = "ProcessEndTime";
			public const string MachineID = "MachineID";
			public const string SRCssdProcessType = "SRCssdProcessType";
			public const string OperatorByUserID = "OperatorByUserID";
			public const string ProcessTo = "ProcessTo";
			public const string IsDtt = "IsDtt";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ExpiredDate = "ExpiredDate";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ProcessNo = "ProcessNo";
			public const string ProcessDate = "ProcessDate";
			public const string ProcessStartTime = "ProcessStartTime";
			public const string ProcessEndTime = "ProcessEndTime";
			public const string MachineID = "MachineID";
			public const string SRCssdProcessType = "SRCssdProcessType";
			public const string OperatorByUserID = "OperatorByUserID";
			public const string ProcessTo = "ProcessTo";
			public const string IsDtt = "IsDtt";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ExpiredDate = "ExpiredDate";
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
			lock (typeof(CssdSterilizationProcessMetadata))
			{
				if (CssdSterilizationProcessMetadata.mapDelegates == null)
				{
					CssdSterilizationProcessMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdSterilizationProcessMetadata.meta == null)
				{
					CssdSterilizationProcessMetadata.meta = new CssdSterilizationProcessMetadata();
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

				meta.AddTypeMap("ProcessNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ProcessStartTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessEndTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCssdProcessType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OperatorByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDtt", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "CssdSterilizationProcess";
				meta.Destination = "CssdSterilizationProcess";
				meta.spInsert = "proc_CssdSterilizationProcessInsert";
				meta.spUpdate = "proc_CssdSterilizationProcessUpdate";
				meta.spDelete = "proc_CssdSterilizationProcessDelete";
				meta.spLoadAll = "proc_CssdSterilizationProcessLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdSterilizationProcessLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdSterilizationProcessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
