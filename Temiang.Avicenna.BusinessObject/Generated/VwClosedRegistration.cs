/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/30/2013 8:13:03 PM
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
	abstract public class esVwClosedRegistrationCollection : esEntityCollectionWAuditLog
	{
		public esVwClosedRegistrationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwClosedRegistrationCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwClosedRegistrationQuery query)
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
			this.InitQuery(query as esVwClosedRegistrationQuery);
		}
		#endregion
		
		virtual public VwClosedRegistration DetachEntity(VwClosedRegistration entity)
		{
			return base.DetachEntity(entity) as VwClosedRegistration;
		}
		
		virtual public VwClosedRegistration AttachEntity(VwClosedRegistration entity)
		{
			return base.AttachEntity(entity) as VwClosedRegistration;
		}
		
		virtual public void Combine(VwClosedRegistrationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwClosedRegistration this[int index]
		{
			get
			{
				return base[index] as VwClosedRegistration;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwClosedRegistration);
		}
	}



	[Serializable]
	abstract public class esVwClosedRegistration : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwClosedRegistrationQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwClosedRegistration()
		{

		}

		public esVwClosedRegistration(DataRow row)
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "RoomID": this.str.RoomID = (string)value; break;							
						case "BedID": this.str.BedID = (string)value; break;							
						case "DischargeDate": this.str.DischargeDate = (string)value; break;							
						case "ParamedicIDReferral": this.str.ParamedicIDReferral = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DischargeDate":
						
							if (value == null || value is System.DateTime)
								this.DischargeDate = (System.DateTime?)value;
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
		/// Maps to vw_ClosedRegistration.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwClosedRegistrationMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwClosedRegistrationMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ClosedRegistration.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwClosedRegistrationMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(VwClosedRegistrationMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ClosedRegistration.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(VwClosedRegistrationMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(VwClosedRegistrationMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ClosedRegistration.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(VwClosedRegistrationMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(VwClosedRegistrationMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ClosedRegistration.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(VwClosedRegistrationMetadata.ColumnNames.RoomID);
			}
			
			set
			{
				base.SetSystemString(VwClosedRegistrationMetadata.ColumnNames.RoomID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ClosedRegistration.BedID
		/// </summary>
		virtual public System.String BedID
		{
			get
			{
				return base.GetSystemString(VwClosedRegistrationMetadata.ColumnNames.BedID);
			}
			
			set
			{
				base.SetSystemString(VwClosedRegistrationMetadata.ColumnNames.BedID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ClosedRegistration.DischargeDate
		/// </summary>
		virtual public System.DateTime? DischargeDate
		{
			get
			{
				return base.GetSystemDateTime(VwClosedRegistrationMetadata.ColumnNames.DischargeDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwClosedRegistrationMetadata.ColumnNames.DischargeDate, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ClosedRegistration.ParamedicIDReferral
		/// </summary>
		virtual public System.String ParamedicIDReferral
		{
			get
			{
				return base.GetSystemString(VwClosedRegistrationMetadata.ColumnNames.ParamedicIDReferral);
			}
			
			set
			{
				base.SetSystemString(VwClosedRegistrationMetadata.ColumnNames.ParamedicIDReferral, value);
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
			public esStrings(esVwClosedRegistration entity)
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
				
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
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
				
			public System.String DischargeDate
			{
				get
				{
					System.DateTime? data = entity.DischargeDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDate = null;
					else entity.DischargeDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ParamedicIDReferral
			{
				get
				{
					System.String data = entity.ParamedicIDReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicIDReferral = null;
					else entity.ParamedicIDReferral = Convert.ToString(value);
				}
			}
			

			private esVwClosedRegistration entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwClosedRegistrationQuery query)
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
				throw new Exception("esVwClosedRegistration can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwClosedRegistrationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwClosedRegistrationMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		} 
		
		public esQueryItem BedID
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.BedID, esSystemType.String);
			}
		} 
		
		public esQueryItem DischargeDate
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.DischargeDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ParamedicIDReferral
		{
			get
			{
				return new esQueryItem(this, VwClosedRegistrationMetadata.ColumnNames.ParamedicIDReferral, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwClosedRegistrationCollection")]
	public partial class VwClosedRegistrationCollection : esVwClosedRegistrationCollection, IEnumerable<VwClosedRegistration>
	{
		public VwClosedRegistrationCollection()
		{

		}
		
		public static implicit operator List<VwClosedRegistration>(VwClosedRegistrationCollection coll)
		{
			List<VwClosedRegistration> list = new List<VwClosedRegistration>();
			
			foreach (VwClosedRegistration emp in coll)
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
				return  VwClosedRegistrationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwClosedRegistrationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwClosedRegistration(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwClosedRegistration();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwClosedRegistrationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwClosedRegistrationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwClosedRegistrationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwClosedRegistration AddNew()
		{
			VwClosedRegistration entity = base.AddNewEntity() as VwClosedRegistration;
			
			return entity;
		}


		#region IEnumerable<VwClosedRegistration> Members

		IEnumerator<VwClosedRegistration> IEnumerable<VwClosedRegistration>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwClosedRegistration;
			}
		}

		#endregion
		
		private VwClosedRegistrationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ClosedRegistration' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwClosedRegistration ()")]
	[Serializable]
	public partial class VwClosedRegistration : esVwClosedRegistration
	{
		public VwClosedRegistration()
		{

		}
	
		public VwClosedRegistration(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwClosedRegistrationMetadata.Meta();
			}
		}
		
		
		
		override protected esVwClosedRegistrationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwClosedRegistrationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwClosedRegistrationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwClosedRegistrationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwClosedRegistrationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwClosedRegistrationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwClosedRegistrationQuery : esVwClosedRegistrationQuery
	{
		public VwClosedRegistrationQuery()
		{

		}		
		
		public VwClosedRegistrationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwClosedRegistrationQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwClosedRegistrationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwClosedRegistrationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.PatientID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.RoomID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.BedID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.BedID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.DischargeDate, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.DischargeDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwClosedRegistrationMetadata.ColumnNames.ParamedicIDReferral, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwClosedRegistrationMetadata.PropertyNames.ParamedicIDReferral;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwClosedRegistrationMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ParamedicID = "ParamedicID";
			 public const string PatientID = "PatientID";
			 public const string RoomID = "RoomID";
			 public const string BedID = "BedID";
			 public const string DischargeDate = "DischargeDate";
			 public const string ParamedicIDReferral = "ParamedicIDReferral";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ParamedicID = "ParamedicID";
			 public const string PatientID = "PatientID";
			 public const string RoomID = "RoomID";
			 public const string BedID = "BedID";
			 public const string DischargeDate = "DischargeDate";
			 public const string ParamedicIDReferral = "ParamedicIDReferral";
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
			lock (typeof(VwClosedRegistrationMetadata))
			{
				if(VwClosedRegistrationMetadata.mapDelegates == null)
				{
					VwClosedRegistrationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwClosedRegistrationMetadata.meta == null)
				{
					VwClosedRegistrationMetadata.meta = new VwClosedRegistrationMetadata();
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
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicIDReferral", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "vw_ClosedRegistration";
				meta.Destination = "vw_ClosedRegistration";
				
				meta.spInsert = "proc_vw_ClosedRegistrationInsert";				
				meta.spUpdate = "proc_vw_ClosedRegistrationUpdate";		
				meta.spDelete = "proc_vw_ClosedRegistrationDelete";
				meta.spLoadAll = "proc_vw_ClosedRegistrationLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ClosedRegistrationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwClosedRegistrationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
