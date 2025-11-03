<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalaryComponentRuleDefinitionDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryComponentRuleDefinitionDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSalaryComponentRuleDefinition" runat="server" ValidationGroup="SalaryComponentRuleDefinition" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SalaryComponentRuleDefinition"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblSalaryComponentRuleDefinitionID" runat="server" Text="Salary Component Rule Definition ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtSalaryComponentRuleDefinitionID" runat="server"
                Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSalaryComponentRuleDefinitionID" runat="server"
                ErrorMessage="Salary Component Rule Definition ID required." ControlToValidate="txtSalaryComponentRuleDefinitionID"
                SetFocusOnError="True" ValidationGroup="SalaryComponentRuleDefinition" Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                MaxDate="12/31/2999" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                ControlToValidate="txtValidFrom" SetFocusOnError="True" ValidationGroup="SalaryComponentRuleDefinition"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                MaxDate="12/31/2999" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                ControlToValidate="txtValidTo" SetFocusOnError="True" ValidationGroup="SalaryComponentRuleDefinition"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trOrganizationUnit">
        <td class="label">
            <asp:Label ID="lblOrganizationUnitID" runat="server" Text="Organization Unit Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboOrganizationUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboOrganizationUnitID_ItemDataBound"
                OnItemsRequested="cboOrganizationUnitID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trEmployeeStatus">
        <td class="label">
            <asp:Label ID="lblSREmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSREmployeeStatus" runat="server" Width="300px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trPosition">
        <td class="label">
            <asp:Label ID="lblPositionID" runat="server" Text="Position Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionID_ItemDataBound"
                OnItemsRequested="cboPositionID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "PositionCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PositionName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trReligion">
        <td class="label">
            <asp:Label ID="lblSRReligion" runat="server" Text="Religion"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRReligion" runat="server" Width="300px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trEmployee">
        <td class="label">
            <asp:Label ID="lblPersonalID" runat="server" Text="Personal Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPersonalID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPersonalID_ItemDataBound"
                OnItemsRequested="cboPersonalID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trEmploymentType">
        <td class="label">
            <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSREmploymentType" runat="server" Width="300px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trPositionGrade">
        <td class="label">
            <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                OnItemsRequested="cboPositionGradeID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "PositionGradeCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PositionGradeName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvPositionGradeID" runat="server" ErrorMessage="Position Grade Name required."
                ValidationGroup="entry" ControlToValidate="cboPositionGradeID" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trMaritalStatus">
        <td class="label">
            <asp:Label ID="lblSRMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRMaritalStatus" runat="server" Width="300px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trServiceYear">
        <td class="label">
            <asp:Label ID="lblServiceYear" runat="server" Text="Service Year"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtServiceYear" runat="server" Width="100px" />
        </td>
        <td width="20px">
        </td>
        <td>
            ex: 0-5
        </td>
    </tr>
    <tr runat="server" visible="false" id="trSalaryTableNumber">
        <td class="label">
            <asp:Label ID="lblSalaryTableNumber" runat="server" Text="Salary Table Number"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtSalaryTableNumber" runat="server" Width="100px"
                NumberFormat-DecimalDigits="0" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSalaryTableNumber" runat="server" ErrorMessage="Salary Table Number required."
                ControlToValidate="txtSalaryTableNumber" SetFocusOnError="True" ValidationGroup="SalaryComponentRuleDefinition"
                Width="100%">
                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trEmployeeGrade">
        <td class="label">
            <asp:Label ID="lblEmployeeGradeMasterID" runat="server" Text="Employee Grade"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboEmployeeGradeMasterID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboEmployeeGradeMasterID_ItemDataBound"
                OnItemsRequested="cboEmployeeGradeMasterID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "EmployeeGradeCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "EmployeeGradeName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Employee Grade Name required."
                ControlToValidate="cboEmployeeGradeMasterID" SetFocusOnError="True" ValidationGroup="EmployeeGrade"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trNoOfDependent">
        <td class="label">
            <asp:Label ID="lblNoOfDependent" runat="server" Text="No Of Dependent"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtNoOfDependent" runat="server" Width="100px" NumberFormat-DecimalDigits="0" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trAttendanceMatrix">
        <td class="label">
            <asp:Label ID="lblAttedanceMatrixID" runat="server" Text="Attedance Matrix Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboAttedanceMatrixID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboAttedanceMatrixID_ItemDataBound"
                OnItemsRequested="cboAttedanceMatrixID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "AttedanceMatrixName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trEducationLevel">
        <td class="label">
            <asp:Label ID="Label1" runat="server" Text="Education Level"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboEducationLevel" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboEducationLevel_ItemDataBound"
                OnItemsRequested="cboEducationLevel_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trEmployeeType">
        <td class="label">
            <asp:Label ID="lblSREmployeeType" runat="server" Text="Employee Type"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSREmployeeType" runat="server" Width="300px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trServiceUnitID">
        <td class="label">
            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                OnItemsRequested="cboServiceUnitID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblNominalAmount" runat="server" Text="Nominal Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtNominalAmount" runat="server" Width="100px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPercentageAmount" runat="server" Text="Percentage Amount"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtPercentageAmount" runat="server" Type="Percent"
                Width="100px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr runat="server" visible="false" id="trPercentageComponent">
        <td class="label">
            <asp:Label ID="lblPercentageComponent" runat="server" Text="Percentage Component"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboPercentageComponent" runat="server" Width="300px" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPercentageComponent_ItemDataBound"
                OnItemsRequested="cboPercentageComponent_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SalaryComponentRuleDefinition"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="SalaryComponentRuleDefinition" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
