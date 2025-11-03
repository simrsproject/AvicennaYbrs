/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 04/02/20 12:43:07 AM
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
	abstract public class esNosocomialMonitoringBedRestCollection : esEntityCollectionWAuditLog
	{
		public esNosocomialMonitoringBedRestCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NosocomialMonitoringBedRestCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringBedRestQuery query)
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
			this.InitQuery(query as esNosocomialMonitoringBedRestQuery);
		}
		#endregion
			
		virtual public NosocomialMonitoringBedRest DetachEntity(NosocomialMonitoringBedRest entity)
		{
			return base.DetachEntity(entity) as NosocomialMonitoringBedRest;
		}
		
		virtual public NosocomialMonitoringBedRest AttachEntity(NosocomialMonitoringBedRest entity)
		{
			return base.AttachEntity(entity) as NosocomialMonitoringBedRest;
		}
		
		virtual public void Combine(NosocomialMonitoringBedRestCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NosocomialMonitoringBedRest this[int index]
		{
			get
			{
				return base[index] as NosocomialMonitoringBedRest;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NosocomialMonitoringBedRest);
		}
	}

	[Serializable]
	abstract public class esNosocomialMonitoringBedRest : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNosocomialMonitoringBedRestQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNosocomialMonitoringBedRest()
		{
		}
	
		public esNosocomialMonitoringBedRest(DataRow row)
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
			esNosocomialMonitoringBedRestQuery query = this.GetDynamicQuery();
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
						case "IsMobilization": this.str.IsMobilization = (string)value; break;
						case "IsInjuryCare": this.str.IsInjuryCare = (string)value; break;
						case "SkinCondition": this.str.SkinCondition = (string)value; break;
						case "InjuryCondition": this.str.InjuryCondition = (string)value; break;
						case "Fisiotherapi": this.str.Fisiotherapi = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "MonitoringByUserID": this.str.MonitoringByUserID = (string)value; break;
						case "Mobilization": this.str.Mobilization = (string)value; break;
						case "IsSkinComplete": this.str.IsSkinComplete = (string)value; break;
						case "IsSkinRed": this.str.IsSkinRed = (string)value; break;
						case "IsSkinNoBlister": this.str.IsSkinNoBlister = (string)value; break;
						case "IsSkinWarm": this.str.IsSkinWarm = (string)value; break;
						case "IsSkinHard": this.str.IsSkinHard = (string)value; break;
						case "IsSkinItchy": this.str.IsSkinItchy = (string)value; break;
						case "IsInjuryBlister": this.str.IsInjuryBlister = (string)value; break;
						case "IsInjuryOpen": this.str.IsInjuryOpen = (string)value; break;
						case "IsInjuryToFat": this.str.IsInjuryToFat = (string)value; break;
						case "IsInjuryNekrosis": this.str.IsInjuryNekrosis = (string)value; break;
						case "IsInjuryToBone": this.str.IsInjuryToBone = (string)value; break;
						case "IsCulture": this.str.IsCulture = (string)value; break;
						case "IsDxDekubitus": this.str.IsDxDekubitus = (string)value; break;
						case "IsStop": this.str.IsStop = (string)value; break;
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
						case "IsMobilization":
						
							if (value == null || value is System.Boolean)
								this.IsMobilization = (System.Boolean?)value;
							break;
						case "IsInjuryCare":
						
							if (value == null || value is System.Boolean)
								this.IsInjuryCare = (System.Boolean?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsSkinComplete":
						
							if (value == null || value is System.Boolean)
								this.IsSkinComplete = (System.Boolean?)value;
							break;
						case "IsSkinRed":
						
							if (value == null || value is System.Boolean)
								this.IsSkinRed = (System.Boolean?)value;
							break;
						case "IsSkinNoBlister":
						
							if (value == null || value is System.Boolean)
								this.IsSkinNoBlister = (System.Boolean?)value;
							break;
						case "IsSkinWarm":
						
							if (value == null || value is System.Boolean)
								this.IsSkinWarm = (System.Boolean?)value;
							break;
						case "IsSkinHard":
						
							if (value == null || value is System.Boolean)
								this.IsSkinHard = (System.Boolean?)value;
							break;
						case "IsSkinItchy":
						
							if (value == null || value is System.Boolean)
								this.IsSkinItchy = (System.Boolean?)value;
							break;
						case "IsInjuryBlister":
						
							if (value == null || value is System.Boolean)
								this.IsInjuryBlister = (System.Boolean?)value;
							break;
						case "IsInjuryOpen":
						
							if (value == null || value is System.Boolean)
								this.IsInjuryOpen = (System.Boolean?)value;
							break;
						case "IsInjuryToFat":
						
							if (value == null || value is System.Boolean)
								this.IsInjuryToFat = (System.Boolean?)value;
							break;
						case "IsInjuryNekrosis":
						
							if (value == null || value is System.Boolean)
								this.IsInjuryNekrosis = (System.Boolean?)value;
							break;
						case "IsInjuryToBone":
						
							if (value == null || value is System.Boolean)
								this.IsInjuryToBone = (System.Boolean?)value;
							break;
						case "IsCulture":
						
							if (value == null || value is System.Boolean)
								this.IsCulture = (System.Boolean?)value;
							break;
						case "IsDxDekubitus":
						
							if (value == null || value is System.Boolean)
								this.IsDxDekubitus = (System.Boolean?)value;
							break;
						case "IsStop":
						
							if (value == null || value is System.Boolean)
								this.IsStop = (System.Boolean?)value;
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
		/// Maps to NosocomialMonitoringBedRest.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.MonitoringNo
		/// </summary>
		virtual public System.Int32? MonitoringNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringBedRestMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringBedRestMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.MonitoringDateTime
		/// </summary>
		virtual public System.DateTime? MonitoringDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsMobilization
		/// </summary>
		virtual public System.Boolean? IsMobilization
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsMobilization);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsMobilization, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsInjuryCare
		/// </summary>
		virtual public System.Boolean? IsInjuryCare
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryCare);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryCare, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.SkinCondition
		/// </summary>
		virtual public System.String SkinCondition
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.SkinCondition);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.SkinCondition, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.InjuryCondition
		/// </summary>
		virtual public System.String InjuryCondition
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.InjuryCondition);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.InjuryCondition, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.Fisiotherapi
		/// </summary>
		virtual public System.String Fisiotherapi
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.Fisiotherapi);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.Fisiotherapi, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.MonitoringByUserID
		/// </summary>
		virtual public System.String MonitoringByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.Mobilization
		/// </summary>
		virtual public System.String Mobilization
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.Mobilization);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringBedRestMetadata.ColumnNames.Mobilization, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsSkinComplete
		/// </summary>
		virtual public System.Boolean? IsSkinComplete
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinComplete);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinComplete, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsSkinRed
		/// </summary>
		virtual public System.Boolean? IsSkinRed
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinRed);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinRed, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsSkinNoBlister
		/// </summary>
		virtual public System.Boolean? IsSkinNoBlister
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinNoBlister);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinNoBlister, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsSkinWarm
		/// </summary>
		virtual public System.Boolean? IsSkinWarm
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinWarm);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinWarm, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsSkinHard
		/// </summary>
		virtual public System.Boolean? IsSkinHard
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinHard);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinHard, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsSkinItchy
		/// </summary>
		virtual public System.Boolean? IsSkinItchy
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinItchy);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinItchy, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsInjuryBlister
		/// </summary>
		virtual public System.Boolean? IsInjuryBlister
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryBlister);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryBlister, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsInjuryOpen
		/// </summary>
		virtual public System.Boolean? IsInjuryOpen
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryOpen);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryOpen, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsInjuryToFat
		/// </summary>
		virtual public System.Boolean? IsInjuryToFat
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToFat);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToFat, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsInjuryNekrosis
		/// </summary>
		virtual public System.Boolean? IsInjuryNekrosis
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryNekrosis);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryNekrosis, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsInjuryToBone
		/// </summary>
		virtual public System.Boolean? IsInjuryToBone
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToBone);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToBone, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsCulture
		/// </summary>
		virtual public System.Boolean? IsCulture
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsCulture);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsCulture, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsDxDekubitus
		/// </summary>
		virtual public System.Boolean? IsDxDekubitus
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsDxDekubitus);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsDxDekubitus, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringBedRest.IsStop
		/// </summary>
		virtual public System.Boolean? IsStop
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsStop);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringBedRestMetadata.ColumnNames.IsStop, value);
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
			public esStrings(esNosocomialMonitoringBedRest entity)
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
			public System.String IsMobilization
			{
				get
				{
					System.Boolean? data = entity.IsMobilization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMobilization = null;
					else entity.IsMobilization = Convert.ToBoolean(value);
				}
			}
			public System.String IsInjuryCare
			{
				get
				{
					System.Boolean? data = entity.IsInjuryCare;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInjuryCare = null;
					else entity.IsInjuryCare = Convert.ToBoolean(value);
				}
			}
			public System.String SkinCondition
			{
				get
				{
					System.String data = entity.SkinCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SkinCondition = null;
					else entity.SkinCondition = Convert.ToString(value);
				}
			}
			public System.String InjuryCondition
			{
				get
				{
					System.String data = entity.InjuryCondition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InjuryCondition = null;
					else entity.InjuryCondition = Convert.ToString(value);
				}
			}
			public System.String Fisiotherapi
			{
				get
				{
					System.String data = entity.Fisiotherapi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Fisiotherapi = null;
					else entity.Fisiotherapi = Convert.ToString(value);
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
			public System.String Mobilization
			{
				get
				{
					System.String data = entity.Mobilization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Mobilization = null;
					else entity.Mobilization = Convert.ToString(value);
				}
			}
			public System.String IsSkinComplete
			{
				get
				{
					System.Boolean? data = entity.IsSkinComplete;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSkinComplete = null;
					else entity.IsSkinComplete = Convert.ToBoolean(value);
				}
			}
			public System.String IsSkinRed
			{
				get
				{
					System.Boolean? data = entity.IsSkinRed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSkinRed = null;
					else entity.IsSkinRed = Convert.ToBoolean(value);
				}
			}
			public System.String IsSkinNoBlister
			{
				get
				{
					System.Boolean? data = entity.IsSkinNoBlister;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSkinNoBlister = null;
					else entity.IsSkinNoBlister = Convert.ToBoolean(value);
				}
			}
			public System.String IsSkinWarm
			{
				get
				{
					System.Boolean? data = entity.IsSkinWarm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSkinWarm = null;
					else entity.IsSkinWarm = Convert.ToBoolean(value);
				}
			}
			public System.String IsSkinHard
			{
				get
				{
					System.Boolean? data = entity.IsSkinHard;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSkinHard = null;
					else entity.IsSkinHard = Convert.ToBoolean(value);
				}
			}
			public System.String IsSkinItchy
			{
				get
				{
					System.Boolean? data = entity.IsSkinItchy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSkinItchy = null;
					else entity.IsSkinItchy = Convert.ToBoolean(value);
				}
			}
			public System.String IsInjuryBlister
			{
				get
				{
					System.Boolean? data = entity.IsInjuryBlister;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInjuryBlister = null;
					else entity.IsInjuryBlister = Convert.ToBoolean(value);
				}
			}
			public System.String IsInjuryOpen
			{
				get
				{
					System.Boolean? data = entity.IsInjuryOpen;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInjuryOpen = null;
					else entity.IsInjuryOpen = Convert.ToBoolean(value);
				}
			}
			public System.String IsInjuryToFat
			{
				get
				{
					System.Boolean? data = entity.IsInjuryToFat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInjuryToFat = null;
					else entity.IsInjuryToFat = Convert.ToBoolean(value);
				}
			}
			public System.String IsInjuryNekrosis
			{
				get
				{
					System.Boolean? data = entity.IsInjuryNekrosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInjuryNekrosis = null;
					else entity.IsInjuryNekrosis = Convert.ToBoolean(value);
				}
			}
			public System.String IsInjuryToBone
			{
				get
				{
					System.Boolean? data = entity.IsInjuryToBone;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInjuryToBone = null;
					else entity.IsInjuryToBone = Convert.ToBoolean(value);
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
			public System.String IsDxDekubitus
			{
				get
				{
					System.Boolean? data = entity.IsDxDekubitus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDxDekubitus = null;
					else entity.IsDxDekubitus = Convert.ToBoolean(value);
				}
			}
			public System.String IsStop
			{
				get
				{
					System.Boolean? data = entity.IsStop;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsStop = null;
					else entity.IsStop = Convert.ToBoolean(value);
				}
			}
			private esNosocomialMonitoringBedRest entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringBedRestQuery query)
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
				throw new Exception("esNosocomialMonitoringBedRest can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NosocomialMonitoringBedRest : esNosocomialMonitoringBedRest
	{	
	}

	[Serializable]
	abstract public class esNosocomialMonitoringBedRestQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringBedRestMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem MonitoringDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsMobilization
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsMobilization, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsInjuryCare
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryCare, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SkinCondition
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.SkinCondition, esSystemType.String);
			}
		} 
			
		public esQueryItem InjuryCondition
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.InjuryCondition, esSystemType.String);
			}
		} 
			
		public esQueryItem Fisiotherapi
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.Fisiotherapi, esSystemType.String);
			}
		} 
			
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem Mobilization
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.Mobilization, esSystemType.String);
			}
		} 
			
		public esQueryItem IsSkinComplete
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinComplete, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSkinRed
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinRed, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSkinNoBlister
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinNoBlister, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSkinWarm
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinWarm, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSkinHard
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinHard, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSkinItchy
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinItchy, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsInjuryBlister
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryBlister, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsInjuryOpen
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryOpen, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsInjuryToFat
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToFat, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsInjuryNekrosis
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryNekrosis, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsInjuryToBone
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToBone, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsCulture
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsCulture, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDxDekubitus
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsDxDekubitus, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsStop
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringBedRestMetadata.ColumnNames.IsStop, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NosocomialMonitoringBedRestCollection")]
	public partial class NosocomialMonitoringBedRestCollection : esNosocomialMonitoringBedRestCollection, IEnumerable< NosocomialMonitoringBedRest>
	{
		public NosocomialMonitoringBedRestCollection()
		{

		}	
		
		public static implicit operator List< NosocomialMonitoringBedRest>(NosocomialMonitoringBedRestCollection coll)
		{
			List< NosocomialMonitoringBedRest> list = new List< NosocomialMonitoringBedRest>();
			
			foreach (NosocomialMonitoringBedRest emp in coll)
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
				return  NosocomialMonitoringBedRestMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringBedRestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NosocomialMonitoringBedRest(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NosocomialMonitoringBedRest();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NosocomialMonitoringBedRestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringBedRestQuery();
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
		public bool Load(NosocomialMonitoringBedRestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NosocomialMonitoringBedRest AddNew()
		{
			NosocomialMonitoringBedRest entity = base.AddNewEntity() as NosocomialMonitoringBedRest;
			
			return entity;		
		}
		public NosocomialMonitoringBedRest FindByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, monitoringNo, sequenceNo) as NosocomialMonitoringBedRest;
		}

		#region IEnumerable< NosocomialMonitoringBedRest> Members

		IEnumerator< NosocomialMonitoringBedRest> IEnumerable< NosocomialMonitoringBedRest>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NosocomialMonitoringBedRest;
			}
		}

		#endregion
		
		private NosocomialMonitoringBedRestQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NosocomialMonitoringBedRest' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NosocomialMonitoringBedRest ({RegistrationNo, MonitoringNo, SequenceNo})")]
	[Serializable]
	public partial class NosocomialMonitoringBedRest : esNosocomialMonitoringBedRest
	{
		public NosocomialMonitoringBedRest()
		{
		}	
	
		public NosocomialMonitoringBedRest(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringBedRestMetadata.Meta();
			}
		}	
	
		override protected esNosocomialMonitoringBedRestQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringBedRestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NosocomialMonitoringBedRestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringBedRestQuery();
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
		public bool Load(NosocomialMonitoringBedRestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NosocomialMonitoringBedRestQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NosocomialMonitoringBedRestQuery : esNosocomialMonitoringBedRestQuery
	{
		public NosocomialMonitoringBedRestQuery()
		{

		}		
		
		public NosocomialMonitoringBedRestQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NosocomialMonitoringBedRestQuery";
        }
	}

	[Serializable]
	public partial class NosocomialMonitoringBedRestMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NosocomialMonitoringBedRestMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.MonitoringNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.SequenceNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.MonitoringDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsMobilization, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsMobilization;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryCare, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsInjuryCare;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.SkinCondition, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.SkinCondition;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.InjuryCondition, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.InjuryCondition;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.Fisiotherapi, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.Fisiotherapi;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.Note, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsDeleted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.MonitoringByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.MonitoringByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.Mobilization, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.Mobilization;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinComplete, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsSkinComplete;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinRed, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsSkinRed;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinNoBlister, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsSkinNoBlister;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinWarm, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsSkinWarm;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinHard, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsSkinHard;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsSkinItchy, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsSkinItchy;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryBlister, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsInjuryBlister;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryOpen, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsInjuryOpen;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToFat, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsInjuryToFat;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryNekrosis, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsInjuryNekrosis;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsInjuryToBone, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsInjuryToBone;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsCulture, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsCulture;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsDxDekubitus, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsDxDekubitus;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringBedRestMetadata.ColumnNames.IsStop, 28, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringBedRestMetadata.PropertyNames.IsStop;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public NosocomialMonitoringBedRestMetadata Meta()
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
			public const string IsMobilization = "IsMobilization";
			public const string IsInjuryCare = "IsInjuryCare";
			public const string SkinCondition = "SkinCondition";
			public const string InjuryCondition = "InjuryCondition";
			public const string Fisiotherapi = "Fisiotherapi";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string Mobilization = "Mobilization";
			public const string IsSkinComplete = "IsSkinComplete";
			public const string IsSkinRed = "IsSkinRed";
			public const string IsSkinNoBlister = "IsSkinNoBlister";
			public const string IsSkinWarm = "IsSkinWarm";
			public const string IsSkinHard = "IsSkinHard";
			public const string IsSkinItchy = "IsSkinItchy";
			public const string IsInjuryBlister = "IsInjuryBlister";
			public const string IsInjuryOpen = "IsInjuryOpen";
			public const string IsInjuryToFat = "IsInjuryToFat";
			public const string IsInjuryNekrosis = "IsInjuryNekrosis";
			public const string IsInjuryToBone = "IsInjuryToBone";
			public const string IsCulture = "IsCulture";
			public const string IsDxDekubitus = "IsDxDekubitus";
			public const string IsStop = "IsStop";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string MonitoringNo = "MonitoringNo";
			public const string SequenceNo = "SequenceNo";
			public const string MonitoringDateTime = "MonitoringDateTime";
			public const string IsMobilization = "IsMobilization";
			public const string IsInjuryCare = "IsInjuryCare";
			public const string SkinCondition = "SkinCondition";
			public const string InjuryCondition = "InjuryCondition";
			public const string Fisiotherapi = "Fisiotherapi";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string Mobilization = "Mobilization";
			public const string IsSkinComplete = "IsSkinComplete";
			public const string IsSkinRed = "IsSkinRed";
			public const string IsSkinNoBlister = "IsSkinNoBlister";
			public const string IsSkinWarm = "IsSkinWarm";
			public const string IsSkinHard = "IsSkinHard";
			public const string IsSkinItchy = "IsSkinItchy";
			public const string IsInjuryBlister = "IsInjuryBlister";
			public const string IsInjuryOpen = "IsInjuryOpen";
			public const string IsInjuryToFat = "IsInjuryToFat";
			public const string IsInjuryNekrosis = "IsInjuryNekrosis";
			public const string IsInjuryToBone = "IsInjuryToBone";
			public const string IsCulture = "IsCulture";
			public const string IsDxDekubitus = "IsDxDekubitus";
			public const string IsStop = "IsStop";
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
			lock (typeof(NosocomialMonitoringBedRestMetadata))
			{
				if(NosocomialMonitoringBedRestMetadata.mapDelegates == null)
				{
					NosocomialMonitoringBedRestMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NosocomialMonitoringBedRestMetadata.meta == null)
				{
					NosocomialMonitoringBedRestMetadata.meta = new NosocomialMonitoringBedRestMetadata();
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
				meta.AddTypeMap("IsMobilization", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInjuryCare", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SkinCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InjuryCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Fisiotherapi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonitoringByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Mobilization", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSkinComplete", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSkinRed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSkinNoBlister", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSkinWarm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSkinHard", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSkinItchy", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInjuryBlister", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInjuryOpen", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInjuryToFat", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInjuryNekrosis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInjuryToBone", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCulture", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDxDekubitus", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsStop", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "NosocomialMonitoringBedRest";
				meta.Destination = "NosocomialMonitoringBedRest";
				meta.spInsert = "proc_NosocomialMonitoringBedRestInsert";				
				meta.spUpdate = "proc_NosocomialMonitoringBedRestUpdate";		
				meta.spDelete = "proc_NosocomialMonitoringBedRestDelete";
				meta.spLoadAll = "proc_NosocomialMonitoringBedRestLoadAll";
				meta.spLoadByPrimaryKey = "proc_NosocomialMonitoringBedRestLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NosocomialMonitoringBedRestMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
