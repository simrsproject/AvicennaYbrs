<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="AutoNumberDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Setting.AppAutoNumberDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRAutoNumber" runat="server" Text="Auto Number ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRAutoNumber" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRAutoNumber" runat="server" ErrorMessage="Auto Number required."
                                ValidationGroup="entry" ControlToValidate="cboSRAutoNumber" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEffectiveDate" runat="server" Text="Effective Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtEffectiveDate" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server" ErrorMessage="Effective Date required."
                                ValidationGroup="entry" ControlToValidate="txtEffectiveDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrefik" runat="server" Text="Prefik"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPrefik" runat="server" Width="100px" MaxLength="5" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeparatorAfterPrefik" runat="server" Text="Separator After Prefik"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeparatorAfterPrefik" runat="server" Width="300px" MaxLength="1" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsedDepartment" runat="server" Text="Used Department" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeparatorAfterDept" runat="server" Text="Separator After Dept"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeparatorAfterDept" runat="server" Width="300px" MaxLength="1" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsedYear" runat="server" Text="Used Year" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblYearDigit" runat="server" Text="Year Digit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtYearDigit" runat="server" Width="100px" NumberFormat-DecimalDigits="0">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20">
                            <asp:RangeValidator ID="ravYearDigit" runat="server" MinimumValue="0" MaximumValue="4"
                                Type="Integer" ErrorMessage="Year Digit min 2 and max 4." ValidationGroup="entry"
                                ControlToValidate="txtYearDigit" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RangeValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeparatorAfterYear" runat="server" Text="Separator After Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeparatorAfterYear" runat="server" Width="300px" MaxLength="1" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsedMonth" runat="server" Text="Used Month" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsMonthInRomawi" runat="server" Text="Month In Roman Symbol Number" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeparatorAfterMonth" runat="server" Text="Separator After Month"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeparatorAfterMonth" runat="server" Width="300px" MaxLength="1" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsedDay" runat="server" Text="Used Day" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeparatorAfterDay" runat="server" Text="Separator After Day"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeparatorAfterDay" runat="server" Width="300px" MaxLength="1" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNumberLength" runat="server" Text="Number Length"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNumberLength" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20">
                            <asp:RangeValidator ID="ravNumberLength" runat="server" MinimumValue="3" MaximumValue="20"
                                Type="Integer" ErrorMessage="Number Length min 3 and max 20." ValidationGroup="entry"
                                ControlToValidate="txtNumberLength" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RangeValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSeparatorAfterNumber" runat="server" Text="Separator After Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSeparatorAfterNumber" runat="server" Width="300px" MaxLength="1" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNumberGroupLength" runat="server" Text="Number Group Length"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNumberGroupLength" runat="server" Width="100px"
                                MinValue="0" MaxValue="3" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNumberGroupSeparator" runat="server" Text="Number Group Separator"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNumberGroupSeparator" runat="server" Width="300px" MaxLength="1" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsedYearToDateOrder" runat="server" Text="Used Year To Date Order" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdAppAutoNumberLast" runat="server" OnNeedDataSource="grdAppAutoNumberLast_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAppAutoNumberLast_UpdateCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SRAutoNumber,EffectiveDate,YearNo, MonthNo, DayNo">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DepartmentInitial"
                                HeaderText="Dept" UniqueName="DepartmentInitial" SortExpression="DepartmentInitial"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="YearNo" HeaderText="Year"
                                UniqueName="YearNo" SortExpression="YearNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MonthNo" HeaderText="Month"
                                UniqueName="MonthNo" SortExpression="MonthNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DayNo" HeaderText="Day"
                                UniqueName="DayNo" SortExpression="DayNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LastNumber" HeaderText="Last Number"
                                UniqueName="LastNumber" SortExpression="LastNumber" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="LastCompleteNumber" HeaderText="Last Complete Number"
                                UniqueName="LastCompleteNumber" SortExpression="LastCompleteNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <EditFormSettings UserControlName="AutoNumberLastDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="AutoNumberLastEditCommand">
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
