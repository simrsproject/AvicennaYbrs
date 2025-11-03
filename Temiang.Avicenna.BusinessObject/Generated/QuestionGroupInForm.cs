/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:24 PM
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
	abstract public class esQuestionGroupInFormCollection : esEntityCollectionWAuditLog
	{
		public esQuestionGroupInFormCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "QuestionGroupInFormCollection";
		}

		#region Query Logic
		protected void InitQuery(esQuestionGroupInFormQuery query)
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
			this.InitQuery(query as esQuestionGroupInFormQuery);
		}
		#endregion
		
		virtual public QuestionGroupInForm DetachEntity(QuestionGroupInForm entity)
		{
			return base.DetachEntity(entity) as QuestionGroupInForm;
		}
		
		virtual public QuestionGroupInForm AttachEntity(QuestionGroupInForm entity)
		{
			return base.AttachEntity(entity) as QuestionGroupInForm;
		}
		
		virtual public void Combine(QuestionGroupInFormCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QuestionGroupInForm this[int index]
		{
			get
			{
				return base[index] as QuestionGroupInForm;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QuestionGroupInForm);
		}
	}



	[Serializable]
	abstract public class esQuestionGroupInForm : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQuestionGroupInFormQuery GetDynamicQuery()
		{
			return null;
		}

		public esQuestionGroupInForm()
		{

		}

		public esQuestionGroupInForm(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String questionFormID, System.String questionGroupID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionFormID, questionGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionFormID, questionGroupID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String questionFormID, System.String questionGroupID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionFormID, questionGroupID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionFormID, questionGroupID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String questionFormID, System.String questionGroupID)
		{
			esQuestionGroupInFormQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionFormID == questionFormID, query.QuestionGroupID == questionGroupID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String questionFormID, System.String questionGroupID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionFormID",questionFormID);			parms.Add("QuestionGroupID",questionGroupID);
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
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;							
						case "QuestionGroupID": this.str.QuestionGroupID = (string)value; break;							
						case "RowIndex": this.str.RowIndex = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RowIndex":
						
							if (value == null || value is System.Int32)
								this.RowIndex = (System.Int32?)value;
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
		/// Maps to QuestionGroupInForm.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(QuestionGroupInFormMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupInFormMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionGroupInForm.QuestionGroupID
		/// </summary>
		virtual public System.String QuestionGroupID
		{
			get
			{
				return base.GetSystemString(QuestionGroupInFormMetadata.ColumnNames.QuestionGroupID);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupInFormMetadata.ColumnNames.QuestionGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionGroupInForm.RowIndex
		/// </summary>
		virtual public System.Int32? RowIndex
		{
			get
			{
				return base.GetSystemInt32(QuestionGroupInFormMetadata.ColumnNames.RowIndex);
			}
			
			set
			{
				base.SetSystemInt32(QuestionGroupInFormMetadata.ColumnNames.RowIndex, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionGroupInForm.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QuestionGroupInFormMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QuestionGroupInFormMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionGroupInForm.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QuestionGroupInFormMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QuestionGroupInFormMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esQuestionGroupInForm entity)
			{
				this.entity = entity;
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
				
			public System.String RowIndex
			{
				get
				{
					System.Int32? data = entity.RowIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RowIndex = null;
					else entity.RowIndex = Convert.ToInt32(value);
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
			

			private esQuestionGroupInForm entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQuestionGroupInFormQuery query)
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
				throw new Exception("esQuestionGroupInForm can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class QuestionGroupInForm : esQuestionGroupInForm
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
	abstract public class esQuestionGroupInFormQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return QuestionGroupInFormMetadata.Meta();
			}
		}	
		

		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, QuestionGroupInFormMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionGroupID
		{
			get
			{
				return new esQueryItem(this, QuestionGroupInFormMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
			}
		} 
		
		public esQueryItem RowIndex
		{
			get
			{
				return new esQueryItem(this, QuestionGroupInFormMetadata.ColumnNames.RowIndex, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QuestionGroupInFormMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QuestionGroupInFormMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QuestionGroupInFormCollection")]
	public partial class QuestionGroupInFormCollection : esQuestionGroupInFormCollection, IEnumerable<QuestionGroupInForm>
	{
		public QuestionGroupInFormCollection()
		{

		}
		
		public static implicit operator List<QuestionGroupInForm>(QuestionGroupInFormCollection coll)
		{
			List<QuestionGroupInForm> list = new List<QuestionGroupInForm>();
			
			foreach (QuestionGroupInForm emp in coll)
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
				return  QuestionGroupInFormMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionGroupInFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QuestionGroupInForm(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QuestionGroupInForm();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public QuestionGroupInFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionGroupInFormQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(QuestionGroupInFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public QuestionGroupInForm AddNew()
		{
			QuestionGroupInForm entity = base.AddNewEntity() as QuestionGroupInForm;
			
			return entity;
		}

		public QuestionGroupInForm FindByPrimaryKey(System.String questionFormID, System.String questionGroupID)
		{
			return base.FindByPrimaryKey(questionFormID, questionGroupID) as QuestionGroupInForm;
		}


		#region IEnumerable<QuestionGroupInForm> Members

		IEnumerator<QuestionGroupInForm> IEnumerable<QuestionGroupInForm>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QuestionGroupInForm;
			}
		}

		#endregion
		
		private QuestionGroupInFormQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QuestionGroupInForm' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("QuestionGroupInForm ({QuestionFormID},{QuestionGroupID})")]
	[Serializable]
	public partial class QuestionGroupInForm : esQuestionGroupInForm
	{
		public QuestionGroupInForm()
		{

		}
	
		public QuestionGroupInForm(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QuestionGroupInFormMetadata.Meta();
			}
		}
		
		
		
		override protected esQuestionGroupInFormQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionGroupInFormQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public QuestionGroupInFormQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionGroupInFormQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(QuestionGroupInFormQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private QuestionGroupInFormQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class QuestionGroupInFormQuery : esQuestionGroupInFormQuery
	{
		public QuestionGroupInFormQuery()
		{

		}		
		
		public QuestionGroupInFormQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "QuestionGroupInFormQuery";
        }
		
			
	}


	[Serializable]
	public partial class QuestionGroupInFormMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QuestionGroupInFormMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(QuestionGroupInFormMetadata.ColumnNames.QuestionFormID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupInFormMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionGroupInFormMetadata.ColumnNames.QuestionGroupID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupInFormMetadata.PropertyNames.QuestionGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionGroupInFormMetadata.ColumnNames.RowIndex, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionGroupInFormMetadata.PropertyNames.RowIndex;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionGroupInFormMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QuestionGroupInFormMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionGroupInFormMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionGroupInFormMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public QuestionGroupInFormMetadata Meta()
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
			 public const string QuestionFormID = "QuestionFormID";
			 public const string QuestionGroupID = "QuestionGroupID";
			 public const string RowIndex = "RowIndex";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string QuestionFormID = "QuestionFormID";
			 public const string QuestionGroupID = "QuestionGroupID";
			 public const string RowIndex = "RowIndex";
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
			lock (typeof(QuestionGroupInFormMetadata))
			{
				if(QuestionGroupInFormMetadata.mapDelegates == null)
				{
					QuestionGroupInFormMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QuestionGroupInFormMetadata.meta == null)
				{
					QuestionGroupInFormMetadata.meta = new QuestionGroupInFormMetadata();
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
				

				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RowIndex", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "QuestionGroupInForm";
				meta.Destination = "QuestionGroupInForm";
				
				meta.spInsert = "proc_QuestionGroupInFormInsert";				
				meta.spUpdate = "proc_QuestionGroupInFormUpdate";		
				meta.spDelete = "proc_QuestionGroupInFormDelete";
				meta.spLoadAll = "proc_QuestionGroupInFormLoadAll";
				meta.spLoadByPrimaryKey = "proc_QuestionGroupInFormLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QuestionGroupInFormMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
