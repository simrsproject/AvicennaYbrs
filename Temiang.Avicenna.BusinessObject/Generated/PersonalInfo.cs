/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/7/2020 11:33:58 AM
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
	abstract public class esPersonalInfoCollection : esEntityCollectionWAuditLog
	{
		public esPersonalInfoCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalInfoQuery query)
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
			this.InitQuery(query as esPersonalInfoQuery);
		}
		#endregion

		virtual public PersonalInfo DetachEntity(PersonalInfo entity)
		{
			return base.DetachEntity(entity) as PersonalInfo;
		}

		virtual public PersonalInfo AttachEntity(PersonalInfo entity)
		{
			return base.AttachEntity(entity) as PersonalInfo;
		}

		virtual public void Combine(PersonalInfoCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalInfo this[int index]
		{
			get
			{
				return base[index] as PersonalInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalInfo);
		}
	}

	[Serializable]
	abstract public class esPersonalInfo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalInfo()
		{
		}

		public esPersonalInfo(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personID)
		{
			esPersonalInfoQuery query = this.GetDynamicQuery();
			query.Where(query.PersonID == personID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonID", personID);
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
						case "PersonID": this.str.PersonID = (string)value; break;
						case "EmployeeNumber": this.str.EmployeeNumber = (string)value; break;
						case "FirstName": this.str.FirstName = (string)value; break;
						case "MiddleName": this.str.MiddleName = (string)value; break;
						case "LastName": this.str.LastName = (string)value; break;
						case "PreTitle": this.str.PreTitle = (string)value; break;
						case "PostTitle": this.str.PostTitle = (string)value; break;
						case "BirthName": this.str.BirthName = (string)value; break;
						case "PlaceBirth": this.str.PlaceBirth = (string)value; break;
						case "BirthDate": this.str.BirthDate = (string)value; break;
						case "SRGenderType": this.str.SRGenderType = (string)value; break;
						case "SRReligion": this.str.SRReligion = (string)value; break;
						case "SRSalutation": this.str.SRSalutation = (string)value; break;
						case "SRBloodType": this.str.SRBloodType = (string)value; break;
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;
						case "Picture": this.str.Picture = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "CoverageClass": this.str.CoverageClass = (string)value; break;
						case "CoverageClassBPJS": this.str.CoverageClassBPJS = (string)value; break;
						case "SREthnic": this.str.SREthnic = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
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
		/// Maps to PersonalInfo.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalInfoMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalInfoMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.EmployeeNumber
		/// </summary>
		virtual public System.String EmployeeNumber
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.EmployeeNumber);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.EmployeeNumber, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.FirstName
		/// </summary>
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.FirstName);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.FirstName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.MiddleName
		/// </summary>
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.MiddleName);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.MiddleName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.LastName
		/// </summary>
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.LastName);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.LastName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.PreTitle
		/// </summary>
		virtual public System.String PreTitle
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.PreTitle);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.PreTitle, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.PostTitle
		/// </summary>
		virtual public System.String PostTitle
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.PostTitle);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.PostTitle, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.BirthName
		/// </summary>
		virtual public System.String BirthName
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.BirthName);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.BirthName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.PlaceBirth
		/// </summary>
		virtual public System.String PlaceBirth
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.PlaceBirth);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.PlaceBirth, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.BirthDate
		/// </summary>
		virtual public System.DateTime? BirthDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalInfoMetadata.ColumnNames.BirthDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalInfoMetadata.ColumnNames.BirthDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.SRGenderType);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.SRGenderType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.SRReligion
		/// </summary>
		virtual public System.String SRReligion
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.SRReligion);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.SRReligion, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.SRSalutation
		/// </summary>
		virtual public System.String SRSalutation
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.SRSalutation);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.SRSalutation, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.SRBloodType
		/// </summary>
		virtual public System.String SRBloodType
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.SRBloodType);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.SRBloodType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.SRMaritalStatus);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.Picture
		/// </summary>
		virtual public System.String Picture
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.Picture);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.Picture, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalInfoMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.CoverageClass
		/// </summary>
		virtual public System.String CoverageClass
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.CoverageClass);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.CoverageClass, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.CoverageClassBPJS
		/// </summary>
		virtual public System.String CoverageClassBPJS
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.CoverageClassBPJS);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.CoverageClassBPJS, value);
			}
		}
		/// <summary>
		/// Maps to PersonalInfo.SREthnic
		/// </summary>
		virtual public System.String SREthnic
		{
			get
			{
				return base.GetSystemString(PersonalInfoMetadata.ColumnNames.SREthnic);
			}

			set
			{
				base.SetSystemString(PersonalInfoMetadata.ColumnNames.SREthnic, value);
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
			public esStrings(esPersonalInfo entity)
			{
				this.entity = entity;
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
			public System.String EmployeeNumber
			{
				get
				{
					System.String data = entity.EmployeeNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeNumber = null;
					else entity.EmployeeNumber = Convert.ToString(value);
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
			public System.String PreTitle
			{
				get
				{
					System.String data = entity.PreTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreTitle = null;
					else entity.PreTitle = Convert.ToString(value);
				}
			}
			public System.String PostTitle
			{
				get
				{
					System.String data = entity.PostTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostTitle = null;
					else entity.PostTitle = Convert.ToString(value);
				}
			}
			public System.String BirthName
			{
				get
				{
					System.String data = entity.BirthName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthName = null;
					else entity.BirthName = Convert.ToString(value);
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
			public System.String SRSalutation
			{
				get
				{
					System.String data = entity.SRSalutation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRSalutation = null;
					else entity.SRSalutation = Convert.ToString(value);
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
			public System.String SREthnic
			{
				get
				{
					System.String data = entity.SREthnic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREthnic = null;
					else entity.SREthnic = Convert.ToString(value);
				}
			}
			private esPersonalInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalInfoQuery query)
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
				throw new Exception("esPersonalInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalInfo : esPersonalInfo
	{
	}

	[Serializable]
	abstract public class esPersonalInfoQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalInfoMetadata.Meta();
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeNumber
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.EmployeeNumber, esSystemType.String);
			}
		}

		public esQueryItem FirstName
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.FirstName, esSystemType.String);
			}
		}

		public esQueryItem MiddleName
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.MiddleName, esSystemType.String);
			}
		}

		public esQueryItem LastName
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.LastName, esSystemType.String);
			}
		}

		public esQueryItem PreTitle
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.PreTitle, esSystemType.String);
			}
		}

		public esQueryItem PostTitle
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.PostTitle, esSystemType.String);
			}
		}

		public esQueryItem BirthName
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.BirthName, esSystemType.String);
			}
		}

		public esQueryItem PlaceBirth
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.PlaceBirth, esSystemType.String);
			}
		}

		public esQueryItem BirthDate
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.BirthDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		}

		public esQueryItem SRReligion
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.SRReligion, esSystemType.String);
			}
		}

		public esQueryItem SRSalutation
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.SRSalutation, esSystemType.String);
			}
		}

		public esQueryItem SRBloodType
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.SRBloodType, esSystemType.String);
			}
		}

		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		}

		public esQueryItem Picture
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.Picture, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem CoverageClass
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.CoverageClass, esSystemType.String);
			}
		}

		public esQueryItem CoverageClassBPJS
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.CoverageClassBPJS, esSystemType.String);
			}
		}

		public esQueryItem SREthnic
		{
			get
			{
				return new esQueryItem(this, PersonalInfoMetadata.ColumnNames.SREthnic, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalInfoCollection")]
	public partial class PersonalInfoCollection : esPersonalInfoCollection, IEnumerable<PersonalInfo>
	{
		public PersonalInfoCollection()
		{

		}

		public static implicit operator List<PersonalInfo>(PersonalInfoCollection coll)
		{
			List<PersonalInfo> list = new List<PersonalInfo>();

			foreach (PersonalInfo emp in coll)
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
				return PersonalInfoMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalInfo();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalInfoQuery();
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
		public bool Load(PersonalInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalInfo AddNew()
		{
			PersonalInfo entity = base.AddNewEntity() as PersonalInfo;

			return entity;
		}
		public PersonalInfo FindByPrimaryKey(Int32 personID)
		{
			return base.FindByPrimaryKey(personID) as PersonalInfo;
		}

		#region IEnumerable< PersonalInfo> Members

		IEnumerator<PersonalInfo> IEnumerable<PersonalInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalInfo;
			}
		}

		#endregion

		private PersonalInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalInfo' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalInfo ({PersonID})")]
	[Serializable]
	public partial class PersonalInfo : esPersonalInfo
	{
		public PersonalInfo()
		{
		}

		public PersonalInfo(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalInfoMetadata.Meta();
			}
		}

		override protected esPersonalInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalInfoQuery();
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
		public bool Load(PersonalInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalInfoQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalInfoQuery : esPersonalInfoQuery
	{
		public PersonalInfoQuery()
		{

		}

		public PersonalInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalInfoQuery";
		}
	}

	[Serializable]
	public partial class PersonalInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.EmployeeNumber, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.EmployeeNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.FirstName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 60;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.MiddleName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.LastName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.PreTitle, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.PreTitle;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.PostTitle, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.PostTitle;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.BirthName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.BirthName;
			c.CharacterMaxLength = 60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.PlaceBirth, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.PlaceBirth;
			c.CharacterMaxLength = 60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.BirthDate, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.BirthDate;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.SRGenderType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.SRReligion, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.SRReligion;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.SRSalutation, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.SRSalutation;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.SRBloodType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.SRBloodType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.SRMaritalStatus, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.Picture, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.Picture;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.PatientID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.CoverageClass, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.CoverageClass;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.CoverageClassBPJS, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.CoverageClassBPJS;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalInfoMetadata.ColumnNames.SREthnic, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalInfoMetadata.PropertyNames.SREthnic;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalInfoMetadata Meta()
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
			public const string PersonID = "PersonID";
			public const string EmployeeNumber = "EmployeeNumber";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string PreTitle = "PreTitle";
			public const string PostTitle = "PostTitle";
			public const string BirthName = "BirthName";
			public const string PlaceBirth = "PlaceBirth";
			public const string BirthDate = "BirthDate";
			public const string SRGenderType = "SRGenderType";
			public const string SRReligion = "SRReligion";
			public const string SRSalutation = "SRSalutation";
			public const string SRBloodType = "SRBloodType";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string Picture = "Picture";
			public const string PatientID = "PatientID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CoverageClass = "CoverageClass";
			public const string CoverageClassBPJS = "CoverageClassBPJS";
			public const string SREthnic = "SREthnic";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonID = "PersonID";
			public const string EmployeeNumber = "EmployeeNumber";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string PreTitle = "PreTitle";
			public const string PostTitle = "PostTitle";
			public const string BirthName = "BirthName";
			public const string PlaceBirth = "PlaceBirth";
			public const string BirthDate = "BirthDate";
			public const string SRGenderType = "SRGenderType";
			public const string SRReligion = "SRReligion";
			public const string SRSalutation = "SRSalutation";
			public const string SRBloodType = "SRBloodType";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string Picture = "Picture";
			public const string PatientID = "PatientID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CoverageClass = "CoverageClass";
			public const string CoverageClassBPJS = "CoverageClassBPJS";
			public const string SREthnic = "SREthnic";
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
			lock (typeof(PersonalInfoMetadata))
			{
				if (PersonalInfoMetadata.mapDelegates == null)
				{
					PersonalInfoMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalInfoMetadata.meta == null)
				{
					PersonalInfoMetadata.meta = new PersonalInfoMetadata();
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

				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PostTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BirthName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlaceBirth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BirthDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSalutation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Picture", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageClass", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageClassBPJS", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREthnic", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalInfo";
				meta.Destination = "PersonalInfo";
				meta.spInsert = "proc_PersonalInfoInsert";
				meta.spUpdate = "proc_PersonalInfoUpdate";
				meta.spDelete = "proc_PersonalInfoDelete";
				meta.spLoadAll = "proc_PersonalInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalInfoLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
