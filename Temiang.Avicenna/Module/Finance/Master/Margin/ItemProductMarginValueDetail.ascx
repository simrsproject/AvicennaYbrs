<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemProductMarginValueDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.ItemProductMarginValueDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemProductMarginValue" runat="server" ValidationGroup="ItemProductMarginValue" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemProductMarginValue"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="30%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblStartingValue" runat="server" Text="Starting Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtStartingValue" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStartingValue" runat="server" ErrorMessage="StartingValue required."
                            ControlToValidate="txtStartingValue" SetFocusOnError="True" ValidationGroup="ItemProductMarginValue"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEndingValue" runat="server" Text="Ending Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEndingValue" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEndingValue" runat="server" ErrorMessage="EndingValue required."
                            ControlToValidate="txtEndingValue" SetFocusOnError="True" ValidationGroup="ItemProductMarginValue"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMarginPercentage" runat="server" Text="Global Percentage"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtMarginPercentage" runat="server" Type="Percent"
                            Width="100px" />
                        <asp:CheckBox ID="chkIsGlobalWithoutVAT" runat="server" Text="Without VAT" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvMarginPercentage" runat="server" ErrorMessage="Margin Percentage required."
                            ControlToValidate="txtMarginPercentage" SetFocusOnError="True" ValidationGroup="ItemProductMarginValue"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsMinusDiscount" runat="server" Text="Minus Discount" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="entry" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemProductMarginValue"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemProductMarginValue" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        Inpatient Value (%) Global
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtInpatientMarginPercentage" runat="server" Type="Percent"
                            Width="100px" />
                        <asp:CheckBox ID="chkIsIpWithoutVAT" runat="server" Text="Without VAT" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Inpatient Value (%) Detailed
                    </td>
                    <td class="entry">
                        <telerik:RadGrid ID="grdItemProductMarginValue" runat="server" AutoGenerateColumns="False"
                            GridLines="None">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="ClassID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name" UniqueName="ClassName"
                                        SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="75px"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="Value (%)" HeaderStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtValue" runat="server" Type="Percent" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "MarginValuePercentage")) %>'
                                                Width="60px" MinValue="0">
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td width="30%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        Outpatient Value (%)
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtOutpatientMarginPercentage" runat="server" Type="Percent"
                            Width="100px" />
                        <asp:CheckBox ID="chkIsOpWithoutVAT" runat="server" Text="Without VAT" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Emergency Value (%)
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmergencyMarginPercentage" runat="server" Type="Percent"
                            Width="100px" />
                        <asp:CheckBox ID="chkIsEmWithoutVAT" runat="server" Text="Without VAT" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        OTC Value (%)
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtOTCMarginPercentage" runat="server" Type="Percent"
                            Width="100px" />
                        <asp:CheckBox ID="chkIsOtcWithoutVAT" runat="server" Text="Without VAT" />
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
