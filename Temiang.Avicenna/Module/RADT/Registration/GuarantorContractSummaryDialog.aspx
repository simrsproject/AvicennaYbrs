<%@ Page Title="Contract Summary" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="GuarantorContractSummaryDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.GuarantorContractSummaryDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="Address" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" MaxLength="10" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorName" runat="server" Text="Guarantor Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGuarantorHeaderID" runat="server" Text="Guarantor Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboGuarantorHeaderID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="false" Enabled="False" >
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "GuarantorID")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblContractSummary" runat="server" Text="Contract Summary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtContractSummary" runat="server" Width="300px" TextMode="MultiLine" Height="250px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                
            </td>
        </tr>
        
    </table>
</asp:Content>