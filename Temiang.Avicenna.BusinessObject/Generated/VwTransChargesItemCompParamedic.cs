/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/24/2012 5:04:50 PM
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
	abstract public class esVwTransChargesItemCompParamedicCollection : esEntityCollectionWAuditLog
	{
		public esVwTransChargesItemCompParamedicCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwTransChargesItemCompParamedicCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwTransChargesItemCompParamedicQuery query)
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
			this.InitQuery(query as esVwTransChargesItemCompParamedicQuery);
		}
		#endregion
		
		virtual public VwTransChargesItemCompParamedic DetachEntity(VwTransChargesItemCompParamedic entity)
		{
			return base.DetachEntity(entity) as VwTransChargesItemCompParamedic;
		}
		
		virtual public VwTransChargesItemCompParamedic AttachEntity(VwTransChargesItemCompParamedic entity)
		{
			return base.AttachEntity(entity) as VwTransChargesItemCompParamedic;
		}
		
		virtual public void Combine(VwTransChargesItemCompParamedicCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwTransChargesItemCompParamedic this[int index]
		{
			get
			{
				return base[index] as VwTransChargesItemCompParamedic;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwTransChargesItemCompParamedic);
		}
	}



	[Serializable]
	abstract public class esVwTransChargesItemCompParamedic : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwTransChargesItemCompParamedicQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwTransChargesItemCompParamedic()
		{

		}

		public esVwTransChargesItemCompParamedic(DataRow row)
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsPackage": this.str.IsPackage = (string)value; break;							
						case "AutoProcessCalculation": this.str.AutoProcessCalculation = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsPackage":
						
							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
							break;
						
						case "AutoProcessCalculation":
						
							if (value == null || value is System.Decimal)
								this.AutoProcessCalculation = (System.Decimal?)value;
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
		/// Maps to vw_TransChargesItemCompParamedic.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemCompParamedicMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemCompParamedicMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemCompParamedicMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemCompParamedicMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(VwTransChargesItemCompParamedicMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(VwTransChargesItemCompParamedicMetadata.ColumnNames.IsPackage, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_TransChargesItemCompParamedic.AutoProcessCalculation
		/// </summary>
		virtual public System.Decimal? AutoProcessCalculation
		{
			get
			{
				return base.GetSystemDecimal(VwTransChargesItemCompParamedicMetadata.ColumnNames.AutoProcessCalculation);
			}
			
			set
			{
				base.SetSystemDecimal(VwTransChargesItemCompParamedicMetadata.ColumnNames.AutoProcessCalculation, value);
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
			public esStrings(esVwTransChargesItemCompParamedic entity)
			{
				this.entity = entity;
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
				
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
				
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
				
			public System.String DiscountAmount
			{
				get
				{
					System.Decimal? data = entity.DiscountAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountAmount = null;
					else entity.DiscountAmount = Convert.ToDecimal(value);
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
				
			public System.String IsPackage
			{
				get
				{
					System.Boolean? data = entity.IsPackage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackage = null;
					else entity.IsPackage = Convert.ToBoolean(value);
				}
			}
				
			public System.String AutoProcessCalculation
			{
				get
				{
					System.Decimal? data = entity.AutoProcessCalculation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AutoProcessCalculation = null;
					else entity.AutoProcessCalculation = Convert.ToDecimal(value);
				}
			}
			

			private esVwTransChargesItemCompParamedic entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwTransChargesItemCompParamedicQuery query)
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
				throw new Exception("esVwTransChargesItemCompParamedic can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwTransChargesItemCompParamedicQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesItemCompParamedicMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem AutoProcessCalculation
		{
			get
			{
				return new esQueryItem(this, VwTransChargesItemCompParamedicMetadata.ColumnNames.AutoProcessCalculation, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwTransChargesItemCompParamedicCollection")]
	public partial class VwTransChargesItemCompParamedicCollection : esVwTransChargesItemCompParamedicCollection, IEnumerable<VwTransChargesItemCompParamedic>
	{
		public VwTransChargesItemCompParamedicCollection()
		{

		}
		
		public static implicit operator List<VwTransChargesItemCompParamedic>(VwTransChargesItemCompParamedicCollection coll)
		{
			List<VwTransChargesItemCompParamedic> list = new List<VwTransChargesItemCompParamedic>();
			
			foreach (VwTransChargesItemCompParamedic emp in coll)
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
				return  VwTransChargesItemCompParamedicMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesItemCompParamedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwTransChargesItemCompParamedic(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwTransChargesItemCompParamedic();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwTransChargesItemCompParamedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesItemCompParamedicQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwTransChargesItemCompParamedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwTransChargesItemCompParamedic AddNew()
		{
			VwTransChargesItemCompParamedic entity = base.AddNewEntity() as VwTransChargesItemCompParamedic;
			
			return entity;
		}


		#region IEnumerable<VwTransChargesItemCompParamedic> Members

		IEnumerator<VwTransChargesItemCompParamedic> IEnumerable<VwTransChargesItemCompParamedic>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwTransChargesItemCompParamedic;
			}
		}

		#endregion
		
		private VwTransChargesItemCompParamedicQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_TransChargesItemCompParamedic' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwTransChargesItemCompParamedic ()")]
	[Serializable]
	public partial class VwTransChargesItemCompParamedic : esVwTransChargesItemCompParamedic
	{
		public VwTransChargesItemCompParamedic()
		{

		}
	
		public VwTransChargesItemCompParamedic(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwTransChargesItemCompParamedicMetadata.Meta();
			}
		}
		
		
		
		override protected esVwTransChargesItemCompParamedicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwTransChargesItemCompParamedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwTransChargesItemCompParamedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwTransChargesItemCompParamedicQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwTransChargesItemCompParamedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwTransChargesItemCompParamedicQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwTransChargesItemCompParamedicQuery : esVwTransChargesItemCompParamedicQuery
	{
		public VwTransChargesItemCompParamedicQuery()
		{

		}		
		
		public VwTransChargesItemCompParamedicQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwTransChargesItemCompParamedicQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwTransChargesItemCompParamedicMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwTransChargesItemCompParamedicMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.Price, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.DiscountAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.ParamedicID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.IsPackage, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.IsPackage;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwTransChargesItemCompParamedicMetadata.ColumnNames.AutoProcessCalculation, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwTransChargesItemCompParamedicMetadata.PropertyNames.AutoProcessCalculation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwTransChargesItemCompParamedicMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string ParamedicID = "ParamedicID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPackage = "IsPackage";
			 public const string AutoProcessCalculation = "AutoProcessCalculation";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string ParamedicID = "ParamedicID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsPackage = "IsPackage";
			 public const string AutoProcessCalculation = "AutoProcessCalculation";
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
			lock (typeof(VwTransChargesItemCompParamedicMetadata))
			{
				if(VwTransChargesItemCompParamedicMetadata.mapDelegates == null)
				{
					VwTransChargesItemCompParamedicMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwTransChargesItemCompParamedicMetadata.meta == null)
				{
					VwTransChargesItemCompParamedicMetadata.meta = new VwTransChargesItemCompParamedicMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AutoProcessCalculation", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "vw_TransChargesItemCompParamedic";
				meta.Destination = "vw_TransChargesItemCompParamedic";
				
				meta.spInsert = "proc_vw_TransChargesItemCompParamedicInsert";				
				meta.spUpdate = "proc_vw_TransChargesItemCompParamedicUpdate";		
				meta.spDelete = "proc_vw_TransChargesItemCompParamedicDelete";
				meta.spLoadAll = "proc_vw_TransChargesItemCompParamedicLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_TransChargesItemCompParamedicLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwTransChargesItemCompParamedicMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
