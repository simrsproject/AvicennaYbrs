/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 12/8/2010 11:05:16 PM
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
    abstract public class esDefektaCollection : esEntityCollection
	{
		public esDefektaCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DefektaCollection";
		}

		#region Query Logic
		protected void InitQuery(esDefektaQuery query)
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
			this.InitQuery(query as esDefektaQuery);
		}
		#endregion
		
		virtual public Defekta DetachEntity(Defekta entity)
		{
			return base.DetachEntity(entity) as Defekta;
		}
		
		virtual public Defekta AttachEntity(Defekta entity)
		{
			return base.AttachEntity(entity) as Defekta;
		}
		
		virtual public void Combine(DefektaCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Defekta this[int index]
		{
			get
			{
				return base[index] as Defekta;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Defekta);
		}
	}



	[Serializable]
    abstract public class esDefekta : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDefektaQuery GetDynamicQuery()
		{
			return null;
		}

		public esDefekta()
		{

		}

		public esDefekta(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String sequenceNo)
		{
			esDefektaQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ItemIDExternal": this.str.ItemIDExternal = (string)value; break;							
						case "ItemName": this.str.ItemName = (string)value; break;							
						case "QtyOrder": this.str.QtyOrder = (string)value; break;							
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;							
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;							
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "Period": this.str.Period = (string)value; break;							
						case "QtyReceive": this.str.QtyReceive = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "Discount": this.str.Discount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsClosed": this.str.IsClosed = (string)value; break;
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
						
						case "QtyOrder":
						
							if (value == null || value is System.Decimal)
								this.QtyOrder = (System.Decimal?)value;
							break;
						
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						
						case "QtyReceive":
						
							if (value == null || value is System.Decimal)
								this.QtyReceive = (System.Decimal?)value;
							break;
						
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "Discount":
						
							if (value == null || value is System.Decimal)
								this.Discount = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsClosed":
						
							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
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
		/// Maps to Defekta.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(DefektaMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(DefektaMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.ItemIDExternal
		/// </summary>
		virtual public System.String ItemIDExternal
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.ItemIDExternal);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.ItemIDExternal, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.ItemName
		/// </summary>
		virtual public System.String ItemName
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.ItemName);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.ItemName, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.QtyOrder
		/// </summary>
		virtual public System.Decimal? QtyOrder
		{
			get
			{
				return base.GetSystemDecimal(DefektaMetadata.ColumnNames.QtyOrder);
			}
			
			set
			{
				base.SetSystemDecimal(DefektaMetadata.ColumnNames.QtyOrder, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DefektaMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DefektaMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.Period
		/// </summary>
		virtual public System.String Period
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.Period);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.Period, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.QtyReceive
		/// </summary>
		virtual public System.Decimal? QtyReceive
		{
			get
			{
				return base.GetSystemDecimal(DefektaMetadata.ColumnNames.QtyReceive);
			}
			
			set
			{
				base.SetSystemDecimal(DefektaMetadata.ColumnNames.QtyReceive, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(DefektaMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(DefektaMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(DefektaMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(DefektaMetadata.ColumnNames.Discount, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DefektaMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DefektaMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DefektaMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DefektaMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Defekta.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(DefektaMetadata.ColumnNames.IsClosed);
			}
			
			set
			{
				base.SetSystemBoolean(DefektaMetadata.ColumnNames.IsClosed, value);
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
			public esStrings(esDefekta entity)
			{
				this.entity = entity;
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
				
			public System.String ItemIDExternal
			{
				get
				{
					System.String data = entity.ItemIDExternal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemIDExternal = null;
					else entity.ItemIDExternal = Convert.ToString(value);
				}
			}
				
			public System.String ItemName
			{
				get
				{
					System.String data = entity.ItemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemName = null;
					else entity.ItemName = Convert.ToString(value);
				}
			}
				
			public System.String QtyOrder
			{
				get
				{
					System.Decimal? data = entity.QtyOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyOrder = null;
					else entity.QtyOrder = Convert.ToDecimal(value);
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
				
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
				
			public System.String Period
			{
				get
				{
					System.String data = entity.Period;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Period = null;
					else entity.Period = Convert.ToString(value);
				}
			}
				
			public System.String QtyReceive
			{
				get
				{
					System.Decimal? data = entity.QtyReceive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyReceive = null;
					else entity.QtyReceive = Convert.ToDecimal(value);
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
				
			public System.String Discount
			{
				get
				{
					System.Decimal? data = entity.Discount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount = null;
					else entity.Discount = Convert.ToDecimal(value);
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
				
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			

			private esDefekta entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDefektaQuery query)
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
				throw new Exception("esDefekta can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Defekta : esDefekta
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
	abstract public class esDefektaQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DefektaMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemIDExternal
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.ItemIDExternal, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemName
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.ItemName, esSystemType.String);
			}
		} 
		
		public esQueryItem QtyOrder
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.QtyOrder, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Period
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.Period, esSystemType.String);
			}
		} 
		
		public esQueryItem QtyReceive
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.QtyReceive, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, DefektaMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DefektaCollection")]
	public partial class DefektaCollection : esDefektaCollection, IEnumerable<Defekta>
	{
		public DefektaCollection()
		{

		}
		
		public static implicit operator List<Defekta>(DefektaCollection coll)
		{
			List<Defekta> list = new List<Defekta>();
			
			foreach (Defekta emp in coll)
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
				return  DefektaMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DefektaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Defekta(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Defekta();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DefektaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DefektaQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DefektaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Defekta AddNew()
		{
			Defekta entity = base.AddNewEntity() as Defekta;
			
			return entity;
		}

		public Defekta FindByPrimaryKey(System.String transactionNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo) as Defekta;
		}


		#region IEnumerable<Defekta> Members

		IEnumerator<Defekta> IEnumerable<Defekta>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Defekta;
			}
		}

		#endregion
		
		private DefektaQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Defekta' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Defekta ({TransactionNo},{SequenceNo})")]
	[Serializable]
	public partial class Defekta : esDefekta
	{
		public Defekta()
		{

		}
	
		public Defekta(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DefektaMetadata.Meta();
			}
		}
		
		
		
		override protected esDefektaQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DefektaQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DefektaQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DefektaQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DefektaQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DefektaQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DefektaQuery : esDefektaQuery
	{
		public DefektaQuery()
		{

		}		
		
		public DefektaQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DefektaQuery";
        }
		
			
	}


	[Serializable]
	public partial class DefektaMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DefektaMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DefektaMetadata.ColumnNames.TransactionDate, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DefektaMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.ItemIDExternal, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.ItemIDExternal;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.ItemName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.QtyOrder, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DefektaMetadata.PropertyNames.QtyOrder;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.SRItemUnit, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.CreateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DefektaMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.CreateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.ReferenceNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.Period, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.Period;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.QtyReceive, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DefektaMetadata.PropertyNames.QtyReceive;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.Price, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DefektaMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.Discount, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DefektaMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DefektaMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = DefektaMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DefektaMetadata.ColumnNames.IsClosed, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DefektaMetadata.PropertyNames.IsClosed;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DefektaMetadata Meta()
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
			 public const string TransactionDate = "TransactionDate";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ItemID = "ItemID";
			 public const string ItemIDExternal = "ItemIDExternal";
			 public const string ItemName = "ItemName";
			 public const string QtyOrder = "QtyOrder";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string Period = "Period";
			 public const string QtyReceive = "QtyReceive";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsClosed = "IsClosed";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionDate = "TransactionDate";
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ItemID = "ItemID";
			 public const string ItemIDExternal = "ItemIDExternal";
			 public const string ItemName = "ItemName";
			 public const string QtyOrder = "QtyOrder";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string CreateDateTime = "CreateDateTime";
			 public const string CreateByUserID = "CreateByUserID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string Period = "Period";
			 public const string QtyReceive = "QtyReceive";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsClosed = "IsClosed";
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
			lock (typeof(DefektaMetadata))
			{
				if(DefektaMetadata.mapDelegates == null)
				{
					DefektaMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DefektaMetadata.meta == null)
				{
					DefektaMetadata.meta = new DefektaMetadata();
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
				

				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemIDExternal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyOrder", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Period", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyReceive", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "Defekta";
				meta.Destination = "Defekta";
				
				meta.spInsert = "proc_DefektaInsert";				
				meta.spUpdate = "proc_DefektaUpdate";		
				meta.spDelete = "proc_DefektaDelete";
				meta.spLoadAll = "proc_DefektaLoadAll";
				meta.spLoadByPrimaryKey = "proc_DefektaLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DefektaMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
