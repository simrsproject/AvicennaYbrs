<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RemunDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.RemunDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
        
            function ExportToExcel() {
                var rno = $find("<%= txtRemunNo.ClientID %>").get_value();
                //alert(rno);

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("ExportToExcelDialog.aspx?rno=" + rno +"&type=BPJS");
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
                                     <tr>
                                        <td ></td>
                                        <td style="text-align:right">
                                        </td>
                                        <td style="padding-left:10px;"></td>
                                        <td >Total Paramedic IGD</td>
                                        <td style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtTotalMedicIGD" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
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
            <td style="vertical-align: top; width:40%;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding:2px;">
 
                            <telerik:RadGrid ID="grdInvoices" runat="server" AutoGenerateColumns="False" GridLines="None" 
                                OnNeedDataSource="grdInvoices_NeedDataSource" OnInsertCommand="grdInvoices_InsertCommand"
                                OnDeleteCommand="grdInvoices_DeleteCommand">
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="RemunID, InvoiceNo" PageSize="10">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="InvoiceNo" HeaderText="No Invoice"
                                            UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Amount" HeaderText="Amount"
                                            UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                                        </telerik:GridNumericColumn >
                                    </Columns>
                                    <EditFormSettings UserControlName="RemunDetailItemInvoice.ascx" EditFormType="WebUserControl">
                                        <EditColumn UniqueName="EditCommandRemunDetailItemInvoice">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                </ClientSettings>
                            </telerik:RadGrid>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                                <legend>Budget Allocation</legend>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td></td>
                                        <td class="entry" style="text-align:right">
                                            <asp:Button ID="btnCalculateBudget" runat="server" Text="Calculate" OnClick="btnCalculateBudget_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >Invoice (@Invoice)</td>
                                        <td class="entry" style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtInvoiceAmount" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >Renumeration Budget Percentage</td>
                                        <td class="entry" style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtBudgetPercentage" Width="150px" ReadOnly="true" 
                                                NumberFormat-DecimalDigits="2" Type="Percent"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >Remuneration Budget Allocation (@Pagu)</td>
                                        <td class="entry" style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtBudgetAllocation" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding:2px;">
                           
                                <telerik:RadGrid ID="grdDeductions" runat="server" AutoGenerateColumns="False" GridLines="None" 
                                    OnNeedDataSource="grdDeductions_NeedDataSource" OnUpdateCommand="grdDeductions_UpdateCommand"
                                    OnItemDataBound="grdDeductions_ItemDataBound">
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="RemunID, SRRemunDeduction" PageSize="10">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="colEdit">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRRemunDeduction" HeaderText="Deduction"
                                                UniqueName="SRRemunDeduction" SortExpression="SRRemunDeduction" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="DeductionName" HeaderText="Deduction Name"
                                                UniqueName="DeductionName" SortExpression="DeductionName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Formula" HeaderText="Formula"
                                                UniqueName="Formula" SortExpression="Formula" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                                                UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                                            </telerik:GridNumericColumn >
                                        </Columns>
                                        <EditFormSettings UserControlName="RemunDetailItemDeduction.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="EditCommandRemunDetailItemDeduction">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <FilterMenu>
                                    </FilterMenu>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Resizing AllowColumnResize="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                      
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                                <legend>Budget Netto</legend>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td >Total Deductions</td>
                                        <td class="entry" style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtTotalDeductions" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >Remuneration Budget</td>
                                        <td class="entry" style="text-align:right">
                                            <telerik:RadNumericTextBox runat="server" ID="txtBudget" Width="150px" ReadOnly="true" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width:60%;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding:2px;">
                            <telerik:RadGrid ID="grdSummary" runat="server" AutoGenerateColumns="False" GridLines="None" OnNeedDataSource="grdSummary_NeedDataSource" >
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
