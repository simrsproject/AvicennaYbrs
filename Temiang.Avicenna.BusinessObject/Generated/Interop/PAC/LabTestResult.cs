/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/28/2012 2:29:50 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.PAC
{

	[Serializable]
	abstract public class esLabTestResultCollection : esEntityCollectionWAuditLog
	{
		public esLabTestResultCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LabTestResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esLabTestResultQuery query)
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
			this.InitQuery(query as esLabTestResultQuery);
		}
		#endregion
		
		virtual public LabTestResult DetachEntity(LabTestResult entity)
		{
			return base.DetachEntity(entity) as LabTestResult;
		}
		
		virtual public LabTestResult AttachEntity(LabTestResult entity)
		{
			return base.AttachEntity(entity) as LabTestResult;
		}
		
		virtual public void Combine(LabTestResultCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LabTestResult this[int index]
		{
			get
			{
				return base[index] as LabTestResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LabTestResult);
		}
	}



	[Serializable]
	abstract public class esLabTestResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLabTestResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esLabTestResult()
		{

		}

		public esLabTestResult(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String regRefNo, System.String rowid)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(regRefNo, rowid);
			else
				return LoadByPrimaryKeyStoredProcedure(regRefNo, rowid);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String regRefNo, System.String rowid)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(regRefNo, rowid);
			else
				return LoadByPrimaryKeyStoredProcedure(regRefNo, rowid);
		}

		private bool LoadByPrimaryKeyDynamic(System.String regRefNo, System.String rowid)
		{
			esLabTestResultQuery query = this.GetDynamicQuery();
			query.Where(query.RegRefNo == regRefNo, query.Rowid == rowid);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String regRefNo, System.String rowid)
		{
			esParameters parms = new esParameters();
			parms.Add("reg_ref_no",regRefNo);			parms.Add("rowid",rowid);
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
						case "RegRefNo": this.str.RegRefNo = (string)value; break;							
						case "Rowid": this.str.Rowid = (string)value; break;							
						case "RegNo": this.str.RegNo = (string)value; break;							
						case "TestId": this.str.TestId = (string)value; break;							
						case "TestName": this.str.TestName = (string)value; break;							
						case "RegTestCreateDate": this.str.RegTestCreateDate = (string)value; break;							
						case "InstrumentName": this.str.InstrumentName = (string)value; break;							
						case "InputDate": this.str.InputDate = (string)value; break;							
						case "Result": this.str.Result = (string)value; break;							
						case "TestUnitsName": this.str.TestUnitsName = (string)value; break;							
						case "ReferenceValue": this.str.ReferenceValue = (string)value; break;							
						case "TestFlagSign": this.str.TestFlagSign = (string)value; break;							
						case "ResultComment": this.str.ResultComment = (string)value; break;							
						case "AuthorizationDate": this.str.AuthorizationDate = (string)value; break;							
						case "AuthorizationName": this.str.AuthorizationName = (string)value; break;							
						case "Seq": this.str.Seq = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RegTestCreateDate":
						
							if (value == null || value is System.DateTime)
								this.RegTestCreateDate = (System.DateTime?)value;
							break;
						
						case "InputDate":
						
							if (value == null || value is System.DateTime)
								this.InputDate = (System.DateTime?)value;
							break;
						
						case "AuthorizationDate":
						
							if (value == null || value is System.DateTime)
								this.AuthorizationDate = (System.DateTime?)value;
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
		/// Nomor order HIS
		/// </summary>
		virtual public System.String RegRefNo
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.RegRefNo);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.RegRefNo, value);
			}
		}
		
		/// <summary>
		/// Row identifier
		/// </summary>
		virtual public System.String Rowid
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.Rowid);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.Rowid, value);
			}
		}
		
		/// <summary>
		/// Nomor order LIS
		/// </summary>
		virtual public System.String RegNo
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.RegNo);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.RegNo, value);
			}
		}
		
		/// <summary>
		/// Kode test
		/// </summary>
		virtual public System.String TestId
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.TestId);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.TestId, value);
			}
		}
		
		/// <summary>
		/// Nama test
		/// </summary>
		virtual public System.String TestName
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.TestName);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.TestName, value);
			}
		}
		
		/// <summary>
		/// Waktu tindakan
		/// </summary>
		virtual public System.DateTime? RegTestCreateDate
		{
			get
			{
				return base.GetSystemDateTime(LabTestResultMetadata.ColumnNames.RegTestCreateDate);
			}
			
			set
			{
				base.SetSystemDateTime(LabTestResultMetadata.ColumnNames.RegTestCreateDate, value);
			}
		}
		
		/// <summary>
		/// Nama instrumen/rujukan
		/// </summary>
		virtual public System.String InstrumentName
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.InstrumentName);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.InstrumentName, value);
			}
		}
		
		/// <summary>
		/// Tanggal Hasil
		/// </summary>
		virtual public System.DateTime? InputDate
		{
			get
			{
				return base.GetSystemDateTime(LabTestResultMetadata.ColumnNames.InputDate);
			}
			
			set
			{
				base.SetSystemDateTime(LabTestResultMetadata.ColumnNames.InputDate, value);
			}
		}
		
		/// <summary>
		/// Hasil
		/// </summary>
		virtual public System.String Result
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.Result);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.Result, value);
			}
		}
		
		/// <summary>
		/// Satuan
		/// </summary>
		virtual public System.String TestUnitsName
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.TestUnitsName);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.TestUnitsName, value);
			}
		}
		
		/// <summary>
		/// Nilai Normal/reference/rujukan
		/// </summary>
		virtual public System.String ReferenceValue
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.ReferenceValue);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.ReferenceValue, value);
			}
		}
		
		/// <summary>
		/// Flag nilai abnormal
		/// </summary>
		virtual public System.String TestFlagSign
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.TestFlagSign);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.TestFlagSign, value);
			}
		}
		
		/// <summary>
		/// Komentar hasil, duplo, dll
		/// </summary>
		virtual public System.String ResultComment
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.ResultComment);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.ResultComment, value);
			}
		}
		
		/// <summary>
		/// Tanggal otorisasi
		/// </summary>
		virtual public System.DateTime? AuthorizationDate
		{
			get
			{
				return base.GetSystemDateTime(LabTestResultMetadata.ColumnNames.AuthorizationDate);
			}
			
			set
			{
				base.SetSystemDateTime(LabTestResultMetadata.ColumnNames.AuthorizationDate, value);
			}
		}
		
		/// <summary>
		/// Nama user otorisasi
		/// </summary>
		virtual public System.String AuthorizationName
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.AuthorizationName);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.AuthorizationName, value);
			}
		}
		
		/// <summary>
		/// Result sequence (urutan)
		/// </summary>
		virtual public System.String Seq
		{
			get
			{
				return base.GetSystemString(LabTestResultMetadata.ColumnNames.Seq);
			}
			
			set
			{
				base.SetSystemString(LabTestResultMetadata.ColumnNames.Seq, value);
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
			public esStrings(esLabTestResult entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegRefNo
			{
				get
				{
					System.String data = entity.RegRefNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegRefNo = null;
					else entity.RegRefNo = Convert.ToString(value);
				}
			}
				
			public System.String Rowid
			{
				get
				{
					System.String data = entity.Rowid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rowid = null;
					else entity.Rowid = Convert.ToString(value);
				}
			}
				
			public System.String RegNo
			{
				get
				{
					System.String data = entity.RegNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegNo = null;
					else entity.RegNo = Convert.ToString(value);
				}
			}
				
			public System.String TestId
			{
				get
				{
					System.String data = entity.TestId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestId = null;
					else entity.TestId = Convert.ToString(value);
				}
			}
				
			public System.String TestName
			{
				get
				{
					System.String data = entity.TestName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestName = null;
					else entity.TestName = Convert.ToString(value);
				}
			}
				
			public System.String RegTestCreateDate
			{
				get
				{
					System.DateTime? data = entity.RegTestCreateDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegTestCreateDate = null;
					else entity.RegTestCreateDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String InstrumentName
			{
				get
				{
					System.String data = entity.InstrumentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstrumentName = null;
					else entity.InstrumentName = Convert.ToString(value);
				}
			}
				
			public System.String InputDate
			{
				get
				{
					System.DateTime? data = entity.InputDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InputDate = null;
					else entity.InputDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String Result
			{
				get
				{
					System.String data = entity.Result;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Result = null;
					else entity.Result = Convert.ToString(value);
				}
			}
				
			public System.String TestUnitsName
			{
				get
				{
					System.String data = entity.TestUnitsName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestUnitsName = null;
					else entity.TestUnitsName = Convert.ToString(value);
				}
			}
				
			public System.String ReferenceValue
			{
				get
				{
					System.String data = entity.ReferenceValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceValue = null;
					else entity.ReferenceValue = Convert.ToString(value);
				}
			}
				
			public System.String TestFlagSign
			{
				get
				{
					System.String data = entity.TestFlagSign;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestFlagSign = null;
					else entity.TestFlagSign = Convert.ToString(value);
				}
			}
				
			public System.String ResultComment
			{
				get
				{
					System.String data = entity.ResultComment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultComment = null;
					else entity.ResultComment = Convert.ToString(value);
				}
			}
				
			public System.String AuthorizationDate
			{
				get
				{
					System.DateTime? data = entity.AuthorizationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AuthorizationDate = null;
					else entity.AuthorizationDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String AuthorizationName
			{
				get
				{
					System.String data = entity.AuthorizationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AuthorizationName = null;
					else entity.AuthorizationName = Convert.ToString(value);
				}
			}
				
			public System.String Seq
			{
				get
				{
					System.String data = entity.Seq;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Seq = null;
					else entity.Seq = Convert.ToString(value);
				}
			}
			

			private esLabTestResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLabTestResultQuery query)
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
				throw new Exception("esLabTestResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LabTestResult : esLabTestResult
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
	abstract public class esLabTestResultQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LabTestResultMetadata.Meta();
			}
		}	
		

		public esQueryItem RegRefNo
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.RegRefNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Rowid
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.Rowid, esSystemType.String);
			}
		} 
		
		public esQueryItem RegNo
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.RegNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TestId
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.TestId, esSystemType.String);
			}
		} 
		
		public esQueryItem TestName
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.TestName, esSystemType.String);
			}
		} 
		
		public esQueryItem RegTestCreateDate
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.RegTestCreateDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem InstrumentName
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.InstrumentName, esSystemType.String);
			}
		} 
		
		public esQueryItem InputDate
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.InputDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Result
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.Result, esSystemType.String);
			}
		} 
		
		public esQueryItem TestUnitsName
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.TestUnitsName, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceValue
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.ReferenceValue, esSystemType.String);
			}
		} 
		
		public esQueryItem TestFlagSign
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.TestFlagSign, esSystemType.String);
			}
		} 
		
		public esQueryItem ResultComment
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.ResultComment, esSystemType.String);
			}
		} 
		
		public esQueryItem AuthorizationDate
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.AuthorizationDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AuthorizationName
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.AuthorizationName, esSystemType.String);
			}
		} 
		
		public esQueryItem Seq
		{
			get
			{
				return new esQueryItem(this, LabTestResultMetadata.ColumnNames.Seq, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LabTestResultCollection")]
	public partial class LabTestResultCollection : esLabTestResultCollection, IEnumerable<LabTestResult>
	{
		public LabTestResultCollection()
		{

		}
		
		public static implicit operator List<LabTestResult>(LabTestResultCollection coll)
		{
			List<LabTestResult> list = new List<LabTestResult>();
			
			foreach (LabTestResult emp in coll)
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
				return  LabTestResultMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabTestResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LabTestResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LabTestResult();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LabTestResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabTestResultQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LabTestResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LabTestResult AddNew()
		{
			LabTestResult entity = base.AddNewEntity() as LabTestResult;
			
			return entity;
		}

		public LabTestResult FindByPrimaryKey(System.String regRefNo, System.String rowid)
		{
			return base.FindByPrimaryKey(regRefNo, rowid) as LabTestResult;
		}


		#region IEnumerable<LabTestResult> Members

		IEnumerator<LabTestResult> IEnumerable<LabTestResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LabTestResult;
			}
		}

		#endregion
		
		private LabTestResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LabTestResult' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LabTestResult ({RegRefNo},{Rowid})")]
	[Serializable]
	public partial class LabTestResult : esLabTestResult
	{
		public LabTestResult()
		{

		}
	
		public LabTestResult(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LabTestResultMetadata.Meta();
			}
		}
		
		
		
		override protected esLabTestResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabTestResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LabTestResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabTestResultQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LabTestResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LabTestResultQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LabTestResultQuery : esLabTestResultQuery
	{
		public LabTestResultQuery()
		{

		}		
		
		public LabTestResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LabTestResultQuery";
        }
		
			
	}


	[Serializable]
	public partial class LabTestResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LabTestResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.RegRefNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.RegRefNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.Description = "Nomor order HIS";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.Rowid, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.Rowid;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.Description = "Row identifier";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.RegNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.RegNo;
			c.CharacterMaxLength = 12;
			c.Description = "Nomor order LIS";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.TestId, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.TestId;
			c.CharacterMaxLength = 8;
			c.Description = "Kode test";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.TestName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.TestName;
			c.CharacterMaxLength = 150;
			c.Description = "Nama test";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.RegTestCreateDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabTestResultMetadata.PropertyNames.RegTestCreateDate;
			c.Description = "Waktu tindakan";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.InstrumentName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.InstrumentName;
			c.CharacterMaxLength = 150;
			c.Description = "Nama instrumen/rujukan";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.InputDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabTestResultMetadata.PropertyNames.InputDate;
			c.Description = "Tanggal Hasil";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.Result, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.Result;
			c.CharacterMaxLength = 1000;
			c.Description = "Hasil";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.TestUnitsName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.TestUnitsName;
			c.CharacterMaxLength = 150;
			c.Description = "Satuan";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.ReferenceValue, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.ReferenceValue;
			c.CharacterMaxLength = 500;
			c.Description = "Nilai Normal/reference/rujukan";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.TestFlagSign, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.TestFlagSign;
			c.CharacterMaxLength = 2;
			c.Description = "Flag nilai abnormal";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.ResultComment, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.ResultComment;
			c.CharacterMaxLength = 1000;
			c.Description = "Komentar hasil, duplo, dll";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.AuthorizationDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LabTestResultMetadata.PropertyNames.AuthorizationDate;
			c.Description = "Tanggal otorisasi";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.AuthorizationName, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.AuthorizationName;
			c.CharacterMaxLength = 150;
			c.Description = "Nama user otorisasi";
			_columns.Add(c);
				
			c = new esColumnMetadata(LabTestResultMetadata.ColumnNames.Seq, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = LabTestResultMetadata.PropertyNames.Seq;
			c.CharacterMaxLength = 8;
			c.Description = "Result sequence (urutan)";
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LabTestResultMetadata Meta()
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
			 public const string RegRefNo = "reg_ref_no";
			 public const string Rowid = "rowid";
			 public const string RegNo = "reg_no";
			 public const string TestId = "test_id";
			 public const string TestName = "test_name";
			 public const string RegTestCreateDate = "reg_test_create_date";
			 public const string InstrumentName = "instrument_name";
			 public const string InputDate = "input_date";
			 public const string Result = "result";
			 public const string TestUnitsName = "test_units_name";
			 public const string ReferenceValue = "reference_value";
			 public const string TestFlagSign = "test_flag_sign";
			 public const string ResultComment = "result_comment";
			 public const string AuthorizationDate = "authorization_date";
			 public const string AuthorizationName = "authorization_name";
			 public const string Seq = "seq";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegRefNo = "RegRefNo";
			 public const string Rowid = "Rowid";
			 public const string RegNo = "RegNo";
			 public const string TestId = "TestId";
			 public const string TestName = "TestName";
			 public const string RegTestCreateDate = "RegTestCreateDate";
			 public const string InstrumentName = "InstrumentName";
			 public const string InputDate = "InputDate";
			 public const string Result = "Result";
			 public const string TestUnitsName = "TestUnitsName";
			 public const string ReferenceValue = "ReferenceValue";
			 public const string TestFlagSign = "TestFlagSign";
			 public const string ResultComment = "ResultComment";
			 public const string AuthorizationDate = "AuthorizationDate";
			 public const string AuthorizationName = "AuthorizationName";
			 public const string Seq = "Seq";
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
			lock (typeof(LabTestResultMetadata))
			{
				if(LabTestResultMetadata.mapDelegates == null)
				{
					LabTestResultMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LabTestResultMetadata.meta == null)
				{
					LabTestResultMetadata.meta = new LabTestResultMetadata();
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
				

				meta.AddTypeMap("RegRefNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Rowid", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("RegNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("RegTestCreateDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("InstrumentName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("InputDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Result", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TestUnitsName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ReferenceValue", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TestFlagSign", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ResultComment", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("AuthorizationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AuthorizationName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Seq", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "LabTestResult";
				meta.Destination = "LabTestResult";
				
				meta.spInsert = "proc_LabTestResultInsert";				
				meta.spUpdate = "proc_LabTestResultUpdate";		
				meta.spDelete = "proc_LabTestResultDelete";
				meta.spLoadAll = "proc_LabTestResultLoadAll";
				meta.spLoadByPrimaryKey = "proc_LabTestResultLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LabTestResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
