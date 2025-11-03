/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/30/2015 11:15:33 AM
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
	abstract public class esVwTransChargesItemIsOrderOutstandingCollection : esEntityCollectionWAuditLog
	{
		public esVwTransChargesItemIsOrderOutstandingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwTransChargesItemIsOrderOutstandingCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwTransChargesItemIsOrderOutstandingQuery query)
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
			this.InitQuery(query as esVwTransChargesItemIsOrderOutstandingQuery);
		}
		#endregion
		
		virtual public VwTransChargesItemIsOrderOutstanding DetachEntity(VwTransChargesItemIsOrderOutstanding entity)
		{
			return base.DetachEntity(entity) as VwTransChargesItemIsOrderOutstanding;
		}
		
		virtual public VwTransChargesItemIsOrderOutstanding AttachEntity(VwTransChargesItemIsOrderOutstanding entity)
		{
			return base.AttachEntity(entity) as VwTransChargesItemIsOrderOutstanding;
		}
		
		virtual public void Combine(VwTransChargesItemIsOrderOutstandingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwTransChargesItemIsOrderOutstanding this[int index]
		{
			get
			{
				return base[index] as VwTransChargesItemIsOrderOutstanding;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwTransChargesItemIsOrderOutstanding);
		}
	}



	[Serializable]
	abstract public class esVwTransChargesItemIsOrderOutstanding : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwTransChargesItemIsOrderOutstandingQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwTransChargesItemIsOrderOutstanding()
		{

		}

		public esVwTransChargesItemIsOrderOutstanding(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "FromServiceUnitID": this.str.FromServiceUnitID = (string)value; break;							
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "RoomID": this.str.RoomID = (string)value; break;							
						case "BedID": this.str.BedID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
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
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.FromServiceUnitID
		/// </summary>
		virtual public System.String FromServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.FromServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.FromServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ToServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItem_IsOrderOutstanding.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.BedID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.BedID, value);
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
			public esStrings(esVwTransChargesItemIsOrderOutstanding entity)
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
				
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
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
				
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
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
				
			public System.String BedID
			{
				get
				{
					System.String data = entity.BedID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BedID = null;
					else entity.BedID = Convert.ToString(value);
				}
			}
			

			private esVwTransChargesItemIsOrderOutstanding entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwTransChargesItemIsOrderOutstandingQuery query)
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
				throw new Exception("esVwTransChargesItemIsOrderOutstanding can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwTransChargesItemIsOrderOutstandingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesItemIsOrderOutstandingMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem FromServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.FromServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.BedID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwTransChargesItemIsOrderOutstandingCollection")]
	public partial class VwTransChargesItemIsOrderOutstandingCollection : esVwTransChargesItemIsOrderOutstandingCollection, IEnumerable<VwTransChargesItemIsOrderOutstanding>
	{
		public VwTransChargesItemIsOrderOutstandingCollection()
		{

		}
		
		public static implicit operator List<VwTransChargesItemIsOrderOutstanding>(VwTransChargesItemIsOrderOutstandingCollection coll)
		{
			List<VwTransChargesItemIsOrderOutstanding> list = new List<VwTransChargesItemIsOrderOutstanding>();
			
			foreach (VwTransChargesItemIsOrderOutstanding emp in coll)
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
				return  VwTransChargesItemIsOrderOutstandingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesItemIsOrderOutstandingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwTransChargesItemIsOrderOutstanding(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwTransChargesItemIsOrderOutstanding();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwTransChargesItemIsOrderOutstandingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesItemIsOrderOutstandingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwTransChargesItemIsOrderOutstandingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwTransChargesItemIsOrderOutstanding AddNew()
		{
			VwTransChargesItemIsOrderOutstanding entity = base.AddNewEntity() as VwTransChargesItemIsOrderOutstanding;
			
			return entity;
		}


		#region IEnumerable<VwTransChargesItemIsOrderOutstanding> Members

		IEnumerator<VwTransChargesItemIsOrderOutstanding> IEnumerable<VwTransChargesItemIsOrderOutstanding>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwTransChargesItemIsOrderOutstanding;
			}
		}

		#endregion
		
		private VwTransChargesItemIsOrderOutstandingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_TransChargesItem_IsOrderOutstanding' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwTransChargesItemIsOrderOutstanding ()")]
	[Serializable]
	public partial class VwTransChargesItemIsOrderOutstanding : esVwTransChargesItemIsOrderOutstanding
	{
		public VwTransChargesItemIsOrderOutstanding()
		{

		}
	
		public VwTransChargesItemIsOrderOutstanding(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesItemIsOrderOutstandingMetadata.Meta();
			}
		}
		
		
		
		override protected esVwTransChargesItemIsOrderOutstandingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesItemIsOrderOutstandingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwTransChargesItemIsOrderOutstandingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesItemIsOrderOutstandingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwTransChargesItemIsOrderOutstandingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwTransChargesItemIsOrderOutstandingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwTransChargesItemIsOrderOutstandingQuery : esVwTransChargesItemIsOrderOutstandingQuery
	{
		public VwTransChargesItemIsOrderOutstandingQuery()
		{

		}		
		
		public VwTransChargesItemIsOrderOutstandingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwTransChargesItemIsOrderOutstandingQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwTransChargesItemIsOrderOutstandingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwTransChargesItemIsOrderOutstandingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.TransactionDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.FromServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.FromServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ToServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.ReferenceNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.RoomID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemIsOrderOutstandingMetadata.ColumnNames.BedID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemIsOrderOutstandingMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwTransChargesItemIsOrderOutstandingMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string FromServiceUnitID = "FromServiceUnitID";
			 public const string ToServiceUnitID = "ToServiceUnitID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string RoomID = "RoomID";
			 public const string BedID = "BedID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string FromServiceUnitID = "FromServiceUnitID";
			 public const string ToServiceUnitID = "ToServiceUnitID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string RoomID = "RoomID";
			 public const string BedID = "BedID";
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
			lock (typeof(VwTransChargesItemIsOrderOutstandingMetadata))
			{
				if(VwTransChargesItemIsOrderOutstandingMetadata.mapDelegates == null)
				{
					VwTransChargesItemIsOrderOutstandingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwTransChargesItemIsOrderOutstandingMetadata.meta == null)
				{
					VwTransChargesItemIsOrderOutstandingMetadata.meta = new VwTransChargesItemIsOrderOutstandingMetadata();
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
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FromServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "vw_TransChargesItem_IsOrderOutstanding";
				meta.Destination = "vw_TransChargesItem_IsOrderOutstanding";
				
				meta.spInsert = "proc_vw_TransChargesItem_IsOrderOutstandingInsert";				
				meta.spUpdate = "proc_vw_TransChargesItem_IsOrderOutstandingUpdate";		
				meta.spDelete = "proc_vw_TransChargesItem_IsOrderOutstandingDelete";
				meta.spLoadAll = "proc_vw_TransChargesItem_IsOrderOutstandingLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_TransChargesItem_IsOrderOutstandingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwTransChargesItemIsOrderOutstandingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
