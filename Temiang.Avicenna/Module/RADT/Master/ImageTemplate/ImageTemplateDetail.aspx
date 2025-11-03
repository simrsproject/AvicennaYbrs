<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ImageTemplateDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ImageTemplateDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="ImageTemplateDetail.js" type="text/javascript"></script>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblImageTemplateID" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtImageTemplateID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvImageTemplateID" runat="server" ErrorMessage="Image Template ID required."
                    ValidationGroup="entry" ControlToValidate="txtImageTemplateID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtImageTemplateName" runat="server" Width="300px" MaxLength="250" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvImageTemplateName" runat="server" ErrorMessage="Name required."
                    ValidationGroup="entry" ControlToValidate="txtImageTemplateName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
                <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRImageTemplateType" runat="server" Width="300px" MaxLength="250" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Type required."
                    ValidationGroup="entry" ControlToValidate="cboSRImageTemplateType" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
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
                <asp:Label ID="lblImage" runat="server" Text="Image Template"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAsyncUpload ID="uplImageTemplate" runat="server"
                    OnClientFilesUploaded="OnClientFilesUploaded"
                    OnFileUploaded="uplImageTemplate_FileUploaded"
                    MaxFileSize="2097152" AllowedFileExtensions="jpg,png,gif,bmp"
                    AutoAddFileInputs="false" Localization-Select="Upload" HideFileInput="True" >
                </telerik:RadAsyncUpload>
                <telerik:RadBinaryImage ID="imgImageTemplate" runat="server" AlternateText=""
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
