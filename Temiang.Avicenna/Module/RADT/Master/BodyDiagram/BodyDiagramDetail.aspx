<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="BodyDiagramDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.BodyDiagramDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="BodyDiagramDetail.js" type="text/javascript"></script>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblBodyID" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtBodyID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvBodyID" runat="server" ErrorMessage="Body ID required."
                    ValidationGroup="entry" ControlToValidate="txtBodyID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblBodyName" runat="server" Text="Body Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtBodyName" runat="server" Width="300px" MaxLength="250" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvBodyName" runat="server" ErrorMessage="Body Name required."
                    ValidationGroup="entry" ControlToValidate="txtBodyName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="250" />
            </td>
            <td width="20">

            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblBodyImage" runat="server" Text="Body Image"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAsyncUpload ID="uplBodyImage" runat="server"
                    OnClientFilesUploaded="OnClientFilesUploaded"
                    OnFileUploaded="uplBodyImage_FileUploaded"
                    MaxFileSize="2097152" AllowedFileExtensions="jpg,png,gif,bmp"
                    AutoAddFileInputs="false" Localization-Select="Upload" HideFileInput="True" >
                </telerik:RadAsyncUpload>
                <telerik:RadBinaryImage ID="imgBodyImage" runat="server" AlternateText=""
                    Width="100%"
                    Height="100%"
                    ResizeMode="Fill" BorderStyle="Double"></telerik:RadBinaryImage>
            </td>
            <td width="20"></td>
            <td></td>
        </tr>

    </table>

    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            //<![CDATA[
            serverID("ajaxManagerID", "<%= AjaxManager.ClientID %>");
            //]]>
        </script>

    </telerik:RadCodeBlock>

</asp:Content>
