<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DownPaymentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.DownPaymentDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        VisibleStatusbar="False" Modal="true" ID="winOrderItem" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script type="text/javascript">
            var i = 0;
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openPatientDeposit() {
                var oWnd = $find("<%= winOrderItem.ClientID %>");
                var mrn = $find("<%= txtMedicalNo.ClientID %>");
                oWnd.setUrl("PatientDepositDialog.aspx?id=" + mrn.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinTemplatePickList() {
                i++;

                var oWnd = $find("<%= winTemplatePickList.ClientID %>");

                oWnd.setUrl("VisitPackegeTemplatePickList.aspx?wid=" + i);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClosePicklist(oWnd) {
                //Jika apply di click
                var arg = oWnd.argument;
                if (arg) {
                    $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>").ajaxRequest("");
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="605px" Behavior="Close, Move, Maximize"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        Title="Template List" ReloadOnShow="true" OnClientClose="onClientClosePicklist"
        ID="winTemplatePickList">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentNo" runat="server" Text="Payment No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvPaymentNo" runat="server" ErrorMessage="Payment No required."
                                ValidationGroup="entry" ControlToValidate="txtPaymentNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date / Time"></asp:Label>
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
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblServiceUnitName"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvServiceUnit" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="txtServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="270px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trInitial">
                        <td class="label">
                            <asp:Label ID="lblInitial" runat="server" Text="Initial"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInitial" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPrintReceiptAsName" runat="server" Text="Receipt As Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPrintReceiptAsName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label runat="server" ID="lblGuarantorName"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr height="24px" runat="server" id="trVisiteDownPayment">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsVisiteDownPayment" runat="server" Text="Visite Package" AutoPostBack="true"
                                OnCheckedChanged="chkIsVisiteDownPayment_CheckedChanged" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrderAmount" runat="server" Text="Transaction Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtOrderAmount" runat="server" Width="100px" ReadOnly="True" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnOrderItem" runat="server" Text="Transaction List" OnClientClick="openWinOrderItem();return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Down Payment" PageViewID="pgDownPayment" Selected="True" />
            <telerik:RadTab runat="server" Text="Visite Item" PageViewID="pgVisiteItem" Enabled="false" />
            <telerik:RadTab runat="server" Text="Transaction Item" PageViewID="pgOrderItem" Enabled="true" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDownPayment" runat="server" Selected="true">
            <telerik:RadGrid ID="grdTransPaymentItem" runat="server" OnNeedDataSource="grdTransPaymentItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdTransPaymentItem_UpdateCommand"
                OnDeleteCommand="grdTransPaymentItem_DeleteCommand" OnInsertCommand="grdTransPaymentItem_InsertCommand"
                ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo, SequenceNo">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdTransPaymentItem.MasterTableView.IsItemInserted %>'>
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record" />
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lblDeposit" runat="server" Visible="False"
                            OnClientClick="openPatientDeposit(); return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                            &nbsp;<asp:Label runat="server" ID="Label1" Text="Load from patient deposit" Visible="False" />
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
                            UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="CardFeeAmount" HeaderText="Card Fee Amount"
                            UniqueName="CardFeeAmount" SortExpression="CardFeeAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="10px"/>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemDownPayment.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemDownPaymentEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgVisiteItem" runat="server">
            <telerik:RadGrid ID="grdVisiteItem" runat="server" OnNeedDataSource="grdVisiteItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdVisiteItem_UpdateCommand"
                OnDeleteCommand="grdVisiteItem_DeleteCommand" OnInsertCommand="grdVisiteItem_InsertCommand"
                ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PaymentNo, PatientID, ItemID">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdVisiteItem.MasterTableView.IsItemInserted %>'>
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                        </asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="lbTemplatePickList" runat="server" Visible='<%# !grdVisiteItem.MasterTableView.IsItemInserted %>'
                            OnClientClick="javascript:openWinTemplatePickList();return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />&nbsp;<asp:Label
                                runat="server" ID="Label2" Text="Pick from Package Template"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                            SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Width="100px" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VisiteQty" HeaderText="Visite Qty"
                            UniqueName="VisiteQty" SortExpression="VisiteQty" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterAggregateFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Discount"
                            UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterAggregateFormatString="{0:n2}" Visible="false" />
                        <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                            DataType="System.Double" DataFields="VisiteQty,Price,Discount" SortExpression="Total"
                            Expression="({0} * {1}) - ({2}/100 * ({0} * {1}))" FooterStyle-HorizontalAlign="Right"
                            Aggregate="Sum" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemDownPaymentVisite.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemDownPaymentVisiteEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgOrderItem" runat="server">
            <telerik:RadGrid ID="grdOrderItem" runat="server" OnNeedDataSource="grdOrderItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
                    GroupLoadMode="Client">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbClearItem" runat="server" OnClientClick="javascript:OnClientClickClearList();return false;">
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblClearItem" Text="Clear List Item"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                            Aggregate="Count" FooterAggregateFormatString="Total :" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                            DataType="System.Double" DataFields="Qty,Price" SortExpression="Total" Expression="{0} * {1}"
                            FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">

            function openWinOrderItem() {
                var oWnd = $find("<%= winOrderItem.ClientID %>");
                var oreg = $find("<%= txtRegistrationNo.ClientID %>");
                var opy = $find("<%= txtPaymentNo.ClientID %>");
                oWnd.setUrl("OrderItemList.aspx?regno=" + oreg.get_value() + "&pyno=" + opy.get_value());
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument) {
                    var val = oWnd.argument.split('|');
                    if (val[0] == 'rebind') {
                        __doPostBack("<%= grdOrderItem.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                    else if (val[0] == 'deposit') {
                        __doPostBack("<%= grdTransPaymentItem.UniqueID %>", oWnd.argument);
                        oWnd.argument = 'undefined';
                    }
                }
            }

            function OnClientClickClearList() {
                __doPostBack("<%= grdOrderItem.UniqueID %>", 'clearlist');
            }
        </script>

    </telerik:RadCodeBlock>
</asp:Content>
