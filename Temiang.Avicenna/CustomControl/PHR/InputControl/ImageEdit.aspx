<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ImageEdit.aspx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.ImageEdit" %>

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
                var val = args.get_argument();
                CloseAndApply(val);
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

        function CloseAndApply(val) {
            var oArg = new Object();
            oArg.image = val;
            oArg.imgId = '<%=Request.QueryString["imgId"]%>';
            oArg.txtId = '<%=Request.QueryString["txtId"]%>';

            //get a reference to the current RadWindow
            var oWnd = window.GetRadWindow();

            //Close the RadWindow            
            oWnd.close(oArg);
        }
    </script>
    <telerik:RadImageEditor ID="imeBodyImage" runat="server" Width="100%" ToolBarMode="Default"
        RenderMode="Lightweight" EnableResize="False"
        OnImageLoading="RadImgEdt_ImageLoading"
        OnImageSaving="RadImgEdt_ImageSaving"
        OnClientCommandExecuting="onClientCommandExecuting"
        OnClientLoad="onClientLoad"
        OnClientSaved="onClientSaved"
        OnClientImageLoad="onClientImageLoad">
        <Tools>
            <telerik:ImageEditorToolGroup>
                <telerik:ImageEditorTool CommandName="Reset" />
                <telerik:ImageEditorToolStrip CommandName="Undo" />
                <telerik:ImageEditorToolStrip CommandName="Redo" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="AddText" />
                <telerik:ImageEditorTool CommandName="Pencil"  />
                <telerik:ImageEditorTool CommandName="Line" />
                <telerik:ImageEditorTool CommandName="DrawRectangle" />
                <telerik:ImageEditorTool CommandName="DrawCircle" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="Resize" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="ZoomIn" />
                <telerik:ImageEditorTool CommandName="ZoomOut" />
                <telerik:ImageEditorTool CommandName="Zoom" />
            </telerik:ImageEditorToolGroup>
        </Tools>
    </telerik:RadImageEditor>

    <script type="text/javascript" language="javascript">

        function applyCtlHeightMax() {
            var height =
                (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

            // Maximize
            var imeBodyImage = $find("<%= imeBodyImage.ClientID %>");
            imeBodyImage.set_height(height - 54);

        }
        window.onload = function () {
            applyCtlHeightMax();
        }
        window.onresize = function () {
            applyCtlHeightMax();
        }

        // After postback
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            applyCtlHeightMax();
        });
    </script>

</asp:Content>
