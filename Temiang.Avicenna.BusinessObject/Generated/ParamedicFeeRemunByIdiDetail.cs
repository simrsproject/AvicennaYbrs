/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/25/2021 12:16:12 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esParamedicFeeRemunByIdiDetailCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeRemunByIdiDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeRemunByIdiDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiDetailQuery query)
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
			this.InitQuery(query as esParamedicFeeRemunByIdiDetailQuery);
		}
		#endregion
			
		virtual public ParamedicFeeRemunByIdiDetail DetachEntity(ParamedicFeeRemunByIdiDetail entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeRemunByIdiDetail;
		}
		
		virtual public ParamedicFeeRemunByIdiDetail AttachEntity(ParamedicFeeRemunByIdiDetail entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeRemunByIdiDetail;
		}
		
		virtual public void Combine(ParamedicFeeRemunByIdiDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeRemunByIdiDetail this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeRemunByIdiDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeRemunByIdiDetail);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdiDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeRemunByIdiDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeRemunByIdiDetail()
		{
		}
	
		public esParamedicFeeRemunByIdiDetail(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 remunDetailID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunDetailID);
		}
	
		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 remunDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(remunDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(remunDetailID);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 remunDetailID)
		{
			esParamedicFeeRemunByIdiDetailQuery query = this.GetDynamicQuery();
			query.Where(query.RemunDetailID==remunDetailID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 remunDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("RemunDetailID",remunDetailID);
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
						case "RemunDetailID": this.str.RemunDetailID = (string)value; break;
						case "RemunID": this.str.RemunID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "IdiCode": this.str.IdiCode = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "Score": this.str.Score = (string)value; break;
						case "Rvu": this.str.Rvu = (string)value; break;
						case "RvuConversion": this.str.RvuConversion = (string)value; break;
						case "Coefficient": this.str.Coefficient = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SmfID": this.str.SmfID = (string)value; break;
						case "ItemGroupID": this.str.ItemGroupID = (string)value; break;
						case "SettingID": this.str.SettingID = (string)value; break;
						case "Multiplier": this.str.Multiplier = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RemunDetailID":
						
							if (value == null || value is System.Int32)
								this.RemunDetailID = (System.Int32?)value;
							break;
						case "RemunID":
						
							if (value == null || value is System.Int32)
								this.RemunID = (System.Int32?)value;
							break;
						case "Qty":
						
							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "Score":
						
							if (value == null || value is System.Decimal)
								this.Score = (System.Decimal?)value;
							break;
						case "Rvu":
						
							if (value == null || value is System.Decimal)
								this.Rvu = (System.Decimal?)value;
							break;
						case "RvuConversion":
						
							if (value == null || value is System.Decimal)
								this.RvuConversion = (System.Decimal?)value;
							break;
						case "Coefficient":
						
							if (value == null || value is System.Decimal)
								this.Coefficient = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "SettingID":
						
							if (value == null || value is System.Int32)
								this.SettingID = (System.Int32?)value;
							break;
						case "Multiplier":
						
							if (value == null || value is System.Decimal)
								this.Multiplier = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeRemunByIdiDetail.RemunDetailID
		/// </summary>
		virtual public System.Int32? RemunDetailID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunDetailID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunDetailID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.IdiCode
		/// </summary>
		virtual public System.String IdiCode
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.IdiCode);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.IdiCode, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.Score
		/// </summary>
		virtual public System.Decimal? Score
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Score);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Score, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.Rvu
		/// </summary>
		virtual public System.Decimal? Rvu
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Rvu);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Rvu, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.RvuConversion
		/// </summary>
		virtual public System.Decimal? RvuConversion
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RvuConversion);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RvuConversion, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.Coefficient
		/// </summary>
		virtual public System.Decimal? Coefficient
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Coefficient);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Coefficient, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.SmfID
		/// </summary>
		virtual public System.String SmfID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SmfID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SmfID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.ItemGroupID
		/// </summary>
		virtual public System.String ItemGroupID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemGroupID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemGroupID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.SettingID
		/// </summary>
		virtual public System.Int32? SettingID
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SettingID);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SettingID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeRemunByIdiDetail.Multiplier
		/// </summary>
		virtual public System.Decimal? Multiplier
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Multiplier);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Multiplier, value);
			}
		}
		
		#endregion	

		#region String Properties
		
		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
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
			public esStrings(esParamedicFeeRemunByIdiDetail entity)
			{
				this.entity = entity;
			}
			public System.String RemunDetailID
			{
				get
				{
					System.Int32? data = entity.RemunDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunDetailID = null;
					else entity.RemunDetailID = Convert.ToInt32(value);
				}
			}
			public System.String RemunID
			{
				get
				{
					System.Int32? data = entity.RemunID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RemunID = null;
					else entity.RemunID = Convert.ToInt32(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String IdiCode
			{
				get
				{
					System.String data = entity.IdiCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdiCode = null;
					else entity.IdiCode = Convert.ToString(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
			public System.String Score
			{
				get
				{
					System.Decimal? data = entity.Score;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score = null;
					else entity.Score = Convert.ToDecimal(value);
				}
			}
			public System.String Rvu
			{
				get
				{
					System.Decimal? data = entity.Rvu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rvu = null;
					else entity.Rvu = Convert.ToDecimal(value);
				}
			}
			public System.String RvuConversion
			{
				get
				{
					System.Decimal? data = entity.RvuConversion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RvuConversion = null;
					else entity.RvuConversion = Convert.ToDecimal(value);
				}
			}
			public System.String Coefficient
			{
				get
				{
					System.Decimal? data = entity.Coefficient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Coefficient = null;
					else entity.Coefficient = Convert.ToDecimal(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			public System.String SmfID
			{
				get
				{
					System.String data = entity.SmfID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SmfID = null;
					else entity.SmfID = Convert.ToString(value);
				}
			}
			public System.String ItemGroupID
			{
				get
				{
					System.String data = entity.ItemGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroupID = null;
					else entity.ItemGroupID = Convert.ToString(value);
				}
			}
			public System.String SettingID
			{
				get
				{
					System.Int32? data = entity.SettingID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SettingID = null;
					else entity.SettingID = Convert.ToInt32(value);
				}
			}
			public System.String Multiplier
			{
				get
				{
					System.Decimal? data = entity.Multiplier;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Multiplier = null;
					else entity.Multiplier = Convert.ToDecimal(value);
				}
			}
			private esParamedicFeeRemunByIdiDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeRemunByIdiDetailQuery query)
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
				throw new Exception("esParamedicFeeRemunByIdiDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeRemunByIdiDetail : esParamedicFeeRemunByIdiDetail
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeRemunByIdiDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunDetailID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunDetailID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem IdiCode
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.IdiCode, esSystemType.String);
			}
		} 
			
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Score
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Score, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Rvu
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Rvu, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem RvuConversion
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RvuConversion, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Coefficient
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Coefficient, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem SmfID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SmfID, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemGroupID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemGroupID, esSystemType.String);
			}
		} 
			
		public esQueryItem SettingID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SettingID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem Multiplier
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Multiplier, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeRemunByIdiDetailCollection")]
	public partial class ParamedicFeeRemunByIdiDetailCollection : esParamedicFeeRemunByIdiDetailCollection, IEnumerable< ParamedicFeeRemunByIdiDetail>
	{
		public ParamedicFeeRemunByIdiDetailCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeRemunByIdiDetail>(ParamedicFeeRemunByIdiDetailCollection coll)
		{
			List< ParamedicFeeRemunByIdiDetail> list = new List< ParamedicFeeRemunByIdiDetail>();
			
			foreach (ParamedicFeeRemunByIdiDetail emp in coll)
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
				return  ParamedicFeeRemunByIdiDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeRemunByIdiDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeRemunByIdiDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(ParamedicFeeRemunByIdiDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeRemunByIdiDetail AddNew()
		{
			ParamedicFeeRemunByIdiDetail entity = base.AddNewEntity() as ParamedicFeeRemunByIdiDetail;
			
			return entity;		
		}
		public ParamedicFeeRemunByIdiDetail FindByPrimaryKey(Int32 remunDetailID)
		{
			return base.FindByPrimaryKey(remunDetailID) as ParamedicFeeRemunByIdiDetail;
		}

		#region IEnumerable< ParamedicFeeRemunByIdiDetail> Members

		IEnumerator< ParamedicFeeRemunByIdiDetail> IEnumerable< ParamedicFeeRemunByIdiDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeRemunByIdiDetail;
			}
		}

		#endregion
		
		private ParamedicFeeRemunByIdiDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeRemunByIdiDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeRemunByIdiDetail ({RemunDetailID})")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdiDetail : esParamedicFeeRemunByIdiDetail
	{
		public ParamedicFeeRemunByIdiDetail()
		{
		}	
	
		public ParamedicFeeRemunByIdiDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeRemunByIdiDetailMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeRemunByIdiDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeRemunByIdiDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeRemunByIdiDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeRemunByIdiDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}
		
		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(ParamedicFeeRemunByIdiDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeRemunByIdiDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeRemunByIdiDetailQuery : esParamedicFeeRemunByIdiDetailQuery
	{
		public ParamedicFeeRemunByIdiDetailQuery()
		{

		}		
		
		public ParamedicFeeRemunByIdiDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeRemunByIdiDetailQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeRemunByIdiDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeRemunByIdiDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.RemunDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RemunID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.RemunID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.IdiCode, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.IdiCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Qty, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.Qty;
			c.NumericPrecision = 18;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Score, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.Score;
			c.NumericPrecision = 18;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Rvu, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.Rvu;
			c.NumericPrecision = 18;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.RvuConversion, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.RvuConversion;
			c.NumericPrecision = 18;
			c.NumericScale = 9;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Coefficient, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.Coefficient;
			c.NumericPrecision = 18;
			c.NumericScale = 9;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.CreateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SmfID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.SmfID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.ItemGroupID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.ItemGroupID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.SettingID, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.SettingID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeRemunByIdiDetailMetadata.ColumnNames.Multiplier, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeRemunByIdiDetailMetadata.PropertyNames.Multiplier;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeRemunByIdiDetailMetadata Meta()
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
			public const string RemunDetailID = "RemunDetailID";
			public const string RemunID = "RemunID";
			public const string ParamedicID = "ParamedicID";
			public const string ItemID = "ItemID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IdiCode = "IdiCode";
			public const string Qty = "Qty";
			public const string Score = "Score";
			public const string Rvu = "Rvu";
			public const string RvuConversion = "RvuConversion";
			public const string Coefficient = "Coefficient";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SmfID = "SmfID";
			public const string ItemGroupID = "ItemGroupID";
			public const string SettingID = "SettingID";
			public const string Multiplier = "Multiplier";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RemunDetailID = "RemunDetailID";
			public const string RemunID = "RemunID";
			public const string ParamedicID = "ParamedicID";
			public const string ItemID = "ItemID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IdiCode = "IdiCode";
			public const string Qty = "Qty";
			public const string Score = "Score";
			public const string Rvu = "Rvu";
			public const string RvuConversion = "RvuConversion";
			public const string Coefficient = "Coefficient";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SmfID = "SmfID";
			public const string ItemGroupID = "ItemGroupID";
			public const string SettingID = "SettingID";
			public const string Multiplier = "Multiplier";
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
			lock (typeof(ParamedicFeeRemunByIdiDetailMetadata))
			{
				if(ParamedicFeeRemunByIdiDetailMetadata.mapDelegates == null)
				{
					ParamedicFeeRemunByIdiDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeRemunByIdiDetailMetadata.meta == null)
				{
					ParamedicFeeRemunByIdiDetailMetadata.meta = new ParamedicFeeRemunByIdiDetailMetadata();
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
				
				meta.AddTypeMap("RemunDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RemunID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IdiCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Score", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Rvu", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("RvuConversion", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Coefficient", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmfID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SettingID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Multiplier", new esTypeMap("decimal", "System.Decimal"));
		

				meta.Source = "ParamedicFeeRemunByIdiDetail";
				meta.Destination = "ParamedicFeeRemunByIdiDetail";
				meta.spInsert = "proc_ParamedicFeeRemunByIdiDetailInsert";				
				meta.spUpdate = "proc_ParamedicFeeRemunByIdiDetailUpdate";		
				meta.spDelete = "proc_ParamedicFeeRemunByIdiDetailDelete";
				meta.spLoadAll = "proc_ParamedicFeeRemunByIdiDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeRemunByIdiDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeRemunByIdiDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
