/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/12/2022 2:29:22 PM
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
	abstract public class esCredentialQuestionnaireItemCollection : esEntityCollectionWAuditLog
	{
		public esCredentialQuestionnaireItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialQuestionnaireItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialQuestionnaireItemQuery query)
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
			this.InitQuery(query as esCredentialQuestionnaireItemQuery);
		}
		#endregion

		virtual public CredentialQuestionnaireItem DetachEntity(CredentialQuestionnaireItem entity)
		{
			return base.DetachEntity(entity) as CredentialQuestionnaireItem;
		}

		virtual public CredentialQuestionnaireItem AttachEntity(CredentialQuestionnaireItem entity)
		{
			return base.AttachEntity(entity) as CredentialQuestionnaireItem;
		}

		virtual public void Combine(CredentialQuestionnaireItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialQuestionnaireItem this[int index]
		{
			get
			{
				return base[index] as CredentialQuestionnaireItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialQuestionnaireItem);
		}
	}

	[Serializable]
	abstract public class esCredentialQuestionnaireItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialQuestionnaireItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialQuestionnaireItem()
		{
		}

		public esCredentialQuestionnaireItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 questionnaireItemID, Int32 questionnaireID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionnaireItemID, questionnaireID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionnaireItemID, questionnaireID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 questionnaireItemID, Int32 questionnaireID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionnaireItemID, questionnaireID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionnaireItemID, questionnaireID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 questionnaireItemID, Int32 questionnaireID)
		{
			esCredentialQuestionnaireItemQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionnaireItemID == questionnaireItemID, query.QuestionnaireID == questionnaireID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 questionnaireItemID, Int32 questionnaireID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionnaireItemID", questionnaireItemID);
			parms.Add("QuestionnaireID", questionnaireID);
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
						case "QuestionnaireItemID": this.str.QuestionnaireItemID = (string)value; break;
						case "QuestionnaireID": this.str.QuestionnaireID = (string)value; break;
						case "QuestionCode": this.str.QuestionCode = (string)value; break;
						case "QuestionNo": this.str.QuestionNo = (string)value; break;
						case "QuestionName": this.str.QuestionName = (string)value; break;
						case "SRCredentialQuestionLevel": this.str.SRCredentialQuestionLevel = (string)value; break;
						case "SRCredentialActionType": this.str.SRCredentialActionType = (string)value; break;
						case "IsDetail": this.str.IsDetail = (string)value; break;
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
						case "QuestionnaireID":

							if (value == null || value is System.Int32)
								this.QuestionnaireID = (System.Int32?)value;
							break;
						case "IsDetail":

							if (value == null || value is System.Boolean)
								this.IsDetail = (System.Boolean?)value;
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
		/// Maps to CredentialQuestionnaireItem.QuestionnaireItemID
		/// </summary>
		virtual public System.Int32? QuestionnaireItemID
		{
			get
			{
				return base.GetSystemInt32(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID);
			}

			set
			{
				base.SetSystemInt32(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.QuestionnaireID
		/// </summary>
		virtual public System.Int32? QuestionnaireID
		{
			get
			{
				return base.GetSystemInt32(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireID);
			}

			set
			{
				base.SetSystemInt32(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.QuestionCode
		/// </summary>
		virtual public System.String QuestionCode
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionCode);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionCode, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.QuestionNo
		/// </summary>
		virtual public System.String QuestionNo
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionNo);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.QuestionName
		/// </summary>
		virtual public System.String QuestionName
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionName);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.SRCredentialQuestionLevel
		/// </summary>
		virtual public System.String SRCredentialQuestionLevel
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialQuestionLevel);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialQuestionLevel, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.SRCredentialActionType
		/// </summary>
		virtual public System.String SRCredentialActionType
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialActionType);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialActionType, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.IsDetail
		/// </summary>
		virtual public System.Boolean? IsDetail
		{
			get
			{
				return base.GetSystemBoolean(CredentialQuestionnaireItemMetadata.ColumnNames.IsDetail);
			}

			set
			{
				base.SetSystemBoolean(CredentialQuestionnaireItemMetadata.ColumnNames.IsDetail, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialQuestionnaireItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialQuestionnaireItem entity)
			{
				this.entity = entity;
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
			public System.String QuestionnaireID
			{
				get
				{
					System.Int32? data = entity.QuestionnaireID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionnaireID = null;
					else entity.QuestionnaireID = Convert.ToInt32(value);
				}
			}
			public System.String QuestionCode
			{
				get
				{
					System.String data = entity.QuestionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionCode = null;
					else entity.QuestionCode = Convert.ToString(value);
				}
			}
			public System.String QuestionNo
			{
				get
				{
					System.String data = entity.QuestionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionNo = null;
					else entity.QuestionNo = Convert.ToString(value);
				}
			}
			public System.String QuestionName
			{
				get
				{
					System.String data = entity.QuestionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionName = null;
					else entity.QuestionName = Convert.ToString(value);
				}
			}
			public System.String SRCredentialQuestionLevel
			{
				get
				{
					System.String data = entity.SRCredentialQuestionLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCredentialQuestionLevel = null;
					else entity.SRCredentialQuestionLevel = Convert.ToString(value);
				}
			}
			public System.String SRCredentialActionType
			{
				get
				{
					System.String data = entity.SRCredentialActionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCredentialActionType = null;
					else entity.SRCredentialActionType = Convert.ToString(value);
				}
			}
			public System.String IsDetail
			{
				get
				{
					System.Boolean? data = entity.IsDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDetail = null;
					else entity.IsDetail = Convert.ToBoolean(value);
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
			private esCredentialQuestionnaireItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialQuestionnaireItemQuery query)
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
				throw new Exception("esCredentialQuestionnaireItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialQuestionnaireItem : esCredentialQuestionnaireItem
	{
	}

	[Serializable]
	abstract public class esCredentialQuestionnaireItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialQuestionnaireItemMetadata.Meta();
			}
		}

		public esQueryItem QuestionnaireItemID
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionnaireID
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireID, esSystemType.Int32);
			}
		}

		public esQueryItem QuestionCode
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionCode, esSystemType.String);
			}
		}

		public esQueryItem QuestionNo
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionName
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.QuestionName, esSystemType.String);
			}
		}

		public esQueryItem SRCredentialQuestionLevel
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialQuestionLevel, esSystemType.String);
			}
		}

		public esQueryItem SRCredentialActionType
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialActionType, esSystemType.String);
			}
		}

		public esQueryItem IsDetail
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.IsDetail, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialQuestionnaireItemCollection")]
	public partial class CredentialQuestionnaireItemCollection : esCredentialQuestionnaireItemCollection, IEnumerable<CredentialQuestionnaireItem>
	{
		public CredentialQuestionnaireItemCollection()
		{

		}

		public static implicit operator List<CredentialQuestionnaireItem>(CredentialQuestionnaireItemCollection coll)
		{
			List<CredentialQuestionnaireItem> list = new List<CredentialQuestionnaireItem>();

			foreach (CredentialQuestionnaireItem emp in coll)
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
				return CredentialQuestionnaireItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialQuestionnaireItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialQuestionnaireItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialQuestionnaireItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialQuestionnaireItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialQuestionnaireItemQuery();
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
		public bool Load(CredentialQuestionnaireItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialQuestionnaireItem AddNew()
		{
			CredentialQuestionnaireItem entity = base.AddNewEntity() as CredentialQuestionnaireItem;

			return entity;
		}
		public CredentialQuestionnaireItem FindByPrimaryKey(Int32 questionnaireItemID, Int32 questionnaireID)
		{
			return base.FindByPrimaryKey(questionnaireItemID, questionnaireID) as CredentialQuestionnaireItem;
		}

		#region IEnumerable< CredentialQuestionnaireItem> Members

		IEnumerator<CredentialQuestionnaireItem> IEnumerable<CredentialQuestionnaireItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialQuestionnaireItem;
			}
		}

		#endregion

		private CredentialQuestionnaireItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialQuestionnaireItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialQuestionnaireItem ({QuestionnaireItemID, QuestionnaireID})")]
	[Serializable]
	public partial class CredentialQuestionnaireItem : esCredentialQuestionnaireItem
	{
		public CredentialQuestionnaireItem()
		{
		}

		public CredentialQuestionnaireItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialQuestionnaireItemMetadata.Meta();
			}
		}

		override protected esCredentialQuestionnaireItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialQuestionnaireItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialQuestionnaireItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialQuestionnaireItemQuery();
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
		public bool Load(CredentialQuestionnaireItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialQuestionnaireItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialQuestionnaireItemQuery : esCredentialQuestionnaireItemQuery
	{
		public CredentialQuestionnaireItemQuery()
		{

		}

		public CredentialQuestionnaireItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialQuestionnaireItemQuery";
		}
	}

	[Serializable]
	public partial class CredentialQuestionnaireItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialQuestionnaireItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireItemID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.QuestionnaireItemID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionnaireID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.QuestionnaireID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.QuestionCode;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.QuestionNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.QuestionName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.QuestionName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialQuestionLevel, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.SRCredentialQuestionLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.SRCredentialActionType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.SRCredentialActionType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.IsDetail, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.IsDetail;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialQuestionnaireItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialQuestionnaireItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialQuestionnaireItemMetadata Meta()
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
			public const string QuestionnaireItemID = "QuestionnaireItemID";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string QuestionCode = "QuestionCode";
			public const string QuestionNo = "QuestionNo";
			public const string QuestionName = "QuestionName";
			public const string SRCredentialQuestionLevel = "SRCredentialQuestionLevel";
			public const string SRCredentialActionType = "SRCredentialActionType";
			public const string IsDetail = "IsDetail";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string QuestionnaireItemID = "QuestionnaireItemID";
			public const string QuestionnaireID = "QuestionnaireID";
			public const string QuestionCode = "QuestionCode";
			public const string QuestionNo = "QuestionNo";
			public const string QuestionName = "QuestionName";
			public const string SRCredentialQuestionLevel = "SRCredentialQuestionLevel";
			public const string SRCredentialActionType = "SRCredentialActionType";
			public const string IsDetail = "IsDetail";
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
			lock (typeof(CredentialQuestionnaireItemMetadata))
			{
				if (CredentialQuestionnaireItemMetadata.mapDelegates == null)
				{
					CredentialQuestionnaireItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialQuestionnaireItemMetadata.meta == null)
				{
					CredentialQuestionnaireItemMetadata.meta = new CredentialQuestionnaireItemMetadata();
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

				meta.AddTypeMap("QuestionnaireItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionnaireID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("QuestionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCredentialQuestionLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCredentialActionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDetail", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialQuestionnaireItem";
				meta.Destination = "CredentialQuestionnaireItem";
				meta.spInsert = "proc_CredentialQuestionnaireItemInsert";
				meta.spUpdate = "proc_CredentialQuestionnaireItemUpdate";
				meta.spDelete = "proc_CredentialQuestionnaireItemDelete";
				meta.spLoadAll = "proc_CredentialQuestionnaireItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialQuestionnaireItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialQuestionnaireItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
