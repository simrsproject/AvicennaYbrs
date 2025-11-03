<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RemunDetailNonBPJS.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.RemunDetailNonBPJS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
        
            function ExportToExcel() {
                var rno = $find("<%= txtRemunNo.ClientID %>").get_value();
                //alert(rno);

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("ExportToExcelDialog.aspx?rno=" + rno + "&type=NonBPJS");
                oWnd.show();

                return false;
            }

            function onClientClose(oWnd, args) {
               <%-- if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                     oWnd.argument = 'undefined';
                 }--%>
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />


    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width:40%;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Remun No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRemunNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Month"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboMonth" runat="server" Width="300px"></telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Month required."
                                ValidationGroup="entry" ControlToValidate="cboMonth" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                     <tr>
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtYear" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Year required."
                                ValidationGroup="entry" ControlToValidate="txtYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        Discharge Date
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="rdpDateFrom" runat="server" Width="150px"></telerik:RadDatePicker>
                                                </td>
                                                <td>to</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <telerik:RadDatePicker ID="rdpDateTo" runat="server" Width="150px"></telerik:RadDatePicker>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnCalculateBudget" runat="server" Text="Calculate" OnClick="btnCalculateBudget_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width:60%;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    
                     <tr>
                        <td colspan="4">
                            <fieldset>
                                <legend>Total</legend>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td >Total Paramedic</td>
                                        <td style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtTotalMedic" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="padding-left:10px;"></td>
                                        <td >Total Staff</td>
                                        <td style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtTotalStaff" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >Total Unit</td>
                                        <td style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtTotalUnit" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="padding-left:10px;"></td>
                                        <td >Total Director</td>
                                        <td style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtTotalDirector" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td >Total Equality</td>
                                        <td style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtTotalEquality" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="padding-left:10px;"></td>
                                        <td ></td>
                                        <td style="text-align:right">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td ><hr /></td>
                                        <td style="text-align:right">
                                            <hr />
                                        </td>
                                        <td ><hr /></td>
                                        <td ><hr /></td>
                                        <td style="text-align:right">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td ><asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClientClick="ExportToExcel();return false;" /></td>
                                        <td style="text-align:right">
                                        </td>
                                        <td ></td>
                                        <td >Grand Total</td>
                                        <td style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtGrandTotal" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top; width:50%;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding:2px;">
                            <telerik:RadGrid ID="grdSummary1" runat="server" AutoGenerateColumns="False" GridLines="None" OnNeedDataSource="grdSummary_NeedDataSource" >
                                <MasterTableView DataKeyNames="ParamedicID" PageSize="10">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ParamedicID" HeaderText="Paramedic ID"
                                            UniqueName="ParamedicID" SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name"
                                            UniqueName="ParamedicName" SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="FeeMedis" HeaderText="Fee Medis"
                                            UniqueName="FeeMedis" SortExpression="FeeMedis" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                                        </telerik:GridNumericColumn >
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width:50%;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding:2px;">
                            <telerik:RadGrid ID="grdSummary2" runat="server" AutoGenerateColumns="False" GridLines="None" OnNeedDataSource="grdSummary_NeedDataSource" >
                                <MasterTableView DataKeyNames="ParamedicID" PageSize="10">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ParamedicID" HeaderText="Paramedic ID"
                                            UniqueName="ParamedicID" SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name"
                                            UniqueName="ParamedicName" SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="FeeMedis" HeaderText="Fee Medis"
                                            UniqueName="FeeMedis" SortExpression="FeeMedis" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                                        </telerik:GridNumericColumn >
                                    </Columns>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
