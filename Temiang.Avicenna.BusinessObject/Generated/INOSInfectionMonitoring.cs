/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/28/2021 2:28:21 PM
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
	abstract public class esINOSInfectionMonitoringCollection : esEntityCollectionWAuditLog
	{
		public esINOSInfectionMonitoringCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "INOSInfectionMonitoringCollection";
		}

		#region Query Logic
		protected void InitQuery(esINOSInfectionMonitoringQuery query)
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
			this.InitQuery(query as esINOSInfectionMonitoringQuery);
		}
		#endregion

		virtual public INOSInfectionMonitoring DetachEntity(INOSInfectionMonitoring entity)
		{
			return base.DetachEntity(entity) as INOSInfectionMonitoring;
		}

		virtual public INOSInfectionMonitoring AttachEntity(INOSInfectionMonitoring entity)
		{
			return base.AttachEntity(entity) as INOSInfectionMonitoring;
		}

		virtual public void Combine(INOSInfectionMonitoringCollection collection)
		{
			base.Combine(collection);
		}

		new public INOSInfectionMonitoring this[int index]
		{
			get
			{
				return base[index] as INOSInfectionMonitoring;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(INOSInfectionMonitoring);
		}
	}

	[Serializable]
	abstract public class esINOSInfectionMonitoring : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esINOSInfectionMonitoringQuery GetDynamicQuery()
		{
			return null;
		}

		public esINOSInfectionMonitoring()
		{
		}

		public esINOSInfectionMonitoring(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 monitoringID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monitoringID);
			else
				return LoadByPrimaryKeyStoredProcedure(monitoringID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 monitoringID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monitoringID);
			else
				return LoadByPrimaryKeyStoredProcedure(monitoringID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 monitoringID)
		{
			esINOSInfectionMonitoringQuery query = this.GetDynamicQuery();
			query.Where(query.MonitoringID == monitoringID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 monitoringID)
		{
			esParameters parms = new esParameters();
			parms.Add("MonitoringID", monitoringID);
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
						case "MonitoringID": this.str.MonitoringID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
						case "MonitoringDate": this.str.MonitoringDate = (string)value; break;
						case "IsMechanicalVentilator": this.str.IsMechanicalVentilator = (string)value; break;
						case "IsInpatient": this.str.IsInpatient = (string)value; break;
						case "IsUrineCatheter": this.str.IsUrineCatheter = (string)value; break;
						case "IsSurgery": this.str.IsSurgery = (string)value; break;
						case "IsCentralVeinLine": this.str.IsCentralVeinLine = (string)value; break;
						case "IsIntraVeinLine": this.str.IsIntraVeinLine = (string)value; break;
						case "IsTotalCare": this.str.IsTotalCare = (string)value; break;
						case "IsAntibioticDrugs": this.str.IsAntibioticDrugs = (string)value; break;
						case "IsVAP": this.str.IsVAP = (string)value; break;
						case "IsHAP": this.str.IsHAP = (string)value; break;
						case "IsISK": this.str.IsISK = (string)value; break;
						case "IsILO": this.str.IsILO = (string)value; break;
						case "IsIADP": this.str.IsIADP = (string)value; break;
						case "IsPhlebitis": this.str.IsPhlebitis = (string)value; break;
						case "IsDecubitus": this.str.IsDecubitus = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MonitoringID":

							if (value == null || value is System.Int64)
								this.MonitoringID = (System.Int64?)value;
							break;
						case "MonitoringDate":

							if (value == null || value is System.DateTime)
								this.MonitoringDate = (System.DateTime?)value;
							break;
						case "IsMechanicalVentilator":

							if (value == null || value is System.Boolean)
								this.IsMechanicalVentilator = (System.Boolean?)value;
							break;
						case "IsInpatient":

							if (value == null || value is System.Boolean)
								this.IsInpatient = (System.Boolean?)value;
							break;
						case "IsUrineCatheter":

							if (value == null || value is System.Boolean)
								this.IsUrineCatheter = (System.Boolean?)value;
							break;
						case "IsSurgery":

							if (value == null || value is System.Boolean)
								this.IsSurgery = (System.Boolean?)value;
							break;
						case "IsCentralVeinLine":

							if (value == null || value is System.Boolean)
								this.IsCentralVeinLine = (System.Boolean?)value;
							break;
						case "IsIntraVeinLine":

							if (value == null || value is System.Boolean)
								this.IsIntraVeinLine = (System.Boolean?)value;
							break;
						case "IsTotalCare":

							if (value == null || value is System.Boolean)
								this.IsTotalCare = (System.Boolean?)value;
							break;
						case "IsAntibioticDrugs":

							if (value == null || value is System.Boolean)
								this.IsAntibioticDrugs = (System.Boolean?)value;
							break;
						case "IsVAP":

							if (value == null || value is System.Boolean)
								this.IsVAP = (System.Boolean?)value;
							break;
						case "IsHAP":

							if (value == null || value is System.Boolean)
								this.IsHAP = (System.Boolean?)value;
							break;
						case "IsISK":

							if (value == null || value is System.Boolean)
								this.IsISK = (System.Boolean?)value;
							break;
						case "IsILO":

							if (value == null || value is System.Boolean)
								this.IsILO = (System.Boolean?)value;
							break;
						case "IsIADP":

							if (value == null || value is System.Boolean)
								this.IsIADP = (System.Boolean?)value;
							break;
						case "IsPhlebitis":

							if (value == null || value is System.Boolean)
								this.IsPhlebitis = (System.Boolean?)value;
							break;
						case "IsDecubitus":

							if (value == null || value is System.Boolean)
								this.IsDecubitus = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to INOSInfectionMonitoring.MonitoringID
		/// </summary>
		virtual public System.Int64? MonitoringID
		{
			get
			{
				return base.GetSystemInt64(INOSInfectionMonitoringMetadata.ColumnNames.MonitoringID);
			}

			set
			{
				base.SetSystemInt64(INOSInfectionMonitoringMetadata.ColumnNames.MonitoringID, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.MonitoringDate
		/// </summary>
		virtual public System.DateTime? MonitoringDate
		{
			get
			{
				return base.GetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.MonitoringDate);
			}

			set
			{
				base.SetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.MonitoringDate, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsMechanicalVentilator
		/// </summary>
		virtual public System.Boolean? IsMechanicalVentilator
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsMechanicalVentilator);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsMechanicalVentilator, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsInpatient
		/// </summary>
		virtual public System.Boolean? IsInpatient
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsInpatient);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsInpatient, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsUrineCatheter
		/// </summary>
		virtual public System.Boolean? IsUrineCatheter
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsUrineCatheter);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsUrineCatheter, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsSurgery
		/// </summary>
		virtual public System.Boolean? IsSurgery
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsSurgery);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsSurgery, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsCentralVeinLine
		/// </summary>
		virtual public System.Boolean? IsCentralVeinLine
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsCentralVeinLine);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsCentralVeinLine, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsIntraVeinLine
		/// </summary>
		virtual public System.Boolean? IsIntraVeinLine
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsIntraVeinLine);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsIntraVeinLine, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsTotalCare
		/// </summary>
		virtual public System.Boolean? IsTotalCare
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsTotalCare);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsTotalCare, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsAntibioticDrugs
		/// </summary>
		virtual public System.Boolean? IsAntibioticDrugs
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsAntibioticDrugs);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsAntibioticDrugs, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsVAP
		/// </summary>
		virtual public System.Boolean? IsVAP
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsVAP);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsVAP, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsHAP
		/// </summary>
		virtual public System.Boolean? IsHAP
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsHAP);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsHAP, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsISK
		/// </summary>
		virtual public System.Boolean? IsISK
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsISK);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsISK, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsILO
		/// </summary>
		virtual public System.Boolean? IsILO
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsILO);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsILO, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsIADP
		/// </summary>
		virtual public System.Boolean? IsIADP
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsIADP);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsIADP, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsPhlebitis
		/// </summary>
		virtual public System.Boolean? IsPhlebitis
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsPhlebitis);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsPhlebitis, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsDecubitus
		/// </summary>
		virtual public System.Boolean? IsDecubitus
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsDecubitus);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsDecubitus, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(INOSInfectionMonitoringMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to INOSInfectionMonitoring.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esINOSInfectionMonitoring entity)
			{
				this.entity = entity;
			}
			public System.String MonitoringID
			{
				get
				{
					System.Int64? data = entity.MonitoringID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonitoringID = null;
					else entity.MonitoringID = Convert.ToInt64(value);
				}
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
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			public System.String MonitoringDate
			{
				get
				{
					System.DateTime? data = entity.MonitoringDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonitoringDate = null;
					else entity.MonitoringDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsMechanicalVentilator
			{
				get
				{
					System.Boolean? data = entity.IsMechanicalVentilator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMechanicalVentilator = null;
					else entity.IsMechanicalVentilator = Convert.ToBoolean(value);
				}
			}
			public System.String IsInpatient
			{
				get
				{
					System.Boolean? data = entity.IsInpatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInpatient = null;
					else entity.IsInpatient = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrineCatheter
			{
				get
				{
					System.Boolean? data = entity.IsUrineCatheter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrineCatheter = null;
					else entity.IsUrineCatheter = Convert.ToBoolean(value);
				}
			}
			public System.String IsSurgery
			{
				get
				{
					System.Boolean? data = entity.IsSurgery;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSurgery = null;
					else entity.IsSurgery = Convert.ToBoolean(value);
				}
			}
			public System.String IsCentralVeinLine
			{
				get
				{
					System.Boolean? data = entity.IsCentralVeinLine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCentralVeinLine = null;
					else entity.IsCentralVeinLine = Convert.ToBoolean(value);
				}
			}
			public System.String IsIntraVeinLine
			{
				get
				{
					System.Boolean? data = entity.IsIntraVeinLine;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIntraVeinLine = null;
					else entity.IsIntraVeinLine = Convert.ToBoolean(value);
				}
			}
			public System.String IsTotalCare
			{
				get
				{
					System.Boolean? data = entity.IsTotalCare;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTotalCare = null;
					else entity.IsTotalCare = Convert.ToBoolean(value);
				}
			}
			public System.String IsAntibioticDrugs
			{
				get
				{
					System.Boolean? data = entity.IsAntibioticDrugs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAntibioticDrugs = null;
					else entity.IsAntibioticDrugs = Convert.ToBoolean(value);
				}
			}
			public System.String IsVAP
			{
				get
				{
					System.Boolean? data = entity.IsVAP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVAP = null;
					else entity.IsVAP = Convert.ToBoolean(value);
				}
			}
			public System.String IsHAP
			{
				get
				{
					System.Boolean? data = entity.IsHAP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHAP = null;
					else entity.IsHAP = Convert.ToBoolean(value);
				}
			}
			public System.String IsISK
			{
				get
				{
					System.Boolean? data = entity.IsISK;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsISK = null;
					else entity.IsISK = Convert.ToBoolean(value);
				}
			}
			public System.String IsILO
			{
				get
				{
					System.Boolean? data = entity.IsILO;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsILO = null;
					else entity.IsILO = Convert.ToBoolean(value);
				}
			}
			public System.String IsIADP
			{
				get
				{
					System.Boolean? data = entity.IsIADP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIADP = null;
					else entity.IsIADP = Convert.ToBoolean(value);
				}
			}
			public System.String IsPhlebitis
			{
				get
				{
					System.Boolean? data = entity.IsPhlebitis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPhlebitis = null;
					else entity.IsPhlebitis = Convert.ToBoolean(value);
				}
			}
			public System.String IsDecubitus
			{
				get
				{
					System.Boolean? data = entity.IsDecubitus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDecubitus = null;
					else entity.IsDecubitus = Convert.ToBoolean(value);
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
			private esINOSInfectionMonitoring entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esINOSInfectionMonitoringQuery query)
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
				throw new Exception("esINOSInfectionMonitoring can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class INOSInfectionMonitoring : esINOSInfectionMonitoring
	{
	}

	[Serializable]
	abstract public class esINOSInfectionMonitoringQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return INOSInfectionMonitoringMetadata.Meta();
			}
		}

		public esQueryItem MonitoringID
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.MonitoringID, esSystemType.Int64);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem MonitoringDate
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.MonitoringDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsMechanicalVentilator
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsMechanicalVentilator, esSystemType.Boolean);
			}
		}

		public esQueryItem IsInpatient
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsInpatient, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrineCatheter
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsUrineCatheter, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSurgery
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsSurgery, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCentralVeinLine
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsCentralVeinLine, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIntraVeinLine
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsIntraVeinLine, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTotalCare
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsTotalCare, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAntibioticDrugs
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsAntibioticDrugs, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVAP
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsVAP, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHAP
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsHAP, esSystemType.Boolean);
			}
		}

		public esQueryItem IsISK
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsISK, esSystemType.Boolean);
			}
		}

		public esQueryItem IsILO
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsILO, esSystemType.Boolean);
			}
		}

		public esQueryItem IsIADP
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsIADP, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPhlebitis
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsPhlebitis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDecubitus
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsDecubitus, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("INOSInfectionMonitoringCollection")]
	public partial class INOSInfectionMonitoringCollection : esINOSInfectionMonitoringCollection, IEnumerable<INOSInfectionMonitoring>
	{
		public INOSInfectionMonitoringCollection()
		{

		}

		public static implicit operator List<INOSInfectionMonitoring>(INOSInfectionMonitoringCollection coll)
		{
			List<INOSInfectionMonitoring> list = new List<INOSInfectionMonitoring>();

			foreach (INOSInfectionMonitoring emp in coll)
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
				return INOSInfectionMonitoringMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new INOSInfectionMonitoringQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new INOSInfectionMonitoring(row);
		}

		override protected esEntity CreateEntity()
		{
			return new INOSInfectionMonitoring();
		}

		#endregion

		[BrowsableAttribute(false)]
		public INOSInfectionMonitoringQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new INOSInfectionMonitoringQuery();
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
		public bool Load(INOSInfectionMonitoringQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public INOSInfectionMonitoring AddNew()
		{
			INOSInfectionMonitoring entity = base.AddNewEntity() as INOSInfectionMonitoring;

			return entity;
		}
		public INOSInfectionMonitoring FindByPrimaryKey(Int64 monitoringID)
		{
			return base.FindByPrimaryKey(monitoringID) as INOSInfectionMonitoring;
		}

		#region IEnumerable< INOSInfectionMonitoring> Members

		IEnumerator<INOSInfectionMonitoring> IEnumerable<INOSInfectionMonitoring>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as INOSInfectionMonitoring;
			}
		}

		#endregion

		private INOSInfectionMonitoringQuery query;
	}


	/// <summary>
	/// Encapsulates the 'INOSInfectionMonitoring' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("INOSInfectionMonitoring ({MonitoringID})")]
	[Serializable]
	public partial class INOSInfectionMonitoring : esINOSInfectionMonitoring
	{
		public INOSInfectionMonitoring()
		{
		}

		public INOSInfectionMonitoring(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return INOSInfectionMonitoringMetadata.Meta();
			}
		}

		override protected esINOSInfectionMonitoringQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new INOSInfectionMonitoringQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public INOSInfectionMonitoringQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new INOSInfectionMonitoringQuery();
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
		public bool Load(INOSInfectionMonitoringQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private INOSInfectionMonitoringQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class INOSInfectionMonitoringQuery : esINOSInfectionMonitoringQuery
	{
		public INOSInfectionMonitoringQuery()
		{

		}

		public INOSInfectionMonitoringQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "INOSInfectionMonitoringQuery";
		}
	}

	[Serializable]
	public partial class INOSInfectionMonitoringMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected INOSInfectionMonitoringMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.MonitoringID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.MonitoringID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.BedID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.MonitoringDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.MonitoringDate;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsMechanicalVentilator, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsMechanicalVentilator;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsInpatient, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsInpatient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsUrineCatheter, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsUrineCatheter;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsSurgery, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsSurgery;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsCentralVeinLine, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsCentralVeinLine;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsIntraVeinLine, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsIntraVeinLine;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsTotalCare, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsTotalCare;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsAntibioticDrugs, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsAntibioticDrugs;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsVAP, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsVAP;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsHAP, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsHAP;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsISK, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsISK;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsILO, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsILO;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsIADP, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsIADP;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsPhlebitis, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsPhlebitis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsDecubitus, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsDecubitus;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.IsVoid, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.VoidDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.VoidByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.CreatedDateTime, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.CreatedByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(INOSInfectionMonitoringMetadata.ColumnNames.LastUpdateByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = INOSInfectionMonitoringMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public INOSInfectionMonitoringMetadata Meta()
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
			public const string MonitoringID = "MonitoringID";
			public const string RegistrationNo = "RegistrationNo";
			public const string BedID = "BedID";
			public const string MonitoringDate = "MonitoringDate";
			public const string IsMechanicalVentilator = "IsMechanicalVentilator";
			public const string IsInpatient = "IsInpatient";
			public const string IsUrineCatheter = "IsUrineCatheter";
			public const string IsSurgery = "IsSurgery";
			public const string IsCentralVeinLine = "IsCentralVeinLine";
			public const string IsIntraVeinLine = "IsIntraVeinLine";
			public const string IsTotalCare = "IsTotalCare";
			public const string IsAntibioticDrugs = "IsAntibioticDrugs";
			public const string IsVAP = "IsVAP";
			public const string IsHAP = "IsHAP";
			public const string IsISK = "IsISK";
			public const string IsILO = "IsILO";
			public const string IsIADP = "IsIADP";
			public const string IsPhlebitis = "IsPhlebitis";
			public const string IsDecubitus = "IsDecubitus";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MonitoringID = "MonitoringID";
			public const string RegistrationNo = "RegistrationNo";
			public const string BedID = "BedID";
			public const string MonitoringDate = "MonitoringDate";
			public const string IsMechanicalVentilator = "IsMechanicalVentilator";
			public const string IsInpatient = "IsInpatient";
			public const string IsUrineCatheter = "IsUrineCatheter";
			public const string IsSurgery = "IsSurgery";
			public const string IsCentralVeinLine = "IsCentralVeinLine";
			public const string IsIntraVeinLine = "IsIntraVeinLine";
			public const string IsTotalCare = "IsTotalCare";
			public const string IsAntibioticDrugs = "IsAntibioticDrugs";
			public const string IsVAP = "IsVAP";
			public const string IsHAP = "IsHAP";
			public const string IsISK = "IsISK";
			public const string IsILO = "IsILO";
			public const string IsIADP = "IsIADP";
			public const string IsPhlebitis = "IsPhlebitis";
			public const string IsDecubitus = "IsDecubitus";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(INOSInfectionMonitoringMetadata))
			{
				if (INOSInfectionMonitoringMetadata.mapDelegates == null)
				{
					INOSInfectionMonitoringMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (INOSInfectionMonitoringMetadata.meta == null)
				{
					INOSInfectionMonitoringMetadata.meta = new INOSInfectionMonitoringMetadata();
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

				meta.AddTypeMap("MonitoringID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonitoringDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsMechanicalVentilator", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInpatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrineCatheter", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSurgery", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCentralVeinLine", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIntraVeinLine", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTotalCare", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAntibioticDrugs", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVAP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHAP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsISK", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsILO", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIADP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPhlebitis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDecubitus", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "INOSInfectionMonitoring";
				meta.Destination = "INOSInfectionMonitoring";
				meta.spInsert = "proc_INOSInfectionMonitoringInsert";
				meta.spUpdate = "proc_INOSInfectionMonitoringUpdate";
				meta.spDelete = "proc_INOSInfectionMonitoringDelete";
				meta.spLoadAll = "proc_INOSInfectionMonitoringLoadAll";
				meta.spLoadByPrimaryKey = "proc_INOSInfectionMonitoringLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private INOSInfectionMonitoringMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
