<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetPicture.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.HR.EmployeePersonalInformation.GetPicture" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>The Eruption</title>
    <style type="text/css">
        html, body, form
        {
         padding: 0;
         margin: 0;
         height: 100%;
         background: #f2f2de;
        
        }
        
        body
        {
         font: normal 11px Arial, Verdana, Sans-serif;
        
        }
        
        fieldset
        {
         height: 150px;
        }
        
        *+html fieldset
        {
         height: 154px;
         width: 268px;
        }
                
        </style>
</head>
<body onload="AdjustRadWidow();">
    <form id="Form2" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server"
            Skin="Sunset" />
        <script src="swfobject.js" language="javascript"></script>
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function openWin2() {
                var parentPage = GetRadWindow().BrowserWindow;
                var parentRadWindowManager = parentPage.GetRadWindowManager();
                var oWnd2 = parentRadWindowManager.open("Dialog2.aspx", "RadWindow2");
                window.setTimeout(function() {
                    oWnd2.setActive(true);
                }, 0);
            }

            function populateCityName(arg) {
                var cityName = document.getElementById("cityName");
                cityName.value = arg;
            }

            function AdjustRadWidow() {
                setTimeout(function() { GetRadWindow().autoSize(true) }, 500);
            }

            function returnToParent() {
                //create the argument that will be returned to the parent page
                var oArg = new Object();

                //get the city's name
                oArg.cityName = document.getElementById("cityName").value;

                
                //get a reference to the current RadWindow
                var oWnd = GetRadWindow();




                //Close the RadWindow and send the argument to the parent page
                if (oArg.selDate && oArg.cityName) {
                    oWnd.close(oArg);
                }
                else {
                    alert("Please fill both fields");
                }
            }
        </script>

                <div id="flashArea" class="flashArea" style="height: 100%;">
                    <p align="center">
                        This content requires the Adobe Flash Player.<br />
                        <a href="http://www.adobe.com/go/getflashplayer">
                            <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"
                                alt="Get Adobe Flash player" /><br />
                            <a href="http://www.macromedia.com/go/getflash" />Get Flash</a></p>
                </div>
                <script type="text/javascript">
                    var mainswf = new SWFObject("take_pictureaspx.swf", "main", "700", "400", "9", "#ffffff");
                    mainswf.addParam("scale", "noscale");
                    mainswf.addParam("wmode", "window");
                    mainswf.addParam("allowFullScreen", "true");
                    //mainswf.addVariable("requireLogin", "false");
                    mainswf.write("flashArea");
            	
                </script>
                <script type="text/javascript">
                    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
                    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
                </script>
                <script type="text/javascript">
                    var pageTracker = _gat._getTracker("UA-3097820-1");
                    pageTracker._trackPageview();
                </script>
                
                <tr>
                    <td class="label">
                            <asp:Label ID="lblPictureName" runat="server" Text="Picture Name"></asp:Label>
			            </td>        
			            <td class="entry">
				            <telerik:RadTextBox ID="txtPictureName" runat="server" Width="300px" MaxLength="200" Visible="true"/>
                        </td>
                </tr>
    </form>
</body>
</html>