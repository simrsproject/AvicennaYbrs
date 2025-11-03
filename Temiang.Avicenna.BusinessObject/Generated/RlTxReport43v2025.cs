/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:16 PM
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
	abstract public class esRlTxReport43V2025Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport43V2025Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport43V2025Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport43V2025Query query)
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
			this.InitQuery(query as esRlTxReport43V2025Query);
		}
		#endregion
		
		virtual public RlTxReport43V2025 DetachEntity(RlTxReport43V2025 entity)
		{
			return base.DetachEntity(entity) as RlTxReport43V2025;
		}
		
		virtual public RlTxReport43V2025 AttachEntity(RlTxReport43V2025 entity)
		{
			return base.AttachEntity(entity) as RlTxReport43V2025;
		}
		
		virtual public void Combine(RlTxReport43V2025Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport43V2025 this[int index]
		{
			get
			{
				return base[index] as RlTxReport43V2025;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport43V2025);
		}
	}



	[Serializable]
	abstract public class esRlTxReport43V2025 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport43V2025Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport43V2025()
		{

		}

		public esRlTxReport43V2025(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.String diagnosaID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, diagnosaID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, diagnosaID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.String diagnosaID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, diagnosaID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, diagnosaID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.String diagnosaID)
		{
			esRlTxReport43V2025Query query = this.GetDynamicQuery();
			query.Where(query.RlTxReportNo == rlTxReportNo, query.DiagnosaID == diagnosaID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.String diagnosaID)
		{
			esParameters parms = new esParameters();
			parms.Add("RlTxReportNo",rlTxReportNo);			parms.Add("DiagnosaID",diagnosaID);
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
						case "DiagnosaID": this.str.DiagnosaID = (string)value; break;							
						case "HidupMatiL": this.str.HidupMatiL = (string)value; break;							
						case "HidupMatiP": this.str.HidupMatiP = (string)value; break;
                        case "TotalHidupMati": this.str.TotalHidupMati = (string)value; break;
                        case "KeluarMatiL": this.str.KeluarMatiL = (string)value; break;							
						case "KeluarMatiP": this.str.KeluarMatiP = (string)value; break;							
						case "TotalKeluarMati": this.str.TotalKeluarMati = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "HidupMatiL":
						
							if (value == null || value is System.Int32)
								this.HidupMatiL = (System.Int32?)value;
							break;
						
						case "HidupMatiP":
						
							if (value == null || value is System.Int32)
								this.HidupMatiP = (System.Int32?)value;
							break;
						
						case "KeluarMatiL":
						
							if (value == null || value is System.Int32)
								this.KeluarMatiL = (System.Int32?)value;
							break;
						
						case "TotalHidupMati":
						
							if (value == null || value is System.Int32)
								this.TotalHidupMati = (System.Int32?)value;
							break;
						
						case "KeluarMatiP":
						
							if (value == null || value is System.Int32)
								this.KeluarMatiP = (System.Int32?)value;
							break;
						
						case "TotalKeluarMati":
						
							if (value == null || value is System.Int32)
								this.TotalKeluarMati = (System.Int32?)value;
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
		/// Maps to RlTxReport4_3.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport43V2025Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport43V2025Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4_3.DiagnosaID
		/// </summary>
		virtual public System.String DiagnosaID
		{
			get
			{
				return base.GetSystemString(RlTxReport43V2025Metadata.ColumnNames.DiagnosaID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport43V2025Metadata.ColumnNames.DiagnosaID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4_3.HidupMatiL
		/// </summary>
		virtual public System.Int32? HidupMatiL
		{
			get
			{
				return base.GetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.HidupMatiL);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.HidupMatiL, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4_3.HidupMatiP
		/// </summary>
		virtual public System.Int32? HidupMatiP
		{
			get
			{
				return base.GetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.HidupMatiP);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.HidupMatiP, value);
			}
        }

        /// <summary>
        /// Maps to RlTxReport4_3.TotalHidupMati
        /// </summary>
        virtual public System.Int32? TotalHidupMati
        {
            get
            {
                return base.GetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.TotalHidupMati);
            }

            set
            {
                base.SetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.TotalHidupMati, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport4_3.KeluarMatiL
        /// </summary>
        virtual public System.Int32? KeluarMatiL
		{
			get
			{
				return base.GetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.KeluarMatiL);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.KeluarMatiL, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4_3.KeluarMatiP
		/// </summary>
		virtual public System.Int32? KeluarMatiP
		{
			get
			{
				return base.GetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.KeluarMatiP);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.KeluarMatiP, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4_3.TotalKeluarMati
		/// </summary>
		virtual public System.Int32? TotalKeluarMati
		{
			get
			{
				return base.GetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.TotalKeluarMati);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport43V2025Metadata.ColumnNames.TotalKeluarMati, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4_3.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport43V2025Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport43V2025Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport4_3.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport43V2025Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport43V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport43V2025 entity)
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
				
			public System.String DiagnosaID
			{
				get
				{
					System.String data = entity.DiagnosaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnosaID = null;
					else entity.DiagnosaID = Convert.ToString(value);
				}
			}
				
			public System.String HidupMatiL
			{
				get
				{
					System.Int32? data = entity.HidupMatiL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HidupMatiL = null;
					else entity.HidupMatiL = Convert.ToInt32(value);
				}
			}
				
			public System.String HidupMatiP
			{
				get
				{
					System.Int32? data = entity.HidupMatiP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HidupMatiP = null;
					else entity.HidupMatiP = Convert.ToInt32(value);
				}
            }

            public System.String TotalHidupMati
            {
                get
                {
                    System.Int32? data = entity.TotalHidupMati;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TotalHidupMati = null;
                    else entity.TotalHidupMati = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMatiL
			{
				get
				{
					System.Int32? data = entity.KeluarMatiL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeluarMatiL = null;
					else entity.KeluarMatiL = Convert.ToInt32(value);
				}
			}
				
			public System.String KeluarMatiP
			{
				get
				{
					System.Int32? data = entity.KeluarMatiP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeluarMatiP = null;
					else entity.KeluarMatiP = Convert.ToInt32(value);
				}
			}
				
			public System.String TotalKeluarMati
			{
				get
				{
					System.Int32? data = entity.TotalKeluarMati;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalKeluarMati = null;
					else entity.TotalKeluarMati = Convert.ToInt32(value);
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
			

			private esRlTxReport43V2025 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport43V2025Query query)
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
				throw new Exception("esRlTxReport43V2025 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport43V2025 : esRlTxReport43V2025
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
	abstract public class esRlTxReport43V2025Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport43V2025Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnosaID
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.DiagnosaID, esSystemType.String);
			}
		} 
		
		public esQueryItem HidupMatiL
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.HidupMatiL, esSystemType.Int32);
			}
		} 
		
		public esQueryItem HidupMatiP
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.HidupMatiP, esSystemType.Int32);
			}
        }

        public esQueryItem TotalHidupMati
        {
            get
            {
                return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.TotalHidupMati, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMatiL
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.KeluarMatiL, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KeluarMatiP
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.KeluarMatiP, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TotalKeluarMati
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.TotalKeluarMati, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport43V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport43V2025Collection")]
	public partial class RlTxReport43V2025Collection : esRlTxReport43V2025Collection, IEnumerable<RlTxReport43V2025>
	{
		public RlTxReport43V2025Collection()
		{

		}
		
		public static implicit operator List<RlTxReport43V2025>(RlTxReport43V2025Collection coll)
		{
			List<RlTxReport43V2025> list = new List<RlTxReport43V2025>();
			
			foreach (RlTxReport43V2025 emp in coll)
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
				return  RlTxReport43V2025Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport43V2025Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport43V2025(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport43V2025();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport43V2025Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport43V2025Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport43V2025Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport43V2025 AddNew()
		{
			RlTxReport43V2025 entity = base.AddNewEntity() as RlTxReport43V2025;
			
			return entity;
		}

		public RlTxReport43V2025 FindByPrimaryKey(System.String rlTxReportNo, System.String diagnosaID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, diagnosaID) as RlTxReport43V2025;
		}


		#region IEnumerable<RlTxReport43V2025> Members

		IEnumerator<RlTxReport43V2025> IEnumerable<RlTxReport43V2025>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport43V2025;
			}
		}

		#endregion
		
		private RlTxReport43V2025Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport4_3' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport43V2025 ({RlTxReportNo},{DiagnosaID})")]
	[Serializable]
	public partial class RlTxReport43V2025 : esRlTxReport43V2025
	{
		public RlTxReport43V2025()
		{

		}
	
		public RlTxReport43V2025(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport43V2025Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport43V2025Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport43V2025Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport43V2025Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport43V2025Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport43V2025Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport43V2025Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport43V2025Query : esRlTxReport43V2025Query
	{
		public RlTxReport43V2025Query()
		{

		}		
		
		public RlTxReport43V2025Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport43V2025Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport43V2025Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport43V2025Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.DiagnosaID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.DiagnosaID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.HidupMatiL, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.HidupMatiL;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.HidupMatiP, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.HidupMatiP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.TotalHidupMati, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.TotalHidupMati;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.KeluarMatiL, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.KeluarMatiL;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.KeluarMatiP, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.KeluarMatiP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.TotalKeluarMati, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.TotalKeluarMati;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport43V2025Metadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport43V2025Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport43V2025Metadata Meta()
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
			 public const string DiagnosaID = "DiagnosaID";
			 public const string HidupMatiL = "HidupMatiL";
			 public const string HidupMatiP = "HidupMatiP";
			 public const string TotalHidupMati = "TotalHidupMati";
             public const string KeluarMatiL = "KeluarMatiL";
			 public const string KeluarMatiP = "KeluarMatiP";
			 public const string TotalKeluarMati = "TotalKeluarMati";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string DiagnosaID = "DiagnosaID";
			 public const string HidupMatiL = "HidupMatiL";
			 public const string HidupMatiP = "HidupMatiP";
             public const string TotalHidupMati = "TotalHidupMati";
             public const string KeluarMatiL = "KeluarMatiL";
			 public const string KeluarMatiP = "KeluarMatiP";
			 public const string TotalKeluarMati = "TotalKeluarMati";
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
			lock (typeof(RlTxReport43V2025Metadata))
			{
				if(RlTxReport43V2025Metadata.mapDelegates == null)
				{
					RlTxReport43V2025Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport43V2025Metadata.meta == null)
				{
					RlTxReport43V2025Metadata.meta = new RlTxReport43V2025Metadata();
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
				meta.AddTypeMap("DiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HidupMatiL", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HidupMatiP", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TotalHidupMati", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMatiL", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KeluarMatiP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TotalKeluarMati", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport4_3V2025";
				meta.Destination = "RlTxReport4_3V2025";
				
				meta.spInsert = "proc_RlTxReport4_3V2025Insert";				
				meta.spUpdate = "proc_RlTxReport4_3V2025Update";		
				meta.spDelete = "proc_RlTxReport4_3V2025Delete";
				meta.spLoadAll = "proc_RlTxReport4_3V2025LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport4_3V2025LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport43V2025Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
