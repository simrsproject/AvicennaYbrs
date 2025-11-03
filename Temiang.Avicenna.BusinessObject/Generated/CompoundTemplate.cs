/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/13/2012 4:26:03 PM
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
	abstract public class esCompoundTemplateCollection : esEntityCollectionWAuditLog
	{
		public esCompoundTemplateCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CompoundTemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esCompoundTemplateQuery query)
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
			this.InitQuery(query as esCompoundTemplateQuery);
		}
		#endregion
		
		virtual public CompoundTemplate DetachEntity(CompoundTemplate entity)
		{
			return base.DetachEntity(entity) as CompoundTemplate;
		}
		
		virtual public CompoundTemplate AttachEntity(CompoundTemplate entity)
		{
			return base.AttachEntity(entity) as CompoundTemplate;
		}
		
		virtual public void Combine(CompoundTemplateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CompoundTemplate this[int index]
		{
			get
			{
				return base[index] as CompoundTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CompoundTemplate);
		}
	}



	[Serializable]
	abstract public class esCompoundTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCompoundTemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esCompoundTemplate()
		{

		}

		public esCompoundTemplate(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String compoundTemplateID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(compoundTemplateID);
			else
				return LoadByPrimaryKeyStoredProcedure(compoundTemplateID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String compoundTemplateID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(compoundTemplateID);
			else
				return LoadByPrimaryKeyStoredProcedure(compoundTemplateID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String compoundTemplateID)
		{
			esCompoundTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.CompoundTemplateID == compoundTemplateID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String compoundTemplateID)
		{
			esParameters parms = new esParameters();
			parms.Add("CompoundTemplateID",compoundTemplateID);
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
						case "CompoundTemplateID": this.str.CompoundTemplateID = (string)value; break;							
						case "HeaderItemID": this.str.HeaderItemID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to CompoundTemplate.CompoundTemplateID
		/// </summary>
		virtual public System.String CompoundTemplateID
		{
			get
			{
				return base.GetSystemString(CompoundTemplateMetadata.ColumnNames.CompoundTemplateID);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateMetadata.ColumnNames.CompoundTemplateID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplate.HeaderItemID
		/// </summary>
		virtual public System.String HeaderItemID
		{
			get
			{
				return base.GetSystemString(CompoundTemplateMetadata.ColumnNames.HeaderItemID);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateMetadata.ColumnNames.HeaderItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplate.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(CompoundTemplateMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplate.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CompoundTemplateMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplate.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CompoundTemplateMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(CompoundTemplateMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CompoundTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CompoundTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CompoundTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CompoundTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CompoundTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected CompoundTemplate _UpToCompoundTemplateByCompoundTemplateID;
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
			public esStrings(esCompoundTemplate entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CompoundTemplateID
			{
				get
				{
					System.String data = entity.CompoundTemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompoundTemplateID = null;
					else entity.CompoundTemplateID = Convert.ToString(value);
				}
			}
				
			public System.String HeaderItemID
			{
				get
				{
					System.String data = entity.HeaderItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HeaderItemID = null;
					else entity.HeaderItemID = Convert.ToString(value);
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
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esCompoundTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCompoundTemplateQuery query)
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
				throw new Exception("esCompoundTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CompoundTemplate : esCompoundTemplate
	{

				
		#region CompoundTemplateCollectionByCompoundTemplateID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_CompoundTemplate_CompoundTemplate
		/// </summary>

		[XmlIgnore]
		public CompoundTemplateCollection CompoundTemplateCollectionByCompoundTemplateID
		{
			get
			{
				if(this._CompoundTemplateCollectionByCompoundTemplateID == null)
				{
					this._CompoundTemplateCollectionByCompoundTemplateID = new CompoundTemplateCollection();
					this._CompoundTemplateCollectionByCompoundTemplateID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CompoundTemplateCollectionByCompoundTemplateID", this._CompoundTemplateCollectionByCompoundTemplateID);
				
					if(this.CompoundTemplateID != null)
					{
						this._CompoundTemplateCollectionByCompoundTemplateID.Query.Where(this._CompoundTemplateCollectionByCompoundTemplateID.Query.CompoundTemplateID == this.CompoundTemplateID);
						this._CompoundTemplateCollectionByCompoundTemplateID.Query.Load();

						// Auto-hookup Foreign Keys
						this._CompoundTemplateCollectionByCompoundTemplateID.fks.Add(CompoundTemplateMetadata.ColumnNames.CompoundTemplateID, this.CompoundTemplateID);
					}
				}

				return this._CompoundTemplateCollectionByCompoundTemplateID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CompoundTemplateCollectionByCompoundTemplateID != null) 
				{ 
					this.RemovePostSave("CompoundTemplateCollectionByCompoundTemplateID"); 
					this._CompoundTemplateCollectionByCompoundTemplateID = null;
					
				} 
			} 			
		}

		private CompoundTemplateCollection _CompoundTemplateCollectionByCompoundTemplateID;
		#endregion

				
		#region UpToCompoundTemplateByCompoundTemplateID - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_CompoundTemplate_CompoundTemplate
		/// </summary>

		[XmlIgnore]
		public CompoundTemplate UpToCompoundTemplateByCompoundTemplateID
		{
			get
			{
				if(this._UpToCompoundTemplateByCompoundTemplateID == null
					&& CompoundTemplateID != null					)
				{
					this._UpToCompoundTemplateByCompoundTemplateID = new CompoundTemplate();
					this._UpToCompoundTemplateByCompoundTemplateID.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToCompoundTemplateByCompoundTemplateID", this._UpToCompoundTemplateByCompoundTemplateID);
					this._UpToCompoundTemplateByCompoundTemplateID.Query.Where(this._UpToCompoundTemplateByCompoundTemplateID.Query.CompoundTemplateID == this.CompoundTemplateID);
					this._UpToCompoundTemplateByCompoundTemplateID.Query.Load();
				}

				return this._UpToCompoundTemplateByCompoundTemplateID;
			}
			
			set
			{
				this.RemovePreSave("UpToCompoundTemplateByCompoundTemplateID");
				

				if(value == null)
				{
					this.CompoundTemplateID = null;
					this._UpToCompoundTemplateByCompoundTemplateID = null;
				}
				else
				{
					this.CompoundTemplateID = value.CompoundTemplateID;
					this._UpToCompoundTemplateByCompoundTemplateID = value;
					this.SetPreSave("UpToCompoundTemplateByCompoundTemplateID", this._UpToCompoundTemplateByCompoundTemplateID);
				}
				
			}
		}
		#endregion
		

				
		#region CompoundTemplateItemCollectionByCompoundTemplateID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_CompoundTemplateItem_CompoundTemplate
		/// </summary>

		[XmlIgnore]
		public CompoundTemplateItemCollection CompoundTemplateItemCollectionByCompoundTemplateID
		{
			get
			{
				if(this._CompoundTemplateItemCollectionByCompoundTemplateID == null)
				{
					this._CompoundTemplateItemCollectionByCompoundTemplateID = new CompoundTemplateItemCollection();
					this._CompoundTemplateItemCollectionByCompoundTemplateID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CompoundTemplateItemCollectionByCompoundTemplateID", this._CompoundTemplateItemCollectionByCompoundTemplateID);
				
					if(this.CompoundTemplateID != null)
					{
						this._CompoundTemplateItemCollectionByCompoundTemplateID.Query.Where(this._CompoundTemplateItemCollectionByCompoundTemplateID.Query.CompoundTemplateID == this.CompoundTemplateID);
						this._CompoundTemplateItemCollectionByCompoundTemplateID.Query.Load();

						// Auto-hookup Foreign Keys
						this._CompoundTemplateItemCollectionByCompoundTemplateID.fks.Add(CompoundTemplateItemMetadata.ColumnNames.CompoundTemplateID, this.CompoundTemplateID);
					}
				}

				return this._CompoundTemplateItemCollectionByCompoundTemplateID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CompoundTemplateItemCollectionByCompoundTemplateID != null) 
				{ 
					this.RemovePostSave("CompoundTemplateItemCollectionByCompoundTemplateID"); 
					this._CompoundTemplateItemCollectionByCompoundTemplateID = null;
					
				} 
			} 			
		}

		private CompoundTemplateItemCollection _CompoundTemplateItemCollectionByCompoundTemplateID;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "CompoundTemplateCollectionByCompoundTemplateID", typeof(CompoundTemplateCollection), new CompoundTemplate()));
			props.Add(new esPropertyDescriptor(this, "CompoundTemplateItemCollectionByCompoundTemplateID", typeof(CompoundTemplateItemCollection), new CompoundTemplateItem()));
		
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
	abstract public class esCompoundTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CompoundTemplateMetadata.Meta();
			}
		}	
		

		public esQueryItem CompoundTemplateID
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateMetadata.ColumnNames.CompoundTemplateID, esSystemType.String);
			}
		} 
		
		public esQueryItem HeaderItemID
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateMetadata.ColumnNames.HeaderItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CompoundTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CompoundTemplateCollection")]
	public partial class CompoundTemplateCollection : esCompoundTemplateCollection, IEnumerable<CompoundTemplate>
	{
		public CompoundTemplateCollection()
		{

		}
		
		public static implicit operator List<CompoundTemplate>(CompoundTemplateCollection coll)
		{
			List<CompoundTemplate> list = new List<CompoundTemplate>();
			
			foreach (CompoundTemplate emp in coll)
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
				return  CompoundTemplateMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompoundTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CompoundTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CompoundTemplate();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CompoundTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompoundTemplateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CompoundTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CompoundTemplate AddNew()
		{
			CompoundTemplate entity = base.AddNewEntity() as CompoundTemplate;
			
			return entity;
		}

		public CompoundTemplate FindByPrimaryKey(System.String compoundTemplateID)
		{
			return base.FindByPrimaryKey(compoundTemplateID) as CompoundTemplate;
		}


		#region IEnumerable<CompoundTemplate> Members

		IEnumerator<CompoundTemplate> IEnumerable<CompoundTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CompoundTemplate;
			}
		}

		#endregion
		
		private CompoundTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CompoundTemplate' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CompoundTemplate ({CompoundTemplateID})")]
	[Serializable]
	public partial class CompoundTemplate : esCompoundTemplate
	{
		public CompoundTemplate()
		{

		}
	
		public CompoundTemplate(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CompoundTemplateMetadata.Meta();
			}
		}
		
		
		
		override protected esCompoundTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CompoundTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CompoundTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CompoundTemplateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CompoundTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CompoundTemplateQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CompoundTemplateQuery : esCompoundTemplateQuery
	{
		public CompoundTemplateQuery()
		{

		}		
		
		public CompoundTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CompoundTemplateQuery";
        }
		
			
	}


	[Serializable]
	public partial class CompoundTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CompoundTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CompoundTemplateMetadata.ColumnNames.CompoundTemplateID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateMetadata.PropertyNames.CompoundTemplateID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateMetadata.ColumnNames.HeaderItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateMetadata.PropertyNames.HeaderItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CompoundTemplateMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CompoundTemplateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CompoundTemplateMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CompoundTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CompoundTemplateMetadata Meta()
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
			 public const string CompoundTemplateID = "CompoundTemplateID";
			 public const string HeaderItemID = "HeaderItemID";
			 public const string ParamedicID = "ParamedicID";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CompoundTemplateID = "CompoundTemplateID";
			 public const string HeaderItemID = "HeaderItemID";
			 public const string ParamedicID = "ParamedicID";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
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
			lock (typeof(CompoundTemplateMetadata))
			{
				if(CompoundTemplateMetadata.mapDelegates == null)
				{
					CompoundTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CompoundTemplateMetadata.meta == null)
				{
					CompoundTemplateMetadata.meta = new CompoundTemplateMetadata();
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
				

				meta.AddTypeMap("CompoundTemplateID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HeaderItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CompoundTemplate";
				meta.Destination = "CompoundTemplate";
				
				meta.spInsert = "proc_CompoundTemplateInsert";				
				meta.spUpdate = "proc_CompoundTemplateUpdate";		
				meta.spDelete = "proc_CompoundTemplateDelete";
				meta.spLoadAll = "proc_CompoundTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_CompoundTemplateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CompoundTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
