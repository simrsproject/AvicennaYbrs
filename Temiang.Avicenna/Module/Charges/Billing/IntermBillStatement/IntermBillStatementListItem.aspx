<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="IntermBillStatementListItem.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Billing.IntermBillStatementListItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">

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
                        location.replace('IntermBillStatementList.aspx?type=<%= Page.Request.QueryString["type"] %>');
                        break;
                    case "print":
                        __doPostBack("<%= grdItem.UniqueID %>", 'print');
                        break;
                    case "printr":
                        __doPostBack("<%= grdItem.UniqueID %>", 'printr');
                        break;
                    case "printd":
                        __doPostBack("<%= grdItem.UniqueID %>", 'printd');
                        break;
                    case "refresh":
                        __doPostBack("<%= grdItem.UniqueID %>", 'rebind');
                        break;
                }
            }

            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="rblToGuarantor" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rblToGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="rblToGuarantor" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Print Billing Statement" Value="print"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print Prescription" Value="printr"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print Deposit Statement" Value="printd"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winProcess" Animation="None" Width="1000px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="True" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
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
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="25px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;Y&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;M&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                            <NumberFormat AllowRounding="False" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;D
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room / Bed No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRoomName" runat="server" Width="179px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;&nbsp;/&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChargeClass" runat="server" Text="Charge / Coverage Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtClassName" runat="server" Width="140px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;&nbsp;/&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtCoverageClassName" runat="server" Width="139px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtParamedicName" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBillTo" runat="server" Text="Bill Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rblToGuarantor" runat="server" RepeatDirection="Horizontal"
                                OnTextChanged="rblToGuarantor_OnTextChanged" AutoPostBack="true">
                                <asp:ListItem Selected="true">To Guarantor</asp:ListItem>
                                <asp:ListItem>To Patient</asp:ListItem>
                            </asp:RadioButtonList>
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
        <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
            <HeaderContextMenu>
            </HeaderContextMenu>
            <MasterTableView DataKeyNames="IntermBillNo">
                <Columns>
                    <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="IntermBillTemplateColumn" HeaderText="Print">
                        <ItemTemplate>
                            <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsUnpaid") %>'
                                Enabled='<%#DataBinder.Eval(Container.DataItem, "IsUnpaid") %>'></asp:CheckBox>
                        </ItemTemplate>
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="PaymentTemplateColumn" HeaderText="Paid">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="paymentChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPaid") %>'
                                Enabled="false"></asp:CheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridNumericColumn DataField="IntermBillNo" HeaderText="Interm Bill No" UniqueName="IntermBillNo"
                        SortExpression="IntermBillNo">
                    </telerik:GridNumericColumn>
                    <telerik:GridBoundColumn DataField="IntermBillDate" HeaderText="Date" UniqueName="IntermBillDate"
                        SortExpression="IntermBillDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="105px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" UniqueName="StartDate"
                        SortExpression="StartDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="105px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" UniqueName="EndDate"
                        SortExpression="EndDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="105px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn DataField="PatientAmount" HeaderText="Patient Amount"
                        UniqueName="PatientAmount" SortExpression="PatientAmount" DataFormatString="{0:n2}"
                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn DataField="GuarantorAmount" HeaderText="Guarantor Amount"
                        UniqueName="GuarantorAmount" SortExpression="GuarantorAmount" DataFormatString="{0:n2}"
                        FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridNumericColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
