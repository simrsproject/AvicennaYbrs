/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/11/2023 10:15:32 AM
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
	abstract public class esPATemplateCollection : esEntityCollectionWAuditLog
	{
		public esPATemplateCollection()
		{

		}
		

		protected override string GetCollectionName()
		{
			return "PATemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esPATemplateQuery query)
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
			this.InitQuery(query as esPATemplateQuery);
		}
		#endregion

		virtual public PATemplate DetachEntity(PATemplate entity)
		{
			return base.DetachEntity(entity) as PATemplate;
		}

		virtual public PATemplate AttachEntity(PATemplate entity)
		{
			return base.AttachEntity(entity) as PATemplate;
		}

		virtual public void Combine(PATemplateCollection collection)
		{
			base.Combine(collection);
		}

		new public PATemplate this[int index]
		{
			get
			{
				return base[index] as PATemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PATemplate);
		}
	}

	[Serializable]
	abstract public class esPATemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPATemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esPATemplate()
		{
		}

		public esPATemplate(DataRow row)
			: base(row)
		{
		}
		

		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 templateID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 templateID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateID);
			else
				return LoadByPrimaryKeyStoredProcedure(templateID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 templateID)
		{
			esPATemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateID == templateID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 templateID)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateID", templateID);
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
						case "TemplateID": this.str.TemplateID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "TemplateName": this.str.TemplateName = (string)value; break;
						case "ResultType": this.str.ResultType = (string)value; break;
						case "ClinicalDiagnosis": this.str.ClinicalDiagnosis = (string)value; break;
						case "ClinicalData": this.str.ClinicalData = (string)value; break;
						case "ExaminationMaterial": this.str.ExaminationMaterial = (string)value; break;
						case "LocationID": this.str.LocationID = (string)value; break;
						case "Macroscopic": this.str.Macroscopic = (string)value; break;
						case "Microscopic": this.str.Microscopic = (string)value; break;
						case "Impression": this.str.Impression = (string)value; break;
						case "ConclusionOrPADiagnosis": this.str.ConclusionOrPADiagnosis = (string)value; break;
						case "AdditionalNotes": this.str.AdditionalNotes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ClinicalDiagnosisID": this.str.ClinicalDiagnosisID = (string)value; break;
						case "LocationName": this.str.LocationName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TemplateID":

							if (value == null || value is System.Int64)
								this.TemplateID = (System.Int64?)value;
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
		/// Maps to PATemplate.TemplateID
		/// </summary>
		virtual public System.Int64? TemplateID
		{
			get
			{
				return base.GetSystemInt64(PATemplateMetadata.ColumnNames.TemplateID);
			}

			set
			{
				base.SetSystemInt64(PATemplateMetadata.ColumnNames.TemplateID, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.TemplateName
		/// </summary>
		virtual public System.String TemplateName
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.TemplateName);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.TemplateName, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ResultType
		/// </summary>
		virtual public System.String ResultType
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ResultType);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ResultType, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ClinicalDiagnosis
		/// </summary>
		virtual public System.String ClinicalDiagnosis
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ClinicalDiagnosis);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ClinicalDiagnosis, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ClinicalData
		/// </summary>
		virtual public System.String ClinicalData
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ClinicalData);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ClinicalData, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ExaminationMaterial
		/// </summary>
		virtual public System.String ExaminationMaterial
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ExaminationMaterial);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ExaminationMaterial, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.LocationID);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.LocationID, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.Macroscopic
		/// </summary>
		virtual public System.String Macroscopic
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.Macroscopic);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.Macroscopic, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.Microscopic
		/// </summary>
		virtual public System.String Microscopic
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.Microscopic);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.Microscopic, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.Impression
		/// </summary>
		virtual public System.String Impression
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.Impression);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.Impression, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ConclusionOrPADiagnosis
		/// </summary>
		virtual public System.String ConclusionOrPADiagnosis
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ConclusionOrPADiagnosis);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ConclusionOrPADiagnosis, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.AdditionalNotes
		/// </summary>
		virtual public System.String AdditionalNotes
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.AdditionalNotes);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.AdditionalNotes, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PATemplateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PATemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.ClinicalDiagnosisID
		/// </summary>
		virtual public System.String ClinicalDiagnosisID
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.ClinicalDiagnosisID);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.ClinicalDiagnosisID, value);
			}
		}
		/// <summary>
		/// Maps to PATemplate.LocationName
		/// </summary>
		virtual public System.String LocationName
		{
			get
			{
				return base.GetSystemString(PATemplateMetadata.ColumnNames.LocationName);
			}

			set
			{
				base.SetSystemString(PATemplateMetadata.ColumnNames.LocationName, value);
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
			public esStrings(esPATemplate entity)
			{
				this.entity = entity;
			}
			public System.String TemplateID
			{
				get
				{
					System.Int64? data = entity.TemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateID = null;
					else entity.TemplateID = Convert.ToInt64(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String TemplateName
			{
				get
				{
					System.String data = entity.TemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateName = null;
					else entity.TemplateName = Convert.ToString(value);
				}
			}
			public System.String ResultType
			{
				get
				{
					System.String data = entity.ResultType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultType = null;
					else entity.ResultType = Convert.ToString(value);
				}
			}
			public System.String ClinicalDiagnosis
			{
				get
				{
					System.String data = entity.ClinicalDiagnosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalDiagnosis = null;
					else entity.ClinicalDiagnosis = Convert.ToString(value);
				}
			}
			public System.String ClinicalData
			{
				get
				{
					System.String data = entity.ClinicalData;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalData = null;
					else entity.ClinicalData = Convert.ToString(value);
				}
			}
			public System.String ExaminationMaterial
			{
				get
				{
					System.String data = entity.ExaminationMaterial;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExaminationMaterial = null;
					else entity.ExaminationMaterial = Convert.ToString(value);
				}
			}
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
			public System.String Macroscopic
			{
				get
				{
					System.String data = entity.Macroscopic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Macroscopic = null;
					else entity.Macroscopic = Convert.ToString(value);
				}
			}
			public System.String Microscopic
			{
				get
				{
					System.String data = entity.Microscopic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Microscopic = null;
					else entity.Microscopic = Convert.ToString(value);
				}
			}
			public System.String Impression
			{
				get
				{
					System.String data = entity.Impression;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Impression = null;
					else entity.Impression = Convert.ToString(value);
				}
			}
			public System.String ConclusionOrPADiagnosis
			{
				get
				{
					System.String data = entity.ConclusionOrPADiagnosis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConclusionOrPADiagnosis = null;
					else entity.ConclusionOrPADiagnosis = Convert.ToString(value);
				}
			}
			public System.String AdditionalNotes
			{
				get
				{
					System.String data = entity.AdditionalNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdditionalNotes = null;
					else entity.AdditionalNotes = Convert.ToString(value);
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
			public System.String ClinicalDiagnosisID
			{
				get
				{
					System.String data = entity.ClinicalDiagnosisID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalDiagnosisID = null;
					else entity.ClinicalDiagnosisID = Convert.ToString(value);
				}
			}
			public System.String LocationName
			{
				get
				{
					System.String data = entity.LocationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationName = null;
					else entity.LocationName = Convert.ToString(value);
				}
			}
			private esPATemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPATemplateQuery query)
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
				throw new Exception("esPATemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PATemplate : esPATemplate
	{
	}

	[Serializable]
	abstract public class esPATemplateQuery : esDynamicQuery
	{		

		override protected IMetadata Meta
		{
			get
			{
				return PATemplateMetadata.Meta();
			}
		}

		public esQueryItem TemplateID
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.TemplateID, esSystemType.Int64);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem TemplateName
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
			}
		}

		public esQueryItem ResultType
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ResultType, esSystemType.String);
			}
		}

		public esQueryItem ClinicalDiagnosis
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ClinicalDiagnosis, esSystemType.String);
			}
		}

		public esQueryItem ClinicalData
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ClinicalData, esSystemType.String);
			}
		}

		public esQueryItem ExaminationMaterial
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ExaminationMaterial, esSystemType.String);
			}
		}

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		}

		public esQueryItem Macroscopic
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.Macroscopic, esSystemType.String);
			}
		}

		public esQueryItem Microscopic
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.Microscopic, esSystemType.String);
			}
		}

		public esQueryItem Impression
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.Impression, esSystemType.String);
			}
		}

		public esQueryItem ConclusionOrPADiagnosis
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ConclusionOrPADiagnosis, esSystemType.String);
			}
		}

		public esQueryItem AdditionalNotes
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.AdditionalNotes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ClinicalDiagnosisID
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.ClinicalDiagnosisID, esSystemType.String);
			}
		}

		public esQueryItem LocationName
		{
			get
			{
				return new esQueryItem(this, PATemplateMetadata.ColumnNames.LocationName, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PATemplateCollection")]
	public partial class PATemplateCollection : esPATemplateCollection, IEnumerable<PATemplate>
	{
		public PATemplateCollection()
		{

		}

		public static implicit operator List<PATemplate>(PATemplateCollection coll)
		{
			List<PATemplate> list = new List<PATemplate>();

			foreach (PATemplate emp in coll)
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
				return PATemplateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PATemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PATemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PATemplate();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PATemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PATemplateQuery();
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
		public bool Load(PATemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PATemplate AddNew()
		{
			PATemplate entity = base.AddNewEntity() as PATemplate;

			return entity;
		}
		public PATemplate FindByPrimaryKey(Int64 templateID)
		{
			return base.FindByPrimaryKey(templateID) as PATemplate;
		}

		#region IEnumerable< PATemplate> Members

		IEnumerator<PATemplate> IEnumerable<PATemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PATemplate;
			}
		}

		#endregion

		private PATemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PATemplate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PATemplate ({TemplateID})")]
	[Serializable]
	public partial class PATemplate : esPATemplate
	{
		public PATemplate()
		{
		}

		public PATemplate(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PATemplateMetadata.Meta();
			}
		}

		override protected esPATemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PATemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PATemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PATemplateQuery();
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
		public bool Load(PATemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PATemplateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PATemplateQuery : esPATemplateQuery
	{
		public PATemplateQuery()
		{

		}

		public PATemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PATemplateQuery";
		}
	}

	[Serializable]
	public partial class PATemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PATemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.TemplateID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PATemplateMetadata.PropertyNames.TemplateID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.TemplateName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.TemplateName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ResultType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ResultType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ClinicalDiagnosis, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ClinicalDiagnosis;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ClinicalData, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ClinicalData;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ExaminationMaterial, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ExaminationMaterial;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.LocationID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.Macroscopic, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.Macroscopic;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.Microscopic, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.Microscopic;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.Impression, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.Impression;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ConclusionOrPADiagnosis, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ConclusionOrPADiagnosis;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.AdditionalNotes, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.AdditionalNotes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PATemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.ClinicalDiagnosisID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.ClinicalDiagnosisID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PATemplateMetadata.ColumnNames.LocationName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = PATemplateMetadata.PropertyNames.LocationName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PATemplateMetadata Meta()
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
			public const string TemplateID = "TemplateID";
			public const string ItemID = "ItemID";
			public const string ParamedicID = "ParamedicID";
			public const string TemplateName = "TemplateName";
			public const string ResultType = "ResultType";
			public const string ClinicalDiagnosis = "ClinicalDiagnosis";
			public const string ClinicalData = "ClinicalData";
			public const string ExaminationMaterial = "ExaminationMaterial";
			public const string LocationID = "LocationID";
			public const string Macroscopic = "Macroscopic";
			public const string Microscopic = "Microscopic";
			public const string Impression = "Impression";
			public const string ConclusionOrPADiagnosis = "ConclusionOrPADiagnosis";
			public const string AdditionalNotes = "AdditionalNotes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ClinicalDiagnosisID = "ClinicalDiagnosisID";
			public const string LocationName = "LocationName";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TemplateID = "TemplateID";
			public const string ItemID = "ItemID";
			public const string ParamedicID = "ParamedicID";
			public const string TemplateName = "TemplateName";
			public const string ResultType = "ResultType";
			public const string ClinicalDiagnosis = "ClinicalDiagnosis";
			public const string ClinicalData = "ClinicalData";
			public const string ExaminationMaterial = "ExaminationMaterial";
			public const string LocationID = "LocationID";
			public const string Macroscopic = "Macroscopic";
			public const string Microscopic = "Microscopic";
			public const string Impression = "Impression";
			public const string ConclusionOrPADiagnosis = "ConclusionOrPADiagnosis";
			public const string AdditionalNotes = "AdditionalNotes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ClinicalDiagnosisID = "ClinicalDiagnosisID";
			public const string LocationName = "LocationName";
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
			lock (typeof(PATemplateMetadata))
			{
				if (PATemplateMetadata.mapDelegates == null)
				{
					PATemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PATemplateMetadata.meta == null)
				{
					PATemplateMetadata.meta = new PATemplateMetadata();
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

				meta.AddTypeMap("TemplateID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalDiagnosis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalData", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExaminationMaterial", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Macroscopic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Microscopic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Impression", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConclusionOrPADiagnosis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdditionalNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalDiagnosisID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationName", new esTypeMap("varchar", "System.String"));


				meta.Source = "PATemplate";
				meta.Destination = "PATemplate";
				meta.spInsert = "proc_PATemplateInsert";
				meta.spUpdate = "proc_PATemplateUpdate";
				meta.spDelete = "proc_PATemplateDelete";
				meta.spLoadAll = "proc_PATemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_PATemplateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PATemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
