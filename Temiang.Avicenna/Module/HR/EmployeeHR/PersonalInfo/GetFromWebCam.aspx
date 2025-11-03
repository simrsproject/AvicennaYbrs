<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" 
         AutoEventWireup="true" CodeBehind="GetFromWebCam.aspx.cs" 
         Inherits="Temiang.Avicenna.Module.HR.EmployeePersonalInformation.GetFromWebCam" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="swfobject.js" language="javascript"></script>
    
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
				<telerik:RadNumericTextBox ID="txtPictureName" runat="server" Width="300px" />
            </td>
    </tr>
</asp:Content>
