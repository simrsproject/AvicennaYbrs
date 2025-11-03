/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:21 PM
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
	abstract public class esPatientRelatedCollection : esEntityCollectionWAuditLog
	{
		public esPatientRelatedCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PatientRelatedCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientRelatedQuery query)
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
			this.InitQuery(query as esPatientRelatedQuery);
		}
		#endregion
		
		virtual public PatientRelated DetachEntity(PatientRelated entity)
		{
			return base.DetachEntity(entity) as PatientRelated;
		}
		
		virtual public PatientRelated AttachEntity(PatientRelated entity)
		{
			return base.AttachEntity(entity) as PatientRelated;
		}
		
		virtual public void Combine(PatientRelatedCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientRelated this[int index]
		{
			get
			{
				return base[index] as PatientRelated;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientRelated);
		}
	}



	[Serializable]
	abstract public class esPatientRelated : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientRelatedQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientRelated()
		{

		}

		public esPatientRelated(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String patientID, System.String relatedPatientID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, relatedPatientID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, relatedPatientID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String patientID, System.String relatedPatientID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID, relatedPatientID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID, relatedPatientID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String patientID, System.String relatedPatientID)
		{
			esPatientRelatedQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID, query.RelatedPatientID == relatedPatientID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String patientID, System.String relatedPatientID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);			parms.Add("RelatedPatientID",relatedPatientID);
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
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "RelatedPatientID": this.str.RelatedPatientID = (string)value; break;							
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
		/// Maps to PatientRelated.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientRelatedMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(PatientRelatedMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientRelated.RelatedPatientID
		/// </summary>
		virtual public System.String RelatedPatientID
		{
			get
			{
				return base.GetSystemString(PatientRelatedMetadata.ColumnNames.RelatedPatientID);
			}
			
			set
			{
				base.SetSystemString(PatientRelatedMetadata.ColumnNames.RelatedPatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientRelated.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientRelatedMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientRelatedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientRelated.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientRelatedMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientRelatedMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPatientRelated entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
				
			public System.String RelatedPatientID
			{
				get
				{
					System.String data = entity.RelatedPatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RelatedPatientID = null;
					else entity.RelatedPatientID = Convert.ToString(value);
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
			

			private esPatientRelated entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientRelatedQuery query)
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
				throw new Exception("esPatientRelated can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PatientRelated : esPatientRelated
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
	abstract public class esPatientRelatedQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PatientRelatedMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientRelatedMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem RelatedPatientID
		{
			get
			{
				return new esQueryItem(this, PatientRelatedMetadata.ColumnNames.RelatedPatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientRelatedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientRelatedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientRelatedCollection")]
	public partial class PatientRelatedCollection : esPatientRelatedCollection, IEnumerable<PatientRelated>
	{
		public PatientRelatedCollection()
		{

		}
		
		public static implicit operator List<PatientRelated>(PatientRelatedCollection coll)
		{
			List<PatientRelated> list = new List<PatientRelated>();
			
			foreach (PatientRelated emp in coll)
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
				return  PatientRelatedMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientRelatedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientRelated(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientRelated();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PatientRelatedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientRelatedQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PatientRelatedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PatientRelated AddNew()
		{
			PatientRelated entity = base.AddNewEntity() as PatientRelated;
			
			return entity;
		}

		public PatientRelated FindByPrimaryKey(System.String patientID, System.String relatedPatientID)
		{
			return base.FindByPrimaryKey(patientID, relatedPatientID) as PatientRelated;
		}


		#region IEnumerable<PatientRelated> Members

		IEnumerator<PatientRelated> IEnumerable<PatientRelated>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientRelated;
			}
		}

		#endregion
		
		private PatientRelatedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientRelated' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PatientRelated ({PatientID},{RelatedPatientID})")]
	[Serializable]
	public partial class PatientRelated : esPatientRelated
	{
		public PatientRelated()
		{

		}
	
		public PatientRelated(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientRelatedMetadata.Meta();
			}
		}
		
		
		
		override protected esPatientRelatedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientRelatedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PatientRelatedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientRelatedQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PatientRelatedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PatientRelatedQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PatientRelatedQuery : esPatientRelatedQuery
	{
		public PatientRelatedQuery()
		{

		}		
		
		public PatientRelatedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PatientRelatedQuery";
        }
		
			
	}


	[Serializable]
	public partial class PatientRelatedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientRelatedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientRelatedMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientRelatedMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientRelatedMetadata.ColumnNames.RelatedPatientID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientRelatedMetadata.PropertyNames.RelatedPatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientRelatedMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientRelatedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientRelatedMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientRelatedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PatientRelatedMetadata Meta()
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
			 public const string PatientID = "PatientID";
			 public const string RelatedPatientID = "RelatedPatientID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientID = "PatientID";
			 public const string RelatedPatientID = "RelatedPatientID";
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
			lock (typeof(PatientRelatedMetadata))
			{
				if(PatientRelatedMetadata.mapDelegates == null)
				{
					PatientRelatedMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientRelatedMetadata.meta == null)
				{
					PatientRelatedMetadata.meta = new PatientRelatedMetadata();
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
				

				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RelatedPatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PatientRelated";
				meta.Destination = "PatientRelated";
				
				meta.spInsert = "proc_PatientRelatedInsert";				
				meta.spUpdate = "proc_PatientRelatedUpdate";		
				meta.spDelete = "proc_PatientRelatedDelete";
				meta.spLoadAll = "proc_PatientRelatedLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientRelatedLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientRelatedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
