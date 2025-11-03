<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="VerificationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.VerificationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function UpdateStatusVerification(transNo, seqNo, unitID, locationNo) {
                var param = transNo + "|" + seqNo + "|" + unitID + "|" + locationNo;
                __doPostBack("<%= grdItem.UniqueID %>", param);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    if (oWnd.argument == 'rebind') {
                        __doPostBack("<%= grdItem.UniqueID %>", 'rebind');
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                

                switch (val) {
                    case "list":
                        location.replace('VerificationList.aspx');
                        break;
                    case "process":
                        if (confirm('Are you sure want to process this transaction?'))
                            __doPostBack("<%= grdItem.UniqueID %>", 'process');
                        break;
                    case "save":
                        if (confirm('Are you sure want to lock billing and transaction process?'))
                            __doPostBack("<%= grdItem.UniqueID %>", 'save');
                        break;
                    case "print":
                       

                        __doPostBack("<%= grdItem.UniqueID %>", 'print');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Process" Value="process" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <asp:Panel ID="pnlInformation" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
                BorderColor="#FFC080" BorderStyle="Solid">
        <table width="90%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50px">
                    <asp:Image ID="Image1" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td align="left" valign="middle">
                    <asp:Label ID="lblInformation" runat="server" Text="This Data Has Processed"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close, Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="onClientClose">
        <Windows>
            <telerik:RadWindow ID="winProcess" Width="1000px" Height="500px" runat="server">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrint" Width="1000px" Height="600px" runat="server" InitialBehavior="Maximize"
                Behavior="Maximize,Close">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtInvoiceDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ErrorMessage="Invoice Date required."
                                ValidationGroup="entry" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorID" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "GuarantorID")%>) </b>
                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                                ValidationGroup="entry" ControlToValidate="cboGuarantorID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRReceivableType" runat="server" Text="Receivable Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRReceivableType" Width="300px" Enabled="false" >
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            
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
                            <asp:Label ID="lblInvoiceDueDate" runat="server" Text="Invoice Due Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtInvoiceDueDate" runat="server" Width="100px" Enabled="false" />
                        </td>
                        <td width="20px">
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTermOfPayment" runat="server" Text="Term Of Payment"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTermOfPayment" runat="server" Width="100px" MaxLength="10"
                                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="0" Enabled="false" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Note"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" TextMode="MultiLine" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRReceivableStatus" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRReceivableStatus" Width="300px" Enabled="false" >
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVerifyDate" runat="server" Text="Verify Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtVerifyDate" runat="server" Width="100px" Enabled="false" />
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
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
        <HeaderContextMenu>
            
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="PaymentNo">
            
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PaymentNo" HeaderText="Payment No"
                    UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="PaymentDate" HeaderText="Payment Date"
                    UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />   
                <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID"
                    UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />  
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name"
                    UniqueName="PatientName" SortExpression="PatientName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />  
                <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Verify Amount" HeaderStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtVerifyAmount" runat="server" Width="150px" DbValue='<%#Eval("VerifyAmountProcess")%>'
                            NumberFormat-DecimalDigits="0" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
            
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    
</asp:Content>

