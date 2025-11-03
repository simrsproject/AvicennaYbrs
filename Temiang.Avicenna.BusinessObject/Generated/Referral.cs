/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:24 PM
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
	abstract public class esReferralCollection : esEntityCollectionWAuditLog
	{
		public esReferralCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ReferralCollection";
		}

		#region Query Logic
		protected void InitQuery(esReferralQuery query)
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
			this.InitQuery(query as esReferralQuery);
		}
		#endregion
		
		virtual public Referral DetachEntity(Referral entity)
		{
			return base.DetachEntity(entity) as Referral;
		}
		
		virtual public Referral AttachEntity(Referral entity)
		{
			return base.AttachEntity(entity) as Referral;
		}
		
		virtual public void Combine(ReferralCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Referral this[int index]
		{
			get
			{
				return base[index] as Referral;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Referral);
		}
	}



	[Serializable]
	abstract public class esReferral : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esReferralQuery GetDynamicQuery()
		{
			return null;
		}

		public esReferral()
		{

		}

		public esReferral(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String referralID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referralID);
			else
				return LoadByPrimaryKeyStoredProcedure(referralID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String referralID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referralID);
			else
				return LoadByPrimaryKeyStoredProcedure(referralID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String referralID)
		{
			esReferralQuery query = this.GetDynamicQuery();
			query.Where(query.ReferralID == referralID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String referralID)
		{
			esParameters parms = new esParameters();
			parms.Add("ReferralID",referralID);
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
						case "ReferralID": this.str.ReferralID = (string)value; break;							
						case "ReferralName": this.str.ReferralName = (string)value; break;							
						case "ShortName": this.str.ShortName = (string)value; break;							
						case "DepartmentName": this.str.DepartmentName = (string)value; break;							
						case "SRReferralGroup": this.str.SRReferralGroup = (string)value; break;							
						case "TaxRegistrationNo": this.str.TaxRegistrationNo = (string)value; break;							
						case "IsPKP": this.str.IsPKP = (string)value; break;							
						case "TermID": this.str.TermID = (string)value; break;							
						case "StreetName": this.str.StreetName = (string)value; break;							
						case "District": this.str.District = (string)value; break;							
						case "City": this.str.City = (string)value; break;							
						case "County": this.str.County = (string)value; break;							
						case "State": this.str.State = (string)value; break;							
						case "ZipCode": this.str.ZipCode = (string)value; break;							
						case "PhoneNo": this.str.PhoneNo = (string)value; break;							
						case "FaxNo": this.str.FaxNo = (string)value; break;							
						case "Email": this.str.Email = (string)value; break;							
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;							
						case "IsRefferalFrom": this.str.IsRefferalFrom = (string)value; break;							
						case "IsRefferalTo": this.str.IsRefferalTo = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsPKP":
						
							if (value == null || value is System.Boolean)
								this.IsPKP = (System.Boolean?)value;
							break;
						
						case "IsRefferalFrom":
						
							if (value == null || value is System.Boolean)
								this.IsRefferalFrom = (System.Boolean?)value;
							break;
						
						case "IsRefferalTo":
						
							if (value == null || value is System.Boolean)
								this.IsRefferalTo = (System.Boolean?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to Referral.ReferralID
		/// </summary>
		virtual public System.String ReferralID
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.ReferralID);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.ReferralID, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.ReferralName
		/// </summary>
		virtual public System.String ReferralName
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.ReferralName);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.ReferralName, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.ShortName);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.ShortName, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.DepartmentName
		/// </summary>
		virtual public System.String DepartmentName
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.DepartmentName);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.DepartmentName, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.SRReferralGroup
		/// </summary>
		virtual public System.String SRReferralGroup
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.SRReferralGroup);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.SRReferralGroup, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.TaxRegistrationNo
		/// </summary>
		virtual public System.String TaxRegistrationNo
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.TaxRegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.TaxRegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.IsPKP
		/// </summary>
		virtual public System.Boolean? IsPKP
		{
			get
			{
				return base.GetSystemBoolean(ReferralMetadata.ColumnNames.IsPKP);
			}
			
			set
			{
				base.SetSystemBoolean(ReferralMetadata.ColumnNames.IsPKP, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.TermID
		/// </summary>
		virtual public System.String TermID
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.TermID);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.TermID, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.StreetName);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.StreetName, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.District);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.District, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.City);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.City, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.County);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.County, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.State);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.State, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.ZipCode, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.PhoneNo);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.PhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.FaxNo);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.FaxNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.MobilePhoneNo);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.IsRefferalFrom
		/// </summary>
		virtual public System.Boolean? IsRefferalFrom
		{
			get
			{
				return base.GetSystemBoolean(ReferralMetadata.ColumnNames.IsRefferalFrom);
			}
			
			set
			{
				base.SetSystemBoolean(ReferralMetadata.ColumnNames.IsRefferalFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.IsRefferalTo
		/// </summary>
		virtual public System.Boolean? IsRefferalTo
		{
			get
			{
				return base.GetSystemBoolean(ReferralMetadata.ColumnNames.IsRefferalTo);
			}
			
			set
			{
				base.SetSystemBoolean(ReferralMetadata.ColumnNames.IsRefferalTo, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ReferralMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ReferralMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ReferralMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ReferralMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Referral.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ReferralMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ReferralMetadata.ColumnNames.ParamedicID, value);
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
			public esStrings(esReferral entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ReferralID
			{
				get
				{
					System.String data = entity.ReferralID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralID = null;
					else entity.ReferralID = Convert.ToString(value);
				}
			}
				
			public System.String ReferralName
			{
				get
				{
					System.String data = entity.ReferralName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralName = null;
					else entity.ReferralName = Convert.ToString(value);
				}
			}
				
			public System.String ShortName
			{
				get
				{
					System.String data = entity.ShortName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShortName = null;
					else entity.ShortName = Convert.ToString(value);
				}
			}
				
			public System.String DepartmentName
			{
				get
				{
					System.String data = entity.DepartmentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentName = null;
					else entity.DepartmentName = Convert.ToString(value);
				}
			}
				
			public System.String SRReferralGroup
			{
				get
				{
					System.String data = entity.SRReferralGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReferralGroup = null;
					else entity.SRReferralGroup = Convert.ToString(value);
				}
			}
				
			public System.String TaxRegistrationNo
			{
				get
				{
					System.String data = entity.TaxRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxRegistrationNo = null;
					else entity.TaxRegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String IsPKP
			{
				get
				{
					System.Boolean? data = entity.IsPKP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPKP = null;
					else entity.IsPKP = Convert.ToBoolean(value);
				}
			}
				
			public System.String TermID
			{
				get
				{
					System.String data = entity.TermID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TermID = null;
					else entity.TermID = Convert.ToString(value);
				}
			}
				
			public System.String StreetName
			{
				get
				{
					System.String data = entity.StreetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StreetName = null;
					else entity.StreetName = Convert.ToString(value);
				}
			}
				
			public System.String District
			{
				get
				{
					System.String data = entity.District;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.District = null;
					else entity.District = Convert.ToString(value);
				}
			}
				
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
				
			public System.String County
			{
				get
				{
					System.String data = entity.County;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.County = null;
					else entity.County = Convert.ToString(value);
				}
			}
				
			public System.String State
			{
				get
				{
					System.String data = entity.State;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.State = null;
					else entity.State = Convert.ToString(value);
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
				
			public System.String PhoneNo
			{
				get
				{
					System.String data = entity.PhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNo = null;
					else entity.PhoneNo = Convert.ToString(value);
				}
			}
				
			public System.String FaxNo
			{
				get
				{
					System.String data = entity.FaxNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaxNo = null;
					else entity.FaxNo = Convert.ToString(value);
				}
			}
				
			public System.String Email
			{
				get
				{
					System.String data = entity.Email;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Email = null;
					else entity.Email = Convert.ToString(value);
				}
			}
				
			public System.String MobilePhoneNo
			{
				get
				{
					System.String data = entity.MobilePhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MobilePhoneNo = null;
					else entity.MobilePhoneNo = Convert.ToString(value);
				}
			}
				
			public System.String IsRefferalFrom
			{
				get
				{
					System.Boolean? data = entity.IsRefferalFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRefferalFrom = null;
					else entity.IsRefferalFrom = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsRefferalTo
			{
				get
				{
					System.Boolean? data = entity.IsRefferalTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRefferalTo = null;
					else entity.IsRefferalTo = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esReferral entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esReferralQuery query)
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
				throw new Exception("esReferral can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Referral : esReferral
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
	abstract public class esReferralQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ReferralMetadata.Meta();
			}
		}	
		

		public esQueryItem ReferralID
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.ReferralID, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferralName
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.ReferralName, esSystemType.String);
			}
		} 
		
		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		} 
		
		public esQueryItem DepartmentName
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.DepartmentName, esSystemType.String);
			}
		} 
		
		public esQueryItem SRReferralGroup
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.SRReferralGroup, esSystemType.String);
			}
		} 
		
		public esQueryItem TaxRegistrationNo
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.TaxRegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPKP
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.IsPKP, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem TermID
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.TermID, esSystemType.String);
			}
		} 
		
		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		} 
		
		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.District, esSystemType.String);
			}
		} 
		
		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.City, esSystemType.String);
			}
		} 
		
		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.County, esSystemType.String);
			}
		} 
		
		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.State, esSystemType.String);
			}
		} 
		
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
		
		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsRefferalFrom
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.IsRefferalFrom, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsRefferalTo
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.IsRefferalTo, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ReferralMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ReferralCollection")]
	public partial class ReferralCollection : esReferralCollection, IEnumerable<Referral>
	{
		public ReferralCollection()
		{

		}
		
		public static implicit operator List<Referral>(ReferralCollection coll)
		{
			List<Referral> list = new List<Referral>();
			
			foreach (Referral emp in coll)
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
				return  ReferralMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReferralQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Referral(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Referral();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ReferralQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReferralQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ReferralQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Referral AddNew()
		{
			Referral entity = base.AddNewEntity() as Referral;
			
			return entity;
		}

		public Referral FindByPrimaryKey(System.String referralID)
		{
			return base.FindByPrimaryKey(referralID) as Referral;
		}


		#region IEnumerable<Referral> Members

		IEnumerator<Referral> IEnumerable<Referral>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Referral;
			}
		}

		#endregion
		
		private ReferralQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Referral' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Referral ({ReferralID})")]
	[Serializable]
	public partial class Referral : esReferral
	{
		public Referral()
		{

		}
	
		public Referral(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ReferralMetadata.Meta();
			}
		}
		
		
		
		override protected esReferralQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReferralQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ReferralQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReferralQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ReferralQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ReferralQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ReferralQuery : esReferralQuery
	{
		public ReferralQuery()
		{

		}		
		
		public ReferralQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ReferralQuery";
        }
		
			
	}


	[Serializable]
	public partial class ReferralMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ReferralMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ReferralMetadata.ColumnNames.ReferralID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.ReferralID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.ReferralName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.ReferralName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.DepartmentName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.DepartmentName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.SRReferralGroup, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.SRReferralGroup;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.TaxRegistrationNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.TaxRegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.IsPKP, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ReferralMetadata.PropertyNames.IsPKP;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.TermID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.TermID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.StreetName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.District, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.District;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.City, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.County, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.State, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.ZipCode, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.PhoneNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.FaxNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.Email, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.MobilePhoneNo, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.IsRefferalFrom, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ReferralMetadata.PropertyNames.IsRefferalFrom;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.IsRefferalTo, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ReferralMetadata.PropertyNames.IsRefferalTo;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.IsActive, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ReferralMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReferralMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReferralMetadata.ColumnNames.ParamedicID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferralMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ReferralMetadata Meta()
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
			 public const string ReferralID = "ReferralID";
			 public const string ReferralName = "ReferralName";
			 public const string ShortName = "ShortName";
			 public const string DepartmentName = "DepartmentName";
			 public const string SRReferralGroup = "SRReferralGroup";
			 public const string TaxRegistrationNo = "TaxRegistrationNo";
			 public const string IsPKP = "IsPKP";
			 public const string TermID = "TermID";
			 public const string StreetName = "StreetName";
			 public const string District = "District";
			 public const string City = "City";
			 public const string County = "County";
			 public const string State = "State";
			 public const string ZipCode = "ZipCode";
			 public const string PhoneNo = "PhoneNo";
			 public const string FaxNo = "FaxNo";
			 public const string Email = "Email";
			 public const string MobilePhoneNo = "MobilePhoneNo";
			 public const string IsRefferalFrom = "IsRefferalFrom";
			 public const string IsRefferalTo = "IsRefferalTo";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParamedicID = "ParamedicID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ReferralID = "ReferralID";
			 public const string ReferralName = "ReferralName";
			 public const string ShortName = "ShortName";
			 public const string DepartmentName = "DepartmentName";
			 public const string SRReferralGroup = "SRReferralGroup";
			 public const string TaxRegistrationNo = "TaxRegistrationNo";
			 public const string IsPKP = "IsPKP";
			 public const string TermID = "TermID";
			 public const string StreetName = "StreetName";
			 public const string District = "District";
			 public const string City = "City";
			 public const string County = "County";
			 public const string State = "State";
			 public const string ZipCode = "ZipCode";
			 public const string PhoneNo = "PhoneNo";
			 public const string FaxNo = "FaxNo";
			 public const string Email = "Email";
			 public const string MobilePhoneNo = "MobilePhoneNo";
			 public const string IsRefferalFrom = "IsRefferalFrom";
			 public const string IsRefferalTo = "IsRefferalTo";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParamedicID = "ParamedicID";
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
			lock (typeof(ReferralMetadata))
			{
				if(ReferralMetadata.mapDelegates == null)
				{
					ReferralMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ReferralMetadata.meta == null)
				{
					ReferralMetadata.meta = new ReferralMetadata();
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
				

				meta.AddTypeMap("ReferralID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReferralGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TaxRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPKP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TermID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRefferalFrom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRefferalTo", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Referral";
				meta.Destination = "Referral";
				
				meta.spInsert = "proc_ReferralInsert";				
				meta.spUpdate = "proc_ReferralUpdate";		
				meta.spDelete = "proc_ReferralDelete";
				meta.spLoadAll = "proc_ReferralLoadAll";
				meta.spLoadByPrimaryKey = "proc_ReferralLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ReferralMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
