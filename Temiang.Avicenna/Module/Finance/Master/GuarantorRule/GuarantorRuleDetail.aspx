<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="GuarantorRuleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorRuleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkIsByTariffComponent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAmountValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtOutpatientAmountValue" />
                    <telerik:AjaxUpdatedControl ControlID="txtEmergencyAmountValue" />
                    <telerik:AjaxUpdatedControl ControlID="tblTariffComponent" />
                    <telerik:AjaxUpdatedControl ControlID="grdTariffComponent" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTariffComponent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTariffComponent" />
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
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantor" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvGuarantor" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="entry" ControlToValidate="txtGuarantor" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRule" runat="server" Text="Rule"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRule" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvRule" runat="server" ErrorMessage="Rule required."
                                ValidationGroup="entry" ControlToValidate="txtRule" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <asp:Panel runat="server" ID="pnlGroup">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemType" runat="server" Text="Item Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboItemType" runat="server" Width="300px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboItemType_SelectedIndexChanged" />
                            </td>
                            <td style="width: 20px">
                                <asp:RequiredFieldValidator ID="rfvItemType" runat="server" ErrorMessage="Item Type required."
                                    ValidationGroup="entry" ControlToValidate="cboItemType" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboItemGroup" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboItemGroup_SelectedIndexChanged" />
                            </td>
                            <td style="width: 20px">
                                <asp:RequiredFieldValidator ID="rfvItemGroup" runat="server" ErrorMessage="Item Group required."
                                    ValidationGroup="entry" ControlToValidate="cboItemGroup" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlItem">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemName" runat="server" Text="Item"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboItemID_ItemDataBound"
                                    OnItemsRequested="cboItemID_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                        </b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 30 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="width: 20px">
                                <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                                    ValidationGroup="entry" ControlToValidate="cboItemID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="lblRules" runat="server" Text="Guarantor Item Rule"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
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
                </table>
            </td>
            <td style="vertical-align: top" width="100%">
                <table width="100%" id="tblRuleType" runat="server">
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
                            <asp:Label ID="Label1" runat="server" Text="Amount Value"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label5" runat="server" Text="IPR/Default"></asp:Label>
                                    </td>
                                    <td style="width=1px"></td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" Text="OPR"></asp:Label>
                                    </td>
                                    <td style="width=1px"></td>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" Text="EMR"></asp:Label>
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
                                                        ControlToValidate="txtAmountValue" SetFocusOnError="True" ValidationGroup="entry"
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
                                                    <telerik:RadNumericTextBox ID="txtOutpatientAmountValue" runat="server" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfvOutpatientAmountValue" runat="server" ErrorMessage="Amount Value (OPR) required."
                                                        ControlToValidate="txtOutpatientAmountValue" SetFocusOnError="True" ValidationGroup="entry"
                                                        Width="100%">
                                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
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
                                                    <telerik:RadNumericTextBox ID="txtEmergencyAmountValue" runat="server" Width="100px" />
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="rfvEmergencyAmountValue" runat="server" ErrorMessage="Amount Value (EMR) required."
                                                        ControlToValidate="txtEmergencyAmountValue" SetFocusOnError="True" ValidationGroup="entry"
                                                        Width="100%">
                                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
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
</asp:Content>
