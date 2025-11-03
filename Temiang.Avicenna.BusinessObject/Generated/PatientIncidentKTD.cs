/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/25/2015 1:22:29 PM
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
	abstract public class esPatientIncidentKTDCollection : esEntityCollectionWAuditLog
	{
		public esPatientIncidentKTDCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PatientIncidentKTDCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientIncidentKTDQuery query)
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
			this.InitQuery(query as esPatientIncidentKTDQuery);
		}
		#endregion
		
		virtual public PatientIncidentKTD DetachEntity(PatientIncidentKTD entity)
		{
			return base.DetachEntity(entity) as PatientIncidentKTD;
		}
		
		virtual public PatientIncidentKTD AttachEntity(PatientIncidentKTD entity)
		{
			return base.AttachEntity(entity) as PatientIncidentKTD;
		}
		
		virtual public void Combine(PatientIncidentKTDCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientIncidentKTD this[int index]
		{
			get
			{
				return base[index] as PatientIncidentKTD;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientIncidentKTD);
		}
	}



	[Serializable]
	abstract public class esPatientIncidentKTD : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientIncidentKTDQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientIncidentKTD()
		{

		}

		public esPatientIncidentKTD(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String patientIncidentNo, System.String sRIncidentKTD)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientIncidentNo, sRIncidentKTD);
			else
				return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRIncidentKTD);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String patientIncidentNo, System.String sRIncidentKTD)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientIncidentNo, sRIncidentKTD);
			else
				return LoadByPrimaryKeyStoredProcedure(patientIncidentNo, sRIncidentKTD);
		}

		private bool LoadByPrimaryKeyDynamic(System.String patientIncidentNo, System.String sRIncidentKTD)
		{
			esPatientIncidentKTDQuery query = this.GetDynamicQuery();
			query.Where(query.PatientIncidentNo == patientIncidentNo, query.SRIncidentKTD == sRIncidentKTD);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String patientIncidentNo, System.String sRIncidentKTD)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientIncidentNo",patientIncidentNo);			parms.Add("SRIncidentKTD",sRIncidentKTD);
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
						case "PatientIncidentNo": this.str.PatientIncidentNo = (string)value; break;							
						case "SRIncidentKTD": this.str.SRIncidentKTD = (string)value; break;							
						case "IncidentKTDName": this.str.IncidentKTDName = (string)value; break;							
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
		/// Maps to PatientIncidentKTD.PatientIncidentNo
		/// </summary>
		virtual public System.String PatientIncidentNo
		{
			get
			{
				return base.GetSystemString(PatientIncidentKTDMetadata.ColumnNames.PatientIncidentNo);
			}
			
			set
			{
				base.SetSystemString(PatientIncidentKTDMetadata.ColumnNames.PatientIncidentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientIncidentKTD.SRIncidentKTD
		/// </summary>
		virtual public System.String SRIncidentKTD
		{
			get
			{
				return base.GetSystemString(PatientIncidentKTDMetadata.ColumnNames.SRIncidentKTD);
			}
			
			set
			{
				base.SetSystemString(PatientIncidentKTDMetadata.ColumnNames.SRIncidentKTD, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientIncidentKTD.IncidentKTDName
		/// </summary>
		virtual public System.String IncidentKTDName
		{
			get
			{
				return base.GetSystemString(PatientIncidentKTDMetadata.ColumnNames.IncidentKTDName);
			}
			
			set
			{
				base.SetSystemString(PatientIncidentKTDMetadata.ColumnNames.IncidentKTDName, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientIncidentKTD.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientIncidentKTDMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientIncidentKTDMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientIncidentKTD.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientIncidentKTDMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientIncidentKTDMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPatientIncidentKTD entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PatientIncidentNo
			{
				get
				{
					System.String data = entity.PatientIncidentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientIncidentNo = null;
					else entity.PatientIncidentNo = Convert.ToString(value);
				}
			}
				
			public System.String SRIncidentKTD
			{
				get
				{
					System.String data = entity.SRIncidentKTD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentKTD = null;
					else entity.SRIncidentKTD = Convert.ToString(value);
				}
			}
				
			public System.String IncidentKTDName
			{
				get
				{
					System.String data = entity.IncidentKTDName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentKTDName = null;
					else entity.IncidentKTDName = Convert.ToString(value);
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
			

			private esPatientIncidentKTD entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientIncidentKTDQuery query)
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
				throw new Exception("esPatientIncidentKTD can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PatientIncidentKTD : esPatientIncidentKTD
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
	abstract public class esPatientIncidentKTDQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PatientIncidentKTDMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientIncidentNo
		{
			get
			{
				return new esQueryItem(this, PatientIncidentKTDMetadata.ColumnNames.PatientIncidentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRIncidentKTD
		{
			get
			{
				return new esQueryItem(this, PatientIncidentKTDMetadata.ColumnNames.SRIncidentKTD, esSystemType.String);
			}
		} 
		
		public esQueryItem IncidentKTDName
		{
			get
			{
				return new esQueryItem(this, PatientIncidentKTDMetadata.ColumnNames.IncidentKTDName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientIncidentKTDMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientIncidentKTDMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientIncidentKTDCollection")]
	public partial class PatientIncidentKTDCollection : esPatientIncidentKTDCollection, IEnumerable<PatientIncidentKTD>
	{
		public PatientIncidentKTDCollection()
		{

		}
		
		public static implicit operator List<PatientIncidentKTD>(PatientIncidentKTDCollection coll)
		{
			List<PatientIncidentKTD> list = new List<PatientIncidentKTD>();
			
			foreach (PatientIncidentKTD emp in coll)
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
				return  PatientIncidentKTDMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientIncidentKTDQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientIncidentKTD(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientIncidentKTD();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PatientIncidentKTDQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientIncidentKTDQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PatientIncidentKTDQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PatientIncidentKTD AddNew()
		{
			PatientIncidentKTD entity = base.AddNewEntity() as PatientIncidentKTD;
			
			return entity;
		}

		public PatientIncidentKTD FindByPrimaryKey(System.String patientIncidentNo, System.String sRIncidentKTD)
		{
			return base.FindByPrimaryKey(patientIncidentNo, sRIncidentKTD) as PatientIncidentKTD;
		}


		#region IEnumerable<PatientIncidentKTD> Members

		IEnumerator<PatientIncidentKTD> IEnumerable<PatientIncidentKTD>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientIncidentKTD;
			}
		}

		#endregion
		
		private PatientIncidentKTDQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientIncidentKTD' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PatientIncidentKTD ({PatientIncidentNo},{SRIncidentKTD})")]
	[Serializable]
	public partial class PatientIncidentKTD : esPatientIncidentKTD
	{
		public PatientIncidentKTD()
		{

		}
	
		public PatientIncidentKTD(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientIncidentKTDMetadata.Meta();
			}
		}
		
		
		
		override protected esPatientIncidentKTDQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientIncidentKTDQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PatientIncidentKTDQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientIncidentKTDQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PatientIncidentKTDQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PatientIncidentKTDQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PatientIncidentKTDQuery : esPatientIncidentKTDQuery
	{
		public PatientIncidentKTDQuery()
		{

		}		
		
		public PatientIncidentKTDQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PatientIncidentKTDQuery";
        }
		
			
	}


	[Serializable]
	public partial class PatientIncidentKTDMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientIncidentKTDMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientIncidentKTDMetadata.ColumnNames.PatientIncidentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentKTDMetadata.PropertyNames.PatientIncidentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientIncidentKTDMetadata.ColumnNames.SRIncidentKTD, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentKTDMetadata.PropertyNames.SRIncidentKTD;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientIncidentKTDMetadata.ColumnNames.IncidentKTDName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentKTDMetadata.PropertyNames.IncidentKTDName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientIncidentKTDMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientIncidentKTDMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientIncidentKTDMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientIncidentKTDMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PatientIncidentKTDMetadata Meta()
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
			 public const string PatientIncidentNo = "PatientIncidentNo";
			 public const string SRIncidentKTD = "SRIncidentKTD";
			 public const string IncidentKTDName = "IncidentKTDName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientIncidentNo = "PatientIncidentNo";
			 public const string SRIncidentKTD = "SRIncidentKTD";
			 public const string IncidentKTDName = "IncidentKTDName";
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
			lock (typeof(PatientIncidentKTDMetadata))
			{
				if(PatientIncidentKTDMetadata.mapDelegates == null)
				{
					PatientIncidentKTDMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientIncidentKTDMetadata.meta == null)
				{
					PatientIncidentKTDMetadata.meta = new PatientIncidentKTDMetadata();
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
				

				meta.AddTypeMap("PatientIncidentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentKTD", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentKTDName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PatientIncidentKTD";
				meta.Destination = "PatientIncidentKTD";
				
				meta.spInsert = "proc_PatientIncidentKTDInsert";				
				meta.spUpdate = "proc_PatientIncidentKTDUpdate";		
				meta.spDelete = "proc_PatientIncidentKTDDelete";
				meta.spLoadAll = "proc_PatientIncidentKTDLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientIncidentKTDLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientIncidentKTDMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
