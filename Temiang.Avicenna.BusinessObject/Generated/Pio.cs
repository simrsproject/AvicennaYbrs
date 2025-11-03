/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2022 6:01:21 PM
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
	abstract public class esPioCollection : esEntityCollectionWAuditLog
	{
		public esPioCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PioCollection";
		}

		#region Query Logic
		protected void InitQuery(esPioQuery query)
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
			this.InitQuery(query as esPioQuery);
		}
		#endregion

		virtual public Pio DetachEntity(Pio entity)
		{
			return base.DetachEntity(entity) as Pio;
		}

		virtual public Pio AttachEntity(Pio entity)
		{
			return base.AttachEntity(entity) as Pio;
		}

		virtual public void Combine(PioCollection collection)
		{
			base.Combine(collection);
		}

		new public Pio this[int index]
		{
			get
			{
				return base[index] as Pio;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Pio);
		}
	}

	[Serializable]
	abstract public class esPio : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPioQuery GetDynamicQuery()
		{
			return null;
		}

		public esPio()
		{
		}

		public esPio(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 pioNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pioNo);
			else
				return LoadByPrimaryKeyStoredProcedure(pioNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 pioNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pioNo);
			else
				return LoadByPrimaryKeyStoredProcedure(pioNo);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 pioNo)
		{
			esPioQuery query = this.GetDynamicQuery();
			query.Where(query.PioNo == pioNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 pioNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PioNo", pioNo);
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
						case "PioNo": this.str.PioNo = (string)value; break;
						case "PioDateTime": this.str.PioDateTime = (string)value; break;
						case "QuestionerName": this.str.QuestionerName = (string)value; break;
						case "SROccupation": this.str.SROccupation = (string)value; break;
						case "Question": this.str.Question = (string)value; break;
						case "Information": this.str.Information = (string)value; break;
						case "OtherSources": this.str.OtherSources = (string)value; break;
						case "OtherCategory": this.str.OtherCategory = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRDurationType": this.str.SRDurationType = (string)value; break;
						case "IsInternRecipient": this.str.IsInternRecipient = (string)value; break;
						case "SRAnswerMethod": this.str.SRAnswerMethod = (string)value; break;
						case "SRQuestionMethod": this.str.SRQuestionMethod = (string)value; break;
						case "AnswerDateTime": this.str.AnswerDateTime = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PioNo":

							if (value == null || value is System.Int32)
								this.PioNo = (System.Int32?)value;
							break;
						case "PioDateTime":

							if (value == null || value is System.DateTime)
								this.PioDateTime = (System.DateTime?)value;
							break;
						case "IsDeleted":

							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsInternRecipient":

							if (value == null || value is System.Boolean)
								this.IsInternRecipient = (System.Boolean?)value;
							break;
						case "AnswerDateTime":

							if (value == null || value is System.DateTime)
								this.AnswerDateTime = (System.DateTime?)value;
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
		/// Maps to Pio.PioNo
		/// </summary>
		virtual public System.Int32? PioNo
		{
			get
			{
				return base.GetSystemInt32(PioMetadata.ColumnNames.PioNo);
			}

			set
			{
				base.SetSystemInt32(PioMetadata.ColumnNames.PioNo, value);
			}
		}
		/// <summary>
		/// Maps to Pio.PioDateTime
		/// </summary>
		virtual public System.DateTime? PioDateTime
		{
			get
			{
				return base.GetSystemDateTime(PioMetadata.ColumnNames.PioDateTime);
			}

			set
			{
				base.SetSystemDateTime(PioMetadata.ColumnNames.PioDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Pio.QuestionerName
		/// </summary>
		virtual public System.String QuestionerName
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.QuestionerName);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.QuestionerName, value);
			}
		}
		/// <summary>
		/// Maps to Pio.SROccupation
		/// </summary>
		virtual public System.String SROccupation
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.SROccupation);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.SROccupation, value);
			}
		}
		/// <summary>
		/// Maps to Pio.Question
		/// </summary>
		virtual public System.String Question
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.Question);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.Question, value);
			}
		}
		/// <summary>
		/// Maps to Pio.Information
		/// </summary>
		virtual public System.String Information
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.Information);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.Information, value);
			}
		}
		/// <summary>
		/// Maps to Pio.OtherSources
		/// </summary>
		virtual public System.String OtherSources
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.OtherSources);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.OtherSources, value);
			}
		}
		/// <summary>
		/// Maps to Pio.OtherCategory
		/// </summary>
		virtual public System.String OtherCategory
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.OtherCategory);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.OtherCategory, value);
			}
		}
		/// <summary>
		/// Maps to Pio.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(PioMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(PioMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to Pio.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Pio.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PioMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PioMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Pio.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PioMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PioMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Pio.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to Pio.SRDurationType
		/// </summary>
		virtual public System.String SRDurationType
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.SRDurationType);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.SRDurationType, value);
			}
		}
		/// <summary>
		/// Maps to Pio.IsInternRecipient
		/// </summary>
		virtual public System.Boolean? IsInternRecipient
		{
			get
			{
				return base.GetSystemBoolean(PioMetadata.ColumnNames.IsInternRecipient);
			}

			set
			{
				base.SetSystemBoolean(PioMetadata.ColumnNames.IsInternRecipient, value);
			}
		}
		/// <summary>
		/// Maps to Pio.SRAnswerMethod
		/// </summary>
		virtual public System.String SRAnswerMethod
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.SRAnswerMethod);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.SRAnswerMethod, value);
			}
		}
		/// <summary>
		/// Maps to Pio.SRQuestionMethod
		/// </summary>
		virtual public System.String SRQuestionMethod
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.SRQuestionMethod);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.SRQuestionMethod, value);
			}
		}
		/// <summary>
		/// Maps to Pio.AnswerDateTime
		/// </summary>
		virtual public System.DateTime? AnswerDateTime
		{
			get
			{
				return base.GetSystemDateTime(PioMetadata.ColumnNames.AnswerDateTime);
			}

			set
			{
				base.SetSystemDateTime(PioMetadata.ColumnNames.AnswerDateTime, value);
			}
		}
		/// <summary>
		/// Maps to Pio.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(PioMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(PioMetadata.ColumnNames.ServiceUnitID, value);
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
			public esStrings(esPio entity)
			{
				this.entity = entity;
			}
			public System.String PioNo
			{
				get
				{
					System.Int32? data = entity.PioNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PioNo = null;
					else entity.PioNo = Convert.ToInt32(value);
				}
			}
			public System.String PioDateTime
			{
				get
				{
					System.DateTime? data = entity.PioDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PioDateTime = null;
					else entity.PioDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String QuestionerName
			{
				get
				{
					System.String data = entity.QuestionerName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionerName = null;
					else entity.QuestionerName = Convert.ToString(value);
				}
			}
			public System.String SROccupation
			{
				get
				{
					System.String data = entity.SROccupation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROccupation = null;
					else entity.SROccupation = Convert.ToString(value);
				}
			}
			public System.String Question
			{
				get
				{
					System.String data = entity.Question;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Question = null;
					else entity.Question = Convert.ToString(value);
				}
			}
			public System.String Information
			{
				get
				{
					System.String data = entity.Information;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Information = null;
					else entity.Information = Convert.ToString(value);
				}
			}
			public System.String OtherSources
			{
				get
				{
					System.String data = entity.OtherSources;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherSources = null;
					else entity.OtherSources = Convert.ToString(value);
				}
			}
			public System.String OtherCategory
			{
				get
				{
					System.String data = entity.OtherCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherCategory = null;
					else entity.OtherCategory = Convert.ToString(value);
				}
			}
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
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
			public System.String SRDurationType
			{
				get
				{
					System.String data = entity.SRDurationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDurationType = null;
					else entity.SRDurationType = Convert.ToString(value);
				}
			}
			public System.String IsInternRecipient
			{
				get
				{
					System.Boolean? data = entity.IsInternRecipient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInternRecipient = null;
					else entity.IsInternRecipient = Convert.ToBoolean(value);
				}
			}
			public System.String SRAnswerMethod
			{
				get
				{
					System.String data = entity.SRAnswerMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAnswerMethod = null;
					else entity.SRAnswerMethod = Convert.ToString(value);
				}
			}
			public System.String SRQuestionMethod
			{
				get
				{
					System.String data = entity.SRQuestionMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQuestionMethod = null;
					else entity.SRQuestionMethod = Convert.ToString(value);
				}
			}
			public System.String AnswerDateTime
			{
				get
				{
					System.DateTime? data = entity.AnswerDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerDateTime = null;
					else entity.AnswerDateTime = Convert.ToDateTime(value);
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
			private esPio entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPioQuery query)
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
				throw new Exception("esPio can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class Pio : esPio
	{
	}

	[Serializable]
	abstract public class esPioQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PioMetadata.Meta();
			}
		}

		public esQueryItem PioNo
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.PioNo, esSystemType.Int32);
			}
		}

		public esQueryItem PioDateTime
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.PioDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem QuestionerName
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.QuestionerName, esSystemType.String);
			}
		}

		public esQueryItem SROccupation
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.SROccupation, esSystemType.String);
			}
		}

		public esQueryItem Question
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.Question, esSystemType.String);
			}
		}

		public esQueryItem Information
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.Information, esSystemType.String);
			}
		}

		public esQueryItem OtherSources
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.OtherSources, esSystemType.String);
			}
		}

		public esQueryItem OtherCategory
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.OtherCategory, esSystemType.String);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRDurationType
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.SRDurationType, esSystemType.String);
			}
		}

		public esQueryItem IsInternRecipient
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.IsInternRecipient, esSystemType.Boolean);
			}
		}

		public esQueryItem SRAnswerMethod
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.SRAnswerMethod, esSystemType.String);
			}
		}

		public esQueryItem SRQuestionMethod
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.SRQuestionMethod, esSystemType.String);
			}
		}

		public esQueryItem AnswerDateTime
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.AnswerDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, PioMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PioCollection")]
	public partial class PioCollection : esPioCollection, IEnumerable<Pio>
	{
		public PioCollection()
		{

		}

		public static implicit operator List<Pio>(PioCollection coll)
		{
			List<Pio> list = new List<Pio>();

			foreach (Pio emp in coll)
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
				return PioMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PioQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Pio(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Pio();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PioQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PioQuery();
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
		public bool Load(PioQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public Pio AddNew()
		{
			Pio entity = base.AddNewEntity() as Pio;

			return entity;
		}
		public Pio FindByPrimaryKey(Int32 pioNo)
		{
			return base.FindByPrimaryKey(pioNo) as Pio;
		}

		#region IEnumerable< Pio> Members

		IEnumerator<Pio> IEnumerable<Pio>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as Pio;
			}
		}

		#endregion

		private PioQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Pio' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("Pio ({PioNo})")]
	[Serializable]
	public partial class Pio : esPio
	{
		public Pio()
		{
		}

		public Pio(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PioMetadata.Meta();
			}
		}

		override protected esPioQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PioQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PioQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PioQuery();
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
		public bool Load(PioQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PioQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PioQuery : esPioQuery
	{
		public PioQuery()
		{

		}

		public PioQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PioQuery";
		}
	}

	[Serializable]
	public partial class PioMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PioMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PioMetadata.ColumnNames.PioNo, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PioMetadata.PropertyNames.PioNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.PioDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PioMetadata.PropertyNames.PioDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.QuestionerName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.QuestionerName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.SROccupation, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.SROccupation;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.Question, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.Question;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.Information, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.Information;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.OtherSources, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.OtherSources;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.OtherCategory, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.OtherCategory;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.IsDeleted, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PioMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.CreatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PioMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PioMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.SRDurationType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.SRDurationType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.IsInternRecipient, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PioMetadata.PropertyNames.IsInternRecipient;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.SRAnswerMethod, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.SRAnswerMethod;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.SRQuestionMethod, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.SRQuestionMethod;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.AnswerDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PioMetadata.PropertyNames.AnswerDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PioMetadata.ColumnNames.ServiceUnitID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = PioMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PioMetadata Meta()
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
			public const string PioNo = "PioNo";
			public const string PioDateTime = "PioDateTime";
			public const string QuestionerName = "QuestionerName";
			public const string SROccupation = "SROccupation";
			public const string Question = "Question";
			public const string Information = "Information";
			public const string OtherSources = "OtherSources";
			public const string OtherCategory = "OtherCategory";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRDurationType = "SRDurationType";
			public const string IsInternRecipient = "IsInternRecipient";
			public const string SRAnswerMethod = "SRAnswerMethod";
			public const string SRQuestionMethod = "SRQuestionMethod";
			public const string AnswerDateTime = "AnswerDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PioNo = "PioNo";
			public const string PioDateTime = "PioDateTime";
			public const string QuestionerName = "QuestionerName";
			public const string SROccupation = "SROccupation";
			public const string Question = "Question";
			public const string Information = "Information";
			public const string OtherSources = "OtherSources";
			public const string OtherCategory = "OtherCategory";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRDurationType = "SRDurationType";
			public const string IsInternRecipient = "IsInternRecipient";
			public const string SRAnswerMethod = "SRAnswerMethod";
			public const string SRQuestionMethod = "SRQuestionMethod";
			public const string AnswerDateTime = "AnswerDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
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
			lock (typeof(PioMetadata))
			{
				if (PioMetadata.mapDelegates == null)
				{
					PioMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PioMetadata.meta == null)
				{
					PioMetadata.meta = new PioMetadata();
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

				meta.AddTypeMap("PioNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PioDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("QuestionerName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROccupation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Question", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Information", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherSources", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDurationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsInternRecipient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRAnswerMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRQuestionMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));


				meta.Source = "Pio";
				meta.Destination = "Pio";
				meta.spInsert = "proc_PioInsert";
				meta.spUpdate = "proc_PioUpdate";
				meta.spDelete = "proc_PioDelete";
				meta.spLoadAll = "proc_PioLoadAll";
				meta.spLoadByPrimaryKey = "proc_PioLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PioMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
