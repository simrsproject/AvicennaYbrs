/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/2/2022 2:59:16 PM
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
	abstract public class esAppraisalParticipantCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalParticipantCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalParticipantCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantQuery query)
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
			this.InitQuery(query as esAppraisalParticipantQuery);
		}
		#endregion

		virtual public AppraisalParticipant DetachEntity(AppraisalParticipant entity)
		{
			return base.DetachEntity(entity) as AppraisalParticipant;
		}

		virtual public AppraisalParticipant AttachEntity(AppraisalParticipant entity)
		{
			return base.AttachEntity(entity) as AppraisalParticipant;
		}

		virtual public void Combine(AppraisalParticipantCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalParticipant this[int index]
		{
			get
			{
				return base[index] as AppraisalParticipant;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalParticipant);
		}
	}

	[Serializable]
	abstract public class esAppraisalParticipant : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalParticipantQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalParticipant()
		{
		}

		public esAppraisalParticipant(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 participantID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantID);
			else
				return LoadByPrimaryKeyStoredProcedure(participantID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 participantID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantID);
			else
				return LoadByPrimaryKeyStoredProcedure(participantID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 participantID)
		{
			esAppraisalParticipantQuery query = this.GetDynamicQuery();
			query.Where(query.ParticipantID == participantID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 participantID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParticipantID", participantID);
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
						case "ParticipantID": this.str.ParticipantID = (string)value; break;
						case "ParticipantName": this.str.ParticipantName = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsScoringRecapitulation": this.str.IsScoringRecapitulation = (string)value; break;
						case "SREmployeeType": this.str.SREmployeeType = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "SRAppraisalType": this.str.SRAppraisalType = (string)value; break;
						case "SRQuarterPeriod": this.str.SRQuarterPeriod = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ParticipantID":

							if (value == null || value is System.Int32)
								this.ParticipantID = (System.Int32?)value;
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
						case "IsScoringRecapitulation":

							if (value == null || value is System.Boolean)
								this.IsScoringRecapitulation = (System.Boolean?)value;
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
		/// Maps to AppraisalParticipant.ParticipantID
		/// </summary>
		virtual public System.Int32? ParticipantID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantMetadata.ColumnNames.ParticipantID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantMetadata.ColumnNames.ParticipantID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.ParticipantName
		/// </summary>
		virtual public System.String ParticipantName
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.ParticipantName);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.ParticipantName, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.PeriodYear);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(AppraisalParticipantMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(AppraisalParticipantMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(AppraisalParticipantMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(AppraisalParticipantMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.IsScoringRecapitulation
		/// </summary>
		virtual public System.Boolean? IsScoringRecapitulation
		{
			get
			{
				return base.GetSystemBoolean(AppraisalParticipantMetadata.ColumnNames.IsScoringRecapitulation);
			}

			set
			{
				base.SetSystemBoolean(AppraisalParticipantMetadata.ColumnNames.IsScoringRecapitulation, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.SREmployeeType
		/// </summary>
		virtual public System.String SREmployeeType
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.SREmployeeType);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.SREmployeeType, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.SRAppraisalType
		/// </summary>
		virtual public System.String SRAppraisalType
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.SRAppraisalType);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.SRAppraisalType, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipant.SRQuarterPeriod
		/// </summary>
		virtual public System.String SRQuarterPeriod
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantMetadata.ColumnNames.SRQuarterPeriod);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantMetadata.ColumnNames.SRQuarterPeriod, value);
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
			public esStrings(esAppraisalParticipant entity)
			{
				this.entity = entity;
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
			public System.String ParticipantName
			{
				get
				{
					System.String data = entity.ParticipantName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantName = null;
					else entity.ParticipantName = Convert.ToString(value);
				}
			}
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
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
			public System.String IsScoringRecapitulation
			{
				get
				{
					System.Boolean? data = entity.IsScoringRecapitulation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScoringRecapitulation = null;
					else entity.IsScoringRecapitulation = Convert.ToBoolean(value);
				}
			}
			public System.String SREmployeeType
			{
				get
				{
					System.String data = entity.SREmployeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeType = null;
					else entity.SREmployeeType = Convert.ToString(value);
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
			public System.String SRAppraisalType
			{
				get
				{
					System.String data = entity.SRAppraisalType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAppraisalType = null;
					else entity.SRAppraisalType = Convert.ToString(value);
				}
			}
			public System.String SRQuarterPeriod
			{
				get
				{
					System.String data = entity.SRQuarterPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQuarterPeriod = null;
					else entity.SRQuarterPeriod = Convert.ToString(value);
				}
			}
			private esAppraisalParticipant entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantQuery query)
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
				throw new Exception("esAppraisalParticipant can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalParticipant : esAppraisalParticipant
	{
	}

	[Serializable]
	abstract public class esAppraisalParticipantQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantMetadata.Meta();
			}
		}

		public esQueryItem ParticipantID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.ParticipantID, esSystemType.Int32);
			}
		}

		public esQueryItem ParticipantName
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.ParticipantName, esSystemType.String);
			}
		}

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsScoringRecapitulation
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.IsScoringRecapitulation, esSystemType.Boolean);
			}
		}

		public esQueryItem SREmployeeType
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.SREmployeeType, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem SRAppraisalType
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.SRAppraisalType, esSystemType.String);
			}
		}

		public esQueryItem SRQuarterPeriod
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantMetadata.ColumnNames.SRQuarterPeriod, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalParticipantCollection")]
	public partial class AppraisalParticipantCollection : esAppraisalParticipantCollection, IEnumerable<AppraisalParticipant>
	{
		public AppraisalParticipantCollection()
		{

		}

		public static implicit operator List<AppraisalParticipant>(AppraisalParticipantCollection coll)
		{
			List<AppraisalParticipant> list = new List<AppraisalParticipant>();

			foreach (AppraisalParticipant emp in coll)
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
				return AppraisalParticipantMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalParticipant(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalParticipant();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantQuery();
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
		public bool Load(AppraisalParticipantQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalParticipant AddNew()
		{
			AppraisalParticipant entity = base.AddNewEntity() as AppraisalParticipant;

			return entity;
		}
		public AppraisalParticipant FindByPrimaryKey(Int32 participantID)
		{
			return base.FindByPrimaryKey(participantID) as AppraisalParticipant;
		}

		#region IEnumerable< AppraisalParticipant> Members

		IEnumerator<AppraisalParticipant> IEnumerable<AppraisalParticipant>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalParticipant;
			}
		}

		#endregion

		private AppraisalParticipantQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalParticipant' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalParticipant ({ParticipantID})")]
	[Serializable]
	public partial class AppraisalParticipant : esAppraisalParticipant
	{
		public AppraisalParticipant()
		{
		}

		public AppraisalParticipant(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantMetadata.Meta();
			}
		}

		override protected esAppraisalParticipantQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantQuery();
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
		public bool Load(AppraisalParticipantQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalParticipantQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalParticipantQuery : esAppraisalParticipantQuery
	{
		public AppraisalParticipantQuery()
		{

		}

		public AppraisalParticipantQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalParticipantQuery";
		}
	}

	[Serializable]
	public partial class AppraisalParticipantMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalParticipantMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.ParticipantID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.ParticipantID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.ParticipantName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.ParticipantName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.PeriodYear, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.PeriodYear;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 16;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.IsApproved, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.ApprovedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.ApprovedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.VoidDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.VoidByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.IsScoringRecapitulation, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.IsScoringRecapitulation;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.SREmployeeType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.SREmployeeType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.ServiceUnitID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.SRAppraisalType, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.SRAppraisalType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantMetadata.ColumnNames.SRQuarterPeriod, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantMetadata.PropertyNames.SRQuarterPeriod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalParticipantMetadata Meta()
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
			public const string ParticipantID = "ParticipantID";
			public const string ParticipantName = "ParticipantName";
			public const string PeriodYear = "PeriodYear";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsScoringRecapitulation = "IsScoringRecapitulation";
			public const string SREmployeeType = "SREmployeeType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SRAppraisalType = "SRAppraisalType";
			public const string SRQuarterPeriod = "SRQuarterPeriod";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ParticipantID = "ParticipantID";
			public const string ParticipantName = "ParticipantName";
			public const string PeriodYear = "PeriodYear";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsScoringRecapitulation = "IsScoringRecapitulation";
			public const string SREmployeeType = "SREmployeeType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string SRAppraisalType = "SRAppraisalType";
			public const string SRQuarterPeriod = "SRQuarterPeriod";
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
			lock (typeof(AppraisalParticipantMetadata))
			{
				if (AppraisalParticipantMetadata.mapDelegates == null)
				{
					AppraisalParticipantMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalParticipantMetadata.meta == null)
				{
					AppraisalParticipantMetadata.meta = new AppraisalParticipantMetadata();
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

				meta.AddTypeMap("ParticipantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParticipantName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsScoringRecapitulation", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SREmployeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRAppraisalType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRQuarterPeriod", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppraisalParticipant";
				meta.Destination = "AppraisalParticipant";
				meta.spInsert = "proc_AppraisalParticipantInsert";
				meta.spUpdate = "proc_AppraisalParticipantUpdate";
				meta.spDelete = "proc_AppraisalParticipantDelete";
				meta.spLoadAll = "proc_AppraisalParticipantLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalParticipantLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalParticipantMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
