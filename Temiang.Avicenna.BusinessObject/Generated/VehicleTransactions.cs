/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/2/2022 6:24:21 PM
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
	abstract public class esVehicleTransactionsCollection : esEntityCollectionWAuditLog
	{
		public esVehicleTransactionsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "VehicleTransactionsCollection";
		}

		#region Query Logic
		protected void InitQuery(esVehicleTransactionsQuery query)
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
			this.InitQuery(query as esVehicleTransactionsQuery);
		}
		#endregion

		virtual public VehicleTransactions DetachEntity(VehicleTransactions entity)
		{
			return base.DetachEntity(entity) as VehicleTransactions;
		}

		virtual public VehicleTransactions AttachEntity(VehicleTransactions entity)
		{
			return base.AttachEntity(entity) as VehicleTransactions;
		}

		virtual public void Combine(VehicleTransactionsCollection collection)
		{
			base.Combine(collection);
		}

		new public VehicleTransactions this[int index]
		{
			get
			{
				return base[index] as VehicleTransactions;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VehicleTransactions);
		}
	}

	[Serializable]
	abstract public class esVehicleTransactions : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVehicleTransactionsQuery GetDynamicQuery()
		{
			return null;
		}

		public esVehicleTransactions()
		{
		}

		public esVehicleTransactions(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esVehicleTransactionsQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "BookingDateTimeStart": this.str.BookingDateTimeStart = (string)value; break;
						case "BookingDateTimeEnd": this.str.BookingDateTimeEnd = (string)value; break;
						case "SRVehicleType": this.str.SRVehicleType = (string)value; break;
						case "Destination": this.str.Destination = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "DistanceInKM": this.str.DistanceInKM = (string)value; break;
						case "VehicleID": this.str.VehicleID = (string)value; break;
						case "DriverID": this.str.DriverID = (string)value; break;
						case "OdometerStart": this.str.OdometerStart = (string)value; break;
						case "OdometerEnd": this.str.OdometerEnd = (string)value; break;
						case "RealizationDateTimeStart": this.str.RealizationDateTimeStart = (string)value; break;
						case "RealizationDateTimeEnd": this.str.RealizationDateTimeEnd = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApproveByUserID": this.str.ApproveByUserID = (string)value; break;
						case "ApproveDateTime": this.str.ApproveDateTime = (string)value; break;
						case "IsConfirmed": this.str.IsConfirmed = (string)value; break;
						case "ConfirmByUserID": this.str.ConfirmByUserID = (string)value; break;
						case "ConfirmDateTime": this.str.ConfirmDateTime = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "IsRealized": this.str.IsRealized = (string)value; break;
						case "RealizationApproveByUserID": this.str.RealizationApproveByUserID = (string)value; break;
						case "RealizationApproveDateTime": this.str.RealizationApproveDateTime = (string)value; break;
						case "IsFromOrder": this.str.IsFromOrder = (string)value; break;
						case "SRVehicleOrderType": this.str.SRVehicleOrderType = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "RealizationNotes": this.str.RealizationNotes = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "BookingDateTimeStart":

							if (value == null || value is System.DateTime)
								this.BookingDateTimeStart = (System.DateTime?)value;
							break;
						case "BookingDateTimeEnd":

							if (value == null || value is System.DateTime)
								this.BookingDateTimeEnd = (System.DateTime?)value;
							break;
						case "DistanceInKM":

							if (value == null || value is System.Decimal)
								this.DistanceInKM = (System.Decimal?)value;
							break;
						case "VehicleID":

							if (value == null || value is System.Int32)
								this.VehicleID = (System.Int32?)value;
							break;
						case "DriverID":

							if (value == null || value is System.Int32)
								this.DriverID = (System.Int32?)value;
							break;
						case "OdometerStart":

							if (value == null || value is System.Decimal)
								this.OdometerStart = (System.Decimal?)value;
							break;
						case "OdometerEnd":

							if (value == null || value is System.Decimal)
								this.OdometerEnd = (System.Decimal?)value;
							break;
						case "RealizationDateTimeStart":

							if (value == null || value is System.DateTime)
								this.RealizationDateTimeStart = (System.DateTime?)value;
							break;
						case "RealizationDateTimeEnd":

							if (value == null || value is System.DateTime)
								this.RealizationDateTimeEnd = (System.DateTime?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApproveDateTime":

							if (value == null || value is System.DateTime)
								this.ApproveDateTime = (System.DateTime?)value;
							break;
						case "IsConfirmed":

							if (value == null || value is System.Boolean)
								this.IsConfirmed = (System.Boolean?)value;
							break;
						case "ConfirmDateTime":

							if (value == null || value is System.DateTime)
								this.ConfirmDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "IsRealized":

							if (value == null || value is System.Boolean)
								this.IsRealized = (System.Boolean?)value;
							break;
						case "RealizationApproveDateTime":

							if (value == null || value is System.DateTime)
								this.RealizationApproveDateTime = (System.DateTime?)value;
							break;
						case "IsFromOrder":

							if (value == null || value is System.Boolean)
								this.IsFromOrder = (System.Boolean?)value;
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
		/// Maps to VehicleTransactions.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.BookingDateTimeStart
		/// </summary>
		virtual public System.DateTime? BookingDateTimeStart
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.BookingDateTimeStart);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.BookingDateTimeStart, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.BookingDateTimeEnd
		/// </summary>
		virtual public System.DateTime? BookingDateTimeEnd
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.BookingDateTimeEnd);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.BookingDateTimeEnd, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.SRVehicleType
		/// </summary>
		virtual public System.String SRVehicleType
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.SRVehicleType);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.SRVehicleType, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.Destination
		/// </summary>
		virtual public System.String Destination
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.Destination);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.Destination, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.DistanceInKM
		/// </summary>
		virtual public System.Decimal? DistanceInKM
		{
			get
			{
				return base.GetSystemDecimal(VehicleTransactionsMetadata.ColumnNames.DistanceInKM);
			}

			set
			{
				base.SetSystemDecimal(VehicleTransactionsMetadata.ColumnNames.DistanceInKM, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.VehicleID
		/// </summary>
		virtual public System.Int32? VehicleID
		{
			get
			{
				return base.GetSystemInt32(VehicleTransactionsMetadata.ColumnNames.VehicleID);
			}

			set
			{
				base.SetSystemInt32(VehicleTransactionsMetadata.ColumnNames.VehicleID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.DriverID
		/// </summary>
		virtual public System.Int32? DriverID
		{
			get
			{
				return base.GetSystemInt32(VehicleTransactionsMetadata.ColumnNames.DriverID);
			}

			set
			{
				base.SetSystemInt32(VehicleTransactionsMetadata.ColumnNames.DriverID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.OdometerStart
		/// </summary>
		virtual public System.Decimal? OdometerStart
		{
			get
			{
				return base.GetSystemDecimal(VehicleTransactionsMetadata.ColumnNames.OdometerStart);
			}

			set
			{
				base.SetSystemDecimal(VehicleTransactionsMetadata.ColumnNames.OdometerStart, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.OdometerEnd
		/// </summary>
		virtual public System.Decimal? OdometerEnd
		{
			get
			{
				return base.GetSystemDecimal(VehicleTransactionsMetadata.ColumnNames.OdometerEnd);
			}

			set
			{
				base.SetSystemDecimal(VehicleTransactionsMetadata.ColumnNames.OdometerEnd, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.RealizationDateTimeStart
		/// </summary>
		virtual public System.DateTime? RealizationDateTimeStart
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeStart);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeStart, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.RealizationDateTimeEnd
		/// </summary>
		virtual public System.DateTime? RealizationDateTimeEnd
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeEnd);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeEnd, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.ApproveByUserID
		/// </summary>
		virtual public System.String ApproveByUserID
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.ApproveByUserID);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.ApproveByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.ApproveDateTime
		/// </summary>
		virtual public System.DateTime? ApproveDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.ApproveDateTime);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.ApproveDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.IsConfirmed
		/// </summary>
		virtual public System.Boolean? IsConfirmed
		{
			get
			{
				return base.GetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsConfirmed);
			}

			set
			{
				base.SetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsConfirmed, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.ConfirmByUserID
		/// </summary>
		virtual public System.String ConfirmByUserID
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.ConfirmByUserID);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.ConfirmByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.ConfirmDateTime
		/// </summary>
		virtual public System.DateTime? ConfirmDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.ConfirmDateTime);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.ConfirmDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.IsRealized
		/// </summary>
		virtual public System.Boolean? IsRealized
		{
			get
			{
				return base.GetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsRealized);
			}

			set
			{
				base.SetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsRealized, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.RealizationApproveByUserID
		/// </summary>
		virtual public System.String RealizationApproveByUserID
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.RealizationApproveByUserID);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.RealizationApproveByUserID, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.RealizationApproveDateTime
		/// </summary>
		virtual public System.DateTime? RealizationApproveDateTime
		{
			get
			{
				return base.GetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.RealizationApproveDateTime);
			}

			set
			{
				base.SetSystemDateTime(VehicleTransactionsMetadata.ColumnNames.RealizationApproveDateTime, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.IsFromOrder
		/// </summary>
		virtual public System.Boolean? IsFromOrder
		{
			get
			{
				return base.GetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsFromOrder);
			}

			set
			{
				base.SetSystemBoolean(VehicleTransactionsMetadata.ColumnNames.IsFromOrder, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.SRVehicleOrderType
		/// </summary>
		virtual public System.String SRVehicleOrderType
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.SRVehicleOrderType);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.SRVehicleOrderType, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to VehicleTransactions.RealizationNotes
		/// </summary>
		virtual public System.String RealizationNotes
		{
			get
			{
				return base.GetSystemString(VehicleTransactionsMetadata.ColumnNames.RealizationNotes);
			}

			set
			{
				base.SetSystemString(VehicleTransactionsMetadata.ColumnNames.RealizationNotes, value);
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
			public esStrings(esVehicleTransactions entity)
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
			public System.String BookingDateTimeStart
			{
				get
				{
					System.DateTime? data = entity.BookingDateTimeStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingDateTimeStart = null;
					else entity.BookingDateTimeStart = Convert.ToDateTime(value);
				}
			}
			public System.String BookingDateTimeEnd
			{
				get
				{
					System.DateTime? data = entity.BookingDateTimeEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingDateTimeEnd = null;
					else entity.BookingDateTimeEnd = Convert.ToDateTime(value);
				}
			}
			public System.String SRVehicleType
			{
				get
				{
					System.String data = entity.SRVehicleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVehicleType = null;
					else entity.SRVehicleType = Convert.ToString(value);
				}
			}
			public System.String Destination
			{
				get
				{
					System.String data = entity.Destination;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Destination = null;
					else entity.Destination = Convert.ToString(value);
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
			public System.String DistanceInKM
			{
				get
				{
					System.Decimal? data = entity.DistanceInKM;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DistanceInKM = null;
					else entity.DistanceInKM = Convert.ToDecimal(value);
				}
			}
			public System.String VehicleID
			{
				get
				{
					System.Int32? data = entity.VehicleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VehicleID = null;
					else entity.VehicleID = Convert.ToInt32(value);
				}
			}
			public System.String DriverID
			{
				get
				{
					System.Int32? data = entity.DriverID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DriverID = null;
					else entity.DriverID = Convert.ToInt32(value);
				}
			}
			public System.String OdometerStart
			{
				get
				{
					System.Decimal? data = entity.OdometerStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OdometerStart = null;
					else entity.OdometerStart = Convert.ToDecimal(value);
				}
			}
			public System.String OdometerEnd
			{
				get
				{
					System.Decimal? data = entity.OdometerEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OdometerEnd = null;
					else entity.OdometerEnd = Convert.ToDecimal(value);
				}
			}
			public System.String RealizationDateTimeStart
			{
				get
				{
					System.DateTime? data = entity.RealizationDateTimeStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationDateTimeStart = null;
					else entity.RealizationDateTimeStart = Convert.ToDateTime(value);
				}
			}
			public System.String RealizationDateTimeEnd
			{
				get
				{
					System.DateTime? data = entity.RealizationDateTimeEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationDateTimeEnd = null;
					else entity.RealizationDateTimeEnd = Convert.ToDateTime(value);
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
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApproveByUserID
			{
				get
				{
					System.String data = entity.ApproveByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproveByUserID = null;
					else entity.ApproveByUserID = Convert.ToString(value);
				}
			}
			public System.String ApproveDateTime
			{
				get
				{
					System.DateTime? data = entity.ApproveDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproveDateTime = null;
					else entity.ApproveDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsConfirmed
			{
				get
				{
					System.Boolean? data = entity.IsConfirmed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsConfirmed = null;
					else entity.IsConfirmed = Convert.ToBoolean(value);
				}
			}
			public System.String ConfirmByUserID
			{
				get
				{
					System.String data = entity.ConfirmByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmByUserID = null;
					else entity.ConfirmByUserID = Convert.ToString(value);
				}
			}
			public System.String ConfirmDateTime
			{
				get
				{
					System.DateTime? data = entity.ConfirmDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmDateTime = null;
					else entity.ConfirmDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsRealized
			{
				get
				{
					System.Boolean? data = entity.IsRealized;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRealized = null;
					else entity.IsRealized = Convert.ToBoolean(value);
				}
			}
			public System.String RealizationApproveByUserID
			{
				get
				{
					System.String data = entity.RealizationApproveByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationApproveByUserID = null;
					else entity.RealizationApproveByUserID = Convert.ToString(value);
				}
			}
			public System.String RealizationApproveDateTime
			{
				get
				{
					System.DateTime? data = entity.RealizationApproveDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationApproveDateTime = null;
					else entity.RealizationApproveDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsFromOrder
			{
				get
				{
					System.Boolean? data = entity.IsFromOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromOrder = null;
					else entity.IsFromOrder = Convert.ToBoolean(value);
				}
			}
			public System.String SRVehicleOrderType
			{
				get
				{
					System.String data = entity.SRVehicleOrderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRVehicleOrderType = null;
					else entity.SRVehicleOrderType = Convert.ToString(value);
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
			public System.String RealizationNotes
			{
				get
				{
					System.String data = entity.RealizationNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealizationNotes = null;
					else entity.RealizationNotes = Convert.ToString(value);
				}
			}
			private esVehicleTransactions entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVehicleTransactionsQuery query)
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
				throw new Exception("esVehicleTransactions can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VehicleTransactions : esVehicleTransactions
	{
	}

	[Serializable]
	abstract public class esVehicleTransactionsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return VehicleTransactionsMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem BookingDateTimeStart
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.BookingDateTimeStart, esSystemType.DateTime);
			}
		}

		public esQueryItem BookingDateTimeEnd
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.BookingDateTimeEnd, esSystemType.DateTime);
			}
		}

		public esQueryItem SRVehicleType
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.SRVehicleType, esSystemType.String);
			}
		}

		public esQueryItem Destination
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.Destination, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem DistanceInKM
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.DistanceInKM, esSystemType.Decimal);
			}
		}

		public esQueryItem VehicleID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.VehicleID, esSystemType.Int32);
			}
		}

		public esQueryItem DriverID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.DriverID, esSystemType.Int32);
			}
		}

		public esQueryItem OdometerStart
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.OdometerStart, esSystemType.Decimal);
			}
		}

		public esQueryItem OdometerEnd
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.OdometerEnd, esSystemType.Decimal);
			}
		}

		public esQueryItem RealizationDateTimeStart
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeStart, esSystemType.DateTime);
			}
		}

		public esQueryItem RealizationDateTimeEnd
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeEnd, esSystemType.DateTime);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApproveByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.ApproveByUserID, esSystemType.String);
			}
		}

		public esQueryItem ApproveDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.ApproveDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsConfirmed
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.IsConfirmed, esSystemType.Boolean);
			}
		}

		public esQueryItem ConfirmByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.ConfirmByUserID, esSystemType.String);
			}
		}

		public esQueryItem ConfirmDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.ConfirmDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsRealized
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.IsRealized, esSystemType.Boolean);
			}
		}

		public esQueryItem RealizationApproveByUserID
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.RealizationApproveByUserID, esSystemType.String);
			}
		}

		public esQueryItem RealizationApproveDateTime
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.RealizationApproveDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsFromOrder
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.IsFromOrder, esSystemType.Boolean);
			}
		}

		public esQueryItem SRVehicleOrderType
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.SRVehicleOrderType, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem RealizationNotes
		{
			get
			{
				return new esQueryItem(this, VehicleTransactionsMetadata.ColumnNames.RealizationNotes, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VehicleTransactionsCollection")]
	public partial class VehicleTransactionsCollection : esVehicleTransactionsCollection, IEnumerable<VehicleTransactions>
	{
		public VehicleTransactionsCollection()
		{

		}

		public static implicit operator List<VehicleTransactions>(VehicleTransactionsCollection coll)
		{
			List<VehicleTransactions> list = new List<VehicleTransactions>();

			foreach (VehicleTransactions emp in coll)
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
				return VehicleTransactionsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VehicleTransactionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VehicleTransactions(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VehicleTransactions();
		}

		#endregion

		[BrowsableAttribute(false)]
		public VehicleTransactionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VehicleTransactionsQuery();
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
		public bool Load(VehicleTransactionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VehicleTransactions AddNew()
		{
			VehicleTransactions entity = base.AddNewEntity() as VehicleTransactions;

			return entity;
		}
		public VehicleTransactions FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as VehicleTransactions;
		}

		#region IEnumerable< VehicleTransactions> Members

		IEnumerator<VehicleTransactions> IEnumerable<VehicleTransactions>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as VehicleTransactions;
			}
		}

		#endregion

		private VehicleTransactionsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VehicleTransactions' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VehicleTransactions ({TransactionNo})")]
	[Serializable]
	public partial class VehicleTransactions : esVehicleTransactions
	{
		public VehicleTransactions()
		{
		}

		public VehicleTransactions(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VehicleTransactionsMetadata.Meta();
			}
		}

		override protected esVehicleTransactionsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VehicleTransactionsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public VehicleTransactionsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VehicleTransactionsQuery();
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
		public bool Load(VehicleTransactionsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private VehicleTransactionsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VehicleTransactionsQuery : esVehicleTransactionsQuery
	{
		public VehicleTransactionsQuery()
		{

		}

		public VehicleTransactionsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "VehicleTransactionsQuery";
		}
	}

	[Serializable]
	public partial class VehicleTransactionsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VehicleTransactionsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.BookingDateTimeStart, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.BookingDateTimeStart;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.BookingDateTimeEnd, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.BookingDateTimeEnd;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.SRVehicleType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.SRVehicleType;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.Destination, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.Destination;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.ServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.DistanceInKM, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.DistanceInKM;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.VehicleID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.VehicleID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.DriverID, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.DriverID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.OdometerStart, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.OdometerStart;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.OdometerEnd, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.OdometerEnd;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeStart, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.RealizationDateTimeStart;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.RealizationDateTimeEnd, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.RealizationDateTimeEnd;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.CreateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.CreateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.IsApproved, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.IsApproved;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.ApproveByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.ApproveByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.ApproveDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.ApproveDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.IsConfirmed, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.IsConfirmed;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.ConfirmByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.ConfirmByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.ConfirmDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.ConfirmDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.IsVoid, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.IsVoid;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.VoidByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.VoidDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.IsRealized, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.IsRealized;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.RealizationApproveByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.RealizationApproveByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.RealizationApproveDateTime, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.RealizationApproveDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.IsFromOrder, 30, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.IsFromOrder;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.SRVehicleOrderType, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.SRVehicleOrderType;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.RegistrationNo, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VehicleTransactionsMetadata.ColumnNames.RealizationNotes, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = VehicleTransactionsMetadata.PropertyNames.RealizationNotes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public VehicleTransactionsMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string BookingDateTimeStart = "BookingDateTimeStart";
			public const string BookingDateTimeEnd = "BookingDateTimeEnd";
			public const string SRVehicleType = "SRVehicleType";
			public const string Destination = "Destination";
			public const string Notes = "Notes";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DistanceInKM = "DistanceInKM";
			public const string VehicleID = "VehicleID";
			public const string DriverID = "DriverID";
			public const string OdometerStart = "OdometerStart";
			public const string OdometerEnd = "OdometerEnd";
			public const string RealizationDateTimeStart = "RealizationDateTimeStart";
			public const string RealizationDateTimeEnd = "RealizationDateTimeEnd";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApproved = "IsApproved";
			public const string ApproveByUserID = "ApproveByUserID";
			public const string ApproveDateTime = "ApproveDateTime";
			public const string IsConfirmed = "IsConfirmed";
			public const string ConfirmByUserID = "ConfirmByUserID";
			public const string ConfirmDateTime = "ConfirmDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string IsRealized = "IsRealized";
			public const string RealizationApproveByUserID = "RealizationApproveByUserID";
			public const string RealizationApproveDateTime = "RealizationApproveDateTime";
			public const string IsFromOrder = "IsFromOrder";
			public const string SRVehicleOrderType = "SRVehicleOrderType";
			public const string RegistrationNo = "RegistrationNo";
			public const string RealizationNotes = "RealizationNotes";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string BookingDateTimeStart = "BookingDateTimeStart";
			public const string BookingDateTimeEnd = "BookingDateTimeEnd";
			public const string SRVehicleType = "SRVehicleType";
			public const string Destination = "Destination";
			public const string Notes = "Notes";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DistanceInKM = "DistanceInKM";
			public const string VehicleID = "VehicleID";
			public const string DriverID = "DriverID";
			public const string OdometerStart = "OdometerStart";
			public const string OdometerEnd = "OdometerEnd";
			public const string RealizationDateTimeStart = "RealizationDateTimeStart";
			public const string RealizationDateTimeEnd = "RealizationDateTimeEnd";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsApproved = "IsApproved";
			public const string ApproveByUserID = "ApproveByUserID";
			public const string ApproveDateTime = "ApproveDateTime";
			public const string IsConfirmed = "IsConfirmed";
			public const string ConfirmByUserID = "ConfirmByUserID";
			public const string ConfirmDateTime = "ConfirmDateTime";
			public const string IsVoid = "IsVoid";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string IsRealized = "IsRealized";
			public const string RealizationApproveByUserID = "RealizationApproveByUserID";
			public const string RealizationApproveDateTime = "RealizationApproveDateTime";
			public const string IsFromOrder = "IsFromOrder";
			public const string SRVehicleOrderType = "SRVehicleOrderType";
			public const string RegistrationNo = "RegistrationNo";
			public const string RealizationNotes = "RealizationNotes";
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
			lock (typeof(VehicleTransactionsMetadata))
			{
				if (VehicleTransactionsMetadata.mapDelegates == null)
				{
					VehicleTransactionsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (VehicleTransactionsMetadata.meta == null)
				{
					VehicleTransactionsMetadata.meta = new VehicleTransactionsMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BookingDateTimeStart", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BookingDateTimeEnd", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRVehicleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Destination", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DistanceInKM", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("VehicleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DriverID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OdometerStart", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("OdometerEnd", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("RealizationDateTimeStart", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RealizationDateTimeEnd", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApproveByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ApproveDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsConfirmed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ConfirmByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConfirmDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsRealized", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RealizationApproveByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationApproveDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsFromOrder", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRVehicleOrderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RealizationNotes", new esTypeMap("varchar", "System.String"));


				meta.Source = "VehicleTransactions";
				meta.Destination = "VehicleTransactions";
				meta.spInsert = "proc_VehicleTransactionsInsert";
				meta.spUpdate = "proc_VehicleTransactionsUpdate";
				meta.spDelete = "proc_VehicleTransactionsDelete";
				meta.spLoadAll = "proc_VehicleTransactionsLoadAll";
				meta.spLoadByPrimaryKey = "proc_VehicleTransactionsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VehicleTransactionsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
