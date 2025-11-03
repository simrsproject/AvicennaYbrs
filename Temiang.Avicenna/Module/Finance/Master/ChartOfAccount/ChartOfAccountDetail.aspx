<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ChartOfAccountDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ChartOfAccountDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">Chart Of Account Code</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtChartOfAccountCode" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td style="width: 20px;">
                <asp:RequiredFieldValidator ID="rfvChartOfAccountCode" runat="server" ErrorMessage="Chart Of Account Code Required"
                    ValidationGroup="entry" ControlToValidate="txtChartOfAccountCode"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Chart Of Account Name</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAccountName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td style="width: 20px;">
                <asp:RequiredFieldValidator ID="rfvAccountName" runat="server" ErrorMessage="Account Name required."
                    ValidationGroup="entry" ControlToValidate="txtAccountName"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsDetail" runat="server" Text="Detail Account" />
                        </td>
                        <td width="10px"></td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsControlDocNumber" runat="server" Text="Control Document Number" />
                        </td>
                        <td width="10px"></td>
                        <td>
                            <asp:CheckBox ID="chkIsReconcile" runat="server" Text="Reconcile" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Account Level</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRAcctLevel" runat="server" Width="300px" AutoPostBack="true" />
            </td>
            <td style="width: 20px;">
                <asp:RequiredFieldValidator ID="rfvSRAcctLevel" runat="server" ErrorMessage="Account Level required."
                    ValidationGroup="entry" ControlToValidate="cboSRAcctLevel"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Normal Balance</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboNormalBalance" runat="server" Width="300px" />
            </td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Account Group</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboAccountGroup" runat="server" Width="300px" />
            </td>
            <td style="width: 20px;">
                <asp:RequiredFieldValidator ID="rfvSRAcctGroup" runat="server" ErrorMessage="Account Group required."
                    ValidationGroup="entry" ControlToValidate="cboAccountGroup"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">General Account</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAccountGroup" runat="server" Width="100px" MaxLength="15" AutoPostBack="true" OnTextChanged="txtAccountGroup_TextChanged" />
                &nbsp;<asp:Label ID="lblAcctSubGroupName" runat="server" />
            </td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Subledger</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSubLedger" runat="server" Width="300px" />
            </td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Note</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" MaxLength="255" />
            </td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>
        <asp:Panel runat="server" ID="pnlBku">
            <tr>
                <td class="label"></td>
                <td class="entry">
                    <asp:CheckBox ID="chkBkuAccount" runat="server" Text="BKU Account" />
                </td>
                <td style="width: 20px;"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">BKU Account Code
                </td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboBkuAccount" runat="server" Width="300px" EnableLoadOnDemand="true"
                        HighlightTemplatedItems="true" OnItemDataBound="cboBkuAccount_ItemDataBound"
                        OnItemsRequested="cboBkuAccount_ItemsRequested">
                        <FooterTemplate>
                            Note : Show max 20 items                                  
                        </FooterTemplate>
                    </telerik:RadComboBox>
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
        </asp:Panel>
    </table>
</asp:Content>

