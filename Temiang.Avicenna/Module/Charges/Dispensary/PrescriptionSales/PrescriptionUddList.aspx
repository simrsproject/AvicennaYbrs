<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionUddList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionUddList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoEditUrl(pno, regno) {
                var url = 'PrescriptionSalesDetail.aspx?md=edit&pno=' + pno + '&regno=' + regno + "&type=udd&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                window.location.href = url;
            }

            function gotoViewUrl(pno, regno, unit, loc) {
                var url = 'PrescriptionSalesDetail.aspx?md=view&pno=' + pno + '&regno=' + regno + "&type=udd&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=&unit=" + unit + "&loc=" + loc;
                window.location.href = url;
            }

            function gotoAddUrl(regno, unit, loc, parid) {
                var url = "PrescriptionSalesDetail.aspx?md=new&regno=" + regno + "&type=udd&unit=" + unit + "&loc=" + loc + "&parid=" + parid +"&rt=<%= Request.QueryString["rt"] %>&ono=";
                window.location.href = url;
            }

            function viewHistory(patientID) {
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('PrescriptionHistoryDialog.aspx?pid=' + patientID + "&rt=" + '<%= Request.QueryString["rt"] %>');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openRpt() {
                var oWnd = $find('<%=winPrint.ClientID%>');
                oWnd.SetUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
                return;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
        ID="winDocsOption">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionSRFloor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDispensary">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDeliveryStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterKioskQueueNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdUddItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdUddItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBarcode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="txtBarcode" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDispensary" runat="server" Text="Dispensary*"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboDispensaryID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterPrescriptionDispensary" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="UDD List" PageViewID="pgOrder" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Prescription History" PageViewID="pgList">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOrder" runat="server" Selected="true">
            <%--            <asp:Panel ID="pnlRegDate" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="110px" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>--%>
            <telerik:RadGrid ID="grdUddItem" runat="server" OnNeedDataSource="grdUddItem_NeedDataSource" OnItemCommand="grdUddItem_ItemCommand"
                AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="RegistrationNo, IsCheckinConfirmed" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
<%--                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>--%>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsCheckinConfirmed").Equals(false) ? string.Empty : 
                                string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "DispSuId"),DataBinder.Eval(Container.DataItem, "DispLocId"),DataBinder.Eval(Container.DataItem, "ParamedicID"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>&nbsp;
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDebtAvailable")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Debt\" title=\"Debt\" />" : string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="200px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                                <%# DataBinder.Eval(Container.DataItem, "IsNotNew").Equals(false)? string.Format("<img src=\"../../../../Images/Toolbar/new_tag_16.png\"/>"):String.Empty%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="Sex" HeaderText="Gender"
                            UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="150px"/>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="150px"/>
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="UDD by Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="150px"/>
                        <telerik:GridDateTimeColumn DataField="UddStartDateTime" HeaderText="UDD Start" UniqueName="UddStartDateTime"
                            SortExpression="UddStartDateTime">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="150px"/>
                        <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                            HeaderStyle-Width="80px" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
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
        <telerik:RadPageView ID="pgList" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptioDate" runat="server" Text="Prescription Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="110px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterPrescriptionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <%--                            <tr runat="server" id="trPrescriptionSRFloor">
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionSRFloor" runat="server" Text="Floor"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboPrescriptionSRFloor" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionSRFloor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionNo" runat="server" Text="Prescription No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr runat="server" id="trKioskQueueNo">
                                <td class="label">
                                    <asp:Label ID="lblKioskQueueNo" runat="server" Text="Queue No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtKioskQueueNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterKioskQueueNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <%--                            <tr runat="server" id="trPrescriptionStatus">
                                <td class="label">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboStatus" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>--%>
                            <tr runat="server" id="tr1">
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Delivery Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboDeliveryStatus" Width="300px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="" Text="" />
                                            <telerik:RadComboBoxItem Value="1" Text="Oustanding" />
                                            <telerik:RadComboBoxItem Value="2" Text="Complete" />
                                            <telerik:RadComboBoxItem Value="3" Text="Delivered" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterDeliveryStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionStatus" runat="server" Text="Prescription Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList runat="server" ID="rbPrescriptionStatus" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Outstanding" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Void" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="Barcode"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rbBarcodeMode" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Search" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Set Complete" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Set Delivered" Value="4"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <telerik:RadTextBox ID="txtBarcode" runat="server" Width="150px" MaxLength="50" OnTextChanged="txtBarcode_TextChanged" AutoPostBack="true">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdPrescription" runat="server" OnNeedDataSource="grdPrescription_NeedDataSource"
                OnDetailTableDataBind="grdPrescription_DetailTableDataBind" AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15" OnItemCommand="grdPrescription_ItemCommand">
                <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" HeaderText="Dispensary "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "IsCheckinConfirmed").Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "DispSuId"), DataBinder.Eval(Container.DataItem, "DispLocId"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="NeedValidationByCasemix" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsGuarantorBpjsCasemix").Equals(false) || DataBinder.Eval(Container.DataItem, "IsApproval").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) ? string.Empty
                                                        : (DataBinder.Eval(Container.DataItem, "IsNeedValidationByCasemix").Equals(false) ? string.Format("<img src=\"../../../../Images/Toolbar/post_green_16.png\" border=\"0\" title=\"\" /></a>") :
                                        string.Format("<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" title=\"Some item(s) need validation by Casemix\" /></a>")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PrescriptionDate"
                            HeaderText="Presc. Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PrescriptionNo" HeaderText="Prescription No"
                            UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="OrderNo" HeaderText="Order No"
                            UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="200px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" HeaderStyle-Width="150px"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Notes" UniqueName="Note" SortExpression="Note"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Proceed"
                            UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridBoundColumn DataField="ApprovalDateTime" HeaderText="Approval Date"
                            UniqueName="ApprovalDateTime" SortExpression="ApprovalDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproval" HeaderText="Approved"
                            UniqueName="IsApproval" SortExpression="IsApproval" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPaid" HeaderText="Paid"
                            UniqueName="IsPaid" SortExpression="IsPaid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsProceedByPharmacist"
                            HeaderText="Proceed" UniqueName="IsProceedByPharmacist" SortExpression="IsProceedByPharmacist"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPrinted" HeaderText="Printed"
                            UniqueName="IsPrinted" SortExpression="IsPrinted" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="KioskQueueNo" HeaderText="Queue No" HeaderStyle-Width="70px"
                            UniqueName="KioskQueueNo" SortExpression="KioskQueueNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbStatus" runat="server" CommandArgument='<%#string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "PrescriptionNo"),((int)DataBinder.Eval(Container.DataItem, "Status")) == 3 ? "" : (((int)DataBinder.Eval(Container.DataItem, "Status")) == 2 ? "deliver":"complete"))%>'
                                    CommandName="setStatus" ToolTip='<%#string.Format("Status: {0}", ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "Delivered" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "Complete" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "In Progress" : "Outstanding")))) %>'
                                    OnClientClick='<%#string.Format("{0}", (int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "alert(\"Prescription has been delivered\"); return false;":((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "return confirm(\"Are you sure want to set as delivered?\");" : "return confirm(\"Are you sure want to set as complete?\");")) %>'>
                                    <%#string.Format("<img style=\"border: 0px; text-align:center; vertical-align: middle;\" src=\"../../../../Images/Toolbar/{0} \" />",
                                        ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "post16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "post_green_16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "post_yellow_16.png" : "post16_d.png"))))%>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <a href="#" onclick="viewHistory('<%# DataBinder.Eval(Container.DataItem, "PatientID") %>'); return false;">
                                    <img src="../../../../Images/Toolbar/details16.png" border="0" title="Prescription List" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Profile" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblEdit" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%>'
                                    CommandName="Profile" ToolTip="Profile">
                                    <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../Images/Toolbar/new16.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="Detail" DataKeyNames="SequenceNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                                    UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Header" UniqueName="ParentNo"
                                    HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "PrescriptionQty") %><br />
                                        (<%# DataBinder.Eval(Container.DataItem, "ResultQty") %>)
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Dosing"
                                    UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                                    UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method"
                                    UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe"
                                    UniqueName="RecipeAmount" SortExpression="RecipeAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LineAmount" HeaderText="Total"
                                    UniqueName="LineAmount" SortExpression="LineAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
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
</asp:Content>
