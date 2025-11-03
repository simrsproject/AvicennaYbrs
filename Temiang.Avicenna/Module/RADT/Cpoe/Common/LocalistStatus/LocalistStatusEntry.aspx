<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="LocalistStatusEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.LocalistStatusEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="cbTop">
        <script type="text/javascript">
            (function (global, undefined) {
                var imageEditor;

                function onClientLoad(sender, args) { //check if the Opacity filter is available
                    imageEditor = sender;
                }
                global.onClientLoad = onClientLoad;

                // ------------ Save Image
                function onClientCommandExecuting(imEditor, eventArgs) {
                    var cmdName = eventArgs.get_commandName();
                    switch (cmdName) {
                        case "Save":
                            imEditor.saveImageOnServer('', true);
                            //Prevent the buil-in Save dialog to pop up
                            eventArgs.set_cancel(true);
                            break;
                        case "ResetToLastSaved":
                        case "ResetToTemplate":
                            __doPostBack("<%= ButtonCancel.UniqueID %>", cmdName);
                            eventArgs.set_cancel(true);
                            break;
                    }
                }
                global.onClientCommandExecuting = onClientCommandExecuting;

                function onClientSaved(imgEditor, args) {
                    var val = args.get_argument();
                    closeAndApply(val);
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


                function closeAndApply(val) {
                    //get a reference to the current RadWindow
                    var oWnd = GetRadWindow();

                    oWnd.argument = new Object();

                    var oArg = new Object();

                    oArg.callbackMethod = '<%=Request.QueryString["ccm"]%>';
                    oArg.eventArgument = '<%=Request.QueryString["cea"]%>';
                    oArg.eventTarget = '<%=Request.QueryString["cet"]%>';

                    //Close the RadWindow            
                    oWnd.close(oArg);
                }

                // Additional Argument Sent to the Server
                var commandList = Telerik.Web.UI.ImageEditor.CommandList;
                commandList.ResetToLastSaved = createCustomCommand('Reset To Last Saved', 'Additional Argument Sent to the Server');
                commandList.ResetToTemplate = createCustomCommand('Reset To Template', 'Additional Argument Sent to the Server');

                function createCustomCommand(text, argument) {
                    return function (imageEditor, commandName, args) {
                        imageEditor.editImageOnServer(commandName, text, argument, function () { });
                    };
                }
            })(window);


        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
<%--        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="imeBodyImage">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imeBodyImage" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>--%>
    </telerik:RadAjaxManagerProxy>
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
                <telerik:ImageEditorTool CommandName="Pencil" />
                <telerik:ImageEditorTool CommandName="Line" />
                <telerik:ImageEditorTool CommandName="DrawRectangle" />
                <telerik:ImageEditorTool CommandName="DrawCircle" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="Resize" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="ZoomIn" />
                <telerik:ImageEditorTool CommandName="ZoomOut" />
                <telerik:ImageEditorTool CommandName="Zoom" />
                <telerik:ImageEditorToolSeparator />
                <telerik:ImageEditorTool CommandName="ResetToLastSaved" ToolTip="Reset To Last Saved" ImageUrl="~/Images/Toolbar/docOpen.png"></telerik:ImageEditorTool>
                <telerik:ImageEditorTool CommandName="ResetToTemplate" ToolTip="Reset To Template" ImageUrl="~/Images/Toolbar/new16.png"></telerik:ImageEditorTool>

            </telerik:ImageEditorToolGroup>
        </Tools>
    </telerik:RadImageEditor>

    <telerik:RadCodeBlock runat="server" ID="cbBottom">
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
    </telerik:RadCodeBlock>

</asp:Content>
