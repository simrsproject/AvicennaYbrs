/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/2/2021 3:20:45 PM
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
	abstract public class esServiceRoomCollection : esEntityCollectionWAuditLog
	{
		public esServiceRoomCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ServiceRoomCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceRoomQuery query)
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
			this.InitQuery(query as esServiceRoomQuery);
		}
		#endregion
		
		virtual public ServiceRoom DetachEntity(ServiceRoom entity)
		{
			return base.DetachEntity(entity) as ServiceRoom;
		}
		
		virtual public ServiceRoom AttachEntity(ServiceRoom entity)
		{
			return base.AttachEntity(entity) as ServiceRoom;
		}
		
		virtual public void Combine(ServiceRoomCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceRoom this[int index]
		{
			get
			{
				return base[index] as ServiceRoom;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceRoom);
		}
	}



	[Serializable]
	abstract public class esServiceRoom : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceRoomQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceRoom()
		{

		}

		public esServiceRoom(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String roomID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(roomID);
			else
				return LoadByPrimaryKeyStoredProcedure(roomID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String roomID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(roomID);
			else
				return LoadByPrimaryKeyStoredProcedure(roomID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String roomID)
		{
			esServiceRoomQuery query = this.GetDynamicQuery();
			query.Where(query.RoomID == roomID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String roomID)
		{
			esParameters parms = new esParameters();
			parms.Add("RoomID",roomID);
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
						case "RoomID": this.str.RoomID = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "RoomName": this.str.RoomName = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsOperatingRoom": this.str.IsOperatingRoom = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "ParamedicID1": this.str.ParamedicID1 = (string)value; break;							
						case "ParamedicID2": this.str.ParamedicID2 = (string)value; break;							
						case "NumberOfBeds": this.str.NumberOfBeds = (string)value; break;							
						case "ItemBookedID": this.str.ItemBookedID = (string)value; break;							
						case "TariffDiscountForRoomIn": this.str.TariffDiscountForRoomIn = (string)value; break;							
						case "SRFloor": this.str.SRFloor = (string)value; break;							
						case "IsBpjs": this.str.IsBpjs = (string)value; break;							
						case "SRGenderType": this.str.SRGenderType = (string)value; break;							
						case "IsShowOnBookingOT": this.str.IsShowOnBookingOT = (string)value; break;							
						case "IsResetPrice": this.str.IsResetPrice = (string)value; break;							
						case "IsIsolationRoom": this.str.IsIsolationRoom = (string)value; break;							
						case "IsNegativePressureRoom": this.str.IsNegativePressureRoom = (string)value; break;							
						case "IsPandemicRoom": this.str.IsPandemicRoom = (string)value; break;							
						case "IsVentilator": this.str.IsVentilator = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsOperatingRoom":
						
							if (value == null || value is System.Boolean)
								this.IsOperatingRoom = (System.Boolean?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "NumberOfBeds":
						
							if (value == null || value is System.Int16)
								this.NumberOfBeds = (System.Int16?)value;
							break;
						
						case "TariffDiscountForRoomIn":
						
							if (value == null || value is System.Decimal)
								this.TariffDiscountForRoomIn = (System.Decimal?)value;
							break;
						
						case "IsBpjs":
						
							if (value == null || value is System.Boolean)
								this.IsBpjs = (System.Boolean?)value;
							break;
						
						case "IsShowOnBookingOT":
						
							if (value == null || value is System.Boolean)
								this.IsShowOnBookingOT = (System.Boolean?)value;
							break;
						
						case "IsResetPrice":
						
							if (value == null || value is System.Boolean)
								this.IsResetPrice = (System.Boolean?)value;
							break;
						
						case "IsIsolationRoom":
						
							if (value == null || value is System.Boolean)
								this.IsIsolationRoom = (System.Boolean?)value;
							break;
						
						case "IsNegativePressureRoom":
						
							if (value == null || value is System.Boolean)
								this.IsNegativePressureRoom = (System.Boolean?)value;
							break;
						
						case "IsPandemicRoom":
						
							if (value == null || value is System.Boolean)
								this.IsPandemicRoom = (System.Boolean?)value;
							break;
						
						case "IsVentilator":
						
							if (value == null || value is System.Boolean)
								this.IsVentilator = (System.Boolean?)value;
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
		/// Maps to ServiceRoom.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.RoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.RoomName
		/// </summary>
		virtual public System.String RoomName
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.RoomName);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.RoomName, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsOperatingRoom
		/// </summary>
		virtual public System.Boolean? IsOperatingRoom
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsOperatingRoom);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsOperatingRoom, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceRoomMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceRoomMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.ParamedicID1
		/// </summary>
		virtual public System.String ParamedicID1
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.ParamedicID1);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.ParamedicID1, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.ParamedicID2
		/// </summary>
		virtual public System.String ParamedicID2
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.ParamedicID2);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.ParamedicID2, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.NumberOfBeds
		/// </summary>
		virtual public System.Int16? NumberOfBeds
		{
			get
			{
				return base.GetSystemInt16(ServiceRoomMetadata.ColumnNames.NumberOfBeds);
			}
			
			set
			{
				base.SetSystemInt16(ServiceRoomMetadata.ColumnNames.NumberOfBeds, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.ItemBookedID
		/// </summary>
		virtual public System.String ItemBookedID
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.ItemBookedID);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.ItemBookedID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.TariffDiscountForRoomIn
		/// </summary>
		virtual public System.Decimal? TariffDiscountForRoomIn
		{
			get
			{
				return base.GetSystemDecimal(ServiceRoomMetadata.ColumnNames.TariffDiscountForRoomIn);
			}
			
			set
			{
				base.SetSystemDecimal(ServiceRoomMetadata.ColumnNames.TariffDiscountForRoomIn, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.SRFloor
		/// </summary>
		virtual public System.String SRFloor
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.SRFloor);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.SRFloor, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsBpjs
		/// </summary>
		virtual public System.Boolean? IsBpjs
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsBpjs);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsBpjs, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(ServiceRoomMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(ServiceRoomMetadata.ColumnNames.SRGenderType, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsShowOnBookingOT
		/// </summary>
		virtual public System.Boolean? IsShowOnBookingOT
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsShowOnBookingOT);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsShowOnBookingOT, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsResetPrice
		/// </summary>
		virtual public System.Boolean? IsResetPrice
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsResetPrice);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsResetPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsIsolationRoom
		/// </summary>
		virtual public System.Boolean? IsIsolationRoom
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsIsolationRoom);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsIsolationRoom, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsNegativePressureRoom
		/// </summary>
		virtual public System.Boolean? IsNegativePressureRoom
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsNegativePressureRoom);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsNegativePressureRoom, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsPandemicRoom
		/// </summary>
		virtual public System.Boolean? IsPandemicRoom
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsPandemicRoom);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsPandemicRoom, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceRoom.IsVentilator
		/// </summary>
		virtual public System.Boolean? IsVentilator
		{
			get
			{
				return base.GetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsVentilator);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceRoomMetadata.ColumnNames.IsVentilator, value);
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
			public esStrings(esServiceRoom entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
				}
			}
				
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String RoomName
			{
				get
				{
					System.String data = entity.RoomName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomName = null;
					else entity.RoomName = Convert.ToString(value);
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
				
			public System.String IsOperatingRoom
			{
				get
				{
					System.Boolean? data = entity.IsOperatingRoom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOperatingRoom = null;
					else entity.IsOperatingRoom = Convert.ToBoolean(value);
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
				
			public System.String ParamedicID1
			{
				get
				{
					System.String data = entity.ParamedicID1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID1 = null;
					else entity.ParamedicID1 = Convert.ToString(value);
				}
			}
				
			public System.String ParamedicID2
			{
				get
				{
					System.String data = entity.ParamedicID2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID2 = null;
					else entity.ParamedicID2 = Convert.ToString(value);
				}
			}
				
			public System.String NumberOfBeds
			{
				get
				{
					System.Int16? data = entity.NumberOfBeds;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfBeds = null;
					else entity.NumberOfBeds = Convert.ToInt16(value);
				}
			}
				
			public System.String ItemBookedID
			{
				get
				{
					System.String data = entity.ItemBookedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemBookedID = null;
					else entity.ItemBookedID = Convert.ToString(value);
				}
			}
				
			public System.String TariffDiscountForRoomIn
			{
				get
				{
					System.Decimal? data = entity.TariffDiscountForRoomIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffDiscountForRoomIn = null;
					else entity.TariffDiscountForRoomIn = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRFloor
			{
				get
				{
					System.String data = entity.SRFloor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFloor = null;
					else entity.SRFloor = Convert.ToString(value);
				}
			}
				
			public System.String IsBpjs
			{
				get
				{
					System.Boolean? data = entity.IsBpjs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBpjs = null;
					else entity.IsBpjs = Convert.ToBoolean(value);
				}
			}
				
			public System.String SRGenderType
			{
				get
				{
					System.String data = entity.SRGenderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGenderType = null;
					else entity.SRGenderType = Convert.ToString(value);
				}
			}
				
			public System.String IsShowOnBookingOT
			{
				get
				{
					System.Boolean? data = entity.IsShowOnBookingOT;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowOnBookingOT = null;
					else entity.IsShowOnBookingOT = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsResetPrice
			{
				get
				{
					System.Boolean? data = entity.IsResetPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsResetPrice = null;
					else entity.IsResetPrice = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsIsolationRoom
			{
				get
				{
					System.Boolean? data = entity.IsIsolationRoom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIsolationRoom = null;
					else entity.IsIsolationRoom = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsNegativePressureRoom
			{
				get
				{
					System.Boolean? data = entity.IsNegativePressureRoom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNegativePressureRoom = null;
					else entity.IsNegativePressureRoom = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsPandemicRoom
			{
				get
				{
					System.Boolean? data = entity.IsPandemicRoom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPandemicRoom = null;
					else entity.IsPandemicRoom = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsVentilator
			{
				get
				{
					System.Boolean? data = entity.IsVentilator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVentilator = null;
					else entity.IsVentilator = Convert.ToBoolean(value);
				}
			}
			

			private esServiceRoom entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceRoomQuery query)
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
				throw new Exception("esServiceRoom can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esServiceRoomQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ServiceRoomMetadata.Meta();
			}
		}	
		

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomName
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.RoomName, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsOperatingRoom
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsOperatingRoom, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID1
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.ParamedicID1, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID2
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.ParamedicID2, esSystemType.String);
			}
		} 
		
		public esQueryItem NumberOfBeds
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.NumberOfBeds, esSystemType.Int16);
			}
		} 
		
		public esQueryItem ItemBookedID
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.ItemBookedID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffDiscountForRoomIn
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.TariffDiscountForRoomIn, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRFloor
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.SRFloor, esSystemType.String);
			}
		} 
		
		public esQueryItem IsBpjs
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsBpjs, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsShowOnBookingOT
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsShowOnBookingOT, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsResetPrice
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsResetPrice, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsIsolationRoom
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsIsolationRoom, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsNegativePressureRoom
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsNegativePressureRoom, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsPandemicRoom
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsPandemicRoom, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVentilator
		{
			get
			{
				return new esQueryItem(this, ServiceRoomMetadata.ColumnNames.IsVentilator, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceRoomCollection")]
	public partial class ServiceRoomCollection : esServiceRoomCollection, IEnumerable<ServiceRoom>
	{
		public ServiceRoomCollection()
		{

		}
		
		public static implicit operator List<ServiceRoom>(ServiceRoomCollection coll)
		{
			List<ServiceRoom> list = new List<ServiceRoom>();
			
			foreach (ServiceRoom emp in coll)
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
				return  ServiceRoomMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceRoomQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceRoom(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceRoom();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ServiceRoomQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceRoomQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ServiceRoomQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ServiceRoom AddNew()
		{
			ServiceRoom entity = base.AddNewEntity() as ServiceRoom;
			
			return entity;
		}

		public ServiceRoom FindByPrimaryKey(System.String roomID)
		{
			return base.FindByPrimaryKey(roomID) as ServiceRoom;
		}


		#region IEnumerable<ServiceRoom> Members

		IEnumerator<ServiceRoom> IEnumerable<ServiceRoom>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceRoom;
			}
		}

		#endregion
		
		private ServiceRoomQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceRoom' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceRoom ({RoomID})")]
	[Serializable]
	public partial class ServiceRoom : esServiceRoom
	{
		public ServiceRoom()
		{

		}
	
		public ServiceRoom(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceRoomMetadata.Meta();
			}
		}
		
		
		
		override protected esServiceRoomQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceRoomQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ServiceRoomQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceRoomQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ServiceRoomQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ServiceRoomQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ServiceRoomQuery : esServiceRoomQuery
	{
		public ServiceRoomQuery()
		{

		}		
		
		public ServiceRoomQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ServiceRoomQuery";
        }
		
			
	}


	[Serializable]
	public partial class ServiceRoomMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceRoomMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.RoomID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.RoomID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.RoomName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.RoomName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsOperatingRoom, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsOperatingRoom;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.ParamedicID1, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.ParamedicID1;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.ParamedicID2, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.ParamedicID2;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.NumberOfBeds, 11, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.NumberOfBeds;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.ItemBookedID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.ItemBookedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.TariffDiscountForRoomIn, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.TariffDiscountForRoomIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.SRFloor, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.SRFloor;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsBpjs, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsBpjs;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.SRGenderType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsShowOnBookingOT, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsShowOnBookingOT;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsResetPrice, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsResetPrice;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsIsolationRoom, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsIsolationRoom;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsNegativePressureRoom, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsNegativePressureRoom;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsPandemicRoom, 21, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsPandemicRoom;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceRoomMetadata.ColumnNames.IsVentilator, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceRoomMetadata.PropertyNames.IsVentilator;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ServiceRoomMetadata Meta()
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
			 public const string RoomID = "RoomID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string RoomName = "RoomName";
			 public const string ItemID = "ItemID";
			 public const string Notes = "Notes";
			 public const string IsOperatingRoom = "IsOperatingRoom";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParamedicID1 = "ParamedicID1";
			 public const string ParamedicID2 = "ParamedicID2";
			 public const string NumberOfBeds = "NumberOfBeds";
			 public const string ItemBookedID = "ItemBookedID";
			 public const string TariffDiscountForRoomIn = "TariffDiscountForRoomIn";
			 public const string SRFloor = "SRFloor";
			 public const string IsBpjs = "IsBpjs";
			 public const string SRGenderType = "SRGenderType";
			 public const string IsShowOnBookingOT = "IsShowOnBookingOT";
			 public const string IsResetPrice = "IsResetPrice";
			 public const string IsIsolationRoom = "IsIsolationRoom";
			 public const string IsNegativePressureRoom = "IsNegativePressureRoom";
			 public const string IsPandemicRoom = "IsPandemicRoom";
			 public const string IsVentilator = "IsVentilator";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RoomID = "RoomID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string RoomName = "RoomName";
			 public const string ItemID = "ItemID";
			 public const string Notes = "Notes";
			 public const string IsOperatingRoom = "IsOperatingRoom";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParamedicID1 = "ParamedicID1";
			 public const string ParamedicID2 = "ParamedicID2";
			 public const string NumberOfBeds = "NumberOfBeds";
			 public const string ItemBookedID = "ItemBookedID";
			 public const string TariffDiscountForRoomIn = "TariffDiscountForRoomIn";
			 public const string SRFloor = "SRFloor";
			 public const string IsBpjs = "IsBpjs";
			 public const string SRGenderType = "SRGenderType";
			 public const string IsShowOnBookingOT = "IsShowOnBookingOT";
			 public const string IsResetPrice = "IsResetPrice";
			 public const string IsIsolationRoom = "IsIsolationRoom";
			 public const string IsNegativePressureRoom = "IsNegativePressureRoom";
			 public const string IsPandemicRoom = "IsPandemicRoom";
			 public const string IsVentilator = "IsVentilator";
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
			lock (typeof(ServiceRoomMetadata))
			{
				if(ServiceRoomMetadata.mapDelegates == null)
				{
					ServiceRoomMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceRoomMetadata.meta == null)
				{
					ServiceRoomMetadata.meta = new ServiceRoomMetadata();
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
				

				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOperatingRoom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumberOfBeds", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("ItemBookedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffDiscountForRoomIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRFloor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsBpjs", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsShowOnBookingOT", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsResetPrice", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsIsolationRoom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNegativePressureRoom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPandemicRoom", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVentilator", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "ServiceRoom";
				meta.Destination = "ServiceRoom";
				
				meta.spInsert = "proc_ServiceRoomInsert";				
				meta.spUpdate = "proc_ServiceRoomUpdate";		
				meta.spDelete = "proc_ServiceRoomDelete";
				meta.spLoadAll = "proc_ServiceRoomLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceRoomLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceRoomMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
