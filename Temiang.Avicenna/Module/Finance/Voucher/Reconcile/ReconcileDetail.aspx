<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="ReconcileDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.Reconcile.ReconcileDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <style type="text/css">
            @keyframes blinkingText {
                0% {
                    opacity: 1;
                }

                40% {
                    opacity: 0;
                }

                60% {
                    opacity: 0;
                }

                100% {
                    opacity: 1;
                }

                100% {
                    opacity: 1;
                }

                100% {
                    opacity: 1;
                }
            }

            .blinking {
                animation: blinkingText 1.4s infinite;
            }
        </style>
        <script language="javascript" type="text/javascript">

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                var tabStrip;

                switch (val) {
                    case "list":
                        location.replace('ReconcileList.aspx');
                        break;
                    case "reconsile":
                        if (confirm('Are you sure want to Reconciled / Canceled Reconciliation this registration?'))
                            __doPostBack("<%= grdVoucherEntryItem.UniqueID %>", 'reconsile');
                        break;

                }
            }
            function setCustomPosition(sender, args) {
                sender.moveTo(sender.get_left(), sender.get_top());
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdVoucherEntryItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rblJournal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rblJournal" />
                    <telerik:AjaxUpdatedControl ControlID="cboChartOfAccountId" />
                    <telerik:AjaxUpdatedControl ControlID="grdVoucherEntryItem" />
                    <telerik:AjaxUpdatedControl ControlID="txtDebit" />
                    <telerik:AjaxUpdatedControl ControlID="txtCredit" />
                    <telerik:AjaxUpdatedControl ControlID="txtDifferent" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSortBy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVoucherEntryItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboChartOfAccountId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdVoucherEntryItem" />
                    <telerik:AjaxUpdatedControl ControlID="txtDebit" />
                    <telerik:AjaxUpdatedControl ControlID="txtCredit" />
                    <telerik:AjaxUpdatedControl ControlID="txtDifferent" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Reconsile" Value="reconsile" ImageUrl="~/Images/Toolbar/post_green_16.png"
                HoveredImageUrl="~/Images/Toolbar/post_green_16.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Patient Information">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="True" />
                                        </td>
                                        <td>
                                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                                    Text=""></asp:Label>&nbsp; </a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" MaxLength="15"
                                                ReadOnly="True" />
                                        </td>
                                        <td>
                                            <a href="javascript:void(0);" onclick="javascript:openWinQuestionFormCheckList();"
                                                class="noti2_Container">
                                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo2" AssociatedControlID="txtRegistrationNo"
                                                    Text=""></asp:Label>&nbsp; </a>
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
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtSalutation" runat="server" Width="25px" ReadOnly="true" />
                                        </td>
                                        <td style="width: 3px"></td>
                                        <td>
                                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="266px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSex" runat="server" Text="Gender"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButton ID="optSexMale" runat="server" Text="Male" GroupName="Sex" />
                                <asp:RadioButton ID="optSexFemale" runat="server" Text="Female" GroupName="Sex" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                    ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" Width="50px" ReadOnly="True">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;Y&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" Width="50px" ReadOnly="True">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;M&nbsp;
                                <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" Width="50px" ReadOnly="True">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                &nbsp;D
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top;">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" MaxLength="10"
                                    ReadOnly="True" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" MaxLength="10"
                                    ReadOnly="True" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChargeClass" runat="server" Text="Charge Class"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtChargeClassName" runat="server" Width="300px" ReadOnly="True" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="True" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 103px">
                                            <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                DatePopupButton-Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <telerik:RadMaskedTextBox ID="txtRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                                PromptChar="_" RoundNumericRanges="false" Width="50px">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <asp:Panel runat="server" ID="pnlForInpatient">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDischargeDate" runat="server" Text="Discharge Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 103px">
                                                <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                                    DatePopupButton-Enabled="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtDischargeTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLengthOfStay" runat="server" Text="Length Of Stay"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtLengthOfStay" runat="server" Width="40px" ReadOnly="True">
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                                &nbsp;Day(s)
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </asp:Panel>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <table style="width: 100%" cellpadding="0" cellspacing="5">
        <tr>
            <td>
                <fieldset>
                    <legend></legend>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblJournal" runat="server" Text="Journal"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rblJournal" runat="server" RepeatDirection="Horizontal"
                                                OnTextChanged="rblJournal_OnTextChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="true">Reconcile</asp:ListItem>
                                                <asp:ListItem>All</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblChartOfAccountId" runat="server" Text="COA"></asp:Label>
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
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSortBy" runat="server" Text="Sort By"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSortBy" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cboSortBy_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDebit" runat="server" Text="Debit"/>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtDebit" runat="server" ReadOnly="true">
                                                <NumberFormat DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCredit" runat="server" Text="Credit"/>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtCredit" runat="server" ReadOnly="true">
                                                <NumberFormat DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDifferent" runat="server" Text="Different"/>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtDifferent" runat="server" ReadOnly="true">
                                                <NumberFormat DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 20px;"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdVoucherEntryItem" runat="server" AllowPaging="false" AllowCustomPaging="true"
        ShowFooter="True" OnNeedDataSource="grdVoucherEntryItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="Horizontal">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="DetailId" ShowGroupFooter="True">
            <Columns>
                <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="100px" DataField="ChartOfAccountCode"
                    HeaderText="Code" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" />
                <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="300px" DataField="ChartOfAccountName"
                    HeaderText="Account Name" UniqueName="ChartOfAccountName" />
                <telerik:GridBoundColumn DataField="SubLedgerId" HeaderText="Subledger ID" UniqueName="SubLedgerId"
                    Visible="false" />
                <telerik:GridBoundColumn ItemStyle-Wrap="True" HeaderStyle-Width="150px" DataField="SubLedger_Description"
                    HeaderText="Subledger" UniqueName="SubLedger_Description" />
                <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="150px" DataField="DocumentNumber"
                    HeaderText="Reference#" UniqueName="DocumentNumber" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Count" FooterAggregateFormatString="Total :" />
                <telerik:GridBoundColumn DataField="Debit" HeaderText="Debit" UniqueName="Debit"
                    DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right">
                    <HeaderStyle HorizontalAlign="Center" Width="115px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Credit" HeaderText="Credit" UniqueName="Credit"
                    DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right">
                    <HeaderStyle HorizontalAlign="Center" Width="115px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                    ItemStyle-Wrap="True">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
