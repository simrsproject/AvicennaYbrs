<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="MergeBillingInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.MergeBillingInfo" %>

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
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo, FromRegistrationNoBefore, FromRegistrationNoAfter, LastUpdateDateTime"
                        GroupLoadMode="Client">
                        <Columns>
                            <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Update Date Time"
                                UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="UsrUpdate" HeaderText="Update By" UniqueName="UsrUpdate"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FromRegistrationNoBefore" HeaderText="Merge To (Before)"
                                UniqueName="FromRegistrationNoBefore" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FromRegistrationNoAfter" HeaderText="Merge To (After)"
                                UniqueName="FromRegistrationNoAfter" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn />
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
