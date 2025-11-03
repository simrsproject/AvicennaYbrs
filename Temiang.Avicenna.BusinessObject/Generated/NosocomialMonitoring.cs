/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/1/2022 11:57:45 AM
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
	abstract public class esNosocomialMonitoringCollection : esEntityCollectionWAuditLog
	{
		public esNosocomialMonitoringCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NosocomialMonitoringCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringQuery query)
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
			this.InitQuery(query as esNosocomialMonitoringQuery);
		}
		#endregion
			
		virtual public NosocomialMonitoring DetachEntity(NosocomialMonitoring entity)
		{
			return base.DetachEntity(entity) as NosocomialMonitoring;
		}
		
		virtual public NosocomialMonitoring AttachEntity(NosocomialMonitoring entity)
		{
			return base.AttachEntity(entity) as NosocomialMonitoring;
		}
		
		virtual public void Combine(NosocomialMonitoringCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NosocomialMonitoring this[int index]
		{
			get
			{
				return base[index] as NosocomialMonitoring;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NosocomialMonitoring);
		}
	}

	[Serializable]
	abstract public class esNosocomialMonitoring : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNosocomialMonitoringQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNosocomialMonitoring()
		{
		}
	
		public esNosocomialMonitoring(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 monitoringNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, monitoringNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, monitoringNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 monitoringNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, monitoringNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, monitoringNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 monitoringNo)
		{
			esNosocomialMonitoringQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo==registrationNo, query.MonitoringNo==monitoringNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 monitoringNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("MonitoringNo",monitoringNo);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "MonitoringNo": this.str.MonitoringNo = (string)value; break;
						case "MonitoringType": this.str.MonitoringType = (string)value; break;
						case "InstallationDateTime": this.str.InstallationDateTime = (string)value; break;
						case "ReleaseDateTime": this.str.ReleaseDateTime = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "Location": this.str.Location = (string)value; break;
						case "TypeOfInfus": this.str.TypeOfInfus = (string)value; break;
						case "FluidOther": this.str.FluidOther = (string)value; break;
						case "TypeOfCatheter": this.str.TypeOfCatheter = (string)value; break;
						case "Antibiotic": this.str.Antibiotic = (string)value; break;
						case "Problem": this.str.Problem = (string)value; break;
						case "Monitoring": this.str.Monitoring = (string)value; break;
						case "TubeNo": this.str.TubeNo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SREttType": this.str.SREttType = (string)value; break;
						case "DecubitusFromType": this.str.DecubitusFromType = (string)value; break;
						case "DecubitusFrom": this.str.DecubitusFrom = (string)value; break;
						case "OtherDrugs": this.str.OtherDrugs = (string)value; break;
						case "BodyWeight": this.str.BodyWeight = (string)value; break;
						case "VentilationMode": this.str.VentilationMode = (string)value; break;
						case "TidalVolume": this.str.TidalVolume = (string)value; break;
						case "RespirationRate": this.str.RespirationRate = (string)value; break;
						case "FiO2": this.str.FiO2 = (string)value; break;
						case "Peep": this.str.Peep = (string)value; break;
						case "PeakFlow": this.str.PeakFlow = (string)value; break;
						case "Sensitivity": this.str.Sensitivity = (string)value; break;
						case "InstallationByUserID": this.str.InstallationByUserID = (string)value; break;
						case "ReleaseByUserID": this.str.ReleaseByUserID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "DecubitusDateTime": this.str.DecubitusDateTime = (string)value; break;
						case "SRIVCatheter": this.str.SRIVCatheter = (string)value; break;
						case "SRInfusSet": this.str.SRInfusSet = (string)value; break;
						case "IsSetBlood": this.str.IsSetBlood = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MonitoringNo":
						
							if (value == null || value is System.Int32)
								this.MonitoringNo = (System.Int32?)value;
							break;
						case "InstallationDateTime":
						
							if (value == null || value is System.DateTime)
								this.InstallationDateTime = (System.DateTime?)value;
							break;
						case "ReleaseDateTime":
						
							if (value == null || value is System.DateTime)
								this.ReleaseDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "BodyWeight":
						
							if (value == null || value is System.Decimal)
								this.BodyWeight = (System.Decimal?)value;
							break;
						case "TidalVolume":
						
							if (value == null || value is System.Int32)
								this.TidalVolume = (System.Int32?)value;
							break;
						case "RespirationRate":
						
							if (value == null || value is System.Int32)
								this.RespirationRate = (System.Int32?)value;
							break;
						case "FiO2":
						
							if (value == null || value is System.Int32)
								this.FiO2 = (System.Int32?)value;
							break;
						case "Peep":
						
							if (value == null || value is System.Int32)
								this.Peep = (System.Int32?)value;
							break;
						case "PeakFlow":
						
							if (value == null || value is System.Int32)
								this.PeakFlow = (System.Int32?)value;
							break;
						case "DecubitusDateTime":
						
							if (value == null || value is System.DateTime)
								this.DecubitusDateTime = (System.DateTime?)value;
							break;
						case "IsSetBlood":
						
							if (value == null || value is System.Boolean)
								this.IsSetBlood = (System.Boolean?)value;
							break;
					
						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to NosocomialMonitoring.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.MonitoringNo
		/// </summary>
		virtual public System.Int32? MonitoringNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.MonitoringNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.MonitoringNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.MonitoringType
		/// </summary>
		virtual public System.String MonitoringType
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.MonitoringType);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.MonitoringType, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.InstallationDateTime
		/// </summary>
		virtual public System.DateTime? InstallationDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.InstallationDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.InstallationDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.ReleaseDateTime
		/// </summary>
		virtual public System.DateTime? ReleaseDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.ReleaseDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.ReleaseDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.Location
		/// </summary>
		virtual public System.String Location
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.Location);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.Location, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.TypeOfInfus
		/// </summary>
		virtual public System.String TypeOfInfus
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.TypeOfInfus);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.TypeOfInfus, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.FluidOther
		/// </summary>
		virtual public System.String FluidOther
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.FluidOther);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.FluidOther, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.TypeOfCatheter
		/// </summary>
		virtual public System.String TypeOfCatheter
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.TypeOfCatheter);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.TypeOfCatheter, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.Antibiotic
		/// </summary>
		virtual public System.String Antibiotic
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.Antibiotic);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.Antibiotic, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.Problem
		/// </summary>
		virtual public System.String Problem
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.Problem);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.Problem, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.Monitoring
		/// </summary>
		virtual public System.String Monitoring
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.Monitoring);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.Monitoring, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.TubeNo
		/// </summary>
		virtual public System.String TubeNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.TubeNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.TubeNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.SREttType
		/// </summary>
		virtual public System.String SREttType
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.SREttType);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.SREttType, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.DecubitusFromType
		/// </summary>
		virtual public System.String DecubitusFromType
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.DecubitusFromType);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.DecubitusFromType, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.DecubitusFrom
		/// </summary>
		virtual public System.String DecubitusFrom
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.DecubitusFrom);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.DecubitusFrom, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.OtherDrugs
		/// </summary>
		virtual public System.String OtherDrugs
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.OtherDrugs);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.OtherDrugs, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.BodyWeight
		/// </summary>
		virtual public System.Decimal? BodyWeight
		{
			get
			{
				return base.GetSystemDecimal(NosocomialMonitoringMetadata.ColumnNames.BodyWeight);
			}
			
			set
			{
				base.SetSystemDecimal(NosocomialMonitoringMetadata.ColumnNames.BodyWeight, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.VentilationMode
		/// </summary>
		virtual public System.String VentilationMode
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.VentilationMode);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.VentilationMode, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.TidalVolume
		/// </summary>
		virtual public System.Int32? TidalVolume
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.TidalVolume);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.TidalVolume, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.RespirationRate
		/// </summary>
		virtual public System.Int32? RespirationRate
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.RespirationRate);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.RespirationRate, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.FiO2
		/// </summary>
		virtual public System.Int32? FiO2
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.FiO2);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.FiO2, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.Peep
		/// </summary>
		virtual public System.Int32? Peep
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.Peep);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.Peep, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.PeakFlow
		/// </summary>
		virtual public System.Int32? PeakFlow
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.PeakFlow);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringMetadata.ColumnNames.PeakFlow, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.Sensitivity
		/// </summary>
		virtual public System.String Sensitivity
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.Sensitivity);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.Sensitivity, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.InstallationByUserID
		/// </summary>
		virtual public System.String InstallationByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.InstallationByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.InstallationByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.ReleaseByUserID
		/// </summary>
		virtual public System.String ReleaseByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.ReleaseByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.ReleaseByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.DecubitusDateTime
		/// </summary>
		virtual public System.DateTime? DecubitusDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.DecubitusDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringMetadata.ColumnNames.DecubitusDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.SRIVCatheter
		/// </summary>
		virtual public System.String SRIVCatheter
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.SRIVCatheter);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.SRIVCatheter, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.SRInfusSet
		/// </summary>
		virtual public System.String SRInfusSet
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringMetadata.ColumnNames.SRInfusSet);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringMetadata.ColumnNames.SRInfusSet, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoring.IsSetBlood
		/// </summary>
		virtual public System.Boolean? IsSetBlood
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringMetadata.ColumnNames.IsSetBlood);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringMetadata.ColumnNames.IsSetBlood, value);
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
		[BrowsableAttribute( false )]		
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
			public esStrings(esNosocomialMonitoring entity)
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
			public System.String MonitoringNo
			{
				get
				{
					System.Int32? data = entity.MonitoringNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonitoringNo = null;
					else entity.MonitoringNo = Convert.ToInt32(value);
				}
			}
			public System.String MonitoringType
			{
				get
				{
					System.String data = entity.MonitoringType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonitoringType = null;
					else entity.MonitoringType = Convert.ToString(value);
				}
			}
			public System.String InstallationDateTime
			{
				get
				{
					System.DateTime? data = entity.InstallationDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstallationDateTime = null;
					else entity.InstallationDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ReleaseDateTime
			{
				get
				{
					System.DateTime? data = entity.ReleaseDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReleaseDateTime = null;
					else entity.ReleaseDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
				}
			}
			public System.String Location
			{
				get
				{
					System.String data = entity.Location;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Location = null;
					else entity.Location = Convert.ToString(value);
				}
			}
			public System.String TypeOfInfus
			{
				get
				{
					System.String data = entity.TypeOfInfus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TypeOfInfus = null;
					else entity.TypeOfInfus = Convert.ToString(value);
				}
			}
			public System.String FluidOther
			{
				get
				{
					System.String data = entity.FluidOther;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FluidOther = null;
					else entity.FluidOther = Convert.ToString(value);
				}
			}
			public System.String TypeOfCatheter
			{
				get
				{
					System.String data = entity.TypeOfCatheter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TypeOfCatheter = null;
					else entity.TypeOfCatheter = Convert.ToString(value);
				}
			}
			public System.String Antibiotic
			{
				get
				{
					System.String data = entity.Antibiotic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Antibiotic = null;
					else entity.Antibiotic = Convert.ToString(value);
				}
			}
			public System.String Problem
			{
				get
				{
					System.String data = entity.Problem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Problem = null;
					else entity.Problem = Convert.ToString(value);
				}
			}
			public System.String Monitoring
			{
				get
				{
					System.String data = entity.Monitoring;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Monitoring = null;
					else entity.Monitoring = Convert.ToString(value);
				}
			}
			public System.String TubeNo
			{
				get
				{
					System.String data = entity.TubeNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TubeNo = null;
					else entity.TubeNo = Convert.ToString(value);
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
			public System.String SREttType
			{
				get
				{
					System.String data = entity.SREttType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREttType = null;
					else entity.SREttType = Convert.ToString(value);
				}
			}
			public System.String DecubitusFromType
			{
				get
				{
					System.String data = entity.DecubitusFromType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecubitusFromType = null;
					else entity.DecubitusFromType = Convert.ToString(value);
				}
			}
			public System.String DecubitusFrom
			{
				get
				{
					System.String data = entity.DecubitusFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecubitusFrom = null;
					else entity.DecubitusFrom = Convert.ToString(value);
				}
			}
			public System.String OtherDrugs
			{
				get
				{
					System.String data = entity.OtherDrugs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherDrugs = null;
					else entity.OtherDrugs = Convert.ToString(value);
				}
			}
			public System.String BodyWeight
			{
				get
				{
					System.Decimal? data = entity.BodyWeight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyWeight = null;
					else entity.BodyWeight = Convert.ToDecimal(value);
				}
			}
			public System.String VentilationMode
			{
				get
				{
					System.String data = entity.VentilationMode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VentilationMode = null;
					else entity.VentilationMode = Convert.ToString(value);
				}
			}
			public System.String TidalVolume
			{
				get
				{
					System.Int32? data = entity.TidalVolume;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TidalVolume = null;
					else entity.TidalVolume = Convert.ToInt32(value);
				}
			}
			public System.String RespirationRate
			{
				get
				{
					System.Int32? data = entity.RespirationRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RespirationRate = null;
					else entity.RespirationRate = Convert.ToInt32(value);
				}
			}
			public System.String FiO2
			{
				get
				{
					System.Int32? data = entity.FiO2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FiO2 = null;
					else entity.FiO2 = Convert.ToInt32(value);
				}
			}
			public System.String Peep
			{
				get
				{
					System.Int32? data = entity.Peep;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Peep = null;
					else entity.Peep = Convert.ToInt32(value);
				}
			}
			public System.String PeakFlow
			{
				get
				{
					System.Int32? data = entity.PeakFlow;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeakFlow = null;
					else entity.PeakFlow = Convert.ToInt32(value);
				}
			}
			public System.String Sensitivity
			{
				get
				{
					System.String data = entity.Sensitivity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sensitivity = null;
					else entity.Sensitivity = Convert.ToString(value);
				}
			}
			public System.String InstallationByUserID
			{
				get
				{
					System.String data = entity.InstallationByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstallationByUserID = null;
					else entity.InstallationByUserID = Convert.ToString(value);
				}
			}
			public System.String ReleaseByUserID
			{
				get
				{
					System.String data = entity.ReleaseByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReleaseByUserID = null;
					else entity.ReleaseByUserID = Convert.ToString(value);
				}
			}
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
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
			public System.String DecubitusDateTime
			{
				get
				{
					System.DateTime? data = entity.DecubitusDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecubitusDateTime = null;
					else entity.DecubitusDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String SRIVCatheter
			{
				get
				{
					System.String data = entity.SRIVCatheter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIVCatheter = null;
					else entity.SRIVCatheter = Convert.ToString(value);
				}
			}
			public System.String SRInfusSet
			{
				get
				{
					System.String data = entity.SRInfusSet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRInfusSet = null;
					else entity.SRInfusSet = Convert.ToString(value);
				}
			}
			public System.String IsSetBlood
			{
				get
				{
					System.Boolean? data = entity.IsSetBlood;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSetBlood = null;
					else entity.IsSetBlood = Convert.ToBoolean(value);
				}
			}
			private esNosocomialMonitoring entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringQuery query)
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
				throw new Exception("esNosocomialMonitoring can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NosocomialMonitoring : esNosocomialMonitoring
	{	
	}

	[Serializable]
	abstract public class esNosocomialMonitoringQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.MonitoringNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem MonitoringType
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.MonitoringType, esSystemType.String);
			}
		} 
			
		public esQueryItem InstallationDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.InstallationDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ReleaseDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.ReleaseDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
			
		public esQueryItem Location
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.Location, esSystemType.String);
			}
		} 
			
		public esQueryItem TypeOfInfus
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.TypeOfInfus, esSystemType.String);
			}
		} 
			
		public esQueryItem FluidOther
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.FluidOther, esSystemType.String);
			}
		} 
			
		public esQueryItem TypeOfCatheter
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.TypeOfCatheter, esSystemType.String);
			}
		} 
			
		public esQueryItem Antibiotic
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.Antibiotic, esSystemType.String);
			}
		} 
			
		public esQueryItem Problem
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.Problem, esSystemType.String);
			}
		} 
			
		public esQueryItem Monitoring
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.Monitoring, esSystemType.String);
			}
		} 
			
		public esQueryItem TubeNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.TubeNo, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem SREttType
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.SREttType, esSystemType.String);
			}
		} 
			
		public esQueryItem DecubitusFromType
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.DecubitusFromType, esSystemType.String);
			}
		} 
			
		public esQueryItem DecubitusFrom
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.DecubitusFrom, esSystemType.String);
			}
		} 
			
		public esQueryItem OtherDrugs
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.OtherDrugs, esSystemType.String);
			}
		} 
			
		public esQueryItem BodyWeight
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.BodyWeight, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem VentilationMode
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.VentilationMode, esSystemType.String);
			}
		} 
			
		public esQueryItem TidalVolume
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.TidalVolume, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RespirationRate
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.RespirationRate, esSystemType.Int32);
			}
		} 
			
		public esQueryItem FiO2
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.FiO2, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Peep
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.Peep, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PeakFlow
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.PeakFlow, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Sensitivity
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.Sensitivity, esSystemType.String);
			}
		} 
			
		public esQueryItem InstallationByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.InstallationByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReleaseByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.ReleaseByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem DecubitusDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.DecubitusDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRIVCatheter
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.SRIVCatheter, esSystemType.String);
			}
		} 
			
		public esQueryItem SRInfusSet
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.SRInfusSet, esSystemType.String);
			}
		} 
			
		public esQueryItem IsSetBlood
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringMetadata.ColumnNames.IsSetBlood, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NosocomialMonitoringCollection")]
	public partial class NosocomialMonitoringCollection : esNosocomialMonitoringCollection, IEnumerable< NosocomialMonitoring>
	{
		public NosocomialMonitoringCollection()
		{

		}	
		
		public static implicit operator List< NosocomialMonitoring>(NosocomialMonitoringCollection coll)
		{
			List< NosocomialMonitoring> list = new List< NosocomialMonitoring>();
			
			foreach (NosocomialMonitoring emp in coll)
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
				return  NosocomialMonitoringMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NosocomialMonitoring(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NosocomialMonitoring();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NosocomialMonitoringQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringQuery();
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
		public bool Load(NosocomialMonitoringQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NosocomialMonitoring AddNew()
		{
			NosocomialMonitoring entity = base.AddNewEntity() as NosocomialMonitoring;
			
			return entity;		
		}
		public NosocomialMonitoring FindByPrimaryKey(String registrationNo, Int32 monitoringNo)
		{
			return base.FindByPrimaryKey(registrationNo, monitoringNo) as NosocomialMonitoring;
		}

		#region IEnumerable< NosocomialMonitoring> Members

		IEnumerator< NosocomialMonitoring> IEnumerable< NosocomialMonitoring>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NosocomialMonitoring;
			}
		}

		#endregion
		
		private NosocomialMonitoringQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NosocomialMonitoring' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NosocomialMonitoring ({RegistrationNo, MonitoringNo})")]
	[Serializable]
	public partial class NosocomialMonitoring : esNosocomialMonitoring
	{
		public NosocomialMonitoring()
		{
		}	
	
		public NosocomialMonitoring(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringMetadata.Meta();
			}
		}	
	
		override protected esNosocomialMonitoringQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NosocomialMonitoringQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringQuery();
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
		public bool Load(NosocomialMonitoringQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NosocomialMonitoringQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NosocomialMonitoringQuery : esNosocomialMonitoringQuery
	{
		public NosocomialMonitoringQuery()
		{

		}		
		
		public NosocomialMonitoringQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NosocomialMonitoringQuery";
        }
	}

	[Serializable]
	public partial class NosocomialMonitoringMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NosocomialMonitoringMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.MonitoringNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.MonitoringNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.MonitoringType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.MonitoringType;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.InstallationDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.InstallationDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.ReleaseDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.ReleaseDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.RoomID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.Location, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.Location;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.TypeOfInfus, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.TypeOfInfus;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.FluidOther, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.FluidOther;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.TypeOfCatheter, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.TypeOfCatheter;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.Antibiotic, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.Antibiotic;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.Problem, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.Problem;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.Monitoring, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.Monitoring;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.TubeNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.TubeNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.SREttType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.SREttType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.DecubitusFromType, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.DecubitusFromType;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.DecubitusFrom, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.DecubitusFrom;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.OtherDrugs, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.OtherDrugs;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.BodyWeight, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.BodyWeight;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.VentilationMode, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.VentilationMode;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.TidalVolume, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.TidalVolume;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.RespirationRate, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.RespirationRate;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.FiO2, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.FiO2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.Peep, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.Peep;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.PeakFlow, 26, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.PeakFlow;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.Sensitivity, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.Sensitivity;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.InstallationByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.InstallationByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.ReleaseByUserID, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.ReleaseByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.ReferenceNo, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.ServiceUnitID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.DecubitusDateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.DecubitusDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.SRIVCatheter, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.SRIVCatheter;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.SRInfusSet, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.SRInfusSet;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringMetadata.ColumnNames.IsSetBlood, 35, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringMetadata.PropertyNames.IsSetBlood;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NosocomialMonitoringMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string MonitoringNo = "MonitoringNo";
			public const string MonitoringType = "MonitoringType";
			public const string InstallationDateTime = "InstallationDateTime";
			public const string ReleaseDateTime = "ReleaseDateTime";
			public const string RoomID = "RoomID";
			public const string Location = "Location";
			public const string TypeOfInfus = "TypeOfInfus";
			public const string FluidOther = "FluidOther";
			public const string TypeOfCatheter = "TypeOfCatheter";
			public const string Antibiotic = "Antibiotic";
			public const string Problem = "Problem";
			public const string Monitoring = "Monitoring";
			public const string TubeNo = "TubeNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREttType = "SREttType";
			public const string DecubitusFromType = "DecubitusFromType";
			public const string DecubitusFrom = "DecubitusFrom";
			public const string OtherDrugs = "OtherDrugs";
			public const string BodyWeight = "BodyWeight";
			public const string VentilationMode = "VentilationMode";
			public const string TidalVolume = "TidalVolume";
			public const string RespirationRate = "RespirationRate";
			public const string FiO2 = "FiO2";
			public const string Peep = "Peep";
			public const string PeakFlow = "PeakFlow";
			public const string Sensitivity = "Sensitivity";
			public const string InstallationByUserID = "InstallationByUserID";
			public const string ReleaseByUserID = "ReleaseByUserID";
			public const string ReferenceNo = "ReferenceNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DecubitusDateTime = "DecubitusDateTime";
			public const string SRIVCatheter = "SRIVCatheter";
			public const string SRInfusSet = "SRInfusSet";
			public const string IsSetBlood = "IsSetBlood";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string MonitoringNo = "MonitoringNo";
			public const string MonitoringType = "MonitoringType";
			public const string InstallationDateTime = "InstallationDateTime";
			public const string ReleaseDateTime = "ReleaseDateTime";
			public const string RoomID = "RoomID";
			public const string Location = "Location";
			public const string TypeOfInfus = "TypeOfInfus";
			public const string FluidOther = "FluidOther";
			public const string TypeOfCatheter = "TypeOfCatheter";
			public const string Antibiotic = "Antibiotic";
			public const string Problem = "Problem";
			public const string Monitoring = "Monitoring";
			public const string TubeNo = "TubeNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SREttType = "SREttType";
			public const string DecubitusFromType = "DecubitusFromType";
			public const string DecubitusFrom = "DecubitusFrom";
			public const string OtherDrugs = "OtherDrugs";
			public const string BodyWeight = "BodyWeight";
			public const string VentilationMode = "VentilationMode";
			public const string TidalVolume = "TidalVolume";
			public const string RespirationRate = "RespirationRate";
			public const string FiO2 = "FiO2";
			public const string Peep = "Peep";
			public const string PeakFlow = "PeakFlow";
			public const string Sensitivity = "Sensitivity";
			public const string InstallationByUserID = "InstallationByUserID";
			public const string ReleaseByUserID = "ReleaseByUserID";
			public const string ReferenceNo = "ReferenceNo";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DecubitusDateTime = "DecubitusDateTime";
			public const string SRIVCatheter = "SRIVCatheter";
			public const string SRInfusSet = "SRInfusSet";
			public const string IsSetBlood = "IsSetBlood";
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
			lock (typeof(NosocomialMonitoringMetadata))
			{
				if(NosocomialMonitoringMetadata.mapDelegates == null)
				{
					NosocomialMonitoringMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NosocomialMonitoringMetadata.meta == null)
				{
					NosocomialMonitoringMetadata.meta = new NosocomialMonitoringMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonitoringNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MonitoringType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstallationDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReleaseDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Location", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TypeOfInfus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FluidOther", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TypeOfCatheter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Antibiotic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Problem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Monitoring", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TubeNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREttType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DecubitusFromType", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("DecubitusFrom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherDrugs", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BodyWeight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VentilationMode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TidalVolume", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RespirationRate", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FiO2", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Peep", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PeakFlow", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Sensitivity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstallationByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReleaseByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DecubitusDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRIVCatheter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRInfusSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSetBlood", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "NosocomialMonitoring";
				meta.Destination = "NosocomialMonitoring";
				meta.spInsert = "proc_NosocomialMonitoringInsert";				
				meta.spUpdate = "proc_NosocomialMonitoringUpdate";		
				meta.spDelete = "proc_NosocomialMonitoringDelete";
				meta.spLoadAll = "proc_NosocomialMonitoringLoadAll";
				meta.spLoadByPrimaryKey = "proc_NosocomialMonitoringLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NosocomialMonitoringMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
