/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/23/2014 9:00:37 PM
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
	abstract public class esChargeBedAutoBillMatrixCollection : esEntityCollectionWAuditLog
	{
		public esChargeBedAutoBillMatrixCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ChargeBedAutoBillMatrixCollection";
		}

		#region Query Logic
		protected void InitQuery(esChargeBedAutoBillMatrixQuery query)
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
			this.InitQuery(query as esChargeBedAutoBillMatrixQuery);
		}
		#endregion
		
		virtual public ChargeBedAutoBillMatrix DetachEntity(ChargeBedAutoBillMatrix entity)
		{
			return base.DetachEntity(entity) as ChargeBedAutoBillMatrix;
		}
		
		virtual public ChargeBedAutoBillMatrix AttachEntity(ChargeBedAutoBillMatrix entity)
		{
			return base.AttachEntity(entity) as ChargeBedAutoBillMatrix;
		}
		
		virtual public void Combine(ChargeBedAutoBillMatrixCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ChargeBedAutoBillMatrix this[int index]
		{
			get
			{
				return base[index] as ChargeBedAutoBillMatrix;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ChargeBedAutoBillMatrix);
		}
	}



	[Serializable]
	abstract public class esChargeBedAutoBillMatrix : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esChargeBedAutoBillMatrixQuery GetDynamicQuery()
		{
			return null;
		}

		public esChargeBedAutoBillMatrix()
		{

		}

		public esChargeBedAutoBillMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sequenceNo)
		{
			esChargeBedAutoBillMatrixQuery query = this.GetDynamicQuery();
			query.Where(query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("SequenceNo",sequenceNo);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "MinHour": this.str.MinHour = (string)value; break;							
						case "MaxHour": this.str.MaxHour = (string)value; break;							
						case "PercentageAmount": this.str.PercentageAmount = (string)value; break;							
						case "ValueAmount": this.str.ValueAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MinHour":
						
							if (value == null || value is System.Decimal)
								this.MinHour = (System.Decimal?)value;
							break;
						
						case "MaxHour":
						
							if (value == null || value is System.Decimal)
								this.MaxHour = (System.Decimal?)value;
							break;
						
						case "PercentageAmount":
						
							if (value == null || value is System.Decimal)
								this.PercentageAmount = (System.Decimal?)value;
							break;
						
						case "ValueAmount":
						
							if (value == null || value is System.Decimal)
								this.ValueAmount = (System.Decimal?)value;
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
		/// Maps to ChargeBedAutoBillMatrix.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ChargeBedAutoBillMatrixMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ChargeBedAutoBillMatrixMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ChargeBedAutoBillMatrix.MinHour
		/// </summary>
		virtual public System.Decimal? MinHour
		{
			get
			{
				return base.GetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.MinHour);
			}
			
			set
			{
				base.SetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.MinHour, value);
			}
		}
		
		/// <summary>
		/// Maps to ChargeBedAutoBillMatrix.MaxHour
		/// </summary>
		virtual public System.Decimal? MaxHour
		{
			get
			{
				return base.GetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.MaxHour);
			}
			
			set
			{
				base.SetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.MaxHour, value);
			}
		}
		
		/// <summary>
		/// Maps to ChargeBedAutoBillMatrix.PercentageAmount
		/// </summary>
		virtual public System.Decimal? PercentageAmount
		{
			get
			{
				return base.GetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.PercentageAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.PercentageAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ChargeBedAutoBillMatrix.ValueAmount
		/// </summary>
		virtual public System.Decimal? ValueAmount
		{
			get
			{
				return base.GetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.ValueAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ChargeBedAutoBillMatrixMetadata.ColumnNames.ValueAmount, value);
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
			public esStrings(esChargeBedAutoBillMatrix entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
				
			public System.String MinHour
			{
				get
				{
					System.Decimal? data = entity.MinHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinHour = null;
					else entity.MinHour = Convert.ToDecimal(value);
				}
			}
				
			public System.String MaxHour
			{
				get
				{
					System.Decimal? data = entity.MaxHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxHour = null;
					else entity.MaxHour = Convert.ToDecimal(value);
				}
			}
				
			public System.String PercentageAmount
			{
				get
				{
					System.Decimal? data = entity.PercentageAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentageAmount = null;
					else entity.PercentageAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String ValueAmount
			{
				get
				{
					System.Decimal? data = entity.ValueAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueAmount = null;
					else entity.ValueAmount = Convert.ToDecimal(value);
				}
			}
			

			private esChargeBedAutoBillMatrix entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esChargeBedAutoBillMatrixQuery query)
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
				throw new Exception("esChargeBedAutoBillMatrix can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esChargeBedAutoBillMatrixQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ChargeBedAutoBillMatrixMetadata.Meta();
			}
		}	
		

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ChargeBedAutoBillMatrixMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MinHour
		{
			get
			{
				return new esQueryItem(this, ChargeBedAutoBillMatrixMetadata.ColumnNames.MinHour, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MaxHour
		{
			get
			{
				return new esQueryItem(this, ChargeBedAutoBillMatrixMetadata.ColumnNames.MaxHour, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PercentageAmount
		{
			get
			{
				return new esQueryItem(this, ChargeBedAutoBillMatrixMetadata.ColumnNames.PercentageAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ValueAmount
		{
			get
			{
				return new esQueryItem(this, ChargeBedAutoBillMatrixMetadata.ColumnNames.ValueAmount, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ChargeBedAutoBillMatrixCollection")]
	public partial class ChargeBedAutoBillMatrixCollection : esChargeBedAutoBillMatrixCollection, IEnumerable<ChargeBedAutoBillMatrix>
	{
		public ChargeBedAutoBillMatrixCollection()
		{

		}
		
		public static implicit operator List<ChargeBedAutoBillMatrix>(ChargeBedAutoBillMatrixCollection coll)
		{
			List<ChargeBedAutoBillMatrix> list = new List<ChargeBedAutoBillMatrix>();
			
			foreach (ChargeBedAutoBillMatrix emp in coll)
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
				return  ChargeBedAutoBillMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ChargeBedAutoBillMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ChargeBedAutoBillMatrix(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ChargeBedAutoBillMatrix();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ChargeBedAutoBillMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ChargeBedAutoBillMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ChargeBedAutoBillMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ChargeBedAutoBillMatrix AddNew()
		{
			ChargeBedAutoBillMatrix entity = base.AddNewEntity() as ChargeBedAutoBillMatrix;
			
			return entity;
		}

		public ChargeBedAutoBillMatrix FindByPrimaryKey(System.String sequenceNo)
		{
			return base.FindByPrimaryKey(sequenceNo) as ChargeBedAutoBillMatrix;
		}


		#region IEnumerable<ChargeBedAutoBillMatrix> Members

		IEnumerator<ChargeBedAutoBillMatrix> IEnumerable<ChargeBedAutoBillMatrix>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ChargeBedAutoBillMatrix;
			}
		}

		#endregion
		
		private ChargeBedAutoBillMatrixQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ChargeBedAutoBillMatrix' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ChargeBedAutoBillMatrix ({SequenceNo})")]
	[Serializable]
	public partial class ChargeBedAutoBillMatrix : esChargeBedAutoBillMatrix
	{
		public ChargeBedAutoBillMatrix()
		{

		}
	
		public ChargeBedAutoBillMatrix(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ChargeBedAutoBillMatrixMetadata.Meta();
			}
		}
		
		
		
		override protected esChargeBedAutoBillMatrixQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ChargeBedAutoBillMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ChargeBedAutoBillMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ChargeBedAutoBillMatrixQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ChargeBedAutoBillMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ChargeBedAutoBillMatrixQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ChargeBedAutoBillMatrixQuery : esChargeBedAutoBillMatrixQuery
	{
		public ChargeBedAutoBillMatrixQuery()
		{

		}		
		
		public ChargeBedAutoBillMatrixQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ChargeBedAutoBillMatrixQuery";
        }
		
			
	}


	[Serializable]
	public partial class ChargeBedAutoBillMatrixMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ChargeBedAutoBillMatrixMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ChargeBedAutoBillMatrixMetadata.ColumnNames.SequenceNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ChargeBedAutoBillMatrixMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChargeBedAutoBillMatrixMetadata.ColumnNames.MinHour, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChargeBedAutoBillMatrixMetadata.PropertyNames.MinHour;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChargeBedAutoBillMatrixMetadata.ColumnNames.MaxHour, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChargeBedAutoBillMatrixMetadata.PropertyNames.MaxHour;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChargeBedAutoBillMatrixMetadata.ColumnNames.PercentageAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChargeBedAutoBillMatrixMetadata.PropertyNames.PercentageAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ChargeBedAutoBillMatrixMetadata.ColumnNames.ValueAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ChargeBedAutoBillMatrixMetadata.PropertyNames.ValueAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ChargeBedAutoBillMatrixMetadata Meta()
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
			 public const string SequenceNo = "SequenceNo";
			 public const string MinHour = "MinHour";
			 public const string MaxHour = "MaxHour";
			 public const string PercentageAmount = "PercentageAmount";
			 public const string ValueAmount = "ValueAmount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SequenceNo = "SequenceNo";
			 public const string MinHour = "MinHour";
			 public const string MaxHour = "MaxHour";
			 public const string PercentageAmount = "PercentageAmount";
			 public const string ValueAmount = "ValueAmount";
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
			lock (typeof(ChargeBedAutoBillMatrixMetadata))
			{
				if(ChargeBedAutoBillMatrixMetadata.mapDelegates == null)
				{
					ChargeBedAutoBillMatrixMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ChargeBedAutoBillMatrixMetadata.meta == null)
				{
					ChargeBedAutoBillMatrixMetadata.meta = new ChargeBedAutoBillMatrixMetadata();
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
				

				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinHour", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("MaxHour", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PercentageAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ValueAmount", new esTypeMap("decimal", "System.Decimal"));			
				
				
				
				meta.Source = "ChargeBedAutoBillMatrix";
				meta.Destination = "ChargeBedAutoBillMatrix";
				
				meta.spInsert = "proc_ChargeBedAutoBillMatrixInsert";				
				meta.spUpdate = "proc_ChargeBedAutoBillMatrixUpdate";		
				meta.spDelete = "proc_ChargeBedAutoBillMatrixDelete";
				meta.spLoadAll = "proc_ChargeBedAutoBillMatrixLoadAll";
				meta.spLoadByPrimaryKey = "proc_ChargeBedAutoBillMatrixLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ChargeBedAutoBillMatrixMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
