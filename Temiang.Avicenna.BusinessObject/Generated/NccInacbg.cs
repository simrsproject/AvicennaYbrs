/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/22/2020 11:52:02 AM
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



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esNccInacbgCollection : esEntityCollectionWAuditLog
	{
		public esNccInacbgCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NccInacbgCollection";
		}

		#region Query Logic
		protected void InitQuery(esNccInacbgQuery query)
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
			this.InitQuery(query as esNccInacbgQuery);
		}
		#endregion
		
		virtual public NccInacbg DetachEntity(NccInacbg entity)
		{
			return base.DetachEntity(entity) as NccInacbg;
		}
		
		virtual public NccInacbg AttachEntity(NccInacbg entity)
		{
			return base.AttachEntity(entity) as NccInacbg;
		}
		
		virtual public void Combine(NccInacbgCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NccInacbg this[int index]
		{
			get
			{
				return base[index] as NccInacbg;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NccInacbg);
		}
	}



	[Serializable]
	abstract public class esNccInacbg : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNccInacbgQuery GetDynamicQuery()
		{
			return null;
		}

		public esNccInacbg()
		{

		}

		public esNccInacbg(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo)
		{
			esNccInacbgQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "PatientId": this.str.PatientId = (string)value; break;							
						case "AdmissionId": this.str.AdmissionId = (string)value; break;							
						case "HospitalAdmissionId": this.str.HospitalAdmissionId = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "AddPaymentAmt": this.str.AddPaymentAmt = (string)value; break;							
						case "CoverageAmount": this.str.CoverageAmount = (string)value; break;							
						case "SpecialDrugID": this.str.SpecialDrugID = (string)value; break;							
						case "SpecialDrugAmount": this.str.SpecialDrugAmount = (string)value; break;							
						case "SpecialProcedureID": this.str.SpecialProcedureID = (string)value; break;							
						case "SpecialProcedureAmount": this.str.SpecialProcedureAmount = (string)value; break;							
						case "CbgID": this.str.CbgID = (string)value; break;							
						case "CbgName": this.str.CbgName = (string)value; break;							
						case "CbgStatus": this.str.CbgStatus = (string)value; break;							
						case "CbgSentStatus": this.str.CbgSentStatus = (string)value; break;							
						case "SpecialInvestigationID": this.str.SpecialInvestigationID = (string)value; break;							
						case "SpecialInvestigationAmount": this.str.SpecialInvestigationAmount = (string)value; break;							
						case "SpecialProsthesisID": this.str.SpecialProsthesisID = (string)value; break;							
						case "SpecialProsthesisAmount": this.str.SpecialProsthesisAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PatientId":
						
							if (value == null || value is System.Int32)
								this.PatientId = (System.Int32?)value;
							break;
						
						case "AdmissionId":
						
							if (value == null || value is System.Int32)
								this.AdmissionId = (System.Int32?)value;
							break;
						
						case "HospitalAdmissionId":
						
							if (value == null || value is System.Int32)
								this.HospitalAdmissionId = (System.Int32?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "AddPaymentAmt":
						
							if (value == null || value is System.Decimal)
								this.AddPaymentAmt = (System.Decimal?)value;
							break;
						
						case "CoverageAmount":
						
							if (value == null || value is System.Decimal)
								this.CoverageAmount = (System.Decimal?)value;
							break;
						
						case "SpecialDrugAmount":
						
							if (value == null || value is System.Decimal)
								this.SpecialDrugAmount = (System.Decimal?)value;
							break;
						
						case "SpecialProcedureAmount":
						
							if (value == null || value is System.Decimal)
								this.SpecialProcedureAmount = (System.Decimal?)value;
							break;
						
						case "SpecialInvestigationAmount":
						
							if (value == null || value is System.Decimal)
								this.SpecialInvestigationAmount = (System.Decimal?)value;
							break;
						
						case "SpecialProsthesisAmount":
						
							if (value == null || value is System.Decimal)
								this.SpecialProsthesisAmount = (System.Decimal?)value;
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
		/// Maps to NccInacbg.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.PatientId
		/// </summary>
		virtual public System.Int32? PatientId
		{
			get
			{
				return base.GetSystemInt32(NccInacbgMetadata.ColumnNames.PatientId);
			}
			
			set
			{
				base.SetSystemInt32(NccInacbgMetadata.ColumnNames.PatientId, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.AdmissionId
		/// </summary>
		virtual public System.Int32? AdmissionId
		{
			get
			{
				return base.GetSystemInt32(NccInacbgMetadata.ColumnNames.AdmissionId);
			}
			
			set
			{
				base.SetSystemInt32(NccInacbgMetadata.ColumnNames.AdmissionId, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.HospitalAdmissionId
		/// </summary>
		virtual public System.Int32? HospitalAdmissionId
		{
			get
			{
				return base.GetSystemInt32(NccInacbgMetadata.ColumnNames.HospitalAdmissionId);
			}
			
			set
			{
				base.SetSystemInt32(NccInacbgMetadata.ColumnNames.HospitalAdmissionId, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(NccInacbgMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(NccInacbgMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.AddPaymentAmt
		/// </summary>
		virtual public System.Decimal? AddPaymentAmt
		{
			get
			{
				return base.GetSystemDecimal(NccInacbgMetadata.ColumnNames.AddPaymentAmt);
			}
			
			set
			{
				base.SetSystemDecimal(NccInacbgMetadata.ColumnNames.AddPaymentAmt, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.CoverageAmount
		/// </summary>
		virtual public System.Decimal? CoverageAmount
		{
			get
			{
				return base.GetSystemDecimal(NccInacbgMetadata.ColumnNames.CoverageAmount);
			}
			
			set
			{
				base.SetSystemDecimal(NccInacbgMetadata.ColumnNames.CoverageAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialDrugID
		/// </summary>
		virtual public System.String SpecialDrugID
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.SpecialDrugID);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.SpecialDrugID, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialDrugAmount
		/// </summary>
		virtual public System.Decimal? SpecialDrugAmount
		{
			get
			{
				return base.GetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialDrugAmount);
			}
			
			set
			{
				base.SetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialDrugAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialProcedureID
		/// </summary>
		virtual public System.String SpecialProcedureID
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.SpecialProcedureID);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.SpecialProcedureID, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialProcedureAmount
		/// </summary>
		virtual public System.Decimal? SpecialProcedureAmount
		{
			get
			{
				return base.GetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialProcedureAmount);
			}
			
			set
			{
				base.SetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialProcedureAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.CbgID
		/// </summary>
		virtual public System.String CbgID
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.CbgID);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.CbgID, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.CbgName
		/// </summary>
		virtual public System.String CbgName
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.CbgName);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.CbgName, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.CbgStatus
		/// </summary>
		virtual public System.String CbgStatus
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.CbgStatus);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.CbgStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.CbgSentStatus
		/// </summary>
		virtual public System.String CbgSentStatus
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.CbgSentStatus);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.CbgSentStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialInvestigationID
		/// </summary>
		virtual public System.String SpecialInvestigationID
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.SpecialInvestigationID);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.SpecialInvestigationID, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialInvestigationAmount
		/// </summary>
		virtual public System.Decimal? SpecialInvestigationAmount
		{
			get
			{
				return base.GetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialInvestigationAmount);
			}
			
			set
			{
				base.SetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialInvestigationAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialProsthesisID
		/// </summary>
		virtual public System.String SpecialProsthesisID
		{
			get
			{
				return base.GetSystemString(NccInacbgMetadata.ColumnNames.SpecialProsthesisID);
			}
			
			set
			{
				base.SetSystemString(NccInacbgMetadata.ColumnNames.SpecialProsthesisID, value);
			}
		}
		
		/// <summary>
		/// Maps to NccInacbg.SpecialProsthesisAmount
		/// </summary>
		virtual public System.Decimal? SpecialProsthesisAmount
		{
			get
			{
				return base.GetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialProsthesisAmount);
			}
			
			set
			{
				base.SetSystemDecimal(NccInacbgMetadata.ColumnNames.SpecialProsthesisAmount, value);
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
			public esStrings(esNccInacbg entity)
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
				
			public System.String PatientId
			{
				get
				{
					System.Int32? data = entity.PatientId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientId = null;
					else entity.PatientId = Convert.ToInt32(value);
				}
			}
				
			public System.String AdmissionId
			{
				get
				{
					System.Int32? data = entity.AdmissionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdmissionId = null;
					else entity.AdmissionId = Convert.ToInt32(value);
				}
			}
				
			public System.String HospitalAdmissionId
			{
				get
				{
					System.Int32? data = entity.HospitalAdmissionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HospitalAdmissionId = null;
					else entity.HospitalAdmissionId = Convert.ToInt32(value);
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
				
			public System.String AddPaymentAmt
			{
				get
				{
					System.Decimal? data = entity.AddPaymentAmt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddPaymentAmt = null;
					else entity.AddPaymentAmt = Convert.ToDecimal(value);
				}
			}
				
			public System.String CoverageAmount
			{
				get
				{
					System.Decimal? data = entity.CoverageAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageAmount = null;
					else entity.CoverageAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String SpecialDrugID
			{
				get
				{
					System.String data = entity.SpecialDrugID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialDrugID = null;
					else entity.SpecialDrugID = Convert.ToString(value);
				}
			}
				
			public System.String SpecialDrugAmount
			{
				get
				{
					System.Decimal? data = entity.SpecialDrugAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialDrugAmount = null;
					else entity.SpecialDrugAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String SpecialProcedureID
			{
				get
				{
					System.String data = entity.SpecialProcedureID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialProcedureID = null;
					else entity.SpecialProcedureID = Convert.ToString(value);
				}
			}
				
			public System.String SpecialProcedureAmount
			{
				get
				{
					System.Decimal? data = entity.SpecialProcedureAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialProcedureAmount = null;
					else entity.SpecialProcedureAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String CbgID
			{
				get
				{
					System.String data = entity.CbgID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CbgID = null;
					else entity.CbgID = Convert.ToString(value);
				}
			}
				
			public System.String CbgName
			{
				get
				{
					System.String data = entity.CbgName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CbgName = null;
					else entity.CbgName = Convert.ToString(value);
				}
			}
				
			public System.String CbgStatus
			{
				get
				{
					System.String data = entity.CbgStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CbgStatus = null;
					else entity.CbgStatus = Convert.ToString(value);
				}
			}
				
			public System.String CbgSentStatus
			{
				get
				{
					System.String data = entity.CbgSentStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CbgSentStatus = null;
					else entity.CbgSentStatus = Convert.ToString(value);
				}
			}
				
			public System.String SpecialInvestigationID
			{
				get
				{
					System.String data = entity.SpecialInvestigationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialInvestigationID = null;
					else entity.SpecialInvestigationID = Convert.ToString(value);
				}
			}
				
			public System.String SpecialInvestigationAmount
			{
				get
				{
					System.Decimal? data = entity.SpecialInvestigationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialInvestigationAmount = null;
					else entity.SpecialInvestigationAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String SpecialProsthesisID
			{
				get
				{
					System.String data = entity.SpecialProsthesisID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialProsthesisID = null;
					else entity.SpecialProsthesisID = Convert.ToString(value);
				}
			}
				
			public System.String SpecialProsthesisAmount
			{
				get
				{
					System.Decimal? data = entity.SpecialProsthesisAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialProsthesisAmount = null;
					else entity.SpecialProsthesisAmount = Convert.ToDecimal(value);
				}
			}
			

			private esNccInacbg entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNccInacbgQuery query)
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
				throw new Exception("esNccInacbg can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esNccInacbgQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NccInacbgMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientId
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.PatientId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AdmissionId
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.AdmissionId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem HospitalAdmissionId
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.HospitalAdmissionId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem AddPaymentAmt
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.AddPaymentAmt, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CoverageAmount
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.CoverageAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SpecialDrugID
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialDrugID, esSystemType.String);
			}
		} 
		
		public esQueryItem SpecialDrugAmount
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialDrugAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SpecialProcedureID
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialProcedureID, esSystemType.String);
			}
		} 
		
		public esQueryItem SpecialProcedureAmount
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialProcedureAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CbgID
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.CbgID, esSystemType.String);
			}
		} 
		
		public esQueryItem CbgName
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.CbgName, esSystemType.String);
			}
		} 
		
		public esQueryItem CbgStatus
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.CbgStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem CbgSentStatus
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.CbgSentStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem SpecialInvestigationID
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialInvestigationID, esSystemType.String);
			}
		} 
		
		public esQueryItem SpecialInvestigationAmount
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialInvestigationAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SpecialProsthesisID
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialProsthesisID, esSystemType.String);
			}
		} 
		
		public esQueryItem SpecialProsthesisAmount
		{
			get
			{
				return new esQueryItem(this, NccInacbgMetadata.ColumnNames.SpecialProsthesisAmount, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NccInacbgCollection")]
	public partial class NccInacbgCollection : esNccInacbgCollection, IEnumerable<NccInacbg>
	{
		public NccInacbgCollection()
		{

		}
		
		public static implicit operator List<NccInacbg>(NccInacbgCollection coll)
		{
			List<NccInacbg> list = new List<NccInacbg>();
			
			foreach (NccInacbg emp in coll)
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
				return  NccInacbgMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NccInacbgQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NccInacbg(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NccInacbg();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NccInacbgQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NccInacbgQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NccInacbgQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NccInacbg AddNew()
		{
			NccInacbg entity = base.AddNewEntity() as NccInacbg;
			
			return entity;
		}

		public NccInacbg FindByPrimaryKey(System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as NccInacbg;
		}


		#region IEnumerable<NccInacbg> Members

		IEnumerator<NccInacbg> IEnumerable<NccInacbg>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NccInacbg;
			}
		}

		#endregion
		
		private NccInacbgQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NccInacbg' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NccInacbg ({RegistrationNo})")]
	[Serializable]
	public partial class NccInacbg : esNccInacbg
	{
		public NccInacbg()
		{

		}
	
		public NccInacbg(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NccInacbgMetadata.Meta();
			}
		}
		
		
		
		override protected esNccInacbgQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NccInacbgQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NccInacbgQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NccInacbgQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NccInacbgQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NccInacbgQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NccInacbgQuery : esNccInacbgQuery
	{
		public NccInacbgQuery()
		{

		}		
		
		public NccInacbgQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NccInacbgQuery";
        }
		
			
	}


	[Serializable]
	public partial class NccInacbgMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NccInacbgMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.PatientId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NccInacbgMetadata.PropertyNames.PatientId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.AdmissionId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NccInacbgMetadata.PropertyNames.AdmissionId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.HospitalAdmissionId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = NccInacbgMetadata.PropertyNames.HospitalAdmissionId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = NccInacbgMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.AddPaymentAmt, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NccInacbgMetadata.PropertyNames.AddPaymentAmt;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.CoverageAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NccInacbgMetadata.PropertyNames.CoverageAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialDrugID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialDrugID;
			c.CharacterMaxLength = 25;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialDrugAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialDrugAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialProcedureID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialProcedureID;
			c.CharacterMaxLength = 25;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialProcedureAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialProcedureAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.CbgID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.CbgID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.CbgName, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.CbgName;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.CbgStatus, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.CbgStatus;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.CbgSentStatus, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.CbgSentStatus;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialInvestigationID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialInvestigationID;
			c.CharacterMaxLength = 25;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialInvestigationAmount, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialInvestigationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialProsthesisID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialProsthesisID;
			c.CharacterMaxLength = 25;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(NccInacbgMetadata.ColumnNames.SpecialProsthesisAmount, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = NccInacbgMetadata.PropertyNames.SpecialProsthesisAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NccInacbgMetadata Meta()
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
			 public const string PatientId = "PatientId";
			 public const string AdmissionId = "AdmissionId";
			 public const string HospitalAdmissionId = "HospitalAdmissionId";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string AddPaymentAmt = "AddPaymentAmt";
			 public const string CoverageAmount = "CoverageAmount";
			 public const string SpecialDrugID = "SpecialDrugID";
			 public const string SpecialDrugAmount = "SpecialDrugAmount";
			 public const string SpecialProcedureID = "SpecialProcedureID";
			 public const string SpecialProcedureAmount = "SpecialProcedureAmount";
			 public const string CbgID = "CbgID";
			 public const string CbgName = "CbgName";
			 public const string CbgStatus = "CbgStatus";
			 public const string CbgSentStatus = "CbgSentStatus";
			 public const string SpecialInvestigationID = "SpecialInvestigationID";
			 public const string SpecialInvestigationAmount = "SpecialInvestigationAmount";
			 public const string SpecialProsthesisID = "SpecialProsthesisID";
			 public const string SpecialProsthesisAmount = "SpecialProsthesisAmount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string PatientId = "PatientId";
			 public const string AdmissionId = "AdmissionId";
			 public const string HospitalAdmissionId = "HospitalAdmissionId";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string AddPaymentAmt = "AddPaymentAmt";
			 public const string CoverageAmount = "CoverageAmount";
			 public const string SpecialDrugID = "SpecialDrugID";
			 public const string SpecialDrugAmount = "SpecialDrugAmount";
			 public const string SpecialProcedureID = "SpecialProcedureID";
			 public const string SpecialProcedureAmount = "SpecialProcedureAmount";
			 public const string CbgID = "CbgID";
			 public const string CbgName = "CbgName";
			 public const string CbgStatus = "CbgStatus";
			 public const string CbgSentStatus = "CbgSentStatus";
			 public const string SpecialInvestigationID = "SpecialInvestigationID";
			 public const string SpecialInvestigationAmount = "SpecialInvestigationAmount";
			 public const string SpecialProsthesisID = "SpecialProsthesisID";
			 public const string SpecialProsthesisAmount = "SpecialProsthesisAmount";
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
			lock (typeof(NccInacbgMetadata))
			{
				if(NccInacbgMetadata.mapDelegates == null)
				{
					NccInacbgMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NccInacbgMetadata.meta == null)
				{
					NccInacbgMetadata.meta = new NccInacbgMetadata();
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
				meta.AddTypeMap("PatientId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AdmissionId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HospitalAdmissionId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AddPaymentAmt", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CoverageAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SpecialDrugID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecialDrugAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SpecialProcedureID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecialProcedureAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CbgID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CbgName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CbgStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CbgSentStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecialInvestigationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecialInvestigationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SpecialProsthesisID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecialProsthesisAmount", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "NccInacbg";
				meta.Destination = "NccInacbg";
				
				meta.spInsert = "proc_NccInacbgInsert";				
				meta.spUpdate = "proc_NccInacbgUpdate";		
				meta.spDelete = "proc_NccInacbgDelete";
				meta.spLoadAll = "proc_NccInacbgLoadAll";
				meta.spLoadByPrimaryKey = "proc_NccInacbgLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NccInacbgMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
