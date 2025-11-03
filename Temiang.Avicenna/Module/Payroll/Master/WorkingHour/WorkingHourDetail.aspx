<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="WorkingHourDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WorkingHourDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Working Hour Name
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtWorkingHourName" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvWorkingHourName" runat="server" ErrorMessage="Working Hour Name required."
                                ValidationGroup="entry" ControlToValidate="txtWorkingHourName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Shift Category
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRShift" runat="server" Width="300px" AllowCustomText="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Working Day
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRWorkingDay" runat="server" Width="300px" AllowCustomText="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="text-align: center;" colspan="2"><b>Schedule 1</b></td>
                                            </tr>
                                            <tr>
                                                <td class="label">Check In Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtStartTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ErrorMessage="Check In Time required."
                                                        ValidationGroup="entry" ControlToValidate="txtStartTime" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Min. Check In Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMinimumStartTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMinimumStartTime" runat="server" ErrorMessage="Min. Check In Time required."
                                                        ValidationGroup="entry" ControlToValidate="txtMinimumStartTime" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Max. Check In Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMaxStartTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMaxStartTime" runat="server" ErrorMessage="Max. Check In Time required."
                                                        ValidationGroup="entry" ControlToValidate="txtMaxStartTime" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Check Out Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtEndTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ErrorMessage="Check Out Time required."
                                                        ValidationGroup="entry" ControlToValidate="txtEndTime" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Min. Check Out Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMinEndTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMinEndTime" runat="server" ErrorMessage="Min. Check Out Time required."
                                                        ValidationGroup="entry" ControlToValidate="txtMinEndTime" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Max. Check Out Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMaxEndTime" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMaxEndTime" runat="server" ErrorMessage="Max. Check Out Time required."
                                                        ValidationGroup="entry" ControlToValidate="txtMaxEndTime" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label"></td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsShiftLeader" runat="server" Text="Shift Leader" />
                                                </td>
                                                <td width="20px"></td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label" />
                                                <td>
                                                    <asp:CheckBox ID="chkIsLongShift" runat="server" Text="Long Shift" />
                                                </td>
                                                <td width="20px" />
                                                <td />
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align:top">
                                        <table>
                                            <tr>
                                                <td style="text-align: center;" colspan="2"><b>Schedule 2</b></td>
                                            </tr>
                                            <tr>
                                                <td class="label">Check In Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtStartTime2" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvStartTime2" runat="server" ErrorMessage="Check In Time 2 required."
                                                        ValidationGroup="entry" ControlToValidate="txtStartTime2" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Min. Check In Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMinimumStartTime2" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMinimumStartTime2" runat="server" ErrorMessage="Min. Check In Time 2 required."
                                                        ValidationGroup="entry" ControlToValidate="txtMinimumStartTime2" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Max. Check In Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMaxStartTime2" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMaxStartTime2" runat="server" ErrorMessage="Max. Check In Time 2 required."
                                                        ValidationGroup="entry" ControlToValidate="txtMaxStartTime2" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Check Out Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtEndTime2" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvEndTime2" runat="server" ErrorMessage="Check Out Time 2 required."
                                                        ValidationGroup="entry" ControlToValidate="txtEndTime2" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Min. Check Out Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMinEndTime2" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMinEndTime2" runat="server" ErrorMessage="Min. Check Out Time 2 required."
                                                        ValidationGroup="entry" ControlToValidate="txtMinEndTime2" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">Max. Check Out Time
                                                </td>
                                                <td>
                                                    <telerik:RadMaskedTextBox ID="txtMaxEndTime2" runat="server" Mask="<00..23>:<00..59>"
                                                        PromptChar="_" RoundNumericRanges="false" Width="100px">
                                                    </telerik:RadMaskedTextBox>
                                                </td>
                                                <td width="20px">
                                                    <asp:RequiredFieldValidator ID="rfvMaxEndTime2" runat="server" ErrorMessage="Max. Check Out Time 2 required."
                                                        ValidationGroup="entry" ControlToValidate="txtMaxEndTime2" SetFocusOnError="True"
                                                        Width="100%">
                                                        <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label"></td>
                                                <td>
                                                    <asp:CheckBox ID="chkIsShiftLeader2" runat="server" Text="Shift Leader" />
                                                </td>
                                                <td width="20px"></td>
                                                <td />
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Meal Qty</td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtMealQty" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMealQty" runat="server" ErrorMessage="Meal Qty required."
                                ValidationGroup="entry" ControlToValidate="txtMealQty" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td>
                            <asp:CheckBox ID="chkIsOverTime" runat="server" Text="Overtime" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Overtime Value In Hour
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtOvertimeValueInMinutes" runat="server" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td>
                            <asp:CheckBox ID="chkIsNotValidated" runat="server" Text="Not Validated" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td>
                            <asp:CheckBox ID="chkIsOffDay" runat="server" Text="Off Day" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>

                    <tr>
                        <td class="label" />
                        <td>
                            <asp:CheckBox ID="chkIsCrossDay" runat="server" Text="Cross Day" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td>
                            <asp:CheckBox ID="chkIsHoliday" runat="server" Text="Holiday" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <telerik:RadGrid ID="grdAliasName" runat="server" OnNeedDataSource="grdAliasName_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAliasName_UpdateCommand"
                    OnDeleteCommand="grdAliasName_DeleteCommand" OnInsertCommand="grdAliasName_InsertCommand"
                    AllowPaging="true">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="WorkingHourID, OrganizationUnitID"
                        PageSize="20">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="OrganizationUnitName" HeaderText="Organization Unit Name" UniqueName="OrganizationUnitName"
                                SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="WorkingHourItem.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="WorkingHourItemEditCommand">
                            </EditColumn>
                        </EditFormSettings>
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
