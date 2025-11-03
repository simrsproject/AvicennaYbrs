<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="BkuJournalList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.BkuJournalList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
            function OpenDetail(bkuid) {
                //alert(coaid);
                var oWnd = $find("<%= winDialog.ClientID %>");
                
                oWnd.setUrl("BkuJournalDetailDialog.aspx?bkuid=" + bkuid);

                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPelanggan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterKasBank">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="lp1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" />
    <cc:collapsepanel id="CollapsePanel1" runat="server" title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Tanggal
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;</td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="label">
                                Pelanggan
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboPelanggan" Width="304px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterPelanggan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Kas/Bank
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboKasBank" Width="304px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterKasBank" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:collapsepanel>


    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="BkuJournalId">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ChartOfAccountCode" HeaderText="Account Code"
                            UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                <telerik:GridHyperLinkColumn HeaderStyle-Width="200px" DataTextField="ChartOfAccountName" 
                    HeaderText="Account Name" UniqueName="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"
                    DataNavigateUrlFields="BkuJournalId"
                    DataNavigateUrlFormatString="javascript:OpenDetail('{0}');"
                    />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RefferenceNumber" HeaderText="Transaction No"
                    UniqueName="RefferenceNumber" SortExpression="RefferenceNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Transaction Date" UniqueName="TransactionDate"
                    SortExpression="TransactionDate">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description"
                            UniqueName="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Debit" HeaderText="Debit"
                            UniqueName="Debit" SortExpression="Debit" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Credit" HeaderText="Kredit"
                            UniqueName="Credit" SortExpression="Credit" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
