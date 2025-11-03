/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/10/2021 2:50:34 PM
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
	abstract public class esEmployeePermissionCollection : esEntityCollectionWAuditLog
	{
		public esEmployeePermissionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeePermissionCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeePermissionQuery query)
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
			this.InitQuery(query as esEmployeePermissionQuery);
		}
		#endregion

		virtual public EmployeePermission DetachEntity(EmployeePermission entity)
		{
			return base.DetachEntity(entity) as EmployeePermission;
		}

		virtual public EmployeePermission AttachEntity(EmployeePermission entity)
		{
			return base.AttachEntity(entity) as EmployeePermission;
		}

		virtual public void Combine(EmployeePermissionCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeePermission this[int index]
		{
			get
			{
				return base[index] as EmployeePermission;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeePermission);
		}
	}

	[Serializable]
	abstract public class esEmployeePermission : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeePermissionQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeePermission()
		{
		}

		public esEmployeePermission(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 permissionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(permissionID);
			else
				return LoadByPrimaryKeyStoredProcedure(permissionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 permissionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(permissionID);
			else
				return LoadByPrimaryKeyStoredProcedure(permissionID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 permissionID)
		{
			esEmployeePermissionQuery query = this.GetDynamicQuery();
			query.Where(query.PermissionID == permissionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 permissionID)
		{
			esParameters parms = new esParameters();
			parms.Add("PermissionID", permissionID);
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
						case "PermissionID": this.str.PermissionID = (string)value; break;
						case "PermissionDate": this.str.PermissionDate = (string)value; break;
						case "SupervisorID": this.str.SupervisorID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRPermissionType": this.str.SRPermissionType = (string)value; break;
						case "PermissionDateFrom": this.str.PermissionDateFrom = (string)value; break;
						case "PermissionDateTo": this.str.PermissionDateTo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsPayCut": this.str.IsPayCut = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsVerified": this.str.IsVerified = (string)value; break;
						case "VerifiedDateTime": this.str.VerifiedDateTime = (string)value; break;
						case "VerifiedByUserID": this.str.VerifiedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "PermissionTimeFrom": this.str.PermissionTimeFrom = (string)value; break;
						case "PermissionTimeTo": this.str.PermissionTimeTo = (string)value; break;
						case "PayCutDays": this.str.PayCutDays = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PermissionID":

							if (value == null || value is System.Int64)
								this.PermissionID = (System.Int64?)value;
							break;
						case "PermissionDate":

							if (value == null || value is System.DateTime)
								this.PermissionDate = (System.DateTime?)value;
							break;
						case "SupervisorID":

							if (value == null || value is System.Int32)
								this.SupervisorID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PermissionDateFrom":

							if (value == null || value is System.DateTime)
								this.PermissionDateFrom = (System.DateTime?)value;
							break;
						case "PermissionDateTo":

							if (value == null || value is System.DateTime)
								this.PermissionDateTo = (System.DateTime?)value;
							break;
						case "IsPayCut":

							if (value == null || value is System.Boolean)
								this.IsPayCut = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "IsVerified":

							if (value == null || value is System.Boolean)
								this.IsVerified = (System.Boolean?)value;
							break;
						case "VerifiedDateTime":

							if (value == null || value is System.DateTime)
								this.VerifiedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "PayCutDays":

							if (value == null || value is System.Int32)
								this.PayCutDays = (System.Int32?)value;
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
		/// Maps to EmployeePermission.PermissionID
		/// </summary>
		virtual public System.Int64? PermissionID
		{
			get
			{
				return base.GetSystemInt64(EmployeePermissionMetadata.ColumnNames.PermissionID);
			}

			set
			{
				base.SetSystemInt64(EmployeePermissionMetadata.ColumnNames.PermissionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.PermissionDate
		/// </summary>
		virtual public System.DateTime? PermissionDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.PermissionDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.PermissionDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.SupervisorID
		/// </summary>
		virtual public System.Int32? SupervisorID
		{
			get
			{
				return base.GetSystemInt32(EmployeePermissionMetadata.ColumnNames.SupervisorID);
			}

			set
			{
				base.SetSystemInt32(EmployeePermissionMetadata.ColumnNames.SupervisorID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeePermissionMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeePermissionMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.SRPermissionType
		/// </summary>
		virtual public System.String SRPermissionType
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.SRPermissionType);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.SRPermissionType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.PermissionDateFrom
		/// </summary>
		virtual public System.DateTime? PermissionDateFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.PermissionDateFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.PermissionDateFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.PermissionDateTo
		/// </summary>
		virtual public System.DateTime? PermissionDateTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.PermissionDateTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.PermissionDateTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.IsPayCut
		/// </summary>
		virtual public System.Boolean? IsPayCut
		{
			get
			{
				return base.GetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsPayCut);
			}

			set
			{
				base.SetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsPayCut, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.IsVerified
		/// </summary>
		virtual public System.Boolean? IsVerified
		{
			get
			{
				return base.GetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsVerified);
			}

			set
			{
				base.SetSystemBoolean(EmployeePermissionMetadata.ColumnNames.IsVerified, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.VerifiedDateTime
		/// </summary>
		virtual public System.DateTime? VerifiedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.VerifiedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.VerifiedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.VerifiedByUserID
		/// </summary>
		virtual public System.String VerifiedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.VerifiedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.VerifiedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePermissionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePermissionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.PermissionTimeFrom
		/// </summary>
		virtual public System.String PermissionTimeFrom
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.PermissionTimeFrom);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.PermissionTimeFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.PermissionTimeTo
		/// </summary>
		virtual public System.String PermissionTimeTo
		{
			get
			{
				return base.GetSystemString(EmployeePermissionMetadata.ColumnNames.PermissionTimeTo);
			}

			set
			{
				base.SetSystemString(EmployeePermissionMetadata.ColumnNames.PermissionTimeTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePermission.PayCutDays
		/// </summary>
		virtual public System.Int32? PayCutDays
		{
			get
			{
				return base.GetSystemInt32(EmployeePermissionMetadata.ColumnNames.PayCutDays);
			}

			set
			{
				base.SetSystemInt32(EmployeePermissionMetadata.ColumnNames.PayCutDays, value);
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
			public esStrings(esEmployeePermission entity)
			{
				this.entity = entity;
			}
			public System.String PermissionID
			{
				get
				{
					System.Int64? data = entity.PermissionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermissionID = null;
					else entity.PermissionID = Convert.ToInt64(value);
				}
			}
			public System.String PermissionDate
			{
				get
				{
					System.DateTime? data = entity.PermissionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermissionDate = null;
					else entity.PermissionDate = Convert.ToDateTime(value);
				}
			}
			public System.String SupervisorID
			{
				get
				{
					System.Int32? data = entity.SupervisorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorID = null;
					else entity.SupervisorID = Convert.ToInt32(value);
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
			public System.String SRPermissionType
			{
				get
				{
					System.String data = entity.SRPermissionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPermissionType = null;
					else entity.SRPermissionType = Convert.ToString(value);
				}
			}
			public System.String PermissionDateFrom
			{
				get
				{
					System.DateTime? data = entity.PermissionDateFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermissionDateFrom = null;
					else entity.PermissionDateFrom = Convert.ToDateTime(value);
				}
			}
			public System.String PermissionDateTo
			{
				get
				{
					System.DateTime? data = entity.PermissionDateTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermissionDateTo = null;
					else entity.PermissionDateTo = Convert.ToDateTime(value);
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
			public System.String IsPayCut
			{
				get
				{
					System.Boolean? data = entity.IsPayCut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPayCut = null;
					else entity.IsPayCut = Convert.ToBoolean(value);
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
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
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
			public System.String IsVerified
			{
				get
				{
					System.Boolean? data = entity.IsVerified;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVerified = null;
					else entity.IsVerified = Convert.ToBoolean(value);
				}
			}
			public System.String VerifiedDateTime
			{
				get
				{
					System.DateTime? data = entity.VerifiedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedDateTime = null;
					else entity.VerifiedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VerifiedByUserID
			{
				get
				{
					System.String data = entity.VerifiedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerifiedByUserID = null;
					else entity.VerifiedByUserID = Convert.ToString(value);
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
			public System.String PermissionTimeFrom
			{
				get
				{
					System.String data = entity.PermissionTimeFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermissionTimeFrom = null;
					else entity.PermissionTimeFrom = Convert.ToString(value);
				}
			}
			public System.String PermissionTimeTo
			{
				get
				{
					System.String data = entity.PermissionTimeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PermissionTimeTo = null;
					else entity.PermissionTimeTo = Convert.ToString(value);
				}
			}
			public System.String PayCutDays
			{
				get
				{
					System.Int32? data = entity.PayCutDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayCutDays = null;
					else entity.PayCutDays = Convert.ToInt32(value);
				}
			}
			private esEmployeePermission entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeePermissionQuery query)
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
				throw new Exception("esEmployeePermission can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeePermission : esEmployeePermission
	{
	}

	[Serializable]
	abstract public class esEmployeePermissionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeePermissionMetadata.Meta();
			}
		}

		public esQueryItem PermissionID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PermissionID, esSystemType.Int64);
			}
		}

		public esQueryItem PermissionDate
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PermissionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SupervisorID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.SupervisorID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRPermissionType
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.SRPermissionType, esSystemType.String);
			}
		}

		public esQueryItem PermissionDateFrom
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PermissionDateFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem PermissionDateTo
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PermissionDateTo, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsPayCut
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.IsPayCut, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVerified
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.IsVerified, esSystemType.Boolean);
			}
		}

		public esQueryItem VerifiedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.VerifiedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VerifiedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.VerifiedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem PermissionTimeFrom
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PermissionTimeFrom, esSystemType.String);
			}
		}

		public esQueryItem PermissionTimeTo
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PermissionTimeTo, esSystemType.String);
			}
		}

		public esQueryItem PayCutDays
		{
			get
			{
				return new esQueryItem(this, EmployeePermissionMetadata.ColumnNames.PayCutDays, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeePermissionCollection")]
	public partial class EmployeePermissionCollection : esEmployeePermissionCollection, IEnumerable<EmployeePermission>
	{
		public EmployeePermissionCollection()
		{

		}

		public static implicit operator List<EmployeePermission>(EmployeePermissionCollection coll)
		{
			List<EmployeePermission> list = new List<EmployeePermission>();

			foreach (EmployeePermission emp in coll)
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
				return EmployeePermissionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePermissionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeePermission(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeePermission();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeePermissionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePermissionQuery();
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
		public bool Load(EmployeePermissionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeePermission AddNew()
		{
			EmployeePermission entity = base.AddNewEntity() as EmployeePermission;

			return entity;
		}
		public EmployeePermission FindByPrimaryKey(Int64 permissionID)
		{
			return base.FindByPrimaryKey(permissionID) as EmployeePermission;
		}

		#region IEnumerable< EmployeePermission> Members

		IEnumerator<EmployeePermission> IEnumerable<EmployeePermission>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeePermission;
			}
		}

		#endregion

		private EmployeePermissionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeePermission' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeePermission ({PermissionID})")]
	[Serializable]
	public partial class EmployeePermission : esEmployeePermission
	{
		public EmployeePermission()
		{
		}

		public EmployeePermission(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeePermissionMetadata.Meta();
			}
		}

		override protected esEmployeePermissionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePermissionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeePermissionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePermissionQuery();
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
		public bool Load(EmployeePermissionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeePermissionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeePermissionQuery : esEmployeePermissionQuery
	{
		public EmployeePermissionQuery()
		{

		}

		public EmployeePermissionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeePermissionQuery";
		}
	}

	[Serializable]
	public partial class EmployeePermissionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeePermissionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PermissionID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PermissionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PermissionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PermissionDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.SupervisorID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.SupervisorID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PersonID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.SRPermissionType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.SRPermissionType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PermissionDateFrom, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PermissionDateFrom;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PermissionDateTo, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PermissionDateTo;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.IsPayCut, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.IsPayCut;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.CreatedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.IsApproved, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.ApprovedDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.ApprovedByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.VoidDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.VoidByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.IsVerified, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.IsVerified;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.VerifiedDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.VerifiedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.VerifiedByUserID, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.VerifiedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.LastUpdateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.LastUpdateByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PermissionTimeFrom, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PermissionTimeFrom;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PermissionTimeTo, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PermissionTimeTo;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePermissionMetadata.ColumnNames.PayCutDays, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePermissionMetadata.PropertyNames.PayCutDays;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeePermissionMetadata Meta()
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
			public const string PermissionID = "PermissionID";
			public const string PermissionDate = "PermissionDate";
			public const string SupervisorID = "SupervisorID";
			public const string PersonID = "PersonID";
			public const string SRPermissionType = "SRPermissionType";
			public const string PermissionDateFrom = "PermissionDateFrom";
			public const string PermissionDateTo = "PermissionDateTo";
			public const string Notes = "Notes";
			public const string IsPayCut = "IsPayCut";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PermissionTimeFrom = "PermissionTimeFrom";
			public const string PermissionTimeTo = "PermissionTimeTo";
			public const string PayCutDays = "PayCutDays";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PermissionID = "PermissionID";
			public const string PermissionDate = "PermissionDate";
			public const string SupervisorID = "SupervisorID";
			public const string PersonID = "PersonID";
			public const string SRPermissionType = "SRPermissionType";
			public const string PermissionDateFrom = "PermissionDateFrom";
			public const string PermissionDateTo = "PermissionDateTo";
			public const string Notes = "Notes";
			public const string IsPayCut = "IsPayCut";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsVerified = "IsVerified";
			public const string VerifiedDateTime = "VerifiedDateTime";
			public const string VerifiedByUserID = "VerifiedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PermissionTimeFrom = "PermissionTimeFrom";
			public const string PermissionTimeTo = "PermissionTimeTo";
			public const string PayCutDays = "PayCutDays";
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
			lock (typeof(EmployeePermissionMetadata))
			{
				if (EmployeePermissionMetadata.mapDelegates == null)
				{
					EmployeePermissionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeePermissionMetadata.meta == null)
				{
					EmployeePermissionMetadata.meta = new EmployeePermissionMetadata();
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

				meta.AddTypeMap("PermissionID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PermissionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SupervisorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPermissionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PermissionDateFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PermissionDateTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPayCut", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVerified", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerifiedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerifiedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PermissionTimeFrom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PermissionTimeTo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PayCutDays", new esTypeMap("int", "System.Int32"));


				meta.Source = "EmployeePermission";
				meta.Destination = "EmployeePermission";
				meta.spInsert = "proc_EmployeePermissionInsert";
				meta.spUpdate = "proc_EmployeePermissionUpdate";
				meta.spDelete = "proc_EmployeePermissionDelete";
				meta.spLoadAll = "proc_EmployeePermissionLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeePermissionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeePermissionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
