<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="InvoicingAdditionalSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Receivable.InvoicingAdditionalSearch"
    Title="" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterInvoiceNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtInvoiceDateFrom" runat="server" Width="100px" />
                        </td>
                        <td>
                            &nbsp;-&nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtInvoiceDateTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblInvoiceDueDate" runat="server" Text="Invoice Due Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtInvoiceDueDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
                <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor Name" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterGuarantorName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" MaxLength="250" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

