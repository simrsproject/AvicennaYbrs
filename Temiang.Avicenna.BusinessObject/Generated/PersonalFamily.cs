/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/25/2023 3:46:53 PM
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
	abstract public class esPersonalFamilyCollection : esEntityCollectionWAuditLog
	{
		public esPersonalFamilyCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalFamilyCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalFamilyQuery query)
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
			this.InitQuery(query as esPersonalFamilyQuery);
		}
		#endregion

		virtual public PersonalFamily DetachEntity(PersonalFamily entity)
		{
			return base.DetachEntity(entity) as PersonalFamily;
		}

		virtual public PersonalFamily AttachEntity(PersonalFamily entity)
		{
			return base.AttachEntity(entity) as PersonalFamily;
		}

		virtual public void Combine(PersonalFamilyCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalFamily this[int index]
		{
			get
			{
				return base[index] as PersonalFamily;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalFamily);
		}
	}

	[Serializable]
	abstract public class esPersonalFamily : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalFamilyQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalFamily()
		{
		}

		public esPersonalFamily(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalFamilyID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalFamilyID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalFamilyID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalFamilyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalFamilyID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalFamilyID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalFamilyID)
		{
			esPersonalFamilyQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalFamilyID == personalFamilyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalFamilyID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalFamilyID", personalFamilyID);
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
						case "PersonalFamilyID": this.str.PersonalFamilyID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "SRFamilyRelation": this.str.SRFamilyRelation = (string)value; break;
						case "FamilyName": this.str.FamilyName = (string)value; break;
						case "CityOfBirth": this.str.CityOfBirth = (string)value; break;
						case "DateBirth": this.str.DateBirth = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "Address": this.str.Address = (string)value; break;
						case "SRState": this.str.SRState = (string)value; break;
						case "SRCity": this.str.SRCity = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "Phone": this.str.Phone = (string)value; break;
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;
						case "SRGenderType": this.str.SRGenderType = (string)value; break;
						case "IsGuaranteed": this.str.IsGuaranteed = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRCoverageType": this.str.SRCoverageType = (string)value; break;
						case "BPJSKesehatanNo": this.str.BPJSKesehatanNo = (string)value; break;
						case "WeddingDate": this.str.WeddingDate = (string)value; break;
						case "SRFamilyOccupation": this.str.SRFamilyOccupation = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "CoverageClass": this.str.CoverageClass = (string)value; break;
						case "CoverageClassBPJS": this.str.CoverageClassBPJS = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalFamilyID":

							if (value == null || value is System.Int32)
								this.PersonalFamilyID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "DateBirth":

							if (value == null || value is System.DateTime)
								this.DateBirth = (System.DateTime?)value;
							break;
						case "IsGuaranteed":

							if (value == null || value is System.Boolean)
								this.IsGuaranteed = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "WeddingDate":

							if (value == null || value is System.DateTime)
								this.WeddingDate = (System.DateTime?)value;
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
		/// Maps to PersonalFamily.PersonalFamilyID
		/// </summary>
		virtual public System.Int32? PersonalFamilyID
		{
			get
			{
				return base.GetSystemInt32(PersonalFamilyMetadata.ColumnNames.PersonalFamilyID);
			}

			set
			{
				base.SetSystemInt32(PersonalFamilyMetadata.ColumnNames.PersonalFamilyID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalFamilyMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalFamilyMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SRFamilyRelation
		/// </summary>
		virtual public System.String SRFamilyRelation
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SRFamilyRelation);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SRFamilyRelation, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.FamilyName
		/// </summary>
		virtual public System.String FamilyName
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.FamilyName);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.FamilyName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.CityOfBirth
		/// </summary>
		virtual public System.String CityOfBirth
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.CityOfBirth);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.CityOfBirth, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.DateBirth
		/// </summary>
		virtual public System.DateTime? DateBirth
		{
			get
			{
				return base.GetSystemDateTime(PersonalFamilyMetadata.ColumnNames.DateBirth);
			}

			set
			{
				base.SetSystemDateTime(PersonalFamilyMetadata.ColumnNames.DateBirth, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.Address);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.Address, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SRState
		/// </summary>
		virtual public System.String SRState
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SRState);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SRState, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SRCity
		/// </summary>
		virtual public System.String SRCity
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SRCity);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SRCity, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.Phone
		/// </summary>
		virtual public System.String Phone
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.Phone);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.Phone, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SRMaritalStatus);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SRGenderType);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SRGenderType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.IsGuaranteed
		/// </summary>
		virtual public System.Boolean? IsGuaranteed
		{
			get
			{
				return base.GetSystemBoolean(PersonalFamilyMetadata.ColumnNames.IsGuaranteed);
			}

			set
			{
				base.SetSystemBoolean(PersonalFamilyMetadata.ColumnNames.IsGuaranteed, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalFamilyMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalFamilyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SRCoverageType
		/// </summary>
		virtual public System.String SRCoverageType
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SRCoverageType);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SRCoverageType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.BPJSKesehatanNo
		/// </summary>
		virtual public System.String BPJSKesehatanNo
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.BPJSKesehatanNo);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.BPJSKesehatanNo, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.WeddingDate
		/// </summary>
		virtual public System.DateTime? WeddingDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalFamilyMetadata.ColumnNames.WeddingDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalFamilyMetadata.ColumnNames.WeddingDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.SRFamilyOccupation
		/// </summary>
		virtual public System.String SRFamilyOccupation
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.SRFamilyOccupation);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.SRFamilyOccupation, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.CoverageClass
		/// </summary>
		virtual public System.String CoverageClass
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.CoverageClass);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.CoverageClass, value);
			}
		}
		/// <summary>
		/// Maps to PersonalFamily.CoverageClassBPJS
		/// </summary>
		virtual public System.String CoverageClassBPJS
		{
			get
			{
				return base.GetSystemString(PersonalFamilyMetadata.ColumnNames.CoverageClassBPJS);
			}

			set
			{
				base.SetSystemString(PersonalFamilyMetadata.ColumnNames.CoverageClassBPJS, value);
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
			public esStrings(esPersonalFamily entity)
			{
				this.entity = entity;
			}
			public System.String PersonalFamilyID
			{
				get
				{
					System.Int32? data = entity.PersonalFamilyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalFamilyID = null;
					else entity.PersonalFamilyID = Convert.ToInt32(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
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
			public System.String SRFamilyRelation
			{
				get
				{
					System.String data = entity.SRFamilyRelation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFamilyRelation = null;
					else entity.SRFamilyRelation = Convert.ToString(value);
				}
			}
			public System.String FamilyName
			{
				get
				{
					System.String data = entity.FamilyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FamilyName = null;
					else entity.FamilyName = Convert.ToString(value);
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
			public System.String DateBirth
			{
				get
				{
					System.DateTime? data = entity.DateBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateBirth = null;
					else entity.DateBirth = Convert.ToDateTime(value);
				}
			}
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
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
			public System.String Phone
			{
				get
				{
					System.String data = entity.Phone;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Phone = null;
					else entity.Phone = Convert.ToString(value);
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
			public System.String IsGuaranteed
			{
				get
				{
					System.Boolean? data = entity.IsGuaranteed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGuaranteed = null;
					else entity.IsGuaranteed = Convert.ToBoolean(value);
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
			public System.String SRCoverageType
			{
				get
				{
					System.String data = entity.SRCoverageType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCoverageType = null;
					else entity.SRCoverageType = Convert.ToString(value);
				}
			}
			public System.String BPJSKesehatanNo
			{
				get
				{
					System.String data = entity.BPJSKesehatanNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BPJSKesehatanNo = null;
					else entity.BPJSKesehatanNo = Convert.ToString(value);
				}
			}
			public System.String WeddingDate
			{
				get
				{
					System.DateTime? data = entity.WeddingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeddingDate = null;
					else entity.WeddingDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRFamilyOccupation
			{
				get
				{
					System.String data = entity.SRFamilyOccupation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFamilyOccupation = null;
					else entity.SRFamilyOccupation = Convert.ToString(value);
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
			public System.String CoverageClass
			{
				get
				{
					System.String data = entity.CoverageClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageClass = null;
					else entity.CoverageClass = Convert.ToString(value);
				}
			}
			public System.String CoverageClassBPJS
			{
				get
				{
					System.String data = entity.CoverageClassBPJS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageClassBPJS = null;
					else entity.CoverageClassBPJS = Convert.ToString(value);
				}
			}
			private esPersonalFamily entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalFamilyQuery query)
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
				throw new Exception("esPersonalFamily can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalFamily : esPersonalFamily
	{
	}

	[Serializable]
	abstract public class esPersonalFamilyQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalFamilyMetadata.Meta();
			}
		}

		public esQueryItem PersonalFamilyID
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.PersonalFamilyID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem SRFamilyRelation
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SRFamilyRelation, esSystemType.String);
			}
		}

		public esQueryItem FamilyName
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.FamilyName, esSystemType.String);
			}
		}

		public esQueryItem CityOfBirth
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.CityOfBirth, esSystemType.String);
			}
		}

		public esQueryItem DateBirth
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.DateBirth, esSystemType.DateTime);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.Address, esSystemType.String);
			}
		}

		public esQueryItem SRState
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SRState, esSystemType.String);
			}
		}

		public esQueryItem SRCity
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SRCity, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem Phone
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.Phone, esSystemType.String);
			}
		}

		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		}

		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		}

		public esQueryItem IsGuaranteed
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.IsGuaranteed, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRCoverageType
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SRCoverageType, esSystemType.String);
			}
		}

		public esQueryItem BPJSKesehatanNo
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.BPJSKesehatanNo, esSystemType.String);
			}
		}

		public esQueryItem WeddingDate
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.WeddingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRFamilyOccupation
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.SRFamilyOccupation, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem CoverageClass
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.CoverageClass, esSystemType.String);
			}
		}

		public esQueryItem CoverageClassBPJS
		{
			get
			{
				return new esQueryItem(this, PersonalFamilyMetadata.ColumnNames.CoverageClassBPJS, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalFamilyCollection")]
	public partial class PersonalFamilyCollection : esPersonalFamilyCollection, IEnumerable<PersonalFamily>
	{
		public PersonalFamilyCollection()
		{

		}

		public static implicit operator List<PersonalFamily>(PersonalFamilyCollection coll)
		{
			List<PersonalFamily> list = new List<PersonalFamily>();

			foreach (PersonalFamily emp in coll)
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
				return PersonalFamilyMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalFamilyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalFamily(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalFamily();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalFamilyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalFamilyQuery();
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
		public bool Load(PersonalFamilyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalFamily AddNew()
		{
			PersonalFamily entity = base.AddNewEntity() as PersonalFamily;

			return entity;
		}
		public PersonalFamily FindByPrimaryKey(Int32 personalFamilyID)
		{
			return base.FindByPrimaryKey(personalFamilyID) as PersonalFamily;
		}

		#region IEnumerable< PersonalFamily> Members

		IEnumerator<PersonalFamily> IEnumerable<PersonalFamily>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalFamily;
			}
		}

		#endregion

		private PersonalFamilyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalFamily' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalFamily ({PersonalFamilyID})")]
	[Serializable]
	public partial class PersonalFamily : esPersonalFamily
	{
		public PersonalFamily()
		{
		}

		public PersonalFamily(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalFamilyMetadata.Meta();
			}
		}

		override protected esPersonalFamilyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalFamilyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalFamilyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalFamilyQuery();
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
		public bool Load(PersonalFamilyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalFamilyQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalFamilyQuery : esPersonalFamilyQuery
	{
		public PersonalFamilyQuery()
		{

		}

		public PersonalFamilyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalFamilyQuery";
		}
	}

	[Serializable]
	public partial class PersonalFamilyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalFamilyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.PersonalFamilyID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.PersonalFamilyID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.PatientID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SRFamilyRelation, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SRFamilyRelation;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.FamilyName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.FamilyName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.CityOfBirth, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.CityOfBirth;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.DateBirth, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.DateBirth;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SREducationLevel, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.Address, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SRState, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SRState;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SRCity, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SRCity;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.ZipCode, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.Phone, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.Phone;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SRMaritalStatus, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SRGenderType, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.IsGuaranteed, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.IsGuaranteed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SRCoverageType, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SRCoverageType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.BPJSKesehatanNo, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.BPJSKesehatanNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.WeddingDate, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.WeddingDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.SRFamilyOccupation, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.SRFamilyOccupation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.District, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.County, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.City, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.CoverageClass, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.CoverageClass;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalFamilyMetadata.ColumnNames.CoverageClassBPJS, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalFamilyMetadata.PropertyNames.CoverageClassBPJS;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalFamilyMetadata Meta()
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
			public const string PersonalFamilyID = "PersonalFamilyID";
			public const string PersonID = "PersonID";
			public const string PatientID = "PatientID";
			public const string SRFamilyRelation = "SRFamilyRelation";
			public const string FamilyName = "FamilyName";
			public const string CityOfBirth = "CityOfBirth";
			public const string DateBirth = "DateBirth";
			public const string SREducationLevel = "SREducationLevel";
			public const string Address = "Address";
			public const string SRState = "SRState";
			public const string SRCity = "SRCity";
			public const string ZipCode = "ZipCode";
			public const string Phone = "Phone";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string SRGenderType = "SRGenderType";
			public const string IsGuaranteed = "IsGuaranteed";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRCoverageType = "SRCoverageType";
			public const string BPJSKesehatanNo = "BPJSKesehatanNo";
			public const string WeddingDate = "WeddingDate";
			public const string SRFamilyOccupation = "SRFamilyOccupation";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
			public const string CoverageClass = "CoverageClass";
			public const string CoverageClassBPJS = "CoverageClassBPJS";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalFamilyID = "PersonalFamilyID";
			public const string PersonID = "PersonID";
			public const string PatientID = "PatientID";
			public const string SRFamilyRelation = "SRFamilyRelation";
			public const string FamilyName = "FamilyName";
			public const string CityOfBirth = "CityOfBirth";
			public const string DateBirth = "DateBirth";
			public const string SREducationLevel = "SREducationLevel";
			public const string Address = "Address";
			public const string SRState = "SRState";
			public const string SRCity = "SRCity";
			public const string ZipCode = "ZipCode";
			public const string Phone = "Phone";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string SRGenderType = "SRGenderType";
			public const string IsGuaranteed = "IsGuaranteed";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRCoverageType = "SRCoverageType";
			public const string BPJSKesehatanNo = "BPJSKesehatanNo";
			public const string WeddingDate = "WeddingDate";
			public const string SRFamilyOccupation = "SRFamilyOccupation";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
			public const string CoverageClass = "CoverageClass";
			public const string CoverageClassBPJS = "CoverageClassBPJS";
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
			lock (typeof(PersonalFamilyMetadata))
			{
				if (PersonalFamilyMetadata.mapDelegates == null)
				{
					PersonalFamilyMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalFamilyMetadata.meta == null)
				{
					PersonalFamilyMetadata.meta = new PersonalFamilyMetadata();
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

				meta.AddTypeMap("PersonalFamilyID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFamilyRelation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FamilyName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CityOfBirth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateBirth", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRState", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Phone", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsGuaranteed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCoverageType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BPJSKesehatanNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WeddingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRFamilyOccupation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageClass", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageClassBPJS", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalFamily";
				meta.Destination = "PersonalFamily";
				meta.spInsert = "proc_PersonalFamilyInsert";
				meta.spUpdate = "proc_PersonalFamilyUpdate";
				meta.spDelete = "proc_PersonalFamilyDelete";
				meta.spLoadAll = "proc_PersonalFamilyLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalFamilyLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalFamilyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
