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
	abstract public class esRlTxReportCollection : esEntityCollectionWAuditLog
	{
		public esRlTxReportCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReportCollection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReportQuery query)
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
			this.InitQuery(query as esRlTxReportQuery);
		}
		#endregion
		
		virtual public RlTxReport DetachEntity(RlTxReport entity)
		{
			return base.DetachEntity(entity) as RlTxReport;
		}
		
		virtual public RlTxReport AttachEntity(RlTxReport entity)
		{
			return base.AttachEntity(entity) as RlTxReport;
		}
		
		virtual public void Combine(RlTxReportCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport this[int index]
		{
			get
			{
				return base[index] as RlTxReport;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport);
		}
	}



	[Serializable]
	abstract public class esRlTxReport : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReportQuery GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport()
		{

		}

		public esRlTxReport(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String rlTxReportNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo)
		{
			esRlTxReportQuery query = this.GetDynamicQuery();
			query.Where(query.RlTxReportNo == rlTxReportNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RlTxReportNo",rlTxReportNo);
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
						case "RlMasterReportID": this.str.RlMasterReportID = (string)value; break;							
						case "PeriodYear": this.str.PeriodYear = (string)value; break;							
						case "PeriodMonthStart": this.str.PeriodMonthStart = (string)value; break;							
						case "PeriodMonthEnd": this.str.PeriodMonthEnd = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;							
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RlMasterReportID":
						
							if (value == null || value is System.Int32)
								this.RlMasterReportID = (System.Int32?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "ApprovedDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
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
		/// Maps to RlTxReport.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReportMetadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReportMetadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.RlMasterReportID
		/// </summary>
		virtual public System.Int32? RlMasterReportID
		{
			get
			{
				return base.GetSystemInt32(RlTxReportMetadata.ColumnNames.RlMasterReportID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReportMetadata.ColumnNames.RlMasterReportID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(RlTxReportMetadata.ColumnNames.PeriodYear);
			}
			
			set
			{
				base.SetSystemString(RlTxReportMetadata.ColumnNames.PeriodYear, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.PeriodMonthStart
		/// </summary>
		virtual public System.String PeriodMonthStart
		{
			get
			{
				return base.GetSystemString(RlTxReportMetadata.ColumnNames.PeriodMonthStart);
			}
			
			set
			{
				base.SetSystemString(RlTxReportMetadata.ColumnNames.PeriodMonthStart, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.PeriodMonthEnd
		/// </summary>
		virtual public System.String PeriodMonthEnd
		{
			get
			{
				return base.GetSystemString(RlTxReportMetadata.ColumnNames.PeriodMonthEnd);
			}
			
			set
			{
				base.SetSystemString(RlTxReportMetadata.ColumnNames.PeriodMonthEnd, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(RlTxReportMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(RlTxReportMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReportMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReportMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReportMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReportMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReportMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReportMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReportMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReportMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport entity)
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
				
			public System.String RlMasterReportID
			{
				get
				{
					System.Int32? data = entity.RlMasterReportID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlMasterReportID = null;
					else entity.RlMasterReportID = Convert.ToInt32(value);
				}
			}
				
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
				}
			}
				
			public System.String PeriodMonthStart
			{
				get
				{
					System.String data = entity.PeriodMonthStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonthStart = null;
					else entity.PeriodMonthStart = Convert.ToString(value);
				}
			}
				
			public System.String PeriodMonthEnd
			{
				get
				{
					System.String data = entity.PeriodMonthEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonthEnd = null;
					else entity.PeriodMonthEnd = Convert.ToString(value);
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
				
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
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
			

			private esRlTxReport entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReportQuery query)
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
				throw new Exception("esRlTxReport can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport : esRlTxReport
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
	abstract public class esRlTxReportQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReportMetadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportID
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.RlMasterReportID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodMonthStart
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.PeriodMonthStart, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodMonthEnd
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.PeriodMonthEnd, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReportMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReportCollection")]
	public partial class RlTxReportCollection : esRlTxReportCollection, IEnumerable<RlTxReport>
	{
		public RlTxReportCollection()
		{

		}
		
		public static implicit operator List<RlTxReport>(RlTxReportCollection coll)
		{
			List<RlTxReport> list = new List<RlTxReport>();
			
			foreach (RlTxReport emp in coll)
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
				return  RlTxReportMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReportQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport AddNew()
		{
			RlTxReport entity = base.AddNewEntity() as RlTxReport;
			
			return entity;
		}

		public RlTxReport FindByPrimaryKey(System.String rlTxReportNo)
		{
			return base.FindByPrimaryKey(rlTxReportNo) as RlTxReport;
		}


		#region IEnumerable<RlTxReport> Members

		IEnumerator<RlTxReport> IEnumerable<RlTxReport>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport;
			}
		}

		#endregion
		
		private RlTxReportQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport ({RlTxReportNo})")]
	[Serializable]
	public partial class RlTxReport : esRlTxReport
	{
		public RlTxReport()
		{

		}
	
		public RlTxReport(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReportMetadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReportQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReportQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReportQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReportQuery : esRlTxReportQuery
	{
		public RlTxReportQuery()
		{

		}		
		
		public RlTxReportQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReportQuery";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReportMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReportMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReportMetadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.RlMasterReportID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReportMetadata.PropertyNames.RlMasterReportID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.PeriodYear, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReportMetadata.PropertyNames.PeriodYear;
			c.CharacterMaxLength = 4;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.PeriodMonthStart, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReportMetadata.PropertyNames.PeriodMonthStart;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.PeriodMonthEnd, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReportMetadata.PropertyNames.PeriodMonthEnd;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.IsApproved, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RlTxReportMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.ApprovedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReportMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.ApprovedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReportMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReportMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReportMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReportMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReportMetadata Meta()
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
			 public const string RlMasterReportID = "RlMasterReportID";
			 public const string PeriodYear = "PeriodYear";
			 public const string PeriodMonthStart = "PeriodMonthStart";
			 public const string PeriodMonthEnd = "PeriodMonthEnd";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDateTime = "ApprovedDateTime";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportID = "RlMasterReportID";
			 public const string PeriodYear = "PeriodYear";
			 public const string PeriodMonthStart = "PeriodMonthStart";
			 public const string PeriodMonthEnd = "PeriodMonthEnd";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDateTime = "ApprovedDateTime";
			 public const string ApprovedByUserID = "ApprovedByUserID";
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
			lock (typeof(RlTxReportMetadata))
			{
				if(RlTxReportMetadata.mapDelegates == null)
				{
					RlTxReportMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReportMetadata.meta == null)
				{
					RlTxReportMetadata.meta = new RlTxReportMetadata();
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
				meta.AddTypeMap("RlMasterReportID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodMonthStart", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodMonthEnd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport";
				meta.Destination = "RlTxReport";
				
				meta.spInsert = "proc_RlTxReportInsert";				
				meta.spUpdate = "proc_RlTxReportUpdate";		
				meta.spDelete = "proc_RlTxReportDelete";
				meta.spLoadAll = "proc_RlTxReportLoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReportLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReportMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
