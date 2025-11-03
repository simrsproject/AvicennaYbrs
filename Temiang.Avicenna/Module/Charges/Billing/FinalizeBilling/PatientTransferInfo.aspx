<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PatientTransferInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PatientTransferInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="5"
                    AllowSorting="False">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="TransferNo" GroupLoadMode="Client">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="TransferNo" HeaderText="Transfer No"
                                UniqueName="TransferNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransferDate" HeaderText="Transfer Date"
                                UniqueName="TransferDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="TransferTime" HeaderText="Transfer Time"
                                UniqueName="TransferTime" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="FromUnit" HeaderText="From Unit" UniqueName="FromUnit"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="FromRoomName" HeaderText="From Room" UniqueName="FromRoomName"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="FromBedID" HeaderText="From Bed No" UniqueName="FromBedID"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="FromClass" HeaderText="From Class" UniqueName="FromClass"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />   
                            <telerik:GridBoundColumn DataField="FromChargeClass" HeaderText="From Charge Class" UniqueName="FromChargeClass"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />   
                            <telerik:GridBoundColumn DataField="ToUnit" HeaderText="To Unit" UniqueName="ToUnit"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />    
                            <telerik:GridBoundColumn DataField="ToRoomName" HeaderText="To Room" UniqueName="ToRoomName"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="ToBedID" HeaderText="To Bed No" UniqueName="ToBedID"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="ToClass" HeaderText="To Class" UniqueName="ToClass"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />  
                            <telerik:GridBoundColumn DataField="ToChargeClass" HeaderText="To Charge Class" UniqueName="ToChargeClass"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />  
                            <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" Visible="False" />
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
</asp:Content>
