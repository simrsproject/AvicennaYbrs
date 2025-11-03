/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:30:46 PM
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
	abstract public class esCredentialObservationInstrumentAssessmentSheetCollection : esEntityCollectionWAuditLog
	{
		public esCredentialObservationInstrumentAssessmentSheetCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialObservationInstrumentAssessmentSheetCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialObservationInstrumentAssessmentSheetQuery query)
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
			this.InitQuery(query as esCredentialObservationInstrumentAssessmentSheetQuery);
		}
		#endregion

		virtual public CredentialObservationInstrumentAssessmentSheet DetachEntity(CredentialObservationInstrumentAssessmentSheet entity)
		{
			return base.DetachEntity(entity) as CredentialObservationInstrumentAssessmentSheet;
		}

		virtual public CredentialObservationInstrumentAssessmentSheet AttachEntity(CredentialObservationInstrumentAssessmentSheet entity)
		{
			return base.AttachEntity(entity) as CredentialObservationInstrumentAssessmentSheet;
		}

		virtual public void Combine(CredentialObservationInstrumentAssessmentSheetCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialObservationInstrumentAssessmentSheet this[int index]
		{
			get
			{
				return base[index] as CredentialObservationInstrumentAssessmentSheet;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialObservationInstrumentAssessmentSheet);
		}
	}

	[Serializable]
	abstract public class esCredentialObservationInstrumentAssessmentSheet : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialObservationInstrumentAssessmentSheetQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialObservationInstrumentAssessmentSheet()
		{
		}

		public esCredentialObservationInstrumentAssessmentSheet(DataRow row)
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
			esCredentialObservationInstrumentAssessmentSheetQuery query = this.GetDynamicQuery();
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
						case "IsEval1": this.str.IsEval1 = (string)value; break;
						case "IsEval2": this.str.IsEval2 = (string)value; break;
						case "IsEval3": this.str.IsEval3 = (string)value; break;
						case "IsEval4": this.str.IsEval4 = (string)value; break;
						case "IsEval5": this.str.IsEval5 = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Score": this.str.Score = (string)value; break;
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
						case "IsEval1":

							if (value == null || value is System.Boolean)
								this.IsEval1 = (System.Boolean?)value;
							break;
						case "IsEval2":

							if (value == null || value is System.Boolean)
								this.IsEval2 = (System.Boolean?)value;
							break;
						case "IsEval3":

							if (value == null || value is System.Boolean)
								this.IsEval3 = (System.Boolean?)value;
							break;
						case "IsEval4":

							if (value == null || value is System.Boolean)
								this.IsEval4 = (System.Boolean?)value;
							break;
						case "IsEval5":

							if (value == null || value is System.Boolean)
								this.IsEval5 = (System.Boolean?)value;
							break;
						case "Score":

							if (value == null || value is System.Decimal)
								this.Score = (System.Decimal?)value;
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
		/// Maps to CredentialObservationInstrumentAssessmentSheet.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.QuestionnaireItemID
		/// </summary>
		virtual public System.Int32? QuestionnaireItemID
		{
			get
			{
				return base.GetSystemInt32(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.QuestionnaireItemID);
			}

			set
			{
				base.SetSystemInt32(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.QuestionnaireItemID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.IsEval1
		/// </summary>
		virtual public System.Boolean? IsEval1
		{
			get
			{
				return base.GetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval1);
			}

			set
			{
				base.SetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval1, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.IsEval2
		/// </summary>
		virtual public System.Boolean? IsEval2
		{
			get
			{
				return base.GetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval2);
			}

			set
			{
				base.SetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval2, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.IsEval3
		/// </summary>
		virtual public System.Boolean? IsEval3
		{
			get
			{
				return base.GetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval3);
			}

			set
			{
				base.SetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval3, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.IsEval4
		/// </summary>
		virtual public System.Boolean? IsEval4
		{
			get
			{
				return base.GetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval4);
			}

			set
			{
				base.SetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval4, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.IsEval5
		/// </summary>
		virtual public System.Boolean? IsEval5
		{
			get
			{
				return base.GetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval5);
			}

			set
			{
				base.SetSystemBoolean(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval5, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.Score
		/// </summary>
		virtual public System.Decimal? Score
		{
			get
			{
				return base.GetSystemDecimal(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Score);
			}

			set
			{
				base.SetSystemDecimal(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Score, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialObservationInstrumentAssessmentSheet.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialObservationInstrumentAssessmentSheet entity)
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
			public System.String IsEval1
			{
				get
				{
					System.Boolean? data = entity.IsEval1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEval1 = null;
					else entity.IsEval1 = Convert.ToBoolean(value);
				}
			}
			public System.String IsEval2
			{
				get
				{
					System.Boolean? data = entity.IsEval2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEval2 = null;
					else entity.IsEval2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsEval3
			{
				get
				{
					System.Boolean? data = entity.IsEval3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEval3 = null;
					else entity.IsEval3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsEval4
			{
				get
				{
					System.Boolean? data = entity.IsEval4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEval4 = null;
					else entity.IsEval4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsEval5
			{
				get
				{
					System.Boolean? data = entity.IsEval5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEval5 = null;
					else entity.IsEval5 = Convert.ToBoolean(value);
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
			public System.String Score
			{
				get
				{
					System.Decimal? data = entity.Score;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score = null;
					else entity.Score = Convert.ToDecimal(value);
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
			private esCredentialObservationInstrumentAssessmentSheet entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialObservationInstrumentAssessmentSheetQuery query)
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
				throw new Exception("esCredentialObservationInstrumentAssessmentSheet can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialObservationInstrumentAssessmentSheet : esCredentialObservationInstrumentAssessmentSheet
	{
	}

	[Serializable]
	abstract public class esCredentialObservationInstrumentAssessmentSheetQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialObservationInstrumentAssessmentSheetMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionnaireItemID
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.QuestionnaireItemID, esSystemType.Int32);
			}
		}

		public esQueryItem IsEval1
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval1, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEval2
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEval3
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEval4
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEval5
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval5, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Score
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Score, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialObservationInstrumentAssessmentSheetCollection")]
	public partial class CredentialObservationInstrumentAssessmentSheetCollection : esCredentialObservationInstrumentAssessmentSheetCollection, IEnumerable<CredentialObservationInstrumentAssessmentSheet>
	{
		public CredentialObservationInstrumentAssessmentSheetCollection()
		{

		}

		public static implicit operator List<CredentialObservationInstrumentAssessmentSheet>(CredentialObservationInstrumentAssessmentSheetCollection coll)
		{
			List<CredentialObservationInstrumentAssessmentSheet> list = new List<CredentialObservationInstrumentAssessmentSheet>();

			foreach (CredentialObservationInstrumentAssessmentSheet emp in coll)
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
				return CredentialObservationInstrumentAssessmentSheetMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialObservationInstrumentAssessmentSheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialObservationInstrumentAssessmentSheet(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialObservationInstrumentAssessmentSheet();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialObservationInstrumentAssessmentSheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialObservationInstrumentAssessmentSheetQuery();
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
		public bool Load(CredentialObservationInstrumentAssessmentSheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialObservationInstrumentAssessmentSheet AddNew()
		{
			CredentialObservationInstrumentAssessmentSheet entity = base.AddNewEntity() as CredentialObservationInstrumentAssessmentSheet;

			return entity;
		}
		public CredentialObservationInstrumentAssessmentSheet FindByPrimaryKey(String transactionNo, Int32 questionnaireItemID)
		{
			return base.FindByPrimaryKey(transactionNo, questionnaireItemID) as CredentialObservationInstrumentAssessmentSheet;
		}

		#region IEnumerable< CredentialObservationInstrumentAssessmentSheet> Members

		IEnumerator<CredentialObservationInstrumentAssessmentSheet> IEnumerable<CredentialObservationInstrumentAssessmentSheet>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialObservationInstrumentAssessmentSheet;
			}
		}

		#endregion

		private CredentialObservationInstrumentAssessmentSheetQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialObservationInstrumentAssessmentSheet' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialObservationInstrumentAssessmentSheet ({TransactionNo, QuestionnaireItemID})")]
	[Serializable]
	public partial class CredentialObservationInstrumentAssessmentSheet : esCredentialObservationInstrumentAssessmentSheet
	{
		public CredentialObservationInstrumentAssessmentSheet()
		{
		}

		public CredentialObservationInstrumentAssessmentSheet(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialObservationInstrumentAssessmentSheetMetadata.Meta();
			}
		}

		override protected esCredentialObservationInstrumentAssessmentSheetQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialObservationInstrumentAssessmentSheetQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialObservationInstrumentAssessmentSheetQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialObservationInstrumentAssessmentSheetQuery();
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
		public bool Load(CredentialObservationInstrumentAssessmentSheetQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialObservationInstrumentAssessmentSheetQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialObservationInstrumentAssessmentSheetQuery : esCredentialObservationInstrumentAssessmentSheetQuery
	{
		public CredentialObservationInstrumentAssessmentSheetQuery()
		{

		}

		public CredentialObservationInstrumentAssessmentSheetQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialObservationInstrumentAssessmentSheetQuery";
		}
	}

	[Serializable]
	public partial class CredentialObservationInstrumentAssessmentSheetMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialObservationInstrumentAssessmentSheetMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.QuestionnaireItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.QuestionnaireItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval1, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.IsEval1;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval2, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.IsEval2;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval3, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.IsEval3;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval4, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.IsEval4;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.IsEval5, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.IsEval5;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.Score, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.Score;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialObservationInstrumentAssessmentSheetMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialObservationInstrumentAssessmentSheetMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialObservationInstrumentAssessmentSheetMetadata Meta()
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
			public const string IsEval1 = "IsEval1";
			public const string IsEval2 = "IsEval2";
			public const string IsEval3 = "IsEval3";
			public const string IsEval4 = "IsEval4";
			public const string IsEval5 = "IsEval5";
			public const string Notes = "Notes";
			public const string Score = "Score";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string QuestionnaireItemID = "QuestionnaireItemID";
			public const string IsEval1 = "IsEval1";
			public const string IsEval2 = "IsEval2";
			public const string IsEval3 = "IsEval3";
			public const string IsEval4 = "IsEval4";
			public const string IsEval5 = "IsEval5";
			public const string Notes = "Notes";
			public const string Score = "Score";
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
			lock (typeof(CredentialObservationInstrumentAssessmentSheetMetadata))
			{
				if (CredentialObservationInstrumentAssessmentSheetMetadata.mapDelegates == null)
				{
					CredentialObservationInstrumentAssessmentSheetMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialObservationInstrumentAssessmentSheetMetadata.meta == null)
				{
					CredentialObservationInstrumentAssessmentSheetMetadata.meta = new CredentialObservationInstrumentAssessmentSheetMetadata();
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
				meta.AddTypeMap("IsEval1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEval2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEval3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEval4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEval5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Score", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialObservationInstrumentAssessmentSheet";
				meta.Destination = "CredentialObservationInstrumentAssessmentSheet";
				meta.spInsert = "proc_CredentialObservationInstrumentAssessmentSheetInsert";
				meta.spUpdate = "proc_CredentialObservationInstrumentAssessmentSheetUpdate";
				meta.spDelete = "proc_CredentialObservationInstrumentAssessmentSheetDelete";
				meta.spLoadAll = "proc_CredentialObservationInstrumentAssessmentSheetLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialObservationInstrumentAssessmentSheetLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialObservationInstrumentAssessmentSheetMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
