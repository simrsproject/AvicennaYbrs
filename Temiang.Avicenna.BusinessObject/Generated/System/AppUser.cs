/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/23/2022 10:18:22 PM
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
	abstract public class esAppUserCollection : esEntityCollectionWAuditLog
	{
		public esAppUserCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppUserCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppUserQuery query)
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
			this.InitQuery(query as esAppUserQuery);
		}
		#endregion

		virtual public AppUser DetachEntity(AppUser entity)
		{
			return base.DetachEntity(entity) as AppUser;
		}

		virtual public AppUser AttachEntity(AppUser entity)
		{
			return base.AttachEntity(entity) as AppUser;
		}

		virtual public void Combine(AppUserCollection collection)
		{
			base.Combine(collection);
		}

		new public AppUser this[int index]
		{
			get
			{
				return base[index] as AppUser;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppUser);
		}
	}

	[Serializable]
	abstract public class esAppUser : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppUserQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppUser()
		{
		}

		public esAppUser(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String userID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String userID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID);
		}

		private bool LoadByPrimaryKeyDynamic(String userID)
		{
			esAppUserQuery query = this.GetDynamicQuery();
			query.Where(query.UserID == userID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String userID)
		{
			esParameters parms = new esParameters();
			parms.Add("UserID", userID);
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
						case "UserID": this.str.UserID = (string)value; break;
						case "UserName": this.str.UserName = (string)value; break;
						case "Password": this.str.Password = (string)value; break;
						case "SRLanguage": this.str.SRLanguage = (string)value; break;
						case "ActiveDate": this.str.ActiveDate = (string)value; break;
						case "ExpireDate": this.str.ExpireDate = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "LicenseNo": this.str.LicenseNo = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "IsLocked": this.str.IsLocked = (string)value; break;
						case "SRUserType": this.str.SRUserType = (string)value; break;
						case "CashManagementNo": this.str.CashManagementNo = (string)value; break;
						case "LastCounterName": this.str.LastCounterName = (string)value; break;
						case "ESignNik": this.str.ESignNik = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ActiveDate":

							if (value == null || value is System.DateTime)
								this.ActiveDate = (System.DateTime?)value;
							break;
						case "ExpireDate":

							if (value == null || value is System.DateTime)
								this.ExpireDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "IsLocked":

							if (value == null || value is System.Boolean)
								this.IsLocked = (System.Boolean?)value;
							break;
						case "SignatureImage":

							if (value == null || value is System.Byte[])
								this.SignatureImage = (System.Byte[])value;
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
		/// Maps to AppUser.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.UserID);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.UserID, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.UserName
		/// </summary>
		virtual public System.String UserName
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.UserName);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.UserName, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.Password
		/// </summary>
		virtual public System.String Password
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.Password);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.Password, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.SRLanguage
		/// </summary>
		virtual public System.String SRLanguage
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.SRLanguage);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.SRLanguage, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.ActiveDate
		/// </summary>
		virtual public System.DateTime? ActiveDate
		{
			get
			{
				return base.GetSystemDateTime(AppUserMetadata.ColumnNames.ActiveDate);
			}

			set
			{
				base.SetSystemDateTime(AppUserMetadata.ColumnNames.ActiveDate, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.ExpireDate
		/// </summary>
		virtual public System.DateTime? ExpireDate
		{
			get
			{
				return base.GetSystemDateTime(AppUserMetadata.ColumnNames.ExpireDate);
			}

			set
			{
				base.SetSystemDateTime(AppUserMetadata.ColumnNames.ExpireDate, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppUserMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppUserMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.LicenseNo
		/// </summary>
		virtual public System.String LicenseNo
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.LicenseNo);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.LicenseNo, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(AppUserMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(AppUserMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.Email);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.IsLocked
		/// </summary>
		virtual public System.Boolean? IsLocked
		{
			get
			{
				return base.GetSystemBoolean(AppUserMetadata.ColumnNames.IsLocked);
			}

			set
			{
				base.SetSystemBoolean(AppUserMetadata.ColumnNames.IsLocked, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.SRUserType
		/// </summary>
		virtual public System.String SRUserType
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.SRUserType);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.SRUserType, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.CashManagementNo
		/// </summary>
		virtual public System.String CashManagementNo
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.CashManagementNo);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.CashManagementNo, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.SignatureImage
		/// </summary>
		virtual public System.Byte[] SignatureImage
		{
			get
			{
				return base.GetSystemByteArray(AppUserMetadata.ColumnNames.SignatureImage);
			}

			set
			{
				base.SetSystemByteArray(AppUserMetadata.ColumnNames.SignatureImage, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.LastCounterName
		/// </summary>
		virtual public System.String LastCounterName
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.LastCounterName);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.LastCounterName, value);
			}
		}
		/// <summary>
		/// Maps to AppUser.ESignNik
		/// </summary>
		virtual public System.String ESignNik
		{
			get
			{
				return base.GetSystemString(AppUserMetadata.ColumnNames.ESignNik);
			}

			set
			{
				base.SetSystemString(AppUserMetadata.ColumnNames.ESignNik, value);
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
			public esStrings(esAppUser entity)
			{
				this.entity = entity;
			}
			public System.String UserID
			{
				get
				{
					System.String data = entity.UserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserID = null;
					else entity.UserID = Convert.ToString(value);
				}
			}
			public System.String UserName
			{
				get
				{
					System.String data = entity.UserName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserName = null;
					else entity.UserName = Convert.ToString(value);
				}
			}
			public System.String Password
			{
				get
				{
					System.String data = entity.Password;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Password = null;
					else entity.Password = Convert.ToString(value);
				}
			}
			public System.String SRLanguage
			{
				get
				{
					System.String data = entity.SRLanguage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLanguage = null;
					else entity.SRLanguage = Convert.ToString(value);
				}
			}
			public System.String ActiveDate
			{
				get
				{
					System.DateTime? data = entity.ActiveDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActiveDate = null;
					else entity.ActiveDate = Convert.ToDateTime(value);
				}
			}
			public System.String ExpireDate
			{
				get
				{
					System.DateTime? data = entity.ExpireDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpireDate = null;
					else entity.ExpireDate = Convert.ToDateTime(value);
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
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String LicenseNo
			{
				get
				{
					System.String data = entity.LicenseNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LicenseNo = null;
					else entity.LicenseNo = Convert.ToString(value);
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
			public System.String IsLocked
			{
				get
				{
					System.Boolean? data = entity.IsLocked;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLocked = null;
					else entity.IsLocked = Convert.ToBoolean(value);
				}
			}
			public System.String SRUserType
			{
				get
				{
					System.String data = entity.SRUserType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRUserType = null;
					else entity.SRUserType = Convert.ToString(value);
				}
			}
			public System.String CashManagementNo
			{
				get
				{
					System.String data = entity.CashManagementNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashManagementNo = null;
					else entity.CashManagementNo = Convert.ToString(value);
				}
			}
			public System.String LastCounterName
			{
				get
				{
					System.String data = entity.LastCounterName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCounterName = null;
					else entity.LastCounterName = Convert.ToString(value);
				}
			}
			public System.String ESignNik
			{
				get
				{
					System.String data = entity.ESignNik;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ESignNik = null;
					else entity.ESignNik = Convert.ToString(value);
				}
			}
			private esAppUser entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppUserQuery query)
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
				throw new Exception("esAppUser can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppUser : esAppUser
	{
	}

	[Serializable]
	abstract public class esAppUserQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppUserMetadata.Meta();
			}
		}

		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.UserID, esSystemType.String);
			}
		}

		public esQueryItem UserName
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.UserName, esSystemType.String);
			}
		}

		public esQueryItem Password
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.Password, esSystemType.String);
			}
		}

		public esQueryItem SRLanguage
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.SRLanguage, esSystemType.String);
			}
		}

		public esQueryItem ActiveDate
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.ActiveDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ExpireDate
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.ExpireDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem LicenseNo
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.LicenseNo, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.Email, esSystemType.String);
			}
		}

		public esQueryItem IsLocked
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.IsLocked, esSystemType.Boolean);
			}
		}

		public esQueryItem SRUserType
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.SRUserType, esSystemType.String);
			}
		}

		public esQueryItem CashManagementNo
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.CashManagementNo, esSystemType.String);
			}
		}

		public esQueryItem SignatureImage
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.SignatureImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem LastCounterName
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.LastCounterName, esSystemType.String);
			}
		}

		public esQueryItem ESignNik
		{
			get
			{
				return new esQueryItem(this, AppUserMetadata.ColumnNames.ESignNik, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppUserCollection")]
	public partial class AppUserCollection : esAppUserCollection, IEnumerable<AppUser>
	{
		public AppUserCollection()
		{

		}

		public static implicit operator List<AppUser>(AppUserCollection coll)
		{
			List<AppUser> list = new List<AppUser>();

			foreach (AppUser emp in coll)
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
				return AppUserMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppUser(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppUser();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppUserQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserQuery();
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
		public bool Load(AppUserQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppUser AddNew()
		{
			AppUser entity = base.AddNewEntity() as AppUser;

			return entity;
		}
		public AppUser FindByPrimaryKey(String userID)
		{
			return base.FindByPrimaryKey(userID) as AppUser;
		}

		#region IEnumerable< AppUser> Members

		IEnumerator<AppUser> IEnumerable<AppUser>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppUser;
			}
		}

		#endregion

		private AppUserQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppUser' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppUser ({UserID})")]
	[Serializable]
	public partial class AppUser : esAppUser
	{
		public AppUser()
		{
		}

		public AppUser(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppUserMetadata.Meta();
			}
		}

		override protected esAppUserQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppUserQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserQuery();
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
		public bool Load(AppUserQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppUserQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppUserQuery : esAppUserQuery
	{
		public AppUserQuery()
		{

		}

		public AppUserQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppUserQuery";
		}
	}

	[Serializable]
	public partial class AppUserMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppUserMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.UserID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.UserID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.UserName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.UserName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.Password, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.Password;
			c.CharacterMaxLength = 5000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.SRLanguage, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.SRLanguage;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('id-ID')";
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.ActiveDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppUserMetadata.PropertyNames.ActiveDate;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.ExpireDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppUserMetadata.PropertyNames.ExpireDate;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppUserMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.ParamedicID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.ServiceUnitID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.LicenseNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.LicenseNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.PersonID, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppUserMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.Email, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.IsLocked, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserMetadata.PropertyNames.IsLocked;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.SRUserType, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.SRUserType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.CashManagementNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.CashManagementNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.SignatureImage, 16, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = AppUserMetadata.PropertyNames.SignatureImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.LastCounterName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.LastCounterName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserMetadata.ColumnNames.ESignNik, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserMetadata.PropertyNames.ESignNik;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppUserMetadata Meta()
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
			public const string UserID = "UserID";
			public const string UserName = "UserName";
			public const string Password = "Password";
			public const string SRLanguage = "SRLanguage";
			public const string ActiveDate = "ActiveDate";
			public const string ExpireDate = "ExpireDate";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ParamedicID = "ParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LicenseNo = "LicenseNo";
			public const string PersonID = "PersonID";
			public const string Email = "Email";
			public const string IsLocked = "IsLocked";
			public const string SRUserType = "SRUserType";
			public const string CashManagementNo = "CashManagementNo";
			public const string SignatureImage = "SignatureImage";
			public const string LastCounterName = "LastCounterName";
			public const string ESignNik = "ESignNik";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string UserID = "UserID";
			public const string UserName = "UserName";
			public const string Password = "Password";
			public const string SRLanguage = "SRLanguage";
			public const string ActiveDate = "ActiveDate";
			public const string ExpireDate = "ExpireDate";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ParamedicID = "ParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LicenseNo = "LicenseNo";
			public const string PersonID = "PersonID";
			public const string Email = "Email";
			public const string IsLocked = "IsLocked";
			public const string SRUserType = "SRUserType";
			public const string CashManagementNo = "CashManagementNo";
			public const string SignatureImage = "SignatureImage";
			public const string LastCounterName = "LastCounterName";
			public const string ESignNik = "ESignNik";
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
			lock (typeof(AppUserMetadata))
			{
				if (AppUserMetadata.mapDelegates == null)
				{
					AppUserMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppUserMetadata.meta == null)
				{
					AppUserMetadata.meta = new AppUserMetadata();
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

				meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UserName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Password", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLanguage", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActiveDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ExpireDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LicenseNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsLocked", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRUserType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CashManagementNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SignatureImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("LastCounterName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ESignNik", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppUser";
				meta.Destination = "AppUser";
				meta.spInsert = "proc_AppUserInsert";
				meta.spUpdate = "proc_AppUserUpdate";
				meta.spDelete = "proc_AppUserDelete";
				meta.spLoadAll = "proc_AppUserLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppUserLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppUserMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
