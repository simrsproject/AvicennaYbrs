<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="CashierCorrectionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashierCorrectionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinPickList() {
                var oWnd = $find("<%= winPL.ClientID %>");

                oWnd.setUrl("PaymentReceivePickList.aspx");
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            
            function onClientClose(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg != null) {
                    if (oWnd.argument.command == 'rebind') {
                        __doPostBack("<%= grdPaymentCorrectionItem.UniqueID %>", "rebind");
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" Title="Payment Pending" OnClientClose="onClientClose" ID="winPL">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCorrectionNo" runat="server" Text="Payment Correction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentCorrectionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Payment Correction Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPaymentCorrectionDate" runat="server" Width="100px" DateInput-ReadOnly="False"
                                DatePopupButton-Enabled="True"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPaymentCorrectionDate" runat="server" ErrorMessage="Payment Correction Date required."
                                ValidationGroup="entry" ControlToValidate="txtPaymentCorrectionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td><asp:Button ID="btnPick" runat="server" Text="Pick List" OnClientClick="javascript:openWinPickList();return false;" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdPaymentCorrectionItem" runat="server" 
        OnNeedDataSource="grdPaymentCorrectionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None"
        OnDeleteCommand="grdPaymentCorrectionItem_DeleteCommand" 
        OnUpdateCommand="grdPaymentCorrectionItem_UpdateCommand"
        ShowFooter="False" AllowPaging="True" PageSize="50">
        <PagerStyle AlwaysVisible="True" Mode="NextPrevNumericAndAdvanced" ></PagerStyle>
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentCorrectionNo, PaymentNo, SequenceNo">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SequenceNo" HeaderText="Seq No"
                    UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />    
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />   
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name"
                    UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />  
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="PaymentMethodOName" HeaderText="Payment Method"
                    UniqueName="PaymentMethodOName" SortExpression="PaymentMethodOName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardProviderOName" HeaderText="Card Provider"
                    UniqueName="CardProviderOName" SortExpression="CardProviderOName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardTypeOName" HeaderText="Card Type"
                    UniqueName="CardTypeOName" SortExpression="CardTypeOName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />   
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="EDCMachineOName" HeaderText="EDC"
                    UniqueName="EDCMachineOName" SortExpression="EDCMachineOName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />   
                    
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardProviderCName" HeaderText="Card Provider Correction"
                    UniqueName="CardProviderCName" SortExpression="CardProviderCName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CardTypeCName" HeaderText="Card Type Correction"
                    UniqueName="CardTypeCName" SortExpression="CardTypeCName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />   
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="EDCMachineCName" HeaderText="EDC Correction"
                    UniqueName="EDCMachineCName" SortExpression="EDCMachineCName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="CashCorrectionDetailEdit.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
