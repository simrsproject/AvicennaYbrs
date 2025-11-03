/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/24/2022 10:05:29 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esAppointmentQueueingCollection : esEntityCollectionWAuditLog
	{
		public esAppointmentQueueingCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppointmentQueueingCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppointmentQueueingQuery query)
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
			this.InitQuery(query as esAppointmentQueueingQuery);
		}
		#endregion
			
		virtual public AppointmentQueueing DetachEntity(AppointmentQueueing entity)
		{
			return base.DetachEntity(entity) as AppointmentQueueing;
		}
		
		virtual public AppointmentQueueing AttachEntity(AppointmentQueueing entity)
		{
			return base.AttachEntity(entity) as AppointmentQueueing;
		}
		
		virtual public void Combine(AppointmentQueueingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppointmentQueueing this[int index]
		{
			get
			{
				return base[index] as AppointmentQueueing;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppointmentQueueing);
		}
	}

	[Serializable]
	abstract public class esAppointmentQueueing : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppointmentQueueingQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppointmentQueueing()
		{
		}
	
		public esAppointmentQueueing(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}
	
		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 id)
		{
			esAppointmentQueueingQuery query = this.GetDynamicQuery();
			query.Where(query.Id==id);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id",id);
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
						case "Id": this.str.Id = (string)value; break;
						case "AppointmentNo": this.str.AppointmentNo = (string)value; break;
						case "SRQueueingLocation": this.str.SRQueueingLocation = (string)value; break;
						case "SRQueueingGroup": this.str.SRQueueingGroup = (string)value; break;
						case "SRQueueingType": this.str.SRQueueingType = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "FormattedNo": this.str.FormattedNo = (string)value; break;
						case "QueueingDate": this.str.QueueingDate = (string)value; break;
						case "SRKioskQueueStatus": this.str.SRKioskQueueStatus = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ProcessByUserID": this.str.ProcessByUserID = (string)value; break;
						case "ProcessDateTime": this.str.ProcessDateTime = (string)value; break;
						case "CounterCode": this.str.CounterCode = (string)value; break;
						case "Recall": this.str.Recall = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
							break;
						case "QueueingDate":
						
							if (value == null || value is System.DateTime)
								this.QueueingDate = (System.DateTime?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ProcessDateTime":
						
							if (value == null || value is System.DateTime)
								this.ProcessDateTime = (System.DateTime?)value;
							break;
						case "Recall":
						
							if (value == null || value is System.Boolean)
								this.Recall = (System.Boolean?)value;
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
		/// Maps to AppointmentQueueing.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(AppointmentQueueingMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(AppointmentQueueingMetadata.ColumnNames.Id, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.AppointmentNo
		/// </summary>
		virtual public System.String AppointmentNo
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.AppointmentNo);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.AppointmentNo, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.SRQueueingLocation
		/// </summary>
		virtual public System.String SRQueueingLocation
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.SRQueueingLocation);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.SRQueueingLocation, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.SRQueueingGroup
		/// </summary>
		virtual public System.String SRQueueingGroup
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.SRQueueingGroup);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.SRQueueingGroup, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.SRQueueingType
		/// </summary>
		virtual public System.String SRQueueingType
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.SRQueueingType);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.SRQueueingType, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.FormattedNo
		/// </summary>
		virtual public System.String FormattedNo
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.FormattedNo);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.FormattedNo, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.QueueingDate
		/// </summary>
		virtual public System.DateTime? QueueingDate
		{
			get
			{
				return base.GetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.QueueingDate);
			}
			
			set
			{
				base.SetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.QueueingDate, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.SRKioskQueueStatus
		/// </summary>
		virtual public System.String SRKioskQueueStatus
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.SRKioskQueueStatus);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.SRKioskQueueStatus, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.ProcessByUserID
		/// </summary>
		virtual public System.String ProcessByUserID
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.ProcessByUserID);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.ProcessByUserID, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.ProcessDateTime
		/// </summary>
		virtual public System.DateTime? ProcessDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.ProcessDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppointmentQueueingMetadata.ColumnNames.ProcessDateTime, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.CounterCode
		/// </summary>
		virtual public System.String CounterCode
		{
			get
			{
				return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.CounterCode);
			}
			
			set
			{
				base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.CounterCode, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentQueueing.Recall
		/// </summary>
		virtual public System.Boolean? Recall
		{
			get
			{
				return base.GetSystemBoolean(AppointmentQueueingMetadata.ColumnNames.Recall);
			}
			
			set
			{
				base.SetSystemBoolean(AppointmentQueueingMetadata.ColumnNames.Recall, value);
			}
		}
        /// <summary>
        /// Maps to AppointmentQueueing.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(AppointmentQueueingMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(AppointmentQueueingMetadata.ColumnNames.ParamedicID, value);
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
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
			public esStrings(esAppointmentQueueing entity)
			{
				this.entity = entity;
			}
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
				}
			}
			public System.String AppointmentNo
			{
				get
				{
					System.String data = entity.AppointmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentNo = null;
					else entity.AppointmentNo = Convert.ToString(value);
				}
			}
			public System.String SRQueueingLocation
			{
				get
				{
					System.String data = entity.SRQueueingLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQueueingLocation = null;
					else entity.SRQueueingLocation = Convert.ToString(value);
				}
			}
			public System.String SRQueueingGroup
			{
				get
				{
					System.String data = entity.SRQueueingGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQueueingGroup = null;
					else entity.SRQueueingGroup = Convert.ToString(value);
				}
			}
			public System.String SRQueueingType
			{
				get
				{
					System.String data = entity.SRQueueingType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQueueingType = null;
					else entity.SRQueueingType = Convert.ToString(value);
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
			public System.String FormattedNo
			{
				get
				{
					System.String data = entity.FormattedNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FormattedNo = null;
					else entity.FormattedNo = Convert.ToString(value);
				}
			}
			public System.String QueueingDate
			{
				get
				{
					System.DateTime? data = entity.QueueingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QueueingDate = null;
					else entity.QueueingDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRKioskQueueStatus
			{
				get
				{
					System.String data = entity.SRKioskQueueStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRKioskQueueStatus = null;
					else entity.SRKioskQueueStatus = Convert.ToString(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			public System.String ProcessByUserID
			{
				get
				{
					System.String data = entity.ProcessByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessByUserID = null;
					else entity.ProcessByUserID = Convert.ToString(value);
				}
			}
			public System.String ProcessDateTime
			{
				get
				{
					System.DateTime? data = entity.ProcessDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessDateTime = null;
					else entity.ProcessDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CounterCode
			{
				get
				{
					System.String data = entity.CounterCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounterCode = null;
					else entity.CounterCode = Convert.ToString(value);
				}
			}
			public System.String Recall
			{
				get
				{
					System.Boolean? data = entity.Recall;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Recall = null;
					else entity.Recall = Convert.ToBoolean(value);
				}
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
            private esAppointmentQueueing entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppointmentQueueingQuery query)
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
				throw new Exception("esAppointmentQueueing can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppointmentQueueing : esAppointmentQueueing
	{	
	}

	[Serializable]
	abstract public class esAppointmentQueueingQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppointmentQueueingMetadata.Meta();
			}
		}	
			
		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
			
		public esQueryItem AppointmentNo
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.AppointmentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SRQueueingLocation
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.SRQueueingLocation, esSystemType.String);
			}
		} 
			
		public esQueryItem SRQueueingGroup
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.SRQueueingGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem SRQueueingType
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.SRQueueingType, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem FormattedNo
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.FormattedNo, esSystemType.String);
			}
		} 
			
		public esQueryItem QueueingDate
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.QueueingDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem SRKioskQueueStatus
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.SRKioskQueueStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ProcessByUserID
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.ProcessByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ProcessDateTime
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.ProcessDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CounterCode
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.CounterCode, esSystemType.String);
			}
		} 
			
		public esQueryItem Recall
		{
			get
			{
				return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.Recall, esSystemType.Boolean);
			}
		}

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, AppointmentQueueingMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }
    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppointmentQueueingCollection")]
	public partial class AppointmentQueueingCollection : esAppointmentQueueingCollection, IEnumerable< AppointmentQueueing>
	{
		public AppointmentQueueingCollection()
		{

		}	
		
		public static implicit operator List< AppointmentQueueing>(AppointmentQueueingCollection coll)
		{
			List< AppointmentQueueing> list = new List< AppointmentQueueing>();
			
			foreach (AppointmentQueueing emp in coll)
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
				return  AppointmentQueueingMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentQueueingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppointmentQueueing(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppointmentQueueing();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppointmentQueueingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentQueueingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(AppointmentQueueingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppointmentQueueing AddNew()
		{
			AppointmentQueueing entity = base.AddNewEntity() as AppointmentQueueing;
			
			return entity;		
		}
		public AppointmentQueueing FindByPrimaryKey(Int32 id)
		{
			return base.FindByPrimaryKey(id) as AppointmentQueueing;
		}

		#region IEnumerable< AppointmentQueueing> Members

		IEnumerator< AppointmentQueueing> IEnumerable< AppointmentQueueing>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppointmentQueueing;
			}
		}

		#endregion
		
		private AppointmentQueueingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppointmentQueueing' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppointmentQueueing ({Id})")]
	[Serializable]
	public partial class AppointmentQueueing : esAppointmentQueueing
	{
		public AppointmentQueueing()
		{
		}	
	
		public AppointmentQueueing(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppointmentQueueingMetadata.Meta();
			}
		}	
	
		override protected esAppointmentQueueingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentQueueingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppointmentQueueingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentQueueingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(AppointmentQueueingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppointmentQueueingQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppointmentQueueingQuery : esAppointmentQueueingQuery
	{
		public AppointmentQueueingQuery()
		{

		}		
		
		public AppointmentQueueingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppointmentQueueingQuery";
        }
	}

	[Serializable]
	public partial class AppointmentQueueingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppointmentQueueingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.AppointmentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.AppointmentNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.SRQueueingLocation, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.SRQueueingLocation;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.SRQueueingGroup, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.SRQueueingGroup;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.SRQueueingType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.SRQueueingType;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.ServiceUnitID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.FormattedNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.FormattedNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.QueueingDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.QueueingDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.SRKioskQueueStatus, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.SRKioskQueueStatus;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.CreateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.CreateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.ProcessByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.ProcessByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.ProcessDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.ProcessDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.CounterCode, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.CounterCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.Recall, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppointmentQueueingMetadata.PropertyNames.Recall;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(AppointmentQueueingMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AppointmentQueueingMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

        }
		#endregion
	
		static public AppointmentQueueingMetadata Meta()
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
			public const string Id = "Id";
			public const string AppointmentNo = "AppointmentNo";
			public const string SRQueueingLocation = "SRQueueingLocation";
			public const string SRQueueingGroup = "SRQueueingGroup";
			public const string SRQueueingType = "SRQueueingType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string FormattedNo = "FormattedNo";
			public const string QueueingDate = "QueueingDate";
			public const string SRKioskQueueStatus = "SRKioskQueueStatus";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ProcessByUserID = "ProcessByUserID";
			public const string ProcessDateTime = "ProcessDateTime";
			public const string CounterCode = "CounterCode";
			public const string Recall = "Recall";
			public const string ParamedicID = "ParamedicID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string Id = "Id";
			public const string AppointmentNo = "AppointmentNo";
			public const string SRQueueingLocation = "SRQueueingLocation";
			public const string SRQueueingGroup = "SRQueueingGroup";
			public const string SRQueueingType = "SRQueueingType";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string FormattedNo = "FormattedNo";
			public const string QueueingDate = "QueueingDate";
			public const string SRKioskQueueStatus = "SRKioskQueueStatus";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ProcessByUserID = "ProcessByUserID";
			public const string ProcessDateTime = "ProcessDateTime";
			public const string CounterCode = "CounterCode";
			public const string Recall = "Recall";
			public const string ParamedicID = "ParamedicID";
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
			lock (typeof(AppointmentQueueingMetadata))
			{
				if(AppointmentQueueingMetadata.mapDelegates == null)
				{
					AppointmentQueueingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppointmentQueueingMetadata.meta == null)
				{
					AppointmentQueueingMetadata.meta = new AppointmentQueueingMetadata();
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
				
				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AppointmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRQueueingLocation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRQueueingGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRQueueingType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FormattedNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QueueingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRKioskQueueStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CounterCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Recall", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "AppointmentQueueing";
				meta.Destination = "AppointmentQueueing";
				meta.spInsert = "proc_AppointmentQueueingInsert";				
				meta.spUpdate = "proc_AppointmentQueueingUpdate";		
				meta.spDelete = "proc_AppointmentQueueingDelete";
				meta.spLoadAll = "proc_AppointmentQueueingLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppointmentQueueingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppointmentQueueingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
