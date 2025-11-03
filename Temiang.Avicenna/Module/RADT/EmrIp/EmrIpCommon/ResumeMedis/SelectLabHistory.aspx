<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SelectLabHistory.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ResumeMedis.SelectLabHistory" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function GetSelectedDescription() {
            var grid = $find("<%=grdList.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var selectedRows = masterTable.get_selectedItems();

            var retVal = "";
            var orderNo = "";
            for (var i = 0; i < selectedRows.length; i++) {
                var row = selectedRows[i];
                if (orderNo != masterTable.getCellByColumnUniqueName(row, "TransactionNo").innerHTML.trim()) {
                    if (retVal != "") {
                        if ("<%=Request.QueryString["textmode"]%>" == "1") {
                            retVal = retVal + "\n"; 
                        }
                        else {
                            retVal = retVal + "</ul>"; // Close List
                        }
                    }


                    orderNo = masterTable.getCellByColumnUniqueName(row, "TransactionNo").innerHTML.trim();
                    var orderDate = masterTable.getCellByColumnUniqueName(row, "TransactionDate").innerHTML.trim();

                    if ("<%=Request.QueryString["textmode"]%>" == "1") {
                        retVal = retVal + "Lab No: " + orderNo + " (" + orderDate + ")\n";
                    }
                    else {
                        retVal = retVal + "<strong>Lab No: " + orderNo + " (" + orderDate + ")</strong>";
                        retVal = retVal + "<ul>"; // Begin List}
                    }
                }

                if ("<%=Request.QueryString["textmode"]%>" == "1") {
                    retVal = retVal + "- " + masterTable.getCellByColumnUniqueName(row, "Description").innerHTML.trim() + "\n";
                }
                else {
                    retVal = retVal + "<li>" + masterTable.getCellByColumnUniqueName(row, "Description").innerHTML.trim() + "</li>\n";
                }
            }
            if ("<%=Request.QueryString["textmode"]%>" != "1") {
                retVal = retVal + "</ul>"; // Close List
            }
            return retVal;
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
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="40px"></telerik:GridClientSelectColumn>
                <telerik:GridTemplateColumn UniqueName="Description" HeaderStyle-Width="350px" HeaderText="Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Order No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo" />
                <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Order Date" UniqueName="TransactionDate"
                    SortExpression="TransactionDate">
                </telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
        </ClientSettings>
    </telerik:RadGrid>
    <br />
    <br />
</asp:Content>
