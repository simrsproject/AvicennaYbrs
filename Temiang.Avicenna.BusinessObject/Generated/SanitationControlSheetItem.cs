/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/20/2022 5:16:56 PM
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
	abstract public class esSanitationControlSheetItemCollection : esEntityCollectionWAuditLog
	{
		public esSanitationControlSheetItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationControlSheetItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationControlSheetItemQuery query)
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
			this.InitQuery(query as esSanitationControlSheetItemQuery);
		}
		#endregion

		virtual public SanitationControlSheetItem DetachEntity(SanitationControlSheetItem entity)
		{
			return base.DetachEntity(entity) as SanitationControlSheetItem;
		}

		virtual public SanitationControlSheetItem AttachEntity(SanitationControlSheetItem entity)
		{
			return base.AttachEntity(entity) as SanitationControlSheetItem;
		}

		virtual public void Combine(SanitationControlSheetItemCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationControlSheetItem this[int index]
		{
			get
			{
				return base[index] as SanitationControlSheetItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationControlSheetItem);
		}
	}

	[Serializable]
	abstract public class esSanitationControlSheetItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationControlSheetItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationControlSheetItem()
		{
		}

		public esSanitationControlSheetItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String controlSheetNo, String questionFormID, String questionGroupID, String questionID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(controlSheetNo, questionFormID, questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(controlSheetNo, questionFormID, questionGroupID, questionID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String controlSheetNo, String questionFormID, String questionGroupID, String questionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(controlSheetNo, questionFormID, questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(controlSheetNo, questionFormID, questionGroupID, questionID);
		}

		private bool LoadByPrimaryKeyDynamic(String controlSheetNo, String questionFormID, String questionGroupID, String questionID)
		{
			esSanitationControlSheetItemQuery query = this.GetDynamicQuery();
			query.Where(query.ControlSheetNo == controlSheetNo, query.QuestionFormID == questionFormID, query.QuestionGroupID == questionGroupID, query.QuestionID == questionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String controlSheetNo, String questionFormID, String questionGroupID, String questionID)
		{
			esParameters parms = new esParameters();
			parms.Add("ControlSheetNo", controlSheetNo);
			parms.Add("QuestionFormID", questionFormID);
			parms.Add("QuestionGroupID", questionGroupID);
			parms.Add("QuestionID", questionID);
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
						case "ControlSheetNo": this.str.ControlSheetNo = (string)value; break;
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "QuestionGroupID": this.str.QuestionGroupID = (string)value; break;
						case "QuestionID": this.str.QuestionID = (string)value; break;
						case "QuestionAnswerPrefix": this.str.QuestionAnswerPrefix = (string)value; break;
						case "QuestionAnswerSuffix": this.str.QuestionAnswerSuffix = (string)value; break;
						case "QuestionAnswerSelectionLineID": this.str.QuestionAnswerSelectionLineID = (string)value; break;
						case "QuestionAnswerText": this.str.QuestionAnswerText = (string)value; break;
						case "QuestionAnswerText2": this.str.QuestionAnswerText2 = (string)value; break;
						case "QuestionAnswerNum": this.str.QuestionAnswerNum = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "QuestionAnswerNum":

							if (value == null || value is System.Decimal)
								this.QuestionAnswerNum = (System.Decimal?)value;
							break;
						case "BodyImage":

							if (value == null || value is System.Byte[])
								this.BodyImage = (System.Byte[])value;
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
		/// Maps to SanitationControlSheetItem.ControlSheetNo
		/// </summary>
		virtual public System.String ControlSheetNo
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.ControlSheetNo);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.ControlSheetNo, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionFormID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionGroupID
		/// </summary>
		virtual public System.String QuestionGroupID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionGroupID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionGroupID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionAnswerPrefix
		/// </summary>
		virtual public System.String QuestionAnswerPrefix
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerPrefix);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerPrefix, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionAnswerSuffix
		/// </summary>
		virtual public System.String QuestionAnswerSuffix
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSuffix);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSuffix, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionAnswerSelectionLineID
		/// </summary>
		virtual public System.String QuestionAnswerSelectionLineID
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSelectionLineID);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSelectionLineID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionAnswerText
		/// </summary>
		virtual public System.String QuestionAnswerText
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionAnswerText2
		/// </summary>
		virtual public System.String QuestionAnswerText2
		{
			get
			{
				return base.GetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText2);
			}

			set
			{
				base.SetSystemString(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText2, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.QuestionAnswerNum
		/// </summary>
		virtual public System.Decimal? QuestionAnswerNum
		{
			get
			{
				return base.GetSystemDecimal(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerNum);
			}

			set
			{
				base.SetSystemDecimal(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerNum, value);
			}
		}
		/// <summary>
		/// Maps to SanitationControlSheetItem.BodyImage
		/// </summary>
		virtual public System.Byte[] BodyImage
		{
			get
			{
				return base.GetSystemByteArray(SanitationControlSheetItemMetadata.ColumnNames.BodyImage);
			}

			set
			{
				base.SetSystemByteArray(SanitationControlSheetItemMetadata.ColumnNames.BodyImage, value);
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
			public esStrings(esSanitationControlSheetItem entity)
			{
				this.entity = entity;
			}
			public System.String ControlSheetNo
			{
				get
				{
					System.String data = entity.ControlSheetNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControlSheetNo = null;
					else entity.ControlSheetNo = Convert.ToString(value);
				}
			}
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
				}
			}
			public System.String QuestionGroupID
			{
				get
				{
					System.String data = entity.QuestionGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionGroupID = null;
					else entity.QuestionGroupID = Convert.ToString(value);
				}
			}
			public System.String QuestionID
			{
				get
				{
					System.String data = entity.QuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionID = null;
					else entity.QuestionID = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerPrefix
			{
				get
				{
					System.String data = entity.QuestionAnswerPrefix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerPrefix = null;
					else entity.QuestionAnswerPrefix = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerSuffix
			{
				get
				{
					System.String data = entity.QuestionAnswerSuffix;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSuffix = null;
					else entity.QuestionAnswerSuffix = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerSelectionLineID
			{
				get
				{
					System.String data = entity.QuestionAnswerSelectionLineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSelectionLineID = null;
					else entity.QuestionAnswerSelectionLineID = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerText
			{
				get
				{
					System.String data = entity.QuestionAnswerText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerText = null;
					else entity.QuestionAnswerText = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerText2
			{
				get
				{
					System.String data = entity.QuestionAnswerText2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerText2 = null;
					else entity.QuestionAnswerText2 = Convert.ToString(value);
				}
			}
			public System.String QuestionAnswerNum
			{
				get
				{
					System.Decimal? data = entity.QuestionAnswerNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerNum = null;
					else entity.QuestionAnswerNum = Convert.ToDecimal(value);
				}
			}
			private esSanitationControlSheetItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationControlSheetItemQuery query)
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
				throw new Exception("esSanitationControlSheetItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationControlSheetItem : esSanitationControlSheetItem
	{
	}

	[Serializable]
	abstract public class esSanitationControlSheetItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationControlSheetItemMetadata.Meta();
			}
		}

		public esQueryItem ControlSheetNo
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.ControlSheetNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		}

		public esQueryItem QuestionGroupID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
			}
		}

		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerPrefix
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerPrefix, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerSuffix
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSuffix, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerSelectionLineID
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSelectionLineID, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerText
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerText2
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText2, esSystemType.String);
			}
		}

		public esQueryItem QuestionAnswerNum
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerNum, esSystemType.Decimal);
			}
		}

		public esQueryItem BodyImage
		{
			get
			{
				return new esQueryItem(this, SanitationControlSheetItemMetadata.ColumnNames.BodyImage, esSystemType.ByteArray);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationControlSheetItemCollection")]
	public partial class SanitationControlSheetItemCollection : esSanitationControlSheetItemCollection, IEnumerable<SanitationControlSheetItem>
	{
		public SanitationControlSheetItemCollection()
		{

		}

		public static implicit operator List<SanitationControlSheetItem>(SanitationControlSheetItemCollection coll)
		{
			List<SanitationControlSheetItem> list = new List<SanitationControlSheetItem>();

			foreach (SanitationControlSheetItem emp in coll)
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
				return SanitationControlSheetItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationControlSheetItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationControlSheetItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationControlSheetItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationControlSheetItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationControlSheetItemQuery();
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
		public bool Load(SanitationControlSheetItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationControlSheetItem AddNew()
		{
			SanitationControlSheetItem entity = base.AddNewEntity() as SanitationControlSheetItem;

			return entity;
		}
		public SanitationControlSheetItem FindByPrimaryKey(String controlSheetNo, String questionFormID, String questionGroupID, String questionID)
		{
			return base.FindByPrimaryKey(controlSheetNo, questionFormID, questionGroupID, questionID) as SanitationControlSheetItem;
		}

		#region IEnumerable< SanitationControlSheetItem> Members

		IEnumerator<SanitationControlSheetItem> IEnumerable<SanitationControlSheetItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationControlSheetItem;
			}
		}

		#endregion

		private SanitationControlSheetItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationControlSheetItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationControlSheetItem ({ControlSheetNo, QuestionFormID, QuestionGroupID, QuestionID})")]
	[Serializable]
	public partial class SanitationControlSheetItem : esSanitationControlSheetItem
	{
		public SanitationControlSheetItem()
		{
		}

		public SanitationControlSheetItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationControlSheetItemMetadata.Meta();
			}
		}

		override protected esSanitationControlSheetItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationControlSheetItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationControlSheetItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationControlSheetItemQuery();
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
		public bool Load(SanitationControlSheetItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationControlSheetItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationControlSheetItemQuery : esSanitationControlSheetItemQuery
	{
		public SanitationControlSheetItemQuery()
		{

		}

		public SanitationControlSheetItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationControlSheetItemQuery";
		}
	}

	[Serializable]
	public partial class SanitationControlSheetItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationControlSheetItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.ControlSheetNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.ControlSheetNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionGroupID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerPrefix, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionAnswerPrefix;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSuffix, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionAnswerSuffix;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerSelectionLineID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionAnswerSelectionLineID;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionAnswerText;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerText2, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionAnswerText2;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.QuestionAnswerNum, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.QuestionAnswerNum;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationControlSheetItemMetadata.ColumnNames.BodyImage, 10, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = SanitationControlSheetItemMetadata.PropertyNames.BodyImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationControlSheetItemMetadata Meta()
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
			public const string ControlSheetNo = "ControlSheetNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionID = "QuestionID";
			public const string QuestionAnswerPrefix = "QuestionAnswerPrefix";
			public const string QuestionAnswerSuffix = "QuestionAnswerSuffix";
			public const string QuestionAnswerSelectionLineID = "QuestionAnswerSelectionLineID";
			public const string QuestionAnswerText = "QuestionAnswerText";
			public const string QuestionAnswerText2 = "QuestionAnswerText2";
			public const string QuestionAnswerNum = "QuestionAnswerNum";
			public const string BodyImage = "BodyImage";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ControlSheetNo = "ControlSheetNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string QuestionGroupID = "QuestionGroupID";
			public const string QuestionID = "QuestionID";
			public const string QuestionAnswerPrefix = "QuestionAnswerPrefix";
			public const string QuestionAnswerSuffix = "QuestionAnswerSuffix";
			public const string QuestionAnswerSelectionLineID = "QuestionAnswerSelectionLineID";
			public const string QuestionAnswerText = "QuestionAnswerText";
			public const string QuestionAnswerText2 = "QuestionAnswerText2";
			public const string QuestionAnswerNum = "QuestionAnswerNum";
			public const string BodyImage = "BodyImage";
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
			lock (typeof(SanitationControlSheetItemMetadata))
			{
				if (SanitationControlSheetItemMetadata.mapDelegates == null)
				{
					SanitationControlSheetItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationControlSheetItemMetadata.meta == null)
				{
					SanitationControlSheetItemMetadata.meta = new SanitationControlSheetItemMetadata();
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

				meta.AddTypeMap("ControlSheetNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerPrefix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerSuffix", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerSelectionLineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerText2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerNum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BodyImage", new esTypeMap("image", "System.Byte[]"));


				meta.Source = "SanitationControlSheetItem";
				meta.Destination = "SanitationControlSheetItem";
				meta.spInsert = "proc_SanitationControlSheetItemInsert";
				meta.spUpdate = "proc_SanitationControlSheetItemUpdate";
				meta.spDelete = "proc_SanitationControlSheetItemDelete";
				meta.spLoadAll = "proc_SanitationControlSheetItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationControlSheetItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationControlSheetItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
