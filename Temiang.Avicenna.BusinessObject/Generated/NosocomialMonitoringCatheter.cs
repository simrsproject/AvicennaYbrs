/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/18/19 6:56:50 AM
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
	abstract public class esNosocomialMonitoringCatheterCollection : esEntityCollectionWAuditLog
	{
		public esNosocomialMonitoringCatheterCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "NosocomialMonitoringCatheterCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringCatheterQuery query)
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
			this.InitQuery(query as esNosocomialMonitoringCatheterQuery);
		}
		#endregion
			
		virtual public NosocomialMonitoringCatheter DetachEntity(NosocomialMonitoringCatheter entity)
		{
			return base.DetachEntity(entity) as NosocomialMonitoringCatheter;
		}
		
		virtual public NosocomialMonitoringCatheter AttachEntity(NosocomialMonitoringCatheter entity)
		{
			return base.AttachEntity(entity) as NosocomialMonitoringCatheter;
		}
		
		virtual public void Combine(NosocomialMonitoringCatheterCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NosocomialMonitoringCatheter this[int index]
		{
			get
			{
				return base[index] as NosocomialMonitoringCatheter;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NosocomialMonitoringCatheter);
		}
	}

	[Serializable]
	abstract public class esNosocomialMonitoringCatheter : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNosocomialMonitoringCatheterQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esNosocomialMonitoringCatheter()
		{
		}
	
		public esNosocomialMonitoringCatheter(DataRow row)
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
			esNosocomialMonitoringCatheterQuery query = this.GetDynamicQuery();
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
						case "SRGeneralChateterNo": this.str.SRGeneralChateterNo = (string)value; break;
						case "SRSiliconChateterNo": this.str.SRSiliconChateterNo = (string)value; break;
						case "IsTempAbove38": this.str.IsTempAbove38 = (string)value; break;
						case "IsDisuria": this.str.IsDisuria = (string)value; break;
						case "IsPain": this.str.IsPain = (string)value; break;
						case "IsPyuria": this.str.IsPyuria = (string)value; break;
						case "IsHematuria": this.str.IsHematuria = (string)value; break;
						case "IsUrineCulture": this.str.IsUrineCulture = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsUrineBagChange": this.str.IsUrineBagChange = (string)value; break;
						case "FixationFluid": this.str.FixationFluid = (string)value; break;
						case "MonitoringByUserID": this.str.MonitoringByUserID = (string)value; break;
						case "IsRelease": this.str.IsRelease = (string)value; break;
						case "IsApneu": this.str.IsApneu = (string)value; break;
						case "IsIskDiagnose": this.str.IsIskDiagnose = (string)value; break;
						case "IsUrineRutin": this.str.IsUrineRutin = (string)value; break;
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
						case "IsDisuria":
						
							if (value == null || value is System.Boolean)
								this.IsDisuria = (System.Boolean?)value;
							break;
						case "IsPain":
						
							if (value == null || value is System.Boolean)
								this.IsPain = (System.Boolean?)value;
							break;
						case "IsPyuria":
						
							if (value == null || value is System.Boolean)
								this.IsPyuria = (System.Boolean?)value;
							break;
						case "IsHematuria":
						
							if (value == null || value is System.Boolean)
								this.IsHematuria = (System.Boolean?)value;
							break;
						case "IsUrineCulture":
						
							if (value == null || value is System.Boolean)
								this.IsUrineCulture = (System.Boolean?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsUrineBagChange":
						
							if (value == null || value is System.Boolean)
								this.IsUrineBagChange = (System.Boolean?)value;
							break;
						case "IsRelease":
						
							if (value == null || value is System.Boolean)
								this.IsRelease = (System.Boolean?)value;
							break;
						case "IsApneu":
						
							if (value == null || value is System.Boolean)
								this.IsApneu = (System.Boolean?)value;
							break;
						case "IsIskDiagnose":
						
							if (value == null || value is System.Boolean)
								this.IsIskDiagnose = (System.Boolean?)value;
							break;
						case "IsUrineRutin":

							if (value == null || value is System.Boolean)
								this.IsUrineRutin = (System.Boolean?)value;
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
		/// Maps to NosocomialMonitoringCatheter.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.MonitoringNo
		/// </summary>
		virtual public System.Int32? MonitoringNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.SequenceNo
		/// </summary>
		virtual public System.Int32? SequenceNo
		{
			get
			{
				return base.GetSystemInt32(NosocomialMonitoringCatheterMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemInt32(NosocomialMonitoringCatheterMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.MonitoringDateTime
		/// </summary>
		virtual public System.DateTime? MonitoringDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.SRGeneralChateterNo
		/// </summary>
		virtual public System.String SRGeneralChateterNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.SRGeneralChateterNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.SRGeneralChateterNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.SRSiliconChateterNo
		/// </summary>
		virtual public System.String SRSiliconChateterNo
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.SRSiliconChateterNo);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.SRSiliconChateterNo, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsTempAbove38
		/// </summary>
		virtual public System.Boolean? IsTempAbove38
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsTempAbove38);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsTempAbove38, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsDisuria
		/// </summary>
		virtual public System.Boolean? IsDisuria
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsDisuria);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsDisuria, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsPain
		/// </summary>
		virtual public System.Boolean? IsPain
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsPain);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsPain, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsPyuria
		/// </summary>
		virtual public System.Boolean? IsPyuria
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsPyuria);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsPyuria, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsHematuria
		/// </summary>
		virtual public System.Boolean? IsHematuria
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsHematuria);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsHematuria, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsUrineCulture
		/// </summary>
		virtual public System.Boolean? IsUrineCulture
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineCulture);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineCulture, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsUrineBagChange
		/// </summary>
		virtual public System.Boolean? IsUrineBagChange
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineBagChange);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineBagChange, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.FixationFluid
		/// </summary>
		virtual public System.String FixationFluid
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.FixationFluid);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.FixationFluid, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.MonitoringByUserID
		/// </summary>
		virtual public System.String MonitoringByUserID
		{
			get
			{
				return base.GetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringByUserID);
			}
			
			set
			{
				base.SetSystemString(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringByUserID, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsRelease
		/// </summary>
		virtual public System.Boolean? IsRelease
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsRelease);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsRelease, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsApneu
		/// </summary>
		virtual public System.Boolean? IsApneu
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsApneu);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsApneu, value);
			}
		}
		/// <summary>
		/// Maps to NosocomialMonitoringCatheter.IsIskDiagnose
		/// </summary>
		virtual public System.Boolean? IsIskDiagnose
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsIskDiagnose);
			}
			
			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsIskDiagnose, value);
			}
		}
		virtual public System.Boolean? IsUrineRutin
		{
			get
			{
				return base.GetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineRutin);
			}

			set
			{
				base.SetSystemBoolean(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineRutin, value);
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
			public esStrings(esNosocomialMonitoringCatheter entity)
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
			public System.String SRGeneralChateterNo
			{
				get
				{
					System.String data = entity.SRGeneralChateterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGeneralChateterNo = null;
					else entity.SRGeneralChateterNo = Convert.ToString(value);
				}
			}
			public System.String SRSiliconChateterNo
			{
				get
				{
					System.String data = entity.SRSiliconChateterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSiliconChateterNo = null;
					else entity.SRSiliconChateterNo = Convert.ToString(value);
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
			public System.String IsDisuria
			{
				get
				{
					System.Boolean? data = entity.IsDisuria;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDisuria = null;
					else entity.IsDisuria = Convert.ToBoolean(value);
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
			public System.String IsPyuria
			{
				get
				{
					System.Boolean? data = entity.IsPyuria;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPyuria = null;
					else entity.IsPyuria = Convert.ToBoolean(value);
				}
			}
			public System.String IsHematuria
			{
				get
				{
					System.Boolean? data = entity.IsHematuria;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHematuria = null;
					else entity.IsHematuria = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrineCulture
			{
				get
				{
					System.Boolean? data = entity.IsUrineCulture;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrineCulture = null;
					else entity.IsUrineCulture = Convert.ToBoolean(value);
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
			public System.String IsUrineBagChange
			{
				get
				{
					System.Boolean? data = entity.IsUrineBagChange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrineBagChange = null;
					else entity.IsUrineBagChange = Convert.ToBoolean(value);
				}
			}
			public System.String FixationFluid
			{
				get
				{
					System.String data = entity.FixationFluid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FixationFluid = null;
					else entity.FixationFluid = Convert.ToString(value);
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
			public System.String IsIskDiagnose
			{
				get
				{
					System.Boolean? data = entity.IsIskDiagnose;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIskDiagnose = null;
					else entity.IsIskDiagnose = Convert.ToBoolean(value);
				}
			}

			public System.String IsUrineRutin
			{
				get
				{
					System.Boolean? data = entity.IsUrineRutin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrineRutin = null;
					else entity.IsUrineRutin = Convert.ToBoolean(value);
				}
			}
			private esNosocomialMonitoringCatheter entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNosocomialMonitoringCatheterQuery query)
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
				throw new Exception("esNosocomialMonitoringCatheter can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class NosocomialMonitoringCatheter : esNosocomialMonitoringCatheter
	{	
	}

	[Serializable]
	abstract public class esNosocomialMonitoringCatheterQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringCatheterMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
			}
		} 
			
		public esQueryItem MonitoringDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRGeneralChateterNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.SRGeneralChateterNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRSiliconChateterNo
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.SRSiliconChateterNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsTempAbove38
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsTempAbove38, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDisuria
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsDisuria, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPain
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsPain, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPyuria
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsPyuria, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsHematuria
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsHematuria, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsUrineCulture
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineCulture, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsUrineBagChange
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineBagChange, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem FixationFluid
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.FixationFluid, esSystemType.String);
			}
		} 
			
		public esQueryItem MonitoringByUserID
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsRelease
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsRelease, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsApneu
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsApneu, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsIskDiagnose
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsIskDiagnose, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrineRutin
		{
			get
			{
				return new esQueryItem(this, NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineRutin, esSystemType.Boolean);
			}
		}

	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NosocomialMonitoringCatheterCollection")]
	public partial class NosocomialMonitoringCatheterCollection : esNosocomialMonitoringCatheterCollection, IEnumerable< NosocomialMonitoringCatheter>
	{
		public NosocomialMonitoringCatheterCollection()
		{

		}	
		
		public static implicit operator List< NosocomialMonitoringCatheter>(NosocomialMonitoringCatheterCollection coll)
		{
			List< NosocomialMonitoringCatheter> list = new List< NosocomialMonitoringCatheter>();
			
			foreach (NosocomialMonitoringCatheter emp in coll)
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
				return  NosocomialMonitoringCatheterMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringCatheterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NosocomialMonitoringCatheter(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NosocomialMonitoringCatheter();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public NosocomialMonitoringCatheterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringCatheterQuery();
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
		public bool Load(NosocomialMonitoringCatheterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public NosocomialMonitoringCatheter AddNew()
		{
			NosocomialMonitoringCatheter entity = base.AddNewEntity() as NosocomialMonitoringCatheter;
			
			return entity;		
		}
		public NosocomialMonitoringCatheter FindByPrimaryKey(String registrationNo, Int32 monitoringNo, Int32 sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, monitoringNo, sequenceNo) as NosocomialMonitoringCatheter;
		}

		#region IEnumerable< NosocomialMonitoringCatheter> Members

		IEnumerator< NosocomialMonitoringCatheter> IEnumerable< NosocomialMonitoringCatheter>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NosocomialMonitoringCatheter;
			}
		}

		#endregion
		
		private NosocomialMonitoringCatheterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NosocomialMonitoringCatheter' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("NosocomialMonitoringCatheter ({RegistrationNo, MonitoringNo, SequenceNo})")]
	[Serializable]
	public partial class NosocomialMonitoringCatheter : esNosocomialMonitoringCatheter
	{
		public NosocomialMonitoringCatheter()
		{
		}	
	
		public NosocomialMonitoringCatheter(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NosocomialMonitoringCatheterMetadata.Meta();
			}
		}	
	
		override protected esNosocomialMonitoringCatheterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NosocomialMonitoringCatheterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public NosocomialMonitoringCatheterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NosocomialMonitoringCatheterQuery();
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
		public bool Load(NosocomialMonitoringCatheterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private NosocomialMonitoringCatheterQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class NosocomialMonitoringCatheterQuery : esNosocomialMonitoringCatheterQuery
	{
		public NosocomialMonitoringCatheterQuery()
		{

		}		
		
		public NosocomialMonitoringCatheterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "NosocomialMonitoringCatheterQuery";
        }
	}

	[Serializable]
	public partial class NosocomialMonitoringCatheterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NosocomialMonitoringCatheterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.MonitoringNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.SequenceNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.MonitoringDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.SRGeneralChateterNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.SRGeneralChateterNo;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.SRSiliconChateterNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.SRSiliconChateterNo;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsTempAbove38, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsTempAbove38;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsDisuria, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsDisuria;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsPain, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsPain;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsPyuria, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsPyuria;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsHematuria, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsHematuria;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineCulture, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsUrineCulture;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.Note, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsDeleted, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineBagChange, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsUrineBagChange;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.FixationFluid, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.FixationFluid;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.MonitoringByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.MonitoringByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsRelease, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsRelease;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsApneu, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsApneu;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsIskDiagnose, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsIskDiagnose;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(NosocomialMonitoringCatheterMetadata.ColumnNames.IsUrineRutin, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = NosocomialMonitoringCatheterMetadata.PropertyNames.IsUrineRutin;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion
	
		static public NosocomialMonitoringCatheterMetadata Meta()
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
			public const string SRGeneralChateterNo = "SRGeneralChateterNo";
			public const string SRSiliconChateterNo = "SRSiliconChateterNo";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsDisuria = "IsDisuria";
			public const string IsPain = "IsPain";
			public const string IsPyuria = "IsPyuria";
			public const string IsHematuria = "IsHematuria";
			public const string IsUrineCulture = "IsUrineCulture";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUrineBagChange = "IsUrineBagChange";
			public const string FixationFluid = "FixationFluid";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsRelease = "IsRelease";
			public const string IsApneu = "IsApneu";
			public const string IsIskDiagnose = "IsIskDiagnose";
			public const string IsUrineRutin = "IsUrineRutin";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string MonitoringNo = "MonitoringNo";
			public const string SequenceNo = "SequenceNo";
			public const string MonitoringDateTime = "MonitoringDateTime";
			public const string SRGeneralChateterNo = "SRGeneralChateterNo";
			public const string SRSiliconChateterNo = "SRSiliconChateterNo";
			public const string IsTempAbove38 = "IsTempAbove38";
			public const string IsDisuria = "IsDisuria";
			public const string IsPain = "IsPain";
			public const string IsPyuria = "IsPyuria";
			public const string IsHematuria = "IsHematuria";
			public const string IsUrineCulture = "IsUrineCulture";
			public const string Note = "Note";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUrineBagChange = "IsUrineBagChange";
			public const string FixationFluid = "FixationFluid";
			public const string MonitoringByUserID = "MonitoringByUserID";
			public const string IsRelease = "IsRelease";
			public const string IsApneu = "IsApneu";
			public const string IsIskDiagnose = "IsIskDiagnose";
			public const string IsUrineRutin = "IsUrineRutin";
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
			lock (typeof(NosocomialMonitoringCatheterMetadata))
			{
				if(NosocomialMonitoringCatheterMetadata.mapDelegates == null)
				{
					NosocomialMonitoringCatheterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NosocomialMonitoringCatheterMetadata.meta == null)
				{
					NosocomialMonitoringCatheterMetadata.meta = new NosocomialMonitoringCatheterMetadata();
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
				meta.AddTypeMap("SRGeneralChateterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSiliconChateterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTempAbove38", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDisuria", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPain", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPyuria", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHematuria", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrineCulture", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUrineBagChange", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("FixationFluid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MonitoringByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRelease", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApneu", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIskDiagnose", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrineRutin", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "NosocomialMonitoringCatheter";
				meta.Destination = "NosocomialMonitoringCatheter";
				meta.spInsert = "proc_NosocomialMonitoringCatheterInsert";				
				meta.spUpdate = "proc_NosocomialMonitoringCatheterUpdate";		
				meta.spDelete = "proc_NosocomialMonitoringCatheterDelete";
				meta.spLoadAll = "proc_NosocomialMonitoringCatheterLoadAll";
				meta.spLoadByPrimaryKey = "proc_NosocomialMonitoringCatheterLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NosocomialMonitoringCatheterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
