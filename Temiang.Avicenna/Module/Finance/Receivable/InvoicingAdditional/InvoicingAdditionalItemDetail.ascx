<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoicingAdditionalItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Receivable.InvoicingAdditionalItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumInvoiceSupplierItem" runat="server" ValidationGroup="InvoicesItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="InvoicesItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblPaymentNo" runat="server" Text="Seq No"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="3"
                Enabled="false" Text="d" />
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
        </td>
        <td class="entry">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                            ReadOnly="true" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                    </b>&nbsp;-&nbsp;
                                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                    <br />
                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                    &nbsp;|&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                    <br />
                    Address :
                                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20">
            <asp:RequiredFieldValidator ID="rfvTxtPatientName" runat="server" ErrorMessage="Patient Name required."
                ValidationGroup="entry" ControlToValidate="txtPatientName" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvCboPatientID" runat="server" ErrorMessage="Patient Name required."
                ValidationGroup="entry" ControlToValidate="cboPatientID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="150px" NumberFormat-DecimalDigits="2" AutoPostBack="True"
                OnTextChanged="txtAmount_TextChanged" />
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td class="entry">
            <table width="100%">
                <tr>
                    <td style="width: 21%">
                        <asp:CheckBox ID="chkIsPpn" runat="server" Text="PPn" AutoPostBack="True" OnCheckedChanged="chkIsPpn_CheckedChanged" />
                    </td>
                    <td style="width: 50%">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>PPh&nbsp;</td>
                                <td>
                                    <asp:RadioButtonList ID="rbPph" runat="server" OnSelectedIndexChanged="rbPph_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="+" Value="+" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>&nbsp;
                                    <telerik:RadComboBox runat="server" ID="cboSRPph" Width="150px" AllowCustomText="true"
                                        Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRPph_OnSelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                        <asp:CheckBox ID="chkIsPph" runat="server" Text="PPh" Visible="False" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="Label7" runat="server" Text="Percentage (%)"></asp:Label>
        </td>
        <td class="entry">
            <table width="100%">
                <tr>
                    <td style="width: 50%">
                        <telerik:RadNumericTextBox ID="txtPpnPercentage" runat="server" Width="150px" MaxLength="10"
                            MaxValue="99.99" MinValue="0" NumberFormat-DecimalDigits="2" Type="Percent" ReadOnly="True" />
                    </td>
                    <td style="width: 50%">
                        <telerik:RadNumericTextBox ID="txtPphPercentage" runat="server" Width="150px" MaxLength="10"
                            MaxValue="99.99" MinValue="0" NumberFormat-DecimalDigits="2" Type="Percent" ReadOnly="True" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="Label6" runat="server" Text="Amount"></asp:Label>
        </td>
        <td class="entry">
            <table width="100%">
                <tr>
                    <td style="width: 50%">
                        <telerik:RadNumericTextBox ID="txtPpnAmount" runat="server" Width="150px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                    </td>
                    <td style="width: 50%">
                        <telerik:RadNumericTextBox ID="txtPphAmount" runat="server" Width="150px" MaxLength="10"
                            NumberFormat-DecimalDigits="2" ReadOnly="True" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px"></td>
        <td></td>
    </tr>
    <tr>
        <td class="label">COA (A/R Invoice)
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Height="190px" Width="300px"
                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                OnItemsRequested="cboChartOfAccountId_ItemsRequested">
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
        <td style="width: 20px;" />
        <td />
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="Label2" runat="server" Text="Subledger"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSubledgerId" Height="190px" Width="300px"
                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                OnItemDataBound="cboSubledgerId_ItemDataBound" OnItemsRequested="cboSubledgerId_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                        &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
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
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="InvoicesItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="InvoicesItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
