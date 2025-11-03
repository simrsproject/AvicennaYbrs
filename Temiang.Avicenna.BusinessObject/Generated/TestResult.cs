/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/29/2022 12:13:13 PM
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
	abstract public class esTestResultCollection : esEntityCollectionWAuditLog
	{
		public esTestResultCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TestResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esTestResultQuery query)
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
			this.InitQuery(query as esTestResultQuery);
		}
		#endregion

		virtual public TestResult DetachEntity(TestResult entity)
		{
			return base.DetachEntity(entity) as TestResult;
		}

		virtual public TestResult AttachEntity(TestResult entity)
		{
			return base.AttachEntity(entity) as TestResult;
		}

		virtual public void Combine(TestResultCollection collection)
		{
			base.Combine(collection);
		}

		new public TestResult this[int index]
		{
			get
			{
				return base[index] as TestResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TestResult);
		}
	}

	[Serializable]
	abstract public class esTestResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTestResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esTestResult()
		{
		}

		public esTestResult(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String itemID)
		{
			esTestResultQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("ItemID", itemID);
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "TestResultDateTime": this.str.TestResultDateTime = (string)value; break;
						case "TestResult": this.str.TestResult = (string)value; break;
						case "TestSummary": this.str.TestSummary = (string)value; break;
						case "TestSuggest": this.str.TestSuggest = (string)value; break;
						case "TestResultOtherLang": this.str.TestResultOtherLang = (string)value; break;
						case "TestSummaryOtherLang": this.str.TestSummaryOtherLang = (string)value; break;
						case "TestSuggestOtherLang": this.str.TestSuggestOtherLang = (string)value; break;
						case "TestResultHistory": this.str.TestResultHistory = (string)value; break;
						case "TestSummaryHistory": this.str.TestSummaryHistory = (string)value; break;
						case "TestSuggestHistory": this.str.TestSuggestHistory = (string)value; break;
						case "TestResultOtherLangHistory": this.str.TestResultOtherLangHistory = (string)value; break;
						case "TestSummaryOtherLangHistory": this.str.TestSummaryOtherLangHistory = (string)value; break;
						case "TestSuggestOtherLangHistory": this.str.TestSuggestOtherLangHistory = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ClinicalInfo": this.str.ClinicalInfo = (string)value; break;
						case "SRRadiologyCriticalResults": this.str.SRRadiologyCriticalResults = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TestResultDateTime":

							if (value == null || value is System.DateTime)
								this.TestResultDateTime = (System.DateTime?)value;
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
		/// Maps to TestResult.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestResultDateTime
		/// </summary>
		virtual public System.DateTime? TestResultDateTime
		{
			get
			{
				return base.GetSystemDateTime(TestResultMetadata.ColumnNames.TestResultDateTime);
			}

			set
			{
				base.SetSystemDateTime(TestResultMetadata.ColumnNames.TestResultDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestResult
		/// </summary>
		virtual public System.String TestResult
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestResult);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestResult, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSummary
		/// </summary>
		virtual public System.String TestSummary
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSummary);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSummary, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSuggest
		/// </summary>
		virtual public System.String TestSuggest
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSuggest);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSuggest, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestResultOtherLang
		/// </summary>
		virtual public System.String TestResultOtherLang
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestResultOtherLang);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestResultOtherLang, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSummaryOtherLang
		/// </summary>
		virtual public System.String TestSummaryOtherLang
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSummaryOtherLang);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSummaryOtherLang, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSuggestOtherLang
		/// </summary>
		virtual public System.String TestSuggestOtherLang
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSuggestOtherLang);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSuggestOtherLang, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestResultHistory
		/// </summary>
		virtual public System.String TestResultHistory
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestResultHistory);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestResultHistory, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSummaryHistory
		/// </summary>
		virtual public System.String TestSummaryHistory
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSummaryHistory);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSummaryHistory, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSuggestHistory
		/// </summary>
		virtual public System.String TestSuggestHistory
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSuggestHistory);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSuggestHistory, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestResultOtherLangHistory
		/// </summary>
		virtual public System.String TestResultOtherLangHistory
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestResultOtherLangHistory);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestResultOtherLangHistory, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSummaryOtherLangHistory
		/// </summary>
		virtual public System.String TestSummaryOtherLangHistory
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSummaryOtherLangHistory);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSummaryOtherLangHistory, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.TestSuggestOtherLangHistory
		/// </summary>
		virtual public System.String TestSuggestOtherLangHistory
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.TestSuggestOtherLangHistory);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.TestSuggestOtherLangHistory, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TestResultMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TestResultMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.ClinicalInfo
		/// </summary>
		virtual public System.String ClinicalInfo
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.ClinicalInfo);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.ClinicalInfo, value);
			}
		}
		/// <summary>
		/// Maps to TestResult.SRRadiologyCriticalResults
		/// </summary>
		virtual public System.String SRRadiologyCriticalResults
		{
			get
			{
				return base.GetSystemString(TestResultMetadata.ColumnNames.SRRadiologyCriticalResults);
			}

			set
			{
				base.SetSystemString(TestResultMetadata.ColumnNames.SRRadiologyCriticalResults, value);
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
			public esStrings(esTestResult entity)
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
			public System.String TestResultDateTime
			{
				get
				{
					System.DateTime? data = entity.TestResultDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResultDateTime = null;
					else entity.TestResultDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String TestResult
			{
				get
				{
					System.String data = entity.TestResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResult = null;
					else entity.TestResult = Convert.ToString(value);
				}
			}
			public System.String TestSummary
			{
				get
				{
					System.String data = entity.TestSummary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSummary = null;
					else entity.TestSummary = Convert.ToString(value);
				}
			}
			public System.String TestSuggest
			{
				get
				{
					System.String data = entity.TestSuggest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSuggest = null;
					else entity.TestSuggest = Convert.ToString(value);
				}
			}
			public System.String TestResultOtherLang
			{
				get
				{
					System.String data = entity.TestResultOtherLang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResultOtherLang = null;
					else entity.TestResultOtherLang = Convert.ToString(value);
				}
			}
			public System.String TestSummaryOtherLang
			{
				get
				{
					System.String data = entity.TestSummaryOtherLang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSummaryOtherLang = null;
					else entity.TestSummaryOtherLang = Convert.ToString(value);
				}
			}
			public System.String TestSuggestOtherLang
			{
				get
				{
					System.String data = entity.TestSuggestOtherLang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSuggestOtherLang = null;
					else entity.TestSuggestOtherLang = Convert.ToString(value);
				}
			}
			public System.String TestResultHistory
			{
				get
				{
					System.String data = entity.TestResultHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResultHistory = null;
					else entity.TestResultHistory = Convert.ToString(value);
				}
			}
			public System.String TestSummaryHistory
			{
				get
				{
					System.String data = entity.TestSummaryHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSummaryHistory = null;
					else entity.TestSummaryHistory = Convert.ToString(value);
				}
			}
			public System.String TestSuggestHistory
			{
				get
				{
					System.String data = entity.TestSuggestHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSuggestHistory = null;
					else entity.TestSuggestHistory = Convert.ToString(value);
				}
			}
			public System.String TestResultOtherLangHistory
			{
				get
				{
					System.String data = entity.TestResultOtherLangHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResultOtherLangHistory = null;
					else entity.TestResultOtherLangHistory = Convert.ToString(value);
				}
			}
			public System.String TestSummaryOtherLangHistory
			{
				get
				{
					System.String data = entity.TestSummaryOtherLangHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSummaryOtherLangHistory = null;
					else entity.TestSummaryOtherLangHistory = Convert.ToString(value);
				}
			}
			public System.String TestSuggestOtherLangHistory
			{
				get
				{
					System.String data = entity.TestSuggestOtherLangHistory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestSuggestOtherLangHistory = null;
					else entity.TestSuggestOtherLangHistory = Convert.ToString(value);
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
			public System.String ClinicalInfo
			{
				get
				{
					System.String data = entity.ClinicalInfo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicalInfo = null;
					else entity.ClinicalInfo = Convert.ToString(value);
				}
			}
			public System.String SRRadiologyCriticalResults
			{
				get
				{
					System.String data = entity.SRRadiologyCriticalResults;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRadiologyCriticalResults = null;
					else entity.SRRadiologyCriticalResults = Convert.ToString(value);
				}
			}
			private esTestResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTestResultQuery query)
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
				throw new Exception("esTestResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TestResult : esTestResult
	{
	}

	[Serializable]
	abstract public class esTestResultQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TestResultMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem TestResultDateTime
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestResultDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem TestResult
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestResult, esSystemType.String);
			}
		}

		public esQueryItem TestSummary
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSummary, esSystemType.String);
			}
		}

		public esQueryItem TestSuggest
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSuggest, esSystemType.String);
			}
		}

		public esQueryItem TestResultOtherLang
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestResultOtherLang, esSystemType.String);
			}
		}

		public esQueryItem TestSummaryOtherLang
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSummaryOtherLang, esSystemType.String);
			}
		}

		public esQueryItem TestSuggestOtherLang
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSuggestOtherLang, esSystemType.String);
			}
		}

		public esQueryItem TestResultHistory
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestResultHistory, esSystemType.String);
			}
		}

		public esQueryItem TestSummaryHistory
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSummaryHistory, esSystemType.String);
			}
		}

		public esQueryItem TestSuggestHistory
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSuggestHistory, esSystemType.String);
			}
		}

		public esQueryItem TestResultOtherLangHistory
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestResultOtherLangHistory, esSystemType.String);
			}
		}

		public esQueryItem TestSummaryOtherLangHistory
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSummaryOtherLangHistory, esSystemType.String);
			}
		}

		public esQueryItem TestSuggestOtherLangHistory
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.TestSuggestOtherLangHistory, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ClinicalInfo
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.ClinicalInfo, esSystemType.String);
			}
		}

		public esQueryItem SRRadiologyCriticalResults
		{
			get
			{
				return new esQueryItem(this, TestResultMetadata.ColumnNames.SRRadiologyCriticalResults, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TestResultCollection")]
	public partial class TestResultCollection : esTestResultCollection, IEnumerable<TestResult>
	{
		public TestResultCollection()
		{

		}

		public static implicit operator List<TestResult>(TestResultCollection coll)
		{
			List<TestResult> list = new List<TestResult>();

			foreach (TestResult emp in coll)
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
				return TestResultMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TestResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TestResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TestResult();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TestResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TestResultQuery();
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
		public bool Load(TestResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TestResult AddNew()
		{
			TestResult entity = base.AddNewEntity() as TestResult;

			return entity;
		}
		public TestResult FindByPrimaryKey(String transactionNo, String itemID)
		{
			return base.FindByPrimaryKey(transactionNo, itemID) as TestResult;
		}

		#region IEnumerable< TestResult> Members

		IEnumerator<TestResult> IEnumerable<TestResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TestResult;
			}
		}

		#endregion

		private TestResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TestResult' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TestResult ({TransactionNo, ItemID})")]
	[Serializable]
	public partial class TestResult : esTestResult
	{
		public TestResult()
		{
		}

		public TestResult(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TestResultMetadata.Meta();
			}
		}

		override protected esTestResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TestResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TestResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TestResultQuery();
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
		public bool Load(TestResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TestResultQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TestResultQuery : esTestResultQuery
	{
		public TestResultQuery()
		{

		}

		public TestResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TestResultQuery";
		}
	}

	[Serializable]
	public partial class TestResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TestResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestResultDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TestResultMetadata.PropertyNames.TestResultDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestResult, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestResult;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSummary, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSummary;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSuggest, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSuggest;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestResultOtherLang, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestResultOtherLang;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSummaryOtherLang, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSummaryOtherLang;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSuggestOtherLang, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSuggestOtherLang;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestResultHistory, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestResultHistory;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSummaryHistory, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSummaryHistory;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSuggestHistory, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSuggestHistory;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestResultOtherLangHistory, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestResultOtherLangHistory;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSummaryOtherLangHistory, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSummaryOtherLangHistory;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.TestSuggestOtherLangHistory, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.TestSuggestOtherLangHistory;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TestResultMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.ClinicalInfo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.ClinicalInfo;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TestResultMetadata.ColumnNames.SRRadiologyCriticalResults, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultMetadata.PropertyNames.SRRadiologyCriticalResults;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TestResultMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string ParamedicID = "ParamedicID";
			public const string TestResultDateTime = "TestResultDateTime";
			public const string TestResult = "TestResult";
			public const string TestSummary = "TestSummary";
			public const string TestSuggest = "TestSuggest";
			public const string TestResultOtherLang = "TestResultOtherLang";
			public const string TestSummaryOtherLang = "TestSummaryOtherLang";
			public const string TestSuggestOtherLang = "TestSuggestOtherLang";
			public const string TestResultHistory = "TestResultHistory";
			public const string TestSummaryHistory = "TestSummaryHistory";
			public const string TestSuggestHistory = "TestSuggestHistory";
			public const string TestResultOtherLangHistory = "TestResultOtherLangHistory";
			public const string TestSummaryOtherLangHistory = "TestSummaryOtherLangHistory";
			public const string TestSuggestOtherLangHistory = "TestSuggestOtherLangHistory";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ClinicalInfo = "ClinicalInfo";
			public const string SRRadiologyCriticalResults = "SRRadiologyCriticalResults";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string ItemID = "ItemID";
			public const string ParamedicID = "ParamedicID";
			public const string TestResultDateTime = "TestResultDateTime";
			public const string TestResult = "TestResult";
			public const string TestSummary = "TestSummary";
			public const string TestSuggest = "TestSuggest";
			public const string TestResultOtherLang = "TestResultOtherLang";
			public const string TestSummaryOtherLang = "TestSummaryOtherLang";
			public const string TestSuggestOtherLang = "TestSuggestOtherLang";
			public const string TestResultHistory = "TestResultHistory";
			public const string TestSummaryHistory = "TestSummaryHistory";
			public const string TestSuggestHistory = "TestSuggestHistory";
			public const string TestResultOtherLangHistory = "TestResultOtherLangHistory";
			public const string TestSummaryOtherLangHistory = "TestSummaryOtherLangHistory";
			public const string TestSuggestOtherLangHistory = "TestSuggestOtherLangHistory";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ClinicalInfo = "ClinicalInfo";
			public const string SRRadiologyCriticalResults = "SRRadiologyCriticalResults";
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
			lock (typeof(TestResultMetadata))
			{
				if (TestResultMetadata.mapDelegates == null)
				{
					TestResultMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TestResultMetadata.meta == null)
				{
					TestResultMetadata.meta = new TestResultMetadata();
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
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResultDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TestResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSummary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSuggest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResultOtherLang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSummaryOtherLang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSuggestOtherLang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResultHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSummaryHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSuggestHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResultOtherLangHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSummaryOtherLangHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSuggestOtherLangHistory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicalInfo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRadiologyCriticalResults", new esTypeMap("varchar", "System.String"));


				meta.Source = "TestResult";
				meta.Destination = "TestResult";
				meta.spInsert = "proc_TestResultInsert";
				meta.spUpdate = "proc_TestResultUpdate";
				meta.spDelete = "proc_TestResultDelete";
				meta.spLoadAll = "proc_TestResultLoadAll";
				meta.spLoadByPrimaryKey = "proc_TestResultLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TestResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
