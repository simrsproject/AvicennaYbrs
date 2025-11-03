<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="CredentialingSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.CredentialingSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboSRProfessionGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRClinicalWorkArea" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRClinicalAuthorityLevel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRClinicalWorkArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRClinicalAuthorityLevel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterTransactionNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Date"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee No"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterEmployeeNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterEmployeeName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterSRProfessionGroup" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRProfessionGroup" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSRProfessionGroup_SelectedIndexChanged" >
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRClinicalWorkArea" runat="server" Text="Work Area"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterSRClinicalWorkArea" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRClinicalWorkArea" runat="server" Width="300px" EnableLoadOnDemand="True"
                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboSRClinicalWorkArea_ItemDataBound"
                    OnItemsRequested="cboSRClinicalWorkArea_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboSRClinicalWorkArea_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRClinicalAuthorityLevel" runat="server" Text="Clinical Authority Level"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterSRClinicalAuthorityLevel" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRClinicalAuthorityLevel" runat="server" Width="300px" EnableLoadOnDemand="True"
                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboSRClinicalAuthorityLevel_ItemDataBound"
                    OnItemsRequested="cboSRClinicalAuthorityLevel_ItemsRequested">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
