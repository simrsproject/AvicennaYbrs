/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/18/2014 4:38:26 PM
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
	abstract public class esGuarantorItemPrescriptionRuleCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorItemPrescriptionRuleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorItemPrescriptionRuleCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorItemPrescriptionRuleQuery query)
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
			this.InitQuery(query as esGuarantorItemPrescriptionRuleQuery);
		}
		#endregion
		
		virtual public GuarantorItemPrescriptionRule DetachEntity(GuarantorItemPrescriptionRule entity)
		{
			return base.DetachEntity(entity) as GuarantorItemPrescriptionRule;
		}
		
		virtual public GuarantorItemPrescriptionRule AttachEntity(GuarantorItemPrescriptionRule entity)
		{
			return base.AttachEntity(entity) as GuarantorItemPrescriptionRule;
		}
		
		virtual public void Combine(GuarantorItemPrescriptionRuleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorItemPrescriptionRule this[int index]
		{
			get
			{
				return base[index] as GuarantorItemPrescriptionRule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorItemPrescriptionRule);
		}
	}



	[Serializable]
	abstract public class esGuarantorItemPrescriptionRule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorItemPrescriptionRuleQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorItemPrescriptionRule()
		{

		}

		public esGuarantorItemPrescriptionRule(DataRow row)
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
			esGuarantorItemPrescriptionRuleQuery query = this.GetDynamicQuery();
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
						case "IsToGuarantor": this.str.IsToGuarantor = (string)value; break;							
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
						
						case "IsInclude":
						
							if (value == null || value is System.Boolean)
								this.IsInclude = (System.Boolean?)value;
							break;
						
						case "IsToGuarantor":
						
							if (value == null || value is System.Boolean)
								this.IsToGuarantor = (System.Boolean?)value;
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
		/// Maps to GuarantorItemPrescriptionRule.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.SRGuarantorRuleType
		/// </summary>
		virtual public System.String SRGuarantorRuleType
		{
			get
			{
				return base.GetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.SRGuarantorRuleType);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.SRGuarantorRuleType, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.AmountValue
		/// </summary>
		virtual public System.Decimal? AmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemPrescriptionRuleMetadata.ColumnNames.AmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemPrescriptionRuleMetadata.ColumnNames.AmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.IsValueInPercent
		/// </summary>
		virtual public System.Boolean? IsValueInPercent
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsValueInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsValueInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.IsInclude
		/// </summary>
		virtual public System.Boolean? IsInclude
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsInclude);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsInclude, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.IsToGuarantor
		/// </summary>
		virtual public System.Boolean? IsToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsToGuarantor);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsToGuarantor, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.OutpatientAmountValue
		/// </summary>
		virtual public System.Decimal? OutpatientAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemPrescriptionRuleMetadata.ColumnNames.OutpatientAmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemPrescriptionRuleMetadata.ColumnNames.OutpatientAmountValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorItemPrescriptionRule.EmergencyAmountValue
		/// </summary>
		virtual public System.Decimal? EmergencyAmountValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorItemPrescriptionRuleMetadata.ColumnNames.EmergencyAmountValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorItemPrescriptionRuleMetadata.ColumnNames.EmergencyAmountValue, value);
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
			public esStrings(esGuarantorItemPrescriptionRule entity)
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
			

			private esGuarantorItemPrescriptionRule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorItemPrescriptionRuleQuery query)
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
				throw new Exception("esGuarantorItemPrescriptionRule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esGuarantorItemPrescriptionRuleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemPrescriptionRuleMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGuarantorRuleType
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.SRGuarantorRuleType, esSystemType.String);
			}
		} 
		
		public esQueryItem AmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsValueInPercent
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsInclude
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsInclude, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsToGuarantor
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsToGuarantor, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem OutpatientAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.OutpatientAmountValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem EmergencyAmountValue
		{
			get
			{
				return new esQueryItem(this, GuarantorItemPrescriptionRuleMetadata.ColumnNames.EmergencyAmountValue, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorItemPrescriptionRuleCollection")]
	public partial class GuarantorItemPrescriptionRuleCollection : esGuarantorItemPrescriptionRuleCollection, IEnumerable<GuarantorItemPrescriptionRule>
	{
		public GuarantorItemPrescriptionRuleCollection()
		{

		}
		
		public static implicit operator List<GuarantorItemPrescriptionRule>(GuarantorItemPrescriptionRuleCollection coll)
		{
			List<GuarantorItemPrescriptionRule> list = new List<GuarantorItemPrescriptionRule>();
			
			foreach (GuarantorItemPrescriptionRule emp in coll)
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
				return  GuarantorItemPrescriptionRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemPrescriptionRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorItemPrescriptionRule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorItemPrescriptionRule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorItemPrescriptionRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemPrescriptionRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorItemPrescriptionRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorItemPrescriptionRule AddNew()
		{
			GuarantorItemPrescriptionRule entity = base.AddNewEntity() as GuarantorItemPrescriptionRule;
			
			return entity;
		}

		public GuarantorItemPrescriptionRule FindByPrimaryKey(System.String guarantorID, System.String itemID)
		{
			return base.FindByPrimaryKey(guarantorID, itemID) as GuarantorItemPrescriptionRule;
		}


		#region IEnumerable<GuarantorItemPrescriptionRule> Members

		IEnumerator<GuarantorItemPrescriptionRule> IEnumerable<GuarantorItemPrescriptionRule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorItemPrescriptionRule;
			}
		}

		#endregion
		
		private GuarantorItemPrescriptionRuleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorItemPrescriptionRule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorItemPrescriptionRule ({GuarantorID},{ItemID})")]
	[Serializable]
	public partial class GuarantorItemPrescriptionRule : esGuarantorItemPrescriptionRule
	{
		public GuarantorItemPrescriptionRule()
		{

		}
	
		public GuarantorItemPrescriptionRule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorItemPrescriptionRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorItemPrescriptionRuleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorItemPrescriptionRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorItemPrescriptionRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorItemPrescriptionRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorItemPrescriptionRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorItemPrescriptionRuleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorItemPrescriptionRuleQuery : esGuarantorItemPrescriptionRuleQuery
	{
		public GuarantorItemPrescriptionRuleQuery()
		{

		}		
		
		public GuarantorItemPrescriptionRuleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorItemPrescriptionRuleQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorItemPrescriptionRuleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorItemPrescriptionRuleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.SRGuarantorRuleType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.SRGuarantorRuleType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.AmountValue, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.AmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsValueInPercent, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.IsValueInPercent;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsInclude, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.IsInclude;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.IsToGuarantor, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.IsToGuarantor;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.OutpatientAmountValue, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.OutpatientAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorItemPrescriptionRuleMetadata.ColumnNames.EmergencyAmountValue, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorItemPrescriptionRuleMetadata.PropertyNames.EmergencyAmountValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorItemPrescriptionRuleMetadata Meta()
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
			 public const string IsToGuarantor = "IsToGuarantor";
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
			 public const string SRGuarantorRuleType = "SRGuarantorRuleType";
			 public const string AmountValue = "AmountValue";
			 public const string IsValueInPercent = "IsValueInPercent";
			 public const string IsInclude = "IsInclude";
			 public const string IsToGuarantor = "IsToGuarantor";
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
			lock (typeof(GuarantorItemPrescriptionRuleMetadata))
			{
				if(GuarantorItemPrescriptionRuleMetadata.mapDelegates == null)
				{
					GuarantorItemPrescriptionRuleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorItemPrescriptionRuleMetadata.meta == null)
				{
					GuarantorItemPrescriptionRuleMetadata.meta = new GuarantorItemPrescriptionRuleMetadata();
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
				meta.AddTypeMap("IsToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OutpatientAmountValue", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("EmergencyAmountValue", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "GuarantorItemPrescriptionRule";
				meta.Destination = "GuarantorItemPrescriptionRule";
				
				meta.spInsert = "proc_GuarantorItemPrescriptionRuleInsert";				
				meta.spUpdate = "proc_GuarantorItemPrescriptionRuleUpdate";		
				meta.spDelete = "proc_GuarantorItemPrescriptionRuleDelete";
				meta.spLoadAll = "proc_GuarantorItemPrescriptionRuleLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorItemPrescriptionRuleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorItemPrescriptionRuleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
