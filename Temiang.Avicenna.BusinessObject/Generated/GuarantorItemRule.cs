/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/2/2018 3:05:49 PM
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
	abstract public class esGuarantorItemRuleCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorItemRuleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorItemRuleCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorItemRuleQuery query)
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
			this.InitQuery(query as esGuarantorItemRuleQuery);
		}
		#endregion
		
		virtual public GuarantorItemRule DetachEntity(GuarantorItemRule entity)
		{
			return base.DetachEntity(entity) as GuarantorItemRule;
		}
		
		virtual public GuarantorItemRule AttachEntity(GuarantorItemRule entity)
		{
			return base.AttachEntity(entity) as GuarantorItemRule;
		}
		
		virtual public void Combine(GuarantorItemRuleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorItemRule this[int index]
		{
			get
			{
				return base[index] as GuarantorItemRule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorItemRule);
		}
	}



	[Serializable]
	abstract public class esGuarantorItemRule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorItemRuleQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorItemRule()
		{

		}

		public esGuarantorItemRule(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID, System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID, System.String itemID)
		{
			esGuarantorItemRuleQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID, System.String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);			parms.Add("ItemID",itemID);
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
						case "SRGuarantorRuleType": this.str.SRGuarantorRuleType = (string)value; break;							
						case "AmountValue": this.str.AmountValue = (string)value; break;							
						case "IsValueInPercent": this.str.IsValueInPercent = (string)value; break;							
						case "IsInclude": this.str.IsInclude = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsToGuarantor": this.str.IsToGuarantor = (string)value; break;							
						case "OutpatientAmountValue": this.str.OutpatientAmountValue = (string)value; break;							
						case "EmergencyAmountValue": this.str.EmergencyAmountValue = (string)value; break;							
						case "IsByTariffComponent": this.str.IsByTariffComponent = (string)value; break;
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
						
						case "IsInclude":
						
							if (value == null || value is System.Boolean)
								this.IsInclude = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsToGuarantor":
						
							if (value == null || value is System.Boolean)
								this.IsToGuarantor = (System.Boolean?)value;
							break;
						
						case "OutpatientAmountValue":
						
							if (value == null || value is System.Decimal)
								this.OutpatientAmountValue = (System.Decimal?)value;
							break;
						
						case "EmergencyAmountValue":
						
							if (value == null || value is System.Decimal)
								this.EmergencyAmountValue = (System.Decimal?)value;
							break;
						
						case "IsByTariffComponent":
						
							if (value == null || value is System.Boolean)
								this.IsByTariffComponent = (System.Boolean?)value;
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
		/// Maps to GuarantorItemRule.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.SRGuarantorRuleType
		/// </summary>
		virtual public System.String SRGuarantorRuleType
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleMetadata.ColumnNames.SRGuarantorRuleType);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleMetadata.ColumnNames.SRGuarantorRuleType, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.AmountValue
		/// </summary>
		virtual public System.Decimal? AmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemRuleMetadata.ColumnNames.AmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemRuleMetadata.ColumnNames.AmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.IsValueInPercent
		/// </summary>
		virtual public System.Boolean? IsValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsValueInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.IsInclude
		/// </summary>
		virtual public System.Boolean? IsInclude
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsInclude);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsInclude, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorItemRuleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorItemRuleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorItemRuleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemRuleMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.IsToGuarantor
		/// </summary>
		virtual public System.Boolean? IsToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsToGuarantor);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsToGuarantor, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.OutpatientAmountValue
		/// </summary>
		virtual public System.Decimal? OutpatientAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemRuleMetadata.ColumnNames.OutpatientAmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemRuleMetadata.ColumnNames.OutpatientAmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.EmergencyAmountValue
		/// </summary>
		virtual public System.Decimal? EmergencyAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemRuleMetadata.ColumnNames.EmergencyAmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemRuleMetadata.ColumnNames.EmergencyAmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemRule.IsByTariffComponent
		/// </summary>
		virtual public System.Boolean? IsByTariffComponent
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsByTariffComponent);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemRuleMetadata.ColumnNames.IsByTariffComponent, value);
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
			public esStrings(esGuarantorItemRule entity)
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
				
			public System.String SRGuarantorRuleType
			{
				get
				{
					System.String data = entity.SRGuarantorRuleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGuarantorRuleType = null;
					else entity.SRGuarantorRuleType = Convert.ToString(value);
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
				
			public System.String IsInclude
			{
				get
				{
					System.Boolean? data = entity.IsInclude;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInclude = null;
					else entity.IsInclude = Convert.ToBoolean(value);
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
				
			public System.String IsToGuarantor
			{
				get
				{
					System.Boolean? data = entity.IsToGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsToGuarantor = null;
					else entity.IsToGuarantor = Convert.ToBoolean(value);
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
				
			public System.String IsByTariffComponent
			{
				get
				{
					System.Boolean? data = entity.IsByTariffComponent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsByTariffComponent = null;
					else entity.IsByTariffComponent = Convert.ToBoolean(value);
				}
			}
			

			private esGuarantorItemRule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorItemRuleQuery query)
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
				throw new Exception("esGuarantorItemRule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorItemRule : esGuarantorItemRule
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
	abstract public class esGuarantorItemRuleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemRuleMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGuarantorRuleType
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.SRGuarantorRuleType, esSystemType.String);
			}
		} 
		
		public esQueryItem AmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsValueInPercent
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsInclude
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.IsInclude, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsToGuarantor
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.IsToGuarantor, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem OutpatientAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.OutpatientAmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmergencyAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.EmergencyAmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsByTariffComponent
		{
			get
			{
				return new esQueryItem(this, GuarantorItemRuleMetadata.ColumnNames.IsByTariffComponent, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorItemRuleCollection")]
	public partial class GuarantorItemRuleCollection : esGuarantorItemRuleCollection, IEnumerable<GuarantorItemRule>
	{
		public GuarantorItemRuleCollection()
		{

		}
		
		public static implicit operator List<GuarantorItemRule>(GuarantorItemRuleCollection coll)
		{
			List<GuarantorItemRule> list = new List<GuarantorItemRule>();
			
			foreach (GuarantorItemRule emp in coll)
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
				return  GuarantorItemRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorItemRule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorItemRule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorItemRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorItemRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorItemRule AddNew()
		{
			GuarantorItemRule entity = base.AddNewEntity() as GuarantorItemRule;
			
			return entity;
		}

		public GuarantorItemRule FindByPrimaryKey(System.String guarantorID, System.String itemID)
		{
			return base.FindByPrimaryKey(guarantorID, itemID) as GuarantorItemRule;
		}


		#region IEnumerable<GuarantorItemRule> Members

		IEnumerator<GuarantorItemRule> IEnumerable<GuarantorItemRule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorItemRule;
			}
		}

		#endregion
		
		private GuarantorItemRuleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorItemRule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorItemRule ({GuarantorID},{ItemID})")]
	[Serializable]
	public partial class GuarantorItemRule : esGuarantorItemRule
	{
		public GuarantorItemRule()
		{

		}
	
		public GuarantorItemRule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorItemRuleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorItemRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorItemRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorItemRuleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorItemRuleQuery : esGuarantorItemRuleQuery
	{
		public GuarantorItemRuleQuery()
		{

		}		
		
		public GuarantorItemRuleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorItemRuleQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorItemRuleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorItemRuleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.SRGuarantorRuleType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.SRGuarantorRuleType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.AmountValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.AmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.IsValueInPercent, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.IsValueInPercent;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.IsInclude, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.IsInclude;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.IsToGuarantor, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.IsToGuarantor;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.OutpatientAmountValue, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.OutpatientAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.EmergencyAmountValue, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.EmergencyAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemRuleMetadata.ColumnNames.IsByTariffComponent, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemRuleMetadata.PropertyNames.IsByTariffComponent;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorItemRuleMetadata Meta()
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
			 public const string SRGuarantorRuleType = "SRGuarantorRuleType";
			 public const string AmountValue = "AmountValue";
			 public const string IsValueInPercent = "IsValueInPercent";
			 public const string IsInclude = "IsInclude";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsToGuarantor = "IsToGuarantor";
			 public const string OutpatientAmountValue = "OutpatientAmountValue";
			 public const string EmergencyAmountValue = "EmergencyAmountValue";
			 public const string IsByTariffComponent = "IsByTariffComponent";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string ItemID = "ItemID";
			 public const string SRGuarantorRuleType = "SRGuarantorRuleType";
			 public const string AmountValue = "AmountValue";
			 public const string IsValueInPercent = "IsValueInPercent";
			 public const string IsInclude = "IsInclude";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsToGuarantor = "IsToGuarantor";
			 public const string OutpatientAmountValue = "OutpatientAmountValue";
			 public const string EmergencyAmountValue = "EmergencyAmountValue";
			 public const string IsByTariffComponent = "IsByTariffComponent";
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
			lock (typeof(GuarantorItemRuleMetadata))
			{
				if(GuarantorItemRuleMetadata.mapDelegates == null)
				{
					GuarantorItemRuleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorItemRuleMetadata.meta == null)
				{
					GuarantorItemRuleMetadata.meta = new GuarantorItemRuleMetadata();
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
				meta.AddTypeMap("SRGuarantorRuleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsValueInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInclude", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OutpatientAmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EmergencyAmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsByTariffComponent", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "GuarantorItemRule";
				meta.Destination = "GuarantorItemRule";
				
				meta.spInsert = "proc_GuarantorItemRuleInsert";				
				meta.spUpdate = "proc_GuarantorItemRuleUpdate";		
				meta.spDelete = "proc_GuarantorItemRuleDelete";
				meta.spLoadAll = "proc_GuarantorItemRuleLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorItemRuleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorItemRuleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
