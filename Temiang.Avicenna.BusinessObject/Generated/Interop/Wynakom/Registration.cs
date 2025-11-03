/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/1/2020 11:37:15 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject.Interop.Wynakom
{

	[Serializable]
	abstract public class esRegistrationCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationQuery query)
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
			this.InitQuery(query as esRegistrationQuery);
		}
		#endregion
		
		virtual public Registration DetachEntity(Registration entity)
		{
			return base.DetachEntity(entity) as Registration;
		}
		
		virtual public Registration AttachEntity(Registration entity)
		{
			return base.AttachEntity(entity) as Registration;
		}
		
		virtual public void Combine(RegistrationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Registration this[int index]
		{
			get
			{
				return base[index] as Registration;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Registration);
		}
	}



	[Serializable]
	abstract public class esRegistration : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistration()
		{

		}

		public esRegistration(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.DateTime orderDateTime, System.String orderNumber, System.String patientId, System.String visitNumber)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderDateTime, orderNumber, patientId, visitNumber);
			else
				return LoadByPrimaryKeyStoredProcedure(orderDateTime, orderNumber, patientId, visitNumber);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime orderDateTime, System.String orderNumber, System.String patientId, System.String visitNumber)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderDateTime, orderNumber, patientId, visitNumber);
			else
				return LoadByPrimaryKeyStoredProcedure(orderDateTime, orderNumber, patientId, visitNumber);
		}

		private bool LoadByPrimaryKeyDynamic(System.DateTime orderDateTime, System.String orderNumber, System.String patientId, System.String visitNumber)
		{
			esRegistrationQuery query = this.GetDynamicQuery();
			query.Where(query.OrderDateTime == orderDateTime, query.OrderNumber == orderNumber, query.PatientId == patientId, query.VisitNumber == visitNumber);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.DateTime orderDateTime, System.String orderNumber, System.String patientId, System.String visitNumber)
		{
			esParameters parms = new esParameters();
			parms.Add("Order_DateTime",orderDateTime);			parms.Add("Order_Number",orderNumber);			parms.Add("Patient_Id",patientId);			parms.Add("Visit_Number",visitNumber);
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
						case "PatientId": this.str.PatientId = (string)value; break;							
						case "VisitNumber": this.str.VisitNumber = (string)value; break;							
						case "OrderNumber": this.str.OrderNumber = (string)value; break;							
						case "OrderDateTime": this.str.OrderDateTime = (string)value; break;							
						case "DiagnoseName": this.str.DiagnoseName = (string)value; break;							
						case "Cito": this.str.Cito = (string)value; break;							
						case "ServiceUnitName": this.str.ServiceUnitName = (string)value; break;							
						case "GuarantorName": this.str.GuarantorName = (string)value; break;							
						case "AgreementName": this.str.AgreementName = (string)value; break;							
						case "DoctorName": this.str.DoctorName = (string)value; break;							
						case "ClassName": this.str.ClassName = (string)value; break;							
						case "WardName": this.str.WardName = (string)value; break;							
						case "RoomName": this.str.RoomName = (string)value; break;							
						case "BedName": this.str.BedName = (string)value; break;							
						case "RegUserName": this.str.RegUserName = (string)value; break;							
						case "LisRegNo": this.str.LisRegNo = (string)value; break;							
						case "RetrievedDt": this.str.RetrievedDt = (string)value; break;							
						case "RetrievedFlag": this.str.RetrievedFlag = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "AgreementID": this.str.AgreementID = (string)value; break;							
						case "DoctorID": this.str.DoctorID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "WardID": this.str.WardID = (string)value; break;							
						case "RoomID": this.str.RoomID = (string)value; break;							
						case "BedID": this.str.BedID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;
						case "PhysicianSenders": this.str.PhysicianSenders = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderDateTime":
						
							if (value == null || value is System.DateTime)
								this.OrderDateTime = (System.DateTime?)value;
							break;
						
						case "Cito":
						
							if (value == null || value is System.Boolean)
								this.Cito = (System.Boolean?)value;
							break;
						
						case "RetrievedDt":
						
							if (value == null || value is System.DateTime)
								this.RetrievedDt = (System.DateTime?)value;
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
		/// Maps to Registration.Patient_Id
		/// </summary>
		virtual public System.String PatientId
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.PatientId);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.PatientId, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Visit_Number
		/// </summary>
		virtual public System.String VisitNumber
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.VisitNumber);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.VisitNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Order_Number
		/// </summary>
		virtual public System.String OrderNumber
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.OrderNumber);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.OrderNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Order_DateTime
		/// </summary>
		virtual public System.DateTime? OrderDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.OrderDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.OrderDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Diagnose_Name
		/// </summary>
		virtual public System.String DiagnoseName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DiagnoseName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DiagnoseName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Cito
		/// </summary>
		virtual public System.Boolean? Cito
		{
			get
			{
				return base.GetSystemBoolean(RegistrationMetadata.ColumnNames.Cito);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationMetadata.ColumnNames.Cito, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Service_Unit_Name
		/// </summary>
		virtual public System.String ServiceUnitName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ServiceUnitName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ServiceUnitName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Guarantor_Name
		/// </summary>
		virtual public System.String GuarantorName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.GuarantorName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.GuarantorName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Agreement_Name
		/// </summary>
		virtual public System.String AgreementName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.AgreementName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.AgreementName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Doctor_Name
		/// </summary>
		virtual public System.String DoctorName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DoctorName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DoctorName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Class_Name
		/// </summary>
		virtual public System.String ClassName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ClassName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ClassName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Ward_Name
		/// </summary>
		virtual public System.String WardName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.WardName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.WardName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Room_Name
		/// </summary>
		virtual public System.String RoomName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.RoomName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.RoomName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Bed_Name
		/// </summary>
		virtual public System.String BedName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.BedName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.BedName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Reg_User_Name
		/// </summary>
		virtual public System.String RegUserName
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.RegUserName);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.RegUserName, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.LIS_REG_NO
		/// </summary>
		virtual public System.String LisRegNo
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.LisRegNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.LisRegNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.RETRIEVED_DT
		/// </summary>
		virtual public System.DateTime? RetrievedDt
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMetadata.ColumnNames.RetrievedDt);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationMetadata.ColumnNames.RetrievedDt, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.RETRIEVED_FLAG
		/// </summary>
		virtual public System.String RetrievedFlag
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.RetrievedFlag);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.RetrievedFlag, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Service_Unit_ID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Guarantor_ID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Agreement_ID
		/// </summary>
		virtual public System.String AgreementID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.AgreementID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.AgreementID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Doctor_ID
		/// </summary>
		virtual public System.String DoctorID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.DoctorID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.DoctorID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Class_ID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Ward_ID
		/// </summary>
		virtual public System.String WardID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.WardID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.WardID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Room_ID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.RoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Bed_ID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.BedID);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.BedID, value);
			}
		}
		
		/// <summary>
		/// Maps to Registration.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.Notes, value);
			}
		}

		/// <summary>
		/// Maps to Registration.PhysicianSenders
		/// </summary>
		virtual public System.String PhysicianSenders
		{
			get
			{
				return base.GetSystemString(RegistrationMetadata.ColumnNames.PhysicianSenders);
			}

			set
			{
				base.SetSystemString(RegistrationMetadata.ColumnNames.PhysicianSenders, value);
			}
		}

		#endregion

		#region String Properties


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
			public esStrings(esRegistration entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PatientId
			{
				get
				{
					System.String data = entity.PatientId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientId = null;
					else entity.PatientId = Convert.ToString(value);
				}
			}
				
			public System.String VisitNumber
			{
				get
				{
					System.String data = entity.VisitNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VisitNumber = null;
					else entity.VisitNumber = Convert.ToString(value);
				}
			}
				
			public System.String OrderNumber
			{
				get
				{
					System.String data = entity.OrderNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNumber = null;
					else entity.OrderNumber = Convert.ToString(value);
				}
			}
				
			public System.String OrderDateTime
			{
				get
				{
					System.DateTime? data = entity.OrderDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDateTime = null;
					else entity.OrderDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String DiagnoseName
			{
				get
				{
					System.String data = entity.DiagnoseName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseName = null;
					else entity.DiagnoseName = Convert.ToString(value);
				}
			}
				
			public System.String Cito
			{
				get
				{
					System.Boolean? data = entity.Cito;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cito = null;
					else entity.Cito = Convert.ToBoolean(value);
				}
			}
				
			public System.String ServiceUnitName
			{
				get
				{
					System.String data = entity.ServiceUnitName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitName = null;
					else entity.ServiceUnitName = Convert.ToString(value);
				}
			}
				
			public System.String GuarantorName
			{
				get
				{
					System.String data = entity.GuarantorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorName = null;
					else entity.GuarantorName = Convert.ToString(value);
				}
			}
				
			public System.String AgreementName
			{
				get
				{
					System.String data = entity.AgreementName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgreementName = null;
					else entity.AgreementName = Convert.ToString(value);
				}
			}
				
			public System.String DoctorName
			{
				get
				{
					System.String data = entity.DoctorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DoctorName = null;
					else entity.DoctorName = Convert.ToString(value);
				}
			}
				
			public System.String ClassName
			{
				get
				{
					System.String data = entity.ClassName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassName = null;
					else entity.ClassName = Convert.ToString(value);
				}
			}
				
			public System.String WardName
			{
				get
				{
					System.String data = entity.WardName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WardName = null;
					else entity.WardName = Convert.ToString(value);
				}
			}
				
			public System.String RoomName
			{
				get
				{
					System.String data = entity.RoomName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomName = null;
					else entity.RoomName = Convert.ToString(value);
				}
			}
				
			public System.String BedName
			{
				get
				{
					System.String data = entity.BedName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedName = null;
					else entity.BedName = Convert.ToString(value);
				}
			}
				
			public System.String RegUserName
			{
				get
				{
					System.String data = entity.RegUserName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegUserName = null;
					else entity.RegUserName = Convert.ToString(value);
				}
			}
				
			public System.String LisRegNo
			{
				get
				{
					System.String data = entity.LisRegNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LisRegNo = null;
					else entity.LisRegNo = Convert.ToString(value);
				}
			}
				
			public System.String RetrievedDt
			{
				get
				{
					System.DateTime? data = entity.RetrievedDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RetrievedDt = null;
					else entity.RetrievedDt = Convert.ToDateTime(value);
				}
			}
				
			public System.String RetrievedFlag
			{
				get
				{
					System.String data = entity.RetrievedFlag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RetrievedFlag = null;
					else entity.RetrievedFlag = Convert.ToString(value);
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
				
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
				
			public System.String AgreementID
			{
				get
				{
					System.String data = entity.AgreementID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgreementID = null;
					else entity.AgreementID = Convert.ToString(value);
				}
			}
				
			public System.String DoctorID
			{
				get
				{
					System.String data = entity.DoctorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DoctorID = null;
					else entity.DoctorID = Convert.ToString(value);
				}
			}
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String WardID
			{
				get
				{
					System.String data = entity.WardID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WardID = null;
					else entity.WardID = Convert.ToString(value);
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

			public System.String PhysicianSenders
			{
				get
				{
					System.String data = entity.PhysicianSenders;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicianSenders = null;
					else entity.PhysicianSenders = Convert.ToString(value);
				}
			}

			private esRegistration entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationQuery query)
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
				throw new Exception("esRegistration can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esRegistrationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientId
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PatientId, esSystemType.String);
			}
		} 
		
		public esQueryItem VisitNumber
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.VisitNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderNumber
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.OrderNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.OrderDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem DiagnoseName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DiagnoseName, esSystemType.String);
			}
		} 
		
		public esQueryItem Cito
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.Cito, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ServiceUnitName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ServiceUnitName, esSystemType.String);
			}
		} 
		
		public esQueryItem GuarantorName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.GuarantorName, esSystemType.String);
			}
		} 
		
		public esQueryItem AgreementName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgreementName, esSystemType.String);
			}
		} 
		
		public esQueryItem DoctorName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DoctorName, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ClassName, esSystemType.String);
			}
		} 
		
		public esQueryItem WardName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.WardName, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RoomName, esSystemType.String);
			}
		} 
		
		public esQueryItem BedName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.BedName, esSystemType.String);
			}
		} 
		
		public esQueryItem RegUserName
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RegUserName, esSystemType.String);
			}
		} 
		
		public esQueryItem LisRegNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.LisRegNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RetrievedDt
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RetrievedDt, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem RetrievedFlag
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RetrievedFlag, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem AgreementID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.AgreementID, esSystemType.String);
			}
		} 
		
		public esQueryItem DoctorID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.DoctorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem WardID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.WardID, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.BedID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem PhysicianSenders
		{
			get
			{
				return new esQueryItem(this, RegistrationMetadata.ColumnNames.PhysicianSenders, esSystemType.String);
			}
		}

	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationCollection")]
	public partial class RegistrationCollection : esRegistrationCollection, IEnumerable<Registration>
	{
		public RegistrationCollection()
		{

		}
		
		public static implicit operator List<Registration>(RegistrationCollection coll)
		{
			List<Registration> list = new List<Registration>();
			
			foreach (Registration emp in coll)
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
				return  RegistrationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Registration(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Registration();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Registration AddNew()
		{
			Registration entity = base.AddNewEntity() as Registration;
			
			return entity;
		}

		public Registration FindByPrimaryKey(System.DateTime orderDateTime, System.String orderNumber, System.String patientId, System.String visitNumber)
		{
			return base.FindByPrimaryKey(orderDateTime, orderNumber, patientId, visitNumber) as Registration;
		}


		#region IEnumerable<Registration> Members

		IEnumerator<Registration> IEnumerable<Registration>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Registration;
			}
		}

		#endregion
		
		private RegistrationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Registration' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Registration ({PatientId},{VisitNumber},{OrderNumber},{OrderDateTime})")]
	[Serializable]
	public partial class Registration : esRegistration
	{
		public Registration()
		{

		}
	
		public Registration(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationQuery : esRegistrationQuery
	{
		public RegistrationQuery()
		{

		}		
		
		public RegistrationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PatientId, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.PatientId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.VisitNumber, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.VisitNumber;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.OrderNumber, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.OrderNumber;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.OrderDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.OrderDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DiagnoseName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DiagnoseName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.Cito, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationMetadata.PropertyNames.Cito;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ServiceUnitName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ServiceUnitName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.GuarantorName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.GuarantorName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgreementName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgreementName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DoctorName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DoctorName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ClassName, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ClassName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.WardName, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.WardName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RoomName, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.RoomName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.BedName, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.BedName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RegUserName, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.RegUserName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.LisRegNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.LisRegNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RetrievedDt, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMetadata.PropertyNames.RetrievedDt;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RetrievedFlag, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.RetrievedFlag;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ServiceUnitID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.GuarantorID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.AgreementID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.AgreementID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.DoctorID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.DoctorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.ClassID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.WardID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.WardID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.RoomID, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.BedID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.Notes, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMetadata.ColumnNames.PhysicianSenders, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMetadata.PropertyNames.PhysicianSenders;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion	
	
		static public RegistrationMetadata Meta()
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
			 public const string PatientId = "Patient_Id";
			 public const string VisitNumber = "Visit_Number";
			 public const string OrderNumber = "Order_Number";
			 public const string OrderDateTime = "Order_DateTime";
			 public const string DiagnoseName = "Diagnose_Name";
			 public const string Cito = "Cito";
			 public const string ServiceUnitName = "Service_Unit_Name";
			 public const string GuarantorName = "Guarantor_Name";
			 public const string AgreementName = "Agreement_Name";
			 public const string DoctorName = "Doctor_Name";
			 public const string ClassName = "Class_Name";
			 public const string WardName = "Ward_Name";
			 public const string RoomName = "Room_Name";
			 public const string BedName = "Bed_Name";
			 public const string RegUserName = "Reg_User_Name";
			 public const string LisRegNo = "LIS_REG_NO";
			 public const string RetrievedDt = "RETRIEVED_DT";
			 public const string RetrievedFlag = "RETRIEVED_FLAG";
			 public const string ServiceUnitID = "Service_Unit_ID";
			 public const string GuarantorID = "Guarantor_ID";
			 public const string AgreementID = "Agreement_ID";
			 public const string DoctorID = "Doctor_ID";
			 public const string ClassID = "Class_ID";
			 public const string WardID = "Ward_ID";
			 public const string RoomID = "Room_ID";
			 public const string BedID = "Bed_ID";
			 public const string Notes = "Notes";
			public const string PhysicianSenders = "PhysicianSenders";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientId = "PatientId";
			 public const string VisitNumber = "VisitNumber";
			 public const string OrderNumber = "OrderNumber";
			 public const string OrderDateTime = "OrderDateTime";
			 public const string DiagnoseName = "DiagnoseName";
			 public const string Cito = "Cito";
			 public const string ServiceUnitName = "ServiceUnitName";
			 public const string GuarantorName = "GuarantorName";
			 public const string AgreementName = "AgreementName";
			 public const string DoctorName = "DoctorName";
			 public const string ClassName = "ClassName";
			 public const string WardName = "WardName";
			 public const string RoomName = "RoomName";
			 public const string BedName = "BedName";
			 public const string RegUserName = "RegUserName";
			 public const string LisRegNo = "LisRegNo";
			 public const string RetrievedDt = "RetrievedDt";
			 public const string RetrievedFlag = "RetrievedFlag";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string GuarantorID = "GuarantorID";
			 public const string AgreementID = "AgreementID";
			 public const string DoctorID = "DoctorID";
			 public const string ClassID = "ClassID";
			 public const string WardID = "WardID";
			 public const string RoomID = "RoomID";
			 public const string BedID = "BedID";
			 public const string Notes = "Notes";
			public const string PhysicianSenders = "PhysicianSenders";
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
			lock (typeof(RegistrationMetadata))
			{
				if(RegistrationMetadata.mapDelegates == null)
				{
					RegistrationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationMetadata.meta == null)
				{
					RegistrationMetadata.meta = new RegistrationMetadata();
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
				

				meta.AddTypeMap("PatientId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VisitNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderDateTime", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("DiagnoseName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Cito", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ServiceUnitName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AgreementName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DoctorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WardName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegUserName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LisRegNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RetrievedDt", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("RetrievedFlag", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AgreementID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DoctorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WardID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicianSenders", new esTypeMap("varchar", "System.String"));



				meta.Source = "Registration";
				meta.Destination = "Registration";
				
				meta.spInsert = "proc_RegistrationInsert";				
				meta.spUpdate = "proc_RegistrationUpdate";		
				meta.spDelete = "proc_RegistrationDelete";
				meta.spLoadAll = "proc_RegistrationLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
