/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/14/2012 4:37:22 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.PAC
{

	[Serializable]
	abstract public class esLabTestOrderCollection : esEntityCollectionWAuditLog
	{
		public esLabTestOrderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LabTestOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esLabTestOrderQuery query)
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
			this.InitQuery(query as esLabTestOrderQuery);
		}
		#endregion
		
		virtual public LabTestOrder DetachEntity(LabTestOrder entity)
		{
			return base.DetachEntity(entity) as LabTestOrder;
		}
		
		virtual public LabTestOrder AttachEntity(LabTestOrder entity)
		{
			return base.AttachEntity(entity) as LabTestOrder;
		}
		
		virtual public void Combine(LabTestOrderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LabTestOrder this[int index]
		{
			get
			{
				return base[index] as LabTestOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LabTestOrder);
		}
	}



	[Serializable]
	abstract public class esLabTestOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLabTestOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esLabTestOrder()
		{

		}

		public esLabTestOrder(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
		{
			esLabTestOrderQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "MedicalNo": this.str.MedicalNo = (string)value; break;							
						case "FirstName": this.str.FirstName = (string)value; break;							
						case "MiddleName": this.str.MiddleName = (string)value; break;							
						case "LastName": this.str.LastName = (string)value; break;							
						case "Sex": this.str.Sex = (string)value; break;							
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;							
						case "FromServiceUnitName": this.str.FromServiceUnitName = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "ClassName": this.str.ClassName = (string)value; break;							
						case "CityOfBirth": this.str.CityOfBirth = (string)value; break;							
						case "DateOfBirth": this.str.DateOfBirth = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "ParamedicName": this.str.ParamedicName = (string)value; break;							
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
						case "Company": this.str.Company = (string)value; break;							
						case "GuarantorName": this.str.GuarantorName = (string)value; break;							
						case "TestOrderID": this.str.TestOrderID = (string)value; break;							
						case "TestOrderName": this.str.TestOrderName = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsConfirm": this.str.IsConfirm = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						
						case "DateOfBirth":
						
							if (value == null || value is System.DateTime)
								this.DateOfBirth = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsConfirm":
						
							if (value == null || value is System.Boolean)
								this.IsConfirm = (System.Boolean?)value;
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
		/// Maps to LabTestOrder.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(LabTestOrderMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(LabTestOrderMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.MedicalNo
		/// </summary>
		virtual public System.String MedicalNo
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.MedicalNo);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.MedicalNo, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.FirstName
		/// </summary>
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.FirstName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.FirstName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.MiddleName
		/// </summary>
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.MiddleName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.MiddleName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.LastName
		/// </summary>
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.LastName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.LastName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.Sex
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.Sex);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.Sex, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.FromServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.FromServiceUnitName
		/// </summary>
		virtual public System.String FromServiceUnitName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.FromServiceUnitName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.FromServiceUnitName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.ClassName
		/// </summary>
		virtual public System.String ClassName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.ClassName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.ClassName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.CityOfBirth
		/// </summary>
		virtual public System.String CityOfBirth
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.CityOfBirth);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.CityOfBirth, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.DateOfBirth
		/// </summary>
		virtual public System.DateTime? DateOfBirth
		{
			get
			{
				return base.GetSystemDateTime(LabTestOrderMetadata.ColumnNames.DateOfBirth);
			}
			
			set
			{
				base.SetSystemDateTime(LabTestOrderMetadata.ColumnNames.DateOfBirth, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.ParamedicName
		/// </summary>
		virtual public System.String ParamedicName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.ParamedicName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.ParamedicName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.StreetName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.StreetName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.District);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.District, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.City);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.City, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.County);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.County, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.State);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.State, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.ZipCode, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.PhoneNo);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.PhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.FaxNo);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.FaxNo, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.MobilePhoneNo);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.Company
		/// </summary>
		virtual public System.String Company
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.Company);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.Company, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.GuarantorName
		/// </summary>
		virtual public System.String GuarantorName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.GuarantorName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.GuarantorName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.TestOrderID
		/// </summary>
		virtual public System.String TestOrderID
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.TestOrderID);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.TestOrderID, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.TestOrderName
		/// </summary>
		virtual public System.String TestOrderName
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.TestOrderName);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.TestOrderName, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LabTestOrderMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LabTestOrderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LabTestOrderMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LabTestOrderMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to LabTestOrder.IsConfirm
		/// </summary>
		virtual public System.Boolean? IsConfirm
		{
			get
			{
				return base.GetSystemBoolean(LabTestOrderMetadata.ColumnNames.IsConfirm);
			}
			
			set
			{
				base.SetSystemBoolean(LabTestOrderMetadata.ColumnNames.IsConfirm, value);
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
			public esStrings(esLabTestOrder entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
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
				
			public System.String MedicalNo
			{
				get
				{
					System.String data = entity.MedicalNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalNo = null;
					else entity.MedicalNo = Convert.ToString(value);
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
				
			public System.String Sex
			{
				get
				{
					System.String data = entity.Sex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sex = null;
					else entity.Sex = Convert.ToString(value);
				}
			}
				
			public System.String FromServiceUnitID
			{
				get
				{
					System.String data = entity.FromServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
					else entity.FromServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String FromServiceUnitName
			{
				get
				{
					System.String data = entity.FromServiceUnitName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitName = null;
					else entity.FromServiceUnitName = Convert.ToString(value);
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
				
			public System.String CityOfBirth
			{
				get
				{
					System.String data = entity.CityOfBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CityOfBirth = null;
					else entity.CityOfBirth = Convert.ToString(value);
				}
			}
				
			public System.String DateOfBirth
			{
				get
				{
					System.DateTime? data = entity.DateOfBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateOfBirth = null;
					else entity.DateOfBirth = Convert.ToDateTime(value);
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
				
			public System.String ParamedicName
			{
				get
				{
					System.String data = entity.ParamedicName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicName = null;
					else entity.ParamedicName = Convert.ToString(value);
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
				
			public System.String Company
			{
				get
				{
					System.String data = entity.Company;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Company = null;
					else entity.Company = Convert.ToString(value);
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
				
			public System.String TestOrderID
			{
				get
				{
					System.String data = entity.TestOrderID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestOrderID = null;
					else entity.TestOrderID = Convert.ToString(value);
				}
			}
				
			public System.String TestOrderName
			{
				get
				{
					System.String data = entity.TestOrderName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestOrderName = null;
					else entity.TestOrderName = Convert.ToString(value);
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
				
			public System.String IsConfirm
			{
				get
				{
					System.Boolean? data = entity.IsConfirm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConfirm = null;
					else entity.IsConfirm = Convert.ToBoolean(value);
				}
			}
			

			private esLabTestOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLabTestOrderQuery query)
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
				throw new Exception("esLabTestOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LabTestOrder : esLabTestOrder
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
	abstract public class esLabTestOrderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LabTestOrderMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MedicalNo
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.MedicalNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FirstName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.FirstName, esSystemType.String);
			}
		} 
		
		public esQueryItem MiddleName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.MiddleName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.LastName, esSystemType.String);
			}
		} 
		
		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.Sex, esSystemType.String);
			}
		} 
		
		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem FromServiceUnitName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.FromServiceUnitName, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.ClassName, esSystemType.String);
			}
		} 
		
		public esQueryItem CityOfBirth
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.CityOfBirth, esSystemType.String);
			}
		} 
		
		public esQueryItem DateOfBirth
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.ParamedicName, esSystemType.String);
			}
		} 
		
		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		} 
		
		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.District, esSystemType.String);
			}
		} 
		
		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.City, esSystemType.String);
			}
		} 
		
		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.County, esSystemType.String);
			}
		} 
		
		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.State, esSystemType.String);
			}
		} 
		
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
		
		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Company
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.Company, esSystemType.String);
			}
		} 
		
		public esQueryItem GuarantorName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.GuarantorName, esSystemType.String);
			}
		} 
		
		public esQueryItem TestOrderID
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.TestOrderID, esSystemType.String);
			}
		} 
		
		public esQueryItem TestOrderName
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.TestOrderName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsConfirm
		{
			get
			{
				return new esQueryItem(this, LabTestOrderMetadata.ColumnNames.IsConfirm, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LabTestOrderCollection")]
	public partial class LabTestOrderCollection : esLabTestOrderCollection, IEnumerable<LabTestOrder>
	{
		public LabTestOrderCollection()
		{

		}
		
		public static implicit operator List<LabTestOrder>(LabTestOrderCollection coll)
		{
			List<LabTestOrder> list = new List<LabTestOrder>();
			
			foreach (LabTestOrder emp in coll)
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
				return  LabTestOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabTestOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LabTestOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LabTestOrder();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LabTestOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabTestOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LabTestOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LabTestOrder AddNew()
		{
			LabTestOrder entity = base.AddNewEntity() as LabTestOrder;
			
			return entity;
		}

		public LabTestOrder FindByPrimaryKey(System.String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as LabTestOrder;
		}


		#region IEnumerable<LabTestOrder> Members

		IEnumerator<LabTestOrder> IEnumerable<LabTestOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LabTestOrder;
			}
		}

		#endregion
		
		private LabTestOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LabTestOrder' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LabTestOrder ({TransactionNo})")]
	[Serializable]
	public partial class LabTestOrder : esLabTestOrder
	{
		public LabTestOrder()
		{

		}
	
		public LabTestOrder(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LabTestOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esLabTestOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabTestOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LabTestOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabTestOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LabTestOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LabTestOrderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LabTestOrderQuery : esLabTestOrderQuery
	{
		public LabTestOrderQuery()
		{

		}		
		
		public LabTestOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LabTestOrderQuery";
        }
		
			
	}


	[Serializable]
	public partial class LabTestOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LabTestOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.TransactionDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.MedicalNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.MedicalNo;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.FirstName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.MiddleName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.LastName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.Sex, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.FromServiceUnitID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.FromServiceUnitName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.FromServiceUnitName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.ClassID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.ClassName, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.ClassName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.CityOfBirth, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.CityOfBirth;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.DateOfBirth, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.DateOfBirth;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.ParamedicID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.ParamedicName, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.ParamedicName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.StreetName, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.District, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.City, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.County, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.State, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.ZipCode, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.PhoneNo, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.FaxNo, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.Email, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.MobilePhoneNo, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.Company, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.Company;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.GuarantorName, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.GuarantorName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.TestOrderID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.TestOrderID;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.TestOrderName, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.TestOrderName;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.LastUpdateDateTime, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.LastUpdateByUserID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestOrderMetadata.ColumnNames.IsConfirm, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LabTestOrderMetadata.PropertyNames.IsConfirm;
			c.HasDefault = true;
			c.Default = @"('0')";
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LabTestOrderMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string MedicalNo = "MedicalNo";
			 public const string FirstName = "FirstName";
			 public const string MiddleName = "MiddleName";
			 public const string LastName = "LastName";
			 public const string Sex = "Sex";
			 public const string FromServiceUnitID = "FromServiceUnitID";
			 public const string FromServiceUnitName = "FromServiceUnitName";
			 public const string ClassID = "ClassID";
			 public const string ClassName = "ClassName";
			 public const string CityOfBirth = "CityOfBirth";
			 public const string DateOfBirth = "DateOfBirth";
			 public const string ParamedicID = "ParamedicID";
			 public const string ParamedicName = "ParamedicName";
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
			 public const string Company = "Company";
			 public const string GuarantorName = "GuarantorName";
			 public const string TestOrderID = "TestOrderID";
			 public const string TestOrderName = "TestOrderName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsConfirm = "IsConfirm";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string MedicalNo = "MedicalNo";
			 public const string FirstName = "FirstName";
			 public const string MiddleName = "MiddleName";
			 public const string LastName = "LastName";
			 public const string Sex = "Sex";
			 public const string FromServiceUnitID = "FromServiceUnitID";
			 public const string FromServiceUnitName = "FromServiceUnitName";
			 public const string ClassID = "ClassID";
			 public const string ClassName = "ClassName";
			 public const string CityOfBirth = "CityOfBirth";
			 public const string DateOfBirth = "DateOfBirth";
			 public const string ParamedicID = "ParamedicID";
			 public const string ParamedicName = "ParamedicName";
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
			 public const string Company = "Company";
			 public const string GuarantorName = "GuarantorName";
			 public const string TestOrderID = "TestOrderID";
			 public const string TestOrderName = "TestOrderName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsConfirm = "IsConfirm";
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
			lock (typeof(LabTestOrderMetadata))
			{
				if(LabTestOrderMetadata.mapDelegates == null)
				{
					LabTestOrderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LabTestOrderMetadata.meta == null)
				{
					LabTestOrderMetadata.meta = new LabTestOrderMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MedicalNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromServiceUnitName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CityOfBirth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateOfBirth", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicName", new esTypeMap("varchar", "System.String"));
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
				meta.AddTypeMap("Company", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestOrderID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestOrderName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsConfirm", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "LabTestOrder";
				meta.Destination = "LabTestOrder";
				
				meta.spInsert = "proc_LabTestOrderInsert";				
				meta.spUpdate = "proc_LabTestOrderUpdate";		
				meta.spDelete = "proc_LabTestOrderDelete";
				meta.spLoadAll = "proc_LabTestOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_LabTestOrderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LabTestOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
