/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/1/2021 8:06:23 AM
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
	abstract public class esEmergencyContactCollection : esEntityCollectionWAuditLog
	{
		public esEmergencyContactCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmergencyContactCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmergencyContactQuery query)
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
			this.InitQuery(query as esEmergencyContactQuery);
		}
		#endregion

		virtual public EmergencyContact DetachEntity(EmergencyContact entity)
		{
			return base.DetachEntity(entity) as EmergencyContact;
		}

		virtual public EmergencyContact AttachEntity(EmergencyContact entity)
		{
			return base.AttachEntity(entity) as EmergencyContact;
		}

		virtual public void Combine(EmergencyContactCollection collection)
		{
			base.Combine(collection);
		}

		new public EmergencyContact this[int index]
		{
			get
			{
				return base[index] as EmergencyContact;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmergencyContact);
		}
	}

	[Serializable]
	abstract public class esEmergencyContact : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmergencyContactQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmergencyContact()
		{
		}

		public esEmergencyContact(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo)
		{
			esEmergencyContactQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ContactName": this.str.ContactName = (string)value; break;
						case "SRRelationship": this.str.SRRelationship = (string)value; break;
						case "StreetName": this.str.StreetName = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "State": this.str.State = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "FaxNo": this.str.FaxNo = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SROccupation": this.str.SROccupation = (string)value; break;
						case "Ssn": this.str.Ssn = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to EmergencyContact.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.ContactName
		/// </summary>
		virtual public System.String ContactName
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.ContactName);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.ContactName, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.SRRelationship
		/// </summary>
		virtual public System.String SRRelationship
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.SRRelationship);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.SRRelationship, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.StreetName);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.StreetName, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.State);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.State, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.FaxNo);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.FaxNo, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.Email);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.MobilePhoneNo);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmergencyContactMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmergencyContactMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.SROccupation
		/// </summary>
		virtual public System.String SROccupation
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.SROccupation);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.SROccupation, value);
			}
		}
		/// <summary>
		/// Maps to EmergencyContact.Ssn
		/// </summary>
		virtual public System.String Ssn
		{
			get
			{
				return base.GetSystemString(EmergencyContactMetadata.ColumnNames.Ssn);
			}

			set
			{
				base.SetSystemString(EmergencyContactMetadata.ColumnNames.Ssn, value);
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
			public esStrings(esEmergencyContact entity)
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
			public System.String ContactName
			{
				get
				{
					System.String data = entity.ContactName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactName = null;
					else entity.ContactName = Convert.ToString(value);
				}
			}
			public System.String SRRelationship
			{
				get
				{
					System.String data = entity.SRRelationship;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRelationship = null;
					else entity.SRRelationship = Convert.ToString(value);
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
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			public System.String SROccupation
			{
				get
				{
					System.String data = entity.SROccupation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROccupation = null;
					else entity.SROccupation = Convert.ToString(value);
				}
			}
			public System.String Ssn
			{
				get
				{
					System.String data = entity.Ssn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ssn = null;
					else entity.Ssn = Convert.ToString(value);
				}
			}
			private esEmergencyContact entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmergencyContactQuery query)
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
				throw new Exception("esEmergencyContact can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmergencyContact : esEmergencyContact
	{
	}

	[Serializable]
	abstract public class esEmergencyContactQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmergencyContactMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ContactName
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.ContactName, esSystemType.String);
			}
		}

		public esQueryItem SRRelationship
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.SRRelationship, esSystemType.String);
			}
		}

		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.State, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		}

		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.Email, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SROccupation
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.SROccupation, esSystemType.String);
			}
		}

		public esQueryItem Ssn
		{
			get
			{
				return new esQueryItem(this, EmergencyContactMetadata.ColumnNames.Ssn, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmergencyContactCollection")]
	public partial class EmergencyContactCollection : esEmergencyContactCollection, IEnumerable<EmergencyContact>
	{
		public EmergencyContactCollection()
		{

		}

		public static implicit operator List<EmergencyContact>(EmergencyContactCollection coll)
		{
			List<EmergencyContact> list = new List<EmergencyContact>();

			foreach (EmergencyContact emp in coll)
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
				return EmergencyContactMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmergencyContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmergencyContact(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmergencyContact();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmergencyContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmergencyContactQuery();
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
		public bool Load(EmergencyContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmergencyContact AddNew()
		{
			EmergencyContact entity = base.AddNewEntity() as EmergencyContact;

			return entity;
		}
		public EmergencyContact FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as EmergencyContact;
		}

		#region IEnumerable< EmergencyContact> Members

		IEnumerator<EmergencyContact> IEnumerable<EmergencyContact>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmergencyContact;
			}
		}

		#endregion

		private EmergencyContactQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmergencyContact' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmergencyContact ({RegistrationNo})")]
	[Serializable]
	public partial class EmergencyContact : esEmergencyContact
	{
		public EmergencyContact()
		{
		}

		public EmergencyContact(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmergencyContactMetadata.Meta();
			}
		}

		override protected esEmergencyContactQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmergencyContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmergencyContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmergencyContactQuery();
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
		public bool Load(EmergencyContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmergencyContactQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmergencyContactQuery : esEmergencyContactQuery
	{
		public EmergencyContactQuery()
		{

		}

		public EmergencyContactQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmergencyContactQuery";
		}
	}

	[Serializable]
	public partial class EmergencyContactMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmergencyContactMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.ContactName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.ContactName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.SRRelationship, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.SRRelationship;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.StreetName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.District, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.City, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.County, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.State, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.ZipCode, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.FaxNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.Email, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.PhoneNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 255;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.MobilePhoneNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 255;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.Notes, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.SROccupation, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.SROccupation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmergencyContactMetadata.ColumnNames.Ssn, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmergencyContactMetadata.PropertyNames.Ssn;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmergencyContactMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string ContactName = "ContactName";
			public const string SRRelationship = "SRRelationship";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string PhoneNo = "PhoneNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SROccupation = "SROccupation";
			public const string Ssn = "Ssn";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string ContactName = "ContactName";
			public const string SRRelationship = "SRRelationship";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string PhoneNo = "PhoneNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SROccupation = "SROccupation";
			public const string Ssn = "Ssn";
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
			lock (typeof(EmergencyContactMetadata))
			{
				if (EmergencyContactMetadata.mapDelegates == null)
				{
					EmergencyContactMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmergencyContactMetadata.meta == null)
				{
					EmergencyContactMetadata.meta = new EmergencyContactMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContactName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRelationship", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ssn", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmergencyContact";
				meta.Destination = "EmergencyContact";
				meta.spInsert = "proc_EmergencyContactInsert";
				meta.spUpdate = "proc_EmergencyContactUpdate";
				meta.spDelete = "proc_EmergencyContactDelete";
				meta.spLoadAll = "proc_EmergencyContactLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmergencyContactLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmergencyContactMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
