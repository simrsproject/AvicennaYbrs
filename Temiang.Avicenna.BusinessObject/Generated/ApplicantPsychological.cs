/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:10 PM
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
	abstract public class esApplicantPsychologicalCollection : esEntityCollectionWAuditLog
	{
		public esApplicantPsychologicalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantPsychologicalCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantPsychologicalQuery query)
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
			this.InitQuery(query as esApplicantPsychologicalQuery);
		}
		#endregion
		
		virtual public ApplicantPsychological DetachEntity(ApplicantPsychological entity)
		{
			return base.DetachEntity(entity) as ApplicantPsychological;
		}
		
		virtual public ApplicantPsychological AttachEntity(ApplicantPsychological entity)
		{
			return base.AttachEntity(entity) as ApplicantPsychological;
		}
		
		virtual public void Combine(ApplicantPsychologicalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantPsychological this[int index]
		{
			get
			{
				return base[index] as ApplicantPsychological;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantPsychological);
		}
	}



	[Serializable]
	abstract public class esApplicantPsychological : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantPsychologicalQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantPsychological()
		{

		}

		public esApplicantPsychological(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantPsychologicalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantPsychologicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantPsychologicalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantPsychologicalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantPsychologicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantPsychologicalID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantPsychologicalID)
		{
			esApplicantPsychologicalQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantPsychologicalID == applicantPsychologicalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantPsychologicalID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantPsychologicalID",applicantPsychologicalID);
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
						case "ApplicantPsychologicalID": this.str.ApplicantPsychologicalID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "SRPsychological": this.str.SRPsychological = (string)value; break;							
						case "SROperandType": this.str.SROperandType = (string)value; break;							
						case "PsychologicalValue": this.str.PsychologicalValue = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantPsychologicalID":
						
							if (value == null || value is System.Int32)
								this.ApplicantPsychologicalID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
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
		/// Maps to ApplicantPsychological.ApplicantPsychologicalID
		/// </summary>
		virtual public System.Int32? ApplicantPsychologicalID
		{
			get
			{
				return base.GetSystemInt32(ApplicantPsychologicalMetadata.ColumnNames.ApplicantPsychologicalID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantPsychologicalMetadata.ColumnNames.ApplicantPsychologicalID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPsychological.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantPsychologicalMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantPsychologicalMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPsychological.SRPsychological
		/// </summary>
		virtual public System.String SRPsychological
		{
			get
			{
				return base.GetSystemString(ApplicantPsychologicalMetadata.ColumnNames.SRPsychological);
			}
			
			set
			{
				base.SetSystemString(ApplicantPsychologicalMetadata.ColumnNames.SRPsychological, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPsychological.SROperandType
		/// </summary>
		virtual public System.String SROperandType
		{
			get
			{
				return base.GetSystemString(ApplicantPsychologicalMetadata.ColumnNames.SROperandType);
			}
			
			set
			{
				base.SetSystemString(ApplicantPsychologicalMetadata.ColumnNames.SROperandType, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPsychological.PsychologicalValue
		/// </summary>
		virtual public System.String PsychologicalValue
		{
			get
			{
				return base.GetSystemString(ApplicantPsychologicalMetadata.ColumnNames.PsychologicalValue);
			}
			
			set
			{
				base.SetSystemString(ApplicantPsychologicalMetadata.ColumnNames.PsychologicalValue, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPsychological.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantPsychologicalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantPsychologicalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantPsychological.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantPsychologicalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantPsychologicalMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantPsychological entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantPsychologicalID
			{
				get
				{
					System.Int32? data = entity.ApplicantPsychologicalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantPsychologicalID = null;
					else entity.ApplicantPsychologicalID = Convert.ToInt32(value);
				}
			}
				
			public System.String ApplicantID
			{
				get
				{
					System.Int32? data = entity.ApplicantID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantID = null;
					else entity.ApplicantID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRPsychological
			{
				get
				{
					System.String data = entity.SRPsychological;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPsychological = null;
					else entity.SRPsychological = Convert.ToString(value);
				}
			}
				
			public System.String SROperandType
			{
				get
				{
					System.String data = entity.SROperandType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROperandType = null;
					else entity.SROperandType = Convert.ToString(value);
				}
			}
				
			public System.String PsychologicalValue
			{
				get
				{
					System.String data = entity.PsychologicalValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PsychologicalValue = null;
					else entity.PsychologicalValue = Convert.ToString(value);
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
			

			private esApplicantPsychological entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantPsychologicalQuery query)
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
				throw new Exception("esApplicantPsychological can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantPsychological : esApplicantPsychological
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
	abstract public class esApplicantPsychologicalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantPsychologicalMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantPsychologicalID
		{
			get
			{
				return new esQueryItem(this, ApplicantPsychologicalMetadata.ColumnNames.ApplicantPsychologicalID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantPsychologicalMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPsychological
		{
			get
			{
				return new esQueryItem(this, ApplicantPsychologicalMetadata.ColumnNames.SRPsychological, esSystemType.String);
			}
		} 
		
		public esQueryItem SROperandType
		{
			get
			{
				return new esQueryItem(this, ApplicantPsychologicalMetadata.ColumnNames.SROperandType, esSystemType.String);
			}
		} 
		
		public esQueryItem PsychologicalValue
		{
			get
			{
				return new esQueryItem(this, ApplicantPsychologicalMetadata.ColumnNames.PsychologicalValue, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantPsychologicalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantPsychologicalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantPsychologicalCollection")]
	public partial class ApplicantPsychologicalCollection : esApplicantPsychologicalCollection, IEnumerable<ApplicantPsychological>
	{
		public ApplicantPsychologicalCollection()
		{

		}
		
		public static implicit operator List<ApplicantPsychological>(ApplicantPsychologicalCollection coll)
		{
			List<ApplicantPsychological> list = new List<ApplicantPsychological>();
			
			foreach (ApplicantPsychological emp in coll)
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
				return  ApplicantPsychologicalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantPsychologicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantPsychological(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantPsychological();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantPsychologicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantPsychologicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantPsychologicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantPsychological AddNew()
		{
			ApplicantPsychological entity = base.AddNewEntity() as ApplicantPsychological;
			
			return entity;
		}

		public ApplicantPsychological FindByPrimaryKey(System.Int32 applicantPsychologicalID)
		{
			return base.FindByPrimaryKey(applicantPsychologicalID) as ApplicantPsychological;
		}


		#region IEnumerable<ApplicantPsychological> Members

		IEnumerator<ApplicantPsychological> IEnumerable<ApplicantPsychological>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantPsychological;
			}
		}

		#endregion
		
		private ApplicantPsychologicalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantPsychological' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantPsychological ({ApplicantPsychologicalID})")]
	[Serializable]
	public partial class ApplicantPsychological : esApplicantPsychological
	{
		public ApplicantPsychological()
		{

		}
	
		public ApplicantPsychological(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantPsychologicalMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantPsychologicalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantPsychologicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantPsychologicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantPsychologicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantPsychologicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantPsychologicalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantPsychologicalQuery : esApplicantPsychologicalQuery
	{
		public ApplicantPsychologicalQuery()
		{

		}		
		
		public ApplicantPsychologicalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantPsychologicalQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantPsychologicalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantPsychologicalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantPsychologicalMetadata.ColumnNames.ApplicantPsychologicalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantPsychologicalMetadata.PropertyNames.ApplicantPsychologicalID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPsychologicalMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantPsychologicalMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPsychologicalMetadata.ColumnNames.SRPsychological, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPsychologicalMetadata.PropertyNames.SRPsychological;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPsychologicalMetadata.ColumnNames.SROperandType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPsychologicalMetadata.PropertyNames.SROperandType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPsychologicalMetadata.ColumnNames.PsychologicalValue, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPsychologicalMetadata.PropertyNames.PsychologicalValue;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPsychologicalMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantPsychologicalMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantPsychologicalMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantPsychologicalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantPsychologicalMetadata Meta()
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
			 public const string ApplicantPsychologicalID = "ApplicantPsychologicalID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRPsychological = "SRPsychological";
			 public const string SROperandType = "SROperandType";
			 public const string PsychologicalValue = "PsychologicalValue";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantPsychologicalID = "ApplicantPsychologicalID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRPsychological = "SRPsychological";
			 public const string SROperandType = "SROperandType";
			 public const string PsychologicalValue = "PsychologicalValue";
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
			lock (typeof(ApplicantPsychologicalMetadata))
			{
				if(ApplicantPsychologicalMetadata.mapDelegates == null)
				{
					ApplicantPsychologicalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantPsychologicalMetadata.meta == null)
				{
					ApplicantPsychologicalMetadata.meta = new ApplicantPsychologicalMetadata();
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
				

				meta.AddTypeMap("ApplicantPsychologicalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPsychological", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROperandType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PsychologicalValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantPsychological";
				meta.Destination = "ApplicantPsychological";
				
				meta.spInsert = "proc_ApplicantPsychologicalInsert";				
				meta.spUpdate = "proc_ApplicantPsychologicalUpdate";		
				meta.spDelete = "proc_ApplicantPsychologicalDelete";
				meta.spLoadAll = "proc_ApplicantPsychologicalLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantPsychologicalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantPsychologicalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
