/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/10/2014 12:23:19 PM
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
	abstract public class esTmpItemRequestMaintenanceCollection : esEntityCollectionWAuditLog
	{
		public esTmpItemRequestMaintenanceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TmpItemRequestMaintenanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esTmpItemRequestMaintenanceQuery query)
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
			this.InitQuery(query as esTmpItemRequestMaintenanceQuery);
		}
		#endregion
		
		virtual public TmpItemRequestMaintenance DetachEntity(TmpItemRequestMaintenance entity)
		{
			return base.DetachEntity(entity) as TmpItemRequestMaintenance;
		}
		
		virtual public TmpItemRequestMaintenance AttachEntity(TmpItemRequestMaintenance entity)
		{
			return base.AttachEntity(entity) as TmpItemRequestMaintenance;
		}
		
		virtual public void Combine(TmpItemRequestMaintenanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TmpItemRequestMaintenance this[int index]
		{
			get
			{
				return base[index] as TmpItemRequestMaintenance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TmpItemRequestMaintenance);
		}
	}



	[Serializable]
	abstract public class esTmpItemRequestMaintenance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTmpItemRequestMaintenanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esTmpItemRequestMaintenance()
		{

		}

		public esTmpItemRequestMaintenance(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String userID, System.DateTime transDate, System.String toServiceUnitID, System.String followUpID, System.String transactionNo, System.String sequenceNo, System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID, transDate, toServiceUnitID, followUpID, transactionNo, sequenceNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID, transDate, toServiceUnitID, followUpID, transactionNo, sequenceNo, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String userID, System.DateTime transDate, System.String toServiceUnitID, System.String followUpID, System.String transactionNo, System.String sequenceNo, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID, transDate, toServiceUnitID, followUpID, transactionNo, sequenceNo, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID, transDate, toServiceUnitID, followUpID, transactionNo, sequenceNo, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String userID, System.DateTime transDate, System.String toServiceUnitID, System.String followUpID, System.String transactionNo, System.String sequenceNo, System.String itemID)
		{
			esTmpItemRequestMaintenanceQuery query = this.GetDynamicQuery();
			query.Where(query.UserID == userID, query.TransDate == transDate, query.ToServiceUnitID == toServiceUnitID, query.FollowUpID == followUpID, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String userID, System.DateTime transDate, System.String toServiceUnitID, System.String followUpID, System.String transactionNo, System.String sequenceNo, System.String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("UserID",userID);			parms.Add("TransDate",transDate);			parms.Add("ToServiceUnitID",toServiceUnitID);			parms.Add("FollowUpID",followUpID);			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("ItemID",itemID);
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
						case "UserID": this.str.UserID = (string)value; break;							
						case "TransDate": this.str.TransDate = (string)value; break;							
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;							
						case "FollowUpID": this.str.FollowUpID = (string)value; break;							
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "Quantity": this.str.Quantity = (string)value; break;							
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;							
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransDate":
						
							if (value == null || value is System.DateTime)
								this.TransDate = (System.DateTime?)value;
							break;
						
						case "Quantity":
						
							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						
						case "ConversionFactor":
						
							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
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
		/// Maps to TmpItemRequestMaintenance.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.UserID);
			}
			
			set
			{
				base.SetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.UserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.TransDate
		/// </summary>
		virtual public System.DateTime? TransDate
		{
			get
			{
				return base.GetSystemDateTime(TmpItemRequestMaintenanceMetadata.ColumnNames.TransDate);
			}
			
			set
			{
				base.SetSystemDateTime(TmpItemRequestMaintenanceMetadata.ColumnNames.TransDate, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.ToServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.FollowUpID
		/// </summary>
		virtual public System.String FollowUpID
		{
			get
			{
				return base.GetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.FollowUpID);
			}
			
			set
			{
				base.SetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.FollowUpID, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(TmpItemRequestMaintenanceMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				base.SetSystemDecimal(TmpItemRequestMaintenanceMetadata.ColumnNames.Quantity, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(TmpItemRequestMaintenanceMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to TmpItemRequestMaintenance.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(TmpItemRequestMaintenanceMetadata.ColumnNames.ConversionFactor);
			}
			
			set
			{
				base.SetSystemDecimal(TmpItemRequestMaintenanceMetadata.ColumnNames.ConversionFactor, value);
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
			public esStrings(esTmpItemRequestMaintenance entity)
			{
				this.entity = entity;
			}
			
	
			public System.String UserID
			{
				get
				{
					System.String data = entity.UserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserID = null;
					else entity.UserID = Convert.ToString(value);
				}
			}
				
			public System.String TransDate
			{
				get
				{
					System.DateTime? data = entity.TransDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransDate = null;
					else entity.TransDate = Convert.ToDateTime(value);
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
				
			public System.String FollowUpID
			{
				get
				{
					System.String data = entity.FollowUpID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FollowUpID = null;
					else entity.FollowUpID = Convert.ToString(value);
				}
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
				
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
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
				
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
				}
			}
				
			public System.String ConversionFactor
			{
				get
				{
					System.Decimal? data = entity.ConversionFactor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConversionFactor = null;
					else entity.ConversionFactor = Convert.ToDecimal(value);
				}
			}
			

			private esTmpItemRequestMaintenance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTmpItemRequestMaintenanceQuery query)
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
				throw new Exception("esTmpItemRequestMaintenance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TmpItemRequestMaintenance : esTmpItemRequestMaintenance
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
	abstract public class esTmpItemRequestMaintenanceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TmpItemRequestMaintenanceMetadata.Meta();
			}
		}	
		

		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.UserID, esSystemType.String);
			}
		} 
		
		public esQueryItem TransDate
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.TransDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem FollowUpID
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.FollowUpID, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, TmpItemRequestMaintenanceMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TmpItemRequestMaintenanceCollection")]
	public partial class TmpItemRequestMaintenanceCollection : esTmpItemRequestMaintenanceCollection, IEnumerable<TmpItemRequestMaintenance>
	{
		public TmpItemRequestMaintenanceCollection()
		{

		}
		
		public static implicit operator List<TmpItemRequestMaintenance>(TmpItemRequestMaintenanceCollection coll)
		{
			List<TmpItemRequestMaintenance> list = new List<TmpItemRequestMaintenance>();
			
			foreach (TmpItemRequestMaintenance emp in coll)
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
				return  TmpItemRequestMaintenanceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TmpItemRequestMaintenanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TmpItemRequestMaintenance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TmpItemRequestMaintenance();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TmpItemRequestMaintenanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TmpItemRequestMaintenanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TmpItemRequestMaintenanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TmpItemRequestMaintenance AddNew()
		{
			TmpItemRequestMaintenance entity = base.AddNewEntity() as TmpItemRequestMaintenance;
			
			return entity;
		}

		public TmpItemRequestMaintenance FindByPrimaryKey(System.String userID, System.DateTime transDate, System.String toServiceUnitID, System.String followUpID, System.String transactionNo, System.String sequenceNo, System.String itemID)
		{
			return base.FindByPrimaryKey(userID, transDate, toServiceUnitID, followUpID, transactionNo, sequenceNo, itemID) as TmpItemRequestMaintenance;
		}


		#region IEnumerable<TmpItemRequestMaintenance> Members

		IEnumerator<TmpItemRequestMaintenance> IEnumerable<TmpItemRequestMaintenance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TmpItemRequestMaintenance;
			}
		}

		#endregion
		
		private TmpItemRequestMaintenanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TmpItemRequestMaintenance' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TmpItemRequestMaintenance ({UserID},{TransDate},{ToServiceUnitID},{FollowUpID},{TransactionNo},{SequenceNo},{ItemID})")]
	[Serializable]
	public partial class TmpItemRequestMaintenance : esTmpItemRequestMaintenance
	{
		public TmpItemRequestMaintenance()
		{

		}
	
		public TmpItemRequestMaintenance(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TmpItemRequestMaintenanceMetadata.Meta();
			}
		}
		
		
		
		override protected esTmpItemRequestMaintenanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TmpItemRequestMaintenanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TmpItemRequestMaintenanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TmpItemRequestMaintenanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TmpItemRequestMaintenanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TmpItemRequestMaintenanceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TmpItemRequestMaintenanceQuery : esTmpItemRequestMaintenanceQuery
	{
		public TmpItemRequestMaintenanceQuery()
		{

		}		
		
		public TmpItemRequestMaintenanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TmpItemRequestMaintenanceQuery";
        }
		
			
	}


	[Serializable]
	public partial class TmpItemRequestMaintenanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TmpItemRequestMaintenanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.UserID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.UserID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.TransDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.TransDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.ToServiceUnitID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.ToServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.FollowUpID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.FollowUpID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.TransactionNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.SequenceNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.ItemID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.Quantity, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.SRItemUnit, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TmpItemRequestMaintenanceMetadata.ColumnNames.ConversionFactor, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TmpItemRequestMaintenanceMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TmpItemRequestMaintenanceMetadata Meta()
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
			 public const string UserID = "UserID";
			 public const string TransDate = "TransDate";
			 public const string ToServiceUnitID = "ToServiceUnitID";
			 public const string FollowUpID = "FollowUpID";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ItemID = "ItemID";
			 public const string Quantity = "Quantity";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string UserID = "UserID";
			 public const string TransDate = "TransDate";
			 public const string ToServiceUnitID = "ToServiceUnitID";
			 public const string FollowUpID = "FollowUpID";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ItemID = "ItemID";
			 public const string Quantity = "Quantity";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
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
			lock (typeof(TmpItemRequestMaintenanceMetadata))
			{
				if(TmpItemRequestMaintenanceMetadata.mapDelegates == null)
				{
					TmpItemRequestMaintenanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TmpItemRequestMaintenanceMetadata.meta == null)
				{
					TmpItemRequestMaintenanceMetadata.meta = new TmpItemRequestMaintenanceMetadata();
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
				

				meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FollowUpID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "TmpItemRequestMaintenance";
				meta.Destination = "TmpItemRequestMaintenance";
				
				meta.spInsert = "proc_TmpItemRequestMaintenanceInsert";				
				meta.spUpdate = "proc_TmpItemRequestMaintenanceUpdate";		
				meta.spDelete = "proc_TmpItemRequestMaintenanceDelete";
				meta.spLoadAll = "proc_TmpItemRequestMaintenanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_TmpItemRequestMaintenanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TmpItemRequestMaintenanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
