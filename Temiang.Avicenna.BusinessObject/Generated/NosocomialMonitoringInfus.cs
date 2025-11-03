/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 04/02/20 5:44:50 AM
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
	abstract public class esNosocomialMonitoringInfusCollection : esEntityCollectionWAuditLog
	{
		public esNosocomialMonitoringInfusCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NosocomialMonitoringInfusCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringInfusQuery query)
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
			this.InitQuery(query as esNosocomialMonitoringInfusQuery);
		}
		#endregion
			
		virtual public NosocomialMonitoringInfus DetachEntity(NosocomialMonitoringInfus entity)
		{
			return base.DetachEntity(entity) as NosocomialMonitoringInfus;
		}
		
		virtual public NosocomialMonitoringInfus AttachEntity(NosocomialMonitoringInfus entity)
		{
			return base.AttachEntity(entity) as NosocomialMonitoringInfus;
		}
		
		virtual public void Combine(NosocomialMonitoringInfusCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NosocomialMonitoringInfus this[int index]
		{
			get
			{
				return base[index] as NosocomialMonitoringInfus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NosocomialMonitoringInfus);
		}
	}

	[Serializable]
	abstract public class esNosocomialMonitoringInfus : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNosocomialMonitoringInfusQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNosocomialMonitoringInfus()
		{
		}
	
		public esNosocomialMonitoringInfus(DataRow row)
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
			esNosocomialMonitoringInfusQuery query = this.GetDynamicQuery();
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
						case "SRIVCatheter": this.str.SRIVCatheter = (string)value; break;
						case "SRInfusSet": this.str.SRInfusSet = (string)value; break;
						case "IsSetBlood": this.str.IsSetBlood = (string)value; break;
						case "SRInfusLocation": this.str.SRInfusLocation = (string)value; break;
						case "IsTempAbove38": this.str.IsTempAbove38 = (string)value; break;
						case "IsPain": this.str.IsPain = (string)value; break;
						case "IsRedness": this.str.IsRedness = (string)value; break;
						case "IsFeelingHot": this.str.IsFeelingHot = (string)value; break;
						case "IsSwollen": this.str.IsSwollen = (string)value; break;
						case "IsPus": this.str.IsPus = (string)value; break;
						case "IsKanulaCulture": this.str.IsKanulaCulture = (string)value; break;
						case "MedicineAndLiquid": this.str.MedicineAndLiquid = (string)value; break;
						case "MedicationMethod": this.str.MedicationMethod = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "InfusLocation": this.str.InfusLocation = (string)value; break;
						case "ReleaseDateTime": this.str.ReleaseDateTime = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LiquidType": this.str.LiquidType = (string)value; break;
						case "IsDirty": this.str.IsDirty = (string)value; break;
						case "MonitoringByUserID": this.str.MonitoringByUserID = (string)value; break;
						case "IsApneu": this.str.IsApneu = (string)value; break;
						case "IsVeinHarden": this.str.IsVeinHarden = (string)value; break;
						case "IsShivers": this.str.IsShivers = (string)value; break;
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
						case "IsSetBlood":
						
							if (value == null || value is System.Boolean)
								this.IsSetBlood = (System.Boolean?)value;
							break;
						case "IsTempAbove38":
						
							if (value == null || value is System.Boolean)
								this.IsTempAbove38 = (System.Boolean?)value;
							break;
						case "IsPain":
						
							if (value == null || value is System.Boolean)
								this.IsPain = (System.Boolean?)value;
							break;
						case "IsRedness":
						
							if (value == null || value is System.Boolean)
								this.IsRedness = (System.Boolean?)value;
							break;
						case "IsFeelingHot":
						
							if (value == null || value is System.Boolean)
								this.IsFeelingHot = (System.Boolean?)value;
							break;
						case "IsSwollen":
						
							if (value == null || value is System.Boolean)
								this.IsSwollen = (System.Boolean?)value;
							break;
						case "IsPus":
						
							if (value == null || value is System.Boolean)
								this.IsPus = (System.Boolean?)value;
							break;
						case "IsKanulaCulture":
						
							if (value == null || value is System.Boolean)
								this.IsKanulaCulture = (System.Boolean?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ReleaseDateTime":
						
							if (value == null || value is System.DateTime)
								this.ReleaseDateTime = (System.DateTime?)value;
							break;
						case "IsDirty":
						
							if (value == null || value is System.Boolean)
								this.IsDirty = (System.Boolean?)value;
							break;
						case "IsApneu":
						
							if (value == null || value is System.Boolean)
								this.IsApneu = (System.Boolean?)value;
							break;
						case "IsVeinHarden":
						
							if (value == null || value is System.Boolean)
								this.IsVeinHarden = (System.Boolean?)value;
							break;
                        case "IsShivers":

                            if (value == null || value is System.Boolean)
                                this.IsShivers = (System.Boolean?)value;
                            break;
						case "IsInfected":

							if (value == null || value is System.Boolean)
								this.IsInfected = (System.Boolean?)value;
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
		/// Maps to NosocomialMonitoringInfus.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.MonitoringNo
		/// </summary>
		virtual public System.Int32? MonitoringNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringInfusMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringInfusMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.MonitoringDateTime
		/// </summary>
		virtual public System.DateTime? MonitoringDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.SRIVCatheter
		/// </summary>
		virtual public System.String SRIVCatheter
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.SRIVCatheter);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.SRIVCatheter, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.SRInfusSet
		/// </summary>
		virtual public System.String SRInfusSet
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusSet);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusSet, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsSetBlood
		/// </summary>
		virtual public System.Boolean? IsSetBlood
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsSetBlood);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsSetBlood, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.SRInfusLocation
		/// </summary>
		virtual public System.String SRInfusLocation
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusLocation);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusLocation, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsTempAbove38
		/// </summary>
		virtual public System.Boolean? IsTempAbove38
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsTempAbove38);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsTempAbove38, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsPain
		/// </summary>
		virtual public System.Boolean? IsPain
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsPain);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsPain, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsRedness
		/// </summary>
		virtual public System.Boolean? IsRedness
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsRedness);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsRedness, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsFeelingHot
		/// </summary>
		virtual public System.Boolean? IsFeelingHot
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsFeelingHot);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsFeelingHot, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsSwollen
		/// </summary>
		virtual public System.Boolean? IsSwollen
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsSwollen);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsSwollen, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsPus
		/// </summary>
		virtual public System.Boolean? IsPus
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsPus);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsPus, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsKanulaCulture
		/// </summary>
		virtual public System.Boolean? IsKanulaCulture
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsKanulaCulture);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsKanulaCulture, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.MedicineAndLiquid
		/// </summary>
		virtual public System.String MedicineAndLiquid
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.MedicineAndLiquid);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.MedicineAndLiquid, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.MedicationMethod
		/// </summary>
		virtual public System.String MedicationMethod
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.MedicationMethod);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.MedicationMethod, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.InfusLocation
		/// </summary>
		virtual public System.String InfusLocation
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.InfusLocation);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.InfusLocation, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.ReleaseDateTime
		/// </summary>
		virtual public System.DateTime? ReleaseDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringInfusMetadata.ColumnNames.ReleaseDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringInfusMetadata.ColumnNames.ReleaseDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.LiquidType
		/// </summary>
		virtual public System.String LiquidType
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.LiquidType);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.LiquidType, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsDirty
		/// </summary>
		virtual public System.Boolean? IsDirty
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsDirty);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsDirty, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.MonitoringByUserID
		/// </summary>
		virtual public System.String MonitoringByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsApneu
		/// </summary>
		virtual public System.Boolean? IsApneu
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsApneu);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsApneu, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringInfus.IsVeinHarden
		/// </summary>
		virtual public System.Boolean? IsVeinHarden
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsVeinHarden);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsVeinHarden, value);
			}
		}
        /// <summary>
        /// Maps to NosocomialMonitoringInfus.IsShivers
        /// </summary>
        virtual public System.Boolean? IsShivers
        {
            get
            {
                return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsShivers);
            }

            set
            {
                base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsShivers, value);
            }
        }
		virtual public System.Boolean? IsInfected
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsInfected);
			}

			set
			{
				base.SetSystemBoolean(NosocomialMonitoringInfusMetadata.ColumnNames.IsInfected, value);
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
			public esStrings(esNosocomialMonitoringInfus entity)
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
			public System.String SRInfusLocation
			{
				get
				{
					System.String data = entity.SRInfusLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRInfusLocation = null;
					else entity.SRInfusLocation = Convert.ToString(value);
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
			public System.String IsPain
			{
				get
				{
					System.Boolean? data = entity.IsPain;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPain = null;
					else entity.IsPain = Convert.ToBoolean(value);
				}
			}
			public System.String IsRedness
			{
				get
				{
					System.Boolean? data = entity.IsRedness;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRedness = null;
					else entity.IsRedness = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeelingHot
			{
				get
				{
					System.Boolean? data = entity.IsFeelingHot;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeelingHot = null;
					else entity.IsFeelingHot = Convert.ToBoolean(value);
				}
			}
			public System.String IsSwollen
			{
				get
				{
					System.Boolean? data = entity.IsSwollen;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSwollen = null;
					else entity.IsSwollen = Convert.ToBoolean(value);
				}
			}
			public System.String IsPus
			{
				get
				{
					System.Boolean? data = entity.IsPus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPus = null;
					else entity.IsPus = Convert.ToBoolean(value);
				}
			}
			public System.String IsKanulaCulture
			{
				get
				{
					System.Boolean? data = entity.IsKanulaCulture;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKanulaCulture = null;
					else entity.IsKanulaCulture = Convert.ToBoolean(value);
				}
			}
			public System.String MedicineAndLiquid
			{
				get
				{
					System.String data = entity.MedicineAndLiquid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicineAndLiquid = null;
					else entity.MedicineAndLiquid = Convert.ToString(value);
				}
			}
			public System.String MedicationMethod
			{
				get
				{
					System.String data = entity.MedicationMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicationMethod = null;
					else entity.MedicationMethod = Convert.ToString(value);
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
			public System.String InfusLocation
			{
				get
				{
					System.String data = entity.InfusLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InfusLocation = null;
					else entity.InfusLocation = Convert.ToString(value);
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
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			public System.String LiquidType
			{
				get
				{
					System.String data = entity.LiquidType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LiquidType = null;
					else entity.LiquidType = Convert.ToString(value);
				}
			}
			public System.String IsDirty
			{
				get
				{
					System.Boolean? data = entity.IsDirty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDirty = null;
					else entity.IsDirty = Convert.ToBoolean(value);
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
			public System.String IsApneu
			{
				get
				{
					System.Boolean? data = entity.IsApneu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApneu = null;
					else entity.IsApneu = Convert.ToBoolean(value);
				}
			}
			public System.String IsVeinHarden
			{
				get
				{
					System.Boolean? data = entity.IsVeinHarden;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVeinHarden = null;
					else entity.IsVeinHarden = Convert.ToBoolean(value);
				}
			}
            public System.String IsShivers
            {
                get
                {
                    System.Boolean? data = entity.IsShivers;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsShivers = null;
                    else entity.IsShivers = Convert.ToBoolean(value);
                }
            }
			public System.String IsInfected
			{
				get
				{
					System.Boolean? data = entity.IsInfected;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInfected = null;
					else entity.IsInfected = Convert.ToBoolean(value);
				}
			}
			private esNosocomialMonitoringInfus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringInfusQuery query)
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
				throw new Exception("esNosocomialMonitoringInfus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NosocomialMonitoringInfus : esNosocomialMonitoringInfus
	{	
	}

	[Serializable]
	abstract public class esNosocomialMonitoringInfusQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringInfusMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem MonitoringDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRIVCatheter
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.SRIVCatheter, esSystemType.String);
			}
		} 
			
		public esQueryItem SRInfusSet
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusSet, esSystemType.String);
			}
		} 
			
		public esQueryItem IsSetBlood
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsSetBlood, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRInfusLocation
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusLocation, esSystemType.String);
			}
		} 
			
		public esQueryItem IsTempAbove38
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsTempAbove38, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPain
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsPain, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsRedness
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsRedness, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsFeelingHot
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsFeelingHot, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSwollen
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsSwollen, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPus
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsPus, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsKanulaCulture
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsKanulaCulture, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem MedicineAndLiquid
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.MedicineAndLiquid, esSystemType.String);
			}
		} 
			
		public esQueryItem MedicationMethod
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.MedicationMethod, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem InfusLocation
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.InfusLocation, esSystemType.String);
			}
		} 
			
		public esQueryItem ReleaseDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.ReleaseDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem LiquidType
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.LiquidType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDirty
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsDirty, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem MonitoringByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsApneu
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsApneu, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVeinHarden
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsVeinHarden, esSystemType.Boolean);
			}
		}
        public esQueryItem IsShivers
        {
            get
            {
                return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsShivers, esSystemType.Boolean);
            }
        }

		public esQueryItem IsInfected
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringInfusMetadata.ColumnNames.IsInfected, esSystemType.Boolean);
			}
		}

	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NosocomialMonitoringInfusCollection")]
	public partial class NosocomialMonitoringInfusCollection : esNosocomialMonitoringInfusCollection, IEnumerable< NosocomialMonitoringInfus>
	{
		public NosocomialMonitoringInfusCollection()
		{

		}	
		
		public static implicit operator List< NosocomialMonitoringInfus>(NosocomialMonitoringInfusCollection coll)
		{
			List< NosocomialMonitoringInfus> list = new List< NosocomialMonitoringInfus>();
			
			foreach (NosocomialMonitoringInfus emp in coll)
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
				return  NosocomialMonitoringInfusMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringInfusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NosocomialMonitoringInfus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NosocomialMonitoringInfus();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NosocomialMonitoringInfusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringInfusQuery();
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
		public bool Load(NosocomialMonitoringInfusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NosocomialMonitoringInfus AddNew()
		{
			NosocomialMonitoringInfus entity = base.AddNewEntity() as NosocomialMonitoringInfus;
			
			return entity;		
		}
		public NosocomialMonitoringInfus FindByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, monitoringNo, sequenceNo) as NosocomialMonitoringInfus;
		}

		#region IEnumerable< NosocomialMonitoringInfus> Members

		IEnumerator< NosocomialMonitoringInfus> IEnumerable< NosocomialMonitoringInfus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NosocomialMonitoringInfus;
			}
		}

		#endregion
		
		private NosocomialMonitoringInfusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NosocomialMonitoringInfus' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NosocomialMonitoringInfus ({RegistrationNo, MonitoringNo, SequenceNo})")]
	[Serializable]
	public partial class NosocomialMonitoringInfus : esNosocomialMonitoringInfus
	{
		public NosocomialMonitoringInfus()
		{
		}	
	
		public NosocomialMonitoringInfus(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringInfusMetadata.Meta();
			}
		}	
	
		override protected esNosocomialMonitoringInfusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringInfusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NosocomialMonitoringInfusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringInfusQuery();
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
		public bool Load(NosocomialMonitoringInfusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NosocomialMonitoringInfusQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NosocomialMonitoringInfusQuery : esNosocomialMonitoringInfusQuery
	{
		public NosocomialMonitoringInfusQuery()
		{

		}		
		
		public NosocomialMonitoringInfusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NosocomialMonitoringInfusQuery";
        }
	}

	[Serializable]
	public partial class NosocomialMonitoringInfusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NosocomialMonitoringInfusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.MonitoringNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.SequenceNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.MonitoringDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.SRIVCatheter, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.SRIVCatheter;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusSet, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.SRInfusSet;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsSetBlood, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsSetBlood;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.SRInfusLocation, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.SRInfusLocation;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsTempAbove38, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsTempAbove38;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsPain, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsPain;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsRedness, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsRedness;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsFeelingHot, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsFeelingHot;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsSwollen, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsSwollen;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsPus, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsPus;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsKanulaCulture, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsKanulaCulture;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.MedicineAndLiquid, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.MedicineAndLiquid;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.MedicationMethod, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.MedicationMethod;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsDeleted, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.InfusLocation, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.InfusLocation;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.ReleaseDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.ReleaseDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.Notes, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.LiquidType, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.LiquidType;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsDirty, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsDirty;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.MonitoringByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.MonitoringByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsApneu, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsApneu;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsVeinHarden, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsVeinHarden;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsShivers, 28, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsShivers;
            c.IsNullable = true;
            _columns.Add(c);

			c = new esColumnMetadata(NosocomialMonitoringInfusMetadata.ColumnNames.IsInfected, 29, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringInfusMetadata.PropertyNames.IsInfected;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion
	
		static public NosocomialMonitoringInfusMetadata Meta()
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
			public const string SRIVCatheter = "SRIVCatheter";
			public const string SRInfusSet = "SRInfusSet";
			public const string IsSetBlood = "IsSetBlood";
			public const string SRInfusLocation = "SRInfusLocation";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsPain = "IsPain";
			public const string IsRedness = "IsRedness";
			public const string IsFeelingHot = "IsFeelingHot";
			public const string IsSwollen = "IsSwollen";
			public const string IsPus = "IsPus";
			public const string IsKanulaCulture = "IsKanulaCulture";
			public const string MedicineAndLiquid = "MedicineAndLiquid";
			public const string MedicationMethod = "MedicationMethod";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string InfusLocation = "InfusLocation";
			public const string ReleaseDateTime = "ReleaseDateTime";
			public const string Notes = "Notes";
			public const string LiquidType = "LiquidType";
			public const string IsDirty = "IsDirty";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsApneu = "IsApneu";
			public const string IsVeinHarden = "IsVeinHarden";
			public const string IsShivers = "IsShivers";
			public const string IsInfected = "IsInfected";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string MonitoringNo = "MonitoringNo";
			public const string SequenceNo = "SequenceNo";
			public const string MonitoringDateTime = "MonitoringDateTime";
			public const string SRIVCatheter = "SRIVCatheter";
			public const string SRInfusSet = "SRInfusSet";
			public const string IsSetBlood = "IsSetBlood";
			public const string SRInfusLocation = "SRInfusLocation";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsPain = "IsPain";
			public const string IsRedness = "IsRedness";
			public const string IsFeelingHot = "IsFeelingHot";
			public const string IsSwollen = "IsSwollen";
			public const string IsPus = "IsPus";
			public const string IsKanulaCulture = "IsKanulaCulture";
			public const string MedicineAndLiquid = "MedicineAndLiquid";
			public const string MedicationMethod = "MedicationMethod";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string InfusLocation = "InfusLocation";
			public const string ReleaseDateTime = "ReleaseDateTime";
			public const string Notes = "Notes";
			public const string LiquidType = "LiquidType";
			public const string IsDirty = "IsDirty";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsApneu = "IsApneu";
			public const string IsVeinHarden = "IsVeinHarden";
			public const string IsShivers = "IsShivers";
			public const string IsInfected = "IsInfected";
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
			lock (typeof(NosocomialMonitoringInfusMetadata))
			{
				if(NosocomialMonitoringInfusMetadata.mapDelegates == null)
				{
					NosocomialMonitoringInfusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NosocomialMonitoringInfusMetadata.meta == null)
				{
					NosocomialMonitoringInfusMetadata.meta = new NosocomialMonitoringInfusMetadata();
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
				meta.AddTypeMap("SRIVCatheter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRInfusSet", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSetBlood", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRInfusLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTempAbove38", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPain", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRedness", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeelingHot", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSwollen", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPus", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsKanulaCulture", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("MedicineAndLiquid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MedicationMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InfusLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReleaseDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LiquidType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDirty", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("MonitoringByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApneu", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVeinHarden", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShivers", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInfected", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "NosocomialMonitoringInfus";
				meta.Destination = "NosocomialMonitoringInfus";
				meta.spInsert = "proc_NosocomialMonitoringInfusInsert";				
				meta.spUpdate = "proc_NosocomialMonitoringInfusUpdate";		
				meta.spDelete = "proc_NosocomialMonitoringInfusDelete";
				meta.spLoadAll = "proc_NosocomialMonitoringInfusLoadAll";
				meta.spLoadByPrimaryKey = "proc_NosocomialMonitoringInfusLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NosocomialMonitoringInfusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
