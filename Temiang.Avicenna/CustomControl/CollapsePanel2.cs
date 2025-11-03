using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.CustomControl
{
    public class CollapsePanel2 : Panel, IPostBackDataHandler
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (DesignMode)
            //    return;

            //if (!Page.IsPostBack)
            {
                if (!Page.ClientScript.IsClientScriptIncludeRegistered("jquery"))
                    Page.ClientScript.RegisterClientScriptInclude("jquery", Page.ResolveUrl("~/JavaScript/jQuery.js"));

                if (!Page.ClientScript.IsClientScriptIncludeRegistered("ddaccordion"))
                    Page.ClientScript.RegisterClientScriptInclude("ddaccordion",
                                                                  Page.ResolveUrl("~/JavaScript/ddaccordion.js"));

                if (!Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType(), "ddaccordion_init_" + ID))
                {
                    var headerClass = string.Format("cpHeader_{0}", this.ID);
                    var contentClass = string.Format("cpContent_{0}", this.ID);
                    string scriptStartUp =
                        @"<script type='text/javascript'>
                            ddaccordion.init({
                                headerclass:  """ + headerClass + @""",
                                contentclass: """ + contentClass + @""",
                                revealtype: ""click"",
                                collapseprev: " + IsCollapsed + @",
                                defaultexpanded: [0,1,2,3,4,5,6],
                                animatedefault: false,
                                persiststate: false,
                                toggleclass: [""cpClosed"", ""cpOpen""],
                                togglehtml: [""prefix"", ""<img src='" +
                        Page.ResolveUrl("~/Images/cpUpArrow.png") +
                        "' style='vertical-align:middle;' /> \", \"<img src='" +
                        Page.ResolveUrl("~/Images/cpDownArrow.png") + @"' style='vertical-align:middle;'/> ""], 
                                animatespeed: ""fast"", 
                                oninit: function(expandedindices) {},
                                onopenclose: function(header, index, state, isuseractivated) {}
                            }) 
                        </script>";

                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ddaccordion_init_" + ID, scriptStartUp);
                }
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            //if (!Page.IsPostBack)
            writer.Write(string.Format("<div style=\"cursor:hand;cursor:pointer;font:bold 11px Verdana;height:24px;background-color:#a8a8a8;vertical-align:middle;\" class=\"cpHeader_{1}\">{0}</div><div style=\"padding-right:0px;padding-left:0px;padding-bottom:0px;padding-top:0px;\" class=\"cpContent_{1}\">", Title, this.ID));

            base.RenderContents(writer);

            //if (!Page.IsPostBack)
            writer.Write("</div>");
        }

        [DefaultValue("")]
        public string Title
        {
            get { return (string)(ViewState["Title"] ?? ""); }
            set { ViewState["Title"] = value; }
        }

        [DefaultValue("false")]
        public string IsCollapsed
        {
            get { return (string)(ViewState["IsCollapsed"] ?? "false"); }
            set { ViewState["IsCollapsed"] = value; }
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return true;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        #endregion
    }
}