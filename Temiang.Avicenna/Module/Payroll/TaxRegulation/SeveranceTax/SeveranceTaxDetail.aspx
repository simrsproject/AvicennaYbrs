<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="SeveranceTaxDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.TaxRegulation.SeveranceTaxDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>                
    				<tr style="DISPLAY:none">
                        <td class="label">
                            <asp:Label ID="lblSeveranceTaxID" runat="server" Text="Severance Tax ID"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadNumericTextBox ID="txtSeveranceTaxID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSeveranceTaxID" runat="server" ErrorMessage="Severance Tax ID required."
                    	            ValidationGroup="entry" ControlToValidate="txtSeveranceTaxID" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
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
				            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999"/>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                    	            ValidationGroup="entry" ControlToValidate="txtValidFrom" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
			            </td>
			            <td class="entry">
				            <asp:CheckBox ID="chkIsNPWP" runat="server" Text="NPWP" />
                        </td>
                        <td width="20px">
                       </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLowerLimit" runat="server" Text="Lower Limit"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadNumericTextBox ID="txtLowerLimit" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvLowerLimit" runat="server" ErrorMessage="Lower Limit required."
                    	            ValidationGroup="entry" ControlToValidate="txtLowerLimit" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblUpperLimit" runat="server" Text="Upper Limit"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadNumericTextBox ID="txtUpperLimit" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvUpperLimit" runat="server" ErrorMessage="Upper Limit required."
                    	            ValidationGroup="entry" ControlToValidate="txtUpperLimit" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image4" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                     <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxRate" runat="server" Text="Tax Rate"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadNumericTextBox ID="txtTaxRate" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTaxRate" runat="server" ErrorMessage="Tax Rate required."
                    	            ValidationGroup="entry" ControlToValidate="txtTaxRate" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image5" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxAmount" runat="server" Text="Tax Amount"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTaxAmount" runat="server" ErrorMessage="Tax Amount required."
                    	            ValidationGroup="entry" ControlToValidate="txtTaxAmount" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image6" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmountOfDeduction" runat="server" Text="Amount Of Deduction"></asp:Label>
			            </td>
			            <td class="entry">
				            <telerik:RadNumericTextBox ID="txtAmountOfDeduction" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAmountOfDeduction" runat="server" ErrorMessage="Amount Of Deduction required."
                    	            ValidationGroup="entry" ControlToValidate="txtAmountOfDeduction" 
						            SetFocusOnError="True" Width="100%">
							            <asp:Image ID="Image7" runat="server" SkinID="rfvImage"/>
					            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>        
    </table>
</asp:Content>

