/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/24/2022 10:23:30 PM
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
	abstract public class esEmployeeRemunDetailCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeRemunDetailCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "EmployeeRemunDetailCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esEmployeeRemunDetailQuery query)
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
			this.InitQuery(query as esEmployeeRemunDetailQuery);
		}
		#endregion
			
		virtual public EmployeeRemunDetail DetachEntity(EmployeeRemunDetail entity)
		{
			return base.DetachEntity(entity) as EmployeeRemunDetail;
		}
		
		virtual public EmployeeRemunDetail AttachEntity(EmployeeRemunDetail entity)
		{
			return base.AttachEntity(entity) as EmployeeRemunDetail;
		}
		
		virtual public void Combine(EmployeeRemunDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeRemunDetail this[int index]
		{
			get
			{
				return base[index] as EmployeeRemunDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeRemunDetail);
		}
	}

	[Serializable]
	abstract public class esEmployeeRemunDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeRemunDetailQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esEmployeeRemunDetail()
		{
		}
	
		public esEmployeeRemunDetail(DataRow row)
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
			esEmployeeRemunDetailQuery query = this.GetDynamicQuery();
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
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "SREmployeeStatus": this.str.SREmployeeStatus = (string)value; break;
						case "CoorporateGradeLevel": this.str.CoorporateGradeLevel = (string)value; break;
						case "CoorporateGradeValue": this.str.CoorporateGradeValue = (string)value; break;
						case "PositionFeeValue": this.str.PositionFeeValue = (string)value; break;
						case "InsentifFeeGrossValue": this.str.InsentifFeeGrossValue = (string)value; break;
						case "RenkinIndex": this.str.RenkinIndex = (string)value; break;
						case "InsentifFeeValue": this.str.InsentifFeeValue = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "CoorporateGradeCoefficient": this.str.CoorporateGradeCoefficient = (string)value; break;
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
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "CoorporateGradeLevel":
						
							if (value == null || value is System.Int32)
								this.CoorporateGradeLevel = (System.Int32?)value;
							break;
						case "CoorporateGradeValue":
						
							if (value == null || value is System.Int32)
								this.CoorporateGradeValue = (System.Int32?)value;
							break;
						case "PositionFeeValue":
						
							if (value == null || value is System.Decimal)
								this.PositionFeeValue = (System.Decimal?)value;
							break;
						case "InsentifFeeGrossValue":
						
							if (value == null || value is System.Decimal)
								this.InsentifFeeGrossValue = (System.Decimal?)value;
							break;
						case "RenkinIndex":
						
							if (value == null || value is System.Decimal)
								this.RenkinIndex = (System.Decimal?)value;
							break;
						case "InsentifFeeValue":
						
							if (value == null || value is System.Decimal)
								this.InsentifFeeValue = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "CoorporateGradeCoefficient":
						
							if (value == null || value is System.Decimal)
								this.CoorporateGradeCoefficient = (System.Decimal?)value;
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
		/// Maps to EmployeeRemunDetail.RemunDetailID
		/// </summary>
		virtual public System.Int32? RemunDetailID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.RemunDetailID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.RemunDetailID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.RemunID
		/// </summary>
		virtual public System.Int32? RemunID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.RemunID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.RemunID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(EmployeeRemunDetailMetadata.ColumnNames.SREmployeeStatus);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunDetailMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.CoorporateGradeLevel
		/// </summary>
		virtual public System.Int32? CoorporateGradeLevel
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeLevel);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.CoorporateGradeValue
		/// </summary>
		virtual public System.Int32? CoorporateGradeValue
		{
			get
			{
				return base.GetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeValue);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.PositionFeeValue
		/// </summary>
		virtual public System.Decimal? PositionFeeValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.PositionFeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.PositionFeeValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.InsentifFeeGrossValue
		/// </summary>
		virtual public System.Decimal? InsentifFeeGrossValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeGrossValue);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeGrossValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.RenkinIndex
		/// </summary>
		virtual public System.Decimal? RenkinIndex
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.RenkinIndex);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.RenkinIndex, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.InsentifFeeValue
		/// </summary>
		virtual public System.Decimal? InsentifFeeValue
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeValue);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeValue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRemunDetailMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeRemunDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeRemunDetailMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeRemunDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeRemunDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeRemunDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeRemunDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeRemunDetail.CoorporateGradeCoefficient
		/// </summary>
		virtual public System.Decimal? CoorporateGradeCoefficient
		{
			get
			{
				return base.GetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeCoefficient);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeCoefficient, value);
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
			public esStrings(esEmployeeRemunDetail entity)
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
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String SREmployeeStatus
			{
				get
				{
					System.String data = entity.SREmployeeStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeStatus = null;
					else entity.SREmployeeStatus = Convert.ToString(value);
				}
			}
			public System.String CoorporateGradeLevel
			{
				get
				{
					System.Int32? data = entity.CoorporateGradeLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeLevel = null;
					else entity.CoorporateGradeLevel = Convert.ToInt32(value);
				}
			}
			public System.String CoorporateGradeValue
			{
				get
				{
					System.Int32? data = entity.CoorporateGradeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeValue = null;
					else entity.CoorporateGradeValue = Convert.ToInt32(value);
				}
			}
			public System.String PositionFeeValue
			{
				get
				{
					System.Decimal? data = entity.PositionFeeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionFeeValue = null;
					else entity.PositionFeeValue = Convert.ToDecimal(value);
				}
			}
			public System.String InsentifFeeGrossValue
			{
				get
				{
					System.Decimal? data = entity.InsentifFeeGrossValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsentifFeeGrossValue = null;
					else entity.InsentifFeeGrossValue = Convert.ToDecimal(value);
				}
			}
			public System.String RenkinIndex
			{
				get
				{
					System.Decimal? data = entity.RenkinIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RenkinIndex = null;
					else entity.RenkinIndex = Convert.ToDecimal(value);
				}
			}
			public System.String InsentifFeeValue
			{
				get
				{
					System.Decimal? data = entity.InsentifFeeValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsentifFeeValue = null;
					else entity.InsentifFeeValue = Convert.ToDecimal(value);
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
			public System.String CoorporateGradeCoefficient
			{
				get
				{
					System.Decimal? data = entity.CoorporateGradeCoefficient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoorporateGradeCoefficient = null;
					else entity.CoorporateGradeCoefficient = Convert.ToDecimal(value);
				}
			}
			private esEmployeeRemunDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeRemunDetailQuery query)
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
				throw new Exception("esEmployeeRemunDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeRemunDetail : esEmployeeRemunDetail
	{	
	}

	[Serializable]
	abstract public class esEmployeeRemunDetailQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeRemunDetailMetadata.Meta();
			}
		}	
			
		public esQueryItem RemunDetailID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.RemunDetailID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem RemunID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.RemunID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		} 
			
		public esQueryItem CoorporateGradeLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeLevel, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CoorporateGradeValue
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeValue, esSystemType.Int32);
			}
		} 
			
		public esQueryItem PositionFeeValue
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.PositionFeeValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem InsentifFeeGrossValue
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeGrossValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem RenkinIndex
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.RenkinIndex, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem InsentifFeeValue
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeValue, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CoorporateGradeCoefficient
		{
			get
			{
				return new esQueryItem(this, EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeCoefficient, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeRemunDetailCollection")]
	public partial class EmployeeRemunDetailCollection : esEmployeeRemunDetailCollection, IEnumerable< EmployeeRemunDetail>
	{
		public EmployeeRemunDetailCollection()
		{

		}	
		
		public static implicit operator List< EmployeeRemunDetail>(EmployeeRemunDetailCollection coll)
		{
			List< EmployeeRemunDetail> list = new List< EmployeeRemunDetail>();
			
			foreach (EmployeeRemunDetail emp in coll)
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
				return  EmployeeRemunDetailMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeRemunDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeRemunDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeRemunDetail();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public EmployeeRemunDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeRemunDetailQuery();
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
		public bool Load(EmployeeRemunDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeRemunDetail AddNew()
		{
			EmployeeRemunDetail entity = base.AddNewEntity() as EmployeeRemunDetail;
			
			return entity;		
		}
		public EmployeeRemunDetail FindByPrimaryKey(Int32 remunDetailID)
		{
			return base.FindByPrimaryKey(remunDetailID) as EmployeeRemunDetail;
		}

		#region IEnumerable< EmployeeRemunDetail> Members

		IEnumerator< EmployeeRemunDetail> IEnumerable< EmployeeRemunDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeRemunDetail;
			}
		}

		#endregion
		
		private EmployeeRemunDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeRemunDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeRemunDetail ({RemunDetailID})")]
	[Serializable]
	public partial class EmployeeRemunDetail : esEmployeeRemunDetail
	{
		public EmployeeRemunDetail()
		{
		}	
	
		public EmployeeRemunDetail(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeRemunDetailMetadata.Meta();
			}
		}	
	
		override protected esEmployeeRemunDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeRemunDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public EmployeeRemunDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeRemunDetailQuery();
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
		public bool Load(EmployeeRemunDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private EmployeeRemunDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeRemunDetailQuery : esEmployeeRemunDetailQuery
	{
		public EmployeeRemunDetailQuery()
		{

		}		
		
		public EmployeeRemunDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "EmployeeRemunDetailQuery";
        }
	}

	[Serializable]
	public partial class EmployeeRemunDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeRemunDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.RemunDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.RemunDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.RemunID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.RemunID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.PositionID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.SREmployeeStatus, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeLevel, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.CoorporateGradeLevel;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeValue, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.CoorporateGradeValue;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.PositionFeeValue, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.PositionFeeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeGrossValue, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.InsentifFeeGrossValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.RenkinIndex, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.RenkinIndex;
			c.NumericPrecision = 10;
			c.NumericScale = 6;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.InsentifFeeValue, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.InsentifFeeValue;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.CreateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.CreateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(EmployeeRemunDetailMetadata.ColumnNames.CoorporateGradeCoefficient, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeRemunDetailMetadata.PropertyNames.CoorporateGradeCoefficient;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				

		}
		#endregion
	
		static public EmployeeRemunDetailMetadata Meta()
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
			public const string PersonID = "PersonID";
			public const string PositionID = "PositionID";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string CoorporateGradeLevel = "CoorporateGradeLevel";
			public const string CoorporateGradeValue = "CoorporateGradeValue";
			public const string PositionFeeValue = "PositionFeeValue";
			public const string InsentifFeeGrossValue = "InsentifFeeGrossValue";
			public const string RenkinIndex = "RenkinIndex";
			public const string InsentifFeeValue = "InsentifFeeValue";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CoorporateGradeCoefficient = "CoorporateGradeCoefficient";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RemunDetailID = "RemunDetailID";
			public const string RemunID = "RemunID";
			public const string PersonID = "PersonID";
			public const string PositionID = "PositionID";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string CoorporateGradeLevel = "CoorporateGradeLevel";
			public const string CoorporateGradeValue = "CoorporateGradeValue";
			public const string PositionFeeValue = "PositionFeeValue";
			public const string InsentifFeeGrossValue = "InsentifFeeGrossValue";
			public const string RenkinIndex = "RenkinIndex";
			public const string InsentifFeeValue = "InsentifFeeValue";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CoorporateGradeCoefficient = "CoorporateGradeCoefficient";
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
			lock (typeof(EmployeeRemunDetailMetadata))
			{
				if(EmployeeRemunDetailMetadata.mapDelegates == null)
				{
					EmployeeRemunDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeRemunDetailMetadata.meta == null)
				{
					EmployeeRemunDetailMetadata.meta = new EmployeeRemunDetailMetadata();
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
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoorporateGradeLevel", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CoorporateGradeValue", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionFeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InsentifFeeGrossValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("RenkinIndex", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("InsentifFeeValue", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoorporateGradeCoefficient", new esTypeMap("decimal", "System.Decimal"));
		

				meta.Source = "EmployeeRemunDetail";
				meta.Destination = "EmployeeRemunDetail";
				meta.spInsert = "proc_EmployeeRemunDetailInsert";				
				meta.spUpdate = "proc_EmployeeRemunDetailUpdate";		
				meta.spDelete = "proc_EmployeeRemunDetailDelete";
				meta.spLoadAll = "proc_EmployeeRemunDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeRemunDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeRemunDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
