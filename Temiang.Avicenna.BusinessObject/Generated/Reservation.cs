/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/23/2020 8:49:51 AM
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
	abstract public class esReservationCollection : esEntityCollectionWAuditLog
	{
		public esReservationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ReservationCollection";
		}

		#region Query Logic
		protected void InitQuery(esReservationQuery query)
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
			this.InitQuery(query as esReservationQuery);
		}
		#endregion

		virtual public Reservation DetachEntity(Reservation entity)
		{
			return base.DetachEntity(entity) as Reservation;
		}

		virtual public Reservation AttachEntity(Reservation entity)
		{
			return base.AttachEntity(entity) as Reservation;
		}

		virtual public void Combine(ReservationCollection collection)
		{
			base.Combine(collection);
		}

		new public Reservation this[int index]
		{
			get
			{
				return base[index] as Reservation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Reservation);
		}
	}

	[Serializable]
	abstract public class esReservation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esReservationQuery GetDynamicQuery()
		{
			return null;
		}

		public esReservation()
		{
		}

		public esReservation(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String reservationNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reservationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(reservationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String reservationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reservationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(reservationNo);
		}

		private bool LoadByPrimaryKeyDynamic(String reservationNo)
		{
			esReservationQuery query = this.GetDynamicQuery();
			query.Where(query.ReservationNo == reservationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String reservationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ReservationNo", reservationNo);
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
						case "ReservationNo": this.str.ReservationNo = (string)value; break;
						case "ReservationDate": this.str.ReservationDate = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "DepartmentID": this.str.DepartmentID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "BedID": this.str.BedID = (string)value; break;
						case "SRReservationStatus": this.str.SRReservationStatus = (string)value; break;
						case "FollowUpDateTime": this.str.FollowUpDateTime = (string)value; break;
						case "FollowUpByUserID": this.str.FollowUpByUserID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "FirstName": this.str.FirstName = (string)value; break;
						case "MiddleName": this.str.MiddleName = (string)value; break;
						case "LastName": this.str.LastName = (string)value; break;
						case "StreetName": this.str.StreetName = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "State": this.str.State = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "PhoneNo": this.str.PhoneNo = (string)value; break;
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;
						case "FaxNo": this.str.FaxNo = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;
						case "FromRoomID": this.str.FromRoomID = (string)value; break;
						case "FromBedID": this.str.FromBedID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReservationDate":

							if (value == null || value is System.DateTime)
								this.ReservationDate = (System.DateTime?)value;
							break;
						case "FollowUpDateTime":

							if (value == null || value is System.DateTime)
								this.FollowUpDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to Reservation.ReservationNo
		/// </summary>
		virtual public System.String ReservationNo
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.ReservationNo);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.ReservationNo, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.ReservationDate
		/// </summary>
		virtual public System.DateTime? ReservationDate
		{
			get
			{
				return base.GetSystemDateTime(ReservationMetadata.ColumnNames.ReservationDate);
			}

			set
			{
				base.SetSystemDateTime(ReservationMetadata.ColumnNames.ReservationDate, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.DepartmentID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.DepartmentID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.SRReservationStatus
		/// </summary>
		virtual public System.String SRReservationStatus
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.SRReservationStatus);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.SRReservationStatus, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.FollowUpDateTime
		/// </summary>
		virtual public System.DateTime? FollowUpDateTime
		{
			get
			{
				return base.GetSystemDateTime(ReservationMetadata.ColumnNames.FollowUpDateTime);
			}

			set
			{
				base.SetSystemDateTime(ReservationMetadata.ColumnNames.FollowUpDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.FollowUpByUserID
		/// </summary>
		virtual public System.String FollowUpByUserID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.FollowUpByUserID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.FollowUpByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ReservationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ReservationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.FirstName
		/// </summary>
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.FirstName);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.FirstName, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.MiddleName
		/// </summary>
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.MiddleName);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.MiddleName, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.LastName
		/// </summary>
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.LastName);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.LastName, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.StreetName);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.StreetName, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.State
		/// </summary>
		virtual public System.String State
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.State);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.State, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.PhoneNo);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.PhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.MobilePhoneNo);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.FaxNo);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.FaxNo, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.Email);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ReservationMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ReservationMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.FromServiceUnitID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.FromRoomID
		/// </summary>
		virtual public System.String FromRoomID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.FromRoomID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.FromRoomID, value);
			}
		}
		/// <summary>
		/// Maps to Reservation.FromBedID
		/// </summary>
		virtual public System.String FromBedID
		{
			get
			{
				return base.GetSystemString(ReservationMetadata.ColumnNames.FromBedID);
			}

			set
			{
				base.SetSystemString(ReservationMetadata.ColumnNames.FromBedID, value);
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
			public esStrings(esReservation entity)
			{
				this.entity = entity;
			}
			public System.String ReservationNo
			{
				get
				{
					System.String data = entity.ReservationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReservationNo = null;
					else entity.ReservationNo = Convert.ToString(value);
				}
			}
			public System.String ReservationDate
			{
				get
				{
					System.DateTime? data = entity.ReservationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReservationDate = null;
					else entity.ReservationDate = Convert.ToDateTime(value);
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
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
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
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			public System.String SRReservationStatus
			{
				get
				{
					System.String data = entity.SRReservationStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReservationStatus = null;
					else entity.SRReservationStatus = Convert.ToString(value);
				}
			}
			public System.String FollowUpDateTime
			{
				get
				{
					System.DateTime? data = entity.FollowUpDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FollowUpDateTime = null;
					else entity.FollowUpDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String FollowUpByUserID
			{
				get
				{
					System.String data = entity.FollowUpByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FollowUpByUserID = null;
					else entity.FollowUpByUserID = Convert.ToString(value);
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
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			public System.String FromRoomID
			{
				get
				{
					System.String data = entity.FromRoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRoomID = null;
					else entity.FromRoomID = Convert.ToString(value);
				}
			}
			public System.String FromBedID
			{
				get
				{
					System.String data = entity.FromBedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromBedID = null;
					else entity.FromBedID = Convert.ToString(value);
				}
			}
			private esReservation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esReservationQuery query)
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
				throw new Exception("esReservation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Reservation : esReservation
	{
	}

	[Serializable]
	abstract public class esReservationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ReservationMetadata.Meta();
			}
		}

		public esQueryItem ReservationNo
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.ReservationNo, esSystemType.String);
			}
		}

		public esQueryItem ReservationDate
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.ReservationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem SRReservationStatus
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.SRReservationStatus, esSystemType.String);
			}
		}

		public esQueryItem FollowUpDateTime
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.FollowUpDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem FollowUpByUserID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.FollowUpByUserID, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem FirstName
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.FirstName, esSystemType.String);
			}
		}

		public esQueryItem MiddleName
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.MiddleName, esSystemType.String);
			}
		}

		public esQueryItem LastName
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.LastName, esSystemType.String);
			}
		}

		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem State
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.State, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		}

		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		}

		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		}

		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.Email, esSystemType.String);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem FromRoomID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.FromRoomID, esSystemType.String);
			}
		}

		public esQueryItem FromBedID
		{
			get
			{
				return new esQueryItem(this, ReservationMetadata.ColumnNames.FromBedID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ReservationCollection")]
	public partial class ReservationCollection : esReservationCollection, IEnumerable<Reservation>
	{
		public ReservationCollection()
		{

		}

		public static implicit operator List<Reservation>(ReservationCollection coll)
		{
			List<Reservation> list = new List<Reservation>();

			foreach (Reservation emp in coll)
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
				return ReservationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReservationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Reservation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Reservation();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ReservationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReservationQuery();
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
		public bool Load(ReservationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Reservation AddNew()
		{
			Reservation entity = base.AddNewEntity() as Reservation;

			return entity;
		}
		public Reservation FindByPrimaryKey(String reservationNo)
		{
			return base.FindByPrimaryKey(reservationNo) as Reservation;
		}

		#region IEnumerable< Reservation> Members

		IEnumerator<Reservation> IEnumerable<Reservation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Reservation;
			}
		}

		#endregion

		private ReservationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Reservation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Reservation ({ReservationNo})")]
	[Serializable]
	public partial class Reservation : esReservation
	{
		public Reservation()
		{
		}

		public Reservation(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ReservationMetadata.Meta();
			}
		}

		override protected esReservationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReservationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ReservationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReservationQuery();
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
		public bool Load(ReservationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ReservationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ReservationQuery : esReservationQuery
	{
		public ReservationQuery()
		{

		}

		public ReservationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ReservationQuery";
		}
	}

	[Serializable]
	public partial class ReservationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ReservationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.ReservationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.ReservationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.ReservationDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReservationMetadata.PropertyNames.ReservationDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.PatientID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.DepartmentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.RoomID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.ClassID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.BedID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.SRReservationStatus, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.SRReservationStatus;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.FollowUpDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReservationMetadata.PropertyNames.FollowUpDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.FollowUpByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.FollowUpByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.Notes, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReservationMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.FirstName, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.MiddleName, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.LastName, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.StreetName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.District, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.City, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.County, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.State, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.State;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.ZipCode, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.PhoneNo, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.MobilePhoneNo, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.FaxNo, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.Email, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.CreatedByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.CreatedDateTime, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReservationMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.RegistrationNo, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.FromServiceUnitID, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.FromRoomID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.FromRoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReservationMetadata.ColumnNames.FromBedID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = ReservationMetadata.PropertyNames.FromBedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ReservationMetadata Meta()
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
			public const string ReservationNo = "ReservationNo";
			public const string ReservationDate = "ReservationDate";
			public const string PatientID = "PatientID";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string ClassID = "ClassID";
			public const string BedID = "BedID";
			public const string SRReservationStatus = "SRReservationStatus";
			public const string FollowUpDateTime = "FollowUpDateTime";
			public const string FollowUpByUserID = "FollowUpByUserID";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string RegistrationNo = "RegistrationNo";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string FromBedID = "FromBedID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ReservationNo = "ReservationNo";
			public const string ReservationDate = "ReservationDate";
			public const string PatientID = "PatientID";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string ClassID = "ClassID";
			public const string BedID = "BedID";
			public const string SRReservationStatus = "SRReservationStatus";
			public const string FollowUpDateTime = "FollowUpDateTime";
			public const string FollowUpByUserID = "FollowUpByUserID";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string StreetName = "StreetName";
			public const string District = "District";
			public const string City = "City";
			public const string County = "County";
			public const string State = "State";
			public const string ZipCode = "ZipCode";
			public const string PhoneNo = "PhoneNo";
			public const string MobilePhoneNo = "MobilePhoneNo";
			public const string FaxNo = "FaxNo";
			public const string Email = "Email";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string RegistrationNo = "RegistrationNo";
			public const string FromServiceUnitID = "FromServiceUnitID";
			public const string FromRoomID = "FromRoomID";
			public const string FromBedID = "FromBedID";
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
			lock (typeof(ReservationMetadata))
			{
				if (ReservationMetadata.mapDelegates == null)
				{
					ReservationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ReservationMetadata.meta == null)
				{
					ReservationMetadata.meta = new ReservationMetadata();
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

				meta.AddTypeMap("ReservationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReservationDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReservationStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FollowUpDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FollowUpByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FirstName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("State", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromBedID", new esTypeMap("varchar", "System.String"));


				meta.Source = "Reservation";
				meta.Destination = "Reservation";
				meta.spInsert = "proc_ReservationInsert";
				meta.spUpdate = "proc_ReservationUpdate";
				meta.spDelete = "proc_ReservationDelete";
				meta.spLoadAll = "proc_ReservationLoadAll";
				meta.spLoadByPrimaryKey = "proc_ReservationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ReservationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
