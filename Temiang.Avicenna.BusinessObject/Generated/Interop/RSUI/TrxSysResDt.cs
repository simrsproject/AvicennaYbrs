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
	abstract public class esTrxSysResDtCollection : esEntityCollectionWAuditLog
	{
		public esTrxSysResDtCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TrxSysResDtCollection";
		}

		#region Query Logic
		protected void InitQuery(esTrxSysResDtQuery query)
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
			this.InitQuery(query as esTrxSysResDtQuery);
		}
		#endregion
		
		virtual public TrxSysResDt DetachEntity(TrxSysResDt entity)
		{
			return base.DetachEntity(entity) as TrxSysResDt;
		}
		
		virtual public TrxSysResDt AttachEntity(TrxSysResDt entity)
		{
			return base.AttachEntity(entity) as TrxSysResDt;
		}
		
		virtual public void Combine(TrxSysResDtCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TrxSysResDt this[int index]
		{
			get
			{
				return base[index] as TrxSysResDt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TrxSysResDt);
		}
	}



	[Serializable]
	abstract public class esTrxSysResDt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTrxSysResDtQuery GetDynamicQuery()
		{
			return null;
		}

		public esTrxSysResDt()
		{

		}

		public esTrxSysResDt(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String ono, System.String testCd)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ono, testCd);
			else
				return LoadByPrimaryKeyStoredProcedure(ono, testCd);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String ono, System.String testCd)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(ono, testCd);
			else
				return LoadByPrimaryKeyStoredProcedure(ono, testCd);
		}

		private bool LoadByPrimaryKeyDynamic(System.String ono, System.String testCd)
		{
			esTrxSysResDtQuery query = this.GetDynamicQuery();
			query.Where(query.Ono == ono, query.TestCd == testCd);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String ono, System.String testCd)
		{
			esParameters parms = new esParameters();
			parms.Add("ONO",ono);			parms.Add("TEST_CD",testCd);
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
						case "Ono": this.str.Ono = (string)value; break;							
						case "TestCd": this.str.TestCd = (string)value; break;							
						case "TestNm": this.str.TestNm = (string)value; break;							
						case "DataTyp": this.str.DataTyp = (string)value; break;							
						case "ResultValue": this.str.ResultValue = (string)value; break;							
						case "ResultFt": this.str.ResultFt = (string)value; break;							
						case "ResultFt1": this.str.ResultFt1 = (string)value; break;							
						case "Unit": this.str.Unit = (string)value; break;							
						case "Flag": this.str.Flag = (string)value; break;							
						case "RefRange": this.str.RefRange = (string)value; break;							
						case "Status": this.str.Status = (string)value; break;							
						case "TestComment": this.str.TestComment = (string)value; break;							
						case "ValidateBy": this.str.ValidateBy = (string)value; break;							
						case "ValidateOn": this.str.ValidateOn = (string)value; break;							
						case "DispSeq": this.str.DispSeq = (string)value; break;							
						case "OrderTestid": this.str.OrderTestid = (string)value; break;							
						case "OrderTestnm": this.str.OrderTestnm = (string)value; break;							
						case "TestGroup": this.str.TestGroup = (string)value; break;							
						case "ItemParent": this.str.ItemParent = (string)value; break;
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
		/// Maps to TRX_SYS_RES_DT.ONO
		/// </summary>
		virtual public System.String Ono
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.Ono);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.Ono, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.TEST_CD
		/// </summary>
		virtual public System.String TestCd
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.TestCd);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.TestCd, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.TEST_NM
		/// </summary>
		virtual public System.String TestNm
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.TestNm);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.TestNm, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.DATA_TYP
		/// </summary>
		virtual public System.String DataTyp
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.DataTyp);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.DataTyp, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.RESULT_VALUE
		/// </summary>
		virtual public System.String ResultValue
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.ResultValue);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.ResultValue, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.RESULT_FT
		/// </summary>
		virtual public System.String ResultFt
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.ResultFt);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.ResultFt, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.RESULT_FT1
		/// </summary>
		virtual public System.String ResultFt1
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.ResultFt1);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.ResultFt1, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.UNIT
		/// </summary>
		virtual public System.String Unit
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.Unit);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.Unit, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.FLAG
		/// </summary>
		virtual public System.String Flag
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.Flag);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.Flag, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.REF_RANGE
		/// </summary>
		virtual public System.String RefRange
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.RefRange);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.RefRange, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.STATUS
		/// </summary>
		virtual public System.String Status
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.Status);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.Status, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.TEST_COMMENT
		/// </summary>
		virtual public System.String TestComment
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.TestComment);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.TestComment, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.VALIDATE_BY
		/// </summary>
		virtual public System.String ValidateBy
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.ValidateBy);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.ValidateBy, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.VALIDATE_ON
		/// </summary>
		virtual public System.String ValidateOn
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.ValidateOn);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.ValidateOn, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.DISP_SEQ
		/// </summary>
		virtual public System.String DispSeq
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.DispSeq);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.DispSeq, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.ORDER_TESTID
		/// </summary>
		virtual public System.String OrderTestid
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.OrderTestid);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.OrderTestid, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.ORDER_TESTNM
		/// </summary>
		virtual public System.String OrderTestnm
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.OrderTestnm);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.OrderTestnm, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.TEST_GROUP
		/// </summary>
		virtual public System.String TestGroup
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.TestGroup);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.TestGroup, value);
			}
		}
		
		/// <summary>
		/// Maps to TRX_SYS_RES_DT.ITEM_PARENT
		/// </summary>
		virtual public System.String ItemParent
		{
			get
			{
				return base.GetSystemString(TrxSysResDtMetadata.ColumnNames.ItemParent);
			}
			
			set
			{
				base.SetSystemString(TrxSysResDtMetadata.ColumnNames.ItemParent, value);
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
			public esStrings(esTrxSysResDt entity)
			{
				this.entity = entity;
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
				
			public System.String TestCd
			{
				get
				{
					System.String data = entity.TestCd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestCd = null;
					else entity.TestCd = Convert.ToString(value);
				}
			}
				
			public System.String TestNm
			{
				get
				{
					System.String data = entity.TestNm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestNm = null;
					else entity.TestNm = Convert.ToString(value);
				}
			}
				
			public System.String DataTyp
			{
				get
				{
					System.String data = entity.DataTyp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DataTyp = null;
					else entity.DataTyp = Convert.ToString(value);
				}
			}
				
			public System.String ResultValue
			{
				get
				{
					System.String data = entity.ResultValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultValue = null;
					else entity.ResultValue = Convert.ToString(value);
				}
			}
				
			public System.String ResultFt
			{
				get
				{
					System.String data = entity.ResultFt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultFt = null;
					else entity.ResultFt = Convert.ToString(value);
				}
			}
				
			public System.String ResultFt1
			{
				get
				{
					System.String data = entity.ResultFt1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultFt1 = null;
					else entity.ResultFt1 = Convert.ToString(value);
				}
			}
				
			public System.String Unit
			{
				get
				{
					System.String data = entity.Unit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Unit = null;
					else entity.Unit = Convert.ToString(value);
				}
			}
				
			public System.String Flag
			{
				get
				{
					System.String data = entity.Flag;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Flag = null;
					else entity.Flag = Convert.ToString(value);
				}
			}
				
			public System.String RefRange
			{
				get
				{
					System.String data = entity.RefRange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RefRange = null;
					else entity.RefRange = Convert.ToString(value);
				}
			}
				
			public System.String Status
			{
				get
				{
					System.String data = entity.Status;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Status = null;
					else entity.Status = Convert.ToString(value);
				}
			}
				
			public System.String TestComment
			{
				get
				{
					System.String data = entity.TestComment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestComment = null;
					else entity.TestComment = Convert.ToString(value);
				}
			}
				
			public System.String ValidateBy
			{
				get
				{
					System.String data = entity.ValidateBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidateBy = null;
					else entity.ValidateBy = Convert.ToString(value);
				}
			}
				
			public System.String ValidateOn
			{
				get
				{
					System.String data = entity.ValidateOn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidateOn = null;
					else entity.ValidateOn = Convert.ToString(value);
				}
			}
				
			public System.String DispSeq
			{
				get
				{
					System.String data = entity.DispSeq;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DispSeq = null;
					else entity.DispSeq = Convert.ToString(value);
				}
			}
				
			public System.String OrderTestid
			{
				get
				{
					System.String data = entity.OrderTestid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderTestid = null;
					else entity.OrderTestid = Convert.ToString(value);
				}
			}
				
			public System.String OrderTestnm
			{
				get
				{
					System.String data = entity.OrderTestnm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderTestnm = null;
					else entity.OrderTestnm = Convert.ToString(value);
				}
			}
				
			public System.String TestGroup
			{
				get
				{
					System.String data = entity.TestGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestGroup = null;
					else entity.TestGroup = Convert.ToString(value);
				}
			}
				
			public System.String ItemParent
			{
				get
				{
					System.String data = entity.ItemParent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemParent = null;
					else entity.ItemParent = Convert.ToString(value);
				}
			}
			

			private esTrxSysResDt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTrxSysResDtQuery query)
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
				throw new Exception("esTrxSysResDt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esTrxSysResDtQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TrxSysResDtMetadata.Meta();
			}
		}	
		

		public esQueryItem Ono
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.Ono, esSystemType.String);
			}
		} 
		
		public esQueryItem TestCd
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.TestCd, esSystemType.String);
			}
		} 
		
		public esQueryItem TestNm
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.TestNm, esSystemType.String);
			}
		} 
		
		public esQueryItem DataTyp
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.DataTyp, esSystemType.String);
			}
		} 
		
		public esQueryItem ResultValue
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.ResultValue, esSystemType.String);
			}
		} 
		
		public esQueryItem ResultFt
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.ResultFt, esSystemType.String);
			}
		} 
		
		public esQueryItem ResultFt1
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.ResultFt1, esSystemType.String);
			}
		} 
		
		public esQueryItem Unit
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.Unit, esSystemType.String);
			}
		} 
		
		public esQueryItem Flag
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.Flag, esSystemType.String);
			}
		} 
		
		public esQueryItem RefRange
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.RefRange, esSystemType.String);
			}
		} 
		
		public esQueryItem Status
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.Status, esSystemType.String);
			}
		} 
		
		public esQueryItem TestComment
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.TestComment, esSystemType.String);
			}
		} 
		
		public esQueryItem ValidateBy
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.ValidateBy, esSystemType.String);
			}
		} 
		
		public esQueryItem ValidateOn
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.ValidateOn, esSystemType.String);
			}
		} 
		
		public esQueryItem DispSeq
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.DispSeq, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderTestid
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.OrderTestid, esSystemType.String);
			}
		} 
		
		public esQueryItem OrderTestnm
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.OrderTestnm, esSystemType.String);
			}
		} 
		
		public esQueryItem TestGroup
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.TestGroup, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemParent
		{
			get
			{
				return new esQueryItem(this, TrxSysResDtMetadata.ColumnNames.ItemParent, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TrxSysResDtCollection")]
	public partial class TrxSysResDtCollection : esTrxSysResDtCollection, IEnumerable<TrxSysResDt>
	{
		public TrxSysResDtCollection()
		{

		}
		
		public static implicit operator List<TrxSysResDt>(TrxSysResDtCollection coll)
		{
			List<TrxSysResDt> list = new List<TrxSysResDt>();
			
			foreach (TrxSysResDt emp in coll)
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
				return  TrxSysResDtMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TrxSysResDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TrxSysResDt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TrxSysResDt();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TrxSysResDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TrxSysResDtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TrxSysResDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TrxSysResDt AddNew()
		{
			TrxSysResDt entity = base.AddNewEntity() as TrxSysResDt;
			
			return entity;
		}

		public TrxSysResDt FindByPrimaryKey(System.String ono, System.String testCd)
		{
			return base.FindByPrimaryKey(ono, testCd) as TrxSysResDt;
		}


		#region IEnumerable<TrxSysResDt> Members

		IEnumerator<TrxSysResDt> IEnumerable<TrxSysResDt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TrxSysResDt;
			}
		}

		#endregion
		
		private TrxSysResDtQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TRX_SYS_RES_DT' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TrxSysResDt ({Ono},{TestCd})")]
	[Serializable]
	public partial class TrxSysResDt : esTrxSysResDt
	{
		public TrxSysResDt()
		{

		}
	
		public TrxSysResDt(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TrxSysResDtMetadata.Meta();
			}
		}
		
		
		
		override protected esTrxSysResDtQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TrxSysResDtQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TrxSysResDtQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TrxSysResDtQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TrxSysResDtQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TrxSysResDtQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TrxSysResDtQuery : esTrxSysResDtQuery
	{
		public TrxSysResDtQuery()
		{

		}		
		
		public TrxSysResDtQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TrxSysResDtQuery";
        }
		
			
	}


	[Serializable]
	public partial class TrxSysResDtMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TrxSysResDtMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.Ono, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.Ono;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.TestCd, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.TestCd;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 6;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.TestNm, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.TestNm;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.DataTyp, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.DataTyp;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.ResultValue, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.ResultValue;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.ResultFt, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.ResultFt;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.ResultFt1, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.ResultFt1;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.Unit, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.Unit;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.Flag, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.Flag;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.RefRange, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.RefRange;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.Status, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.Status;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.TestComment, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.TestComment;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.ValidateBy, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.ValidateBy;
			c.CharacterMaxLength = 60;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.ValidateOn, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.ValidateOn;
			c.CharacterMaxLength = 14;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.DispSeq, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.DispSeq;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.OrderTestid, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.OrderTestid;
			c.CharacterMaxLength = 6;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.OrderTestnm, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.OrderTestnm;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.TestGroup, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.TestGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TrxSysResDtMetadata.ColumnNames.ItemParent, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = TrxSysResDtMetadata.PropertyNames.ItemParent;
			c.CharacterMaxLength = 6;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TrxSysResDtMetadata Meta()
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
			 public const string Ono = "ONO";
			 public const string TestCd = "TEST_CD";
			 public const string TestNm = "TEST_NM";
			 public const string DataTyp = "DATA_TYP";
			 public const string ResultValue = "RESULT_VALUE";
			 public const string ResultFt = "RESULT_FT";
			 public const string ResultFt1 = "RESULT_FT1";
			 public const string Unit = "UNIT";
			 public const string Flag = "FLAG";
			 public const string RefRange = "REF_RANGE";
			 public const string Status = "STATUS";
			 public const string TestComment = "TEST_COMMENT";
			 public const string ValidateBy = "VALIDATE_BY";
			 public const string ValidateOn = "VALIDATE_ON";
			 public const string DispSeq = "DISP_SEQ";
			 public const string OrderTestid = "ORDER_TESTID";
			 public const string OrderTestnm = "ORDER_TESTNM";
			 public const string TestGroup = "TEST_GROUP";
			 public const string ItemParent = "ITEM_PARENT";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Ono = "Ono";
			 public const string TestCd = "TestCd";
			 public const string TestNm = "TestNm";
			 public const string DataTyp = "DataTyp";
			 public const string ResultValue = "ResultValue";
			 public const string ResultFt = "ResultFt";
			 public const string ResultFt1 = "ResultFt1";
			 public const string Unit = "Unit";
			 public const string Flag = "Flag";
			 public const string RefRange = "RefRange";
			 public const string Status = "Status";
			 public const string TestComment = "TestComment";
			 public const string ValidateBy = "ValidateBy";
			 public const string ValidateOn = "ValidateOn";
			 public const string DispSeq = "DispSeq";
			 public const string OrderTestid = "OrderTestid";
			 public const string OrderTestnm = "OrderTestnm";
			 public const string TestGroup = "TestGroup";
			 public const string ItemParent = "ItemParent";
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
			lock (typeof(TrxSysResDtMetadata))
			{
				if(TrxSysResDtMetadata.mapDelegates == null)
				{
					TrxSysResDtMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TrxSysResDtMetadata.meta == null)
				{
					TrxSysResDtMetadata.meta = new TrxSysResDtMetadata();
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
				

				meta.AddTypeMap("Ono", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestCd", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestNm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DataTyp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultFt", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultFt1", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Unit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Flag", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RefRange", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Status", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestComment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidateBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidateOn", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DispSeq", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderTestid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderTestnm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemParent", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TRX_SYS_RES_DT";
				meta.Destination = "TRX_SYS_RES_DT";
				
				meta.spInsert = "proc_TRX_SYS_RES_DTInsert";				
				meta.spUpdate = "proc_TRX_SYS_RES_DTUpdate";		
				meta.spDelete = "proc_TRX_SYS_RES_DTDelete";
				meta.spLoadAll = "proc_TRX_SYS_RES_DTLoadAll";
				meta.spLoadByPrimaryKey = "proc_TRX_SYS_RES_DTLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TrxSysResDtMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
