/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/8/2021 9:40:26 PM
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
	abstract public class esCustomerCollection : esEntityCollectionWAuditLog
	{
		public esCustomerCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "CustomerCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esCustomerQuery query)
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
			this.InitQuery(query as esCustomerQuery);
		}
		#endregion
			
		virtual public Customer DetachEntity(Customer entity)
		{
			return base.DetachEntity(entity) as Customer;
		}
		
		virtual public Customer AttachEntity(Customer entity)
		{
			return base.AttachEntity(entity) as Customer;
		}
		
		virtual public void Combine(CustomerCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Customer this[int index]
		{
			get
			{
				return base[index] as Customer;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Customer);
		}
	}

	[Serializable]
	abstract public class esCustomer : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCustomerQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esCustomer()
		{
		}
	
		public esCustomer(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String customerID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(customerID);
			else
				return LoadByPrimaryKeyStoredProcedure(customerID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String customerID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(customerID);
			else
				return LoadByPrimaryKeyStoredProcedure(customerID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String customerID)
		{
			esCustomerQuery query = this.GetDynamicQuery();
			query.Where(query.CustomerID == customerID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String customerID)
		{
			esParameters parms = new esParameters();
			parms.Add("CustomerID",customerID);
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
						case "CustomerID": this.str.CustomerID = (string)value; break;
						case "CustomerName": this.str.CustomerName = (string)value; break;
						case "ContactPerson": this.str.ContactPerson = (string)value; break;
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
						case "SalesMarginPercentage": this.str.SalesMarginPercentage = (string)value; break;
						case "IsUseTax": this.str.IsUseTax = (string)value; break;
						case "ChartOfAccountIdAR": this.str.ChartOfAccountIdAR = (string)value; break;
						case "SubledgerIdAR": this.str.SubledgerIdAR = (string)value; break;
						case "ChartOfAccountIdARInProcess": this.str.ChartOfAccountIdARInProcess = (string)value; break;
						case "SubledgerIdARInProcess": this.str.SubledgerIdARInProcess = (string)value; break;
						case "ChartOfAccountIdARTemporary": this.str.ChartOfAccountIdARTemporary = (string)value; break;
						case "SubledgerIdARTemporary": this.str.SubledgerIdARTemporary = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SalesMarginPercentage":
						
							if (value == null || value is System.Decimal)
								this.SalesMarginPercentage = (System.Decimal?)value;
							break;
						case "IsUseTax":
						
							if (value == null || value is System.Boolean)
								this.IsUseTax = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdAR":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdAR = (System.Int32?)value;
							break;
						case "SubledgerIdAR":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdAR = (System.Int32?)value;
							break;
						case "ChartOfAccountIdARInProcess":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdARInProcess = (System.Int32?)value;
							break;
						case "SubledgerIdARInProcess":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdARInProcess = (System.Int32?)value;
							break;
						case "ChartOfAccountIdARTemporary":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdARTemporary = (System.Int32?)value;
							break;
						case "SubledgerIdARTemporary":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdARTemporary = (System.Int32?)value;
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
		/// Maps to Customer.CustomerID
		/// </summary>
		virtual public System.String CustomerID
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.CustomerID);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.CustomerID, value);
			}
		}
		/// <summary>
		/// Maps to Customer.CustomerName
		/// </summary>
		virtual public System.String CustomerName
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.CustomerName);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.CustomerName, value);
			}
		}
		/// <summary>
		/// Maps to Customer.ContactPerson
		/// </summary>
		virtual public System.String ContactPerson
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.ContactPerson);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.ContactPerson, value);
			}
		}
		/// <summary>
		/// Maps to Customer.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CustomerMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(CustomerMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Customer.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.StreetName);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.StreetName, value);
			}
		}
		/// <summary>
		/// Maps to Customer.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.District);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to Customer.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.City);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to Customer.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.County);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to Customer.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.State);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.State, value);
			}
		}
		/// <summary>
		/// Maps to Customer.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.ZipCode);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to Customer.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.PhoneNo);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Customer.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.FaxNo);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.FaxNo, value);
			}
		}
		/// <summary>
		/// Maps to Customer.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to Customer.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.MobilePhoneNo);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Customer.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CustomerMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CustomerMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Customer.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CustomerMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CustomerMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Customer.SalesMarginPercentage
		/// </summary>
		virtual public System.Decimal? SalesMarginPercentage
		{
			get
			{
				return base.GetSystemDecimal(CustomerMetadata.ColumnNames.SalesMarginPercentage);
			}
			
			set
			{
				base.SetSystemDecimal(CustomerMetadata.ColumnNames.SalesMarginPercentage, value);
			}
		}
		/// <summary>
		/// Maps to Customer.IsUseTax
		/// </summary>
		virtual public System.Boolean? IsUseTax
		{
			get
			{
				return base.GetSystemBoolean(CustomerMetadata.ColumnNames.IsUseTax);
			}
			
			set
			{
				base.SetSystemBoolean(CustomerMetadata.ColumnNames.IsUseTax, value);
			}
		}
		/// <summary>
		/// Maps to Customer.ChartOfAccountIdAR
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAR
		{
			get
			{
				return base.GetSystemInt32(CustomerMetadata.ColumnNames.ChartOfAccountIdAR);
			}
			
			set
			{
				base.SetSystemInt32(CustomerMetadata.ColumnNames.ChartOfAccountIdAR, value);
			}
		}
		/// <summary>
		/// Maps to Customer.SubledgerIdAR
		/// </summary>
		virtual public System.Int32? SubledgerIdAR
		{
			get
			{
				return base.GetSystemInt32(CustomerMetadata.ColumnNames.SubledgerIdAR);
			}
			
			set
			{
				base.SetSystemInt32(CustomerMetadata.ColumnNames.SubledgerIdAR, value);
			}
		}
		/// <summary>
		/// Maps to Customer.ChartOfAccountIdARInProcess
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdARInProcess
		{
			get
			{
				return base.GetSystemInt32(CustomerMetadata.ColumnNames.ChartOfAccountIdARInProcess);
			}
			
			set
			{
				base.SetSystemInt32(CustomerMetadata.ColumnNames.ChartOfAccountIdARInProcess, value);
			}
		}
		/// <summary>
		/// Maps to Customer.SubledgerIdARInProcess
		/// </summary>
		virtual public System.Int32? SubledgerIdARInProcess
		{
			get
			{
				return base.GetSystemInt32(CustomerMetadata.ColumnNames.SubledgerIdARInProcess);
			}
			
			set
			{
				base.SetSystemInt32(CustomerMetadata.ColumnNames.SubledgerIdARInProcess, value);
			}
		}
		/// <summary>
		/// Maps to Customer.ChartOfAccountIdARTemporary
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdARTemporary
		{
			get
			{
				return base.GetSystemInt32(CustomerMetadata.ColumnNames.ChartOfAccountIdARTemporary);
			}
			
			set
			{
				base.SetSystemInt32(CustomerMetadata.ColumnNames.ChartOfAccountIdARTemporary, value);
			}
		}
		/// <summary>
		/// Maps to Customer.SubledgerIdARTemporary
		/// </summary>
		virtual public System.Int32? SubledgerIdARTemporary
		{
			get
			{
				return base.GetSystemInt32(CustomerMetadata.ColumnNames.SubledgerIdARTemporary);
			}
			
			set
			{
				base.SetSystemInt32(CustomerMetadata.ColumnNames.SubledgerIdARTemporary, value);
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
			public esStrings(esCustomer entity)
			{
				this.entity = entity;
			}
			public System.String CustomerID
			{
				get
				{
					System.String data = entity.CustomerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerID = null;
					else entity.CustomerID = Convert.ToString(value);
				}
			}
			public System.String CustomerName
			{
				get
				{
					System.String data = entity.CustomerName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerName = null;
					else entity.CustomerName = Convert.ToString(value);
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
			public System.String SalesMarginPercentage
			{
				get
				{
					System.Decimal? data = entity.SalesMarginPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalesMarginPercentage = null;
					else entity.SalesMarginPercentage = Convert.ToDecimal(value);
				}
			}
			public System.String IsUseTax
			{
				get
				{
					System.Boolean? data = entity.IsUseTax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUseTax = null;
					else entity.IsUseTax = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountIdAR
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdAR = null;
					else entity.ChartOfAccountIdAR = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdAR
			{
				get
				{
					System.Int32? data = entity.SubledgerIdAR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdAR = null;
					else entity.SubledgerIdAR = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdARInProcess
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdARInProcess;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdARInProcess = null;
					else entity.ChartOfAccountIdARInProcess = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdARInProcess
			{
				get
				{
					System.Int32? data = entity.SubledgerIdARInProcess;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdARInProcess = null;
					else entity.SubledgerIdARInProcess = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdARTemporary
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdARTemporary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdARTemporary = null;
					else entity.ChartOfAccountIdARTemporary = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdARTemporary
			{
				get
				{
					System.Int32? data = entity.SubledgerIdARTemporary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdARTemporary = null;
					else entity.SubledgerIdARTemporary = Convert.ToInt32(value);
				}
			}
			private esCustomer entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCustomerQuery query)
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
				throw new Exception("esCustomer can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Customer : esCustomer
	{	
	}

	[Serializable]
	abstract public class esCustomerQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return CustomerMetadata.Meta();
			}
		}	
			
		public esQueryItem CustomerID
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.CustomerID, esSystemType.String);
			}
		} 
			
		public esQueryItem CustomerName
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.CustomerName, esSystemType.String);
			}
		} 
			
		public esQueryItem ContactPerson
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.ContactPerson, esSystemType.String);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		} 
			
		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.District, esSystemType.String);
			}
		} 
			
		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.City, esSystemType.String);
			}
		} 
			
		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.County, esSystemType.String);
			}
		} 
			
		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.State, esSystemType.String);
			}
		} 
			
		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		} 
			
		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		} 
			
		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
			
		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem SalesMarginPercentage
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.SalesMarginPercentage, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsUseTax
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.IsUseTax, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAR
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.ChartOfAccountIdAR, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAR
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.SubledgerIdAR, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdARInProcess
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.ChartOfAccountIdARInProcess, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdARInProcess
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.SubledgerIdARInProcess, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdARTemporary
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.ChartOfAccountIdARTemporary, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdARTemporary
		{
			get
			{
				return new esQueryItem(this, CustomerMetadata.ColumnNames.SubledgerIdARTemporary, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CustomerCollection")]
	public partial class CustomerCollection : esCustomerCollection, IEnumerable< Customer>
	{
		public CustomerCollection()
		{

		}	
		
		public static implicit operator List< Customer>(CustomerCollection coll)
		{
			List< Customer> list = new List< Customer>();
			
			foreach (Customer emp in coll)
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
				return  CustomerMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CustomerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Customer(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Customer();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public CustomerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CustomerQuery();
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
		public bool Load(CustomerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Customer AddNew()
		{
			Customer entity = base.AddNewEntity() as Customer;
			
			return entity;		
		}
		public Customer FindByPrimaryKey(String customerID)
		{
			return base.FindByPrimaryKey(customerID) as Customer;
		}

		#region IEnumerable< Customer> Members

		IEnumerator< Customer> IEnumerable< Customer>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Customer;
			}
		}

		#endregion
		
		private CustomerQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Customer' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Customer ({CustomerID})")]
	[Serializable]
	public partial class Customer : esCustomer
	{
		public Customer()
		{
		}	
	
		public Customer(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CustomerMetadata.Meta();
			}
		}	
	
		override protected esCustomerQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CustomerQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public CustomerQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CustomerQuery();
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
		public bool Load(CustomerQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private CustomerQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CustomerQuery : esCustomerQuery
	{
		public CustomerQuery()
		{

		}		
		
		public CustomerQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "CustomerQuery";
        }
	}

	[Serializable]
	public partial class CustomerMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CustomerMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.CustomerID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.CustomerID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.CustomerName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.CustomerName;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.ContactPerson, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.ContactPerson;
			c.CharacterMaxLength = 100;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CustomerMetadata.PropertyNames.IsActive;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.StreetName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.District, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.City, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.County, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.State, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.ZipCode, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.PhoneNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.FaxNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.Email, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.MobilePhoneNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CustomerMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = CustomerMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.SalesMarginPercentage, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CustomerMetadata.PropertyNames.SalesMarginPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.IsUseTax, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CustomerMetadata.PropertyNames.IsUseTax;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.ChartOfAccountIdAR, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CustomerMetadata.PropertyNames.ChartOfAccountIdAR;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.SubledgerIdAR, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CustomerMetadata.PropertyNames.SubledgerIdAR;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.ChartOfAccountIdARInProcess, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CustomerMetadata.PropertyNames.ChartOfAccountIdARInProcess;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.SubledgerIdARInProcess, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CustomerMetadata.PropertyNames.SubledgerIdARInProcess;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.ChartOfAccountIdARTemporary, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CustomerMetadata.PropertyNames.ChartOfAccountIdARTemporary;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CustomerMetadata.ColumnNames.SubledgerIdARTemporary, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CustomerMetadata.PropertyNames.SubledgerIdARTemporary;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public CustomerMetadata Meta()
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
			public const string CustomerID = "CustomerID";
			public const string CustomerName = "CustomerName";
			public const string ContactPerson = "ContactPerson";
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
			public const string SalesMarginPercentage = "SalesMarginPercentage";
			public const string IsUseTax = "IsUseTax";
			public const string ChartOfAccountIdAR = "ChartOfAccountIdAR";
			public const string SubledgerIdAR = "SubledgerIdAR";
			public const string ChartOfAccountIdARInProcess = "ChartOfAccountIdARInProcess";
			public const string SubledgerIdARInProcess = "SubledgerIdARInProcess";
			public const string ChartOfAccountIdARTemporary = "ChartOfAccountIdARTemporary";
			public const string SubledgerIdARTemporary = "SubledgerIdARTemporary";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string CustomerID = "CustomerID";
			public const string CustomerName = "CustomerName";
			public const string ContactPerson = "ContactPerson";
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
			public const string SalesMarginPercentage = "SalesMarginPercentage";
			public const string IsUseTax = "IsUseTax";
			public const string ChartOfAccountIdAR = "ChartOfAccountIdAR";
			public const string SubledgerIdAR = "SubledgerIdAR";
			public const string ChartOfAccountIdARInProcess = "ChartOfAccountIdARInProcess";
			public const string SubledgerIdARInProcess = "SubledgerIdARInProcess";
			public const string ChartOfAccountIdARTemporary = "ChartOfAccountIdARTemporary";
			public const string SubledgerIdARTemporary = "SubledgerIdARTemporary";
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
			lock (typeof(CustomerMetadata))
			{
				if(CustomerMetadata.mapDelegates == null)
				{
					CustomerMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CustomerMetadata.meta == null)
				{
					CustomerMetadata.meta = new CustomerMetadata();
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
				
				meta.AddTypeMap("CustomerID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CustomerName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContactPerson", new esTypeMap("varchar", "System.String"));
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
				meta.AddTypeMap("SalesMarginPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsUseTax", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountIdAR", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAR", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdARInProcess", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdARInProcess", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdARTemporary", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdARTemporary", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "Customer";
				meta.Destination = "Customer";
				meta.spInsert = "proc_CustomerInsert";				
				meta.spUpdate = "proc_CustomerUpdate";		
				meta.spDelete = "proc_CustomerDelete";
				meta.spLoadAll = "proc_CustomerLoadAll";
				meta.spLoadByPrimaryKey = "proc_CustomerLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CustomerMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
