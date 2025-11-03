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
	abstract public class esRlTxReport4ACollection : esEntityCollectionWAuditLog
	{
		public esRlTxReport4ACollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport4ACollection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport4AQuery query)
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
			this.InitQuery(query as esRlTxReport4AQuery);
		}
		#endregion
		
		virtual public RlTxReport4A DetachEntity(RlTxReport4A entity)
		{
			return base.DetachEntity(entity) as RlTxReport4A;
		}
		
		virtual public RlTxReport4A AttachEntity(RlTxReport4A entity)
		{
			return base.AttachEntity(entity) as RlTxReport4A;
		}
		
		virtual public void Combine(RlTxReport4ACollection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport4A this[int index]
		{
			get
			{
				return base[index] as RlTxReport4A;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport4A);
		}
	}



	[Serializable]
	abstract public class esRlTxReport4A : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport4AQuery GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport4A()
		{

		}

		public esRlTxReport4A(DataRow row)
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
			esRlTxReport4AQuery query = this.GetDynamicQuery();
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
						case "TotalL": this.str.TotalL = (string)value; break;							
						case "TotalP": this.str.TotalP = (string)value; break;							
						case "Total": this.str.Total = (string)value; break;							
						case "TotalMati": this.str.TotalMati = (string)value; break;							
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
						
						case "TotalL":
						
							if (value == null || value is System.Int32)
								this.TotalL = (System.Int32?)value;
							break;
						
						case "TotalP":
						
							if (value == null || value is System.Int32)
								this.TotalP = (System.Int32?)value;
							break;
						
						case "Total":
						
							if (value == null || value is System.Int32)
								this.Total = (System.Int32?)value;
							break;
						
						case "TotalMati":
						
							if (value == null || value is System.Int32)
								this.TotalMati = (System.Int32?)value;
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
		/// Maps to RlTxReport4A.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport4AMetadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport4AMetadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L0006h
		/// </summary>
		virtual public System.Int32? L0006h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0006h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0006h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P0006h
		/// </summary>
		virtual public System.Int32? P0006h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0006h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0006h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L0628h
		/// </summary>
		virtual public System.Int32? L0628h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0628h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0628h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P0628h
		/// </summary>
		virtual public System.Int32? P0628h
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0628h);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0628h, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L28h01t
		/// </summary>
		virtual public System.Int32? L28h01t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L28h01t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L28h01t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P28h01t
		/// </summary>
		virtual public System.Int32? P28h01t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P28h01t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P28h01t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L0104t
		/// </summary>
		virtual public System.Int32? L0104t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0104t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0104t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P0104t
		/// </summary>
		virtual public System.Int32? P0104t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0104t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0104t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L0414t
		/// </summary>
		virtual public System.Int32? L0414t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0414t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L0414t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P0414t
		/// </summary>
		virtual public System.Int32? P0414t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0414t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P0414t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L1424t
		/// </summary>
		virtual public System.Int32? L1424t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L1424t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L1424t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P1424t
		/// </summary>
		virtual public System.Int32? P1424t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P1424t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P1424t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L2444t
		/// </summary>
		virtual public System.Int32? L2444t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L2444t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L2444t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P2444t
		/// </summary>
		virtual public System.Int32? P2444t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P2444t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P2444t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L4464t
		/// </summary>
		virtual public System.Int32? L4464t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L4464t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L4464t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P4464t
		/// </summary>
		virtual public System.Int32? P4464t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P4464t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P4464t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.L64t
		/// </summary>
		virtual public System.Int32? L64t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.L64t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.L64t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.P64t
		/// </summary>
		virtual public System.Int32? P64t
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.P64t);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.P64t, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.TotalL
		/// </summary>
		virtual public System.Int32? TotalL
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.TotalL);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.TotalL, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.TotalP
		/// </summary>
		virtual public System.Int32? TotalP
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.TotalP);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.TotalP, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.Total
		/// </summary>
		virtual public System.Int32? Total
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.Total);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.Total, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.TotalMati
		/// </summary>
		virtual public System.Int32? TotalMati
		{
			get
			{
				return base.GetSystemInt32(RlTxReport4AMetadata.ColumnNames.TotalMati);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport4AMetadata.ColumnNames.TotalMati, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport4AMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport4AMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4A.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport4AMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport4AMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport4A entity)
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
				
			public System.String TotalL
			{
				get
				{
					System.Int32? data = entity.TotalL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalL = null;
					else entity.TotalL = Convert.ToInt32(value);
				}
			}
				
			public System.String TotalP
			{
				get
				{
					System.Int32? data = entity.TotalP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalP = null;
					else entity.TotalP = Convert.ToInt32(value);
				}
			}
				
			public System.String Total
			{
				get
				{
					System.Int32? data = entity.Total;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Total = null;
					else entity.Total = Convert.ToInt32(value);
				}
			}
				
			public System.String TotalMati
			{
				get
				{
					System.Int32? data = entity.TotalMati;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalMati = null;
					else entity.TotalMati = Convert.ToInt32(value);
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
			

			private esRlTxReport4A entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport4AQuery query)
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
				throw new Exception("esRlTxReport4A can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport4A : esRlTxReport4A
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
	abstract public class esRlTxReport4AQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport4AMetadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0006h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L0006h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0006h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P0006h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0628h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L0628h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0628h
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P0628h, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L28h01t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L28h01t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P28h01t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P28h01t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0104t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L0104t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0104t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P0104t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L0414t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L0414t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P0414t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P0414t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L1424t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L1424t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P1424t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P1424t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L2444t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L2444t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P2444t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P2444t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L4464t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L4464t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P4464t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P4464t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem L64t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.L64t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem P64t
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.P64t, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TotalL
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.TotalL, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TotalP
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.TotalP, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Total
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.Total, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TotalMati
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.TotalMati, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport4AMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport4ACollection")]
	public partial class RlTxReport4ACollection : esRlTxReport4ACollection, IEnumerable<RlTxReport4A>
	{
		public RlTxReport4ACollection()
		{

		}
		
		public static implicit operator List<RlTxReport4A>(RlTxReport4ACollection coll)
		{
			List<RlTxReport4A> list = new List<RlTxReport4A>();
			
			foreach (RlTxReport4A emp in coll)
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
				return  RlTxReport4AMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport4AQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport4A(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport4A();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport4AQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport4AQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport4AQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport4A AddNew()
		{
			RlTxReport4A entity = base.AddNewEntity() as RlTxReport4A;
			
			return entity;
		}

		public RlTxReport4A FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport4A;
		}


		#region IEnumerable<RlTxReport4A> Members

		IEnumerator<RlTxReport4A> IEnumerable<RlTxReport4A>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport4A;
			}
		}

		#endregion
		
		private RlTxReport4AQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport4A' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport4A ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport4A : esRlTxReport4A
	{
		public RlTxReport4A()
		{

		}
	
		public RlTxReport4A(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport4AMetadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport4AQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport4AQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport4AQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport4AQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport4AQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport4AQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport4AQuery : esRlTxReport4AQuery
	{
		public RlTxReport4AQuery()
		{

		}		
		
		public RlTxReport4AQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport4AQuery";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport4AMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport4AMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L0006h, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L0006h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P0006h, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P0006h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L0628h, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L0628h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P0628h, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P0628h;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L28h01t, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L28h01t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P28h01t, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P28h01t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L0104t, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L0104t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P0104t, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P0104t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L0414t, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L0414t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P0414t, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P0414t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L1424t, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L1424t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P1424t, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P1424t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L2444t, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L2444t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P2444t, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P2444t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L4464t, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L4464t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P4464t, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P4464t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.L64t, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.L64t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.P64t, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.P64t;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.TotalL, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.TotalL;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.TotalP, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.TotalP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.Total, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.Total;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.TotalMati, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.TotalMati;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.LastUpdateDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport4AMetadata.ColumnNames.LastUpdateByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport4AMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport4AMetadata Meta()
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
			 public const string TotalL = "TotalL";
			 public const string TotalP = "TotalP";
			 public const string Total = "Total";
			 public const string TotalMati = "TotalMati";
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
			 public const string TotalL = "TotalL";
			 public const string TotalP = "TotalP";
			 public const string Total = "Total";
			 public const string TotalMati = "TotalMati";
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
			lock (typeof(RlTxReport4AMetadata))
			{
				if(RlTxReport4AMetadata.mapDelegates == null)
				{
					RlTxReport4AMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport4AMetadata.meta == null)
				{
					RlTxReport4AMetadata.meta = new RlTxReport4AMetadata();
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
				meta.AddTypeMap("TotalL", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Total", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalMati", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport4A";
				meta.Destination = "RlTxReport4A";
				
				meta.spInsert = "proc_RlTxReport4AInsert";				
				meta.spUpdate = "proc_RlTxReport4AUpdate";		
				meta.spDelete = "proc_RlTxReport4ADelete";
				meta.spLoadAll = "proc_RlTxReport4ALoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport4ALoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport4AMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
