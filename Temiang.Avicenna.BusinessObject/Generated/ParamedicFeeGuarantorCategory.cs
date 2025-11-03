/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/19/2015 10:37:47 AM
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
	abstract public class esParamedicFeeGuarantorCategoryCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeGuarantorCategoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeGuarantorCategoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeGuarantorCategoryQuery query)
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
			this.InitQuery(query as esParamedicFeeGuarantorCategoryQuery);
		}
		#endregion
		
		virtual public ParamedicFeeGuarantorCategory DetachEntity(ParamedicFeeGuarantorCategory entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeGuarantorCategory;
		}
		
		virtual public ParamedicFeeGuarantorCategory AttachEntity(ParamedicFeeGuarantorCategory entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeGuarantorCategory;
		}
		
		virtual public void Combine(ParamedicFeeGuarantorCategoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeGuarantorCategory this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeGuarantorCategory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeGuarantorCategory);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeGuarantorCategory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeGuarantorCategoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeGuarantorCategory()
		{

		}

		public esParamedicFeeGuarantorCategory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paramedicID, System.String sRPhysicianFeeType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, sRPhysicianFeeType);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, sRPhysicianFeeType);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paramedicID, System.String sRPhysicianFeeType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, sRPhysicianFeeType);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, sRPhysicianFeeType);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paramedicID, System.String sRPhysicianFeeType)
		{
			esParamedicFeeGuarantorCategoryQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID == paramedicID, query.SRPhysicianFeeType == sRPhysicianFeeType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paramedicID, System.String sRPhysicianFeeType)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID",paramedicID);			parms.Add("SRPhysicianFeeType",sRPhysicianFeeType);
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
						case "SRPhysicianFeeType": this.str.SRPhysicianFeeType = (string)value; break;							
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
		/// Maps to ParamedicFeeGuarantorCategory.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.SRPhysicianFeeType
		/// </summary>
		virtual public System.String SRPhysicianFeeType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.SRPhysicianFeeType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.SRPhysicianFeeType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.IsParamedicFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsParamedicFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsParamedicFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.ParamedicFeeAmount
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.ParamedicFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.IsDeductionFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsDeductionFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsDeductionFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.DeductionFeeAmount
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.DeductionFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeGuarantorCategory entity)
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
				
			public System.String SRPhysicianFeeType
			{
				get
				{
					System.String data = entity.SRPhysicianFeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicianFeeType = null;
					else entity.SRPhysicianFeeType = Convert.ToString(value);
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
			

			private esParamedicFeeGuarantorCategory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeGuarantorCategoryQuery query)
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
				throw new Exception("esParamedicFeeGuarantorCategory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeGuarantorCategory : esParamedicFeeGuarantorCategory
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
	abstract public class esParamedicFeeGuarantorCategoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeGuarantorCategoryMetadata.Meta();
			}
		}	
		

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPhysicianFeeType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.SRPhysicianFeeType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsParamedicFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ParamedicFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsDeductionFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem DeductionFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeductionFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeGuarantorCategoryCollection")]
	public partial class ParamedicFeeGuarantorCategoryCollection : esParamedicFeeGuarantorCategoryCollection, IEnumerable<ParamedicFeeGuarantorCategory>
	{
		public ParamedicFeeGuarantorCategoryCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeGuarantorCategory>(ParamedicFeeGuarantorCategoryCollection coll)
		{
			List<ParamedicFeeGuarantorCategory> list = new List<ParamedicFeeGuarantorCategory>();
			
			foreach (ParamedicFeeGuarantorCategory emp in coll)
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
				return  ParamedicFeeGuarantorCategoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeGuarantorCategoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeGuarantorCategory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeGuarantorCategory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeGuarantorCategoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeGuarantorCategoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeGuarantorCategoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeGuarantorCategory AddNew()
		{
			ParamedicFeeGuarantorCategory entity = base.AddNewEntity() as ParamedicFeeGuarantorCategory;
			
			return entity;
		}

		public ParamedicFeeGuarantorCategory FindByPrimaryKey(System.String paramedicID, System.String sRPhysicianFeeType)
		{
			return base.FindByPrimaryKey(paramedicID, sRPhysicianFeeType) as ParamedicFeeGuarantorCategory;
		}


		#region IEnumerable<ParamedicFeeGuarantorCategory> Members

		IEnumerator<ParamedicFeeGuarantorCategory> IEnumerable<ParamedicFeeGuarantorCategory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeGuarantorCategory;
			}
		}

		#endregion
		
		private ParamedicFeeGuarantorCategoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeGuarantorCategory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeGuarantorCategory ({ParamedicID},{SRPhysicianFeeType})")]
	[Serializable]
	public partial class ParamedicFeeGuarantorCategory : esParamedicFeeGuarantorCategory
	{
		public ParamedicFeeGuarantorCategory()
		{

		}
	
		public ParamedicFeeGuarantorCategory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeGuarantorCategoryMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeGuarantorCategoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeGuarantorCategoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeGuarantorCategoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeGuarantorCategoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeGuarantorCategoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeGuarantorCategoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeGuarantorCategoryQuery : esParamedicFeeGuarantorCategoryQuery
	{
		public ParamedicFeeGuarantorCategoryQuery()
		{

		}		
		
		public ParamedicFeeGuarantorCategoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeGuarantorCategoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeGuarantorCategoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeGuarantorCategoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.SRPhysicianFeeType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.SRPhysicianFeeType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsParamedicFeeUsePercentage, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.IsParamedicFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.ParamedicFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmountReferral, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.ParamedicFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsDeductionFeeUsePercentage, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.IsDeductionFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.DeductionFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmountReferral, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.DeductionFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeGuarantorCategoryMetadata Meta()
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
			 public const string SRPhysicianFeeType = "SRPhysicianFeeType";
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
			 public const string SRPhysicianFeeType = "SRPhysicianFeeType";
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
			lock (typeof(ParamedicFeeGuarantorCategoryMetadata))
			{
				if(ParamedicFeeGuarantorCategoryMetadata.mapDelegates == null)
				{
					ParamedicFeeGuarantorCategoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeGuarantorCategoryMetadata.meta == null)
				{
					ParamedicFeeGuarantorCategoryMetadata.meta = new ParamedicFeeGuarantorCategoryMetadata();
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
				meta.AddTypeMap("SRPhysicianFeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeGuarantorCategory";
				meta.Destination = "ParamedicFeeGuarantorCategory";
				
				meta.spInsert = "proc_ParamedicFeeGuarantorCategoryInsert";				
				meta.spUpdate = "proc_ParamedicFeeGuarantorCategoryUpdate";		
				meta.spDelete = "proc_ParamedicFeeGuarantorCategoryDelete";
				meta.spLoadAll = "proc_ParamedicFeeGuarantorCategoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeGuarantorCategoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeGuarantorCategoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
