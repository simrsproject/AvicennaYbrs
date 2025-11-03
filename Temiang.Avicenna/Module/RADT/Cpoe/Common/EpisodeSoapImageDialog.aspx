<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="EpisodeSoapImageDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EpisodeSoapImageDialog" %>

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
                    imEditor.saveImageOnServer('', true);
                    eventArgs.set_cancel(true);
                }
            }
            global.onClientCommandExecuting = onClientCommandExecuting;

            function onClientSaved(imgEditor, args) {
                // PostBack lagi untuk menyimpan entrian yg lain
                __doPostBack('<%= txtSubjective.UniqueID %>', 'saveSoap:' + args.get_argument());
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

    <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="526px" Width="980px" HeightOffset="30" BorderStyle="None">
        <telerik:RadPane ID="navigationPane" runat="server" Width="450px">
            <table width="100%">
                <tr>
                    <td class="label" style="font-style: italic">Date
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtDateSOAP" runat="server" Enabled="False" Width="100px" />
                        <telerik:RadTimePicker ID="txtTimeSOAP" runat="server" Width="93px" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="font-style: italic">Subjective
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtSubjective" runat="server" Width="99%" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="font-style: italic">Objective
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtObjective" runat="server" Width="99%" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="font-style: italic">Assessment / Diagnosis
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtAssesment" runat="server" Width="99%" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="font-style: italic">Planning
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPlanning" runat="server" Width="99%" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="font-style: italic">Notes
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtAttendingNotes" runat="server" Width="99%" />
                    </td>
                </tr>
                <tr>
                    <td class="label" style="font-style: italic"></td>
                    <td>
                        <asp:CheckBox ID="chkIsInformConcern" runat="server" Text="Inform Concern" />
                    </td>
                </tr>
            </table>
        </telerik:RadPane>
        <telerik:RadSplitBar ID="RadSplitbar1" runat="server" CollapseMode="Forward">
        </telerik:RadSplitBar>
        <telerik:RadPane ID="contentPane" runat="server" Scrolling="None" Height="100%">

            <table width="100%">
                <tr>
                    <td class="label" style="font-style: italic">Change image template
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboBodyImage" runat="server" Width="100%" AutoPostBack="true" DataTextField="BodyName" DataValueField="BodyID"
                            OnSelectedIndexChanged="cboBodyImage_SelectedIndexChanged">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "BodyName") %>
                                </b>
                                <br />
                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" AlternateText="" DataValue='<%# DataBinder.Eval(Container.DataItem,"BodyImage") == DBNull.Value? new System.Byte[0]: DataBinder.Eval(Container.DataItem,"BodyImage") %>'
                                    Width="100"
                                    Height="100"
                                    ResizeMode="Fill"></telerik:RadBinaryImage>
                            </ItemTemplate>

                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>

            <telerik:RadImageEditor ID="imeBodyImage" runat="server" Width="100%" Height="488px" ToolBarMode="Default" StatusBarMode="Hidden" EnableResize="False"
                OnImageLoading="RadImgEdt_ImageLoading"
                OnImageSaving="RadImgEdt_ImageSaving"
                OnClientCommandExecuting="onClientCommandExecuting"
                OnClientLoad="onClientLoad"
                OnClientSaved="onClientSaved"
                OnClientImageLoad="onClientImageLoad">
                <Tools>
                    <telerik:ImageEditorToolGroup>
                        <telerik:ImageEditorTool CommandName="Reset"/>
                        <telerik:ImageEditorTool CommandName="Undo"/>
                        <telerik:ImageEditorTool CommandName="Redo"/>
                        <telerik:ImageEditorToolSeparator/>
                        <telerik:ImageEditorTool CommandName="AddText"/>
                        <telerik:ImageEditorTool CommandName="Pencil"/>
                        <telerik:ImageEditorTool CommandName="Line"/>
                        <telerik:ImageEditorTool CommandName="DrawRectangle"/>
                        <telerik:ImageEditorTool CommandName="DrawCircle"/>
                        <telerik:ImageEditorToolSeparator/>
                        <telerik:ImageEditorTool CommandName="ZoomIn"/>
                        <telerik:ImageEditorTool CommandName="ZoomOut"/>
                        <telerik:ImageEditorTool CommandName="Zoom"/>
                    </telerik:ImageEditorToolGroup>
                </Tools>
            </telerik:RadImageEditor>
        </telerik:RadPane>
    </telerik:RadSplitter>
</asp:Content>
