/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 13/06/2022 14:58:34
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esRenkinTransactionDetailCollection : esEntityCollectionWAuditLog
	{
		public esRenkinTransactionDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RenkinTransactionDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRenkinTransactionDetailQuery query)
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
			this.InitQuery(query as esRenkinTransactionDetailQuery);
		}
		#endregion
			
		virtual public RenkinTransactionDetail DetachEntity(RenkinTransactionDetail entity)
		{
			return base.DetachEntity(entity) as RenkinTransactionDetail;
		}
		
		virtual public RenkinTransactionDetail AttachEntity(RenkinTransactionDetail entity)
		{
			return base.AttachEntity(entity) as RenkinTransactionDetail;
		}
		
		virtual public void Combine(RenkinTransactionDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RenkinTransactionDetail this[int index]
		{
			get
			{
				return base[index] as RenkinTransactionDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RenkinTransactionDetail);
		}
	}

	[Serializable]
	abstract public class esRenkinTransactionDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRenkinTransactionDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRenkinTransactionDetail()
		{
		}
	
		public esRenkinTransactionDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 detailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailID);
			else
				return LoadByPrimaryKeyStoredProcedure(detailID);
		}
	
		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 detailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(detailID);
			else
				return LoadByPrimaryKeyStoredProcedure(detailID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 detailID)
		{
			esRenkinTransactionDetailQuery query = this.GetDynamicQuery();
			query.Where(query.DetailID == detailID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 detailID)
		{
			esParameters parms = new esParameters();
			parms.Add("DetailID",detailID);
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
						case "DetailID": this.str.DetailID = (string)value; break;
						case "TransactionID": this.str.TransactionID = (string)value; break;
						case "RenkinID": this.str.RenkinID = (string)value; break;
						case "Kegiatan": this.str.Kegiatan = (string)value; break;
						case "SRRenkinRumusCapaian": this.str.SRRenkinRumusCapaian = (string)value; break;
						case "SRRenkinRumusRealisasi": this.str.SRRenkinRumusRealisasi = (string)value; break;
						case "TargetTW1": this.str.TargetTW1 = (string)value; break;
						case "TargetTW2": this.str.TargetTW2 = (string)value; break;
						case "TargetTW3": this.str.TargetTW3 = (string)value; break;
						case "TargetTW4": this.str.TargetTW4 = (string)value; break;
						case "RealisasiTW1": this.str.RealisasiTW1 = (string)value; break;
						case "RealisasiTW2": this.str.RealisasiTW2 = (string)value; break;
						case "RealisasiTW3": this.str.RealisasiTW3 = (string)value; break;
						case "RealisasiTW4": this.str.RealisasiTW4 = (string)value; break;
						case "ValidasiTW1": this.str.ValidasiTW1 = (string)value; break;
						case "ValidasiTW2": this.str.ValidasiTW2 = (string)value; break;
						case "ValidasiTW3": this.str.ValidasiTW3 = (string)value; break;
						case "ValidasiTW4": this.str.ValidasiTW4 = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DetailID":
						
							if (value == null || value is System.Int32)
								this.DetailID = (System.Int32?)value;
							break;
						case "TransactionID":
						
							if (value == null || value is System.Int32)
								this.TransactionID = (System.Int32?)value;
							break;
						case "RenkinID":
						
							if (value == null || value is System.Int32)
								this.RenkinID = (System.Int32?)value;
							break;
						case "TargetTW1":
						
							if (value == null || value is System.Int32)
								this.TargetTW1 = (System.Int32?)value;
							break;
						case "TargetTW2":
						
							if (value == null || value is System.Int32)
								this.TargetTW2 = (System.Int32?)value;
							break;
						case "TargetTW3":
						
							if (value == null || value is System.Int32)
								this.TargetTW3 = (System.Int32?)value;
							break;
						case "TargetTW4":
						
							if (value == null || value is System.Int32)
								this.TargetTW4 = (System.Int32?)value;
							break;
						case "RealisasiTW1":
						
							if (value == null || value is System.Int32)
								this.RealisasiTW1 = (System.Int32?)value;
							break;
						case "RealisasiTW2":
						
							if (value == null || value is System.Int32)
								this.RealisasiTW2 = (System.Int32?)value;
							break;
						case "RealisasiTW3":
						
							if (value == null || value is System.Int32)
								this.RealisasiTW3 = (System.Int32?)value;
							break;
						case "RealisasiTW4":
						
							if (value == null || value is System.Int32)
								this.RealisasiTW4 = (System.Int32?)value;
							break;
						case "ValidasiTW1":
						
							if (value == null || value is System.Int32)
								this.ValidasiTW1 = (System.Int32?)value;
							break;
						case "ValidasiTW2":
						
							if (value == null || value is System.Int32)
								this.ValidasiTW2 = (System.Int32?)value;
							break;
						case "ValidasiTW3":
						
							if (value == null || value is System.Int32)
								this.ValidasiTW3 = (System.Int32?)value;
							break;
						case "ValidasiTW4":
						
							if (value == null || value is System.Int32)
								this.ValidasiTW4 = (System.Int32?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to RenkinTransactionDetail.DetailID
		/// </summary>
		virtual public System.Int32? DetailID
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.DetailID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.DetailID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.TransactionID
		/// </summary>
		virtual public System.Int32? TransactionID
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TransactionID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TransactionID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.RenkinID
		/// </summary>
		virtual public System.Int32? RenkinID
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RenkinID);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RenkinID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.Kegiatan
		/// </summary>
		virtual public System.String Kegiatan
		{
			get
			{
				return base.GetSystemString(RenkinTransactionDetailMetadata.ColumnNames.Kegiatan);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionDetailMetadata.ColumnNames.Kegiatan, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.SRRenkinRumusCapaian
		/// </summary>
		virtual public System.String SRRenkinRumusCapaian
		{
			get
			{
				return base.GetSystemString(RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusCapaian);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusCapaian, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.SRRenkinRumusRealisasi
		/// </summary>
		virtual public System.String SRRenkinRumusRealisasi
		{
			get
			{
				return base.GetSystemString(RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusRealisasi);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusRealisasi, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.TargetTW1
		/// </summary>
		virtual public System.Int32? TargetTW1
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW1);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW1, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.TargetTW2
		/// </summary>
		virtual public System.Int32? TargetTW2
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW2);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW2, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.TargetTW3
		/// </summary>
		virtual public System.Int32? TargetTW3
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW3);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW3, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.TargetTW4
		/// </summary>
		virtual public System.Int32? TargetTW4
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW4);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.TargetTW4, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.RealisasiTW1
		/// </summary>
		virtual public System.Int32? RealisasiTW1
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW1);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW1, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.RealisasiTW2
		/// </summary>
		virtual public System.Int32? RealisasiTW2
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW2);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW2, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.RealisasiTW3
		/// </summary>
		virtual public System.Int32? RealisasiTW3
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW3);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW3, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.RealisasiTW4
		/// </summary>
		virtual public System.Int32? RealisasiTW4
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW4);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW4, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.ValidasiTW1
		/// </summary>
		virtual public System.Int32? ValidasiTW1
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW1);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW1, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.ValidasiTW2
		/// </summary>
		virtual public System.Int32? ValidasiTW2
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW2);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW2, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.ValidasiTW3
		/// </summary>
		virtual public System.Int32? ValidasiTW3
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW3);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW3, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.ValidasiTW4
		/// </summary>
		virtual public System.Int32? ValidasiTW4
		{
			get
			{
				return base.GetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW4);
			}
			
			set
			{
				base.SetSystemInt32(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW4, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinTransactionDetailMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinTransactionDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinTransactionDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RenkinTransactionDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RenkinTransactionDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RenkinTransactionDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RenkinTransactionDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RenkinTransactionDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		#endregion	

		#region String Properties
		
		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
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
			public esStrings(esRenkinTransactionDetail entity)
			{
				this.entity = entity;
			}
			public System.String DetailID
			{
				get
				{
					System.Int32? data = entity.DetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailID = null;
					else entity.DetailID = Convert.ToInt32(value);
				}
			}
			public System.String TransactionID
			{
				get
				{
					System.Int32? data = entity.TransactionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionID = null;
					else entity.TransactionID = Convert.ToInt32(value);
				}
			}
			public System.String RenkinID
			{
				get
				{
					System.Int32? data = entity.RenkinID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RenkinID = null;
					else entity.RenkinID = Convert.ToInt32(value);
				}
			}
			public System.String Kegiatan
			{
				get
				{
					System.String data = entity.Kegiatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kegiatan = null;
					else entity.Kegiatan = Convert.ToString(value);
				}
			}
			public System.String SRRenkinRumusCapaian
			{
				get
				{
					System.String data = entity.SRRenkinRumusCapaian;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRenkinRumusCapaian = null;
					else entity.SRRenkinRumusCapaian = Convert.ToString(value);
				}
			}
			public System.String SRRenkinRumusRealisasi
			{
				get
				{
					System.String data = entity.SRRenkinRumusRealisasi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRenkinRumusRealisasi = null;
					else entity.SRRenkinRumusRealisasi = Convert.ToString(value);
				}
			}
			public System.String TargetTW1
			{
				get
				{
					System.Int32? data = entity.TargetTW1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetTW1 = null;
					else entity.TargetTW1 = Convert.ToInt32(value);
				}
			}
			public System.String TargetTW2
			{
				get
				{
					System.Int32? data = entity.TargetTW2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetTW2 = null;
					else entity.TargetTW2 = Convert.ToInt32(value);
				}
			}
			public System.String TargetTW3
			{
				get
				{
					System.Int32? data = entity.TargetTW3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetTW3 = null;
					else entity.TargetTW3 = Convert.ToInt32(value);
				}
			}
			public System.String TargetTW4
			{
				get
				{
					System.Int32? data = entity.TargetTW4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TargetTW4 = null;
					else entity.TargetTW4 = Convert.ToInt32(value);
				}
			}
			public System.String RealisasiTW1
			{
				get
				{
					System.Int32? data = entity.RealisasiTW1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealisasiTW1 = null;
					else entity.RealisasiTW1 = Convert.ToInt32(value);
				}
			}
			public System.String RealisasiTW2
			{
				get
				{
					System.Int32? data = entity.RealisasiTW2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealisasiTW2 = null;
					else entity.RealisasiTW2 = Convert.ToInt32(value);
				}
			}
			public System.String RealisasiTW3
			{
				get
				{
					System.Int32? data = entity.RealisasiTW3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealisasiTW3 = null;
					else entity.RealisasiTW3 = Convert.ToInt32(value);
				}
			}
			public System.String RealisasiTW4
			{
				get
				{
					System.Int32? data = entity.RealisasiTW4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RealisasiTW4 = null;
					else entity.RealisasiTW4 = Convert.ToInt32(value);
				}
			}
			public System.String ValidasiTW1
			{
				get
				{
					System.Int32? data = entity.ValidasiTW1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidasiTW1 = null;
					else entity.ValidasiTW1 = Convert.ToInt32(value);
				}
			}
			public System.String ValidasiTW2
			{
				get
				{
					System.Int32? data = entity.ValidasiTW2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidasiTW2 = null;
					else entity.ValidasiTW2 = Convert.ToInt32(value);
				}
			}
			public System.String ValidasiTW3
			{
				get
				{
					System.Int32? data = entity.ValidasiTW3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidasiTW3 = null;
					else entity.ValidasiTW3 = Convert.ToInt32(value);
				}
			}
			public System.String ValidasiTW4
			{
				get
				{
					System.Int32? data = entity.ValidasiTW4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidasiTW4 = null;
					else entity.ValidasiTW4 = Convert.ToInt32(value);
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
			private esRenkinTransactionDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRenkinTransactionDetailQuery query)
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
				throw new Exception("esRenkinTransactionDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RenkinTransactionDetail : esRenkinTransactionDetail
	{	
	}

	[Serializable]
	abstract public class esRenkinTransactionDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RenkinTransactionDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem DetailID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.DetailID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TransactionID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.TransactionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RenkinID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.RenkinID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Kegiatan
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.Kegiatan, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRenkinRumusCapaian
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusCapaian, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRenkinRumusRealisasi
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusRealisasi, esSystemType.String);
			}
		} 
			
		public esQueryItem TargetTW1
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.TargetTW1, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TargetTW2
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.TargetTW2, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TargetTW3
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.TargetTW3, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TargetTW4
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.TargetTW4, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RealisasiTW1
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW1, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RealisasiTW2
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW2, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RealisasiTW3
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW3, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RealisasiTW4
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW4, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ValidasiTW1
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW1, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ValidasiTW2
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW2, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ValidasiTW3
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW3, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ValidasiTW4
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW4, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RenkinTransactionDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RenkinTransactionDetailCollection")]
	public partial class RenkinTransactionDetailCollection : esRenkinTransactionDetailCollection, IEnumerable< RenkinTransactionDetail>
	{
		public RenkinTransactionDetailCollection()
		{

		}	
		
		public static implicit operator List< RenkinTransactionDetail>(RenkinTransactionDetailCollection coll)
		{
			List< RenkinTransactionDetail> list = new List< RenkinTransactionDetail>();
			
			foreach (RenkinTransactionDetail emp in coll)
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
				return  RenkinTransactionDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinTransactionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RenkinTransactionDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RenkinTransactionDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RenkinTransactionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinTransactionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(RenkinTransactionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RenkinTransactionDetail AddNew()
		{
			RenkinTransactionDetail entity = base.AddNewEntity() as RenkinTransactionDetail;
			
			return entity;		
		}
		public RenkinTransactionDetail FindByPrimaryKey(Int32 detailID)
		{
			return base.FindByPrimaryKey(detailID) as RenkinTransactionDetail;
		}

		#region IEnumerable< RenkinTransactionDetail> Members

		IEnumerator< RenkinTransactionDetail> IEnumerable< RenkinTransactionDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RenkinTransactionDetail;
			}
		}

		#endregion
		
		private RenkinTransactionDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RenkinTransactionDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RenkinTransactionDetail ({DetailID})")]
	[Serializable]
	public partial class RenkinTransactionDetail : esRenkinTransactionDetail
	{
		public RenkinTransactionDetail()
		{
		}	
	
		public RenkinTransactionDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RenkinTransactionDetailMetadata.Meta();
			}
		}	
	
		override protected esRenkinTransactionDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RenkinTransactionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RenkinTransactionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RenkinTransactionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(RenkinTransactionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RenkinTransactionDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RenkinTransactionDetailQuery : esRenkinTransactionDetailQuery
	{
		public RenkinTransactionDetailQuery()
		{

		}		
		
		public RenkinTransactionDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RenkinTransactionDetailQuery";
        }
	}

	[Serializable]
	public partial class RenkinTransactionDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RenkinTransactionDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.DetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.DetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.TransactionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.TransactionID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.RenkinID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.RenkinID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.Kegiatan, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.Kegiatan;
			c.CharacterMaxLength = 300;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusCapaian, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.SRRenkinRumusCapaian;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.SRRenkinRumusRealisasi, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.SRRenkinRumusRealisasi;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.TargetTW1, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.TargetTW1;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.TargetTW2, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.TargetTW2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.TargetTW3, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.TargetTW3;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.TargetTW4, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.TargetTW4;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW1, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.RealisasiTW1;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW2, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.RealisasiTW2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW3, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.RealisasiTW3;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.RealisasiTW4, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.RealisasiTW4;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW1, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.ValidasiTW1;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW2, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.ValidasiTW2;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW3, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.ValidasiTW3;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.ValidasiTW4, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.ValidasiTW4;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.CreateByUserID, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.CreateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RenkinTransactionDetailMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RenkinTransactionDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public RenkinTransactionDetailMetadata Meta()
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
			public const string DetailID = "DetailID";
			public const string TransactionID = "TransactionID";
			public const string RenkinID = "RenkinID";
			public const string Kegiatan = "Kegiatan";
			public const string SRRenkinRumusCapaian = "SRRenkinRumusCapaian";
			public const string SRRenkinRumusRealisasi = "SRRenkinRumusRealisasi";
			public const string TargetTW1 = "TargetTW1";
			public const string TargetTW2 = "TargetTW2";
			public const string TargetTW3 = "TargetTW3";
			public const string TargetTW4 = "TargetTW4";
			public const string RealisasiTW1 = "RealisasiTW1";
			public const string RealisasiTW2 = "RealisasiTW2";
			public const string RealisasiTW3 = "RealisasiTW3";
			public const string RealisasiTW4 = "RealisasiTW4";
			public const string ValidasiTW1 = "ValidasiTW1";
			public const string ValidasiTW2 = "ValidasiTW2";
			public const string ValidasiTW3 = "ValidasiTW3";
			public const string ValidasiTW4 = "ValidasiTW4";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string DetailID = "DetailID";
			public const string TransactionID = "TransactionID";
			public const string RenkinID = "RenkinID";
			public const string Kegiatan = "Kegiatan";
			public const string SRRenkinRumusCapaian = "SRRenkinRumusCapaian";
			public const string SRRenkinRumusRealisasi = "SRRenkinRumusRealisasi";
			public const string TargetTW1 = "TargetTW1";
			public const string TargetTW2 = "TargetTW2";
			public const string TargetTW3 = "TargetTW3";
			public const string TargetTW4 = "TargetTW4";
			public const string RealisasiTW1 = "RealisasiTW1";
			public const string RealisasiTW2 = "RealisasiTW2";
			public const string RealisasiTW3 = "RealisasiTW3";
			public const string RealisasiTW4 = "RealisasiTW4";
			public const string ValidasiTW1 = "ValidasiTW1";
			public const string ValidasiTW2 = "ValidasiTW2";
			public const string ValidasiTW3 = "ValidasiTW3";
			public const string ValidasiTW4 = "ValidasiTW4";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(RenkinTransactionDetailMetadata))
			{
				if(RenkinTransactionDetailMetadata.mapDelegates == null)
				{
					RenkinTransactionDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RenkinTransactionDetailMetadata.meta == null)
				{
					RenkinTransactionDetailMetadata.meta = new RenkinTransactionDetailMetadata();
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
				
				meta.AddTypeMap("DetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TransactionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RenkinID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Kegiatan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRenkinRumusCapaian", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRenkinRumusRealisasi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TargetTW1", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TargetTW2", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TargetTW3", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TargetTW4", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RealisasiTW1", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RealisasiTW2", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RealisasiTW3", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RealisasiTW4", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidasiTW1", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidasiTW2", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidasiTW3", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidasiTW4", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "RenkinTransactionDetail";
				meta.Destination = "RenkinTransactionDetail";
				meta.spInsert = "proc_RenkinTransactionDetailInsert";				
				meta.spUpdate = "proc_RenkinTransactionDetailUpdate";		
				meta.spDelete = "proc_RenkinTransactionDetailDelete";
				meta.spLoadAll = "proc_RenkinTransactionDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_RenkinTransactionDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RenkinTransactionDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
