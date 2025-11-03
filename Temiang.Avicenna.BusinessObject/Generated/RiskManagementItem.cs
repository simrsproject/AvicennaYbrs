/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/27/2022 9:21:51 AM
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
	abstract public class esRiskManagementItemCollection : esEntityCollectionWAuditLog
	{
		public esRiskManagementItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RiskManagementItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esRiskManagementItemQuery query)
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
			this.InitQuery(query as esRiskManagementItemQuery);
		}
		#endregion

		virtual public RiskManagementItem DetachEntity(RiskManagementItem entity)
		{
			return base.DetachEntity(entity) as RiskManagementItem;
		}

		virtual public RiskManagementItem AttachEntity(RiskManagementItem entity)
		{
			return base.AttachEntity(entity) as RiskManagementItem;
		}

		virtual public void Combine(RiskManagementItemCollection collection)
		{
			base.Combine(collection);
		}

		new public RiskManagementItem this[int index]
		{
			get
			{
				return base[index] as RiskManagementItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RiskManagementItem);
		}
	}

	[Serializable]
	abstract public class esRiskManagementItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRiskManagementItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esRiskManagementItem()
		{
		}

		public esRiskManagementItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String riskManagementNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(riskManagementNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(riskManagementNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String riskManagementNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(riskManagementNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(riskManagementNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String riskManagementNo, String sequenceNo)
		{
			esRiskManagementItemQuery query = this.GetDynamicQuery();
			query.Where(query.RiskManagementNo == riskManagementNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String riskManagementNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RiskManagementNo", riskManagementNo);
			parms.Add("SequenceNo", sequenceNo);
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
						case "RiskManagementNo": this.str.RiskManagementNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "SRRiskManagementCategory": this.str.SRRiskManagementCategory = (string)value; break;
						case "RiskManagementDescription": this.str.RiskManagementDescription = (string)value; break;
						case "SRRiskManagementImpact": this.str.SRRiskManagementImpact = (string)value; break;
						case "ImpactScore": this.str.ImpactScore = (string)value; break;
						case "SRRiskManagementProbability": this.str.SRRiskManagementProbability = (string)value; break;
						case "ProbabilityScore": this.str.ProbabilityScore = (string)value; break;
						case "RiskScore": this.str.RiskScore = (string)value; break;
						case "SRRiskManagementBands": this.str.SRRiskManagementBands = (string)value; break;
						case "SRRiskManagementControlling": this.str.SRRiskManagementControlling = (string)value; break;
						case "ControllingScore": this.str.ControllingScore = (string)value; break;
						case "TotalScore": this.str.TotalScore = (string)value; break;
						case "RiskRating": this.str.RiskRating = (string)value; break;
						case "Reason": this.str.Reason = (string)value; break;
						case "Action": this.str.Action = (string)value; break;
						case "Pic": this.str.Pic = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ImpactScore":

							if (value == null || value is System.Int16)
								this.ImpactScore = (System.Int16?)value;
							break;
						case "ProbabilityScore":

							if (value == null || value is System.Int16)
								this.ProbabilityScore = (System.Int16?)value;
							break;
						case "RiskScore":

							if (value == null || value is System.Int16)
								this.RiskScore = (System.Int16?)value;
							break;
						case "ControllingScore":

							if (value == null || value is System.Int16)
								this.ControllingScore = (System.Int16?)value;
							break;
						case "TotalScore":

							if (value == null || value is System.Int16)
								this.TotalScore = (System.Int16?)value;
							break;
						case "RiskRating":

							if (value == null || value is System.Int16)
								this.RiskRating = (System.Int16?)value;
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
		/// Maps to RiskManagementItem.RiskManagementNo
		/// </summary>
		virtual public System.String RiskManagementNo
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.RiskManagementNo);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.RiskManagementNo, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.SRRiskManagementCategory
		/// </summary>
		virtual public System.String SRRiskManagementCategory
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementCategory);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementCategory, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.RiskManagementDescription
		/// </summary>
		virtual public System.String RiskManagementDescription
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.RiskManagementDescription);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.RiskManagementDescription, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.SRRiskManagementImpact
		/// </summary>
		virtual public System.String SRRiskManagementImpact
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementImpact);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementImpact, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.ImpactScore
		/// </summary>
		virtual public System.Int16? ImpactScore
		{
			get
			{
				return base.GetSystemInt16(RiskManagementItemMetadata.ColumnNames.ImpactScore);
			}

			set
			{
				base.SetSystemInt16(RiskManagementItemMetadata.ColumnNames.ImpactScore, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.SRRiskManagementProbability
		/// </summary>
		virtual public System.String SRRiskManagementProbability
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementProbability);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementProbability, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.ProbabilityScore
		/// </summary>
		virtual public System.Int16? ProbabilityScore
		{
			get
			{
				return base.GetSystemInt16(RiskManagementItemMetadata.ColumnNames.ProbabilityScore);
			}

			set
			{
				base.SetSystemInt16(RiskManagementItemMetadata.ColumnNames.ProbabilityScore, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.RiskScore
		/// </summary>
		virtual public System.Int16? RiskScore
		{
			get
			{
				return base.GetSystemInt16(RiskManagementItemMetadata.ColumnNames.RiskScore);
			}

			set
			{
				base.SetSystemInt16(RiskManagementItemMetadata.ColumnNames.RiskScore, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.SRRiskManagementBands
		/// </summary>
		virtual public System.String SRRiskManagementBands
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementBands);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementBands, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.SRRiskManagementControlling
		/// </summary>
		virtual public System.String SRRiskManagementControlling
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementControlling);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.SRRiskManagementControlling, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.ControllingScore
		/// </summary>
		virtual public System.Int16? ControllingScore
		{
			get
			{
				return base.GetSystemInt16(RiskManagementItemMetadata.ColumnNames.ControllingScore);
			}

			set
			{
				base.SetSystemInt16(RiskManagementItemMetadata.ColumnNames.ControllingScore, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.TotalScore
		/// </summary>
		virtual public System.Int16? TotalScore
		{
			get
			{
				return base.GetSystemInt16(RiskManagementItemMetadata.ColumnNames.TotalScore);
			}

			set
			{
				base.SetSystemInt16(RiskManagementItemMetadata.ColumnNames.TotalScore, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.RiskRating
		/// </summary>
		virtual public System.Int16? RiskRating
		{
			get
			{
				return base.GetSystemInt16(RiskManagementItemMetadata.ColumnNames.RiskRating);
			}

			set
			{
				base.SetSystemInt16(RiskManagementItemMetadata.ColumnNames.RiskRating, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.Reason);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.Reason, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.Action
		/// </summary>
		virtual public System.String Action
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.Action);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.Action, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.Pic
		/// </summary>
		virtual public System.String Pic
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.Pic);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.Pic, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RiskManagementItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RiskManagementItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RiskManagementItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RiskManagementItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RiskManagementItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRiskManagementItem entity)
			{
				this.entity = entity;
			}
			public System.String RiskManagementNo
			{
				get
				{
					System.String data = entity.RiskManagementNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskManagementNo = null;
					else entity.RiskManagementNo = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String SRRiskManagementCategory
			{
				get
				{
					System.String data = entity.SRRiskManagementCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRiskManagementCategory = null;
					else entity.SRRiskManagementCategory = Convert.ToString(value);
				}
			}
			public System.String RiskManagementDescription
			{
				get
				{
					System.String data = entity.RiskManagementDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskManagementDescription = null;
					else entity.RiskManagementDescription = Convert.ToString(value);
				}
			}
			public System.String SRRiskManagementImpact
			{
				get
				{
					System.String data = entity.SRRiskManagementImpact;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRiskManagementImpact = null;
					else entity.SRRiskManagementImpact = Convert.ToString(value);
				}
			}
			public System.String ImpactScore
			{
				get
				{
					System.Int16? data = entity.ImpactScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ImpactScore = null;
					else entity.ImpactScore = Convert.ToInt16(value);
				}
			}
			public System.String SRRiskManagementProbability
			{
				get
				{
					System.String data = entity.SRRiskManagementProbability;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRiskManagementProbability = null;
					else entity.SRRiskManagementProbability = Convert.ToString(value);
				}
			}
			public System.String ProbabilityScore
			{
				get
				{
					System.Int16? data = entity.ProbabilityScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProbabilityScore = null;
					else entity.ProbabilityScore = Convert.ToInt16(value);
				}
			}
			public System.String RiskScore
			{
				get
				{
					System.Int16? data = entity.RiskScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskScore = null;
					else entity.RiskScore = Convert.ToInt16(value);
				}
			}
			public System.String SRRiskManagementBands
			{
				get
				{
					System.String data = entity.SRRiskManagementBands;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRiskManagementBands = null;
					else entity.SRRiskManagementBands = Convert.ToString(value);
				}
			}
			public System.String SRRiskManagementControlling
			{
				get
				{
					System.String data = entity.SRRiskManagementControlling;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRiskManagementControlling = null;
					else entity.SRRiskManagementControlling = Convert.ToString(value);
				}
			}
			public System.String ControllingScore
			{
				get
				{
					System.Int16? data = entity.ControllingScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControllingScore = null;
					else entity.ControllingScore = Convert.ToInt16(value);
				}
			}
			public System.String TotalScore
			{
				get
				{
					System.Int16? data = entity.TotalScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalScore = null;
					else entity.TotalScore = Convert.ToInt16(value);
				}
			}
			public System.String RiskRating
			{
				get
				{
					System.Int16? data = entity.RiskRating;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiskRating = null;
					else entity.RiskRating = Convert.ToInt16(value);
				}
			}
			public System.String Reason
			{
				get
				{
					System.String data = entity.Reason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reason = null;
					else entity.Reason = Convert.ToString(value);
				}
			}
			public System.String Action
			{
				get
				{
					System.String data = entity.Action;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action = null;
					else entity.Action = Convert.ToString(value);
				}
			}
			public System.String Pic
			{
				get
				{
					System.String data = entity.Pic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pic = null;
					else entity.Pic = Convert.ToString(value);
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
			private esRiskManagementItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRiskManagementItemQuery query)
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
				throw new Exception("esRiskManagementItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RiskManagementItem : esRiskManagementItem
	{
	}

	[Serializable]
	abstract public class esRiskManagementItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RiskManagementItemMetadata.Meta();
			}
		}

		public esQueryItem RiskManagementNo
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.RiskManagementNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem SRRiskManagementCategory
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.SRRiskManagementCategory, esSystemType.String);
			}
		}

		public esQueryItem RiskManagementDescription
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.RiskManagementDescription, esSystemType.String);
			}
		}

		public esQueryItem SRRiskManagementImpact
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.SRRiskManagementImpact, esSystemType.String);
			}
		}

		public esQueryItem ImpactScore
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.ImpactScore, esSystemType.Int16);
			}
		}

		public esQueryItem SRRiskManagementProbability
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.SRRiskManagementProbability, esSystemType.String);
			}
		}

		public esQueryItem ProbabilityScore
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.ProbabilityScore, esSystemType.Int16);
			}
		}

		public esQueryItem RiskScore
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.RiskScore, esSystemType.Int16);
			}
		}

		public esQueryItem SRRiskManagementBands
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.SRRiskManagementBands, esSystemType.String);
			}
		}

		public esQueryItem SRRiskManagementControlling
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.SRRiskManagementControlling, esSystemType.String);
			}
		}

		public esQueryItem ControllingScore
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.ControllingScore, esSystemType.Int16);
			}
		}

		public esQueryItem TotalScore
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.TotalScore, esSystemType.Int16);
			}
		}

		public esQueryItem RiskRating
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.RiskRating, esSystemType.Int16);
			}
		}

		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.Reason, esSystemType.String);
			}
		}

		public esQueryItem Action
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.Action, esSystemType.String);
			}
		}

		public esQueryItem Pic
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.Pic, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RiskManagementItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RiskManagementItemCollection")]
	public partial class RiskManagementItemCollection : esRiskManagementItemCollection, IEnumerable<RiskManagementItem>
	{
		public RiskManagementItemCollection()
		{

		}

		public static implicit operator List<RiskManagementItem>(RiskManagementItemCollection coll)
		{
			List<RiskManagementItem> list = new List<RiskManagementItem>();

			foreach (RiskManagementItem emp in coll)
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
				return RiskManagementItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RiskManagementItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RiskManagementItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RiskManagementItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RiskManagementItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RiskManagementItemQuery();
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
		public bool Load(RiskManagementItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RiskManagementItem AddNew()
		{
			RiskManagementItem entity = base.AddNewEntity() as RiskManagementItem;

			return entity;
		}
		public RiskManagementItem FindByPrimaryKey(String riskManagementNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(riskManagementNo, sequenceNo) as RiskManagementItem;
		}

		#region IEnumerable< RiskManagementItem> Members

		IEnumerator<RiskManagementItem> IEnumerable<RiskManagementItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RiskManagementItem;
			}
		}

		#endregion

		private RiskManagementItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RiskManagementItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RiskManagementItem ({RiskManagementNo, SequenceNo})")]
	[Serializable]
	public partial class RiskManagementItem : esRiskManagementItem
	{
		public RiskManagementItem()
		{
		}

		public RiskManagementItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RiskManagementItemMetadata.Meta();
			}
		}

		override protected esRiskManagementItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RiskManagementItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RiskManagementItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RiskManagementItemQuery();
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
		public bool Load(RiskManagementItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RiskManagementItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RiskManagementItemQuery : esRiskManagementItemQuery
	{
		public RiskManagementItemQuery()
		{

		}

		public RiskManagementItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RiskManagementItemQuery";
		}
	}

	[Serializable]
	public partial class RiskManagementItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RiskManagementItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.RiskManagementNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.RiskManagementNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.SRRiskManagementCategory, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.SRRiskManagementCategory;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.RiskManagementDescription, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.RiskManagementDescription;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.SRRiskManagementImpact, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.SRRiskManagementImpact;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.ImpactScore, 5, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.ImpactScore;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.SRRiskManagementProbability, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.SRRiskManagementProbability;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.ProbabilityScore, 7, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.ProbabilityScore;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.RiskScore, 8, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.RiskScore;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.SRRiskManagementBands, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.SRRiskManagementBands;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.SRRiskManagementControlling, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.SRRiskManagementControlling;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.ControllingScore, 11, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.ControllingScore;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.TotalScore, 12, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.TotalScore;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.RiskRating, 13, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.RiskRating;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.Reason, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.Action, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.Action;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.Pic, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.Pic;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RiskManagementItemMetadata.ColumnNames.LastUpdateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = RiskManagementItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RiskManagementItemMetadata Meta()
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
			public const string RiskManagementNo = "RiskManagementNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRRiskManagementCategory = "SRRiskManagementCategory";
			public const string RiskManagementDescription = "RiskManagementDescription";
			public const string SRRiskManagementImpact = "SRRiskManagementImpact";
			public const string ImpactScore = "ImpactScore";
			public const string SRRiskManagementProbability = "SRRiskManagementProbability";
			public const string ProbabilityScore = "ProbabilityScore";
			public const string RiskScore = "RiskScore";
			public const string SRRiskManagementBands = "SRRiskManagementBands";
			public const string SRRiskManagementControlling = "SRRiskManagementControlling";
			public const string ControllingScore = "ControllingScore";
			public const string TotalScore = "TotalScore";
			public const string RiskRating = "RiskRating";
			public const string Reason = "Reason";
			public const string Action = "Action";
			public const string Pic = "Pic";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RiskManagementNo = "RiskManagementNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRRiskManagementCategory = "SRRiskManagementCategory";
			public const string RiskManagementDescription = "RiskManagementDescription";
			public const string SRRiskManagementImpact = "SRRiskManagementImpact";
			public const string ImpactScore = "ImpactScore";
			public const string SRRiskManagementProbability = "SRRiskManagementProbability";
			public const string ProbabilityScore = "ProbabilityScore";
			public const string RiskScore = "RiskScore";
			public const string SRRiskManagementBands = "SRRiskManagementBands";
			public const string SRRiskManagementControlling = "SRRiskManagementControlling";
			public const string ControllingScore = "ControllingScore";
			public const string TotalScore = "TotalScore";
			public const string RiskRating = "RiskRating";
			public const string Reason = "Reason";
			public const string Action = "Action";
			public const string Pic = "Pic";
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
			lock (typeof(RiskManagementItemMetadata))
			{
				if (RiskManagementItemMetadata.mapDelegates == null)
				{
					RiskManagementItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RiskManagementItemMetadata.meta == null)
				{
					RiskManagementItemMetadata.meta = new RiskManagementItemMetadata();
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

				meta.AddTypeMap("RiskManagementNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRiskManagementCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RiskManagementDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRiskManagementImpact", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ImpactScore", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SRRiskManagementProbability", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProbabilityScore", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("RiskScore", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SRRiskManagementBands", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRiskManagementControlling", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ControllingScore", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("TotalScore", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("RiskRating", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Action", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "RiskManagementItem";
				meta.Destination = "RiskManagementItem";
				meta.spInsert = "proc_RiskManagementItemInsert";
				meta.spUpdate = "proc_RiskManagementItemUpdate";
				meta.spDelete = "proc_RiskManagementItemDelete";
				meta.spLoadAll = "proc_RiskManagementItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_RiskManagementItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RiskManagementItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
