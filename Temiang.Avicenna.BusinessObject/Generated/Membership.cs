/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/27/2022 7:46:59 PM
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
	abstract public class esMembershipCollection : esEntityCollectionWAuditLog
	{
		public esMembershipCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MembershipCollection";
		}

		#region Query Logic
		protected void InitQuery(esMembershipQuery query)
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
			this.InitQuery(query as esMembershipQuery);
		}
		#endregion

		virtual public Membership DetachEntity(Membership entity)
		{
			return base.DetachEntity(entity) as Membership;
		}

		virtual public Membership AttachEntity(Membership entity)
		{
			return base.AttachEntity(entity) as Membership;
		}

		virtual public void Combine(MembershipCollection collection)
		{
			base.Combine(collection);
		}

		new public Membership this[int index]
		{
			get
			{
				return base[index] as Membership;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Membership);
		}
	}

	[Serializable]
	abstract public class esMembership : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMembershipQuery GetDynamicQuery()
		{
			return null;
		}

		public esMembership()
		{
		}

		public esMembership(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String membershipNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(membershipNo);
			else
				return LoadByPrimaryKeyStoredProcedure(membershipNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String membershipNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(membershipNo);
			else
				return LoadByPrimaryKeyStoredProcedure(membershipNo);
		}

		private bool LoadByPrimaryKeyDynamic(String membershipNo)
		{
			esMembershipQuery query = this.GetDynamicQuery();
			query.Where(query.MembershipNo == membershipNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String membershipNo)
		{
			esParameters parms = new esParameters();
			parms.Add("MembershipNo", membershipNo);
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
						case "MembershipNo": this.str.MembershipNo = (string)value; break;
						case "SRMembershipType": this.str.SRMembershipType = (string)value; break;
						case "JoinDate": this.str.JoinDate = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "MemberName": this.str.MemberName = (string)value; break;
						case "SRSalutation": this.str.SRSalutation = (string)value; break;
						case "Sex": this.str.Sex = (string)value; break;
						case "CityOfBirth": this.str.CityOfBirth = (string)value; break;
						case "DateOfBirth": this.str.DateOfBirth = (string)value; break;
						case "Address": this.str.Address = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "JoinDate":

							if (value == null || value is System.DateTime)
								this.JoinDate = (System.DateTime?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "DateOfBirth":

							if (value == null || value is System.DateTime)
								this.DateOfBirth = (System.DateTime?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to Membership.MembershipNo
		/// </summary>
		virtual public System.String MembershipNo
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.MembershipNo);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.MembershipNo, value);
			}
		}
		/// <summary>
		/// Maps to Membership.SRMembershipType
		/// </summary>
		virtual public System.String SRMembershipType
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.SRMembershipType);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.SRMembershipType, value);
			}
		}
		/// <summary>
		/// Maps to Membership.JoinDate
		/// </summary>
		virtual public System.DateTime? JoinDate
		{
			get
			{
				return base.GetSystemDateTime(MembershipMetadata.ColumnNames.JoinDate);
			}

			set
			{
				base.SetSystemDateTime(MembershipMetadata.ColumnNames.JoinDate, value);
			}
		}
		/// <summary>
		/// Maps to Membership.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to Membership.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MembershipMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(MembershipMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to Membership.MemberName
		/// </summary>
		virtual public System.String MemberName
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.MemberName);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.MemberName, value);
			}
		}
		/// <summary>
		/// Maps to Membership.SRSalutation
		/// </summary>
		virtual public System.String SRSalutation
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.SRSalutation);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.SRSalutation, value);
			}
		}
		/// <summary>
		/// Maps to Membership.Sex
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.Sex);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.Sex, value);
			}
		}
		/// <summary>
		/// Maps to Membership.CityOfBirth
		/// </summary>
		virtual public System.String CityOfBirth
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.CityOfBirth);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.CityOfBirth, value);
			}
		}
		/// <summary>
		/// Maps to Membership.DateOfBirth
		/// </summary>
		virtual public System.DateTime? DateOfBirth
		{
			get
			{
				return base.GetSystemDateTime(MembershipMetadata.ColumnNames.DateOfBirth);
			}

			set
			{
				base.SetSystemDateTime(MembershipMetadata.ColumnNames.DateOfBirth, value);
			}
		}
		/// <summary>
		/// Maps to Membership.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.Address);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.Address, value);
			}
		}
		/// <summary>
		/// Maps to Membership.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Membership.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.MobilePhoneNo);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Membership.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.Email);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to Membership.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(MembershipMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(MembershipMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Membership.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MembershipMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MembershipMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Membership.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Membership.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MembershipMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MembershipMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Membership.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MembershipMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MembershipMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Membership.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(MembershipMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(MembershipMetadata.ColumnNames.ValidTo, value);
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
			public esStrings(esMembership entity)
			{
				this.entity = entity;
			}
			public System.String MembershipNo
			{
				get
				{
					System.String data = entity.MembershipNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MembershipNo = null;
					else entity.MembershipNo = Convert.ToString(value);
				}
			}
			public System.String SRMembershipType
			{
				get
				{
					System.String data = entity.SRMembershipType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMembershipType = null;
					else entity.SRMembershipType = Convert.ToString(value);
				}
			}
			public System.String JoinDate
			{
				get
				{
					System.DateTime? data = entity.JoinDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JoinDate = null;
					else entity.JoinDate = Convert.ToDateTime(value);
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
			public System.String MemberName
			{
				get
				{
					System.String data = entity.MemberName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MemberName = null;
					else entity.MemberName = Convert.ToString(value);
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
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
				}
			}
			private esMembership entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMembershipQuery query)
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
				throw new Exception("esMembership can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Membership : esMembership
	{
	}

	[Serializable]
	abstract public class esMembershipQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MembershipMetadata.Meta();
			}
		}

		public esQueryItem MembershipNo
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.MembershipNo, esSystemType.String);
			}
		}

		public esQueryItem SRMembershipType
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.SRMembershipType, esSystemType.String);
			}
		}

		public esQueryItem JoinDate
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.JoinDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem MemberName
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.MemberName, esSystemType.String);
			}
		}

		public esQueryItem SRSalutation
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.SRSalutation, esSystemType.String);
			}
		}

		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.Sex, esSystemType.String);
			}
		}

		public esQueryItem CityOfBirth
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.CityOfBirth, esSystemType.String);
			}
		}

		public esQueryItem DateOfBirth
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
			}
		}

		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.Address, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		}

		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.Email, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, MembershipMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MembershipCollection")]
	public partial class MembershipCollection : esMembershipCollection, IEnumerable<Membership>
	{
		public MembershipCollection()
		{

		}

		public static implicit operator List<Membership>(MembershipCollection coll)
		{
			List<Membership> list = new List<Membership>();

			foreach (Membership emp in coll)
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
				return MembershipMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Membership(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Membership();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MembershipQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipQuery();
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
		public bool Load(MembershipQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Membership AddNew()
		{
			Membership entity = base.AddNewEntity() as Membership;

			return entity;
		}
		public Membership FindByPrimaryKey(String membershipNo)
		{
			return base.FindByPrimaryKey(membershipNo) as Membership;
		}

		#region IEnumerable< Membership> Members

		IEnumerator<Membership> IEnumerable<Membership>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Membership;
			}
		}

		#endregion

		private MembershipQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Membership' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Membership ({MembershipNo})")]
	[Serializable]
	public partial class Membership : esMembership
	{
		public Membership()
		{
		}

		public Membership(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MembershipMetadata.Meta();
			}
		}

		override protected esMembershipQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MembershipQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipQuery();
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
		public bool Load(MembershipQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MembershipQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MembershipQuery : esMembershipQuery
	{
		public MembershipQuery()
		{

		}

		public MembershipQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MembershipQuery";
		}
	}

	[Serializable]
	public partial class MembershipMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MembershipMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.MembershipNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.MembershipNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.SRMembershipType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.SRMembershipType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.JoinDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipMetadata.PropertyNames.JoinDate;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.PatientID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.PersonID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MembershipMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.MemberName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.MemberName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.SRSalutation, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.SRSalutation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.Sex, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.CityOfBirth, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.CityOfBirth;
			c.CharacterMaxLength = 60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.DateOfBirth, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipMetadata.PropertyNames.DateOfBirth;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.Address, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.PhoneNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.MobilePhoneNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.Email, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.IsActive, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MembershipMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.CreateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.CreateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipMetadata.ColumnNames.ValidTo, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MembershipMetadata Meta()
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
			public const string MembershipNo = "MembershipNo";
			public const string SRMembershipType = "SRMembershipType";
			public const string JoinDate = "JoinDate";
			public const string PatientID = "PatientID";
			public const string PersonID = "PersonID";
			public const string MemberName = "MemberName";
			public const string SRSalutation = "SRSalutation";
			public const string Sex = "Sex";
			public const string CityOfBirth = "CityOfBirth";
			public const string DateOfBirth = "DateOfBirth";
			public const string Address = "Address";
			public const string PhoneNo = "PhoneNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Email = "Email";
			public const string IsActive = "IsActive";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ValidTo = "ValidTo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MembershipNo = "MembershipNo";
			public const string SRMembershipType = "SRMembershipType";
			public const string JoinDate = "JoinDate";
			public const string PatientID = "PatientID";
			public const string PersonID = "PersonID";
			public const string MemberName = "MemberName";
			public const string SRSalutation = "SRSalutation";
			public const string Sex = "Sex";
			public const string CityOfBirth = "CityOfBirth";
			public const string DateOfBirth = "DateOfBirth";
			public const string Address = "Address";
			public const string PhoneNo = "PhoneNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string Email = "Email";
			public const string IsActive = "IsActive";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ValidTo = "ValidTo";
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
			lock (typeof(MembershipMetadata))
			{
				if (MembershipMetadata.mapDelegates == null)
				{
					MembershipMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MembershipMetadata.meta == null)
				{
					MembershipMetadata.meta = new MembershipMetadata();
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

				meta.AddTypeMap("MembershipNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMembershipType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JoinDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MemberName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRSalutation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("CityOfBirth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateOfBirth", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "Membership";
				meta.Destination = "Membership";
				meta.spInsert = "proc_MembershipInsert";
				meta.spUpdate = "proc_MembershipUpdate";
				meta.spDelete = "proc_MembershipDelete";
				meta.spLoadAll = "proc_MembershipLoadAll";
				meta.spLoadByPrimaryKey = "proc_MembershipLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MembershipMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
