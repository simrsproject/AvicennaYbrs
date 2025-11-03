<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeVerificationByDischargeDateDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.V2.ParamedicFeeVerificationByDischargeDateDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinFeeCalc() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var over = $find("<%= txtVerificationNo.ClientID %>");
                var opid = $find("<%= cboParamedicID.ClientID %>");
                var osd = $find("<%= txtStartDate.ClientID %>");
                var oed = $find("<%= txtEndDate.ClientID %>");
                var osd1 = osd.get_selectedDate().format("MM/dd/yyyy");
                var oed1 = oed.get_selectedDate().format("MM/dd/yyyy");

                oWnd.setUrl("../ParamedicFeeVerificationByPayment/ParamedicFeeVerificationByPaymentPickList.aspx?ver=" + over.get_value() + "&pid=" + opid._value + "&sd=" + osd1 + "&ed=" + oed1);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinAddDeduc() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var over = $find("<%= txtVerificationNo.ClientID %>");
                var opid = $find("<%= cboParamedicID.ClientID %>");

                oWnd.setUrl("ParamedicFeeVerificationByPaymentAddDeducPickList.aspx?ver=" + over.get_value() + "&pid=" + opid._value);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command != null) {
                    var command = oWnd.argument.command.split('|');
                    if (command[1] == 'calc')
                        __doPostBack("<%= grdFeeCalculation.UniqueID %>", "rebind");
                    else
                        __doPostBack("<%= grdFeeCalculation.UniqueID %>", "rebind");
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="false"
        Modal="true" Title="Physician Fee Outstanding" OnClientClose="onClientClose"
        ID="winPr">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVerificationNo" runat="server" Text="Verification No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtVerificationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPeriode" runat="server" Text="Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="110px" />
                                    </td>
                                    <td>
                                        -&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="110px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="display: none">
                                        <asp:Button ID="btnGetItem" runat="server" Text="Fee Calculation" OnClientClick="javascript:openWinFeeCalc();return false;" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGetAddDeduc" runat="server" Text="Additional/Deduction" OnClientClick="javascript:openWinAddDeduc();return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVerificationDate" runat="server" Text="Verification Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtVerificationDate" runat="server" Width="110px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvVerificationDate" runat="server" ErrorMessage="Verification Date required."
                                ValidationGroup="entry" ControlToValidate="txtVerificationDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVerificationAmount" runat="server" Text="Verification Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtVerificationAmount" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVerificationTaxAmount" runat="server" Text="Verification Tax Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtVerificationTaxAmount" runat="server" Width="100px"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxAmount" runat="server" Text="Tax Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" Width="100px"
                                ReadOnly="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" Visible="false" />
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
            <telerik:RadGrid ID="grdFeeCalculation" runat="server" OnNeedDataSource="grdFeeCalculation_NeedDataSource"
                AutoGenerateColumns="False" ShowFooter="true" GridLines="None"
                OnDeleteCommand="grdFeeCalculation_DeleteCommand" EnableViewState="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo, TariffComponentID, ParamedicID"
                    FilterExpression="VerificationNo IS NOT NULL">
                    <Columns>
                        <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                            <HeaderStyle HorizontalAlign="Center" Width="140px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                            SortExpression="MedicalNo">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PaymentMethodName" HeaderText="Payment Method" UniqueName="PaymentMethodName"
                            SortExpression="PaymentMethodName">
                            <HeaderStyle HorizontalAlign="Left" Width="140px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PriceItem" HeaderText="Price"
                                UniqueName="PriceItem" SortExpression="PriceItem" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Price" HeaderText="Price"
                                UniqueName="Price" SortExpression="Price" DataFormatString="{0:n2}" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DiscountItem" HeaderText="Discount"
                                UniqueName="DiscountItem" SortExpression="DiscountItem" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Discount" HeaderText="Discount"
                                UniqueName="Discount" SortExpression="Discount" DataFormatString="{0:n2}" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CalculatedAmount" HeaderText="Share"
                                UniqueName="CalculatedAmount" SortExpression="CalculatedAmount" DataFormatString="{0:n2}%">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FeeAmount" HeaderText="Gross" UniqueName="FeeAmount"
                            SortExpression="FeeAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                            FooterAggregateFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SumDeductionAmount" HeaderText="Deduction" UniqueName="SumDeductionAmount"
                            SortExpression="SumDeductionAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                            FooterAggregateFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nett" HeaderText="FeeAmount" UniqueName="Nett"
                            SortExpression="Nett" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                            FooterAggregateFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="VerificationNo" HeaderText="VerificationNo" UniqueName="VerificationNo"
                            SortExpression="VerificationNo" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="false" />
                </ClientSettings>
            </telerik:RadGrid>
</asp:Content>
