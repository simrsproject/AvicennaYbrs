using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class ImageViewer : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["status"] == "recruit")
                ProgramID = AppConstant.Program.ApplicantPersonalInfo;
            else
                ProgramID = AppConstant.Program.PersonalInfo; //TODO: Isi ProgramID

            if (!IsPostBack)
            {
                if (Request.QueryString["type"] == "1")
                {
                    var collection =
                        Session["collPersonalIdentification"] as BusinessObject.PersonalIdentificationCollection;
                    if (collection != null && collection.Any())
                    {
                        var entity = collection.SingleOrDefault(c =>
                            c.PersonalIdentificationID == int.Parse(Request.QueryString["id"]) &&
                            c.PersonID == int.Parse(Request.QueryString["pid"]));
                        if (entity != null)
                        {
                            imgImage.ImageUrl = "data:image;base64," + Convert.ToBase64String(entity.Image);
                        }
                    }
                }
                else if (Request.QueryString["type"] == "2")
                {
                    var collection =
                        Session["collPersonalEducationHistory"] as BusinessObject.PersonalEducationHistoryCollection;
                    if (collection != null && collection.Any())
                    {
                        var entity = collection.SingleOrDefault(c =>
                            c.PersonalEducationHistoryID == int.Parse(Request.QueryString["id"]) &&
                            c.PersonID == int.Parse(Request.QueryString["pid"]));
                        if (entity != null)
                        {
                            imgImage.ImageUrl = "data:image;base64," + Convert.ToBase64String(entity.Image);
                        }
                    }
                }
                else if (Request.QueryString["type"] == "3")
                {
                    var collection =
                        Session["collPersonalLicence"] as BusinessObject.PersonalLicenceCollection;
                    if (collection != null && collection.Any())
                    {
                        var entity = collection.SingleOrDefault(c =>
                            c.PersonalLicenceID == int.Parse(Request.QueryString["id"]) &&
                            c.PersonID == int.Parse(Request.QueryString["pid"]));
                        if (entity != null)
                        {
                            imgImage.ImageUrl = "data:image;base64," + Convert.ToBase64String(entity.Image);
                        }
                    }
                }
            }
        }
    }
}