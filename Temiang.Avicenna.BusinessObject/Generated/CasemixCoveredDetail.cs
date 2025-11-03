/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/4/2023 4:27:01 PM
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
	abstract public class esCasemixCoveredDetailCollection : esEntityCollectionWAuditLog
	{
		public esCasemixCoveredDetailCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CasemixCoveredDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esCasemixCoveredDetailQuery query)
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
			this.InitQuery(query as esCasemixCoveredDetailQuery);
		}
		#endregion

		virtual public CasemixCoveredDetail DetachEntity(CasemixCoveredDetail entity)
		{
			return base.DetachEntity(entity) as CasemixCoveredDetail;
		}

		virtual public CasemixCoveredDetail AttachEntity(CasemixCoveredDetail entity)
		{
			return base.AttachEntity(entity) as CasemixCoveredDetail;
		}

		virtual public void Combine(CasemixCoveredDetailCollection collection)
		{
			base.Combine(collection);
		}

		new public CasemixCoveredDetail this[int index]
		{
			get
			{
				return base[index] as CasemixCoveredDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CasemixCoveredDetail);
		}
	}

	[Serializable]
	abstract public class esCasemixCoveredDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCasemixCoveredDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esCasemixCoveredDetail()
		{
		}

		public esCasemixCoveredDetail(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 casemixCoveredDetailID, Int32 casemixCoveredID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixCoveredDetailID, casemixCoveredID);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixCoveredDetailID, casemixCoveredID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 casemixCoveredDetailID, Int32 casemixCoveredID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixCoveredDetailID, casemixCoveredID);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixCoveredDetailID, casemixCoveredID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 casemixCoveredDetailID, Int32 casemixCoveredID)
		{
			esCasemixCoveredDetailQuery query = this.GetDynamicQuery();
			query.Where(query.CasemixCoveredDetailID == casemixCoveredDetailID, query.CasemixCoveredID == casemixCoveredID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 casemixCoveredDetailID, Int32 casemixCoveredID)
		{
			esParameters parms = new esParameters();
			parms.Add("CasemixCoveredDetailID", casemixCoveredDetailID);
			parms.Add("CasemixCoveredID", casemixCoveredID);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "CasemixCoveredDetailID": this.str.CasemixCoveredDetailID = (string)value; break;
						case "CasemixCoveredID": this.str.CasemixCoveredID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "IsInclude": this.str.IsInclude = (string)value; break;
						case "IsNeedCasemixValidate": this.str.IsNeedCasemixValidate = (string)value; break;
						case "IsAllowedToOrder": this.str.IsAllowedToOrder = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsUsingGlobalSetting": this.str.IsUsingGlobalSetting = (string)value; break;
						case "QtyIpr": this.str.QtyIpr = (string)value; break;
						case "QtyOpr": this.str.QtyOpr = (string)value; break;
						case "QtyEmr": this.str.QtyEmr = (string)value; break;
						case "IsNeedCasemixValidateIpr": this.str.IsNeedCasemixValidateIpr = (string)value; break;
						case "IsAllowedToOrderIpr": this.str.IsAllowedToOrderIpr = (string)value; break;
						case "IsNeedCasemixValidateOpr": this.str.IsNeedCasemixValidateOpr = (string)value; break;
						case "IsAllowedToOrderOpr": this.str.IsAllowedToOrderOpr = (string)value; break;
						case "IsNeedCasemixValidateEmr": this.str.IsNeedCasemixValidateEmr = (string)value; break;
						case "IsAllowedToOrderEmr": this.str.IsAllowedToOrderEmr = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "CasemixCoveredDetailID":

							if (value == null || value is System.Int32)
								this.CasemixCoveredDetailID = (System.Int32?)value;
							break;
						case "CasemixCoveredID":

							if (value == null || value is System.Int32)
								this.CasemixCoveredID = (System.Int32?)value;
							break;
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "IsInclude":

							if (value == null || value is System.Boolean)
								this.IsInclude = (System.Boolean?)value;
							break;
						case "IsNeedCasemixValidate":

							if (value == null || value is System.Boolean)
								this.IsNeedCasemixValidate = (System.Boolean?)value;
							break;
						case "IsAllowedToOrder":

							if (value == null || value is System.Boolean)
								this.IsAllowedToOrder = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsUsingGlobalSetting":

							if (value == null || value is System.Boolean)
								this.IsUsingGlobalSetting = (System.Boolean?)value;
							break;
						case "QtyIpr":

							if (value == null || value is System.Decimal)
								this.QtyIpr = (System.Decimal?)value;
							break;
						case "QtyOpr":

							if (value == null || value is System.Decimal)
								this.QtyOpr = (System.Decimal?)value;
							break;
						case "QtyEmr":

							if (value == null || value is System.Decimal)
								this.QtyEmr = (System.Decimal?)value;
							break;
						case "IsNeedCasemixValidateIpr":

							if (value == null || value is System.Boolean)
								this.IsNeedCasemixValidateIpr = (System.Boolean?)value;
							break;
						case "IsAllowedToOrderIpr":

							if (value == null || value is System.Boolean)
								this.IsAllowedToOrderIpr = (System.Boolean?)value;
							break;
						case "IsNeedCasemixValidateOpr":

							if (value == null || value is System.Boolean)
								this.IsNeedCasemixValidateOpr = (System.Boolean?)value;
							break;
						case "IsAllowedToOrderOpr":

							if (value == null || value is System.Boolean)
								this.IsAllowedToOrderOpr = (System.Boolean?)value;
							break;
						case "IsNeedCasemixValidateEmr":

							if (value == null || value is System.Boolean)
								this.IsNeedCasemixValidateEmr = (System.Boolean?)value;
							break;
						case "IsAllowedToOrderEmr":

							if (value == null || value is System.Boolean)
								this.IsAllowedToOrderEmr = (System.Boolean?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to CasemixCoveredDetail.CasemixCoveredDetailID
		/// </summary>
		virtual public System.Int32? CasemixCoveredDetailID
		{
			get
			{
				return base.GetSystemInt32(CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID);
			}

			set
			{
				base.SetSystemInt32(CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.CasemixCoveredID
		/// </summary>
		virtual public System.Int32? CasemixCoveredID
		{
			get
			{
				return base.GetSystemInt32(CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredID);
			}

			set
			{
				base.SetSystemInt32(CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredID, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CasemixCoveredDetailMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(CasemixCoveredDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsInclude
		/// </summary>
		virtual public System.Boolean? IsInclude
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsInclude);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsInclude, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsNeedCasemixValidate
		/// </summary>
		virtual public System.Boolean? IsNeedCasemixValidate
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidate);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidate, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsAllowedToOrder
		/// </summary>
		virtual public System.Boolean? IsAllowedToOrder
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrder);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrder, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CasemixCoveredDetailMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CasemixCoveredDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CasemixCoveredDetailMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CasemixCoveredDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsUsingGlobalSetting
		/// </summary>
		virtual public System.Boolean? IsUsingGlobalSetting
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsUsingGlobalSetting);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsUsingGlobalSetting, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.QtyIpr
		/// </summary>
		virtual public System.Decimal? QtyIpr
		{
			get
			{
				return base.GetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.QtyIpr);
			}

			set
			{
				base.SetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.QtyIpr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.QtyOpr
		/// </summary>
		virtual public System.Decimal? QtyOpr
		{
			get
			{
				return base.GetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.QtyOpr);
			}

			set
			{
				base.SetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.QtyOpr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.QtyEmr
		/// </summary>
		virtual public System.Decimal? QtyEmr
		{
			get
			{
				return base.GetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.QtyEmr);
			}

			set
			{
				base.SetSystemDecimal(CasemixCoveredDetailMetadata.ColumnNames.QtyEmr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsNeedCasemixValidateIpr
		/// </summary>
		virtual public System.Boolean? IsNeedCasemixValidateIpr
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateIpr);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateIpr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsAllowedToOrderIpr
		/// </summary>
		virtual public System.Boolean? IsAllowedToOrderIpr
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderIpr);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderIpr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsNeedCasemixValidateOpr
		/// </summary>
		virtual public System.Boolean? IsNeedCasemixValidateOpr
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateOpr);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateOpr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsAllowedToOrderOpr
		/// </summary>
		virtual public System.Boolean? IsAllowedToOrderOpr
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderOpr);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderOpr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsNeedCasemixValidateEmr
		/// </summary>
		virtual public System.Boolean? IsNeedCasemixValidateEmr
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateEmr);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateEmr, value);
			}
		}
		/// <summary>
		/// Maps to CasemixCoveredDetail.IsAllowedToOrderEmr
		/// </summary>
		virtual public System.Boolean? IsAllowedToOrderEmr
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderEmr);
			}

			set
			{
				base.SetSystemBoolean(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderEmr, value);
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
		[BrowsableAttribute(false)]
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
			public esStrings(esCasemixCoveredDetail entity)
			{
				this.entity = entity;
			}
			public System.String CasemixCoveredDetailID
			{
				get
				{
					System.Int32? data = entity.CasemixCoveredDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixCoveredDetailID = null;
					else entity.CasemixCoveredDetailID = Convert.ToInt32(value);
				}
			}
			public System.String CasemixCoveredID
			{
				get
				{
					System.Int32? data = entity.CasemixCoveredID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixCoveredID = null;
					else entity.CasemixCoveredID = Convert.ToInt32(value);
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
			public System.String IsInclude
			{
				get
				{
					System.Boolean? data = entity.IsInclude;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInclude = null;
					else entity.IsInclude = Convert.ToBoolean(value);
				}
			}
			public System.String IsNeedCasemixValidate
			{
				get
				{
					System.Boolean? data = entity.IsNeedCasemixValidate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedCasemixValidate = null;
					else entity.IsNeedCasemixValidate = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowedToOrder
			{
				get
				{
					System.Boolean? data = entity.IsAllowedToOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowedToOrder = null;
					else entity.IsAllowedToOrder = Convert.ToBoolean(value);
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
			public System.String IsUsingGlobalSetting
			{
				get
				{
					System.Boolean? data = entity.IsUsingGlobalSetting;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingGlobalSetting = null;
					else entity.IsUsingGlobalSetting = Convert.ToBoolean(value);
				}
			}
			public System.String QtyIpr
			{
				get
				{
					System.Decimal? data = entity.QtyIpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyIpr = null;
					else entity.QtyIpr = Convert.ToDecimal(value);
				}
			}
			public System.String QtyOpr
			{
				get
				{
					System.Decimal? data = entity.QtyOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyOpr = null;
					else entity.QtyOpr = Convert.ToDecimal(value);
				}
			}
			public System.String QtyEmr
			{
				get
				{
					System.Decimal? data = entity.QtyEmr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyEmr = null;
					else entity.QtyEmr = Convert.ToDecimal(value);
				}
			}
			public System.String IsNeedCasemixValidateIpr
			{
				get
				{
					System.Boolean? data = entity.IsNeedCasemixValidateIpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedCasemixValidateIpr = null;
					else entity.IsNeedCasemixValidateIpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowedToOrderIpr
			{
				get
				{
					System.Boolean? data = entity.IsAllowedToOrderIpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowedToOrderIpr = null;
					else entity.IsAllowedToOrderIpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsNeedCasemixValidateOpr
			{
				get
				{
					System.Boolean? data = entity.IsNeedCasemixValidateOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedCasemixValidateOpr = null;
					else entity.IsNeedCasemixValidateOpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowedToOrderOpr
			{
				get
				{
					System.Boolean? data = entity.IsAllowedToOrderOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowedToOrderOpr = null;
					else entity.IsAllowedToOrderOpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsNeedCasemixValidateEmr
			{
				get
				{
					System.Boolean? data = entity.IsNeedCasemixValidateEmr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedCasemixValidateEmr = null;
					else entity.IsNeedCasemixValidateEmr = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowedToOrderEmr
			{
				get
				{
					System.Boolean? data = entity.IsAllowedToOrderEmr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowedToOrderEmr = null;
					else entity.IsAllowedToOrderEmr = Convert.ToBoolean(value);
				}
			}
			private esCasemixCoveredDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCasemixCoveredDetailQuery query)
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
				throw new Exception("esCasemixCoveredDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CasemixCoveredDetail : esCasemixCoveredDetail
	{
	}

	[Serializable]
	abstract public class esCasemixCoveredDetailQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CasemixCoveredDetailMetadata.Meta();
			}
		}

		public esQueryItem CasemixCoveredDetailID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID, esSystemType.Int32);
			}
		}

		public esQueryItem CasemixCoveredID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredID, esSystemType.Int32);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem IsInclude
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsInclude, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNeedCasemixValidate
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidate, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowedToOrder
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrder, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsUsingGlobalSetting
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsUsingGlobalSetting, esSystemType.Boolean);
			}
		}

		public esQueryItem QtyIpr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.QtyIpr, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyOpr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.QtyOpr, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyEmr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.QtyEmr, esSystemType.Decimal);
			}
		}

		public esQueryItem IsNeedCasemixValidateIpr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateIpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowedToOrderIpr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderIpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNeedCasemixValidateOpr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateOpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowedToOrderOpr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderOpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNeedCasemixValidateEmr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateEmr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowedToOrderEmr
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderEmr, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CasemixCoveredDetailCollection")]
	public partial class CasemixCoveredDetailCollection : esCasemixCoveredDetailCollection, IEnumerable<CasemixCoveredDetail>
	{
		public CasemixCoveredDetailCollection()
		{

		}

		public static implicit operator List<CasemixCoveredDetail>(CasemixCoveredDetailCollection coll)
		{
			List<CasemixCoveredDetail> list = new List<CasemixCoveredDetail>();

			foreach (CasemixCoveredDetail emp in coll)
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
				return CasemixCoveredDetailMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixCoveredDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CasemixCoveredDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CasemixCoveredDetail();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CasemixCoveredDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixCoveredDetailQuery();
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
		public bool Load(CasemixCoveredDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CasemixCoveredDetail AddNew()
		{
			CasemixCoveredDetail entity = base.AddNewEntity() as CasemixCoveredDetail;

			return entity;
		}
		public CasemixCoveredDetail FindByPrimaryKey(Int32 casemixCoveredDetailID, Int32 casemixCoveredID)
		{
			return base.FindByPrimaryKey(casemixCoveredDetailID, casemixCoveredID) as CasemixCoveredDetail;
		}

		#region IEnumerable< CasemixCoveredDetail> Members

		IEnumerator<CasemixCoveredDetail> IEnumerable<CasemixCoveredDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CasemixCoveredDetail;
			}
		}

		#endregion

		private CasemixCoveredDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CasemixCoveredDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CasemixCoveredDetail ({CasemixCoveredDetailID, CasemixCoveredID})")]
	[Serializable]
	public partial class CasemixCoveredDetail : esCasemixCoveredDetail
	{
		public CasemixCoveredDetail()
		{
		}

		public CasemixCoveredDetail(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CasemixCoveredDetailMetadata.Meta();
			}
		}

		override protected esCasemixCoveredDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixCoveredDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CasemixCoveredDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixCoveredDetailQuery();
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
		public bool Load(CasemixCoveredDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CasemixCoveredDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CasemixCoveredDetailQuery : esCasemixCoveredDetailQuery
	{
		public CasemixCoveredDetailQuery()
		{

		}

		public CasemixCoveredDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CasemixCoveredDetailQuery";
		}
	}

	[Serializable]
	public partial class CasemixCoveredDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CasemixCoveredDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredDetailID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.CasemixCoveredDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.CasemixCoveredID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.CasemixCoveredID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.Qty;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsInclude, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsInclude;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidate, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsNeedCasemixValidate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrder, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsAllowedToOrder;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsUsingGlobalSetting, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsUsingGlobalSetting;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.QtyIpr, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.QtyIpr;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.QtyOpr, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.QtyOpr;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.QtyEmr, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.QtyEmr;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateIpr, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsNeedCasemixValidateIpr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderIpr, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsAllowedToOrderIpr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateOpr, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsNeedCasemixValidateOpr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderOpr, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsAllowedToOrderOpr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateEmr, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsNeedCasemixValidateEmr;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderEmr, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredDetailMetadata.PropertyNames.IsAllowedToOrderEmr;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CasemixCoveredDetailMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string CasemixCoveredDetailID = "CasemixCoveredDetailID";
			public const string CasemixCoveredID = "CasemixCoveredID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string IsInclude = "IsInclude";
			public const string IsNeedCasemixValidate = "IsNeedCasemixValidate";
			public const string IsAllowedToOrder = "IsAllowedToOrder";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUsingGlobalSetting = "IsUsingGlobalSetting";
			public const string QtyIpr = "QtyIpr";
			public const string QtyOpr = "QtyOpr";
			public const string QtyEmr = "QtyEmr";
			public const string IsNeedCasemixValidateIpr = "IsNeedCasemixValidateIpr";
			public const string IsAllowedToOrderIpr = "IsAllowedToOrderIpr";
			public const string IsNeedCasemixValidateOpr = "IsNeedCasemixValidateOpr";
			public const string IsAllowedToOrderOpr = "IsAllowedToOrderOpr";
			public const string IsNeedCasemixValidateEmr = "IsNeedCasemixValidateEmr";
			public const string IsAllowedToOrderEmr = "IsAllowedToOrderEmr";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string CasemixCoveredDetailID = "CasemixCoveredDetailID";
			public const string CasemixCoveredID = "CasemixCoveredID";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string IsInclude = "IsInclude";
			public const string IsNeedCasemixValidate = "IsNeedCasemixValidate";
			public const string IsAllowedToOrder = "IsAllowedToOrder";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsUsingGlobalSetting = "IsUsingGlobalSetting";
			public const string QtyIpr = "QtyIpr";
			public const string QtyOpr = "QtyOpr";
			public const string QtyEmr = "QtyEmr";
			public const string IsNeedCasemixValidateIpr = "IsNeedCasemixValidateIpr";
			public const string IsAllowedToOrderIpr = "IsAllowedToOrderIpr";
			public const string IsNeedCasemixValidateOpr = "IsNeedCasemixValidateOpr";
			public const string IsAllowedToOrderOpr = "IsAllowedToOrderOpr";
			public const string IsNeedCasemixValidateEmr = "IsNeedCasemixValidateEmr";
			public const string IsAllowedToOrderEmr = "IsAllowedToOrderEmr";
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
			lock (typeof(CasemixCoveredDetailMetadata))
			{
				if (CasemixCoveredDetailMetadata.mapDelegates == null)
				{
					CasemixCoveredDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CasemixCoveredDetailMetadata.meta == null)
				{
					CasemixCoveredDetailMetadata.meta = new CasemixCoveredDetailMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("CasemixCoveredDetailID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CasemixCoveredID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsInclude", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNeedCasemixValidate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowedToOrder", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsingGlobalSetting", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("QtyIpr", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyOpr", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyEmr", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsNeedCasemixValidateIpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowedToOrderIpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNeedCasemixValidateOpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowedToOrderOpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNeedCasemixValidateEmr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowedToOrderEmr", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "CasemixCoveredDetail";
				meta.Destination = "CasemixCoveredDetail";
				meta.spInsert = "proc_CasemixCoveredDetailInsert";
				meta.spUpdate = "proc_CasemixCoveredDetailUpdate";
				meta.spDelete = "proc_CasemixCoveredDetailDelete";
				meta.spLoadAll = "proc_CasemixCoveredDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_CasemixCoveredDetailLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CasemixCoveredDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
