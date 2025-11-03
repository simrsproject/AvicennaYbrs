/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/30/2022 2:51:17 PM
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
	abstract public class esRegistrationGyssensCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationGyssensCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationGyssensCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationGyssensQuery query)
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
			this.InitQuery(query as esRegistrationGyssensQuery);
		}
		#endregion

		virtual public RegistrationGyssens DetachEntity(RegistrationGyssens entity)
		{
			return base.DetachEntity(entity) as RegistrationGyssens;
		}

		virtual public RegistrationGyssens AttachEntity(RegistrationGyssens entity)
		{
			return base.AttachEntity(entity) as RegistrationGyssens;
		}

		virtual public void Combine(RegistrationGyssensCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationGyssens this[int index]
		{
			get
			{
				return base[index] as RegistrationGyssens;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationGyssens);
		}
	}

	[Serializable]
	abstract public class esRegistrationGyssens : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationGyssensQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationGyssens()
		{
		}

		public esRegistrationGyssens(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 seqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 seqNo)
		{
			esRegistrationGyssensQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("SeqNo", seqNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ZatActiveID": this.str.ZatActiveID = (string)value; break;
						case "SRConsumeMethod": this.str.SRConsumeMethod = (string)value; break;
						case "ConsumeQty": this.str.ConsumeQty = (string)value; break;
						case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
						case "RasproSeqNo": this.str.RasproSeqNo = (string)value; break;
						case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
						case "PrescriptionDateStart": this.str.PrescriptionDateStart = (string)value; break;
						case "PrescriptionDateEnd": this.str.PrescriptionDateEnd = (string)value; break;
						case "GyssensCreateDateTime": this.str.GyssensCreateDateTime = (string)value; break;
						case "GyssensCreateByUserID": this.str.GyssensCreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SeqNo":

							if (value == null || value is System.Int32)
								this.SeqNo = (System.Int32?)value;
							break;
						case "RasproSeqNo":

							if (value == null || value is System.Int32)
								this.RasproSeqNo = (System.Int32?)value;
							break;
						case "PrescriptionDateStart":

							if (value == null || value is System.DateTime)
								this.PrescriptionDateStart = (System.DateTime?)value;
							break;
						case "PrescriptionDateEnd":

							if (value == null || value is System.DateTime)
								this.PrescriptionDateEnd = (System.DateTime?)value;
							break;
						case "GyssensCreateDateTime":

							if (value == null || value is System.DateTime)
								this.GyssensCreateDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationGyssens.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.SeqNo
		/// </summary>
		virtual public System.Int32? SeqNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationGyssensMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationGyssensMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.ZatActiveID
		/// </summary>
		virtual public System.String ZatActiveID
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.ZatActiveID);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.ZatActiveID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.SRConsumeMethod
		/// </summary>
		virtual public System.String SRConsumeMethod
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.SRConsumeMethod);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.SRConsumeMethod, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.ConsumeQty
		/// </summary>
		virtual public System.String ConsumeQty
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.ConsumeQty);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.ConsumeQty, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.SRConsumeUnit
		/// </summary>
		virtual public System.String SRConsumeUnit
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.SRConsumeUnit);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.SRConsumeUnit, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.RasproSeqNo
		/// </summary>
		virtual public System.Int32? RasproSeqNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationGyssensMetadata.ColumnNames.RasproSeqNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationGyssensMetadata.ColumnNames.RasproSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.PrescriptionNo
		/// </summary>
		virtual public System.String PrescriptionNo
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.PrescriptionNo);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.PrescriptionNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.PrescriptionDateStart
		/// </summary>
		virtual public System.DateTime? PrescriptionDateStart
		{
			get
			{
				return base.GetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.PrescriptionDateStart);
			}

			set
			{
				base.SetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.PrescriptionDateStart, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.PrescriptionDateEnd
		/// </summary>
		virtual public System.DateTime? PrescriptionDateEnd
		{
			get
			{
				return base.GetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.PrescriptionDateEnd);
			}

			set
			{
				base.SetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.PrescriptionDateEnd, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.GyssensCreateDateTime
		/// </summary>
		virtual public System.DateTime? GyssensCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.GyssensCreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.GyssensCreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.GyssensCreateByUserID
		/// </summary>
		virtual public System.String GyssensCreateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.GyssensCreateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.GyssensCreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationGyssensMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationGyssens.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationGyssensMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationGyssensMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationGyssens entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String SeqNo
			{
				get
				{
					System.Int32? data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToInt32(value);
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
			public System.String ZatActiveID
			{
				get
				{
					System.String data = entity.ZatActiveID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZatActiveID = null;
					else entity.ZatActiveID = Convert.ToString(value);
				}
			}
			public System.String SRConsumeMethod
			{
				get
				{
					System.String data = entity.SRConsumeMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeMethod = null;
					else entity.SRConsumeMethod = Convert.ToString(value);
				}
			}
			public System.String ConsumeQty
			{
				get
				{
					System.String data = entity.ConsumeQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConsumeQty = null;
					else entity.ConsumeQty = Convert.ToString(value);
				}
			}
			public System.String SRConsumeUnit
			{
				get
				{
					System.String data = entity.SRConsumeUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRConsumeUnit = null;
					else entity.SRConsumeUnit = Convert.ToString(value);
				}
			}
			public System.String RasproSeqNo
			{
				get
				{
					System.Int32? data = entity.RasproSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RasproSeqNo = null;
					else entity.RasproSeqNo = Convert.ToInt32(value);
				}
			}
			public System.String PrescriptionNo
			{
				get
				{
					System.String data = entity.PrescriptionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionNo = null;
					else entity.PrescriptionNo = Convert.ToString(value);
				}
			}
			public System.String PrescriptionDateStart
			{
				get
				{
					System.DateTime? data = entity.PrescriptionDateStart;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionDateStart = null;
					else entity.PrescriptionDateStart = Convert.ToDateTime(value);
				}
			}
			public System.String PrescriptionDateEnd
			{
				get
				{
					System.DateTime? data = entity.PrescriptionDateEnd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrescriptionDateEnd = null;
					else entity.PrescriptionDateEnd = Convert.ToDateTime(value);
				}
			}
			public System.String GyssensCreateDateTime
			{
				get
				{
					System.DateTime? data = entity.GyssensCreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GyssensCreateDateTime = null;
					else entity.GyssensCreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String GyssensCreateByUserID
			{
				get
				{
					System.String data = entity.GyssensCreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GyssensCreateByUserID = null;
					else entity.GyssensCreateByUserID = Convert.ToString(value);
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
			private esRegistrationGyssens entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationGyssensQuery query)
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
				throw new Exception("esRegistrationGyssens can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationGyssens : esRegistrationGyssens
	{
	}

	[Serializable]
	abstract public class esRegistrationGyssensQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGyssensMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.SeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ZatActiveID
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.ZatActiveID, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeMethod
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.SRConsumeMethod, esSystemType.String);
			}
		}

		public esQueryItem ConsumeQty
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.ConsumeQty, esSystemType.String);
			}
		}

		public esQueryItem SRConsumeUnit
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
			}
		}

		public esQueryItem RasproSeqNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.RasproSeqNo, esSystemType.Int32);
			}
		}

		public esQueryItem PrescriptionNo
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
			}
		}

		public esQueryItem PrescriptionDateStart
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.PrescriptionDateStart, esSystemType.DateTime);
			}
		}

		public esQueryItem PrescriptionDateEnd
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.PrescriptionDateEnd, esSystemType.DateTime);
			}
		}

		public esQueryItem GyssensCreateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.GyssensCreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem GyssensCreateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.GyssensCreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationGyssensMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationGyssensCollection")]
	public partial class RegistrationGyssensCollection : esRegistrationGyssensCollection, IEnumerable<RegistrationGyssens>
	{
		public RegistrationGyssensCollection()
		{

		}

		public static implicit operator List<RegistrationGyssens>(RegistrationGyssensCollection coll)
		{
			List<RegistrationGyssens> list = new List<RegistrationGyssens>();

			foreach (RegistrationGyssens emp in coll)
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
				return RegistrationGyssensMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGyssensQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationGyssens(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationGyssens();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationGyssensQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGyssensQuery();
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
		public bool Load(RegistrationGyssensQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationGyssens AddNew()
		{
			RegistrationGyssens entity = base.AddNewEntity() as RegistrationGyssens;

			return entity;
		}
		public RegistrationGyssens FindByPrimaryKey(String registrationNo, Int32 seqNo)
		{
			return base.FindByPrimaryKey(registrationNo, seqNo) as RegistrationGyssens;
		}

		#region IEnumerable< RegistrationGyssens> Members

		IEnumerator<RegistrationGyssens> IEnumerable<RegistrationGyssens>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationGyssens;
			}
		}

		#endregion

		private RegistrationGyssensQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationGyssens' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationGyssens ({RegistrationNo, SeqNo})")]
	[Serializable]
	public partial class RegistrationGyssens : esRegistrationGyssens
	{
		public RegistrationGyssens()
		{
		}

		public RegistrationGyssens(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationGyssensMetadata.Meta();
			}
		}

		override protected esRegistrationGyssensQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationGyssensQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationGyssensQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationGyssensQuery();
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
		public bool Load(RegistrationGyssensQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationGyssensQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationGyssensQuery : esRegistrationGyssensQuery
	{
		public RegistrationGyssensQuery()
		{

		}

		public RegistrationGyssensQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationGyssensQuery";
		}
	}

	[Serializable]
	public partial class RegistrationGyssensMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationGyssensMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.ZatActiveID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.ZatActiveID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.SRConsumeMethod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.SRConsumeMethod;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.ConsumeQty, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.ConsumeQty;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.SRConsumeUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.SRConsumeUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.RasproSeqNo, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.RasproSeqNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.PrescriptionNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.PrescriptionNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.PrescriptionDateStart, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.PrescriptionDateStart;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.PrescriptionDateEnd, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.PrescriptionDateEnd;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.GyssensCreateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.GyssensCreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.GyssensCreateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.GyssensCreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationGyssensMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationGyssensMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationGyssensMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string ItemID = "ItemID";
			public const string ZatActiveID = "ZatActiveID";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string RasproSeqNo = "RasproSeqNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string PrescriptionDateStart = "PrescriptionDateStart";
			public const string PrescriptionDateEnd = "PrescriptionDateEnd";
			public const string GyssensCreateDateTime = "GyssensCreateDateTime";
			public const string GyssensCreateByUserID = "GyssensCreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string SeqNo = "SeqNo";
			public const string ItemID = "ItemID";
			public const string ZatActiveID = "ZatActiveID";
			public const string SRConsumeMethod = "SRConsumeMethod";
			public const string ConsumeQty = "ConsumeQty";
			public const string SRConsumeUnit = "SRConsumeUnit";
			public const string RasproSeqNo = "RasproSeqNo";
			public const string PrescriptionNo = "PrescriptionNo";
			public const string PrescriptionDateStart = "PrescriptionDateStart";
			public const string PrescriptionDateEnd = "PrescriptionDateEnd";
			public const string GyssensCreateDateTime = "GyssensCreateDateTime";
			public const string GyssensCreateByUserID = "GyssensCreateByUserID";
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
			lock (typeof(RegistrationGyssensMetadata))
			{
				if (RegistrationGyssensMetadata.mapDelegates == null)
				{
					RegistrationGyssensMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationGyssensMetadata.meta == null)
				{
					RegistrationGyssensMetadata.meta = new RegistrationGyssensMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZatActiveID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConsumeQty", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RasproSeqNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrescriptionDateStart", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PrescriptionDateEnd", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("GyssensCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("GyssensCreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "RegistrationGyssens";
				meta.Destination = "RegistrationGyssens";
				meta.spInsert = "proc_RegistrationGyssensInsert";
				meta.spUpdate = "proc_RegistrationGyssensUpdate";
				meta.spDelete = "proc_RegistrationGyssensDelete";
				meta.spLoadAll = "proc_RegistrationGyssensLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationGyssensLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationGyssensMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
