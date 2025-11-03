<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="RlReport4_1.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.v2025.RlReport4_1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onProcess() {
                __doPostBack("<%= grdRlReport4_1.UniqueID %>", "process");
            }
            
            function onPrint() {
                __doPostBack("<%= grdRlReport4_1.UniqueID %>", "print");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxLoadingPanel ID="ajxLoadingPanel" runat="server" Transparency="30">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px; margin-top: 75px;" />
    </telerik:RadAjaxLoadingPanel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblRlMasterReportID" runat="server" Text="RL Master Report"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRlMasterReportID" runat="server" Width="20px" Visible="False" />
                <telerik:RadTextBox ID="txtRlMasterReportNo" runat="server" Width="100px" ReadOnly="True" />
                <telerik:RadTextBox ID="txtRlMasterReportName" runat="server" Width="193px" ReadOnly="True" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvRlMasterReportID" runat="server" ErrorMessage="RL Master Report ID required."
                    ValidationGroup="entry" ControlToValidate="txtRlMasterReportID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <asp:Panel runat="server" ID="pnlPrint">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbPrint" runat="server" OnClientClick="javascript:onPrint();return false;">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/print16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblPrint" Text="Print Report" Font-Bold="True"></asp:Label>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblRlReportNo" runat="server" Text="Report No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRlTxReportNo" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvRlReportNo" runat="server" ErrorMessage="Report No required."
                    ValidationGroup="entry" ControlToValidate="txtRlTxReportNo" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPeriodMonthStart" runat="server" Width="104px" />
                &nbsp;to&nbsp;
                <telerik:RadComboBox ID="cboPeriodMonthEnd" runat="server" Width="104px" />
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" MaxLength="4" />
            </td>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year required."
                    ValidationGroup="entry" ControlToValidate="txtPeriodYear" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="ajxPanel" runat="server" Width="100%" LoadingPanelID="ajxLoadingPanel">
        <telerik:RadGrid ID="grdRlReport4_1" runat="server" OnNeedDataSource="grdRlReport4_1_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" AllowPaging="False">
            <MasterTableView CommandItemDisplay="None" DataKeyNames="RlMasterReportItemID">
                <CommandItemTemplate>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbPickList" runat="server" OnClientClick="javascript:onProcess();return false;">
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/process16.png" />
                        &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Process Data"></asp:Label>
                    </asp:LinkButton>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>
                    <telerik:GridBoundColumn DataField="RlMasterReportItemID" HeaderText="ID" UniqueName="RlMasterReportItemID"
                        SortExpression="RlMasterReportItemID" Visible="False">
                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RlMasterReportItemCode" HeaderText="Kode ICD"
                        UniqueName="RlMasterReportItemCode" SortExpression="RlMasterReportItemCode">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RlMasterReportItemName" HeaderText="Diagnosis Penyakit"
                        UniqueName="RlMasterReportItemName" SortExpression="RlMasterReportItemName">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="< 1 Jam Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL1j" runat="server" Width="35px" DbValue='<%#Eval("L1j")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="< 1 jam Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP1j" runat="server" Width="35px" DbValue='<%#Eval("P1j")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="1-23 jam Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL123j" runat="server" Width="35px" DbValue='<%#Eval("L123j")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="1-23 jam Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP123j" runat="server" Width="35px" DbValue='<%#Eval("P123j")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="1-7 hari Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL0107h" runat="server" Width="35px" DbValue='<%#Eval("L0107h")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="1-7 hari Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP0107h" runat="server" Width="35px" DbValue='<%#Eval("P0107h")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="8-28 hari Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL0828h" runat="server" Width="35px" DbValue='<%#Eval("L0828h")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="8-28 hari Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP0828h" runat="server" Width="35px" DbValue='<%#Eval("P0828h")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="29 hari - <3 bulan Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL29h03b" runat="server" Width="35px" DbValue='<%#Eval("L29h03b")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="29 hari - <3 bulan Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP29h03b" runat="server" Width="35px" DbValue='<%#Eval("P29h03b")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="3 - <6 bulan Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL0306b" runat="server" Width="35px" DbValue='<%#Eval("L0306b")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="3 - <6 bulan Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP0306b" runat="server" Width="35px" DbValue='<%#Eval("P0306b")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="6-11 bulan Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL0611b" runat="server" Width="35px" DbValue='<%#Eval("L0611b")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="6-11 bulan Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP0611b" runat="server" Width="35px" DbValue='<%#Eval("P0611b")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="1-4 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL0104t" runat="server" Width="35px" DbValue='<%#Eval("L0104t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="1-4 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP0104t" runat="server" Width="35px" DbValue='<%#Eval("P0104t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="5-9 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL0509t" runat="server" Width="35px" DbValue='<%#Eval("L0509t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="5-9 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP0509t" runat="server" Width="35px" DbValue='<%#Eval("P0509t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="10-14 tahun Lk"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL1014t" runat="server" Width="35px" DbValue='<%#Eval("L1014t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="10-14 tahun Pr" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP1014t" runat="server" Width="35px" DbValue='<%#Eval("P1014t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="15-19 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL1519t" runat="server" Width="35px" DbValue='<%#Eval("L1519t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="15-19 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP1519t" runat="server" Width="35px" DbValue='<%#Eval("P1519t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="20-24 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL2024t" runat="server" Width="35px" DbValue='<%#Eval("L2024t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="20-24 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP2024t" runat="server" Width="35px" DbValue='<%#Eval("P2024t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="25-29 tahun Lk" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL2529t" runat="server" Width="35px" DbValue='<%#Eval("L2529t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="25-29 tahuun Pr" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP2529t" runat="server" Width="35px" DbValue='<%#Eval("P2529t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="30-34 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL3034t" runat="server" Width="35px" DbValue='<%#Eval("L3034t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="30-34 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP3034t" runat="server" Width="35px" DbValue='<%#Eval("P3034t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="35-39 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL3539t" runat="server" Width="35px" DbValue='<%#Eval("L3539t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="35-39 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP3539t" runat="server" Width="35px" DbValue='<%#Eval("P3539t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="40-44 tahun Lk" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL4044t" runat="server" Width="35px" DbValue='<%#Eval("L4044t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="40-44 tahun Pr" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP4044t" runat="server" Width="35px" DbValue='<%#Eval("P4044t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="45-49 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL4549t" runat="server" Width="35px" DbValue='<%#Eval("L4549t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="45-49 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP4549t" runat="server" Width="35px" DbValue='<%#Eval("P4549t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="50-54 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL5054t" runat="server" Width="35px" DbValue='<%#Eval("L5054t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="50-54 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP5054t" runat="server" Width="35px" DbValue='<%#Eval("P5054t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="55-59 tahun Lk" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL5559t" runat="server" Width="35px" DbValue='<%#Eval("L5559t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="55-59 tahuun Pr" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP5559t" runat="server" Width="35px" DbValue='<%#Eval("P5559t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="60-64 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL6064t" runat="server" Width="35px" DbValue='<%#Eval("L6064t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="60-64 tahun Pr" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP6064t" runat="server" Width="35px" DbValue='<%#Eval("P6064t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="65-69 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL6569t" runat="server" Width="35px" DbValue='<%#Eval("L6569t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="65-69 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP6569t" runat="server" Width="35px" DbValue='<%#Eval("P6569t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="70-74 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL7074t" runat="server" Width="35px" DbValue='<%#Eval("L7074t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="70-74 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP7074t" runat="server" Width="35px" DbValue='<%#Eval("P7074t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="75-79 tahun Lk" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL7579t" runat="server" Width="35px" DbValue='<%#Eval("L7579t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="75-79 tahun Pr" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP7579t" runat="server" Width="35px" DbValue='<%#Eval("P7579t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="80-84 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL8084t" runat="server" Width="35px" DbValue='<%#Eval("L8084t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="80-84 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP8084t" runat="server" Width="35px" DbValue='<%#Eval("P8084t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText=">85 tahun Lk" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtL85t" runat="server" Width="35px" DbValue='<%#Eval("L85t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText=">85 tahun Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtP85t" runat="server" Width="35px" DbValue='<%#Eval("P85t")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Jumlah Pasien Keluar Hidup dan Mati Lk" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtTotalPasienHidupL" runat="server" Width="35px" DbValue='<%#Eval("TotalPasienHidupL")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Jumlah Pasien Keluar Hidup dan Mati Pr" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtTotalPasienHidupP" runat="server" Width="35px" DbValue='<%#Eval("TotalPasienHidupP")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Total Pasien Keluar Hidup dan Mati" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtTotalPasienHidup" runat="server" Width="35px" DbValue='<%#Eval("TotalPasienHidup")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Jumlah Pasien Keluar Mati Lk" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtTotalPasienMatiL" runat="server" Width="35px" DbValue='<%#Eval("TotalPasienMatiL")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Jumlah Pasien Keluar Mati Pr" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtTotalPasienMatiP" runat="server" Width="35px" DbValue='<%#Eval("TotalPasienMatiP")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Total Pasien Keluar Mati" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtTotalPasienMati" runat="server" Width="35px" DbValue='<%#Eval("TotalPasienMati")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="True">
                <Resizing AllowColumnResize="True" />
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="True" FrozenColumnsCount="2" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
