/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:15 PM
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
	abstract public class esRlTxReport4BCollection : esEntityCollectionWAuditLog
	{
		public esRlTxReport4BCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport4BCollection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport4BQuery query)
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
			this.InitQuery(query as esRlTxReport4BQuery);
		}
		#endregion
		
		virtual public RlTxReport4B DetachEntity(RlTxReport4B entity)
		{
			return base.DetachEntity(entity) as RlTxReport4B;
		}
		
		virtual public RlTxReport4B AttachEntity(RlTxReport4B entity)
		{
			return base.AttachEntity(entity) as RlTxReport4B;
		}
		
		virtual public void Combine(RlTxReport4BCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport4B this[int index]
		{
			get
			{
				return base[index] as RlTxReport4B;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport4B);
		}
	}



	[Serializable]
	abstract public class esRlTxReport4B : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport4BQuery GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport4B()
		{

		}

		public esRlTxReport4B(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			esRlTxReport4BQuery query = this.GetDynamicQuery();
			query.Where(query.RlTxReportNo == rlTxReportNo, query.RlMasterReportItemID == rlMasterReportItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("RlTxReportNo",rlTxReportNo);			parms.Add("RlMasterReportItemID",rlMasterReportItemID);
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
						case "RlTxReportNo": this.str.RlTxReportNo = (string)value; break;							
						case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;							
						case "L0006h": this.str.L0006h = (string)value; break;							
						case "P0006h": this.str.P0006h = (string)value; break;							
						case "L0628h": this.str.L0628h = (string)value; break;							
						case "P0628h": this.str.P0628h = (string)value; break;							
						case "L28h01t": this.str.L28h01t = (string)value; break;							
						case "P28h01t": this.str.P28h01t = (string)value; break;							
						case "L0104t": this.str.L0104t = (string)value; break;							
						case "P0104t": this.str.P0104t = (string)value; break;							
						case "L0414t": this.str.L0414t = (string)value; break;							
						case "P0414t": this.str.P0414t = (string)value; break;							
						case "L1424t": this.str.L1424t = (string)value; break;							
						case "P1424t": this.str.P1424t = (string)value; break;							
						case "L2444t": this.str.L2444t = (string)value; break;							
						case "P2444t": this.str.P2444t = (string)value; break;							
						case "L4464t": this.str.L4464t = (string)value; break;							
						case "P4464t": this.str.P4464t = (string)value; break;							
						case "L64t": this.str.L64t = (string)value; break;							
						case "P64t": this.str.P64t = (string)value; break;							
						case "KasusBaruL": this.str.KasusBaruL = (string)value; break;							
						case "KasusBaruP": this.str.KasusBaruP = (string)value; break;							
						case "TotalKasusBaru": this.str.TotalKasusBaru = (string)value; break;							
						case "TotalKunjungan": this.str.TotalKunjungan = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RlMasterReportItemID":
						
							if (value == null || value is System.Int32)
								this.RlMasterReportItemID = (System.Int32?)value;
							break;
						
						case "L0006h":
						
							if (value == null || value is System.Int32)
								this.L0006h = (System.Int32?)value;
							break;
						
						case "P0006h":
						
							if (value == null || value is System.Int32)
								this.P0006h = (System.Int32?)value;
							break;
						
						case "L0628h":
						
							if (value == null || value is System.Int32)
								this.L0628h = (System.Int32?)value;
							break;
						
						case "P0628h":
						
							if (value == null || value is System.Int32)
								this.P0628h = (System.Int32?)value;
							break;
						
						case "L28h01t":
						
							if (value == null || value is System.Int32)
								this.L28h01t = (System.Int32?)value;
							break;
						
						case "P28h01t":
						
							if (value == null || value is System.Int32)
								this.P28h01t = (System.Int32?)value;
							break;
						
						case "L0104t":
						
							if (value == null || value is System.Int32)
								this.L0104t = (System.Int32?)value;
							break;
						
						case "P0104t":
						
							if (value == null || value is System.Int32)
								this.P0104t = (System.Int32?)value;
							break;
						
						case "L0414t":
						
							if (value == null || value is System.Int32)
								this.L0414t = (System.Int32?)value;
							break;
						
						case "P0414t":
						
							if (value == null || value is System.Int32)
								this.P0414t = (System.Int32?)value;
							break;
						
						case "L1424t":
						
							if (value == null || value is System.Int32)
								this.L1424t = (System.Int32?)value;
							break;
						
						case "P1424t":
						
							if (value == null || value is System.Int32)
								this.P1424t = (System.Int32?)value;
							break;
						
						case "L2444t":
						
							if (value == null || value is System.Int32)
								this.L2444t = (System.Int32?)value;
							break;
						
						case "P2444t":
						
							if (value == null || value is System.Int32)
								this.P2444t = (System.Int32?)value;
							break;
						
						case "L4464t":
						
							if (value == null || value is System.Int32)
								this.L4464t = (System.Int32?)value;
							break;
						
						case "P4464t":
						
							if (value == null || value is System.Int32)
								this.P4464t = (System.Int32?)value;
							break;
						
						case "L64t":
						
							if (value == null || value is System.Int32)
								this.L64t = (System.Int32?)value;
							break;
						
						case "P64t":
						
							if (value == null || value is System.Int32)
								this.P64t = (System.Int32?)value;
							break;
						
						case "KasusBaruL":
						
							if (value == null || value is System.Int32)
								this.KasusBaruL = (System.Int32?)value;
							break;
						
						case "KasusBaruP":
						
							if (value == null || value is System.Int32)
								this.KasusBaruP = (System.Int32?)value;
							break;
						
						case "TotalKasusBaru":
						
							if (value == null || value is System.Int32)
								this.TotalKasusBaru = (System.Int32?)value;
							break;
						
						case "TotalKunjungan":
						
							if (value == null || value is System.Int32)
								this.TotalKunjungan = (System.Int32?)value;
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
		/// Maps to RlTxReport4B.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport4BMetadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport4BMetadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L0006h
		/// </summary>
		virtual public System.Int32? L0006h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0006h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0006h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P0006h
		/// </summary>
		virtual public System.Int32? P0006h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0006h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0006h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L0628h
		/// </summary>
		virtual public System.Int32? L0628h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0628h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0628h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P0628h
		/// </summary>
		virtual public System.Int32? P0628h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0628h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0628h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L28h01t
		/// </summary>
		virtual public System.Int32? L28h01t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L28h01t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L28h01t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P28h01t
		/// </summary>
		virtual public System.Int32? P28h01t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P28h01t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P28h01t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L0104t
		/// </summary>
		virtual public System.Int32? L0104t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0104t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0104t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P0104t
		/// </summary>
		virtual public System.Int32? P0104t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0104t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0104t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L0414t
		/// </summary>
		virtual public System.Int32? L0414t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0414t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L0414t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P0414t
		/// </summary>
		virtual public System.Int32? P0414t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0414t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P0414t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L1424t
		/// </summary>
		virtual public System.Int32? L1424t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L1424t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L1424t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P1424t
		/// </summary>
		virtual public System.Int32? P1424t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P1424t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P1424t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L2444t
		/// </summary>
		virtual public System.Int32? L2444t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L2444t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L2444t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P2444t
		/// </summary>
		virtual public System.Int32? P2444t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P2444t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P2444t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L4464t
		/// </summary>
		virtual public System.Int32? L4464t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L4464t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L4464t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P4464t
		/// </summary>
		virtual public System.Int32? P4464t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P4464t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P4464t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.L64t
		/// </summary>
		virtual public System.Int32? L64t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.L64t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.L64t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.P64t
		/// </summary>
		virtual public System.Int32? P64t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.P64t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.P64t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.KasusBaruL
		/// </summary>
		virtual public System.Int32? KasusBaruL
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.KasusBaruL);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.KasusBaruL, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.KasusBaruP
		/// </summary>
		virtual public System.Int32? KasusBaruP
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.KasusBaruP);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.KasusBaruP, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.TotalKasusBaru
		/// </summary>
		virtual public System.Int32? TotalKasusBaru
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.TotalKasusBaru);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.TotalKasusBaru, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.TotalKunjungan
		/// </summary>
		virtual public System.Int32? TotalKunjungan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4BMetadata.ColumnNames.TotalKunjungan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4BMetadata.ColumnNames.TotalKunjungan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport4BMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport4BMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4B.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport4BMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport4BMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport4B entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RlTxReportNo
			{
				get
				{
					System.String data = entity.RlTxReportNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlTxReportNo = null;
					else entity.RlTxReportNo = Convert.ToString(value);
				}
			}
				
			public System.String RlMasterReportItemID
			{
				get
				{
					System.Int32? data = entity.RlMasterReportItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
					else entity.RlMasterReportItemID = Convert.ToInt32(value);
				}
			}
				
			public System.String L0006h
			{
				get
				{
					System.Int32? data = entity.L0006h;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L0006h = null;
					else entity.L0006h = Convert.ToInt32(value);
				}
			}
				
			public System.String P0006h
			{
				get
				{
					System.Int32? data = entity.P0006h;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P0006h = null;
					else entity.P0006h = Convert.ToInt32(value);
				}
			}
				
			public System.String L0628h
			{
				get
				{
					System.Int32? data = entity.L0628h;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L0628h = null;
					else entity.L0628h = Convert.ToInt32(value);
				}
			}
				
			public System.String P0628h
			{
				get
				{
					System.Int32? data = entity.P0628h;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P0628h = null;
					else entity.P0628h = Convert.ToInt32(value);
				}
			}
				
			public System.String L28h01t
			{
				get
				{
					System.Int32? data = entity.L28h01t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L28h01t = null;
					else entity.L28h01t = Convert.ToInt32(value);
				}
			}
				
			public System.String P28h01t
			{
				get
				{
					System.Int32? data = entity.P28h01t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P28h01t = null;
					else entity.P28h01t = Convert.ToInt32(value);
				}
			}
				
			public System.String L0104t
			{
				get
				{
					System.Int32? data = entity.L0104t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L0104t = null;
					else entity.L0104t = Convert.ToInt32(value);
				}
			}
				
			public System.String P0104t
			{
				get
				{
					System.Int32? data = entity.P0104t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P0104t = null;
					else entity.P0104t = Convert.ToInt32(value);
				}
			}
				
			public System.String L0414t
			{
				get
				{
					System.Int32? data = entity.L0414t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L0414t = null;
					else entity.L0414t = Convert.ToInt32(value);
				}
			}
				
			public System.String P0414t
			{
				get
				{
					System.Int32? data = entity.P0414t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P0414t = null;
					else entity.P0414t = Convert.ToInt32(value);
				}
			}
				
			public System.String L1424t
			{
				get
				{
					System.Int32? data = entity.L1424t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L1424t = null;
					else entity.L1424t = Convert.ToInt32(value);
				}
			}
				
			public System.String P1424t
			{
				get
				{
					System.Int32? data = entity.P1424t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P1424t = null;
					else entity.P1424t = Convert.ToInt32(value);
				}
			}
				
			public System.String L2444t
			{
				get
				{
					System.Int32? data = entity.L2444t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L2444t = null;
					else entity.L2444t = Convert.ToInt32(value);
				}
			}
				
			public System.String P2444t
			{
				get
				{
					System.Int32? data = entity.P2444t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P2444t = null;
					else entity.P2444t = Convert.ToInt32(value);
				}
			}
				
			public System.String L4464t
			{
				get
				{
					System.Int32? data = entity.L4464t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L4464t = null;
					else entity.L4464t = Convert.ToInt32(value);
				}
			}
				
			public System.String P4464t
			{
				get
				{
					System.Int32? data = entity.P4464t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P4464t = null;
					else entity.P4464t = Convert.ToInt32(value);
				}
			}
				
			public System.String L64t
			{
				get
				{
					System.Int32? data = entity.L64t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.L64t = null;
					else entity.L64t = Convert.ToInt32(value);
				}
			}
				
			public System.String P64t
			{
				get
				{
					System.Int32? data = entity.P64t;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.P64t = null;
					else entity.P64t = Convert.ToInt32(value);
				}
			}
				
			public System.String KasusBaruL
			{
				get
				{
					System.Int32? data = entity.KasusBaruL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KasusBaruL = null;
					else entity.KasusBaruL = Convert.ToInt32(value);
				}
			}
				
			public System.String KasusBaruP
			{
				get
				{
					System.Int32? data = entity.KasusBaruP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KasusBaruP = null;
					else entity.KasusBaruP = Convert.ToInt32(value);
				}
			}
				
			public System.String TotalKasusBaru
			{
				get
				{
					System.Int32? data = entity.TotalKasusBaru;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalKasusBaru = null;
					else entity.TotalKasusBaru = Convert.ToInt32(value);
				}
			}
				
			public System.String TotalKunjungan
			{
				get
				{
					System.Int32? data = entity.TotalKunjungan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalKunjungan = null;
					else entity.TotalKunjungan = Convert.ToInt32(value);
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
			

			private esRlTxReport4B entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport4BQuery query)
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
				throw new Exception("esRlTxReport4B can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport4B : esRlTxReport4B
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
	abstract public class esRlTxReport4BQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport4BMetadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0006h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L0006h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0006h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P0006h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0628h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L0628h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0628h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P0628h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L28h01t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L28h01t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P28h01t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P28h01t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0104t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L0104t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0104t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P0104t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0414t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L0414t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0414t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P0414t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L1424t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L1424t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P1424t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P1424t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L2444t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L2444t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P2444t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P2444t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L4464t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L4464t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P4464t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P4464t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L64t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.L64t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P64t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.P64t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KasusBaruL
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.KasusBaruL, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KasusBaruP
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.KasusBaruP, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TotalKasusBaru
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.TotalKasusBaru, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TotalKunjungan
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.TotalKunjungan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport4BMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport4BCollection")]
	public partial class RlTxReport4BCollection : esRlTxReport4BCollection, IEnumerable<RlTxReport4B>
	{
		public RlTxReport4BCollection()
		{

		}
		
		public static implicit operator List<RlTxReport4B>(RlTxReport4BCollection coll)
		{
			List<RlTxReport4B> list = new List<RlTxReport4B>();
			
			foreach (RlTxReport4B emp in coll)
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
				return  RlTxReport4BMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport4BQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport4B(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport4B();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport4BQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport4BQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport4BQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport4B AddNew()
		{
			RlTxReport4B entity = base.AddNewEntity() as RlTxReport4B;
			
			return entity;
		}

		public RlTxReport4B FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport4B;
		}


		#region IEnumerable<RlTxReport4B> Members

		IEnumerator<RlTxReport4B> IEnumerable<RlTxReport4B>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport4B;
			}
		}

		#endregion
		
		private RlTxReport4BQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport4B' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport4B ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport4B : esRlTxReport4B
	{
		public RlTxReport4B()
		{

		}
	
		public RlTxReport4B(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport4BMetadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport4BQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport4BQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport4BQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport4BQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport4BQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport4BQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport4BQuery : esRlTxReport4BQuery
	{
		public RlTxReport4BQuery()
		{

		}		
		
		public RlTxReport4BQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport4BQuery";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport4BMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport4BMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L0006h, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L0006h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P0006h, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P0006h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L0628h, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L0628h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P0628h, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P0628h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L28h01t, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L28h01t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P28h01t, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P28h01t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L0104t, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L0104t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P0104t, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P0104t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L0414t, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L0414t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P0414t, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P0414t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L1424t, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L1424t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P1424t, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P1424t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L2444t, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L2444t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P2444t, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P2444t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L4464t, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L4464t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P4464t, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P4464t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.L64t, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.L64t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.P64t, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.P64t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.KasusBaruL, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.KasusBaruL;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.KasusBaruP, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.KasusBaruP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.TotalKasusBaru, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.TotalKasusBaru;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.TotalKunjungan, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.TotalKunjungan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.LastUpdateDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4BMetadata.ColumnNames.LastUpdateByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport4BMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport4BMetadata Meta()
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
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string L0006h = "L0006h";
			 public const string P0006h = "P0006h";
			 public const string L0628h = "L0628h";
			 public const string P0628h = "P0628h";
			 public const string L28h01t = "L28h01t";
			 public const string P28h01t = "P28h01t";
			 public const string L0104t = "L0104t";
			 public const string P0104t = "P0104t";
			 public const string L0414t = "L0414t";
			 public const string P0414t = "P0414t";
			 public const string L1424t = "L1424t";
			 public const string P1424t = "P1424t";
			 public const string L2444t = "L2444t";
			 public const string P2444t = "P2444t";
			 public const string L4464t = "L4464t";
			 public const string P4464t = "P4464t";
			 public const string L64t = "L64t";
			 public const string P64t = "P64t";
			 public const string KasusBaruL = "KasusBaruL";
			 public const string KasusBaruP = "KasusBaruP";
			 public const string TotalKasusBaru = "TotalKasusBaru";
			 public const string TotalKunjungan = "TotalKunjungan";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string L0006h = "L0006h";
			 public const string P0006h = "P0006h";
			 public const string L0628h = "L0628h";
			 public const string P0628h = "P0628h";
			 public const string L28h01t = "L28h01t";
			 public const string P28h01t = "P28h01t";
			 public const string L0104t = "L0104t";
			 public const string P0104t = "P0104t";
			 public const string L0414t = "L0414t";
			 public const string P0414t = "P0414t";
			 public const string L1424t = "L1424t";
			 public const string P1424t = "P1424t";
			 public const string L2444t = "L2444t";
			 public const string P2444t = "P2444t";
			 public const string L4464t = "L4464t";
			 public const string P4464t = "P4464t";
			 public const string L64t = "L64t";
			 public const string P64t = "P64t";
			 public const string KasusBaruL = "KasusBaruL";
			 public const string KasusBaruP = "KasusBaruP";
			 public const string TotalKasusBaru = "TotalKasusBaru";
			 public const string TotalKunjungan = "TotalKunjungan";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(RlTxReport4BMetadata))
			{
				if(RlTxReport4BMetadata.mapDelegates == null)
				{
					RlTxReport4BMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport4BMetadata.meta == null)
				{
					RlTxReport4BMetadata.meta = new RlTxReport4BMetadata();
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
				

				meta.AddTypeMap("RlTxReportNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L0006h", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P0006h", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L0628h", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P0628h", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L28h01t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P28h01t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L0104t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P0104t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L0414t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P0414t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L1424t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P1424t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L2444t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P2444t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L4464t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P4464t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("L64t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("P64t", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KasusBaruL", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KasusBaruP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalKasusBaru", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalKunjungan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport4B";
				meta.Destination = "RlTxReport4B";
				
				meta.spInsert = "proc_RlTxReport4BInsert";				
				meta.spUpdate = "proc_RlTxReport4BUpdate";		
				meta.spDelete = "proc_RlTxReport4BDelete";
				meta.spLoadAll = "proc_RlTxReport4BLoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport4BLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport4BMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
