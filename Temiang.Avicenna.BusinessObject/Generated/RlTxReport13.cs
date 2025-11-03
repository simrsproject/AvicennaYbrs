/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:14 PM
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
	abstract public class esRlTxReport13Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport13Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport13Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport13Query query)
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
			this.InitQuery(query as esRlTxReport13Query);
		}
		#endregion
		
		virtual public RlTxReport13 DetachEntity(RlTxReport13 entity)
		{
			return base.DetachEntity(entity) as RlTxReport13;
		}
		
		virtual public RlTxReport13 AttachEntity(RlTxReport13 entity)
		{
			return base.AttachEntity(entity) as RlTxReport13;
		}
		
		virtual public void Combine(RlTxReport13Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport13 this[int index]
		{
			get
			{
				return base[index] as RlTxReport13;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport13);
		}
	}



	[Serializable]
	abstract public class esRlTxReport13 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport13Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport13()
		{

		}

		public esRlTxReport13(DataRow row)
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
			esRlTxReport13Query query = this.GetDynamicQuery();
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
						case "JumlahTt": this.str.JumlahTt = (string)value; break;							
						case "Vvip": this.str.Vvip = (string)value; break;							
						case "Vip": this.str.Vip = (string)value; break;							
						case "I": this.str.I = (string)value; break;							
						case "Ii": this.str.Ii = (string)value; break;							
						case "Iii": this.str.Iii = (string)value; break;							
						case "KelasKhusus": this.str.KelasKhusus = (string)value; break;							
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
						
						case "JumlahTt":
						
							if (value == null || value is System.Int32)
								this.JumlahTt = (System.Int32?)value;
							break;
						
						case "Vvip":
						
							if (value == null || value is System.Int32)
								this.Vvip = (System.Int32?)value;
							break;
						
						case "Vip":
						
							if (value == null || value is System.Int32)
								this.Vip = (System.Int32?)value;
							break;
						
						case "I":
						
							if (value == null || value is System.Int32)
								this.I = (System.Int32?)value;
							break;
						
						case "Ii":
						
							if (value == null || value is System.Int32)
								this.Ii = (System.Int32?)value;
							break;
						
						case "Iii":
						
							if (value == null || value is System.Int32)
								this.Iii = (System.Int32?)value;
							break;
						
						case "KelasKhusus":
						
							if (value == null || value is System.Int32)
								this.KelasKhusus = (System.Int32?)value;
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
		/// Maps to RlTxReport1_3.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport13Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport13Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.JumlahTt
		/// </summary>
		virtual public System.Int32? JumlahTt
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.JumlahTt);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.JumlahTt, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.Vvip
		/// </summary>
		virtual public System.Int32? Vvip
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.Vvip);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.Vvip, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.Vip
		/// </summary>
		virtual public System.Int32? Vip
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.Vip);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.Vip, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.I
		/// </summary>
		virtual public System.Int32? I
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.I);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.I, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.Ii
		/// </summary>
		virtual public System.Int32? Ii
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.Ii);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.Ii, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.Iii
		/// </summary>
		virtual public System.Int32? Iii
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.Iii);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.Iii, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.KelasKhusus
		/// </summary>
		virtual public System.Int32? KelasKhusus
		{
			get
			{
				return base.GetSystemInt32(RlTxReport13Metadata.ColumnNames.KelasKhusus);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport13Metadata.ColumnNames.KelasKhusus, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport13Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport13Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport1_3.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport13Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport13Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport13 entity)
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
				
			public System.String JumlahTt
			{
				get
				{
					System.Int32? data = entity.JumlahTt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JumlahTt = null;
					else entity.JumlahTt = Convert.ToInt32(value);
				}
			}
				
			public System.String Vvip
			{
				get
				{
					System.Int32? data = entity.Vvip;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Vvip = null;
					else entity.Vvip = Convert.ToInt32(value);
				}
			}
				
			public System.String Vip
			{
				get
				{
					System.Int32? data = entity.Vip;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Vip = null;
					else entity.Vip = Convert.ToInt32(value);
				}
			}
				
			public System.String I
			{
				get
				{
					System.Int32? data = entity.I;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.I = null;
					else entity.I = Convert.ToInt32(value);
				}
			}
				
			public System.String Ii
			{
				get
				{
					System.Int32? data = entity.Ii;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ii = null;
					else entity.Ii = Convert.ToInt32(value);
				}
			}
				
			public System.String Iii
			{
				get
				{
					System.Int32? data = entity.Iii;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Iii = null;
					else entity.Iii = Convert.ToInt32(value);
				}
			}
				
			public System.String KelasKhusus
			{
				get
				{
					System.Int32? data = entity.KelasKhusus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KelasKhusus = null;
					else entity.KelasKhusus = Convert.ToInt32(value);
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
			

			private esRlTxReport13 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport13Query query)
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
				throw new Exception("esRlTxReport13 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport13 : esRlTxReport13
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
	abstract public class esRlTxReport13Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport13Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JumlahTt
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.JumlahTt, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Vvip
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.Vvip, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Vip
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.Vip, esSystemType.Int32);
			}
		} 
		
		public esQueryItem I
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.I, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Ii
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.Ii, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Iii
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.Iii, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KelasKhusus
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.KelasKhusus, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport13Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport13Collection")]
	public partial class RlTxReport13Collection : esRlTxReport13Collection, IEnumerable<RlTxReport13>
	{
		public RlTxReport13Collection()
		{

		}
		
		public static implicit operator List<RlTxReport13>(RlTxReport13Collection coll)
		{
			List<RlTxReport13> list = new List<RlTxReport13>();
			
			foreach (RlTxReport13 emp in coll)
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
				return  RlTxReport13Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport13Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport13(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport13();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport13Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport13Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport13Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport13 AddNew()
		{
			RlTxReport13 entity = base.AddNewEntity() as RlTxReport13;
			
			return entity;
		}

		public RlTxReport13 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport13;
		}


		#region IEnumerable<RlTxReport13> Members

		IEnumerator<RlTxReport13> IEnumerable<RlTxReport13>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport13;
			}
		}

		#endregion
		
		private RlTxReport13Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport1_3' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport13 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport13 : esRlTxReport13
	{
		public RlTxReport13()
		{

		}
	
		public RlTxReport13(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport13Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport13Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport13Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport13Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport13Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport13Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport13Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport13Query : esRlTxReport13Query
	{
		public RlTxReport13Query()
		{

		}		
		
		public RlTxReport13Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport13Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport13Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport13Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.JumlahTt, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.JumlahTt;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.Vvip, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.Vvip;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.Vip, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.Vip;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.I, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.I;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.Ii, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.Ii;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.Iii, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.Iii;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.KelasKhusus, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.KelasKhusus;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport13Metadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport13Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport13Metadata Meta()
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
			 public const string JumlahTt = "JumlahTt";
			 public const string Vvip = "Vvip";
			 public const string Vip = "Vip";
			 public const string I = "I";
			 public const string Ii = "Ii";
			 public const string Iii = "Iii";
			 public const string KelasKhusus = "KelasKhusus";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string JumlahTt = "JumlahTt";
			 public const string Vvip = "Vvip";
			 public const string Vip = "Vip";
			 public const string I = "I";
			 public const string Ii = "Ii";
			 public const string Iii = "Iii";
			 public const string KelasKhusus = "KelasKhusus";
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
			lock (typeof(RlTxReport13Metadata))
			{
				if(RlTxReport13Metadata.mapDelegates == null)
				{
					RlTxReport13Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport13Metadata.meta == null)
				{
					RlTxReport13Metadata.meta = new RlTxReport13Metadata();
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
				meta.AddTypeMap("JumlahTt", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Vvip", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Vip", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("I", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Ii", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Iii", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KelasKhusus", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport1_3";
				meta.Destination = "RlTxReport1_3";
				
				meta.spInsert = "proc_RlTxReport1_3Insert";				
				meta.spUpdate = "proc_RlTxReport1_3Update";		
				meta.spDelete = "proc_RlTxReport1_3Delete";
				meta.spLoadAll = "proc_RlTxReport1_3LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport1_3LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport13Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
