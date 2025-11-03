<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="MealAttendanceDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.MealAttendanceDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="label">Open Datetime</td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker runat="server" ID="txtOpenDate" Width="100px">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <telerik:RadMaskedTextBox ID="txtOpenTime" runat="server" Mask="<00..23>:<00..59>"
                                PromptChar="_" RoundNumericRanges="false" Width="100px">
                            </telerik:RadMaskedTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Working Hour Name required."
                    ValidationGroup="entry" ControlToValidate="txtOpenDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Working Hour Name required."
                    ValidationGroup="entry" ControlToValidate="txtOpenTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Close Datetime</td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker runat="server" ID="txtCloseDate" Width="100px"></telerik:RadDatePicker>
                        </td>
                        <td>
                            <telerik:RadMaskedTextBox ID="txtCloseTime" runat="server" Mask="<00..23>:<00..59>"
                                PromptChar="_" RoundNumericRanges="false" Width="100px">
                            </telerik:RadMaskedTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Working Hour Name required."
                    ValidationGroup="entry" ControlToValidate="txtCloseDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Status</td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboStatusID" Width="304px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Open" Value="1" />
                        <telerik:RadComboBoxItem runat="server" Text="Close" Value="2" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Working Hour Name required."
                    ValidationGroup="entry" ControlToValidate="cboStatusID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">Notes</td>
            <td class="entry">
                <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine"></telerik:RadTextBox>
            </td>
            <td width="20px"></td>
            <td />
        </tr>
    </table>
    <telerik:RadGrid ID="grdPersonalAddress" runat="server" OnNeedDataSource="grdPersonalAddress_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPersonalAddress_UpdateCommand"
        OnDeleteCommand="grdPersonalAddress_DeleteCommand" OnInsertCommand="grdPersonalAddress_InsertCommand">
        <HeaderContextMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="MealAttendanceDetailID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="Datetime"
                    HeaderText="Datetime" UniqueName="Datetime" SortExpression="Datetime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EmployeeName" HeaderText="Employee Name"
                    UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ServiceUnitName" HeaderText="Service Unit Name"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="MealAttendanceItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="MealAttendanceItemEditCommand">
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
