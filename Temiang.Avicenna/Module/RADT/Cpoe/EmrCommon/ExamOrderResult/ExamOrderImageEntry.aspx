<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true" CodeBehind="ExamOrderImageEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ExamOrderImageEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="contentEntry" ContentPlaceHolderID="cphEntry" runat="server">
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
                            <asp:Label ID="Label1" runat="server" Text="Document Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtDocumentName" Width="100%"></telerik:RadTextBox>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtDocumentNotes" TextMode="MultiLine" Resize="Vertical" Width="100%"></telerik:RadTextBox>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <table width="100%">
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
<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
    <script type="text/javascript">
        function SetImageNoForEdit(no) {
            document.getElementById("<%= hdnImageNoForEdit.ClientID %>").value = no;
        }
        function ZoomViewImage(no) {
            var url = "ExamOrderImageZoomView.aspx?trno=<%= TransactionNo %>&seqno=<%= SequenceNo %>&imgno=" + no;
            var wnd = $find("<%=winDialog.ClientID %>");
            wnd.setUrl(url);
            wnd.show();
            wnd.maximize();
        }
    </script>

    <asp:HiddenField runat="server" ID="hdnImageNoForEdit" />

    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="False" ShowOnTopWhenMaximized="True" VisibleStatusbar="False">
    </telerik:RadWindow>

    <telerik:RadListView ID="lvItemDocumentImage" runat="server" RenderMode="Lightweight"
        ItemPlaceholderID="ImageContainer" OnNeedDataSource="lvItemDocumentImage_NeedDataSource">

        <LayoutTemplate>
            <fieldset style="height: 150px; overflow: auto;">
                <legend>Document Image</legend>
                <table>
                    <tr>
                        <asp:PlaceHolder ID="ImageContainer" runat="server"></asp:PlaceHolder>
                    </tr>
                </table>
            </fieldset>

        </LayoutTemplate>
        <ItemTemplate>
            <td style="height: 125px; width: 225px;">
                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="Zoom"
                                OnClientClick='<%#string.Format("javascript:ZoomViewImage({0});return false;",DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                    Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("DocumentImage") == DBNull.Value? new System.Byte[0]: Eval("DocumentImage") %>'></telerik:RadBinaryImage>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbtnDocumentImageEdit" runat="server" ToolTip="Edit" OnClick="lbtnDocumentImageEdit_OnClick"
                                            OnClientClick='<%#string.Format("SetImageNoForEdit({0})",DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                                            <img src="../../../../../Images/Toolbar/edit16.png"/>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><%#DataBinder.Eval(Container.DataItem, "DocumentName")%></td>
                                </tr>
                                <tr>
                                    <td style="width: 50px;">Add:</td>
                                    <td><%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </ItemTemplate>
    </telerik:RadListView>

</asp:Content>
