/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/18/19 6:57:10 AM
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
	abstract public class esNosocomialMonitoringSurgeryCollection : esEntityCollectionWAuditLog
	{
		public esNosocomialMonitoringSurgeryCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NosocomialMonitoringSurgeryCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringSurgeryQuery query)
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
			this.InitQuery(query as esNosocomialMonitoringSurgeryQuery);
		}
		#endregion
			
		virtual public NosocomialMonitoringSurgery DetachEntity(NosocomialMonitoringSurgery entity)
		{
			return base.DetachEntity(entity) as NosocomialMonitoringSurgery;
		}
		
		virtual public NosocomialMonitoringSurgery AttachEntity(NosocomialMonitoringSurgery entity)
		{
			return base.AttachEntity(entity) as NosocomialMonitoringSurgery;
		}
		
		virtual public void Combine(NosocomialMonitoringSurgeryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NosocomialMonitoringSurgery this[int index]
		{
			get
			{
				return base[index] as NosocomialMonitoringSurgery;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NosocomialMonitoringSurgery);
		}
	}

	[Serializable]
	abstract public class esNosocomialMonitoringSurgery : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNosocomialMonitoringSurgeryQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNosocomialMonitoringSurgery()
		{
		}
	
		public esNosocomialMonitoringSurgery(DataRow row)
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
			esNosocomialMonitoringSurgeryQuery query = this.GetDynamicQuery();
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
						case "SRExudateCharacter": this.str.SRExudateCharacter = (string)value; break;
						case "IsAfDrain": this.str.IsAfDrain = (string)value; break;
						case "IsAfSuture": this.str.IsAfSuture = (string)value; break;
						case "IsRedness": this.str.IsRedness = (string)value; break;
						case "IsSwollen": this.str.IsSwollen = (string)value; break;
						case "IsPain": this.str.IsPain = (string)value; break;
						case "IsFeelingHot": this.str.IsFeelingHot = (string)value; break;
						case "IsTempAbove38": this.str.IsTempAbove38 = (string)value; break;
						case "IsPus": this.str.IsPus = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsCulture": this.str.IsCulture = (string)value; break;
						case "InjuryCondition": this.str.InjuryCondition = (string)value; break;
						case "MonitoringByUserID": this.str.MonitoringByUserID = (string)value; break;
						case "IsIdoDiagnose": this.str.IsIdoDiagnose = (string)value; break;
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
						case "IsAfDrain":
						
							if (value == null || value is System.Boolean)
								this.IsAfDrain = (System.Boolean?)value;
							break;
						case "IsAfSuture":
						
							if (value == null || value is System.Boolean)
								this.IsAfSuture = (System.Boolean?)value;
							break;
						case "IsRedness":
						
							if (value == null || value is System.Boolean)
								this.IsRedness = (System.Boolean?)value;
							break;
						case "IsSwollen":
						
							if (value == null || value is System.Boolean)
								this.IsSwollen = (System.Boolean?)value;
							break;
						case "IsPain":
						
							if (value == null || value is System.Boolean)
								this.IsPain = (System.Boolean?)value;
							break;
						case "IsFeelingHot":
						
							if (value == null || value is System.Boolean)
								this.IsFeelingHot = (System.Boolean?)value;
							break;
						case "IsTempAbove38":
						
							if (value == null || value is System.Boolean)
								this.IsTempAbove38 = (System.Boolean?)value;
							break;
						case "IsPus":
						
							if (value == null || value is System.Boolean)
								this.IsPus = (System.Boolean?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsCulture":
						
							if (value == null || value is System.Boolean)
								this.IsCulture = (System.Boolean?)value;
							break;
						case "IsIdoDiagnose":
						
							if (value == null || value is System.Boolean)
								this.IsIdoDiagnose = (System.Boolean?)value;
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
		/// Maps to NosocomialMonitoringSurgery.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.MonitoringNo
		/// </summary>
		virtual public System.Int32? MonitoringNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringSurgeryMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringSurgeryMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.MonitoringDateTime
		/// </summary>
		virtual public System.DateTime? MonitoringDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.SRExudateCharacter
		/// </summary>
		virtual public System.String SRExudateCharacter
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.SRExudateCharacter);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.SRExudateCharacter, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsAfDrain
		/// </summary>
		virtual public System.Boolean? IsAfDrain
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfDrain);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfDrain, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsAfSuture
		/// </summary>
		virtual public System.Boolean? IsAfSuture
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfSuture);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfSuture, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsRedness
		/// </summary>
		virtual public System.Boolean? IsRedness
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsRedness);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsRedness, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsSwollen
		/// </summary>
		virtual public System.Boolean? IsSwollen
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsSwollen);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsSwollen, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsPain
		/// </summary>
		virtual public System.Boolean? IsPain
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPain);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPain, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsFeelingHot
		/// </summary>
		virtual public System.Boolean? IsFeelingHot
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsFeelingHot);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsFeelingHot, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsTempAbove38
		/// </summary>
		virtual public System.Boolean? IsTempAbove38
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsTempAbove38);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsTempAbove38, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsPus
		/// </summary>
		virtual public System.Boolean? IsPus
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPus);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPus, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsCulture
		/// </summary>
		virtual public System.Boolean? IsCulture
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsCulture);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsCulture, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.InjuryCondition
		/// </summary>
		virtual public System.String InjuryCondition
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.InjuryCondition);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.InjuryCondition, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.MonitoringByUserID
		/// </summary>
		virtual public System.String MonitoringByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.IsIdoDiagnose
		/// </summary>
		virtual public System.Boolean? IsIdoDiagnose
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsIdoDiagnose);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsIdoDiagnose, value);
			}
		}

		virtual public System.Boolean? IsGlukosa
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsGlukosa);
			}

			set
			{
				base.SetSystemBoolean(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsGlukosa, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringSurgery.LamaOperasi
		/// </summary>
		virtual public System.Int32? LamaOperasi
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringSurgeryMetadata.ColumnNames.LamaOperasi);
			}

			set
			{
				base.SetSystemInt32(NosocomialMonitoringSurgeryMetadata.ColumnNames.LamaOperasi, value);
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
			public esStrings(esNosocomialMonitoringSurgery entity)
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
			public System.String SRExudateCharacter
			{
				get
				{
					System.String data = entity.SRExudateCharacter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRExudateCharacter = null;
					else entity.SRExudateCharacter = Convert.ToString(value);
				}
			}
			public System.String IsAfDrain
			{
				get
				{
					System.Boolean? data = entity.IsAfDrain;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAfDrain = null;
					else entity.IsAfDrain = Convert.ToBoolean(value);
				}
			}
			public System.String IsAfSuture
			{
				get
				{
					System.Boolean? data = entity.IsAfSuture;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAfSuture = null;
					else entity.IsAfSuture = Convert.ToBoolean(value);
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
			public System.String IsIdoDiagnose
			{
				get
				{
					System.Boolean? data = entity.IsIdoDiagnose;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIdoDiagnose = null;
					else entity.IsIdoDiagnose = Convert.ToBoolean(value);
				}
			}

			public System.String IsGlukosa
			{
				get
				{
					System.Boolean? data = entity.IsGlukosa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGlukosa = null;
					else entity.IsGlukosa = Convert.ToBoolean(value);
				}
			}
			private esNosocomialMonitoringSurgery entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringSurgeryQuery query)
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
				throw new Exception("esNosocomialMonitoringSurgery can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NosocomialMonitoringSurgery : esNosocomialMonitoringSurgery
	{	
	}

	[Serializable]
	abstract public class esNosocomialMonitoringSurgeryQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringSurgeryMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem MonitoringDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRExudateCharacter
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.SRExudateCharacter, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAfDrain
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfDrain, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsAfSuture
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfSuture, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsRedness
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsRedness, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsSwollen
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsSwollen, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPain
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPain, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsFeelingHot
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsFeelingHot, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsTempAbove38
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsTempAbove38, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPus
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPus, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsCulture
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsCulture, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem InjuryCondition
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.InjuryCondition, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsIdoDiagnose
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsIdoDiagnose, esSystemType.Boolean);
			}
		}

		public esQueryItem IsGlukosa
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.IsGlukosa, esSystemType.Boolean);
			}
		}

		public esQueryItem LamaOperasi
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringSurgeryMetadata.ColumnNames.LamaOperasi, esSystemType.Int32);
			}
		}

	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NosocomialMonitoringSurgeryCollection")]
	public partial class NosocomialMonitoringSurgeryCollection : esNosocomialMonitoringSurgeryCollection, IEnumerable< NosocomialMonitoringSurgery>
	{
		public NosocomialMonitoringSurgeryCollection()
		{

		}	
		
		public static implicit operator List< NosocomialMonitoringSurgery>(NosocomialMonitoringSurgeryCollection coll)
		{
			List< NosocomialMonitoringSurgery> list = new List< NosocomialMonitoringSurgery>();
			
			foreach (NosocomialMonitoringSurgery emp in coll)
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
				return  NosocomialMonitoringSurgeryMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringSurgeryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NosocomialMonitoringSurgery(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NosocomialMonitoringSurgery();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NosocomialMonitoringSurgeryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringSurgeryQuery();
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
		public bool Load(NosocomialMonitoringSurgeryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NosocomialMonitoringSurgery AddNew()
		{
			NosocomialMonitoringSurgery entity = base.AddNewEntity() as NosocomialMonitoringSurgery;
			
			return entity;		
		}
		public NosocomialMonitoringSurgery FindByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, monitoringNo, sequenceNo) as NosocomialMonitoringSurgery;
		}

		#region IEnumerable< NosocomialMonitoringSurgery> Members

		IEnumerator< NosocomialMonitoringSurgery> IEnumerable< NosocomialMonitoringSurgery>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NosocomialMonitoringSurgery;
			}
		}

		#endregion
		
		private NosocomialMonitoringSurgeryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NosocomialMonitoringSurgery' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NosocomialMonitoringSurgery ({RegistrationNo, MonitoringNo, SequenceNo})")]
	[Serializable]
	public partial class NosocomialMonitoringSurgery : esNosocomialMonitoringSurgery
	{
		public NosocomialMonitoringSurgery()
		{
		}	
	
		public NosocomialMonitoringSurgery(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringSurgeryMetadata.Meta();
			}
		}	
	
		override protected esNosocomialMonitoringSurgeryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringSurgeryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NosocomialMonitoringSurgeryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringSurgeryQuery();
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
		public bool Load(NosocomialMonitoringSurgeryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NosocomialMonitoringSurgeryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NosocomialMonitoringSurgeryQuery : esNosocomialMonitoringSurgeryQuery
	{
		public NosocomialMonitoringSurgeryQuery()
		{

		}		
		
		public NosocomialMonitoringSurgeryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NosocomialMonitoringSurgeryQuery";
        }
	}

	[Serializable]
	public partial class NosocomialMonitoringSurgeryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NosocomialMonitoringSurgeryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.MonitoringNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.SequenceNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.MonitoringDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.SRExudateCharacter, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.SRExudateCharacter;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfDrain, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsAfDrain;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsAfSuture, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsAfSuture;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsRedness, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsRedness;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsSwollen, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsSwollen;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPain, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsPain;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsFeelingHot, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsFeelingHot;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsTempAbove38, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsTempAbove38;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsPus, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsPus;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.Note, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsDeleted, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsCulture, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsCulture;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.InjuryCondition, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.InjuryCondition;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.MonitoringByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.MonitoringByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsIdoDiagnose, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsIdoDiagnose;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.IsGlukosa, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.IsGlukosa;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(NosocomialMonitoringSurgeryMetadata.ColumnNames.LamaOperasi, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringSurgeryMetadata.PropertyNames.LamaOperasi;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion
	
		static public NosocomialMonitoringSurgeryMetadata Meta()
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
			public const string SRExudateCharacter = "SRExudateCharacter";
			public const string IsAfDrain = "IsAfDrain";
			public const string IsAfSuture = "IsAfSuture";
			public const string IsRedness = "IsRedness";
			public const string IsSwollen = "IsSwollen";
			public const string IsPain = "IsPain";
			public const string IsFeelingHot = "IsFeelingHot";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsPus = "IsPus";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsCulture = "IsCulture";
			public const string InjuryCondition = "InjuryCondition";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsIdoDiagnose = "IsIdoDiagnose";
			public const string IsGlukosa = "IsGlukosa";
			public const string LamaOperasi = "LamaOperasi";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string MonitoringNo = "MonitoringNo";
			public const string SequenceNo = "SequenceNo";
			public const string MonitoringDateTime = "MonitoringDateTime";
			public const string SRExudateCharacter = "SRExudateCharacter";
			public const string IsAfDrain = "IsAfDrain";
			public const string IsAfSuture = "IsAfSuture";
			public const string IsRedness = "IsRedness";
			public const string IsSwollen = "IsSwollen";
			public const string IsPain = "IsPain";
			public const string IsFeelingHot = "IsFeelingHot";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsPus = "IsPus";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsCulture = "IsCulture";
			public const string InjuryCondition = "InjuryCondition";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsIdoDiagnose = "IsIdoDiagnose";
			public const string IsGlukosa = "IsGlukosa";
			public const string LamaOperasi = "LamaOperasi";
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
			lock (typeof(NosocomialMonitoringSurgeryMetadata))
			{
				if(NosocomialMonitoringSurgeryMetadata.mapDelegates == null)
				{
					NosocomialMonitoringSurgeryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NosocomialMonitoringSurgeryMetadata.meta == null)
				{
					NosocomialMonitoringSurgeryMetadata.meta = new NosocomialMonitoringSurgeryMetadata();
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
				meta.AddTypeMap("SRExudateCharacter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAfDrain", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAfSuture", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRedness", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSwollen", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPain", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeelingHot", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTempAbove38", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPus", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCulture", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InjuryCondition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonitoringByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsIdoDiagnose", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGlukosa", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LamaOperasi", new esTypeMap("int", "System.Int32"));


				meta.Source = "NosocomialMonitoringSurgery";
				meta.Destination = "NosocomialMonitoringSurgery";
				meta.spInsert = "proc_NosocomialMonitoringSurgeryInsert";				
				meta.spUpdate = "proc_NosocomialMonitoringSurgeryUpdate";		
				meta.spDelete = "proc_NosocomialMonitoringSurgeryDelete";
				meta.spLoadAll = "proc_NosocomialMonitoringSurgeryLoadAll";
				meta.spLoadByPrimaryKey = "proc_NosocomialMonitoringSurgeryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NosocomialMonitoringSurgeryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
