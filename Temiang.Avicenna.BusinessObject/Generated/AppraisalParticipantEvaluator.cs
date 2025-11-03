/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/25/2022 7:07:11 PM
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
	abstract public class esAppraisalParticipantEvaluatorCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalParticipantEvaluatorCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalParticipantEvaluatorCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantEvaluatorQuery query)
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
			this.InitQuery(query as esAppraisalParticipantEvaluatorQuery);
		}
		#endregion

		virtual public AppraisalParticipantEvaluator DetachEntity(AppraisalParticipantEvaluator entity)
		{
			return base.DetachEntity(entity) as AppraisalParticipantEvaluator;
		}

		virtual public AppraisalParticipantEvaluator AttachEntity(AppraisalParticipantEvaluator entity)
		{
			return base.AttachEntity(entity) as AppraisalParticipantEvaluator;
		}

		virtual public void Combine(AppraisalParticipantEvaluatorCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalParticipantEvaluator this[int index]
		{
			get
			{
				return base[index] as AppraisalParticipantEvaluator;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalParticipantEvaluator);
		}
	}

	[Serializable]
	abstract public class esAppraisalParticipantEvaluator : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalParticipantEvaluatorQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalParticipantEvaluator()
		{
		}

		public esAppraisalParticipantEvaluator(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 participantEvaluatorID, Int32 participantItemID, Int32 evaluatorID, String sREvaluatorType)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantEvaluatorID, participantItemID, evaluatorID, sREvaluatorType);
			else
				return LoadByPrimaryKeyStoredProcedure(participantEvaluatorID, participantItemID, evaluatorID, sREvaluatorType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 participantEvaluatorID, Int32 participantItemID, Int32 evaluatorID, String sREvaluatorType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(participantEvaluatorID, participantItemID, evaluatorID, sREvaluatorType);
			else
				return LoadByPrimaryKeyStoredProcedure(participantEvaluatorID, participantItemID, evaluatorID, sREvaluatorType);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 participantEvaluatorID, Int32 participantItemID, Int32 evaluatorID, String sREvaluatorType)
		{
			esAppraisalParticipantEvaluatorQuery query = this.GetDynamicQuery();
			query.Where(query.ParticipantEvaluatorID == participantEvaluatorID, query.ParticipantItemID == participantItemID, query.EvaluatorID == evaluatorID, query.SREvaluatorType == sREvaluatorType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 participantEvaluatorID, Int32 participantItemID, Int32 evaluatorID, String sREvaluatorType)
		{
			esParameters parms = new esParameters();
			parms.Add("ParticipantEvaluatorID", participantEvaluatorID);
			parms.Add("ParticipantItemID", participantItemID);
			parms.Add("EvaluatorID", evaluatorID);
			parms.Add("SREvaluatorType", sREvaluatorType);
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
						case "ParticipantEvaluatorID": this.str.ParticipantEvaluatorID = (string)value; break;
						case "ParticipantItemID": this.str.ParticipantItemID = (string)value; break;
						case "EvaluatorID": this.str.EvaluatorID = (string)value; break;
						case "SREvaluatorType": this.str.SREvaluatorType = (string)value; break;
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
						case "ParticipantEvaluatorID":

							if (value == null || value is System.Int32)
								this.ParticipantEvaluatorID = (System.Int32?)value;
							break;
						case "ParticipantItemID":

							if (value == null || value is System.Int32)
								this.ParticipantItemID = (System.Int32?)value;
							break;
						case "EvaluatorID":

							if (value == null || value is System.Int32)
								this.EvaluatorID = (System.Int32?)value;
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
		/// Maps to AppraisalParticipantEvaluator.ParticipantEvaluatorID
		/// </summary>
		virtual public System.Int32? ParticipantEvaluatorID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantEvaluatorID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantEvaluatorID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.ParticipantItemID
		/// </summary>
		virtual public System.Int32? ParticipantItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.EvaluatorID
		/// </summary>
		virtual public System.Int32? EvaluatorID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.EvaluatorID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.EvaluatorID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.SREvaluatorType
		/// </summary>
		virtual public System.String SREvaluatorType
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantEvaluatorMetadata.ColumnNames.SREvaluatorType);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantEvaluatorMetadata.ColumnNames.SREvaluatorType, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.PositionValidFromDate
		/// </summary>
		virtual public System.DateTime? PositionValidFromDate
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionValidFromDate);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionValidFromDate, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.SubOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantEvaluatorMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantEvaluatorMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalParticipantEvaluator.SubDivisonID
		/// </summary>
		virtual public System.Int32? SubDivisonID
		{
			get
			{
				return base.GetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.SubDivisonID);
			}

			set
			{
				base.SetSystemInt32(AppraisalParticipantEvaluatorMetadata.ColumnNames.SubDivisonID, value);
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
			public esStrings(esAppraisalParticipantEvaluator entity)
			{
				this.entity = entity;
			}
			public System.String ParticipantEvaluatorID
			{
				get
				{
					System.Int32? data = entity.ParticipantEvaluatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantEvaluatorID = null;
					else entity.ParticipantEvaluatorID = Convert.ToInt32(value);
				}
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
			public System.String EvaluatorID
			{
				get
				{
					System.Int32? data = entity.EvaluatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EvaluatorID = null;
					else entity.EvaluatorID = Convert.ToInt32(value);
				}
			}
			public System.String SREvaluatorType
			{
				get
				{
					System.String data = entity.SREvaluatorType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREvaluatorType = null;
					else entity.SREvaluatorType = Convert.ToString(value);
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
			private esAppraisalParticipantEvaluator entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalParticipantEvaluatorQuery query)
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
				throw new Exception("esAppraisalParticipantEvaluator can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalParticipantEvaluator : esAppraisalParticipantEvaluator
	{
	}

	[Serializable]
	abstract public class esAppraisalParticipantEvaluatorQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantEvaluatorMetadata.Meta();
			}
		}

		public esQueryItem ParticipantEvaluatorID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantEvaluatorID, esSystemType.Int32);
			}
		}

		public esQueryItem ParticipantItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantItemID, esSystemType.Int32);
			}
		}

		public esQueryItem EvaluatorID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.EvaluatorID, esSystemType.Int32);
			}
		}

		public esQueryItem SREvaluatorType
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.SREvaluatorType, esSystemType.String);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionValidFromDate
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionValidFromDate, esSystemType.DateTime);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SubDivisonID
		{
			get
			{
				return new esQueryItem(this, AppraisalParticipantEvaluatorMetadata.ColumnNames.SubDivisonID, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalParticipantEvaluatorCollection")]
	public partial class AppraisalParticipantEvaluatorCollection : esAppraisalParticipantEvaluatorCollection, IEnumerable<AppraisalParticipantEvaluator>
	{
		public AppraisalParticipantEvaluatorCollection()
		{

		}

		public static implicit operator List<AppraisalParticipantEvaluator>(AppraisalParticipantEvaluatorCollection coll)
		{
			List<AppraisalParticipantEvaluator> list = new List<AppraisalParticipantEvaluator>();

			foreach (AppraisalParticipantEvaluator emp in coll)
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
				return AppraisalParticipantEvaluatorMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalParticipantEvaluator(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalParticipantEvaluator();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantEvaluatorQuery();
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
		public bool Load(AppraisalParticipantEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalParticipantEvaluator AddNew()
		{
			AppraisalParticipantEvaluator entity = base.AddNewEntity() as AppraisalParticipantEvaluator;

			return entity;
		}
		public AppraisalParticipantEvaluator FindByPrimaryKey(Int32 participantEvaluatorID, Int32 participantItemID, Int32 evaluatorID, String sREvaluatorType)
		{
			return base.FindByPrimaryKey(participantEvaluatorID, participantItemID, evaluatorID, sREvaluatorType) as AppraisalParticipantEvaluator;
		}

		#region IEnumerable< AppraisalParticipantEvaluator> Members

		IEnumerator<AppraisalParticipantEvaluator> IEnumerable<AppraisalParticipantEvaluator>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalParticipantEvaluator;
			}
		}

		#endregion

		private AppraisalParticipantEvaluatorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalParticipantEvaluator' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalParticipantEvaluator ({ParticipantEvaluatorID, ParticipantItemID, EvaluatorID, SREvaluatorType})")]
	[Serializable]
	public partial class AppraisalParticipantEvaluator : esAppraisalParticipantEvaluator
	{
		public AppraisalParticipantEvaluator()
		{
		}

		public AppraisalParticipantEvaluator(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalParticipantEvaluatorMetadata.Meta();
			}
		}

		override protected esAppraisalParticipantEvaluatorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalParticipantEvaluatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalParticipantEvaluatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalParticipantEvaluatorQuery();
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
		public bool Load(AppraisalParticipantEvaluatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalParticipantEvaluatorQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalParticipantEvaluatorQuery : esAppraisalParticipantEvaluatorQuery
	{
		public AppraisalParticipantEvaluatorQuery()
		{

		}

		public AppraisalParticipantEvaluatorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalParticipantEvaluatorQuery";
		}
	}

	[Serializable]
	public partial class AppraisalParticipantEvaluatorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalParticipantEvaluatorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantEvaluatorID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.ParticipantEvaluatorID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.ParticipantItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.ParticipantItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.EvaluatorID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.EvaluatorID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.SREvaluatorType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.SREvaluatorType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.PositionValidFromDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.PositionValidFromDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.OrganizationUnitID, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.SubOrganizationUnitID, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.ServiceUnitID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalParticipantEvaluatorMetadata.ColumnNames.SubDivisonID, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalParticipantEvaluatorMetadata.PropertyNames.SubDivisonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalParticipantEvaluatorMetadata Meta()
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
			public const string ParticipantEvaluatorID = "ParticipantEvaluatorID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string EvaluatorID = "EvaluatorID";
			public const string SREvaluatorType = "SREvaluatorType";
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
			public const string ParticipantEvaluatorID = "ParticipantEvaluatorID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string EvaluatorID = "EvaluatorID";
			public const string SREvaluatorType = "SREvaluatorType";
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
			lock (typeof(AppraisalParticipantEvaluatorMetadata))
			{
				if (AppraisalParticipantEvaluatorMetadata.mapDelegates == null)
				{
					AppraisalParticipantEvaluatorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalParticipantEvaluatorMetadata.meta == null)
				{
					AppraisalParticipantEvaluatorMetadata.meta = new AppraisalParticipantEvaluatorMetadata();
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

				meta.AddTypeMap("ParticipantEvaluatorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParticipantItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EvaluatorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREvaluatorType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionValidFromDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubDivisonID", new esTypeMap("int", "System.Int32"));


				meta.Source = "AppraisalParticipantEvaluator";
				meta.Destination = "AppraisalParticipantEvaluator";
				meta.spInsert = "proc_AppraisalParticipantEvaluatorInsert";
				meta.spUpdate = "proc_AppraisalParticipantEvaluatorUpdate";
				meta.spDelete = "proc_AppraisalParticipantEvaluatorDelete";
				meta.spLoadAll = "proc_AppraisalParticipantEvaluatorLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalParticipantEvaluatorLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalParticipantEvaluatorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
