/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:11 PM
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
	abstract public class esAttedanceMatrixCollection : esEntityCollectionWAuditLog
	{
		public esAttedanceMatrixCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AttedanceMatrixCollection";
		}

		#region Query Logic
		protected void InitQuery(esAttedanceMatrixQuery query)
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
			this.InitQuery(query as esAttedanceMatrixQuery);
		}
		#endregion
		
		virtual public AttedanceMatrix DetachEntity(AttedanceMatrix entity)
		{
			return base.DetachEntity(entity) as AttedanceMatrix;
		}
		
		virtual public AttedanceMatrix AttachEntity(AttedanceMatrix entity)
		{
			return base.AttachEntity(entity) as AttedanceMatrix;
		}
		
		virtual public void Combine(AttedanceMatrixCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AttedanceMatrix this[int index]
		{
			get
			{
				return base[index] as AttedanceMatrix;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AttedanceMatrix);
		}
	}



	[Serializable]
	abstract public class esAttedanceMatrix : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAttedanceMatrixQuery GetDynamicQuery()
		{
			return null;
		}

		public esAttedanceMatrix()
		{

		}

		public esAttedanceMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 attedanceMatrixID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(attedanceMatrixID);
			else
				return LoadByPrimaryKeyStoredProcedure(attedanceMatrixID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 attedanceMatrixID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(attedanceMatrixID);
			else
				return LoadByPrimaryKeyStoredProcedure(attedanceMatrixID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 attedanceMatrixID)
		{
			esAttedanceMatrixQuery query = this.GetDynamicQuery();
			query.Where(query.AttedanceMatrixID == attedanceMatrixID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 attedanceMatrixID)
		{
			esParameters parms = new esParameters();
			parms.Add("AttedanceMatrixID",attedanceMatrixID);
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
						case "AttedanceMatrixID": this.str.AttedanceMatrixID = (string)value; break;							
						case "AttedanceMatrixName": this.str.AttedanceMatrixName = (string)value; break;							
						case "AttedanceMatrixFieldt": this.str.AttedanceMatrixFieldt = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AttedanceMatrixID":
						
							if (value == null || value is System.Int32)
								this.AttedanceMatrixID = (System.Int32?)value;
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
		/// Maps to AttedanceMatrix.AttedanceMatrixID
		/// </summary>
		virtual public System.Int32? AttedanceMatrixID
		{
			get
			{
				return base.GetSystemInt32(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixID);
			}
			
			set
			{
				base.SetSystemInt32(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixID, value);
			}
		}
		
		/// <summary>
		/// Maps to AttedanceMatrix.AttedanceMatrixName
		/// </summary>
		virtual public System.String AttedanceMatrixName
		{
			get
			{
				return base.GetSystemString(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixName);
			}
			
			set
			{
				base.SetSystemString(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixName, value);
			}
		}
		
		/// <summary>
		/// Maps to AttedanceMatrix.AttedanceMatrixFieldt
		/// </summary>
		virtual public System.String AttedanceMatrixFieldt
		{
			get
			{
				return base.GetSystemString(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixFieldt);
			}
			
			set
			{
				base.SetSystemString(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixFieldt, value);
			}
		}
		
		/// <summary>
		/// Maps to AttedanceMatrix.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AttedanceMatrixMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AttedanceMatrixMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AttedanceMatrix.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AttedanceMatrixMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AttedanceMatrixMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAttedanceMatrix entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AttedanceMatrixID
			{
				get
				{
					System.Int32? data = entity.AttedanceMatrixID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AttedanceMatrixID = null;
					else entity.AttedanceMatrixID = Convert.ToInt32(value);
				}
			}
				
			public System.String AttedanceMatrixName
			{
				get
				{
					System.String data = entity.AttedanceMatrixName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AttedanceMatrixName = null;
					else entity.AttedanceMatrixName = Convert.ToString(value);
				}
			}
				
			public System.String AttedanceMatrixFieldt
			{
				get
				{
					System.String data = entity.AttedanceMatrixFieldt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AttedanceMatrixFieldt = null;
					else entity.AttedanceMatrixFieldt = Convert.ToString(value);
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
			

			private esAttedanceMatrix entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAttedanceMatrixQuery query)
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
				throw new Exception("esAttedanceMatrix can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AttedanceMatrix : esAttedanceMatrix
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
	abstract public class esAttedanceMatrixQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AttedanceMatrixMetadata.Meta();
			}
		}	
		

		public esQueryItem AttedanceMatrixID
		{
			get
			{
				return new esQueryItem(this, AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AttedanceMatrixName
		{
			get
			{
				return new esQueryItem(this, AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixName, esSystemType.String);
			}
		} 
		
		public esQueryItem AttedanceMatrixFieldt
		{
			get
			{
				return new esQueryItem(this, AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixFieldt, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AttedanceMatrixMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AttedanceMatrixMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AttedanceMatrixCollection")]
	public partial class AttedanceMatrixCollection : esAttedanceMatrixCollection, IEnumerable<AttedanceMatrix>
	{
		public AttedanceMatrixCollection()
		{

		}
		
		public static implicit operator List<AttedanceMatrix>(AttedanceMatrixCollection coll)
		{
			List<AttedanceMatrix> list = new List<AttedanceMatrix>();
			
			foreach (AttedanceMatrix emp in coll)
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
				return  AttedanceMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AttedanceMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AttedanceMatrix(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AttedanceMatrix();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AttedanceMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AttedanceMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AttedanceMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AttedanceMatrix AddNew()
		{
			AttedanceMatrix entity = base.AddNewEntity() as AttedanceMatrix;
			
			return entity;
		}

		public AttedanceMatrix FindByPrimaryKey(System.Int32 attedanceMatrixID)
		{
			return base.FindByPrimaryKey(attedanceMatrixID) as AttedanceMatrix;
		}


		#region IEnumerable<AttedanceMatrix> Members

		IEnumerator<AttedanceMatrix> IEnumerable<AttedanceMatrix>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AttedanceMatrix;
			}
		}

		#endregion
		
		private AttedanceMatrixQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AttedanceMatrix' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AttedanceMatrix ({AttedanceMatrixID})")]
	[Serializable]
	public partial class AttedanceMatrix : esAttedanceMatrix
	{
		public AttedanceMatrix()
		{

		}
	
		public AttedanceMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AttedanceMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esAttedanceMatrixQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AttedanceMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AttedanceMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AttedanceMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AttedanceMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AttedanceMatrixQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AttedanceMatrixQuery : esAttedanceMatrixQuery
	{
		public AttedanceMatrixQuery()
		{

		}		
		
		public AttedanceMatrixQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AttedanceMatrixQuery";
        }
		
			
	}


	[Serializable]
	public partial class AttedanceMatrixMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AttedanceMatrixMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AttedanceMatrixMetadata.PropertyNames.AttedanceMatrixID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AttedanceMatrixMetadata.PropertyNames.AttedanceMatrixName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AttedanceMatrixMetadata.ColumnNames.AttedanceMatrixFieldt, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AttedanceMatrixMetadata.PropertyNames.AttedanceMatrixFieldt;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AttedanceMatrixMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AttedanceMatrixMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(AttedanceMatrixMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AttedanceMatrixMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AttedanceMatrixMetadata Meta()
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
			 public const string AttedanceMatrixID = "AttedanceMatrixID";
			 public const string AttedanceMatrixName = "AttedanceMatrixName";
			 public const string AttedanceMatrixFieldt = "AttedanceMatrixFieldt";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AttedanceMatrixID = "AttedanceMatrixID";
			 public const string AttedanceMatrixName = "AttedanceMatrixName";
			 public const string AttedanceMatrixFieldt = "AttedanceMatrixFieldt";
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
			lock (typeof(AttedanceMatrixMetadata))
			{
				if(AttedanceMatrixMetadata.mapDelegates == null)
				{
					AttedanceMatrixMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AttedanceMatrixMetadata.meta == null)
				{
					AttedanceMatrixMetadata.meta = new AttedanceMatrixMetadata();
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
				

				meta.AddTypeMap("AttedanceMatrixID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AttedanceMatrixName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AttedanceMatrixFieldt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AttedanceMatrix";
				meta.Destination = "AttedanceMatrix";
				
				meta.spInsert = "proc_AttedanceMatrixInsert";				
				meta.spUpdate = "proc_AttedanceMatrixUpdate";		
				meta.spDelete = "proc_AttedanceMatrixDelete";
				meta.spLoadAll = "proc_AttedanceMatrixLoadAll";
				meta.spLoadByPrimaryKey = "proc_AttedanceMatrixLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AttedanceMatrixMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
