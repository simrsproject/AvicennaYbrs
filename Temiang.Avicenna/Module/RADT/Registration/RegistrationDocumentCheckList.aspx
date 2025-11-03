<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="RegistrationDocumentCheckList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.RegistrationDocumentCheckList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            var _wcWidth =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamWidth) %>;
            var _wcHeight =  <%= AppParameter.GetParameterValue(AppParameter.ParameterItem.WebCamHeight) %>;
            var _height = 0;
            var _width = 0;


            function UpdateInformationCount2(objectName, iCount) {
                if (objectName == null || objectName == undefined || objectName == 'none') {
                    // do nothing
                } else {
                    var obj = GetRadWindow().BrowserWindow.document.getElementById(objectName);
                    obj.innerHTML = iCount;
                    if (iCount > 0) {
                        // set bubble visible true
                        obj.style.visibility = 'visible';
                    } else {
                        // set bubble visible false
                        obj.style.visibility = 'hidden';
                    }
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

            function CallFnOnParent() {
                GetRadWindow().BrowserWindow.CalledFn();
            }

            function scanDocument(dfid) {
                // Resize window
                var curWnd = GetRadWindow();
                var curWndBounds = curWnd.getWindowBounds();
                // Not Maximized
                if (curWndBounds.y > 0 & curWndBounds.x > 0) {
                    _height = curWndBounds.height;
                    _width = curWndBounds.width;

                    var setHeight = _wcHeight + 200;
                    if (setHeight < _height)
                        setHeight = _height;

                    var setWidth = _wcWidth + 160;
                    if (setWidth < _width)
                        setWidth = _width;

                    curWnd.setSize(setWidth, setHeight);
                    curWnd.center();
                }

                var url = 'WebCamCaptureDocument.aspx?dfid=' + dfid +'&regno=<%= RegistrationNo %>&ccm=rebind&cet=<%=grdList.ClientID %>';

                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(_wcWidth + 40, _wcHeight + 120);
                oWnd.center();
                oWnd.show();

            }
            function winEntry_ClientClose(oWnd, args) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
                // Restore window size
                var curWnd = GetRadWindow();
                if (_width > 0) {
                    curWnd.setSize(_width, _height);
                    curWnd.center();
                }

                //get the transferred arguments from Dialog
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'rebind') {
                        var masterTable = $find(arg.eventTarget).get_masterTableView();
                        masterTable.rebind();

                        var rcount = <%= (DocumentChecklistCount??0) %> - parseInt(arg.count);
                        UpdateInformationCount2('<%= Page.Request.QueryString["lblRegistrationInfo"] %>', rcount);
                    }
                }
            }
        </script>

        <style type="text/css">
            .MyImageButton {
                cursor: hand;
            }

            .EditFormHeader td {
                font-size: 14px;
                padding: 4px !important;
                color: #0066cc;
            }

            /* Big Checkbox */
            button.RadCheckBox {
                font-size: 20px;
            }

            /* makes the checkbox icon elastic in addition to the label text */
            .RadButton.RadCheckBox .rbIcon,
            .RadButton.RadCheckBox .rbIcon::before {
                font-size: inherit;
                width: 1em;
                height: 1em;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server" OnClientClose="winEntry_ClientClose"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <asp:HiddenField runat="server" ID="hdDocumentChecklist" />
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        OnNeedDataSource="grdList_NeedDataSource" OnItemCommand="grdList_ItemCommand">
        <MasterTableView DataKeyNames="DocumentFilesID">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Stat">
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                    <ItemTemplate>
                        <telerik:RadCheckBox runat="server" ID="IsAttached" Checked='<%#DataBinder.Eval(Container.DataItem, "IsAttached").Equals(1)%>'
                            CommandName="IsAttached" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "DocumentFilesID")%>'>
                        </telerik:RadCheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center" HeaderText="Upl"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" title=\"Scan additional document\" onclick=\"scanDocument('{0}'); return false;\"><img src=\"../../../Images/Toolbar/upload_ascii16.png\" border=\"0\" alt=\"Patient Document\" /></a>", DataBinder.Eval(Container.DataItem, "DocumentFilesID"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="colSmallImage" HeaderText="Thumbnail" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <div style="width: 130px; text-align: center;">
                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible='<%#Eval("ThumbNail") != DBNull.Value%>'
                                Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("ThumbNail") == DBNull.Value? new System.Byte[0]: Eval("ThumbNail") %>'></telerik:RadBinaryImage>
                        </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="DocumentFilesID" HeaderText="DocumentFilesID" UniqueName="DocumentFilesID"
                    SortExpression="DocumentFilesID" Visible="false">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="DocumentName" HeaderText="Document" UniqueName="DocumentName"
                    SortExpression="DocumentName" HeaderStyle-Width="300px">
                    <ItemTemplate>
                        <strong><%# DataBinder.Eval(Container.DataItem, "DocumentName")%></strong><br />
                        <%#DataBinder.Eval(Container.DataItem, "FileName")==null || DataBinder.Eval(Container.DataItem, "FileName").ToString() == string.Empty?string.Empty: string.Format("<fieldset>{0}</fieldset>", DataBinder.Eval(Container.DataItem, "FileName"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridDateTimeColumn DataField="CreatedDateTime" HeaderText="Create" UniqueName="CreatedDateTime"
                    SortExpression="CreatedDateTime">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="CreatedByUserID" HeaderText="User" UniqueName="CreatedByUserID"
                    SortExpression="CreatedByUserID">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings Selecting-AllowRowSelect="false">
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
