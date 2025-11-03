<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ResearchLetterDocumentHist.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEPK.ResearchLetterDocumentHist" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" src="<%= Helper.UrlRoot() %>/JavaScript/jquery.js"></script>
        <script type="text/javascript" language="javascript">
            function tbarMain_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "close":
                        Close();
                        args.set_cancel(true);
                        break;
                    case "refresh":
                        __doPostBack("<%=grdDocument.UniqueID %>", "refresh");
                        args.set_cancel(true);
                        break;
                    case "delete":
                        if (!window.confirm('Are you sure to delete this data ?')) {
                            args.set_cancel(true);
                        }
                        break;
                    case "newupload":
                        showDetail("new", "");
                        break;
                    case "batchupload":
                        var url = '<%= Helper.UrlRoot() %>/Module/HR/KEPK/ResearchLetterDocument/ResearchLetterDocumentBatchUpload.aspx?pid=<%= Request.QueryString["pid"] %>&ccm=rebind&cet=<%=grdDocument.ClientID %>';
                        openWinMaximize(url);
                        break;
                    case "newwebcam":
                        var url = '<%= Helper.UrlRoot() %>/Module/HR/KEPK/ResearchLetterDocument/ResearchLetterDocumentWebCam.aspx?mod=new&pid=<%= Request.QueryString["pid"] %>&ccm=rebind&cet=<%=grdDocument.ClientID %>';
                        //openWindow(url, 800, 600);
                        openWinMaximize(url);
                        break;
                }
            }
            function showDetail(mode, pdid) {
                var url = '<%= Helper.UrlRoot() %>/Module/HR/KEPK/ResearchLetterDocument/ResearchLetterDocumentUpload.aspx?md=' + mode + '&pdid=' + pdid +'&pid=<%= Request.QueryString["pid"] %>&ccm=rebind&cet=<%=grdDocument.ClientID %>';
                openWindow(url, 600, 500);
            }

            $.download = function (url, data, method) {
                //url and data options required
                if (url && data) {
                    //data can be string of parameters or array/object
                    data = typeof data == 'string' ? data : $.param(data);
                    //split params into form inputs
                    var inputs = '';
                    $.each(data.split('&'), function () {
                        var pair = this.split('=');
                        inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
                    });
                    //send request
                    $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
                };
            };

            function ShowFile(id, fileName) {
                if (fileName.toLowerCase().includes(".dcm") == true) // DICOM file gunakan external app
                {
                    $.download("ResearchLetterDocumentDownload.aspx", "id=" + id);
                }
                else {
                    var url;
                    if (fileName.toLowerCase().includes(".pdf") == true) {
                        url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=patdoc&id=" + id;
                    }
                    else {
                        url = "ResearchLetterDocumentImageZoomView.aspx?id=" + id +"&pid=<%= Request.QueryString["pid"] %>";
                    }
                    openWinMaximize(url);
                }
            }
            function openWinMaximize(url) {
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }
            function openWindow(url, width, height) {
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                oWnd.show();
            }
            function winEntry_ClientClose(oWnd, args) {
                //get the transferred arguments from Dialog
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {
                        if (arg.callbackMethod === 'rebind') {
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }
                        }
                    }
                }
            }

            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                // set height to the whole RadGrid control
                var grid = $find("<%= grdDocument.ClientID %>");
                grid.get_element().style.height = height - 40 + "px";
                grid.repaint();
            }
            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });


            var delay = (function () {
                var timer = 0;
                return function (callback, ms) {
                    clearTimeout(timer);
                    timer = setTimeout(callback, ms);
                };
            })();

            function QuickSearchKeypress(sender, args) {
                var c = args.get_keyCode();
                if (c == 13) {
                    ApplyQuickSearch();
                }
                else {
                    delay(function () { ApplyQuickSearch(); }, 1000);
                }
            }
            function ApplyQuickSearch() {
                __doPostBack("<%=grdDocument.UniqueID %>", "quicksearch");
            }
        </script>
    </telerik:RadCodeBlock>


    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server" OnClientClose="winEntry_ClientClose"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <telerik:RadToolBar ID="tbarMain" runat="server" Width="100%"
        EnableEmbeddedScripts="false" OnClientButtonClicking="tbarMain_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Add Using Webcam" Value="newwebcam" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton runat="server" Text="Upload File" Value="newupload" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server">
                <ItemTemplate>
                    &nbsp;&nbsp;Quick Search&nbsp;
                        <telerik:RadTextBox runat="server" ID="txtQuickSearch" Width="300px">
                            <ClientEvents OnKeyPress="QuickSearchKeypress"></ClientEvents>
                        </telerik:RadTextBox>
                </ItemTemplate>
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png">
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Close" Value="close" ImageUrl="~/Images/Toolbar/close16.png" />

        </Items>
    </telerik:RadToolBar>

    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDocument">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tbarMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDocument" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadGrid runat="server" ID="grdDocument" OnNeedDataSource="grdDocument_NeedDataSource"
        OnDeleteCommand="grdDocument_DeleteCommand"
        Height="530px"
        AllowSorting="true">
        <MasterTableView ShowHeader="true" AutoGenerateColumns="False" AllowPaging="false"
            DataKeyNames="DocumentID">
            <CommandItemStyle Height="29px" />
            <Columns>

                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"showDetail('view', '{0}')\"><img src=\"../../../../../Images/Toolbar/views16.png\" border=\"0\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "DocumentID"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="File" HeaderText="File">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FileAttachName").ToString() == string.Empty ? string.Empty : string.Format("<a href=\"#\" onclick=\"$.download('ResearchLetterDocumentDownload.aspx','id={0}'); return false;\"><img src=\"../../../../../Images/Toolbar/download16.png\" border=\"0\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "DocumentID"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="colSmallImage" HeaderText="" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <div style="width: 130px; text-align: center;">
                            <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="View"
                                OnClientClick='<%#string.Format("javascript:ShowFile(\"{0}\",\"{1}\");return false;",DataBinder.Eval(Container.DataItem, "DocumentID"), Eval("FileAttachName"))%>'>
                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                    Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("SmallImage") == DBNull.Value? new System.Byte[0]: Eval("SmallImage") %>'></telerik:RadBinaryImage>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="DocumentName" HeaderText="Document Name"
                    UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="DocumentDate" HeaderText="Document Date"
                    UniqueName="DocumentDate" SortExpression="DocumentDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Resizing AllowColumnResize="true" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>