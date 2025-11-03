/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/4/2014 10:46:23 AM
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
	abstract public class esItemTariffRequestCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffRequestCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemTariffRequestCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffRequestQuery query)
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
			this.InitQuery(query as esItemTariffRequestQuery);
		}
		#endregion
		
		virtual public ItemTariffRequest DetachEntity(ItemTariffRequest entity)
		{
			return base.DetachEntity(entity) as ItemTariffRequest;
		}
		
		virtual public ItemTariffRequest AttachEntity(ItemTariffRequest entity)
		{
			return base.AttachEntity(entity) as ItemTariffRequest;
		}
		
		virtual public void Combine(ItemTariffRequestCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemTariffRequest this[int index]
		{
			get
			{
				return base[index] as ItemTariffRequest;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffRequest);
		}
	}



	[Serializable]
	abstract public class esItemTariffRequest : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffRequestQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffRequest()
		{

		}

		public esItemTariffRequest(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String tariffRequestNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String tariffRequestNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String tariffRequestNo)
		{
			esItemTariffRequestQuery query = this.GetDynamicQuery();
			query.Where(query.TariffRequestNo == tariffRequestNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String tariffRequestNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TariffRequestNo",tariffRequestNo);
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
						case "TariffRequestNo": this.str.TariffRequestNo = (string)value; break;							
						case "TariffRequestDate": this.str.TariffRequestDate = (string)value; break;							
						case "SRTariffType": this.str.SRTariffType = (string)value; break;							
						case "SRItemType": this.str.SRItemType = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "StartingDate": this.str.StartingDate = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "VoidDate": this.str.VoidDate = (string)value; break;							
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;							
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TariffRequestDate":
						
							if (value == null || value is System.DateTime)
								this.TariffRequestDate = (System.DateTime?)value;
							break;
						
						case "StartingDate":
						
							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "VoidDate":
						
							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "ApprovedDate":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDate = (System.DateTime?)value;
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
		/// Maps to ItemTariffRequest.TariffRequestNo
		/// </summary>
		virtual public System.String TariffRequestNo
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.TariffRequestNo);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.TariffRequestNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.TariffRequestDate
		/// </summary>
		virtual public System.DateTime? TariffRequestDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.TariffRequestDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.TariffRequestDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.SRTariffType);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.SRTariffType, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.SRItemType
		/// </summary>
		virtual public System.String SRItemType
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.SRItemType);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.SRItemType, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.StartingDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.StartingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequestMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffRequestMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.VoidDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequestMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffRequestMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.ApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffRequestMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemTariffRequest entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TariffRequestNo
			{
				get
				{
					System.String data = entity.TariffRequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffRequestNo = null;
					else entity.TariffRequestNo = Convert.ToString(value);
				}
			}
				
			public System.String TariffRequestDate
			{
				get
				{
					System.DateTime? data = entity.TariffRequestDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffRequestDate = null;
					else entity.TariffRequestDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
				}
			}
				
			public System.String SRItemType
			{
				get
				{
					System.String data = entity.SRItemType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemType = null;
					else entity.SRItemType = Convert.ToString(value);
				}
			}
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
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
				
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
				
			public System.String VoidDate
			{
				get
				{
					System.DateTime? data = entity.VoidDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDate = null;
					else entity.VoidDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
				
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
				
			public System.String ApprovedDate
			{
				get
				{
					System.DateTime? data = entity.ApprovedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDate = null;
					else entity.ApprovedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
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
			

			private esItemTariffRequest entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffRequestQuery query)
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
				throw new Exception("esItemTariffRequest can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemTariffRequest : esItemTariffRequest
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
	abstract public class esItemTariffRequestQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestMetadata.Meta();
			}
		}	
		

		public esQueryItem TariffRequestNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffRequestDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.TariffRequestDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRItemType
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.SRItemType, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffRequestCollection")]
	public partial class ItemTariffRequestCollection : esItemTariffRequestCollection, IEnumerable<ItemTariffRequest>
	{
		public ItemTariffRequestCollection()
		{

		}
		
		public static implicit operator List<ItemTariffRequest>(ItemTariffRequestCollection coll)
		{
			List<ItemTariffRequest> list = new List<ItemTariffRequest>();
			
			foreach (ItemTariffRequest emp in coll)
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
				return  ItemTariffRequestMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffRequest(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffRequest();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemTariffRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemTariffRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemTariffRequest AddNew()
		{
			ItemTariffRequest entity = base.AddNewEntity() as ItemTariffRequest;
			
			return entity;
		}

		public ItemTariffRequest FindByPrimaryKey(System.String tariffRequestNo)
		{
			return base.FindByPrimaryKey(tariffRequestNo) as ItemTariffRequest;
		}


		#region IEnumerable<ItemTariffRequest> Members

		IEnumerator<ItemTariffRequest> IEnumerable<ItemTariffRequest>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffRequest;
			}
		}

		#endregion
		
		private ItemTariffRequestQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffRequest' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemTariffRequest ({TariffRequestNo})")]
	[Serializable]
	public partial class ItemTariffRequest : esItemTariffRequest
	{
		public ItemTariffRequest()
		{

		}
	
		public ItemTariffRequest(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestMetadata.Meta();
			}
		}
		
		
		
		override protected esItemTariffRequestQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemTariffRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemTariffRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemTariffRequestQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemTariffRequestQuery : esItemTariffRequestQuery
	{
		public ItemTariffRequestQuery()
		{

		}		
		
		public ItemTariffRequestQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemTariffRequestQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemTariffRequestMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffRequestMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.TariffRequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.TariffRequestDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.TariffRequestDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.SRTariffType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.SRTariffType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.SRItemType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.SRItemType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.ClassID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.StartingDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.StartingDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.VoidDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.VoidByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.IsApproved, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.ApprovedDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.ApprovedByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemTariffRequestMetadata Meta()
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
			 public const string TariffRequestNo = "TariffRequestNo";
			 public const string TariffRequestDate = "TariffRequestDate";
			 public const string SRTariffType = "SRTariffType";
			 public const string SRItemType = "SRItemType";
			 public const string ClassID = "ClassID";
			 public const string StartingDate = "StartingDate";
			 public const string Notes = "Notes";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TariffRequestNo = "TariffRequestNo";
			 public const string TariffRequestDate = "TariffRequestDate";
			 public const string SRTariffType = "SRTariffType";
			 public const string SRItemType = "SRItemType";
			 public const string ClassID = "ClassID";
			 public const string StartingDate = "StartingDate";
			 public const string Notes = "Notes";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
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
			lock (typeof(ItemTariffRequestMetadata))
			{
				if(ItemTariffRequestMetadata.mapDelegates == null)
				{
					ItemTariffRequestMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemTariffRequestMetadata.meta == null)
				{
					ItemTariffRequestMetadata.meta = new ItemTariffRequestMetadata();
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
				

				meta.AddTypeMap("TariffRequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffRequestDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemTariffRequest";
				meta.Destination = "ItemTariffRequest";
				
				meta.spInsert = "proc_ItemTariffRequestInsert";				
				meta.spUpdate = "proc_ItemTariffRequestUpdate";		
				meta.spDelete = "proc_ItemTariffRequestDelete";
				meta.spLoadAll = "proc_ItemTariffRequestLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffRequestLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffRequestMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
