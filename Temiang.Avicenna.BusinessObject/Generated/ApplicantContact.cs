/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esApplicantContactCollection : esEntityCollectionWAuditLog
	{
		public esApplicantContactCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantContactCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantContactQuery query)
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
			this.InitQuery(query as esApplicantContactQuery);
		}
		#endregion
		
		virtual public ApplicantContact DetachEntity(ApplicantContact entity)
		{
			return base.DetachEntity(entity) as ApplicantContact;
		}
		
		virtual public ApplicantContact AttachEntity(ApplicantContact entity)
		{
			return base.AttachEntity(entity) as ApplicantContact;
		}
		
		virtual public void Combine(ApplicantContactCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantContact this[int index]
		{
			get
			{
				return base[index] as ApplicantContact;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantContact);
		}
	}



	[Serializable]
	abstract public class esApplicantContact : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantContactQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantContact()
		{

		}

		public esApplicantContact(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantContactID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantContactID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantContactID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantContactID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantContactID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantContactID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantContactID)
		{
			esApplicantContactQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantContactID == applicantContactID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantContactID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantContactID",applicantContactID);
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
						case "ApplicantContactID": this.str.ApplicantContactID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "SRContactType": this.str.SRContactType = (string)value; break;							
						case "ContactValue": this.str.ContactValue = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantContactID":
						
							if (value == null || value is System.Int32)
								this.ApplicantContactID = (System.Int32?)value;
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
		/// Maps to ApplicantContact.ApplicantContactID
		/// </summary>
		virtual public System.Int32? ApplicantContactID
		{
			get
			{
				return base.GetSystemInt32(ApplicantContactMetadata.ColumnNames.ApplicantContactID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantContactMetadata.ColumnNames.ApplicantContactID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantContact.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantContactMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantContactMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantContact.SRContactType
		/// </summary>
		virtual public System.String SRContactType
		{
			get
			{
				return base.GetSystemString(ApplicantContactMetadata.ColumnNames.SRContactType);
			}
			
			set
			{
				base.SetSystemString(ApplicantContactMetadata.ColumnNames.SRContactType, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantContact.ContactValue
		/// </summary>
		virtual public System.String ContactValue
		{
			get
			{
				return base.GetSystemString(ApplicantContactMetadata.ColumnNames.ContactValue);
			}
			
			set
			{
				base.SetSystemString(ApplicantContactMetadata.ColumnNames.ContactValue, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantContact.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantContactMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantContactMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantContact.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantContactMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantContactMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantContact entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantContactID
			{
				get
				{
					System.Int32? data = entity.ApplicantContactID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantContactID = null;
					else entity.ApplicantContactID = Convert.ToInt32(value);
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
				
			public System.String SRContactType
			{
				get
				{
					System.String data = entity.SRContactType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRContactType = null;
					else entity.SRContactType = Convert.ToString(value);
				}
			}
				
			public System.String ContactValue
			{
				get
				{
					System.String data = entity.ContactValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactValue = null;
					else entity.ContactValue = Convert.ToString(value);
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
			

			private esApplicantContact entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantContactQuery query)
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
				throw new Exception("esApplicantContact can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantContact : esApplicantContact
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
	abstract public class esApplicantContactQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantContactMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantContactID
		{
			get
			{
				return new esQueryItem(this, ApplicantContactMetadata.ColumnNames.ApplicantContactID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantContactMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRContactType
		{
			get
			{
				return new esQueryItem(this, ApplicantContactMetadata.ColumnNames.SRContactType, esSystemType.String);
			}
		} 
		
		public esQueryItem ContactValue
		{
			get
			{
				return new esQueryItem(this, ApplicantContactMetadata.ColumnNames.ContactValue, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantContactMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantContactMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantContactCollection")]
	public partial class ApplicantContactCollection : esApplicantContactCollection, IEnumerable<ApplicantContact>
	{
		public ApplicantContactCollection()
		{

		}
		
		public static implicit operator List<ApplicantContact>(ApplicantContactCollection coll)
		{
			List<ApplicantContact> list = new List<ApplicantContact>();
			
			foreach (ApplicantContact emp in coll)
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
				return  ApplicantContactMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantContact(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantContact();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantContactQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantContact AddNew()
		{
			ApplicantContact entity = base.AddNewEntity() as ApplicantContact;
			
			return entity;
		}

		public ApplicantContact FindByPrimaryKey(System.Int32 applicantContactID)
		{
			return base.FindByPrimaryKey(applicantContactID) as ApplicantContact;
		}


		#region IEnumerable<ApplicantContact> Members

		IEnumerator<ApplicantContact> IEnumerable<ApplicantContact>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantContact;
			}
		}

		#endregion
		
		private ApplicantContactQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantContact' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantContact ({ApplicantContactID})")]
	[Serializable]
	public partial class ApplicantContact : esApplicantContact
	{
		public ApplicantContact()
		{

		}
	
		public ApplicantContact(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantContactMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantContactQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantContactQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantContactQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantContactQuery : esApplicantContactQuery
	{
		public ApplicantContactQuery()
		{

		}		
		
		public ApplicantContactQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantContactQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantContactMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantContactMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantContactMetadata.ColumnNames.ApplicantContactID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantContactMetadata.PropertyNames.ApplicantContactID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantContactMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantContactMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantContactMetadata.ColumnNames.SRContactType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantContactMetadata.PropertyNames.SRContactType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantContactMetadata.ColumnNames.ContactValue, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantContactMetadata.PropertyNames.ContactValue;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantContactMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantContactMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantContactMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantContactMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantContactMetadata Meta()
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
			 public const string ApplicantContactID = "ApplicantContactID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRContactType = "SRContactType";
			 public const string ContactValue = "ContactValue";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantContactID = "ApplicantContactID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRContactType = "SRContactType";
			 public const string ContactValue = "ContactValue";
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
			lock (typeof(ApplicantContactMetadata))
			{
				if(ApplicantContactMetadata.mapDelegates == null)
				{
					ApplicantContactMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantContactMetadata.meta == null)
				{
					ApplicantContactMetadata.meta = new ApplicantContactMetadata();
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
				

				meta.AddTypeMap("ApplicantContactID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRContactType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContactValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantContact";
				meta.Destination = "ApplicantContact";
				
				meta.spInsert = "proc_ApplicantContactInsert";				
				meta.spUpdate = "proc_ApplicantContactUpdate";		
				meta.spDelete = "proc_ApplicantContactDelete";
				meta.spLoadAll = "proc_ApplicantContactLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantContactLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantContactMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
