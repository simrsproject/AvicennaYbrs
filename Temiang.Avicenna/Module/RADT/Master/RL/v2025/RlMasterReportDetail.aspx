<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RlMasterReportDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.v2025.RlMasterReportDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%;vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblRlMasterReportID" runat="server" Text="RL Master Report ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtRlMasterReportID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRlMasterReportNo" runat="server" Text="RL Master Report No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRlMasterReportNo" runat="server" Width="300px" MaxLength="10" ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRlMasterReportNo" runat="server" ErrorMessage="RL Master Report No required."
                                ValidationGroup="entry" ControlToValidate="txtRlMasterReportNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRlMasterReportName" runat="server" Text="RL Master Report Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRlMasterReportName" runat="server" Width="300px" MaxLength="300" ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRlMasterReportName" runat="server" ErrorMessage="RL Master Report Name required."
                                ValidationGroup="entry" ControlToValidate="txtRlMasterReportName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Enabled="False" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" ReadOnly="True" Height="70px"/>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdRlMasterReportItem" runat="server" OnNeedDataSource="grdRlMasterReportItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRlMasterReportItem_UpdateCommand"
        OnDeleteCommand="grdRlMasterReportItem_DeleteCommand" OnInsertCommand="grdRlMasterReportItem_InsertCommand">
        <HeaderContextMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="RlMasterReportItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RlMasterReportItemID"
                    HeaderText="RL Master Report Item ID" UniqueName="RlMasterReportItemID" SortExpression="RlMasterReportItemID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RlMasterReportID"
                    HeaderText="RL Master Report ID" UniqueName="RlMasterReportID" SortExpression="RlMasterReportID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RlMasterReportItemNo"
                    HeaderText="No" UniqueName="RlMasterReportItemNo" SortExpression="RlMasterReportItemNo"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="RlMasterReportItemCode"
                    HeaderText="Report Code" UniqueName="RlMasterReportItemCode" SortExpression="RlMasterReportItemCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="RlMasterReportItemName" HeaderText="Report Name"
                    UniqueName="RlMasterReportItemName" SortExpression="RlMasterReportItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParamedicRL1Name" HeaderText="SMF"
                    UniqueName="ParamedicRL1Name" SortExpression="ParamedicRL1Name" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ParameterValue" HeaderText="Parameter Value"
                    UniqueName="ParameterValue" SortExpression="ParameterValue" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Updater" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <EditFormSettings UserControlName="RlMasterReportItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="RlMasterReportItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
