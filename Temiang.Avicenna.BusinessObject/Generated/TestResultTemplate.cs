/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:27 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esTestResultTemplateCollection : esEntityCollectionWAuditLog
	{
		public esTestResultTemplateCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TestResultTemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esTestResultTemplateQuery query)
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
			this.InitQuery(query as esTestResultTemplateQuery);
		}
		#endregion
		
		virtual public TestResultTemplate DetachEntity(TestResultTemplate entity)
		{
			return base.DetachEntity(entity) as TestResultTemplate;
		}
		
		virtual public TestResultTemplate AttachEntity(TestResultTemplate entity)
		{
			return base.AttachEntity(entity) as TestResultTemplate;
		}
		
		virtual public void Combine(TestResultTemplateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TestResultTemplate this[int index]
		{
			get
			{
				return base[index] as TestResultTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TestResultTemplate);
		}
	}



	[Serializable]
	abstract public class esTestResultTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTestResultTemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esTestResultTemplate()
		{

		}

		public esTestResultTemplate(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 testResultTemplateID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(testResultTemplateID);
			else
				return LoadByPrimaryKeyStoredProcedure(testResultTemplateID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 testResultTemplateID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(testResultTemplateID);
			else
				return LoadByPrimaryKeyStoredProcedure(testResultTemplateID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 testResultTemplateID)
		{
			esTestResultTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TestResultTemplateID == testResultTemplateID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 testResultTemplateID)
		{
			esParameters parms = new esParameters();
			parms.Add("TestResultTemplateID",testResultTemplateID);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "TestResultTemplateID": this.str.TestResultTemplateID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "TestResultTemplateName": this.str.TestResultTemplateName = (string)value; break;							
						case "TestResult": this.str.TestResult = (string)value; break;							
						case "TestSummary": this.str.TestSummary = (string)value; break;							
						case "TestSuggest": this.str.TestSuggest = (string)value; break;							
						case "TestResultOtherLang": this.str.TestResultOtherLang = (string)value; break;							
						case "TestSummaryOtherLang": this.str.TestSummaryOtherLang = (string)value; break;							
						case "TestSuggestOtherLang": this.str.TestSuggestOtherLang = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TestResultTemplateID":
						
							if (value == null || value is System.Int64)
								this.TestResultTemplateID = (System.Int64?)value;
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
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}
		
		
		/// <summary>
		/// Maps to TestResultTemplate.TestResultTemplateID
		/// </summary>
		virtual public System.Int64? TestResultTemplateID
		{
			get
			{
				return base.GetSystemInt64(TestResultTemplateMetadata.ColumnNames.TestResultTemplateID);
			}
			
			set
			{
				base.SetSystemInt64(TestResultTemplateMetadata.ColumnNames.TestResultTemplateID, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.TestResultTemplateName
		/// </summary>
		virtual public System.String TestResultTemplateName
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.TestResultTemplateName);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.TestResultTemplateName, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.TestResult
		/// </summary>
		virtual public System.String TestResult
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.TestResult);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.TestResult, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.TestSummary
		/// </summary>
		virtual public System.String TestSummary
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.TestSummary);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.TestSummary, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.TestSuggest
		/// </summary>
		virtual public System.String TestSuggest
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.TestSuggest);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.TestSuggest, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.TestResultOtherLang
		/// </summary>
		virtual public System.String TestResultOtherLang
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.TestResultOtherLang);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.TestResultOtherLang, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.TestSummaryOtherLang
		/// </summary>
		virtual public System.String TestSummaryOtherLang
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.TestSummaryOtherLang);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.TestSummaryOtherLang, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.TestSuggestOtherLang
		/// </summary>
		virtual public System.String TestSuggestOtherLang
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.TestSuggestOtherLang);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.TestSuggestOtherLang, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TestResultTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TestResultTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TestResultTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TestResultTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TestResultTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


		[BrowsableAttribute( false )]
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
			public esStrings(esTestResultTemplate entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TestResultTemplateID
			{
				get
				{
					System.Int64? data = entity.TestResultTemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResultTemplateID = null;
					else entity.TestResultTemplateID = Convert.ToInt64(value);
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
				
			public System.String TestResultTemplateName
			{
				get
				{
					System.String data = entity.TestResultTemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResultTemplateName = null;
					else entity.TestResultTemplateName = Convert.ToString(value);
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
			

			private esTestResultTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTestResultTemplateQuery query)
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
				throw new Exception("esTestResultTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TestResultTemplate : esTestResultTemplate
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esTestResultTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TestResultTemplateMetadata.Meta();
			}
		}	
		

		public esQueryItem TestResultTemplateID
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestResultTemplateID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem TestResultTemplateName
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestResultTemplateName, esSystemType.String);
			}
		} 
		
		public esQueryItem TestResult
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestResult, esSystemType.String);
			}
		} 
		
		public esQueryItem TestSummary
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestSummary, esSystemType.String);
			}
		} 
		
		public esQueryItem TestSuggest
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestSuggest, esSystemType.String);
			}
		} 
		
		public esQueryItem TestResultOtherLang
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestResultOtherLang, esSystemType.String);
			}
		} 
		
		public esQueryItem TestSummaryOtherLang
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestSummaryOtherLang, esSystemType.String);
			}
		} 
		
		public esQueryItem TestSuggestOtherLang
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.TestSuggestOtherLang, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TestResultTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TestResultTemplateCollection")]
	public partial class TestResultTemplateCollection : esTestResultTemplateCollection, IEnumerable<TestResultTemplate>
	{
		public TestResultTemplateCollection()
		{

		}
		
		public static implicit operator List<TestResultTemplate>(TestResultTemplateCollection coll)
		{
			List<TestResultTemplate> list = new List<TestResultTemplate>();
			
			foreach (TestResultTemplate emp in coll)
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
				return  TestResultTemplateMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TestResultTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TestResultTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TestResultTemplate();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TestResultTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TestResultTemplateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TestResultTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TestResultTemplate AddNew()
		{
			TestResultTemplate entity = base.AddNewEntity() as TestResultTemplate;
			
			return entity;
		}

		public TestResultTemplate FindByPrimaryKey(System.Int64 testResultTemplateID)
		{
			return base.FindByPrimaryKey(testResultTemplateID) as TestResultTemplate;
		}


		#region IEnumerable<TestResultTemplate> Members

		IEnumerator<TestResultTemplate> IEnumerable<TestResultTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TestResultTemplate;
			}
		}

		#endregion
		
		private TestResultTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TestResultTemplate' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TestResultTemplate ({TestResultTemplateID})")]
	[Serializable]
	public partial class TestResultTemplate : esTestResultTemplate
	{
		public TestResultTemplate()
		{

		}
	
		public TestResultTemplate(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TestResultTemplateMetadata.Meta();
			}
		}
		
		
		
		override protected esTestResultTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TestResultTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TestResultTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TestResultTemplateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TestResultTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TestResultTemplateQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TestResultTemplateQuery : esTestResultTemplateQuery
	{
		public TestResultTemplateQuery()
		{

		}		
		
		public TestResultTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TestResultTemplateQuery";
        }
		
			
	}


	[Serializable]
	public partial class TestResultTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TestResultTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestResultTemplateID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestResultTemplateID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestResultTemplateName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestResultTemplateName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestResult, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestResult;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestSummary, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestSummary;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestSuggest, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestSuggest;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestResultOtherLang, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestResultOtherLang;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestSummaryOtherLang, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestSummaryOtherLang;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.TestSuggestOtherLang, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.TestSuggestOtherLang;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TestResultTemplateMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TestResultTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TestResultTemplateMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string TestResultTemplateID = "TestResultTemplateID";
			 public const string ItemID = "ItemID";
			 public const string ParamedicID = "ParamedicID";
			 public const string TestResultTemplateName = "TestResultTemplateName";
			 public const string TestResult = "TestResult";
			 public const string TestSummary = "TestSummary";
			 public const string TestSuggest = "TestSuggest";
			 public const string TestResultOtherLang = "TestResultOtherLang";
			 public const string TestSummaryOtherLang = "TestSummaryOtherLang";
			 public const string TestSuggestOtherLang = "TestSuggestOtherLang";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TestResultTemplateID = "TestResultTemplateID";
			 public const string ItemID = "ItemID";
			 public const string ParamedicID = "ParamedicID";
			 public const string TestResultTemplateName = "TestResultTemplateName";
			 public const string TestResult = "TestResult";
			 public const string TestSummary = "TestSummary";
			 public const string TestSuggest = "TestSuggest";
			 public const string TestResultOtherLang = "TestResultOtherLang";
			 public const string TestSummaryOtherLang = "TestSummaryOtherLang";
			 public const string TestSuggestOtherLang = "TestSuggestOtherLang";
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
			lock (typeof(TestResultTemplateMetadata))
			{
				if(TestResultTemplateMetadata.mapDelegates == null)
				{
					TestResultTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TestResultTemplateMetadata.meta == null)
				{
					TestResultTemplateMetadata.meta = new TestResultTemplateMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				

				meta.AddTypeMap("TestResultTemplateID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResultTemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSummary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSuggest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResultOtherLang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSummaryOtherLang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestSuggestOtherLang", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TestResultTemplate";
				meta.Destination = "TestResultTemplate";
				
				meta.spInsert = "proc_TestResultTemplateInsert";				
				meta.spUpdate = "proc_TestResultTemplateUpdate";		
				meta.spDelete = "proc_TestResultTemplateDelete";
				meta.spLoadAll = "proc_TestResultTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_TestResultTemplateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TestResultTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
