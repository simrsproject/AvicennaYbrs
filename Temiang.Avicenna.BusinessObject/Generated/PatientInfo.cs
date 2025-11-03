/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 5/8/2014 2:57:11 PM
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
	abstract public class esPatientInfoCollection : esEntityCollection
	{
		public esPatientInfoCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PatientInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientInfoQuery query)
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
			this.InitQuery(query as esPatientInfoQuery);
		}
		#endregion
		
		virtual public PatientInfo DetachEntity(PatientInfo entity)
		{
			return base.DetachEntity(entity) as PatientInfo;
		}
		
		virtual public PatientInfo AttachEntity(PatientInfo entity)
		{
			return base.AttachEntity(entity) as PatientInfo;
		}
		
		virtual public void Combine(PatientInfoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientInfo this[int index]
		{
			get
			{
				return base[index] as PatientInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientInfo);
		}
	}



	[Serializable]
	abstract public class esPatientInfo : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientInfo()
		{

		}

		public esPatientInfo(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String PatientInfoID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(PatientInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(PatientInfoID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String PatientInfoID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(PatientInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(PatientInfoID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String PatientInfoID)
		{
			esPatientInfoQuery query = this.GetDynamicQuery();
			query.Where(query.PatientInfoID == PatientInfoID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String PatientInfoID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientInfoID", PatientInfoID);
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
						case "PatientInfoID": this.str.PatientInfoID = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "Information": this.str.Information = (string)value; break;							
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;							
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to PatientInfo.PatientInfoID
		/// </summary>
		virtual public System.String PatientInfoID
		{
			get
			{
				return base.GetSystemString(PatientInfoMetadata.ColumnNames.PatientInfoID);
			}
			
			set
			{
				base.SetSystemString(PatientInfoMetadata.ColumnNames.PatientInfoID, value);
			}
		}

		/// <summary>
		/// Maps to PatientInfo.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientInfoMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				if(base.SetSystemString(PatientInfoMetadata.ColumnNames.PatientID, value))
				{
					this._UpToPatientByPatient = null;
				}
			}
		}

		/// <summary>
		/// Maps to PatientInfo.Information
		/// </summary>
		virtual public System.String Information
		{
			get
			{
				return base.GetSystemString(PatientInfoMetadata.ColumnNames.Information);
			}
			
			set
			{
				base.SetSystemString(PatientInfoMetadata.ColumnNames.Information, value);
			}
		}

		/// <summary>
		/// Maps to PatientInfo.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PatientInfoMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientInfoMetadata.ColumnNames.CreatedByUserID, value);
			}
		}

		/// <summary>
		/// Maps to PatientInfo.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientInfoMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientInfoMetadata.ColumnNames.CreatedDateTime, value);
			}
		}

		/// <summary>
		/// Maps to PatientInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientInfoMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientInfoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}

		/// <summary>
		/// Maps to PatientInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientInfoMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected Patient _UpToPatientByPatient;
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
			public esStrings(esPatientInfo entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PatientInfoID
			{
				get
				{
					System.String data = entity.PatientInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientInfoID = null;
					else entity.PatientInfoID = Convert.ToString(value);
				}
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
				
			public System.String Information
			{
				get
				{
					System.String data = entity.Information;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Information = null;
					else entity.Information = Convert.ToString(value);
				}
			}
				
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
				
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			

			private esPatientInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientInfoQuery query)
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
				throw new Exception("esPatientInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PatientInfo : esPatientInfo
	{


		#region UpToPatientByPatient - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - UpToPatientByPatient
		/// </summary>

		[XmlIgnore]
		public Patient UpToPatientByPatient
		{
			get
			{
				if(this._UpToPatientByPatient == null
					&& PatientID != null					)
				{
					this._UpToPatientByPatient = new Patient();
					this._UpToPatientByPatient.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToPatientByPatient", this._UpToPatientByPatient);
					this._UpToPatientByPatient.Query.Where(this._UpToPatientByPatient.Query.PatientID == this.PatientID);
					this._UpToPatientByPatient.Query.Load();
				}

				return this._UpToPatientByPatient;
			}
			
			set
			{
				this.RemovePreSave("UpToPatientByPatient");
				

				if(value == null)
				{
					this.PatientID = null;
					this._UpToPatientByPatient = null;
				}
				else
				{
					this.PatientID = value.PatientID;
					this._UpToPatientByPatient = value;
					this.SetPreSave("UpToPatientByPatient", this._UpToPatientByPatient);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esPatientInfoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PatientInfoMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientInfoID
		{
			get
			{
				return new esQueryItem(this, PatientInfoMetadata.ColumnNames.PatientInfoID, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientInfoMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem Information
		{
			get
			{
				return new esQueryItem(this, PatientInfoMetadata.ColumnNames.Information, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PatientInfoMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PatientInfoMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationInfoCollection")]
	public partial class PatientInfoCollection : esPatientInfoCollection, IEnumerable<PatientInfo>
	{
		public PatientInfoCollection()
		{

		}
		
		public static implicit operator List<PatientInfo>(PatientInfoCollection coll)
		{
			List<PatientInfo> list = new List<PatientInfo>();
			
			foreach (PatientInfo emp in coll)
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
				return PatientInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientInfo();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PatientInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PatientInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PatientInfo AddNew()
		{
			PatientInfo entity = base.AddNewEntity() as PatientInfo;
			
			return entity;
		}

		public PatientInfo FindByPrimaryKey(System.String PatientInfo)
		{
			return base.FindByPrimaryKey(PatientInfo) as PatientInfo;
		}


		#region IEnumerable<PatientInfo> Members

		IEnumerator<PatientInfo> IEnumerable<PatientInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientInfo;
			}
		}

		#endregion
		
		private PatientInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientInfo' table
	/// </summary>

	[System.Diagnostics.DebuggerDisplay("PatientInfo ({PatientInfoID})")]
	[Serializable]
	public partial class PatientInfo : esPatientInfo
	{
		public PatientInfo()
		{

		}
	
		public PatientInfo(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esPatientInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PatientInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PatientInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PatientInfoQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PatientInfoQuery : esPatientInfoQuery
	{
		public PatientInfoQuery()
		{

		}		
		
		public PatientInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PatientInfoQuery";
        }
		
			
	}


	[Serializable]
	public partial class PatientInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientInfoMetadata.ColumnNames.PatientInfoID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientInfoMetadata.PropertyNames.PatientInfoID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientInfoMetadata.ColumnNames.PatientID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientInfoMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientInfoMetadata.ColumnNames.Information, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientInfoMetadata.PropertyNames.Information;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientInfoMetadata.ColumnNames.CreatedByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientInfoMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientInfoMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientInfoMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientInfoMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientInfoMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientInfoMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PatientInfoMetadata Meta()
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
			 public const string PatientInfoID = "PatientInfoID";
			 public const string PatientID = "PatientID";
			 public const string Information = "Information";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientInfoID = "PatientInfoID";
			 public const string PatientID = "PatientID";
			 public const string Information = "Information";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
		
		static private int PatientInfoDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(PatientInfoMetadata))
			{
				if(PatientInfoMetadata.mapDelegates == null)
				{
					PatientInfoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientInfoMetadata.meta == null)
				{
					PatientInfoMetadata.meta = new PatientInfoMetadata();
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
				

				meta.AddTypeMap("PatientInfoID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Information", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "PatientInfo";
				meta.Destination = "PatientInfo";
				
				meta.spInsert = "proc_PatientInfoInsert";				
				meta.spUpdate = "proc_PatientInfoUpdate";		
				meta.spDelete = "proc_PatientInfoDelete";
				meta.spLoadAll = "proc_PatientInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientInfoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = PatientInfoDelegateesDefault();
	}
}
