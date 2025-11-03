/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/7/2023 4:09:58 PM
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
	abstract public class esAppraisalScoringAccumulationCollection : esEntityCollectionWAuditLog
	{
		public esAppraisalScoringAccumulationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppraisalScoringAccumulationCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppraisalScoringAccumulationQuery query)
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
			this.InitQuery(query as esAppraisalScoringAccumulationQuery);
		}
		#endregion

		virtual public AppraisalScoringAccumulation DetachEntity(AppraisalScoringAccumulation entity)
		{
			return base.DetachEntity(entity) as AppraisalScoringAccumulation;
		}

		virtual public AppraisalScoringAccumulation AttachEntity(AppraisalScoringAccumulation entity)
		{
			return base.AttachEntity(entity) as AppraisalScoringAccumulation;
		}

		virtual public void Combine(AppraisalScoringAccumulationCollection collection)
		{
			base.Combine(collection);
		}

		new public AppraisalScoringAccumulation this[int index]
		{
			get
			{
				return base[index] as AppraisalScoringAccumulation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppraisalScoringAccumulation);
		}
	}

	[Serializable]
	abstract public class esAppraisalScoringAccumulation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppraisalScoringAccumulationQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppraisalScoringAccumulation()
		{
		}

		public esAppraisalScoringAccumulation(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 scoringAccumulationID, Int32 participantItemID, Int32 questionerItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoringAccumulationID, participantItemID, questionerItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(scoringAccumulationID, participantItemID, questionerItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 scoringAccumulationID, Int32 participantItemID, Int32 questionerItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(scoringAccumulationID, participantItemID, questionerItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(scoringAccumulationID, participantItemID, questionerItemID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 scoringAccumulationID, Int32 participantItemID, Int32 questionerItemID)
		{
			esAppraisalScoringAccumulationQuery query = this.GetDynamicQuery();
			query.Where(query.ScoringAccumulationID == scoringAccumulationID, query.ParticipantItemID == participantItemID, query.QuestionerItemID == questionerItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 scoringAccumulationID, Int32 participantItemID, Int32 questionerItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ScoringAccumulationID", scoringAccumulationID);
			parms.Add("ParticipantItemID", participantItemID);
			parms.Add("QuestionerItemID", questionerItemID);
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
						case "ScoringAccumulationID": this.str.ScoringAccumulationID = (string)value; break;
						case "ParticipantItemID": this.str.ParticipantItemID = (string)value; break;
						case "QuestionerItemID": this.str.QuestionerItemID = (string)value; break;
						case "SupervisorScore": this.str.SupervisorScore = (string)value; break;
						case "PartnerScore": this.str.PartnerScore = (string)value; break;
						case "SubordinateScore": this.str.SubordinateScore = (string)value; break;
						case "SelfScore": this.str.SelfScore = (string)value; break;
						case "SupervisorScoreIntervention": this.str.SupervisorScoreIntervention = (string)value; break;
						case "PartnerScoreIntervention": this.str.PartnerScoreIntervention = (string)value; break;
						case "SubordinateScoreIntervention": this.str.SubordinateScoreIntervention = (string)value; break;
						case "SelfScoreIntervention": this.str.SelfScoreIntervention = (string)value; break;
						case "AverageScore": this.str.AverageScore = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ScoringAccumulationID":

							if (value == null || value is System.Int32)
								this.ScoringAccumulationID = (System.Int32?)value;
							break;
						case "ParticipantItemID":

							if (value == null || value is System.Int32)
								this.ParticipantItemID = (System.Int32?)value;
							break;
						case "QuestionerItemID":

							if (value == null || value is System.Int32)
								this.QuestionerItemID = (System.Int32?)value;
							break;
						case "SupervisorScore":

							if (value == null || value is System.Decimal)
								this.SupervisorScore = (System.Decimal?)value;
							break;
						case "PartnerScore":

							if (value == null || value is System.Decimal)
								this.PartnerScore = (System.Decimal?)value;
							break;
						case "SubordinateScore":

							if (value == null || value is System.Decimal)
								this.SubordinateScore = (System.Decimal?)value;
							break;
						case "SelfScore":

							if (value == null || value is System.Decimal)
								this.SelfScore = (System.Decimal?)value;
							break;
						case "SupervisorScoreIntervention":

							if (value == null || value is System.Decimal)
								this.SupervisorScoreIntervention = (System.Decimal?)value;
							break;
						case "PartnerScoreIntervention":

							if (value == null || value is System.Decimal)
								this.PartnerScoreIntervention = (System.Decimal?)value;
							break;
						case "SubordinateScoreIntervention":

							if (value == null || value is System.Decimal)
								this.SubordinateScoreIntervention = (System.Decimal?)value;
							break;
						case "SelfScoreIntervention":

							if (value == null || value is System.Decimal)
								this.SelfScoreIntervention = (System.Decimal?)value;
							break;
						case "AverageScore":

							if (value == null || value is System.Decimal)
								this.AverageScore = (System.Decimal?)value;
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
		/// Maps to AppraisalScoringAccumulation.ScoringAccumulationID
		/// </summary>
		virtual public System.Int32? ScoringAccumulationID
		{
			get
			{
				return base.GetSystemInt32(AppraisalScoringAccumulationMetadata.ColumnNames.ScoringAccumulationID);
			}

			set
			{
				base.SetSystemInt32(AppraisalScoringAccumulationMetadata.ColumnNames.ScoringAccumulationID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.ParticipantItemID
		/// </summary>
		virtual public System.Int32? ParticipantItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalScoringAccumulationMetadata.ColumnNames.ParticipantItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalScoringAccumulationMetadata.ColumnNames.ParticipantItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.QuestionerItemID
		/// </summary>
		virtual public System.Int32? QuestionerItemID
		{
			get
			{
				return base.GetSystemInt32(AppraisalScoringAccumulationMetadata.ColumnNames.QuestionerItemID);
			}

			set
			{
				base.SetSystemInt32(AppraisalScoringAccumulationMetadata.ColumnNames.QuestionerItemID, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.SupervisorScore
		/// </summary>
		virtual public System.Decimal? SupervisorScore
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScore);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScore, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.PartnerScore
		/// </summary>
		virtual public System.Decimal? PartnerScore
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScore);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScore, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.SubordinateScore
		/// </summary>
		virtual public System.Decimal? SubordinateScore
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScore);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScore, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.SelfScore
		/// </summary>
		virtual public System.Decimal? SelfScore
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SelfScore);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SelfScore, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.SupervisorScoreIntervention
		/// </summary>
		virtual public System.Decimal? SupervisorScoreIntervention
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScoreIntervention);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScoreIntervention, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.PartnerScoreIntervention
		/// </summary>
		virtual public System.Decimal? PartnerScoreIntervention
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScoreIntervention);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScoreIntervention, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.SubordinateScoreIntervention
		/// </summary>
		virtual public System.Decimal? SubordinateScoreIntervention
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScoreIntervention);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScoreIntervention, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.SelfScoreIntervention
		/// </summary>
		virtual public System.Decimal? SelfScoreIntervention
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SelfScoreIntervention);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.SelfScoreIntervention, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.AverageScore
		/// </summary>
		virtual public System.Decimal? AverageScore
		{
			get
			{
				return base.GetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.AverageScore);
			}

			set
			{
				base.SetSystemDecimal(AppraisalScoringAccumulationMetadata.ColumnNames.AverageScore, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppraisalScoringAccumulation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAppraisalScoringAccumulation entity)
			{
				this.entity = entity;
			}
			public System.String ScoringAccumulationID
			{
				get
				{
					System.Int32? data = entity.ScoringAccumulationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScoringAccumulationID = null;
					else entity.ScoringAccumulationID = Convert.ToInt32(value);
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
			public System.String QuestionerItemID
			{
				get
				{
					System.Int32? data = entity.QuestionerItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionerItemID = null;
					else entity.QuestionerItemID = Convert.ToInt32(value);
				}
			}
			public System.String SupervisorScore
			{
				get
				{
					System.Decimal? data = entity.SupervisorScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorScore = null;
					else entity.SupervisorScore = Convert.ToDecimal(value);
				}
			}
			public System.String PartnerScore
			{
				get
				{
					System.Decimal? data = entity.PartnerScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PartnerScore = null;
					else entity.PartnerScore = Convert.ToDecimal(value);
				}
			}
			public System.String SubordinateScore
			{
				get
				{
					System.Decimal? data = entity.SubordinateScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubordinateScore = null;
					else entity.SubordinateScore = Convert.ToDecimal(value);
				}
			}
			public System.String SelfScore
			{
				get
				{
					System.Decimal? data = entity.SelfScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SelfScore = null;
					else entity.SelfScore = Convert.ToDecimal(value);
				}
			}
			public System.String SupervisorScoreIntervention
			{
				get
				{
					System.Decimal? data = entity.SupervisorScoreIntervention;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorScoreIntervention = null;
					else entity.SupervisorScoreIntervention = Convert.ToDecimal(value);
				}
			}
			public System.String PartnerScoreIntervention
			{
				get
				{
					System.Decimal? data = entity.PartnerScoreIntervention;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PartnerScoreIntervention = null;
					else entity.PartnerScoreIntervention = Convert.ToDecimal(value);
				}
			}
			public System.String SubordinateScoreIntervention
			{
				get
				{
					System.Decimal? data = entity.SubordinateScoreIntervention;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubordinateScoreIntervention = null;
					else entity.SubordinateScoreIntervention = Convert.ToDecimal(value);
				}
			}
			public System.String SelfScoreIntervention
			{
				get
				{
					System.Decimal? data = entity.SelfScoreIntervention;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SelfScoreIntervention = null;
					else entity.SelfScoreIntervention = Convert.ToDecimal(value);
				}
			}
			public System.String AverageScore
			{
				get
				{
					System.Decimal? data = entity.AverageScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AverageScore = null;
					else entity.AverageScore = Convert.ToDecimal(value);
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
			private esAppraisalScoringAccumulation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppraisalScoringAccumulationQuery query)
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
				throw new Exception("esAppraisalScoringAccumulation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppraisalScoringAccumulation : esAppraisalScoringAccumulation
	{
	}

	[Serializable]
	abstract public class esAppraisalScoringAccumulationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppraisalScoringAccumulationMetadata.Meta();
			}
		}

		public esQueryItem ScoringAccumulationID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.ScoringAccumulationID, esSystemType.Int32);
			}
		}

		public esQueryItem ParticipantItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.ParticipantItemID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionerItemID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.QuestionerItemID, esSystemType.Int32);
			}
		}

		public esQueryItem SupervisorScore
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScore, esSystemType.Decimal);
			}
		}

		public esQueryItem PartnerScore
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScore, esSystemType.Decimal);
			}
		}

		public esQueryItem SubordinateScore
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScore, esSystemType.Decimal);
			}
		}

		public esQueryItem SelfScore
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.SelfScore, esSystemType.Decimal);
			}
		}

		public esQueryItem SupervisorScoreIntervention
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScoreIntervention, esSystemType.Decimal);
			}
		}

		public esQueryItem PartnerScoreIntervention
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScoreIntervention, esSystemType.Decimal);
			}
		}

		public esQueryItem SubordinateScoreIntervention
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScoreIntervention, esSystemType.Decimal);
			}
		}

		public esQueryItem SelfScoreIntervention
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.SelfScoreIntervention, esSystemType.Decimal);
			}
		}

		public esQueryItem AverageScore
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.AverageScore, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppraisalScoringAccumulationCollection")]
	public partial class AppraisalScoringAccumulationCollection : esAppraisalScoringAccumulationCollection, IEnumerable<AppraisalScoringAccumulation>
	{
		public AppraisalScoringAccumulationCollection()
		{

		}

		public static implicit operator List<AppraisalScoringAccumulation>(AppraisalScoringAccumulationCollection coll)
		{
			List<AppraisalScoringAccumulation> list = new List<AppraisalScoringAccumulation>();

			foreach (AppraisalScoringAccumulation emp in coll)
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
				return AppraisalScoringAccumulationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalScoringAccumulationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppraisalScoringAccumulation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppraisalScoringAccumulation();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppraisalScoringAccumulationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalScoringAccumulationQuery();
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
		public bool Load(AppraisalScoringAccumulationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppraisalScoringAccumulation AddNew()
		{
			AppraisalScoringAccumulation entity = base.AddNewEntity() as AppraisalScoringAccumulation;

			return entity;
		}
		public AppraisalScoringAccumulation FindByPrimaryKey(Int32 scoringAccumulationID, Int32 participantItemID, Int32 questionerItemID)
		{
			return base.FindByPrimaryKey(scoringAccumulationID, participantItemID, questionerItemID) as AppraisalScoringAccumulation;
		}

		#region IEnumerable< AppraisalScoringAccumulation> Members

		IEnumerator<AppraisalScoringAccumulation> IEnumerable<AppraisalScoringAccumulation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppraisalScoringAccumulation;
			}
		}

		#endregion

		private AppraisalScoringAccumulationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppraisalScoringAccumulation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppraisalScoringAccumulation ({ScoringAccumulationID, ParticipantItemID, QuestionerItemID})")]
	[Serializable]
	public partial class AppraisalScoringAccumulation : esAppraisalScoringAccumulation
	{
		public AppraisalScoringAccumulation()
		{
		}

		public AppraisalScoringAccumulation(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppraisalScoringAccumulationMetadata.Meta();
			}
		}

		override protected esAppraisalScoringAccumulationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppraisalScoringAccumulationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppraisalScoringAccumulationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppraisalScoringAccumulationQuery();
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
		public bool Load(AppraisalScoringAccumulationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppraisalScoringAccumulationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppraisalScoringAccumulationQuery : esAppraisalScoringAccumulationQuery
	{
		public AppraisalScoringAccumulationQuery()
		{

		}

		public AppraisalScoringAccumulationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppraisalScoringAccumulationQuery";
		}
	}

	[Serializable]
	public partial class AppraisalScoringAccumulationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppraisalScoringAccumulationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.ScoringAccumulationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.ScoringAccumulationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.ParticipantItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.ParticipantItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.QuestionerItemID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.QuestionerItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScore, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.SupervisorScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScore, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.PartnerScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScore, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.SubordinateScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.SelfScore, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.SelfScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.SupervisorScoreIntervention, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.SupervisorScoreIntervention;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.PartnerScoreIntervention, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.PartnerScoreIntervention;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.SubordinateScoreIntervention, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.SubordinateScoreIntervention;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.SelfScoreIntervention, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.SelfScoreIntervention;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.AverageScore, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.AverageScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppraisalScoringAccumulationMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AppraisalScoringAccumulationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppraisalScoringAccumulationMetadata Meta()
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
			public const string ScoringAccumulationID = "ScoringAccumulationID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string QuestionerItemID = "QuestionerItemID";
			public const string SupervisorScore = "SupervisorScore";
			public const string PartnerScore = "PartnerScore";
			public const string SubordinateScore = "SubordinateScore";
			public const string SelfScore = "SelfScore";
			public const string SupervisorScoreIntervention = "SupervisorScoreIntervention";
			public const string PartnerScoreIntervention = "PartnerScoreIntervention";
			public const string SubordinateScoreIntervention = "SubordinateScoreIntervention";
			public const string SelfScoreIntervention = "SelfScoreIntervention";
			public const string AverageScore = "AverageScore";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ScoringAccumulationID = "ScoringAccumulationID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string QuestionerItemID = "QuestionerItemID";
			public const string SupervisorScore = "SupervisorScore";
			public const string PartnerScore = "PartnerScore";
			public const string SubordinateScore = "SubordinateScore";
			public const string SelfScore = "SelfScore";
			public const string SupervisorScoreIntervention = "SupervisorScoreIntervention";
			public const string PartnerScoreIntervention = "PartnerScoreIntervention";
			public const string SubordinateScoreIntervention = "SubordinateScoreIntervention";
			public const string SelfScoreIntervention = "SelfScoreIntervention";
			public const string AverageScore = "AverageScore";
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
			lock (typeof(AppraisalScoringAccumulationMetadata))
			{
				if (AppraisalScoringAccumulationMetadata.mapDelegates == null)
				{
					AppraisalScoringAccumulationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppraisalScoringAccumulationMetadata.meta == null)
				{
					AppraisalScoringAccumulationMetadata.meta = new AppraisalScoringAccumulationMetadata();
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

				meta.AddTypeMap("ScoringAccumulationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParticipantItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionerItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SupervisorScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PartnerScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SubordinateScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SelfScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SupervisorScoreIntervention", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PartnerScoreIntervention", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SubordinateScoreIntervention", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SelfScoreIntervention", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AverageScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppraisalScoringAccumulation";
				meta.Destination = "AppraisalScoringAccumulation";
				meta.spInsert = "proc_AppraisalScoringAccumulationInsert";
				meta.spUpdate = "proc_AppraisalScoringAccumulationUpdate";
				meta.spDelete = "proc_AppraisalScoringAccumulationDelete";
				meta.spLoadAll = "proc_AppraisalScoringAccumulationLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppraisalScoringAccumulationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppraisalScoringAccumulationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
