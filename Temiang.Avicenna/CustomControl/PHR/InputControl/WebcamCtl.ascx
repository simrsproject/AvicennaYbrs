<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebcamCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.WebcamCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<script type="text/javascript">

<%--    window.onload = function () {
        var imgVal = document.getElementById("<%=hdnImage.ClientID %>").value;
        if (imgVal != "") {

            var img = document.getElementById("<%=rbImage.ClientID %>");

            if (imgVal.includes("base64"))
                img.setAttribute('src', imgVal);
            else
                img.setAttribute('src', "data:image/Png;base64," + imgVal);
        }
    }--%>

</script>
<table width="100%">
    <tr>
        <td style="width: 128px">
            <a style="cursor: pointer;" onclick="javascript:openWebCam('<%=rbImage.ClientID %>','<%=hdnImage.ClientID %>');return false;">
                <telerik:RadBinaryImage ID="rbImage" runat="server"
                    Width="246px" Height="185px" ResizeMode="None"></telerik:RadBinaryImage>
            </a>
            <div>
                <asp:HiddenField runat="server" ID="hdnImage" Value="" />
            </div>
        </td>
    </tr>
</table>

