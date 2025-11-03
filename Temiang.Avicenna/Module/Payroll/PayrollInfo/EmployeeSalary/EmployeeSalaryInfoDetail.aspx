<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="EmployeeSalaryInfoDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.PayrollInfo.EmployeeSalaryInfoDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnPageId" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table width="150px">
                    <tr>
                        <td style="vertical-align: top">
                            <fieldset id="FieldSet1" style="width: 135px; min-height: 180px;">
                                <legend>Photo</legend>
                                <asp:Image runat="server" ID="imgPhoto" Width="135px" Height="180px" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Person ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPersonID" runat="server" Width="300px" AutoPostBack="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Person ID required."
                                ValidationGroup="entry" ControlToValidate="txtPersonID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrganizationName" runat="server" Text="Organization Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrganizationName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionTitle" runat="server" Text="Position Title"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionTitle" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionGrade" runat="server" Text="Position Grade"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPositionGrade" runat="server" Width="200px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtGradeYear" runat="server" Width="96px" ReadOnly="true" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSalaryScale">
                        <td class="label">
                            <asp:Label ID="lblSalaryScale" runat="server" Text="Salary Scale"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSalaryScaleCode" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSREmploymentType" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSREmployeeStatus" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trEmployeeTypePayroll" visible="false">
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeTypePayroll" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNPWP" runat="server" Text="NPWP"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNPWP" runat="server" Width="300px" MaxLength="30" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblJamsostekNo" runat="server" Text="BPJS Ketenagakerjaan"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtJamsostekNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>

                    <tr runat="server">
                        <td class="label">
                            <asp:Label ID="lblNoOfDependent" runat="server" Text="Number Of Dependent"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtNoOfDependent" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNoOfDependent" runat="server" ErrorMessage="No Of Dependent required."
                                ValidationGroup="entry" ControlToValidate="txtNoOfDependent" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPaymentFrequency" runat="server" Text="Payment Frequency"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPaymentFrequency" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRPaymentFrequency" runat="server" ErrorMessage="Payment Frequency required."
                                ValidationGroup="entry" ControlToValidate="cboSRPaymentFrequency" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRRemunerationType" runat="server" Text="Remuneration Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRRemunerationType" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRRemunerationType" runat="server" ErrorMessage="Remuneration Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRRemunerationType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRTaxStatus" runat="server" Text="Tax Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRTaxStatus" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRTaxStatus" runat="server" ErrorMessage="Tax Status required."
                                ValidationGroup="entry" ControlToValidate="cboSRTaxStatus" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBankID" runat="server" Text="Bank"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRBankHRD" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBankID" runat="server" ErrorMessage="Bank required."
                                ValidationGroup="entry" ControlToValidate="cboSRBankHRD" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBankAccountNo" runat="server" Text="Bank Account No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBankAccountNo" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBankAccountNo" runat="server" ErrorMessage="Bank Account No required."
                                ValidationGroup="entry" ControlToValidate="txtBankAccountNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBankAccountName" runat="server" Text="Bank Account Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBankAccountName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBankAccountName" runat="server" ErrorMessage="Bank Account Name required."
                                ValidationGroup="entry" ControlToValidate="txtBankAccountName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trIsSalaryManaged" visible="false">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox runat="server" ID="chkIsSalaryManaged" Text="Salary Managed" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Salary Matrix" PageViewID="pgMatrix"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Tax Status" PageViewID="pgTaxStatus" />
            <telerik:RadTab runat="server" Text="Remuneration Position" PageViewID="pgWageScalePosition" />
            <telerik:RadTab runat="server" Text="Incentive Position" PageViewID="pgIncentivePosition" />
            <telerik:RadTab runat="server" Text="Salary History" PageViewID="pgHistory" />
            <telerik:RadTab runat="server" Text="THR History" PageViewID="pgThr" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgMatrix" runat="server" Selected="true">
            <telerik:RadGrid ID="grdEmployeeSalaryMatrix" runat="server" OnNeedDataSource="grdEmployeeSalaryMatrix_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeSalaryMatrix_UpdateCommand"
                OnDeleteCommand="grdEmployeeSalaryMatrix_DeleteCommand" OnInsertCommand="grdEmployeeSalaryMatrix_InsertCommand" OnItemCommand="grdEmployeeSalaryMatrix_ItemCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeSalaryMatrixID">
                    <CommandItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdEmployeeSalaryMatrix.MasterTableView.IsItemInserted %>'>
                                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                        &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                    </asp:LinkButton>&nbsp;&nbsp;
                                </td>
                                <td width="100px">&nbsp;</td>
                                <td>
                                    <asp:Label ID="lblTemplate" runat="server" Text="Copy from template" ForeColor="White" Visible='<%# !grdEmployeeSalaryMatrix.MasterTableView.IsItemInserted %>'></asp:Label>
                                </td>
                                <td>&nbsp;
                            <telerik:RadComboBox ID="cboSalaryTemplateID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSalaryTemplateID_ItemDataBound"
                                OnItemsRequested="cboSalaryTemplateID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                                </td>
                                <td>&nbsp;
                            <asp:LinkButton ID="lbTemplate" runat="server" CommandName="Insert" Visible='<%# !grdEmployeeSalaryMatrix.MasterTableView.IsItemInserted %>'>
                                  <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/download16.png" />
                            </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeSalaryMatrixID"
                            HeaderText="Employee Salary Matrix ID" UniqueName="EmployeeSalaryMatrixID" SortExpression="EmployeeSalaryMatrixID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="SalaryComponentID"
                            HeaderText="Salary Component ID" UniqueName="SalaryComponentID" SortExpression="SalaryComponentID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn DataField="SalaryComponentName" HeaderText="Salary Component Name"
                            UniqueName="SalaryComponentName" SortExpression="SalaryComponentName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="NominalAmount" HeaderText="Nominal Amount"
                            UniqueName="NominalAmount" SortExpression="NominalAmount" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRCurrencyCode" HeaderText="Currency Code"
                            UniqueName="SRCurrencyCode" SortExpression="SRCurrencyCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="130px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeSalaryMatrixDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeSalaryMatrixEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgTaxStatus" runat="server">
            <telerik:RadGrid ID="grdEmployeeTaxStatus" runat="server" OnNeedDataSource="grdEmployeeTaxStatus_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeTaxStatus_UpdateCommand" OnInsertCommand="grdEmployeeTaxStatus_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SPTYear">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="SPTYear"
                            HeaderText="SPT Year" UniqueName="SPTYear" SortExpression="SPTYear"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DecimalDigits="0" />
                        <telerik:GridNumericColumn DataField="TaxStatusName" HeaderText="Tax Status"
                            UniqueName="TaxStatusName" SortExpression="TaxStatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsClosed" HeaderText="Closed"
                            UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeTaxStatusDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeTaxStatusEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgWageScalePosition" runat="server">
            <telerik:RadGrid ID="grdWageScalePosition" runat="server" OnNeedDataSource="grdWageScalePosition_NeedDataSource"
                OnDetailTableDataBind="grdWageScalePosition_DetailTableDataBind" AutoGenerateColumns="False" GridLines="None">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeWageStructureAndScaleID, WageStructureAndScalePositionID">
                    <Columns>
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="EmployeeWageStructureAndScaleID"
                            HeaderText="ID" UniqueName="EmployeeWageStructureAndScaleID" SortExpression="EmployeeWageStructureAndScaleID"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="ValidFrom"
                            HeaderText="Valid From" UniqueName="ValidFrom" SortExpression="ValidFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="EmployeeWorkGroupName"
                            HeaderText="Work Group" UniqueName="EmployeeWorkGroupName" SortExpression="EmployeeWorkGroupName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="EmployeeWorkSubGroupName"
                            HeaderText="Work Sub Group" UniqueName="EmployeeWorkSubGroupName" SortExpression="EmployeeWorkSubGroupName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="EmployeeJobPositionName"
                            HeaderText="Job Position" UniqueName="EmployeeJobPositionName" SortExpression="EmployeeJobPositionName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Points" HeaderText="Points"
                            UniqueName="Points" SortExpression="Points" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="WageStructureAndScalePositionItemID" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="WageStructureAndScalePositionItemID" UniqueName="WageStructureAndScalePositionItemID"
                                    SortExpression="WageStructureAndScalePositionItemID" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SRWageStructureAndScaleType" UniqueName="SRWageStructureAndScaleType"
                                    SortExpression="SRWageStructureAndScaleType" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="WageStructureAndScaleTypeName" HeaderText="Type" UniqueName="WageStructureAndScaleTypeName"
                                    SortExpression="WageStructureAndScaleTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="WageStructureAndScaleName" HeaderText="Wage Structure And Scale" UniqueName="WageStructureAndScaleName"
                                    SortExpression="WageStructureAndScaleName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="WageStructureAndScaleItemName" HeaderText="Wage Structure And Scale Item" UniqueName="WageStructureAndScaleItemName"
                                    SortExpression="WageStructureAndScaleItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="LoadPoint" HeaderText="Load (%)" UniqueName="LoadPoint"
                                    SortExpression="LoadPoint" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="BasePoint" HeaderText="Points" UniqueName="BasePoint"
                                    SortExpression="BasePoint" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Points" HeaderText="Result (Load x Points)" UniqueName="Points"
                                    SortExpression="Points" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgIncentivePosition" runat="server">
            <telerik:RadGrid ID="grdIncentivePosition" runat="server" OnNeedDataSource="grdIncentivePosition_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdIncentivePosition_UpdateCommand" OnInsertCommand="grdIncentivePosition_InsertCommand"
                OnDeleteCommand="grdIncentivePosition_DeleteCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="IncentivePositionID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="IncentivePositionID"
                            HeaderText="ID" UniqueName="IncentivePositionID" SortExpression="IncentivePositionID"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ValidFrom"
                            HeaderText="Valid From" UniqueName="ValidFrom" SortExpression="ValidFrom"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ValidTo"
                            HeaderText="Valid To" UniqueName="ValidFrom" SortExpression="ValidTo"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncentiveServiceUnitGroupName"
                            HeaderText="Incentive Service Unit Group" UniqueName="IncentiveServiceUnitGroupName" SortExpression="IncentiveServiceUnitGroupName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncentivePositionGroupName"
                            HeaderText="Incentive Position Group" UniqueName="IncentivePositionGroupName" SortExpression="IncentivePositionGroupName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="IncentivePositionName"
                            HeaderText="Incentive Position" UniqueName="IncentivePositionName" SortExpression="IncentivePositionName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="IncentivePositionPoints" HeaderText="Points"
                            UniqueName="IncentivePositionPoints" SortExpression="IncentivePositionPoints" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="IncentivePositionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="IncentivePositionEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgHistory" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPayrollPeriod" runat="server" Text="Payroll Period"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                        OnItemsRequested="cboPayrollPeriodID_ItemsRequested" Enabled="False">
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
                                <td width="20px" colspan="2">
                                    <asp:ImageButton ID="btnFilterHistory" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterHistory_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdHistoryItem" runat="server" ShowFooter="false" OnNeedDataSource="grdHistoryItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="False">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="WageTransactionItemID" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SalaryComponentCode"
                            HeaderText="Code" UniqueName="SalaryComponentCode" SortExpression="SalaryComponentID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SalaryComponentName"
                            HeaderText="Salary Component" UniqueName="SalaryComponentName" SortExpression="SalaryComponentName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NominalAmount" HeaderText="Amount"
                            UniqueName="NominalAmount" SortExpression="NominalAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRCurrencyCode" HeaderText="Currency Code"
                            UniqueName="SRCurrencyCode" SortExpression="SRCurrencyCode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CurrencyRate" HeaderText="Currency Rate"
                            UniqueName="CurrencyRate" SortExpression="CurrencyRate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CurrencyAmount" HeaderText="Currency Amount"
                            UniqueName="CurrencyAmount" SortExpression="CurrencyAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgThr" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblThrPayrollPeriodID" runat="server" Text="Payroll Period"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboThrPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                        MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                        OnItemsRequested="cboThrPayrollPeriodID_ItemsRequested" Enabled="False">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Note : Show max 10 items
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px" colspan="2">
                                    <asp:ImageButton ID="btnFilterThr" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterThr_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdThr" runat="server" ShowFooter="false" OnNeedDataSource="grdThr_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="False">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="WageTransactionItemID" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SalaryComponentCode"
                            HeaderText="Code" UniqueName="SalaryComponentCode" SortExpression="SalaryComponentID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SalaryComponentName"
                            HeaderText="Salary Component" UniqueName="SalaryComponentName" SortExpression="SalaryComponentName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NominalAmount" HeaderText="Amount"
                            UniqueName="NominalAmount" SortExpression="NominalAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRCurrencyCode" HeaderText="Currency Code"
                            UniqueName="SRCurrencyCode" SortExpression="SRCurrencyCode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CurrencyRate" HeaderText="Currency Rate"
                            UniqueName="CurrencyRate" SortExpression="CurrencyRate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CurrencyAmount" HeaderText="Currency Amount"
                            UniqueName="CurrencyAmount" SortExpression="CurrencyAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                        <telerik:GridTemplateColumn />
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
