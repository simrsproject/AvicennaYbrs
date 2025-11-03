/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:16 PM
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
	abstract public class esFabricCollection : esEntityCollectionWAuditLog
	{
		public esFabricCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "FabricCollection";
		}

		#region Query Logic
		protected void InitQuery(esFabricQuery query)
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
			this.InitQuery(query as esFabricQuery);
		}
		#endregion
		
		virtual public Fabric DetachEntity(Fabric entity)
		{
			return base.DetachEntity(entity) as Fabric;
		}
		
		virtual public Fabric AttachEntity(Fabric entity)
		{
			return base.AttachEntity(entity) as Fabric;
		}
		
		virtual public void Combine(FabricCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Fabric this[int index]
		{
			get
			{
				return base[index] as Fabric;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Fabric);
		}
	}



	[Serializable]
	abstract public class esFabric : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esFabricQuery GetDynamicQuery()
		{
			return null;
		}

		public esFabric()
		{

		}

		public esFabric(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String fabricID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(fabricID);
			else
				return LoadByPrimaryKeyStoredProcedure(fabricID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String fabricID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(fabricID);
			else
				return LoadByPrimaryKeyStoredProcedure(fabricID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String fabricID)
		{
			esFabricQuery query = this.GetDynamicQuery();
			query.Where(query.FabricID == fabricID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String fabricID)
		{
			esParameters parms = new esParameters();
			parms.Add("FabricID",fabricID);
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
						case "FabricID": this.str.FabricID = (string)value; break;							
						case "FabricName": this.str.FabricName = (string)value; break;							
						case "ShortName": this.str.ShortName = (string)value; break;							
						case "ContractNumber": this.str.ContractNumber = (string)value; break;							
						case "ContractStart": this.str.ContractStart = (string)value; break;							
						case "ContractEnd": this.str.ContractEnd = (string)value; break;							
						case "ContractSummary": this.str.ContractSummary = (string)value; break;							
						case "ContactPerson": this.str.ContactPerson = (string)value; break;							
						case "IsPKP": this.str.IsPKP = (string)value; break;							
						case "TaxRegistrationNo": this.str.TaxRegistrationNo = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
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
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ContractStart":
						
							if (value == null || value is System.DateTime)
								this.ContractStart = (System.DateTime?)value;
							break;
						
						case "ContractEnd":
						
							if (value == null || value is System.DateTime)
								this.ContractEnd = (System.DateTime?)value;
							break;
						
						case "IsPKP":
						
							if (value == null || value is System.Boolean)
								this.IsPKP = (System.Boolean?)value;
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
		/// Maps to Fabric.FabricID
		/// </summary>
		virtual public System.String FabricID
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.FabricID);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.FabricID, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.FabricName
		/// </summary>
		virtual public System.String FabricName
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.FabricName);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.FabricName, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.ShortName);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.ShortName, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.ContractNumber
		/// </summary>
		virtual public System.String ContractNumber
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.ContractNumber);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.ContractNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.ContractStart
		/// </summary>
		virtual public System.DateTime? ContractStart
		{
			get
			{
				return base.GetSystemDateTime(FabricMetadata.ColumnNames.ContractStart);
			}
			
			set
			{
				base.SetSystemDateTime(FabricMetadata.ColumnNames.ContractStart, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.ContractEnd
		/// </summary>
		virtual public System.DateTime? ContractEnd
		{
			get
			{
				return base.GetSystemDateTime(FabricMetadata.ColumnNames.ContractEnd);
			}
			
			set
			{
				base.SetSystemDateTime(FabricMetadata.ColumnNames.ContractEnd, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.ContractSummary
		/// </summary>
		virtual public System.String ContractSummary
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.ContractSummary);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.ContractSummary, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.ContactPerson
		/// </summary>
		virtual public System.String ContactPerson
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.ContactPerson);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.ContactPerson, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.IsPKP
		/// </summary>
		virtual public System.Boolean? IsPKP
		{
			get
			{
				return base.GetSystemBoolean(FabricMetadata.ColumnNames.IsPKP);
			}
			
			set
			{
				base.SetSystemBoolean(FabricMetadata.ColumnNames.IsPKP, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.TaxRegistrationNo
		/// </summary>
		virtual public System.String TaxRegistrationNo
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.TaxRegistrationNo);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.TaxRegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(FabricMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(FabricMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.StreetName);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.StreetName, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.District);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.District, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.City);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.City, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.County);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.County, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.State);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.State, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.ZipCode, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.PhoneNo);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.PhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.FaxNo);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.FaxNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.MobilePhoneNo);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(FabricMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(FabricMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Fabric.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(FabricMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(FabricMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esFabric entity)
			{
				this.entity = entity;
			}
			
	
			public System.String FabricID
			{
				get
				{
					System.String data = entity.FabricID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FabricID = null;
					else entity.FabricID = Convert.ToString(value);
				}
			}
				
			public System.String FabricName
			{
				get
				{
					System.String data = entity.FabricName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FabricName = null;
					else entity.FabricName = Convert.ToString(value);
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
				
			public System.String ContractNumber
			{
				get
				{
					System.String data = entity.ContractNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractNumber = null;
					else entity.ContractNumber = Convert.ToString(value);
				}
			}
				
			public System.String ContractStart
			{
				get
				{
					System.DateTime? data = entity.ContractStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractStart = null;
					else entity.ContractStart = Convert.ToDateTime(value);
				}
			}
				
			public System.String ContractEnd
			{
				get
				{
					System.DateTime? data = entity.ContractEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractEnd = null;
					else entity.ContractEnd = Convert.ToDateTime(value);
				}
			}
				
			public System.String ContractSummary
			{
				get
				{
					System.String data = entity.ContractSummary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractSummary = null;
					else entity.ContractSummary = Convert.ToString(value);
				}
			}
				
			public System.String ContactPerson
			{
				get
				{
					System.String data = entity.ContactPerson;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactPerson = null;
					else entity.ContactPerson = Convert.ToString(value);
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
			

			private esFabric entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esFabricQuery query)
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
				throw new Exception("esFabric can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Fabric : esFabric
	{

				
		#region SupplierFabricCollectionByFabricID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefFabricToSupplierFabric
		/// </summary>

		[XmlIgnore]
		public SupplierFabricCollection SupplierFabricCollectionByFabricID
		{
			get
			{
				if(this._SupplierFabricCollectionByFabricID == null)
				{
					this._SupplierFabricCollectionByFabricID = new SupplierFabricCollection();
					this._SupplierFabricCollectionByFabricID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("SupplierFabricCollectionByFabricID", this._SupplierFabricCollectionByFabricID);
				
					if(this.FabricID != null)
					{
						this._SupplierFabricCollectionByFabricID.Query.Where(this._SupplierFabricCollectionByFabricID.Query.FabricID == this.FabricID);
						this._SupplierFabricCollectionByFabricID.Query.Load();

						// Auto-hookup Foreign Keys
						this._SupplierFabricCollectionByFabricID.fks.Add(SupplierFabricMetadata.ColumnNames.FabricID, this.FabricID);
					}
				}

				return this._SupplierFabricCollectionByFabricID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._SupplierFabricCollectionByFabricID != null) 
				{ 
					this.RemovePostSave("SupplierFabricCollectionByFabricID"); 
					this._SupplierFabricCollectionByFabricID = null;
					
				} 
			} 			
		}

		private SupplierFabricCollection _SupplierFabricCollectionByFabricID;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "SupplierFabricCollectionByFabricID", typeof(SupplierFabricCollection), new SupplierFabric()));
		
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
	abstract public class esFabricQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return FabricMetadata.Meta();
			}
		}	
		

		public esQueryItem FabricID
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.FabricID, esSystemType.String);
			}
		} 
		
		public esQueryItem FabricName
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.FabricName, esSystemType.String);
			}
		} 
		
		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		} 
		
		public esQueryItem ContractNumber
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.ContractNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem ContractStart
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.ContractStart, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ContractEnd
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.ContractEnd, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ContractSummary
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.ContractSummary, esSystemType.String);
			}
		} 
		
		public esQueryItem ContactPerson
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.ContactPerson, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPKP
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.IsPKP, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem TaxRegistrationNo
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.TaxRegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		} 
		
		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.District, esSystemType.String);
			}
		} 
		
		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.City, esSystemType.String);
			}
		} 
		
		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.County, esSystemType.String);
			}
		} 
		
		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.State, esSystemType.String);
			}
		} 
		
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
		
		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, FabricMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("FabricCollection")]
	public partial class FabricCollection : esFabricCollection, IEnumerable<Fabric>
	{
		public FabricCollection()
		{

		}
		
		public static implicit operator List<Fabric>(FabricCollection coll)
		{
			List<Fabric> list = new List<Fabric>();
			
			foreach (Fabric emp in coll)
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
				return  FabricMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new FabricQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Fabric(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Fabric();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public FabricQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new FabricQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(FabricQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Fabric AddNew()
		{
			Fabric entity = base.AddNewEntity() as Fabric;
			
			return entity;
		}

		public Fabric FindByPrimaryKey(System.String fabricID)
		{
			return base.FindByPrimaryKey(fabricID) as Fabric;
		}


		#region IEnumerable<Fabric> Members

		IEnumerator<Fabric> IEnumerable<Fabric>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Fabric;
			}
		}

		#endregion
		
		private FabricQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Fabric' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Fabric ({FabricID})")]
	[Serializable]
	public partial class Fabric : esFabric
	{
		public Fabric()
		{

		}
	
		public Fabric(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return FabricMetadata.Meta();
			}
		}
		
		
		
		override protected esFabricQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new FabricQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public FabricQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new FabricQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(FabricQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private FabricQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class FabricQuery : esFabricQuery
	{
		public FabricQuery()
		{

		}		
		
		public FabricQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "FabricQuery";
        }
		
			
	}


	[Serializable]
	public partial class FabricMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected FabricMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(FabricMetadata.ColumnNames.FabricID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.FabricID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.FabricName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.FabricName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.ContractNumber, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.ContractNumber;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.ContractStart, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = FabricMetadata.PropertyNames.ContractStart;
			c.HasDefault = true;
			c.Default = @"(CONVERT([smalldatetime],'19000101',(105)))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.ContractEnd, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = FabricMetadata.PropertyNames.ContractEnd;
			c.HasDefault = true;
			c.Default = @"(CONVERT([smalldatetime],'19000101',(105)))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.ContractSummary, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.ContractSummary;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.ContactPerson, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.ContactPerson;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.IsPKP, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = FabricMetadata.PropertyNames.IsPKP;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.TaxRegistrationNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.TaxRegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.IsActive, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = FabricMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.StreetName, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.District, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.District;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.City, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.County, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.State, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.ZipCode, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.PhoneNo, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.FaxNo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.Email, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.MobilePhoneNo, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = FabricMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(FabricMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = FabricMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public FabricMetadata Meta()
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
			 public const string FabricID = "FabricID";
			 public const string FabricName = "FabricName";
			 public const string ShortName = "ShortName";
			 public const string ContractNumber = "ContractNumber";
			 public const string ContractStart = "ContractStart";
			 public const string ContractEnd = "ContractEnd";
			 public const string ContractSummary = "ContractSummary";
			 public const string ContactPerson = "ContactPerson";
			 public const string IsPKP = "IsPKP";
			 public const string TaxRegistrationNo = "TaxRegistrationNo";
			 public const string IsActive = "IsActive";
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
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string FabricID = "FabricID";
			 public const string FabricName = "FabricName";
			 public const string ShortName = "ShortName";
			 public const string ContractNumber = "ContractNumber";
			 public const string ContractStart = "ContractStart";
			 public const string ContractEnd = "ContractEnd";
			 public const string ContractSummary = "ContractSummary";
			 public const string ContactPerson = "ContactPerson";
			 public const string IsPKP = "IsPKP";
			 public const string TaxRegistrationNo = "TaxRegistrationNo";
			 public const string IsActive = "IsActive";
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
			lock (typeof(FabricMetadata))
			{
				if(FabricMetadata.mapDelegates == null)
				{
					FabricMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (FabricMetadata.meta == null)
				{
					FabricMetadata.meta = new FabricMetadata();
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
				

				meta.AddTypeMap("FabricID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FabricName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContractNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContractStart", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ContractEnd", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ContractSummary", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("ContactPerson", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPKP", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TaxRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
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
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Fabric";
				meta.Destination = "Fabric";
				
				meta.spInsert = "proc_FabricInsert";				
				meta.spUpdate = "proc_FabricUpdate";		
				meta.spDelete = "proc_FabricDelete";
				meta.spLoadAll = "proc_FabricLoadAll";
				meta.spLoadByPrimaryKey = "proc_FabricLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private FabricMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
