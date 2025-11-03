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
	abstract public class esRlTxReport34Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport34Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport34Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport34Query query)
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
			this.InitQuery(query as esRlTxReport34Query);
		}
		#endregion
		
		virtual public RlTxReport34 DetachEntity(RlTxReport34 entity)
		{
			return base.DetachEntity(entity) as RlTxReport34;
		}
		
		virtual public RlTxReport34 AttachEntity(RlTxReport34 entity)
		{
			return base.AttachEntity(entity) as RlTxReport34;
		}
		
		virtual public void Combine(RlTxReport34Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport34 this[int index]
		{
			get
			{
				return base[index] as RlTxReport34;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport34);
		}
	}



	[Serializable]
	abstract public class esRlTxReport34 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport34Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport34()
		{

		}

		public esRlTxReport34(DataRow row)
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
			esRlTxReport34Query query = this.GetDynamicQuery();
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
						case "RmRumahSakit": this.str.RmRumahSakit = (string)value; break;							
						case "RmBidan": this.str.RmBidan = (string)value; break;							
						case "RmPuskesmas": this.str.RmPuskesmas = (string)value; break;							
						case "RmFasKesLain": this.str.RmFasKesLain = (string)value; break;							
						case "RmHidup": this.str.RmHidup = (string)value; break;							
						case "RmMati": this.str.RmMati = (string)value; break;							
						case "RmTotal": this.str.RmTotal = (string)value; break;							
						case "RnmHidup": this.str.RnmHidup = (string)value; break;							
						case "RnmMati": this.str.RnmMati = (string)value; break;							
						case "RnmTotal": this.str.RnmTotal = (string)value; break;							
						case "NrHidup": this.str.NrHidup = (string)value; break;							
						case "NrMati": this.str.NrMati = (string)value; break;							
						case "NrTotal": this.str.NrTotal = (string)value; break;							
						case "DiRujuk": this.str.DiRujuk = (string)value; break;							
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
						
						case "RmRumahSakit":
						
							if (value == null || value is System.Int32)
								this.RmRumahSakit = (System.Int32?)value;
							break;
						
						case "RmBidan":
						
							if (value == null || value is System.Int32)
								this.RmBidan = (System.Int32?)value;
							break;
						
						case "RmPuskesmas":
						
							if (value == null || value is System.Int32)
								this.RmPuskesmas = (System.Int32?)value;
							break;
						
						case "RmFasKesLain":
						
							if (value == null || value is System.Int32)
								this.RmFasKesLain = (System.Int32?)value;
							break;
						
						case "RmHidup":
						
							if (value == null || value is System.Int32)
								this.RmHidup = (System.Int32?)value;
							break;
						
						case "RmMati":
						
							if (value == null || value is System.Int32)
								this.RmMati = (System.Int32?)value;
							break;
						
						case "RmTotal":
						
							if (value == null || value is System.Int32)
								this.RmTotal = (System.Int32?)value;
							break;
						
						case "RnmHidup":
						
							if (value == null || value is System.Int32)
								this.RnmHidup = (System.Int32?)value;
							break;
						
						case "RnmMati":
						
							if (value == null || value is System.Int32)
								this.RnmMati = (System.Int32?)value;
							break;
						
						case "RnmTotal":
						
							if (value == null || value is System.Int32)
								this.RnmTotal = (System.Int32?)value;
							break;
						
						case "NrHidup":
						
							if (value == null || value is System.Int32)
								this.NrHidup = (System.Int32?)value;
							break;
						
						case "NrMati":
						
							if (value == null || value is System.Int32)
								this.NrMati = (System.Int32?)value;
							break;
						
						case "NrTotal":
						
							if (value == null || value is System.Int32)
								this.NrTotal = (System.Int32?)value;
							break;
						
						case "DiRujuk":
						
							if (value == null || value is System.Int32)
								this.DiRujuk = (System.Int32?)value;
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
		/// Maps to RlTxReport3_4.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport34Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport34Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RmRumahSakit
		/// </summary>
		virtual public System.Int32? RmRumahSakit
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RmRumahSakit);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RmRumahSakit, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RmBidan
		/// </summary>
		virtual public System.Int32? RmBidan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RmBidan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RmBidan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RmPuskesmas
		/// </summary>
		virtual public System.Int32? RmPuskesmas
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RmPuskesmas);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RmPuskesmas, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RmFasKesLain
		/// </summary>
		virtual public System.Int32? RmFasKesLain
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RmFasKesLain);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RmFasKesLain, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RmHidup
		/// </summary>
		virtual public System.Int32? RmHidup
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RmHidup);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RmHidup, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RmMati
		/// </summary>
		virtual public System.Int32? RmMati
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RmMati);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RmMati, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RmTotal
		/// </summary>
		virtual public System.Int32? RmTotal
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RmTotal);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RmTotal, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RnmHidup
		/// </summary>
		virtual public System.Int32? RnmHidup
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RnmHidup);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RnmHidup, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RnmMati
		/// </summary>
		virtual public System.Int32? RnmMati
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RnmMati);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RnmMati, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.RnmTotal
		/// </summary>
		virtual public System.Int32? RnmTotal
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.RnmTotal);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.RnmTotal, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.NrHidup
		/// </summary>
		virtual public System.Int32? NrHidup
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.NrHidup);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.NrHidup, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.NrMati
		/// </summary>
		virtual public System.Int32? NrMati
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.NrMati);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.NrMati, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.NrTotal
		/// </summary>
		virtual public System.Int32? NrTotal
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.NrTotal);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.NrTotal, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.DiRujuk
		/// </summary>
		virtual public System.Int32? DiRujuk
		{
			get
			{
				return base.GetSystemInt32(RlTxReport34Metadata.ColumnNames.DiRujuk);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport34Metadata.ColumnNames.DiRujuk, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport34Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport34Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_4.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport34Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport34Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport34 entity)
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
				
			public System.String RmRumahSakit
			{
				get
				{
					System.Int32? data = entity.RmRumahSakit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmRumahSakit = null;
					else entity.RmRumahSakit = Convert.ToInt32(value);
				}
			}
				
			public System.String RmBidan
			{
				get
				{
					System.Int32? data = entity.RmBidan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmBidan = null;
					else entity.RmBidan = Convert.ToInt32(value);
				}
			}
				
			public System.String RmPuskesmas
			{
				get
				{
					System.Int32? data = entity.RmPuskesmas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmPuskesmas = null;
					else entity.RmPuskesmas = Convert.ToInt32(value);
				}
			}
				
			public System.String RmFasKesLain
			{
				get
				{
					System.Int32? data = entity.RmFasKesLain;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmFasKesLain = null;
					else entity.RmFasKesLain = Convert.ToInt32(value);
				}
			}
				
			public System.String RmHidup
			{
				get
				{
					System.Int32? data = entity.RmHidup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmHidup = null;
					else entity.RmHidup = Convert.ToInt32(value);
				}
			}
				
			public System.String RmMati
			{
				get
				{
					System.Int32? data = entity.RmMati;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmMati = null;
					else entity.RmMati = Convert.ToInt32(value);
				}
			}
				
			public System.String RmTotal
			{
				get
				{
					System.Int32? data = entity.RmTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RmTotal = null;
					else entity.RmTotal = Convert.ToInt32(value);
				}
			}
				
			public System.String RnmHidup
			{
				get
				{
					System.Int32? data = entity.RnmHidup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RnmHidup = null;
					else entity.RnmHidup = Convert.ToInt32(value);
				}
			}
				
			public System.String RnmMati
			{
				get
				{
					System.Int32? data = entity.RnmMati;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RnmMati = null;
					else entity.RnmMati = Convert.ToInt32(value);
				}
			}
				
			public System.String RnmTotal
			{
				get
				{
					System.Int32? data = entity.RnmTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RnmTotal = null;
					else entity.RnmTotal = Convert.ToInt32(value);
				}
			}
				
			public System.String NrHidup
			{
				get
				{
					System.Int32? data = entity.NrHidup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NrHidup = null;
					else entity.NrHidup = Convert.ToInt32(value);
				}
			}
				
			public System.String NrMati
			{
				get
				{
					System.Int32? data = entity.NrMati;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NrMati = null;
					else entity.NrMati = Convert.ToInt32(value);
				}
			}
				
			public System.String NrTotal
			{
				get
				{
					System.Int32? data = entity.NrTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NrTotal = null;
					else entity.NrTotal = Convert.ToInt32(value);
				}
			}
				
			public System.String DiRujuk
			{
				get
				{
					System.Int32? data = entity.DiRujuk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiRujuk = null;
					else entity.DiRujuk = Convert.ToInt32(value);
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
			

			private esRlTxReport34 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport34Query query)
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
				throw new Exception("esRlTxReport34 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport34 : esRlTxReport34
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
	abstract public class esRlTxReport34Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport34Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RmRumahSakit
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RmRumahSakit, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RmBidan
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RmBidan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RmPuskesmas
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RmPuskesmas, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RmFasKesLain
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RmFasKesLain, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RmHidup
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RmHidup, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RmMati
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RmMati, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RmTotal
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RmTotal, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RnmHidup
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RnmHidup, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RnmMati
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RnmMati, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RnmTotal
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.RnmTotal, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NrHidup
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.NrHidup, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NrMati
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.NrMati, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NrTotal
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.NrTotal, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DiRujuk
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.DiRujuk, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport34Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport34Collection")]
	public partial class RlTxReport34Collection : esRlTxReport34Collection, IEnumerable<RlTxReport34>
	{
		public RlTxReport34Collection()
		{

		}
		
		public static implicit operator List<RlTxReport34>(RlTxReport34Collection coll)
		{
			List<RlTxReport34> list = new List<RlTxReport34>();
			
			foreach (RlTxReport34 emp in coll)
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
				return  RlTxReport34Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport34Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport34(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport34();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport34Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport34Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport34Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport34 AddNew()
		{
			RlTxReport34 entity = base.AddNewEntity() as RlTxReport34;
			
			return entity;
		}

		public RlTxReport34 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport34;
		}


		#region IEnumerable<RlTxReport34> Members

		IEnumerator<RlTxReport34> IEnumerable<RlTxReport34>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport34;
			}
		}

		#endregion
		
		private RlTxReport34Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_4' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport34 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport34 : esRlTxReport34
	{
		public RlTxReport34()
		{

		}
	
		public RlTxReport34(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport34Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport34Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport34Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport34Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport34Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport34Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport34Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport34Query : esRlTxReport34Query
	{
		public RlTxReport34Query()
		{

		}		
		
		public RlTxReport34Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport34Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport34Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport34Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RmRumahSakit, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RmRumahSakit;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RmBidan, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RmBidan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RmPuskesmas, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RmPuskesmas;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RmFasKesLain, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RmFasKesLain;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RmHidup, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RmHidup;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RmMati, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RmMati;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RmTotal, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RmTotal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RnmHidup, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RnmHidup;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RnmMati, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RnmMati;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.RnmTotal, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.RnmTotal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.NrHidup, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.NrHidup;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.NrMati, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.NrMati;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.NrTotal, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.NrTotal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.DiRujuk, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.DiRujuk;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport34Metadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport34Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport34Metadata Meta()
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
			 public const string RmRumahSakit = "RmRumahSakit";
			 public const string RmBidan = "RmBidan";
			 public const string RmPuskesmas = "RmPuskesmas";
			 public const string RmFasKesLain = "RmFasKesLain";
			 public const string RmHidup = "RmHidup";
			 public const string RmMati = "RmMati";
			 public const string RmTotal = "RmTotal";
			 public const string RnmHidup = "RnmHidup";
			 public const string RnmMati = "RnmMati";
			 public const string RnmTotal = "RnmTotal";
			 public const string NrHidup = "NrHidup";
			 public const string NrMati = "NrMati";
			 public const string NrTotal = "NrTotal";
			 public const string DiRujuk = "DiRujuk";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string RmRumahSakit = "RmRumahSakit";
			 public const string RmBidan = "RmBidan";
			 public const string RmPuskesmas = "RmPuskesmas";
			 public const string RmFasKesLain = "RmFasKesLain";
			 public const string RmHidup = "RmHidup";
			 public const string RmMati = "RmMati";
			 public const string RmTotal = "RmTotal";
			 public const string RnmHidup = "RnmHidup";
			 public const string RnmMati = "RnmMati";
			 public const string RnmTotal = "RnmTotal";
			 public const string NrHidup = "NrHidup";
			 public const string NrMati = "NrMati";
			 public const string NrTotal = "NrTotal";
			 public const string DiRujuk = "DiRujuk";
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
			lock (typeof(RlTxReport34Metadata))
			{
				if(RlTxReport34Metadata.mapDelegates == null)
				{
					RlTxReport34Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport34Metadata.meta == null)
				{
					RlTxReport34Metadata.meta = new RlTxReport34Metadata();
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
				meta.AddTypeMap("RmRumahSakit", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RmBidan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RmPuskesmas", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RmFasKesLain", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RmHidup", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RmMati", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RmTotal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RnmHidup", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RnmMati", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RnmTotal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NrHidup", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NrMati", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NrTotal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DiRujuk", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_4";
				meta.Destination = "RlTxReport3_4";
				
				meta.spInsert = "proc_RlTxReport3_4Insert";				
				meta.spUpdate = "proc_RlTxReport3_4Update";		
				meta.spDelete = "proc_RlTxReport3_4Delete";
				meta.spLoadAll = "proc_RlTxReport3_4LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_4LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport34Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
