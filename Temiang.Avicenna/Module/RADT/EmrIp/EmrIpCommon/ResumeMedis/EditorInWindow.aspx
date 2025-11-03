<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditorInWindow.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.ResumeMedis.EditorInWindow" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Editor</title>
        <style type="text/css">
            html, form, body
            {
                height: 100%;
                margin: 0px;
            }
 
            #editor1Bottom UL
            {
                float:right !important;
            }
        </style>
    </head>
    <body>
        <form id="Form1" method="post" runat="server">
        <telerik:RadScriptManager ID="ScriptManager1" runat="server" />
        <script type="text/javascript">
            function GetRadWindow()
            {
                  var oWindow = null;
                  if (window.radWindow) oWindow = window.radWindow;
                  else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                  return oWindow;
            }
 
            function setContent(content)
            {
                var editor = $find("<%= editor1.ClientID %>");
                if (editor) editor.set_html(content);  //set content from the parent page to RadEditor in RadWindow
            }
 
            function OnClientLoad(editor)
            {
                editor.get_contentArea().setAttribute("spellcheck", "false");
                editor.fire("ToggleScreenMode"); //set RadEditor in Full Scree mode
                editor.setFocus();
            }
 
            function OnClientCommandExecuting(editor, args)
            {
               var commandName = args.get_commandName();   //returns the executed command
               if (commandName == "SaveAndClose")
               {
                   var radWindow = GetRadWindow();
                   var browserWindow = radWindow.get_browserWindow();
                   browserWindow.Avicenna.SetEditorContent(editor.get_html(true));    //set the editor content on RadWindow to the editor on the parent page
                   radWindow.close(); //close RadWindow
                   args.set_cancel(true); //cancel the SaveAndClose command
               } 
            }
        </script>
        <telerik:RadEditor RenderMode="Lightweight" 
            OnClientCommandExecuting="OnClientCommandExecuting"
            Width="100%"
            EnableResize="false"
            Height="520px"
            ID="editor1" 
            OnClientLoad="OnClientLoad"
            Runat="server"
            ToolsFile="ToolBarEditorAdv.xml" NewLineMode="Br">
        </telerik:RadEditor> 
 
        </form>
    </body>
</html>
