/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:11 PM
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
	abstract public class esAssetDepreciationPostCollection : esEntityCollectionWAuditLog
	{
		public esAssetDepreciationPostCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetDepreciationPostCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetDepreciationPostQuery query)
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
			this.InitQuery(query as esAssetDepreciationPostQuery);
		}
		#endregion
		
		virtual public AssetDepreciationPost DetachEntity(AssetDepreciationPost entity)
		{
			return base.DetachEntity(entity) as AssetDepreciationPost;
		}
		
		virtual public AssetDepreciationPost AttachEntity(AssetDepreciationPost entity)
		{
			return base.AttachEntity(entity) as AssetDepreciationPost;
		}
		
		virtual public void Combine(AssetDepreciationPostCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetDepreciationPost this[int index]
		{
			get
			{
				return base[index] as AssetDepreciationPost;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetDepreciationPost);
		}
	}



	[Serializable]
	abstract public class esAssetDepreciationPost : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetDepreciationPostQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetDepreciationPost()
		{

		}

		public esAssetDepreciationPost(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 assetDepreciationPostId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetDepreciationPostId);
			else
				return LoadByPrimaryKeyStoredProcedure(assetDepreciationPostId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 assetDepreciationPostId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(assetDepreciationPostId);
			else
				return LoadByPrimaryKeyStoredProcedure(assetDepreciationPostId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 assetDepreciationPostId)
		{
			esAssetDepreciationPostQuery query = this.GetDynamicQuery();
			query.Where(query.AssetDepreciationPostId == assetDepreciationPostId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 assetDepreciationPostId)
		{
			esParameters parms = new esParameters();
			parms.Add("AssetDepreciationPostId",assetDepreciationPostId);
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
						case "AssetDepreciationPostId": this.str.AssetDepreciationPostId = (string)value; break;							
						case "AssetID": this.str.AssetID = (string)value; break;							
						case "PeriodNo": this.str.PeriodNo = (string)value; break;							
						case "Year": this.str.Year = (string)value; break;							
						case "Month": this.str.Month = (string)value; break;							
						case "DepreciationDate": this.str.DepreciationDate = (string)value; break;							
						case "CurrentAmount": this.str.CurrentAmount = (string)value; break;							
						case "DepreciationAmount": this.str.DepreciationAmount = (string)value; break;							
						case "AccumulationAmount": this.str.AccumulationAmount = (string)value; break;							
						case "IsPosted": this.str.IsPosted = (string)value; break;							
						case "PostingId": this.str.PostingId = (string)value; break;							
						case "PostedDate": this.str.PostedDate = (string)value; break;							
						case "JournalId": this.str.JournalId = (string)value; break;							
						case "JournalNumber": this.str.JournalNumber = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AssetDepreciationPostId":
						
							if (value == null || value is System.Int32)
								this.AssetDepreciationPostId = (System.Int32?)value;
							break;
						
						case "PeriodNo":
						
							if (value == null || value is System.Int32)
								this.PeriodNo = (System.Int32?)value;
							break;
						
						case "DepreciationDate":
						
							if (value == null || value is System.DateTime)
								this.DepreciationDate = (System.DateTime?)value;
							break;
						
						case "CurrentAmount":
						
							if (value == null || value is System.Decimal)
								this.CurrentAmount = (System.Decimal?)value;
							break;
						
						case "DepreciationAmount":
						
							if (value == null || value is System.Decimal)
								this.DepreciationAmount = (System.Decimal?)value;
							break;
						
						case "AccumulationAmount":
						
							if (value == null || value is System.Decimal)
								this.AccumulationAmount = (System.Decimal?)value;
							break;
						
						case "IsPosted":
						
							if (value == null || value is System.Boolean)
								this.IsPosted = (System.Boolean?)value;
							break;
						
						case "PostingId":
						
							if (value == null || value is System.Int32)
								this.PostingId = (System.Int32?)value;
							break;
						
						case "PostedDate":
						
							if (value == null || value is System.DateTime)
								this.PostedDate = (System.DateTime?)value;
							break;
						
						case "JournalId":
						
							if (value == null || value is System.Int32)
								this.JournalId = (System.Int32?)value;
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
		/// Maps to AssetDepreciationPost.AssetDepreciationPostId
		/// </summary>
		virtual public System.Int32? AssetDepreciationPostId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.AssetDepreciationPostId);
			}
			
			set
			{
				base.SetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.AssetDepreciationPostId, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationPostMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.PeriodNo
		/// </summary>
		virtual public System.Int32? PeriodNo
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.PeriodNo);
			}
			
			set
			{
				base.SetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.PeriodNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.Year
		/// </summary>
		virtual public System.String Year
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostMetadata.ColumnNames.Year);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationPostMetadata.ColumnNames.Year, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.Month
		/// </summary>
		virtual public System.String Month
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostMetadata.ColumnNames.Month);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationPostMetadata.ColumnNames.Month, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.DepreciationDate
		/// </summary>
		virtual public System.DateTime? DepreciationDate
		{
			get
			{
				return base.GetSystemDateTime(AssetDepreciationPostMetadata.ColumnNames.DepreciationDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetDepreciationPostMetadata.ColumnNames.DepreciationDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.CurrentAmount
		/// </summary>
		virtual public System.Decimal? CurrentAmount
		{
			get
			{
				return base.GetSystemDecimal(AssetDepreciationPostMetadata.ColumnNames.CurrentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(AssetDepreciationPostMetadata.ColumnNames.CurrentAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.DepreciationAmount
		/// </summary>
		virtual public System.Decimal? DepreciationAmount
		{
			get
			{
				return base.GetSystemDecimal(AssetDepreciationPostMetadata.ColumnNames.DepreciationAmount);
			}
			
			set
			{
				base.SetSystemDecimal(AssetDepreciationPostMetadata.ColumnNames.DepreciationAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.AccumulationAmount
		/// </summary>
		virtual public System.Decimal? AccumulationAmount
		{
			get
			{
				return base.GetSystemDecimal(AssetDepreciationPostMetadata.ColumnNames.AccumulationAmount);
			}
			
			set
			{
				base.SetSystemDecimal(AssetDepreciationPostMetadata.ColumnNames.AccumulationAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.IsPosted
		/// </summary>
		virtual public System.Boolean? IsPosted
		{
			get
			{
				return base.GetSystemBoolean(AssetDepreciationPostMetadata.ColumnNames.IsPosted);
			}
			
			set
			{
				base.SetSystemBoolean(AssetDepreciationPostMetadata.ColumnNames.IsPosted, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.PostingId
		/// </summary>
		virtual public System.Int32? PostingId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.PostingId);
			}
			
			set
			{
				base.SetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.PostingId, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.PostedDate
		/// </summary>
		virtual public System.DateTime? PostedDate
		{
			get
			{
				return base.GetSystemDateTime(AssetDepreciationPostMetadata.ColumnNames.PostedDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetDepreciationPostMetadata.ColumnNames.PostedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.JournalId
		/// </summary>
		virtual public System.Int32? JournalId
		{
			get
			{
				return base.GetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.JournalId);
			}
			
			set
			{
				base.SetSystemInt32(AssetDepreciationPostMetadata.ColumnNames.JournalId, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.JournalNumber
		/// </summary>
		virtual public System.String JournalNumber
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostMetadata.ColumnNames.JournalNumber);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationPostMetadata.ColumnNames.JournalNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetDepreciationPostMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetDepreciationPostMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetDepreciationPost.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetDepreciationPostMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetDepreciationPostMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetDepreciationPost entity)
			{
				this.entity = entity;
			}
			
	
			public System.String AssetDepreciationPostId
			{
				get
				{
					System.Int32? data = entity.AssetDepreciationPostId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetDepreciationPostId = null;
					else entity.AssetDepreciationPostId = Convert.ToInt32(value);
				}
			}
				
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
				}
			}
				
			public System.String PeriodNo
			{
				get
				{
					System.Int32? data = entity.PeriodNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodNo = null;
					else entity.PeriodNo = Convert.ToInt32(value);
				}
			}
				
			public System.String Year
			{
				get
				{
					System.String data = entity.Year;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Year = null;
					else entity.Year = Convert.ToString(value);
				}
			}
				
			public System.String Month
			{
				get
				{
					System.String data = entity.Month;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Month = null;
					else entity.Month = Convert.ToString(value);
				}
			}
				
			public System.String DepreciationDate
			{
				get
				{
					System.DateTime? data = entity.DepreciationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepreciationDate = null;
					else entity.DepreciationDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String CurrentAmount
			{
				get
				{
					System.Decimal? data = entity.CurrentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentAmount = null;
					else entity.CurrentAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DepreciationAmount
			{
				get
				{
					System.Decimal? data = entity.DepreciationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepreciationAmount = null;
					else entity.DepreciationAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String AccumulationAmount
			{
				get
				{
					System.Decimal? data = entity.AccumulationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccumulationAmount = null;
					else entity.AccumulationAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsPosted
			{
				get
				{
					System.Boolean? data = entity.IsPosted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPosted = null;
					else entity.IsPosted = Convert.ToBoolean(value);
				}
			}
				
			public System.String PostingId
			{
				get
				{
					System.Int32? data = entity.PostingId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostingId = null;
					else entity.PostingId = Convert.ToInt32(value);
				}
			}
				
			public System.String PostedDate
			{
				get
				{
					System.DateTime? data = entity.PostedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostedDate = null;
					else entity.PostedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String JournalId
			{
				get
				{
					System.Int32? data = entity.JournalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalId = null;
					else entity.JournalId = Convert.ToInt32(value);
				}
			}
				
			public System.String JournalNumber
			{
				get
				{
					System.String data = entity.JournalNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalNumber = null;
					else entity.JournalNumber = Convert.ToString(value);
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
			

			private esAssetDepreciationPost entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetDepreciationPostQuery query)
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
				throw new Exception("esAssetDepreciationPost can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetDepreciationPost : esAssetDepreciationPost
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
	abstract public class esAssetDepreciationPostQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetDepreciationPostMetadata.Meta();
			}
		}	
		

		public esQueryItem AssetDepreciationPostId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.AssetDepreciationPostId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodNo
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.PeriodNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Year
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.Year, esSystemType.String);
			}
		} 
		
		public esQueryItem Month
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.Month, esSystemType.String);
			}
		} 
		
		public esQueryItem DepreciationDate
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.DepreciationDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CurrentAmount
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.CurrentAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DepreciationAmount
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.DepreciationAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AccumulationAmount
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.AccumulationAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsPosted
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.IsPosted, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem PostingId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.PostingId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PostedDate
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.PostedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem JournalId
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.JournalId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JournalNumber
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.JournalNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetDepreciationPostMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetDepreciationPostCollection")]
	public partial class AssetDepreciationPostCollection : esAssetDepreciationPostCollection, IEnumerable<AssetDepreciationPost>
	{
		public AssetDepreciationPostCollection()
		{

		}
		
		public static implicit operator List<AssetDepreciationPost>(AssetDepreciationPostCollection coll)
		{
			List<AssetDepreciationPost> list = new List<AssetDepreciationPost>();
			
			foreach (AssetDepreciationPost emp in coll)
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
				return  AssetDepreciationPostMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetDepreciationPostQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetDepreciationPost(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetDepreciationPost();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetDepreciationPostQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetDepreciationPostQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetDepreciationPostQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetDepreciationPost AddNew()
		{
			AssetDepreciationPost entity = base.AddNewEntity() as AssetDepreciationPost;
			
			return entity;
		}

		public AssetDepreciationPost FindByPrimaryKey(System.Int32 assetDepreciationPostId)
		{
			return base.FindByPrimaryKey(assetDepreciationPostId) as AssetDepreciationPost;
		}


		#region IEnumerable<AssetDepreciationPost> Members

		IEnumerator<AssetDepreciationPost> IEnumerable<AssetDepreciationPost>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetDepreciationPost;
			}
		}

		#endregion
		
		private AssetDepreciationPostQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetDepreciationPost' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetDepreciationPost ({AssetDepreciationPostId})")]
	[Serializable]
	public partial class AssetDepreciationPost : esAssetDepreciationPost
	{
		public AssetDepreciationPost()
		{

		}
	
		public AssetDepreciationPost(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetDepreciationPostMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetDepreciationPostQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetDepreciationPostQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetDepreciationPostQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetDepreciationPostQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetDepreciationPostQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetDepreciationPostQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetDepreciationPostQuery : esAssetDepreciationPostQuery
	{
		public AssetDepreciationPostQuery()
		{

		}		
		
		public AssetDepreciationPostQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetDepreciationPostQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetDepreciationPostMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetDepreciationPostMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.AssetDepreciationPostId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.AssetDepreciationPostId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.AssetID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.PeriodNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.PeriodNo;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.Year, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.Year;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.Month, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.Month;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.DepreciationDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.DepreciationDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.CurrentAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.CurrentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.DepreciationAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.DepreciationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.AccumulationAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.AccumulationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.IsPosted, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.IsPosted;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.PostingId, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.PostingId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.PostedDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.PostedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.JournalId, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.JournalId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.JournalNumber, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.JournalNumber;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetDepreciationPostMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetDepreciationPostMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetDepreciationPostMetadata Meta()
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
			 public const string AssetDepreciationPostId = "AssetDepreciationPostId";
			 public const string AssetID = "AssetID";
			 public const string PeriodNo = "PeriodNo";
			 public const string Year = "Year";
			 public const string Month = "Month";
			 public const string DepreciationDate = "DepreciationDate";
			 public const string CurrentAmount = "CurrentAmount";
			 public const string DepreciationAmount = "DepreciationAmount";
			 public const string AccumulationAmount = "AccumulationAmount";
			 public const string IsPosted = "IsPosted";
			 public const string PostingId = "PostingId";
			 public const string PostedDate = "PostedDate";
			 public const string JournalId = "JournalId";
			 public const string JournalNumber = "JournalNumber";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string AssetDepreciationPostId = "AssetDepreciationPostId";
			 public const string AssetID = "AssetID";
			 public const string PeriodNo = "PeriodNo";
			 public const string Year = "Year";
			 public const string Month = "Month";
			 public const string DepreciationDate = "DepreciationDate";
			 public const string CurrentAmount = "CurrentAmount";
			 public const string DepreciationAmount = "DepreciationAmount";
			 public const string AccumulationAmount = "AccumulationAmount";
			 public const string IsPosted = "IsPosted";
			 public const string PostingId = "PostingId";
			 public const string PostedDate = "PostedDate";
			 public const string JournalId = "JournalId";
			 public const string JournalNumber = "JournalNumber";
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
			lock (typeof(AssetDepreciationPostMetadata))
			{
				if(AssetDepreciationPostMetadata.mapDelegates == null)
				{
					AssetDepreciationPostMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetDepreciationPostMetadata.meta == null)
				{
					AssetDepreciationPostMetadata.meta = new AssetDepreciationPostMetadata();
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
				

				meta.AddTypeMap("AssetDepreciationPostId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Year", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Month", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepreciationDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("CurrentAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DepreciationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AccumulationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPosted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PostingId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PostedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("JournalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalNumber", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetDepreciationPost";
				meta.Destination = "AssetDepreciationPost";
				
				meta.spInsert = "proc_AssetDepreciationPostInsert";				
				meta.spUpdate = "proc_AssetDepreciationPostUpdate";		
				meta.spDelete = "proc_AssetDepreciationPostDelete";
				meta.spLoadAll = "proc_AssetDepreciationPostLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetDepreciationPostLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetDepreciationPostMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
