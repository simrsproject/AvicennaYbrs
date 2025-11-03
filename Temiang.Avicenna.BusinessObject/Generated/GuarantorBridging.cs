/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/12/2018 6:41:53 AM
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
	abstract public class esGuarantorBridgingCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorBridgingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorBridgingCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorBridgingQuery query)
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
			this.InitQuery(query as esGuarantorBridgingQuery);
		}
		#endregion
		
		virtual public GuarantorBridging DetachEntity(GuarantorBridging entity)
		{
			return base.DetachEntity(entity) as GuarantorBridging;
		}
		
		virtual public GuarantorBridging AttachEntity(GuarantorBridging entity)
		{
			return base.AttachEntity(entity) as GuarantorBridging;
		}
		
		virtual public void Combine(GuarantorBridgingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorBridging this[int index]
		{
			get
			{
				return base[index] as GuarantorBridging;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorBridging);
		}
	}



	[Serializable]
	abstract public class esGuarantorBridging : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorBridgingQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorBridging()
		{

		}

		public esGuarantorBridging(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID, System.String sRBridgingType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, sRBridgingType);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, sRBridgingType);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID, System.String sRBridgingType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, sRBridgingType);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, sRBridgingType);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID, System.String sRBridgingType)
		{
			esGuarantorBridgingQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.SRBridgingType == sRBridgingType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID, System.String sRBridgingType)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);			parms.Add("SRBridgingType",sRBridgingType);
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
						case "SRBridgingType": this.str.SRBridgingType = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsPercentageCoverageValue": this.str.IsPercentageCoverageValue = (string)value; break;							
						case "CoverageValue": this.str.CoverageValue = (string)value; break;							
						case "MarginValue": this.str.MarginValue = (string)value; break;							
						case "BridgingID": this.str.BridgingID = (string)value; break;							
						case "BridgingCode": this.str.BridgingCode = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsPercentageCoverageValue":
						
							if (value == null || value is System.Boolean)
								this.IsPercentageCoverageValue = (System.Boolean?)value;
							break;
						
						case "CoverageValue":
						
							if (value == null || value is System.Decimal)
								this.CoverageValue = (System.Decimal?)value;
							break;
						
						case "MarginValue":
						
							if (value == null || value is System.Decimal)
								this.MarginValue = (System.Decimal?)value;
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
		/// Maps to GuarantorBridging.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorBridgingMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorBridgingMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.SRBridgingType
		/// </summary>
		virtual public System.String SRBridgingType
		{
			get
			{
				return base.GetSystemString(GuarantorBridgingMetadata.ColumnNames.SRBridgingType);
			}
			
			set
			{
				base.SetSystemString(GuarantorBridgingMetadata.ColumnNames.SRBridgingType, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(GuarantorBridgingMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorBridgingMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorBridgingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorBridgingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorBridgingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorBridgingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.IsPercentageCoverageValue
		/// </summary>
		virtual public System.Boolean? IsPercentageCoverageValue
		{
			get
			{
				return base.GetSystemBoolean(GuarantorBridgingMetadata.ColumnNames.IsPercentageCoverageValue);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorBridgingMetadata.ColumnNames.IsPercentageCoverageValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.CoverageValue
		/// </summary>
		virtual public System.Decimal? CoverageValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorBridgingMetadata.ColumnNames.CoverageValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorBridgingMetadata.ColumnNames.CoverageValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.MarginValue
		/// </summary>
		virtual public System.Decimal? MarginValue
		{
			get
			{
				return base.GetSystemDecimal(GuarantorBridgingMetadata.ColumnNames.MarginValue);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorBridgingMetadata.ColumnNames.MarginValue, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.BridgingID
		/// </summary>
		virtual public System.String BridgingID
		{
			get
			{
				return base.GetSystemString(GuarantorBridgingMetadata.ColumnNames.BridgingID);
			}
			
			set
			{
				base.SetSystemString(GuarantorBridgingMetadata.ColumnNames.BridgingID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorBridging.BridgingCode
		/// </summary>
		virtual public System.String BridgingCode
		{
			get
			{
				return base.GetSystemString(GuarantorBridgingMetadata.ColumnNames.BridgingCode);
			}
			
			set
			{
				base.SetSystemString(GuarantorBridgingMetadata.ColumnNames.BridgingCode, value);
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
			public esStrings(esGuarantorBridging entity)
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
				
			public System.String SRBridgingType
			{
				get
				{
					System.String data = entity.SRBridgingType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBridgingType = null;
					else entity.SRBridgingType = Convert.ToString(value);
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
				
			public System.String IsPercentageCoverageValue
			{
				get
				{
					System.Boolean? data = entity.IsPercentageCoverageValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPercentageCoverageValue = null;
					else entity.IsPercentageCoverageValue = Convert.ToBoolean(value);
				}
			}
				
			public System.String CoverageValue
			{
				get
				{
					System.Decimal? data = entity.CoverageValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageValue = null;
					else entity.CoverageValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String MarginValue
			{
				get
				{
					System.Decimal? data = entity.MarginValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MarginValue = null;
					else entity.MarginValue = Convert.ToDecimal(value);
				}
			}
				
			public System.String BridgingID
			{
				get
				{
					System.String data = entity.BridgingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingID = null;
					else entity.BridgingID = Convert.ToString(value);
				}
			}
				
			public System.String BridgingCode
			{
				get
				{
					System.String data = entity.BridgingCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BridgingCode = null;
					else entity.BridgingCode = Convert.ToString(value);
				}
			}
			

			private esGuarantorBridging entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorBridgingQuery query)
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
				throw new Exception("esGuarantorBridging can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorBridging : esGuarantorBridging
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
	abstract public class esGuarantorBridgingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorBridgingMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRBridgingType
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.SRBridgingType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPercentageCoverageValue
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.IsPercentageCoverageValue, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem CoverageValue
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.CoverageValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MarginValue
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.MarginValue, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem BridgingID
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.BridgingID, esSystemType.String);
			}
		} 
		
		public esQueryItem BridgingCode
		{
			get
			{
				return new esQueryItem(this, GuarantorBridgingMetadata.ColumnNames.BridgingCode, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorBridgingCollection")]
	public partial class GuarantorBridgingCollection : esGuarantorBridgingCollection, IEnumerable<GuarantorBridging>
	{
		public GuarantorBridgingCollection()
		{

		}
		
		public static implicit operator List<GuarantorBridging>(GuarantorBridgingCollection coll)
		{
			List<GuarantorBridging> list = new List<GuarantorBridging>();
			
			foreach (GuarantorBridging emp in coll)
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
				return  GuarantorBridgingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorBridging(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorBridging();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorBridgingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorBridging AddNew()
		{
			GuarantorBridging entity = base.AddNewEntity() as GuarantorBridging;
			
			return entity;
		}

		public GuarantorBridging FindByPrimaryKey(System.String guarantorID, System.String sRBridgingType)
		{
			return base.FindByPrimaryKey(guarantorID, sRBridgingType) as GuarantorBridging;
		}


		#region IEnumerable<GuarantorBridging> Members

		IEnumerator<GuarantorBridging> IEnumerable<GuarantorBridging>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorBridging;
			}
		}

		#endregion
		
		private GuarantorBridgingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorBridging' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorBridging ({GuarantorID},{SRBridgingType})")]
	[Serializable]
	public partial class GuarantorBridging : esGuarantorBridging
	{
		public GuarantorBridging()
		{

		}
	
		public GuarantorBridging(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorBridgingMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorBridgingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorBridgingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorBridgingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorBridgingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorBridgingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorBridgingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorBridgingQuery : esGuarantorBridgingQuery
	{
		public GuarantorBridgingQuery()
		{

		}		
		
		public GuarantorBridgingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorBridgingQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorBridgingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorBridgingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.SRBridgingType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.SRBridgingType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.IsPercentageCoverageValue, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.IsPercentageCoverageValue;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.CoverageValue, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.CoverageValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.MarginValue, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.MarginValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.BridgingID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.BridgingID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorBridgingMetadata.ColumnNames.BridgingCode, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorBridgingMetadata.PropertyNames.BridgingCode;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorBridgingMetadata Meta()
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
			 public const string SRBridgingType = "SRBridgingType";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPercentageCoverageValue = "IsPercentageCoverageValue";
			 public const string CoverageValue = "CoverageValue";
			 public const string MarginValue = "MarginValue";
			 public const string BridgingID = "BridgingID";
			 public const string BridgingCode = "BridgingCode";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string SRBridgingType = "SRBridgingType";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPercentageCoverageValue = "IsPercentageCoverageValue";
			 public const string CoverageValue = "CoverageValue";
			 public const string MarginValue = "MarginValue";
			 public const string BridgingID = "BridgingID";
			 public const string BridgingCode = "BridgingCode";
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
			lock (typeof(GuarantorBridgingMetadata))
			{
				if(GuarantorBridgingMetadata.mapDelegates == null)
				{
					GuarantorBridgingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorBridgingMetadata.meta == null)
				{
					GuarantorBridgingMetadata.meta = new GuarantorBridgingMetadata();
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
				meta.AddTypeMap("SRBridgingType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPercentageCoverageValue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CoverageValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("MarginValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("BridgingID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BridgingCode", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "GuarantorBridging";
				meta.Destination = "GuarantorBridging";
				
				meta.spInsert = "proc_GuarantorBridgingInsert";				
				meta.spUpdate = "proc_GuarantorBridgingUpdate";		
				meta.spDelete = "proc_GuarantorBridgingDelete";
				meta.spLoadAll = "proc_GuarantorBridgingLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorBridgingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorBridgingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
