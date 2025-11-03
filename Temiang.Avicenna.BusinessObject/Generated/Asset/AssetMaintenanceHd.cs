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
	abstract public class esAssetMaintenanceHdCollection : esEntityCollectionWAuditLog
	{
		public esAssetMaintenanceHdCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetMaintenanceHdCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetMaintenanceHdQuery query)
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
			this.InitQuery(query as esAssetMaintenanceHdQuery);
		}
		#endregion
		
		virtual public AssetMaintenanceHd DetachEntity(AssetMaintenanceHd entity)
		{
			return base.DetachEntity(entity) as AssetMaintenanceHd;
		}
		
		virtual public AssetMaintenanceHd AttachEntity(AssetMaintenanceHd entity)
		{
			return base.AttachEntity(entity) as AssetMaintenanceHd;
		}
		
		virtual public void Combine(AssetMaintenanceHdCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetMaintenanceHd this[int index]
		{
			get
			{
				return base[index] as AssetMaintenanceHd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetMaintenanceHd);
		}
	}



	[Serializable]
	abstract public class esAssetMaintenanceHd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetMaintenanceHdQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetMaintenanceHd()
		{

		}

		public esAssetMaintenanceHd(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
		{
			esAssetMaintenanceHdQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "MaintenanceDate": this.str.MaintenanceDate = (string)value; break;							
						case "JobOrderNo": this.str.JobOrderNo = (string)value; break;							
						case "AssetID": this.str.AssetID = (string)value; break;							
						case "SRMaintenanceType": this.str.SRMaintenanceType = (string)value; break;							
						case "MaintenanceBy": this.str.MaintenanceBy = (string)value; break;							
						case "Condition": this.str.Condition = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "NextMaintenanceDate": this.str.NextMaintenanceDate = (string)value; break;							
						case "IsPosted": this.str.IsPosted = (string)value; break;							
						case "IsDeleted": this.str.IsDeleted = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MaintenanceDate":
						
							if (value == null || value is System.DateTime)
								this.MaintenanceDate = (System.DateTime?)value;
							break;
						
						case "Condition":
						
							if (value == null || value is System.Decimal)
								this.Condition = (System.Decimal?)value;
							break;
						
						case "NextMaintenanceDate":
						
							if (value == null || value is System.DateTime)
								this.NextMaintenanceDate = (System.DateTime?)value;
							break;
						
						case "IsPosted":
						
							if (value == null || value is System.Boolean)
								this.IsPosted = (System.Boolean?)value;
							break;
						
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
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
		/// Maps to AssetMaintenanceHd.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceHdMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceHdMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.MaintenanceDate
		/// </summary>
		virtual public System.DateTime? MaintenanceDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMaintenanceHdMetadata.ColumnNames.MaintenanceDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetMaintenanceHdMetadata.ColumnNames.MaintenanceDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.JobOrderNo
		/// </summary>
		virtual public System.String JobOrderNo
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceHdMetadata.ColumnNames.JobOrderNo);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceHdMetadata.ColumnNames.JobOrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceHdMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceHdMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.SRMaintenanceType
		/// </summary>
		virtual public System.String SRMaintenanceType
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceHdMetadata.ColumnNames.SRMaintenanceType);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceHdMetadata.ColumnNames.SRMaintenanceType, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.MaintenanceBy
		/// </summary>
		virtual public System.String MaintenanceBy
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceHdMetadata.ColumnNames.MaintenanceBy);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceHdMetadata.ColumnNames.MaintenanceBy, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.Condition
		/// </summary>
		virtual public System.Decimal? Condition
		{
			get
			{
				return base.GetSystemDecimal(AssetMaintenanceHdMetadata.ColumnNames.Condition);
			}
			
			set
			{
				base.SetSystemDecimal(AssetMaintenanceHdMetadata.ColumnNames.Condition, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceHdMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceHdMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.NextMaintenanceDate
		/// </summary>
		virtual public System.DateTime? NextMaintenanceDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMaintenanceHdMetadata.ColumnNames.NextMaintenanceDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetMaintenanceHdMetadata.ColumnNames.NextMaintenanceDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.IsPosted
		/// </summary>
		virtual public System.Boolean? IsPosted
		{
			get
			{
				return base.GetSystemBoolean(AssetMaintenanceHdMetadata.ColumnNames.IsPosted);
			}
			
			set
			{
				base.SetSystemBoolean(AssetMaintenanceHdMetadata.ColumnNames.IsPosted, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(AssetMaintenanceHdMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				base.SetSystemBoolean(AssetMaintenanceHdMetadata.ColumnNames.IsDeleted, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetMaintenanceHdMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetMaintenanceHdMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceHd.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceHdMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceHdMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetMaintenanceHd entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String MaintenanceDate
			{
				get
				{
					System.DateTime? data = entity.MaintenanceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaintenanceDate = null;
					else entity.MaintenanceDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String JobOrderNo
			{
				get
				{
					System.String data = entity.JobOrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobOrderNo = null;
					else entity.JobOrderNo = Convert.ToString(value);
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
				
			public System.String SRMaintenanceType
			{
				get
				{
					System.String data = entity.SRMaintenanceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaintenanceType = null;
					else entity.SRMaintenanceType = Convert.ToString(value);
				}
			}
				
			public System.String MaintenanceBy
			{
				get
				{
					System.String data = entity.MaintenanceBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaintenanceBy = null;
					else entity.MaintenanceBy = Convert.ToString(value);
				}
			}
				
			public System.String Condition
			{
				get
				{
					System.Decimal? data = entity.Condition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Condition = null;
					else entity.Condition = Convert.ToDecimal(value);
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
				
			public System.String NextMaintenanceDate
			{
				get
				{
					System.DateTime? data = entity.NextMaintenanceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NextMaintenanceDate = null;
					else entity.NextMaintenanceDate = Convert.ToDateTime(value);
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
				
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
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
			

			private esAssetMaintenanceHd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetMaintenanceHdQuery query)
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
				throw new Exception("esAssetMaintenanceHd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetMaintenanceHd : esAssetMaintenanceHd
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
	abstract public class esAssetMaintenanceHdQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetMaintenanceHdMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem MaintenanceDate
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.MaintenanceDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem JobOrderNo
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.JobOrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMaintenanceType
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.SRMaintenanceType, esSystemType.String);
			}
		} 
		
		public esQueryItem MaintenanceBy
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.MaintenanceBy, esSystemType.String);
			}
		} 
		
		public esQueryItem Condition
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.Condition, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem NextMaintenanceDate
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.NextMaintenanceDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsPosted
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.IsPosted, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceHdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetMaintenanceHdCollection")]
	public partial class AssetMaintenanceHdCollection : esAssetMaintenanceHdCollection, IEnumerable<AssetMaintenanceHd>
	{
		public AssetMaintenanceHdCollection()
		{

		}
		
		public static implicit operator List<AssetMaintenanceHd>(AssetMaintenanceHdCollection coll)
		{
			List<AssetMaintenanceHd> list = new List<AssetMaintenanceHd>();
			
			foreach (AssetMaintenanceHd emp in coll)
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
				return  AssetMaintenanceHdMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMaintenanceHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetMaintenanceHd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetMaintenanceHd();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetMaintenanceHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMaintenanceHdQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetMaintenanceHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetMaintenanceHd AddNew()
		{
			AssetMaintenanceHd entity = base.AddNewEntity() as AssetMaintenanceHd;
			
			return entity;
		}

		public AssetMaintenanceHd FindByPrimaryKey(System.String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as AssetMaintenanceHd;
		}


		#region IEnumerable<AssetMaintenanceHd> Members

		IEnumerator<AssetMaintenanceHd> IEnumerable<AssetMaintenanceHd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetMaintenanceHd;
			}
		}

		#endregion
		
		private AssetMaintenanceHdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetMaintenanceHd' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetMaintenanceHd ({TransactionNo})")]
	[Serializable]
	public partial class AssetMaintenanceHd : esAssetMaintenanceHd
	{
		public AssetMaintenanceHd()
		{

		}
	
		public AssetMaintenanceHd(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetMaintenanceHdMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetMaintenanceHdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMaintenanceHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetMaintenanceHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMaintenanceHdQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetMaintenanceHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetMaintenanceHdQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetMaintenanceHdQuery : esAssetMaintenanceHdQuery
	{
		public AssetMaintenanceHdQuery()
		{

		}		
		
		public AssetMaintenanceHdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetMaintenanceHdQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetMaintenanceHdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetMaintenanceHdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.MaintenanceDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.MaintenanceDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.JobOrderNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.JobOrderNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.AssetID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.SRMaintenanceType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.SRMaintenanceType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.MaintenanceBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.MaintenanceBy;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.Condition, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.Condition;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.NextMaintenanceDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.NextMaintenanceDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.IsPosted, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.IsPosted;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.IsDeleted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.IsDeleted;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceHdMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceHdMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetMaintenanceHdMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string MaintenanceDate = "MaintenanceDate";
			 public const string JobOrderNo = "JobOrderNo";
			 public const string AssetID = "AssetID";
			 public const string SRMaintenanceType = "SRMaintenanceType";
			 public const string MaintenanceBy = "MaintenanceBy";
			 public const string Condition = "Condition";
			 public const string Notes = "Notes";
			 public const string NextMaintenanceDate = "NextMaintenanceDate";
			 public const string IsPosted = "IsPosted";
			 public const string IsDeleted = "IsDeleted";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string MaintenanceDate = "MaintenanceDate";
			 public const string JobOrderNo = "JobOrderNo";
			 public const string AssetID = "AssetID";
			 public const string SRMaintenanceType = "SRMaintenanceType";
			 public const string MaintenanceBy = "MaintenanceBy";
			 public const string Condition = "Condition";
			 public const string Notes = "Notes";
			 public const string NextMaintenanceDate = "NextMaintenanceDate";
			 public const string IsPosted = "IsPosted";
			 public const string IsDeleted = "IsDeleted";
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
			lock (typeof(AssetMaintenanceHdMetadata))
			{
				if(AssetMaintenanceHdMetadata.mapDelegates == null)
				{
					AssetMaintenanceHdMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetMaintenanceHdMetadata.meta == null)
				{
					AssetMaintenanceHdMetadata.meta = new AssetMaintenanceHdMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaintenanceDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("JobOrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMaintenanceType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MaintenanceBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Condition", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NextMaintenanceDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("IsPosted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetMaintenanceHd";
				meta.Destination = "AssetMaintenanceHd";
				
				meta.spInsert = "proc_AssetMaintenanceHdInsert";				
				meta.spUpdate = "proc_AssetMaintenanceHdUpdate";		
				meta.spDelete = "proc_AssetMaintenanceHdDelete";
				meta.spLoadAll = "proc_AssetMaintenanceHdLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetMaintenanceHdLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetMaintenanceHdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
