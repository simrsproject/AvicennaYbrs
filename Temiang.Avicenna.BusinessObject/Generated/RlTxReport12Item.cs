/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/5/2020 2:12:31 PM
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
	abstract public class esRlTxReport12ItemCollection : esEntityCollectionWAuditLog
	{
		public esRlTxReport12ItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RlTxReport12ItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport12ItemQuery query)
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
			this.InitQuery(query as esRlTxReport12ItemQuery);
		}
		#endregion

		virtual public RlTxReport12Item DetachEntity(RlTxReport12Item entity)
		{
			return base.DetachEntity(entity) as RlTxReport12Item;
		}

		virtual public RlTxReport12Item AttachEntity(RlTxReport12Item entity)
		{
			return base.AttachEntity(entity) as RlTxReport12Item;
		}

		virtual public void Combine(RlTxReport12ItemCollection collection)
		{
			base.Combine(collection);
		}

		new public RlTxReport12Item this[int index]
		{
			get
			{
				return base[index] as RlTxReport12Item;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport12Item);
		}
	}

	[Serializable]
	abstract public class esRlTxReport12Item : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport12ItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport12Item()
		{
		}

		public esRlTxReport12Item(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String periodMonth, String periodYear)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodMonth, periodYear);
			else
				return LoadByPrimaryKeyStoredProcedure(periodMonth, periodYear);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String periodMonth, String periodYear)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(periodMonth, periodYear);
			else
				return LoadByPrimaryKeyStoredProcedure(periodMonth, periodYear);
		}

		private bool LoadByPrimaryKeyDynamic(String periodMonth, String periodYear)
		{
			esRlTxReport12ItemQuery query = this.GetDynamicQuery();
			query.Where(query.PeriodMonth == periodMonth, query.PeriodYear == periodYear);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String periodMonth, String periodYear)
		{
			esParameters parms = new esParameters();
			parms.Add("PeriodMonth", periodMonth);
			parms.Add("PeriodYear", periodYear);
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
						case "PeriodMonth": this.str.PeriodMonth = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "HariPerawatan": this.str.HariPerawatan = (string)value; break;
						case "LamaDirawat": this.str.LamaDirawat = (string)value; break;
						case "Keluar": this.str.Keluar = (string)value; break;
						case "KeluarMati48": this.str.KeluarMati48 = (string)value; break;
						case "KeluarMati": this.str.KeluarMati = (string)value; break;
						case "Tt": this.str.Tt = (string)value; break;
						case "HariDlmSatuPeriode": this.str.HariDlmSatuPeriode = (string)value; break;
						case "Kunjungan": this.str.Kunjungan = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "JTt": this.str.JTt = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "HariPerawatan":

							if (value == null || value is System.Int32)
								this.HariPerawatan = (System.Int32?)value;
							break;
						case "LamaDirawat":

							if (value == null || value is System.Int32)
								this.LamaDirawat = (System.Int32?)value;
							break;
						case "Keluar":

							if (value == null || value is System.Int32)
								this.Keluar = (System.Int32?)value;
							break;
						case "KeluarMati48":

							if (value == null || value is System.Int32)
								this.KeluarMati48 = (System.Int32?)value;
							break;
						case "KeluarMati":

							if (value == null || value is System.Int32)
								this.KeluarMati = (System.Int32?)value;
							break;
						case "Tt":

							if (value == null || value is System.Int32)
								this.Tt = (System.Int32?)value;
							break;
						case "HariDlmSatuPeriode":

							if (value == null || value is System.Int32)
								this.HariDlmSatuPeriode = (System.Int32?)value;
							break;
						case "Kunjungan":

							if (value == null || value is System.Int32)
								this.Kunjungan = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "JTt":

							if (value == null || value is System.Int32)
								this.JTt = (System.Int32?)value;
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
		/// Maps to RlTxReport12Item.PeriodMonth
		/// </summary>
		virtual public System.String PeriodMonth
		{
			get
			{
				return base.GetSystemString(RlTxReport12ItemMetadata.ColumnNames.PeriodMonth);
			}

			set
			{
				base.SetSystemString(RlTxReport12ItemMetadata.ColumnNames.PeriodMonth, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(RlTxReport12ItemMetadata.ColumnNames.PeriodYear);
			}

			set
			{
				base.SetSystemString(RlTxReport12ItemMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.HariPerawatan
		/// </summary>
		virtual public System.Int32? HariPerawatan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.HariPerawatan);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.HariPerawatan, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.LamaDirawat
		/// </summary>
		virtual public System.Int32? LamaDirawat
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.LamaDirawat);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.LamaDirawat, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.Keluar
		/// </summary>
		virtual public System.Int32? Keluar
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.Keluar);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.Keluar, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.KeluarMati48
		/// </summary>
		virtual public System.Int32? KeluarMati48
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.KeluarMati48);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.KeluarMati48, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.KeluarMati
		/// </summary>
		virtual public System.Int32? KeluarMati
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.KeluarMati);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.KeluarMati, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.Tt
		/// </summary>
		virtual public System.Int32? Tt
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.Tt);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.Tt, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.HariDlmSatuPeriode
		/// </summary>
		virtual public System.Int32? HariDlmSatuPeriode
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.HariDlmSatuPeriode);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.HariDlmSatuPeriode, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.Kunjungan
		/// </summary>
		virtual public System.Int32? Kunjungan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.Kunjungan);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.Kunjungan, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport12ItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RlTxReport12ItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport12ItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RlTxReport12ItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RlTxReport12Item.JTt
		/// </summary>
		virtual public System.Int32? JTt
		{
			get
			{
				return base.GetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.JTt);
			}

			set
			{
				base.SetSystemInt32(RlTxReport12ItemMetadata.ColumnNames.JTt, value);
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
			public esStrings(esRlTxReport12Item entity)
			{
				this.entity = entity;
			}
			public System.String PeriodMonth
			{
				get
				{
					System.String data = entity.PeriodMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonth = null;
					else entity.PeriodMonth = Convert.ToString(value);
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
			public System.String HariPerawatan
			{
				get
				{
					System.Int32? data = entity.HariPerawatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HariPerawatan = null;
					else entity.HariPerawatan = Convert.ToInt32(value);
				}
			}
			public System.String LamaDirawat
			{
				get
				{
					System.Int32? data = entity.LamaDirawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LamaDirawat = null;
					else entity.LamaDirawat = Convert.ToInt32(value);
				}
			}
			public System.String Keluar
			{
				get
				{
					System.Int32? data = entity.Keluar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Keluar = null;
					else entity.Keluar = Convert.ToInt32(value);
				}
			}
			public System.String KeluarMati48
			{
				get
				{
					System.Int32? data = entity.KeluarMati48;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeluarMati48 = null;
					else entity.KeluarMati48 = Convert.ToInt32(value);
				}
			}
			public System.String KeluarMati
			{
				get
				{
					System.Int32? data = entity.KeluarMati;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeluarMati = null;
					else entity.KeluarMati = Convert.ToInt32(value);
				}
			}
			public System.String Tt
			{
				get
				{
					System.Int32? data = entity.Tt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tt = null;
					else entity.Tt = Convert.ToInt32(value);
				}
			}
			public System.String HariDlmSatuPeriode
			{
				get
				{
					System.Int32? data = entity.HariDlmSatuPeriode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HariDlmSatuPeriode = null;
					else entity.HariDlmSatuPeriode = Convert.ToInt32(value);
				}
			}
			public System.String Kunjungan
			{
				get
				{
					System.Int32? data = entity.Kunjungan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kunjungan = null;
					else entity.Kunjungan = Convert.ToInt32(value);
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
			public System.String JTt
			{
				get
				{
					System.Int32? data = entity.JTt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JTt = null;
					else entity.JTt = Convert.ToInt32(value);
				}
			}
			private esRlTxReport12Item entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport12ItemQuery query)
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
				throw new Exception("esRlTxReport12Item can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RlTxReport12Item : esRlTxReport12Item
	{
	}

	[Serializable]
	abstract public class esRlTxReport12ItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport12ItemMetadata.Meta();
			}
		}

		public esQueryItem PeriodMonth
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.PeriodMonth, esSystemType.String);
			}
		}

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		}

		public esQueryItem HariPerawatan
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.HariPerawatan, esSystemType.Int32);
			}
		}

		public esQueryItem LamaDirawat
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.LamaDirawat, esSystemType.Int32);
			}
		}

		public esQueryItem Keluar
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.Keluar, esSystemType.Int32);
			}
		}

		public esQueryItem KeluarMati48
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.KeluarMati48, esSystemType.Int32);
			}
		}

		public esQueryItem KeluarMati
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.KeluarMati, esSystemType.Int32);
			}
		}

		public esQueryItem Tt
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.Tt, esSystemType.Int32);
			}
		}

		public esQueryItem HariDlmSatuPeriode
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.HariDlmSatuPeriode, esSystemType.Int32);
			}
		}

		public esQueryItem Kunjungan
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.Kunjungan, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem JTt
		{
			get
			{
				return new esQueryItem(this, RlTxReport12ItemMetadata.ColumnNames.JTt, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport12ItemCollection")]
	public partial class RlTxReport12ItemCollection : esRlTxReport12ItemCollection, IEnumerable<RlTxReport12Item>
	{
		public RlTxReport12ItemCollection()
		{

		}

		public static implicit operator List<RlTxReport12Item>(RlTxReport12ItemCollection coll)
		{
			List<RlTxReport12Item> list = new List<RlTxReport12Item>();

			foreach (RlTxReport12Item emp in coll)
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
				return RlTxReport12ItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport12ItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport12Item(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport12Item();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RlTxReport12ItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport12ItemQuery();
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
		public bool Load(RlTxReport12ItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RlTxReport12Item AddNew()
		{
			RlTxReport12Item entity = base.AddNewEntity() as RlTxReport12Item;

			return entity;
		}
		public RlTxReport12Item FindByPrimaryKey(String periodMonth, String periodYear)
		{
			return base.FindByPrimaryKey(periodMonth, periodYear) as RlTxReport12Item;
		}

		#region IEnumerable< RlTxReport12Item> Members

		IEnumerator<RlTxReport12Item> IEnumerable<RlTxReport12Item>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport12Item;
			}
		}

		#endregion

		private RlTxReport12ItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport12Item' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RlTxReport12Item ({PeriodMonth, PeriodYear})")]
	[Serializable]
	public partial class RlTxReport12Item : esRlTxReport12Item
	{
		public RlTxReport12Item()
		{
		}

		public RlTxReport12Item(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport12ItemMetadata.Meta();
			}
		}

		override protected esRlTxReport12ItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport12ItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RlTxReport12ItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport12ItemQuery();
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
		public bool Load(RlTxReport12ItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RlTxReport12ItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RlTxReport12ItemQuery : esRlTxReport12ItemQuery
	{
		public RlTxReport12ItemQuery()
		{

		}

		public RlTxReport12ItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RlTxReport12ItemQuery";
		}
	}

	[Serializable]
	public partial class RlTxReport12ItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport12ItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.PeriodMonth, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.PeriodMonth;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.PeriodYear, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.HariPerawatan, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.HariPerawatan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.LamaDirawat, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.LamaDirawat;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.Keluar, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.Keluar;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.KeluarMati48, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.KeluarMati48;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.KeluarMati, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.KeluarMati;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.Tt, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.Tt;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.HariDlmSatuPeriode, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.HariDlmSatuPeriode;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.Kunjungan, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.Kunjungan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport12ItemMetadata.ColumnNames.JTt, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport12ItemMetadata.PropertyNames.JTt;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RlTxReport12ItemMetadata Meta()
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
			public const string PeriodMonth = "PeriodMonth";
			public const string PeriodYear = "PeriodYear";
			public const string HariPerawatan = "HariPerawatan";
			public const string LamaDirawat = "LamaDirawat";
			public const string Keluar = "Keluar";
			public const string KeluarMati48 = "KeluarMati48";
			public const string KeluarMati = "KeluarMati";
			public const string Tt = "Tt";
			public const string HariDlmSatuPeriode = "HariDlmSatuPeriode";
			public const string Kunjungan = "Kunjungan";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string JTt = "JTt";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PeriodMonth = "PeriodMonth";
			public const string PeriodYear = "PeriodYear";
			public const string HariPerawatan = "HariPerawatan";
			public const string LamaDirawat = "LamaDirawat";
			public const string Keluar = "Keluar";
			public const string KeluarMati48 = "KeluarMati48";
			public const string KeluarMati = "KeluarMati";
			public const string Tt = "Tt";
			public const string HariDlmSatuPeriode = "HariDlmSatuPeriode";
			public const string Kunjungan = "Kunjungan";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string JTt = "JTt";
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
			lock (typeof(RlTxReport12ItemMetadata))
			{
				if (RlTxReport12ItemMetadata.mapDelegates == null)
				{
					RlTxReport12ItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RlTxReport12ItemMetadata.meta == null)
				{
					RlTxReport12ItemMetadata.meta = new RlTxReport12ItemMetadata();
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

				meta.AddTypeMap("PeriodMonth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HariPerawatan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LamaDirawat", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Keluar", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KeluarMati48", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KeluarMati", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Tt", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HariDlmSatuPeriode", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Kunjungan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JTt", new esTypeMap("int", "System.Int32"));


				meta.Source = "RlTxReport12Item";
				meta.Destination = "RlTxReport12Item";
				meta.spInsert = "proc_RlTxReport12ItemInsert";
				meta.spUpdate = "proc_RlTxReport12ItemUpdate";
				meta.spDelete = "proc_RlTxReport12ItemDelete";
				meta.spLoadAll = "proc_RlTxReport12ItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport12ItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport12ItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
