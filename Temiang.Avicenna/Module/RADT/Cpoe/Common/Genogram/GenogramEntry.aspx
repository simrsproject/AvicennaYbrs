<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="GenogramEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.GenogramEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        (function (global, undefined) {
            var imageEditor;

            function onClientLoad(sender, args) { //check if the Opacity filter is available
                imageEditor = sender;

                // Maximize
                var h = window.innerHeight;
                $find("<%= imeBodyImage.ClientID %>").set_height(h - 86);
            }
            global.onClientLoad = onClientLoad;

            // ------------ Save Image
            function onClientCommandExecuting(imEditor, eventArgs) {
                if (eventArgs.get_commandName() == 'Save') {
                    //Prevent the buil-in Save dialog to pop up
                    imEditor.saveImageOnServer('', true);
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
                // Deafult Pencil Mode //
                imageEditor.executeCommand("Pencil");
            }
            global.onClientImageLoad = onClientImageLoad;


        })(window);


    </script>
    <telerik:RadImageEditor ID="imeBodyImage" runat="server" Width="100%" Height="100%" ToolBarMode="Default" 
        StatusBarMode="Hidden" EnableResize="False" 
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
