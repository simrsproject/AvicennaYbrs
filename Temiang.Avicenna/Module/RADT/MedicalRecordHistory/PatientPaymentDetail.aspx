<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientPaymentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientPaymentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRegistrationNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPaymentNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatientPaymentNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Payment" Selected="True" PageViewID="pgPayment">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Patient Deposit" PageViewID="pgPatientDeposit">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgPayment" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnSearchRegistrationNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" />
                                </td>
                                <td />
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPaymentNo" runat="server" Text="Payment No" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPaymentNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnSearchPaymentNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" />
                                </td>
                                <td />
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                            OnDetailTableDataBind="grdList_DetailTableDataBind" AutoGenerateColumns="false"
                            OnItemCommand="grdList_ItemCommand">
                            <MasterTableView DataKeyNames="PaymentNo">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="RegistrationNo" HeaderText="Registration No ">
                                            </telerik:GridGroupByField>
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="RegDateRegNo" SortOrder="Descending"></telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PatientID" HeaderText="Patient ID"
                                        UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" Visible="False" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                                        UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" Visible="False" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="PaymentNo" HeaderText="Payment No"
                                        UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="PaymentDate" HeaderText="Date"
                                        UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="PaymentTime" HeaderText="Time"
                                        UniqueName="PaymentTime" SortExpression="PaymentTime" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ChargeToGuarantor" HeaderText="Charge To" UniqueName="ChargeToGuarantor"
                                        SortExpression="ChargeToGuarantor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="150px" />
                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Payment"
                                        UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                        SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CreatedBy" HeaderText="Created By"
                                        UniqueName="CreatedBy" SortExpression="CreatedBy" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                                        HeaderText="Last Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsApproved" HeaderText="Approved"
                                        UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid" HeaderText="Void"
                                        UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsPrinted" HeaderText="Print"
                                        UniqueName="IsPrinted" SortExpression="IsPrinted" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridTemplateColumn UniqueName="Print" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>'
                                                ToolTip='Print Billing Statement' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                        <img src="../../../Images/Toolbar/print16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="Print" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPrintGuarantorReceipt" runat="server" CommandName="PrintGuarantorReceipt"
                                                Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' 
                                                ToolTip='<%# (DataBinder.Eval(Container.DataItem, "ChargeToGuarantorID").ToString() == "SELF") ? "Print Payment Receipt":"Print Guarantor Receipt" %>'
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                        <img src="../../../Images/Toolbar/print16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="PrintD" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPrintGuarantorBillingStatement" runat="server" CommandName="PrintGuarantorBillingStatement"
                                                Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' 
                                                ToolTip='<%# (DataBinder.Eval(Container.DataItem, "ChargeToGuarantorID").ToString() == "SELF") ? "Print Billing Statement":"Print Guarantor Billing Statement" %>'
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                        <img src="../../../Images/Toolbar/print16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="PrintGuarantorOnlyBillingStatementNoDiscNoDP"
                                        HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPrintGuarantorOnlyBillingStatementNoDiscNoDP" runat="server"
                                                CommandName="PrintGuarantorOnlyBillingStatementNoDiscNoDP" Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>'
                                                ToolTip='Print Guarantor Billing Statement (Guarantor Only Without Discount and Down Payment)'
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                        <img src="../../../Images/Toolbar/print16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="PrintGuarantorOnlyBillingStatement" HeaderStyle-Width="35px"
                                        ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPrintGuarantorOnlyBillingStatement" runat="server" CommandName="PrintGuarantorOnlyBillingStatement"
                                                Visible='<%# DataBinder.Eval(Container.DataItem, "IsApproved") %>' ToolTip='Print Guarantor Billing Statement (Guarantor Only)'
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentNo") %>'>
                                        <img src="../../../Images/Toolbar/print16.png" border="0" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="PaymentNo, SequenceNo" Name="grdDetail" Width="100%"
                                        AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PaymentTypeName" HeaderText="Payment Type"
                                                UniqueName="PaymentTypeName" SortExpression="PaymentTypeName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PaymentMethodName"
                                                HeaderText="Payment Method" UniqueName="PaymentMethodName" SortExpression="PaymentMethodName"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CardTypeName" HeaderText="Card Type"
                                                UniqueName="CardTypeName" SortExpression="CardTypeName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CardProviderName" HeaderText="Card Provider"
                                                UniqueName="CardProviderName" SortExpression="CardProviderName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="EDCMachineName" HeaderText="EDC Machine" UniqueName="EDCMachineName"
                                                SortExpression="EDCMachineName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="DiscountReasonName" HeaderText="Discount Reason"
                                                UniqueName="DiscountReasonName" SortExpression="DiscountReasonName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Payment"
                                                UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Return"
                                                UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="InvoiceNo" Name="grdDetail2" Width="100%" AutoGenerateColumns="false"
                                        AllowPaging="true" PageSize="10">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="InvoiceNo" HeaderText="AR Payment No"
                                                UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PaymentNo" HeaderText="Payment No"
                                                UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PaymentDate" HeaderText="Payment Date"
                                                UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PaymentAmount" HeaderText="AR Payment"
                                                UniqueName="PaymentAmount" SortExpression="PaymentAmount" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowExpandCollapse="true">
                            </ClientSettings>
                        </telerik:RadGrid>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
                            EnableEmbeddedScripts="false">
                        </telerik:RadAjaxPanel>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPatientDeposit" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPatientPaymentNo" runat="server" Text="Payment No" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPatientPaymentNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnSearchPatientPaymentNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter2_Click" />
                                </td>
                                <td />
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%">
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdList2" runat="server" OnNeedDataSource="grdList2_NeedDataSource" AutoGenerateColumns="false">
                                <MasterTableView DataKeyNames="PaymentNo">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate" HeaderText="Payment Date"
                                            UniqueName="PaymentDate" SortExpression="PaymentDate" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                                            UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                                            HeaderText="Last Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsApproved" HeaderText="Approved"
                                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid" HeaderText="Void"
                                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
                                EnableEmbeddedScripts="false">
                            </telerik:RadAjaxPanel>
                        </td>
                    </tr>
                </table>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
