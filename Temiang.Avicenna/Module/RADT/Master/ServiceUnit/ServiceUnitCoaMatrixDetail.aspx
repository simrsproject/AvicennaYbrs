<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitCoaMatrixDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitCoaMatrixDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboChartOfAccountIdIncome">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSubledgerIdIncome" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboChartOfAccountIdDiscount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSubledgerIdDiscount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnLoadList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listLeft" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="listRight" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnLeftToRight">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listLeft" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="listRight" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRightToLeft">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listLeft" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="listRight" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript" language="jscript">
            function CheckAll(leftright) {
                var list = leftright == 'left' ? $find("<%= listLeft.ClientID %>") : $find("<%= listRight.ClientID %>");
                list.get_items().forEach(
                    function(item) {
                        item.set_selected(item.get_selected() == false ? true : false);
                        item.set_checked(item.get_checked() == false ? true : false);
                    }
                );
            }

            function ClearCheckBoxes() {
                document.getElementById('ctl00_ContentPlaceHolder1_chkLeft').checked = false;
                document.getElementById('ctl00_ContentPlaceHolder1_chkRight').checked = false;
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblServiceUnit" Text="Service Unit" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtServiceUnitID" Width="100px" ReadOnly="True" />
                            <asp:Label runat="server" ID="lblServiceUnitName" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblClass" Text="Class" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboClass" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvClass" runat="server" ErrorMessage="Class required."
                                ValidationGroup="entry" ControlToValidate="cboClass" SetFocusOnError="True" Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblTariffComponent" Text="Tariff Component" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboTariffComponent" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTariffComponent" runat="server" ErrorMessage="Tariff Component required."
                                ValidationGroup="entry" ControlToValidate="cboTariffComponent" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblCOAType" Text="COA Type" />
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList runat="server" ID="rblCOAType" RepeatLayout="Flow " RepeatDirection="Horizontal">
                                <asp:ListItem Text="Income" Value="income" Selected="True" />
                                <asp:ListItem Text="Discount" Value="discount" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:Button runat="server" ID="btnLoadList" Text="Load" OnClick="btnLoadList_Click"
                                OnClientClick="ClearCheckBoxes()" ValidationGroup="entry" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblCOA" Text="COA" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIncome" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdIncome_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdIncome_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdIncome_ItemsRequested">
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
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCOAIncome" runat="server" ErrorMessage="COA required."
                                ValidationGroup="left" ControlToValidate="cboChartOfAccountIdIncome" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblSubledger" Text="Subledger" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdIncome" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdIncome_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdIncome_ItemsRequested">
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
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSubledgerIncome" runat="server" ErrorMessage="Subledger required."
                                ValidationGroup="left" ControlToValidate="cboSubledgerIdIncome" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="Label1" Text="Transaction Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px"/>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="Label2" runat="server" Text="Matrix Setting"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="45%">
                &nbsp;&nbsp;<input id="chkLeft" runat="server" type="checkbox" onclick="CheckAll('left')">Check
                    All</input>
                <telerik:RadListBox runat="server" ID="listLeft" CheckBoxes="True" SelectionMode="Multiple"
                    Width="100%" Height="390px" />
            </td>
            <td width="5%" valign="middle" align="center">
                <asp:Button runat="server" ID="btnLeftToRight" Text=">>" OnClick="btnLeftToRight_Click"
                    OnClientClick="ClearCheckBoxes()" ValidationGroup="left" />
                <br />
                <asp:Button runat="server" ID="btnRightToLeft" Text="<<" OnClick="btnRightToLeft_Click"
                    OnClientClick="ClearCheckBoxes()" />
            </td>
            <td width="45%">
                &nbsp;&nbsp;<input id="chkRight" runat="server" type="checkbox" onclick="CheckAll('right')">Check
                    All</input>
                <telerik:RadListBox runat="server" ID="listRight" CheckBoxes="True" SelectionMode="Multiple"
                    Width="100%" Height="390px" />
            </td>
        </tr>
    </table>
</asp:Content>
