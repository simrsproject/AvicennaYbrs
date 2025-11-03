/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:21 PM
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
	abstract public class esPatientVitalSignMonitoringCollection : esEntityCollectionWAuditLog
	{
		public esPatientVitalSignMonitoringCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PatientVitalSignMonitoringCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientVitalSignMonitoringQuery query)
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
			this.InitQuery(query as esPatientVitalSignMonitoringQuery);
		}
		#endregion
		
		virtual public PatientVitalSignMonitoring DetachEntity(PatientVitalSignMonitoring entity)
		{
			return base.DetachEntity(entity) as PatientVitalSignMonitoring;
		}
		
		virtual public PatientVitalSignMonitoring AttachEntity(PatientVitalSignMonitoring entity)
		{
			return base.AttachEntity(entity) as PatientVitalSignMonitoring;
		}
		
		virtual public void Combine(PatientVitalSignMonitoringCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PatientVitalSignMonitoring this[int index]
		{
			get
			{
				return base[index] as PatientVitalSignMonitoring;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientVitalSignMonitoring);
		}
	}



	[Serializable]
	abstract public class esPatientVitalSignMonitoring : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientVitalSignMonitoringQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientVitalSignMonitoring()
		{

		}

		public esPatientVitalSignMonitoring(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String orderNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, orderNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String orderNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, orderNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, orderNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String orderNo)
		{
			esPatientVitalSignMonitoringQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.OrderNo == orderNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String orderNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("OrderNo",orderNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "OrderNo": this.str.OrderNo = (string)value; break;							
						case "StartingDate": this.str.StartingDate = (string)value; break;							
						case "StartingTime": this.str.StartingTime = (string)value; break;							
						case "EndingDate": this.str.EndingDate = (string)value; break;							
						case "EndingTime": this.str.EndingTime = (string)value; break;							
						case "Interval": this.str.Interval = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StartingDate":
						
							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						
						case "EndingDate":
						
							if (value == null || value is System.DateTime)
								this.EndingDate = (System.DateTime?)value;
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
		/// Maps to PatientVitalSignMonitoring.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.OrderNo
		/// </summary>
		virtual public System.String OrderNo
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.OrderNo);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.OrderNo, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(PatientVitalSignMonitoringMetadata.ColumnNames.StartingDate);
			}
			
			set
			{
				base.SetSystemDateTime(PatientVitalSignMonitoringMetadata.ColumnNames.StartingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.StartingTime
		/// </summary>
		virtual public System.String StartingTime
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.StartingTime);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.StartingTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.EndingDate
		/// </summary>
		virtual public System.DateTime? EndingDate
		{
			get
			{
				return base.GetSystemDateTime(PatientVitalSignMonitoringMetadata.ColumnNames.EndingDate);
			}
			
			set
			{
				base.SetSystemDateTime(PatientVitalSignMonitoringMetadata.ColumnNames.EndingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.EndingTime
		/// </summary>
		virtual public System.String EndingTime
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.EndingTime);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.EndingTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.Interval
		/// </summary>
		virtual public System.String Interval
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.Interval);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.Interval, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PatientVitalSignMonitoring.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPatientVitalSignMonitoring entity)
			{
				this.entity = entity;
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
				
			public System.String StartingTime
			{
				get
				{
					System.String data = entity.StartingTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingTime = null;
					else entity.StartingTime = Convert.ToString(value);
				}
			}
				
			public System.String EndingDate
			{
				get
				{
					System.DateTime? data = entity.EndingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndingDate = null;
					else entity.EndingDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EndingTime
			{
				get
				{
					System.String data = entity.EndingTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndingTime = null;
					else entity.EndingTime = Convert.ToString(value);
				}
			}
				
			public System.String Interval
			{
				get
				{
					System.String data = entity.Interval;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Interval = null;
					else entity.Interval = Convert.ToString(value);
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
			

			private esPatientVitalSignMonitoring entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientVitalSignMonitoringQuery query)
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
				throw new Exception("esPatientVitalSignMonitoring can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PatientVitalSignMonitoring : esPatientVitalSignMonitoring
	{

				
		#region PatientVitalSignMonitoringItemCollectionByRegistrationNo - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefVitalSignMonitoringDtToVitalSignMonitoringHd
		/// </summary>

		[XmlIgnore]
		public PatientVitalSignMonitoringItemCollection PatientVitalSignMonitoringItemCollectionByRegistrationNo
		{
			get
			{
				if(this._PatientVitalSignMonitoringItemCollectionByRegistrationNo == null)
				{
					this._PatientVitalSignMonitoringItemCollectionByRegistrationNo = new PatientVitalSignMonitoringItemCollection();
					this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("PatientVitalSignMonitoringItemCollectionByRegistrationNo", this._PatientVitalSignMonitoringItemCollectionByRegistrationNo);
				
					if(this.RegistrationNo != null && this.OrderNo != null)
					{
						this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.Query.Where(this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.Query.RegistrationNo == this.RegistrationNo);
						this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.Query.Where(this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.Query.OrderNo == this.OrderNo);
						this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.Query.Load();

						// Auto-hookup Foreign Keys
						this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.fks.Add(PatientVitalSignMonitoringItemMetadata.ColumnNames.RegistrationNo, this.RegistrationNo);
						this._PatientVitalSignMonitoringItemCollectionByRegistrationNo.fks.Add(PatientVitalSignMonitoringItemMetadata.ColumnNames.OrderNo, this.OrderNo);
					}
				}

				return this._PatientVitalSignMonitoringItemCollectionByRegistrationNo;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._PatientVitalSignMonitoringItemCollectionByRegistrationNo != null) 
				{ 
					this.RemovePostSave("PatientVitalSignMonitoringItemCollectionByRegistrationNo"); 
					this._PatientVitalSignMonitoringItemCollectionByRegistrationNo = null;
					
				} 
			} 			
		}

		private PatientVitalSignMonitoringItemCollection _PatientVitalSignMonitoringItemCollectionByRegistrationNo;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "PatientVitalSignMonitoringItemCollectionByRegistrationNo", typeof(PatientVitalSignMonitoringItemCollection), new PatientVitalSignMonitoringItem()));
		
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
	abstract public class esPatientVitalSignMonitoringQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PatientVitalSignMonitoringMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderNo
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.OrderNo, esSystemType.String);
			}
		} 
		
		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem StartingTime
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.StartingTime, esSystemType.String);
			}
		} 
		
		public esQueryItem EndingDate
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.EndingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EndingTime
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.EndingTime, esSystemType.String);
			}
		} 
		
		public esQueryItem Interval
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.Interval, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientVitalSignMonitoringCollection")]
	public partial class PatientVitalSignMonitoringCollection : esPatientVitalSignMonitoringCollection, IEnumerable<PatientVitalSignMonitoring>
	{
		public PatientVitalSignMonitoringCollection()
		{

		}
		
		public static implicit operator List<PatientVitalSignMonitoring>(PatientVitalSignMonitoringCollection coll)
		{
			List<PatientVitalSignMonitoring> list = new List<PatientVitalSignMonitoring>();
			
			foreach (PatientVitalSignMonitoring emp in coll)
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
				return  PatientVitalSignMonitoringMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientVitalSignMonitoringQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientVitalSignMonitoring(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientVitalSignMonitoring();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PatientVitalSignMonitoringQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientVitalSignMonitoringQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PatientVitalSignMonitoringQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PatientVitalSignMonitoring AddNew()
		{
			PatientVitalSignMonitoring entity = base.AddNewEntity() as PatientVitalSignMonitoring;
			
			return entity;
		}

		public PatientVitalSignMonitoring FindByPrimaryKey(System.String registrationNo, System.String orderNo)
		{
			return base.FindByPrimaryKey(registrationNo, orderNo) as PatientVitalSignMonitoring;
		}


		#region IEnumerable<PatientVitalSignMonitoring> Members

		IEnumerator<PatientVitalSignMonitoring> IEnumerable<PatientVitalSignMonitoring>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PatientVitalSignMonitoring;
			}
		}

		#endregion
		
		private PatientVitalSignMonitoringQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientVitalSignMonitoring' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PatientVitalSignMonitoring ({RegistrationNo},{OrderNo})")]
	[Serializable]
	public partial class PatientVitalSignMonitoring : esPatientVitalSignMonitoring
	{
		public PatientVitalSignMonitoring()
		{

		}
	
		public PatientVitalSignMonitoring(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientVitalSignMonitoringMetadata.Meta();
			}
		}
		
		
		
		override protected esPatientVitalSignMonitoringQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientVitalSignMonitoringQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PatientVitalSignMonitoringQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientVitalSignMonitoringQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PatientVitalSignMonitoringQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PatientVitalSignMonitoringQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PatientVitalSignMonitoringQuery : esPatientVitalSignMonitoringQuery
	{
		public PatientVitalSignMonitoringQuery()
		{

		}		
		
		public PatientVitalSignMonitoringQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PatientVitalSignMonitoringQuery";
        }
		
			
	}


	[Serializable]
	public partial class PatientVitalSignMonitoringMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientVitalSignMonitoringMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.OrderNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.OrderNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.StartingDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.StartingDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.StartingTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.StartingTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.EndingDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.EndingDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.EndingTime, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.EndingTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.Interval, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.Interval;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PatientVitalSignMonitoringMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientVitalSignMonitoringMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PatientVitalSignMonitoringMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string OrderNo = "OrderNo";
			 public const string StartingDate = "StartingDate";
			 public const string StartingTime = "StartingTime";
			 public const string EndingDate = "EndingDate";
			 public const string EndingTime = "EndingTime";
			 public const string Interval = "Interval";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string OrderNo = "OrderNo";
			 public const string StartingDate = "StartingDate";
			 public const string StartingTime = "StartingTime";
			 public const string EndingDate = "EndingDate";
			 public const string EndingTime = "EndingTime";
			 public const string Interval = "Interval";
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
			lock (typeof(PatientVitalSignMonitoringMetadata))
			{
				if(PatientVitalSignMonitoringMetadata.mapDelegates == null)
				{
					PatientVitalSignMonitoringMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PatientVitalSignMonitoringMetadata.meta == null)
				{
					PatientVitalSignMonitoringMetadata.meta = new PatientVitalSignMonitoringMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("StartingTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndingDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("EndingTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Interval", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PatientVitalSignMonitoring";
				meta.Destination = "PatientVitalSignMonitoring";
				
				meta.spInsert = "proc_PatientVitalSignMonitoringInsert";				
				meta.spUpdate = "proc_PatientVitalSignMonitoringUpdate";		
				meta.spDelete = "proc_PatientVitalSignMonitoringDelete";
				meta.spLoadAll = "proc_PatientVitalSignMonitoringLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientVitalSignMonitoringLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientVitalSignMonitoringMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
