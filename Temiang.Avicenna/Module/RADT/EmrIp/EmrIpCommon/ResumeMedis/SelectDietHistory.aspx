<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SelectDietHistory.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ResumeMedis.SelectDietHistory" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function GetSelectedDescription() {
            var grid = $find("<%=grdList.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var retVal = "<ul>";
            var selectedRows = masterTable.get_selectedItems();
            for (var i = 0; i < selectedRows.length; i++) {
                var row = selectedRows[i];
                var cell = masterTable.getCellByColumnUniqueName(row, "Description")
                //here cell.innerHTML holds the value of the cell
                retVal = retVal + "<li>"+ cell.innerHTML.trim() +"</li>\n";
            }
            return retVal+"\n</ul>";
        }

        function CloseAndReturnValue() {
            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            var oArg = new Object();
            oArg.callbackMethod = '<%=Request.QueryString["ccm"]%>';
            oArg.eventArgument = '<%=Request.QueryString["cea"]%>';
            oArg.eventTarget = '<%=Request.QueryString["cet"]%>';
            oArg.value = GetSelectedDescription();

            //Close the RadWindow            
            oWnd.close(oArg);
        }
    </script>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None"
        AllowPaging="false" AllowMultiRowSelection="True">
        <MasterTableView>
            <Columns>
                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="40px"></telerik:GridClientSelectColumn>
                <telerik:GridTemplateColumn UniqueName="Description" HeaderStyle-Width="350px" HeaderText="Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
        </ClientSettings>
    </telerik:RadGrid>
    <br />
    <br />
</asp:Content>
