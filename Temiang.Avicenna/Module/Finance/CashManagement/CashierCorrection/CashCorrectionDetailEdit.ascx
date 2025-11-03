<%@ Control AutoEventWireup="true" CodeBehind="CashCorrectionDetailEdit.ascx.cs" 
Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashCorrectionDetailEdit"
    Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ErrorMessage="" ID="customValidator" OnServerValidate="customValidator_ServerValidate"
    runat="server" ValidationGroup="MemorialItem">&nbsp;</asp:CustomValidator>
<asp:Label ID="lblDetailId" runat="server" Visible="false"></asp:Label>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRCardProvider" runat="server" Text="Card Provider"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCardProvider" runat="server" Width="300px" AutoPostBack="True"
                            OnSelectedIndexChanged="cboSRCardProvider_SelectedIndexChanged"
                            AllowCustomText="true" Filter="Contains" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                        <asp:HiddenField ID="hfPaymentNo" runat="server" />
                        <asp:HiddenField ID="hfSequenceNo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRCardType" runat="server" Text="Card Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCardType" runat="server" Width="300px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboEDCMachineID_SelectedIndexChanged" 
                            AllowCustomText="true" Filter="Contains" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEDCMachineID" runat="server" Text="EDC Machine"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboEDCMachineID" runat="server" Width="300px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboEDCMachineID_SelectedIndexChanged" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCardFeeAmount" runat="server" Text="Card Fee Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtCardFeeAmount" runat="server" Width="100px" ReadOnly="true" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="MemorialItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MemorialItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                        </asp:Button>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
            </table>  
        </td>
    </tr>
</table>
