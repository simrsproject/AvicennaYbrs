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
	abstract public class esAssetMaintenanceOrderCollection : esEntityCollectionWAuditLog
	{
		public esAssetMaintenanceOrderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetMaintenanceOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetMaintenanceOrderQuery query)
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
			this.InitQuery(query as esAssetMaintenanceOrderQuery);
		}
		#endregion
		
		virtual public AssetMaintenanceOrder DetachEntity(AssetMaintenanceOrder entity)
		{
			return base.DetachEntity(entity) as AssetMaintenanceOrder;
		}
		
		virtual public AssetMaintenanceOrder AttachEntity(AssetMaintenanceOrder entity)
		{
			return base.AttachEntity(entity) as AssetMaintenanceOrder;
		}
		
		virtual public void Combine(AssetMaintenanceOrderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetMaintenanceOrder this[int index]
		{
			get
			{
				return base[index] as AssetMaintenanceOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetMaintenanceOrder);
		}
	}



	[Serializable]
	abstract public class esAssetMaintenanceOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetMaintenanceOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetMaintenanceOrder()
		{

		}

		public esAssetMaintenanceOrder(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String jobOrderNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(jobOrderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(jobOrderNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String jobOrderNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(jobOrderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(jobOrderNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String jobOrderNo)
		{
			esAssetMaintenanceOrderQuery query = this.GetDynamicQuery();
			query.Where(query.JobOrderNo == jobOrderNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String jobOrderNo)
		{
			esParameters parms = new esParameters();
			parms.Add("JobOrderNo",jobOrderNo);
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
						case "JobOrderNo": this.str.JobOrderNo = (string)value; break;							
						case "OrderedDate": this.str.OrderedDate = (string)value; break;							
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;							
						case "FromLocationID": this.str.FromLocationID = (string)value; break;							
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;							
						case "AssetID": this.str.AssetID = (string)value; break;							
						case "RequestBy": this.str.RequestBy = (string)value; break;							
						case "RequestDate": this.str.RequestDate = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsPosted": this.str.IsPosted = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderedDate":
						
							if (value == null || value is System.DateTime)
								this.OrderedDate = (System.DateTime?)value;
							break;
						
						case "RequestDate":
						
							if (value == null || value is System.DateTime)
								this.RequestDate = (System.DateTime?)value;
							break;
						
						case "IsPosted":
						
							if (value == null || value is System.Boolean)
								this.IsPosted = (System.Boolean?)value;
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
		/// Maps to AssetMaintenanceOrder.JobOrderNo
		/// </summary>
		virtual public System.String JobOrderNo
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.JobOrderNo);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.JobOrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.OrderedDate
		/// </summary>
		virtual public System.DateTime? OrderedDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMaintenanceOrderMetadata.ColumnNames.OrderedDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetMaintenanceOrderMetadata.ColumnNames.OrderedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.FromServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.FromLocationID
		/// </summary>
		virtual public System.String FromLocationID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.FromLocationID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.FromLocationID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.ToServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.RequestBy
		/// </summary>
		virtual public System.String RequestBy
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.RequestBy);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.RequestBy, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.RequestDate
		/// </summary>
		virtual public System.DateTime? RequestDate
		{
			get
			{
				return base.GetSystemDateTime(AssetMaintenanceOrderMetadata.ColumnNames.RequestDate);
			}
			
			set
			{
				base.SetSystemDateTime(AssetMaintenanceOrderMetadata.ColumnNames.RequestDate, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.IsPosted
		/// </summary>
		virtual public System.Boolean? IsPosted
		{
			get
			{
				return base.GetSystemBoolean(AssetMaintenanceOrderMetadata.ColumnNames.IsPosted);
			}
			
			set
			{
				base.SetSystemBoolean(AssetMaintenanceOrderMetadata.ColumnNames.IsPosted, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetMaintenanceOrder.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetMaintenanceOrder entity)
			{
				this.entity = entity;
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
				
			public System.String OrderedDate
			{
				get
				{
					System.DateTime? data = entity.OrderedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderedDate = null;
					else entity.OrderedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String FromServiceUnitID
			{
				get
				{
					System.String data = entity.FromServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromServiceUnitID = null;
					else entity.FromServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String FromLocationID
			{
				get
				{
					System.String data = entity.FromLocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromLocationID = null;
					else entity.FromLocationID = Convert.ToString(value);
				}
			}
				
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
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
				
			public System.String RequestBy
			{
				get
				{
					System.String data = entity.RequestBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestBy = null;
					else entity.RequestBy = Convert.ToString(value);
				}
			}
				
			public System.String RequestDate
			{
				get
				{
					System.DateTime? data = entity.RequestDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDate = null;
					else entity.RequestDate = Convert.ToDateTime(value);
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
			

			private esAssetMaintenanceOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetMaintenanceOrderQuery query)
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
				throw new Exception("esAssetMaintenanceOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetMaintenanceOrder : esAssetMaintenanceOrder
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
	abstract public class esAssetMaintenanceOrderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetMaintenanceOrderMetadata.Meta();
			}
		}	
		

		public esQueryItem JobOrderNo
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.JobOrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderedDate
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.OrderedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem FromLocationID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.FromLocationID, esSystemType.String);
			}
		} 
		
		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem RequestBy
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.RequestBy, esSystemType.String);
			}
		} 
		
		public esQueryItem RequestDate
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.RequestDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPosted
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.IsPosted, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetMaintenanceOrderCollection")]
	public partial class AssetMaintenanceOrderCollection : esAssetMaintenanceOrderCollection, IEnumerable<AssetMaintenanceOrder>
	{
		public AssetMaintenanceOrderCollection()
		{

		}
		
		public static implicit operator List<AssetMaintenanceOrder>(AssetMaintenanceOrderCollection coll)
		{
			List<AssetMaintenanceOrder> list = new List<AssetMaintenanceOrder>();
			
			foreach (AssetMaintenanceOrder emp in coll)
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
				return  AssetMaintenanceOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMaintenanceOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetMaintenanceOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetMaintenanceOrder();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetMaintenanceOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMaintenanceOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetMaintenanceOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetMaintenanceOrder AddNew()
		{
			AssetMaintenanceOrder entity = base.AddNewEntity() as AssetMaintenanceOrder;
			
			return entity;
		}

		public AssetMaintenanceOrder FindByPrimaryKey(System.String jobOrderNo)
		{
			return base.FindByPrimaryKey(jobOrderNo) as AssetMaintenanceOrder;
		}


		#region IEnumerable<AssetMaintenanceOrder> Members

		IEnumerator<AssetMaintenanceOrder> IEnumerable<AssetMaintenanceOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetMaintenanceOrder;
			}
		}

		#endregion
		
		private AssetMaintenanceOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetMaintenanceOrder' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetMaintenanceOrder ({JobOrderNo})")]
	[Serializable]
	public partial class AssetMaintenanceOrder : esAssetMaintenanceOrder
	{
		public AssetMaintenanceOrder()
		{

		}
	
		public AssetMaintenanceOrder(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetMaintenanceOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetMaintenanceOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetMaintenanceOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetMaintenanceOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetMaintenanceOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetMaintenanceOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetMaintenanceOrderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetMaintenanceOrderQuery : esAssetMaintenanceOrderQuery
	{
		public AssetMaintenanceOrderQuery()
		{

		}		
		
		public AssetMaintenanceOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetMaintenanceOrderQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetMaintenanceOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetMaintenanceOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.JobOrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.JobOrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.OrderedDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.OrderedDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.FromServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.FromLocationID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.FromLocationID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.ToServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.AssetID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.AssetID;
			c.CharacterMaxLength = 30;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.RequestBy, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.RequestBy;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.RequestDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.RequestDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.IsPosted, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.IsPosted;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetMaintenanceOrderMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetMaintenanceOrderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetMaintenanceOrderMetadata Meta()
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
			 public const string JobOrderNo = "JobOrderNo";
			 public const string OrderedDate = "OrderedDate";
			 public const string FromServiceUnitID = "FromServiceUnitID";
			 public const string FromLocationID = "FromLocationID";
			 public const string ToServiceUnitID = "ToServiceUnitID";
			 public const string AssetID = "AssetID";
			 public const string RequestBy = "RequestBy";
			 public const string RequestDate = "RequestDate";
			 public const string Notes = "Notes";
			 public const string IsPosted = "IsPosted";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string JobOrderNo = "JobOrderNo";
			 public const string OrderedDate = "OrderedDate";
			 public const string FromServiceUnitID = "FromServiceUnitID";
			 public const string FromLocationID = "FromLocationID";
			 public const string ToServiceUnitID = "ToServiceUnitID";
			 public const string AssetID = "AssetID";
			 public const string RequestBy = "RequestBy";
			 public const string RequestDate = "RequestDate";
			 public const string Notes = "Notes";
			 public const string IsPosted = "IsPosted";
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
			lock (typeof(AssetMaintenanceOrderMetadata))
			{
				if(AssetMaintenanceOrderMetadata.mapDelegates == null)
				{
					AssetMaintenanceOrderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetMaintenanceOrderMetadata.meta == null)
				{
					AssetMaintenanceOrderMetadata.meta = new AssetMaintenanceOrderMetadata();
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
				

				meta.AddTypeMap("JobOrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderedDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromLocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPosted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetMaintenanceOrder";
				meta.Destination = "AssetMaintenanceOrder";
				
				meta.spInsert = "proc_AssetMaintenanceOrderInsert";				
				meta.spUpdate = "proc_AssetMaintenanceOrderUpdate";		
				meta.spDelete = "proc_AssetMaintenanceOrderDelete";
				meta.spLoadAll = "proc_AssetMaintenanceOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetMaintenanceOrderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetMaintenanceOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
