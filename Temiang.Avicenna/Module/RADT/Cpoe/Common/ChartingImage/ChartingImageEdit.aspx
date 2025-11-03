<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ChartingImageEdit.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ChartingImageEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        (function (global, undefined) {
            var imageEditor;

            function onClientLoad(sender, args) { //check if the Opacity filter is available
                imageEditor = sender;
            }
            global.onClientLoad = onClientLoad;

            // ------------ Save Image
            function onClientCommandExecuting(imEditor, eventArgs) {
                if (eventArgs.get_commandName() == 'Save') {
                    imEditor.saveImageOnServer('', true);

                    //Prevent the buil-in Save dialog to pop up
                    eventArgs.set_cancel(true);
                }
            }
            global.onClientCommandExecuting = onClientCommandExecuting;

            function onClientSaved(imgEditor, args) {
                CloseAndApply();
            }
            global.onClientSaved = onClientSaved;

            function onSaveImage() {
                imageEditor.fire('Save');
            }
            global.onSaveImage = onSaveImage;

            function onClientImageLoad(imageEditor, args) {
                // Deafult Line Mode //
                imageEditor.executeCommand("Line");
            }
            global.onClientImageLoad = onClientImageLoad;


        })(window);


    </script>



    <table width="100%">
        <tr>
            <td class="label" style="font-style: italic">Change image template
            </td>
            <td>
                <telerik:RadComboBox ID="cboImageTemplateID" runat="server" Width="100%" AutoPostBack="true" DataTextField="ImageTemplateName" DataValueField="ImageTemplateID"
                    OnSelectedIndexChanged="cboImageTemplateID_SelectedIndexChanged">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "ImageTemplateName") %>
                        </b>
                        <br />
                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" AlternateText="" DataValue='<%# DataBinder.Eval(Container.DataItem,"Image") == DBNull.Value? new System.Byte[0]: DataBinder.Eval(Container.DataItem,"Image") %>'
                            Width="100"
                            Height="100"
                            ResizeMode="Fill"></telerik:RadBinaryImage>
                    </ItemTemplate>

                </telerik:RadComboBox>
            </td>
        </tr>
    </table>

    <telerik:RadImageEditor ID="imeImage" runat="server" Width="100%" Height="488px" ToolBarMode="Default" StatusBarMode="Hidden" EnableResize="False"
        OnImageLoading="RadImgEdt_ImageLoading"
        OnImageSaving="RadImgEdt_ImageSaving"
        OnClientCommandExecuting="onClientCommandExecuting"
        OnClientLoad="onClientLoad"
        OnClientSaved="onClientSaved"
        OnClientImageLoad="onClientImageLoad">
        <Tools>
            <telerik:ImageEditorToolGroup>
                <telerik:ImageEditorTool CommandName="Reset" />
                <telerik:ImageEditorTool CommandName="Undo" />
                <telerik:ImageEditorTool CommandName="Redo" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="AddText" />
                <telerik:ImageEditorTool CommandName="Pencil" />
                <telerik:ImageEditorTool CommandName="Line" />
                <telerik:ImageEditorTool CommandName="DrawRectangle" />
                <telerik:ImageEditorTool CommandName="DrawCircle" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="ZoomIn" />
                <telerik:ImageEditorTool CommandName="ZoomOut" />
                <telerik:ImageEditorTool CommandName="Zoom" />
            </telerik:ImageEditorToolGroup>
        </Tools>
    </telerik:RadImageEditor>
</asp:Content>
