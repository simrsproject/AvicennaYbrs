<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PrinterList.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.PrinterManagement.PrinterList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function rowDelete(printNo) {
                if (confirm('Are you sure to delete print log for selected ID?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'delete|' + printNo);
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="PrinterID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PrinterID" HeaderText="Printer ID"
                    UniqueName="PrinterID" SortExpression="PrinterID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="PrinterName" HeaderText="Printer Name"
                    UniqueName="PrinterName" SortExpression="PrinterName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="PrinterLocationHost"
                    HeaderText="Printer Host" UniqueName="PrinterLocationHost" SortExpression="PrinterLocationHost"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="PrinterManagerHost"
                    HeaderText="Printer Manager" UniqueName="PrinterManagerHost" SortExpression="PrinterManagerHost"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"rowDelete('{0}'); return false;\">{1}</a>",
                                                                    DataBinder.Eval(Container.DataItem, "PrinterID"), 
                                        "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Clear Print Job\" />")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
