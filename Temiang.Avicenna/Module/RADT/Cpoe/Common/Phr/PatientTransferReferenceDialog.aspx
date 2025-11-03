<%@ Page Title="Patient Transfer Reference" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientTransferReferenceDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PatientTransferReferenceDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterBookingDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table>
        <tr>
            <td class="label">Transfer Date
            </td>
            <td class="entry">
                <telerik:RadDatePicker runat="server" ID="txtTransferDate" Width="100px" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnFilterBookingDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnFilter_Click" ToolTip="Search" />
            </td>
            <td />
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="TransferNo" ClientDataKeyNames="TransferNo">
            <Columns>
                <telerik:GridBoundColumn DataField="TransferNo" HeaderText="TransferNo" UniqueName="TransferNo"
                    SortExpression="TransferNo">
                    <HeaderStyle HorizontalAlign="Center" Width="135px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DateOfEntry" HeaderText="Transfer Date"
                    UniqueName="DateOfEntry" SortExpression="DateOfEntry">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="TransferByUserName" HeaderText="Transfer By" UniqueName="TransferByUserName"
                    SortExpression="TransferByUserName">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room Name" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Center" Width="95px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed" UniqueName="BedID"
                    SortExpression="BedID">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="ArrivedDateTime" HeaderText="Arrive Date"
                    UniqueName="ArrivedDateTime" SortExpression="ArrivedDateTime">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ReceivedByUserName" HeaderText="Receive By" UniqueName="ReceivedByUserName"
                    SortExpression="ReceivedByUserName">
                    <HeaderStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
