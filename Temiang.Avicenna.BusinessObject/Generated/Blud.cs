/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/23/2020 7:41:57 AM
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
	abstract public class esBludCollection : esEntityCollectionWAuditLog
	{
		public esBludCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BludCollection";
		}

		#region Query Logic
		protected void InitQuery(esBludQuery query)
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
			this.InitQuery(query as esBludQuery);
		}
		#endregion
		
		virtual public Blud DetachEntity(Blud entity)
		{
			return base.DetachEntity(entity) as Blud;
		}
		
		virtual public Blud AttachEntity(Blud entity)
		{
			return base.AttachEntity(entity) as Blud;
		}
		
		virtual public void Combine(BludCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Blud this[int index]
		{
			get
			{
				return base[index] as Blud;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Blud);
		}
	}



	[Serializable]
	abstract public class esBlud : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBludQuery GetDynamicQuery()
		{
			return null;
		}

		public esBlud()
		{

		}

		public esBlud(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 bludID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bludID);
			else
				return LoadByPrimaryKeyStoredProcedure(bludID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 bludID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bludID);
			else
				return LoadByPrimaryKeyStoredProcedure(bludID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 bludID)
		{
			esBludQuery query = this.GetDynamicQuery();
			query.Where(query.BludID == bludID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 bludID)
		{
			esParameters parms = new esParameters();
			parms.Add("BludID",bludID);
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
						case "BludID": this.str.BludID = (string)value; break;							
						case "IsBlud": this.str.IsBlud = (string)value; break;							
						case "IsPendapatan": this.str.IsPendapatan = (string)value; break;							
						case "RekeningBlud": this.str.RekeningBlud = (string)value; break;							
						case "PelangganBlud": this.str.PelangganBlud = (string)value; break;							
						case "UnitBlud": this.str.UnitBlud = (string)value; break;							
						case "Tanggal": this.str.Tanggal = (string)value; break;							
						case "Nomor": this.str.Nomor = (string)value; break;							
						case "Uraian": this.str.Uraian = (string)value; break;							
						case "User": this.str.User = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;							
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "VoidDate": this.str.VoidDate = (string)value; break;							
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BludID":
						
							if (value == null || value is System.Int32)
								this.BludID = (System.Int32?)value;
							break;
						
						case "IsBlud":
						
							if (value == null || value is System.Boolean)
								this.IsBlud = (System.Boolean?)value;
							break;
						
						case "IsPendapatan":
						
							if (value == null || value is System.Boolean)
								this.IsPendapatan = (System.Boolean?)value;
							break;
						
						case "Tanggal":
						
							if (value == null || value is System.DateTime)
								this.Tanggal = (System.DateTime?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "ApprovedDate":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDate = (System.DateTime?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						
						case "VoidDate":
						
							if (value == null || value is System.DateTime)
								this.VoidDate = (System.DateTime?)value;
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
		/// Maps to Blud.BludID
		/// </summary>
		virtual public System.Int32? BludID
		{
			get
			{
				return base.GetSystemInt32(BludMetadata.ColumnNames.BludID);
			}
			
			set
			{
				base.SetSystemInt32(BludMetadata.ColumnNames.BludID, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.IsBlud
		/// </summary>
		virtual public System.Boolean? IsBlud
		{
			get
			{
				return base.GetSystemBoolean(BludMetadata.ColumnNames.IsBlud);
			}
			
			set
			{
				base.SetSystemBoolean(BludMetadata.ColumnNames.IsBlud, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.IsPendapatan
		/// </summary>
		virtual public System.Boolean? IsPendapatan
		{
			get
			{
				return base.GetSystemBoolean(BludMetadata.ColumnNames.IsPendapatan);
			}
			
			set
			{
				base.SetSystemBoolean(BludMetadata.ColumnNames.IsPendapatan, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.RekeningBlud
		/// </summary>
		virtual public System.String RekeningBlud
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.RekeningBlud);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.RekeningBlud, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.PelangganBlud
		/// </summary>
		virtual public System.String PelangganBlud
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.PelangganBlud);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.PelangganBlud, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.UnitBlud
		/// </summary>
		virtual public System.String UnitBlud
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.UnitBlud);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.UnitBlud, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.Tanggal
		/// </summary>
		virtual public System.DateTime? Tanggal
		{
			get
			{
				return base.GetSystemDateTime(BludMetadata.ColumnNames.Tanggal);
			}
			
			set
			{
				base.SetSystemDateTime(BludMetadata.ColumnNames.Tanggal, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.Nomor
		/// </summary>
		virtual public System.String Nomor
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.Nomor);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.Nomor, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.Uraian
		/// </summary>
		virtual public System.String Uraian
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.Uraian);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.Uraian, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.User
		/// </summary>
		virtual public System.String User
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.User);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.User, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BludMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BludMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(BludMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(BludMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(BludMetadata.ColumnNames.ApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(BludMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(BludMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(BludMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.VoidDate
		/// </summary>
		virtual public System.DateTime? VoidDate
		{
			get
			{
				return base.GetSystemDateTime(BludMetadata.ColumnNames.VoidDate);
			}
			
			set
			{
				base.SetSystemDateTime(BludMetadata.ColumnNames.VoidDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Blud.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(BludMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(BludMetadata.ColumnNames.VoidByUserID, value);
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
			public esStrings(esBlud entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BludID
			{
				get
				{
					System.Int32? data = entity.BludID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BludID = null;
					else entity.BludID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsBlud
			{
				get
				{
					System.Boolean? data = entity.IsBlud;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBlud = null;
					else entity.IsBlud = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsPendapatan
			{
				get
				{
					System.Boolean? data = entity.IsPendapatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPendapatan = null;
					else entity.IsPendapatan = Convert.ToBoolean(value);
				}
			}
				
			public System.String RekeningBlud
			{
				get
				{
					System.String data = entity.RekeningBlud;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RekeningBlud = null;
					else entity.RekeningBlud = Convert.ToString(value);
				}
			}
				
			public System.String PelangganBlud
			{
				get
				{
					System.String data = entity.PelangganBlud;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PelangganBlud = null;
					else entity.PelangganBlud = Convert.ToString(value);
				}
			}
				
			public System.String UnitBlud
			{
				get
				{
					System.String data = entity.UnitBlud;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnitBlud = null;
					else entity.UnitBlud = Convert.ToString(value);
				}
			}
				
			public System.String Tanggal
			{
				get
				{
					System.DateTime? data = entity.Tanggal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tanggal = null;
					else entity.Tanggal = Convert.ToDateTime(value);
				}
			}
				
			public System.String Nomor
			{
				get
				{
					System.String data = entity.Nomor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nomor = null;
					else entity.Nomor = Convert.ToString(value);
				}
			}
				
			public System.String Uraian
			{
				get
				{
					System.String data = entity.Uraian;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Uraian = null;
					else entity.Uraian = Convert.ToString(value);
				}
			}
				
			public System.String User
			{
				get
				{
					System.String data = entity.User;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.User = null;
					else entity.User = Convert.ToString(value);
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
				
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
				
			public System.String ApprovedDate
			{
				get
				{
					System.DateTime? data = entity.ApprovedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDate = null;
					else entity.ApprovedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
				
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
				
			public System.String VoidDate
			{
				get
				{
					System.DateTime? data = entity.VoidDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDate = null;
					else entity.VoidDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			

			private esBlud entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBludQuery query)
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
				throw new Exception("esBlud can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esBludQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BludMetadata.Meta();
			}
		}	
		

		public esQueryItem BludID
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.BludID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsBlud
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.IsBlud, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsPendapatan
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.IsPendapatan, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem RekeningBlud
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.RekeningBlud, esSystemType.String);
			}
		} 
		
		public esQueryItem PelangganBlud
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.PelangganBlud, esSystemType.String);
			}
		} 
		
		public esQueryItem UnitBlud
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.UnitBlud, esSystemType.String);
			}
		} 
		
		public esQueryItem Tanggal
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.Tanggal, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Nomor
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.Nomor, esSystemType.String);
			}
		} 
		
		public esQueryItem Uraian
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.Uraian, esSystemType.String);
			}
		} 
		
		public esQueryItem User
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.User, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem VoidDate
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.VoidDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, BludMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BludCollection")]
	public partial class BludCollection : esBludCollection, IEnumerable<Blud>
	{
		public BludCollection()
		{

		}
		
		public static implicit operator List<Blud>(BludCollection coll)
		{
			List<Blud> list = new List<Blud>();
			
			foreach (Blud emp in coll)
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
				return  BludMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BludQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Blud(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Blud();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BludQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BludQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BludQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Blud AddNew()
		{
			Blud entity = base.AddNewEntity() as Blud;
			
			return entity;
		}

		public Blud FindByPrimaryKey(System.Int32 bludID)
		{
			return base.FindByPrimaryKey(bludID) as Blud;
		}


		#region IEnumerable<Blud> Members

		IEnumerator<Blud> IEnumerable<Blud>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Blud;
			}
		}

		#endregion
		
		private BludQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Blud' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Blud ({BludID})")]
	[Serializable]
	public partial class Blud : esBlud
	{
		public Blud()
		{

		}
	
		public Blud(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BludMetadata.Meta();
			}
		}
		
		
		
		override protected esBludQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BludQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BludQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BludQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BludQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BludQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BludQuery : esBludQuery
	{
		public BludQuery()
		{

		}		
		
		public BludQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BludQuery";
        }
		
			
	}


	[Serializable]
	public partial class BludMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BludMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BludMetadata.ColumnNames.BludID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = BludMetadata.PropertyNames.BludID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.IsBlud, 1, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BludMetadata.PropertyNames.IsBlud;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.IsPendapatan, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BludMetadata.PropertyNames.IsPendapatan;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.RekeningBlud, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.RekeningBlud;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.PelangganBlud, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.PelangganBlud;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.UnitBlud, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.UnitBlud;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.Tanggal, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BludMetadata.PropertyNames.Tanggal;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.Nomor, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.Nomor;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.Uraian, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.Uraian;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.User, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.User;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BludMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.IsApproved, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BludMetadata.PropertyNames.IsApproved;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.ApprovedDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BludMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.ApprovedByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.IsVoid, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BludMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.VoidDate, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BludMetadata.PropertyNames.VoidDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BludMetadata.ColumnNames.VoidByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = BludMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BludMetadata Meta()
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
			 public const string BludID = "BludID";
			 public const string IsBlud = "IsBlud";
			 public const string IsPendapatan = "IsPendapatan";
			 public const string RekeningBlud = "RekeningBlud";
			 public const string PelangganBlud = "PelangganBlud";
			 public const string UnitBlud = "UnitBlud";
			 public const string Tanggal = "Tanggal";
			 public const string Nomor = "Nomor";
			 public const string Uraian = "Uraian";
			 public const string User = "User";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BludID = "BludID";
			 public const string IsBlud = "IsBlud";
			 public const string IsPendapatan = "IsPendapatan";
			 public const string RekeningBlud = "RekeningBlud";
			 public const string PelangganBlud = "PelangganBlud";
			 public const string UnitBlud = "UnitBlud";
			 public const string Tanggal = "Tanggal";
			 public const string Nomor = "Nomor";
			 public const string Uraian = "Uraian";
			 public const string User = "User";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDate = "ApprovedDate";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string IsVoid = "IsVoid";
			 public const string VoidDate = "VoidDate";
			 public const string VoidByUserID = "VoidByUserID";
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
			lock (typeof(BludMetadata))
			{
				if(BludMetadata.mapDelegates == null)
				{
					BludMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BludMetadata.meta == null)
				{
					BludMetadata.meta = new BludMetadata();
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
				

				meta.AddTypeMap("BludID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsBlud", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPendapatan", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RekeningBlud", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PelangganBlud", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UnitBlud", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Tanggal", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Nomor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Uraian", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("User", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Blud";
				meta.Destination = "Blud";
				
				meta.spInsert = "proc_BludInsert";				
				meta.spUpdate = "proc_BludUpdate";		
				meta.spDelete = "proc_BludDelete";
				meta.spLoadAll = "proc_BludLoadAll";
				meta.spLoadByPrimaryKey = "proc_BludLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BludMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
