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
	abstract public class esParamedicFeeItemGuarantorCompCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeItemGuarantorCompCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeItemGuarantorCompCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeItemGuarantorCompQuery query)
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
			this.InitQuery(query as esParamedicFeeItemGuarantorCompQuery);
		}
		#endregion
		
		virtual public ParamedicFeeItemGuarantorComp DetachEntity(ParamedicFeeItemGuarantorComp entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeItemGuarantorComp;
		}
		
		virtual public ParamedicFeeItemGuarantorComp AttachEntity(ParamedicFeeItemGuarantorComp entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeItemGuarantorComp;
		}
		
		virtual public void Combine(ParamedicFeeItemGuarantorCompCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeItemGuarantorComp this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeItemGuarantorComp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeItemGuarantorComp);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeItemGuarantorComp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeItemGuarantorCompQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeItemGuarantorComp()
		{

		}

		public esParamedicFeeItemGuarantorComp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paramedicID, System.String itemID, System.String guarantorID, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, itemID, guarantorID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID, guarantorID, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paramedicID, System.String itemID, System.String guarantorID, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, itemID, guarantorID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID, guarantorID, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paramedicID, System.String itemID, System.String guarantorID, System.String tariffComponentID)
		{
			esParamedicFeeItemGuarantorCompQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID == paramedicID, query.ItemID == itemID, query.GuarantorID == guarantorID, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paramedicID, System.String itemID, System.String guarantorID, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID",paramedicID);			parms.Add("ItemID",itemID);			parms.Add("GuarantorID",guarantorID);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
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
		/// Maps to ParamedicFeeItemGuarantorComp.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.IsParamedicFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsParamedicFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsParamedicFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.ParamedicFeeAmount
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.ParamedicFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.IsDeductionFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsDeductionFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsDeductionFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.DeductionFeeAmount
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.DeductionFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeItemGuarantorComp.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeItemGuarantorComp entity)
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
			

			private esParamedicFeeItemGuarantorComp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeItemGuarantorCompQuery query)
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
				throw new Exception("esParamedicFeeItemGuarantorComp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeItemGuarantorComp : esParamedicFeeItemGuarantorComp
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
	abstract public class esParamedicFeeItemGuarantorCompQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeItemGuarantorCompMetadata.Meta();
			}
		}	
		

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsParamedicFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ParamedicFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsDeductionFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem DeductionFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeductionFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeItemGuarantorCompCollection")]
	public partial class ParamedicFeeItemGuarantorCompCollection : esParamedicFeeItemGuarantorCompCollection, IEnumerable<ParamedicFeeItemGuarantorComp>
	{
		public ParamedicFeeItemGuarantorCompCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeItemGuarantorComp>(ParamedicFeeItemGuarantorCompCollection coll)
		{
			List<ParamedicFeeItemGuarantorComp> list = new List<ParamedicFeeItemGuarantorComp>();
			
			foreach (ParamedicFeeItemGuarantorComp emp in coll)
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
				return  ParamedicFeeItemGuarantorCompMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeItemGuarantorCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeItemGuarantorComp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeItemGuarantorComp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeItemGuarantorCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeItemGuarantorCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeItemGuarantorCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeItemGuarantorComp AddNew()
		{
			ParamedicFeeItemGuarantorComp entity = base.AddNewEntity() as ParamedicFeeItemGuarantorComp;
			
			return entity;
		}

		public ParamedicFeeItemGuarantorComp FindByPrimaryKey(System.String paramedicID, System.String itemID, System.String guarantorID, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(paramedicID, itemID, guarantorID, tariffComponentID) as ParamedicFeeItemGuarantorComp;
		}


		#region IEnumerable<ParamedicFeeItemGuarantorComp> Members

		IEnumerator<ParamedicFeeItemGuarantorComp> IEnumerable<ParamedicFeeItemGuarantorComp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeItemGuarantorComp;
			}
		}

		#endregion
		
		private ParamedicFeeItemGuarantorCompQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeItemGuarantorComp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeItemGuarantorComp ({ParamedicID},{ItemID},{GuarantorID},{TariffComponentID})")]
	[Serializable]
	public partial class ParamedicFeeItemGuarantorComp : esParamedicFeeItemGuarantorComp
	{
		public ParamedicFeeItemGuarantorComp()
		{

		}
	
		public ParamedicFeeItemGuarantorComp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeItemGuarantorCompMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeItemGuarantorCompQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeItemGuarantorCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeItemGuarantorCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeItemGuarantorCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeItemGuarantorCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeItemGuarantorCompQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeItemGuarantorCompQuery : esParamedicFeeItemGuarantorCompQuery
	{
		public ParamedicFeeItemGuarantorCompQuery()
		{

		}		
		
		public ParamedicFeeItemGuarantorCompQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeItemGuarantorCompQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeItemGuarantorCompMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeItemGuarantorCompMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.GuarantorID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.IsParamedicFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.ParamedicFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmountReferral, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.ParamedicFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.IsDeductionFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.DeductionFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmountReferral, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.DeductionFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeItemGuarantorCompMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeItemGuarantorCompMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeItemGuarantorCompMetadata Meta()
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
			 public const string GuarantorID = "GuarantorID";
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
			 public const string GuarantorID = "GuarantorID";
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
			lock (typeof(ParamedicFeeItemGuarantorCompMetadata))
			{
				if(ParamedicFeeItemGuarantorCompMetadata.mapDelegates == null)
				{
					ParamedicFeeItemGuarantorCompMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeItemGuarantorCompMetadata.meta == null)
				{
					ParamedicFeeItemGuarantorCompMetadata.meta = new ParamedicFeeItemGuarantorCompMetadata();
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
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeItemGuarantorComp";
				meta.Destination = "ParamedicFeeItemGuarantorComp";
				
				meta.spInsert = "proc_ParamedicFeeItemGuarantorCompInsert";				
				meta.spUpdate = "proc_ParamedicFeeItemGuarantorCompUpdate";		
				meta.spDelete = "proc_ParamedicFeeItemGuarantorCompDelete";
				meta.spLoadAll = "proc_ParamedicFeeItemGuarantorCompLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeItemGuarantorCompLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeItemGuarantorCompMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
