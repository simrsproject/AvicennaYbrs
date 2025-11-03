/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/4/2016 10:51:50 AM
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
	abstract public class esParamedicFeeByNumberOfPatientsRangeAmountCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeByNumberOfPatientsRangeAmountCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeByNumberOfPatientsRangeAmountCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeByNumberOfPatientsRangeAmountQuery query)
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
			this.InitQuery(query as esParamedicFeeByNumberOfPatientsRangeAmountQuery);
		}
		#endregion
		
		virtual public ParamedicFeeByNumberOfPatientsRangeAmount DetachEntity(ParamedicFeeByNumberOfPatientsRangeAmount entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeByNumberOfPatientsRangeAmount;
		}
		
		virtual public ParamedicFeeByNumberOfPatientsRangeAmount AttachEntity(ParamedicFeeByNumberOfPatientsRangeAmount entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeByNumberOfPatientsRangeAmount;
		}
		
		virtual public void Combine(ParamedicFeeByNumberOfPatientsRangeAmountCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeByNumberOfPatientsRangeAmount this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeByNumberOfPatientsRangeAmount;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeByNumberOfPatientsRangeAmount);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeByNumberOfPatientsRangeAmount : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeByNumberOfPatientsRangeAmountQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeByNumberOfPatientsRangeAmount()
		{

		}

		public esParamedicFeeByNumberOfPatientsRangeAmount(DataRow row)
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
			esParamedicFeeByNumberOfPatientsRangeAmountQuery query = this.GetDynamicQuery();
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
						case "Amount": this.str.Amount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
						
							if (value == null || value is System.Int16)
								this.MinAmount = (System.Int16?)value;
							break;
						
						case "MaxAmount":
						
							if (value == null || value is System.Int16)
								this.MaxAmount = (System.Int16?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeByNumberOfPatientsRangeAmount.CounterID
		/// </summary>
		virtual public System.Int32? CounterID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.CounterID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.CounterID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsRangeAmount.MinAmount
		/// </summary>
		virtual public System.Int16? MinAmount
		{
			get
			{
				return base.GetSystemInt16(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MinAmount);
			}
			
			set
			{
				base.SetSystemInt16(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MinAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsRangeAmount.MaxAmount
		/// </summary>
		virtual public System.Int16? MaxAmount
		{
			get
			{
				return base.GetSystemInt16(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MaxAmount);
			}
			
			set
			{
				base.SetSystemInt16(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MaxAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsRangeAmount.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsRangeAmount.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeByNumberOfPatientsRangeAmount.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeByNumberOfPatientsRangeAmount entity)
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
					System.Int16? data = entity.MinAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinAmount = null;
					else entity.MinAmount = Convert.ToInt16(value);
				}
			}
				
			public System.String MaxAmount
			{
				get
				{
					System.Int16? data = entity.MaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxAmount = null;
					else entity.MaxAmount = Convert.ToInt16(value);
				}
			}
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
			

			private esParamedicFeeByNumberOfPatientsRangeAmount entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeByNumberOfPatientsRangeAmountQuery query)
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
				throw new Exception("esParamedicFeeByNumberOfPatientsRangeAmount can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeByNumberOfPatientsRangeAmount : esParamedicFeeByNumberOfPatientsRangeAmount
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
	abstract public class esParamedicFeeByNumberOfPatientsRangeAmountQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByNumberOfPatientsRangeAmountMetadata.Meta();
			}
		}	
		

		public esQueryItem CounterID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.CounterID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MinAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MinAmount, esSystemType.Int16);
			}
		} 
		
		public esQueryItem MaxAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MaxAmount, esSystemType.Int16);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeByNumberOfPatientsRangeAmountCollection")]
	public partial class ParamedicFeeByNumberOfPatientsRangeAmountCollection : esParamedicFeeByNumberOfPatientsRangeAmountCollection, IEnumerable<ParamedicFeeByNumberOfPatientsRangeAmount>
	{
		public ParamedicFeeByNumberOfPatientsRangeAmountCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeByNumberOfPatientsRangeAmount>(ParamedicFeeByNumberOfPatientsRangeAmountCollection coll)
		{
			List<ParamedicFeeByNumberOfPatientsRangeAmount> list = new List<ParamedicFeeByNumberOfPatientsRangeAmount>();
			
			foreach (ParamedicFeeByNumberOfPatientsRangeAmount emp in coll)
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
				return  ParamedicFeeByNumberOfPatientsRangeAmountMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByNumberOfPatientsRangeAmountQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeByNumberOfPatientsRangeAmount(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeByNumberOfPatientsRangeAmount();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeByNumberOfPatientsRangeAmountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByNumberOfPatientsRangeAmountQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeByNumberOfPatientsRangeAmountQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeByNumberOfPatientsRangeAmount AddNew()
		{
			ParamedicFeeByNumberOfPatientsRangeAmount entity = base.AddNewEntity() as ParamedicFeeByNumberOfPatientsRangeAmount;
			
			return entity;
		}

		public ParamedicFeeByNumberOfPatientsRangeAmount FindByPrimaryKey(System.Int32 counterID)
		{
			return base.FindByPrimaryKey(counterID) as ParamedicFeeByNumberOfPatientsRangeAmount;
		}


		#region IEnumerable<ParamedicFeeByNumberOfPatientsRangeAmount> Members

		IEnumerator<ParamedicFeeByNumberOfPatientsRangeAmount> IEnumerable<ParamedicFeeByNumberOfPatientsRangeAmount>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeByNumberOfPatientsRangeAmount;
			}
		}

		#endregion
		
		private ParamedicFeeByNumberOfPatientsRangeAmountQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeByNumberOfPatientsRangeAmount' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeByNumberOfPatientsRangeAmount ({CounterID})")]
	[Serializable]
	public partial class ParamedicFeeByNumberOfPatientsRangeAmount : esParamedicFeeByNumberOfPatientsRangeAmount
	{
		public ParamedicFeeByNumberOfPatientsRangeAmount()
		{

		}
	
		public ParamedicFeeByNumberOfPatientsRangeAmount(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeByNumberOfPatientsRangeAmountMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeByNumberOfPatientsRangeAmountQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeByNumberOfPatientsRangeAmountQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeByNumberOfPatientsRangeAmountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeByNumberOfPatientsRangeAmountQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeByNumberOfPatientsRangeAmountQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeByNumberOfPatientsRangeAmountQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeByNumberOfPatientsRangeAmountQuery : esParamedicFeeByNumberOfPatientsRangeAmountQuery
	{
		public ParamedicFeeByNumberOfPatientsRangeAmountQuery()
		{

		}		
		
		public ParamedicFeeByNumberOfPatientsRangeAmountQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeByNumberOfPatientsRangeAmountQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeByNumberOfPatientsRangeAmountMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeByNumberOfPatientsRangeAmountMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.CounterID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeByNumberOfPatientsRangeAmountMetadata.PropertyNames.CounterID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MinAmount, 1, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicFeeByNumberOfPatientsRangeAmountMetadata.PropertyNames.MinAmount;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.MaxAmount, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicFeeByNumberOfPatientsRangeAmountMetadata.PropertyNames.MaxAmount;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.Amount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeByNumberOfPatientsRangeAmountMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeByNumberOfPatientsRangeAmountMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeByNumberOfPatientsRangeAmountMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeByNumberOfPatientsRangeAmountMetadata Meta()
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
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CounterID = "CounterID";
			 public const string MinAmount = "MinAmount";
			 public const string MaxAmount = "MaxAmount";
			 public const string Amount = "Amount";
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
			lock (typeof(ParamedicFeeByNumberOfPatientsRangeAmountMetadata))
			{
				if(ParamedicFeeByNumberOfPatientsRangeAmountMetadata.mapDelegates == null)
				{
					ParamedicFeeByNumberOfPatientsRangeAmountMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeByNumberOfPatientsRangeAmountMetadata.meta == null)
				{
					ParamedicFeeByNumberOfPatientsRangeAmountMetadata.meta = new ParamedicFeeByNumberOfPatientsRangeAmountMetadata();
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
				meta.AddTypeMap("MinAmount", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("MaxAmount", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeByNumberOfPatientsRangeAmount";
				meta.Destination = "ParamedicFeeByNumberOfPatientsRangeAmount";
				
				meta.spInsert = "proc_ParamedicFeeByNumberOfPatientsRangeAmountInsert";				
				meta.spUpdate = "proc_ParamedicFeeByNumberOfPatientsRangeAmountUpdate";		
				meta.spDelete = "proc_ParamedicFeeByNumberOfPatientsRangeAmountDelete";
				meta.spLoadAll = "proc_ParamedicFeeByNumberOfPatientsRangeAmountLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeByNumberOfPatientsRangeAmountLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeByNumberOfPatientsRangeAmountMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
