/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/7/2016 10:24:13 AM
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
	abstract public class esParamedicFeeProgressiveTaxCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeProgressiveTaxCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeProgressiveTaxCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeProgressiveTaxQuery query)
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
			this.InitQuery(query as esParamedicFeeProgressiveTaxQuery);
		}
		#endregion
		
		virtual public ParamedicFeeProgressiveTax DetachEntity(ParamedicFeeProgressiveTax entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeProgressiveTax;
		}
		
		virtual public ParamedicFeeProgressiveTax AttachEntity(ParamedicFeeProgressiveTax entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeProgressiveTax;
		}
		
		virtual public void Combine(ParamedicFeeProgressiveTaxCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeProgressiveTax this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeProgressiveTax;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeProgressiveTax);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeProgressiveTax : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeProgressiveTaxQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeProgressiveTax()
		{

		}

		public esParamedicFeeProgressiveTax(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 counterID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 counterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 counterID)
		{
			esParamedicFeeProgressiveTaxQuery query = this.GetDynamicQuery();
			query.Where(query.CounterID == counterID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 counterID)
		{
			esParameters parms = new esParameters();
			parms.Add("CounterID",counterID);
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
						case "CounterID": this.str.CounterID = (string)value; break;							
						case "MinAmount": this.str.MinAmount = (string)value; break;							
						case "MaxAmount": this.str.MaxAmount = (string)value; break;							
						case "Percentage": this.str.Percentage = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "PercentageNonNpwp": this.str.PercentageNonNpwp = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CounterID":
						
							if (value == null || value is System.Int32)
								this.CounterID = (System.Int32?)value;
							break;
						
						case "MinAmount":
						
							if (value == null || value is System.Decimal)
								this.MinAmount = (System.Decimal?)value;
							break;
						
						case "MaxAmount":
						
							if (value == null || value is System.Decimal)
								this.MaxAmount = (System.Decimal?)value;
							break;
						
						case "Percentage":
						
							if (value == null || value is System.Decimal)
								this.Percentage = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "PercentageNonNpwp":
						
							if (value == null || value is System.Decimal)
								this.PercentageNonNpwp = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeProgressiveTax.CounterID
		/// </summary>
		virtual public System.Int32? CounterID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeProgressiveTaxMetadata.ColumnNames.CounterID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeProgressiveTaxMetadata.ColumnNames.CounterID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeProgressiveTax.MinAmount
		/// </summary>
		virtual public System.Decimal? MinAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.MinAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.MinAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeProgressiveTax.MaxAmount
		/// </summary>
		virtual public System.Decimal? MaxAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.MaxAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.MaxAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeProgressiveTax.Percentage
		/// </summary>
		virtual public System.Decimal? Percentage
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeProgressiveTax.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeProgressiveTax.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeProgressiveTax.PercentageNonNpwp
		/// </summary>
		virtual public System.Decimal? PercentageNonNpwp
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.PercentageNonNpwp);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeProgressiveTaxMetadata.ColumnNames.PercentageNonNpwp, value);
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
			public esStrings(esParamedicFeeProgressiveTax entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CounterID
			{
				get
				{
					System.Int32? data = entity.CounterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounterID = null;
					else entity.CounterID = Convert.ToInt32(value);
				}
			}
				
			public System.String MinAmount
			{
				get
				{
					System.Decimal? data = entity.MinAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinAmount = null;
					else entity.MinAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String MaxAmount
			{
				get
				{
					System.Decimal? data = entity.MaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxAmount = null;
					else entity.MaxAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String Percentage
			{
				get
				{
					System.Decimal? data = entity.Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Percentage = null;
					else entity.Percentage = Convert.ToDecimal(value);
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
				
			public System.String PercentageNonNpwp
			{
				get
				{
					System.Decimal? data = entity.PercentageNonNpwp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentageNonNpwp = null;
					else entity.PercentageNonNpwp = Convert.ToDecimal(value);
				}
			}
			

			private esParamedicFeeProgressiveTax entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeProgressiveTaxQuery query)
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
				throw new Exception("esParamedicFeeProgressiveTax can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esParamedicFeeProgressiveTaxQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeProgressiveTaxMetadata.Meta();
			}
		}	
		

		public esQueryItem CounterID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeProgressiveTaxMetadata.ColumnNames.CounterID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MinAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeProgressiveTaxMetadata.ColumnNames.MinAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MaxAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeProgressiveTaxMetadata.ColumnNames.MaxAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Percentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeProgressiveTaxMetadata.ColumnNames.Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem PercentageNonNpwp
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeProgressiveTaxMetadata.ColumnNames.PercentageNonNpwp, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeProgressiveTaxCollection")]
	public partial class ParamedicFeeProgressiveTaxCollection : esParamedicFeeProgressiveTaxCollection, IEnumerable<ParamedicFeeProgressiveTax>
	{
		public ParamedicFeeProgressiveTaxCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeProgressiveTax>(ParamedicFeeProgressiveTaxCollection coll)
		{
			List<ParamedicFeeProgressiveTax> list = new List<ParamedicFeeProgressiveTax>();
			
			foreach (ParamedicFeeProgressiveTax emp in coll)
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
				return  ParamedicFeeProgressiveTaxMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeProgressiveTaxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeProgressiveTax(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeProgressiveTax();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeProgressiveTaxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeProgressiveTaxQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeProgressiveTaxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeProgressiveTax AddNew()
		{
			ParamedicFeeProgressiveTax entity = base.AddNewEntity() as ParamedicFeeProgressiveTax;
			
			return entity;
		}

		public ParamedicFeeProgressiveTax FindByPrimaryKey(System.Int32 counterID)
		{
			return base.FindByPrimaryKey(counterID) as ParamedicFeeProgressiveTax;
		}


		#region IEnumerable<ParamedicFeeProgressiveTax> Members

		IEnumerator<ParamedicFeeProgressiveTax> IEnumerable<ParamedicFeeProgressiveTax>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeProgressiveTax;
			}
		}

		#endregion
		
		private ParamedicFeeProgressiveTaxQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeProgressiveTax' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeProgressiveTax ({CounterID})")]
	[Serializable]
	public partial class ParamedicFeeProgressiveTax : esParamedicFeeProgressiveTax
	{
		public ParamedicFeeProgressiveTax()
		{

		}
	
		public ParamedicFeeProgressiveTax(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeProgressiveTaxMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeProgressiveTaxQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeProgressiveTaxQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeProgressiveTaxQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeProgressiveTaxQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeProgressiveTaxQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeProgressiveTaxQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeProgressiveTaxQuery : esParamedicFeeProgressiveTaxQuery
	{
		public ParamedicFeeProgressiveTaxQuery()
		{

		}		
		
		public ParamedicFeeProgressiveTaxQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeProgressiveTaxQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeProgressiveTaxMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeProgressiveTaxMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeProgressiveTaxMetadata.ColumnNames.CounterID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeProgressiveTaxMetadata.PropertyNames.CounterID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeProgressiveTaxMetadata.ColumnNames.MinAmount, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeProgressiveTaxMetadata.PropertyNames.MinAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeProgressiveTaxMetadata.ColumnNames.MaxAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeProgressiveTaxMetadata.PropertyNames.MaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeProgressiveTaxMetadata.ColumnNames.Percentage, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeProgressiveTaxMetadata.PropertyNames.Percentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeProgressiveTaxMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeProgressiveTaxMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeProgressiveTaxMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeProgressiveTaxMetadata.ColumnNames.PercentageNonNpwp, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeProgressiveTaxMetadata.PropertyNames.PercentageNonNpwp;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeProgressiveTaxMetadata Meta()
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
			 public const string CounterID = "CounterID";
			 public const string MinAmount = "MinAmount";
			 public const string MaxAmount = "MaxAmount";
			 public const string Percentage = "Percentage";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PercentageNonNpwp = "PercentageNonNpwp";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CounterID = "CounterID";
			 public const string MinAmount = "MinAmount";
			 public const string MaxAmount = "MaxAmount";
			 public const string Percentage = "Percentage";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PercentageNonNpwp = "PercentageNonNpwp";
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
			lock (typeof(ParamedicFeeProgressiveTaxMetadata))
			{
				if(ParamedicFeeProgressiveTaxMetadata.mapDelegates == null)
				{
					ParamedicFeeProgressiveTaxMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeProgressiveTaxMetadata.meta == null)
				{
					ParamedicFeeProgressiveTaxMetadata.meta = new ParamedicFeeProgressiveTaxMetadata();
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
				

				meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MinAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PercentageNonNpwp", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "ParamedicFeeProgressiveTax";
				meta.Destination = "ParamedicFeeProgressiveTax";
				
				meta.spInsert = "proc_ParamedicFeeProgressiveTaxInsert";				
				meta.spUpdate = "proc_ParamedicFeeProgressiveTaxUpdate";		
				meta.spDelete = "proc_ParamedicFeeProgressiveTaxDelete";
				meta.spLoadAll = "proc_ParamedicFeeProgressiveTaxLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeProgressiveTaxLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeProgressiveTaxMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
