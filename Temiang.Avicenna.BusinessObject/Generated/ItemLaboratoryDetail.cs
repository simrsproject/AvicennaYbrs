/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/25/2017 7:52:47 PM
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
	abstract public class esItemLaboratoryDetailCollection : esEntityCollectionWAuditLog
	{
		public esItemLaboratoryDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemLaboratoryDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemLaboratoryDetailQuery query)
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
			this.InitQuery(query as esItemLaboratoryDetailQuery);
		}
		#endregion
		
		virtual public ItemLaboratoryDetail DetachEntity(ItemLaboratoryDetail entity)
		{
			return base.DetachEntity(entity) as ItemLaboratoryDetail;
		}
		
		virtual public ItemLaboratoryDetail AttachEntity(ItemLaboratoryDetail entity)
		{
			return base.AttachEntity(entity) as ItemLaboratoryDetail;
		}
		
		virtual public void Combine(ItemLaboratoryDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemLaboratoryDetail this[int index]
		{
			get
			{
				return base[index] as ItemLaboratoryDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemLaboratoryDetail);
		}
	}



	[Serializable]
	abstract public class esItemLaboratoryDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemLaboratoryDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemLaboratoryDetail()
		{

		}

		public esItemLaboratoryDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String sequenceNo)
		{
			esItemLaboratoryDetailQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("SequenceNo",sequenceNo);
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
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "Sex": this.str.Sex = (string)value; break;							
						case "SRAgeUnit": this.str.SRAgeUnit = (string)value; break;							
						case "AgeMin": this.str.AgeMin = (string)value; break;							
						case "TotalAgeMin": this.str.TotalAgeMin = (string)value; break;							
						case "AgeMax": this.str.AgeMax = (string)value; break;							
						case "TotalAgeMax": this.str.TotalAgeMax = (string)value; break;							
						case "SRAnswerType": this.str.SRAnswerType = (string)value; break;							
						case "NormalValueMin": this.str.NormalValueMin = (string)value; break;							
						case "NormalValueMax": this.str.NormalValueMax = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "AnswerTypeReferenceID": this.str.AnswerTypeReferenceID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AgeMin":
						
							if (value == null || value is System.Decimal)
								this.AgeMin = (System.Decimal?)value;
							break;
						
						case "TotalAgeMin":
						
							if (value == null || value is System.Decimal)
								this.TotalAgeMin = (System.Decimal?)value;
							break;
						
						case "AgeMax":
						
							if (value == null || value is System.Decimal)
								this.AgeMax = (System.Decimal?)value;
							break;
						
						case "TotalAgeMax":
						
							if (value == null || value is System.Decimal)
								this.TotalAgeMax = (System.Decimal?)value;
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
		/// Maps to ItemLaboratoryDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.Sex
		/// </summary>
		virtual public System.String Sex
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.Sex);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.Sex, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.SRAgeUnit
		/// </summary>
		virtual public System.String SRAgeUnit
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.SRAgeUnit);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.SRAgeUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.AgeMin
		/// </summary>
		virtual public System.Decimal? AgeMin
		{
			get
			{
				return base.GetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.AgeMin);
			}
			
			set
			{
				base.SetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.AgeMin, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.TotalAgeMin
		/// </summary>
		virtual public System.Decimal? TotalAgeMin
		{
			get
			{
				return base.GetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMin);
			}
			
			set
			{
				base.SetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMin, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.AgeMax
		/// </summary>
		virtual public System.Decimal? AgeMax
		{
			get
			{
				return base.GetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.AgeMax);
			}
			
			set
			{
				base.SetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.AgeMax, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.TotalAgeMax
		/// </summary>
		virtual public System.Decimal? TotalAgeMax
		{
			get
			{
				return base.GetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMax);
			}
			
			set
			{
				base.SetSystemDecimal(ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMax, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.SRAnswerType
		/// </summary>
		virtual public System.String SRAnswerType
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.SRAnswerType);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.SRAnswerType, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.NormalValueMin
		/// </summary>
		virtual public System.String NormalValueMin
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMin);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMin, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.NormalValueMax
		/// </summary>
		virtual public System.String NormalValueMax
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMax);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMax, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemLaboratoryDetail.AnswerTypeReferenceID
		/// </summary>
		virtual public System.String AnswerTypeReferenceID
		{
			get
			{
				return base.GetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.AnswerTypeReferenceID);
			}
			
			set
			{
				base.SetSystemString(ItemLaboratoryDetailMetadata.ColumnNames.AnswerTypeReferenceID, value);
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
			public esStrings(esItemLaboratoryDetail entity)
			{
				this.entity = entity;
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
				
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
				
			public System.String Sex
			{
				get
				{
					System.String data = entity.Sex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sex = null;
					else entity.Sex = Convert.ToString(value);
				}
			}
				
			public System.String SRAgeUnit
			{
				get
				{
					System.String data = entity.SRAgeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAgeUnit = null;
					else entity.SRAgeUnit = Convert.ToString(value);
				}
			}
				
			public System.String AgeMin
			{
				get
				{
					System.Decimal? data = entity.AgeMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeMin = null;
					else entity.AgeMin = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalAgeMin
			{
				get
				{
					System.Decimal? data = entity.TotalAgeMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalAgeMin = null;
					else entity.TotalAgeMin = Convert.ToDecimal(value);
				}
			}
				
			public System.String AgeMax
			{
				get
				{
					System.Decimal? data = entity.AgeMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AgeMax = null;
					else entity.AgeMax = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalAgeMax
			{
				get
				{
					System.Decimal? data = entity.TotalAgeMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalAgeMax = null;
					else entity.TotalAgeMax = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRAnswerType
			{
				get
				{
					System.String data = entity.SRAnswerType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAnswerType = null;
					else entity.SRAnswerType = Convert.ToString(value);
				}
			}
				
			public System.String NormalValueMin
			{
				get
				{
					System.String data = entity.NormalValueMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalValueMin = null;
					else entity.NormalValueMin = Convert.ToString(value);
				}
			}
				
			public System.String NormalValueMax
			{
				get
				{
					System.String data = entity.NormalValueMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NormalValueMax = null;
					else entity.NormalValueMax = Convert.ToString(value);
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
				
			public System.String AnswerTypeReferenceID
			{
				get
				{
					System.String data = entity.AnswerTypeReferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AnswerTypeReferenceID = null;
					else entity.AnswerTypeReferenceID = Convert.ToString(value);
				}
			}
			

			private esItemLaboratoryDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemLaboratoryDetailQuery query)
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
				throw new Exception("esItemLaboratoryDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemLaboratoryDetail : esItemLaboratoryDetail
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
	abstract public class esItemLaboratoryDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemLaboratoryDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Sex
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.Sex, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAgeUnit
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.SRAgeUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem AgeMin
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.AgeMin, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalAgeMin
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMin, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AgeMax
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.AgeMax, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalAgeMax
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMax, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRAnswerType
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.SRAnswerType, esSystemType.String);
			}
		} 
		
		public esQueryItem NormalValueMin
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMin, esSystemType.String);
			}
		} 
		
		public esQueryItem NormalValueMax
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMax, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem AnswerTypeReferenceID
		{
			get
			{
				return new esQueryItem(this, ItemLaboratoryDetailMetadata.ColumnNames.AnswerTypeReferenceID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemLaboratoryDetailCollection")]
	public partial class ItemLaboratoryDetailCollection : esItemLaboratoryDetailCollection, IEnumerable<ItemLaboratoryDetail>
	{
		public ItemLaboratoryDetailCollection()
		{

		}
		
		public static implicit operator List<ItemLaboratoryDetail>(ItemLaboratoryDetailCollection coll)
		{
			List<ItemLaboratoryDetail> list = new List<ItemLaboratoryDetail>();
			
			foreach (ItemLaboratoryDetail emp in coll)
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
				return  ItemLaboratoryDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLaboratoryDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemLaboratoryDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemLaboratoryDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemLaboratoryDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLaboratoryDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemLaboratoryDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemLaboratoryDetail AddNew()
		{
			ItemLaboratoryDetail entity = base.AddNewEntity() as ItemLaboratoryDetail;
			
			return entity;
		}

		public ItemLaboratoryDetail FindByPrimaryKey(System.String itemID, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(itemID, sequenceNo) as ItemLaboratoryDetail;
		}


		#region IEnumerable<ItemLaboratoryDetail> Members

		IEnumerator<ItemLaboratoryDetail> IEnumerable<ItemLaboratoryDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemLaboratoryDetail;
			}
		}

		#endregion
		
		private ItemLaboratoryDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemLaboratoryDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemLaboratoryDetail ({ItemID},{SequenceNo})")]
	[Serializable]
	public partial class ItemLaboratoryDetail : esItemLaboratoryDetail
	{
		public ItemLaboratoryDetail()
		{

		}
	
		public ItemLaboratoryDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemLaboratoryDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esItemLaboratoryDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemLaboratoryDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemLaboratoryDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemLaboratoryDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemLaboratoryDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemLaboratoryDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemLaboratoryDetailQuery : esItemLaboratoryDetailQuery
	{
		public ItemLaboratoryDetailQuery()
		{

		}		
		
		public ItemLaboratoryDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemLaboratoryDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemLaboratoryDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemLaboratoryDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.Sex, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.Sex;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.SRAgeUnit, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.SRAgeUnit;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.AgeMin, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.AgeMin;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMin, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.TotalAgeMin;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.AgeMax, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.AgeMax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.TotalAgeMax, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.TotalAgeMax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.SRAnswerType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.SRAnswerType;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMin, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.NormalValueMin;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.NormalValueMax, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.NormalValueMax;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.Notes, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemLaboratoryDetailMetadata.ColumnNames.AnswerTypeReferenceID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemLaboratoryDetailMetadata.PropertyNames.AnswerTypeReferenceID;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemLaboratoryDetailMetadata Meta()
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
			 public const string ItemID = "ItemID";
			 public const string SequenceNo = "SequenceNo";
			 public const string Sex = "Sex";
			 public const string SRAgeUnit = "SRAgeUnit";
			 public const string AgeMin = "AgeMin";
			 public const string TotalAgeMin = "TotalAgeMin";
			 public const string AgeMax = "AgeMax";
			 public const string TotalAgeMax = "TotalAgeMax";
			 public const string SRAnswerType = "SRAnswerType";
			 public const string NormalValueMin = "NormalValueMin";
			 public const string NormalValueMax = "NormalValueMax";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string AnswerTypeReferenceID = "AnswerTypeReferenceID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string SequenceNo = "SequenceNo";
			 public const string Sex = "Sex";
			 public const string SRAgeUnit = "SRAgeUnit";
			 public const string AgeMin = "AgeMin";
			 public const string TotalAgeMin = "TotalAgeMin";
			 public const string AgeMax = "AgeMax";
			 public const string TotalAgeMax = "TotalAgeMax";
			 public const string SRAnswerType = "SRAnswerType";
			 public const string NormalValueMin = "NormalValueMin";
			 public const string NormalValueMax = "NormalValueMax";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string AnswerTypeReferenceID = "AnswerTypeReferenceID";
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
			lock (typeof(ItemLaboratoryDetailMetadata))
			{
				if(ItemLaboratoryDetailMetadata.mapDelegates == null)
				{
					ItemLaboratoryDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemLaboratoryDetailMetadata.meta == null)
				{
					ItemLaboratoryDetailMetadata.meta = new ItemLaboratoryDetailMetadata();
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
				

				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("SRAgeUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AgeMin", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalAgeMin", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AgeMax", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TotalAgeMax", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SRAnswerType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NormalValueMin", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NormalValueMax", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AnswerTypeReferenceID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemLaboratoryDetail";
				meta.Destination = "ItemLaboratoryDetail";
				
				meta.spInsert = "proc_ItemLaboratoryDetailInsert";				
				meta.spUpdate = "proc_ItemLaboratoryDetailUpdate";		
				meta.spDelete = "proc_ItemLaboratoryDetailDelete";
				meta.spLoadAll = "proc_ItemLaboratoryDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemLaboratoryDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemLaboratoryDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
