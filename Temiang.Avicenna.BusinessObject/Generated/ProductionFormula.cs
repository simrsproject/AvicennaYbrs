/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:24 PM
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
	abstract public class esProductionFormulaCollection : esEntityCollectionWAuditLog
	{
		public esProductionFormulaCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ProductionFormulaCollection";
		}

		#region Query Logic
		protected void InitQuery(esProductionFormulaQuery query)
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
			this.InitQuery(query as esProductionFormulaQuery);
		}
		#endregion
		
		virtual public ProductionFormula DetachEntity(ProductionFormula entity)
		{
			return base.DetachEntity(entity) as ProductionFormula;
		}
		
		virtual public ProductionFormula AttachEntity(ProductionFormula entity)
		{
			return base.AttachEntity(entity) as ProductionFormula;
		}
		
		virtual public void Combine(ProductionFormulaCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ProductionFormula this[int index]
		{
			get
			{
				return base[index] as ProductionFormula;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ProductionFormula);
		}
	}



	[Serializable]
	abstract public class esProductionFormula : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esProductionFormulaQuery GetDynamicQuery()
		{
			return null;
		}

		public esProductionFormula()
		{

		}

		public esProductionFormula(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String formulaID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(formulaID);
			else
				return LoadByPrimaryKeyStoredProcedure(formulaID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String formulaID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(formulaID);
			else
				return LoadByPrimaryKeyStoredProcedure(formulaID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String formulaID)
		{
			esProductionFormulaQuery query = this.GetDynamicQuery();
			query.Where(query.FormulaID == formulaID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String formulaID)
		{
			esParameters parms = new esParameters();
			parms.Add("FormulaID",formulaID);
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
						case "FormulaID": this.str.FormulaID = (string)value; break;							
						case "FormulaName": this.str.FormulaName = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "Qty": this.str.Qty = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsCostInPercentage": this.str.IsCostInPercentage = (string)value; break;							
						case "CostAmount": this.str.CostAmount = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Qty":
						
							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						
						case "IsCostInPercentage":
						
							if (value == null || value is System.Boolean)
								this.IsCostInPercentage = (System.Boolean?)value;
							break;
						
						case "CostAmount":
						
							if (value == null || value is System.Decimal)
								this.CostAmount = (System.Decimal?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to ProductionFormula.FormulaID
		/// </summary>
		virtual public System.String FormulaID
		{
			get
			{
				return base.GetSystemString(ProductionFormulaMetadata.ColumnNames.FormulaID);
			}
			
			set
			{
				base.SetSystemString(ProductionFormulaMetadata.ColumnNames.FormulaID, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.FormulaName
		/// </summary>
		virtual public System.String FormulaName
		{
			get
			{
				return base.GetSystemString(ProductionFormulaMetadata.ColumnNames.FormulaName);
			}
			
			set
			{
				base.SetSystemString(ProductionFormulaMetadata.ColumnNames.FormulaName, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ProductionFormulaMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ProductionFormulaMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(ProductionFormulaMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(ProductionFormulaMetadata.ColumnNames.Qty, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ProductionFormulaMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ProductionFormulaMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.IsCostInPercentage
		/// </summary>
		virtual public System.Boolean? IsCostInPercentage
		{
			get
			{
				return base.GetSystemBoolean(ProductionFormulaMetadata.ColumnNames.IsCostInPercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ProductionFormulaMetadata.ColumnNames.IsCostInPercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.CostAmount
		/// </summary>
		virtual public System.Decimal? CostAmount
		{
			get
			{
				return base.GetSystemDecimal(ProductionFormulaMetadata.ColumnNames.CostAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ProductionFormulaMetadata.ColumnNames.CostAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ProductionFormulaMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ProductionFormulaMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProductionFormulaMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ProductionFormulaMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ProductionFormula.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ProductionFormulaMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ProductionFormulaMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esProductionFormula entity)
			{
				this.entity = entity;
			}
			
	
			public System.String FormulaID
			{
				get
				{
					System.String data = entity.FormulaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaID = null;
					else entity.FormulaID = Convert.ToString(value);
				}
			}
				
			public System.String FormulaName
			{
				get
				{
					System.String data = entity.FormulaName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormulaName = null;
					else entity.FormulaName = Convert.ToString(value);
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
				
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
				
			public System.String IsCostInPercentage
			{
				get
				{
					System.Boolean? data = entity.IsCostInPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCostInPercentage = null;
					else entity.IsCostInPercentage = Convert.ToBoolean(value);
				}
			}
				
			public System.String CostAmount
			{
				get
				{
					System.Decimal? data = entity.CostAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostAmount = null;
					else entity.CostAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esProductionFormula entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esProductionFormulaQuery query)
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
				throw new Exception("esProductionFormula can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ProductionFormula : esProductionFormula
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
	abstract public class esProductionFormulaQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductionFormulaMetadata.Meta();
			}
		}	
		

		public esQueryItem FormulaID
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.FormulaID, esSystemType.String);
			}
		} 
		
		public esQueryItem FormulaName
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.FormulaName, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsCostInPercentage
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.IsCostInPercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CostAmount
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.CostAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ProductionFormulaMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ProductionFormulaCollection")]
	public partial class ProductionFormulaCollection : esProductionFormulaCollection, IEnumerable<ProductionFormula>
	{
		public ProductionFormulaCollection()
		{

		}
		
		public static implicit operator List<ProductionFormula>(ProductionFormulaCollection coll)
		{
			List<ProductionFormula> list = new List<ProductionFormula>();
			
			foreach (ProductionFormula emp in coll)
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
				return  ProductionFormulaMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductionFormulaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ProductionFormula(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ProductionFormula();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ProductionFormulaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductionFormulaQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ProductionFormulaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ProductionFormula AddNew()
		{
			ProductionFormula entity = base.AddNewEntity() as ProductionFormula;
			
			return entity;
		}

		public ProductionFormula FindByPrimaryKey(System.String formulaID)
		{
			return base.FindByPrimaryKey(formulaID) as ProductionFormula;
		}


		#region IEnumerable<ProductionFormula> Members

		IEnumerator<ProductionFormula> IEnumerable<ProductionFormula>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ProductionFormula;
			}
		}

		#endregion
		
		private ProductionFormulaQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ProductionFormula' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ProductionFormula ({FormulaID})")]
	[Serializable]
	public partial class ProductionFormula : esProductionFormula
	{
		public ProductionFormula()
		{

		}
	
		public ProductionFormula(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductionFormulaMetadata.Meta();
			}
		}
		
		
		
		override protected esProductionFormulaQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductionFormulaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ProductionFormulaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductionFormulaQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ProductionFormulaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ProductionFormulaQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ProductionFormulaQuery : esProductionFormulaQuery
	{
		public ProductionFormulaQuery()
		{

		}		
		
		public ProductionFormulaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ProductionFormulaQuery";
        }
		
			
	}


	[Serializable]
	public partial class ProductionFormulaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductionFormulaMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.FormulaID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.FormulaID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.FormulaName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.FormulaName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.IsCostInPercentage, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.IsCostInPercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.CostAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.CostAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.IsActive, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProductionFormulaMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductionFormulaMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductionFormulaMetadata Meta()
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
			 public const string FormulaID = "FormulaID";
			 public const string FormulaName = "FormulaName";
			 public const string ItemID = "ItemID";
			 public const string Qty = "Qty";
			 public const string Notes = "Notes";
			 public const string IsCostInPercentage = "IsCostInPercentage";
			 public const string CostAmount = "CostAmount";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string FormulaID = "FormulaID";
			 public const string FormulaName = "FormulaName";
			 public const string ItemID = "ItemID";
			 public const string Qty = "Qty";
			 public const string Notes = "Notes";
			 public const string IsCostInPercentage = "IsCostInPercentage";
			 public const string CostAmount = "CostAmount";
			 public const string IsActive = "IsActive";
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
			lock (typeof(ProductionFormulaMetadata))
			{
				if(ProductionFormulaMetadata.mapDelegates == null)
				{
					ProductionFormulaMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductionFormulaMetadata.meta == null)
				{
					ProductionFormulaMetadata.meta = new ProductionFormulaMetadata();
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
				

				meta.AddTypeMap("FormulaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormulaName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCostInPercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CostAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ProductionFormula";
				meta.Destination = "ProductionFormula";
				
				meta.spInsert = "proc_ProductionFormulaInsert";				
				meta.spUpdate = "proc_ProductionFormulaUpdate";		
				meta.spDelete = "proc_ProductionFormulaDelete";
				meta.spLoadAll = "proc_ProductionFormulaLoadAll";
				meta.spLoadByPrimaryKey = "proc_ProductionFormulaLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductionFormulaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
