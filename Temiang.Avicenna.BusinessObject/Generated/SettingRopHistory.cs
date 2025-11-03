/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/16/2014 12:12:34 PM
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
	abstract public class esSettingRopHistoryCollection : esEntityCollectionWAuditLog
	{
		public esSettingRopHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "SettingRopHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esSettingRopHistoryQuery query)
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
			this.InitQuery(query as esSettingRopHistoryQuery);
		}
		#endregion
		
		virtual public SettingRopHistory DetachEntity(SettingRopHistory entity)
		{
			return base.DetachEntity(entity) as SettingRopHistory;
		}
		
		virtual public SettingRopHistory AttachEntity(SettingRopHistory entity)
		{
			return base.AttachEntity(entity) as SettingRopHistory;
		}
		
		virtual public void Combine(SettingRopHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public SettingRopHistory this[int index]
		{
			get
			{
				return base[index] as SettingRopHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SettingRopHistory);
		}
	}



	[Serializable]
	abstract public class esSettingRopHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSettingRopHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esSettingRopHistory()
		{

		}

		public esSettingRopHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Guid ropHistoryID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ropHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(ropHistoryID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Guid ropHistoryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ropHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(ropHistoryID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Guid ropHistoryID)
		{
			esSettingRopHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RopHistoryID == ropHistoryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Guid ropHistoryID)
		{
			esParameters parms = new esParameters();
			parms.Add("RopHistoryID",ropHistoryID);
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
						case "RopHistoryID": this.str.RopHistoryID = (string)value; break;							
						case "RopHistoryDateTime": this.str.RopHistoryDateTime = (string)value; break;							
						case "LocationID": this.str.LocationID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "FromMinimum": this.str.FromMinimum = (string)value; break;							
						case "ToMinimum": this.str.ToMinimum = (string)value; break;							
						case "FromMaximum": this.str.FromMaximum = (string)value; break;							
						case "ToMaximum": this.str.ToMaximum = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RopHistoryID":
						
							if (value == null || value is System.Guid)
								this.RopHistoryID = (System.Guid?)value;
							break;
						
						case "RopHistoryDateTime":
						
							if (value == null || value is System.DateTime)
								this.RopHistoryDateTime = (System.DateTime?)value;
							break;
						
						case "FromMinimum":
						
							if (value == null || value is System.Decimal)
								this.FromMinimum = (System.Decimal?)value;
							break;
						
						case "ToMinimum":
						
							if (value == null || value is System.Decimal)
								this.ToMinimum = (System.Decimal?)value;
							break;
						
						case "FromMaximum":
						
							if (value == null || value is System.Decimal)
								this.FromMaximum = (System.Decimal?)value;
							break;
						
						case "ToMaximum":
						
							if (value == null || value is System.Decimal)
								this.ToMaximum = (System.Decimal?)value;
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
		/// Maps to SettingRopHistory.RopHistoryID
		/// </summary>
		virtual public System.Guid? RopHistoryID
		{
			get
			{
				return base.GetSystemGuid(SettingRopHistoryMetadata.ColumnNames.RopHistoryID);
			}
			
			set
			{
				base.SetSystemGuid(SettingRopHistoryMetadata.ColumnNames.RopHistoryID, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.RopHistoryDateTime
		/// </summary>
		virtual public System.DateTime? RopHistoryDateTime
		{
			get
			{
				return base.GetSystemDateTime(SettingRopHistoryMetadata.ColumnNames.RopHistoryDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SettingRopHistoryMetadata.ColumnNames.RopHistoryDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(SettingRopHistoryMetadata.ColumnNames.LocationID);
			}
			
			set
			{
				base.SetSystemString(SettingRopHistoryMetadata.ColumnNames.LocationID, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(SettingRopHistoryMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(SettingRopHistoryMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.FromMinimum
		/// </summary>
		virtual public System.Decimal? FromMinimum
		{
			get
			{
				return base.GetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.FromMinimum);
			}
			
			set
			{
				base.SetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.FromMinimum, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.ToMinimum
		/// </summary>
		virtual public System.Decimal? ToMinimum
		{
			get
			{
				return base.GetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.ToMinimum);
			}
			
			set
			{
				base.SetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.ToMinimum, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.FromMaximum
		/// </summary>
		virtual public System.Decimal? FromMaximum
		{
			get
			{
				return base.GetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.FromMaximum);
			}
			
			set
			{
				base.SetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.FromMaximum, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.ToMaximum
		/// </summary>
		virtual public System.Decimal? ToMaximum
		{
			get
			{
				return base.GetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.ToMaximum);
			}
			
			set
			{
				base.SetSystemDecimal(SettingRopHistoryMetadata.ColumnNames.ToMaximum, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SettingRopHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(SettingRopHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to SettingRopHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SettingRopHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(SettingRopHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esSettingRopHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RopHistoryID
			{
				get
				{
					System.Guid? data = entity.RopHistoryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RopHistoryID = null;
					else entity.RopHistoryID = new Guid(value);
				}
			}
				
			public System.String RopHistoryDateTime
			{
				get
				{
					System.DateTime? data = entity.RopHistoryDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RopHistoryDateTime = null;
					else entity.RopHistoryDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
			}
				
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
				
			public System.String FromMinimum
			{
				get
				{
					System.Decimal? data = entity.FromMinimum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromMinimum = null;
					else entity.FromMinimum = Convert.ToDecimal(value);
				}
			}
				
			public System.String ToMinimum
			{
				get
				{
					System.Decimal? data = entity.ToMinimum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToMinimum = null;
					else entity.ToMinimum = Convert.ToDecimal(value);
				}
			}
				
			public System.String FromMaximum
			{
				get
				{
					System.Decimal? data = entity.FromMaximum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromMaximum = null;
					else entity.FromMaximum = Convert.ToDecimal(value);
				}
			}
				
			public System.String ToMaximum
			{
				get
				{
					System.Decimal? data = entity.ToMaximum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToMaximum = null;
					else entity.ToMaximum = Convert.ToDecimal(value);
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
			

			private esSettingRopHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSettingRopHistoryQuery query)
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
				throw new Exception("esSettingRopHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class SettingRopHistory : esSettingRopHistory
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
	abstract public class esSettingRopHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SettingRopHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RopHistoryID
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.RopHistoryID, esSystemType.Guid);
			}
		} 
		
		public esQueryItem RopHistoryDateTime
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.RopHistoryDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem FromMinimum
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.FromMinimum, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ToMinimum
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.ToMinimum, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FromMaximum
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.FromMaximum, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ToMaximum
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.ToMaximum, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SettingRopHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SettingRopHistoryCollection")]
	public partial class SettingRopHistoryCollection : esSettingRopHistoryCollection, IEnumerable<SettingRopHistory>
	{
		public SettingRopHistoryCollection()
		{

		}
		
		public static implicit operator List<SettingRopHistory>(SettingRopHistoryCollection coll)
		{
			List<SettingRopHistory> list = new List<SettingRopHistory>();
			
			foreach (SettingRopHistory emp in coll)
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
				return  SettingRopHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SettingRopHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SettingRopHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SettingRopHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public SettingRopHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SettingRopHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(SettingRopHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public SettingRopHistory AddNew()
		{
			SettingRopHistory entity = base.AddNewEntity() as SettingRopHistory;
			
			return entity;
		}

		public SettingRopHistory FindByPrimaryKey(System.Guid ropHistoryID)
		{
			return base.FindByPrimaryKey(ropHistoryID) as SettingRopHistory;
		}


		#region IEnumerable<SettingRopHistory> Members

		IEnumerator<SettingRopHistory> IEnumerable<SettingRopHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as SettingRopHistory;
			}
		}

		#endregion
		
		private SettingRopHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SettingRopHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("SettingRopHistory ({RopHistoryID})")]
	[Serializable]
	public partial class SettingRopHistory : esSettingRopHistory
	{
		public SettingRopHistory()
		{

		}
	
		public SettingRopHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SettingRopHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esSettingRopHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SettingRopHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public SettingRopHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SettingRopHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(SettingRopHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private SettingRopHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class SettingRopHistoryQuery : esSettingRopHistoryQuery
	{
		public SettingRopHistoryQuery()
		{

		}		
		
		public SettingRopHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "SettingRopHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class SettingRopHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SettingRopHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.RopHistoryID, 0, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.RopHistoryID;
			c.IsInPrimaryKey = true;
			c.HasDefault = true;
			c.Default = @"(newid())";
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.RopHistoryDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.RopHistoryDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.LocationID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.LocationID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.FromMinimum, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.FromMinimum;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.ToMinimum, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.ToMinimum;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.FromMaximum, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.FromMaximum;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.ToMaximum, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.ToMaximum;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(SettingRopHistoryMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SettingRopHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public SettingRopHistoryMetadata Meta()
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
			 public const string RopHistoryID = "RopHistoryID";
			 public const string RopHistoryDateTime = "RopHistoryDateTime";
			 public const string LocationID = "LocationID";
			 public const string ItemID = "ItemID";
			 public const string FromMinimum = "FromMinimum";
			 public const string ToMinimum = "ToMinimum";
			 public const string FromMaximum = "FromMaximum";
			 public const string ToMaximum = "ToMaximum";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RopHistoryID = "RopHistoryID";
			 public const string RopHistoryDateTime = "RopHistoryDateTime";
			 public const string LocationID = "LocationID";
			 public const string ItemID = "ItemID";
			 public const string FromMinimum = "FromMinimum";
			 public const string ToMinimum = "ToMinimum";
			 public const string FromMaximum = "FromMaximum";
			 public const string ToMaximum = "ToMaximum";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(SettingRopHistoryMetadata))
			{
				if(SettingRopHistoryMetadata.mapDelegates == null)
				{
					SettingRopHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SettingRopHistoryMetadata.meta == null)
				{
					SettingRopHistoryMetadata.meta = new SettingRopHistoryMetadata();
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
				

				meta.AddTypeMap("RopHistoryID", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("RopHistoryDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromMinimum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ToMinimum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FromMaximum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ToMaximum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "SettingRopHistory";
				meta.Destination = "SettingRopHistory";
				
				meta.spInsert = "proc_SettingRopHistoryInsert";				
				meta.spUpdate = "proc_SettingRopHistoryUpdate";		
				meta.spDelete = "proc_SettingRopHistoryDelete";
				meta.spLoadAll = "proc_SettingRopHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_SettingRopHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SettingRopHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
