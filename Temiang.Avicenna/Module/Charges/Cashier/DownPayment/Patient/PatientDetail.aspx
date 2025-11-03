<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PatientDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.DownPayment.PatientDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Modal="true" ID="winOrderItem" OnClientClose="onClientClose" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openPatientDeposit() {
                var mrn = $find("<%= cboPatient.ClientID %>");
                if (mrn.get_value() == '') return;
                
                var oWnd = $find("<%= winOrderItem.ClientID %>");
                oWnd.setUrl("../PatientDepositDialog.aspx?id=" + mrn.get_value() + "&type=return");
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    var val = oWnd.argument.split('|');
                    if (val[0] == 'deposit') {
                        __doPostBack("<%= grdTransPaymentItem.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblNumber" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblDateTime" />
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtPaymentDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPaymentTime" runat="server" Width="50px" MaxLength="5"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Patient Name
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboPatient" Width="300px" EnableLoadOnDemand="true"
                                OnItemsRequested="cboPatient_ItemsRequested" OnItemDataBound="cboPatient_ItemDataBound" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPatient" runat="server" ErrorMessage="Patient Name required."
                                ValidationGroup="entry" ControlToValidate="cboPatient" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Deposit Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPatientDepositType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdTransPaymentItem" runat="server" OnNeedDataSource="grdTransPaymentItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransPaymentItem_UpdateCommand"
        OnDeleteCommand="grdTransPaymentItem_DeleteCommand" OnInsertCommand="grdTransPaymentItem_InsertCommand"
        ShowFooter="true">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo, SequenceNo">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdTransPaymentItem.MasterTableView.IsItemInserted && ProgramID == Temiang.Avicenna.Common.AppConstant.Program.PatientDepositReceive %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record" />
                </asp:LinkButton>
                <asp:LinkButton ID="lblDeposit" runat="server" Visible='<%# !grdTransPaymentItem.MasterTableView.IsItemInserted && ProgramID == Temiang.Avicenna.Common.AppConstant.Program.PatientDepositReturn %>'
                    OnClientClick="openPatientDeposit(); return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/views16.png" />
                    &nbsp;<asp:Label runat="server" ID="Label1" Text="Load from patient deposit" />
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="PaymentTypeName" HeaderText="Payment Type" UniqueName="PaymentTypeName"
                    SortExpression="PaymentTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PaymentMethodName" HeaderText="Payment Method"
                    UniqueName="PaymentMethodName" SortExpression="PaymentMethodName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="right" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="../ItemDownPayment.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemDownPaymentEditCommand" />
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
