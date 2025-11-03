/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/25/2022 7:06:45 PM
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
	abstract public class esAppraisalParticipantItemCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalParticipantItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalParticipantItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantItemQuery query)
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
			this.InitQuery(query as esAppraisalParticipantItemQuery);
		}
		#endregion

		virtual public AppraisalParticipantItem DetachEntity(AppraisalParticipantItem entity)
		{
			return base.DetachEntity(entity) as AppraisalParticipantItem;
		}

		virtual public AppraisalParticipantItem AttachEntity(AppraisalParticipantItem entity)
		{
			return base.AttachEntity(entity) as AppraisalParticipantItem;
		}

		virtual public void Combine(AppraisalParticipantItemCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalParticipantItem this[int index]
		{
			get
			{
				return base[index] as AppraisalParticipantItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalParticipantItem);
		}
	}

	[Serializable]
	abstract public class esAppraisalParticipantItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalParticipantItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalParticipantItem()
		{
		}

		public esAppraisalParticipantItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 participantItemID, Int32 participantID, Int32 employeeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantItemID, participantID, employeeID);
			else
				return LoadByPrimaryKeyStoredProcedure(participantItemID, participantID, employeeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 participantItemID, Int32 participantID, Int32 employeeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantItemID, participantID, employeeID);
			else
				return LoadByPrimaryKeyStoredProcedure(participantItemID, participantID, employeeID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 participantItemID, Int32 participantID, Int32 employeeID)
		{
			esAppraisalParticipantItemQuery query = this.GetDynamicQuery();
			query.Where(query.ParticipantItemID == participantItemID, query.ParticipantID == participantID, query.EmployeeID == employeeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 participantItemID, Int32 participantID, Int32 employeeID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParticipantItemID", participantItemID);
			parms.Add("ParticipantID", participantID);
			parms.Add("EmployeeID", employeeID);
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
						case "ParticipantItemID": this.str.ParticipantItemID = (string)value; break;
						case "ParticipantID": this.str.ParticipantID = (string)value; break;
						case "EmployeeID": this.str.EmployeeID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ClosedDateTime": this.str.ClosedDateTime = (string)value; break;
						case "ClosedByUserID": this.str.ClosedByUserID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "PositionValidFromDate": this.str.PositionValidFromDate = (string)value; break;
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
						case "SubOrganizationUnitID": this.str.SubOrganizationUnitID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SubDivisonID": this.str.SubDivisonID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ParticipantItemID":

							if (value == null || value is System.Int32)
								this.ParticipantItemID = (System.Int32?)value;
							break;
						case "ParticipantID":

							if (value == null || value is System.Int32)
								this.ParticipantID = (System.Int32?)value;
							break;
						case "EmployeeID":

							if (value == null || value is System.Int32)
								this.EmployeeID = (System.Int32?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "ClosedDateTime":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime = (System.DateTime?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "PositionValidFromDate":

							if (value == null || value is System.DateTime)
								this.PositionValidFromDate = (System.DateTime?)value;
							break;
						case "OrganizationUnitID":

							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						case "SubOrganizationUnitID":

							if (value == null || value is System.Int32)
								this.SubOrganizationUnitID = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SubDivisonID":

							if (value == null || value is System.Int32)
								this.SubDivisonID = (System.Int32?)value;
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
		/// Maps to AppraisalParticipantItem.ParticipantItemID
		/// </summary>
		virtual public System.Int32? ParticipantItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.ParticipantItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.ParticipantItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.ParticipantID
		/// </summary>
		virtual public System.Int32? ParticipantID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.ParticipantID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.ParticipantID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.EmployeeID
		/// </summary>
		virtual public System.Int32? EmployeeID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.EmployeeID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.EmployeeID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(AppraisalParticipantItemMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(AppraisalParticipantItemMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.ClosedDateTime
		/// </summary>
		virtual public System.DateTime? ClosedDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantItemMetadata.ColumnNames.ClosedDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantItemMetadata.ColumnNames.ClosedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.ClosedByUserID
		/// </summary>
		virtual public System.String ClosedByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantItemMetadata.ColumnNames.ClosedByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantItemMetadata.ColumnNames.ClosedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.PositionValidFromDate
		/// </summary>
		virtual public System.DateTime? PositionValidFromDate
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantItemMetadata.ColumnNames.PositionValidFromDate);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantItemMetadata.ColumnNames.PositionValidFromDate, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.SubOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantItemMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantItemMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantItem.SubDivisonID
		/// </summary>
		virtual public System.Int32? SubDivisonID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.SubDivisonID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantItemMetadata.ColumnNames.SubDivisonID, value);
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
			public esStrings(esAppraisalParticipantItem entity)
			{
				this.entity = entity;
			}
			public System.String ParticipantItemID
			{
				get
				{
					System.Int32? data = entity.ParticipantItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantItemID = null;
					else entity.ParticipantItemID = Convert.ToInt32(value);
				}
			}
			public System.String ParticipantID
			{
				get
				{
					System.Int32? data = entity.ParticipantID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantID = null;
					else entity.ParticipantID = Convert.ToInt32(value);
				}
			}
			public System.String EmployeeID
			{
				get
				{
					System.Int32? data = entity.EmployeeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeID = null;
					else entity.EmployeeID = Convert.ToInt32(value);
				}
			}
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime = null;
					else entity.ClosedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedByUserID
			{
				get
				{
					System.String data = entity.ClosedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedByUserID = null;
					else entity.ClosedByUserID = Convert.ToString(value);
				}
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String PositionValidFromDate
			{
				get
				{
					System.DateTime? data = entity.PositionValidFromDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionValidFromDate = null;
					else entity.PositionValidFromDate = Convert.ToDateTime(value);
				}
			}
			public System.String OrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.OrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
					else entity.OrganizationUnitID = Convert.ToInt32(value);
				}
			}
			public System.String SubOrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.SubOrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubOrganizationUnitID = null;
					else entity.SubOrganizationUnitID = Convert.ToInt32(value);
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
			public System.String SubDivisonID
			{
				get
				{
					System.Int32? data = entity.SubDivisonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubDivisonID = null;
					else entity.SubDivisonID = Convert.ToInt32(value);
				}
			}
			private esAppraisalParticipantItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantItemQuery query)
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
				throw new Exception("esAppraisalParticipantItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalParticipantItem : esAppraisalParticipantItem
	{
	}

	[Serializable]
	abstract public class esAppraisalParticipantItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantItemMetadata.Meta();
			}
		}

		public esQueryItem ParticipantItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.ParticipantItemID, esSystemType.Int32);
			}
		}

		public esQueryItem ParticipantID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.ParticipantID, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.EmployeeID, esSystemType.Int32);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.ClosedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.ClosedByUserID, esSystemType.String);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionValidFromDate
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.PositionValidFromDate, esSystemType.DateTime);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SubDivisonID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantItemMetadata.ColumnNames.SubDivisonID, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalParticipantItemCollection")]
	public partial class AppraisalParticipantItemCollection : esAppraisalParticipantItemCollection, IEnumerable<AppraisalParticipantItem>
	{
		public AppraisalParticipantItemCollection()
		{

		}

		public static implicit operator List<AppraisalParticipantItem>(AppraisalParticipantItemCollection coll)
		{
			List<AppraisalParticipantItem> list = new List<AppraisalParticipantItem>();

			foreach (AppraisalParticipantItem emp in coll)
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
				return AppraisalParticipantItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalParticipantItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalParticipantItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantItemQuery();
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
		public bool Load(AppraisalParticipantItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalParticipantItem AddNew()
		{
			AppraisalParticipantItem entity = base.AddNewEntity() as AppraisalParticipantItem;

			return entity;
		}
		public AppraisalParticipantItem FindByPrimaryKey(Int32 participantItemID, Int32 participantID, Int32 employeeID)
		{
			return base.FindByPrimaryKey(participantItemID, participantID, employeeID) as AppraisalParticipantItem;
		}

		#region IEnumerable< AppraisalParticipantItem> Members

		IEnumerator<AppraisalParticipantItem> IEnumerable<AppraisalParticipantItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalParticipantItem;
			}
		}

		#endregion

		private AppraisalParticipantItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalParticipantItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalParticipantItem ({ParticipantItemID, ParticipantID, EmployeeID})")]
	[Serializable]
	public partial class AppraisalParticipantItem : esAppraisalParticipantItem
	{
		public AppraisalParticipantItem()
		{
		}

		public AppraisalParticipantItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantItemMetadata.Meta();
			}
		}

		override protected esAppraisalParticipantItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantItemQuery();
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
		public bool Load(AppraisalParticipantItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalParticipantItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalParticipantItemQuery : esAppraisalParticipantItemQuery
	{
		public AppraisalParticipantItemQuery()
		{

		}

		public AppraisalParticipantItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalParticipantItemQuery";
		}
	}

	[Serializable]
	public partial class AppraisalParticipantItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalParticipantItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.ParticipantItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.ParticipantItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.ParticipantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.ParticipantID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.EmployeeID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.EmployeeID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.IsClosed, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.ClosedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.ClosedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.ClosedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.ClosedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.PositionID, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.PositionValidFromDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.PositionValidFromDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.OrganizationUnitID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.SubOrganizationUnitID, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.ServiceUnitID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantItemMetadata.ColumnNames.SubDivisonID, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantItemMetadata.PropertyNames.SubDivisonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalParticipantItemMetadata Meta()
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
			public const string ParticipantItemID = "ParticipantItemID";
			public const string ParticipantID = "ParticipantID";
			public const string EmployeeID = "EmployeeID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string PositionID = "PositionID";
			public const string PositionValidFromDate = "PositionValidFromDate";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SubDivisonID = "SubDivisonID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ParticipantItemID = "ParticipantItemID";
			public const string ParticipantID = "ParticipantID";
			public const string EmployeeID = "EmployeeID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string PositionID = "PositionID";
			public const string PositionValidFromDate = "PositionValidFromDate";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SubDivisonID = "SubDivisonID";
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
			lock (typeof(AppraisalParticipantItemMetadata))
			{
				if (AppraisalParticipantItemMetadata.mapDelegates == null)
				{
					AppraisalParticipantItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalParticipantItemMetadata.meta == null)
				{
					AppraisalParticipantItemMetadata.meta = new AppraisalParticipantItemMetadata();
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

				meta.AddTypeMap("ParticipantItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParticipantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionValidFromDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubDivisonID", new esTypeMap("int", "System.Int32"));


				meta.Source = "AppraisalParticipantItem";
				meta.Destination = "AppraisalParticipantItem";
				meta.spInsert = "proc_AppraisalParticipantItemInsert";
				meta.spUpdate = "proc_AppraisalParticipantItemUpdate";
				meta.spDelete = "proc_AppraisalParticipantItemDelete";
				meta.spLoadAll = "proc_AppraisalParticipantItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalParticipantItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalParticipantItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
