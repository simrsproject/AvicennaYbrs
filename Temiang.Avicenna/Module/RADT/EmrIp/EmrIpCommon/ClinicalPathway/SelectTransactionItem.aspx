<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="SelectTransactionItem.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.ClinicalPathway.SelectTransactionItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="proxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRefresh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="optDayNoType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label" style="width: 50px;">
                <asp:Label ID="Label1" runat="server" Text="Search" Width="50px"></asp:Label></td>
            <td width="200px">
                <telerik:RadTextBox ID="txtSearch" runat="server" Width="100%">
                </telerik:RadTextBox></td>
            <td width="150px">
                <asp:RadioButtonList ID="optDayNoType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="All" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Day No" Value="D" Selected="true"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td width="80px">
                <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" Width="80px" /></td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" AllowSorting="true" AutoGenerateColumns="False"
        OnNeedDataSource="grdList_NeedDataSource" GridLines="None">
        <MasterTableView AllowSorting="true" DataKeyNames="ReferenceNo,ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                         SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DayNo" HeaderText="Day" UniqueName="DayNo"
                                         SortExpression="DayNo" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ReferenceNo" HeaderText="No" UniqueName="ReferenceNo"
                                         SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                                         SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"></telerik:GridDateTimeColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
