/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esLeaveRequestCollection : esEntityCollectionWAuditLog
	{
		public esLeaveRequestCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LeaveRequestCollection";
		}

		#region Query Logic
		protected void InitQuery(esLeaveRequestQuery query)
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
			this.InitQuery(query as esLeaveRequestQuery);
		}
		#endregion
		
		virtual public LeaveRequest DetachEntity(LeaveRequest entity)
		{
			return base.DetachEntity(entity) as LeaveRequest;
		}
		
		virtual public LeaveRequest AttachEntity(LeaveRequest entity)
		{
			return base.AttachEntity(entity) as LeaveRequest;
		}
		
		virtual public void Combine(LeaveRequestCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LeaveRequest this[int index]
		{
			get
			{
				return base[index] as LeaveRequest;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LeaveRequest);
		}
	}



	[Serializable]
	abstract public class esLeaveRequest : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLeaveRequestQuery GetDynamicQuery()
		{
			return null;
		}

		public esLeaveRequest()
		{

		}

		public esLeaveRequest(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 leaveRequestID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(leaveRequestID);
			else
				return LoadByPrimaryKeyStoredProcedure(leaveRequestID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 leaveRequestID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(leaveRequestID);
			else
				return LoadByPrimaryKeyStoredProcedure(leaveRequestID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 leaveRequestID)
		{
			esLeaveRequestQuery query = this.GetDynamicQuery();
			query.Where(query.LeaveRequestID == leaveRequestID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 leaveRequestID)
		{
			esParameters parms = new esParameters();
			parms.Add("LeaveRequestID",leaveRequestID);
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
						case "LeaveRequestID": this.str.LeaveRequestID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "RequestDate": this.str.RequestDate = (string)value; break;							
						case "LeaveTypeID": this.str.LeaveTypeID = (string)value; break;							
						case "LeaveBalance": this.str.LeaveBalance = (string)value; break;							
						case "LeaveDateFrom": this.str.LeaveDateFrom = (string)value; break;							
						case "LeaveTimeFrom": this.str.LeaveTimeFrom = (string)value; break;							
						case "LeaveDateTo": this.str.LeaveDateTo = (string)value; break;							
						case "LeaveTimeTo": this.str.LeaveTimeTo = (string)value; break;							
						case "RequestDays": this.str.RequestDays = (string)value; break;							
						case "WorkingDate": this.str.WorkingDate = (string)value; break;							
						case "Reason": this.str.Reason = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LeaveRequestID":
						
							if (value == null || value is System.Int32)
								this.LeaveRequestID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "RequestDate":
						
							if (value == null || value is System.DateTime)
								this.RequestDate = (System.DateTime?)value;
							break;
						
						case "LeaveTypeID":
						
							if (value == null || value is System.Int32)
								this.LeaveTypeID = (System.Int32?)value;
							break;
						
						case "LeaveBalance":
						
							if (value == null || value is System.Decimal)
								this.LeaveBalance = (System.Decimal?)value;
							break;
						
						case "LeaveDateFrom":
						
							if (value == null || value is System.DateTime)
								this.LeaveDateFrom = (System.DateTime?)value;
							break;
						
						case "LeaveDateTo":
						
							if (value == null || value is System.DateTime)
								this.LeaveDateTo = (System.DateTime?)value;
							break;
						
						case "RequestDays":
						
							if (value == null || value is System.Decimal)
								this.RequestDays = (System.Decimal?)value;
							break;
						
						case "WorkingDate":
						
							if (value == null || value is System.DateTime)
								this.WorkingDate = (System.DateTime?)value;
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
		/// Maps to LeaveRequest.LeaveRequestID
		/// </summary>
		virtual public System.Int32? LeaveRequestID
		{
			get
			{
				return base.GetSystemInt32(LeaveRequestMetadata.ColumnNames.LeaveRequestID);
			}
			
			set
			{
				base.SetSystemInt32(LeaveRequestMetadata.ColumnNames.LeaveRequestID, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(LeaveRequestMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(LeaveRequestMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.RequestDate
		/// </summary>
		virtual public System.DateTime? RequestDate
		{
			get
			{
				return base.GetSystemDateTime(LeaveRequestMetadata.ColumnNames.RequestDate);
			}
			
			set
			{
				base.SetSystemDateTime(LeaveRequestMetadata.ColumnNames.RequestDate, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LeaveTypeID
		/// </summary>
		virtual public System.Int32? LeaveTypeID
		{
			get
			{
				return base.GetSystemInt32(LeaveRequestMetadata.ColumnNames.LeaveTypeID);
			}
			
			set
			{
				base.SetSystemInt32(LeaveRequestMetadata.ColumnNames.LeaveTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LeaveBalance
		/// </summary>
		virtual public System.Decimal? LeaveBalance
		{
			get
			{
				return base.GetSystemDecimal(LeaveRequestMetadata.ColumnNames.LeaveBalance);
			}
			
			set
			{
				base.SetSystemDecimal(LeaveRequestMetadata.ColumnNames.LeaveBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LeaveDateFrom
		/// </summary>
		virtual public System.DateTime? LeaveDateFrom
		{
			get
			{
				return base.GetSystemDateTime(LeaveRequestMetadata.ColumnNames.LeaveDateFrom);
			}
			
			set
			{
				base.SetSystemDateTime(LeaveRequestMetadata.ColumnNames.LeaveDateFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LeaveTimeFrom
		/// </summary>
		virtual public System.String LeaveTimeFrom
		{
			get
			{
				return base.GetSystemString(LeaveRequestMetadata.ColumnNames.LeaveTimeFrom);
			}
			
			set
			{
				base.SetSystemString(LeaveRequestMetadata.ColumnNames.LeaveTimeFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LeaveDateTo
		/// </summary>
		virtual public System.DateTime? LeaveDateTo
		{
			get
			{
				return base.GetSystemDateTime(LeaveRequestMetadata.ColumnNames.LeaveDateTo);
			}
			
			set
			{
				base.SetSystemDateTime(LeaveRequestMetadata.ColumnNames.LeaveDateTo, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LeaveTimeTo
		/// </summary>
		virtual public System.String LeaveTimeTo
		{
			get
			{
				return base.GetSystemString(LeaveRequestMetadata.ColumnNames.LeaveTimeTo);
			}
			
			set
			{
				base.SetSystemString(LeaveRequestMetadata.ColumnNames.LeaveTimeTo, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.RequestDays
		/// </summary>
		virtual public System.Decimal? RequestDays
		{
			get
			{
				return base.GetSystemDecimal(LeaveRequestMetadata.ColumnNames.RequestDays);
			}
			
			set
			{
				base.SetSystemDecimal(LeaveRequestMetadata.ColumnNames.RequestDays, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.WorkingDate
		/// </summary>
		virtual public System.DateTime? WorkingDate
		{
			get
			{
				return base.GetSystemDateTime(LeaveRequestMetadata.ColumnNames.WorkingDate);
			}
			
			set
			{
				base.SetSystemDateTime(LeaveRequestMetadata.ColumnNames.WorkingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(LeaveRequestMetadata.ColumnNames.Reason);
			}
			
			set
			{
				base.SetSystemString(LeaveRequestMetadata.ColumnNames.Reason, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LeaveRequestMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(LeaveRequestMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to LeaveRequest.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LeaveRequestMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(LeaveRequestMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLeaveRequest entity)
			{
				this.entity = entity;
			}
			
	
			public System.String LeaveRequestID
			{
				get
				{
					System.Int32? data = entity.LeaveRequestID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveRequestID = null;
					else entity.LeaveRequestID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
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
				
			public System.String LeaveTypeID
			{
				get
				{
					System.Int32? data = entity.LeaveTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveTypeID = null;
					else entity.LeaveTypeID = Convert.ToInt32(value);
				}
			}
				
			public System.String LeaveBalance
			{
				get
				{
					System.Decimal? data = entity.LeaveBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveBalance = null;
					else entity.LeaveBalance = Convert.ToDecimal(value);
				}
			}
				
			public System.String LeaveDateFrom
			{
				get
				{
					System.DateTime? data = entity.LeaveDateFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveDateFrom = null;
					else entity.LeaveDateFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String LeaveTimeFrom
			{
				get
				{
					System.String data = entity.LeaveTimeFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveTimeFrom = null;
					else entity.LeaveTimeFrom = Convert.ToString(value);
				}
			}
				
			public System.String LeaveDateTo
			{
				get
				{
					System.DateTime? data = entity.LeaveDateTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveDateTo = null;
					else entity.LeaveDateTo = Convert.ToDateTime(value);
				}
			}
				
			public System.String LeaveTimeTo
			{
				get
				{
					System.String data = entity.LeaveTimeTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveTimeTo = null;
					else entity.LeaveTimeTo = Convert.ToString(value);
				}
			}
				
			public System.String RequestDays
			{
				get
				{
					System.Decimal? data = entity.RequestDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDays = null;
					else entity.RequestDays = Convert.ToDecimal(value);
				}
			}
				
			public System.String WorkingDate
			{
				get
				{
					System.DateTime? data = entity.WorkingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingDate = null;
					else entity.WorkingDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String Reason
			{
				get
				{
					System.String data = entity.Reason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reason = null;
					else entity.Reason = Convert.ToString(value);
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
			

			private esLeaveRequest entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLeaveRequestQuery query)
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
				throw new Exception("esLeaveRequest can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LeaveRequest : esLeaveRequest
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
	abstract public class esLeaveRequestQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LeaveRequestMetadata.Meta();
			}
		}	
		

		public esQueryItem LeaveRequestID
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LeaveRequestID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RequestDate
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.RequestDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LeaveTypeID
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LeaveTypeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LeaveBalance
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LeaveBalance, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LeaveDateFrom
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LeaveDateFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LeaveTimeFrom
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LeaveTimeFrom, esSystemType.String);
			}
		} 
		
		public esQueryItem LeaveDateTo
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LeaveDateTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LeaveTimeTo
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LeaveTimeTo, esSystemType.String);
			}
		} 
		
		public esQueryItem RequestDays
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.RequestDays, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem WorkingDate
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.WorkingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.Reason, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LeaveRequestMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LeaveRequestCollection")]
	public partial class LeaveRequestCollection : esLeaveRequestCollection, IEnumerable<LeaveRequest>
	{
		public LeaveRequestCollection()
		{

		}
		
		public static implicit operator List<LeaveRequest>(LeaveRequestCollection coll)
		{
			List<LeaveRequest> list = new List<LeaveRequest>();
			
			foreach (LeaveRequest emp in coll)
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
				return  LeaveRequestMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LeaveRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LeaveRequest(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LeaveRequest();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LeaveRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LeaveRequestQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LeaveRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LeaveRequest AddNew()
		{
			LeaveRequest entity = base.AddNewEntity() as LeaveRequest;
			
			return entity;
		}

		public LeaveRequest FindByPrimaryKey(System.Int32 leaveRequestID)
		{
			return base.FindByPrimaryKey(leaveRequestID) as LeaveRequest;
		}


		#region IEnumerable<LeaveRequest> Members

		IEnumerator<LeaveRequest> IEnumerable<LeaveRequest>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LeaveRequest;
			}
		}

		#endregion
		
		private LeaveRequestQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LeaveRequest' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LeaveRequest ({LeaveRequestID})")]
	[Serializable]
	public partial class LeaveRequest : esLeaveRequest
	{
		public LeaveRequest()
		{

		}
	
		public LeaveRequest(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LeaveRequestMetadata.Meta();
			}
		}
		
		
		
		override protected esLeaveRequestQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LeaveRequestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LeaveRequestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LeaveRequestQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LeaveRequestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LeaveRequestQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LeaveRequestQuery : esLeaveRequestQuery
	{
		public LeaveRequestQuery()
		{

		}		
		
		public LeaveRequestQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LeaveRequestQuery";
        }
		
			
	}


	[Serializable]
	public partial class LeaveRequestMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LeaveRequestMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LeaveRequestID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LeaveRequestID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.RequestDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.RequestDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LeaveTypeID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LeaveTypeID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LeaveBalance, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LeaveBalance;
			c.NumericPrecision = 3;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LeaveDateFrom, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LeaveDateFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LeaveTimeFrom, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LeaveTimeFrom;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LeaveDateTo, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LeaveDateTo;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LeaveTimeTo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LeaveTimeTo;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.RequestDays, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.RequestDays;
			c.NumericPrecision = 3;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.WorkingDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.WorkingDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.Reason, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(LeaveRequestMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = LeaveRequestMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LeaveRequestMetadata Meta()
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
			 public const string LeaveRequestID = "LeaveRequestID";
			 public const string PersonID = "PersonID";
			 public const string RequestDate = "RequestDate";
			 public const string LeaveTypeID = "LeaveTypeID";
			 public const string LeaveBalance = "LeaveBalance";
			 public const string LeaveDateFrom = "LeaveDateFrom";
			 public const string LeaveTimeFrom = "LeaveTimeFrom";
			 public const string LeaveDateTo = "LeaveDateTo";
			 public const string LeaveTimeTo = "LeaveTimeTo";
			 public const string RequestDays = "RequestDays";
			 public const string WorkingDate = "WorkingDate";
			 public const string Reason = "Reason";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string LeaveRequestID = "LeaveRequestID";
			 public const string PersonID = "PersonID";
			 public const string RequestDate = "RequestDate";
			 public const string LeaveTypeID = "LeaveTypeID";
			 public const string LeaveBalance = "LeaveBalance";
			 public const string LeaveDateFrom = "LeaveDateFrom";
			 public const string LeaveTimeFrom = "LeaveTimeFrom";
			 public const string LeaveDateTo = "LeaveDateTo";
			 public const string LeaveTimeTo = "LeaveTimeTo";
			 public const string RequestDays = "RequestDays";
			 public const string WorkingDate = "WorkingDate";
			 public const string Reason = "Reason";
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
			lock (typeof(LeaveRequestMetadata))
			{
				if(LeaveRequestMetadata.mapDelegates == null)
				{
					LeaveRequestMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LeaveRequestMetadata.meta == null)
				{
					LeaveRequestMetadata.meta = new LeaveRequestMetadata();
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
				

				meta.AddTypeMap("LeaveRequestID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RequestDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LeaveTypeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LeaveBalance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LeaveDateFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LeaveTimeFrom", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LeaveDateTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LeaveTimeTo", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("RequestDays", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("WorkingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LeaveRequest";
				meta.Destination = "LeaveRequest";
				
				meta.spInsert = "proc_LeaveRequestInsert";				
				meta.spUpdate = "proc_LeaveRequestUpdate";		
				meta.spDelete = "proc_LeaveRequestDelete";
				meta.spLoadAll = "proc_LeaveRequestLoadAll";
				meta.spLoadByPrimaryKey = "proc_LeaveRequestLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LeaveRequestMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
