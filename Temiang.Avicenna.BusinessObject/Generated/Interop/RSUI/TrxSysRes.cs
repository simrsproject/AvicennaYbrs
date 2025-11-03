/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/14/2014 4:21:04 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.RSUI
{

	[Serializable]
	abstract public class esTrxSysResCollection : esEntityCollectionWAuditLog
	{
		public esTrxSysResCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TrxSysResCollection";
		}

		#region Query Logic
		protected void InitQuery(esTrxSysResQuery query)
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
			this.InitQuery(query as esTrxSysResQuery);
		}
		#endregion
		
		virtual public TrxSysRes DetachEntity(TrxSysRes entity)
		{
			return base.DetachEntity(entity) as TrxSysRes;
		}
		
		virtual public TrxSysRes AttachEntity(TrxSysRes entity)
		{
			return base.AttachEntity(entity) as TrxSysRes;
		}
		
		virtual public void Combine(TrxSysResCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TrxSysRes this[int index]
		{
			get
			{
				return base[index] as TrxSysRes;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TrxSysRes);
		}
	}



	[Serializable]
	abstract public class esTrxSysRes : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTrxSysResQuery GetDynamicQuery()
		{
			return null;
		}

		public esTrxSysRes()
		{

		}

		public esTrxSysRes(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String ono)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ono);
			else
				return LoadByPrimaryKeyStoredProcedure(ono);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String ono)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ono);
			else
				return LoadByPrimaryKeyStoredProcedure(ono);
		}

		private bool LoadByPrimaryKeyDynamic(System.String ono)
		{
			esTrxSysResQuery query = this.GetDynamicQuery();
			query.Where(query.Ono == ono);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String ono)
		{
			esParameters parms = new esParameters();
			parms.Add("ONO",ono);
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
						case "Pid": this.str.Pid = (string)value; break;							
						case "Apid": this.str.Apid = (string)value; break;							
						case "Pname": this.str.Pname = (string)value; break;							
						case "Ono": this.str.Ono = (string)value; break;							
						case "Lno": this.str.Lno = (string)value; break;							
						case "RequestDt": this.str.RequestDt = (string)value; break;							
						case "SourceCd": this.str.SourceCd = (string)value; break;							
						case "SourceNm": this.str.SourceNm = (string)value; break;							
						case "ClinicianCd": this.str.ClinicianCd = (string)value; break;							
						case "ClinicianNm": this.str.ClinicianNm = (string)value; break;							
						case "Priority": this.str.Priority = (string)value; break;							
						case "Cmt": this.str.Cmt = (string)value; break;							
						case "Visitno": this.str.Visitno = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{

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
		/// Maps to TRX_SYS_RES.PID
		/// </summary>
		virtual public System.String Pid
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Pid);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Pid, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.APID
		/// </summary>
		virtual public System.String Apid
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Apid);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Apid, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.PNAME
		/// </summary>
		virtual public System.String Pname
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Pname);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Pname, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.ONO
		/// </summary>
		virtual public System.String Ono
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Ono);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Ono, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.LNO
		/// </summary>
		virtual public System.String Lno
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Lno);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Lno, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.REQUEST_DT
		/// </summary>
		virtual public System.String RequestDt
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.RequestDt);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.RequestDt, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.SOURCE_CD
		/// </summary>
		virtual public System.String SourceCd
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.SourceCd);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.SourceCd, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.SOURCE_NM
		/// </summary>
		virtual public System.String SourceNm
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.SourceNm);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.SourceNm, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.CLINICIAN_CD
		/// </summary>
		virtual public System.String ClinicianCd
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.ClinicianCd);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.ClinicianCd, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.CLINICIAN_NM
		/// </summary>
		virtual public System.String ClinicianNm
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.ClinicianNm);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.ClinicianNm, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.PRIORITY
		/// </summary>
		virtual public System.String Priority
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Priority);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Priority, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.CMT
		/// </summary>
		virtual public System.String Cmt
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Cmt);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Cmt, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES.VISITNO
		/// </summary>
		virtual public System.String Visitno
		{
			get
			{
				return base.GetSystemString(TrxSysResMetadata.ColumnNames.Visitno);
			}
			
			set
			{
				base.SetSystemString(TrxSysResMetadata.ColumnNames.Visitno, value);
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
			public esStrings(esTrxSysRes entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Pid
			{
				get
				{
					System.String data = entity.Pid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pid = null;
					else entity.Pid = Convert.ToString(value);
				}
			}
				
			public System.String Apid
			{
				get
				{
					System.String data = entity.Apid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Apid = null;
					else entity.Apid = Convert.ToString(value);
				}
			}
				
			public System.String Pname
			{
				get
				{
					System.String data = entity.Pname;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pname = null;
					else entity.Pname = Convert.ToString(value);
				}
			}
				
			public System.String Ono
			{
				get
				{
					System.String data = entity.Ono;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ono = null;
					else entity.Ono = Convert.ToString(value);
				}
			}
				
			public System.String Lno
			{
				get
				{
					System.String data = entity.Lno;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Lno = null;
					else entity.Lno = Convert.ToString(value);
				}
			}
				
			public System.String RequestDt
			{
				get
				{
					System.String data = entity.RequestDt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestDt = null;
					else entity.RequestDt = Convert.ToString(value);
				}
			}
				
			public System.String SourceCd
			{
				get
				{
					System.String data = entity.SourceCd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SourceCd = null;
					else entity.SourceCd = Convert.ToString(value);
				}
			}
				
			public System.String SourceNm
			{
				get
				{
					System.String data = entity.SourceNm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SourceNm = null;
					else entity.SourceNm = Convert.ToString(value);
				}
			}
				
			public System.String ClinicianCd
			{
				get
				{
					System.String data = entity.ClinicianCd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicianCd = null;
					else entity.ClinicianCd = Convert.ToString(value);
				}
			}
				
			public System.String ClinicianNm
			{
				get
				{
					System.String data = entity.ClinicianNm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClinicianNm = null;
					else entity.ClinicianNm = Convert.ToString(value);
				}
			}
				
			public System.String Priority
			{
				get
				{
					System.String data = entity.Priority;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Priority = null;
					else entity.Priority = Convert.ToString(value);
				}
			}
				
			public System.String Cmt
			{
				get
				{
					System.String data = entity.Cmt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cmt = null;
					else entity.Cmt = Convert.ToString(value);
				}
			}
				
			public System.String Visitno
			{
				get
				{
					System.String data = entity.Visitno;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Visitno = null;
					else entity.Visitno = Convert.ToString(value);
				}
			}
			

			private esTrxSysRes entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTrxSysResQuery query)
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
				throw new Exception("esTrxSysRes can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esTrxSysResQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TrxSysResMetadata.Meta();
			}
		}	
		

		public esQueryItem Pid
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Pid, esSystemType.String);
			}
		} 
		
		public esQueryItem Apid
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Apid, esSystemType.String);
			}
		} 
		
		public esQueryItem Pname
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Pname, esSystemType.String);
			}
		} 
		
		public esQueryItem Ono
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Ono, esSystemType.String);
			}
		} 
		
		public esQueryItem Lno
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Lno, esSystemType.String);
			}
		} 
		
		public esQueryItem RequestDt
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.RequestDt, esSystemType.String);
			}
		} 
		
		public esQueryItem SourceCd
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.SourceCd, esSystemType.String);
			}
		} 
		
		public esQueryItem SourceNm
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.SourceNm, esSystemType.String);
			}
		} 
		
		public esQueryItem ClinicianCd
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.ClinicianCd, esSystemType.String);
			}
		} 
		
		public esQueryItem ClinicianNm
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.ClinicianNm, esSystemType.String);
			}
		} 
		
		public esQueryItem Priority
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Priority, esSystemType.String);
			}
		} 
		
		public esQueryItem Cmt
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Cmt, esSystemType.String);
			}
		} 
		
		public esQueryItem Visitno
		{
			get
			{
				return new esQueryItem(this, TrxSysResMetadata.ColumnNames.Visitno, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TrxSysResCollection")]
	public partial class TrxSysResCollection : esTrxSysResCollection, IEnumerable<TrxSysRes>
	{
		public TrxSysResCollection()
		{

		}
		
		public static implicit operator List<TrxSysRes>(TrxSysResCollection coll)
		{
			List<TrxSysRes> list = new List<TrxSysRes>();
			
			foreach (TrxSysRes emp in coll)
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
				return  TrxSysResMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TrxSysResQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TrxSysRes(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TrxSysRes();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TrxSysResQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TrxSysResQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TrxSysResQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TrxSysRes AddNew()
		{
			TrxSysRes entity = base.AddNewEntity() as TrxSysRes;
			
			return entity;
		}

		public TrxSysRes FindByPrimaryKey(System.String ono)
		{
			return base.FindByPrimaryKey(ono) as TrxSysRes;
		}


		#region IEnumerable<TrxSysRes> Members

		IEnumerator<TrxSysRes> IEnumerable<TrxSysRes>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TrxSysRes;
			}
		}

		#endregion
		
		private TrxSysResQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TRX_SYS_RES' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TrxSysRes ({Ono})")]
	[Serializable]
	public partial class TrxSysRes : esTrxSysRes
	{
		public TrxSysRes()
		{

		}
	
		public TrxSysRes(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TrxSysResMetadata.Meta();
			}
		}
		
		
		
		override protected esTrxSysResQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TrxSysResQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TrxSysResQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TrxSysResQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TrxSysResQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TrxSysResQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TrxSysResQuery : esTrxSysResQuery
	{
		public TrxSysResQuery()
		{

		}		
		
		public TrxSysResQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TrxSysResQuery";
        }
		
			
	}


	[Serializable]
	public partial class TrxSysResMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TrxSysResMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Pid, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Pid;
			c.CharacterMaxLength = 13;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Apid, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Apid;
			c.CharacterMaxLength = 16;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Pname, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Pname;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Ono, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Ono;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Lno, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Lno;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.RequestDt, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.RequestDt;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.SourceCd, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.SourceCd;
			c.CharacterMaxLength = 6;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.SourceNm, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.SourceNm;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.ClinicianCd, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.ClinicianCd;
			c.CharacterMaxLength = 9;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.ClinicianNm, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.ClinicianNm;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Priority, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Priority;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Cmt, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Cmt;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResMetadata.ColumnNames.Visitno, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResMetadata.PropertyNames.Visitno;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TrxSysResMetadata Meta()
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
			 public const string Pid = "PID";
			 public const string Apid = "APID";
			 public const string Pname = "PNAME";
			 public const string Ono = "ONO";
			 public const string Lno = "LNO";
			 public const string RequestDt = "REQUEST_DT";
			 public const string SourceCd = "SOURCE_CD";
			 public const string SourceNm = "SOURCE_NM";
			 public const string ClinicianCd = "CLINICIAN_CD";
			 public const string ClinicianNm = "CLINICIAN_NM";
			 public const string Priority = "PRIORITY";
			 public const string Cmt = "CMT";
			 public const string Visitno = "VISITNO";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Pid = "Pid";
			 public const string Apid = "Apid";
			 public const string Pname = "Pname";
			 public const string Ono = "Ono";
			 public const string Lno = "Lno";
			 public const string RequestDt = "RequestDt";
			 public const string SourceCd = "SourceCd";
			 public const string SourceNm = "SourceNm";
			 public const string ClinicianCd = "ClinicianCd";
			 public const string ClinicianNm = "ClinicianNm";
			 public const string Priority = "Priority";
			 public const string Cmt = "Cmt";
			 public const string Visitno = "Visitno";
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
			lock (typeof(TrxSysResMetadata))
			{
				if(TrxSysResMetadata.mapDelegates == null)
				{
					TrxSysResMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TrxSysResMetadata.meta == null)
				{
					TrxSysResMetadata.meta = new TrxSysResMetadata();
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
				

				meta.AddTypeMap("Pid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Apid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pname", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Ono", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Lno", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestDt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SourceCd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SourceNm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicianCd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClinicianNm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Priority", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Cmt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Visitno", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TRX_SYS_RES";
				meta.Destination = "TRX_SYS_RES";
				
				meta.spInsert = "proc_TRX_SYS_RESInsert";				
				meta.spUpdate = "proc_TRX_SYS_RESUpdate";		
				meta.spDelete = "proc_TRX_SYS_RESDelete";
				meta.spLoadAll = "proc_TRX_SYS_RESLoadAll";
				meta.spLoadByPrimaryKey = "proc_TRX_SYS_RESLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TrxSysResMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
