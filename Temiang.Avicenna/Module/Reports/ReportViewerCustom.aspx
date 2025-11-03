<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerCustom.aspx.cs" Inherits="Temiang.Avicenna.Module.Reports.ReportViewerCustom" %>



<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=17.0.23.118, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement && window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function close() {
            var oWindow = GetRadWindow();
            if (oWindow) {
                oWindow.close();
            }
            else {
                window.close();
            }
        }

    </script>

    <form id="form1" runat="server" style="width: 100%; height: 100%;">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        

        <table width="100%">
            <tr>
                <td>
                    <telerik:RadToolBar ID="tbarPrintPreview" runat="server" Width="100%" OnClientButtonClicked="OnClientButtonClickingHandler"
                        OnButtonClick="tbarPrintPreview_ButtonClick">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                        <Items>
                            <telerik:RadToolBarButton runat="server" Value="FirstPage" ToolTip="First Page" PostBack="False" Text="|<<">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server"  Value="PrevPage" ToolTip="Previous Page" Text="<" PostBack="False">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" PostBack="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" Width="30px" runat="server" ToolTip="Current Page">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" Width="40px" runat="server" ToolTip="Total Pages">
                                    </asp:Label>
                                </ItemTemplate>
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server"  Value="NextPage" ToolTip="Next Page" Text=">" PostBack="False">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server"  Value="LastPage" ToolTip="Last Page" PostBack="False" Text=">>|">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarDropDown runat="server" PostBack="False" Text="Export To" >
                                <Buttons>
                                    <telerik:RadToolBarButton runat="server" Text="Excel" Value="exp_XLS" PostBack="False" />
                                    <telerik:RadToolBarButton runat="server" Text="TIFF file" Value="exp_IMAGE" PostBack="False"/>
                                    <telerik:RadToolBarButton runat="server" Text="Acrobat (PDF) file" Value="exp_PDF" PostBack="False"/>
                                    <telerik:RadToolBarButton runat="server" Text="Rich Text Format" Value="exp_RTF" PostBack="False"/>
                                    <telerik:RadToolBarButton runat="server" Text="Web Archive" Value="exp_MHTML" PostBack="False"/>
                                    <telerik:RadToolBarButton runat="server" Text="CSV (comma delimited)" Value="exp_CSV" PostBack="False"/>
                                </Buttons>
                            </telerik:RadToolBarDropDown>
                            <telerik:RadToolBarButton runat="server" ToolTip="Print" DisabledImageUrl="~/Images/Toolbar/Print16_d.png"
                                ImageUrl="~/Images/Toolbar/Print16.png" Value="ShowDialogPrint">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" ToolTip="Print in Service Unit Printer" DisabledImageUrl="~/Images/Toolbar/process16_d.png"
                                ImageUrl="~/Images/Toolbar/process16.png" Value="PrintDirect">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton ToolTip="Save as PDF to SEP Folder" DisabledImageUrl="~/Images/Toolbar/Save16_d.png"
                                ImageUrl="~/Images/Toolbar/Save16.png" Value="SaveToSep">
                            </telerik:RadToolBarButton>
                        </Items>
                    </telerik:RadToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%"
                        AsyncRendering="False" SizeToReportContent="True">
                    </telerik:ReportViewer>
                    &nbsp;
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">

        var viewer = <%=ReportViewer1.ClientID%>;
        var RPToolbar = document.getElementById(viewer.toolbarID);
        RPToolbar.style.display = "none";
        var toolbar = null;
        var firstP = null;
        var prevP = null;
        var lastP = null;
        var nextP = null;
        var select = null;
                
        function pageLoad()
        { 
            toolbar = $find('<%=tbarPrintPreview.ClientID %>');
            firstP = toolbar.get_items().getItem(0);
            prevP = toolbar.get_items().getItem(1);
            nextP = toolbar.get_items().getItem(4);
            lastP = toolbar.get_items().getItem(5);
            prevP.disable();
            firstP.disable();
        }

        viewer.baseOnReportLoaded = viewer.OnReportLoaded;
           
        viewer.OnReportLoaded = function()
        {  
            this.baseOnReportLoaded();
            var textbox = document.getElementById("tbarPrintPreview_i2_TextBox1");
            textbox.value = this.get_CurrentPage();
            var label = document.getElementById("tbarPrintPreview_i3_Label1");
            label.innerHTML = " of " + viewer.get_TotalPages();
        }
            
        function OnClientButtonClickingHandler(sender, eventArgs) {
            var value = eventArgs.get_item().get_value();
            switch (value) {
                case "FirstPage": // First Page
                    viewer.set_CurrentPage(1);
                    prevP.disable();
                    firstP.disable();
                    nextP.enable();
                    lastP.enable();
                    break;
                case "PrevPage": // Prev Page
                    if (viewer.get_CurrentPage() > 2)
                    {
                        viewer.set_CurrentPage(viewer.get_CurrentPage() - 1);
                        nextP.enable();
                        lastP.enable();
                    }
                    else if (viewer.get_CurrentPage() == 2)
                    {
                        viewer.set_CurrentPage(viewer.get_CurrentPage() - 1);
                        prevP.disable();
                        firstP.disable();

                    }
                    break;
                case "NextPage": // Next Page
                    if (viewer.get_TotalPages() > viewer.get_CurrentPage() + 1)
                    {
                        viewer.set_CurrentPage(viewer.get_CurrentPage()+ 1);
                        firstP.enable();
                        prevP.enable();
                    }
                    else if (viewer.get_TotalPages() == viewer.get_CurrentPage() + 1)
                    {
                        viewer.set_CurrentPage(viewer.get_CurrentPage()+ 1);
                        nextP.disable();
                        lastP.disable();
                    }
                    break;
                case "LastPage": // Last Page
                    viewer.set_CurrentPage(viewer.get_TotalPages());
                    firstP.enable();
                    prevP.enable();
                    nextP.disable();
                    lastP.disable();
                    break;
                case "exp_XLS": 
                case "exp_PDF": 
                case "exp_RTF": 
                case "exp_MHTML": 
                case "exp_CSV": 
                    viewer.ExportReport(value.split('_')[1]);
                    break;
                case "TogglePreview":
                    break;
                default:
                    break;
            }


            //if(eventArgs.get_item().get_index() == 8)
            //{
            //    viewer.RefreshReport();
            //}
            //if(eventArgs.get_item().get_index() == 10)
            //{
            //    var select = document.getElementById("tbarPrintPreview_i9_DropDownList2");
            //    if (select.value == "Select")
            //    {
            //        alert('Please Select Print Format');
            //    }
            //    else
            //    {
            //        viewer.PrintAs(select.value);
            //    }
            //}
        }
    </script>

    <script language="javascript" type="text/javascript">
        ResizeReport();

        function ResizeReport() {
            var viewer = document.getElementById("<%= ReportViewer1.ClientID %>");
            var htmlheight = document.documentElement.clientHeight;
            viewer.style.height = (htmlheight - 30) + "px";
        }

        window.onresize = function resize() { ResizeReport(); }
    </script>

</body>
</html>
