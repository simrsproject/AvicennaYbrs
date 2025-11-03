/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/18/19 6:57:31 AM
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
	abstract public class esNosocomialMonitoringEttCollection : esEntityCollectionWAuditLog
	{
		public esNosocomialMonitoringEttCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NosocomialMonitoringEttCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringEttQuery query)
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
			this.InitQuery(query as esNosocomialMonitoringEttQuery);
		}
		#endregion
			
		virtual public NosocomialMonitoringEtt DetachEntity(NosocomialMonitoringEtt entity)
		{
			return base.DetachEntity(entity) as NosocomialMonitoringEtt;
		}
		
		virtual public NosocomialMonitoringEtt AttachEntity(NosocomialMonitoringEtt entity)
		{
			return base.AttachEntity(entity) as NosocomialMonitoringEtt;
		}
		
		virtual public void Combine(NosocomialMonitoringEttCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NosocomialMonitoringEtt this[int index]
		{
			get
			{
				return base[index] as NosocomialMonitoringEtt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NosocomialMonitoringEtt);
		}
	}

	[Serializable]
	abstract public class esNosocomialMonitoringEtt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNosocomialMonitoringEttQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNosocomialMonitoringEtt()
		{
		}
	
		public esNosocomialMonitoringEtt(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, monitoringNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, monitoringNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, monitoringNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, monitoringNo, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			esNosocomialMonitoringEttQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.MonitoringNo == monitoringNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("MonitoringNo",monitoringNo);
			parms.Add("SequenceNo",sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "MonitoringDateTime": this.str.MonitoringDateTime = (string)value; break;
						case "SREttType": this.str.SREttType = (string)value; break;
						case "IsTempAbove38": this.str.IsTempAbove38 = (string)value; break;
						case "IsLeukopenia": this.str.IsLeukopenia = (string)value; break;
						case "IsLeukositosis": this.str.IsLeukositosis = (string)value; break;
						case "IsSputum": this.str.IsSputum = (string)value; break;
						case "IsCough": this.str.IsCough = (string)value; break;
						case "IsDipsnoe": this.str.IsDipsnoe = (string)value; break;
						case "IsWetRonchi": this.str.IsWetRonchi = (string)value; break;
						case "IsDesaturasi": this.str.IsDesaturasi = (string)value; break;
						case "IsCulture": this.str.IsCulture = (string)value; break;
						case "IsRadiology": this.str.IsRadiology = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsElbowConnectorRepl": this.str.IsElbowConnectorRepl = (string)value; break;
						case "IsHumidificationRepl": this.str.IsHumidificationRepl = (string)value; break;
						case "IsGuedeleRepl": this.str.IsGuedeleRepl = (string)value; break;
						case "IsTidalVolChange": this.str.IsTidalVolChange = (string)value; break;
						case "IsRrChange": this.str.IsRrChange = (string)value; break;
						case "IsModeVentChange": this.str.IsModeVentChange = (string)value; break;
						case "SputumColor": this.str.SputumColor = (string)value; break;
						case "Leukosit": this.str.Leukosit = (string)value; break;
						case "Thorax": this.str.Thorax = (string)value; break;
						case "MonitoringByUserID": this.str.MonitoringByUserID = (string)value; break;
						case "IsRelease": this.str.IsRelease = (string)value; break;
						case "IsBradikardi": this.str.IsBradikardi = (string)value; break;
						case "IsDispenea": this.str.IsDispenea = (string)value; break;
						case "IsSpO2LessThan94": this.str.IsSpO2LessThan94 = (string)value; break;
						case "IsVapDiagnose": this.str.IsVapDiagnose = (string)value; break;
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
						case "SequenceNo":
						
							if (value == null || value is System.Int32)
								this.SequenceNo = (System.Int32?)value;
							break;
						case "MonitoringDateTime":
						
							if (value == null || value is System.DateTime)
								this.MonitoringDateTime = (System.DateTime?)value;
							break;
						case "IsTempAbove38":
						
							if (value == null || value is System.Boolean)
								this.IsTempAbove38 = (System.Boolean?)value;
							break;
						case "IsLeukopenia":
						
							if (value == null || value is System.Boolean)
								this.IsLeukopenia = (System.Boolean?)value;
							break;
						case "IsLeukositosis":
						
							if (value == null || value is System.Boolean)
								this.IsLeukositosis = (System.Boolean?)value;
							break;
						case "IsSputum":
						
							if (value == null || value is System.Boolean)
								this.IsSputum = (System.Boolean?)value;
							break;
						case "IsCough":
						
							if (value == null || value is System.Boolean)
								this.IsCough = (System.Boolean?)value;
							break;
						case "IsDipsnoe":
						
							if (value == null || value is System.Boolean)
								this.IsDipsnoe = (System.Boolean?)value;
							break;
						case "IsWetRonchi":
						
							if (value == null || value is System.Boolean)
								this.IsWetRonchi = (System.Boolean?)value;
							break;
						case "IsDesaturasi":
						
							if (value == null || value is System.Boolean)
								this.IsDesaturasi = (System.Boolean?)value;
							break;
						case "IsCulture":
						
							if (value == null || value is System.Boolean)
								this.IsCulture = (System.Boolean?)value;
							break;
						case "IsRadiology":
						
							if (value == null || value is System.Boolean)
								this.IsRadiology = (System.Boolean?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsElbowConnectorRepl":
						
							if (value == null || value is System.Boolean)
								this.IsElbowConnectorRepl = (System.Boolean?)value;
							break;
						case "IsHumidificationRepl":
						
							if (value == null || value is System.Boolean)
								this.IsHumidificationRepl = (System.Boolean?)value;
							break;
						case "IsGuedeleRepl":
						
							if (value == null || value is System.Boolean)
								this.IsGuedeleRepl = (System.Boolean?)value;
							break;
						case "IsTidalVolChange":
						
							if (value == null || value is System.Boolean)
								this.IsTidalVolChange = (System.Boolean?)value;
							break;
						case "IsRrChange":
						
							if (value == null || value is System.Boolean)
								this.IsRrChange = (System.Boolean?)value;
							break;
						case "IsModeVentChange":
						
							if (value == null || value is System.Boolean)
								this.IsModeVentChange = (System.Boolean?)value;
							break;
						case "IsRelease":
						
							if (value == null || value is System.Boolean)
								this.IsRelease = (System.Boolean?)value;
							break;
						case "IsBradikardi":
						
							if (value == null || value is System.Boolean)
								this.IsBradikardi = (System.Boolean?)value;
							break;
						case "IsDispenea":
						
							if (value == null || value is System.Boolean)
								this.IsDispenea = (System.Boolean?)value;
							break;
						case "IsSpO2LessThan94":
						
							if (value == null || value is System.Boolean)
								this.IsSpO2LessThan94 = (System.Boolean?)value;
							break;
						case "IsVapDiagnose":
						
							if (value == null || value is System.Boolean)
								this.IsVapDiagnose = (System.Boolean?)value;
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
		/// Maps to NosocomialMonitoringEtt.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.MonitoringNo
		/// </summary>
		virtual public System.Int32? MonitoringNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringEttMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringEttMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.MonitoringDateTime
		/// </summary>
		virtual public System.DateTime? MonitoringDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.SREttType
		/// </summary>
		virtual public System.String SREttType
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.SREttType);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.SREttType, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsTempAbove38
		/// </summary>
		virtual public System.Boolean? IsTempAbove38
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsTempAbove38);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsTempAbove38, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsLeukopenia
		/// </summary>
		virtual public System.Boolean? IsLeukopenia
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsLeukopenia);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsLeukopenia, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsLeukositosis
		/// </summary>
		virtual public System.Boolean? IsLeukositosis
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsLeukositosis);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsLeukositosis, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsSputum
		/// </summary>
		virtual public System.Boolean? IsSputum
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsSputum);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsSputum, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsCough
		/// </summary>
		virtual public System.Boolean? IsCough
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsCough);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsCough, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsDipsnoe
		/// </summary>
		virtual public System.Boolean? IsDipsnoe
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDipsnoe);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDipsnoe, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsWetRonchi
		/// </summary>
		virtual public System.Boolean? IsWetRonchi
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsWetRonchi);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsWetRonchi, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsDesaturasi
		/// </summary>
		virtual public System.Boolean? IsDesaturasi
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDesaturasi);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDesaturasi, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsCulture
		/// </summary>
		virtual public System.Boolean? IsCulture
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsCulture);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsCulture, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsRadiology
		/// </summary>
		virtual public System.Boolean? IsRadiology
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsRadiology);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsRadiology, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsElbowConnectorRepl
		/// </summary>
		virtual public System.Boolean? IsElbowConnectorRepl
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsElbowConnectorRepl);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsElbowConnectorRepl, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsHumidificationRepl
		/// </summary>
		virtual public System.Boolean? IsHumidificationRepl
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsHumidificationRepl);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsHumidificationRepl, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsGuedeleRepl
		/// </summary>
		virtual public System.Boolean? IsGuedeleRepl
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsGuedeleRepl);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsGuedeleRepl, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsTidalVolChange
		/// </summary>
		virtual public System.Boolean? IsTidalVolChange
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsTidalVolChange);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsTidalVolChange, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsRrChange
		/// </summary>
		virtual public System.Boolean? IsRrChange
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsRrChange);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsRrChange, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsModeVentChange
		/// </summary>
		virtual public System.Boolean? IsModeVentChange
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsModeVentChange);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsModeVentChange, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.SputumColor
		/// </summary>
		virtual public System.String SputumColor
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.SputumColor);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.SputumColor, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.Leukosit
		/// </summary>
		virtual public System.String Leukosit
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.Leukosit);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.Leukosit, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.Thorax
		/// </summary>
		virtual public System.String Thorax
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.Thorax);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.Thorax, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.MonitoringByUserID
		/// </summary>
		virtual public System.String MonitoringByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsRelease
		/// </summary>
		virtual public System.Boolean? IsRelease
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsRelease);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsRelease, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsBradikardi
		/// </summary>
		virtual public System.Boolean? IsBradikardi
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsBradikardi);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsBradikardi, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsDispenea
		/// </summary>
		virtual public System.Boolean? IsDispenea
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDispenea);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsDispenea, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsSpO2LessThan94
		/// </summary>
		virtual public System.Boolean? IsSpO2LessThan94
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsSpO2LessThan94);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsSpO2LessThan94, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringEtt.IsVapDiagnose
		/// </summary>
		virtual public System.Boolean? IsVapDiagnose
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsVapDiagnose);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringEttMetadata.ColumnNames.IsVapDiagnose, value);
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
			public esStrings(esNosocomialMonitoringEtt entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.Int32? data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToInt32(value);
				}
			}
			public System.String MonitoringDateTime
			{
				get
				{
					System.DateTime? data = entity.MonitoringDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonitoringDateTime = null;
					else entity.MonitoringDateTime = Convert.ToDateTime(value);
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
			public System.String IsTempAbove38
			{
				get
				{
					System.Boolean? data = entity.IsTempAbove38;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTempAbove38 = null;
					else entity.IsTempAbove38 = Convert.ToBoolean(value);
				}
			}
			public System.String IsLeukopenia
			{
				get
				{
					System.Boolean? data = entity.IsLeukopenia;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLeukopenia = null;
					else entity.IsLeukopenia = Convert.ToBoolean(value);
				}
			}
			public System.String IsLeukositosis
			{
				get
				{
					System.Boolean? data = entity.IsLeukositosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLeukositosis = null;
					else entity.IsLeukositosis = Convert.ToBoolean(value);
				}
			}
			public System.String IsSputum
			{
				get
				{
					System.Boolean? data = entity.IsSputum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSputum = null;
					else entity.IsSputum = Convert.ToBoolean(value);
				}
			}
			public System.String IsCough
			{
				get
				{
					System.Boolean? data = entity.IsCough;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCough = null;
					else entity.IsCough = Convert.ToBoolean(value);
				}
			}
			public System.String IsDipsnoe
			{
				get
				{
					System.Boolean? data = entity.IsDipsnoe;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDipsnoe = null;
					else entity.IsDipsnoe = Convert.ToBoolean(value);
				}
			}
			public System.String IsWetRonchi
			{
				get
				{
					System.Boolean? data = entity.IsWetRonchi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWetRonchi = null;
					else entity.IsWetRonchi = Convert.ToBoolean(value);
				}
			}
			public System.String IsDesaturasi
			{
				get
				{
					System.Boolean? data = entity.IsDesaturasi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDesaturasi = null;
					else entity.IsDesaturasi = Convert.ToBoolean(value);
				}
			}
			public System.String IsCulture
			{
				get
				{
					System.Boolean? data = entity.IsCulture;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCulture = null;
					else entity.IsCulture = Convert.ToBoolean(value);
				}
			}
			public System.String IsRadiology
			{
				get
				{
					System.Boolean? data = entity.IsRadiology;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRadiology = null;
					else entity.IsRadiology = Convert.ToBoolean(value);
				}
			}
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
				}
			}
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
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
			public System.String IsElbowConnectorRepl
			{
				get
				{
					System.Boolean? data = entity.IsElbowConnectorRepl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsElbowConnectorRepl = null;
					else entity.IsElbowConnectorRepl = Convert.ToBoolean(value);
				}
			}
			public System.String IsHumidificationRepl
			{
				get
				{
					System.Boolean? data = entity.IsHumidificationRepl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHumidificationRepl = null;
					else entity.IsHumidificationRepl = Convert.ToBoolean(value);
				}
			}
			public System.String IsGuedeleRepl
			{
				get
				{
					System.Boolean? data = entity.IsGuedeleRepl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGuedeleRepl = null;
					else entity.IsGuedeleRepl = Convert.ToBoolean(value);
				}
			}
			public System.String IsTidalVolChange
			{
				get
				{
					System.Boolean? data = entity.IsTidalVolChange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTidalVolChange = null;
					else entity.IsTidalVolChange = Convert.ToBoolean(value);
				}
			}
			public System.String IsRrChange
			{
				get
				{
					System.Boolean? data = entity.IsRrChange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRrChange = null;
					else entity.IsRrChange = Convert.ToBoolean(value);
				}
			}
			public System.String IsModeVentChange
			{
				get
				{
					System.Boolean? data = entity.IsModeVentChange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsModeVentChange = null;
					else entity.IsModeVentChange = Convert.ToBoolean(value);
				}
			}
			public System.String SputumColor
			{
				get
				{
					System.String data = entity.SputumColor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SputumColor = null;
					else entity.SputumColor = Convert.ToString(value);
				}
			}
			public System.String Leukosit
			{
				get
				{
					System.String data = entity.Leukosit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Leukosit = null;
					else entity.Leukosit = Convert.ToString(value);
				}
			}
			public System.String Thorax
			{
				get
				{
					System.String data = entity.Thorax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Thorax = null;
					else entity.Thorax = Convert.ToString(value);
				}
			}
			public System.String MonitoringByUserID
			{
				get
				{
					System.String data = entity.MonitoringByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonitoringByUserID = null;
					else entity.MonitoringByUserID = Convert.ToString(value);
				}
			}
			public System.String IsRelease
			{
				get
				{
					System.Boolean? data = entity.IsRelease;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRelease = null;
					else entity.IsRelease = Convert.ToBoolean(value);
				}
			}
			public System.String IsBradikardi
			{
				get
				{
					System.Boolean? data = entity.IsBradikardi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBradikardi = null;
					else entity.IsBradikardi = Convert.ToBoolean(value);
				}
			}
			public System.String IsDispenea
			{
				get
				{
					System.Boolean? data = entity.IsDispenea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDispenea = null;
					else entity.IsDispenea = Convert.ToBoolean(value);
				}
			}
			public System.String IsSpO2LessThan94
			{
				get
				{
					System.Boolean? data = entity.IsSpO2LessThan94;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSpO2LessThan94 = null;
					else entity.IsSpO2LessThan94 = Convert.ToBoolean(value);
				}
			}
			public System.String IsVapDiagnose
			{
				get
				{
					System.Boolean? data = entity.IsVapDiagnose;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVapDiagnose = null;
					else entity.IsVapDiagnose = Convert.ToBoolean(value);
				}
			}
			private esNosocomialMonitoringEtt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringEttQuery query)
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
				throw new Exception("esNosocomialMonitoringEtt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NosocomialMonitoringEtt : esNosocomialMonitoringEtt
	{	
	}

	[Serializable]
	abstract public class esNosocomialMonitoringEttQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringEttMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.MonitoringNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem MonitoringDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.MonitoringDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SREttType
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.SREttType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsTempAbove38
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsTempAbove38, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsLeukopenia
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsLeukopenia, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsLeukositosis
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsLeukositosis, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSputum
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsSputum, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsCough
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsCough, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDipsnoe
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsDipsnoe, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsWetRonchi
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsWetRonchi, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDesaturasi
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsDesaturasi, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsCulture
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsCulture, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsRadiology
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsRadiology, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsElbowConnectorRepl
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsElbowConnectorRepl, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsHumidificationRepl
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsHumidificationRepl, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsGuedeleRepl
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsGuedeleRepl, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsTidalVolChange
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsTidalVolChange, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsRrChange
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsRrChange, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsModeVentChange
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsModeVentChange, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SputumColor
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.SputumColor, esSystemType.String);
			}
		} 
			
		public esQueryItem Leukosit
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.Leukosit, esSystemType.String);
			}
		} 
			
		public esQueryItem Thorax
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.Thorax, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.MonitoringByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsRelease
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsRelease, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsBradikardi
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsBradikardi, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDispenea
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsDispenea, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSpO2LessThan94
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsSpO2LessThan94, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVapDiagnose
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringEttMetadata.ColumnNames.IsVapDiagnose, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NosocomialMonitoringEttCollection")]
	public partial class NosocomialMonitoringEttCollection : esNosocomialMonitoringEttCollection, IEnumerable< NosocomialMonitoringEtt>
	{
		public NosocomialMonitoringEttCollection()
		{

		}	
		
		public static implicit operator List< NosocomialMonitoringEtt>(NosocomialMonitoringEttCollection coll)
		{
			List< NosocomialMonitoringEtt> list = new List< NosocomialMonitoringEtt>();
			
			foreach (NosocomialMonitoringEtt emp in coll)
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
				return  NosocomialMonitoringEttMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringEttQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NosocomialMonitoringEtt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NosocomialMonitoringEtt();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NosocomialMonitoringEttQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringEttQuery();
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
		public bool Load(NosocomialMonitoringEttQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NosocomialMonitoringEtt AddNew()
		{
			NosocomialMonitoringEtt entity = base.AddNewEntity() as NosocomialMonitoringEtt;
			
			return entity;		
		}
		public NosocomialMonitoringEtt FindByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, monitoringNo, sequenceNo) as NosocomialMonitoringEtt;
		}

		#region IEnumerable< NosocomialMonitoringEtt> Members

		IEnumerator< NosocomialMonitoringEtt> IEnumerable< NosocomialMonitoringEtt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NosocomialMonitoringEtt;
			}
		}

		#endregion
		
		private NosocomialMonitoringEttQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NosocomialMonitoringEtt' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NosocomialMonitoringEtt ({RegistrationNo, MonitoringNo, SequenceNo})")]
	[Serializable]
	public partial class NosocomialMonitoringEtt : esNosocomialMonitoringEtt
	{
		public NosocomialMonitoringEtt()
		{
		}	
	
		public NosocomialMonitoringEtt(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringEttMetadata.Meta();
			}
		}	
	
		override protected esNosocomialMonitoringEttQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringEttQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NosocomialMonitoringEttQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringEttQuery();
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
		public bool Load(NosocomialMonitoringEttQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NosocomialMonitoringEttQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NosocomialMonitoringEttQuery : esNosocomialMonitoringEttQuery
	{
		public NosocomialMonitoringEttQuery()
		{

		}		
		
		public NosocomialMonitoringEttQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NosocomialMonitoringEttQuery";
        }
	}

	[Serializable]
	public partial class NosocomialMonitoringEttMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NosocomialMonitoringEttMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.MonitoringNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.SequenceNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.MonitoringDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.SREttType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.SREttType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsTempAbove38, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsTempAbove38;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsLeukopenia, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsLeukopenia;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsLeukositosis, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsLeukositosis;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsSputum, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsSputum;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsCough, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsCough;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsDipsnoe, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsDipsnoe;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsWetRonchi, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsWetRonchi;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsDesaturasi, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsDesaturasi;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsCulture, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsCulture;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsRadiology, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsRadiology;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.Note, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsDeleted, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsElbowConnectorRepl, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsElbowConnectorRepl;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsHumidificationRepl, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsHumidificationRepl;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsGuedeleRepl, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsGuedeleRepl;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsTidalVolChange, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsTidalVolChange;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsRrChange, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsRrChange;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsModeVentChange, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsModeVentChange;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.SputumColor, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.SputumColor;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.Leukosit, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.Leukosit;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.Thorax, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.Thorax;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.MonitoringByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.MonitoringByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsRelease, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsRelease;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsBradikardi, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsBradikardi;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsDispenea, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsDispenea;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsSpO2LessThan94, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsSpO2LessThan94;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringEttMetadata.ColumnNames.IsVapDiagnose, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringEttMetadata.PropertyNames.IsVapDiagnose;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NosocomialMonitoringEttMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string MonitoringDateTime = "MonitoringDateTime";
			public const string SREttType = "SREttType";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsLeukopenia = "IsLeukopenia";
			public const string IsLeukositosis = "IsLeukositosis";
			public const string IsSputum = "IsSputum";
			public const string IsCough = "IsCough";
			public const string IsDipsnoe = "IsDipsnoe";
			public const string IsWetRonchi = "IsWetRonchi";
			public const string IsDesaturasi = "IsDesaturasi";
			public const string IsCulture = "IsCulture";
			public const string IsRadiology = "IsRadiology";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsElbowConnectorRepl = "IsElbowConnectorRepl";
			public const string IsHumidificationRepl = "IsHumidificationRepl";
			public const string IsGuedeleRepl = "IsGuedeleRepl";
			public const string IsTidalVolChange = "IsTidalVolChange";
			public const string IsRrChange = "IsRrChange";
			public const string IsModeVentChange = "IsModeVentChange";
			public const string SputumColor = "SputumColor";
			public const string Leukosit = "Leukosit";
			public const string Thorax = "Thorax";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsRelease = "IsRelease";
			public const string IsBradikardi = "IsBradikardi";
			public const string IsDispenea = "IsDispenea";
			public const string IsSpO2LessThan94 = "IsSpO2LessThan94";
			public const string IsVapDiagnose = "IsVapDiagnose";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string MonitoringNo = "MonitoringNo";
			public const string SequenceNo = "SequenceNo";
			public const string MonitoringDateTime = "MonitoringDateTime";
			public const string SREttType = "SREttType";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsLeukopenia = "IsLeukopenia";
			public const string IsLeukositosis = "IsLeukositosis";
			public const string IsSputum = "IsSputum";
			public const string IsCough = "IsCough";
			public const string IsDipsnoe = "IsDipsnoe";
			public const string IsWetRonchi = "IsWetRonchi";
			public const string IsDesaturasi = "IsDesaturasi";
			public const string IsCulture = "IsCulture";
			public const string IsRadiology = "IsRadiology";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsElbowConnectorRepl = "IsElbowConnectorRepl";
			public const string IsHumidificationRepl = "IsHumidificationRepl";
			public const string IsGuedeleRepl = "IsGuedeleRepl";
			public const string IsTidalVolChange = "IsTidalVolChange";
			public const string IsRrChange = "IsRrChange";
			public const string IsModeVentChange = "IsModeVentChange";
			public const string SputumColor = "SputumColor";
			public const string Leukosit = "Leukosit";
			public const string Thorax = "Thorax";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsRelease = "IsRelease";
			public const string IsBradikardi = "IsBradikardi";
			public const string IsDispenea = "IsDispenea";
			public const string IsSpO2LessThan94 = "IsSpO2LessThan94";
			public const string IsVapDiagnose = "IsVapDiagnose";
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
			lock (typeof(NosocomialMonitoringEttMetadata))
			{
				if(NosocomialMonitoringEttMetadata.mapDelegates == null)
				{
					NosocomialMonitoringEttMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NosocomialMonitoringEttMetadata.meta == null)
				{
					NosocomialMonitoringEttMetadata.meta = new NosocomialMonitoringEttMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MonitoringDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SREttType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTempAbove38", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLeukopenia", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLeukositosis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSputum", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCough", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDipsnoe", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWetRonchi", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDesaturasi", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCulture", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRadiology", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsElbowConnectorRepl", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHumidificationRepl", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGuedeleRepl", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTidalVolChange", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRrChange", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsModeVentChange", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SputumColor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Leukosit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Thorax", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonitoringByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRelease", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBradikardi", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDispenea", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSpO2LessThan94", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVapDiagnose", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "NosocomialMonitoringEtt";
				meta.Destination = "NosocomialMonitoringEtt";
				meta.spInsert = "proc_NosocomialMonitoringEttInsert";				
				meta.spUpdate = "proc_NosocomialMonitoringEttUpdate";		
				meta.spDelete = "proc_NosocomialMonitoringEttDelete";
				meta.spLoadAll = "proc_NosocomialMonitoringEttLoadAll";
				meta.spLoadByPrimaryKey = "proc_NosocomialMonitoringEttLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NosocomialMonitoringEttMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
