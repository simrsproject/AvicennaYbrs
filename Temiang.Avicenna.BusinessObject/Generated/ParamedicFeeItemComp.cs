/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/5/2015 9:42:47 AM
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
	abstract public class esParamedicFeeItemCompCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeItemCompCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeItemCompCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeItemCompQuery query)
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
			this.InitQuery(query as esParamedicFeeItemCompQuery);
		}
		#endregion
		
		virtual public ParamedicFeeItemComp DetachEntity(ParamedicFeeItemComp entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeItemComp;
		}
		
		virtual public ParamedicFeeItemComp AttachEntity(ParamedicFeeItemComp entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeItemComp;
		}
		
		virtual public void Combine(ParamedicFeeItemCompCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeItemComp this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeItemComp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeItemComp);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeItemComp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeItemCompQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeItemComp()
		{

		}

		public esParamedicFeeItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paramedicID, System.String itemID, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, itemID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paramedicID, System.String itemID, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, itemID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paramedicID, System.String itemID, System.String tariffComponentID)
		{
			esParamedicFeeItemCompQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID == paramedicID, query.ItemID == itemID, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paramedicID, System.String itemID, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID",paramedicID);			parms.Add("ItemID",itemID);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "IsParamedicFeeUsePercentage": this.str.IsParamedicFeeUsePercentage = (string)value; break;							
						case "ParamedicFeeAmount": this.str.ParamedicFeeAmount = (string)value; break;							
						case "ParamedicFeeAmountReferral": this.str.ParamedicFeeAmountReferral = (string)value; break;							
						case "IsDeductionFeeUsePercentage": this.str.IsDeductionFeeUsePercentage = (string)value; break;							
						case "DeductionFeeAmount": this.str.DeductionFeeAmount = (string)value; break;							
						case "DeductionFeeAmountReferral": this.str.DeductionFeeAmountReferral = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsParamedicFeeUsePercentage":
						
							if (value == null || value is System.Boolean)
								this.IsParamedicFeeUsePercentage = (System.Boolean?)value;
							break;
						
						case "ParamedicFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.ParamedicFeeAmount = (System.Decimal?)value;
							break;
						
						case "ParamedicFeeAmountReferral":
						
							if (value == null || value is System.Decimal)
								this.ParamedicFeeAmountReferral = (System.Decimal?)value;
							break;
						
						case "IsDeductionFeeUsePercentage":
						
							if (value == null || value is System.Boolean)
								this.IsDeductionFeeUsePercentage = (System.Boolean?)value;
							break;
						
						case "DeductionFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.DeductionFeeAmount = (System.Decimal?)value;
							break;
						
						case "DeductionFeeAmountReferral":
						
							if (value == null || value is System.Decimal)
								this.DeductionFeeAmountReferral = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeItemComp.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.IsParamedicFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsParamedicFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.ParamedicFeeAmount
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.ParamedicFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.IsDeductionFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsDeductionFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.DeductionFeeAmount
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.DeductionFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemComp.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeItemComp entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
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
				
			public System.String IsParamedicFeeUsePercentage
			{
				get
				{
					System.Boolean? data = entity.IsParamedicFeeUsePercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParamedicFeeUsePercentage = null;
					else entity.IsParamedicFeeUsePercentage = Convert.ToBoolean(value);
				}
			}
				
			public System.String ParamedicFeeAmount
			{
				get
				{
					System.Decimal? data = entity.ParamedicFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicFeeAmount = null;
					else entity.ParamedicFeeAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String ParamedicFeeAmountReferral
			{
				get
				{
					System.Decimal? data = entity.ParamedicFeeAmountReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicFeeAmountReferral = null;
					else entity.ParamedicFeeAmountReferral = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsDeductionFeeUsePercentage
			{
				get
				{
					System.Boolean? data = entity.IsDeductionFeeUsePercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeductionFeeUsePercentage = null;
					else entity.IsDeductionFeeUsePercentage = Convert.ToBoolean(value);
				}
			}
				
			public System.String DeductionFeeAmount
			{
				get
				{
					System.Decimal? data = entity.DeductionFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionFeeAmount = null;
					else entity.DeductionFeeAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DeductionFeeAmountReferral
			{
				get
				{
					System.Decimal? data = entity.DeductionFeeAmountReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionFeeAmountReferral = null;
					else entity.DeductionFeeAmountReferral = Convert.ToDecimal(value);
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
			

			private esParamedicFeeItemComp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeItemCompQuery query)
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
				throw new Exception("esParamedicFeeItemComp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeItemComp : esParamedicFeeItemComp
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
	abstract public class esParamedicFeeItemCompQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeItemCompMetadata.Meta();
			}
		}	
		

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsParamedicFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ParamedicFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsDeductionFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem DeductionFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeductionFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeItemCompCollection")]
	public partial class ParamedicFeeItemCompCollection : esParamedicFeeItemCompCollection, IEnumerable<ParamedicFeeItemComp>
	{
		public ParamedicFeeItemCompCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeItemComp>(ParamedicFeeItemCompCollection coll)
		{
			List<ParamedicFeeItemComp> list = new List<ParamedicFeeItemComp>();
			
			foreach (ParamedicFeeItemComp emp in coll)
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
				return  ParamedicFeeItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeItemComp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeItemComp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeItemComp AddNew()
		{
			ParamedicFeeItemComp entity = base.AddNewEntity() as ParamedicFeeItemComp;
			
			return entity;
		}

		public ParamedicFeeItemComp FindByPrimaryKey(System.String paramedicID, System.String itemID, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(paramedicID, itemID, tariffComponentID) as ParamedicFeeItemComp;
		}


		#region IEnumerable<ParamedicFeeItemComp> Members

		IEnumerator<ParamedicFeeItemComp> IEnumerable<ParamedicFeeItemComp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeItemComp;
			}
		}

		#endregion
		
		private ParamedicFeeItemCompQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeItemComp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeItemComp ({ParamedicID},{ItemID},{TariffComponentID})")]
	[Serializable]
	public partial class ParamedicFeeItemComp : esParamedicFeeItemComp
	{
		public ParamedicFeeItemComp()
		{

		}
	
		public ParamedicFeeItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeItemCompQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeItemCompQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeItemCompQuery : esParamedicFeeItemCompQuery
	{
		public ParamedicFeeItemCompQuery()
		{

		}		
		
		public ParamedicFeeItemCompQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeItemCompQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeItemCompMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeItemCompMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.IsParamedicFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.ParamedicFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.ParamedicFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.IsDeductionFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.DeductionFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.DeductionFeeAmountReferral, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.DeductionFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemCompMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemCompMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeItemCompMetadata Meta()
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
			 public const string ParamedicID = "ParamedicID";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
			 public const string ParamedicFeeAmount = "ParamedicFeeAmount";
			 public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
			 public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
			 public const string DeductionFeeAmount = "DeductionFeeAmount";
			 public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ParamedicID = "ParamedicID";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
			 public const string ParamedicFeeAmount = "ParamedicFeeAmount";
			 public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
			 public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
			 public const string DeductionFeeAmount = "DeductionFeeAmount";
			 public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
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
			lock (typeof(ParamedicFeeItemCompMetadata))
			{
				if(ParamedicFeeItemCompMetadata.mapDelegates == null)
				{
					ParamedicFeeItemCompMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeItemCompMetadata.meta == null)
				{
					ParamedicFeeItemCompMetadata.meta = new ParamedicFeeItemCompMetadata();
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
				

				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeItemComp";
				meta.Destination = "ParamedicFeeItemComp";
				
				meta.spInsert = "proc_ParamedicFeeItemCompInsert";				
				meta.spUpdate = "proc_ParamedicFeeItemCompUpdate";		
				meta.spDelete = "proc_ParamedicFeeItemCompDelete";
				meta.spLoadAll = "proc_ParamedicFeeItemCompLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeItemCompLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeItemCompMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
