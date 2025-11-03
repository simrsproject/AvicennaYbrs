<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PhysicianTeamInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PhysicianTeamInfo" %>

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
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo,ParamedicID,StartDate"
                        GroupLoadMode="Client">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ParamedicID" HeaderText="ID"
                                UniqueName="ParamedicID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ParamedicName" HeaderText="Physician"
                                UniqueName="ParamedicName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ParamedicTeamStatus"
                                HeaderText="Status" UniqueName="ParamedicTeamStatus" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="StartDate" HeaderText="Start Date"
                                UniqueName="StartDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="EndDate" HeaderText="End Date"
                                UniqueName="EndDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
