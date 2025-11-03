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
	abstract public class esRlTxReport315Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport315Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport315Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport315Query query)
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
			this.InitQuery(query as esRlTxReport315Query);
		}
		#endregion
		
		virtual public RlTxReport315 DetachEntity(RlTxReport315 entity)
		{
			return base.DetachEntity(entity) as RlTxReport315;
		}
		
		virtual public RlTxReport315 AttachEntity(RlTxReport315 entity)
		{
			return base.AttachEntity(entity) as RlTxReport315;
		}
		
		virtual public void Combine(RlTxReport315Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport315 this[int index]
		{
			get
			{
				return base[index] as RlTxReport315;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport315);
		}
	}



	[Serializable]
	abstract public class esRlTxReport315 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport315Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport315()
		{

		}

		public esRlTxReport315(DataRow row)
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
			esRlTxReport315Query query = this.GetDynamicQuery();
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
						case "RiJpk": this.str.RiJpk = (string)value; break;							
						case "RiJld": this.str.RiJld = (string)value; break;							
						case "Rj": this.str.Rj = (string)value; break;							
						case "RjLab": this.str.RjLab = (string)value; break;							
						case "RjRad": this.str.RjRad = (string)value; break;							
						case "RjLl": this.str.RjLl = (string)value; break;							
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
						
						case "RiJpk":
						
							if (value == null || value is System.Int32)
								this.RiJpk = (System.Int32?)value;
							break;
						
						case "RiJld":
						
							if (value == null || value is System.Int32)
								this.RiJld = (System.Int32?)value;
							break;
						
						case "Rj":
						
							if (value == null || value is System.Int32)
								this.Rj = (System.Int32?)value;
							break;
						
						case "RjLab":
						
							if (value == null || value is System.Int32)
								this.RjLab = (System.Int32?)value;
							break;
						
						case "RjRad":
						
							if (value == null || value is System.Int32)
								this.RjRad = (System.Int32?)value;
							break;
						
						case "RjLl":
						
							if (value == null || value is System.Int32)
								this.RjLl = (System.Int32?)value;
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
		/// Maps to RlTxReport3_15.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport315Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport315Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport315Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport315Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.RiJpk
		/// </summary>
		virtual public System.Int32? RiJpk
		{
			get
			{
				return base.GetSystemInt32(RlTxReport315Metadata.ColumnNames.RiJpk);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport315Metadata.ColumnNames.RiJpk, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.RiJld
		/// </summary>
		virtual public System.Int32? RiJld
		{
			get
			{
				return base.GetSystemInt32(RlTxReport315Metadata.ColumnNames.RiJld);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport315Metadata.ColumnNames.RiJld, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.Rj
		/// </summary>
		virtual public System.Int32? Rj
		{
			get
			{
				return base.GetSystemInt32(RlTxReport315Metadata.ColumnNames.Rj);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport315Metadata.ColumnNames.Rj, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.RjLab
		/// </summary>
		virtual public System.Int32? RjLab
		{
			get
			{
				return base.GetSystemInt32(RlTxReport315Metadata.ColumnNames.RjLab);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport315Metadata.ColumnNames.RjLab, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.RjRad
		/// </summary>
		virtual public System.Int32? RjRad
		{
			get
			{
				return base.GetSystemInt32(RlTxReport315Metadata.ColumnNames.RjRad);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport315Metadata.ColumnNames.RjRad, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.RjLl
		/// </summary>
		virtual public System.Int32? RjLl
		{
			get
			{
				return base.GetSystemInt32(RlTxReport315Metadata.ColumnNames.RjLl);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport315Metadata.ColumnNames.RjLl, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport315Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport315Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_15.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport315Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport315Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport315 entity)
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
				
			public System.String RiJpk
			{
				get
				{
					System.Int32? data = entity.RiJpk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiJpk = null;
					else entity.RiJpk = Convert.ToInt32(value);
				}
			}
				
			public System.String RiJld
			{
				get
				{
					System.Int32? data = entity.RiJld;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RiJld = null;
					else entity.RiJld = Convert.ToInt32(value);
				}
			}
				
			public System.String Rj
			{
				get
				{
					System.Int32? data = entity.Rj;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rj = null;
					else entity.Rj = Convert.ToInt32(value);
				}
			}
				
			public System.String RjLab
			{
				get
				{
					System.Int32? data = entity.RjLab;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RjLab = null;
					else entity.RjLab = Convert.ToInt32(value);
				}
			}
				
			public System.String RjRad
			{
				get
				{
					System.Int32? data = entity.RjRad;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RjRad = null;
					else entity.RjRad = Convert.ToInt32(value);
				}
			}
				
			public System.String RjLl
			{
				get
				{
					System.Int32? data = entity.RjLl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RjLl = null;
					else entity.RjLl = Convert.ToInt32(value);
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
			

			private esRlTxReport315 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport315Query query)
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
				throw new Exception("esRlTxReport315 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport315 : esRlTxReport315
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
	abstract public class esRlTxReport315Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport315Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RiJpk
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.RiJpk, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RiJld
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.RiJld, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Rj
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.Rj, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RjLab
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.RjLab, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RjRad
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.RjRad, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RjLl
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.RjLl, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport315Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport315Collection")]
	public partial class RlTxReport315Collection : esRlTxReport315Collection, IEnumerable<RlTxReport315>
	{
		public RlTxReport315Collection()
		{

		}
		
		public static implicit operator List<RlTxReport315>(RlTxReport315Collection coll)
		{
			List<RlTxReport315> list = new List<RlTxReport315>();
			
			foreach (RlTxReport315 emp in coll)
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
				return  RlTxReport315Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport315Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport315(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport315();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport315Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport315Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport315Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport315 AddNew()
		{
			RlTxReport315 entity = base.AddNewEntity() as RlTxReport315;
			
			return entity;
		}

		public RlTxReport315 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport315;
		}


		#region IEnumerable<RlTxReport315> Members

		IEnumerator<RlTxReport315> IEnumerable<RlTxReport315>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport315;
			}
		}

		#endregion
		
		private RlTxReport315Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_15' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport315 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport315 : esRlTxReport315
	{
		public RlTxReport315()
		{

		}
	
		public RlTxReport315(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport315Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport315Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport315Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport315Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport315Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport315Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport315Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport315Query : esRlTxReport315Query
	{
		public RlTxReport315Query()
		{

		}		
		
		public RlTxReport315Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport315Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport315Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport315Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.RiJpk, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.RiJpk;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.RiJld, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.RiJld;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.Rj, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.Rj;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.RjLab, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.RjLab;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.RjRad, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.RjRad;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.RjLl, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.RjLl;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport315Metadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport315Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport315Metadata Meta()
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
			 public const string RiJpk = "RiJpk";
			 public const string RiJld = "RiJld";
			 public const string Rj = "Rj";
			 public const string RjLab = "RjLab";
			 public const string RjRad = "RjRad";
			 public const string RjLl = "RjLl";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string RiJpk = "RiJpk";
			 public const string RiJld = "RiJld";
			 public const string Rj = "Rj";
			 public const string RjLab = "RjLab";
			 public const string RjRad = "RjRad";
			 public const string RjLl = "RjLl";
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
			lock (typeof(RlTxReport315Metadata))
			{
				if(RlTxReport315Metadata.mapDelegates == null)
				{
					RlTxReport315Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport315Metadata.meta == null)
				{
					RlTxReport315Metadata.meta = new RlTxReport315Metadata();
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
				meta.AddTypeMap("RiJpk", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RiJld", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Rj", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RjLab", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RjRad", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RjLl", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_15";
				meta.Destination = "RlTxReport3_15";
				
				meta.spInsert = "proc_RlTxReport3_15Insert";				
				meta.spUpdate = "proc_RlTxReport3_15Update";		
				meta.spDelete = "proc_RlTxReport3_15Delete";
				meta.spLoadAll = "proc_RlTxReport3_15LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_15LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport315Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
