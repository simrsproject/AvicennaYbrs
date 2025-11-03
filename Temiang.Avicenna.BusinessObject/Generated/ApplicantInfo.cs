/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esApplicantInfoCollection : esEntityCollectionWAuditLog
	{
		public esApplicantInfoCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantInfoQuery query)
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
			this.InitQuery(query as esApplicantInfoQuery);
		}
		#endregion
		
		virtual public ApplicantInfo DetachEntity(ApplicantInfo entity)
		{
			return base.DetachEntity(entity) as ApplicantInfo;
		}
		
		virtual public ApplicantInfo AttachEntity(ApplicantInfo entity)
		{
			return base.AttachEntity(entity) as ApplicantInfo;
		}
		
		virtual public void Combine(ApplicantInfoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantInfo this[int index]
		{
			get
			{
				return base[index] as ApplicantInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantInfo);
		}
	}



	[Serializable]
	abstract public class esApplicantInfo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantInfo()
		{

		}

		public esApplicantInfo(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantID)
		{
			esApplicantInfoQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantID == applicantID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantID",applicantID);
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
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "FirstName": this.str.FirstName = (string)value; break;							
						case "MiddleName": this.str.MiddleName = (string)value; break;							
						case "LastName": this.str.LastName = (string)value; break;							
						case "SRApplicantStatus": this.str.SRApplicantStatus = (string)value; break;							
						case "DateApplied": this.str.DateApplied = (string)value; break;							
						case "DateAvailable": this.str.DateAvailable = (string)value; break;							
						case "ExpectedSalary": this.str.ExpectedSalary = (string)value; break;							
						case "SRCurrencyCode": this.str.SRCurrencyCode = (string)value; break;							
						case "JobOpportunityReferenceNo": this.str.JobOpportunityReferenceNo = (string)value; break;							
						case "SendRejectionDate": this.str.SendRejectionDate = (string)value; break;							
						case "KeepOnFileDuration": this.str.KeepOnFileDuration = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "SRApplicantSource": this.str.SRApplicantSource = (string)value; break;							
						case "Address": this.str.Address = (string)value; break;							
						case "SRState": this.str.SRState = (string)value; break;							
						case "SRCity": this.str.SRCity = (string)value; break;							
						case "ZipCode": this.str.ZipCode = (string)value; break;							
						case "PlaceBirth": this.str.PlaceBirth = (string)value; break;							
						case "BirthDate": this.str.BirthDate = (string)value; break;							
						case "KTPNo": this.str.KTPNo = (string)value; break;							
						case "SRGenderType": this.str.SRGenderType = (string)value; break;							
						case "SRReligion": this.str.SRReligion = (string)value; break;							
						case "SRBloodType": this.str.SRBloodType = (string)value; break;							
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;							
						case "Picture": this.str.Picture = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
							break;
						
						case "DateApplied":
						
							if (value == null || value is System.DateTime)
								this.DateApplied = (System.DateTime?)value;
							break;
						
						case "DateAvailable":
						
							if (value == null || value is System.DateTime)
								this.DateAvailable = (System.DateTime?)value;
							break;
						
						case "ExpectedSalary":
						
							if (value == null || value is System.Int32)
								this.ExpectedSalary = (System.Int32?)value;
							break;
						
						case "SendRejectionDate":
						
							if (value == null || value is System.DateTime)
								this.SendRejectionDate = (System.DateTime?)value;
							break;
						
						case "KeepOnFileDuration":
						
							if (value == null || value is System.Int32)
								this.KeepOnFileDuration = (System.Int32?)value;
							break;
						
						case "BirthDate":
						
							if (value == null || value is System.DateTime)
								this.BirthDate = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to ApplicantInfo.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantInfoMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantInfoMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.FirstName
		/// </summary>
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.FirstName);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.FirstName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.MiddleName
		/// </summary>
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.MiddleName);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.MiddleName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.LastName
		/// </summary>
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.LastName);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.LastName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRApplicantStatus
		/// </summary>
		virtual public System.String SRApplicantStatus
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRApplicantStatus);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRApplicantStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.DateApplied
		/// </summary>
		virtual public System.DateTime? DateApplied
		{
			get
			{
				return base.GetSystemDateTime(ApplicantInfoMetadata.ColumnNames.DateApplied);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantInfoMetadata.ColumnNames.DateApplied, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.DateAvailable
		/// </summary>
		virtual public System.DateTime? DateAvailable
		{
			get
			{
				return base.GetSystemDateTime(ApplicantInfoMetadata.ColumnNames.DateAvailable);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantInfoMetadata.ColumnNames.DateAvailable, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.ExpectedSalary
		/// </summary>
		virtual public System.Int32? ExpectedSalary
		{
			get
			{
				return base.GetSystemInt32(ApplicantInfoMetadata.ColumnNames.ExpectedSalary);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantInfoMetadata.ColumnNames.ExpectedSalary, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRCurrencyCode
		/// </summary>
		virtual public System.String SRCurrencyCode
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRCurrencyCode);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRCurrencyCode, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.JobOpportunityReferenceNo
		/// </summary>
		virtual public System.String JobOpportunityReferenceNo
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.JobOpportunityReferenceNo);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.JobOpportunityReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SendRejectionDate
		/// </summary>
		virtual public System.DateTime? SendRejectionDate
		{
			get
			{
				return base.GetSystemDateTime(ApplicantInfoMetadata.ColumnNames.SendRejectionDate);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantInfoMetadata.ColumnNames.SendRejectionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.KeepOnFileDuration
		/// </summary>
		virtual public System.Int32? KeepOnFileDuration
		{
			get
			{
				return base.GetSystemInt32(ApplicantInfoMetadata.ColumnNames.KeepOnFileDuration);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantInfoMetadata.ColumnNames.KeepOnFileDuration, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRApplicantSource
		/// </summary>
		virtual public System.String SRApplicantSource
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRApplicantSource);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRApplicantSource, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.Address);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.Address, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRState
		/// </summary>
		virtual public System.String SRState
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRState);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRState, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRCity
		/// </summary>
		virtual public System.String SRCity
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRCity);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRCity, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.ZipCode, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.PlaceBirth
		/// </summary>
		virtual public System.String PlaceBirth
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.PlaceBirth);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.PlaceBirth, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.BirthDate
		/// </summary>
		virtual public System.DateTime? BirthDate
		{
			get
			{
				return base.GetSystemDateTime(ApplicantInfoMetadata.ColumnNames.BirthDate);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantInfoMetadata.ColumnNames.BirthDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.KTPNo
		/// </summary>
		virtual public System.String KTPNo
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.KTPNo);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.KTPNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRGenderType, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRReligion
		/// </summary>
		virtual public System.String SRReligion
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRReligion);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRReligion, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRBloodType
		/// </summary>
		virtual public System.String SRBloodType
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRBloodType);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRBloodType, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.SRMaritalStatus);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.Picture
		/// </summary>
		virtual public System.String Picture
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.Picture);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.Picture, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantInfoMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantInfoMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantInfoMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantInfo entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantID
			{
				get
				{
					System.Int32? data = entity.ApplicantID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantID = null;
					else entity.ApplicantID = Convert.ToInt32(value);
				}
			}
				
			public System.String FirstName
			{
				get
				{
					System.String data = entity.FirstName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FirstName = null;
					else entity.FirstName = Convert.ToString(value);
				}
			}
				
			public System.String MiddleName
			{
				get
				{
					System.String data = entity.MiddleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MiddleName = null;
					else entity.MiddleName = Convert.ToString(value);
				}
			}
				
			public System.String LastName
			{
				get
				{
					System.String data = entity.LastName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastName = null;
					else entity.LastName = Convert.ToString(value);
				}
			}
				
			public System.String SRApplicantStatus
			{
				get
				{
					System.String data = entity.SRApplicantStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRApplicantStatus = null;
					else entity.SRApplicantStatus = Convert.ToString(value);
				}
			}
				
			public System.String DateApplied
			{
				get
				{
					System.DateTime? data = entity.DateApplied;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateApplied = null;
					else entity.DateApplied = Convert.ToDateTime(value);
				}
			}
				
			public System.String DateAvailable
			{
				get
				{
					System.DateTime? data = entity.DateAvailable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateAvailable = null;
					else entity.DateAvailable = Convert.ToDateTime(value);
				}
			}
				
			public System.String ExpectedSalary
			{
				get
				{
					System.Int32? data = entity.ExpectedSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpectedSalary = null;
					else entity.ExpectedSalary = Convert.ToInt32(value);
				}
			}
				
			public System.String SRCurrencyCode
			{
				get
				{
					System.String data = entity.SRCurrencyCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrencyCode = null;
					else entity.SRCurrencyCode = Convert.ToString(value);
				}
			}
				
			public System.String JobOpportunityReferenceNo
			{
				get
				{
					System.String data = entity.JobOpportunityReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobOpportunityReferenceNo = null;
					else entity.JobOpportunityReferenceNo = Convert.ToString(value);
				}
			}
				
			public System.String SendRejectionDate
			{
				get
				{
					System.DateTime? data = entity.SendRejectionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SendRejectionDate = null;
					else entity.SendRejectionDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String KeepOnFileDuration
			{
				get
				{
					System.Int32? data = entity.KeepOnFileDuration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeepOnFileDuration = null;
					else entity.KeepOnFileDuration = Convert.ToInt32(value);
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
				
			public System.String SRApplicantSource
			{
				get
				{
					System.String data = entity.SRApplicantSource;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRApplicantSource = null;
					else entity.SRApplicantSource = Convert.ToString(value);
				}
			}
				
			public System.String Address
			{
				get
				{
					System.String data = entity.Address;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address = null;
					else entity.Address = Convert.ToString(value);
				}
			}
				
			public System.String SRState
			{
				get
				{
					System.String data = entity.SRState;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRState = null;
					else entity.SRState = Convert.ToString(value);
				}
			}
				
			public System.String SRCity
			{
				get
				{
					System.String data = entity.SRCity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCity = null;
					else entity.SRCity = Convert.ToString(value);
				}
			}
				
			public System.String ZipCode
			{
				get
				{
					System.String data = entity.ZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipCode = null;
					else entity.ZipCode = Convert.ToString(value);
				}
			}
				
			public System.String PlaceBirth
			{
				get
				{
					System.String data = entity.PlaceBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlaceBirth = null;
					else entity.PlaceBirth = Convert.ToString(value);
				}
			}
				
			public System.String BirthDate
			{
				get
				{
					System.DateTime? data = entity.BirthDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthDate = null;
					else entity.BirthDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String KTPNo
			{
				get
				{
					System.String data = entity.KTPNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KTPNo = null;
					else entity.KTPNo = Convert.ToString(value);
				}
			}
				
			public System.String SRGenderType
			{
				get
				{
					System.String data = entity.SRGenderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGenderType = null;
					else entity.SRGenderType = Convert.ToString(value);
				}
			}
				
			public System.String SRReligion
			{
				get
				{
					System.String data = entity.SRReligion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReligion = null;
					else entity.SRReligion = Convert.ToString(value);
				}
			}
				
			public System.String SRBloodType
			{
				get
				{
					System.String data = entity.SRBloodType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodType = null;
					else entity.SRBloodType = Convert.ToString(value);
				}
			}
				
			public System.String SRMaritalStatus
			{
				get
				{
					System.String data = entity.SRMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
					else entity.SRMaritalStatus = Convert.ToString(value);
				}
			}
				
			public System.String Picture
			{
				get
				{
					System.String data = entity.Picture;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Picture = null;
					else entity.Picture = Convert.ToString(value);
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
			

			private esApplicantInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantInfoQuery query)
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
				throw new Exception("esApplicantInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantInfo : esApplicantInfo
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esApplicantInfoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantInfoMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem FirstName
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.FirstName, esSystemType.String);
			}
		} 
		
		public esQueryItem MiddleName
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.MiddleName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastName
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.LastName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRApplicantStatus
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRApplicantStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem DateApplied
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.DateApplied, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem DateAvailable
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.DateAvailable, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ExpectedSalary
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.ExpectedSalary, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRCurrencyCode
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRCurrencyCode, esSystemType.String);
			}
		} 
		
		public esQueryItem JobOpportunityReferenceNo
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.JobOpportunityReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SendRejectionDate
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SendRejectionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem KeepOnFileDuration
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.KeepOnFileDuration, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem SRApplicantSource
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRApplicantSource, esSystemType.String);
			}
		} 
		
		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.Address, esSystemType.String);
			}
		} 
		
		public esQueryItem SRState
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRState, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCity
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRCity, esSystemType.String);
			}
		} 
		
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
		
		public esQueryItem PlaceBirth
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.PlaceBirth, esSystemType.String);
			}
		} 
		
		public esQueryItem BirthDate
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.BirthDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem KTPNo
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.KTPNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRReligion
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRReligion, esSystemType.String);
			}
		} 
		
		public esQueryItem SRBloodType
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRBloodType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem Picture
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.Picture, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantInfoCollection")]
	public partial class ApplicantInfoCollection : esApplicantInfoCollection, IEnumerable<ApplicantInfo>
	{
		public ApplicantInfoCollection()
		{

		}
		
		public static implicit operator List<ApplicantInfo>(ApplicantInfoCollection coll)
		{
			List<ApplicantInfo> list = new List<ApplicantInfo>();
			
			foreach (ApplicantInfo emp in coll)
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
				return  ApplicantInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantInfo();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantInfo AddNew()
		{
			ApplicantInfo entity = base.AddNewEntity() as ApplicantInfo;
			
			return entity;
		}

		public ApplicantInfo FindByPrimaryKey(System.Int32 applicantID)
		{
			return base.FindByPrimaryKey(applicantID) as ApplicantInfo;
		}


		#region IEnumerable<ApplicantInfo> Members

		IEnumerator<ApplicantInfo> IEnumerable<ApplicantInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantInfo;
			}
		}

		#endregion
		
		private ApplicantInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantInfo' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantInfo ({ApplicantID})")]
	[Serializable]
	public partial class ApplicantInfo : esApplicantInfo
	{
		public ApplicantInfo()
		{

		}
	
		public ApplicantInfo(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantInfoQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantInfoQuery : esApplicantInfoQuery
	{
		public ApplicantInfoQuery()
		{

		}		
		
		public ApplicantInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantInfoQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.ApplicantID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.ApplicantID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.FirstName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 60;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.MiddleName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.LastName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRApplicantStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRApplicantStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.DateApplied, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.DateApplied;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.DateAvailable, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.DateAvailable;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.ExpectedSalary, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.ExpectedSalary;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRCurrencyCode, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRCurrencyCode;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.JobOpportunityReferenceNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.JobOpportunityReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SendRejectionDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SendRejectionDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.KeepOnFileDuration, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.KeepOnFileDuration;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.Note, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRApplicantSource, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRApplicantSource;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.Address, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRState, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRState;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRCity, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRCity;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.ZipCode, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.PlaceBirth, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.PlaceBirth;
			c.CharacterMaxLength = 60;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.BirthDate, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.BirthDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.KTPNo, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.KTPNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRGenderType, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRReligion, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRReligion;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRBloodType, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRBloodType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.SRMaritalStatus, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.Picture, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.Picture;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.LastUpdateDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantInfoMetadata.ColumnNames.LastUpdateByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantInfoMetadata Meta()
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
			 public const string ApplicantID = "ApplicantID";
			 public const string FirstName = "FirstName";
			 public const string MiddleName = "MiddleName";
			 public const string LastName = "LastName";
			 public const string SRApplicantStatus = "SRApplicantStatus";
			 public const string DateApplied = "DateApplied";
			 public const string DateAvailable = "DateAvailable";
			 public const string ExpectedSalary = "ExpectedSalary";
			 public const string SRCurrencyCode = "SRCurrencyCode";
			 public const string JobOpportunityReferenceNo = "JobOpportunityReferenceNo";
			 public const string SendRejectionDate = "SendRejectionDate";
			 public const string KeepOnFileDuration = "KeepOnFileDuration";
			 public const string Note = "Note";
			 public const string SRApplicantSource = "SRApplicantSource";
			 public const string Address = "Address";
			 public const string SRState = "SRState";
			 public const string SRCity = "SRCity";
			 public const string ZipCode = "ZipCode";
			 public const string PlaceBirth = "PlaceBirth";
			 public const string BirthDate = "BirthDate";
			 public const string KTPNo = "KTPNo";
			 public const string SRGenderType = "SRGenderType";
			 public const string SRReligion = "SRReligion";
			 public const string SRBloodType = "SRBloodType";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string Picture = "Picture";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantID = "ApplicantID";
			 public const string FirstName = "FirstName";
			 public const string MiddleName = "MiddleName";
			 public const string LastName = "LastName";
			 public const string SRApplicantStatus = "SRApplicantStatus";
			 public const string DateApplied = "DateApplied";
			 public const string DateAvailable = "DateAvailable";
			 public const string ExpectedSalary = "ExpectedSalary";
			 public const string SRCurrencyCode = "SRCurrencyCode";
			 public const string JobOpportunityReferenceNo = "JobOpportunityReferenceNo";
			 public const string SendRejectionDate = "SendRejectionDate";
			 public const string KeepOnFileDuration = "KeepOnFileDuration";
			 public const string Note = "Note";
			 public const string SRApplicantSource = "SRApplicantSource";
			 public const string Address = "Address";
			 public const string SRState = "SRState";
			 public const string SRCity = "SRCity";
			 public const string ZipCode = "ZipCode";
			 public const string PlaceBirth = "PlaceBirth";
			 public const string BirthDate = "BirthDate";
			 public const string KTPNo = "KTPNo";
			 public const string SRGenderType = "SRGenderType";
			 public const string SRReligion = "SRReligion";
			 public const string SRBloodType = "SRBloodType";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string Picture = "Picture";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(ApplicantInfoMetadata))
			{
				if(ApplicantInfoMetadata.mapDelegates == null)
				{
					ApplicantInfoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantInfoMetadata.meta == null)
				{
					ApplicantInfoMetadata.meta = new ApplicantInfoMetadata();
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
				

				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRApplicantStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateApplied", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DateAvailable", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ExpectedSalary", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRCurrencyCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JobOpportunityReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SendRejectionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("KeepOnFileDuration", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRApplicantSource", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRState", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlaceBirth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BirthDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("KTPNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Picture", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantInfo";
				meta.Destination = "ApplicantInfo";
				
				meta.spInsert = "proc_ApplicantInfoInsert";				
				meta.spUpdate = "proc_ApplicantInfoUpdate";		
				meta.spDelete = "proc_ApplicantInfoDelete";
				meta.spLoadAll = "proc_ApplicantInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantInfoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
