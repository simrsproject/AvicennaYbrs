<%@ Page Title="Booking Reference" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="BookingReferenceDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.Phr.BookingReferenceDialog" %>

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
            <td class="label">
                <asp:Label ID="lblBookingDate" runat="server" Text="Booking Date" />
            </td>
            <td class="entry">
                <telerik:RadDatePicker runat="server" ID="txtBookingDate" Width="100px" />
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
        <MasterTableView DataKeyNames="BookingNo" ClientDataKeyNames="BookingNo">
            <Columns>
                <telerik:GridBoundColumn DataField="BookingNo" HeaderText="BookingNo" UniqueName="BookingNo"
                    SortExpression="BookingNo">
                    <HeaderStyle HorizontalAlign="Center" Width="135px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="BookingDateTimeFrom" HeaderText="Booking From"
                    Visible="false" UniqueName="BookingDateTimeFrom" SortExpression="BookingDateTimeFrom">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="BookingDateTimeTo" HeaderText="Booking To"
                    Visible="false" UniqueName="BookingDateTimeTo" SortExpression="BookingDateTimeTo">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="RealizationDateTimeFrom" HeaderText="Realization From" DataFormatString="{0:MM/dd/yyyy HH:mm}"
                    UniqueName="RealizationDateTimeFrom" SortExpression="RealizationDateTimeFrom">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RealizationDateTimeTo" HeaderText="Realization To" DataFormatString="{0:MM/dd/yyyy HH:mm}"
                    UniqueName="RealizationDateTimeTo" SortExpression="RealizationDateTimeTo">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room Name" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Center" Width="95px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Surgeon Name" UniqueName="ParamedicName"
                    SortExpression="ParamedicName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AnesthesiologistName" HeaderText="Anesthesiologist Name"
                    UniqueName="AnesthesiologistName" SortExpression="AnesthesiologistName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
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
