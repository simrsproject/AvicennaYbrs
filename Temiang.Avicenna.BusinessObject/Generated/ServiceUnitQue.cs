/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:26 PM
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
	abstract public class esServiceUnitQueCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitQueCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ServiceUnitQueCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitQueQuery query)
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
			this.InitQuery(query as esServiceUnitQueQuery);
		}
		#endregion
		
		virtual public ServiceUnitQue DetachEntity(ServiceUnitQue entity)
		{
			return base.DetachEntity(entity) as ServiceUnitQue;
		}
		
		virtual public ServiceUnitQue AttachEntity(ServiceUnitQue entity)
		{
			return base.AttachEntity(entity) as ServiceUnitQue;
		}
		
		virtual public void Combine(ServiceUnitQueCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnitQue this[int index]
		{
			get
			{
				return base[index] as ServiceUnitQue;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitQue);
		}
	}



	[Serializable]
	abstract public class esServiceUnitQue : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitQueQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitQue()
		{

		}

		public esServiceUnitQue(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String serviceUnitID, System.String paramedicID, System.DateTime queDate, System.Int32 queNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, queDate, queNo);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, queDate, queNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID, System.String paramedicID, System.DateTime queDate, System.Int32 queNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, queDate, queNo);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, queDate, queNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID, System.String paramedicID, System.DateTime queDate, System.Int32 queNo)
		{
			esServiceUnitQueQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ParamedicID == paramedicID, query.QueDate == queDate, query.QueNo == queNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID, System.String paramedicID, System.DateTime queDate, System.Int32 queNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID",serviceUnitID);			parms.Add("ParamedicID",paramedicID);			parms.Add("QueDate",queDate);			parms.Add("QueNo",queNo);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "QueDate": this.str.QueDate = (string)value; break;							
						case "QueNo": this.str.QueNo = (string)value; break;							
						case "ServiceRoomID": this.str.ServiceRoomID = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "IsClosed": this.str.IsClosed = (string)value; break;							
						case "IsFromReferProcess": this.str.IsFromReferProcess = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "VoidDate": this.str.VoidDate = (string)value; break;							
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "ParentNo": this.str.ParentNo = (string)value; break;							
						case "StartTime": this.str.StartTime = (string)value; break;							
						case "EndTime": this.str.EndTime = (string)value; break;							
						case "IsStopped": this.str.IsStopped = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "QueDate":
						
							if (value == null || value is System.DateTime)
								this.QueDate = (System.DateTime?)value;
							break;
						
						case "QueNo":
						
							if (value == null || value is System.Int32)
								this.QueNo = (System.Int32?)value;
							break;
						
						case "IsClosed":
						
							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						
						case "IsFromReferProcess":
						
							if (value == null || value is System.Boolean)
								this.IsFromReferProcess = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "VoidDate":
						
							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "StartTime":
						
							if (value == null || value is System.DateTime)
								this.StartTime = (System.DateTime?)value;
							break;
						
						case "EndTime":
						
							if (value == null || value is System.DateTime)
								this.EndTime = (System.DateTime?)value;
							break;
						
						case "IsStopped":
						
							if (value == null || value is System.Boolean)
								this.IsStopped = (System.Boolean?)value;
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
		/// Maps to ServiceUnitQue.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.QueDate
		/// </summary>
		virtual public System.DateTime? QueDate
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.QueDate);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.QueDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.QueNo
		/// </summary>
		virtual public System.Int32? QueNo
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitQueMetadata.ColumnNames.QueNo);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitQueMetadata.ColumnNames.QueNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.ServiceRoomID
		/// </summary>
		virtual public System.String ServiceRoomID
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.ServiceRoomID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.ServiceRoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				if(base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.RegistrationNo, value))
				{
					this._UpToRegistrationByRegistrationNo = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsClosed);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsClosed, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.IsFromReferProcess
		/// </summary>
		virtual public System.Boolean? IsFromReferProcess
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsFromReferProcess);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsFromReferProcess, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.VoidDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(ServiceUnitQueMetadata.ColumnNames.ParentNo);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitQueMetadata.ColumnNames.ParentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.StartTime
		/// </summary>
		virtual public System.DateTime? StartTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.StartTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.StartTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.EndTime
		/// </summary>
		virtual public System.DateTime? EndTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.EndTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitQueMetadata.ColumnNames.EndTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ServiceUnitQue.IsStopped
		/// </summary>
		virtual public System.Boolean? IsStopped
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsStopped);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitQueMetadata.ColumnNames.IsStopped, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected Registration _UpToRegistrationByRegistrationNo;
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
			public esStrings(esServiceUnitQue entity)
			{
				this.entity = entity;
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
				
			public System.String QueDate
			{
				get
				{
					System.DateTime? data = entity.QueDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QueDate = null;
					else entity.QueDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String QueNo
			{
				get
				{
					System.Int32? data = entity.QueNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QueNo = null;
					else entity.QueNo = Convert.ToInt32(value);
				}
			}
				
			public System.String ServiceRoomID
			{
				get
				{
					System.String data = entity.ServiceRoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceRoomID = null;
					else entity.ServiceRoomID = Convert.ToString(value);
				}
			}
				
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsFromReferProcess
			{
				get
				{
					System.Boolean? data = entity.IsFromReferProcess;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFromReferProcess = null;
					else entity.IsFromReferProcess = Convert.ToBoolean(value);
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
				
			public System.String ParentNo
			{
				get
				{
					System.String data = entity.ParentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentNo = null;
					else entity.ParentNo = Convert.ToString(value);
				}
			}
				
			public System.String StartTime
			{
				get
				{
					System.DateTime? data = entity.StartTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartTime = null;
					else entity.StartTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String EndTime
			{
				get
				{
					System.DateTime? data = entity.EndTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndTime = null;
					else entity.EndTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String IsStopped
			{
				get
				{
					System.Boolean? data = entity.IsStopped;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsStopped = null;
					else entity.IsStopped = Convert.ToBoolean(value);
				}
			}
			

			private esServiceUnitQue entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitQueQuery query)
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
				throw new Exception("esServiceUnitQue can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ServiceUnitQue : esServiceUnitQue
	{

				
		#region UpToRegistrationByRegistrationNo - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - RefServiceUnitQuetoRegistration
		/// </summary>

		[XmlIgnore]
		public Registration UpToRegistrationByRegistrationNo
		{
			get
			{
				if(this._UpToRegistrationByRegistrationNo == null
					&& RegistrationNo != null					)
				{
					this._UpToRegistrationByRegistrationNo = new Registration();
					this._UpToRegistrationByRegistrationNo.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
					this._UpToRegistrationByRegistrationNo.Query.Where(this._UpToRegistrationByRegistrationNo.Query.RegistrationNo == this.RegistrationNo);
					this._UpToRegistrationByRegistrationNo.Query.Load();
				}

				return this._UpToRegistrationByRegistrationNo;
			}
			
			set
			{
				this.RemovePreSave("UpToRegistrationByRegistrationNo");
				

				if(value == null)
				{
					this.RegistrationNo = null;
					this._UpToRegistrationByRegistrationNo = null;
				}
				else
				{
					this.RegistrationNo = value.RegistrationNo;
					this._UpToRegistrationByRegistrationNo = value;
					this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esServiceUnitQueQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitQueMetadata.Meta();
			}
		}	
		

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem QueDate
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.QueDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem QueNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.QueNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ServiceRoomID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.ServiceRoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsFromReferProcess
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.IsFromReferProcess, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem StartTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.StartTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EndTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.EndTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsStopped
		{
			get
			{
				return new esQueryItem(this, ServiceUnitQueMetadata.ColumnNames.IsStopped, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitQueCollection")]
	public partial class ServiceUnitQueCollection : esServiceUnitQueCollection, IEnumerable<ServiceUnitQue>
	{
		public ServiceUnitQueCollection()
		{

		}
		
		public static implicit operator List<ServiceUnitQue>(ServiceUnitQueCollection coll)
		{
			List<ServiceUnitQue> list = new List<ServiceUnitQue>();
			
			foreach (ServiceUnitQue emp in coll)
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
				return  ServiceUnitQueMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitQueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitQue(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitQue();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ServiceUnitQueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitQueQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ServiceUnitQueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ServiceUnitQue AddNew()
		{
			ServiceUnitQue entity = base.AddNewEntity() as ServiceUnitQue;
			
			return entity;
		}

		public ServiceUnitQue FindByPrimaryKey(System.String serviceUnitID, System.String paramedicID, System.DateTime queDate, System.Int32 queNo)
		{
			return base.FindByPrimaryKey(serviceUnitID, paramedicID, queDate, queNo) as ServiceUnitQue;
		}


		#region IEnumerable<ServiceUnitQue> Members

		IEnumerator<ServiceUnitQue> IEnumerable<ServiceUnitQue>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitQue;
			}
		}

		#endregion
		
		private ServiceUnitQueQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitQue' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ServiceUnitQue ({ServiceUnitID},{ParamedicID},{QueDate},{QueNo})")]
	[Serializable]
	public partial class ServiceUnitQue : esServiceUnitQue
	{
		public ServiceUnitQue()
		{

		}
	
		public ServiceUnitQue(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitQueMetadata.Meta();
			}
		}
		
		
		
		override protected esServiceUnitQueQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitQueQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ServiceUnitQueQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitQueQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ServiceUnitQueQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ServiceUnitQueQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ServiceUnitQueQuery : esServiceUnitQueQuery
	{
		public ServiceUnitQueQuery()
		{

		}		
		
		public ServiceUnitQueQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ServiceUnitQueQuery";
        }
		
			
	}


	[Serializable]
	public partial class ServiceUnitQueMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitQueMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.QueDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.QueDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.QueNo, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.QueNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.ServiceRoomID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.ServiceRoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.RegistrationNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.IsClosed, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.IsClosed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.IsFromReferProcess, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.IsFromReferProcess;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.VoidDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.ParentNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.StartTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.StartTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.EndTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.EndTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ServiceUnitQueMetadata.ColumnNames.IsStopped, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitQueMetadata.PropertyNames.IsStopped;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ServiceUnitQueMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ParamedicID = "ParamedicID";
			 public const string QueDate = "QueDate";
			 public const string QueNo = "QueNo";
			 public const string ServiceRoomID = "ServiceRoomID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string IsClosed = "IsClosed";
			 public const string IsFromReferProcess = "IsFromReferProcess";
			 public const string Notes = "Notes";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParentNo = "ParentNo";
			 public const string StartTime = "StartTime";
			 public const string EndTime = "EndTime";
			 public const string IsStopped = "IsStopped";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ParamedicID = "ParamedicID";
			 public const string QueDate = "QueDate";
			 public const string QueNo = "QueNo";
			 public const string ServiceRoomID = "ServiceRoomID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string IsClosed = "IsClosed";
			 public const string IsFromReferProcess = "IsFromReferProcess";
			 public const string Notes = "Notes";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParentNo = "ParentNo";
			 public const string StartTime = "StartTime";
			 public const string EndTime = "EndTime";
			 public const string IsStopped = "IsStopped";
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
			lock (typeof(ServiceUnitQueMetadata))
			{
				if(ServiceUnitQueMetadata.mapDelegates == null)
				{
					ServiceUnitQueMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitQueMetadata.meta == null)
				{
					ServiceUnitQueMetadata.meta = new ServiceUnitQueMetadata();
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
				

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QueDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("QueNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceRoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFromReferProcess", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsStopped", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "ServiceUnitQue";
				meta.Destination = "ServiceUnitQue";
				
				meta.spInsert = "proc_ServiceUnitQueInsert";				
				meta.spUpdate = "proc_ServiceUnitQueUpdate";		
				meta.spDelete = "proc_ServiceUnitQueDelete";
				meta.spLoadAll = "proc_ServiceUnitQueLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitQueLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitQueMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
