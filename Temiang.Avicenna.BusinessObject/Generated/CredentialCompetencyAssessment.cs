/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:21:33 PM
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
	abstract public class esCredentialCompetencyAssessmentCollection : esEntityCollectionWAuditLog
	{
		public esCredentialCompetencyAssessmentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialCompetencyAssessmentCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentQuery query)
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
			this.InitQuery(query as esCredentialCompetencyAssessmentQuery);
		}
		#endregion

		virtual public CredentialCompetencyAssessment DetachEntity(CredentialCompetencyAssessment entity)
		{
			return base.DetachEntity(entity) as CredentialCompetencyAssessment;
		}

		virtual public CredentialCompetencyAssessment AttachEntity(CredentialCompetencyAssessment entity)
		{
			return base.AttachEntity(entity) as CredentialCompetencyAssessment;
		}

		virtual public void Combine(CredentialCompetencyAssessmentCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialCompetencyAssessment this[int index]
		{
			get
			{
				return base[index] as CredentialCompetencyAssessment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialCompetencyAssessment);
		}
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialCompetencyAssessmentQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialCompetencyAssessment()
		{
		}

		public esCredentialCompetencyAssessment(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esCredentialCompetencyAssessmentQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "Confirmation": this.str.Confirmation = (string)value; break;
						case "ConfirmationResult": this.str.ConfirmationResult = (string)value; break;
						case "Observation": this.str.Observation = (string)value; break;
						case "ObservationResult": this.str.ObservationResult = (string)value; break;
						case "Question": this.str.Question = (string)value; break;
						case "QuestionResult": this.str.QuestionResult = (string)value; break;
						case "Exploration": this.str.Exploration = (string)value; break;
						case "CodeOfEthics": this.str.CodeOfEthics = (string)value; break;
						case "CodeOfEthicsResult": this.str.CodeOfEthicsResult = (string)value; break;
						case "SRMedicalClinicalCompetence": this.str.SRMedicalClinicalCompetence = (string)value; break;
						case "SRMedicalGeneralCompetence": this.str.SRMedicalGeneralCompetence = (string)value; break;
						case "SRMedicalEthicalStanding": this.str.SRMedicalEthicalStanding = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to CredentialCompetencyAssessment.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.Confirmation
		/// </summary>
		virtual public System.String Confirmation
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Confirmation);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Confirmation, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.ConfirmationResult
		/// </summary>
		virtual public System.String ConfirmationResult
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.ConfirmationResult);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.ConfirmationResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.Observation
		/// </summary>
		virtual public System.String Observation
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Observation);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Observation, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.ObservationResult
		/// </summary>
		virtual public System.String ObservationResult
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.ObservationResult);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.ObservationResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.Question
		/// </summary>
		virtual public System.String Question
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Question);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Question, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.QuestionResult
		/// </summary>
		virtual public System.String QuestionResult
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.QuestionResult);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.QuestionResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.Exploration
		/// </summary>
		virtual public System.String Exploration
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Exploration);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.Exploration, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.CodeOfEthics
		/// </summary>
		virtual public System.String CodeOfEthics
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthics);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthics, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.CodeOfEthicsResult
		/// </summary>
		virtual public System.String CodeOfEthicsResult
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthicsResult);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthicsResult, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.SRMedicalClinicalCompetence
		/// </summary>
		virtual public System.String SRMedicalClinicalCompetence
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalClinicalCompetence);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalClinicalCompetence, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.SRMedicalGeneralCompetence
		/// </summary>
		virtual public System.String SRMedicalGeneralCompetence
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalGeneralCompetence);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalGeneralCompetence, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.SRMedicalEthicalStanding
		/// </summary>
		virtual public System.String SRMedicalEthicalStanding
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalEthicalStanding);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalEthicalStanding, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialCompetencyAssessment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialCompetencyAssessment entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String Confirmation
			{
				get
				{
					System.String data = entity.Confirmation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Confirmation = null;
					else entity.Confirmation = Convert.ToString(value);
				}
			}
			public System.String ConfirmationResult
			{
				get
				{
					System.String data = entity.ConfirmationResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConfirmationResult = null;
					else entity.ConfirmationResult = Convert.ToString(value);
				}
			}
			public System.String Observation
			{
				get
				{
					System.String data = entity.Observation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Observation = null;
					else entity.Observation = Convert.ToString(value);
				}
			}
			public System.String ObservationResult
			{
				get
				{
					System.String data = entity.ObservationResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ObservationResult = null;
					else entity.ObservationResult = Convert.ToString(value);
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
			public System.String QuestionResult
			{
				get
				{
					System.String data = entity.QuestionResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionResult = null;
					else entity.QuestionResult = Convert.ToString(value);
				}
			}
			public System.String Exploration
			{
				get
				{
					System.String data = entity.Exploration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Exploration = null;
					else entity.Exploration = Convert.ToString(value);
				}
			}
			public System.String CodeOfEthics
			{
				get
				{
					System.String data = entity.CodeOfEthics;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CodeOfEthics = null;
					else entity.CodeOfEthics = Convert.ToString(value);
				}
			}
			public System.String CodeOfEthicsResult
			{
				get
				{
					System.String data = entity.CodeOfEthicsResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CodeOfEthicsResult = null;
					else entity.CodeOfEthicsResult = Convert.ToString(value);
				}
			}
			public System.String SRMedicalClinicalCompetence
			{
				get
				{
					System.String data = entity.SRMedicalClinicalCompetence;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalClinicalCompetence = null;
					else entity.SRMedicalClinicalCompetence = Convert.ToString(value);
				}
			}
			public System.String SRMedicalGeneralCompetence
			{
				get
				{
					System.String data = entity.SRMedicalGeneralCompetence;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalGeneralCompetence = null;
					else entity.SRMedicalGeneralCompetence = Convert.ToString(value);
				}
			}
			public System.String SRMedicalEthicalStanding
			{
				get
				{
					System.String data = entity.SRMedicalEthicalStanding;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalEthicalStanding = null;
					else entity.SRMedicalEthicalStanding = Convert.ToString(value);
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
			private esCredentialCompetencyAssessment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialCompetencyAssessmentQuery query)
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
				throw new Exception("esCredentialCompetencyAssessment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialCompetencyAssessment : esCredentialCompetencyAssessment
	{
	}

	[Serializable]
	abstract public class esCredentialCompetencyAssessmentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem Confirmation
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.Confirmation, esSystemType.String);
			}
		}

		public esQueryItem ConfirmationResult
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.ConfirmationResult, esSystemType.String);
			}
		}

		public esQueryItem Observation
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.Observation, esSystemType.String);
			}
		}

		public esQueryItem ObservationResult
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.ObservationResult, esSystemType.String);
			}
		}

		public esQueryItem Question
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.Question, esSystemType.String);
			}
		}

		public esQueryItem QuestionResult
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.QuestionResult, esSystemType.String);
			}
		}

		public esQueryItem Exploration
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.Exploration, esSystemType.String);
			}
		}

		public esQueryItem CodeOfEthics
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthics, esSystemType.String);
			}
		}

		public esQueryItem CodeOfEthicsResult
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthicsResult, esSystemType.String);
			}
		}

		public esQueryItem SRMedicalClinicalCompetence
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalClinicalCompetence, esSystemType.String);
			}
		}

		public esQueryItem SRMedicalGeneralCompetence
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalGeneralCompetence, esSystemType.String);
			}
		}

		public esQueryItem SRMedicalEthicalStanding
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalEthicalStanding, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialCompetencyAssessmentCollection")]
	public partial class CredentialCompetencyAssessmentCollection : esCredentialCompetencyAssessmentCollection, IEnumerable<CredentialCompetencyAssessment>
	{
		public CredentialCompetencyAssessmentCollection()
		{

		}

		public static implicit operator List<CredentialCompetencyAssessment>(CredentialCompetencyAssessmentCollection coll)
		{
			List<CredentialCompetencyAssessment> list = new List<CredentialCompetencyAssessment>();

			foreach (CredentialCompetencyAssessment emp in coll)
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
				return CredentialCompetencyAssessmentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialCompetencyAssessment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialCompetencyAssessment();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentQuery();
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
		public bool Load(CredentialCompetencyAssessmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialCompetencyAssessment AddNew()
		{
			CredentialCompetencyAssessment entity = base.AddNewEntity() as CredentialCompetencyAssessment;

			return entity;
		}
		public CredentialCompetencyAssessment FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as CredentialCompetencyAssessment;
		}

		#region IEnumerable< CredentialCompetencyAssessment> Members

		IEnumerator<CredentialCompetencyAssessment> IEnumerable<CredentialCompetencyAssessment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialCompetencyAssessment;
			}
		}

		#endregion

		private CredentialCompetencyAssessmentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialCompetencyAssessment' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialCompetencyAssessment ({TransactionNo})")]
	[Serializable]
	public partial class CredentialCompetencyAssessment : esCredentialCompetencyAssessment
	{
		public CredentialCompetencyAssessment()
		{
		}

		public CredentialCompetencyAssessment(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialCompetencyAssessmentMetadata.Meta();
			}
		}

		override protected esCredentialCompetencyAssessmentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialCompetencyAssessmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialCompetencyAssessmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialCompetencyAssessmentQuery();
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
		public bool Load(CredentialCompetencyAssessmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialCompetencyAssessmentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialCompetencyAssessmentQuery : esCredentialCompetencyAssessmentQuery
	{
		public CredentialCompetencyAssessmentQuery()
		{

		}

		public CredentialCompetencyAssessmentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialCompetencyAssessmentQuery";
		}
	}

	[Serializable]
	public partial class CredentialCompetencyAssessmentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialCompetencyAssessmentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.Confirmation, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.Confirmation;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.ConfirmationResult, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.ConfirmationResult;
			c.CharacterMaxLength = 2000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.Observation, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.Observation;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.ObservationResult, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.ObservationResult;
			c.CharacterMaxLength = 2000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.Question, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.Question;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.QuestionResult, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.QuestionResult;
			c.CharacterMaxLength = 2000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.Exploration, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.Exploration;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthics, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.CodeOfEthics;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.CodeOfEthicsResult, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.CodeOfEthicsResult;
			c.CharacterMaxLength = 2000;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalClinicalCompetence, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.SRMedicalClinicalCompetence;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalGeneralCompetence, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.SRMedicalGeneralCompetence;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.SRMedicalEthicalStanding, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.SRMedicalEthicalStanding;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialCompetencyAssessmentMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialCompetencyAssessmentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialCompetencyAssessmentMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string Confirmation = "Confirmation";
			public const string ConfirmationResult = "ConfirmationResult";
			public const string Observation = "Observation";
			public const string ObservationResult = "ObservationResult";
			public const string Question = "Question";
			public const string QuestionResult = "QuestionResult";
			public const string Exploration = "Exploration";
			public const string CodeOfEthics = "CodeOfEthics";
			public const string CodeOfEthicsResult = "CodeOfEthicsResult";
			public const string SRMedicalClinicalCompetence = "SRMedicalClinicalCompetence";
			public const string SRMedicalGeneralCompetence = "SRMedicalGeneralCompetence";
			public const string SRMedicalEthicalStanding = "SRMedicalEthicalStanding";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string Confirmation = "Confirmation";
			public const string ConfirmationResult = "ConfirmationResult";
			public const string Observation = "Observation";
			public const string ObservationResult = "ObservationResult";
			public const string Question = "Question";
			public const string QuestionResult = "QuestionResult";
			public const string Exploration = "Exploration";
			public const string CodeOfEthics = "CodeOfEthics";
			public const string CodeOfEthicsResult = "CodeOfEthicsResult";
			public const string SRMedicalClinicalCompetence = "SRMedicalClinicalCompetence";
			public const string SRMedicalGeneralCompetence = "SRMedicalGeneralCompetence";
			public const string SRMedicalEthicalStanding = "SRMedicalEthicalStanding";
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
			lock (typeof(CredentialCompetencyAssessmentMetadata))
			{
				if (CredentialCompetencyAssessmentMetadata.mapDelegates == null)
				{
					CredentialCompetencyAssessmentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialCompetencyAssessmentMetadata.meta == null)
				{
					CredentialCompetencyAssessmentMetadata.meta = new CredentialCompetencyAssessmentMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Confirmation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConfirmationResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Observation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ObservationResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Question", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Exploration", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CodeOfEthics", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CodeOfEthicsResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalClinicalCompetence", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalGeneralCompetence", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalEthicalStanding", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialCompetencyAssessment";
				meta.Destination = "CredentialCompetencyAssessment";
				meta.spInsert = "proc_CredentialCompetencyAssessmentInsert";
				meta.spUpdate = "proc_CredentialCompetencyAssessmentUpdate";
				meta.spDelete = "proc_CredentialCompetencyAssessmentDelete";
				meta.spLoadAll = "proc_CredentialCompetencyAssessmentLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialCompetencyAssessmentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialCompetencyAssessmentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
