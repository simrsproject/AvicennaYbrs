<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ParamedicFeeRemunDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeRemunDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script language="javascript" type="text/javascript">
        
            var sumInput = null;
            var tempValue = 0.0;

            function Load(sender, args) {
                sumInput = sender;
            }

            function Blur(sender, args) {
                sumInput.set_value(tempValue + sender.get_value());
            }

            function Focus(sender, args) {
                tempValue = sumInput.get_value() - sender.get_value();
            }

            function openDialogDetailRemun(parid) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                var remunno = $find("<%= txtRemunNo.ClientID %>").get_value();
                oWnd.setUrl("ParamedicFeeRemunDetailDialog.aspx?parid=" + parid + "&remunno=" + remunno);

                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
               <%-- if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                     oWnd.argument = 'undefined';
                 }--%>
            }

            function ExportToExcel() {
                var rno = $find("<%= txtRemunNo.ClientID %>").get_value();
                //alert(rno);

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("ExportToExcelDialog.aspx?rno=" + rno + "&type=sum");
                oWnd.show();

                return false;
            }
            function ExportDetailToExcel() {
                var rno = $find("<%= txtRemunNo.ClientID %>").get_value();
                //alert(rno);

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("ExportToExcelDialog.aspx?rno=" + rno + "&type=detail");
                oWnd.show();

                return false;
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Maximize, Move, Close"
        Width="1000px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width:35%;">
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
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Period"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDateStart" runat="server" Width="100px" />
                            <telerik:RadDatePicker ID="txtDateEnd" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvDateStart" runat="server" ErrorMessage="Date start required."
                                ValidationGroup="entry" ControlToValidate="txtDateStart" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvDateEnd" runat="server" ErrorMessage="Date end required."
                                ValidationGroup="entry" ControlToValidate="txtDateEnd" SetFocusOnError="True"
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
            <td style="vertical-align: top; width:35%;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Procedure Fund Allocation"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtFundAllocProcedure" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="P1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtKursPosition" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="P2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtKursInsentif" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Adjustment Factor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAdjustmentFactor" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td colspan="3" class="entry">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" Text="Calculate" ></telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExportExcel" runat="server" Text="Export Summary to Excel" OnClientClick="ExportToExcel();return false;" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExportExcelDetail" runat="server" Text="Export Detail to Excel" OnClientClick="ExportDetailToExcel();return false;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width:30%;">
                <fieldset>
                    <legend>
                        Control
                    </legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Fund Allocation"></asp:Label>
                            </td>
                            <td>:</td>
                            <td class="entry" style="text-align:right">
                                <asp:Label ID="lblFundAllocation" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="20">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Total Allocated"></asp:Label>
                            </td>
                            <td>:</td>
                            <td class="entry" style="text-align:right">
                                <asp:Label ID="lblTotalAllocated" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="20">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Difference"></asp:Label>
                            </td>
                            <td>:</td>
                            <td class="entry" style="text-align:right">
                                <asp:Label ID="lblDifference" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="20">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Summary" PageViewID="pgSum"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Detail" PageViewID="pgDetail" Visible="false">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgSum" runat="server" Selected="true">
            <telerik:RadGrid ID="grdSummary" runat="server" AutoGenerateColumns="False" GridLines="None" OnItemDataBound="grdSummary_ItemDataBound">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParamedicID" PageSize="10">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Paramedic" Name="Paramedic" />
                        <telerik:GridColumnGroup HeaderText="Coorporate Grade" Name="CoorporateGrade" />
                        <telerik:GridColumnGroup HeaderText="Procedure" Name="Procedure" />
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ParamedicID" HeaderText="ID"
                            UniqueName="ParamedicID" SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Paramedic" />

                        <telerik:GridTemplateColumn UniqueName="ParamedicName" HeaderText="Name" ColumnGroupName="Paramedic" >
                            <ItemTemplate>
                                <%# string.Format("<a href='#' onclick='javascript:openDialogDetailRemun(\"{0}\")'>{1}</a>", 
                                    DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                    DataBinder.Eval(Container.DataItem, "ParamedicName"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="SMFName" HeaderText="SMF"
                            UniqueName="SMFName" SortExpression="SMFName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Paramedic" />

                        <telerik:GridBoundColumn DataField="CoorporateGradeLevel" HeaderText="Level" UniqueName="CoorporateGradeLevel"
                            SortExpression="CoorporateGradeLevel" DataFormatString="{0:n0}" ColumnGroupName="CoorporateGrade">
                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="CoorporateGradeValue" HeaderText="Value" ColumnGroupName="CoorporateGrade" >
                            <HeaderStyle HorizontalAlign="Right" Width="50px" />
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtCoorporateGradeValue" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="40px" Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "CoorporateGradeValue")) %>'>
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="PositionFeeValue" HeaderText="Position Fee" SortExpression="PositionFeeValue">
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtPositionFeeValue" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="90px" Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PositionFeeValue")) %>'>
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="InsentifFeeValue" HeaderText="Insentif Fee" SortExpression="InsentifFeeValue">
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtInsentifFeeValue" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="90px" Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "InsentifFeeValue")) %>'>
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="CoefficientSummary" HeaderText="Coefficient" UniqueName="CoefficientSummary"
                            SortExpression="CoefficientSummary" DataFormatString="{0:n6}" ColumnGroupName="Procedure">
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="ProcedureFeeValue" HeaderText="Procedure Fee" UniqueName="ProcedureFeeValue" 
                            SortExpression="ProcedureFeeValue" DataFormatString="{0:n0}" ColumnGroupName="Procedure">
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridCalculatedColumn DataFields="PositionFeeValue,InsentifFeeValue,ProcedureFeeValue" DataType ="System.Decimal" 
                            UniqueName ="Total" HeaderText="Amount" 
                            DataFormatString ="{0:N2}" Expression ="{0}+{1}+{2}">
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridCalculatedColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDetail" runat="server">
            <telerik:RadGrid ID="grdRemunUnmap" runat="server" AutoGenerateColumns="False" GridLines="None" Visible="false">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParamedicID, ItemID, ServiceUnitID, IdiCode" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ParamedicID" HeaderText="Paramedic ID"
                            UniqueName="ParamedicID" SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="IdiCode" HeaderText="Idi Code"
                            UniqueName="IdiCode" SortExpression="IdiCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="IdiName" HeaderText="IDI Name" UniqueName="IdiName"
                            SortExpression="IdiName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ServiceUnitID" HeaderText="Service Unit ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />

                        <telerik:GridBoundColumn DataField="Qty" HeaderText="Qty" UniqueName="Qty"
                            SortExpression="Qty" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Score" HeaderText="Score" UniqueName="Score"
                            SortExpression="Score" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="Rvu" HeaderText="Rvu" UniqueName="Rvu"
                            SortExpression="Rvu" DataFormatString="{0:n0}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="RvuConversion" HeaderText="Rvu Conversion" UniqueName="RvuConversion"
                            SortExpression="RvuConversion" DataFormatString="{0:n6}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>


                        <telerik:GridBoundColumn DataField="Coefficient" HeaderText="Coefficient" UniqueName="Coefficient"
                            SortExpression="Coefficient" DataFormatString="{0:n6}">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
