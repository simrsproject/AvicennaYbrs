/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/30/2014 1:44:16 PM
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
	abstract public class esEventMealOrderCollection : esEntityCollectionWAuditLog
	{
		public esEventMealOrderCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EventMealOrderCollection";
		}

		#region Query Logic
		protected void InitQuery(esEventMealOrderQuery query)
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
			this.InitQuery(query as esEventMealOrderQuery);
		}
		#endregion
		
		virtual public EventMealOrder DetachEntity(EventMealOrder entity)
		{
			return base.DetachEntity(entity) as EventMealOrder;
		}
		
		virtual public EventMealOrder AttachEntity(EventMealOrder entity)
		{
			return base.AttachEntity(entity) as EventMealOrder;
		}
		
		virtual public void Combine(EventMealOrderCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EventMealOrder this[int index]
		{
			get
			{
				return base[index] as EventMealOrder;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EventMealOrder);
		}
	}



	[Serializable]
	abstract public class esEventMealOrder : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEventMealOrderQuery GetDynamicQuery()
		{
			return null;
		}

		public esEventMealOrder()
		{

		}

		public esEventMealOrder(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String orderNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String orderNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(orderNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String orderNo)
		{
			esEventMealOrderQuery query = this.GetDynamicQuery();
			query.Where(query.OrderNo == orderNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String orderNo)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderNo",orderNo);
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
						case "OrderNo": this.str.OrderNo = (string)value; break;							
						case "OrderDate": this.str.OrderDate = (string)value; break;							
						case "EventDate": this.str.EventDate = (string)value; break;							
						case "EventTime": this.str.EventTime = (string)value; break;							
						case "EventName": this.str.EventName = (string)value; break;							
						case "Pic": this.str.Pic = (string)value; break;							
						case "NoOfParticipant": this.str.NoOfParticipant = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderDate":
						
							if (value == null || value is System.DateTime)
								this.OrderDate = (System.DateTime?)value;
							break;
						
						case "EventDate":
						
							if (value == null || value is System.DateTime)
								this.EventDate = (System.DateTime?)value;
							break;
						
						case "NoOfParticipant":
						
							if (value == null || value is System.Int16)
								this.NoOfParticipant = (System.Int16?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to EventMealOrder.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(EventMealOrderMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.OrderDate
		/// </summary>
		virtual public System.DateTime? OrderDate
		{
			get
			{
				return base.GetSystemDateTime(EventMealOrderMetadata.ColumnNames.OrderDate);
			}
			
			set
			{
				base.SetSystemDateTime(EventMealOrderMetadata.ColumnNames.OrderDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.EventDate
		/// </summary>
		virtual public System.DateTime? EventDate
		{
			get
			{
				return base.GetSystemDateTime(EventMealOrderMetadata.ColumnNames.EventDate);
			}
			
			set
			{
				base.SetSystemDateTime(EventMealOrderMetadata.ColumnNames.EventDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.EventTime
		/// </summary>
		virtual public System.String EventTime
		{
			get
			{
				return base.GetSystemString(EventMealOrderMetadata.ColumnNames.EventTime);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderMetadata.ColumnNames.EventTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.EventName
		/// </summary>
		virtual public System.String EventName
		{
			get
			{
				return base.GetSystemString(EventMealOrderMetadata.ColumnNames.EventName);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderMetadata.ColumnNames.EventName, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.Pic
		/// </summary>
		virtual public System.String Pic
		{
			get
			{
				return base.GetSystemString(EventMealOrderMetadata.ColumnNames.Pic);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderMetadata.ColumnNames.Pic, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.NoOfParticipant
		/// </summary>
		virtual public System.Int16? NoOfParticipant
		{
			get
			{
				return base.GetSystemInt16(EventMealOrderMetadata.ColumnNames.NoOfParticipant);
			}
			
			set
			{
				base.SetSystemInt16(EventMealOrderMetadata.ColumnNames.NoOfParticipant, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EventMealOrderMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EventMealOrderMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(EventMealOrderMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EventMealOrderMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(EventMealOrderMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EventMealOrderMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EventMealOrderMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EventMealOrder.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EventMealOrderMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EventMealOrderMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEventMealOrder entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderNo
			{
				get
				{
					System.String data = entity.OrderNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNo = null;
					else entity.OrderNo = Convert.ToString(value);
				}
			}
				
			public System.String OrderDate
			{
				get
				{
					System.DateTime? data = entity.OrderDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderDate = null;
					else entity.OrderDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EventDate
			{
				get
				{
					System.DateTime? data = entity.EventDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EventDate = null;
					else entity.EventDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EventTime
			{
				get
				{
					System.String data = entity.EventTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EventTime = null;
					else entity.EventTime = Convert.ToString(value);
				}
			}
				
			public System.String EventName
			{
				get
				{
					System.String data = entity.EventName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EventName = null;
					else entity.EventName = Convert.ToString(value);
				}
			}
				
			public System.String Pic
			{
				get
				{
					System.String data = entity.Pic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pic = null;
					else entity.Pic = Convert.ToString(value);
				}
			}
				
			public System.String NoOfParticipant
			{
				get
				{
					System.Int16? data = entity.NoOfParticipant;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoOfParticipant = null;
					else entity.NoOfParticipant = Convert.ToInt16(value);
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
			

			private esEventMealOrder entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEventMealOrderQuery query)
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
				throw new Exception("esEventMealOrder can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EventMealOrder : esEventMealOrder
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
	abstract public class esEventMealOrderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EventMealOrderMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderDate
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.OrderDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EventDate
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.EventDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EventTime
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.EventTime, esSystemType.String);
			}
		} 
		
		public esQueryItem EventName
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.EventName, esSystemType.String);
			}
		} 
		
		public esQueryItem Pic
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.Pic, esSystemType.String);
			}
		} 
		
		public esQueryItem NoOfParticipant
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.NoOfParticipant, esSystemType.Int16);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EventMealOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EventMealOrderCollection")]
	public partial class EventMealOrderCollection : esEventMealOrderCollection, IEnumerable<EventMealOrder>
	{
		public EventMealOrderCollection()
		{

		}
		
		public static implicit operator List<EventMealOrder>(EventMealOrderCollection coll)
		{
			List<EventMealOrder> list = new List<EventMealOrder>();
			
			foreach (EventMealOrder emp in coll)
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
				return  EventMealOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EventMealOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EventMealOrder(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EventMealOrder();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EventMealOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EventMealOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EventMealOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EventMealOrder AddNew()
		{
			EventMealOrder entity = base.AddNewEntity() as EventMealOrder;
			
			return entity;
		}

		public EventMealOrder FindByPrimaryKey(System.String orderNo)
		{
			return base.FindByPrimaryKey(orderNo) as EventMealOrder;
		}


		#region IEnumerable<EventMealOrder> Members

		IEnumerator<EventMealOrder> IEnumerable<EventMealOrder>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EventMealOrder;
			}
		}

		#endregion
		
		private EventMealOrderQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EventMealOrder' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EventMealOrder ({OrderNo})")]
	[Serializable]
	public partial class EventMealOrder : esEventMealOrder
	{
		public EventMealOrder()
		{

		}
	
		public EventMealOrder(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EventMealOrderMetadata.Meta();
			}
		}
		
		
		
		override protected esEventMealOrderQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EventMealOrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EventMealOrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EventMealOrderQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EventMealOrderQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EventMealOrderQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EventMealOrderQuery : esEventMealOrderQuery
	{
		public EventMealOrderQuery()
		{

		}		
		
		public EventMealOrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EventMealOrderQuery";
        }
		
			
	}


	[Serializable]
	public partial class EventMealOrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EventMealOrderMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.OrderDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.OrderDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.EventDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.EventDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.EventTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.EventTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.EventName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.EventName;
			c.CharacterMaxLength = 250;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.Pic, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.Pic;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.NoOfParticipant, 6, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.NoOfParticipant;
			c.NumericPrecision = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.IsApproved, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EventMealOrderMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EventMealOrderMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EventMealOrderMetadata Meta()
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
			 public const string OrderNo = "OrderNo";
			 public const string OrderDate = "OrderDate";
			 public const string EventDate = "EventDate";
			 public const string EventTime = "EventTime";
			 public const string EventName = "EventName";
			 public const string Pic = "Pic";
			 public const string NoOfParticipant = "NoOfParticipant";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderNo = "OrderNo";
			 public const string OrderDate = "OrderDate";
			 public const string EventDate = "EventDate";
			 public const string EventTime = "EventTime";
			 public const string EventName = "EventName";
			 public const string Pic = "Pic";
			 public const string NoOfParticipant = "NoOfParticipant";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
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
			lock (typeof(EventMealOrderMetadata))
			{
				if(EventMealOrderMetadata.mapDelegates == null)
				{
					EventMealOrderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EventMealOrderMetadata.meta == null)
				{
					EventMealOrderMetadata.meta = new EventMealOrderMetadata();
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
				

				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EventDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EventTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EventName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoOfParticipant", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EventMealOrder";
				meta.Destination = "EventMealOrder";
				
				meta.spInsert = "proc_EventMealOrderInsert";				
				meta.spUpdate = "proc_EventMealOrderUpdate";		
				meta.spDelete = "proc_EventMealOrderDelete";
				meta.spLoadAll = "proc_EventMealOrderLoadAll";
				meta.spLoadByPrimaryKey = "proc_EventMealOrderLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EventMealOrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
