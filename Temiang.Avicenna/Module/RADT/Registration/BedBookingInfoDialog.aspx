<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="BedBookingInfoDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.BedBookingInfoDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend>BED BOOKING INFORMATION</legend>
                    <table width="100%">
                        <tr>
                            <td width="100%" style="vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBedID" runat="server" Text="Bed No / Room"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtRoomName" runat="server" Width="289px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblUnitRoom" runat="server" Text="Service Unit"></asp:Label>
                                        </td>
                                        <td class="entry2Column">
                                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="400px" />
                                        </td>
                                        <td width="20">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grdBedManagement" runat="server" OnNeedDataSource="grdBedManagement_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="10"
                                    AllowSorting="False">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="BedManagementID" GroupLoadMode="Client">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="BedManagementID" HeaderText="ID" UniqueName="BedManagementID"
                                                SortExpression="BedManagementID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                Visible="False" />
                                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                                                HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BedStatusName" HeaderText="Status"
                                                UniqueName="BedStatusName" SortExpression="BedStatusName" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                                                UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReservationNo" HeaderText="Reservation No"
                                                UniqueName="ReservationNo" SortExpression="ReservationNo" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                                                SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                                                SortExpression="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
