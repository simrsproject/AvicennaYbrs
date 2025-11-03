/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/23/2015 2:25:09 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.RSCH
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
						case "OrderLabTglOrder": this.str.OrderLabTglOrder = (string)value; break;							
						case "OrderLabNoMR": this.str.OrderLabNoMR = (string)value; break;							
						case "OrderLabNama": this.str.OrderLabNama = (string)value; break;							
						case "CheckupResultGroupCode": this.str.CheckupResultGroupCode = (string)value; break;							
						case "CheckupResultGroupName": this.str.CheckupResultGroupName = (string)value; break;							
						case "CheckupResultTestCode": this.str.CheckupResultTestCode = (string)value; break;							
						case "CheckupResultTestName": this.str.CheckupResultTestName = (string)value; break;							
						case "CheckupResultFractionCode": this.str.CheckupResultFractionCode = (string)value; break;							
						case "CheckupResultFractionName": this.str.CheckupResultFractionName = (string)value; break;							
						case "WithinRange": this.str.WithinRange = (string)value; break;							
						case "OutRange": this.str.OutRange = (string)value; break;							
						case "Satuan": this.str.Satuan = (string)value; break;							
						case "StandarValue": this.str.StandarValue = (string)value; break;							
						case "OrderLabCritical": this.str.OrderLabCritical = (string)value; break;							
						case "Seq": this.str.Seq = (string)value; break;
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
						
						case "CheckupResultGroupCode":
						
							if (value == null || value is System.Int32)
								this.CheckupResultGroupCode = (System.Int32?)value;
							break;
						
						case "Seq":
						
							if (value == null || value is System.Int32)
								this.Seq = (System.Int32?)value;
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
		/// Maps to vw_HasilPasien.OrderLabNo
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
		/// Maps to vw_HasilPasien.OrderLabTglOrder
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
		/// Maps to vw_HasilPasien.OrderLabNoMR
		/// </summary>
		virtual public System.String OrderLabNoMR
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabNoMR);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabNoMR, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.OrderLabNama
		/// </summary>
		virtual public System.String OrderLabNama
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabNama);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabNama, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.CheckupResultGroupCode
		/// </summary>
		virtual public System.Int32? CheckupResultGroupCode
		{
			get
			{
				return base.GetSystemInt32(VwHasilPasienMetadata.ColumnNames.CheckupResultGroupCode);
			}
			
			set
			{
				base.SetSystemInt32(VwHasilPasienMetadata.ColumnNames.CheckupResultGroupCode, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.CheckupResultGroupName
		/// </summary>
		virtual public System.String CheckupResultGroupName
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultGroupName);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultGroupName, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.CheckupResultTestCode
		/// </summary>
		virtual public System.String CheckupResultTestCode
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultTestCode);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultTestCode, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.CheckupResultTestName
		/// </summary>
		virtual public System.String CheckupResultTestName
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultTestName);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultTestName, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.CheckupResultFractionCode
		/// </summary>
		virtual public System.String CheckupResultFractionCode
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultFractionCode);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultFractionCode, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.CheckupResultFractionName
		/// </summary>
		virtual public System.String CheckupResultFractionName
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultFractionName);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.CheckupResultFractionName, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.WithinRange
		/// </summary>
		virtual public System.String WithinRange
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.WithinRange);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.WithinRange, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.OutRange
		/// </summary>
		virtual public System.String OutRange
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.OutRange);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.OutRange, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.Satuan
		/// </summary>
		virtual public System.String Satuan
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.Satuan);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.Satuan, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.StandarValue
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
		/// Maps to vw_HasilPasien.OrderLabCritical
		/// </summary>
		virtual public System.String OrderLabCritical
		{
			get
			{
				return base.GetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabCritical);
			}
			
			set
			{
				base.SetSystemString(VwHasilPasienMetadata.ColumnNames.OrderLabCritical, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_HasilPasien.Seq
		/// </summary>
		virtual public System.Int32? Seq
		{
			get
			{
				return base.GetSystemInt32(VwHasilPasienMetadata.ColumnNames.Seq);
			}
			
			set
			{
				base.SetSystemInt32(VwHasilPasienMetadata.ColumnNames.Seq, value);
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
				
			public System.String OrderLabNoMR
			{
				get
				{
					System.String data = entity.OrderLabNoMR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabNoMR = null;
					else entity.OrderLabNoMR = Convert.ToString(value);
				}
			}
				
			public System.String OrderLabNama
			{
				get
				{
					System.String data = entity.OrderLabNama;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabNama = null;
					else entity.OrderLabNama = Convert.ToString(value);
				}
			}
				
			public System.String CheckupResultGroupCode
			{
				get
				{
					System.Int32? data = entity.CheckupResultGroupCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckupResultGroupCode = null;
					else entity.CheckupResultGroupCode = Convert.ToInt32(value);
				}
			}
				
			public System.String CheckupResultGroupName
			{
				get
				{
					System.String data = entity.CheckupResultGroupName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckupResultGroupName = null;
					else entity.CheckupResultGroupName = Convert.ToString(value);
				}
			}
				
			public System.String CheckupResultTestCode
			{
				get
				{
					System.String data = entity.CheckupResultTestCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckupResultTestCode = null;
					else entity.CheckupResultTestCode = Convert.ToString(value);
				}
			}
				
			public System.String CheckupResultTestName
			{
				get
				{
					System.String data = entity.CheckupResultTestName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckupResultTestName = null;
					else entity.CheckupResultTestName = Convert.ToString(value);
				}
			}
				
			public System.String CheckupResultFractionCode
			{
				get
				{
					System.String data = entity.CheckupResultFractionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckupResultFractionCode = null;
					else entity.CheckupResultFractionCode = Convert.ToString(value);
				}
			}
				
			public System.String CheckupResultFractionName
			{
				get
				{
					System.String data = entity.CheckupResultFractionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckupResultFractionName = null;
					else entity.CheckupResultFractionName = Convert.ToString(value);
				}
			}
				
			public System.String WithinRange
			{
				get
				{
					System.String data = entity.WithinRange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WithinRange = null;
					else entity.WithinRange = Convert.ToString(value);
				}
			}
				
			public System.String OutRange
			{
				get
				{
					System.String data = entity.OutRange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OutRange = null;
					else entity.OutRange = Convert.ToString(value);
				}
			}
				
			public System.String Satuan
			{
				get
				{
					System.String data = entity.Satuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Satuan = null;
					else entity.Satuan = Convert.ToString(value);
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
				
			public System.String OrderLabCritical
			{
				get
				{
					System.String data = entity.OrderLabCritical;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderLabCritical = null;
					else entity.OrderLabCritical = Convert.ToString(value);
				}
			}
				
			public System.String Seq
			{
				get
				{
					System.Int32? data = entity.Seq;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Seq = null;
					else entity.Seq = Convert.ToInt32(value);
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
		
		public esQueryItem OrderLabTglOrder
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OrderLabTglOrder, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem OrderLabNoMR
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OrderLabNoMR, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderLabNama
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OrderLabNama, esSystemType.String);
			}
		} 
		
		public esQueryItem CheckupResultGroupCode
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.CheckupResultGroupCode, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CheckupResultGroupName
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.CheckupResultGroupName, esSystemType.String);
			}
		} 
		
		public esQueryItem CheckupResultTestCode
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.CheckupResultTestCode, esSystemType.String);
			}
		} 
		
		public esQueryItem CheckupResultTestName
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.CheckupResultTestName, esSystemType.String);
			}
		} 
		
		public esQueryItem CheckupResultFractionCode
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.CheckupResultFractionCode, esSystemType.String);
			}
		} 
		
		public esQueryItem CheckupResultFractionName
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.CheckupResultFractionName, esSystemType.String);
			}
		} 
		
		public esQueryItem WithinRange
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.WithinRange, esSystemType.String);
			}
		} 
		
		public esQueryItem OutRange
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OutRange, esSystemType.String);
			}
		} 
		
		public esQueryItem Satuan
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.Satuan, esSystemType.String);
			}
		} 
		
		public esQueryItem StandarValue
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.StandarValue, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderLabCritical
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.OrderLabCritical, esSystemType.String);
			}
		} 
		
		public esQueryItem Seq
		{
			get
			{
				return new esQueryItem(this, VwHasilPasienMetadata.ColumnNames.Seq, esSystemType.Int32);
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
	/// Encapsulates the 'vw_HasilPasien' view
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
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OrderLabTglOrder, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OrderLabTglOrder;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OrderLabNoMR, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OrderLabNoMR;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OrderLabNama, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OrderLabNama;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.CheckupResultGroupCode, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.CheckupResultGroupCode;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.CheckupResultGroupName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.CheckupResultGroupName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.CheckupResultTestCode, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.CheckupResultTestCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.CheckupResultTestName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.CheckupResultTestName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.CheckupResultFractionCode, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.CheckupResultFractionCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.CheckupResultFractionName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.CheckupResultFractionName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.WithinRange, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.WithinRange;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OutRange, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OutRange;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.Satuan, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.Satuan;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.StandarValue, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.StandarValue;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.OrderLabCritical, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.OrderLabCritical;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwHasilPasienMetadata.ColumnNames.Seq, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwHasilPasienMetadata.PropertyNames.Seq;
			c.NumericPrecision = 10;
			c.IsNullable = true;
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
			 public const string OrderLabTglOrder = "OrderLabTglOrder";
			 public const string OrderLabNoMR = "OrderLabNoMR";
			 public const string OrderLabNama = "OrderLabNama";
			 public const string CheckupResultGroupCode = "CheckupResultGroupCode";
			 public const string CheckupResultGroupName = "CheckupResultGroupName";
			 public const string CheckupResultTestCode = "CheckupResultTestCode";
			 public const string CheckupResultTestName = "CheckupResultTestName";
			 public const string CheckupResultFractionCode = "CheckupResultFractionCode";
			 public const string CheckupResultFractionName = "CheckupResultFractionName";
			 public const string WithinRange = "WithinRange";
			 public const string OutRange = "OutRange";
			 public const string Satuan = "Satuan";
			 public const string StandarValue = "StandarValue";
			 public const string OrderLabCritical = "OrderLabCritical";
			 public const string Seq = "Seq";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderLabNo = "OrderLabNo";
			 public const string OrderLabTglOrder = "OrderLabTglOrder";
			 public const string OrderLabNoMR = "OrderLabNoMR";
			 public const string OrderLabNama = "OrderLabNama";
			 public const string CheckupResultGroupCode = "CheckupResultGroupCode";
			 public const string CheckupResultGroupName = "CheckupResultGroupName";
			 public const string CheckupResultTestCode = "CheckupResultTestCode";
			 public const string CheckupResultTestName = "CheckupResultTestName";
			 public const string CheckupResultFractionCode = "CheckupResultFractionCode";
			 public const string CheckupResultFractionName = "CheckupResultFractionName";
			 public const string WithinRange = "WithinRange";
			 public const string OutRange = "OutRange";
			 public const string Satuan = "Satuan";
			 public const string StandarValue = "StandarValue";
			 public const string OrderLabCritical = "OrderLabCritical";
			 public const string Seq = "Seq";
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
				meta.AddTypeMap("OrderLabTglOrder", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OrderLabNoMR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderLabNama", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckupResultGroupCode", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CheckupResultGroupName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckupResultTestCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckupResultTestName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckupResultFractionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckupResultFractionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WithinRange", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OutRange", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Satuan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StandarValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderLabCritical", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Seq", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "vw_HasilPasien";
				meta.Destination = "vw_HasilPasien";
				
				meta.spInsert = "proc_vw_HasilPasienInsert";				
				meta.spUpdate = "proc_vw_HasilPasienUpdate";		
				meta.spDelete = "proc_vw_HasilPasienDelete";
				meta.spLoadAll = "proc_vw_HasilPasienLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_HasilPasienLoadByPrimaryKey";
				
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
