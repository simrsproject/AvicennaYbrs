<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="GuarantorRuleList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorRuleList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinEdit(itemId) {
                var guarId = $find("<%= cboGuarantorID.ClientID %>");
                var ruleId = $find("<%= cboRuleId.ClientID %>");
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("GuarantorRuleDetail.aspx?itemId=" + itemId + "&guarId=" + guarId.get_value() + "&type=edit" + "&ruleId=" + ruleId.get_value());
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinAdd(itemId) {
                var guarId = $find("<%= cboGuarantorID.ClientID %>");
                var ruleId = $find("<%= cboRuleId.ClientID %>");
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("GuarantorRuleDetail.aspx?itemId=" + itemId + "&guarId=" + guarId.get_value() + "&type=add" + "&ruleId=" + ruleId.get_value());
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinImport() {
                var guarId = $find("<%= cboGuarantorID.ClientID %>");
                var ruleId = $find("<%= cboRuleId.ClientID %>");
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("GuarantorRuleDetail.aspx?itemId=&guarId=" + guarId.get_value() + "&type=import" + "&ruleId=" + ruleId.get_value());
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            var checkedItems = [];

            function updateCheckedItems() {
                checkedItems = [];
                $("input[type=checkbox][id$='deleteChkBox']:checked").each(function () {
                    checkedItems.push($(this).closest("tr").attr("data-itemid"));
                });
            }

            $(document).ready(function () {
                // Event ketika "Select All" di klik
                $("#chkSelectAll").change(function () {
                    var isChecked = $(this).prop("checked");
                    $("input[type=checkbox][id$='deleteChkBox']").prop("checked", isChecked);
                    updateCheckedItems();
                });

                // Event ketika checkbox individual di klik
                $("input[type=checkbox][id$='deleteChkBox']").change(function () {
                    updateCheckedItems();
                });

                // Event ketika tombol Delete di klik
                $("#lbDelete").click(function () {
                    rowDeleteAll();
                });
            });

            function rowDeleteAll() {
                updateCheckedItems(); // Pastikan checkedItems selalu update

                if (checkedItems.length === 0) {
                    alert("Please select at least one item to delete.");
                    return false;
                }

                if (confirm('Are you sure to delete rule for selected items?')) {
                    // Postback dengan data checkedItems yang diperbarui
                    __doPostBack("<%= grdList.UniqueID %>", 'delete|' + checkedItems.join(','));
                }
            }            

            <%--function updateHeaderCheckbox() {
                var grid = document.getElementById('<%= grdList.ClientID %>');
                var headerCheckbox = grid.querySelector("input[type='checkbox'][id$='chkSelectAll']");
                var checkboxes = grid.querySelectorAll("input[type='checkbox'][id$='deleteChkBox']");
                var allChecked = true;
               
                checkboxes.forEach(function (checkbox) {
                    if (!checkbox.checked) {
                        allChecked = false;
                    }
                });
                
                headerCheckbox.checked = allChecked;
            }--%>

            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }           
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winProcess" Animation="None" Width="1000px" Height="600px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboGuarantorID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboRuleId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSRItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItemID" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterItemID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboItemID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                    OnItemsRequested="cboGuarantorID_ItemsRequested" OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged"
                                    AutoPostBack="True">
                                    <FooterTemplate>
                                        Note : Show max 30 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterGuarantorID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRule" runat="server" Text="Rule"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboRuleId" runat="server" Width="300px" OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRule" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterSRItemType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboItemID_ItemDataBound"
                                    OnItemsRequested="cboItemID_ItemsRequested" OnSelectedIndexChanged="cboGuarantorID_SelectedIndexChanged"
                                    AutoPostBack="True">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                        </b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 30 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterItemID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="50"
        OnDetailTableDataBind="grdGuarantorItemRule_DetailTableDataBind" OnItemDataBound="grdList_ItemDataBound"
        OnPageIndexChanged="grdList_PageIndexChanged" OnPageSizeChanged="grdList_PageSizeChanged">
        <MasterTableView DataKeyNames="ItemID" ClientDataKeyNames="ItemID" GroupLoadMode="client"
            CommandItemDisplay="Top">
            <CommandItemTemplate>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lbNew" runat="server" OnClientClick="javascript:openWinAdd('');return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblNew" Text="New"></asp:Label></asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lbImport" runat="server" OnClientClick="javascript:openWinImport();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/transactions16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblImport" Text="Import"></asp:Label></asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lbDelete" runat="server" OnClientClick="javascript:rowDeleteAll();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/delete16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblDelete" Text="Delete"></asp:Label></asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="" Visible="False">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"openWinAdd('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ItemID"))) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"openWinEdit('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ItemID"))) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="checkbox" HeaderText="">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="deleteChkBox" runat="server" AutoPostBack="true" OnCheckedChanged="deleteChkBox_CheckedChanged" Checked="false" ></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="IsInclude" HeaderText="Include" UniqueName="IsInclude"
                    SortExpression="IsInclude">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsToGuarantor" HeaderText="To Guarantor" UniqueName="IsToGuarantor"
                    SortExpression="IsToGuarantor">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsValueInPercent" HeaderText="In Percent"
                    UniqueName="IsValueInPercent" SortExpression="IsValueInPercent">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="GuarantorRuleTypeName" HeaderText="Rule Type Name"
                    UniqueName="GuarantorRuleTypeName" SortExpression="GuarantorRuleTypeName">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AmountValue" HeaderText="Amount Value (IPR/Default)"
                    UniqueName="AmountValue" SortExpression="AmountValue" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OutpatientAmountValue" HeaderText="Amount Value (OPR)"
                    UniqueName="OutpatientAmountValue" SortExpression="OutpatientAmountValue" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EmergencyAmountValue" HeaderText="Amount Value (EMR)"
                    UniqueName="EmergencyAmountValue" SortExpression="EmergencyAmountValue" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="IsByTariffComponent" HeaderText="By Tariff Component"
                    UniqueName="IsByTariffComponent" SortExpression="IsByTariffComponent" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
               <%-- <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"rowDelete('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/delete16.png\" border=\"0\" alt=\"Delete\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ItemID"))) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>--%>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="GuarantorID,ItemID,TariffComponentID"
                    AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component Name"
                            UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="AmountValue" UniqueName="AmountValue" SortExpression="AmountValue"
                            HeaderText="IPR/Default" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="OutpatientAmountValue" UniqueName="OutpatientAmountValue"
                            HeaderText="OPR" SortExpression="OutpatientAmountValue" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="EmergencyAmountValue" UniqueName="EmergencyAmountValue"
                            HeaderText="EMR" SortExpression="EmergencyAmountValue" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <ExpandCollapseColumn Visible="True" />
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
