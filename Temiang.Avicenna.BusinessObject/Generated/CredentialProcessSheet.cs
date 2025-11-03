/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/28/2022 12:21:17 PM
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
	abstract public class esCredentialProcessSheetCollection : esEntityCollectionWAuditLog
	{
		public esCredentialProcessSheetCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialProcessSheetCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialProcessSheetQuery query)
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
			this.InitQuery(query as esCredentialProcessSheetQuery);
		}
		#endregion

		virtual public CredentialProcessSheet DetachEntity(CredentialProcessSheet entity)
		{
			return base.DetachEntity(entity) as CredentialProcessSheet;
		}

		virtual public CredentialProcessSheet AttachEntity(CredentialProcessSheet entity)
		{
			return base.AttachEntity(entity) as CredentialProcessSheet;
		}

		virtual public void Combine(CredentialProcessSheetCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialProcessSheet this[int index]
		{
			get
			{
				return base[index] as CredentialProcessSheet;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialProcessSheet);
		}
	}

	[Serializable]
	abstract public class esCredentialProcessSheet : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialProcessSheetQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialProcessSheet()
		{
		}

		public esCredentialProcessSheet(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, Int32 questionnaireItemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, questionnaireItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, questionnaireItemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 questionnaireItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, questionnaireItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, questionnaireItemID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 questionnaireItemID)
		{
			esCredentialProcessSheetQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.QuestionnaireItemID == questionnaireItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 questionnaireItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("QuestionnaireItemID", questionnaireItemID);
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
						case "QuestionnaireItemID": this.str.QuestionnaireItemID = (string)value; break;
						case "SRCurrentAbility": this.str.SRCurrentAbility = (string)value; break;
						case "SelfAssessmentNotes": this.str.SelfAssessmentNotes = (string)value; break;
						case "SRCurrentAbilitySupervisor": this.str.SRCurrentAbilitySupervisor = (string)value; break;
						case "SRCurrentAbilitySupervisor2": this.str.SRCurrentAbilitySupervisor2 = (string)value; break;
						case "SRReview": this.str.SRReview = (string)value; break;
						case "SRRecomendation": this.str.SRRecomendation = (string)value; break;
						case "SRConclusion": this.str.SRConclusion = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "QuestionnaireItemID":

							if (value == null || value is System.Int32)
								this.QuestionnaireItemID = (System.Int32?)value;
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
		/// Maps to CredentialProcessSheet.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.QuestionnaireItemID
		/// </summary>
		virtual public System.Int32? QuestionnaireItemID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessSheetMetadata.ColumnNames.QuestionnaireItemID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessSheetMetadata.ColumnNames.QuestionnaireItemID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.SRCurrentAbility
		/// </summary>
		virtual public System.String SRCurrentAbility
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbility);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbility, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.SelfAssessmentNotes
		/// </summary>
		virtual public System.String SelfAssessmentNotes
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.SelfAssessmentNotes);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.SelfAssessmentNotes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.SRCurrentAbilitySupervisor
		/// </summary>
		virtual public System.String SRCurrentAbilitySupervisor
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.SRCurrentAbilitySupervisor2
		/// </summary>
		virtual public System.String SRCurrentAbilitySupervisor2
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor2);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.SRReview
		/// </summary>
		virtual public System.String SRReview
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRReview);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRReview, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.SRRecomendation
		/// </summary>
		virtual public System.String SRRecomendation
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRRecomendation);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRRecomendation, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.SRConclusion
		/// </summary>
		virtual public System.String SRConclusion
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRConclusion);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.SRConclusion, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessSheetMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessSheetMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessSheet.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessSheetMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessSheetMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialProcessSheet entity)
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
			public System.String QuestionnaireItemID
			{
				get
				{
					System.Int32? data = entity.QuestionnaireItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionnaireItemID = null;
					else entity.QuestionnaireItemID = Convert.ToInt32(value);
				}
			}
			public System.String SRCurrentAbility
			{
				get
				{
					System.String data = entity.SRCurrentAbility;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrentAbility = null;
					else entity.SRCurrentAbility = Convert.ToString(value);
				}
			}
			public System.String SelfAssessmentNotes
			{
				get
				{
					System.String data = entity.SelfAssessmentNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SelfAssessmentNotes = null;
					else entity.SelfAssessmentNotes = Convert.ToString(value);
				}
			}
			public System.String SRCurrentAbilitySupervisor
			{
				get
				{
					System.String data = entity.SRCurrentAbilitySupervisor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrentAbilitySupervisor = null;
					else entity.SRCurrentAbilitySupervisor = Convert.ToString(value);
				}
			}
			public System.String SRCurrentAbilitySupervisor2
			{
				get
				{
					System.String data = entity.SRCurrentAbilitySupervisor2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrentAbilitySupervisor2 = null;
					else entity.SRCurrentAbilitySupervisor2 = Convert.ToString(value);
				}
			}
			public System.String SRReview
			{
				get
				{
					System.String data = entity.SRReview;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReview = null;
					else entity.SRReview = Convert.ToString(value);
				}
			}
			public System.String SRRecomendation
			{
				get
				{
					System.String data = entity.SRRecomendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRecomendation = null;
					else entity.SRRecomendation = Convert.ToString(value);
				}
			}
			public System.String SRConclusion
			{
				get
				{
					System.String data = entity.SRConclusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConclusion = null;
					else entity.SRConclusion = Convert.ToString(value);
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
			private esCredentialProcessSheet entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialProcessSheetQuery query)
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
				throw new Exception("esCredentialProcessSheet can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialProcessSheet : esCredentialProcessSheet
	{
	}

	[Serializable]
	abstract public class esCredentialProcessSheetQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessSheetMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireItemID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.QuestionnaireItemID, esSystemType.Int32);
			}
		}

		public esQueryItem SRCurrentAbility
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbility, esSystemType.String);
			}
		}

		public esQueryItem SelfAssessmentNotes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.SelfAssessmentNotes, esSystemType.String);
			}
		}

		public esQueryItem SRCurrentAbilitySupervisor
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor, esSystemType.String);
			}
		}

		public esQueryItem SRCurrentAbilitySupervisor2
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor2, esSystemType.String);
			}
		}

		public esQueryItem SRReview
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.SRReview, esSystemType.String);
			}
		}

		public esQueryItem SRRecomendation
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.SRRecomendation, esSystemType.String);
			}
		}

		public esQueryItem SRConclusion
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.SRConclusion, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessSheetMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialProcessSheetCollection")]
	public partial class CredentialProcessSheetCollection : esCredentialProcessSheetCollection, IEnumerable<CredentialProcessSheet>
	{
		public CredentialProcessSheetCollection()
		{

		}

		public static implicit operator List<CredentialProcessSheet>(CredentialProcessSheetCollection coll)
		{
			List<CredentialProcessSheet> list = new List<CredentialProcessSheet>();

			foreach (CredentialProcessSheet emp in coll)
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
				return CredentialProcessSheetMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessSheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialProcessSheet(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialProcessSheet();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessSheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessSheetQuery();
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
		public bool Load(CredentialProcessSheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialProcessSheet AddNew()
		{
			CredentialProcessSheet entity = base.AddNewEntity() as CredentialProcessSheet;

			return entity;
		}
		public CredentialProcessSheet FindByPrimaryKey(String transactionNo, Int32 questionnaireItemID)
		{
			return base.FindByPrimaryKey(transactionNo, questionnaireItemID) as CredentialProcessSheet;
		}

		#region IEnumerable< CredentialProcessSheet> Members

		IEnumerator<CredentialProcessSheet> IEnumerable<CredentialProcessSheet>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialProcessSheet;
			}
		}

		#endregion

		private CredentialProcessSheetQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialProcessSheet' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialProcessSheet ({TransactionNo, QuestionnaireItemID})")]
	[Serializable]
	public partial class CredentialProcessSheet : esCredentialProcessSheet
	{
		public CredentialProcessSheet()
		{
		}

		public CredentialProcessSheet(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessSheetMetadata.Meta();
			}
		}

		override protected esCredentialProcessSheetQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessSheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessSheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessSheetQuery();
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
		public bool Load(CredentialProcessSheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialProcessSheetQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialProcessSheetQuery : esCredentialProcessSheetQuery
	{
		public CredentialProcessSheetQuery()
		{

		}

		public CredentialProcessSheetQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialProcessSheetQuery";
		}
	}

	[Serializable]
	public partial class CredentialProcessSheetMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialProcessSheetMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.QuestionnaireItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.QuestionnaireItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbility, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.SRCurrentAbility;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.SelfAssessmentNotes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.SelfAssessmentNotes;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.SRCurrentAbilitySupervisor;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.SRCurrentAbilitySupervisor2, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.SRCurrentAbilitySupervisor2;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.SRReview, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.SRReview;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.SRRecomendation, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.SRRecomendation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.SRConclusion, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.SRConclusion;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.Notes, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessSheetMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessSheetMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialProcessSheetMetadata Meta()
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
			public const string QuestionnaireItemID = "QuestionnaireItemID";
			public const string SRCurrentAbility = "SRCurrentAbility";
			public const string SelfAssessmentNotes = "SelfAssessmentNotes";
			public const string SRCurrentAbilitySupervisor = "SRCurrentAbilitySupervisor";
			public const string SRCurrentAbilitySupervisor2 = "SRCurrentAbilitySupervisor2";
			public const string SRReview = "SRReview";
			public const string SRRecomendation = "SRRecomendation";
			public const string SRConclusion = "SRConclusion";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string QuestionnaireItemID = "QuestionnaireItemID";
			public const string SRCurrentAbility = "SRCurrentAbility";
			public const string SelfAssessmentNotes = "SelfAssessmentNotes";
			public const string SRCurrentAbilitySupervisor = "SRCurrentAbilitySupervisor";
			public const string SRCurrentAbilitySupervisor2 = "SRCurrentAbilitySupervisor2";
			public const string SRReview = "SRReview";
			public const string SRRecomendation = "SRRecomendation";
			public const string SRConclusion = "SRConclusion";
			public const string Notes = "Notes";
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
			lock (typeof(CredentialProcessSheetMetadata))
			{
				if (CredentialProcessSheetMetadata.mapDelegates == null)
				{
					CredentialProcessSheetMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialProcessSheetMetadata.meta == null)
				{
					CredentialProcessSheetMetadata.meta = new CredentialProcessSheetMetadata();
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
				meta.AddTypeMap("QuestionnaireItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRCurrentAbility", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SelfAssessmentNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCurrentAbilitySupervisor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCurrentAbilitySupervisor2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReview", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRecomendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConclusion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialProcessSheet";
				meta.Destination = "CredentialProcessSheet";
				meta.spInsert = "proc_CredentialProcessSheetInsert";
				meta.spUpdate = "proc_CredentialProcessSheetUpdate";
				meta.spDelete = "proc_CredentialProcessSheetDelete";
				meta.spLoadAll = "proc_CredentialProcessSheetLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialProcessSheetLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialProcessSheetMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
