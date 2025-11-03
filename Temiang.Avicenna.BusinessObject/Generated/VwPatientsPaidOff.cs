/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/21/2011 4:03:07 PM
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
	abstract public class esVwPatientsPaidOffCollection : esEntityCollectionWAuditLog
	{
		public esVwPatientsPaidOffCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwPatientsPaidOffCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwPatientsPaidOffQuery query)
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
			this.InitQuery(query as esVwPatientsPaidOffQuery);
		}
		#endregion
		
		virtual public VwPatientsPaidOff DetachEntity(VwPatientsPaidOff entity)
		{
			return base.DetachEntity(entity) as VwPatientsPaidOff;
		}
		
		virtual public VwPatientsPaidOff AttachEntity(VwPatientsPaidOff entity)
		{
			return base.AttachEntity(entity) as VwPatientsPaidOff;
		}
		
		virtual public void Combine(VwPatientsPaidOffCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwPatientsPaidOff this[int index]
		{
			get
			{
				return base[index] as VwPatientsPaidOff;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwPatientsPaidOff);
		}
	}



	[Serializable]
	abstract public class esVwPatientsPaidOff : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwPatientsPaidOffQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwPatientsPaidOff()
		{

		}

		public esVwPatientsPaidOff(DataRow row)
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
						case "IsPaidOff": this.str.IsPaidOff = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsPaidOff":
						
							if (value == null || value is System.Boolean)
								this.IsPaidOff = (System.Boolean?)value;
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
		/// Maps to vw_PatientsPaidOff.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwPatientsPaidOffMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwPatientsPaidOffMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_PatientsPaidOff.IsPaidOff
		/// </summary>
		virtual public System.Boolean? IsPaidOff
		{
			get
			{
				return base.GetSystemBoolean(VwPatientsPaidOffMetadata.ColumnNames.IsPaidOff);
			}
			
			set
			{
				base.SetSystemBoolean(VwPatientsPaidOffMetadata.ColumnNames.IsPaidOff, value);
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
			public esStrings(esVwPatientsPaidOff entity)
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
				
			public System.String IsPaidOff
			{
				get
				{
					System.Boolean? data = entity.IsPaidOff;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPaidOff = null;
					else entity.IsPaidOff = Convert.ToBoolean(value);
				}
			}
			

			private esVwPatientsPaidOff entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwPatientsPaidOffQuery query)
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
				throw new Exception("esVwPatientsPaidOff can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwPatientsPaidOffQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwPatientsPaidOffMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwPatientsPaidOffMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPaidOff
		{
			get
			{
				return new esQueryItem(this, VwPatientsPaidOffMetadata.ColumnNames.IsPaidOff, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwPatientsPaidOffCollection")]
	public partial class VwPatientsPaidOffCollection : esVwPatientsPaidOffCollection, IEnumerable<VwPatientsPaidOff>
	{
		public VwPatientsPaidOffCollection()
		{

		}
		
		public static implicit operator List<VwPatientsPaidOff>(VwPatientsPaidOffCollection coll)
		{
			List<VwPatientsPaidOff> list = new List<VwPatientsPaidOff>();
			
			foreach (VwPatientsPaidOff emp in coll)
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
				return  VwPatientsPaidOffMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwPatientsPaidOffQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwPatientsPaidOff(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwPatientsPaidOff();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwPatientsPaidOffQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwPatientsPaidOffQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwPatientsPaidOffQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwPatientsPaidOff AddNew()
		{
			VwPatientsPaidOff entity = base.AddNewEntity() as VwPatientsPaidOff;
			
			return entity;
		}


		#region IEnumerable<VwPatientsPaidOff> Members

		IEnumerator<VwPatientsPaidOff> IEnumerable<VwPatientsPaidOff>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwPatientsPaidOff;
			}
		}

		#endregion
		
		private VwPatientsPaidOffQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_PatientsPaidOff' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwPatientsPaidOff ()")]
	[Serializable]
	public partial class VwPatientsPaidOff : esVwPatientsPaidOff
	{
		public VwPatientsPaidOff()
		{

		}
	
		public VwPatientsPaidOff(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwPatientsPaidOffMetadata.Meta();
			}
		}
		
		
		
		override protected esVwPatientsPaidOffQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwPatientsPaidOffQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwPatientsPaidOffQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwPatientsPaidOffQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwPatientsPaidOffQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwPatientsPaidOffQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwPatientsPaidOffQuery : esVwPatientsPaidOffQuery
	{
		public VwPatientsPaidOffQuery()
		{

		}		
		
		public VwPatientsPaidOffQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwPatientsPaidOffQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwPatientsPaidOffMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwPatientsPaidOffMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwPatientsPaidOffMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwPatientsPaidOffMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwPatientsPaidOffMetadata.ColumnNames.IsPaidOff, 1, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwPatientsPaidOffMetadata.PropertyNames.IsPaidOff;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwPatientsPaidOffMetadata Meta()
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
			 public const string IsPaidOff = "IsPaidOff";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string IsPaidOff = "IsPaidOff";
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
			lock (typeof(VwPatientsPaidOffMetadata))
			{
				if(VwPatientsPaidOffMetadata.mapDelegates == null)
				{
					VwPatientsPaidOffMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwPatientsPaidOffMetadata.meta == null)
				{
					VwPatientsPaidOffMetadata.meta = new VwPatientsPaidOffMetadata();
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
				meta.AddTypeMap("IsPaidOff", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "vw_PatientsPaidOff";
				meta.Destination = "vw_PatientsPaidOff";
				
				meta.spInsert = "proc_vw_PatientsPaidOffInsert";				
				meta.spUpdate = "proc_vw_PatientsPaidOffUpdate";		
				meta.spDelete = "proc_vw_PatientsPaidOffDelete";
				meta.spLoadAll = "proc_vw_PatientsPaidOffLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_PatientsPaidOffLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwPatientsPaidOffMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
