<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RemunDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Remun.RemunDetail" %>

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
                oWnd.setUrl("RemunDetailDialog.aspx?parid=" + parid + "&remunno=" + remunno);

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
            <td style="vertical-align: top; width:50%;">
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
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Period Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtYear" runat="server" Width="300px" MaxLength="4"/>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year required."
                                ValidationGroup="entry" ControlToValidate="txtYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Period Month"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboMonth" runat="server" Width="300px"></telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ErrorMessage="Month required."
                                ValidationGroup="entry" ControlToValidate="cboMonth" SetFocusOnError="True"
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
            <td style="vertical-align: top; width:49%;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" Text="Position Fund Allocation"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFundAllocPosition" runat="server" Width="200px" MaxLength="20" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td class="label">
                                        <asp:Label ID="Label6" runat="server" Text="Position Rate"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtKursPosition" runat="server" Width="98px" MaxLength="20" ReadOnly="true" />
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
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label7" runat="server" Text="Incentive Fund Allocation"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtFundAllocIncentive" runat="server" Width="200px" MaxLength="20" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td class="label">
                                        <asp:Label ID="Label8" runat="server" Text="Incentive Rate"></asp:Label>
                                    </td>
                                    <td>
                                         <telerik:RadNumericTextBox ID="txtKursInsentif" runat="server" Width="98px" MaxLength="20" ReadOnly="true" />
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
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" Text="Calculate" ></telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExportExcel" runat="server" Text="Export Summary to Excel" OnClientClick="ExportToExcel();return false;" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExportExcelDetail" runat="server" Text="Export Detail to Excel" OnClientClick="ExportDetailToExcel();return false;" Visible="false" />
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
            <td style="vertical-align: top; width:1%;" runat="server" visible="false">
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
            <telerik:RadTab runat="server" Text="Detail" PageViewID="pgDetail" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Summary" PageViewID="pgSum">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
            <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None" OnItemDataBound="grdDetail_ItemDataBound">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="RemunDetailID, PersonID, PositionID" PageSize="10">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Employee" Name="Person" />
                        <telerik:GridColumnGroup HeaderText="Coorporate Grade" Name="CoorporateGrade" HeaderStyle-HorizontalAlign="Center"/>
                        <telerik:GridColumnGroup HeaderText="Incentive" Name="Incentive" HeaderStyle-HorizontalAlign="Center"/>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="EmployeeNumber" HeaderText="No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Person" />

                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Name"
                            UniqueName="EmployeeName" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Person" />

                        <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position"
                            UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Person" />

                        <telerik:GridBoundColumn DataField="CoorporateGradeLevel" HeaderText="Level" UniqueName="CoorporateGradeLevel"
                            SortExpression="CoorporateGradeLevel" DataFormatString="{0:n0}" ColumnGroupName="CoorporateGrade">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="CoorporateGradeValue" HeaderText="Value" ColumnGroupName="CoorporateGrade" >
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtCoorporateGradeValue" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="40px" Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "CoorporateGradeValue")) %>'>
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="CoorporateGradeCoefficient" HeaderText="Coefficient" UniqueName="CoorporateGradeCoefficient"
                            SortExpression="CoorporateGradeCoefficient" DataFormatString="{0:n2}" ColumnGroupName="CoorporateGrade">
                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="PositionFeeValue" HeaderText="Position Fee" SortExpression="PositionFeeValue" >
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtPositionFeeValue" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="90px" Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PositionFeeValue")) %>'>
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="Index" HeaderText="Index" UniqueName="Index"
                            SortExpression="Index" DataFormatString="{0:n6}" ColumnGroupName="Incentive">
                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn UniqueName="InsentifFeeValue" HeaderText="Insentif Fee" SortExpression="InsentifFeeValue" ColumnGroupName="Incentive">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtInsentifFeeValue" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="90px" Value='<%# System.Convert.ToDouble(DataBinder.Eval(Container.DataItem, "InsentifFeeValue")) %>'>
                                </telerik:RadNumericTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridCalculatedColumn DataFields="PositionFeeValue,InsentifFeeValue" DataType ="System.Decimal" 
                            UniqueName ="Total" HeaderText="Amount" 
                            DataFormatString ="{0:N2}" Expression ="{0}+{1}">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
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
        <telerik:RadPageView ID="pgSum" runat="server">
            <telerik:RadGrid ID="grdSummary" runat="server" AutoGenerateColumns="False" GridLines="None">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="CoorporateGradeLevel" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn DataField="CoorporateGradeLevel" HeaderText="Level" UniqueName="CoorporateGradeLevel"
                            SortExpression="CoorporateGradeLevel" DataFormatString="{0:n0}" ColumnGroupName="CoorporateGrade">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="CoorporateGradeValueSum" HeaderText="Total CV" UniqueName="CoorporateGradeValueSum"
                            SortExpression="CoorporateGradeValueSum" DataFormatString="{0:n0}" >
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="CoorporateGradeCoefficient" HeaderText="Coefficient" UniqueName="CoorporateGradeCoefficient"
                            SortExpression="CoorporateGradeCoefficient" DataFormatString="{0:n2}" >
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>

                        <telerik:GridCalculatedColumn DataFields="CoorporateGradeValueSum,CoorporateGradeCoefficient" DataType ="System.Decimal" 
                            UniqueName ="Total" HeaderText="CV x Coefficient" 
                            DataFormatString ="{0:N2}" Expression ="{0}*{1}">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridCalculatedColumn>

                        <telerik:GridBoundColumn DataField="EmployeeCount" HeaderText="Employee Count" UniqueName="EmployeeCount"
                            SortExpression="EmployeeCount" DataFormatString="{0:n0}" >
                            <HeaderStyle HorizontalAlign="Right" />
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
