<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandardReferenceItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.ControlPanel.Setting.StandardReferenceItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAppStandardReferenceItem" runat="server" ValidationGroup="AppStandardReferenceItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AppStandardReferenceItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                            ControlToValidate="txtItemID" SetFocusOnError="True" ValidationGroup="AppStandardReferenceItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Item Name required."
                            ControlToValidate="txtItemName" SetFocusOnError="True" ValidationGroup="AppStandardReferenceItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label6" runat="server" Text="Numeric Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtNumericValue" runat="server" Width="300px" ReadOnly="true">
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvNumericValue" runat="server" ErrorMessage="Numeric Value required."
                            ControlToValidate="txtNumericValue" SetFocusOnError="True" ValidationGroup="AppStandardReferenceItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label3" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNote" runat="server" Width="301px" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="trReferenceID">
                    <td class="label">
                        <asp:Label ID="lblReferenceID" runat="server" Text="Reference ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReferenceID" runat="server" Width="300px" MaxLength="20">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="trBackColor">
                    <td class="label">
                        <asp:Label ID="lblBackColor" runat="server" Text="Back Color"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadColorPicker ShowIcon="true" ID="txtBackColor" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLineNumber" runat="server" Text="Line Number"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtLineNumber" runat="server" Width="300px" NumberFormat-DecimalDigits="0">
                        </telerik:RadNumericTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td>
                        <asp:CheckBox ID="chkIsUsedBySystem" Text="Used By System" runat="server" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AppStandardReferenceItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="AppStandardReferenceItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <asp:Panel ID="pnlCOA" runat="server">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Chart Of Account">
                            </asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboCOA" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboCOA_SelectedIndexChanged"
                                OnItemDataBound="cboCOA_ItemDataBound" OnItemsRequested="cboCOA_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Subledger">
                            </asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledger" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboSubledger_ItemDataBound" OnItemsRequested="cboSubledger_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%">
               <tr>
                    <td class="label">
                        <asp:Label ID="Label7" runat="server" Text="Custom Field 1"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCustomField" runat="server" Width="301px" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">

                    </td>
                    <td></td>
                </tr>
               <tr>
                    <td class="label">
                        <asp:Label ID="Label8" runat="server" Text="Custom Field 2"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCustomField2" runat="server" Width="301px" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px">

                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
