/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/18/2022 2:52:39 PM
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
	abstract public class esHealthcareCollection : esEntityCollectionWAuditLog
	{
		public esHealthcareCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "HealthcareCollection";
		}

		#region Query Logic
		protected void InitQuery(esHealthcareQuery query)
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
			this.InitQuery(query as esHealthcareQuery);
		}
		#endregion

		virtual public Healthcare DetachEntity(Healthcare entity)
		{
			return base.DetachEntity(entity) as Healthcare;
		}

		virtual public Healthcare AttachEntity(Healthcare entity)
		{
			return base.AttachEntity(entity) as Healthcare;
		}

		virtual public void Combine(HealthcareCollection collection)
		{
			base.Combine(collection);
		}

		new public Healthcare this[int index]
		{
			get
			{
				return base[index] as Healthcare;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Healthcare);
		}
	}

	[Serializable]
	abstract public class esHealthcare : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHealthcareQuery GetDynamicQuery()
		{
			return null;
		}

		public esHealthcare()
		{
		}

		public esHealthcare(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String healthcareID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(healthcareID);
			else
				return LoadByPrimaryKeyStoredProcedure(healthcareID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String healthcareID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(healthcareID);
			else
				return LoadByPrimaryKeyStoredProcedure(healthcareID);
		}

		private bool LoadByPrimaryKeyDynamic(String healthcareID)
		{
			esHealthcareQuery query = this.GetDynamicQuery();
			query.Where(query.HealthcareID == healthcareID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String healthcareID)
		{
			esParameters parms = new esParameters();
			parms.Add("HealthcareID", healthcareID);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "HealthcareID": this.str.HealthcareID = (string)value; break;
						case "HealthcareName": this.str.HealthcareName = (string)value; break;
						case "AddressLine1": this.str.AddressLine1 = (string)value; break;
						case "AddressLine2": this.str.AddressLine2 = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "FaxNo": this.str.FaxNo = (string)value; break;
						case "HospitalCode": this.str.HospitalCode = (string)value; break;
						case "FoundationName": this.str.FoundationName = (string)value; break;
						case "FoundationAddr1": this.str.FoundationAddr1 = (string)value; break;
						case "FoundationAddr2": this.str.FoundationAddr2 = (string)value; break;
						case "FoundationCity": this.str.FoundationCity = (string)value; break;
						case "FoundationZipCode": this.str.FoundationZipCode = (string)value; break;
						case "FoundationPhoneNo": this.str.FoundationPhoneNo = (string)value; break;
						case "FoundationFaxNo": this.str.FoundationFaxNo = (string)value; break;
						case "EmailAddr": this.str.EmailAddr = (string)value; break;
						case "Website": this.str.Website = (string)value; break;
						case "NPWP": this.str.NPWP = (string)value; break;
						case "HospitalType": this.str.HospitalType = (string)value; break;
						case "Provinces": this.str.Provinces = (string)value; break;
						case "ProvincesCode": this.str.ProvincesCode = (string)value; break;
						case "AdditionalInfo": this.str.AdditionalInfo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "HealthcareLogo":

							if (value == null || value is System.Byte[])
								this.HealthcareLogo = (System.Byte[])value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to Healthcare.HealthcareID
		/// </summary>
		virtual public System.String HealthcareID
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.HealthcareID);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.HealthcareID, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.HealthcareName
		/// </summary>
		virtual public System.String HealthcareName
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.HealthcareName);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.HealthcareName, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.AddressLine1
		/// </summary>
		virtual public System.String AddressLine1
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.AddressLine1);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.AddressLine1, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.AddressLine2
		/// </summary>
		virtual public System.String AddressLine2
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.AddressLine2);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.AddressLine2, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FaxNo);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FaxNo, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.HospitalCode
		/// </summary>
		virtual public System.String HospitalCode
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.HospitalCode);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.HospitalCode, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FoundationName
		/// </summary>
		virtual public System.String FoundationName
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FoundationName);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FoundationName, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FoundationAddr1
		/// </summary>
		virtual public System.String FoundationAddr1
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FoundationAddr1);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FoundationAddr1, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FoundationAddr2
		/// </summary>
		virtual public System.String FoundationAddr2
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FoundationAddr2);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FoundationAddr2, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FoundationCity
		/// </summary>
		virtual public System.String FoundationCity
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FoundationCity);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FoundationCity, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FoundationZipCode
		/// </summary>
		virtual public System.String FoundationZipCode
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FoundationZipCode);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FoundationZipCode, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FoundationPhoneNo
		/// </summary>
		virtual public System.String FoundationPhoneNo
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FoundationPhoneNo);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FoundationPhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.FoundationFaxNo
		/// </summary>
		virtual public System.String FoundationFaxNo
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.FoundationFaxNo);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.FoundationFaxNo, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.EmailAddr
		/// </summary>
		virtual public System.String EmailAddr
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.EmailAddr);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.EmailAddr, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.Website
		/// </summary>
		virtual public System.String Website
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.Website);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.Website, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.NPWP
		/// </summary>
		virtual public System.String NPWP
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.NPWP);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.NPWP, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.HospitalType
		/// </summary>
		virtual public System.String HospitalType
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.HospitalType);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.HospitalType, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.Provinces
		/// </summary>
		virtual public System.String Provinces
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.Provinces);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.Provinces, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.ProvincesCode
		/// </summary>
		virtual public System.String ProvincesCode
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.ProvincesCode);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.ProvincesCode, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.AdditionalInfo
		/// </summary>
		virtual public System.String AdditionalInfo
		{
			get
			{
				return base.GetSystemString(HealthcareMetadata.ColumnNames.AdditionalInfo);
			}

			set
			{
				base.SetSystemString(HealthcareMetadata.ColumnNames.AdditionalInfo, value);
			}
		}
		/// <summary>
		/// Maps to Healthcare.HealthcareLogo
		/// </summary>
		virtual public System.Byte[] HealthcareLogo
		{
			get
			{
				return base.GetSystemByteArray(HealthcareMetadata.ColumnNames.HealthcareLogo);
			}

			set
			{
				base.SetSystemByteArray(HealthcareMetadata.ColumnNames.HealthcareLogo, value);
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
		[BrowsableAttribute(false)]
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
			public esStrings(esHealthcare entity)
			{
				this.entity = entity;
			}
			public System.String HealthcareID
			{
				get
				{
					System.String data = entity.HealthcareID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HealthcareID = null;
					else entity.HealthcareID = Convert.ToString(value);
				}
			}
			public System.String HealthcareName
			{
				get
				{
					System.String data = entity.HealthcareName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HealthcareName = null;
					else entity.HealthcareName = Convert.ToString(value);
				}
			}
			public System.String AddressLine1
			{
				get
				{
					System.String data = entity.AddressLine1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddressLine1 = null;
					else entity.AddressLine1 = Convert.ToString(value);
				}
			}
			public System.String AddressLine2
			{
				get
				{
					System.String data = entity.AddressLine2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AddressLine2 = null;
					else entity.AddressLine2 = Convert.ToString(value);
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
			public System.String HospitalCode
			{
				get
				{
					System.String data = entity.HospitalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HospitalCode = null;
					else entity.HospitalCode = Convert.ToString(value);
				}
			}
			public System.String FoundationName
			{
				get
				{
					System.String data = entity.FoundationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoundationName = null;
					else entity.FoundationName = Convert.ToString(value);
				}
			}
			public System.String FoundationAddr1
			{
				get
				{
					System.String data = entity.FoundationAddr1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoundationAddr1 = null;
					else entity.FoundationAddr1 = Convert.ToString(value);
				}
			}
			public System.String FoundationAddr2
			{
				get
				{
					System.String data = entity.FoundationAddr2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoundationAddr2 = null;
					else entity.FoundationAddr2 = Convert.ToString(value);
				}
			}
			public System.String FoundationCity
			{
				get
				{
					System.String data = entity.FoundationCity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoundationCity = null;
					else entity.FoundationCity = Convert.ToString(value);
				}
			}
			public System.String FoundationZipCode
			{
				get
				{
					System.String data = entity.FoundationZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoundationZipCode = null;
					else entity.FoundationZipCode = Convert.ToString(value);
				}
			}
			public System.String FoundationPhoneNo
			{
				get
				{
					System.String data = entity.FoundationPhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoundationPhoneNo = null;
					else entity.FoundationPhoneNo = Convert.ToString(value);
				}
			}
			public System.String FoundationFaxNo
			{
				get
				{
					System.String data = entity.FoundationFaxNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FoundationFaxNo = null;
					else entity.FoundationFaxNo = Convert.ToString(value);
				}
			}
			public System.String EmailAddr
			{
				get
				{
					System.String data = entity.EmailAddr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmailAddr = null;
					else entity.EmailAddr = Convert.ToString(value);
				}
			}
			public System.String Website
			{
				get
				{
					System.String data = entity.Website;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Website = null;
					else entity.Website = Convert.ToString(value);
				}
			}
			public System.String NPWP
			{
				get
				{
					System.String data = entity.NPWP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NPWP = null;
					else entity.NPWP = Convert.ToString(value);
				}
			}
			public System.String HospitalType
			{
				get
				{
					System.String data = entity.HospitalType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HospitalType = null;
					else entity.HospitalType = Convert.ToString(value);
				}
			}
			public System.String Provinces
			{
				get
				{
					System.String data = entity.Provinces;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Provinces = null;
					else entity.Provinces = Convert.ToString(value);
				}
			}
			public System.String ProvincesCode
			{
				get
				{
					System.String data = entity.ProvincesCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProvincesCode = null;
					else entity.ProvincesCode = Convert.ToString(value);
				}
			}
			public System.String AdditionalInfo
			{
				get
				{
					System.String data = entity.AdditionalInfo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdditionalInfo = null;
					else entity.AdditionalInfo = Convert.ToString(value);
				}
			}
			private esHealthcare entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHealthcareQuery query)
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
				throw new Exception("esHealthcare can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Healthcare : esHealthcare
	{
	}

	[Serializable]
	abstract public class esHealthcareQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return HealthcareMetadata.Meta();
			}
		}

		public esQueryItem HealthcareID
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.HealthcareID, esSystemType.String);
			}
		}

		public esQueryItem HealthcareName
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.HealthcareName, esSystemType.String);
			}
		}

		public esQueryItem AddressLine1
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.AddressLine1, esSystemType.String);
			}
		}

		public esQueryItem AddressLine2
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.AddressLine2, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		}

		public esQueryItem HospitalCode
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.HospitalCode, esSystemType.String);
			}
		}

		public esQueryItem FoundationName
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FoundationName, esSystemType.String);
			}
		}

		public esQueryItem FoundationAddr1
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FoundationAddr1, esSystemType.String);
			}
		}

		public esQueryItem FoundationAddr2
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FoundationAddr2, esSystemType.String);
			}
		}

		public esQueryItem FoundationCity
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FoundationCity, esSystemType.String);
			}
		}

		public esQueryItem FoundationZipCode
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FoundationZipCode, esSystemType.String);
			}
		}

		public esQueryItem FoundationPhoneNo
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FoundationPhoneNo, esSystemType.String);
			}
		}

		public esQueryItem FoundationFaxNo
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.FoundationFaxNo, esSystemType.String);
			}
		}

		public esQueryItem EmailAddr
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.EmailAddr, esSystemType.String);
			}
		}

		public esQueryItem Website
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.Website, esSystemType.String);
			}
		}

		public esQueryItem NPWP
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.NPWP, esSystemType.String);
			}
		}

		public esQueryItem HospitalType
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.HospitalType, esSystemType.String);
			}
		}

		public esQueryItem Provinces
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.Provinces, esSystemType.String);
			}
		}

		public esQueryItem ProvincesCode
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.ProvincesCode, esSystemType.String);
			}
		}

		public esQueryItem AdditionalInfo
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.AdditionalInfo, esSystemType.String);
			}
		}

		public esQueryItem HealthcareLogo
		{
			get
			{
				return new esQueryItem(this, HealthcareMetadata.ColumnNames.HealthcareLogo, esSystemType.ByteArray);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HealthcareCollection")]
	public partial class HealthcareCollection : esHealthcareCollection, IEnumerable<Healthcare>
	{
		public HealthcareCollection()
		{

		}

		public static implicit operator List<Healthcare>(HealthcareCollection coll)
		{
			List<Healthcare> list = new List<Healthcare>();

			foreach (Healthcare emp in coll)
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
				return HealthcareMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HealthcareQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Healthcare(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Healthcare();
		}

		#endregion

		[BrowsableAttribute(false)]
		public HealthcareQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HealthcareQuery();
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
		public bool Load(HealthcareQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Healthcare AddNew()
		{
			Healthcare entity = base.AddNewEntity() as Healthcare;

			return entity;
		}
		public Healthcare FindByPrimaryKey(String healthcareID)
		{
			return base.FindByPrimaryKey(healthcareID) as Healthcare;
		}

		#region IEnumerable< Healthcare> Members

		IEnumerator<Healthcare> IEnumerable<Healthcare>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Healthcare;
			}
		}

		#endregion

		private HealthcareQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Healthcare' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Healthcare ({HealthcareID})")]
	[Serializable]
	public partial class Healthcare : esHealthcare
	{
		public Healthcare()
		{
		}

		public Healthcare(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HealthcareMetadata.Meta();
			}
		}

		override protected esHealthcareQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HealthcareQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public HealthcareQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HealthcareQuery();
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
		public bool Load(HealthcareQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private HealthcareQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class HealthcareQuery : esHealthcareQuery
	{
		public HealthcareQuery()
		{

		}

		public HealthcareQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "HealthcareQuery";
		}
	}

	[Serializable]
	public partial class HealthcareMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HealthcareMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.HealthcareID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.HealthcareID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.HealthcareName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.HealthcareName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.AddressLine1, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.AddressLine1;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.AddressLine2, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.AddressLine2;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.City, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.ZipCode, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.PhoneNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FaxNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.HospitalCode, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.HospitalCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FoundationName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FoundationName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FoundationAddr1, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FoundationAddr1;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FoundationAddr2, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FoundationAddr2;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FoundationCity, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FoundationCity;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FoundationZipCode, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FoundationZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FoundationPhoneNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FoundationPhoneNo;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.FoundationFaxNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.FoundationFaxNo;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.EmailAddr, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.EmailAddr;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.Website, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.Website;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.NPWP, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.NPWP;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.HospitalType, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.HospitalType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.Provinces, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.Provinces;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.ProvincesCode, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.ProvincesCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.AdditionalInfo, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = HealthcareMetadata.PropertyNames.AdditionalInfo;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HealthcareMetadata.ColumnNames.HealthcareLogo, 23, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = HealthcareMetadata.PropertyNames.HealthcareLogo;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public HealthcareMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string HealthcareID = "HealthcareID";
			public const string HealthcareName = "HealthcareName";
			public const string AddressLine1 = "AddressLine1";
			public const string AddressLine2 = "AddressLine2";
			public const string City = "City";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string HospitalCode = "HospitalCode";
			public const string FoundationName = "FoundationName";
			public const string FoundationAddr1 = "FoundationAddr1";
			public const string FoundationAddr2 = "FoundationAddr2";
			public const string FoundationCity = "FoundationCity";
			public const string FoundationZipCode = "FoundationZipCode";
			public const string FoundationPhoneNo = "FoundationPhoneNo";
			public const string FoundationFaxNo = "FoundationFaxNo";
			public const string EmailAddr = "EmailAddr";
			public const string Website = "Website";
			public const string NPWP = "NPWP";
			public const string HospitalType = "HospitalType";
			public const string Provinces = "Provinces";
			public const string ProvincesCode = "ProvincesCode";
			public const string AdditionalInfo = "AdditionalInfo";
			public const string HealthcareLogo = "HealthcareLogo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string HealthcareID = "HealthcareID";
			public const string HealthcareName = "HealthcareName";
			public const string AddressLine1 = "AddressLine1";
			public const string AddressLine2 = "AddressLine2";
			public const string City = "City";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string FaxNo = "FaxNo";
			public const string HospitalCode = "HospitalCode";
			public const string FoundationName = "FoundationName";
			public const string FoundationAddr1 = "FoundationAddr1";
			public const string FoundationAddr2 = "FoundationAddr2";
			public const string FoundationCity = "FoundationCity";
			public const string FoundationZipCode = "FoundationZipCode";
			public const string FoundationPhoneNo = "FoundationPhoneNo";
			public const string FoundationFaxNo = "FoundationFaxNo";
			public const string EmailAddr = "EmailAddr";
			public const string Website = "Website";
			public const string NPWP = "NPWP";
			public const string HospitalType = "HospitalType";
			public const string Provinces = "Provinces";
			public const string ProvincesCode = "ProvincesCode";
			public const string AdditionalInfo = "AdditionalInfo";
			public const string HealthcareLogo = "HealthcareLogo";
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
			lock (typeof(HealthcareMetadata))
			{
				if (HealthcareMetadata.mapDelegates == null)
				{
					HealthcareMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (HealthcareMetadata.meta == null)
				{
					HealthcareMetadata.meta = new HealthcareMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("HealthcareID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HealthcareName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AddressLine1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AddressLine2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HospitalCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoundationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoundationAddr1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoundationAddr2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoundationCity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoundationZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoundationPhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FoundationFaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmailAddr", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Website", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NPWP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HospitalType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Provinces", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProvincesCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdditionalInfo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HealthcareLogo", new esTypeMap("image", "System.Byte[]"));


				meta.Source = "Healthcare";
				meta.Destination = "Healthcare";
				meta.spInsert = "proc_HealthcareInsert";
				meta.spUpdate = "proc_HealthcareUpdate";
				meta.spDelete = "proc_HealthcareDelete";
				meta.spLoadAll = "proc_HealthcareLoadAll";
				meta.spLoadByPrimaryKey = "proc_HealthcareLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HealthcareMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
