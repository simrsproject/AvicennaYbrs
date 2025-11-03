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
	abstract public class esApplicantLicenceCollection : esEntityCollectionWAuditLog
	{
		public esApplicantLicenceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantLicenceCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantLicenceQuery query)
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
			this.InitQuery(query as esApplicantLicenceQuery);
		}
		#endregion
		
		virtual public ApplicantLicence DetachEntity(ApplicantLicence entity)
		{
			return base.DetachEntity(entity) as ApplicantLicence;
		}
		
		virtual public ApplicantLicence AttachEntity(ApplicantLicence entity)
		{
			return base.AttachEntity(entity) as ApplicantLicence;
		}
		
		virtual public void Combine(ApplicantLicenceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantLicence this[int index]
		{
			get
			{
				return base[index] as ApplicantLicence;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantLicence);
		}
	}



	[Serializable]
	abstract public class esApplicantLicence : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantLicenceQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantLicence()
		{

		}

		public esApplicantLicence(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantLicenceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantLicenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantLicenceID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantLicenceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantLicenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantLicenceID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantLicenceID)
		{
			esApplicantLicenceQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantLicenceID == applicantLicenceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantLicenceID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantLicenceID",applicantLicenceID);
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
						case "ApplicantLicenceID": this.str.ApplicantLicenceID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "SRLicenceType": this.str.SRLicenceType = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantLicenceID":
						
							if (value == null || value is System.Int32)
								this.ApplicantLicenceID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to ApplicantLicence.ApplicantLicenceID
		/// </summary>
		virtual public System.Int32? ApplicantLicenceID
		{
			get
			{
				return base.GetSystemInt32(ApplicantLicenceMetadata.ColumnNames.ApplicantLicenceID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantLicenceMetadata.ColumnNames.ApplicantLicenceID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantLicence.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantLicenceMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantLicenceMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantLicence.SRLicenceType
		/// </summary>
		virtual public System.String SRLicenceType
		{
			get
			{
				return base.GetSystemString(ApplicantLicenceMetadata.ColumnNames.SRLicenceType);
			}
			
			set
			{
				base.SetSystemString(ApplicantLicenceMetadata.ColumnNames.SRLicenceType, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantLicence.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(ApplicantLicenceMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantLicenceMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantLicence.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(ApplicantLicenceMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantLicenceMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantLicence.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(ApplicantLicenceMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(ApplicantLicenceMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantLicence.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantLicenceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantLicenceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantLicence.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantLicenceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantLicenceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantLicence entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantLicenceID
			{
				get
				{
					System.Int32? data = entity.ApplicantLicenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantLicenceID = null;
					else entity.ApplicantLicenceID = Convert.ToInt32(value);
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
				
			public System.String SRLicenceType
			{
				get
				{
					System.String data = entity.SRLicenceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLicenceType = null;
					else entity.SRLicenceType = Convert.ToString(value);
				}
			}
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
				}
			}
				
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			

			private esApplicantLicence entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantLicenceQuery query)
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
				throw new Exception("esApplicantLicence can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantLicence : esApplicantLicence
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
	abstract public class esApplicantLicenceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantLicenceMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantLicenceID
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.ApplicantLicenceID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRLicenceType
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.SRLicenceType, esSystemType.String);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantLicenceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantLicenceCollection")]
	public partial class ApplicantLicenceCollection : esApplicantLicenceCollection, IEnumerable<ApplicantLicence>
	{
		public ApplicantLicenceCollection()
		{

		}
		
		public static implicit operator List<ApplicantLicence>(ApplicantLicenceCollection coll)
		{
			List<ApplicantLicence> list = new List<ApplicantLicence>();
			
			foreach (ApplicantLicence emp in coll)
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
				return  ApplicantLicenceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantLicenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantLicence(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantLicence();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantLicenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantLicenceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantLicenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantLicence AddNew()
		{
			ApplicantLicence entity = base.AddNewEntity() as ApplicantLicence;
			
			return entity;
		}

		public ApplicantLicence FindByPrimaryKey(System.Int32 applicantLicenceID)
		{
			return base.FindByPrimaryKey(applicantLicenceID) as ApplicantLicence;
		}


		#region IEnumerable<ApplicantLicence> Members

		IEnumerator<ApplicantLicence> IEnumerable<ApplicantLicence>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantLicence;
			}
		}

		#endregion
		
		private ApplicantLicenceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantLicence' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantLicence ({ApplicantLicenceID})")]
	[Serializable]
	public partial class ApplicantLicence : esApplicantLicence
	{
		public ApplicantLicence()
		{

		}
	
		public ApplicantLicence(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantLicenceMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantLicenceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantLicenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantLicenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantLicenceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantLicenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantLicenceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantLicenceQuery : esApplicantLicenceQuery
	{
		public ApplicantLicenceQuery()
		{

		}		
		
		public ApplicantLicenceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantLicenceQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantLicenceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantLicenceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.ApplicantLicenceID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.ApplicantLicenceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.SRLicenceType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.SRLicenceType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.ValidTo, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.ValidTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.Note, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantLicenceMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantLicenceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantLicenceMetadata Meta()
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
			 public const string ApplicantLicenceID = "ApplicantLicenceID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRLicenceType = "SRLicenceType";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantLicenceID = "ApplicantLicenceID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRLicenceType = "SRLicenceType";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string Note = "Note";
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
			lock (typeof(ApplicantLicenceMetadata))
			{
				if(ApplicantLicenceMetadata.mapDelegates == null)
				{
					ApplicantLicenceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantLicenceMetadata.meta == null)
				{
					ApplicantLicenceMetadata.meta = new ApplicantLicenceMetadata();
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
				

				meta.AddTypeMap("ApplicantLicenceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRLicenceType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantLicence";
				meta.Destination = "ApplicantLicence";
				
				meta.spInsert = "proc_ApplicantLicenceInsert";				
				meta.spUpdate = "proc_ApplicantLicenceUpdate";		
				meta.spDelete = "proc_ApplicantLicenceDelete";
				meta.spLoadAll = "proc_ApplicantLicenceLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantLicenceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantLicenceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
