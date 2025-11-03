/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/21/2014 11:40:59 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Inacbg
{

	[Serializable]
	abstract public class esTariff2012RsuRnRiCollection : esEntityCollectionWAuditLog
	{
		public esTariff2012RsuRnRiCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "Tariff2012RsuRnRiCollection";
		}

		#region Query Logic
		protected void InitQuery(esTariff2012RsuRnRiQuery query)
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
			this.InitQuery(query as esTariff2012RsuRnRiQuery);
		}
		#endregion
		
		virtual public Tariff2012RsuRnRi DetachEntity(Tariff2012RsuRnRi entity)
		{
			return base.DetachEntity(entity) as Tariff2012RsuRnRi;
		}
		
		virtual public Tariff2012RsuRnRi AttachEntity(Tariff2012RsuRnRi entity)
		{
			return base.AttachEntity(entity) as Tariff2012RsuRnRi;
		}
		
		virtual public void Combine(Tariff2012RsuRnRiCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Tariff2012RsuRnRi this[int index]
		{
			get
			{
				return base[index] as Tariff2012RsuRnRi;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Tariff2012RsuRnRi);
		}
	}



	[Serializable]
	abstract public class esTariff2012RsuRnRi : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTariff2012RsuRnRiQuery GetDynamicQuery()
		{
			return null;
		}

		public esTariff2012RsuRnRi()
		{

		}

		public esTariff2012RsuRnRi(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 no)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(no);
			else
				return LoadByPrimaryKeyStoredProcedure(no);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 no)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(no);
			else
				return LoadByPrimaryKeyStoredProcedure(no);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 no)
		{
			esTariff2012RsuRnRiQuery query = this.GetDynamicQuery();
			query.Where(query.No == no);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 no)
		{
			esParameters parms = new esParameters();
			parms.Add("no",no);
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
						case "No": this.str.No = (string)value; break;							
						case "IrCode": this.str.IrCode = (string)value; break;							
						case "Code": this.str.Code = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "Alos": this.str.Alos = (string)value; break;							
						case "FinalCostWeight": this.str.FinalCostWeight = (string)value; break;							
						case "BaseRate": this.str.BaseRate = (string)value; break;							
						case "Kelas3": this.str.Kelas3 = (string)value; break;							
						case "Kelas2": this.str.Kelas2 = (string)value; break;							
						case "Kelas1": this.str.Kelas1 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "No":
						
							if (value == null || value is System.Int64)
								this.No = (System.Int64?)value;
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
		/// Maps to tariff_2012_rsu_rn_ri.no
		/// </summary>
		virtual public System.Int64? No
		{
			get
			{
				return base.GetSystemInt64(Tariff2012RsuRnRiMetadata.ColumnNames.No);
			}
			
			set
			{
				base.SetSystemInt64(Tariff2012RsuRnRiMetadata.ColumnNames.No, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.ir_code
		/// </summary>
		virtual public System.String IrCode
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.IrCode);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.IrCode, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.code
		/// </summary>
		virtual public System.String Code
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Code);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Code, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.alos
		/// </summary>
		virtual public System.String Alos
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Alos);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Alos, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.final_cost_weight
		/// </summary>
		virtual public System.String FinalCostWeight
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.FinalCostWeight);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.FinalCostWeight, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.base_rate
		/// </summary>
		virtual public System.String BaseRate
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.BaseRate);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.BaseRate, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.kelas_3
		/// </summary>
		virtual public System.String Kelas3
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas3);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas3, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.kelas_2
		/// </summary>
		virtual public System.String Kelas2
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas2);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas2, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_ri.kelas_1
		/// </summary>
		virtual public System.String Kelas1
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas1);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas1, value);
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
			public esStrings(esTariff2012RsuRnRi entity)
			{
				this.entity = entity;
			}
			
	
			public System.String No
			{
				get
				{
					System.Int64? data = entity.No;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.No = null;
					else entity.No = Convert.ToInt64(value);
				}
			}
				
			public System.String IrCode
			{
				get
				{
					System.String data = entity.IrCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IrCode = null;
					else entity.IrCode = Convert.ToString(value);
				}
			}
				
			public System.String Code
			{
				get
				{
					System.String data = entity.Code;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Code = null;
					else entity.Code = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String Alos
			{
				get
				{
					System.String data = entity.Alos;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Alos = null;
					else entity.Alos = Convert.ToString(value);
				}
			}
				
			public System.String FinalCostWeight
			{
				get
				{
					System.String data = entity.FinalCostWeight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinalCostWeight = null;
					else entity.FinalCostWeight = Convert.ToString(value);
				}
			}
				
			public System.String BaseRate
			{
				get
				{
					System.String data = entity.BaseRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BaseRate = null;
					else entity.BaseRate = Convert.ToString(value);
				}
			}
				
			public System.String Kelas3
			{
				get
				{
					System.String data = entity.Kelas3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kelas3 = null;
					else entity.Kelas3 = Convert.ToString(value);
				}
			}
				
			public System.String Kelas2
			{
				get
				{
					System.String data = entity.Kelas2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kelas2 = null;
					else entity.Kelas2 = Convert.ToString(value);
				}
			}
				
			public System.String Kelas1
			{
				get
				{
					System.String data = entity.Kelas1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kelas1 = null;
					else entity.Kelas1 = Convert.ToString(value);
				}
			}
			

			private esTariff2012RsuRnRi entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTariff2012RsuRnRiQuery query)
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
				throw new Exception("esTariff2012RsuRnRi can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esTariff2012RsuRnRiQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return Tariff2012RsuRnRiMetadata.Meta();
			}
		}	
		

		public esQueryItem No
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.No, esSystemType.Int64);
			}
		} 
		
		public esQueryItem IrCode
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.IrCode, esSystemType.String);
			}
		} 
		
		public esQueryItem Code
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.Code, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem Alos
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.Alos, esSystemType.String);
			}
		} 
		
		public esQueryItem FinalCostWeight
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.FinalCostWeight, esSystemType.String);
			}
		} 
		
		public esQueryItem BaseRate
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.BaseRate, esSystemType.String);
			}
		} 
		
		public esQueryItem Kelas3
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.Kelas3, esSystemType.String);
			}
		} 
		
		public esQueryItem Kelas2
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.Kelas2, esSystemType.String);
			}
		} 
		
		public esQueryItem Kelas1
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRiMetadata.ColumnNames.Kelas1, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("Tariff2012RsuRnRiCollection")]
	public partial class Tariff2012RsuRnRiCollection : esTariff2012RsuRnRiCollection, IEnumerable<Tariff2012RsuRnRi>
	{
		public Tariff2012RsuRnRiCollection()
		{

		}
		
		public static implicit operator List<Tariff2012RsuRnRi>(Tariff2012RsuRnRiCollection coll)
		{
			List<Tariff2012RsuRnRi> list = new List<Tariff2012RsuRnRi>();
			
			foreach (Tariff2012RsuRnRi emp in coll)
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
				return  Tariff2012RsuRnRiMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new Tariff2012RsuRnRiQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Tariff2012RsuRnRi(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Tariff2012RsuRnRi();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public Tariff2012RsuRnRiQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new Tariff2012RsuRnRiQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(Tariff2012RsuRnRiQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Tariff2012RsuRnRi AddNew()
		{
			Tariff2012RsuRnRi entity = base.AddNewEntity() as Tariff2012RsuRnRi;
			
			return entity;
		}

		public Tariff2012RsuRnRi FindByPrimaryKey(System.Int64 no)
		{
			return base.FindByPrimaryKey(no) as Tariff2012RsuRnRi;
		}


		#region IEnumerable<Tariff2012RsuRnRi> Members

		IEnumerator<Tariff2012RsuRnRi> IEnumerable<Tariff2012RsuRnRi>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Tariff2012RsuRnRi;
			}
		}

		#endregion
		
		private Tariff2012RsuRnRiQuery query;
	}


	/// <summary>
	/// Encapsulates the 'tariff_2012_rsu_rn_ri' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Tariff2012RsuRnRi ({No})")]
	[Serializable]
	public partial class Tariff2012RsuRnRi : esTariff2012RsuRnRi
	{
		public Tariff2012RsuRnRi()
		{

		}
	
		public Tariff2012RsuRnRi(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return Tariff2012RsuRnRiMetadata.Meta();
			}
		}
		
		
		
		override protected esTariff2012RsuRnRiQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new Tariff2012RsuRnRiQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public Tariff2012RsuRnRiQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new Tariff2012RsuRnRiQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(Tariff2012RsuRnRiQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private Tariff2012RsuRnRiQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class Tariff2012RsuRnRiQuery : esTariff2012RsuRnRiQuery
	{
		public Tariff2012RsuRnRiQuery()
		{

		}		
		
		public Tariff2012RsuRnRiQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "Tariff2012RsuRnRiQuery";
        }
		
			
	}


	[Serializable]
	public partial class Tariff2012RsuRnRiMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected Tariff2012RsuRnRiMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.No, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.No;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.IrCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.IrCode;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.Code, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.Code;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.Alos, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.Alos;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.FinalCostWeight, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.FinalCostWeight;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.BaseRate, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.BaseRate;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas3, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.Kelas3;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas2, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.Kelas2;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRiMetadata.ColumnNames.Kelas1, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRiMetadata.PropertyNames.Kelas1;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
		}
		#endregion	
	
		static public Tariff2012RsuRnRiMetadata Meta()
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
			 public const string No = "no";
			 public const string IrCode = "ir_code";
			 public const string Code = "code";
			 public const string Description = "description";
			 public const string Alos = "alos";
			 public const string FinalCostWeight = "final_cost_weight";
			 public const string BaseRate = "base_rate";
			 public const string Kelas3 = "kelas_3";
			 public const string Kelas2 = "kelas_2";
			 public const string Kelas1 = "kelas_1";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string No = "No";
			 public const string IrCode = "IrCode";
			 public const string Code = "Code";
			 public const string Description = "Description";
			 public const string Alos = "Alos";
			 public const string FinalCostWeight = "FinalCostWeight";
			 public const string BaseRate = "BaseRate";
			 public const string Kelas3 = "Kelas3";
			 public const string Kelas2 = "Kelas2";
			 public const string Kelas1 = "Kelas1";
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
			lock (typeof(Tariff2012RsuRnRiMetadata))
			{
				if(Tariff2012RsuRnRiMetadata.mapDelegates == null)
				{
					Tariff2012RsuRnRiMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (Tariff2012RsuRnRiMetadata.meta == null)
				{
					Tariff2012RsuRnRiMetadata.meta = new Tariff2012RsuRnRiMetadata();
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
				

				meta.AddTypeMap("No", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("IrCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Code", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Alos", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("FinalCostWeight", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("BaseRate", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Kelas3", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Kelas2", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Kelas1", new esTypeMap("char", "System.String"));			
				
				
				
				meta.Source = "tariff_2012_rsu_rn_ri";
				meta.Destination = "tariff_2012_rsu_rn_ri";
				
				meta.spInsert = "proc_tariff_2012_rsu_rn_riInsert";				
				meta.spUpdate = "proc_tariff_2012_rsu_rn_riUpdate";		
				meta.spDelete = "proc_tariff_2012_rsu_rn_riDelete";
				meta.spLoadAll = "proc_tariff_2012_rsu_rn_riLoadAll";
				meta.spLoadByPrimaryKey = "proc_tariff_2012_rsu_rn_riLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private Tariff2012RsuRnRiMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
