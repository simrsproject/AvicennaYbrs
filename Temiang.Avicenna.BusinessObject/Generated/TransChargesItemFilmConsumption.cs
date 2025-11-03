/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/20/2020 10:57:30 AM
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
	abstract public class esTransChargesItemFilmConsumptionCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesItemFilmConsumptionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransChargesItemFilmConsumptionCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesItemFilmConsumptionQuery query)
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
			this.InitQuery(query as esTransChargesItemFilmConsumptionQuery);
		}
		#endregion

		virtual public TransChargesItemFilmConsumption DetachEntity(TransChargesItemFilmConsumption entity)
		{
			return base.DetachEntity(entity) as TransChargesItemFilmConsumption;
		}

		virtual public TransChargesItemFilmConsumption AttachEntity(TransChargesItemFilmConsumption entity)
		{
			return base.AttachEntity(entity) as TransChargesItemFilmConsumption;
		}

		virtual public void Combine(TransChargesItemFilmConsumptionCollection collection)
		{
			base.Combine(collection);
		}

		new public TransChargesItemFilmConsumption this[int index]
		{
			get
			{
				return base[index] as TransChargesItemFilmConsumption;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesItemFilmConsumption);
		}
	}

	[Serializable]
	abstract public class esTransChargesItemFilmConsumption : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesItemFilmConsumptionQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesItemFilmConsumption()
		{
		}

		public esTransChargesItemFilmConsumption(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, String sRFilmID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, sRFilmID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, sRFilmID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, String sRFilmID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, sRFilmID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, sRFilmID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, String sRFilmID)
		{
			esTransChargesItemFilmConsumptionQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.SRFilmID == sRFilmID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, String sRFilmID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SequenceNo", sequenceNo);
			parms.Add("SRFilmID", sRFilmID);
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
						case "SRFilmID": this.str.SRFilmID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Kv": this.str.Kv = (string)value; break;
						case "Ma": this.str.Ma = (string)value; break;
						case "S": this.str.S = (string)value; break;
						case "Mas": this.str.Mas = (string)value; break;
						case "KvC": this.str.KvC = (string)value; break;
						case "MaC": this.str.MaC = (string)value; break;
						case "SC": this.str.SC = (string)value; break;
						case "MasC": this.str.MasC = (string)value; break;
						case "Ffd": this.str.Ffd = (string)value; break;
						case "ScreeningTime": this.str.ScreeningTime = (string)value; break;
						case "CineTime": this.str.CineTime = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "Kv":

							if (value == null || value is System.Decimal)
								this.Kv = (System.Decimal?)value;
							break;
						case "Ma":

							if (value == null || value is System.Decimal)
								this.Ma = (System.Decimal?)value;
							break;
						case "S":

							if (value == null || value is System.Decimal)
								this.S = (System.Decimal?)value;
							break;
						case "Mas":

							if (value == null || value is System.Decimal)
								this.Mas = (System.Decimal?)value;
							break;
						case "Kv_c":

							if (value == null || value is System.Decimal)
								this.KvC = (System.Decimal?)value;
							break;
						case "Ma_c":

							if (value == null || value is System.Decimal)
								this.MaC = (System.Decimal?)value;
							break;
						case "S_c":

							if (value == null || value is System.Decimal)
								this.SC = (System.Decimal?)value;
							break;
						case "Mas_c":

							if (value == null || value is System.Decimal)
								this.MasC = (System.Decimal?)value;
							break;
						case "Ffd":

							if (value == null || value is System.Decimal)
								this.Ffd = (System.Decimal?)value;
							break;
						case "ScreeningTime":

							if (value == null || value is System.Decimal)
								this.ScreeningTime = (System.Decimal?)value;
							break;
						case "CineTime":

							if (value == null || value is System.Decimal)
								this.CineTime = (System.Decimal?)value;
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
		/// Maps to TransChargesItemFilmConsumption.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.SRFilmID
		/// </summary>
		virtual public System.String SRFilmID
		{
			get
			{
				return base.GetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.SRFilmID);
			}

			set
			{
				base.SetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.SRFilmID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Kv
		/// </summary>
		virtual public System.Decimal? Kv
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Kv);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Kv, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Ma
		/// </summary>
		virtual public System.Decimal? Ma
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Ma);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Ma, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.S
		/// </summary>
		virtual public System.Decimal? S
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.S);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.S, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Mas
		/// </summary>
		virtual public System.Decimal? Mas
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Mas);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Mas, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Kv_c
		/// </summary>
		virtual public System.Decimal? KvC
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.KvC);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.KvC, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Ma_c
		/// </summary>
		virtual public System.Decimal? MaC
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.MaC);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.MaC, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.S_c
		/// </summary>
		virtual public System.Decimal? SC
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.SC);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.SC, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Mas_c
		/// </summary>
		virtual public System.Decimal? MasC
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.MasC);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.MasC, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Ffd
		/// </summary>
		virtual public System.Decimal? Ffd
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Ffd);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.Ffd, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.ScreeningTime
		/// </summary>
		virtual public System.Decimal? ScreeningTime
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.ScreeningTime);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.ScreeningTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.CineTime
		/// </summary>
		virtual public System.Decimal? CineTime
		{
			get
			{
				return base.GetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.CineTime);
			}

			set
			{
				base.SetSystemDecimal(TransChargesItemFilmConsumptionMetadata.ColumnNames.CineTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesItemFilmConsumption.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(TransChargesItemFilmConsumptionMetadata.ColumnNames.Notes, value);
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
			public esStrings(esTransChargesItemFilmConsumption entity)
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
			public System.String SRFilmID
			{
				get
				{
					System.String data = entity.SRFilmID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFilmID = null;
					else entity.SRFilmID = Convert.ToString(value);
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
			public System.String Kv
			{
				get
				{
					System.Decimal? data = entity.Kv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kv = null;
					else entity.Kv = Convert.ToDecimal(value);
				}
			}
			public System.String Ma
			{
				get
				{
					System.Decimal? data = entity.Ma;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ma = null;
					else entity.Ma = Convert.ToDecimal(value);
				}
			}
			public System.String S
			{
				get
				{
					System.Decimal? data = entity.S;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.S = null;
					else entity.S = Convert.ToDecimal(value);
				}
			}
			public System.String Mas
			{
				get
				{
					System.Decimal? data = entity.Mas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Mas = null;
					else entity.Mas = Convert.ToDecimal(value);
				}
			}
			public System.String KvC
			{
				get
				{
					System.Decimal? data = entity.KvC;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KvC = null;
					else entity.KvC = Convert.ToDecimal(value);
				}
			}
			public System.String MaC
			{
				get
				{
					System.Decimal? data = entity.MaC;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaC = null;
					else entity.MaC = Convert.ToDecimal(value);
				}
			}
			public System.String SC
			{
				get
				{
					System.Decimal? data = entity.SC;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SC = null;
					else entity.SC = Convert.ToDecimal(value);
				}
			}
			public System.String MasC
			{
				get
				{
					System.Decimal? data = entity.MasC;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MasC = null;
					else entity.MasC = Convert.ToDecimal(value);
				}
			}
			public System.String Ffd
			{
				get
				{
					System.Decimal? data = entity.Ffd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Ffd = null;
					else entity.Ffd = Convert.ToDecimal(value);
				}
			}
			public System.String ScreeningTime
			{
				get
				{
					System.Decimal? data = entity.ScreeningTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScreeningTime = null;
					else entity.ScreeningTime = Convert.ToDecimal(value);
				}
			}
			public System.String CineTime
			{
				get
				{
					System.Decimal? data = entity.CineTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CineTime = null;
					else entity.CineTime = Convert.ToDecimal(value);
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
			private esTransChargesItemFilmConsumption entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesItemFilmConsumptionQuery query)
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
				throw new Exception("esTransChargesItemFilmConsumption can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransChargesItemFilmConsumption : esTransChargesItemFilmConsumption
	{
	}

	[Serializable]
	abstract public class esTransChargesItemFilmConsumptionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemFilmConsumptionMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem SRFilmID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.SRFilmID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Kv
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.Kv, esSystemType.Decimal);
			}
		}

		public esQueryItem Ma
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.Ma, esSystemType.Decimal);
			}
		}

		public esQueryItem S
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.S, esSystemType.Decimal);
			}
		}

		public esQueryItem Mas
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.Mas, esSystemType.Decimal);
			}
		}

		public esQueryItem KvC
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.KvC, esSystemType.Decimal);
			}
		}

		public esQueryItem MaC
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.MaC, esSystemType.Decimal);
			}
		}

		public esQueryItem SC
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.SC, esSystemType.Decimal);
			}
		}

		public esQueryItem MasC
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.MasC, esSystemType.Decimal);
			}
		}

		public esQueryItem Ffd
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.Ffd, esSystemType.Decimal);
			}
		}

		public esQueryItem ScreeningTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.ScreeningTime, esSystemType.Decimal);
			}
		}

		public esQueryItem CineTime
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.CineTime, esSystemType.Decimal);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, TransChargesItemFilmConsumptionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesItemFilmConsumptionCollection")]
	public partial class TransChargesItemFilmConsumptionCollection : esTransChargesItemFilmConsumptionCollection, IEnumerable<TransChargesItemFilmConsumption>
	{
		public TransChargesItemFilmConsumptionCollection()
		{

		}

		public static implicit operator List<TransChargesItemFilmConsumption>(TransChargesItemFilmConsumptionCollection coll)
		{
			List<TransChargesItemFilmConsumption> list = new List<TransChargesItemFilmConsumption>();

			foreach (TransChargesItemFilmConsumption emp in coll)
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
				return TransChargesItemFilmConsumptionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemFilmConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesItemFilmConsumption(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesItemFilmConsumption();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransChargesItemFilmConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemFilmConsumptionQuery();
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
		public bool Load(TransChargesItemFilmConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransChargesItemFilmConsumption AddNew()
		{
			TransChargesItemFilmConsumption entity = base.AddNewEntity() as TransChargesItemFilmConsumption;

			return entity;
		}
		public TransChargesItemFilmConsumption FindByPrimaryKey(String transactionNo, String sequenceNo, String sRFilmID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, sRFilmID) as TransChargesItemFilmConsumption;
		}

		#region IEnumerable< TransChargesItemFilmConsumption> Members

		IEnumerator<TransChargesItemFilmConsumption> IEnumerable<TransChargesItemFilmConsumption>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesItemFilmConsumption;
			}
		}

		#endregion

		private TransChargesItemFilmConsumptionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesItemFilmConsumption' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransChargesItemFilmConsumption ({TransactionNo, SequenceNo, SRFilmID})")]
	[Serializable]
	public partial class TransChargesItemFilmConsumption : esTransChargesItemFilmConsumption
	{
		public TransChargesItemFilmConsumption()
		{
		}

		public TransChargesItemFilmConsumption(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesItemFilmConsumptionMetadata.Meta();
			}
		}

		override protected esTransChargesItemFilmConsumptionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesItemFilmConsumptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransChargesItemFilmConsumptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesItemFilmConsumptionQuery();
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
		public bool Load(TransChargesItemFilmConsumptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransChargesItemFilmConsumptionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransChargesItemFilmConsumptionQuery : esTransChargesItemFilmConsumptionQuery
	{
		public TransChargesItemFilmConsumptionQuery()
		{

		}

		public TransChargesItemFilmConsumptionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransChargesItemFilmConsumptionQuery";
		}
	}

	[Serializable]
	public partial class TransChargesItemFilmConsumptionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesItemFilmConsumptionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 9;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.SRFilmID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.SRFilmID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.Kv, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.Kv;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.Ma, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.Ma;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.S, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.S;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.Mas, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.Mas;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.KvC, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.KvC;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.MaC, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.MaC;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.SC, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.SC;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.MasC, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.MasC;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.Ffd, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.Ffd;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.ScreeningTime, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.ScreeningTime;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.CineTime, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.CineTime;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesItemFilmConsumptionMetadata.ColumnNames.Notes, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesItemFilmConsumptionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransChargesItemFilmConsumptionMetadata Meta()
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
			public const string SRFilmID = "SRFilmID";
			public const string Qty = "Qty";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Kv = "Kv";
			public const string Ma = "Ma";
			public const string S = "S";
			public const string Mas = "Mas";
			public const string KvC = "Kv_c";
			public const string MaC = "Ma_c";
			public const string SC = "S_c";
			public const string MasC = "Mas_c";
			public const string Ffd = "Ffd";
			public const string ScreeningTime = "ScreeningTime";
			public const string CineTime = "CineTime";
			public const string Notes = "Notes";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string SRFilmID = "SRFilmID";
			public const string Qty = "Qty";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Kv = "Kv";
			public const string Ma = "Ma";
			public const string S = "S";
			public const string Mas = "Mas";
			public const string KvC = "KvC";
			public const string MaC = "MaC";
			public const string SC = "SC";
			public const string MasC = "MasC";
			public const string Ffd = "Ffd";
			public const string ScreeningTime = "ScreeningTime";
			public const string CineTime = "CineTime";
			public const string Notes = "Notes";
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
			lock (typeof(TransChargesItemFilmConsumptionMetadata))
			{
				if (TransChargesItemFilmConsumptionMetadata.mapDelegates == null)
				{
					TransChargesItemFilmConsumptionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransChargesItemFilmConsumptionMetadata.meta == null)
				{
					TransChargesItemFilmConsumptionMetadata.meta = new TransChargesItemFilmConsumptionMetadata();
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
				meta.AddTypeMap("SRFilmID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kv", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Ma", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("S", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Mas", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("KvC", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaC", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SC", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MasC", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Ffd", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ScreeningTime", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("CineTime", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));


				meta.Source = "TransChargesItemFilmConsumption";
				meta.Destination = "TransChargesItemFilmConsumption";
				meta.spInsert = "proc_TransChargesItemFilmConsumptionInsert";
				meta.spUpdate = "proc_TransChargesItemFilmConsumptionUpdate";
				meta.spDelete = "proc_TransChargesItemFilmConsumptionDelete";
				meta.spLoadAll = "proc_TransChargesItemFilmConsumptionLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesItemFilmConsumptionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesItemFilmConsumptionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
