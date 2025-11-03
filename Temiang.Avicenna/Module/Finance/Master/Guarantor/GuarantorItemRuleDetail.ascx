<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorItemRuleDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorItemRuleDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumGuarantorItemRule" runat="server" ValidationGroup="GuarantorItemRule" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="GuarantorItemRule"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rblInclude">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tblRuleType" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="100px">
                                    <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" AutoPostBack="true"
                                        OnTextChanged="txtItemID_TextChanged" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="GuarantorItemRule"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rblInclude" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblInclude_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Include</asp:ListItem>
                            <asp:ListItem>Exclude</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rblToGuarantor" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">To Guarantor</asp:ListItem>
                            <asp:ListItem>To Patient</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="GuarantorItemRule"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="GuarantorItemRule" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%" runat="server" id="tblRuleType">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRGuarantorRuleType" runat="server" Text="Rule Type Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboSRGuarantorRuleType" runat="server" Width="200px" />
                                </td>
                                <td>&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsValueInPercent" runat="server" Text="In Percent" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAmountValue" runat="server" Text="Amount Value"></asp:Label>
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="IPR/Default"></asp:Label>
                                </td>
                                <td style="width=1px"></td>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="OPR"></asp:Label>
                                </td>
                                <td style="width=1px"></td>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" Text="EMR"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAmountValue" runat="server" Width="100px" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvAmountValue" runat="server" ErrorMessage="Amount Value (IPR/Default) required."
                                                    ControlToValidate="txtAmountValue" SetFocusOnError="True" ValidationGroup="GuarantorItemRule"
                                                    Width="100%">
                                                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width=1px"></td>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAmountOPR" runat="server" Width="100px" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvAmountValueOPR" runat="server" ErrorMessage="Amount Value (OPR) required."
                                                    ControlToValidate="txtAmountOPR" SetFocusOnError="True" ValidationGroup="GuarantorItemRule"
                                                    Width="100%">
                                                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width=1px"></td>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtAmountEMR" runat="server" Width="100px" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvAmountValueEMR" runat="server" ErrorMessage="Amount Value (EMR) required."
                                                    ControlToValidate="txtAmountEMR" SetFocusOnError="True" ValidationGroup="GuarantorItemRule"
                                                    Width="100%">
                                                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label"></td>
                    <td>
                        <asp:CheckBox ID="chkIsByTariffComponent" runat="server" Text="By Tariff Component"
                            AutoPostBack="true" OnCheckedChanged="chkIsByTariffComponent_CheckedChanged" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
            <table id="tblTariffComponent" runat="server">
                <tr>
                    <td class="label"></td>
                    <td>
                        <telerik:RadGrid ID="grdTariffComponent" runat="server" AutoGenerateColumns="False"
                            GridLines="None">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView DataKeyNames="TariffComponentID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TariffComponentID" UniqueName="TariffComponentID"
                                        SortExpression="TariffComponentID" Visible="false" />
                                    <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component Name"
                                        UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AmountValue" UniqueName="AmountValue" SortExpression="AmountValue"
                                        Visible="false" />
                                    <telerik:GridTemplateColumn HeaderText="IPR/Default" UniqueName="IPR" HeaderStyle-HorizontalAlign="center">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtIPR" Width="100px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "AmountValue")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="OutpatientAmountValue" UniqueName="OutpatientAmountValue"
                                        SortExpression="OutpatientAmountValue" Visible="false" />
                                    <telerik:GridTemplateColumn HeaderText="OPR" UniqueName="OPR" HeaderStyle-HorizontalAlign="center">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtOPR" Width="100px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "OutpatientAmountValue")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="EmergencyAmountValue" UniqueName="EmergencyAmountValue"
                                        SortExpression="EmergencyAmountValue" Visible="false" />
                                    <telerik:GridTemplateColumn HeaderText="EMR" UniqueName="EMR" HeaderStyle-HorizontalAlign="center">
                                        <HeaderStyle Width="120px" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txtEMR" Width="100px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "EmergencyAmountValue")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings EnableRowHoverStyle="True">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
