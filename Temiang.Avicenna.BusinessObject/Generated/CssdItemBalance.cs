/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/29/2023 5:49:23 PM
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
	abstract public class esCssdItemBalanceCollection : esEntityCollectionWAuditLog
	{
		public esCssdItemBalanceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdItemBalanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdItemBalanceQuery query)
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
			this.InitQuery(query as esCssdItemBalanceQuery);
		}
		#endregion

		virtual public CssdItemBalance DetachEntity(CssdItemBalance entity)
		{
			return base.DetachEntity(entity) as CssdItemBalance;
		}

		virtual public CssdItemBalance AttachEntity(CssdItemBalance entity)
		{
			return base.AttachEntity(entity) as CssdItemBalance;
		}

		virtual public void Combine(CssdItemBalanceCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdItemBalance this[int index]
		{
			get
			{
				return base[index] as CssdItemBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdItemBalance);
		}
	}

	[Serializable]
	abstract public class esCssdItemBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdItemBalanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdItemBalance()
		{
		}

		public esCssdItemBalance(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String itemID)
		{
			esCssdItemBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ItemID", itemID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
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
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to CssdItemBalance.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(CssdItemBalanceMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(CssdItemBalanceMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CssdItemBalanceMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(CssdItemBalanceMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceReceived
		/// </summary>
		virtual public System.Decimal? BalanceReceived
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceReceived);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceReceived, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceDeconImmersion
		/// </summary>
		virtual public System.Decimal? BalanceDeconImmersion
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDeconImmersion);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDeconImmersion, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceDeconAbstersion
		/// </summary>
		virtual public System.Decimal? BalanceDeconAbstersion
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDeconAbstersion);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDeconAbstersion, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceDeconDrying
		/// </summary>
		virtual public System.Decimal? BalanceDeconDrying
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDeconDrying);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDeconDrying, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceFeasibilityTest
		/// </summary>
		virtual public System.Decimal? BalanceFeasibilityTest
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceFeasibilityTest);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceFeasibilityTest, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalancePackaging
		/// </summary>
		virtual public System.Decimal? BalancePackaging
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalancePackaging);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalancePackaging, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceUltrasound
		/// </summary>
		virtual public System.Decimal? BalanceUltrasound
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceUltrasound);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceUltrasound, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceSterilization
		/// </summary>
		virtual public System.Decimal? BalanceSterilization
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceSterilization);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceSterilization, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceDistribution
		/// </summary>
		virtual public System.Decimal? BalanceDistribution
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDistribution);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceDistribution, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.BalanceReturned
		/// </summary>
		virtual public System.Decimal? BalanceReturned
		{
			get
			{
				return base.GetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceReturned);
			}

			set
			{
				base.SetSystemDecimal(CssdItemBalanceMetadata.ColumnNames.BalanceReturned, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdItemBalanceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdItemBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdItemBalance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdItemBalanceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdItemBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdItemBalance entity)
			{
				this.entity = entity;
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
			private esCssdItemBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdItemBalanceQuery query)
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
				throw new Exception("esCssdItemBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdItemBalance : esCssdItemBalance
	{
	}

	[Serializable]
	abstract public class esCssdItemBalanceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdItemBalanceMetadata.Meta();
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceReceived
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceReceived, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDeconImmersion
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceDeconImmersion, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDeconAbstersion
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceDeconAbstersion, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDeconDrying
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceDeconDrying, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceFeasibilityTest
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceFeasibilityTest, esSystemType.Decimal);
			}
		}

		public esQueryItem BalancePackaging
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalancePackaging, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceUltrasound
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceUltrasound, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceSterilization
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceSterilization, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceDistribution
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceDistribution, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceReturned
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.BalanceReturned, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdItemBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdItemBalanceCollection")]
	public partial class CssdItemBalanceCollection : esCssdItemBalanceCollection, IEnumerable<CssdItemBalance>
	{
		public CssdItemBalanceCollection()
		{

		}

		public static implicit operator List<CssdItemBalance>(CssdItemBalanceCollection coll)
		{
			List<CssdItemBalance> list = new List<CssdItemBalance>();

			foreach (CssdItemBalance emp in coll)
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
				return CssdItemBalanceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdItemBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdItemBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdItemBalance();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdItemBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdItemBalanceQuery();
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
		public bool Load(CssdItemBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdItemBalance AddNew()
		{
			CssdItemBalance entity = base.AddNewEntity() as CssdItemBalance;

			return entity;
		}
		public CssdItemBalance FindByPrimaryKey(String serviceUnitID, String itemID)
		{
			return base.FindByPrimaryKey(serviceUnitID, itemID) as CssdItemBalance;
		}

		#region IEnumerable< CssdItemBalance> Members

		IEnumerator<CssdItemBalance> IEnumerable<CssdItemBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdItemBalance;
			}
		}

		#endregion

		private CssdItemBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdItemBalance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdItemBalance ({ServiceUnitID, ItemID})")]
	[Serializable]
	public partial class CssdItemBalance : esCssdItemBalance
	{
		public CssdItemBalance()
		{
		}

		public CssdItemBalance(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdItemBalanceMetadata.Meta();
			}
		}

		override protected esCssdItemBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdItemBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdItemBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdItemBalanceQuery();
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
		public bool Load(CssdItemBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdItemBalanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdItemBalanceQuery : esCssdItemBalanceQuery
	{
		public CssdItemBalanceQuery()
		{

		}

		public CssdItemBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdItemBalanceQuery";
		}
	}

	[Serializable]
	public partial class CssdItemBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdItemBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.Balance, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceReceived, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceReceived;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceDeconImmersion, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceDeconImmersion;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceDeconAbstersion, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceDeconAbstersion;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceDeconDrying, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceDeconDrying;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceFeasibilityTest, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceFeasibilityTest;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalancePackaging, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalancePackaging;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceUltrasound, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceUltrasound;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceSterilization, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceSterilization;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceDistribution, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceDistribution;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.BalanceReturned, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.BalanceReturned;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdItemBalanceMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdItemBalanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdItemBalanceMetadata Meta()
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
			public const string ServiceUnitID = "ServiceUnitID";
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
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ServiceUnitID = "ServiceUnitID";
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
			lock (typeof(CssdItemBalanceMetadata))
			{
				if (CssdItemBalanceMetadata.mapDelegates == null)
				{
					CssdItemBalanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdItemBalanceMetadata.meta == null)
				{
					CssdItemBalanceMetadata.meta = new CssdItemBalanceMetadata();
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

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
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
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CssdItemBalance";
				meta.Destination = "CssdItemBalance";
				meta.spInsert = "proc_CssdItemBalanceInsert";
				meta.spUpdate = "proc_CssdItemBalanceUpdate";
				meta.spDelete = "proc_CssdItemBalanceDelete";
				meta.spLoadAll = "proc_CssdItemBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdItemBalanceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdItemBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
