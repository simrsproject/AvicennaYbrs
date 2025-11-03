<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="IntermBillDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Billing.IntermBillDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinGetItem() {
                var oWnd = $find("<%= winPr.ClientID %>");
                var oibno = $find("<%= txtIntermBillNo.ClientID %>");
                var orno = $find("<%= txtRegistrationNo.ClientID %>");
                var osd = $find("<%= txtStartDate.ClientID %>");
                var oed = $find("<%= txtEndDate.ClientID %>");
                var osd1 = osd.get_selectedDate().format("MM/dd/yyyy");
                var oed1 = oed.get_selectedDate().format("MM/dd/yyyy");
                var presc = document.getElementById('ctl00_ContentPlaceHolder1_chkIsIncludePrescription').checked;
                var osu = $find("<%= cboFilterByServiceUnitID.ClientID %>");
                var oit = $find("<%= cboFilterByItemType.ClientID %>");

                oWnd.setUrl("IntermBillPickList.aspx?ibno=" + oibno.get_value() + "&rno=" + orno.get_value() + "&sd=" + osd1 + "&ed=" + oed1 + "&type=" + '<%= Request.QueryString["type"] %>' + "&presc=" + presc + "&it=" + oit._value + "&su=" + osu._value);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command != null) {
                    __doPostBack("<%= grdItem.UniqueID %>", "rebind");
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
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move, Maximize" ShowContentDuringLoad="False" VisibleStatusbar="false"
        Modal="true" Title="Transaction Item List Outstanding" OnClientClose="onClientClose"
        ID="winPr">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIntermBillNo" runat="server" Text="Interm Bill No / Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtIntermBillNo" runat="server" Width="120px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;/&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtIntermBillDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvIntermBillDate" runat="server" ErrorMessage="Interm Bill  Date required."
                                ValidationGroup="entry" ControlToValidate="txtIntermBillDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
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
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        -&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsIncludePrescription" runat="server" Text="Include Prescription"
                                            Checked="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Start Date required."
                                ValidationGroup="entry" ControlToValidate="txtStartDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvEndData" runat="server" ErrorMessage="End Date required."
                                ValidationGroup="entry" ControlToValidate="txtStartDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFilterByServiceUnit" runat="server" Text="Filter By Unit"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboFilterByServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFilterByItemType" runat="server" Text="Filter By Item Type"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboFilterByItemType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <table width="100%">
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Button ID="btnGetItem" runat="server" Text="Get Item List" OnClientClick="javascript:openWinGetItem();return false;" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                                    </td>
                                    <td>
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
                        <td>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientAmount" runat="server" Text="Patient Amount"></asp:Label>
                                    </td>
                                    <td class="label">
                                        <asp:Label ID="lblGuarantorAmount" runat="server" Text="Guarantor Amount"></asp:Label>
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
                            <asp:Label ID="lblAmount" runat="server" Text="Total Interm Bill Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0">
                                <tr>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtPatientAmount" runat="server" Width="150px" ReadOnly="True" />
                                    </td>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtGuarantorAmount" runat="server" Width="150px" ReadOnly="True" />
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
                            <asp:Label ID="lblAdm" runat="server" Text="Administration Fee Amount"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0">
                                <tr>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtPatientAdmAmount" runat="server" Width="150px"
                                            ReadOnly="True" />
                                    </td>
                                    <td class="entry">
                                        <telerik:RadNumericTextBox ID="txtGuarantorAdmAmount" runat="server" Width="150px"
                                            ReadOnly="True" />
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
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
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
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="true" />
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomBed" runat="server" Text="Room / Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRoomName" runat="server" Width="179px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 10px">&nbsp;&nbsp;/&nbsp;&nbsp;</td>
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
                            <asp:Label ID="lblClass" runat="server" Text="Charge / Covered Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtClassName" runat="server" Width="140px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 10px">&nbsp;&nbsp;/&nbsp;&nbsp;</td>
                                    <td>
                                        <telerik:RadTextBox ID="txtCoverageClassName" runat="server" Width="139px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtParamedicName" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" Visible="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" ShowFooter="true" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
            FilterExpression="IntermBillNo IS NOT NULL">
            <Columns>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID" HeaderStyle-Width="90px">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PatientAmount" HeaderText="Patient Amount" UniqueName="PatientAmount"
                    SortExpression="PatientAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                    FooterAggregateFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GuarantorAmount" HeaderText="Guarantor Amount"
                    UniqueName="GuarantorAmount" SortExpression="GuarantorAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DiscountAmount" HeaderText="Discount Amount"
                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IntermBillNo" HeaderText="IntermBillNo" UniqueName="IntermBillNo"
                    SortExpression="IntermBillNo" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
