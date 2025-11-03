/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/9/2020 11:43:37 AM
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
	abstract public class esMembershipDetailCollection : esEntityCollectionWAuditLog
	{
		public esMembershipDetailCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MembershipDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esMembershipDetailQuery query)
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
			this.InitQuery(query as esMembershipDetailQuery);
		}
		#endregion

		virtual public MembershipDetail DetachEntity(MembershipDetail entity)
		{
			return base.DetachEntity(entity) as MembershipDetail;
		}

		virtual public MembershipDetail AttachEntity(MembershipDetail entity)
		{
			return base.AttachEntity(entity) as MembershipDetail;
		}

		virtual public void Combine(MembershipDetailCollection collection)
		{
			base.Combine(collection);
		}

		new public MembershipDetail this[int index]
		{
			get
			{
				return base[index] as MembershipDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MembershipDetail);
		}
	}

	[Serializable]
	abstract public class esMembershipDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMembershipDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esMembershipDetail()
		{
		}

		public esMembershipDetail(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 membershipDetailID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(membershipDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(membershipDetailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 membershipDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(membershipDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(membershipDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 membershipDetailID)
		{
			esMembershipDetailQuery query = this.GetDynamicQuery();
			query.Where(query.MembershipDetailID == membershipDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 membershipDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("MembershipDetailID", membershipDetailID);
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
						case "MembershipDetailID": this.str.MembershipDetailID = (string)value; break;
						case "MembershipNo": this.str.MembershipNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "TotalAmount": this.str.TotalAmount = (string)value; break;
						case "ReedeemAmount": this.str.ReedeemAmount = (string)value; break;
						case "BalanceAmount": this.str.BalanceAmount = (string)value; break;
						case "RewardPoint": this.str.RewardPoint = (string)value; break;
						case "RewardPointRefferal": this.str.RewardPointRefferal = (string)value; break;
						case "ClaimedPoint": this.str.ClaimedPoint = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MembershipDetailID":

							if (value == null || value is System.Int64)
								this.MembershipDetailID = (System.Int64?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "TotalAmount":

							if (value == null || value is System.Decimal)
								this.TotalAmount = (System.Decimal?)value;
							break;
						case "ReedeemAmount":

							if (value == null || value is System.Decimal)
								this.ReedeemAmount = (System.Decimal?)value;
							break;
						case "BalanceAmount":

							if (value == null || value is System.Decimal)
								this.BalanceAmount = (System.Decimal?)value;
							break;
						case "RewardPoint":

							if (value == null || value is System.Decimal)
								this.RewardPoint = (System.Decimal?)value;
							break;
						case "RewardPointRefferal":

							if (value == null || value is System.Decimal)
								this.RewardPointRefferal = (System.Decimal?)value;
							break;
						case "ClaimedPoint":

							if (value == null || value is System.Decimal)
								this.ClaimedPoint = (System.Decimal?)value;
							break;
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
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
		/// Maps to MembershipDetail.MembershipDetailID
		/// </summary>
		virtual public System.Int64? MembershipDetailID
		{
			get
			{
				return base.GetSystemInt64(MembershipDetailMetadata.ColumnNames.MembershipDetailID);
			}

			set
			{
				base.SetSystemInt64(MembershipDetailMetadata.ColumnNames.MembershipDetailID, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.MembershipNo
		/// </summary>
		virtual public System.String MembershipNo
		{
			get
			{
				return base.GetSystemString(MembershipDetailMetadata.ColumnNames.MembershipNo);
			}

			set
			{
				base.SetSystemString(MembershipDetailMetadata.ColumnNames.MembershipNo, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MembershipDetailMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MembershipDetailMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(MembershipDetailMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(MembershipDetailMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(MembershipDetailMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(MembershipDetailMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.TotalAmount
		/// </summary>
		virtual public System.Decimal? TotalAmount
		{
			get
			{
				return base.GetSystemDecimal(MembershipDetailMetadata.ColumnNames.TotalAmount);
			}

			set
			{
				base.SetSystemDecimal(MembershipDetailMetadata.ColumnNames.TotalAmount, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.ReedeemAmount
		/// </summary>
		virtual public System.Decimal? ReedeemAmount
		{
			get
			{
				return base.GetSystemDecimal(MembershipDetailMetadata.ColumnNames.ReedeemAmount);
			}

			set
			{
				base.SetSystemDecimal(MembershipDetailMetadata.ColumnNames.ReedeemAmount, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.BalanceAmount
		/// </summary>
		virtual public System.Decimal? BalanceAmount
		{
			get
			{
				return base.GetSystemDecimal(MembershipDetailMetadata.ColumnNames.BalanceAmount);
			}

			set
			{
				base.SetSystemDecimal(MembershipDetailMetadata.ColumnNames.BalanceAmount, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.RewardPoint
		/// </summary>
		virtual public System.Decimal? RewardPoint
		{
			get
			{
				return base.GetSystemDecimal(MembershipDetailMetadata.ColumnNames.RewardPoint);
			}

			set
			{
				base.SetSystemDecimal(MembershipDetailMetadata.ColumnNames.RewardPoint, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.RewardPointRefferal
		/// </summary>
		virtual public System.Decimal? RewardPointRefferal
		{
			get
			{
				return base.GetSystemDecimal(MembershipDetailMetadata.ColumnNames.RewardPointRefferal);
			}

			set
			{
				base.SetSystemDecimal(MembershipDetailMetadata.ColumnNames.RewardPointRefferal, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.ClaimedPoint
		/// </summary>
		virtual public System.Decimal? ClaimedPoint
		{
			get
			{
				return base.GetSystemDecimal(MembershipDetailMetadata.ColumnNames.ClaimedPoint);
			}

			set
			{
				base.SetSystemDecimal(MembershipDetailMetadata.ColumnNames.ClaimedPoint, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(MembershipDetailMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(MembershipDetailMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MembershipDetailMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MembershipDetailMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(MembershipDetailMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(MembershipDetailMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MembershipDetailMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MembershipDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MembershipDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MembershipDetailMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MembershipDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMembershipDetail entity)
			{
				this.entity = entity;
			}
			public System.String MembershipDetailID
			{
				get
				{
					System.Int64? data = entity.MembershipDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MembershipDetailID = null;
					else entity.MembershipDetailID = Convert.ToInt64(value);
				}
			}
			public System.String MembershipNo
			{
				get
				{
					System.String data = entity.MembershipNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MembershipNo = null;
					else entity.MembershipNo = Convert.ToString(value);
				}
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
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
			public System.String TotalAmount
			{
				get
				{
					System.Decimal? data = entity.TotalAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalAmount = null;
					else entity.TotalAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ReedeemAmount
			{
				get
				{
					System.Decimal? data = entity.ReedeemAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReedeemAmount = null;
					else entity.ReedeemAmount = Convert.ToDecimal(value);
				}
			}
			public System.String BalanceAmount
			{
				get
				{
					System.Decimal? data = entity.BalanceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceAmount = null;
					else entity.BalanceAmount = Convert.ToDecimal(value);
				}
			}
			public System.String RewardPoint
			{
				get
				{
					System.Decimal? data = entity.RewardPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RewardPoint = null;
					else entity.RewardPoint = Convert.ToDecimal(value);
				}
			}
			public System.String RewardPointRefferal
			{
				get
				{
					System.Decimal? data = entity.RewardPointRefferal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RewardPointRefferal = null;
					else entity.RewardPointRefferal = Convert.ToDecimal(value);
				}
			}
			public System.String ClaimedPoint
			{
				get
				{
					System.Decimal? data = entity.ClaimedPoint;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClaimedPoint = null;
					else entity.ClaimedPoint = Convert.ToDecimal(value);
				}
			}
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
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
			private esMembershipDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMembershipDetailQuery query)
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
				throw new Exception("esMembershipDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MembershipDetail : esMembershipDetail
	{
	}

	[Serializable]
	abstract public class esMembershipDetailQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MembershipDetailMetadata.Meta();
			}
		}

		public esQueryItem MembershipDetailID
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.MembershipDetailID, esSystemType.Int64);
			}
		}

		public esQueryItem MembershipNo
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.MembershipNo, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem TotalAmount
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.TotalAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem ReedeemAmount
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.ReedeemAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem BalanceAmount
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.BalanceAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem RewardPoint
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.RewardPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem RewardPointRefferal
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.RewardPointRefferal, esSystemType.Decimal);
			}
		}

		public esQueryItem ClaimedPoint
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.ClaimedPoint, esSystemType.Decimal);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MembershipDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MembershipDetailCollection")]
	public partial class MembershipDetailCollection : esMembershipDetailCollection, IEnumerable<MembershipDetail>
	{
		public MembershipDetailCollection()
		{

		}

		public static implicit operator List<MembershipDetail>(MembershipDetailCollection coll)
		{
			List<MembershipDetail> list = new List<MembershipDetail>();

			foreach (MembershipDetail emp in coll)
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
				return MembershipDetailMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MembershipDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MembershipDetail();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MembershipDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipDetailQuery();
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
		public bool Load(MembershipDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MembershipDetail AddNew()
		{
			MembershipDetail entity = base.AddNewEntity() as MembershipDetail;

			return entity;
		}
		public MembershipDetail FindByPrimaryKey(Int64 membershipDetailID)
		{
			return base.FindByPrimaryKey(membershipDetailID) as MembershipDetail;
		}

		#region IEnumerable< MembershipDetail> Members

		IEnumerator<MembershipDetail> IEnumerable<MembershipDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MembershipDetail;
			}
		}

		#endregion

		private MembershipDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MembershipDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MembershipDetail ({MembershipDetailID})")]
	[Serializable]
	public partial class MembershipDetail : esMembershipDetail
	{
		public MembershipDetail()
		{
		}

		public MembershipDetail(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MembershipDetailMetadata.Meta();
			}
		}

		override protected esMembershipDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MembershipDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MembershipDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MembershipDetailQuery();
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
		public bool Load(MembershipDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MembershipDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MembershipDetailQuery : esMembershipDetailQuery
	{
		public MembershipDetailQuery()
		{

		}

		public MembershipDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MembershipDetailQuery";
		}
	}

	[Serializable]
	public partial class MembershipDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MembershipDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.MembershipDetailID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.MembershipDetailID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.MembershipNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.MembershipNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.StartDate;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.EndDate;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.TotalAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.TotalAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.ReedeemAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.ReedeemAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.BalanceAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.BalanceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.RewardPoint, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.RewardPoint;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.RewardPointRefferal, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.RewardPointRefferal;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.ClaimedPoint, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.ClaimedPoint;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.IsClosed, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.CreateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.CreateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MembershipDetailMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = MembershipDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MembershipDetailMetadata Meta()
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
			public const string MembershipDetailID = "MembershipDetailID";
			public const string MembershipNo = "MembershipNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string TotalAmount = "TotalAmount";
			public const string ReedeemAmount = "ReedeemAmount";
			public const string BalanceAmount = "BalanceAmount";
			public const string RewardPoint = "RewardPoint";
			public const string RewardPointRefferal = "RewardPointRefferal";
			public const string ClaimedPoint = "ClaimedPoint";
			public const string IsClosed = "IsClosed";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MembershipDetailID = "MembershipDetailID";
			public const string MembershipNo = "MembershipNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string TotalAmount = "TotalAmount";
			public const string ReedeemAmount = "ReedeemAmount";
			public const string BalanceAmount = "BalanceAmount";
			public const string RewardPoint = "RewardPoint";
			public const string RewardPointRefferal = "RewardPointRefferal";
			public const string ClaimedPoint = "ClaimedPoint";
			public const string IsClosed = "IsClosed";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
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
			lock (typeof(MembershipDetailMetadata))
			{
				if (MembershipDetailMetadata.mapDelegates == null)
				{
					MembershipDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MembershipDetailMetadata.meta == null)
				{
					MembershipDetailMetadata.meta = new MembershipDetailMetadata();
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

				meta.AddTypeMap("MembershipDetailID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("MembershipNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TotalAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ReedeemAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BalanceAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RewardPoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RewardPointRefferal", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ClaimedPoint", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MembershipDetail";
				meta.Destination = "MembershipDetail";
				meta.spInsert = "proc_MembershipDetailInsert";
				meta.spUpdate = "proc_MembershipDetailUpdate";
				meta.spDelete = "proc_MembershipDetailDelete";
				meta.spLoadAll = "proc_MembershipDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_MembershipDetailLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MembershipDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
