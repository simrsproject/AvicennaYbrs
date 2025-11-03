<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ExamOrderRadiologyImageUpload.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ExamOrderRadiologyImageUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        (function (global, undefined) {
            var ajaxManagerID;
            var demo = {};
            function onClientFilesUploaded(sender, args) {
                $find(ajaxManagerID).ajaxRequest();
            }
            function serverID(name, id) {
                demo[name] = id;
                ajaxManagerID = id;
            }

            global.serverID = serverID;
            global.OnClientFilesUploaded = onClientFilesUploaded;


        })(window);
    </script>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtImageNo" Width="100%"></telerik:RadTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Document Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtDocumentName" Width="100%"></telerik:RadTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDocumentImage" runat="server" Text="Document Image<br/>(Max File Size 2MB)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadAsyncUpload ID="uplDocumentImage" runat="server"
                                OnClientFilesUploaded="OnClientFilesUploaded"
                                OnFileUploaded="uplDocumentImage_FileUploaded"
                                MaxFileSize="2097152" AllowedFileExtensions="jpg,png,gif,bmp"
                                AutoAddFileInputs="false" Localization-Select="Upload" HideFileInput="True">
                            </telerik:RadAsyncUpload>
                            <telerik:RadBinaryImage ID="imgDocumentImage" runat="server" AlternateText=""
                                Width="500px"
                                Height="500px" ResizeMode="Fit"
                                BorderStyle="Double"></telerik:RadBinaryImage>
                        </td>
                        <td></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //<![CDATA[
            serverID("ajaxManagerID", "<%= AjaxManager.ClientID %>");
            //]]>
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
