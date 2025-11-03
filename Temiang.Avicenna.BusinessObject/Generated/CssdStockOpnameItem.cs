/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/4/2023 5:43:59 PM
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
	abstract public class esCssdStockOpnameItemCollection : esEntityCollectionWAuditLog
	{
		public esCssdStockOpnameItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdStockOpnameItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdStockOpnameItemQuery query)
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
			this.InitQuery(query as esCssdStockOpnameItemQuery);
		}
		#endregion

		virtual public CssdStockOpnameItem DetachEntity(CssdStockOpnameItem entity)
		{
			return base.DetachEntity(entity) as CssdStockOpnameItem;
		}

		virtual public CssdStockOpnameItem AttachEntity(CssdStockOpnameItem entity)
		{
			return base.AttachEntity(entity) as CssdStockOpnameItem;
		}

		virtual public void Combine(CssdStockOpnameItemCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdStockOpnameItem this[int index]
		{
			get
			{
				return base[index] as CssdStockOpnameItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdStockOpnameItem);
		}
	}

	[Serializable]
	abstract public class esCssdStockOpnameItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdStockOpnameItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdStockOpnameItem()
		{
		}

		public esCssdStockOpnameItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo)
		{
			esCssdStockOpnameItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SequenceNo", sequenceNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "PageNo": this.str.PageNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
						case "BalanceReceived": this.str.BalanceReceived = (string)value; break;
						case "BalanceDeconImmersion": this.str.BalanceDeconImmersion = (string)value; break;
						case "BalanceDeconAbstersion": this.str.BalanceDeconAbstersion = (string)value; break;
						case "BalanceDeconDrying": this.str.BalanceDeconDrying = (string)value; break;
						case "BalanceFeasibilityTest": this.str.BalanceFeasibilityTest = (string)value; break;
						case "BalancePackaging": this.str.BalancePackaging = (string)value; break;
						case "BalanceUltrasound": this.str.BalanceUltrasound = (string)value; break;
						case "BalanceSterilization": this.str.BalanceSterilization = (string)value; break;
						case "BalanceDistribution": this.str.BalanceDistribution = (string)value; break;
						case "BalanceReturned": this.str.BalanceReturned = (string)value; break;
						case "PrevBalance": this.str.PrevBalance = (string)value; break;
						case "PrevBalanceReceived": this.str.PrevBalanceReceived = (string)value; break;
						case "PrevBalanceDeconImmersion": this.str.PrevBalanceDeconImmersion = (string)value; break;
						case "PrevBalanceDeconAbstersion": this.str.PrevBalanceDeconAbstersion = (string)value; break;
						case "PrevBalanceDeconDrying": this.str.PrevBalanceDeconDrying = (string)value; break;
						case "PrevBalanceFeasibilityTest": this.str.PrevBalanceFeasibilityTest = (string)value; break;
						case "PrevBalancePackaging": this.str.PrevBalancePackaging = (string)value; break;
						case "PrevBalanceUltrasound": this.str.PrevBalanceUltrasound = (string)value; break;
						case "PrevBalanceSterilization": this.str.PrevBalanceSterilization = (string)value; break;
						case "PrevBalanceDistribution": this.str.PrevBalanceDistribution = (string)value; break;
						case "PrevBalanceReturned": this.str.PrevBalanceReturned = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PageNo":

							if (value == null || value is System.Int32)
								this.PageNo = (System.Int32?)value;
							break;
						case "Balance":

							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
							break;
						case "BalanceReceived":

							if (value == null || value is System.Decimal)
								this.BalanceReceived = (System.Decimal?)value;
							break;
						case "BalanceDeconImmersion":

							if (value == null || value is System.Decimal)
								this.BalanceDeconImmersion = (System.Decimal?)value;
							break;
						case "BalanceDeconAbstersion":

							if (value == null || value is System.Decimal)
								this.BalanceDeconAbstersion = (System.Decimal?)value;
							break;
						case "BalanceDeconDrying":

							if (value == null || value is System.Decimal)
								this.BalanceDeconDrying = (System.Decimal?)value;
							break;
						case "BalanceFeasibilityTest":

							if (value == null || value is System.Decimal)
								this.BalanceFeasibilityTest = (System.Decimal?)value;
							break;
						case "BalancePackaging":

							if (value == null || value is System.Decimal)
								this.BalancePackaging = (System.Decimal?)value;
							break;
						case "BalanceUltrasound":

							if (value == null || value is System.Decimal)
								this.BalanceUltrasound = (System.Decimal?)value;
							break;
						case "BalanceSterilization":

							if (value == null || value is System.Decimal)
								this.BalanceSterilization = (System.Decimal?)value;
							break;
						case "BalanceDistribution":

							if (value == null || value is System.Decimal)
								this.BalanceDistribution = (System.Decimal?)value;
							break;
						case "BalanceReturned":

							if (value == null || value is System.Decimal)
								this.BalanceReturned = (System.Decimal?)value;
							break;
						case "PrevBalance":

							if (value == null || value is System.Decimal)
								this.PrevBalance = (System.Decimal?)value;
							break;
						case "PrevBalanceReceived":

							if (value == null || value is System.Decimal)
								this.PrevBalanceReceived = (System.Decimal?)value;
							break;
						case "PrevBalanceDeconImmersion":

							if (value == null || value is System.Decimal)
								this.PrevBalanceDeconImmersion = (System.Decimal?)value;
							break;
						case "PrevBalanceDeconAbstersion":

							if (value == null || value is System.Decimal)
								this.PrevBalanceDeconAbstersion = (System.Decimal?)value;
							break;
						case "PrevBalanceDeconDrying":

							if (value == null || value is System.Decimal)
								this.PrevBalanceDeconDrying = (System.Decimal?)value;
							break;
						case "PrevBalanceFeasibilityTest":

							if (value == null || value is System.Decimal)
								this.PrevBalanceFeasibilityTest = (System.Decimal?)value;
							break;
						case "PrevBalancePackaging":

							if (value == null || value is System.Decimal)
								this.PrevBalancePackaging = (System.Decimal?)value;
							break;
						case "PrevBalanceUltrasound":

							if (value == null || value is System.Decimal)
								this.PrevBalanceUltrasound = (System.Decimal?)value;
							break;
						case "PrevBalanceSterilization":

							if (value == null || value is System.Decimal)
								this.PrevBalanceSterilization = (System.Decimal?)value;
							break;
						case "PrevBalanceDistribution":

							if (value == null || value is System.Decimal)
								this.PrevBalanceDistribution = (System.Decimal?)value;
							break;
						case "PrevBalanceReturned":

							if (value == null || value is System.Decimal)
								this.PrevBalanceReturned = (System.Decimal?)value;
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
		/// Maps to CssdStockOpnameItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CssdStockOpnameItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CssdStockOpnameItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(CssdStockOpnameItemMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(CssdStockOpnameItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PageNo
		/// </summary>
		virtual public System.Int32? PageNo
		{
			get
			{
				return base.GetSystemInt32(CssdStockOpnameItemMetadata.ColumnNames.PageNo);
			}

			set
			{
				base.SetSystemInt32(CssdStockOpnameItemMetadata.ColumnNames.PageNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CssdStockOpnameItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(CssdStockOpnameItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceReceived
		/// </summary>
		virtual public System.Decimal? BalanceReceived
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceReceived);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceReceived, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceDeconImmersion
		/// </summary>
		virtual public System.Decimal? BalanceDeconImmersion
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconImmersion);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconImmersion, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceDeconAbstersion
		/// </summary>
		virtual public System.Decimal? BalanceDeconAbstersion
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconAbstersion);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconAbstersion, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceDeconDrying
		/// </summary>
		virtual public System.Decimal? BalanceDeconDrying
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconDrying);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconDrying, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceFeasibilityTest
		/// </summary>
		virtual public System.Decimal? BalanceFeasibilityTest
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceFeasibilityTest);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceFeasibilityTest, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalancePackaging
		/// </summary>
		virtual public System.Decimal? BalancePackaging
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalancePackaging);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalancePackaging, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceUltrasound
		/// </summary>
		virtual public System.Decimal? BalanceUltrasound
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceUltrasound);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceUltrasound, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceSterilization
		/// </summary>
		virtual public System.Decimal? BalanceSterilization
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceSterilization);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceSterilization, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceDistribution
		/// </summary>
		virtual public System.Decimal? BalanceDistribution
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDistribution);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceDistribution, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.BalanceReturned
		/// </summary>
		virtual public System.Decimal? BalanceReturned
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceReturned);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.BalanceReturned, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalance
		/// </summary>
		virtual public System.Decimal? PrevBalance
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalance);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalance, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceReceived
		/// </summary>
		virtual public System.Decimal? PrevBalanceReceived
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReceived);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReceived, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceDeconImmersion
		/// </summary>
		virtual public System.Decimal? PrevBalanceDeconImmersion
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconImmersion);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconImmersion, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceDeconAbstersion
		/// </summary>
		virtual public System.Decimal? PrevBalanceDeconAbstersion
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconAbstersion);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconAbstersion, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceDeconDrying
		/// </summary>
		virtual public System.Decimal? PrevBalanceDeconDrying
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconDrying);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconDrying, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceFeasibilityTest
		/// </summary>
		virtual public System.Decimal? PrevBalanceFeasibilityTest
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceFeasibilityTest);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceFeasibilityTest, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalancePackaging
		/// </summary>
		virtual public System.Decimal? PrevBalancePackaging
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalancePackaging);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalancePackaging, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceUltrasound
		/// </summary>
		virtual public System.Decimal? PrevBalanceUltrasound
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceUltrasound);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceUltrasound, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceSterilization
		/// </summary>
		virtual public System.Decimal? PrevBalanceSterilization
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceSterilization);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceSterilization, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceDistribution
		/// </summary>
		virtual public System.Decimal? PrevBalanceDistribution
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDistribution);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDistribution, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.PrevBalanceReturned
		/// </summary>
		virtual public System.Decimal? PrevBalanceReturned
		{
			get
			{
				return base.GetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReturned);
			}

			set
			{
				base.SetSystemDecimal(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReturned, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CssdStockOpnameItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CssdStockOpnameItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdStockOpnameItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdStockOpnameItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdStockOpnameItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdStockOpnameItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdStockOpnameItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdStockOpnameItem entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String PageNo
			{
				get
				{
					System.Int32? data = entity.PageNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PageNo = null;
					else entity.PageNo = Convert.ToInt32(value);
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
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceReceived
			{
				get
				{
					System.Decimal? data = entity.BalanceReceived;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceReceived = null;
					else entity.BalanceReceived = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceDeconImmersion
			{
				get
				{
					System.Decimal? data = entity.BalanceDeconImmersion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceDeconImmersion = null;
					else entity.BalanceDeconImmersion = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceDeconAbstersion
			{
				get
				{
					System.Decimal? data = entity.BalanceDeconAbstersion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceDeconAbstersion = null;
					else entity.BalanceDeconAbstersion = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceDeconDrying
			{
				get
				{
					System.Decimal? data = entity.BalanceDeconDrying;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceDeconDrying = null;
					else entity.BalanceDeconDrying = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceFeasibilityTest
			{
				get
				{
					System.Decimal? data = entity.BalanceFeasibilityTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceFeasibilityTest = null;
					else entity.BalanceFeasibilityTest = Convert.ToDecimal(value);
				}
			}
			public System.String BalancePackaging
			{
				get
				{
					System.Decimal? data = entity.BalancePackaging;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalancePackaging = null;
					else entity.BalancePackaging = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceUltrasound
			{
				get
				{
					System.Decimal? data = entity.BalanceUltrasound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceUltrasound = null;
					else entity.BalanceUltrasound = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceSterilization
			{
				get
				{
					System.Decimal? data = entity.BalanceSterilization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceSterilization = null;
					else entity.BalanceSterilization = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceDistribution
			{
				get
				{
					System.Decimal? data = entity.BalanceDistribution;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceDistribution = null;
					else entity.BalanceDistribution = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceReturned
			{
				get
				{
					System.Decimal? data = entity.BalanceReturned;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceReturned = null;
					else entity.BalanceReturned = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalance
			{
				get
				{
					System.Decimal? data = entity.PrevBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalance = null;
					else entity.PrevBalance = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceReceived
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceReceived;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceReceived = null;
					else entity.PrevBalanceReceived = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceDeconImmersion
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceDeconImmersion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceDeconImmersion = null;
					else entity.PrevBalanceDeconImmersion = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceDeconAbstersion
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceDeconAbstersion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceDeconAbstersion = null;
					else entity.PrevBalanceDeconAbstersion = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceDeconDrying
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceDeconDrying;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceDeconDrying = null;
					else entity.PrevBalanceDeconDrying = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceFeasibilityTest
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceFeasibilityTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceFeasibilityTest = null;
					else entity.PrevBalanceFeasibilityTest = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalancePackaging
			{
				get
				{
					System.Decimal? data = entity.PrevBalancePackaging;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalancePackaging = null;
					else entity.PrevBalancePackaging = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceUltrasound
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceUltrasound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceUltrasound = null;
					else entity.PrevBalanceUltrasound = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceSterilization
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceSterilization;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceSterilization = null;
					else entity.PrevBalanceSterilization = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceDistribution
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceDistribution;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceDistribution = null;
					else entity.PrevBalanceDistribution = Convert.ToDecimal(value);
				}
			}
			public System.String PrevBalanceReturned
			{
				get
				{
					System.Decimal? data = entity.PrevBalanceReturned;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrevBalanceReturned = null;
					else entity.PrevBalanceReturned = Convert.ToDecimal(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			private esCssdStockOpnameItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdStockOpnameItemQuery query)
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
				throw new Exception("esCssdStockOpnameItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdStockOpnameItem : esCssdStockOpnameItem
	{
	}

	[Serializable]
	abstract public class esCssdStockOpnameItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdStockOpnameItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem PageNo
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PageNo, esSystemType.Int32);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceReceived
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceReceived, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDeconImmersion
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconImmersion, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDeconAbstersion
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconAbstersion, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDeconDrying
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconDrying, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceFeasibilityTest
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceFeasibilityTest, esSystemType.Decimal);
			}
		}

		public esQueryItem BalancePackaging
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalancePackaging, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceUltrasound
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceUltrasound, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceSterilization
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceSterilization, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDistribution
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceDistribution, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceReturned
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.BalanceReturned, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalance
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalance, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceReceived
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReceived, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceDeconImmersion
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconImmersion, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceDeconAbstersion
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconAbstersion, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceDeconDrying
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconDrying, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceFeasibilityTest
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceFeasibilityTest, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalancePackaging
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalancePackaging, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceUltrasound
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceUltrasound, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceSterilization
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceSterilization, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceDistribution
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDistribution, esSystemType.Decimal);
			}
		}

		public esQueryItem PrevBalanceReturned
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReturned, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdStockOpnameItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdStockOpnameItemCollection")]
	public partial class CssdStockOpnameItemCollection : esCssdStockOpnameItemCollection, IEnumerable<CssdStockOpnameItem>
	{
		public CssdStockOpnameItemCollection()
		{

		}

		public static implicit operator List<CssdStockOpnameItem>(CssdStockOpnameItemCollection coll)
		{
			List<CssdStockOpnameItem> list = new List<CssdStockOpnameItem>();

			foreach (CssdStockOpnameItem emp in coll)
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
				return CssdStockOpnameItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdStockOpnameItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdStockOpnameItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdStockOpnameItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdStockOpnameItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdStockOpnameItemQuery();
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
		public bool Load(CssdStockOpnameItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdStockOpnameItem AddNew()
		{
			CssdStockOpnameItem entity = base.AddNewEntity() as CssdStockOpnameItem;

			return entity;
		}
		public CssdStockOpnameItem FindByPrimaryKey(String transactionNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo) as CssdStockOpnameItem;
		}

		#region IEnumerable< CssdStockOpnameItem> Members

		IEnumerator<CssdStockOpnameItem> IEnumerable<CssdStockOpnameItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdStockOpnameItem;
			}
		}

		#endregion

		private CssdStockOpnameItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdStockOpnameItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdStockOpnameItem ({TransactionNo, SequenceNo})")]
	[Serializable]
	public partial class CssdStockOpnameItem : esCssdStockOpnameItem
	{
		public CssdStockOpnameItem()
		{
		}

		public CssdStockOpnameItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdStockOpnameItemMetadata.Meta();
			}
		}

		override protected esCssdStockOpnameItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdStockOpnameItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdStockOpnameItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdStockOpnameItemQuery();
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
		public bool Load(CssdStockOpnameItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdStockOpnameItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdStockOpnameItemQuery : esCssdStockOpnameItemQuery
	{
		public CssdStockOpnameItemQuery()
		{

		}

		public CssdStockOpnameItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdStockOpnameItemQuery";
		}
	}

	[Serializable]
	public partial class CssdStockOpnameItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdStockOpnameItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PageNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PageNo;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.Balance, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceReceived, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceReceived;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconImmersion, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceDeconImmersion;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconAbstersion, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceDeconAbstersion;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceDeconDrying, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceDeconDrying;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceFeasibilityTest, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceFeasibilityTest;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalancePackaging, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalancePackaging;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceUltrasound, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceUltrasound;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceSterilization, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceSterilization;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceDistribution, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceDistribution;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.BalanceReturned, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.BalanceReturned;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalance, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReceived, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceReceived;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconImmersion, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceDeconImmersion;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconAbstersion, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceDeconAbstersion;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDeconDrying, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceDeconDrying;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceFeasibilityTest, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceFeasibilityTest;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalancePackaging, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalancePackaging;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceUltrasound, 22, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceUltrasound;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceSterilization, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceSterilization;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceDistribution, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceDistribution;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.PrevBalanceReturned, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.PrevBalanceReturned;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.Notes, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.LastUpdateDateTime, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdStockOpnameItemMetadata.ColumnNames.LastUpdateByUserID, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdStockOpnameItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdStockOpnameItemMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string PageNo = "PageNo";
			public const string ItemID = "ItemID";
			public const string Balance = "Balance";
			public const string BalanceReceived = "BalanceReceived";
			public const string BalanceDeconImmersion = "BalanceDeconImmersion";
			public const string BalanceDeconAbstersion = "BalanceDeconAbstersion";
			public const string BalanceDeconDrying = "BalanceDeconDrying";
			public const string BalanceFeasibilityTest = "BalanceFeasibilityTest";
			public const string BalancePackaging = "BalancePackaging";
			public const string BalanceUltrasound = "BalanceUltrasound";
			public const string BalanceSterilization = "BalanceSterilization";
			public const string BalanceDistribution = "BalanceDistribution";
			public const string BalanceReturned = "BalanceReturned";
			public const string PrevBalance = "PrevBalance";
			public const string PrevBalanceReceived = "PrevBalanceReceived";
			public const string PrevBalanceDeconImmersion = "PrevBalanceDeconImmersion";
			public const string PrevBalanceDeconAbstersion = "PrevBalanceDeconAbstersion";
			public const string PrevBalanceDeconDrying = "PrevBalanceDeconDrying";
			public const string PrevBalanceFeasibilityTest = "PrevBalanceFeasibilityTest";
			public const string PrevBalancePackaging = "PrevBalancePackaging";
			public const string PrevBalanceUltrasound = "PrevBalanceUltrasound";
			public const string PrevBalanceSterilization = "PrevBalanceSterilization";
			public const string PrevBalanceDistribution = "PrevBalanceDistribution";
			public const string PrevBalanceReturned = "PrevBalanceReturned";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string PageNo = "PageNo";
			public const string ItemID = "ItemID";
			public const string Balance = "Balance";
			public const string BalanceReceived = "BalanceReceived";
			public const string BalanceDeconImmersion = "BalanceDeconImmersion";
			public const string BalanceDeconAbstersion = "BalanceDeconAbstersion";
			public const string BalanceDeconDrying = "BalanceDeconDrying";
			public const string BalanceFeasibilityTest = "BalanceFeasibilityTest";
			public const string BalancePackaging = "BalancePackaging";
			public const string BalanceUltrasound = "BalanceUltrasound";
			public const string BalanceSterilization = "BalanceSterilization";
			public const string BalanceDistribution = "BalanceDistribution";
			public const string BalanceReturned = "BalanceReturned";
			public const string PrevBalance = "PrevBalance";
			public const string PrevBalanceReceived = "PrevBalanceReceived";
			public const string PrevBalanceDeconImmersion = "PrevBalanceDeconImmersion";
			public const string PrevBalanceDeconAbstersion = "PrevBalanceDeconAbstersion";
			public const string PrevBalanceDeconDrying = "PrevBalanceDeconDrying";
			public const string PrevBalanceFeasibilityTest = "PrevBalanceFeasibilityTest";
			public const string PrevBalancePackaging = "PrevBalancePackaging";
			public const string PrevBalanceUltrasound = "PrevBalanceUltrasound";
			public const string PrevBalanceSterilization = "PrevBalanceSterilization";
			public const string PrevBalanceDistribution = "PrevBalanceDistribution";
			public const string PrevBalanceReturned = "PrevBalanceReturned";
			public const string Notes = "Notes";
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
			lock (typeof(CssdStockOpnameItemMetadata))
			{
				if (CssdStockOpnameItemMetadata.mapDelegates == null)
				{
					CssdStockOpnameItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdStockOpnameItemMetadata.meta == null)
				{
					CssdStockOpnameItemMetadata.meta = new CssdStockOpnameItemMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PageNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceReceived", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceDeconImmersion", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceDeconAbstersion", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceDeconDrying", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceFeasibilityTest", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalancePackaging", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceUltrasound", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceSterilization", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceDistribution", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceReturned", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceReceived", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceDeconImmersion", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceDeconAbstersion", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceDeconDrying", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceFeasibilityTest", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalancePackaging", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceUltrasound", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceSterilization", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceDistribution", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PrevBalanceReturned", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdStockOpnameItem";
				meta.Destination = "CssdStockOpnameItem";
				meta.spInsert = "proc_CssdStockOpnameItemInsert";
				meta.spUpdate = "proc_CssdStockOpnameItemUpdate";
				meta.spDelete = "proc_CssdStockOpnameItemDelete";
				meta.spLoadAll = "proc_CssdStockOpnameItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdStockOpnameItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdStockOpnameItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
