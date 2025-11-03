<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="IncentiveProcessDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Remuneration.IncentiveProcessDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function OnCalculation() {
                __doPostBack("<%= grdItem.UniqueID %>", "calculation");
            }
            function viewEmployeeList(iId, gId) {
                var oWnd = $find("<%= winEmployeeList.ClientID %>");
                oWnd.setUrl('IncentiveProcessDetailItemList.aspx?iId=' + iId + "&gId=" + gId);

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winEmployeeList">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblEmployeeIncentiveProcessID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtEmployeeIncentiveProcessID" runat="server" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lbPayrollPeriod" runat="server" Text="Payroll Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                OnItemsRequested="cboPayrollPeriodID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 12 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPayrollPeriodID" runat="server" ErrorMessage="Payroll Period required."
                                ValidationGroup="entry" ControlToValidate="cboPayrollPeriodID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
        <HeaderContextMenu>
            <CollapseAnimation Duration="200" Type="OutQuint" />
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="SRIncentiveServiceUnitGroup" GroupLoadMode="Client">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbCalculation" runat="server" OnClientClick="javascript:OnCalculation();return false;">
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/process16.png" />
                        &nbsp;<asp:Label runat="server" ID="lblCalculation" Text="Calculation"></asp:Label>
                    </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>

                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeIncentiveProcessID" UniqueName="EmployeeIncentiveProcessID"
                    SortExpression="EmployeeIncentiveProcessID" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRIncentiveServiceUnitGroup" HeaderText="SRIncentiveServiceUnitGroup" UniqueName="SRIncentiveServiceUnitGroup"
                    SortExpression="SRIncentiveServiceUnitGroup" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncentiveServiceUnitGroupName" HeaderText="Incentive Service Unit Group" UniqueName="IncentiveServiceUnitGroupName"
                    SortExpression="IncentiveServiceUnitGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Incentive Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtNominal" runat="server" Width="100px" DbValue='<%#Eval("Nominal")%>'
                            Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="TotalPoint" HeaderText="Total Points" UniqueName="TotalPoint"
                    SortExpression="TotalPoint" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="NominalPerPoint" HeaderText="Amount / Points" UniqueName="NominalPerPoint"
                    SortExpression="NominalPerPoint" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn HeaderStyle-Width="20px"/>
                <telerik:GridTemplateColumn UniqueName="EmpList" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <a href="#" onclick="viewEmployeeList('<%# DataBinder.Eval(Container.DataItem, "EmployeeIncentiveProcessID") %>', '<%# DataBinder.Eval(Container.DataItem, "SRIncentiveServiceUnitGroup") %>'); return false;">
                            <img src="../../../../Images/Toolbar/details16.png" border="0" title="Employee List" /></a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
