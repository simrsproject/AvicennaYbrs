/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/14/2023 11:55:22 PM
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
	abstract public class esRegistrationInfoMedicCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationInfoMedicCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationInfoMedicCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicQuery query)
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
			this.InitQuery(query as esRegistrationInfoMedicQuery);
		}
		#endregion
			
		virtual public RegistrationInfoMedic DetachEntity(RegistrationInfoMedic entity)
		{
			return base.DetachEntity(entity) as RegistrationInfoMedic;
		}
		
		virtual public RegistrationInfoMedic AttachEntity(RegistrationInfoMedic entity)
		{
			return base.AttachEntity(entity) as RegistrationInfoMedic;
		}
		
		virtual public void Combine(RegistrationInfoMedicCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationInfoMedic this[int index]
		{
			get
			{
				return base[index] as RegistrationInfoMedic;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationInfoMedic);
		}
	}

	[Serializable]
	abstract public class esRegistrationInfoMedic : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationInfoMedicQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationInfoMedic()
		{
		}
	
		public esRegistrationInfoMedic(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationInfoMedicID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationInfoMedicID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationInfoMedicID)
		{
			esRegistrationInfoMedicQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationInfoMedicID == registrationInfoMedicID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationInfoMedicID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationInfoMedicID",registrationInfoMedicID);
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
						case "RegistrationInfoMedicID": this.str.RegistrationInfoMedicID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SRMedicalNotesInputType": this.str.SRMedicalNotesInputType = (string)value; break;
						case "DateTimeInfo": this.str.DateTimeInfo = (string)value; break;
						case "Info1": this.str.Info1 = (string)value; break;
						case "Info2": this.str.Info2 = (string)value; break;
						case "Info3": this.str.Info3 = (string)value; break;
						case "Info4": this.str.Info4 = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "Info1Log": this.str.Info1Log = (string)value; break;
						case "Info2Log": this.str.Info2Log = (string)value; break;
						case "Info3Log": this.str.Info3Log = (string)value; break;
						case "Info4Log": this.str.Info4Log = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "AttendingNotes": this.str.AttendingNotes = (string)value; break;
						case "IsInformConcern": this.str.IsInformConcern = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ApprovedDatetime": this.str.ApprovedDatetime = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsPRMRJ": this.str.IsPRMRJ = (string)value; break;
						case "PpaInstruction": this.str.PpaInstruction = (string)value; break;
						case "IsCreatedByUserDpjp": this.str.IsCreatedByUserDpjp = (string)value; break;
						case "SRUserType": this.str.SRUserType = (string)value; break;
						case "PrescriptionCurrentDay": this.str.PrescriptionCurrentDay = (string)value; break;
						case "ReferenceType": this.str.ReferenceType = (string)value; break;
						case "Info1Entry": this.str.Info1Entry = (string)value; break;
						case "Info3Entry": this.str.Info3Entry = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "Info5": this.str.Info5 = (string)value; break;
						case "DpjpNotes": this.str.DpjpNotes = (string)value; break;
						case "ReceiveBy": this.str.ReceiveBy = (string)value; break;
						case "Info2Entry": this.str.Info2Entry = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DateTimeInfo":
						
							if (value == null || value is System.DateTime)
								this.DateTimeInfo = (System.DateTime?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "IsInformConcern":
						
							if (value == null || value is System.Boolean)
								this.IsInformConcern = (System.Boolean?)value;
							break;
						case "ApprovedDatetime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDatetime = (System.DateTime?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "IsPRMRJ":
						
							if (value == null || value is System.Boolean)
								this.IsPRMRJ = (System.Boolean?)value;
							break;
						case "IsCreatedByUserDpjp":
						
							if (value == null || value is System.Boolean)
								this.IsCreatedByUserDpjp = (System.Boolean?)value;
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
		/// Maps to RegistrationInfoMedic.RegistrationInfoMedicID
		/// </summary>
		virtual public System.String RegistrationInfoMedicID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.RegistrationInfoMedicID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.RegistrationInfoMedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.SRMedicalNotesInputType
		/// </summary>
		virtual public System.String SRMedicalNotesInputType
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.SRMedicalNotesInputType);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.SRMedicalNotesInputType, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.DateTimeInfo
		/// </summary>
		virtual public System.DateTime? DateTimeInfo
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.DateTimeInfo);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.DateTimeInfo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info1
		/// </summary>
		virtual public System.String Info1
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info1);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info1, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info2
		/// </summary>
		virtual public System.String Info2
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info2);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info2, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info3
		/// </summary>
		virtual public System.String Info3
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info3);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info3, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info4
		/// </summary>
		virtual public System.String Info4
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info4);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info4, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info1Log
		/// </summary>
		virtual public System.String Info1Log
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info1Log);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info1Log, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info2Log
		/// </summary>
		virtual public System.String Info2Log
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info2Log);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info2Log, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info3Log
		/// </summary>
		virtual public System.String Info3Log
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info3Log);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info3Log, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info4Log
		/// </summary>
		virtual public System.String Info4Log
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info4Log);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info4Log, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.AttendingNotes
		/// </summary>
		virtual public System.String AttendingNotes
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.AttendingNotes);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.AttendingNotes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.IsInformConcern
		/// </summary>
		virtual public System.Boolean? IsInformConcern
		{
			get
			{
				return base.GetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsInformConcern);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsInformConcern, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.ApprovedDatetime
		/// </summary>
		virtual public System.DateTime? ApprovedDatetime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.ApprovedDatetime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicMetadata.ColumnNames.ApprovedDatetime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.IsPRMRJ
		/// </summary>
		virtual public System.Boolean? IsPRMRJ
		{
			get
			{
				return base.GetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsPRMRJ);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsPRMRJ, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.PpaInstruction
		/// </summary>
		virtual public System.String PpaInstruction
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.PpaInstruction);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.PpaInstruction, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.IsCreatedByUserDpjp
		/// </summary>
		virtual public System.Boolean? IsCreatedByUserDpjp
		{
			get
			{
				return base.GetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsCreatedByUserDpjp);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationInfoMedicMetadata.ColumnNames.IsCreatedByUserDpjp, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.SRUserType
		/// </summary>
		virtual public System.String SRUserType
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.SRUserType);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.SRUserType, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.PrescriptionCurrentDay
		/// </summary>
		virtual public System.String PrescriptionCurrentDay
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.PrescriptionCurrentDay);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.PrescriptionCurrentDay, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.ReferenceType
		/// </summary>
		virtual public System.String ReferenceType
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ReferenceType);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ReferenceType, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info1Entry
		/// </summary>
		virtual public System.String Info1Entry
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info1Entry);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info1Entry, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info3Entry
		/// </summary>
		virtual public System.String Info3Entry
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info3Entry);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info3Entry, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info5
		/// </summary>
		virtual public System.String Info5
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info5);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info5, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.DpjpNotes
		/// </summary>
		virtual public System.String DpjpNotes
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.DpjpNotes);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.DpjpNotes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.ReceiveBy
		/// </summary>
		virtual public System.String ReceiveBy
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ReceiveBy);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.ReceiveBy, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedic.Info2Entry
		/// </summary>
		virtual public System.String Info2Entry
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info2Entry);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicMetadata.ColumnNames.Info2Entry, value);
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
			public esStrings(esRegistrationInfoMedic entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationInfoMedicID
			{
				get
				{
					System.String data = entity.RegistrationInfoMedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationInfoMedicID = null;
					else entity.RegistrationInfoMedicID = Convert.ToString(value);
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
			public System.String SRMedicalNotesInputType
			{
				get
				{
					System.String data = entity.SRMedicalNotesInputType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalNotesInputType = null;
					else entity.SRMedicalNotesInputType = Convert.ToString(value);
				}
			}
			public System.String DateTimeInfo
			{
				get
				{
					System.DateTime? data = entity.DateTimeInfo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateTimeInfo = null;
					else entity.DateTimeInfo = Convert.ToDateTime(value);
				}
			}
			public System.String Info1
			{
				get
				{
					System.String data = entity.Info1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info1 = null;
					else entity.Info1 = Convert.ToString(value);
				}
			}
			public System.String Info2
			{
				get
				{
					System.String data = entity.Info2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info2 = null;
					else entity.Info2 = Convert.ToString(value);
				}
			}
			public System.String Info3
			{
				get
				{
					System.String data = entity.Info3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info3 = null;
					else entity.Info3 = Convert.ToString(value);
				}
			}
			public System.String Info4
			{
				get
				{
					System.String data = entity.Info4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info4 = null;
					else entity.Info4 = Convert.ToString(value);
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
			public System.String Info1Log
			{
				get
				{
					System.String data = entity.Info1Log;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info1Log = null;
					else entity.Info1Log = Convert.ToString(value);
				}
			}
			public System.String Info2Log
			{
				get
				{
					System.String data = entity.Info2Log;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info2Log = null;
					else entity.Info2Log = Convert.ToString(value);
				}
			}
			public System.String Info3Log
			{
				get
				{
					System.String data = entity.Info3Log;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info3Log = null;
					else entity.Info3Log = Convert.ToString(value);
				}
			}
			public System.String Info4Log
			{
				get
				{
					System.String data = entity.Info4Log;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info4Log = null;
					else entity.Info4Log = Convert.ToString(value);
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
			public System.String AttendingNotes
			{
				get
				{
					System.String data = entity.AttendingNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AttendingNotes = null;
					else entity.AttendingNotes = Convert.ToString(value);
				}
			}
			public System.String IsInformConcern
			{
				get
				{
					System.Boolean? data = entity.IsInformConcern;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInformConcern = null;
					else entity.IsInformConcern = Convert.ToBoolean(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String ApprovedDatetime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDatetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDatetime = null;
					else entity.ApprovedDatetime = Convert.ToDateTime(value);
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
			public System.String IsPRMRJ
			{
				get
				{
					System.Boolean? data = entity.IsPRMRJ;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPRMRJ = null;
					else entity.IsPRMRJ = Convert.ToBoolean(value);
				}
			}
			public System.String PpaInstruction
			{
				get
				{
					System.String data = entity.PpaInstruction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PpaInstruction = null;
					else entity.PpaInstruction = Convert.ToString(value);
				}
			}
			public System.String IsCreatedByUserDpjp
			{
				get
				{
					System.Boolean? data = entity.IsCreatedByUserDpjp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCreatedByUserDpjp = null;
					else entity.IsCreatedByUserDpjp = Convert.ToBoolean(value);
				}
			}
			public System.String SRUserType
			{
				get
				{
					System.String data = entity.SRUserType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRUserType = null;
					else entity.SRUserType = Convert.ToString(value);
				}
			}
			public System.String PrescriptionCurrentDay
			{
				get
				{
					System.String data = entity.PrescriptionCurrentDay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionCurrentDay = null;
					else entity.PrescriptionCurrentDay = Convert.ToString(value);
				}
			}
			public System.String ReferenceType
			{
				get
				{
					System.String data = entity.ReferenceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceType = null;
					else entity.ReferenceType = Convert.ToString(value);
				}
			}
			public System.String Info1Entry
			{
				get
				{
					System.String data = entity.Info1Entry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info1Entry = null;
					else entity.Info1Entry = Convert.ToString(value);
				}
			}
			public System.String Info3Entry
			{
				get
				{
					System.String data = entity.Info3Entry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info3Entry = null;
					else entity.Info3Entry = Convert.ToString(value);
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
			public System.String Info5
			{
				get
				{
					System.String data = entity.Info5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info5 = null;
					else entity.Info5 = Convert.ToString(value);
				}
			}
			public System.String DpjpNotes
			{
				get
				{
					System.String data = entity.DpjpNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DpjpNotes = null;
					else entity.DpjpNotes = Convert.ToString(value);
				}
			}
			public System.String ReceiveBy
			{
				get
				{
					System.String data = entity.ReceiveBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceiveBy = null;
					else entity.ReceiveBy = Convert.ToString(value);
				}
			}
			public System.String Info2Entry
			{
				get
				{
					System.String data = entity.Info2Entry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Info2Entry = null;
					else entity.Info2Entry = Convert.ToString(value);
				}
			}
			private esRegistrationInfoMedic entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicQuery query)
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
				throw new Exception("esRegistrationInfoMedic can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationInfoMedic : esRegistrationInfoMedic
	{	
	}

	[Serializable]
	abstract public class esRegistrationInfoMedicQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationInfoMedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRMedicalNotesInputType
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.SRMedicalNotesInputType, esSystemType.String);
			}
		} 
			
		public esQueryItem DateTimeInfo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.DateTimeInfo, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Info1
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info1, esSystemType.String);
			}
		} 
			
		public esQueryItem Info2
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info2, esSystemType.String);
			}
		} 
			
		public esQueryItem Info3
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info3, esSystemType.String);
			}
		} 
			
		public esQueryItem Info4
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info4, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Info1Log
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info1Log, esSystemType.String);
			}
		} 
			
		public esQueryItem Info2Log
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info2Log, esSystemType.String);
			}
		} 
			
		public esQueryItem Info3Log
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info3Log, esSystemType.String);
			}
		} 
			
		public esQueryItem Info4Log
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info4Log, esSystemType.String);
			}
		} 
			
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem AttendingNotes
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.AttendingNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem IsInformConcern
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.IsInformConcern, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ApprovedDatetime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.ApprovedDatetime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPRMRJ
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.IsPRMRJ, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PpaInstruction
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.PpaInstruction, esSystemType.String);
			}
		} 
			
		public esQueryItem IsCreatedByUserDpjp
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.IsCreatedByUserDpjp, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRUserType
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.SRUserType, esSystemType.String);
			}
		} 
			
		public esQueryItem PrescriptionCurrentDay
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.PrescriptionCurrentDay, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceType
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.ReferenceType, esSystemType.String);
			}
		} 
			
		public esQueryItem Info1Entry
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info1Entry, esSystemType.String);
			}
		} 
			
		public esQueryItem Info3Entry
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info3Entry, esSystemType.String);
			}
		} 
			
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Info5
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info5, esSystemType.String);
			}
		} 
			
		public esQueryItem DpjpNotes
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.DpjpNotes, esSystemType.String);
			}
		} 
			
		public esQueryItem ReceiveBy
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.ReceiveBy, esSystemType.String);
			}
		} 
			
		public esQueryItem Info2Entry
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicMetadata.ColumnNames.Info2Entry, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationInfoMedicCollection")]
	public partial class RegistrationInfoMedicCollection : esRegistrationInfoMedicCollection, IEnumerable< RegistrationInfoMedic>
	{
		public RegistrationInfoMedicCollection()
		{

		}	
		
		public static implicit operator List< RegistrationInfoMedic>(RegistrationInfoMedicCollection coll)
		{
			List< RegistrationInfoMedic> list = new List< RegistrationInfoMedic>();
			
			foreach (RegistrationInfoMedic emp in coll)
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
				return  RegistrationInfoMedicMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationInfoMedic(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationInfoMedic();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationInfoMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicQuery();
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
		public bool Load(RegistrationInfoMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationInfoMedic AddNew()
		{
			RegistrationInfoMedic entity = base.AddNewEntity() as RegistrationInfoMedic;
			
			return entity;		
		}
		public RegistrationInfoMedic FindByPrimaryKey(String registrationInfoMedicID)
		{
			return base.FindByPrimaryKey(registrationInfoMedicID) as RegistrationInfoMedic;
		}

		#region IEnumerable< RegistrationInfoMedic> Members

		IEnumerator< RegistrationInfoMedic> IEnumerable< RegistrationInfoMedic>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationInfoMedic;
			}
		}

		#endregion
		
		private RegistrationInfoMedicQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationInfoMedic' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationInfoMedic ({RegistrationInfoMedicID})")]
	[Serializable]
	public partial class RegistrationInfoMedic : esRegistrationInfoMedic
	{
		public RegistrationInfoMedic()
		{
		}	
	
		public RegistrationInfoMedic(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicMetadata.Meta();
			}
		}	
	
		override protected esRegistrationInfoMedicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationInfoMedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicQuery();
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
		public bool Load(RegistrationInfoMedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationInfoMedicQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationInfoMedicQuery : esRegistrationInfoMedicQuery
	{
		public RegistrationInfoMedicQuery()
		{

		}		
		
		public RegistrationInfoMedicQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationInfoMedicQuery";
        }
	}

	[Serializable]
	public partial class RegistrationInfoMedicMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationInfoMedicMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.RegistrationInfoMedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.RegistrationInfoMedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.SRMedicalNotesInputType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.SRMedicalNotesInputType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.DateTimeInfo, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.DateTimeInfo;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info1, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info1;
			c.CharacterMaxLength = 4000;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info2, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info2;
			c.CharacterMaxLength = 4000;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info3, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info3;
			c.CharacterMaxLength = 4000;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info4, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info4;
			c.CharacterMaxLength = 4000;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info1Log, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info1Log;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info2Log, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info2Log;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info3Log, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info3Log;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info4Log, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info4Log;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.IsDeleted, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.ServiceUnitID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.AttendingNotes, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.AttendingNotes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.IsInformConcern, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.IsInformConcern;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.ParamedicID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.ApprovedDatetime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.ApprovedDatetime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.IsApproved, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.ApprovedByUserID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.IsPRMRJ, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.IsPRMRJ;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.PpaInstruction, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.PpaInstruction;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.IsCreatedByUserDpjp, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.IsCreatedByUserDpjp;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.SRUserType, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.SRUserType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.PrescriptionCurrentDay, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.PrescriptionCurrentDay;
			c.CharacterMaxLength = 8000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.ReferenceType, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.ReferenceType;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info1Entry, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info1Entry;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info3Entry, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info3Entry;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.ReferenceNo, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info5, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info5;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.DpjpNotes, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.DpjpNotes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.ReceiveBy, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.ReceiveBy;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicMetadata.ColumnNames.Info2Entry, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicMetadata.PropertyNames.Info2Entry;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RegistrationInfoMedicMetadata Meta()
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
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string RegistrationNo = "RegistrationNo";
			public const string SRMedicalNotesInputType = "SRMedicalNotesInputType";
			public const string DateTimeInfo = "DateTimeInfo";
			public const string Info1 = "Info1";
			public const string Info2 = "Info2";
			public const string Info3 = "Info3";
			public const string Info4 = "Info4";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string Info1Log = "Info1Log";
			public const string Info2Log = "Info2Log";
			public const string Info3Log = "Info3Log";
			public const string Info4Log = "Info4Log";
			public const string IsDeleted = "IsDeleted";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string AttendingNotes = "AttendingNotes";
			public const string IsInformConcern = "IsInformConcern";
			public const string ParamedicID = "ParamedicID";
			public const string ApprovedDatetime = "ApprovedDatetime";
			public const string IsApproved = "IsApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsPRMRJ = "IsPRMRJ";
			public const string PpaInstruction = "PpaInstruction";
			public const string IsCreatedByUserDpjp = "IsCreatedByUserDpjp";
			public const string SRUserType = "SRUserType";
			public const string PrescriptionCurrentDay = "PrescriptionCurrentDay";
			public const string ReferenceType = "ReferenceType";
			public const string Info1Entry = "Info1Entry";
			public const string Info3Entry = "Info3Entry";
			public const string ReferenceNo = "ReferenceNo";
			public const string Info5 = "Info5";
			public const string DpjpNotes = "DpjpNotes";
			public const string ReceiveBy = "ReceiveBy";
			public const string Info2Entry = "Info2Entry";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string RegistrationNo = "RegistrationNo";
			public const string SRMedicalNotesInputType = "SRMedicalNotesInputType";
			public const string DateTimeInfo = "DateTimeInfo";
			public const string Info1 = "Info1";
			public const string Info2 = "Info2";
			public const string Info3 = "Info3";
			public const string Info4 = "Info4";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string Info1Log = "Info1Log";
			public const string Info2Log = "Info2Log";
			public const string Info3Log = "Info3Log";
			public const string Info4Log = "Info4Log";
			public const string IsDeleted = "IsDeleted";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string AttendingNotes = "AttendingNotes";
			public const string IsInformConcern = "IsInformConcern";
			public const string ParamedicID = "ParamedicID";
			public const string ApprovedDatetime = "ApprovedDatetime";
			public const string IsApproved = "IsApproved";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsPRMRJ = "IsPRMRJ";
			public const string PpaInstruction = "PpaInstruction";
			public const string IsCreatedByUserDpjp = "IsCreatedByUserDpjp";
			public const string SRUserType = "SRUserType";
			public const string PrescriptionCurrentDay = "PrescriptionCurrentDay";
			public const string ReferenceType = "ReferenceType";
			public const string Info1Entry = "Info1Entry";
			public const string Info3Entry = "Info3Entry";
			public const string ReferenceNo = "ReferenceNo";
			public const string Info5 = "Info5";
			public const string DpjpNotes = "DpjpNotes";
			public const string ReceiveBy = "ReceiveBy";
			public const string Info2Entry = "Info2Entry";
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
			lock (typeof(RegistrationInfoMedicMetadata))
			{
				if(RegistrationInfoMedicMetadata.mapDelegates == null)
				{
					RegistrationInfoMedicMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationInfoMedicMetadata.meta == null)
				{
					RegistrationInfoMedicMetadata.meta = new RegistrationInfoMedicMetadata();
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
				
				meta.AddTypeMap("RegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalNotesInputType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateTimeInfo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Info1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Info1Log", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info2Log", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info3Log", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info4Log", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AttendingNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInformConcern", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApprovedDatetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPRMRJ", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PpaInstruction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCreatedByUserDpjp", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRUserType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionCurrentDay", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info1Entry", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info3Entry", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info5", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DpjpNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceiveBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Info2Entry", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "RegistrationInfoMedic";
				meta.Destination = "RegistrationInfoMedic";
				meta.spInsert = "proc_RegistrationInfoMedicInsert";				
				meta.spUpdate = "proc_RegistrationInfoMedicUpdate";		
				meta.spDelete = "proc_RegistrationInfoMedicDelete";
				meta.spLoadAll = "proc_RegistrationInfoMedicLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationInfoMedicLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationInfoMedicMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
