<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ResearchLetterSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEPK.ResearchLetterSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblLetterDate" runat="server" Text="Letter Date" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <table>
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtLetterDateFrom" runat="server" Width="100px" />
                        </td>
                        <td>to</td>
                        <td>
                            <telerik:RadDatePicker ID="txtLetterDateTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblResearcherName" runat="server" Text="Researcher Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterResearcherName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtResearcherName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLetterNo" runat="server" Text="Letter No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterLetterNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLetterNo" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRResearchDecision" runat="server" Text="Decision" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRResearchDecision" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRResearchInstitution" runat="server" Text="Institution" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRResearchInstitution" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRResearchFaculty" runat="server" Text="Faculty" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRResearchFaculty" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRResearchReviewerName" runat="server" Text="Reviewer Name" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRResearchReviewerName" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td />
        </tr>
    </table>
</asp:Content>
