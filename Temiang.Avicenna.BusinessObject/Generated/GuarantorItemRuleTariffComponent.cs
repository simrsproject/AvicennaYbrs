/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/2/2018 3:05:50 PM
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
	abstract public class esGuarantorItemRuleTariffComponentCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorItemRuleTariffComponentCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorItemRuleTariffComponentCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorItemRuleTariffComponentQuery query)
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
			this.InitQuery(query as esGuarantorItemRuleTariffComponentQuery);
		}
		#endregion
		
		virtual public GuarantorItemRuleTariffComponent DetachEntity(GuarantorItemRuleTariffComponent entity)
		{
			return base.DetachEntity(entity) as GuarantorItemRuleTariffComponent;
		}
		
		virtual public GuarantorItemRuleTariffComponent AttachEntity(GuarantorItemRuleTariffComponent entity)
		{
			return base.AttachEntity(entity) as GuarantorItemRuleTariffComponent;
		}
		
		virtual public void Combine(GuarantorItemRuleTariffComponentCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorItemRuleTariffComponent this[int index]
		{
			get
			{
				return base[index] as GuarantorItemRuleTariffComponent;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorItemRuleTariffComponent);
		}
	}



	[Serializable]
	abstract public class esGuarantorItemRuleTariffComponent : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorItemRuleTariffComponentQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorItemRuleTariffComponent()
		{

		}

		public esGuarantorItemRuleTariffComponent(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID, System.String itemID, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, itemID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, itemID, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID, System.String itemID, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, itemID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, itemID, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID, System.String itemID, System.String tariffComponentID)
		{
			esGuarantorItemRuleTariffComponentQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.ItemID == itemID, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID, System.String itemID, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);			parms.Add("ItemID",itemID);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "AmountValue": this.str.AmountValue = (string)value; break;							
						case "IsValueInPercent": this.str.IsValueInPercent = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "OutpatientAmountValue": this.str.OutpatientAmountValue = (string)value; break;							
						case "EmergencyAmountValue": this.str.EmergencyAmountValue = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AmountValue":
						
							if (value == null || value is System.Decimal)
								this.AmountValue = (System.Decimal?)value;
							break;
						
						case "IsValueInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsValueInPercent = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "OutpatientAmountValue":
						
							if (value == null || value is System.Decimal)
								this.OutpatientAmountValue = (System.Decimal?)value;
							break;
						
						case "EmergencyAmountValue":
						
							if (value == null || value is System.Decimal)
								this.EmergencyAmountValue = (System.Decimal?)value;
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
		/// Maps to GuarantorItemRuleTariffComponent.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.AmountValue
		/// </summary>
		virtual public System.Decimal? AmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemRuleTariffComponentMetadata.ColumnNames.AmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemRuleTariffComponentMetadata.ColumnNames.AmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.IsValueInPercent
		/// </summary>
		virtual public System.Boolean? IsValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemRuleTariffComponentMetadata.ColumnNames.IsValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemRuleTariffComponentMetadata.ColumnNames.IsValueInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.OutpatientAmountValue
		/// </summary>
		virtual public System.Decimal? OutpatientAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemRuleTariffComponentMetadata.ColumnNames.OutpatientAmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemRuleTariffComponentMetadata.ColumnNames.OutpatientAmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRuleTariffComponent.EmergencyAmountValue
		/// </summary>
		virtual public System.Decimal? EmergencyAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemRuleTariffComponentMetadata.ColumnNames.EmergencyAmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemRuleTariffComponentMetadata.ColumnNames.EmergencyAmountValue, value);
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
			public esStrings(esGuarantorItemRuleTariffComponent entity)
			{
				this.entity = entity;
			}
			
	
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
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
				
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
				
			public System.String AmountValue
			{
				get
				{
					System.Decimal? data = entity.AmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountValue = null;
					else entity.AmountValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsValueInPercent
			{
				get
				{
					System.Boolean? data = entity.IsValueInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsValueInPercent = null;
					else entity.IsValueInPercent = Convert.ToBoolean(value);
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
				
			public System.String OutpatientAmountValue
			{
				get
				{
					System.Decimal? data = entity.OutpatientAmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OutpatientAmountValue = null;
					else entity.OutpatientAmountValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String EmergencyAmountValue
			{
				get
				{
					System.Decimal? data = entity.EmergencyAmountValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmergencyAmountValue = null;
					else entity.EmergencyAmountValue = Convert.ToDecimal(value);
				}
			}
			

			private esGuarantorItemRuleTariffComponent entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorItemRuleTariffComponentQuery query)
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
				throw new Exception("esGuarantorItemRuleTariffComponent can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorItemRuleTariffComponent : esGuarantorItemRuleTariffComponent
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
	abstract public class esGuarantorItemRuleTariffComponentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemRuleTariffComponentMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem AmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsValueInPercent
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem OutpatientAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.OutpatientAmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmergencyAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleTariffComponentMetadata.ColumnNames.EmergencyAmountValue, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorItemRuleTariffComponentCollection")]
	public partial class GuarantorItemRuleTariffComponentCollection : esGuarantorItemRuleTariffComponentCollection, IEnumerable<GuarantorItemRuleTariffComponent>
	{
		public GuarantorItemRuleTariffComponentCollection()
		{

		}
		
		public static implicit operator List<GuarantorItemRuleTariffComponent>(GuarantorItemRuleTariffComponentCollection coll)
		{
			List<GuarantorItemRuleTariffComponent> list = new List<GuarantorItemRuleTariffComponent>();
			
			foreach (GuarantorItemRuleTariffComponent emp in coll)
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
				return  GuarantorItemRuleTariffComponentMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemRuleTariffComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorItemRuleTariffComponent(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorItemRuleTariffComponent();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorItemRuleTariffComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemRuleTariffComponentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorItemRuleTariffComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorItemRuleTariffComponent AddNew()
		{
			GuarantorItemRuleTariffComponent entity = base.AddNewEntity() as GuarantorItemRuleTariffComponent;
			
			return entity;
		}

		public GuarantorItemRuleTariffComponent FindByPrimaryKey(System.String guarantorID, System.String itemID, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(guarantorID, itemID, tariffComponentID) as GuarantorItemRuleTariffComponent;
		}


		#region IEnumerable<GuarantorItemRuleTariffComponent> Members

		IEnumerator<GuarantorItemRuleTariffComponent> IEnumerable<GuarantorItemRuleTariffComponent>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorItemRuleTariffComponent;
			}
		}

		#endregion
		
		private GuarantorItemRuleTariffComponentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorItemRuleTariffComponent' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorItemRuleTariffComponent ({GuarantorID},{ItemID},{TariffComponentID})")]
	[Serializable]
	public partial class GuarantorItemRuleTariffComponent : esGuarantorItemRuleTariffComponent
	{
		public GuarantorItemRuleTariffComponent()
		{

		}
	
		public GuarantorItemRuleTariffComponent(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemRuleTariffComponentMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorItemRuleTariffComponentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemRuleTariffComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorItemRuleTariffComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemRuleTariffComponentQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorItemRuleTariffComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorItemRuleTariffComponentQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorItemRuleTariffComponentQuery : esGuarantorItemRuleTariffComponentQuery
	{
		public GuarantorItemRuleTariffComponentQuery()
		{

		}		
		
		public GuarantorItemRuleTariffComponentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorItemRuleTariffComponentQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorItemRuleTariffComponentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorItemRuleTariffComponentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.AmountValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.AmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.IsValueInPercent, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.IsValueInPercent;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.OutpatientAmountValue, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.OutpatientAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleTariffComponentMetadata.ColumnNames.EmergencyAmountValue, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemRuleTariffComponentMetadata.PropertyNames.EmergencyAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorItemRuleTariffComponentMetadata Meta()
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
			 public const string GuarantorID = "GuarantorID";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string AmountValue = "AmountValue";
			 public const string IsValueInPercent = "IsValueInPercent";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string OutpatientAmountValue = "OutpatientAmountValue";
			 public const string EmergencyAmountValue = "EmergencyAmountValue";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string AmountValue = "AmountValue";
			 public const string IsValueInPercent = "IsValueInPercent";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string OutpatientAmountValue = "OutpatientAmountValue";
			 public const string EmergencyAmountValue = "EmergencyAmountValue";
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
			lock (typeof(GuarantorItemRuleTariffComponentMetadata))
			{
				if(GuarantorItemRuleTariffComponentMetadata.mapDelegates == null)
				{
					GuarantorItemRuleTariffComponentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorItemRuleTariffComponentMetadata.meta == null)
				{
					GuarantorItemRuleTariffComponentMetadata.meta = new GuarantorItemRuleTariffComponentMetadata();
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
				

				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OutpatientAmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EmergencyAmountValue", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "GuarantorItemRuleTariffComponent";
				meta.Destination = "GuarantorItemRuleTariffComponent";
				
				meta.spInsert = "proc_GuarantorItemRuleTariffComponentInsert";				
				meta.spUpdate = "proc_GuarantorItemRuleTariffComponentUpdate";		
				meta.spDelete = "proc_GuarantorItemRuleTariffComponentDelete";
				meta.spLoadAll = "proc_GuarantorItemRuleTariffComponentLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorItemRuleTariffComponentLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorItemRuleTariffComponentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
