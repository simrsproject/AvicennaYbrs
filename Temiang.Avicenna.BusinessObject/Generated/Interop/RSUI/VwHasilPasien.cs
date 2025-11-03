/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/25/2016 1:57:33 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.RSUI
{

	[Serializable]
	abstract public class esVwHasilPasienCollection : esEntityCollectionWAuditLog
	{
		public esVwHasilPasienCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwHasilPasienCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwHasilPasienQuery query)
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
			this.InitQuery(query as esVwHasilPasienQuery);
		}
		#endregion
		
		virtual public VwHasilPasien DetachEntity(VwHasilPasien entity)
		{
			return base.DetachEntity(entity) as VwHasilPasien;
		}
		
		virtual public VwHasilPasien AttachEntity(VwHasilPasien entity)
		{
			return base.AttachEntity(entity) as VwHasilPasien;
		}
		
		virtual public void Combine(VwHasilPasienCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwHasilPasien this[int index]
		{
			get
			{
				return base[index] as VwHasilPasien;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwHasilPasien);
		}
	}



	[Serializable]
	abstract public class esVwHasilPasien : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwHasilPasienQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwHasilPasien()
		{

		}

		public esVwHasilPasien(DataRow row)
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
						case "OrderLabNo": this.str.OrderLabNo = (string)value; break;							
						case "LabOrderCode": this.str.LabOrderCode = (string)value; break;							
						case "LabOrderSummary": this.str.LabOrderSummary = (string)value; break;							
						case "Result": this.str.Result = (string)value; break;							
						case "StandarValue": this.str.StandarValue = (string)value; break;							
						case "OrderLabTglOrder": this.str.OrderLabTglOrder = (string)value; break;							
						case "TestGroup": this.str.TestGroup = (string)value; break;							
						case "OrderTestid": this.str.OrderTestid = (string)value; break;							
						case "DispSeq": this.str.DispSeq = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderLabTglOrder":
						
							if (value == null || value is System.DateTime)
								this.OrderLabTglOrder = (System.DateTime?)value;
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
		/// Maps to Vw_HasilPasien.OrderLabNo
		/// </summary>
		virtual public System.String OrderLabNo
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabNo);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.LabOrderCode
		/// </summary>
		virtual public System.String LabOrderCode
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.LabOrderCode);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.LabOrderCode, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.LabOrderSummary
		/// </summary>
		virtual public System.String LabOrderSummary
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.LabOrderSummary);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.LabOrderSummary, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.Result
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.Result);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.Result, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.StandarValue
		/// </summary>
		virtual public System.String StandarValue
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.StandarValue);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.StandarValue, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.OrderLabTglOrder
		/// </summary>
		virtual public System.DateTime? OrderLabTglOrder
		{
			get
			{
				return base.GetSystemDateTime(VwHasilPasienMetadata.ColumnNames.OrderLabTglOrder);
			}
			
			set
			{
				base.SetSystemDateTime(VwHasilPasienMetadata.ColumnNames.OrderLabTglOrder, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.TEST_GROUP
		/// </summary>
		virtual public System.String TestGroup
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.TestGroup);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.TestGroup, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.ORDER_TESTID
		/// </summary>
		virtual public System.String OrderTestid
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.OrderTestid);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.OrderTestid, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.DISP_SEQ
		/// </summary>
		virtual public System.String DispSeq
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.DispSeq);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.DispSeq, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_HasilPasien.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.PatientID, value);
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
			public esStrings(esVwHasilPasien entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderLabNo
			{
				get
				{
					System.String data = entity.OrderLabNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabNo = null;
					else entity.OrderLabNo = Convert.ToString(value);
				}
			}
				
			public System.String LabOrderCode
			{
				get
				{
					System.String data = entity.LabOrderCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LabOrderCode = null;
					else entity.LabOrderCode = Convert.ToString(value);
				}
			}
				
			public System.String LabOrderSummary
			{
				get
				{
					System.String data = entity.LabOrderSummary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LabOrderSummary = null;
					else entity.LabOrderSummary = Convert.ToString(value);
				}
			}
				
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
				
			public System.String StandarValue
			{
				get
				{
					System.String data = entity.StandarValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StandarValue = null;
					else entity.StandarValue = Convert.ToString(value);
				}
			}
				
			public System.String OrderLabTglOrder
			{
				get
				{
					System.DateTime? data = entity.OrderLabTglOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabTglOrder = null;
					else entity.OrderLabTglOrder = Convert.ToDateTime(value);
				}
			}
				
			public System.String TestGroup
			{
				get
				{
					System.String data = entity.TestGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestGroup = null;
					else entity.TestGroup = Convert.ToString(value);
				}
			}
				
			public System.String OrderTestid
			{
				get
				{
					System.String data = entity.OrderTestid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderTestid = null;
					else entity.OrderTestid = Convert.ToString(value);
				}
			}
				
			public System.String DispSeq
			{
				get
				{
					System.String data = entity.DispSeq;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DispSeq = null;
					else entity.DispSeq = Convert.ToString(value);
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
			

			private esVwHasilPasien entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwHasilPasienQuery query)
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
				throw new Exception("esVwHasilPasien can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwHasilPasienQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwHasilPasienMetadata.Meta();
			}
		}	
		

		public esQueryItem OrderLabNo
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OrderLabNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LabOrderCode
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.LabOrderCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LabOrderSummary
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.LabOrderSummary, esSystemType.String);
			}
		} 
		
		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.Result, esSystemType.String);
			}
		} 
		
		public esQueryItem StandarValue
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.StandarValue, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderLabTglOrder
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OrderLabTglOrder, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TestGroup
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.TestGroup, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderTestid
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OrderTestid, esSystemType.String);
			}
		} 
		
		public esQueryItem DispSeq
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.DispSeq, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwHasilPasienCollection")]
	public partial class VwHasilPasienCollection : esVwHasilPasienCollection, IEnumerable<VwHasilPasien>
	{
		public VwHasilPasienCollection()
		{

		}
		
		public static implicit operator List<VwHasilPasien>(VwHasilPasienCollection coll)
		{
			List<VwHasilPasien> list = new List<VwHasilPasien>();
			
			foreach (VwHasilPasien emp in coll)
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
				return  VwHasilPasienMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwHasilPasienQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwHasilPasien(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwHasilPasien();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwHasilPasienQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwHasilPasienQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwHasilPasienQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwHasilPasien AddNew()
		{
			VwHasilPasien entity = base.AddNewEntity() as VwHasilPasien;
			
			return entity;
		}


		#region IEnumerable<VwHasilPasien> Members

		IEnumerator<VwHasilPasien> IEnumerable<VwHasilPasien>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwHasilPasien;
			}
		}

		#endregion
		
		private VwHasilPasienQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Vw_HasilPasien' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwHasilPasien ()")]
	[Serializable]
	public partial class VwHasilPasien : esVwHasilPasien
	{
		public VwHasilPasien()
		{

		}
	
		public VwHasilPasien(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwHasilPasienMetadata.Meta();
			}
		}
		
		
		
		override protected esVwHasilPasienQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwHasilPasienQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwHasilPasienQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwHasilPasienQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwHasilPasienQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwHasilPasienQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwHasilPasienQuery : esVwHasilPasienQuery
	{
		public VwHasilPasienQuery()
		{

		}		
		
		public VwHasilPasienQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwHasilPasienQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwHasilPasienMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwHasilPasienMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OrderLabNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OrderLabNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.LabOrderCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.LabOrderCode;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.LabOrderSummary, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.LabOrderSummary;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.Result, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.StandarValue, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.StandarValue;
			c.CharacterMaxLength = 46;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OrderLabTglOrder, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OrderLabTglOrder;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.TestGroup, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.TestGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OrderTestid, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OrderTestid;
			c.CharacterMaxLength = 6;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.DispSeq, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.DispSeq;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.PatientID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwHasilPasienMetadata Meta()
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
			 public const string OrderLabNo = "OrderLabNo";
			 public const string LabOrderCode = "LabOrderCode";
			 public const string LabOrderSummary = "LabOrderSummary";
			 public const string Result = "Result";
			 public const string StandarValue = "StandarValue";
			 public const string OrderLabTglOrder = "OrderLabTglOrder";
			 public const string TestGroup = "TEST_GROUP";
			 public const string OrderTestid = "ORDER_TESTID";
			 public const string DispSeq = "DISP_SEQ";
			 public const string PatientID = "PatientID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderLabNo = "OrderLabNo";
			 public const string LabOrderCode = "LabOrderCode";
			 public const string LabOrderSummary = "LabOrderSummary";
			 public const string Result = "Result";
			 public const string StandarValue = "StandarValue";
			 public const string OrderLabTglOrder = "OrderLabTglOrder";
			 public const string TestGroup = "TestGroup";
			 public const string OrderTestid = "OrderTestid";
			 public const string DispSeq = "DispSeq";
			 public const string PatientID = "PatientID";
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
			lock (typeof(VwHasilPasienMetadata))
			{
				if(VwHasilPasienMetadata.mapDelegates == null)
				{
					VwHasilPasienMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwHasilPasienMetadata.meta == null)
				{
					VwHasilPasienMetadata.meta = new VwHasilPasienMetadata();
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
				

				meta.AddTypeMap("OrderLabNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LabOrderCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LabOrderSummary", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Result", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StandarValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderLabTglOrder", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TestGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderTestid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DispSeq", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Vw_HasilPasien";
				meta.Destination = "Vw_HasilPasien";
				
				meta.spInsert = "proc_Vw_HasilPasienInsert";				
				meta.spUpdate = "proc_Vw_HasilPasienUpdate";		
				meta.spDelete = "proc_Vw_HasilPasienDelete";
				meta.spLoadAll = "proc_Vw_HasilPasienLoadAll";
				meta.spLoadByPrimaryKey = "proc_Vw_HasilPasienLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwHasilPasienMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
