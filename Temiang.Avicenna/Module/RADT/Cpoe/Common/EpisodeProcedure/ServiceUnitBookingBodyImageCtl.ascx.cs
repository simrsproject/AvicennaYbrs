using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ServiceUnitBookingBodyImageCtl : BaseUserControl
    {

        internal string BookingNo
        {
            get { return hdnBookingNo.Value; }
            set { hdnBookingNo.Value = value; }
        }

        internal string OpNotesSeqNo
        {
            get { return hdnOpNotesSeqNo.Value; }
            set { hdnOpNotesSeqNo.Value = value; }
        }

        internal string ServiceUnitID
        {
            get { return hdnServiceUnitID.Value; }
            set { hdnServiceUnitID.Value = value; }
        }
        #region override method

        internal void Save(ValidateArgs args)
        {
            // Save Localist / Body Image 
            var dtbSession = (DataTable)Session["rimBodyImage"];
            foreach (DataRow row in dtbSession.Rows)
            {
                if (true.Equals(row["IsModified"]))
                {
                    SaveLocalistStatus(BookingNo,OpNotesSeqNo, row["BodyID"].ToString(),
                        (byte[])row["BodyImage"]);
                }
            }
        }

        #endregion

        private void SaveLocalistStatus(string bookingNo, string opNotesSeqNo, string bodyId, byte[] bodyImage)
        {
            var es = new ServiceUnitBookingBodyImage();
            if (!es.LoadByPrimaryKey(bookingNo,opNotesSeqNo, bodyId))
            {
                es = new ServiceUnitBookingBodyImage
                {
                    BookingNo = bookingNo,
                    OpNotesSeqNo = opNotesSeqNo,
                    BodyID = bodyId,
                    CreatedDateTime = DateTime.Now,
                    CreatedByUserID = AppSession.UserLogin.UserID
                };
            }

            es.BodyImage = bodyImage;
            es.Save();
        }

        protected void lvLocalistStatus_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || !OpNotesSeqNo.Equals(Session["edit_id"]))
            {
                var dtb = BodyDiagramServiceUnitMatrix();
                Session["edit_id"] = OpNotesSeqNo;
                LocalistStatusEntry.BodyImages = dtb;
            }

            var dtbSession = LocalistStatusEntry.BodyImages;
            lvLocalistStatus.DataSource = dtbSession;
        }

        private DataTable BodyDiagramServiceUnitMatrix()
        {
            DataTable dtb;

            if (AppSession.Parameter.IsServiceUnitBookingUsingBodyDiagramServiceUnit)
            {
                //-- RSMM => BodyDiagramServiceUnit
                var qr = new ServiceUnitBookingBodyImageQuery("rim");
                var qrSubd = new BodyDiagramServiceUnitQuery("bsu");
                qr.RightJoin(qrSubd)
                    .On(qr.BodyID == qrSubd.BodyID & qr.BookingNo == BookingNo & qr.OpNotesSeqNo == OpNotesSeqNo);

                var qrBody = new BodyDiagramQuery("bd");
                qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

                qr.Select(qr.BookingNo, qr.OpNotesSeqNo,
                    "<CASE WHEN rim.BookingNo IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                    qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime,
                    "<CASE WHEN rim.BookingNo IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                    qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

                qr.Where(qrSubd.ServiceUnitID == ServiceUnitID);

                dtb = qr.LoadDataTable();
            }
            else
            {
                //-- AssessmentTypeBodyDiagram 
                var mainAssesmenType = "xxx";
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(ServiceUnitID) && !string.IsNullOrEmpty(su.SRAssessmentType))
                    mainAssesmenType = su.SRAssessmentType;

                var coll = new ServiceUnitAssessmentTypeCollection();
                coll.Query.Where(coll.Query.ServiceUnitID == ServiceUnitID, coll.Query.SRAssessmentType != mainAssesmenType);
                coll.LoadAll();
                
                var otherAssesmenTypes = (coll.Select(i => i.SRAssessmentType)).Distinct();

                var qr = new ServiceUnitBookingBodyImageQuery("rim");
                var qrSubd = new AssessmentTypeBodyDiagramQuery("bsu");
                qr.RightJoin(qrSubd)
                    .On(qr.BodyID == qrSubd.BodyID & qr.BookingNo == BookingNo & qr.OpNotesSeqNo == OpNotesSeqNo);

                var qrBody = new BodyDiagramQuery("bd");
                qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

                qr.Select(qr.BookingNo, qr.OpNotesSeqNo,
                    "<CASE WHEN rim.BookingNo IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                    qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime,
                    "<CASE WHEN rim.BookingNo IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                    qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

                if (!otherAssesmenTypes.Any())
                    qr.Where(qrSubd.SRAssessmentType == mainAssesmenType);
                else
                    qr.Where(qr.Or(qrSubd.SRAssessmentType == mainAssesmenType, qrSubd.SRAssessmentType.In(otherAssesmenTypes)));

                dtb = qr.LoadDataTable();
            }
            
            return dtb;
        }

    }
}