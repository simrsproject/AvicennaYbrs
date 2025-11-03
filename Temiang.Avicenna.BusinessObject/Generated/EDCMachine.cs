/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:14 PM
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
	abstract public class esEDCMachineCollection : esEntityCollectionWAuditLog
	{
		public esEDCMachineCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EDCMachineCollection";
		}

		#region Query Logic
		protected void InitQuery(esEDCMachineQuery query)
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
			this.InitQuery(query as esEDCMachineQuery);
		}
		#endregion
		
		virtual public EDCMachine DetachEntity(EDCMachine entity)
		{
			return base.DetachEntity(entity) as EDCMachine;
		}
		
		virtual public EDCMachine AttachEntity(EDCMachine entity)
		{
			return base.AttachEntity(entity) as EDCMachine;
		}
		
		virtual public void Combine(EDCMachineCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EDCMachine this[int index]
		{
			get
			{
				return base[index] as EDCMachine;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EDCMachine);
		}
	}



	[Serializable]
	abstract public class esEDCMachine : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEDCMachineQuery GetDynamicQuery()
		{
			return null;
		}

		public esEDCMachine()
		{

		}

		public esEDCMachine(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String eDCMachineID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(eDCMachineID);
			else
				return LoadByPrimaryKeyStoredProcedure(eDCMachineID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String eDCMachineID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(eDCMachineID);
			else
				return LoadByPrimaryKeyStoredProcedure(eDCMachineID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String eDCMachineID)
		{
			esEDCMachineQuery query = this.GetDynamicQuery();
			query.Where(query.EDCMachineID == eDCMachineID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String eDCMachineID)
		{
			esParameters parms = new esParameters();
			parms.Add("EDCMachineID",eDCMachineID);
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
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;							
						case "SRCardProvider": this.str.SRCardProvider = (string)value; break;							
						case "EDCMachineName": this.str.EDCMachineName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "ChartOfAccountID": this.str.ChartOfAccountID = (string)value; break;							
						case "SubledgerID": this.str.SubledgerID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "ChartOfAccountID":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountID = (System.Int32?)value;
							break;
						
						case "SubledgerID":
						
							if (value == null || value is System.Int32)
								this.SubledgerID = (System.Int32?)value;
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
		/// Maps to EDCMachine.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(EDCMachineMetadata.ColumnNames.EDCMachineID);
			}
			
			set
			{
				base.SetSystemString(EDCMachineMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		
		/// <summary>
		/// Lippo, BCA, BNI
		/// </summary>
		virtual public System.String SRCardProvider
		{
			get
			{
				return base.GetSystemString(EDCMachineMetadata.ColumnNames.SRCardProvider);
			}
			
			set
			{
				base.SetSystemString(EDCMachineMetadata.ColumnNames.SRCardProvider, value);
			}
		}
		
		/// <summary>
		/// Maps to EDCMachine.EDCMachineName
		/// </summary>
		virtual public System.String EDCMachineName
		{
			get
			{
				return base.GetSystemString(EDCMachineMetadata.ColumnNames.EDCMachineName);
			}
			
			set
			{
				base.SetSystemString(EDCMachineMetadata.ColumnNames.EDCMachineName, value);
			}
		}
		
		/// <summary>
		/// Maps to EDCMachine.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(EDCMachineMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(EDCMachineMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to EDCMachine.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EDCMachineMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EDCMachineMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EDCMachine.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EDCMachineMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EDCMachineMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EDCMachine.ChartOfAccountID
		/// </summary>
		virtual public System.Int32? ChartOfAccountID
		{
			get
			{
				return base.GetSystemInt32(EDCMachineMetadata.ColumnNames.ChartOfAccountID);
			}
			
			set
			{
				base.SetSystemInt32(EDCMachineMetadata.ColumnNames.ChartOfAccountID, value);
			}
		}
		
		/// <summary>
		/// Maps to EDCMachine.SubledgerID
		/// </summary>
		virtual public System.Int32? SubledgerID
		{
			get
			{
				return base.GetSystemInt32(EDCMachineMetadata.ColumnNames.SubledgerID);
			}
			
			set
			{
				base.SetSystemInt32(EDCMachineMetadata.ColumnNames.SubledgerID, value);
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
			public esStrings(esEDCMachine entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EDCMachineID
			{
				get
				{
					System.String data = entity.EDCMachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineID = null;
					else entity.EDCMachineID = Convert.ToString(value);
				}
			}
				
			public System.String SRCardProvider
			{
				get
				{
					System.String data = entity.SRCardProvider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardProvider = null;
					else entity.SRCardProvider = Convert.ToString(value);
				}
			}
				
			public System.String EDCMachineName
			{
				get
				{
					System.String data = entity.EDCMachineName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineName = null;
					else entity.EDCMachineName = Convert.ToString(value);
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
				
			public System.String ChartOfAccountID
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountID = null;
					else entity.ChartOfAccountID = Convert.ToInt32(value);
				}
			}
				
			public System.String SubledgerID
			{
				get
				{
					System.Int32? data = entity.SubledgerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerID = null;
					else entity.SubledgerID = Convert.ToInt32(value);
				}
			}
			

			private esEDCMachine entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEDCMachineQuery query)
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
				throw new Exception("esEDCMachine can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EDCMachine : esEDCMachine
	{

				
		#region EDCMachineTariffCollectionByEDCMachineID - Zero To Many
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - RefEDCMachineToCreditCard
		/// </summary>

		[XmlIgnore]
		public EDCMachineTariffCollection EDCMachineTariffCollectionByEDCMachineID
		{
			get
			{
				if(this._EDCMachineTariffCollectionByEDCMachineID == null)
				{
					this._EDCMachineTariffCollectionByEDCMachineID = new EDCMachineTariffCollection();
					this._EDCMachineTariffCollectionByEDCMachineID.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("EDCMachineTariffCollectionByEDCMachineID", this._EDCMachineTariffCollectionByEDCMachineID);
				
					if(this.EDCMachineID != null)
					{
						this._EDCMachineTariffCollectionByEDCMachineID.Query.Where(this._EDCMachineTariffCollectionByEDCMachineID.Query.EDCMachineID == this.EDCMachineID);
						this._EDCMachineTariffCollectionByEDCMachineID.Query.Load();

						// Auto-hookup Foreign Keys
						this._EDCMachineTariffCollectionByEDCMachineID.fks.Add(EDCMachineTariffMetadata.ColumnNames.EDCMachineID, this.EDCMachineID);
					}
				}

				return this._EDCMachineTariffCollectionByEDCMachineID;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._EDCMachineTariffCollectionByEDCMachineID != null) 
				{ 
					this.RemovePostSave("EDCMachineTariffCollectionByEDCMachineID"); 
					this._EDCMachineTariffCollectionByEDCMachineID = null;
					
				} 
			} 			
		}

		private EDCMachineTariffCollection _EDCMachineTariffCollectionByEDCMachineID;
		#endregion

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "EDCMachineTariffCollectionByEDCMachineID", typeof(EDCMachineTariffCollection), new EDCMachineTariff()));
		
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
	abstract public class esEDCMachineQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EDCMachineMetadata.Meta();
			}
		}	
		

		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCardProvider
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.SRCardProvider, esSystemType.String);
			}
		} 
		
		public esQueryItem EDCMachineName
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.EDCMachineName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ChartOfAccountID
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.ChartOfAccountID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubledgerID
		{
			get
			{
				return new esQueryItem(this, EDCMachineMetadata.ColumnNames.SubledgerID, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EDCMachineCollection")]
	public partial class EDCMachineCollection : esEDCMachineCollection, IEnumerable<EDCMachine>
	{
		public EDCMachineCollection()
		{

		}
		
		public static implicit operator List<EDCMachine>(EDCMachineCollection coll)
		{
			List<EDCMachine> list = new List<EDCMachine>();
			
			foreach (EDCMachine emp in coll)
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
				return  EDCMachineMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EDCMachineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EDCMachine(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EDCMachine();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EDCMachineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EDCMachineQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EDCMachineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EDCMachine AddNew()
		{
			EDCMachine entity = base.AddNewEntity() as EDCMachine;
			
			return entity;
		}

		public EDCMachine FindByPrimaryKey(System.String eDCMachineID)
		{
			return base.FindByPrimaryKey(eDCMachineID) as EDCMachine;
		}


		#region IEnumerable<EDCMachine> Members

		IEnumerator<EDCMachine> IEnumerable<EDCMachine>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EDCMachine;
			}
		}

		#endregion
		
		private EDCMachineQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EDCMachine' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EDCMachine ({EDCMachineID})")]
	[Serializable]
	public partial class EDCMachine : esEDCMachine
	{
		public EDCMachine()
		{

		}
	
		public EDCMachine(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EDCMachineMetadata.Meta();
			}
		}
		
		
		
		override protected esEDCMachineQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EDCMachineQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EDCMachineQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EDCMachineQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EDCMachineQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EDCMachineQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EDCMachineQuery : esEDCMachineQuery
	{
		public EDCMachineQuery()
		{

		}		
		
		public EDCMachineQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EDCMachineQuery";
        }
		
			
	}


	[Serializable]
	public partial class EDCMachineMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EDCMachineMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.EDCMachineID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EDCMachineMetadata.PropertyNames.EDCMachineID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.SRCardProvider, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EDCMachineMetadata.PropertyNames.SRCardProvider;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.Description = "Lippo, BCA, BNI";
			_columns.Add(c);
				
			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.EDCMachineName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EDCMachineMetadata.PropertyNames.EDCMachineName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EDCMachineMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EDCMachineMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EDCMachineMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.ChartOfAccountID, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EDCMachineMetadata.PropertyNames.ChartOfAccountID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EDCMachineMetadata.ColumnNames.SubledgerID, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EDCMachineMetadata.PropertyNames.SubledgerID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EDCMachineMetadata Meta()
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
			 public const string EDCMachineID = "EDCMachineID";
			 public const string SRCardProvider = "SRCardProvider";
			 public const string EDCMachineName = "EDCMachineName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string ChartOfAccountID = "ChartOfAccountID";
			 public const string SubledgerID = "SubledgerID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EDCMachineID = "EDCMachineID";
			 public const string SRCardProvider = "SRCardProvider";
			 public const string EDCMachineName = "EDCMachineName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string ChartOfAccountID = "ChartOfAccountID";
			 public const string SubledgerID = "SubledgerID";
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
			lock (typeof(EDCMachineMetadata))
			{
				if(EDCMachineMetadata.mapDelegates == null)
				{
					EDCMachineMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EDCMachineMetadata.meta == null)
				{
					EDCMachineMetadata.meta = new EDCMachineMetadata();
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
				

				meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardProvider", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EDCMachineName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ChartOfAccountID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerID", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "EDCMachine";
				meta.Destination = "EDCMachine";
				
				meta.spInsert = "proc_EDCMachineInsert";				
				meta.spUpdate = "proc_EDCMachineUpdate";		
				meta.spDelete = "proc_EDCMachineDelete";
				meta.spLoadAll = "proc_EDCMachineLoadAll";
				meta.spLoadByPrimaryKey = "proc_EDCMachineLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EDCMachineMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
