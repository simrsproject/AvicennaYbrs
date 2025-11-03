<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientTransferDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientTransferDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowSorting="True"
                    GridLines="None" ShowStatusBar="true">
                    <MasterTableView DataKeyNames="TransferNo" GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="RegistrationNo" HeaderText="Registration No ">
                                    </telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="RegistrationNo" SortOrder="Descending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="TransferDate" HeaderText="Date"
                                UniqueName="TransferDate" SortExpression="TransferDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="TransferTime" HeaderText="Time"
                                UniqueName="TransferTime" SortExpression="TransferTime" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridDateTimeColumn DataField="FromServiceUnitName" HeaderText="From Service Unit"
                                UniqueName="FromServiceUnitName" SortExpression="FromServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn DataField="FromRoomName" HeaderText="From Room"
                                UniqueName="FromRoomName" SortExpression="FromRoomName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn DataField="FromBedID" HeaderText="From Bed No" UniqueName="FromBedID"
                                SortExpression="FromBedID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn DataField="FromChargeClassName" HeaderText="From Charge Class" UniqueName="FromChargeClassName"
                                SortExpression="FromChargeClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn DataField="ToServiceUnitName" HeaderText="To Service Unit"
                                UniqueName="ToServiceUnitName" SortExpression="ToServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn DataField="ToRoomName" HeaderText="To Room" UniqueName="ToRoomName"
                                SortExpression="ToRoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn DataField="ToBedID" HeaderText="To Bed No" UniqueName="ToBedID"
                                SortExpression="ToBedID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridDateTimeColumn DataField="ToChargeClassName" HeaderText="To Charge Class" UniqueName="ToChargeClassName"
                                SortExpression="ToChargeClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsApprove" HeaderText="Approved"
                                UniqueName="IsApprove" SortExpression="IsApprove" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid" HeaderText="Void"
                                UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" AllowExpandCollapse="true">
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
