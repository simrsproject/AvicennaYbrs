/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/24/2021 12:08:45 PM
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
	abstract public class esBedCollection : esEntityCollectionWAuditLog
	{
		public esBedCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "BedCollection";
		}

		#region Query Logic
		protected void InitQuery(esBedQuery query)
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
			this.InitQuery(query as esBedQuery);
		}
		#endregion

		virtual public Bed DetachEntity(Bed entity)
		{
			return base.DetachEntity(entity) as Bed;
		}

		virtual public Bed AttachEntity(Bed entity)
		{
			return base.AttachEntity(entity) as Bed;
		}

		virtual public void Combine(BedCollection collection)
		{
			base.Combine(collection);
		}

		new public Bed this[int index]
		{
			get
			{
				return base[index] as Bed;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Bed);
		}
	}

	[Serializable]
	abstract public class esBed : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBedQuery GetDynamicQuery()
		{
			return null;
		}

		public esBed()
		{
		}

		public esBed(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String bedID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bedID);
			else
				return LoadByPrimaryKeyStoredProcedure(bedID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bedID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bedID);
			else
				return LoadByPrimaryKeyStoredProcedure(bedID);
		}

		private bool LoadByPrimaryKeyDynamic(String bedID)
		{
			esBedQuery query = this.GetDynamicQuery();
			query.Where(query.BedID == bedID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String bedID)
		{
			esParameters parms = new esParameters();
			parms.Add("BedID", bedID);
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
						case "BedID": this.str.BedID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "SRBedStatus": this.str.SRBedStatus = (string)value; break;
						case "BedStatusUpdatedBy": this.str.BedStatusUpdatedBy = (string)value; break;
						case "IsTemporary": this.str.IsTemporary = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "IsNeedConfirmation": this.str.IsNeedConfirmation = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsRoomIn": this.str.IsRoomIn = (string)value; break;
						case "BookingDateTime": this.str.BookingDateTime = (string)value; break;
						case "IsVisibleTo3rdParty": this.str.IsVisibleTo3rdParty = (string)value; break;
						case "DefaultChargeClassID": this.str.DefaultChargeClassID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsTemporary":

							if (value == null || value is System.Boolean)
								this.IsTemporary = (System.Boolean?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "IsNeedConfirmation":

							if (value == null || value is System.Boolean)
								this.IsNeedConfirmation = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsRoomIn":

							if (value == null || value is System.Boolean)
								this.IsRoomIn = (System.Boolean?)value;
							break;
						case "BookingDateTime":

							if (value == null || value is System.DateTime)
								this.BookingDateTime = (System.DateTime?)value;
							break;
						case "IsVisibleTo3rdParty":

							if (value == null || value is System.Boolean)
								this.IsVisibleTo3rdParty = (System.Boolean?)value;
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
		/// Maps to Bed.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.BedID);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.BedID, value);
			}
		}
		/// <summary>
		/// Maps to Bed.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to Bed.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to Bed.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to Bed.SRBedStatus
		/// </summary>
		virtual public System.String SRBedStatus
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.SRBedStatus);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.SRBedStatus, value);
			}
		}
		/// <summary>
		/// Maps to Bed.BedStatusUpdatedBy
		/// </summary>
		virtual public System.String BedStatusUpdatedBy
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.BedStatusUpdatedBy);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.BedStatusUpdatedBy, value);
			}
		}
		/// <summary>
		/// Maps to Bed.IsTemporary
		/// </summary>
		virtual public System.Boolean? IsTemporary
		{
			get
			{
				return base.GetSystemBoolean(BedMetadata.ColumnNames.IsTemporary);
			}

			set
			{
				base.SetSystemBoolean(BedMetadata.ColumnNames.IsTemporary, value);
			}
		}
		/// <summary>
		/// Maps to Bed.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(BedMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(BedMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to Bed.IsNeedConfirmation
		/// </summary>
		virtual public System.Boolean? IsNeedConfirmation
		{
			get
			{
				return base.GetSystemBoolean(BedMetadata.ColumnNames.IsNeedConfirmation);
			}

			set
			{
				base.SetSystemBoolean(BedMetadata.ColumnNames.IsNeedConfirmation, value);
			}
		}
		/// <summary>
		/// Maps to Bed.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BedMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Bed.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Bed.IsRoomIn
		/// </summary>
		virtual public System.Boolean? IsRoomIn
		{
			get
			{
				return base.GetSystemBoolean(BedMetadata.ColumnNames.IsRoomIn);
			}

			set
			{
				base.SetSystemBoolean(BedMetadata.ColumnNames.IsRoomIn, value);
			}
		}
		/// <summary>
		/// Maps to Bed.BookingDateTime
		/// </summary>
		virtual public System.DateTime? BookingDateTime
		{
			get
			{
				return base.GetSystemDateTime(BedMetadata.ColumnNames.BookingDateTime);
			}

			set
			{
				base.SetSystemDateTime(BedMetadata.ColumnNames.BookingDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Bed.IsVisibleTo3rdParty
		/// </summary>
		virtual public System.Boolean? IsVisibleTo3rdParty
		{
			get
			{
				return base.GetSystemBoolean(BedMetadata.ColumnNames.IsVisibleTo3rdParty);
			}

			set
			{
				base.SetSystemBoolean(BedMetadata.ColumnNames.IsVisibleTo3rdParty, value);
			}
		}
		/// <summary>
		/// Maps to Bed.DefaultChargeClassID
		/// </summary>
		virtual public System.String DefaultChargeClassID
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.DefaultChargeClassID);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.DefaultChargeClassID, value);
			}
		}
		/// <summary>
		/// Maps to Bed.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(BedMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(BedMetadata.ColumnNames.Notes, value);
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
			public esStrings(esBed entity)
			{
				this.entity = entity;
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
			public System.String SRBedStatus
			{
				get
				{
					System.String data = entity.SRBedStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBedStatus = null;
					else entity.SRBedStatus = Convert.ToString(value);
				}
			}
			public System.String BedStatusUpdatedBy
			{
				get
				{
					System.String data = entity.BedStatusUpdatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedStatusUpdatedBy = null;
					else entity.BedStatusUpdatedBy = Convert.ToString(value);
				}
			}
			public System.String IsTemporary
			{
				get
				{
					System.Boolean? data = entity.IsTemporary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTemporary = null;
					else entity.IsTemporary = Convert.ToBoolean(value);
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
			public System.String IsNeedConfirmation
			{
				get
				{
					System.Boolean? data = entity.IsNeedConfirmation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedConfirmation = null;
					else entity.IsNeedConfirmation = Convert.ToBoolean(value);
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
			public System.String IsRoomIn
			{
				get
				{
					System.Boolean? data = entity.IsRoomIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRoomIn = null;
					else entity.IsRoomIn = Convert.ToBoolean(value);
				}
			}
			public System.String BookingDateTime
			{
				get
				{
					System.DateTime? data = entity.BookingDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BookingDateTime = null;
					else entity.BookingDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsVisibleTo3rdParty
			{
				get
				{
					System.Boolean? data = entity.IsVisibleTo3rdParty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisibleTo3rdParty = null;
					else entity.IsVisibleTo3rdParty = Convert.ToBoolean(value);
				}
			}
			public System.String DefaultChargeClassID
			{
				get
				{
					System.String data = entity.DefaultChargeClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DefaultChargeClassID = null;
					else entity.DefaultChargeClassID = Convert.ToString(value);
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
			private esBed entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBedQuery query)
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
				throw new Exception("esBed can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Bed : esBed
	{
	}

	[Serializable]
	abstract public class esBedQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return BedMetadata.Meta();
			}
		}

		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.BedID, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem SRBedStatus
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.SRBedStatus, esSystemType.String);
			}
		}

		public esQueryItem BedStatusUpdatedBy
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.BedStatusUpdatedBy, esSystemType.String);
			}
		}

		public esQueryItem IsTemporary
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.IsTemporary, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNeedConfirmation
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.IsNeedConfirmation, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRoomIn
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.IsRoomIn, esSystemType.Boolean);
			}
		}

		public esQueryItem BookingDateTime
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.BookingDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsVisibleTo3rdParty
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.IsVisibleTo3rdParty, esSystemType.Boolean);
			}
		}

		public esQueryItem DefaultChargeClassID
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.DefaultChargeClassID, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, BedMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BedCollection")]
	public partial class BedCollection : esBedCollection, IEnumerable<Bed>
	{
		public BedCollection()
		{

		}

		public static implicit operator List<Bed>(BedCollection coll)
		{
			List<Bed> list = new List<Bed>();

			foreach (Bed emp in coll)
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
				return BedMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Bed(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Bed();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BedQuery();
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
		public bool Load(BedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Bed AddNew()
		{
			Bed entity = base.AddNewEntity() as Bed;

			return entity;
		}
		public Bed FindByPrimaryKey(String bedID)
		{
			return base.FindByPrimaryKey(bedID) as Bed;
		}

		#region IEnumerable< Bed> Members

		IEnumerator<Bed> IEnumerable<Bed>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Bed;
			}
		}

		#endregion

		private BedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Bed' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Bed ({BedID})")]
	[Serializable]
	public partial class Bed : esBed
	{
		public Bed()
		{
		}

		public Bed(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BedMetadata.Meta();
			}
		}

		override protected esBedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BedQuery();
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
		public bool Load(BedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BedQuery : esBedQuery
	{
		public BedQuery()
		{

		}

		public BedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BedQuery";
		}
	}

	[Serializable]
	public partial class BedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BedMetadata.ColumnNames.BedID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.BedID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.RoomID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.ClassID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.SRBedStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.SRBedStatus;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.BedStatusUpdatedBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.BedStatusUpdatedBy;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.IsTemporary, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BedMetadata.PropertyNames.IsTemporary;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BedMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.IsNeedConfirmation, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BedMetadata.PropertyNames.IsNeedConfirmation;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.IsRoomIn, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BedMetadata.PropertyNames.IsRoomIn;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.BookingDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BedMetadata.PropertyNames.BookingDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.IsVisibleTo3rdParty, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BedMetadata.PropertyNames.IsVisibleTo3rdParty;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.DefaultChargeClassID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.DefaultChargeClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BedMetadata.ColumnNames.Notes, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = BedMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BedMetadata Meta()
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
			public const string BedID = "BedID";
			public const string RoomID = "RoomID";
			public const string RegistrationNo = "RegistrationNo";
			public const string ClassID = "ClassID";
			public const string SRBedStatus = "SRBedStatus";
			public const string BedStatusUpdatedBy = "BedStatusUpdatedBy";
			public const string IsTemporary = "IsTemporary";
			public const string IsActive = "IsActive";
			public const string IsNeedConfirmation = "IsNeedConfirmation";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsRoomIn = "IsRoomIn";
			public const string BookingDateTime = "BookingDateTime";
			public const string IsVisibleTo3rdParty = "IsVisibleTo3rdParty";
			public const string DefaultChargeClassID = "DefaultChargeClassID";
			public const string Notes = "Notes";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string BedID = "BedID";
			public const string RoomID = "RoomID";
			public const string RegistrationNo = "RegistrationNo";
			public const string ClassID = "ClassID";
			public const string SRBedStatus = "SRBedStatus";
			public const string BedStatusUpdatedBy = "BedStatusUpdatedBy";
			public const string IsTemporary = "IsTemporary";
			public const string IsActive = "IsActive";
			public const string IsNeedConfirmation = "IsNeedConfirmation";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsRoomIn = "IsRoomIn";
			public const string BookingDateTime = "BookingDateTime";
			public const string IsVisibleTo3rdParty = "IsVisibleTo3rdParty";
			public const string DefaultChargeClassID = "DefaultChargeClassID";
			public const string Notes = "Notes";
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
			lock (typeof(BedMetadata))
			{
				if (BedMetadata.mapDelegates == null)
				{
					BedMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BedMetadata.meta == null)
				{
					BedMetadata.meta = new BedMetadata();
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

				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBedStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedStatusUpdatedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTemporary", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNeedConfirmation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRoomIn", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BookingDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsVisibleTo3rdParty", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DefaultChargeClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));


				meta.Source = "Bed";
				meta.Destination = "Bed";
				meta.spInsert = "proc_BedInsert";
				meta.spUpdate = "proc_BedUpdate";
				meta.spDelete = "proc_BedDelete";
				meta.spLoadAll = "proc_BedLoadAll";
				meta.spLoadByPrimaryKey = "proc_BedLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
