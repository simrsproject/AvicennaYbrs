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
	abstract public class esPatientVitalSignMonitoringItemCollection : esEntityCollectionWAuditLog
	{
		public esPatientVitalSignMonitoringItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PatientVitalSignMonitoringItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientVitalSignMonitoringItemQuery query)
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
			this.InitQuery(query as esPatientVitalSignMonitoringItemQuery);
		}
		#endregion
		
		virtual public PatientVitalSignMonitoringItem DetachEntity(PatientVitalSignMonitoringItem entity)
		{
			return base.DetachEntity(entity) as PatientVitalSignMonitoringItem;
		}
		
		virtual public PatientVitalSignMonitoringItem AttachEntity(PatientVitalSignMonitoringItem entity)
		{
			return base.AttachEntity(entity) as PatientVitalSignMonitoringItem;
		}
		
		virtual public void Combine(PatientVitalSignMonitoringItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientVitalSignMonitoringItem this[int index]
		{
			get
			{
				return base[index] as PatientVitalSignMonitoringItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientVitalSignMonitoringItem);
		}
	}



	[Serializable]
	abstract public class esPatientVitalSignMonitoringItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientVitalSignMonitoringItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientVitalSignMonitoringItem()
		{

		}

		public esPatientVitalSignMonitoringItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String orderNo, System.String vitalSignID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, orderNo, vitalSignID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, orderNo, vitalSignID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String orderNo, System.String vitalSignID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, orderNo, vitalSignID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, orderNo, vitalSignID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String orderNo, System.String vitalSignID)
		{
			esPatientVitalSignMonitoringItemQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.OrderNo == orderNo, query.VitalSignID == vitalSignID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String orderNo, System.String vitalSignID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("OrderNo",orderNo);			parms.Add("VitalSignID",vitalSignID);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "OrderNo": this.str.OrderNo = (string)value; break;							
						case "VitalSignID": this.str.VitalSignID = (string)value; break;							
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
		/// Maps to PatientVitalSignMonitoringItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				if(base.SetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.RegistrationNo, value))
				{
					this._UpToPatientVitalSignMonitoringByRegistrationNo = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoringItem.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				if(base.SetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.OrderNo, value))
				{
					this._UpToPatientVitalSignMonitoringByRegistrationNo = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoringItem.VitalSignID
		/// </summary>
		virtual public System.String VitalSignID
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.VitalSignID);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.VitalSignID, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoringItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoringItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected PatientVitalSignMonitoring _UpToPatientVitalSignMonitoringByRegistrationNo;
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
			public esStrings(esPatientVitalSignMonitoringItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
				
			public System.String VitalSignID
			{
				get
				{
					System.String data = entity.VitalSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignID = null;
					else entity.VitalSignID = Convert.ToString(value);
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
			

			private esPatientVitalSignMonitoringItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientVitalSignMonitoringItemQuery query)
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
				throw new Exception("esPatientVitalSignMonitoringItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PatientVitalSignMonitoringItem : esPatientVitalSignMonitoringItem
	{

				
		#region UpToPatientVitalSignMonitoringByRegistrationNo - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - RefVitalSignMonitoringDtToVitalSignMonitoringHd
		/// </summary>

		[XmlIgnore]
		public PatientVitalSignMonitoring UpToPatientVitalSignMonitoringByRegistrationNo
		{
			get
			{
				if(this._UpToPatientVitalSignMonitoringByRegistrationNo == null
					&& RegistrationNo != null					&& OrderNo != null					)
				{
					this._UpToPatientVitalSignMonitoringByRegistrationNo = new PatientVitalSignMonitoring();
					this._UpToPatientVitalSignMonitoringByRegistrationNo.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToPatientVitalSignMonitoringByRegistrationNo", this._UpToPatientVitalSignMonitoringByRegistrationNo);
					this._UpToPatientVitalSignMonitoringByRegistrationNo.Query.Where(this._UpToPatientVitalSignMonitoringByRegistrationNo.Query.RegistrationNo == this.RegistrationNo);
					this._UpToPatientVitalSignMonitoringByRegistrationNo.Query.Where(this._UpToPatientVitalSignMonitoringByRegistrationNo.Query.OrderNo == this.OrderNo);
					this._UpToPatientVitalSignMonitoringByRegistrationNo.Query.Load();
				}

				return this._UpToPatientVitalSignMonitoringByRegistrationNo;
			}
			
			set
			{
				this.RemovePreSave("UpToPatientVitalSignMonitoringByRegistrationNo");
				

				if(value == null)
				{
					this.RegistrationNo = null;
					this.OrderNo = null;
					this._UpToPatientVitalSignMonitoringByRegistrationNo = null;
				}
				else
				{
					this.RegistrationNo = value.RegistrationNo;
					this.OrderNo = value.OrderNo;
					this._UpToPatientVitalSignMonitoringByRegistrationNo = value;
					this.SetPreSave("UpToPatientVitalSignMonitoringByRegistrationNo", this._UpToPatientVitalSignMonitoringByRegistrationNo);
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
	abstract public class esPatientVitalSignMonitoringItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PatientVitalSignMonitoringItemMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringItemMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem VitalSignID
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringItemMetadata.ColumnNames.VitalSignID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientVitalSignMonitoringItemCollection")]
	public partial class PatientVitalSignMonitoringItemCollection : esPatientVitalSignMonitoringItemCollection, IEnumerable<PatientVitalSignMonitoringItem>
	{
		public PatientVitalSignMonitoringItemCollection()
		{

		}
		
		public static implicit operator List<PatientVitalSignMonitoringItem>(PatientVitalSignMonitoringItemCollection coll)
		{
			List<PatientVitalSignMonitoringItem> list = new List<PatientVitalSignMonitoringItem>();
			
			foreach (PatientVitalSignMonitoringItem emp in coll)
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
				return  PatientVitalSignMonitoringItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientVitalSignMonitoringItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientVitalSignMonitoringItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientVitalSignMonitoringItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PatientVitalSignMonitoringItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientVitalSignMonitoringItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PatientVitalSignMonitoringItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PatientVitalSignMonitoringItem AddNew()
		{
			PatientVitalSignMonitoringItem entity = base.AddNewEntity() as PatientVitalSignMonitoringItem;
			
			return entity;
		}

		public PatientVitalSignMonitoringItem FindByPrimaryKey(System.String registrationNo, System.String orderNo, System.String vitalSignID)
		{
			return base.FindByPrimaryKey(registrationNo, orderNo, vitalSignID) as PatientVitalSignMonitoringItem;
		}


		#region IEnumerable<PatientVitalSignMonitoringItem> Members

		IEnumerator<PatientVitalSignMonitoringItem> IEnumerable<PatientVitalSignMonitoringItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientVitalSignMonitoringItem;
			}
		}

		#endregion
		
		private PatientVitalSignMonitoringItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientVitalSignMonitoringItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PatientVitalSignMonitoringItem ({RegistrationNo},{OrderNo},{VitalSignID})")]
	[Serializable]
	public partial class PatientVitalSignMonitoringItem : esPatientVitalSignMonitoringItem
	{
		public PatientVitalSignMonitoringItem()
		{

		}
	
		public PatientVitalSignMonitoringItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientVitalSignMonitoringItemMetadata.Meta();
			}
		}
		
		
		
		override protected esPatientVitalSignMonitoringItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientVitalSignMonitoringItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PatientVitalSignMonitoringItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientVitalSignMonitoringItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PatientVitalSignMonitoringItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PatientVitalSignMonitoringItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PatientVitalSignMonitoringItemQuery : esPatientVitalSignMonitoringItemQuery
	{
		public PatientVitalSignMonitoringItemQuery()
		{

		}		
		
		public PatientVitalSignMonitoringItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PatientVitalSignMonitoringItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class PatientVitalSignMonitoringItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientVitalSignMonitoringItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientVitalSignMonitoringItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringItemMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringItemMetadata.ColumnNames.OrderNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringItemMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringItemMetadata.ColumnNames.VitalSignID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringItemMetadata.PropertyNames.VitalSignID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientVitalSignMonitoringItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PatientVitalSignMonitoringItemMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string OrderNo = "OrderNo";
			 public const string VitalSignID = "VitalSignID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string OrderNo = "OrderNo";
			 public const string VitalSignID = "VitalSignID";
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
			lock (typeof(PatientVitalSignMonitoringItemMetadata))
			{
				if(PatientVitalSignMonitoringItemMetadata.mapDelegates == null)
				{
					PatientVitalSignMonitoringItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientVitalSignMonitoringItemMetadata.meta == null)
				{
					PatientVitalSignMonitoringItemMetadata.meta = new PatientVitalSignMonitoringItemMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PatientVitalSignMonitoringItem";
				meta.Destination = "PatientVitalSignMonitoringItem";
				
				meta.spInsert = "proc_PatientVitalSignMonitoringItemInsert";				
				meta.spUpdate = "proc_PatientVitalSignMonitoringItemUpdate";		
				meta.spDelete = "proc_PatientVitalSignMonitoringItemDelete";
				meta.spLoadAll = "proc_PatientVitalSignMonitoringItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientVitalSignMonitoringItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientVitalSignMonitoringItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
