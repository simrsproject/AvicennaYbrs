/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esMedicalEmployeeInitialCollection : esEntityCollectionWAuditLog
	{
		public esMedicalEmployeeInitialCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MedicalEmployeeInitialCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalEmployeeInitialQuery query)
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
			this.InitQuery(query as esMedicalEmployeeInitialQuery);
		}
		#endregion
		
		virtual public MedicalEmployeeInitial DetachEntity(MedicalEmployeeInitial entity)
		{
			return base.DetachEntity(entity) as MedicalEmployeeInitial;
		}
		
		virtual public MedicalEmployeeInitial AttachEntity(MedicalEmployeeInitial entity)
		{
			return base.AttachEntity(entity) as MedicalEmployeeInitial;
		}
		
		virtual public void Combine(MedicalEmployeeInitialCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalEmployeeInitial this[int index]
		{
			get
			{
				return base[index] as MedicalEmployeeInitial;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalEmployeeInitial);
		}
	}



	[Serializable]
	abstract public class esMedicalEmployeeInitial : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalEmployeeInitialQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalEmployeeInitial()
		{

		}

		public esMedicalEmployeeInitial(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 medicalEmployeeInitialID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalEmployeeInitialID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalEmployeeInitialID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 medicalEmployeeInitialID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalEmployeeInitialID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalEmployeeInitialID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 medicalEmployeeInitialID)
		{
			esMedicalEmployeeInitialQuery query = this.GetDynamicQuery();
			query.Where(query.MedicalEmployeeInitialID == medicalEmployeeInitialID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 medicalEmployeeInitialID)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicalEmployeeInitialID",medicalEmployeeInitialID);
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
						case "MedicalEmployeeInitialID": this.str.MedicalEmployeeInitialID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "Year": this.str.Year = (string)value; break;							
						case "EmployeeUsedAmount": this.str.EmployeeUsedAmount = (string)value; break;							
						case "DependentUsedAmount": this.str.DependentUsedAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MedicalEmployeeInitialID":
						
							if (value == null || value is System.Int32)
								this.MedicalEmployeeInitialID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "EmployeeUsedAmount":
						
							if (value == null || value is System.Decimal)
								this.EmployeeUsedAmount = (System.Decimal?)value;
							break;
						
						case "DependentUsedAmount":
						
							if (value == null || value is System.Decimal)
								this.DependentUsedAmount = (System.Decimal?)value;
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
		/// Maps to MedicalEmployeeInitial.MedicalEmployeeInitialID
		/// </summary>
		virtual public System.Int32? MedicalEmployeeInitialID
		{
			get
			{
				return base.GetSystemInt32(MedicalEmployeeInitialMetadata.ColumnNames.MedicalEmployeeInitialID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalEmployeeInitialMetadata.ColumnNames.MedicalEmployeeInitialID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalEmployeeInitial.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MedicalEmployeeInitialMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalEmployeeInitialMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalEmployeeInitial.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(MedicalEmployeeInitialMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(MedicalEmployeeInitialMetadata.ColumnNames.Year, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalEmployeeInitial.EmployeeUsedAmount
		/// </summary>
		virtual public System.Decimal? EmployeeUsedAmount
		{
			get
			{
				return base.GetSystemDecimal(MedicalEmployeeInitialMetadata.ColumnNames.EmployeeUsedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalEmployeeInitialMetadata.ColumnNames.EmployeeUsedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalEmployeeInitial.DependentUsedAmount
		/// </summary>
		virtual public System.Decimal? DependentUsedAmount
		{
			get
			{
				return base.GetSystemDecimal(MedicalEmployeeInitialMetadata.ColumnNames.DependentUsedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalEmployeeInitialMetadata.ColumnNames.DependentUsedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalEmployeeInitial.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalEmployeeInitial.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalEmployeeInitial entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MedicalEmployeeInitialID
			{
				get
				{
					System.Int32? data = entity.MedicalEmployeeInitialID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalEmployeeInitialID = null;
					else entity.MedicalEmployeeInitialID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String Year
			{
				get
				{
					System.String data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToString(value);
				}
			}
				
			public System.String EmployeeUsedAmount
			{
				get
				{
					System.Decimal? data = entity.EmployeeUsedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeUsedAmount = null;
					else entity.EmployeeUsedAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DependentUsedAmount
			{
				get
				{
					System.Decimal? data = entity.DependentUsedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DependentUsedAmount = null;
					else entity.DependentUsedAmount = Convert.ToDecimal(value);
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
			

			private esMedicalEmployeeInitial entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalEmployeeInitialQuery query)
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
				throw new Exception("esMedicalEmployeeInitial can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MedicalEmployeeInitial : esMedicalEmployeeInitial
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
	abstract public class esMedicalEmployeeInitialQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MedicalEmployeeInitialMetadata.Meta();
			}
		}	
		

		public esQueryItem MedicalEmployeeInitialID
		{
			get
			{
				return new esQueryItem(this, MedicalEmployeeInitialMetadata.ColumnNames.MedicalEmployeeInitialID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MedicalEmployeeInitialMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, MedicalEmployeeInitialMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeUsedAmount
		{
			get
			{
				return new esQueryItem(this, MedicalEmployeeInitialMetadata.ColumnNames.EmployeeUsedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DependentUsedAmount
		{
			get
			{
				return new esQueryItem(this, MedicalEmployeeInitialMetadata.ColumnNames.DependentUsedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalEmployeeInitialCollection")]
	public partial class MedicalEmployeeInitialCollection : esMedicalEmployeeInitialCollection, IEnumerable<MedicalEmployeeInitial>
	{
		public MedicalEmployeeInitialCollection()
		{

		}
		
		public static implicit operator List<MedicalEmployeeInitial>(MedicalEmployeeInitialCollection coll)
		{
			List<MedicalEmployeeInitial> list = new List<MedicalEmployeeInitial>();
			
			foreach (MedicalEmployeeInitial emp in coll)
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
				return  MedicalEmployeeInitialMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalEmployeeInitialQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalEmployeeInitial(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalEmployeeInitial();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MedicalEmployeeInitialQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalEmployeeInitialQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MedicalEmployeeInitialQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MedicalEmployeeInitial AddNew()
		{
			MedicalEmployeeInitial entity = base.AddNewEntity() as MedicalEmployeeInitial;
			
			return entity;
		}

		public MedicalEmployeeInitial FindByPrimaryKey(System.Int32 medicalEmployeeInitialID)
		{
			return base.FindByPrimaryKey(medicalEmployeeInitialID) as MedicalEmployeeInitial;
		}


		#region IEnumerable<MedicalEmployeeInitial> Members

		IEnumerator<MedicalEmployeeInitial> IEnumerable<MedicalEmployeeInitial>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalEmployeeInitial;
			}
		}

		#endregion
		
		private MedicalEmployeeInitialQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalEmployeeInitial' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalEmployeeInitial ({MedicalEmployeeInitialID})")]
	[Serializable]
	public partial class MedicalEmployeeInitial : esMedicalEmployeeInitial
	{
		public MedicalEmployeeInitial()
		{

		}
	
		public MedicalEmployeeInitial(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalEmployeeInitialMetadata.Meta();
			}
		}
		
		
		
		override protected esMedicalEmployeeInitialQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalEmployeeInitialQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MedicalEmployeeInitialQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalEmployeeInitialQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MedicalEmployeeInitialQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MedicalEmployeeInitialQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MedicalEmployeeInitialQuery : esMedicalEmployeeInitialQuery
	{
		public MedicalEmployeeInitialQuery()
		{

		}		
		
		public MedicalEmployeeInitialQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MedicalEmployeeInitialQuery";
        }
		
			
	}


	[Serializable]
	public partial class MedicalEmployeeInitialMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalEmployeeInitialMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalEmployeeInitialMetadata.ColumnNames.MedicalEmployeeInitialID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalEmployeeInitialMetadata.PropertyNames.MedicalEmployeeInitialID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalEmployeeInitialMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalEmployeeInitialMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalEmployeeInitialMetadata.ColumnNames.Year, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalEmployeeInitialMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalEmployeeInitialMetadata.ColumnNames.EmployeeUsedAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalEmployeeInitialMetadata.PropertyNames.EmployeeUsedAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalEmployeeInitialMetadata.ColumnNames.DependentUsedAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalEmployeeInitialMetadata.PropertyNames.DependentUsedAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalEmployeeInitialMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalEmployeeInitialMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalEmployeeInitialMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MedicalEmployeeInitialMetadata Meta()
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
			 public const string MedicalEmployeeInitialID = "MedicalEmployeeInitialID";
			 public const string PersonID = "PersonID";
			 public const string Year = "Year";
			 public const string EmployeeUsedAmount = "EmployeeUsedAmount";
			 public const string DependentUsedAmount = "DependentUsedAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MedicalEmployeeInitialID = "MedicalEmployeeInitialID";
			 public const string PersonID = "PersonID";
			 public const string Year = "Year";
			 public const string EmployeeUsedAmount = "EmployeeUsedAmount";
			 public const string DependentUsedAmount = "DependentUsedAmount";
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
			lock (typeof(MedicalEmployeeInitialMetadata))
			{
				if(MedicalEmployeeInitialMetadata.mapDelegates == null)
				{
					MedicalEmployeeInitialMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalEmployeeInitialMetadata.meta == null)
				{
					MedicalEmployeeInitialMetadata.meta = new MedicalEmployeeInitialMetadata();
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
				

				meta.AddTypeMap("MedicalEmployeeInitialID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Year", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EmployeeUsedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DependentUsedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MedicalEmployeeInitial";
				meta.Destination = "MedicalEmployeeInitial";
				
				meta.spInsert = "proc_MedicalEmployeeInitialInsert";				
				meta.spUpdate = "proc_MedicalEmployeeInitialUpdate";		
				meta.spDelete = "proc_MedicalEmployeeInitialDelete";
				meta.spLoadAll = "proc_MedicalEmployeeInitialLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalEmployeeInitialLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalEmployeeInitialMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
