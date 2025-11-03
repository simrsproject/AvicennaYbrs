/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:14 PM
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
	abstract public class esEmployeeCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeQuery query)
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
			this.InitQuery(query as esEmployeeQuery);
		}
		#endregion
		
		virtual public Employee DetachEntity(Employee entity)
		{
			return base.DetachEntity(entity) as Employee;
		}
		
		virtual public Employee AttachEntity(Employee entity)
		{
			return base.AttachEntity(entity) as Employee;
		}
		
		virtual public void Combine(EmployeeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Employee this[int index]
		{
			get
			{
				return base[index] as Employee;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Employee);
		}
	}



	[Serializable]
	abstract public class esEmployee : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployee()
		{

		}

		public esEmployee(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String employeeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String employeeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String employeeID)
		{
			esEmployeeQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeID == employeeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String employeeID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeID",employeeID);
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
						case "EmployeeID": this.str.EmployeeID = (string)value; break;							
						case "EmployeeName": this.str.EmployeeName = (string)value; break;							
						case "ShortName": this.str.ShortName = (string)value; break;							
						case "RegistrationDate": this.str.RegistrationDate = (string)value; break;							
						case "SREmployeeStatus": this.str.SREmployeeStatus = (string)value; break;							
						case "SRGenderType": this.str.SRGenderType = (string)value; break;							
						case "DepartmentID": this.str.DepartmentID = (string)value; break;							
						case "JobTitle": this.str.JobTitle = (string)value; break;							
						case "PermanentDate": this.str.PermanentDate = (string)value; break;							
						case "ContractNumber": this.str.ContractNumber = (string)value; break;							
						case "ContractStartDate": this.str.ContractStartDate = (string)value; break;							
						case "ContractEndDate": this.str.ContractEndDate = (string)value; break;							
						case "TaxRegistrationNo": this.str.TaxRegistrationNo = (string)value; break;							
						case "JamsostekNo": this.str.JamsostekNo = (string)value; break;							
						case "StreetName": this.str.StreetName = (string)value; break;							
						case "City": this.str.City = (string)value; break;							
						case "Country": this.str.Country = (string)value; break;							
						case "ZipCode": this.str.ZipCode = (string)value; break;							
						case "PhoneNo": this.str.PhoneNo = (string)value; break;							
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;							
						case "Email": this.str.Email = (string)value; break;							
						case "BornDate": this.str.BornDate = (string)value; break;							
						case "BornPlace": this.str.BornPlace = (string)value; break;							
						case "EmployeeNotes": this.str.EmployeeNotes = (string)value; break;							
						case "ResignDate": this.str.ResignDate = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RegistrationDate":
						
							if (value == null || value is System.DateTime)
								this.RegistrationDate = (System.DateTime?)value;
							break;
						
						case "PermanentDate":
						
							if (value == null || value is System.DateTime)
								this.PermanentDate = (System.DateTime?)value;
							break;
						
						case "ContractStartDate":
						
							if (value == null || value is System.DateTime)
								this.ContractStartDate = (System.DateTime?)value;
							break;
						
						case "ContractEndDate":
						
							if (value == null || value is System.DateTime)
								this.ContractEndDate = (System.DateTime?)value;
							break;
						
						case "BornDate":
						
							if (value == null || value is System.DateTime)
								this.BornDate = (System.DateTime?)value;
							break;
						
						case "ResignDate":
						
							if (value == null || value is System.DateTime)
								this.ResignDate = (System.DateTime?)value;
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
		/// Maps to Employee.EmployeeID
		/// </summary>
		virtual public System.String EmployeeID
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.EmployeeID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.EmployeeID, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.EmployeeName
		/// </summary>
		virtual public System.String EmployeeName
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.EmployeeName);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.EmployeeName, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.ShortName);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.ShortName, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.RegistrationDate
		/// </summary>
		virtual public System.DateTime? RegistrationDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMetadata.ColumnNames.RegistrationDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMetadata.ColumnNames.RegistrationDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.SREmployeeStatus);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.SRGenderType, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.DepartmentID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.DepartmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.JobTitle
		/// </summary>
		virtual public System.String JobTitle
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.JobTitle);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.JobTitle, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.PermanentDate
		/// </summary>
		virtual public System.DateTime? PermanentDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMetadata.ColumnNames.PermanentDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMetadata.ColumnNames.PermanentDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.ContractNumber
		/// </summary>
		virtual public System.String ContractNumber
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.ContractNumber);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.ContractNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.ContractStartDate
		/// </summary>
		virtual public System.DateTime? ContractStartDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMetadata.ColumnNames.ContractStartDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMetadata.ColumnNames.ContractStartDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.ContractEndDate
		/// </summary>
		virtual public System.DateTime? ContractEndDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMetadata.ColumnNames.ContractEndDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMetadata.ColumnNames.ContractEndDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.TaxRegistrationNo
		/// </summary>
		virtual public System.String TaxRegistrationNo
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.TaxRegistrationNo);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.TaxRegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.JamsostekNo
		/// </summary>
		virtual public System.String JamsostekNo
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.JamsostekNo);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.JamsostekNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.StreetName);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.StreetName, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.City);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.City, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.Country
		/// </summary>
		virtual public System.String Country
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.Country);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.Country, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.ZipCode, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.PhoneNo);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.PhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.MobilePhoneNo);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.BornDate
		/// </summary>
		virtual public System.DateTime? BornDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMetadata.ColumnNames.BornDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMetadata.ColumnNames.BornDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.BornPlace
		/// </summary>
		virtual public System.String BornPlace
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.BornPlace);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.BornPlace, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.EmployeeNotes
		/// </summary>
		virtual public System.String EmployeeNotes
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.EmployeeNotes);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.EmployeeNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.ResignDate
		/// </summary>
		virtual public System.DateTime? ResignDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMetadata.ColumnNames.ResignDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMetadata.ColumnNames.ResignDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EmployeeMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Employee.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployee entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeID
			{
				get
				{
					System.String data = entity.EmployeeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeID = null;
					else entity.EmployeeID = Convert.ToString(value);
				}
			}
				
			public System.String EmployeeName
			{
				get
				{
					System.String data = entity.EmployeeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeName = null;
					else entity.EmployeeName = Convert.ToString(value);
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
				
			public System.String RegistrationDate
			{
				get
				{
					System.DateTime? data = entity.RegistrationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationDate = null;
					else entity.RegistrationDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String SREmployeeStatus
			{
				get
				{
					System.String data = entity.SREmployeeStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeStatus = null;
					else entity.SREmployeeStatus = Convert.ToString(value);
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
				
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
				}
			}
				
			public System.String JobTitle
			{
				get
				{
					System.String data = entity.JobTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobTitle = null;
					else entity.JobTitle = Convert.ToString(value);
				}
			}
				
			public System.String PermanentDate
			{
				get
				{
					System.DateTime? data = entity.PermanentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermanentDate = null;
					else entity.PermanentDate = Convert.ToDateTime(value);
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
				
			public System.String ContractStartDate
			{
				get
				{
					System.DateTime? data = entity.ContractStartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractStartDate = null;
					else entity.ContractStartDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ContractEndDate
			{
				get
				{
					System.DateTime? data = entity.ContractEndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContractEndDate = null;
					else entity.ContractEndDate = Convert.ToDateTime(value);
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
				
			public System.String JamsostekNo
			{
				get
				{
					System.String data = entity.JamsostekNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JamsostekNo = null;
					else entity.JamsostekNo = Convert.ToString(value);
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
				
			public System.String Country
			{
				get
				{
					System.String data = entity.Country;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Country = null;
					else entity.Country = Convert.ToString(value);
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
				
			public System.String BornDate
			{
				get
				{
					System.DateTime? data = entity.BornDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BornDate = null;
					else entity.BornDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String BornPlace
			{
				get
				{
					System.String data = entity.BornPlace;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BornPlace = null;
					else entity.BornPlace = Convert.ToString(value);
				}
			}
				
			public System.String EmployeeNotes
			{
				get
				{
					System.String data = entity.EmployeeNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeNotes = null;
					else entity.EmployeeNotes = Convert.ToString(value);
				}
			}
				
			public System.String ResignDate
			{
				get
				{
					System.DateTime? data = entity.ResignDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResignDate = null;
					else entity.ResignDate = Convert.ToDateTime(value);
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
			

			private esEmployee entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeQuery query)
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
				throw new Exception("esEmployee can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Employee : esEmployee
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
	abstract public class esEmployeeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeID
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.EmployeeID, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeName
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.EmployeeName, esSystemType.String);
			}
		} 
		
		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.RegistrationDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
		
		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem JobTitle
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.JobTitle, esSystemType.String);
			}
		} 
		
		public esQueryItem PermanentDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.PermanentDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ContractNumber
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.ContractNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem ContractStartDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.ContractStartDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ContractEndDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.ContractEndDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TaxRegistrationNo
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.TaxRegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem JamsostekNo
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.JamsostekNo, esSystemType.String);
			}
		} 
		
		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		} 
		
		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.City, esSystemType.String);
			}
		} 
		
		public esQueryItem Country
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.Country, esSystemType.String);
			}
		} 
		
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
		
		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem BornDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.BornDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem BornPlace
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.BornPlace, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeNotes
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.EmployeeNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem ResignDate
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.ResignDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeCollection")]
	public partial class EmployeeCollection : esEmployeeCollection, IEnumerable<Employee>
	{
		public EmployeeCollection()
		{

		}
		
		public static implicit operator List<Employee>(EmployeeCollection coll)
		{
			List<Employee> list = new List<Employee>();
			
			foreach (Employee emp in coll)
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
				return  EmployeeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Employee(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Employee();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Employee AddNew()
		{
			Employee entity = base.AddNewEntity() as Employee;
			
			return entity;
		}

		public Employee FindByPrimaryKey(System.String employeeID)
		{
			return base.FindByPrimaryKey(employeeID) as Employee;
		}


		#region IEnumerable<Employee> Members

		IEnumerator<Employee> IEnumerable<Employee>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Employee;
			}
		}

		#endregion
		
		private EmployeeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Employee' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Employee ({EmployeeID})")]
	[Serializable]
	public partial class Employee : esEmployee
	{
		public Employee()
		{

		}
	
		public Employee(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeQuery : esEmployeeQuery
	{
		public EmployeeQuery()
		{

		}		
		
		public EmployeeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.EmployeeID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.EmployeeID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.EmployeeName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.EmployeeName;
			c.CharacterMaxLength = 60;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.RegistrationDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMetadata.PropertyNames.RegistrationDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.SREmployeeStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.SRGenderType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.DepartmentID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.JobTitle, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.JobTitle;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.PermanentDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMetadata.PropertyNames.PermanentDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.ContractNumber, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.ContractNumber;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.ContractStartDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMetadata.PropertyNames.ContractStartDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.ContractEndDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMetadata.PropertyNames.ContractEndDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.TaxRegistrationNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.TaxRegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.JamsostekNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.JamsostekNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.StreetName, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.City, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.City;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.Country, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.Country;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.ZipCode, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.PhoneNo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.MobilePhoneNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.Email, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.BornDate, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMetadata.PropertyNames.BornDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.BornPlace, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.BornPlace;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.EmployeeNotes, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.EmployeeNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.ResignDate, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMetadata.PropertyNames.ResignDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.IsActive, 25, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.LastUpdateDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMetadata.ColumnNames.LastUpdateByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeMetadata Meta()
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
			 public const string EmployeeID = "EmployeeID";
			 public const string EmployeeName = "EmployeeName";
			 public const string ShortName = "ShortName";
			 public const string RegistrationDate = "RegistrationDate";
			 public const string SREmployeeStatus = "SREmployeeStatus";
			 public const string SRGenderType = "SRGenderType";
			 public const string DepartmentID = "DepartmentID";
			 public const string JobTitle = "JobTitle";
			 public const string PermanentDate = "PermanentDate";
			 public const string ContractNumber = "ContractNumber";
			 public const string ContractStartDate = "ContractStartDate";
			 public const string ContractEndDate = "ContractEndDate";
			 public const string TaxRegistrationNo = "TaxRegistrationNo";
			 public const string JamsostekNo = "JamsostekNo";
			 public const string StreetName = "StreetName";
			 public const string City = "City";
			 public const string Country = "Country";
			 public const string ZipCode = "ZipCode";
			 public const string PhoneNo = "PhoneNo";
			 public const string MobilePhoneNo = "MobilePhoneNo";
			 public const string Email = "Email";
			 public const string BornDate = "BornDate";
			 public const string BornPlace = "BornPlace";
			 public const string EmployeeNotes = "EmployeeNotes";
			 public const string ResignDate = "ResignDate";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeID = "EmployeeID";
			 public const string EmployeeName = "EmployeeName";
			 public const string ShortName = "ShortName";
			 public const string RegistrationDate = "RegistrationDate";
			 public const string SREmployeeStatus = "SREmployeeStatus";
			 public const string SRGenderType = "SRGenderType";
			 public const string DepartmentID = "DepartmentID";
			 public const string JobTitle = "JobTitle";
			 public const string PermanentDate = "PermanentDate";
			 public const string ContractNumber = "ContractNumber";
			 public const string ContractStartDate = "ContractStartDate";
			 public const string ContractEndDate = "ContractEndDate";
			 public const string TaxRegistrationNo = "TaxRegistrationNo";
			 public const string JamsostekNo = "JamsostekNo";
			 public const string StreetName = "StreetName";
			 public const string City = "City";
			 public const string Country = "Country";
			 public const string ZipCode = "ZipCode";
			 public const string PhoneNo = "PhoneNo";
			 public const string MobilePhoneNo = "MobilePhoneNo";
			 public const string Email = "Email";
			 public const string BornDate = "BornDate";
			 public const string BornPlace = "BornPlace";
			 public const string EmployeeNotes = "EmployeeNotes";
			 public const string ResignDate = "ResignDate";
			 public const string IsActive = "IsActive";
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
			lock (typeof(EmployeeMetadata))
			{
				if(EmployeeMetadata.mapDelegates == null)
				{
					EmployeeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeMetadata.meta == null)
				{
					EmployeeMetadata.meta = new EmployeeMetadata();
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
				

				meta.AddTypeMap("EmployeeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SREmployeeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JobTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PermanentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ContractNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContractStartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ContractEndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TaxRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JamsostekNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Country", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BornDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BornPlace", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResignDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Employee";
				meta.Destination = "Employee";
				
				meta.spInsert = "proc_EmployeeInsert";				
				meta.spUpdate = "proc_EmployeeUpdate";		
				meta.spDelete = "proc_EmployeeDelete";
				meta.spLoadAll = "proc_EmployeeLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
