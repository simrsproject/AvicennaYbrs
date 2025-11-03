/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 4:22:33 PM
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
	abstract public class esCredentialInvitationCollection : esEntityCollectionWAuditLog
	{
		public esCredentialInvitationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialInvitationCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialInvitationQuery query)
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
			this.InitQuery(query as esCredentialInvitationQuery);
		}
		#endregion

		virtual public CredentialInvitation DetachEntity(CredentialInvitation entity)
		{
			return base.DetachEntity(entity) as CredentialInvitation;
		}

		virtual public CredentialInvitation AttachEntity(CredentialInvitation entity)
		{
			return base.AttachEntity(entity) as CredentialInvitation;
		}

		virtual public void Combine(CredentialInvitationCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialInvitation this[int index]
		{
			get
			{
				return base[index] as CredentialInvitation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialInvitation);
		}
	}

	[Serializable]
	abstract public class esCredentialInvitation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialInvitationQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialInvitation()
		{
		}

		public esCredentialInvitation(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String invitationNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invitationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invitationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invitationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invitationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(invitationNo);
		}

		private bool LoadByPrimaryKeyDynamic(String invitationNo)
		{
			esCredentialInvitationQuery query = this.GetDynamicQuery();
			query.Where(query.InvitationNo == invitationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String invitationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("InvitationNo", invitationNo);
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
						case "InvitationNo": this.str.InvitationNo = (string)value; break;
						case "InvitationDate": this.str.InvitationDate = (string)value; break;
						case "SRProfessionGroup": this.str.SRProfessionGroup = (string)value; break;
						case "LetterNo": this.str.LetterNo = (string)value; break;
						case "ScheduleDate": this.str.ScheduleDate = (string)value; break;
						case "CredentialingLocation": this.str.CredentialingLocation = (string)value; break;
						case "ParticipantLetterNo": this.str.ParticipantLetterNo = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "InvitationDate":

							if (value == null || value is System.DateTime)
								this.InvitationDate = (System.DateTime?)value;
							break;
						case "ScheduleDate":

							if (value == null || value is System.DateTime)
								this.ScheduleDate = (System.DateTime?)value;
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
		/// Maps to CredentialInvitation.InvitationNo
		/// </summary>
		virtual public System.String InvitationNo
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.InvitationNo);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.InvitationNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.InvitationDate
		/// </summary>
		virtual public System.DateTime? InvitationDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialInvitationMetadata.ColumnNames.InvitationDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialInvitationMetadata.ColumnNames.InvitationDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.SRProfessionGroup
		/// </summary>
		virtual public System.String SRProfessionGroup
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.SRProfessionGroup);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.SRProfessionGroup, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.LetterNo
		/// </summary>
		virtual public System.String LetterNo
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.LetterNo);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.LetterNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.ScheduleDate
		/// </summary>
		virtual public System.DateTime? ScheduleDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialInvitationMetadata.ColumnNames.ScheduleDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialInvitationMetadata.ColumnNames.ScheduleDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.CredentialingLocation
		/// </summary>
		virtual public System.String CredentialingLocation
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.CredentialingLocation);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.CredentialingLocation, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.ParticipantLetterNo
		/// </summary>
		virtual public System.String ParticipantLetterNo
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.ParticipantLetterNo);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.ParticipantLetterNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CredentialInvitationMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CredentialInvitationMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialInvitationMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialInvitationMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CredentialInvitationMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CredentialInvitationMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialInvitationMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialInvitationMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialInvitationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialInvitationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialInvitationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialInvitationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialInvitation entity)
			{
				this.entity = entity;
			}
			public System.String InvitationNo
			{
				get
				{
					System.String data = entity.InvitationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvitationNo = null;
					else entity.InvitationNo = Convert.ToString(value);
				}
			}
			public System.String InvitationDate
			{
				get
				{
					System.DateTime? data = entity.InvitationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvitationDate = null;
					else entity.InvitationDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRProfessionGroup
			{
				get
				{
					System.String data = entity.SRProfessionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProfessionGroup = null;
					else entity.SRProfessionGroup = Convert.ToString(value);
				}
			}
			public System.String LetterNo
			{
				get
				{
					System.String data = entity.LetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterNo = null;
					else entity.LetterNo = Convert.ToString(value);
				}
			}
			public System.String ScheduleDate
			{
				get
				{
					System.DateTime? data = entity.ScheduleDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleDate = null;
					else entity.ScheduleDate = Convert.ToDateTime(value);
				}
			}
			public System.String CredentialingLocation
			{
				get
				{
					System.String data = entity.CredentialingLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CredentialingLocation = null;
					else entity.CredentialingLocation = Convert.ToString(value);
				}
			}
			public System.String ParticipantLetterNo
			{
				get
				{
					System.String data = entity.ParticipantLetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantLetterNo = null;
					else entity.ParticipantLetterNo = Convert.ToString(value);
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
			private esCredentialInvitation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialInvitationQuery query)
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
				throw new Exception("esCredentialInvitation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialInvitation : esCredentialInvitation
	{
	}

	[Serializable]
	abstract public class esCredentialInvitationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialInvitationMetadata.Meta();
			}
		}

		public esQueryItem InvitationNo
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.InvitationNo, esSystemType.String);
			}
		}

		public esQueryItem InvitationDate
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.InvitationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRProfessionGroup
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.SRProfessionGroup, esSystemType.String);
			}
		}

		public esQueryItem LetterNo
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.LetterNo, esSystemType.String);
			}
		}

		public esQueryItem ScheduleDate
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.ScheduleDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CredentialingLocation
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.CredentialingLocation, esSystemType.String);
			}
		}

		public esQueryItem ParticipantLetterNo
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.ParticipantLetterNo, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialInvitationCollection")]
	public partial class CredentialInvitationCollection : esCredentialInvitationCollection, IEnumerable<CredentialInvitation>
	{
		public CredentialInvitationCollection()
		{

		}

		public static implicit operator List<CredentialInvitation>(CredentialInvitationCollection coll)
		{
			List<CredentialInvitation> list = new List<CredentialInvitation>();

			foreach (CredentialInvitation emp in coll)
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
				return CredentialInvitationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialInvitationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialInvitation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialInvitation();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialInvitationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialInvitationQuery();
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
		public bool Load(CredentialInvitationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialInvitation AddNew()
		{
			CredentialInvitation entity = base.AddNewEntity() as CredentialInvitation;

			return entity;
		}
		public CredentialInvitation FindByPrimaryKey(String invitationNo)
		{
			return base.FindByPrimaryKey(invitationNo) as CredentialInvitation;
		}

		#region IEnumerable< CredentialInvitation> Members

		IEnumerator<CredentialInvitation> IEnumerable<CredentialInvitation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialInvitation;
			}
		}

		#endregion

		private CredentialInvitationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialInvitation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialInvitation ({InvitationNo})")]
	[Serializable]
	public partial class CredentialInvitation : esCredentialInvitation
	{
		public CredentialInvitation()
		{
		}

		public CredentialInvitation(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialInvitationMetadata.Meta();
			}
		}

		override protected esCredentialInvitationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialInvitationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialInvitationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialInvitationQuery();
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
		public bool Load(CredentialInvitationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialInvitationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialInvitationQuery : esCredentialInvitationQuery
	{
		public CredentialInvitationQuery()
		{

		}

		public CredentialInvitationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialInvitationQuery";
		}
	}

	[Serializable]
	public partial class CredentialInvitationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialInvitationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.InvitationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.InvitationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.InvitationDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.InvitationDate;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.SRProfessionGroup, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.SRProfessionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.LetterNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.LetterNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.ScheduleDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.ScheduleDate;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.CredentialingLocation, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.CredentialingLocation;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.ParticipantLetterNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.ParticipantLetterNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.IsApproved, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.ApprovedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.ApprovedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.IsVoid, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.VoidDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.VoidByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialInvitationMetadata Meta()
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
			public const string InvitationNo = "InvitationNo";
			public const string InvitationDate = "InvitationDate";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string LetterNo = "LetterNo";
			public const string ScheduleDate = "ScheduleDate";
			public const string CredentialingLocation = "CredentialingLocation";
			public const string ParticipantLetterNo = "ParticipantLetterNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string InvitationNo = "InvitationNo";
			public const string InvitationDate = "InvitationDate";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string LetterNo = "LetterNo";
			public const string ScheduleDate = "ScheduleDate";
			public const string CredentialingLocation = "CredentialingLocation";
			public const string ParticipantLetterNo = "ParticipantLetterNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(CredentialInvitationMetadata))
			{
				if (CredentialInvitationMetadata.mapDelegates == null)
				{
					CredentialInvitationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialInvitationMetadata.meta == null)
				{
					CredentialInvitationMetadata.meta = new CredentialInvitationMetadata();
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

				meta.AddTypeMap("InvitationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvitationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRProfessionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScheduleDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CredentialingLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParticipantLetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialInvitation";
				meta.Destination = "CredentialInvitation";
				meta.spInsert = "proc_CredentialInvitationInsert";
				meta.spUpdate = "proc_CredentialInvitationUpdate";
				meta.spDelete = "proc_CredentialInvitationDelete";
				meta.spLoadAll = "proc_CredentialInvitationLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialInvitationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialInvitationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
