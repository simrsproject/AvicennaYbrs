/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/08/19 3:18:07 PM
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
	abstract public class esParamedicConsultReferCollection : esEntityCollectionWAuditLog
	{
		public esParamedicConsultReferCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicConsultReferCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicConsultReferQuery query)
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
			this.InitQuery(query as esParamedicConsultReferQuery);
		}
		#endregion
			
		virtual public ParamedicConsultRefer DetachEntity(ParamedicConsultRefer entity)
		{
			return base.DetachEntity(entity) as ParamedicConsultRefer;
		}
		
		virtual public ParamedicConsultRefer AttachEntity(ParamedicConsultRefer entity)
		{
			return base.AttachEntity(entity) as ParamedicConsultRefer;
		}
		
		virtual public void Combine(ParamedicConsultReferCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicConsultRefer this[int index]
		{
			get
			{
				return base[index] as ParamedicConsultRefer;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicConsultRefer);
		}
	}

	[Serializable]
	abstract public class esParamedicConsultRefer : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicConsultReferQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicConsultRefer()
		{
		}
	
		public esParamedicConsultRefer(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String consultReferNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(consultReferNo);
			else
				return LoadByPrimaryKeyStoredProcedure(consultReferNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String consultReferNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(consultReferNo);
			else
				return LoadByPrimaryKeyStoredProcedure(consultReferNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String consultReferNo)
		{
			esParamedicConsultReferQuery query = this.GetDynamicQuery();
			query.Where(query.ConsultReferNo == consultReferNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String consultReferNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ConsultReferNo",consultReferNo);
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
						case "ConsultReferNo": this.str.ConsultReferNo = (string)value; break;
						case "ConsultDateTime": this.str.ConsultDateTime = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "SRParamedicConsultType": this.str.SRParamedicConsultType = (string)value; break;
						case "ConsultReferType": this.str.ConsultReferType = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "ToParamedicID": this.str.ToParamedicID = (string)value; break;
						case "ToRoomID": this.str.ToRoomID = (string)value; break;
						case "ToRegistrationQue": this.str.ToRegistrationQue = (string)value; break;
						case "ChiefComplaint": this.str.ChiefComplaint = (string)value; break;
						case "PastMedicalHistory": this.str.PastMedicalHistory = (string)value; break;
						case "Hpi": this.str.Hpi = (string)value; break;
						case "ActionExamTreatment": this.str.ActionExamTreatment = (string)value; break;
						case "ActiveMotion": this.str.ActiveMotion = (string)value; break;
						case "PassiveMotion": this.str.PassiveMotion = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "AnswerDateTime": this.str.AnswerDateTime = (string)value; break;
						case "Answer": this.str.Answer = (string)value; break;
						case "AnswerDiagnose": this.str.AnswerDiagnose = (string)value; break;
						case "AnswerPlan": this.str.AnswerPlan = (string)value; break;
						case "AnswerAction": this.str.AnswerAction = (string)value; break;
						case "ToRegistrationNo": this.str.ToRegistrationNo = (string)value; break;
						case "ToAppointmentNo": this.str.ToAppointmentNo = (string)value; break;
						case "IsPhysiotherapy": this.str.IsPhysiotherapy = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "SRConsultAnswerType": this.str.SRConsultAnswerType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ConsultDateTime":
						
							if (value == null || value is System.DateTime)
								this.ConsultDateTime = (System.DateTime?)value;
							break;
						case "ToRegistrationQue":
						
							if (value == null || value is System.Int32)
								this.ToRegistrationQue = (System.Int32?)value;
							break;
						case "AnswerDateTime":
						
							if (value == null || value is System.DateTime)
								this.AnswerDateTime = (System.DateTime?)value;
							break;
						case "IsPhysiotherapy":
						
							if (value == null || value is System.Boolean)
								this.IsPhysiotherapy = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "PhysicianSign":

							if (value == null || value is System.Byte[])
								this.PhysicianSign = (System.Byte[])value;
							break;
						case "PhysicianAnswerSign":

							if (value == null || value is System.Byte[])
								this.PhysicianAnswerSign = (System.Byte[])value;
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
		/// Maps to ParamedicConsultRefer.ConsultReferNo
		/// </summary>
		virtual public System.String ConsultReferNo
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ConsultReferNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ConsultReferNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ConsultDateTime
		/// </summary>
		virtual public System.DateTime? ConsultDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicConsultReferMetadata.ColumnNames.ConsultDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicConsultReferMetadata.ColumnNames.ConsultDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.SRParamedicConsultType
		/// </summary>
		virtual public System.String SRParamedicConsultType
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.SRParamedicConsultType);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.SRParamedicConsultType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ConsultReferType
		/// </summary>
		virtual public System.String ConsultReferType
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ConsultReferType);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ConsultReferType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ToParamedicID
		/// </summary>
		virtual public System.String ToParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ToRoomID
		/// </summary>
		virtual public System.String ToRoomID
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToRoomID);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToRoomID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ToRegistrationQue
		/// </summary>
		virtual public System.Int32? ToRegistrationQue
		{
			get
			{
				return base.GetSystemInt32(ParamedicConsultReferMetadata.ColumnNames.ToRegistrationQue);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicConsultReferMetadata.ColumnNames.ToRegistrationQue, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ChiefComplaint
		/// </summary>
		virtual public System.String ChiefComplaint
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ChiefComplaint);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ChiefComplaint, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.PastMedicalHistory
		/// </summary>
		virtual public System.String PastMedicalHistory
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.PastMedicalHistory);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.PastMedicalHistory, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.Hpi
		/// </summary>
		virtual public System.String Hpi
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.Hpi);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.Hpi, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ActionExamTreatment
		/// </summary>
		virtual public System.String ActionExamTreatment
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ActionExamTreatment);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ActionExamTreatment, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ActiveMotion
		/// </summary>
		virtual public System.String ActiveMotion
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ActiveMotion);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ActiveMotion, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.PassiveMotion
		/// </summary>
		virtual public System.String PassiveMotion
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.PassiveMotion);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.PassiveMotion, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.AnswerDateTime
		/// </summary>
		virtual public System.DateTime? AnswerDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicConsultReferMetadata.ColumnNames.AnswerDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicConsultReferMetadata.ColumnNames.AnswerDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.Answer
		/// </summary>
		virtual public System.String Answer
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.Answer);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.Answer, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.AnswerDiagnose
		/// </summary>
		virtual public System.String AnswerDiagnose
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.AnswerDiagnose);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.AnswerDiagnose, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.AnswerPlan
		/// </summary>
		virtual public System.String AnswerPlan
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.AnswerPlan);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.AnswerPlan, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.AnswerAction
		/// </summary>
		virtual public System.String AnswerAction
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.AnswerAction);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.AnswerAction, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ToRegistrationNo
		/// </summary>
		virtual public System.String ToRegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToRegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToRegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.ToAppointmentNo
		/// </summary>
		virtual public System.String ToAppointmentNo
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToAppointmentNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.ToAppointmentNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.IsPhysiotherapy
		/// </summary>
		virtual public System.Boolean? IsPhysiotherapy
		{
			get
			{
				return base.GetSystemBoolean(ParamedicConsultReferMetadata.ColumnNames.IsPhysiotherapy);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicConsultReferMetadata.ColumnNames.IsPhysiotherapy, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicConsultReferMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicConsultReferMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicConsultRefer.SRConsultAnswerType
		/// </summary>
		virtual public System.String SRConsultAnswerType
		{
			get
			{
				return base.GetSystemString(ParamedicConsultReferMetadata.ColumnNames.SRConsultAnswerType);
			}
			
			set
			{
				base.SetSystemString(ParamedicConsultReferMetadata.ColumnNames.SRConsultAnswerType, value);
			}
		}

		/// <summary>
		/// Maps to ParamedicConsultRefer.PhysicianSign
		/// </summary>
		virtual public System.Byte[] PhysicianSign
		{
			get
			{
				return base.GetSystemByteArray(ParamedicConsultReferMetadata.ColumnNames.PhysicianSign);
			}

			set
			{
				base.SetSystemByteArray(ParamedicConsultReferMetadata.ColumnNames.PhysicianSign, value);
			}
		}

		/// <summary>
		/// Maps to ParamedicConsultRefer.PhysicianAnswerSign
		/// </summary>
		virtual public System.Byte[] PhysicianAnswerSign
		{
			get
			{
				return base.GetSystemByteArray(ParamedicConsultReferMetadata.ColumnNames.PhysicianAnswerSign);
			}

			set
			{
				base.SetSystemByteArray(ParamedicConsultReferMetadata.ColumnNames.PhysicianAnswerSign, value);
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
			public esStrings(esParamedicConsultRefer entity)
			{
				this.entity = entity;
			}
			public System.String ConsultReferNo
			{
				get
				{
					System.String data = entity.ConsultReferNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsultReferNo = null;
					else entity.ConsultReferNo = Convert.ToString(value);
				}
			}
			public System.String ConsultDateTime
			{
				get
				{
					System.DateTime? data = entity.ConsultDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsultDateTime = null;
					else entity.ConsultDateTime = Convert.ToDateTime(value);
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
			public System.String SRParamedicConsultType
			{
				get
				{
					System.String data = entity.SRParamedicConsultType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicConsultType = null;
					else entity.SRParamedicConsultType = Convert.ToString(value);
				}
			}
			public System.String ConsultReferType
			{
				get
				{
					System.String data = entity.ConsultReferType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsultReferType = null;
					else entity.ConsultReferType = Convert.ToString(value);
				}
			}
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String ToParamedicID
			{
				get
				{
					System.String data = entity.ToParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToParamedicID = null;
					else entity.ToParamedicID = Convert.ToString(value);
				}
			}
			public System.String ToRoomID
			{
				get
				{
					System.String data = entity.ToRoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToRoomID = null;
					else entity.ToRoomID = Convert.ToString(value);
				}
			}
			public System.String ToRegistrationQue
			{
				get
				{
					System.Int32? data = entity.ToRegistrationQue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToRegistrationQue = null;
					else entity.ToRegistrationQue = Convert.ToInt32(value);
				}
			}
			public System.String ChiefComplaint
			{
				get
				{
					System.String data = entity.ChiefComplaint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChiefComplaint = null;
					else entity.ChiefComplaint = Convert.ToString(value);
				}
			}
			public System.String PastMedicalHistory
			{
				get
				{
					System.String data = entity.PastMedicalHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PastMedicalHistory = null;
					else entity.PastMedicalHistory = Convert.ToString(value);
				}
			}
			public System.String Hpi
			{
				get
				{
					System.String data = entity.Hpi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hpi = null;
					else entity.Hpi = Convert.ToString(value);
				}
			}
			public System.String ActionExamTreatment
			{
				get
				{
					System.String data = entity.ActionExamTreatment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionExamTreatment = null;
					else entity.ActionExamTreatment = Convert.ToString(value);
				}
			}
			public System.String ActiveMotion
			{
				get
				{
					System.String data = entity.ActiveMotion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActiveMotion = null;
					else entity.ActiveMotion = Convert.ToString(value);
				}
			}
			public System.String PassiveMotion
			{
				get
				{
					System.String data = entity.PassiveMotion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PassiveMotion = null;
					else entity.PassiveMotion = Convert.ToString(value);
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
			public System.String AnswerDateTime
			{
				get
				{
					System.DateTime? data = entity.AnswerDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerDateTime = null;
					else entity.AnswerDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String Answer
			{
				get
				{
					System.String data = entity.Answer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Answer = null;
					else entity.Answer = Convert.ToString(value);
				}
			}
			public System.String AnswerDiagnose
			{
				get
				{
					System.String data = entity.AnswerDiagnose;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerDiagnose = null;
					else entity.AnswerDiagnose = Convert.ToString(value);
				}
			}
			public System.String AnswerPlan
			{
				get
				{
					System.String data = entity.AnswerPlan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerPlan = null;
					else entity.AnswerPlan = Convert.ToString(value);
				}
			}
			public System.String AnswerAction
			{
				get
				{
					System.String data = entity.AnswerAction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerAction = null;
					else entity.AnswerAction = Convert.ToString(value);
				}
			}
			public System.String ToRegistrationNo
			{
				get
				{
					System.String data = entity.ToRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToRegistrationNo = null;
					else entity.ToRegistrationNo = Convert.ToString(value);
				}
			}
			public System.String ToAppointmentNo
			{
				get
				{
					System.String data = entity.ToAppointmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToAppointmentNo = null;
					else entity.ToAppointmentNo = Convert.ToString(value);
				}
			}
			public System.String IsPhysiotherapy
			{
				get
				{
					System.Boolean? data = entity.IsPhysiotherapy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPhysiotherapy = null;
					else entity.IsPhysiotherapy = Convert.ToBoolean(value);
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
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
			public System.String SRConsultAnswerType
			{
				get
				{
					System.String data = entity.SRConsultAnswerType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsultAnswerType = null;
					else entity.SRConsultAnswerType = Convert.ToString(value);
				}
			}


			private esParamedicConsultRefer entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicConsultReferQuery query)
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
				throw new Exception("esParamedicConsultRefer can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicConsultRefer : esParamedicConsultRefer
	{	
	}

	[Serializable]
	abstract public class esParamedicConsultReferQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicConsultReferMetadata.Meta();
			}
		}	
			
		public esQueryItem ConsultReferNo
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ConsultReferNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ConsultDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ConsultDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicConsultType
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.SRParamedicConsultType, esSystemType.String);
			}
		} 
			
		public esQueryItem ConsultReferType
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ConsultReferType, esSystemType.String);
			}
		} 
			
		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem ToParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ToParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ToRoomID
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ToRoomID, esSystemType.String);
			}
		} 
			
		public esQueryItem ToRegistrationQue
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ToRegistrationQue, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChiefComplaint
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ChiefComplaint, esSystemType.String);
			}
		} 
			
		public esQueryItem PastMedicalHistory
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.PastMedicalHistory, esSystemType.String);
			}
		} 
			
		public esQueryItem Hpi
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.Hpi, esSystemType.String);
			}
		} 
			
		public esQueryItem ActionExamTreatment
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ActionExamTreatment, esSystemType.String);
			}
		} 
			
		public esQueryItem ActiveMotion
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ActiveMotion, esSystemType.String);
			}
		} 
			
		public esQueryItem PassiveMotion
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.PassiveMotion, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem AnswerDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.AnswerDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem Answer
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.Answer, esSystemType.String);
			}
		} 
			
		public esQueryItem AnswerDiagnose
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.AnswerDiagnose, esSystemType.String);
			}
		} 
			
		public esQueryItem AnswerPlan
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.AnswerPlan, esSystemType.String);
			}
		} 
			
		public esQueryItem AnswerAction
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.AnswerAction, esSystemType.String);
			}
		} 
			
		public esQueryItem ToRegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ToRegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ToAppointmentNo
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.ToAppointmentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPhysiotherapy
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.IsPhysiotherapy, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRConsultAnswerType
		{
			get
			{
				return new esQueryItem(this, ParamedicConsultReferMetadata.ColumnNames.SRConsultAnswerType, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicConsultReferCollection")]
	public partial class ParamedicConsultReferCollection : esParamedicConsultReferCollection, IEnumerable< ParamedicConsultRefer>
	{
		public ParamedicConsultReferCollection()
		{

		}	
		
		public static implicit operator List< ParamedicConsultRefer>(ParamedicConsultReferCollection coll)
		{
			List< ParamedicConsultRefer> list = new List< ParamedicConsultRefer>();
			
			foreach (ParamedicConsultRefer emp in coll)
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
				return  ParamedicConsultReferMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicConsultReferQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicConsultRefer(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicConsultRefer();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicConsultReferQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicConsultReferQuery();
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
		public bool Load(ParamedicConsultReferQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicConsultRefer AddNew()
		{
			ParamedicConsultRefer entity = base.AddNewEntity() as ParamedicConsultRefer;
			
			return entity;		
		}
		public ParamedicConsultRefer FindByPrimaryKey(String consultReferNo)
		{
			return base.FindByPrimaryKey(consultReferNo) as ParamedicConsultRefer;
		}

		#region IEnumerable< ParamedicConsultRefer> Members

		IEnumerator< ParamedicConsultRefer> IEnumerable< ParamedicConsultRefer>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicConsultRefer;
			}
		}

		#endregion
		
		private ParamedicConsultReferQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicConsultRefer' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicConsultRefer ({ConsultReferNo})")]
	[Serializable]
	public partial class ParamedicConsultRefer : esParamedicConsultRefer
	{
		public ParamedicConsultRefer()
		{
		}	
	
		public ParamedicConsultRefer(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicConsultReferMetadata.Meta();
			}
		}	
	
		override protected esParamedicConsultReferQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicConsultReferQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicConsultReferQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicConsultReferQuery();
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
		public bool Load(ParamedicConsultReferQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicConsultReferQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicConsultReferQuery : esParamedicConsultReferQuery
	{
		public ParamedicConsultReferQuery()
		{

		}		
		
		public ParamedicConsultReferQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicConsultReferQuery";
        }
	}

	[Serializable]
	public partial class ParamedicConsultReferMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicConsultReferMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ConsultReferNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ConsultReferNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 25;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ConsultDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ConsultDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ParamedicID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.SRParamedicConsultType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.SRParamedicConsultType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ConsultReferType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ConsultReferType;
			c.CharacterMaxLength = 1;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ToServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ToParamedicID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ToParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ToRoomID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ToRoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ToRegistrationQue, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ToRegistrationQue;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ChiefComplaint, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ChiefComplaint;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.PastMedicalHistory, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.PastMedicalHistory;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.Hpi, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.Hpi;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ActionExamTreatment, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ActionExamTreatment;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ActiveMotion, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ActiveMotion;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.PassiveMotion, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.PassiveMotion;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.Notes, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.AnswerDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.AnswerDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.Answer, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.Answer;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.AnswerDiagnose, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.AnswerDiagnose;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.AnswerPlan, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.AnswerPlan;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.AnswerAction, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.AnswerAction;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ToRegistrationNo, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ToRegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.ToAppointmentNo, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.ToAppointmentNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.IsPhysiotherapy, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.IsPhysiotherapy;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.LastUpdateDateTime, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.LastUpdateByUserID, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.PatientID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.SRConsultAnswerType, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.SRConsultAnswerType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.PhysicianSign, 29, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.PhysicianSign;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);


			c = new esColumnMetadata(ParamedicConsultReferMetadata.ColumnNames.PhysicianAnswerSign, 30, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ParamedicConsultReferMetadata.PropertyNames.PhysicianAnswerSign;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion
	
		static public ParamedicConsultReferMetadata Meta()
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
			public const string ConsultReferNo = "ConsultReferNo";
			public const string ConsultDateTime = "ConsultDateTime";
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string SRParamedicConsultType = "SRParamedicConsultType";
			public const string ConsultReferType = "ConsultReferType";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ToParamedicID = "ToParamedicID";
			public const string ToRoomID = "ToRoomID";
			public const string ToRegistrationQue = "ToRegistrationQue";
			public const string ChiefComplaint = "ChiefComplaint";
			public const string PastMedicalHistory = "PastMedicalHistory";
			public const string Hpi = "Hpi";
			public const string ActionExamTreatment = "ActionExamTreatment";
			public const string ActiveMotion = "ActiveMotion";
			public const string PassiveMotion = "PassiveMotion";
			public const string Notes = "Notes";
			public const string AnswerDateTime = "AnswerDateTime";
			public const string Answer = "Answer";
			public const string AnswerDiagnose = "AnswerDiagnose";
			public const string AnswerPlan = "AnswerPlan";
			public const string AnswerAction = "AnswerAction";
			public const string ToRegistrationNo = "ToRegistrationNo";
			public const string ToAppointmentNo = "ToAppointmentNo";
			public const string IsPhysiotherapy = "IsPhysiotherapy";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PatientID = "PatientID";
			public const string SRConsultAnswerType = "SRConsultAnswerType";
			public const string PhysicianSign = "PhysicianSign";
			public const string PhysicianAnswerSign = "PhysicianAnswerSign";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ConsultReferNo = "ConsultReferNo";
			public const string ConsultDateTime = "ConsultDateTime";
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string SRParamedicConsultType = "SRParamedicConsultType";
			public const string ConsultReferType = "ConsultReferType";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string ToParamedicID = "ToParamedicID";
			public const string ToRoomID = "ToRoomID";
			public const string ToRegistrationQue = "ToRegistrationQue";
			public const string ChiefComplaint = "ChiefComplaint";
			public const string PastMedicalHistory = "PastMedicalHistory";
			public const string Hpi = "Hpi";
			public const string ActionExamTreatment = "ActionExamTreatment";
			public const string ActiveMotion = "ActiveMotion";
			public const string PassiveMotion = "PassiveMotion";
			public const string Notes = "Notes";
			public const string AnswerDateTime = "AnswerDateTime";
			public const string Answer = "Answer";
			public const string AnswerDiagnose = "AnswerDiagnose";
			public const string AnswerPlan = "AnswerPlan";
			public const string AnswerAction = "AnswerAction";
			public const string ToRegistrationNo = "ToRegistrationNo";
			public const string ToAppointmentNo = "ToAppointmentNo";
			public const string IsPhysiotherapy = "IsPhysiotherapy";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PatientID = "PatientID";
			public const string SRConsultAnswerType = "SRConsultAnswerType";
			public const string PhysicianSign = "PhysicianSign";
			public const string PhysicianAnswerSign = "PhysicianAnswerSign";
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
			lock (typeof(ParamedicConsultReferMetadata))
			{
				if(ParamedicConsultReferMetadata.mapDelegates == null)
				{
					ParamedicConsultReferMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicConsultReferMetadata.meta == null)
				{
					ParamedicConsultReferMetadata.meta = new ParamedicConsultReferMetadata();
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
				
				meta.AddTypeMap("ConsultReferNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsultDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicConsultType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsultReferType", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToRoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToRegistrationQue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChiefComplaint", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PastMedicalHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Hpi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionExamTreatment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActiveMotion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PassiveMotion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Answer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerDiagnose", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerPlan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerAction", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToAppointmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPhysiotherapy", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsultAnswerType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianSign", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("PhysicianAnswerSign", new esTypeMap("image", "System.Byte[]"));


				meta.Source = "ParamedicConsultRefer";
				meta.Destination = "ParamedicConsultRefer";
				meta.spInsert = "proc_ParamedicConsultReferInsert";				
				meta.spUpdate = "proc_ParamedicConsultReferUpdate";		
				meta.spDelete = "proc_ParamedicConsultReferDelete";
				meta.spLoadAll = "proc_ParamedicConsultReferLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicConsultReferLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicConsultReferMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
