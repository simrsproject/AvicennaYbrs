<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="GuarantorSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor ID" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterGuarantorID" runat="server" Width="100px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblGuarantorName" runat="server" Text="Guarantor Name" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterGuarantorName" runat="server" Width="100px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterContactPerson" runat="server" Width="100px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtContactPerson" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStreetName" runat="server" Text="Street Name" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterStreetName" runat="server" Width="100px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStreetName" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="100px"></asp:Label></td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterPhoneNo" runat="server" Width="100px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px">
                </telerik:RadTextBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
